using Radius.Attributes;
using System.Security.Cryptography;
using System.Text;

namespace Radius
{
    /// <summary>
    /// Represents a RADIUS protocol packet for both constructing outbound requests
    /// and parsing inbound responses, as defined in RFC 2865 and RFC 2866.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use <see cref="RadiusPacket(RadiusCode)"/> or <see cref="RadiusPacket(RadiusCode, byte)"/>
    /// to build a new outbound packet, then populate attributes via <see cref="SetAttribute"/>,
    /// optionally call <see cref="SetMessageAuthenticator"/>, and finally call
    /// <see cref="SetAuthenticator"/> to complete the packet before sending.
    /// </para>
    /// <para>
    /// Use <see cref="RadiusPacket(byte[])"/> to parse a received UDP payload.
    /// Always check <see cref="Valid"/> before consuming any parsed fields.
    /// </para>
    /// <para>
    /// The serialised packet is always available via <see cref="RawData"/>.
    /// The 20-byte header region (Code, Identifier, Length, Authenticator) is
    /// accessible as a computed snapshot via <see cref="Header"/>.
    /// </para>
    /// <para>
    /// <b>Shared secret encoding:</b> All methods that accept a shared secret delegate
    /// cryptographic operations to <see cref="RadiusUtils"/>, which encodes secrets as
    /// strict ASCII. Secrets containing non-ASCII characters (code points above 127)
    /// will cause an <c>EncoderFallbackException</c> to be thrown, rather than
    /// silently producing incorrect authenticators via <c>'?'</c> substitution.
    /// </para>
    /// <para>
    /// <b>Thread safety:</b> This class is <b>not</b> thread-safe. A single
    /// <see cref="RadiusPacket"/> instance must not be mutated (via <see cref="SetAttribute"/>,
    /// <see cref="SetMessageAuthenticator"/>, <see cref="SetAuthenticator"/>, or
    /// <see cref="SetIdentifier"/>) concurrently from multiple threads. Once fully constructed
    /// and signed, the packet may be read concurrently (e.g. by <c>SendAndReceivePacketAsync</c>)
    /// provided no further mutations occur. The <see cref="Authenticator"/> and
    /// <see cref="Attributes"/> properties return defensive copies to prevent external
    /// mutation from desynchronising internal state.
    /// </para>
    /// </remarks>
    public sealed class RadiusPacket
    {
        #region Constants

        /// <summary>Byte offset of the Code field within the RADIUS packet header (RFC 2865 §3, octet 1).</summary>
        private const int RADIUS_CODE_INDEX = 0;

        /// <summary>Byte offset of the Identifier field within the RADIUS packet header (RFC 2865 §3, octet 2).</summary>
        private const int RADIUS_IDENTIFIER_INDEX = 1;

        /// <summary>Byte offset of the big-endian Length field within the RADIUS packet header (RFC 2865 §3, octets 3–4).</summary>
        private const int RADIUS_LENGTH_INDEX = 2;

        /// <summary>Byte offset of the Authenticator field within the RADIUS packet header (RFC 2865 §3, octets 5–20).</summary>
        private const int RADIUS_AUTHENTICATOR_INDEX = 4;

        /// <summary>Length of the Authenticator field in bytes (RFC 2865 §3).</summary>
        private const int RADIUS_AUTHENTICATOR_FIELD_LENGTH = 16;

        /// <summary>
        /// Total wire length in bytes of a Message-Authenticator attribute TLV:
        /// Type (1) + Length (1) + Value (16) = 18 bytes (RFC 3579 §3.2).
        /// </summary>
        private const int RADIUS_MESSAGE_AUTHENTICATOR_LENGTH = 18;

        /// <summary>Byte offset of the first attribute TLV within a RADIUS packet (RFC 2865 §3).</summary>
        private const int ATTRIBUTES_INDEX = 20;

        /// <summary>Length of the fixed RADIUS packet header in bytes (RFC 2865 §3).</summary>
        private const int RADIUS_HEADER_LENGTH = ATTRIBUTES_INDEX;

        /// <summary>Minimum length of a valid RADIUS packet in bytes (RFC 2865 §3).</summary>
        private const int MINIMUM_PACKET_LENGTH = RADIUS_HEADER_LENGTH;

