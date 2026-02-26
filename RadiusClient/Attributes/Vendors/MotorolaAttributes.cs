using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Motorola / Zebra Technologies (IANA PEN 161) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.motorola</c>.
    /// </summary>
    /// <remarks>
    /// Motorola Solutions (formerly Symbol Technologies, now part of Zebra Technologies
    /// for enterprise networking) produced enterprise wireless LAN controllers and
    /// access points (WiNG, RFS, AP series).
    /// </remarks>
    public enum MotorolaAttributeType : byte
    {
        /// <summary>Motorola-WLANGroup-Name (Type 1). String. WLAN group name.</summary>
        WLANGROUP_NAME = 1,

        /// <summary>Motorola-VLAN-Name (Type 2). String. VLAN name to assign.</summary>
        VLAN_NAME = 2,

        /// <summary>Motorola-Priv-Level (Type 3). Integer. CLI privilege level.</summary>
        PRIV_LEVEL = 3,

        /// <summary>Motorola-Role (Type 4). String. User role name.</summary>
        ROLE = 4,

        /// <summary>Motorola-Firewall-Policy (Type 5). String. Firewall policy name.</summary>
        FIREWALL_POLICY = 5,

        /// <summary>Motorola-ACL-Name (Type 6). String. ACL name to apply.</summary>
        ACL_NAME = 6,

        /// <summary>Motorola-Rate-Limit-Up (Type 7). Integer. Upstream rate limit in Kbps.</summary>
        RATE_LIMIT_UP = 7,

        /// <summary>Motorola-Rate-Limit-Down (Type 8). Integer. Downstream rate limit in Kbps.</summary>
        RATE_LIMIT_DOWN = 8,

        /// <summary>Motorola-VLAN-Id (Type 9). Integer. VLAN identifier to assign.</summary>
        VLAN_ID = 9,

        /// <summary>Motorola-Session-Timeout (Type 10). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 10,

        /// <summary>Motorola-Captive-Portal (Type 11). String. Captive portal profile name.</summary>
        CAPTIVE_PORTAL = 11,

        /// <summary>Motorola-Redirect-URL (Type 12). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 12,

        /// <summary>Motorola-AP-Location (Type 13). String. Access point location.</summary>
        AP_LOCATION = 13,

        /// <summary>Motorola-AP-Name (Type 14). String. Access point name.</summary>
        AP_NAME = 14,

        /// <summary>Motorola-SSID (Type 15). String. Wireless SSID name.</summary>
        SSID = 15,

        /// <summary>Motorola-QoS-Profile (Type 16). String. QoS profile name.</summary>
        QOS_PROFILE = 16
    }

    /// <summary>
    /// Motorola-Priv-Level attribute values (Type 3).
    /// </summary>
    public enum MOTOROLA_PRIV_LEVEL
    {
        /// <summary>Monitor (read-only) access.</summary>
        MONITOR = 0,

        /// <summary>Helpdesk level access.</summary>
        HELPDESK = 1,

        /// <summary>Network operator access.</summary>
        NETWORK_OPERATOR = 5,

        /// <summary>System administrator access.</summary>
        SYSTEM_ADMIN = 10,

        /// <summary>Super-user (full) access.</summary>
        SUPER_USER = 15
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Motorola /
    /// Zebra Technologies (IANA PEN 161) Vendor-Specific Attributes (VSAs), as defined
    /// in the FreeRADIUS <c>dictionary.motorola</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Motorola's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 161</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Motorola Solutions / Zebra Technologies WiNG
    /// wireless LAN controllers and access points (RFS, AP series) for RADIUS-based
    /// WLAN group assignment, user role mapping, CLI privilege level assignment,
    /// VLAN assignment (by name and ID), firewall policy and ACL enforcement,
    /// upstream/downstream rate limiting, session timeout configuration, captive
    /// portal redirection (profile and URL), AP location and name identification,
    /// wireless SSID identification, and QoS profile selection.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(MotorolaAttributes.Role("network-admin"));
    /// packet.SetAttribute(MotorolaAttributes.PrivLevel(MOTOROLA_PRIV_LEVEL.SUPER_USER));
    /// packet.SetAttribute(MotorolaAttributes.VlanId(100));
    /// packet.SetAttribute(MotorolaAttributes.RateLimitUp(10000));
    /// packet.SetAttribute(MotorolaAttributes.RateLimitDown(50000));
    /// packet.SetAttribute(MotorolaAttributes.FirewallPolicy("corporate-policy"));
    /// </code>
    /// </remarks>
    public static class MotorolaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Motorola.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 161;

        #region Integer Attributes

        /// <summary>
        /// Creates a Motorola-Priv-Level attribute (Type 3) with the specified level.
        /// </summary>
        /// <param name="value">The CLI privilege level. See <see cref="MOTOROLA_PRIV_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivLevel(MOTOROLA_PRIV_LEVEL value)
        {
            return CreateInteger(MotorolaAttributeType.PRIV_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Motorola-Rate-Limit-Up attribute (Type 7) with the specified rate.
        /// </summary>
        /// <param name="value">The upstream rate limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RateLimitUp(int value)
        {
            return CreateInteger(MotorolaAttributeType.RATE_LIMIT_UP, value);
        }

        /// <summary>
        /// Creates a Motorola-Rate-Limit-Down attribute (Type 8) with the specified rate.
        /// </summary>
        /// <param name="value">The downstream rate limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RateLimitDown(int value)
        {
            return CreateInteger(MotorolaAttributeType.RATE_LIMIT_DOWN, value);
        }

        /// <summary>
        /// Creates a Motorola-VLAN-Id attribute (Type 9) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier to assign.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(MotorolaAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Motorola-Session-Timeout attribute (Type 10) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(MotorolaAttributeType.SESSION_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Motorola-WLANGroup-Name attribute (Type 1) with the specified group name.
        /// </summary>
        /// <param name="value">The WLAN group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WlanGroupName(string value)
        {
            return CreateString(MotorolaAttributeType.WLANGROUP_NAME, value);
        }

        /// <summary>
        /// Creates a Motorola-VLAN-Name attribute (Type 2) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(MotorolaAttributeType.VLAN_NAME, value);
        }

        /// <summary>
        /// Creates a Motorola-Role attribute (Type 4) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Role(string value)
        {
            return CreateString(MotorolaAttributeType.ROLE, value);
        }

        /// <summary>
        /// Creates a Motorola-Firewall-Policy attribute (Type 5) with the specified policy name.
        /// </summary>
        /// <param name="value">The firewall policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FirewallPolicy(string value)
        {
            return CreateString(MotorolaAttributeType.FIREWALL_POLICY, value);
        }

        /// <summary>
        /// Creates a Motorola-ACL-Name attribute (Type 6) with the specified ACL name.
        /// </summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value)
        {
            return CreateString(MotorolaAttributeType.ACL_NAME, value);
        }

        /// <summary>
        /// Creates a Motorola-Captive-Portal attribute (Type 11) with the specified portal profile.
        /// </summary>
        /// <param name="value">The captive portal profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CaptivePortal(string value)
        {
            return CreateString(MotorolaAttributeType.CAPTIVE_PORTAL, value);
        }

        /// <summary>
        /// Creates a Motorola-Redirect-URL attribute (Type 12) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(MotorolaAttributeType.REDIRECT_URL, value);
        }

        /// <summary>
        /// Creates a Motorola-AP-Location attribute (Type 13) with the specified location.
        /// </summary>
        /// <param name="value">The access point location. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApLocation(string value)
        {
            return CreateString(MotorolaAttributeType.AP_LOCATION, value);
        }

        /// <summary>
        /// Creates a Motorola-AP-Name attribute (Type 14) with the specified access point name.
        /// </summary>
        /// <param name="value">The access point name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApName(string value)
        {
            return CreateString(MotorolaAttributeType.AP_NAME, value);
        }

        /// <summary>
        /// Creates a Motorola-SSID attribute (Type 15) with the specified wireless SSID.
        /// </summary>
        /// <param name="value">The wireless SSID name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ssid(string value)
        {
            return CreateString(MotorolaAttributeType.SSID, value);
        }

        /// <summary>
        /// Creates a Motorola-QoS-Profile attribute (Type 16) with the specified profile name.
        /// </summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value)
        {
            return CreateString(MotorolaAttributeType.QOS_PROFILE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Motorola attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(MotorolaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Motorola attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(MotorolaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}