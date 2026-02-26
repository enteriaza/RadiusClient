using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Yubico (IANA PEN 41482) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.yubico</c>.
    /// </summary>
    /// <remarks>
    /// Yubico AB produces hardware security keys (YubiKey series) and authentication
    /// platforms for strong two-factor (2FA) and multi-factor authentication (MFA)
    /// using OTP, FIDO U2F, FIDO2/WebAuthn, PIV, and OpenPGP standards. These
    /// RADIUS attributes are used by the Yubico RADIUS validation server
    /// (yubico-radius-server) and FreeRADIUS rlm_yubikey module.
    /// </remarks>
    public enum YubicoAttributeType : byte
    {
        /// <summary>Yubico-OTP-Token (Type 1). String. The YubiKey OTP token string.</summary>
        OTP_TOKEN = 1,

        /// <summary>Yubico-OTP-Public-ID (Type 2). String. The YubiKey public ID (modhex-encoded first 12 characters of OTP).</summary>
        OTP_PUBLIC_ID = 2,

        /// <summary>Yubico-OTP-Private-ID (Type 3). String. The YubiKey private ID (hex-encoded 6-byte UID).</summary>
        OTP_PRIVATE_ID = 3,

        /// <summary>Yubico-OTP-Session-Counter (Type 4). Integer. Session counter (non-volatile, increments on power-up).</summary>
        OTP_SESSION_COUNTER = 4,

        /// <summary>Yubico-OTP-Timestamp (Type 5). Integer. Token internal timestamp (8 Hz timer).</summary>
        OTP_TIMESTAMP = 5,

        /// <summary>Yubico-OTP-Random (Type 6). Integer. Random value from the OTP generation.</summary>
        OTP_RANDOM = 6,

        /// <summary>Yubico-OTP-CRC (Type 7). Integer. CRC-16 checksum of the OTP data.</summary>
        OTP_CRC = 7,

        /// <summary>Yubico-OTP-Counter (Type 8). Integer. Usage counter (volatile, increments per OTP within a session).</summary>
        OTP_COUNTER = 8
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Yubico
    /// (IANA PEN 41482) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.yubico</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Yubico's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 41482</c>.
    /// </para>
    /// <para>
    /// These attributes are used by the Yubico RADIUS validation server and the
    /// FreeRADIUS <c>rlm_yubikey</c> module for RADIUS-based YubiKey OTP token
    /// validation, exposing the decoded OTP fields: the full OTP string, public
    /// and private identifiers, session counter, internal timestamp, random nonce,
    /// CRC-16 checksum, and per-session usage counter. These attributes are
    /// typically included in <c>Access-Accept</c> responses or internal server
    /// attributes after successful OTP validation.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(YubicoAttributes.OtpPublicId("cccccccgklgcbh"));
    /// packet.SetAttribute(YubicoAttributes.OtpSessionCounter(42));
    /// packet.SetAttribute(YubicoAttributes.OtpCounter(7));
    /// packet.SetAttribute(YubicoAttributes.OtpTimestamp(1234567));
    /// </code>
    /// </remarks>
    public static class YubicoAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Yubico AB.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 41482;

        #region Integer Attributes

        /// <summary>
        /// Creates a Yubico-OTP-Session-Counter attribute (Type 4) with the specified value.
        /// </summary>
        /// <param name="value">The session counter (non-volatile, increments on power-up).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes OtpSessionCounter(int value)
        {
            return CreateInteger(YubicoAttributeType.OTP_SESSION_COUNTER, value);
        }

        /// <summary>
        /// Creates a Yubico-OTP-Timestamp attribute (Type 5) with the specified value.
        /// </summary>
        /// <param name="value">The token internal timestamp (8 Hz timer).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes OtpTimestamp(int value)
        {
            return CreateInteger(YubicoAttributeType.OTP_TIMESTAMP, value);
        }

        /// <summary>
        /// Creates a Yubico-OTP-Random attribute (Type 6) with the specified value.
        /// </summary>
        /// <param name="value">The random value from OTP generation.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes OtpRandom(int value)
        {
            return CreateInteger(YubicoAttributeType.OTP_RANDOM, value);
        }

        /// <summary>
        /// Creates a Yubico-OTP-CRC attribute (Type 7) with the specified value.
        /// </summary>
        /// <param name="value">The CRC-16 checksum of the OTP data.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes OtpCrc(int value)
        {
            return CreateInteger(YubicoAttributeType.OTP_CRC, value);
        }

        /// <summary>
        /// Creates a Yubico-OTP-Counter attribute (Type 8) with the specified value.
        /// </summary>
        /// <param name="value">The usage counter (volatile, increments per OTP within a session).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes OtpCounter(int value)
        {
            return CreateInteger(YubicoAttributeType.OTP_COUNTER, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Yubico-OTP-Token attribute (Type 1) with the specified OTP string.
        /// </summary>
        /// <param name="value">The full YubiKey OTP token string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OtpToken(string value)
        {
            return CreateString(YubicoAttributeType.OTP_TOKEN, value);
        }

        /// <summary>
        /// Creates a Yubico-OTP-Public-ID attribute (Type 2) with the specified public ID.
        /// </summary>
        /// <param name="value">The YubiKey public ID (modhex-encoded first 12 characters of OTP). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OtpPublicId(string value)
        {
            return CreateString(YubicoAttributeType.OTP_PUBLIC_ID, value);
        }

        /// <summary>
        /// Creates a Yubico-OTP-Private-ID attribute (Type 3) with the specified private ID.
        /// </summary>
        /// <param name="value">The YubiKey private ID (hex-encoded 6-byte UID). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OtpPrivateId(string value)
        {
            return CreateString(YubicoAttributeType.OTP_PRIVATE_ID, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(YubicoAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(YubicoAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}