        /// <summary>Maximum length of a valid RADIUS packet in bytes (RFC 2865 §3).</summary>
        private const int MAXIMUM_PACKET_LENGTH = 4096;

        #endregion

        #region Private

        /// <summary>The list of parsed or appended RADIUS attributes for this packet.</summary>
        private readonly List<RadiusAttributes> _Attributes = [];

        /// <summary>The 16-byte Authenticator field (RFC 2865 §3, octets 5–20).</summary>
        private byte[] _Authenticator = new byte[RADIUS_AUTHENTICATOR_FIELD_LENGTH];

        /// <summary>Backing field for <see cref="NasPortType"/>.</summary>
        private NAS_PORT_TYPE _NasPortType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the full serialised RADIUS packet as a raw byte array,
        /// including the 20-byte header and all appended attributes.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property returns the internal buffer directly for performance.
        /// Callers must not mutate the returned array; doing so will corrupt
        /// the packet and desynchronise the <see cref="Authenticator"/> field.
        /// </para>
        /// <para>
        /// For a safe read-only copy, use <c>RawData.ToArray()</c> or
        /// <c>RawData.AsSpan().ToArray()</c>.
        /// </para>
        /// </remarks>
        public byte[] RawData { get; private set; }

        /// <summary>
        /// Gets the RADIUS packet code identifying the packet type (RFC 2865 §3, octet 1).
        /// </summary>
        public RadiusCode PacketType { get; private set; }

        /// <summary>
        /// Gets the packet Identifier byte used to match requests to replies (RFC 2865 §3, octet 2).
        /// </summary>
        public byte Identifier { get; private set; }

