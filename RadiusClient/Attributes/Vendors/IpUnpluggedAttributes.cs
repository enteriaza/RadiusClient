using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an ipUnplugged (IANA PEN 13926) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.ipunplugged</c>.
    /// </summary>
    /// <remarks>
    /// ipUnplugged (now Meru Networks / Fortinet) produced wireless LAN
    /// infrastructure and controller platforms.
    /// </remarks>
    public enum IpUnpluggedAttributeType : byte
    {
        /// <summary>IPUnplugged-User-Role (Type 1). String. User role name.</summary>
        USER_ROLE = 1,

        /// <summary>IPUnplugged-SSID (Type 2). String. Wireless SSID.</summary>
        SSID = 2,

        /// <summary>IPUnplugged-AP-Name (Type 3). String. Access point name.</summary>
        AP_NAME = 3,

        /// <summary>IPUnplugged-Location-ID (Type 4). String. Location identifier.</summary>
        LOCATION_ID = 4,

        /// <summary>IPUnplugged-VLAN-Name (Type 5). String. VLAN name to assign.</summary>
        VLAN_NAME = 5,

        /// <summary>IPUnplugged-Bandwidth-Max-Up (Type 6). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 6,

        /// <summary>IPUnplugged-Bandwidth-Max-Down (Type 7). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 7,

        /// <summary>IPUnplugged-URL-Redirect (Type 8). String. Captive portal redirect URL.</summary>
        URL_REDIRECT = 8,

        /// <summary>IPUnplugged-ACL-Name (Type 9). String. ACL name to apply.</summary>
        ACL_NAME = 9,

        /// <summary>IPUnplugged-Session-Timeout (Type 10). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing ipUnplugged
    /// (IANA PEN 13926) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.ipunplugged</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ipUnplugged's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 13926</c>.
    /// </para>
    /// <para>
    /// These attributes are used by ipUnplugged (now Meru Networks / Fortinet)
    /// wireless LAN controllers for RADIUS-based user role assignment, SSID and
    /// AP identification, location tracking, VLAN assignment by name,
    /// upstream/downstream bandwidth provisioning, captive portal URL
    /// redirection, ACL enforcement, and session timeout configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(IpUnpluggedAttributes.UserRole("guest"));
    /// packet.SetAttribute(IpUnpluggedAttributes.VlanName("guest-vlan"));
    /// packet.SetAttribute(IpUnpluggedAttributes.BandwidthMaxUp(5000));
    /// packet.SetAttribute(IpUnpluggedAttributes.BandwidthMaxDown(20000));
    /// packet.SetAttribute(IpUnpluggedAttributes.UrlRedirect("https://portal.example.com"));
    /// </code>
    /// </remarks>
    public static class IpUnpluggedAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for ipUnplugged.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 13926;

        #region Integer Attributes

        /// <summary>
        /// Creates an IPUnplugged-Bandwidth-Max-Up attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(IpUnpluggedAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates an IPUnplugged-Bandwidth-Max-Down attribute (Type 7) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(IpUnpluggedAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates an IPUnplugged-Session-Timeout attribute (Type 10) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(IpUnpluggedAttributeType.SESSION_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an IPUnplugged-User-Role attribute (Type 1) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(IpUnpluggedAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates an IPUnplugged-SSID attribute (Type 2) with the specified wireless SSID.
        /// </summary>
        /// <param name="value">The wireless SSID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ssid(string value)
        {
            return CreateString(IpUnpluggedAttributeType.SSID, value);
        }

        /// <summary>
        /// Creates an IPUnplugged-AP-Name attribute (Type 3) with the specified access point name.
        /// </summary>
        /// <param name="value">The access point name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApName(string value)
        {
            return CreateString(IpUnpluggedAttributeType.AP_NAME, value);
        }

        /// <summary>
        /// Creates an IPUnplugged-Location-ID attribute (Type 4) with the specified location.
        /// </summary>
        /// <param name="value">The location identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LocationId(string value)
        {
            return CreateString(IpUnpluggedAttributeType.LOCATION_ID, value);
        }

        /// <summary>
        /// Creates an IPUnplugged-VLAN-Name attribute (Type 5) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(IpUnpluggedAttributeType.VLAN_NAME, value);
        }

        /// <summary>
        /// Creates an IPUnplugged-URL-Redirect attribute (Type 8) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UrlRedirect(string value)
        {
            return CreateString(IpUnpluggedAttributeType.URL_REDIRECT, value);
        }

        /// <summary>
        /// Creates an IPUnplugged-ACL-Name attribute (Type 9) with the specified ACL name.
        /// </summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value)
        {
            return CreateString(IpUnpluggedAttributeType.ACL_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified ipUnplugged attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(IpUnpluggedAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified ipUnplugged attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(IpUnpluggedAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}