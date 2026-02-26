using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Gemtek Technology (IANA PEN 10529) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.gemtek</c>.
    /// </summary>
    public enum GemtekAttributeType : byte
    {
        /// <summary>Gemtek-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Gemtek-User-Role (Type 2). String. User role name.</summary>
        USER_ROLE = 2,

        /// <summary>Gemtek-VLAN-Id (Type 3). Integer. VLAN identifier.</summary>
        VLAN_ID = 3,

        /// <summary>Gemtek-Bandwidth-Max-Up (Type 4). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 4,

        /// <summary>Gemtek-Bandwidth-Max-Down (Type 5). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 5,

        /// <summary>Gemtek-SSID (Type 6). String. Wireless SSID.</summary>
        SSID = 6,

        /// <summary>Gemtek-Session-Timeout (Type 7). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 7,

        /// <summary>Gemtek-URL-Redirect (Type 8). String. URL redirect destination.</summary>
        URL_REDIRECT = 8
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Gemtek Technology
    /// (IANA PEN 10529) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.gemtek</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Gemtek's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 10529</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Gemtek Technology wireless access points
    /// and broadband CPE for RADIUS-based user role assignment, VLAN mapping,
    /// upstream/downstream bandwidth provisioning, SSID identification, session
    /// timeout configuration, URL redirection, and general-purpose attribute-value
    /// pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(GemtekAttributes.UserRole("guest"));
    /// packet.SetAttribute(GemtekAttributes.VlanId(200));
    /// packet.SetAttribute(GemtekAttributes.BandwidthMaxUp(5000));
    /// packet.SetAttribute(GemtekAttributes.BandwidthMaxDown(20000));
    /// packet.SetAttribute(GemtekAttributes.UrlRedirect("https://portal.example.com"));
    /// </code>
    /// </remarks>
    public static class GemtekAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Gemtek Technology.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 10529;

        #region Integer Attributes

        /// <summary>
        /// Creates a Gemtek-VLAN-Id attribute (Type 3) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(GemtekAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Gemtek-Bandwidth-Max-Up attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(GemtekAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Gemtek-Bandwidth-Max-Down attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(GemtekAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a Gemtek-Session-Timeout attribute (Type 7) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(GemtekAttributeType.SESSION_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Gemtek-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(GemtekAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Gemtek-User-Role attribute (Type 2) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(GemtekAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates a Gemtek-SSID attribute (Type 6) with the specified wireless SSID.
        /// </summary>
        /// <param name="value">The wireless SSID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ssid(string value)
        {
            return CreateString(GemtekAttributeType.SSID, value);
        }

        /// <summary>
        /// Creates a Gemtek-URL-Redirect attribute (Type 8) with the specified URL.
        /// </summary>
        /// <param name="value">The URL redirect destination. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UrlRedirect(string value)
        {
            return CreateString(GemtekAttributeType.URL_REDIRECT, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Gemtek attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(GemtekAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Gemtek attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(GemtekAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}