        /// <summary>
        /// Gets a snapshot of the 20-byte RADIUS packet header (Code, Identifier, Length, Authenticator)
        /// from the current state of <see cref="RawData"/>.
        /// </summary>
        /// <remarks>
        /// This property returns a fresh copy each time it is accessed, ensuring it always reflects
        /// the current state of <see cref="RawData"/> even after calls to <see cref="SetAuthenticator"/>.
        /// </remarks>
        public byte[] Header
        {
            get
            {
                byte[] header = new byte[RADIUS_HEADER_LENGTH];
                if (RawData.Length >= RADIUS_HEADER_LENGTH)
                    RawData.AsSpan(0, RADIUS_HEADER_LENGTH).CopyTo(header);
                return header;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the packet passed all structural validation checks.
        /// </summary>
        /// <remarks>
        /// Always <see langword="true"/> for outbound packets constructed via
        /// <see cref="RadiusPacket(RadiusCode)"/> or <see cref="RadiusPacket(RadiusCode, byte)"/>.
        /// For inbound packets parsed via <see cref="RadiusPacket(byte[])"/>, callers must
        /// check this before consuming any parsed fields.
        /// </remarks>
        public bool Valid { get; private set; }

        /// <summary>
        /// Gets or sets the NAS-Port-Type attribute (RFC 2865 §5.41).
        /// Setting this value appends the corresponding RADIUS attribute to the packet.
        /// </summary>
        public NAS_PORT_TYPE NasPortType
        {
            get { return _NasPortType; }
            set
            {
                _NasPortType = value;
                SetAttribute(new RadiusAttributes(RadiusAttributeType.NAS_PORT_TYPE, (int)value));
            }
        }

        /// <summary>
        /// Gets a read-only snapshot of the RADIUS attributes currently associated with this packet.
        /// </summary>
        /// <remarks>
        /// A new list is returned on each access to prevent external mutation from
        /// desynchronising the attribute list with <see cref="RawData"/>. To append
        /// attributes, use <see cref="SetAttribute"/>.
        /// </remarks>
        public List<RadiusAttributes> Attributes
        {
            get { return new List<RadiusAttributes>(_Attributes); }
        }

        /// <summary>
        /// Gets a defensive copy of the 16-byte Authenticator field of this packet
        /// (RFC 2865 §3, octets 5–20).
        /// </summary>
        /// <remarks>
        /// A fresh copy is returned on each access to prevent external mutation from
        /// desynchronising <see cref="_Authenticator"/> with <see cref="RawData"/>.
        /// </remarks>
        public byte[] Authenticator
        {
            get { return (byte[])_Authenticator.Clone(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new outbound RADIUS packet of the specified type with a
        /// cryptographically random Identifier, as defined in RFC 2865 §3.
        /// </summary>
        /// <remarks>
        /// The packet is initialised with the 20-byte header only. Attributes are
        /// added via <see cref="SetAttribute"/>. The Authenticator field is left
        /// zeroed until <see cref="SetAuthenticator"/> is called.
        /// </remarks>
        /// <param name="packetType">The RADIUS packet code (RFC 2865 §3, octet 1).</param>
        public RadiusPacket(RadiusCode packetType)
            : this(packetType, RandomNumberGenerator.GetBytes(1)[0])
        {
        }

        /// <summary>
        /// Initialises a new outbound RADIUS packet of the specified type with
        /// an explicit Identifier byte, as defined in RFC 2865 §3.
        /// </summary>
        /// <remarks>
        /// Use this overload when constructing a reply that must echo the Identifier
        /// of the originating request (RFC 2865 §3). For new outbound requests,
        /// prefer <see cref="RadiusPacket(RadiusCode)"/> which generates a random Identifier.
        /// </remarks>
        /// <param name="packetType">The RADIUS packet code (RFC 2865 §3, octet 1).</param>
        /// <param name="identifier">
        /// The Identifier byte to use. For replies this must match the Identifier
        /// of the corresponding request packet.
        /// </param>
        public RadiusPacket(RadiusCode packetType, byte identifier)
        {
            PacketType = packetType;
            Identifier = identifier;

            RawData = new byte[RADIUS_HEADER_LENGTH];
            RawData[RADIUS_CODE_INDEX] = (byte)PacketType;
            RawData[RADIUS_IDENTIFIER_INDEX] = Identifier;

            // Write the initial packet length in big-endian order (RFC 2865 §3).
            RawData[RADIUS_LENGTH_INDEX] = (byte)(RADIUS_HEADER_LENGTH >> 8);
            RawData[RADIUS_LENGTH_INDEX + 1] = (byte)(RADIUS_HEADER_LENGTH & 0xFF);

            Valid = true;
        }

        /// <summary>
        /// Parses a received RADIUS packet from a raw byte buffer, as defined in RFC 2865 §3.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the buffer fails any structural validation check, <see cref="Valid"/> is set
        /// to <see langword="false"/> and the remaining fields are left in a default state.
        /// No exception is thrown for malformed packets — callers must check <see cref="Valid"/>
        /// before using the parsed packet.
        /// </para>
        /// <para>Validation checks performed:</para>
        /// <list type="bullet">
        ///   <item><description>Buffer length must be between 20 and 4096 bytes inclusive (RFC 2865 §3).</description></item>
        ///   <item><description>The Length field in the packet header must be at least 20 bytes and must not exceed the buffer length.</description></item>
        ///   <item><description>All attribute TLVs must be well-formed (see <see cref="ParseAttributes"/>).</description></item>
        /// </list>
        /// </remarks>
        /// <param name="receivedData">
        /// The raw UDP payload of the received RADIUS packet. Must not be <see langword="null"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="receivedData"/> is <see langword="null"/>.
        /// </exception>
        public RadiusPacket(byte[] receivedData)
        {
            ArgumentNullException.ThrowIfNull(receivedData);

            // Ensure RawData is always non-null, even on early validation failure.
            RawData = receivedData;

            try
            {
                // RFC 2865 §3: minimum packet length is 20 bytes, maximum is 4096 bytes.
                if (RawData.Length < MINIMUM_PACKET_LENGTH || RawData.Length > MAXIMUM_PACKET_LENGTH)
                {
                    Valid = false;
                    return;
                }

                // Get the RADIUS Code (octet 1).
                PacketType = (RadiusCode)RawData[RADIUS_CODE_INDEX];

                // Get the RADIUS Identifier (octet 2).
                Identifier = RawData[RADIUS_IDENTIFIER_INDEX];

                // Get the big-endian RADIUS Length field (octets 3–4).
                ushort length = (ushort)(RawData[RADIUS_LENGTH_INDEX] << 8 | RawData[RADIUS_LENGTH_INDEX + 1]);

                // RFC 2865 §3: the Length field must be at least 20 and must not
                // exceed the actual buffer size.
                if (length < MINIMUM_PACKET_LENGTH || length > RawData.Length)
                {
                    Valid = false;
                    return;
                }

                // Extract the 16-byte Authenticator field (octets 5–20).
                RawData.AsSpan(RADIUS_AUTHENTICATOR_INDEX, RADIUS_AUTHENTICATOR_FIELD_LENGTH)
                       .CopyTo(_Authenticator);

                // Parse attributes directly from the raw buffer — no intermediate allocation.
                ParseAttributes(RawData.AsSpan(ATTRIBUTES_INDEX, length - ATTRIBUTES_INDEX));

                Valid = true;
            }
            catch (Exception ex) when (
                ex is ArgumentException
                  or ArgumentNullException
                  or ArgumentOutOfRangeException
                  or ArrayTypeMismatchException
                  or InvalidCastException
                  or OverflowException
                  or RankException)
            {
                Valid = false;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Computes and writes the Authenticator field into <see cref="RawData"/>
        /// for the current <see cref="PacketType"/>, as defined in RFC 2865, RFC 2866,
        /// and RFC 5176.
        /// </summary>
        /// <remarks>
        /// <para>The computation strategy varies by packet type:</para>
        /// <list type="table">
        ///   <listheader>
        ///     <term>Packet Type(s)</term>
        ///     <description>Authenticator Strategy</description>
        ///   </listheader>
        ///   <item>
        ///     <term><see cref="RadiusCode.ACCESS_REQUEST"/>, <see cref="RadiusCode.SERVER_STATUS"/></term>
        ///     <description>Random 16-byte value hashed with SharedSecret (RFC 2865 §3).</description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="RadiusCode.ACCESS_ACCEPT"/>, <see cref="RadiusCode.ACCESS_REJECT"/>,
        ///     <see cref="RadiusCode.ACCESS_CHALLENGE"/>, <see cref="RadiusCode.ACCOUNTING_RESPONSE"/>,
        ///     <see cref="RadiusCode.DISCONNECT_ACK"/>, <see cref="RadiusCode.DISCONNECT_NACK"/>,
        ///     <see cref="RadiusCode.COA_ACK"/>, <see cref="RadiusCode.COA_NACK"/></term>
        ///     <description>MD5 over response packet with <paramref name="requestAuthenticator"/> substituted
        ///     at offset 4 (RFC 2865 §3, RFC 5176 §3.2). <paramref name="requestAuthenticator"/> is required.</description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="RadiusCode.ACCOUNTING_REQUEST"/>, <see cref="RadiusCode.COA_REQUEST"/>,
        ///     <see cref="RadiusCode.DISCONNECT_REQUEST"/></term>
        ///     <description>MD5 over packet with authenticator field zeroed (RFC 2866 §3.1, RFC 5176 §3.2).</description>
        ///   </item>
        /// </list>
        /// <para>
        /// This method must be called <b>after</b> <see cref="SetMessageAuthenticator"/>
        /// (if used), as it reads the final <see cref="RawData"/> to compute the hash.
        /// </para>
        /// </remarks>
        /// <param name="sharedSecret">
        /// The shared secret negotiated with the RADIUS server. Must not be
        /// <see langword="null"/> or empty.
        /// </param>
        /// <param name="requestAuthenticator">
        /// The 16-byte Authenticator field from the original request packet. Required for
        /// response packet types (<see cref="RadiusCode.ACCESS_ACCEPT"/>,
        /// <see cref="RadiusCode.ACCESS_REJECT"/>, <see cref="RadiusCode.ACCESS_CHALLENGE"/>,
        /// <see cref="RadiusCode.ACCOUNTING_RESPONSE"/>, <see cref="RadiusCode.DISCONNECT_ACK"/>,
        /// <see cref="RadiusCode.DISCONNECT_NACK"/>, <see cref="RadiusCode.COA_ACK"/>,
        /// <see cref="RadiusCode.COA_NACK"/>); ignored for all other types.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="sharedSecret"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="sharedSecret"/> is empty, or when a response packet type
        /// is used without a valid 16-byte <paramref name="requestAuthenticator"/>.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown when <see cref="PacketType"/> does not have a defined authenticator strategy.
        /// </exception>
        public void SetAuthenticator(byte[] sharedSecret, byte[]? requestAuthenticator = null)
        {
            ArgumentNullException.ThrowIfNull(sharedSecret);
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            switch (PacketType)
            {
                case RadiusCode.ACCESS_REQUEST:
                case RadiusCode.SERVER_STATUS:
                    // Per RFC 2865 §3: random authenticator hashed with the shared secret.
                    _Authenticator = RadiusUtils.AccessRequestAuthenticator(sharedSecret);
                    break;

                case RadiusCode.ACCESS_ACCEPT:
                case RadiusCode.ACCESS_REJECT:
                case RadiusCode.ACCESS_CHALLENGE:
                case RadiusCode.ACCOUNTING_RESPONSE:
                case RadiusCode.DISCONNECT_ACK:   // RFC 5176 §3.2
                case RadiusCode.DISCONNECT_NACK:  // RFC 5176 §3.2
                case RadiusCode.COA_ACK:          // RFC 5176 §3.2
                case RadiusCode.COA_NACK:         // RFC 5176 §3.2
                    // Per RFC 2865 §3 / RFC 5176 §3.2: response authenticator requires
                    // the original request authenticator to complete the hash input.
                    if (requestAuthenticator is null || requestAuthenticator.Length != RADIUS_AUTHENTICATOR_FIELD_LENGTH)
                        throw new ArgumentException(
                            $"A valid {RADIUS_AUTHENTICATOR_FIELD_LENGTH}-byte Request Authenticator is required for packet type {PacketType}.",
                            nameof(requestAuthenticator));

                    _Authenticator = RadiusUtils.ResponseAuthenticator(RawData, requestAuthenticator, sharedSecret);
                    break;

                case RadiusCode.ACCOUNTING_REQUEST:
                case RadiusCode.COA_REQUEST:
                case RadiusCode.DISCONNECT_REQUEST:
                    // Per RFC 2866 §3.1 / RFC 5176 §3.2: authenticator field is zeroed before hashing.
                    _Authenticator = RadiusUtils.AccountingRequestAuthenticator(RawData, sharedSecret);
                    break;

                default:
                    throw new NotSupportedException(
                        $"Packet type '{PacketType}' does not have a defined authenticator strategy.");
            }

            // Write the computed authenticator into the packet header at offset 4.
            _Authenticator.CopyTo(RawData, RADIUS_AUTHENTICATOR_INDEX);

            // For packet types with a random Request Authenticator (Access-Request,
            // Status-Server), the Message-Authenticator HMAC must be recomputed now
            // that the Authenticator field has changed. The HMAC input includes the
            // full packet header (RFC 3579 §3.2), so the value computed by
            // SetMessageAuthenticator (when the Authenticator was still zeroed) is
            // no longer valid.
            if (PacketType is RadiusCode.ACCESS_REQUEST or RadiusCode.SERVER_STATUS)
            {
                RecomputeMessageAuthenticatorIfPresent(sharedSecret);
            }
        }

        /// <summary>
        /// Overrides the packet Identifier byte and updates the corresponding field
        /// in <see cref="RawData"/> at offset <c>1</c>, as defined in RFC 2865 §3.
        /// </summary>
        /// <remarks>
        /// The Identifier is an 8-bit value used to match RADIUS request and reply packets.
        /// It is typically set once during construction via <see cref="RadiusPacket(RadiusCode)"/>,
        /// but may be overridden here when retransmitting a request or when constructing a reply
        /// that must echo the request Identifier.
        /// </remarks>
        /// <param name="id">The Identifier byte to assign to this packet.</param>
        public void SetIdentifier(byte id)
        {
            Identifier = id;
            RawData[RADIUS_IDENTIFIER_INDEX] = id;
        }

        /// <summary>
        /// Appends a RADIUS attribute to the packet and updates the packet length field.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each call extends <see cref="RawData"/> by <see cref="RadiusAttributes.Length"/> bytes
        /// and updates the 2-byte big-endian Length field at offset 2 of the packet header,
        /// as required by RFC 2865 §3.
        /// </para>
        /// <para>
        /// Per RFC 2865 §3, the maximum RADIUS packet size is 4096 bytes. Attempting to
        /// exceed this limit will throw an <see cref="InvalidOperationException"/>.
        /// </para>
        /// <para>
        /// <b>Note:</b> If a Message-Authenticator is required, add all standard attributes
        /// first via this method, then call <see cref="SetMessageAuthenticator"/>, followed
        /// by <see cref="SetAuthenticator"/>.
        /// </para>
        /// </remarks>
        /// <param name="attribute">
        /// The <see cref="RadiusAttributes"/> instance to append. Must not be
        /// <see langword="null"/> and must have a valid wire representation obtainable via
        /// <see cref="RadiusAttributes.GetWireBytes"/> with a length matching
        /// <see cref="RadiusAttributes.Length"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="attribute"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="attribute"/> has an invalid length (less than 2,
        /// greater than 255, or inconsistent with its <see cref="RadiusAttributes.GetWireBytes"/> result).
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when appending the attribute would cause the packet to exceed the
        /// 4096-byte maximum defined in RFC 2865 §3.
        /// </exception>
        public void SetAttribute(RadiusAttributes attribute)
        {
            ArgumentNullException.ThrowIfNull(attribute);

            // Use the public GetWireBytes method to obtain the serialised TLV.
            byte[] attributeRawData = attribute.GetWireBytes();

            if (attribute.Length < 2 || attribute.Length > 255 || attributeRawData is null ||
                attribute.Length != attributeRawData.Length)
                throw new ArgumentException(
                    "Attribute has an invalid or inconsistent Length/Data.", nameof(attribute));

            if (RawData.Length + attribute.Length > MAXIMUM_PACKET_LENGTH)
                throw new InvalidOperationException(
                    $"Cannot add attribute: packet would exceed the {MAXIMUM_PACKET_LENGTH}-byte maximum defined in RFC 2865 §3. " +
                    $"Current size: {RawData.Length}, attribute size: {attribute.Length}.");

            // Extend RawData to accommodate the new attribute.
            byte[] newRawData = new byte[RawData.Length + attribute.Length];
            RawData.AsSpan().CopyTo(newRawData);
            attributeRawData.AsSpan().CopyTo(newRawData.AsSpan(RawData.Length));

            RawData = newRawData;
            ushort length = (ushort)RawData.Length;

            // Update the packet Length field in big-endian order (RFC 2865 §3).
            RawData[RADIUS_LENGTH_INDEX] = (byte)(length >> 8);
            RawData[RADIUS_LENGTH_INDEX + 1] = (byte)(length & 0xFF);

            _Attributes.Add(attribute);
        }

        /// <summary>
        /// Computes and appends a Message-Authenticator attribute (Type 80) to the packet,
        /// as defined in RFC 3579 §3.2.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The Message-Authenticator is an HMAC-MD5 signature over the entire packet,
        /// computed with the 16-byte attribute value field zeroed, then replaced with
        /// the resulting hash. Its structure is:
        /// </para>
        /// <list type="bullet">
        ///   <item><description><b>Type</b> (1 byte): 80</description></item>
        ///   <item><description><b>Length</b> (1 byte): 18</description></item>
        ///   <item><description><b>Value</b> (16 bytes): HMAC-MD5(packet, SharedSecret)</description></item>
        /// </list>
        /// <para>
        /// This method must be called <b>after</b> all other attributes have been added,
        /// and <b>before</b> <see cref="SetAuthenticator"/> if both are required,
        /// as it finalises the packet length field.
        /// </para>
        /// <para>
        /// All cryptographic operations — including strict ASCII encoding, HMAC-MD5
        /// computation, and sensitive key material zeroing — are delegated to
        /// <see cref="RadiusUtils.ComputeMessageAuthenticator"/>.
        /// </para>
        /// </remarks>
        /// <param name="sharedSecret">
        /// The shared secret negotiated with the RADIUS server, used as the HMAC-MD5 key.
        /// Must not be <see langword="null"/> or empty. The secret is encoded as strict ASCII
        /// per RFC 2865 §3; non-ASCII characters will cause an <c>EncoderFallbackException</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="sharedSecret"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="sharedSecret"/> is empty.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when appending the Message-Authenticator attribute would cause the packet
        /// to exceed the 4096-byte maximum defined in RFC 2865 §3.
        /// </exception>
        public void SetMessageAuthenticator(byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(sharedSecret);
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            if (RawData.Length + RADIUS_MESSAGE_AUTHENTICATOR_LENGTH > MAXIMUM_PACKET_LENGTH)
                throw new InvalidOperationException(
                    $"Cannot add Message-Authenticator: packet would exceed the {MAXIMUM_PACKET_LENGTH}-byte " +
                    $"maximum defined in RFC 2865 §3. Current size: {RawData.Length}, " +
                    $"attribute size: {RADIUS_MESSAGE_AUTHENTICATOR_LENGTH}.");

            // Allocate the expanded packet: original + Type(1) + Length(1) + Value(16).
            // The Value region is implicitly zero-initialised by the runtime,
            // satisfying the RFC 3579 §3.2 requirement that the HMAC is computed
            // over the packet with the Message-Authenticator value field zeroed.
            byte[] newRawData = new byte[RawData.Length + RADIUS_MESSAGE_AUTHENTICATOR_LENGTH];

            // Copy original packet into the new buffer using Span for consistency.
            RawData.AsSpan().CopyTo(newRawData);

            int attrOffset = RawData.Length;

            // Write the Message-Authenticator TLV header (Type + Length).
            newRawData[attrOffset] = (byte)RadiusAttributeType.MESSAGE_AUTHENTICATOR;
            newRawData[attrOffset + 1] = (byte)RADIUS_MESSAGE_AUTHENTICATOR_LENGTH;

            // Write the updated packet length in big-endian order (RFC 2865 §3).
            ushort totalLength = (ushort)newRawData.Length;
            newRawData[RADIUS_LENGTH_INDEX] = (byte)(totalLength >> 8);
            newRawData[RADIUS_LENGTH_INDEX + 1] = (byte)(totalLength & 0xFF);

            // Delegate HMAC-MD5 computation to RadiusUtils, which handles StrictAscii
            // encoding, exact-sized HMAC key construction, and CryptographicOperations.ZeroMemory
            // cleanup of all sensitive intermediate buffers.
            byte[] hash = RadiusUtils.ComputeMessageAuthenticator(newRawData, sharedSecret);

            // Replace the zeroed 16-byte Value field with the computed hash.
            hash.AsSpan().CopyTo(newRawData.AsSpan(attrOffset + 2));

            RawData = newRawData;

            // Reflect the Message-Authenticator attribute in the tracked attribute list.
            // Extract only the 16-byte HMAC value, not the remainder of the packet.
            byte[] msgAuthValue = newRawData.AsSpan(attrOffset + 2, RADIUS_AUTHENTICATOR_FIELD_LENGTH).ToArray();
            _Attributes.Add(new RadiusAttributes(RadiusAttributeType.MESSAGE_AUTHENTICATOR, msgAuthValue));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Parses a RADIUS attribute list from a raw byte span and populates
        /// <see cref="_Attributes"/> with the decoded attributes.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each RADIUS attribute is encoded as a Type-Length-Value (TLV) triplet
        /// per RFC 2865 §5:
        /// </para>
        /// <list type="bullet">
        ///   <item><description><b>Type</b> (1 byte) — identifies the attribute.</description></item>
        ///   <item><description><b>Length</b> (1 byte) — total byte length of the attribute including Type and Length fields; minimum value is 2.</description></item>
        ///   <item><description><b>Value</b> (Length - 2 bytes) — the attribute payload.</description></item>
        /// </list>
        /// <para>
        /// If a malformed attribute is encountered (insufficient header bytes,
        /// length less than 2, or length exceeding the remaining buffer),
        /// <see cref="Valid"/> is set to <see langword="false"/> and parsing stops.
        /// </para>
        /// <para>
        /// Vendor-Specific Attributes (VSAs, Type 26) are decoded into
        /// <see cref="VendorSpecificAttributes"/> instances; all others into
        /// <see cref="RadiusAttributes"/>.
        /// </para>
        /// </remarks>
        /// <param name="attributeSpan">
        /// A read-only view of the raw attribute bytes, starting immediately after
        /// the 20-byte RADIUS packet header.
        /// </param>
        private void ParseAttributes(ReadOnlySpan<byte> attributeSpan)
        {
            int offset = 0;

            while (offset < attributeSpan.Length)
            {
                // Each attribute requires at least a 1-byte Type and 1-byte Length field.
                if (offset + 2 > attributeSpan.Length)
                {
                    Valid = false;
                    return;
                }

                RadiusAttributeType type = (RadiusAttributeType)attributeSpan[offset];
                byte length = attributeSpan[offset + 1];

                // Length must include the Type and Length bytes themselves (minimum 2),
                // and must not exceed the remaining buffer.
                if (length < 2 || offset + length > attributeSpan.Length)
                {
                    Valid = false;
                    return;
                }

                // Extract the Value portion (everything after Type + Length bytes).
                byte[] value = attributeSpan.Slice(offset + 2, length - 2).ToArray();

                // For VSAs, pass the full TLV byte slice (Type + Length + Value) at offset 0
                // so that VendorSpecificAttributes can read VSA_ID_INDEX = 2 correctly.
                _Attributes.Add(type == RadiusAttributeType.VENDOR_SPECIFIC
                    ? new VendorSpecificAttributes(attributeSpan.Slice(offset, length).ToArray(), 0)
                    : new RadiusAttributes(type, value));

                offset += length;
            }
        }

        /// <summary>
        /// Scans <see cref="RawData"/> for a Message-Authenticator attribute (Type 80)
        /// and recomputes its HMAC-MD5 value in-place using the current packet contents.
        /// </summary>
        /// <remarks>
        /// This is called by <see cref="SetAuthenticator"/> after writing a new random
        /// Request Authenticator for Access-Request and Status-Server packets, because
        /// the HMAC computed by <see cref="SetMessageAuthenticator"/> was calculated
        /// over the packet with the Authenticator field still zeroed.
        /// </remarks>
        /// <param name="sharedSecret">
        /// The shared secret as a pre-encoded byte array, used as the HMAC-MD5 key.
        /// </param>
        private void RecomputeMessageAuthenticatorIfPresent(byte[] sharedSecret)
        {
            // Scan attributes for Message-Authenticator (Type 80, Length 18).
            int offset = ATTRIBUTES_INDEX;
            int packetLength = RawData.Length;

            while (offset + 2 <= packetLength)
            {
                byte attrType = RawData[offset];
                byte attrLength = RawData[offset + 1];

                if (attrLength < 2 || offset + attrLength > packetLength)
                    break;

                if (attrType == (byte)RadiusAttributeType.MESSAGE_AUTHENTICATOR
                    && attrLength == RADIUS_MESSAGE_AUTHENTICATOR_LENGTH)
                {
                    // Zero the 16-byte value field before computing the HMAC
                    // (RFC 3579 §3.2).
                    RawData.AsSpan(offset + 2, RADIUS_AUTHENTICATOR_FIELD_LENGTH).Clear();

                    // Recompute the HMAC-MD5 over the entire packet with the
                    // Message-Authenticator value zeroed.
                    byte[] hash = RadiusUtils.ComputeMessageAuthenticator(RawData, sharedSecret);

                    // Write the new HMAC into the Message-Authenticator value field.
                    hash.AsSpan().CopyTo(RawData.AsSpan(offset + 2));

                    // Also update the tracked attribute in _Attributes.
                    for (int i = 0; i < _Attributes.Count; i++)
                    {
                        if (_Attributes[i].Type == RadiusAttributeType.MESSAGE_AUTHENTICATOR)
                        {
                            _Attributes[i] = new RadiusAttributes(
                                RadiusAttributeType.MESSAGE_AUTHENTICATOR, hash);
                            break;
                        }
                    }

                    return;
                }

                offset += attrLength;
            }
        }

        #endregion
    }
}