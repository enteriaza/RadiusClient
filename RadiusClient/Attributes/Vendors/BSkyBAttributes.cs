using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a BSkyB / Sky UK (IANA PEN 21859) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.bskyb</c>.
    /// </summary>
    public enum BSkyBAttributeType : byte
    {
        /// <summary>BSKYB-Auth-Realm (Type 1). String. Authentication realm name.</summary>
        AUTH_REALM = 1,

        /// <summary>BSKYB-Require-Encryption (Type 2). Integer. Require encryption flag.</summary>
        REQUIRE_ENCRYPTION = 2,

        /// <summary>BSKYB-IP-Throttle-Rate (Type 3). Integer. IP throttle rate in Kbps.</summary>
        IP_THROTTLE_RATE = 3
    }

    /// <summary>
    /// BSKYB-Require-Encryption attribute values (Type 2).
    /// </summary>
    public enum BSKYB_REQUIRE_ENCRYPTION
    {
        /// <summary>Encryption not required.</summary>
        NO = 0,

        /// <summary>Encryption required.</summary>
        YES = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing BSkyB / Sky UK
    /// (IANA PEN 21859) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.bskyb</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// BSkyB's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 21859</c>.
    /// </para>
    /// <para>
    /// These attributes are used by BSkyB (Sky UK) broadband platforms for
    /// RADIUS-based authentication realm selection, encryption enforcement,
    /// and IP bandwidth throttling.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(BSkyBAttributes.AuthRealm("sky-broadband"));
    /// packet.SetAttribute(BSkyBAttributes.RequireEncryption(BSKYB_REQUIRE_ENCRYPTION.YES));
    /// packet.SetAttribute(BSkyBAttributes.IpThrottleRate(8000));
    /// </code>
    /// </remarks>
    public static class BSkyBAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for BSkyB (Sky UK).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 21859;

        #region Integer Attributes

        /// <summary>
        /// Creates a BSKYB-Require-Encryption attribute (Type 2) with the specified setting.
        /// </summary>
        /// <param name="value">Whether encryption is required. See <see cref="BSKYB_REQUIRE_ENCRYPTION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RequireEncryption(BSKYB_REQUIRE_ENCRYPTION value)
        {
            return CreateInteger(BSkyBAttributeType.REQUIRE_ENCRYPTION, (int)value);
        }

        /// <summary>
        /// Creates a BSKYB-IP-Throttle-Rate attribute (Type 3) with the specified rate.
        /// </summary>
        /// <param name="value">The IP throttle rate in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpThrottleRate(int value)
        {
            return CreateInteger(BSkyBAttributeType.IP_THROTTLE_RATE, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a BSKYB-Auth-Realm attribute (Type 1) with the specified realm name.
        /// </summary>
        /// <param name="value">The authentication realm name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthRealm(string value)
        {
            return CreateString(BSkyBAttributeType.AUTH_REALM, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified BSkyB attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(BSkyBAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified BSkyB attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(BSkyBAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}