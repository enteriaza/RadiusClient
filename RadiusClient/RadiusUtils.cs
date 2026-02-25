using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Radius
{
    /// <summary>
    /// Provides static utility methods for RADIUS protocol packet construction,
    /// authenticator computation, and binary encoding, as defined in
    /// RFC 2865 (RADIUS) and RFC 2866 (RADIUS Accounting).
    /// </summary>
    /// <remarks>
    /// <para><b>Authenticator methods</b> handle MD5-based request/response authenticator
    /// generation and verification per RFC 2865 §3 and RFC 2866 §3.1.</para>
    /// <para><b>PAP encoding</b> implements the User-Password obfuscation scheme
    /// defined in RFC 2865 §5.2.</para>
    /// <para><b>CHAP encoding</b> implements the challenge-response computation
    /// defined in RFC 1994 §3 and referenced in RFC 2865 §5.3.</para>
    /// <para><b>Tunnel-Password encoding</b> implements the obfuscation scheme
    /// defined in RFC 2868 §3.5.</para>
    /// <para><b>Binary encoding</b> methods convert numeric types to big-endian
    /// (network byte order) byte arrays suitable for use in RADIUS attribute values.</para>
    /// <para><b>Packet inspection</b> methods scan raw wire buffers without constructing
    /// a full <see cref="RadiusPacket"/>, providing zero-allocation attribute lookup.</para>
    /// <para>
    /// All methods that handle shared secret or password material zero their
    /// intermediate buffers after use via <see cref="CryptographicOperations.ZeroMemory"/>
    /// to limit the exposure of sensitive data in memory.
    /// </para>
    /// <para>
    /// <b>Shared secret representation:</b> All methods accept the shared secret as a
    /// pre-encoded <c>byte[]</c>. Callers are responsible for encoding the secret
    /// (typically as ASCII per RFC 2865 §3) before passing it to these methods.
    /// </para>
    /// </remarks>
    public static class RadiusUtils
    {
        #region Constants

        /// <summary>Minimum length of a valid RADIUS packet in bytes (RFC 2865 §3).</summary>
        private const int MinimumPacketLength = 20;

        /// <summary>Maximum length of a valid RADIUS packet in bytes (RFC 2865 §3).</summary>
        private const int MaximumPacketLength = 4096;

        /// <summary>Required length of a RADIUS Authenticator field in bytes (RFC 2865 §3).</summary>
        private const int AuthenticatorLength = 16;

        /// <summary>Byte offset of the Authenticator field within a RADIUS packet header (RFC 2865 §3).</summary>
        private const int AuthenticatorOffset = 4;

        /// <summary>Byte offset of the big-endian Length field within a RADIUS packet header (RFC 2865 §3).</summary>
        private const int PacketLengthOffset = 2;

        /// <summary>Byte offset of the first attribute TLV within a RADIUS packet (RFC 2865 §3).</summary>
        private const int AttributesOffset = 20;

        #endregion

        #region Authenticator Methods

        /// <summary>
        /// Computes the MD5 authenticator for a RADIUS Accounting-Request packet
        /// as defined in RFC 2866 §3.1.
        /// </summary>
        /// <remarks>
        /// The authenticator is calculated as:
        /// <c>MD5(Code + Identifier + Length + 16-zero-bytes + Attributes + SharedSecret)</c>
        /// The 16-byte authenticator field in <paramref name="data"/> (offset 4–19)
        /// is zeroed in-place before hashing, as required by the RFC.
        /// </remarks>
        /// <param name="data">
        /// The raw RADIUS packet bytes. The 16-byte authenticator field at offset 4
        /// will be zeroed in-place as part of the computation.
        /// </param>
        /// <param name="sharedSecret">
        /// The shared secret negotiated with the RADIUS server, as a pre-encoded byte array.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>A 16-byte MD5 hash to be used as the packet authenticator.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="data"/> or <paramref name="sharedSecret"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="data"/> is shorter than 20 bytes (the minimum RADIUS packet length),
        /// or when <paramref name="sharedSecret"/> is empty.
        /// </exception>
        public static byte[] AccountingRequestAuthenticator(byte[] data, byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(sharedSecret);

            if (sharedSecret.Length == 0)
                throw new ArgumentException(
                    "Shared secret must not be empty.", nameof(sharedSecret));

            if (data.Length < MinimumPacketLength)
                throw new ArgumentException(
                    $"RADIUS packet must be at least {MinimumPacketLength} bytes.", nameof(data));

            // Work on a pooled copy to avoid mutating the caller's buffer.
            // This eliminates the silent data-destruction contract that requires
            // callers to remember to clone before calling.
            byte[] working = System.Buffers.ArrayPool<byte>.Shared.Rent(data.Length);
            try
            {
                data.AsSpan().CopyTo(working);
                working.AsSpan(AuthenticatorOffset, AuthenticatorLength).Clear();
                return HashPacketWithSecret(working, data.Length, sharedSecret);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(working.AsSpan(0, data.Length));
                System.Buffers.ArrayPool<byte>.Shared.Return(working);
            }
        }

        /// <summary>
        /// Generates a random 16-byte Request Authenticator for a RADIUS Access-Request packet
        /// as defined in RFC 2865 §3.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The authenticator is calculated as:
        /// <c>MD5(16-random-bytes + SharedSecret)</c>
        /// A cryptographically secure random number generator is used to produce
        /// the 16 random bytes, as required by RFC 2865.
        /// </para>
        /// <para>
        /// <b>Protocol note:</b> RFC 2865 §3 requires the Request Authenticator to be
        /// a 16-octet random number. This implementation computes
        /// <c>MD5(random + SharedSecret)</c> for additional diffusion, which is
        /// compatible with all known RADIUS server implementations but is not
        /// strictly required by the RFC.
        /// </para>
        /// </remarks>
        /// <param name="sharedSecret">
        /// The shared secret negotiated with the RADIUS server, as a pre-encoded byte array.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>A 16-byte MD5 hash to be used as the Request Authenticator.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="sharedSecret"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="sharedSecret"/> is empty.
        /// </exception>
        public static byte[] AccessRequestAuthenticator(byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(sharedSecret);
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            int totalLength = AuthenticatorLength + sharedSecret.Length;

            byte[] buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(totalLength);
            try
            {
                Span<byte> bufferSpan = buffer.AsSpan(0, totalLength);

                // Per RFC 2865 §3: fill the first 16 bytes with cryptographically
                // secure random data to ensure authenticator uniqueness per request.
                RandomNumberGenerator.Fill(bufferSpan[..AuthenticatorLength]);

                // Copy the shared secret bytes directly after the random bytes.
                sharedSecret.AsSpan().CopyTo(bufferSpan[AuthenticatorLength..]);

                return MD5.HashData(bufferSpan);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(buffer.AsSpan(0, totalLength));
                System.Buffers.ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// Computes the MD5 Response Authenticator for a RADIUS response packet
        /// as defined in RFC 2865 §3.
        /// </summary>
        /// <remarks>
        /// The authenticator is calculated as:
        /// <c>MD5(Code + Identifier + Length + RequestAuthenticator + Attributes + SharedSecret)</c>
        /// where <paramref name="requestAuthenticator"/> replaces the Authenticator field
        /// (offset 4–19) of the response packet bytes before hashing.
        /// </remarks>
        /// <param name="data">
        /// The raw RADIUS response packet bytes. Must be at least 20 bytes.
        /// The Authenticator field (offset 4–19) is overwritten in-place with
        /// <paramref name="requestAuthenticator"/> before hashing; the caller
        /// is responsible for passing a copy if the original must be preserved.
        /// </param>
        /// <param name="requestAuthenticator">
        /// The 16-byte Request Authenticator from the original Access-Request packet,
        /// as defined in RFC 2865 §3.
        /// </param>
        /// <param name="sharedSecret">
        /// The shared secret negotiated with the RADIUS server, as a pre-encoded byte array.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>A 16-byte MD5 hash to be used as the Response Authenticator.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="data"/>, <paramref name="requestAuthenticator"/>,
        /// or <paramref name="sharedSecret"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="data"/> is shorter than 20 bytes (the minimum RADIUS packet length),
        /// or when <paramref name="requestAuthenticator"/> is not exactly 16 bytes,
        /// or when <paramref name="sharedSecret"/> is empty.
        /// </exception>
        public static byte[] ResponseAuthenticator(byte[] data, byte[] requestAuthenticator, byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(requestAuthenticator);
            ArgumentNullException.ThrowIfNull(sharedSecret);

            if (data.Length < MinimumPacketLength)
                throw new ArgumentException(
                    $"RADIUS packet must be at least {MinimumPacketLength} bytes.", nameof(data));
            if (requestAuthenticator.Length != AuthenticatorLength)
                throw new ArgumentException(
                    $"Request Authenticator must be exactly {AuthenticatorLength} bytes.",
                    nameof(requestAuthenticator));
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            byte[] working = System.Buffers.ArrayPool<byte>.Shared.Rent(data.Length);
            try
            {
                data.AsSpan().CopyTo(working);
                requestAuthenticator.AsSpan().CopyTo(working.AsSpan(AuthenticatorOffset, AuthenticatorLength));
                return HashPacketWithSecret(working, data.Length, sharedSecret);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(working.AsSpan(0, data.Length));
                System.Buffers.ArrayPool<byte>.Shared.Return(working);
            }
        }

        /// <summary>
        /// Computes the HMAC-MD5 Message-Authenticator value for an outbound RADIUS packet,
        /// as defined in RFC 3579 §3.2.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The HMAC is computed over the entire packet with the 16-byte Message-Authenticator
        /// value field zeroed. The packet must already have the Message-Authenticator TLV
        /// appended (with the value region zero-filled) and the Length field updated.
        /// </para>
        /// <para>
        /// All sensitive intermediate buffers (HMAC key material) are zeroed after use.
        /// </para>
        /// </remarks>
        /// <param name="packetBytes">
        /// The full packet bytes with the Message-Authenticator value region zeroed.
        /// Must be at least 20 bytes.
        /// </param>
        /// <param name="sharedSecret">
        /// The shared secret used as the HMAC-MD5 key, as a pre-encoded byte array.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>A 16-byte HMAC-MD5 hash to be written into the Message-Authenticator value field.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="packetBytes"/> or <paramref name="sharedSecret"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="packetBytes"/> is shorter than 20 bytes or
        /// <paramref name="sharedSecret"/> is empty.
        /// </exception>
        public static byte[] ComputeMessageAuthenticator(byte[] packetBytes, byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(packetBytes);
            ArgumentNullException.ThrowIfNull(sharedSecret);
            if (packetBytes.Length < MinimumPacketLength)
                throw new ArgumentException(
                    $"RADIUS packet must be at least {MinimumPacketLength} bytes.", nameof(packetBytes));
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            byte[] hmacKey = new byte[sharedSecret.Length];

            try
            {
                sharedSecret.AsSpan().CopyTo(hmacKey);

                using HMACMD5 hmac = new(hmacKey);
                byte[] hash = hmac.ComputeHash(packetBytes);
                return hash;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(hmacKey);
            }
        }

        /// <summary>
        /// Verifies the Authenticator field of a received RADIUS Accounting-Request packet
        /// against a locally recomputed value, as defined in RFC 2866 §3.1.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The verification recomputes the expected authenticator as:
        /// <c>MD5(Code + Identifier + Length + 16-zero-bytes + Attributes + SharedSecret)</c>
        /// and compares it against the Authenticator field stored at octets 5–20 of
        /// <paramref name="radiusPacket"/> using a constant-time comparison to prevent
        /// timing side-channel attacks.
        /// </para>
        /// <para>
        /// This method also applies to CoA-Request (<see cref="RadiusCode.COA_REQUEST"/>)
        /// and Disconnect-Request (<see cref="RadiusCode.DISCONNECT_REQUEST"/>) packets,
        /// which use the same authenticator scheme (RFC 5176 §3.2).
        /// </para>
        /// <para>
        /// The packet buffer is not modified by this method.
        /// <see cref="AccountingRequestAuthenticator"/> operates on an internal pooled
        /// working copy and zeros all intermediate buffers before returning them to the pool.
        /// </para>
        /// <para>
        /// <b>Thread safety:</b> This method is safe to call concurrently from multiple
        /// threads. No shared mutable state is accessed.
        /// </para>
        /// </remarks>
        /// <param name="radiusPacket">
        /// The raw bytes of the received RADIUS packet (Accounting-Request, CoA-Request,
        /// or Disconnect-Request). Must not be <see langword="null"/> and must be at least
        /// 20 bytes (the minimum RADIUS packet length per RFC 2865 §3).
        /// </param>
        /// <param name="secret">
        /// The shared secret negotiated with the RADIUS peer, as a pre-encoded byte array.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the recomputed authenticator matches the one in the packet;
        /// <see langword="false"/> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="radiusPacket"/> or <paramref name="secret"/> is
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="radiusPacket"/> is shorter than 20 bytes (the minimum
        /// RADIUS packet length per RFC 2865 §3), or when <paramref name="secret"/> is empty.
        /// </exception>
        public static bool VerifyAccountingAuthenticator(byte[] radiusPacket, byte[] secret)
        {
            ArgumentNullException.ThrowIfNull(radiusPacket);
            ArgumentNullException.ThrowIfNull(secret);

            if (radiusPacket.Length < MinimumPacketLength)
                throw new ArgumentException(
                    $"RADIUS packet must be at least {MinimumPacketLength} bytes.", nameof(radiusPacket));
            if (secret.Length == 0)
                throw new ArgumentException(
                    "Shared secret must not be empty.", nameof(secret));

            // Extract the original 16-byte Authenticator field (octets 5–20).
            // This span references the caller's buffer directly — no copy.
            // AccountingRequestAuthenticator operates on an internal pooled copy,
            // so the caller's buffer is never modified.
            ReadOnlySpan<byte> originalAuthenticator =
                radiusPacket.AsSpan(AuthenticatorOffset, AuthenticatorLength);

            // Recompute the expected authenticator per RFC 2866 §3.1:
            // MD5(Code + Identifier + Length + 16-zero-bytes + Attributes + SharedSecret)
            byte[] expected = AccountingRequestAuthenticator(radiusPacket, secret);

            // Use constant-time comparison to prevent timing side-channel attacks.
            return CryptographicOperations.FixedTimeEquals(originalAuthenticator, expected);
        }

        /// <summary>
        /// Verifies the Response Authenticator of a received RADIUS reply packet
        /// against a locally recomputed value, as defined in RFC 2865 §3.
        /// </summary>
        /// <param name="responsePacket">The full raw bytes of the received response packet.</param>
        /// <param name="requestAuthenticator">
        /// The 16-byte Request Authenticator from the original request packet.
        /// </param>
        /// <param name="sharedSecret">
        /// The shared secret, as a pre-encoded byte array.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the recomputed authenticator matches the one in the packet;
        /// <see langword="false"/> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="responsePacket"/>, <paramref name="requestAuthenticator"/>,
        /// or <paramref name="sharedSecret"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="responsePacket"/> is shorter than 20 bytes,
        /// <paramref name="requestAuthenticator"/> is not exactly 16 bytes,
        /// or <paramref name="sharedSecret"/> is empty.
        /// </exception>
        public static bool VerifyResponseAuthenticator(
            byte[] responsePacket, byte[] requestAuthenticator, byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(responsePacket);
            ArgumentNullException.ThrowIfNull(requestAuthenticator);
            ArgumentNullException.ThrowIfNull(sharedSecret);

            if (responsePacket.Length < MinimumPacketLength)
                throw new ArgumentException(
                    $"RADIUS packet must be at least {MinimumPacketLength} bytes.", nameof(responsePacket));
            if (requestAuthenticator.Length != AuthenticatorLength)
                throw new ArgumentException(
                    $"Request Authenticator must be exactly {AuthenticatorLength} bytes.",
                    nameof(requestAuthenticator));
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            ReadOnlySpan<byte> receivedAuthenticator =
                responsePacket.AsSpan(AuthenticatorOffset, AuthenticatorLength);

            byte[] expected = ResponseAuthenticator(responsePacket, requestAuthenticator, sharedSecret);

            return CryptographicOperations.FixedTimeEquals(receivedAuthenticator, expected);
        }

        /// <summary>
        /// Verifies the Message-Authenticator attribute (Type 80) of a received RADIUS packet
        /// against a locally recomputed HMAC-MD5, as defined in RFC 3579 §3.2.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The verification recomputes HMAC-MD5 over the entire packet with the 16-byte
        /// Message-Authenticator value field temporarily zeroed, then compares the result
        /// against the received value using a constant-time comparison to prevent timing
        /// side-channel attacks.
        /// </para>
        /// <para>
        /// The packet buffer is not modified. A pooled working copy is used for the
        /// zeroing step and is zeroed before being returned to the pool.
        /// </para>
        /// </remarks>
        /// <param name="packetBytes">
        /// The full raw bytes of the received RADIUS packet. Must not be <see langword="null"/>
        /// and must be at least 20 bytes.
        /// </param>
        /// <param name="sharedSecret">
        /// The shared secret negotiated with the RADIUS server, as a pre-encoded byte array.
        /// Used as the HMAC-MD5 key. Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a Message-Authenticator attribute is present and its
        /// HMAC-MD5 matches the recomputed value; <see langword="false"/> if the attribute
        /// is absent or the values do not match.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="packetBytes"/> or <paramref name="sharedSecret"/> is
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="packetBytes"/> is shorter than 20 bytes or
        /// <paramref name="sharedSecret"/> is empty.
        /// </exception>
        public static bool VerifyMessageAuthenticator(byte[] packetBytes, byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(packetBytes);
            ArgumentNullException.ThrowIfNull(sharedSecret);
            if (packetBytes.Length < MinimumPacketLength)
                throw new ArgumentException(
                    $"RADIUS packet must be at least {MinimumPacketLength} bytes.", nameof(packetBytes));
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            const byte MessageAuthType = 80;
            const int MessageAuthLength = 18; // Type(1) + Length(1) + Value(16)

            // Use the declared packet length (octets 3–4) rather than the buffer length
            // to avoid scanning past the RADIUS packet boundary when the UDP buffer is
            // larger than the declared length (e.g., recvfrom padding or oversized buffers).
            ushort declaredLength = (ushort)(packetBytes[PacketLengthOffset] << 8
                                           | packetBytes[PacketLengthOffset + 1]);
            if (declaredLength < MinimumPacketLength || declaredLength > packetBytes.Length)
                return false;

            // Scan the attribute list for Message-Authenticator (Type 80).
            int offset = AttributesOffset;
            int msgAuthOffset = -1;

            while (offset + 2 <= declaredLength)
            {
                byte attrType = packetBytes[offset];
                byte attrLength = packetBytes[offset + 1];

                if (attrLength < 2 || offset + attrLength > declaredLength)
                    break;

                if (attrType == MessageAuthType && attrLength == MessageAuthLength)
                {
                    msgAuthOffset = offset;
                    break;
                }

                offset += attrLength;
            }

            if (msgAuthOffset < 0)
                return false; // Attribute absent — not verifiable.

            // Rent working copy from ArrayPool instead of
            // Clone() which creates an unzeroed heap allocation that cannot be cleaned up.
            byte[] working = System.Buffers.ArrayPool<byte>.Shared.Rent(declaredLength);

            try
            {
                packetBytes.AsSpan(0, declaredLength).CopyTo(working);

                // Per RFC 3579 §3.2: zero the 16-byte value field of Message-Authenticator.
                working.AsSpan(msgAuthOffset + 2, AuthenticatorLength).Clear();

                // Extract the received HMAC value from the unmodified original buffer.
                ReadOnlySpan<byte> received = packetBytes.AsSpan(msgAuthOffset + 2, AuthenticatorLength);
                Span<byte> computed = stackalloc byte[AuthenticatorLength];

                // Copy the secret into an exact-sized key buffer for HMAC.
                byte[] hmacKey = new byte[sharedSecret.Length];
                sharedSecret.AsSpan().CopyTo(hmacKey);
                try
                {
                    using HMACMD5 hmac = new(hmacKey);
                    hmac.TryComputeHash(working.AsSpan(0, declaredLength), computed, out _);

                    return CryptographicOperations.FixedTimeEquals(received, computed);
                }
                finally
                {
                    CryptographicOperations.ZeroMemory(hmacKey);
                }
            }
            finally
            {
                // Zero the working copy — it contains the full packet including sensitive attributes.
                CryptographicOperations.ZeroMemory(working.AsSpan(0, declaredLength));
                System.Buffers.ArrayPool<byte>.Shared.Return(working);
            }
        }

        #endregion

        #region PAP Password Encoding

        /// <summary>
        /// Encodes a PAP (Password Authentication Protocol) password using the
        /// RADIUS User-Password obfuscation scheme defined in RFC 2865 §5.2.
        /// </summary>
        /// <remarks>
        /// <para>The password is encrypted as follows:</para>
        /// <para>
        /// For the first 16-byte block:<br/>
        /// <c>c(1) = p(1) XOR MD5(SharedSecret + RequestAuthenticator)</c>
        /// </para>
        /// <para>
        /// For each subsequent 16-byte block <c>i</c>:<br/>
        /// <c>c(i) = p(i) XOR MD5(SharedSecret + c(i-1))</c>
        /// </para>
        /// <para>
        /// The password is zero-padded to the nearest multiple of 16 bytes before encoding.
        /// Sensitive intermediate buffers are zeroed after use.
        /// </para>
        /// </remarks>
        /// <param name="userPassBytes">
        /// The raw password bytes (ASCII-encoded). Must be between 1 and 128 bytes inclusive.
        /// </param>
        /// <param name="requestAuthenticator">
        /// The 16-byte Request Authenticator from the Access-Request packet, as defined in RFC 2865 §3.
        /// </param>
        /// <param name="sharedSecret">
        /// The shared secret negotiated with the RADIUS server, as a pre-encoded byte array.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>
        /// A byte array of length padded to the nearest multiple of 16, containing the
        /// obfuscated password suitable for use in the User-Password attribute (Type 2).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="userPassBytes"/>, <paramref name="requestAuthenticator"/>,
        /// or <paramref name="sharedSecret"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="userPassBytes"/> is empty or exceeds 128 bytes,
        /// when <paramref name="requestAuthenticator"/> is not exactly 16 bytes,
        /// or when <paramref name="sharedSecret"/> is empty.
        /// </exception>
        public static byte[] EncodePapPassword(byte[] userPassBytes, byte[] requestAuthenticator, byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(userPassBytes);
            ArgumentNullException.ThrowIfNull(requestAuthenticator);
            ArgumentNullException.ThrowIfNull(sharedSecret);

            if (userPassBytes.Length == 0)
                throw new ArgumentException("Password must not be empty.", nameof(userPassBytes));
            if (userPassBytes.Length > 128)
                throw new ArgumentException("PAP password cannot be greater than 128 bytes.", nameof(userPassBytes));
            if (requestAuthenticator.Length != AuthenticatorLength)
                throw new ArgumentException(
                    $"Request Authenticator must be exactly {AuthenticatorLength} bytes.",
                    nameof(requestAuthenticator));
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            // Zero-pad the password to the nearest multiple of 16 bytes (RFC 2865 §5.2).
            int paddedLength = (userPassBytes.Length + 15) / 16 * 16;
            byte[] encryptedPass = new byte[paddedLength];
            userPassBytes.AsSpan().CopyTo(encryptedPass);

            int hashInputLength = sharedSecret.Length + AuthenticatorLength;
            byte[] hashInput = System.Buffers.ArrayPool<byte>.Shared.Rent(hashInputLength);

            // Track success so we can zero encryptedPass on failure.
            // Before XOR completes, encryptedPass contains plaintext password bytes.
            bool success = false;
            try
            {
                Span<byte> hashInputSpan = hashInput.AsSpan(0, hashInputLength);
                Span<byte> secretSlot = hashInputSpan[..sharedSecret.Length];
                Span<byte> chainSlot = hashInputSpan[sharedSecret.Length..];

                sharedSecret.AsSpan().CopyTo(secretSlot);
                requestAuthenticator.AsSpan().CopyTo(chainSlot);

                int chunkCount = paddedLength / AuthenticatorLength;
                Span<byte> hash = stackalloc byte[AuthenticatorLength];

                for (int chunk = 0; chunk < chunkCount; chunk++)
                {
                    MD5.TryHashData(hashInputSpan, hash, out _);

                    int offset = chunk * AuthenticatorLength;
                    Span<byte> block = encryptedPass.AsSpan(offset, AuthenticatorLength);

                    ref byte blockRef = ref MemoryMarshal.GetReference(block);
                    ref byte hashRef = ref MemoryMarshal.GetReference(hash);
                    for (int i = 0; i < AuthenticatorLength; i++)
                        Unsafe.Add(ref blockRef, i) ^= Unsafe.Add(ref hashRef, i);

                    block.CopyTo(chainSlot);
                }

                success = true;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(hashInput.AsSpan(0, hashInputLength));
                System.Buffers.ArrayPool<byte>.Shared.Return(hashInput);

                // If encoding failed mid-XOR, encryptedPass still contains plaintext bytes.
                if (!success)
                    CryptographicOperations.ZeroMemory(encryptedPass);
            }

            return encryptedPass;
        }

        /// <summary>
        /// Decodes a PAP (Password Authentication Protocol) User-Password attribute value
        /// back to plaintext using the RADIUS obfuscation scheme defined in RFC 2865 §5.2.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Decryption is the same XOR operation as encryption — the algorithm is symmetric.
        /// For each 16-byte block <c>i</c>:
        /// </para>
        /// <para>
        /// Block 1: <c>p(1) = c(1) XOR MD5(SharedSecret + RequestAuthenticator)</c><br/>
        /// Block i: <c>p(i) = c(i) XOR MD5(SharedSecret + c(i-1))</c>
        /// </para>
        /// <para>
        /// Trailing zero-pad bytes introduced during encoding are stripped from the result.
        /// Sensitive intermediate buffers are zeroed after use.
        /// </para>
        /// <para>
        /// <b>Known limitation:</b> Passwords that legitimately end with one or more
        /// <c>0x00</c> bytes will have those bytes stripped, as the padding zeros added
        /// during encoding are indistinguishable from legitimate trailing null bytes
        /// (RFC 2865 §5.2 does not define a length field for the plaintext password).
        /// </para>
        /// </remarks>
        /// <param name="encodedPassBytes">
        /// The obfuscated password bytes from the User-Password attribute (Type 2).
        /// Must be between 16 and 128 bytes inclusive, and a multiple of 16.
        /// </param>
        /// <param name="requestAuthenticator">
        /// The 16-byte Request Authenticator from the Access-Request packet (RFC 2865 §3).
        /// </param>
        /// <param name="sharedSecret">
        /// The shared secret negotiated with the RADIUS server, as a pre-encoded byte array.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>
        /// The decoded plaintext password bytes with trailing zero-padding stripped.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any parameter is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="encodedPassBytes"/> is empty, exceeds 128 bytes,
        /// is not a multiple of 16 bytes, when <paramref name="requestAuthenticator"/>
        /// is not exactly 16 bytes, or when <paramref name="sharedSecret"/> is empty.
        /// </exception>
        public static byte[] DecodePapPassword(byte[] encodedPassBytes, byte[] requestAuthenticator, byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(encodedPassBytes);
            ArgumentNullException.ThrowIfNull(requestAuthenticator);
            ArgumentNullException.ThrowIfNull(sharedSecret);

            if (encodedPassBytes.Length == 0 || encodedPassBytes.Length > 128)
                throw new ArgumentException(
                    "Encoded password must be between 1 and 128 bytes.", nameof(encodedPassBytes));
            if (encodedPassBytes.Length % AuthenticatorLength != 0)
                throw new ArgumentException(
                    "Encoded password length must be a multiple of 16 bytes (RFC 2865 §5.2).",
                    nameof(encodedPassBytes));
            if (requestAuthenticator.Length != AuthenticatorLength)
                throw new ArgumentException(
                    $"Request Authenticator must be exactly {AuthenticatorLength} bytes.",
                    nameof(requestAuthenticator));
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            int hashInputLength = sharedSecret.Length + AuthenticatorLength;
            int chunkCount = encodedPassBytes.Length / AuthenticatorLength;

            byte[] decoded = new byte[encodedPassBytes.Length];
            byte[] hashInput = System.Buffers.ArrayPool<byte>.Shared.Rent(hashInputLength);

            try
            {
                Span<byte> hashInputSpan = hashInput.AsSpan(0, hashInputLength);
                Span<byte> secretSlot = hashInputSpan[..sharedSecret.Length];
                Span<byte> chainSlot = hashInputSpan[sharedSecret.Length..];

                sharedSecret.AsSpan().CopyTo(secretSlot);
                requestAuthenticator.AsSpan().CopyTo(chainSlot);

                Span<byte> hash = stackalloc byte[AuthenticatorLength];

                for (int chunk = 0; chunk < chunkCount; chunk++)
                {
                    MD5.TryHashData(hashInputSpan, hash, out _);

                    int offset = chunk * AuthenticatorLength;
                    Span<byte> encoded = encodedPassBytes.AsSpan(offset, AuthenticatorLength);
                    Span<byte> plain = decoded.AsSpan(offset, AuthenticatorLength);

                    ref byte encodedRef = ref MemoryMarshal.GetReference(encoded);
                    ref byte hashRef = ref MemoryMarshal.GetReference(hash);
                    ref byte plainRef = ref MemoryMarshal.GetReference(plain);

                    for (int i = 0; i < AuthenticatorLength; i++)
                        Unsafe.Add(ref plainRef, i) =
                            (byte)(Unsafe.Add(ref encodedRef, i) ^ Unsafe.Add(ref hashRef, i));

                    // Chain: next block uses the encoded (ciphertext) block, not the decoded one.
                    encoded.CopyTo(chainSlot);
                }
            }
            finally
            {
                CryptographicOperations.ZeroMemory(hashInput.AsSpan(0, hashInputLength));
                System.Buffers.ArrayPool<byte>.Shared.Return(hashInput);
            }

            // Strip trailing zero-padding added during encoding (RFC 2865 §5.2).
            int actualLength = decoded.Length;
            while (actualLength > 0 && decoded[actualLength - 1] == 0)
                actualLength--;

            // Zero the intermediate decoded buffer after extracting the result.
            // Without this, the full plaintext password remains on the heap until GC collects it.
            byte[] result = decoded.AsSpan(0, actualLength).ToArray();
            CryptographicOperations.ZeroMemory(decoded);
            return result;
        }

        #endregion

        #region CHAP

        /// <summary>
        /// Computes a CHAP response value for use in the CHAP-Password attribute (Type 3),
        /// as defined in RFC 1994 and referenced in RFC 2865 §5.3.
        /// </summary>
        /// <remarks>
        /// The CHAP response is: <c>MD5(ChapId + Password + Challenge)</c><br/>
        /// The CHAP-Password attribute value is: <c>ChapId(1) + Response(16)</c>
        /// </remarks>
        /// <param name="chapId">
        /// The 1-byte CHAP identifier matching the CHAP-Challenge sent by the NAS.
        /// </param>
        /// <param name="passwordBytes">
        /// The plaintext user password bytes. Must not be <see langword="null"/> or empty.
        /// </param>
        /// <param name="chapChallenge">
        /// The CHAP challenge bytes sent by the NAS. Must not be <see langword="null"/>
        /// and must be at least 1 byte.
        /// </param>
        /// <returns>
        /// A 17-byte array: <c>ChapId(1) + CHAP-Response(16)</c>, ready to be used as the
        /// value of the CHAP-Password attribute (RFC 2865 §5.3).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="passwordBytes"/> or <paramref name="chapChallenge"/>
        /// is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="passwordBytes"/> or <paramref name="chapChallenge"/>
        /// is empty.
        /// </exception>
        public static byte[] ComputeChapResponse(byte chapId, byte[] passwordBytes, byte[] chapChallenge)
        {
            ArgumentNullException.ThrowIfNull(passwordBytes);
            ArgumentNullException.ThrowIfNull(chapChallenge);

            if (passwordBytes.Length == 0)
                throw new ArgumentException("Password must not be empty.", nameof(passwordBytes));
            if (chapChallenge.Length == 0)
                throw new ArgumentException("CHAP challenge must not be empty.", nameof(chapChallenge));

            // Hash input: ChapId(1) + Password(n) + Challenge(m) — RFC 1994 §3.
            int totalLength = 1 + passwordBytes.Length + chapChallenge.Length;
            byte[] hashInput = System.Buffers.ArrayPool<byte>.Shared.Rent(totalLength);

            try
            {
                hashInput[0] = chapId;
                passwordBytes.AsSpan().CopyTo(hashInput.AsSpan(1));
                chapChallenge.AsSpan().CopyTo(hashInput.AsSpan(1 + passwordBytes.Length));

                Span<byte> response = stackalloc byte[AuthenticatorLength];
                MD5.TryHashData(hashInput.AsSpan(0, totalLength), response, out _);

                // Wire format: ChapId(1) + Response(16) = 17 bytes total (RFC 2865 §5.3).
                byte[] result = new byte[17];
                result[0] = chapId;
                response.CopyTo(result.AsSpan(1));
                return result;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(hashInput.AsSpan(0, totalLength));
                System.Buffers.ArrayPool<byte>.Shared.Return(hashInput);
            }
        }

        #endregion

        #region Tunnel Password Encoding

        /// <summary>
        /// Encodes a Tunnel-Password attribute value using the obfuscation scheme
        /// defined in RFC 2868 §3.5.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The encoding uses a 2-byte random salt and computes:
        /// </para>
        /// <para>
        /// <c>b(1) = MD5(SharedSecret + RequestAuthenticator + Salt)</c><br/>
        /// <c>c(1) = p(1) XOR b(1)</c><br/>
        /// <c>b(i) = MD5(SharedSecret + c(i-1))</c><br/>
        /// <c>c(i) = p(i) XOR b(i)</c>
        /// </para>
        /// <para>
        /// The wire value is: <c>Salt(2) + Length(1) + c(1)...c(n)</c>
        /// where Length encodes the plaintext password byte count.
        /// The password is zero-padded to the nearest multiple of 16 bytes.
        /// Sensitive intermediate buffers are zeroed after use.
        /// </para>
        /// </remarks>
        /// <param name="passwordBytes">
        /// The plaintext password bytes. Must not be <see langword="null"/> or empty,
        /// and must not exceed 240 bytes (255 − 3 bytes of salt/length overhead,
        /// rounded down to a 16-byte boundary).
        /// </param>
        /// <param name="requestAuthenticator">
        /// The 16-byte Request Authenticator from the Access-Request packet (RFC 2865 §3).
        /// </param>
        /// <param name="sharedSecret">
        /// The shared secret negotiated with the RADIUS server, as a pre-encoded byte array.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>
        /// A byte array containing the 2-byte salt, the 1-byte plaintext length, and the
        /// obfuscated password blocks, ready to be used as the Tunnel-Password attribute value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any parameter is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="passwordBytes"/> is empty or exceeds 240 bytes,
        /// when <paramref name="requestAuthenticator"/> is not exactly 16 bytes,
        /// or when <paramref name="sharedSecret"/> is empty.
        /// </exception>
        public static byte[] EncodeTunnelPassword(byte[] passwordBytes, byte[] requestAuthenticator, byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(passwordBytes);
            ArgumentNullException.ThrowIfNull(requestAuthenticator);
            ArgumentNullException.ThrowIfNull(sharedSecret);

            if (passwordBytes.Length == 0 || passwordBytes.Length > 240)
                throw new ArgumentException(
                    "Password must be between 1 and 240 bytes.", nameof(passwordBytes));
            if (requestAuthenticator.Length != AuthenticatorLength)
                throw new ArgumentException(
                    $"Request Authenticator must be exactly {AuthenticatorLength} bytes.",
                    nameof(requestAuthenticator));
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            // Generate a 2-byte random salt with the high bit set (RFC 2868 §3.5).
            Span<byte> salt = stackalloc byte[2];
            RandomNumberGenerator.Fill(salt);
            salt[0] |= 0x80;

            // Per RFC 2868 §3.5: the plaintext P that gets encrypted is:
            //   P = Length(1) + Password + zero-padding
            // This entire sequence is padded to a multiple of 16 bytes.
            int plainTextLength = 1 + passwordBytes.Length;
            int paddedLength = (plainTextLength + 15) / 16 * 16;

            // Wire value: Salt(2) + CipherText(paddedLength).
            // The Length byte is INSIDE the encrypted blocks, not outside.
            byte[] result = new byte[2 + paddedLength];
            result[0] = salt[0];
            result[1] = salt[1];

            // Build plaintext in-place starting at offset 2 (will be XOR'd into ciphertext).
            // Defensive: ensure password length fits in a single byte (RFC 2868 §3.5).
            if (passwordBytes.Length > byte.MaxValue)
                throw new ArgumentException(
                    $"Password length ({passwordBytes.Length}) exceeds the single-byte length field maximum (255).",
                    nameof(passwordBytes));
            result[2] = (byte)passwordBytes.Length;
            passwordBytes.AsSpan().CopyTo(result.AsSpan(3));
            // Bytes beyond the password are already zero (runtime guarantee = zero padding).

            int firstBlockInputLen = sharedSecret.Length + AuthenticatorLength + 2;
            int chainBlockInputLen = sharedSecret.Length + AuthenticatorLength;

            byte[] hashInput = System.Buffers.ArrayPool<byte>.Shared.Rent(firstBlockInputLen);

            bool success = false;
            try
            {
                Span<byte> hashInputSpan = hashInput.AsSpan(0, firstBlockInputLen);
                sharedSecret.AsSpan().CopyTo(hashInputSpan[..sharedSecret.Length]);

                requestAuthenticator.AsSpan().CopyTo(hashInputSpan[sharedSecret.Length..]);
                salt.CopyTo(hashInputSpan[(sharedSecret.Length + AuthenticatorLength)..]);

                Span<byte> hash = stackalloc byte[AuthenticatorLength];
                int chunkCount = paddedLength / AuthenticatorLength;

                for (int chunk = 0; chunk < chunkCount; chunk++)
                {
                    MD5.TryHashData(hashInput.AsSpan(0, chunk == 0 ? firstBlockInputLen : chainBlockInputLen), hash, out _);

                    // Ciphertext starts at offset 2 (immediately after the salt).
                    // The Length byte is the first byte of block 1 and gets encrypted.
                    int cipherOffset = 2 + chunk * AuthenticatorLength;
                    Span<byte> block = result.AsSpan(cipherOffset, AuthenticatorLength);

                    ref byte blockRef = ref MemoryMarshal.GetReference(block);
                    ref byte hashRef = ref MemoryMarshal.GetReference(hash);
                    for (int i = 0; i < AuthenticatorLength; i++)
                        Unsafe.Add(ref blockRef, i) ^= Unsafe.Add(ref hashRef, i);

                    block.CopyTo(hashInputSpan[sharedSecret.Length..]);
                }

                success = true;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(hashInput.AsSpan(0, firstBlockInputLen));
                System.Buffers.ArrayPool<byte>.Shared.Return(hashInput);

                if (!success)
                    CryptographicOperations.ZeroMemory(result);
            }

            return result;
        }

        /// <summary>
        /// Decodes a Tunnel-Password attribute value using the obfuscation scheme
        /// defined in RFC 2868 §3.5.
        /// </summary>
        /// <param name="encodedValue">
        /// The full attribute value: Salt(2) + encrypted blocks.
        /// </param>
        /// <param name="requestAuthenticator">
        /// The 16-byte Request Authenticator from the Access-Request packet.
        /// </param>
        /// <param name="sharedSecret">
        /// The shared secret, as a pre-encoded byte array.
        /// Must not be <see langword="null"/> or empty.
        /// </param>
        /// <returns>The decoded plaintext password bytes.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any parameter is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="encodedValue"/> is shorter than 18 bytes or its
        /// cipher blocks are not a multiple of 16 bytes, when
        /// <paramref name="requestAuthenticator"/> is not exactly 16 bytes, or when
        /// <paramref name="sharedSecret"/> is empty.
        /// </exception>
        public static byte[] DecodeTunnelPassword(byte[] encodedValue, byte[] requestAuthenticator, byte[] sharedSecret)
        {
            ArgumentNullException.ThrowIfNull(encodedValue);
            ArgumentNullException.ThrowIfNull(requestAuthenticator);
            ArgumentNullException.ThrowIfNull(sharedSecret);

            // Minimum: Salt(2) + at least one 16-byte block
            if (encodedValue.Length < 18)
                throw new ArgumentException(
                    "Encoded Tunnel-Password must be at least 18 bytes (2-byte salt + 16-byte block).",
                    nameof(encodedValue));
            if ((encodedValue.Length - 2) % AuthenticatorLength != 0)
                throw new ArgumentException(
                    "Encoded Tunnel-Password cipher blocks must be a multiple of 16 bytes.",
                    nameof(encodedValue));
            if (requestAuthenticator.Length != AuthenticatorLength)
                throw new ArgumentException(
                    $"Request Authenticator must be exactly {AuthenticatorLength} bytes.",
                    nameof(requestAuthenticator));
            if (sharedSecret.Length == 0)
                throw new ArgumentException("Shared secret must not be empty.", nameof(sharedSecret));

            ReadOnlySpan<byte> salt = encodedValue.AsSpan(0, 2);
            ReadOnlySpan<byte> cipherText = encodedValue.AsSpan(2);
            int cipherLength = cipherText.Length;
            int chunkCount = cipherLength / AuthenticatorLength;

            int firstBlockInputLen = sharedSecret.Length + AuthenticatorLength + 2;
            int chainBlockInputLen = sharedSecret.Length + AuthenticatorLength;

            byte[] plainText = new byte[cipherLength];
            byte[] hashInput = System.Buffers.ArrayPool<byte>.Shared.Rent(firstBlockInputLen);

            try
            {
                Span<byte> hashInputSpan = hashInput.AsSpan(0, firstBlockInputLen);
                sharedSecret.AsSpan().CopyTo(hashInputSpan[..sharedSecret.Length]);

                // Block 1 seed: secret + authenticator + salt
                requestAuthenticator.AsSpan().CopyTo(hashInputSpan[sharedSecret.Length..]);
                salt.CopyTo(hashInputSpan[(sharedSecret.Length + AuthenticatorLength)..]);

                Span<byte> hash = stackalloc byte[AuthenticatorLength];

                for (int chunk = 0; chunk < chunkCount; chunk++)
                {
                    int activeLen = chunk == 0 ? firstBlockInputLen : chainBlockInputLen;
                    MD5.TryHashData(hashInput.AsSpan(0, activeLen), hash, out _);

                    int offset = chunk * AuthenticatorLength;
                    ReadOnlySpan<byte> cipher = cipherText.Slice(offset, AuthenticatorLength);
                    Span<byte> plain = plainText.AsSpan(offset, AuthenticatorLength);

                    ref byte cipherRef = ref MemoryMarshal.GetReference(cipher);
                    ref byte hashRef = ref MemoryMarshal.GetReference(hash);
                    ref byte plainRef = ref MemoryMarshal.GetReference(plain);

                    for (int i = 0; i < AuthenticatorLength; i++)
                        Unsafe.Add(ref plainRef, i) =
                            (byte)(Unsafe.Add(ref cipherRef, i) ^ Unsafe.Add(ref hashRef, i));

                    // Chain: next block uses the ciphertext block
                    cipher.CopyTo(hashInputSpan[sharedSecret.Length..]);
                }
            }
            finally
            {
                CryptographicOperations.ZeroMemory(hashInput.AsSpan(0, firstBlockInputLen));
                System.Buffers.ArrayPool<byte>.Shared.Return(hashInput);
            }

            // First decrypted byte is the plaintext length field (RFC 2868 §3.5)
            int passwordLength = plainText[0];
            if (passwordLength > plainText.Length - 1)
                passwordLength = plainText.Length - 1; // Defensive clamp

            byte[] result = plainText.AsSpan(1, passwordLength).ToArray();
            CryptographicOperations.ZeroMemory(plainText);
            return result;
        }

        #endregion

        #region Packet Inspection

        /// <summary>
        /// Determines whether a raw byte buffer is a structurally valid RADIUS packet,
        /// as defined in RFC 2865 §3.
        /// </summary>
        /// <remarks>
        /// Checks performed:
        /// <list type="bullet">
        ///   <item><description>Buffer length is between 20 and 4096 bytes inclusive.</description></item>
        ///   <item><description>The big-endian Length field (octets 3–4) matches the buffer length exactly.</description></item>
        /// </list>
        /// This does not verify authenticators or attribute contents.
        /// </remarks>
        /// <param name="packetBytes">The raw UDP payload to validate.</param>
        /// <returns>
        /// <see langword="true"/> if the buffer satisfies RFC 2865 §3 structural requirements;
        /// <see langword="false"/> otherwise.
        /// </returns>
        public static bool IsValidPacketLength(ReadOnlySpan<byte> packetBytes)
        {
            if (packetBytes.Length < MinimumPacketLength || packetBytes.Length > MaximumPacketLength)
                return false;

            ushort declaredLength = (ushort)(packetBytes[PacketLengthOffset] << 8 | packetBytes[PacketLengthOffset + 1]);
            return declaredLength == packetBytes.Length;
        }

        /// <summary>
        /// Validates the structural integrity of a raw RADIUS packet: length fields,
        /// attribute TLV boundaries, and the Code octet, per RFC 2865 §3.
        /// </summary>
        /// <param name="packetBytes">The raw UDP payload to validate.</param>
        /// <returns>
        /// <see langword="true"/> if the packet passes all structural checks;
        /// <see langword="false"/> otherwise.
        /// </returns>
        public static bool ValidatePacketStructure(ReadOnlySpan<byte> packetBytes)
        {
            if (!IsValidPacketLength(packetBytes))
                return false;

            // Verify the Code byte is a known RADIUS packet type.
            if (!Enum.IsDefined((RadiusCode)packetBytes[0]))
                return false;

            // Walk all attribute TLVs to verify none overflow the declared packet length.
            ushort declaredLength = (ushort)(packetBytes[PacketLengthOffset] << 8
                                           | packetBytes[PacketLengthOffset + 1]);
            int offset = AttributesOffset;

            while (offset < declaredLength)
            {
                if (offset + 2 > declaredLength)
                    return false;

                byte attrLength = packetBytes[offset + 1];
                if (attrLength < 2 || offset + attrLength > declaredLength)
                    return false;

                offset += attrLength;
            }

            // All attributes must exactly consume the remaining bytes.
            return offset == declaredLength;
        }

        /// <summary>
        /// Reads the RADIUS Code byte (octet 1) from a raw packet buffer without
        /// constructing a <see cref="RadiusPacket"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RadiusCode ExtractPacketCode(ReadOnlySpan<byte> packetBytes)
            => packetBytes.Length >= MinimumPacketLength
                ? (RadiusCode)packetBytes[0]
                : throw new ArgumentException(
                    $"Packet must be at least {MinimumPacketLength} bytes.", nameof(packetBytes));

        /// <summary>
        /// Reads the RADIUS Identifier byte (octet 2) from a raw packet buffer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ExtractPacketIdentifier(ReadOnlySpan<byte> packetBytes)
            => packetBytes.Length >= MinimumPacketLength
                ? packetBytes[1]
                : throw new ArgumentException(
                    $"Packet must be at least {MinimumPacketLength} bytes.", nameof(packetBytes));

        /// <summary>
        /// Reads the big-endian Length field (octets 3–4) from a raw RADIUS packet buffer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ExtractDeclaredLength(ReadOnlySpan<byte> packetBytes)
            => packetBytes.Length >= MinimumPacketLength
                ? (ushort)(packetBytes[PacketLengthOffset] << 8 | packetBytes[PacketLengthOffset + 1])
                : throw new ArgumentException(
                    $"Packet must be at least {MinimumPacketLength} bytes.", nameof(packetBytes));

        /// <summary>
        /// Reads the 16-byte Authenticator field (octets 5–20) from a raw packet buffer.
        /// </summary>
        public static ReadOnlySpan<byte> ExtractAuthenticator(ReadOnlySpan<byte> packetBytes)
            => packetBytes.Length >= MinimumPacketLength
                ? packetBytes.Slice(AuthenticatorOffset, AuthenticatorLength)
                : throw new ArgumentException(
                    $"Packet must be at least {MinimumPacketLength} bytes.", nameof(packetBytes));

        /// <summary>
        /// Searches a raw RADIUS packet buffer for the first attribute of the specified type
        /// and returns its value bytes, without parsing the entire packet.
        /// </summary>
        /// <remarks>
        /// Operates directly on the wire buffer — no <see cref="RadiusPacket"/> construction
        /// or heap allocation. Returns an empty span when the attribute is not present or
        /// the packet is malformed.
        /// </remarks>
        /// <param name="packetBytes">
        /// The raw RADIUS packet bytes. Must be at least 20 bytes.
        /// </param>
        /// <param name="attributeType">
        /// The <see cref="RadiusAttributeType"/> to search for.
        /// </param>
        /// <returns>
        /// A <see cref="ReadOnlySpan{T}"/> over the attribute value bytes (excluding the
        /// 2-byte TLV header), or an empty span if the attribute is not found.
        /// </returns>
        public static ReadOnlySpan<byte> FindAttributeValue(ReadOnlySpan<byte> packetBytes, RadiusAttributeType attributeType)
        {
            if (packetBytes.Length < MinimumPacketLength)
                return ReadOnlySpan<byte>.Empty;

            // Use the declared packet length (octets 3–4) rather than the buffer length
            // to avoid scanning past the RADIUS packet boundary.
            ushort declaredLength = (ushort)(packetBytes[PacketLengthOffset] << 8
                                           | packetBytes[PacketLengthOffset + 1]);
            if (declaredLength < MinimumPacketLength || declaredLength > packetBytes.Length)
                return ReadOnlySpan<byte>.Empty;

            int offset = AttributesOffset;
            byte target = (byte)attributeType;

            while (offset + 2 <= declaredLength)
            {
                byte type = packetBytes[offset];
                byte length = packetBytes[offset + 1];

                if (length < 2 || offset + length > declaredLength)
                    break;

                if (type == target)
                    return packetBytes.Slice(offset + 2, length - 2);

                offset += length;
            }

            return ReadOnlySpan<byte>.Empty;
        }

        /// <summary>
        /// Enumerates all attribute values of the specified type in a raw RADIUS packet,
        /// without parsing the entire packet or allocating.
        /// </summary>
        /// <remarks>
        /// Many RADIUS attributes (e.g. Reply-Message, Proxy-State, EAP-Message) may appear
        /// more than once in a single packet. This method returns each occurrence.
        /// </remarks>
        public static IEnumerable<byte[]> FindAllAttributeValues(
            byte[] packetBytes, RadiusAttributeType attributeType)
        {
            ArgumentNullException.ThrowIfNull(packetBytes);
            if (packetBytes.Length < MinimumPacketLength)
                yield break;

            // Use the declared packet length (octets 3–4) rather than the buffer length.
            ushort declaredLength = (ushort)(packetBytes[PacketLengthOffset] << 8
                                           | packetBytes[PacketLengthOffset + 1]);
            if (declaredLength < MinimumPacketLength || declaredLength > packetBytes.Length)
                yield break;

            int offset = AttributesOffset;
            byte target = (byte)attributeType;

            while (offset + 2 <= declaredLength)
            {
                byte type = packetBytes[offset];
                byte length = packetBytes[offset + 1];

                if (length < 2 || offset + length > declaredLength)
                    yield break;

                if (type == target)
                    yield return packetBytes.AsSpan(offset + 2, length - 2).ToArray();

                offset += length;
            }
        }

        /// <summary>
        /// Searches a raw RADIUS packet buffer for the first attribute of the specified type
        /// and reads its value as a big-endian 32-bit unsigned integer, as used by most
        /// enumerated and numeric RADIUS attributes (RFC 2865 §5).
        /// </summary>
        /// <param name="packetBytes">The raw RADIUS packet bytes. Must be at least 20 bytes.</param>
        /// <param name="attributeType">The <see cref="RadiusAttributeType"/> to read.</param>
        /// <param name="value">
        /// When this method returns <see langword="true"/>, contains the decoded value;
        /// otherwise, <c>0</c>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the attribute was found and its value is exactly 4 bytes;
        /// <see langword="false"/> otherwise.
        /// </returns>
        public static bool TryGetUInt32Attribute(ReadOnlySpan<byte> packetBytes, RadiusAttributeType attributeType, out uint value)
        {
            ReadOnlySpan<byte> data = FindAttributeValue(packetBytes, attributeType);

            if (data.Length == sizeof(uint))
            {
                value = BinaryPrimitives.ReadUInt32BigEndian(data);
                return true;
            }

            value = 0;
            return false;
        }

        #endregion

        #region Binary Encoding

        /// <summary>
        /// Converts a <see cref="uint"/> to its big-endian 3-byte (24-bit) representation.
        /// </summary>
        /// <param name="value">
        /// The unsigned integer to convert. Must not exceed <c>0xFFFFFF</c> (16,777,215).
        /// </param>
        /// <returns>
        /// A 3-byte array containing the big-endian representation of the 24 least-significant
        /// bits of <paramref name="value"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> exceeds <c>0xFFFFFF</c>.
        /// </exception>
        public static byte[] UintTo3Byte(uint value)
        {
            if (value > 0xFFFFFF)
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, "Value must not exceed 0xFFFFFF (24 bits).");

            return [(byte)(value >> 16), (byte)(value >> 8), (byte)value];
        }

        /// <summary>
        /// Converts a non-negative <see cref="int"/> to its big-endian 3-byte (24-bit) representation.
        /// </summary>
        /// <param name="value">
        /// The signed integer to convert. Must be in the range [0, 0xFFFFFF].
        /// </param>
        /// <returns>
        /// A 3-byte array containing the big-endian representation of <paramref name="value"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is negative or exceeds <c>0xFFFFFF</c>.
        /// </exception>
        public static byte[] IntTo3Byte(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(value), value, "Value must not be negative.");

            return UintTo3Byte((uint)value);
        }

        /// <summary>
        /// Reads 3 consecutive bytes from <paramref name="bytes"/> at <paramref name="offset"/>
        /// and interprets them as a big-endian 24-bit unsigned integer.
        /// </summary>
        /// <remarks>
        /// This is the inverse operation of <see cref="UintTo3Byte"/>:
        /// <code>
        /// ThreeBytesToUInt(UintTo3Byte(v), 0) == v  // for any v in [0, 0xFFFFFF]
        /// </code>
        /// </remarks>
        /// <param name="bytes">
        /// The source buffer containing at least 3 bytes starting at <paramref name="offset"/>.
        /// </param>
        /// <param name="offset">
        /// The zero-based index within <paramref name="bytes"/> at which to begin reading.
        /// Must satisfy <c>offset + 3 &lt;= bytes.Length</c>.
        /// </param>
        /// <returns>
        /// A <see cref="uint"/> in the range [0, 0xFFFFFF] representing the big-endian
        /// value of the 3 bytes read.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="offset"/> is negative or there are fewer than
        /// 3 bytes available from <paramref name="offset"/>.
        /// </exception>
        public static uint ThreeBytesToUInt(ReadOnlySpan<byte> bytes, int offset)
        {
            if (offset < 0 || offset + 3 > bytes.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), offset,
                    "offset must be non-negative and allow at least 3 bytes to be read.");

            return (uint)(bytes[offset] << 16 | bytes[offset + 1] << 8 | bytes[offset + 2]);
        }

        /// <summary>
        /// Converts a <see cref="short"/> to its big-endian (network byte order) 2-byte representation.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A 2-byte array in big-endian order.</returns>
        public static byte[] GetNetworkBytes(short value)
        {
            byte[] bytes = new byte[sizeof(short)];
            BinaryPrimitives.WriteInt16BigEndian(bytes, value);
            return bytes;
        }

        /// <summary>
        /// Converts a <see cref="ushort"/> to its big-endian (network byte order) 2-byte representation.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A 2-byte array in big-endian order.</returns>
        public static byte[] GetNetworkBytes(ushort value)
        {
            byte[] bytes = new byte[sizeof(ushort)];
            BinaryPrimitives.WriteUInt16BigEndian(bytes, value);
            return bytes;
        }

        /// <summary>
        /// Converts an <see cref="int"/> to its big-endian (network byte order) 4-byte representation.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A 4-byte array in big-endian order.</returns>
        public static byte[] GetNetworkBytes(int value)
        {
            byte[] bytes = new byte[sizeof(int)];
            BinaryPrimitives.WriteInt32BigEndian(bytes, value);
            return bytes;
        }

        /// <summary>
        /// Converts a <see cref="uint"/> to its big-endian (network byte order) 4-byte representation.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A 4-byte array in big-endian order.</returns>
        public static byte[] GetNetworkBytes(uint value)
        {
            byte[] bytes = new byte[sizeof(uint)];
            BinaryPrimitives.WriteUInt32BigEndian(bytes, value);
            return bytes;
        }

        /// <summary>
        /// Converts a <see cref="long"/> to its big-endian (network byte order) 8-byte representation.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>An 8-byte array in big-endian order.</returns>
        public static byte[] GetNetworkBytes(long value)
        {
            byte[] bytes = new byte[sizeof(long)];
            BinaryPrimitives.WriteInt64BigEndian(bytes, value);
            return bytes;
        }

        /// <summary>
        /// Converts a <see cref="ulong"/> to its big-endian (network byte order) 8-byte representation.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>An 8-byte array in big-endian order.</returns>
        public static byte[] GetNetworkBytes(ulong value)
        {
            byte[] bytes = new byte[sizeof(ulong)];
            BinaryPrimitives.WriteUInt64BigEndian(bytes, value);
            return bytes;
        }

        /// <summary>
        /// Writes a <see cref="short"/> as a big-endian 2-byte sequence into <paramref name="destination"/>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="destination">The span to write into. Must be at least 2 bytes.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="destination"/> is shorter than 2 bytes.
        /// </exception>
        public static void WriteNetworkBytes(short value, Span<byte> destination)
        {
            if (destination.Length < sizeof(short))
                throw new ArgumentException($"Destination must be at least {sizeof(short)} bytes.", nameof(destination));
            BinaryPrimitives.WriteInt16BigEndian(destination, value);
        }

        /// <summary>
        /// Writes a <see cref="ushort"/> as a big-endian 2-byte sequence into <paramref name="destination"/>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="destination">The span to write into. Must be at least 2 bytes.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="destination"/> is shorter than 2 bytes.
        /// </exception>
        public static void WriteNetworkBytes(ushort value, Span<byte> destination)
        {
            if (destination.Length < sizeof(ushort))
                throw new ArgumentException($"Destination must be at least {sizeof(ushort)} bytes.", nameof(destination));
            BinaryPrimitives.WriteUInt16BigEndian(destination, value);
        }

        /// <summary>
        /// Writes an <see cref="int"/> as a big-endian 4-byte sequence into <paramref name="destination"/>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="destination">The span to write into. Must be at least 4 bytes.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="destination"/> is shorter than 4 bytes.
        /// </exception>
        public static void WriteNetworkBytes(int value, Span<byte> destination)
        {
            if (destination.Length < sizeof(int))
                throw new ArgumentException($"Destination must be at least {sizeof(int)} bytes.", nameof(destination));
            BinaryPrimitives.WriteInt32BigEndian(destination, value);
        }

        /// <summary>
        /// Writes a <see cref="uint"/> as a big-endian 4-byte sequence into <paramref name="destination"/>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="destination">The span to write into. Must be at least 4 bytes.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="destination"/> is shorter than 4 bytes.
        /// </exception>
        public static void WriteNetworkBytes(uint value, Span<byte> destination)
        {
            if (destination.Length < sizeof(uint))
                throw new ArgumentException($"Destination must be at least {sizeof(uint)} bytes.", nameof(destination));
            BinaryPrimitives.WriteUInt32BigEndian(destination, value);
        }

        /// <summary>
        /// Writes a <see cref="long"/> as a big-endian 8-byte sequence into <paramref name="destination"/>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="destination">The span to write into. Must be at least 8 bytes.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="destination"/> is shorter than 8 bytes.
        /// </exception>
        public static void WriteNetworkBytes(long value, Span<byte> destination)
        {
            if (destination.Length < sizeof(long))
                throw new ArgumentException($"Destination must be at least {sizeof(long)} bytes.", nameof(destination));
            BinaryPrimitives.WriteInt64BigEndian(destination, value);
        }

        /// <summary>
        /// Writes a <see cref="ulong"/> as a big-endian 8-byte sequence into <paramref name="destination"/>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="destination">The span to write into. Must be at least 8 bytes.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="destination"/> is shorter than 8 bytes.
        /// </exception>
        public static void WriteNetworkBytes(ulong value, Span<byte> destination)
        {
            if (destination.Length < sizeof(ulong))
                throw new ArgumentException($"Destination must be at least {sizeof(ulong)} bytes.", nameof(destination));
            BinaryPrimitives.WriteUInt64BigEndian(destination, value);
        }

        /// <summary>Reads a big-endian <see cref="short"/> from <paramref name="source"/> at <paramref name="offset"/>.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadInt16NetworkBytes(ReadOnlySpan<byte> source, int offset = 0)
            => BinaryPrimitives.ReadInt16BigEndian(source[offset..]);

        /// <summary>Reads a big-endian <see cref="ushort"/> from <paramref name="source"/> at <paramref name="offset"/>.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUInt16NetworkBytes(ReadOnlySpan<byte> source, int offset = 0)
            => BinaryPrimitives.ReadUInt16BigEndian(source[offset..]);

        /// <summary>Reads a big-endian <see cref="int"/> from <paramref name="source"/> at <paramref name="offset"/>.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadInt32NetworkBytes(ReadOnlySpan<byte> source, int offset = 0)
            => BinaryPrimitives.ReadInt32BigEndian(source[offset..]);

        /// <summary>Reads a big-endian <see cref="uint"/> from <paramref name="source"/> at <paramref name="offset"/>.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUInt32NetworkBytes(ReadOnlySpan<byte> source, int offset = 0)
            => BinaryPrimitives.ReadUInt32BigEndian(source[offset..]);

        /// <summary>Reads a big-endian <see cref="long"/> from <paramref name="source"/> at <paramref name="offset"/>.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReadInt64NetworkBytes(ReadOnlySpan<byte> source, int offset = 0)
            => BinaryPrimitives.ReadInt64BigEndian(source[offset..]);

        /// <summary>Reads a big-endian <see cref="ulong"/> from <paramref name="source"/> at <paramref name="offset"/>.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadUInt64NetworkBytes(ReadOnlySpan<byte> source, int offset = 0)
            => BinaryPrimitives.ReadUInt64BigEndian(source[offset..]);

        #endregion

        #region Private Helpers

        /// <summary>
        /// Core hashing primitive shared by <see cref="AccountingRequestAuthenticator"/> and
        /// <see cref="ResponseAuthenticator"/>: concatenates <paramref name="packetData"/> with
        /// the <paramref name="sharedSecret"/> bytes and returns the MD5 digest.
        /// </summary>
        /// <remarks>
        /// The combined buffer is rented from <see cref="System.Buffers.ArrayPool{T}.Shared"/>
        /// to avoid a per-call heap allocation, and is zeroed and returned in the
        /// <see langword="finally"/> block regardless of outcome.
        /// </remarks>
        private static byte[] HashPacketWithSecret(byte[] packetData, int packetLength, byte[] sharedSecret)
        {
            int totalLength = packetLength + sharedSecret.Length;

            byte[] buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(totalLength);
            try
            {
                Span<byte> bufferSpan = buffer.AsSpan(0, totalLength);
                packetData.AsSpan(0, packetLength).CopyTo(bufferSpan);
                sharedSecret.AsSpan().CopyTo(bufferSpan[packetLength..]);

                return MD5.HashData(bufferSpan);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(buffer.AsSpan(0, totalLength));
                System.Buffers.ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        #endregion
    }
}