using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Symbol Technologies (IANA PEN 388) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.symbol</c>.
    /// </summary>
    /// <remarks>
    /// Symbol Technologies (acquired by Motorola Solutions in 2007, later Zebra
    /// Technologies in 2014) produced enterprise wireless LAN infrastructure
    /// including access points, wireless switches/controllers (WS series), and
    /// mobile computing devices.
    /// </remarks>
    public enum SymbolAttributeType : byte
    {
        /// <summary>Symbol-Admin-Role (Type 1). Integer. Administrative role.</summary>
        ADMIN_ROLE = 1,

        /// <summary>Symbol-Current-ESSID (Type 2). String. Current ESSID (SSID).</summary>
        CURRENT_ESSID = 2,

        /// <summary>Symbol-Allowed-ESSID (Type 3). String. Allowed ESSID (SSID).</summary>
        ALLOWED_ESSID = 3,

        /// <summary>Symbol-WLAN-Index (Type 4). Integer. WLAN index.</summary>
        WLAN_INDEX = 4,

        /// <summary>Symbol-QoS-Profile (Type 5). String. QoS profile name.</summary>
        QOS_PROFILE = 5,

        /// <summary>Symbol-Allowed-Radio (Type 6). String. Allowed radio interface.</summary>
        ALLOWED_RADIO = 6,

        /// <summary>Symbol-Expiry-Date-Time (Type 7). String. Session expiry date/time.</summary>
        EXPIRY_DATE_TIME = 7,

        /// <summary>Symbol-Start-Date-Time (Type 8). String. Session start date/time.</summary>
        START_DATE_TIME = 8,

        /// <summary>Symbol-Posture-Status (Type 9). String. Posture assessment status.</summary>
        POSTURE_STATUS = 9,

        /// <summary>Symbol-Downlink-Limit (Type 10). Integer. Downlink rate limit in Kbps.</summary>
        DOWNLINK_LIMIT = 10,

        /// <summary>Symbol-Uplink-Limit (Type 11). Integer. Uplink rate limit in Kbps.</summary>
        UPLINK_LIMIT = 11,

        /// <summary>Symbol-User-Group (Type 12). String. User group name.</summary>
        USER_GROUP = 12,

        /// <summary>Symbol-Login-Source (Type 100). Integer. Login source type.</summary>
        LOGIN_SOURCE = 100,

        /// <summary>Symbol-VLAN-Name (Type 101). String. VLAN name to assign.</summary>
        VLAN_NAME = 101,

        /// <summary>Symbol-Allowed-VLAN (Type 102). String. Allowed VLAN.</summary>
        ALLOWED_VLAN = 102,

        /// <summary>Symbol-ACL-Name (Type 103). String. ACL name to apply.</summary>
        ACL_NAME = 103,

        /// <summary>Symbol-Redirect-URL (Type 104). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 104,

        /// <summary>Symbol-AP-Name (Type 105). String. Access point name.</summary>
        AP_NAME = 105,

        /// <summary>Symbol-AP-IP-Address (Type 106). IP address. Access point IP address.</summary>
        AP_IP_ADDRESS = 106
    }

    /// <summary>
    /// Symbol-Admin-Role attribute values (Type 1).
    /// </summary>
    public enum SYMBOL_ADMIN_ROLE
    {
        /// <summary>Monitor (read-only) access.</summary>
        MONITOR = 0,

        /// <summary>Help desk (limited) access.</summary>
        HELPDESK = 1,

        /// <summary>Network administrator access.</summary>
        NETWORK_ADMIN = 2,

        /// <summary>Super user (full) access.</summary>
        SUPER_USER = 3
    }

    /// <summary>
    /// Symbol-Login-Source attribute values (Type 100).
    /// </summary>
    public enum SYMBOL_LOGIN_SOURCE
    {
        /// <summary>Console login.</summary>
        CONSOLE = 0,

        /// <summary>Telnet login.</summary>
        TELNET = 1,

        /// <summary>SSH login.</summary>
        SSH = 2,

        /// <summary>Web/HTTP login.</summary>
        WEB = 3,

        /// <summary>SNMP login.</summary>
        SNMP = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Symbol Technologies
    /// (IANA PEN 388) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.symbol</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Symbol's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 388</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Symbol Technologies (Motorola Solutions / Zebra
    /// Technologies) enterprise WLAN controllers and access points for RADIUS-based
    /// administrative role assignment, ESSID/radio access control, WLAN index
    /// selection, QoS profile assignment, session expiry/start date-time
    /// configuration, posture assessment, uplink/downlink rate limiting, user
    /// group mapping, login source tracking, VLAN assignment (by name and
    /// allowed list), ACL enforcement, captive portal URL redirection, and
    /// AP name/IP identification.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(SymbolAttributes.AdminRole(SYMBOL_ADMIN_ROLE.SUPER_USER));
    /// packet.SetAttribute(SymbolAttributes.AllowedEssid("Corporate-WiFi"));
    /// packet.SetAttribute(SymbolAttributes.DownlinkLimit(50000));
    /// packet.SetAttribute(SymbolAttributes.UplinkLimit(10000));
    /// packet.SetAttribute(SymbolAttributes.VlanName("employees"));
    /// packet.SetAttribute(SymbolAttributes.QosProfile("voice-priority"));
    /// </code>
    /// </remarks>
    public static class SymbolAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Symbol Technologies.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 388;

        #region Integer Attributes

        /// <summary>
        /// Creates a Symbol-Admin-Role attribute (Type 1) with the specified role.
        /// </summary>
        /// <param name="value">The administrative role. See <see cref="SYMBOL_ADMIN_ROLE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminRole(SYMBOL_ADMIN_ROLE value)
        {
            return CreateInteger(SymbolAttributeType.ADMIN_ROLE, (int)value);
        }

        /// <summary>
        /// Creates a Symbol-WLAN-Index attribute (Type 4) with the specified index.
        /// </summary>
        /// <param name="value">The WLAN index.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WlanIndex(int value)
        {
            return CreateInteger(SymbolAttributeType.WLAN_INDEX, value);
        }

        /// <summary>
        /// Creates a Symbol-Downlink-Limit attribute (Type 10) with the specified rate.
        /// </summary>
        /// <param name="value">The downlink rate limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DownlinkLimit(int value)
        {
            return CreateInteger(SymbolAttributeType.DOWNLINK_LIMIT, value);
        }

        /// <summary>
        /// Creates a Symbol-Uplink-Limit attribute (Type 11) with the specified rate.
        /// </summary>
        /// <param name="value">The uplink rate limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UplinkLimit(int value)
        {
            return CreateInteger(SymbolAttributeType.UPLINK_LIMIT, value);
        }

        /// <summary>
        /// Creates a Symbol-Login-Source attribute (Type 100) with the specified source.
        /// </summary>
        /// <param name="value">The login source type. See <see cref="SYMBOL_LOGIN_SOURCE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LoginSource(SYMBOL_LOGIN_SOURCE value)
        {
            return CreateInteger(SymbolAttributeType.LOGIN_SOURCE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>Creates a Symbol-Current-ESSID attribute (Type 2).</summary>
        /// <param name="value">The current ESSID. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CurrentEssid(string value) => CreateString(SymbolAttributeType.CURRENT_ESSID, value);

        /// <summary>Creates a Symbol-Allowed-ESSID attribute (Type 3).</summary>
        /// <param name="value">The allowed ESSID. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AllowedEssid(string value) => CreateString(SymbolAttributeType.ALLOWED_ESSID, value);

        /// <summary>Creates a Symbol-QoS-Profile attribute (Type 5).</summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value) => CreateString(SymbolAttributeType.QOS_PROFILE, value);

        /// <summary>Creates a Symbol-Allowed-Radio attribute (Type 6).</summary>
        /// <param name="value">The allowed radio interface. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AllowedRadio(string value) => CreateString(SymbolAttributeType.ALLOWED_RADIO, value);

        /// <summary>Creates a Symbol-Expiry-Date-Time attribute (Type 7).</summary>
        /// <param name="value">The session expiry date/time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ExpiryDateTime(string value) => CreateString(SymbolAttributeType.EXPIRY_DATE_TIME, value);

        /// <summary>Creates a Symbol-Start-Date-Time attribute (Type 8).</summary>
        /// <param name="value">The session start date/time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes StartDateTime(string value) => CreateString(SymbolAttributeType.START_DATE_TIME, value);

        /// <summary>Creates a Symbol-Posture-Status attribute (Type 9).</summary>
        /// <param name="value">The posture assessment status. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PostureStatus(string value) => CreateString(SymbolAttributeType.POSTURE_STATUS, value);

        /// <summary>Creates a Symbol-User-Group attribute (Type 12).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value) => CreateString(SymbolAttributeType.USER_GROUP, value);

        /// <summary>Creates a Symbol-VLAN-Name attribute (Type 101).</summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value) => CreateString(SymbolAttributeType.VLAN_NAME, value);

        /// <summary>Creates a Symbol-Allowed-VLAN attribute (Type 102).</summary>
        /// <param name="value">The allowed VLAN. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AllowedVlan(string value) => CreateString(SymbolAttributeType.ALLOWED_VLAN, value);

        /// <summary>Creates a Symbol-ACL-Name attribute (Type 103).</summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value) => CreateString(SymbolAttributeType.ACL_NAME, value);

        /// <summary>Creates a Symbol-Redirect-URL attribute (Type 104).</summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value) => CreateString(SymbolAttributeType.REDIRECT_URL, value);

        /// <summary>Creates a Symbol-AP-Name attribute (Type 105).</summary>
        /// <param name="value">The access point name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApName(string value) => CreateString(SymbolAttributeType.AP_NAME, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Symbol-AP-IP-Address attribute (Type 106).</summary>
        /// <param name="value">The access point IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ApIpAddress(IPAddress value)
        {
            return CreateIpv4(SymbolAttributeType.AP_IP_ADDRESS, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(SymbolAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(SymbolAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(SymbolAttributeType type, IPAddress value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException(
                    $"Expected an IPv4 (InterNetwork) address, got '{value.AddressFamily}'.",
                    nameof(value));

            byte[] addrBytes = new byte[4];
            value.TryWriteBytes(addrBytes, out _);

            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, addrBytes);
        }

        #endregion
    }
}