using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Radius
{
    /// <summary>
    /// Provides a high-throughput, thread-safe UDP client for sending and receiving RADIUS
    /// protocol packets, as defined in RFC 2865 (Authentication) and RFC 2866 (Accounting).
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="RadiusClient"/> manages the full RADIUS request/response lifecycle:
    /// packet construction, transmission with configurable retry behaviour,
    /// and cryptographic authenticator verification.
    /// </para>
    /// <para>
    /// <b>Typical usage sequence for an Access-Request:</b>
    /// </para>
    /// <list type="number">
    ///   <item><description>Construct a <see cref="RadiusClient"/> with the server hostname, shared secret, and optional port/timeout overrides.</description></item>
    ///   <item><description>Call <see cref="CreatePacket"/> to create a new <see cref="RadiusPacket"/> of the desired type.</description></item>
    ///   <item><description>Populate attributes via <see cref="RadiusPacket.SetAttribute"/>.</description></item>
    ///   <item><description>Optionally call <see cref="RadiusPacket.SetMessageAuthenticator"/> (required for EAP; recommended for all Access-Request packets per RFC 3579 §3.2).</description></item>
    ///   <item><description>Call <see cref="RadiusPacket.SetAuthenticator"/> to finalise the packet.</description></item>
    ///   <item><description>Transmit via <see cref="SendAndReceivePacketAsync"/> and await the response.</description></item>
    ///   <item><description>Verify the Response Authenticator of the received packet via <see cref="VerifyAuthenticator"/> before trusting its contents.</description></item>
    /// </list>
    /// <para>
    /// <b>Lifecycle:</b> This class implements <see cref="IDisposable"/>. Call
    /// <see cref="Dispose()"/> (or use a <c>using</c> statement) to release the underlying
    /// UDP sockets when the client is no longer needed. Failing to dispose will leave
    /// sockets open until the finaliser runs.
    /// </para>
    /// <para>
    /// <b>Thread safety:</b> All public members of this class are thread-safe.
    /// <see cref="SendAndReceivePacketAsync"/> may be called concurrently from multiple
    /// threads; each call uses the shared pre-connected UDP socket and correlates
    /// responses by RADIUS Identifier (RFC 2865 §3) via an internal demultiplexer loop.
    /// The <see cref="SocketTimeout"/> property uses <see cref="Volatile"/> reads and writes
    /// for safe cross-thread visibility.
    /// </para>
    /// <para>
    /// <b>High throughput:</b> Unlike a naive implementation that creates a new
    /// <see cref="UdpClient"/> per request, this class maintains one pre-connected
    /// <see cref="Socket"/> for the lifetime of the instance and a single background
    /// receive loop that demultiplexes responses to their corresponding callers.
    /// This eliminates per-request socket creation overhead, ephemeral port exhaustion,
    /// <c>TIME_WAIT</c> socket accumulation under high load, and cross-caller datagram
    /// theft that occurs when multiple <c>ReceiveFromAsync</c> calls compete on the same socket.
    /// </para>
    /// <para>
    /// <b>IPv4 and IPv6</b> are both supported. The address family is resolved from
    /// <c>hostName</c> during construction. When DNS returns multiple addresses, one is
    /// selected at random using <see cref="Random.Shared"/>.
    /// </para>
    /// </remarks>
    public sealed class RadiusClient : IDisposable
    {
        #region Constants

        /// <summary>Default number of send attempts (initial + retries) per request.</summary>
        private const int DEFAULT_ATTEMPTS = 3;

        /// <summary>Exactly one attempt — no retries. Used for Status-Server (RFC 5997 §6).</summary>
        private const int NO_RETRIES = 1;

        /// <summary>Default UDP port for RADIUS authentication (RFC 2865 §3).</summary>
        private const int DEFAULT_AUTH_PORT = 1812;

        /// <summary>Default UDP port for RADIUS accounting (RFC 2866 §3).</summary>
        private const int DEFAULT_ACCT_PORT = 1813;

        /// <summary>Default socket receive timeout in milliseconds.</summary>
        private const int DEFAULT_SOCKET_TIMEOUT = 3000;

        /// <summary>Maximum RADIUS packet size in bytes (RFC 2865 §3).</summary>
        private const int MAX_PACKET_SIZE = 4096;

        /// <summary>Byte offset of the Identifier field in the RADIUS packet header (RFC 2865 §3).</summary>
        private const int IDENTIFIER_OFFSET = 1;

        /// <summary>
        /// Default OS-level socket receive buffer size in bytes.
        /// Set to 1 MiB to reduce kernel-side packet drops under burst load.
        /// Linux defaults (~212 KB) can cause silent drops at high RPS.
        /// </summary>
        private const int DEFAULT_RECEIVE_BUFFER_SIZE = 1 << 20;

        /// <summary>
        /// Default OS-level socket send buffer size in bytes (1 MiB).
        /// </summary>
        private const int DEFAULT_SEND_BUFFER_SIZE = 1 << 20;

        #endregion

        #region Private

        /// <summary>
        /// Strict ASCII encoding that rejects non-ASCII characters with an exception
        /// rather than silently substituting '?'. Used to encode the shared secret,
        /// ensuring fail-fast behaviour for secrets containing non-ASCII code points.
        /// </summary>
        private static readonly Encoding StrictAscii =
            Encoding.GetEncoding("us-ascii", EncoderFallback.ExceptionFallback, DecoderFallback.ReplacementFallback);

        /// <summary>The shared secret for authenticator computation (RFC 2865 §3).</summary>
        private readonly byte[] _secret;

        /// <summary>The resolved remote IP address of the RADIUS server.</summary>
        private readonly IPAddress _remoteAddress;

        /// <summary>The remote endpoint for authentication/authorisation packets.</summary>
        private readonly IPEndPoint _authEndPoint;

        /// <summary>The remote endpoint for accounting packets.</summary>
        private readonly IPEndPoint _acctEndPoint;

        /// <summary>
        /// The pre-connected UDP socket used for all send/receive operations.
        /// Connected to the authentication endpoint; accounting packets are sent
        /// via <see cref="Socket.SendToAsync(ReadOnlyMemory{byte}, SocketFlags, EndPoint, CancellationToken)"/> to the accounting endpoint.
        /// </summary>
        private readonly Socket _socket;

        /// <summary>
        /// Socket receive timeout in milliseconds. Accessed via <see cref="Volatile"/>
        /// to ensure cross-thread visibility without locks.
        /// </summary>
        private volatile int _timeout;

        /// <summary>Tracks whether <see cref="Dispose()"/> has been called.</summary>
        private int _disposed;

        /// <summary>
        /// Pending response demultiplexer keyed by RADIUS Identifier (0–255).
        /// Each entry holds a <see cref="TaskCompletionSource{TResult}"/> that the
        /// background receive loop completes when a matching response arrives.
        /// The receive loop uses a peek-then-remove strategy: it first checks for a
        /// pending entry via <see cref="ConcurrentDictionary{TKey,TValue}.TryGetValue"/>,
        /// validates the packet structurally, and only then atomically removes the entry
        /// via <see cref="ConcurrentDictionary{TKey,TValue}.TryRemove(TKey, out TValue)"/> before completing
        /// the TCS. This prevents malformed packets from ejecting pending requests.
        /// </summary>
        private readonly ConcurrentDictionary<byte, TaskCompletionSource<RadiusPacket>> _pendingRequests = new();

        /// <summary>
        /// Cancellation source for the background receive loop.
        /// Cancelled during <see cref="Dispose(bool)"/> to terminate the loop.
        /// </summary>
        private readonly CancellationTokenSource _receiveCts = new();

        /// <summary>
        /// The background receive loop task. Started lazily on first send via
        /// <see cref="Interlocked.CompareExchange{T}(ref T, T, T)"/> and runs until
        /// disposal. A single loop avoids the race condition where multiple concurrent
        /// <c>ReceiveFromAsync</c> calls steal each other's datagrams.
        /// </summary>
        private Task? _receiveLoopTask;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the receive timeout, in milliseconds, used by
        /// <see cref="SendAndReceivePacketAsync"/> for each attempt.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When a response is not received within this window the attempt is treated as a
        /// transient timeout. <see cref="SendAndReceivePacketAsync"/> will retry up to
        /// <c>maxAttempts</c> times before returning <see langword="null"/>.
        /// </para>
        /// <para>
        /// The default value is <c>3000</c> ms (3 seconds).
        /// </para>
        /// <para>
        /// This property is thread-safe. Reads and writes use <see cref="Volatile"/>
        /// semantics for cross-thread visibility.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the value being set is less than or equal to zero.
        /// </exception>
        /// <value>
        /// A positive integer representing the receive timeout in milliseconds.
        /// </value>
        public int SocketTimeout
        {
            get { return _timeout; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value,
                        "Socket timeout must be greater than zero.");

                _timeout = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of <see cref="RadiusClient"/> with the specified
        /// server address, shared secret, and optional transport configuration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The constructor eagerly resolves <paramref name="hostName"/> to an IP address
        /// and creates a pre-connected UDP socket. This socket is reused for all subsequent
        /// <see cref="SendAndReceivePacketAsync"/> calls, eliminating per-request socket
        /// creation overhead.
        /// </para>
        /// <para>
        /// When <paramref name="hostName"/> resolves to multiple addresses (e.g. a DNS
        /// round-robin record), one is selected at random using <see cref="Random.Shared"/>.
        /// </para>
        /// <para>
        /// When <paramref name="localEndPoint"/> is supplied, the underlying socket is
        /// bound to that endpoint before connecting. This is useful on multi-homed hosts
        /// or when policy routing requires a specific source address.
        /// </para>
        /// </remarks>
        /// <param name="hostName">
        /// The hostname or IP address (IPv4 or IPv6) of the RADIUS server.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <param name="sharedSecret">
        /// The shared secret negotiated with the RADIUS server, used to compute and verify
        /// all Authenticator and Message-Authenticator fields. Must not be
        /// <see langword="null"/> or empty.
        /// </param>
        /// <param name="sockTimeout">
        /// The receive timeout in milliseconds for each send/receive attempt.
        /// Must be greater than zero. Defaults to <c>3000</c> ms.
        /// </param>
        /// <param name="authPort">
        /// The UDP port for authentication and authorisation packets (RFC 2865 §3).
        /// Must be in the range [1, 65535]. Defaults to <c>1812</c>.
        /// </param>
        /// <param name="acctPort">
        /// The UDP port for accounting packets (RFC 2866 §3).
        /// Must be in the range [1, 65535]. Defaults to <c>1813</c>.
        /// </param>
        /// <param name="localEndPoint">
        /// An optional local <see cref="IPEndPoint"/> to bind the UDP socket to.
        /// Pass <see langword="null"/> (the default) to let the OS select the
        /// source address and port automatically.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="hostName"/> or <paramref name="sharedSecret"/>
        /// is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="hostName"/> or <paramref name="sharedSecret"/>
        /// is empty.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="sockTimeout"/> is less than or equal to zero,
        /// or when <paramref name="authPort"/> or <paramref name="acctPort"/> is outside
        /// the valid UDP port range [1, 65535].
        /// </exception>
        /// <exception cref="SocketException">
        /// Thrown when DNS resolution fails or the socket cannot be created/bound.
        /// </exception>
        public RadiusClient(
            string hostName,
            string sharedSecret,
            int sockTimeout = DEFAULT_SOCKET_TIMEOUT,
            int authPort = DEFAULT_AUTH_PORT,
            int acctPort = DEFAULT_ACCT_PORT,
            IPEndPoint? localEndPoint = null)
        {
            ArgumentNullException.ThrowIfNull(hostName);
            ArgumentNullException.ThrowIfNull(sharedSecret);

            if (hostName.Length == 0)
                throw new ArgumentException("Host name must not be empty.", nameof(hostName));

            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            if (sockTimeout <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(sockTimeout), sockTimeout,
                    "Socket timeout must be greater than zero.");

            if (authPort is < 1 or > 65535)
                throw new ArgumentOutOfRangeException(
                    nameof(authPort), authPort,
                    "Authentication port must be in the range [1, 65535].");

            if (acctPort is < 1 or > 65535)
                throw new ArgumentOutOfRangeException(
                    nameof(acctPort), acctPort,
                    "Accounting port must be in the range [1, 65535].");

            // StrictAscii encoding deliberately rejects non-ASCII characters rather than
            // silently substituting '?'. While RFC 2865 §3 defines the shared secret as
            // an "octet string", virtually all RADIUS deployments use printable ASCII.
            // Fail-fast here prevents subtle authenticator mismatches at runtime.
            _secret = StrictAscii.GetBytes(sharedSecret);
            _timeout = sockTimeout;

            // Eagerly resolve the hostname to avoid per-request DNS overhead.
            // For IP address literals, TryParse is allocation-free.
            if (!IPAddress.TryParse(hostName, out IPAddress? resolved))
            {
                // Synchronous DNS resolution during construction — callers who need
                // async resolution should resolve the address themselves and pass the
                // IP address string directly.
                IPHostEntry host = Dns.GetHostEntry(hostName);

                resolved = host.AddressList.Length switch
                {
                    0 => throw new SocketException((int)SocketError.HostNotFound),
                    1 => host.AddressList[0],
                    _ => host.AddressList[Random.Shared.Next(host.AddressList.Length)]
                };
            }

            _remoteAddress = resolved;
            _authEndPoint = new IPEndPoint(resolved, authPort);
            _acctEndPoint = new IPEndPoint(resolved, acctPort);

            // Create a single long-lived UDP socket. Using Socket directly (rather than
            // UdpClient) avoids the UdpClient wrapper overhead and gives full control
            // over socket options.
            _socket = new Socket(resolved.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                // On Windows, sending UDP to a closed port causes an ICMP Port Unreachable
                // message that triggers WSAECONNRESET on the next ReceiveFrom. This IOControl
                // disables that behaviour, preventing spurious SocketExceptions in the
                // receive loop. Safe to call on all platforms (no-op on non-Windows).
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    const int SIO_UDP_CONNRESET = unchecked((int)0x9800000C);
                    _socket.IOControl(SIO_UDP_CONNRESET, [0], null);
                }

                // Increase OS-level socket buffers to reduce kernel-side packet drops
                // under burst load. Linux defaults (~212 KB) are often insufficient for
                // high-RPS RADIUS traffic.
                _socket.ReceiveBufferSize = DEFAULT_RECEIVE_BUFFER_SIZE;
                _socket.SendBufferSize = DEFAULT_SEND_BUFFER_SIZE;

                if (localEndPoint is not null)
                {
                    if (localEndPoint.AddressFamily != resolved.AddressFamily)
                        throw new ArgumentException(
                            $"Local endpoint address family ({localEndPoint.AddressFamily}) " +
                            $"does not match the resolved server address family ({resolved.AddressFamily}).",
                            nameof(localEndPoint));

                    _socket.Bind(localEndPoint);
                }

                // Connect to the auth endpoint so that Send/Receive (without endpoint)
                // can be used for the common auth path. Accounting packets use SendTo
                // with the accounting endpoint.
                _socket.Connect(_authEndPoint);
            }
            catch
            {
                _socket.Dispose();
                throw;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Ensures the background receive loop is running. Called once before the first send.
        /// Uses <see cref="Interlocked.CompareExchange{T}(ref T, T, T)"/> to guarantee
        /// exactly-once start semantics without a lock object.
        /// </summary>
        private void EnsureReceiveLoopStarted()
        {
            if (Volatile.Read(ref _receiveLoopTask) is not null)
                return;

            // Create the task eagerly, then atomically publish it. If another thread
            // wins the race, the duplicate task is never observed — Task.Run hasn't
            // been called yet because we create the Task via CompareExchange.
            Task newTask = Task.Run(() => ReceiveLoopAsync(_receiveCts.Token));
            Interlocked.CompareExchange(ref _receiveLoopTask, newTask, null);
        }

        /// <summary>
        /// Background receive loop that reads datagrams from the shared socket and
        /// demultiplexes them to pending callers by RADIUS Identifier.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A single receive loop eliminates the race condition where multiple concurrent
        /// <c>ReceiveFromAsync</c> calls compete for datagrams on the same socket. The
        /// kernel delivers each datagram to exactly one <c>ReceiveFromAsync</c> call;
        /// without a demultiplexer, Thread B can receive Thread A's response and discard
        /// it due to Identifier mismatch, causing Thread A to time out.
        /// </para>
        /// <para>
        /// The loop uses a peek-then-remove strategy: it first checks for a pending
        /// request via <see cref="ConcurrentDictionary{TKey,TValue}.TryGetValue"/>,
        /// then validates the packet structurally, and only then atomically removes
        /// the entry via <see cref="ConcurrentDictionary{TKey,TValue}.TryRemove(TKey, out TValue)"/>
        /// before completing the <see cref="TaskCompletionSource{TResult}"/>. This
        /// ensures that a malformed packet with a matching Identifier byte does not
        /// eject the pending request and orphan the caller.
        /// </para>
        /// <para>
        /// This loop runs until the <paramref name="cancellationToken"/> is cancelled
        /// (during disposal). All non-cancellation exceptions are swallowed to prevent
        /// the loop from terminating on transient socket errors.
        /// </para>
        /// </remarks>
        private async Task ReceiveLoopAsync(CancellationToken cancellationToken)
        {
            byte[] receiveBuffer = GC.AllocateUninitializedArray<byte>(MAX_PACKET_SIZE);

            // Use a wildcard endpoint as the ReceiveFromAsync source address placeholder.
            // ReceiveFromAsync overwrites this with the actual sender address on each call.
            // Using a throwaway endpoint (rather than _authEndPoint) avoids semantic confusion
            // and prevents accidental mutation of the real endpoint reference.
            EndPoint remoteEndPoint = new IPEndPoint(
                _remoteAddress.AddressFamily == AddressFamily.InterNetwork
                    ? IPAddress.Any
                    : IPAddress.IPv6Any,
                0);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    SocketReceiveFromResult result = await _socket.ReceiveFromAsync(
                        receiveBuffer,
                        SocketFlags.None,
                        remoteEndPoint,
                        cancellationToken).ConfigureAwait(false);

                    // Validate source endpoint — only accept datagrams from the server.
                    if (result.RemoteEndPoint is not IPEndPoint source ||
                        !source.Address.Equals(_remoteAddress) ||
                        source.Port != _authEndPoint.Port && source.Port != _acctEndPoint.Port)
                    {
                        continue;
                    }

                    // Quick Identifier extraction on the raw buffer before constructing
                    // a RadiusPacket — avoids parsing overhead for unmatched responses.
                    if (result.ReceivedBytes < 20)
                        continue;

                    byte identifier = receiveBuffer[IDENTIFIER_OFFSET];

                    // Peek first: check whether anyone is waiting for this Identifier
                    // without removing the entry. This ensures that structurally invalid
                    // packets (which pass the raw Identifier check but fail full parsing)
                    // do not eject the pending request and orphan the caller.
                    if (!_pendingRequests.TryGetValue(identifier, out TaskCompletionSource<RadiusPacket>? tcs))
                        continue;

                    // Construct a RadiusPacket directly from the receive buffer span.
                    // The span constructor copies exactly the received bytes into a
                    // right-sized internal array, avoiding the intermediate .ToArray()
                    // allocation that the byte[] constructor would require at the call site.
                    RadiusPacket receivedPacket = new(
                        receiveBuffer.AsSpan(0, result.ReceivedBytes));

                    // Validate the parsed packet before removing the pending entry.
                    // If validation fails, the entry remains in the dictionary so the
                    // caller can still receive a valid response on a subsequent datagram.
                    if (!receivedPacket.Valid)
                        continue;

                    // Atomically remove the pending request and complete the caller's task.
                    // TryRemove guards against a narrow race where two valid responses for
                    // the same Identifier arrive back-to-back: only the first removal wins
                    // and delivers the result; the second is silently discarded.
                    if (_pendingRequests.TryRemove(identifier, out tcs))
                    {
                        // RunContinuationsAsynchronously (set at TCS creation) prevents the
                        // caller's continuation from hijacking this receive loop thread.
                        tcs.TrySetResult(receivedPacket);
                    }
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    // Disposal requested — exit the loop cleanly.
                    break;
                }
                catch (ObjectDisposedException)
                {
                    // Socket disposed during shutdown — exit the loop cleanly.
                    break;
                }
                catch (SocketException)
                {
                    // Transient socket error (e.g. ICMP port unreachable on platforms
                    // where SIO_UDP_CONNRESET is not available). Continue receiving.
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new outbound RADIUS packet of the specified type with a
        /// cryptographically random Identifier, as defined in RFC 2865 §3.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This factory method produces a packet that is initialised with the 20-byte
        /// header only. The packet is ready for attribute population via
        /// <see cref="RadiusPacket.SetAttribute"/>. The Authenticator field is left
        /// zeroed until <see cref="RadiusPacket.SetAuthenticator"/> is called.
        /// </para>
        /// <para>
        /// The typical outbound packet construction sequence is:
        /// </para>
        /// <list type="number">
        ///   <item><description>Call <see cref="CreatePacket"/> to obtain a new packet.</description></item>
        ///   <item><description>Append all required attributes via <see cref="RadiusPacket.SetAttribute"/>.</description></item>
        ///   <item><description>Optionally call <see cref="RadiusPacket.SetMessageAuthenticator"/> (required for EAP and recommended for all Access-Request packets per RFC 3579 §3.2).</description></item>
        ///   <item><description>Call <see cref="RadiusPacket.SetAuthenticator"/> to finalise the packet.</description></item>
        ///   <item><description>Transmit via <see cref="SendAndReceivePacketAsync"/>.</description></item>
        /// </list>
        /// </remarks>
        /// <param name="packetType">
        /// The RADIUS packet code identifying the packet type (RFC 2865 §3, octet 1).
        /// Must be a defined member of <see cref="RadiusCode"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="RadiusPacket"/> instance with <see cref="RadiusPacket.PacketType"/>
        /// set to <paramref name="packetType"/>, a cryptographically random
        /// <see cref="RadiusPacket.Identifier"/>, and <see cref="RadiusPacket.Valid"/> set to
        /// <see langword="true"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="packetType"/> is not a defined member of <see cref="RadiusCode"/>.
        /// </exception>
        public RadiusPacket CreatePacket(RadiusCode packetType)
        {
            if (!Enum.IsDefined(packetType))
                throw new ArgumentOutOfRangeException(
                    nameof(packetType),
                    packetType,
                    $"'{packetType}' is not a defined {nameof(RadiusCode)} value.");

            return new RadiusPacket(packetType);
        }

        /// <summary>
        /// Transmits a RADIUS packet to the configured server and waits for a matching
        /// response, retrying on timeout up to <paramref name="maxAttempts"/> total attempts.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The destination port is selected automatically based on the packet type:
        /// <see cref="RadiusCode.ACCOUNTING_REQUEST"/> packets are sent to the accounting port
        /// (<c>1813</c> by default); all other packet types are sent to the authentication port
        /// (<c>1812</c> by default).
        /// </para>
        /// <para>
        /// Responses are demultiplexed by RADIUS Identifier (RFC 2865 §3, octet 2) via a
        /// single background receive loop. This ensures that concurrent callers never steal
        /// each other's responses, which would occur if multiple <c>ReceiveFromAsync</c> calls
        /// competed on the same socket.
        /// </para>
        /// <para>
        /// A single <see cref="TaskCompletionSource{TResult}"/> is used across all retry
        /// attempts for a given request. This eliminates the race condition where a response
        /// arrives between the timeout firing and a replacement TCS being registered —
        /// the same pattern used by DNS retransmission implementations.
        /// </para>
        /// <para>
        /// A response packet is accepted only if it passes structural validation
        /// (<see cref="RadiusPacket.Valid"/> is <see langword="true"/>) and Identifier
        /// matching. Callers are responsible for verifying the Response Authenticator
        /// on the returned packet using <see cref="VerifyAuthenticator"/> before trusting
        /// its contents.
        /// </para>
        /// <para>
        /// Each attempt uses a per-attempt timeout derived from <see cref="SocketTimeout"/>,
        /// linked to the caller's <paramref name="cancellationToken"/>, ensuring the receive
        /// never hangs indefinitely regardless of platform.
        /// </para>
        /// <para>
        /// <b>Identifier uniqueness:</b> The RADIUS Identifier is an 8-bit field, limiting
        /// the number of concurrent in-flight requests to 256 (RFC 2865 §3). The
        /// <see cref="RadiusPacket.Identifier"/> of <paramref name="packet"/> must be unique
        /// among all currently outstanding requests on this <see cref="RadiusClient"/> instance.
        /// If a request with the same Identifier is already in flight, an
        /// <see cref="InvalidOperationException"/> is thrown. Use <see cref="CreatePacket"/>
        /// to generate packets with cryptographically random Identifiers, which provides
        /// sufficient collision resistance for typical workloads.
        /// </para>
        /// </remarks>
        /// <param name="packet">
        /// The fully constructed and signed RADIUS packet to transmit. Must not be
        /// <see langword="null"/> and must have <see cref="RadiusPacket.Valid"/> set to
        /// <see langword="true"/>. The packet must have had <see cref="RadiusPacket.SetAuthenticator"/>
        /// (and optionally <see cref="RadiusPacket.SetMessageAuthenticator"/>) called before
        /// being passed here.
        /// </param>
        /// <param name="maxAttempts">
        /// The total number of send attempts to make, including the first. A value of
        /// <c>1</c> sends exactly once with no retries. Use <c>1</c> for packet types
        /// that must not be retransmitted, such as Status-Server (RFC 5997 §6).
        /// Defaults to <c>3</c>.
        /// </param>
        /// <param name="cancellationToken">
        /// A token that can be used to cancel any pending send or receive operation.
        /// Defaults to <see cref="CancellationToken.None"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that resolves to the first structurally valid
        /// <see cref="RadiusPacket"/> with a matching Identifier received from the server,
        /// or <see langword="null"/> if all attempts timed out without a valid response.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="packet"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="packet"/> has <see cref="RadiusPacket.Valid"/> set to
        /// <see langword="false"/>, or when <paramref name="maxAttempts"/> is less than 1.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when a request with the same <see cref="RadiusPacket.Identifier"/> is
        /// already in flight on this instance. RADIUS Identifiers must be unique among
        /// outstanding requests (RFC 2865 §3).
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// Thrown when this instance has been disposed.
        /// </exception>
        /// <exception cref="SocketException">
        /// Thrown when a non-timeout network error occurs.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown when <paramref name="cancellationToken"/> is cancelled before a response
        /// is received.
        /// </exception>
        public async Task<RadiusPacket?> SendAndReceivePacketAsync(
            RadiusPacket packet,
            int maxAttempts = DEFAULT_ATTEMPTS,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(packet);
            ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);

            if (!packet.Valid)
                throw new ArgumentException(
                    "The packet is not valid. Ensure SetAuthenticator has been called.", nameof(packet));

            if (maxAttempts < 1)
                throw new ArgumentException(
                    $"{nameof(maxAttempts)} must be at least 1.", nameof(maxAttempts));

            byte[] rawData = packet.RawData;
            byte expectedIdentifier = packet.Identifier;

            // Determine the target endpoint for this packet type.
            bool isAccounting = packet.PacketType == RadiusCode.ACCOUNTING_REQUEST;

            // Ensure the background receive loop is running before sending.
            EnsureReceiveLoopStarted();

            // Register a single TaskCompletionSource for this Identifier. The same TCS
            // is reused across all retry attempts — this eliminates the race condition
            // where a response arrives between a timeout firing and a replacement TCS
            // being registered (the classic retry-demux race). The receive loop can
            // complete the TCS at any point, regardless of which send attempt triggered
            // the server's response.
            //
            // Only one in-flight request per Identifier is supported (RADIUS Identifier
            // is 8 bits, so at most 256 concurrent requests per RFC 2865 §3).
            TaskCompletionSource<RadiusPacket> tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

            if (!_pendingRequests.TryAdd(expectedIdentifier, tcs))
            {
                // Another request with the same Identifier is already in flight.
                // This is a caller error — RADIUS Identifiers must be unique among
                // outstanding requests (RFC 2865 §3).
                throw new InvalidOperationException(
                    $"A request with Identifier {expectedIdentifier} is already in flight. " +
                    "RADIUS Identifiers must be unique among outstanding requests.");
            }

            try
            {
                for (int attempt = 0; attempt < maxAttempts; attempt++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    // Create a per-attempt timeout CTS linked to the caller's token.
                    int timeoutMs = _timeout;
                    using CancellationTokenSource attemptCts =
                        CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    attemptCts.CancelAfter(timeoutMs);

                    try
                    {
                        // Send the packet. For accounting, use SendTo with the accounting
                        // endpoint; for all other types, the socket is pre-connected to the
                        // auth endpoint so Send (without endpoint) is used.
                        if (isAccounting)
                        {
                            await _socket.SendToAsync(rawData, SocketFlags.None, _acctEndPoint, attemptCts.Token).ConfigureAwait(false);
                        }
                        else
                        {
                            await _socket.SendAsync(rawData, SocketFlags.None, attemptCts.Token).ConfigureAwait(false);
                        }

                        // Wait for the receive loop to complete our TCS, or for the
                        // per-attempt timeout to fire. The same TCS is awaited across
                        // retries — if the server responds late (after a timeout but
                        // before the next send), the response is still captured.
                        RadiusPacket result = await tcs.Task.WaitAsync(attemptCts.Token).ConfigureAwait(false);
                        return result;
                    }
                    catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
                    {
                        // The per-attempt CTS fired (timeout), not the caller's token.
                        // Treat as transient and retry if attempts remain. The same TCS
                        // remains registered in _pendingRequests — no replacement needed.
                    }
                    catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
                    {
                        // Platform-specific timeout — treat as transient.
                    }
                }

                return null;
            }
            finally
            {
                // Safety-net cleanup: remove the pending entry if the receive loop
                // hasn't already done so (e.g. all attempts timed out, or the caller's
                // token was cancelled). TryRemove is idempotent.
                _pendingRequests.TryRemove(expectedIdentifier, out _);
            }
        }

        /// <summary>
        /// Sends a RADIUS Status-Server packet to the configured server to test reachability,
        /// as defined in RFC 5997.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The Status-Server packet is constructed as follows (RFC 5997 §6):
        /// </para>
        /// <list type="number">
        ///   <item><description>A new <see cref="RadiusCode.SERVER_STATUS"/> packet is created with a random Identifier.</description></item>
        ///   <item><description>A Message-Authenticator attribute (Type 80) is appended and computed per RFC 3579 §3.2.</description></item>
        ///   <item><description>The Request Authenticator is computed and written per RFC 2865 §3.</description></item>
        /// </list>
        /// <para>
        /// Per RFC 5997 §6, Status-Server packets <b>must not</b> be retransmitted.
        /// This method therefore makes exactly one send attempt.
        /// </para>
        /// <para>
        /// A <see langword="null"/> return value indicates the server did not respond within
        /// the configured <see cref="SocketTimeout"/> period. Callers should treat
        /// <see langword="null"/> as a timeout or unreachable server condition.
        /// </para>
        /// </remarks>
        /// <param name="cancellationToken">
        /// A token that can be used to cancel the operation before it completes.
        /// Defaults to <see cref="CancellationToken.None"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that resolves to the received <see cref="RadiusPacket"/>
        /// (typically an Access-Accept or Status-Server response) if the server replied within
        /// the timeout; or <see langword="null"/> if no valid response was received.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        /// Thrown when this instance has been disposed.
        /// </exception>
        /// <exception cref="SocketException">
        /// Thrown when a network-level error occurs that is not a simple timeout.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown when <paramref name="cancellationToken"/> is cancelled.
        /// </exception>
        public Task<RadiusPacket?> PingAsync(CancellationToken cancellationToken = default)
        {
            // Fail fast before constructing a packet if the client is already disposed.
            ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);

            // Per RFC 5997 §6: the Status-Server packet requires a Message-Authenticator
            // attribute. SetMessageAuthenticator must be called BEFORE SetAuthenticator
            // so that the authenticator hash is computed over the final packet content,
            // including the Message-Authenticator TLV (RFC 3579 §3.2).
            RadiusPacket statusPacket = new(RadiusCode.SERVER_STATUS);
            statusPacket.SetMessageAuthenticator(_secret);
            statusPacket.SetAuthenticator(_secret);

            // Per RFC 5997 §6: Status-Server packets MUST NOT be retransmitted.
            return SendAndReceivePacketAsync(statusPacket, NO_RETRIES, cancellationToken);
        }

        /// <summary>
        /// Verifies the Response Authenticator of a received RADIUS reply packet against
        /// a locally recomputed value, as defined in RFC 2865 §3.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Two conditions must both hold for verification to succeed:
        /// </para>
        /// <list type="number">
        ///   <item>
        ///     <description>
        ///       The <see cref="RadiusPacket.Identifier"/> of <paramref name="receivedPacket"/>
        ///       must match that of <paramref name="requestedPacket"/>, as required by RFC 2865 §3.
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <description>
        ///       The 16-byte Authenticator field of <paramref name="receivedPacket"/> must equal
        ///       <c>MD5(Code + Identifier + Length + RequestAuthenticator + Attributes + SharedSecret)</c>,
        ///       where <c>RequestAuthenticator</c> is the Authenticator from <paramref name="requestedPacket"/>.
        ///     </description>
        ///   </item>
        /// </list>
        /// <para>
        /// The Identifier comparison is performed as a fast byte equality check. The
        /// authenticator comparison is performed by <see cref="RadiusUtils.VerifyResponseAuthenticator"/>,
        /// which uses <see cref="CryptographicOperations.FixedTimeEquals"/> to prevent
        /// timing side-channel attacks.
        /// </para>
        /// </remarks>
        /// <param name="requestedPacket">
        /// The outbound RADIUS request packet. Must not be <see langword="null"/> and must
        /// have <see cref="RadiusPacket.Valid"/> set to <see langword="true"/>.
        /// </param>
        /// <param name="receivedPacket">
        /// The inbound RADIUS response packet. Must not be <see langword="null"/> and must
        /// have <see cref="RadiusPacket.Valid"/> set to <see langword="true"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the Identifier fields match and the recomputed Response
        /// Authenticator equals the one in <paramref name="receivedPacket"/>;
        /// <see langword="false"/> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="requestedPacket"/> or <paramref name="receivedPacket"/>
        /// is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when either packet has <see cref="RadiusPacket.Valid"/> set to
        /// <see langword="false"/>, indicating a structurally invalid packet.
        /// </exception>
        public bool VerifyAuthenticator(RadiusPacket requestedPacket, RadiusPacket receivedPacket)
        {
            ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);

            ArgumentNullException.ThrowIfNull(requestedPacket);
            ArgumentNullException.ThrowIfNull(receivedPacket);

            if (!requestedPacket.Valid)
                throw new ArgumentException(
                    "The request packet is not valid.", nameof(requestedPacket));

            if (!receivedPacket.Valid)
                throw new ArgumentException(
                    "The received packet is not valid.", nameof(receivedPacket));

            // Per RFC 2865 §3: the Identifier in the response must match the request.
            // Check before the expensive crypto operation. This is not a timing-sensitive
            // comparison — the Identifier is a single public byte, not a secret.
            if (requestedPacket.Identifier != receivedPacket.Identifier)
                return false;

            // Read the 16-byte Request Authenticator directly from the request packet's
            // RawData at offset 4, avoiding the defensive-copy heap allocation that the
            // RadiusPacket.Authenticator property performs on every access.
            // VerifyResponseAuthenticator uses CryptographicOperations.FixedTimeEquals
            // internally for constant-time comparison.
            return RadiusUtils.VerifyResponseAuthenticator(
                receivedPacket.RawData,
                requestedPacket.RawData.AsSpan(4, 16).ToArray(),
                _secret);
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Finaliser that releases the underlying UDP socket if <see cref="Dispose()"/>
        /// was not called by the consumer.
        /// </summary>
        /// <remarks>
        /// Relying on the finaliser is not recommended. Always use a <c>using</c>
        /// statement or call <see cref="Dispose()"/> explicitly to release resources
        /// deterministically.
        /// </remarks>
        ~RadiusClient()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// Releases the underlying UDP socket and all associated resources.
        /// </summary>
        /// <remarks>
        /// <para>
        /// After this method is called, any subsequent call to
        /// <see cref="SendAndReceivePacketAsync"/> or <see cref="PingAsync"/>
        /// will throw <see cref="ObjectDisposedException"/>.
        /// </para>
        /// <para>
        /// This method is idempotent — calling it multiple times has no additional effect.
        /// </para>
        /// </remarks>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Core disposal logic shared by <see cref="Dispose()"/> and the finaliser.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true"/> when called from <see cref="Dispose()"/>;
        /// <see langword="false"/> when called from the finaliser.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref _disposed, 1, 0) != 0)
                return;

            if (disposing)
            {
                // Cancel the background receive loop before disposing the socket,
                // so it exits cleanly via OperationCanceledException rather than
                // ObjectDisposedException.
                _receiveCts.Cancel();

                // Wait for the receive loop to terminate, ensuring the ReceiveFromAsync
                // call has completed before the socket is disposed. This prevents a race
                // where the socket is disposed while ReceiveFromAsync is still in-flight
                // on the thread pool, producing a noisy ObjectDisposedException.
                // Use Task.Wait with a bounded timeout to prevent hanging during disposal
                // if the receive loop is stuck — 5 seconds is generous for a UDP cancel.
                try
                {
                    _receiveLoopTask?.Wait(TimeSpan.FromSeconds(5));
                }
                catch (AggregateException)
                {
                    // Swallow any exception from the receive loop — it's already shutting
                    // down and we only care about join semantics, not the result.
                }

                _receiveCts.Dispose();

                // Cancel all pending requests so callers don't hang forever.
                foreach (var kvp in _pendingRequests)
                {
                    kvp.Value.TrySetCanceled();
                }
                _pendingRequests.Clear();

                // Managed cleanup: zero the shared secret to reduce exposure
                // window of sensitive material in managed memory.
                CryptographicOperations.ZeroMemory(_secret);

                // Dispose the socket deterministically.
                _socket.Dispose();
            }
        }

        #endregion
    }
}