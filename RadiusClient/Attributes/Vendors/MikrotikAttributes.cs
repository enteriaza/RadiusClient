using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a MikroTik (IANA PEN 14988) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.mikrotik</c>.
    /// </summary>
    /// <remarks>
    /// MikroTik is a Latvian manufacturer of networking equipment running RouterOS,
    /// including routers, switches, wireless access points, and the Winbox/Webfig
    /// management tools.
    /// </remarks>
    public enum MikrotikAttributeType : byte
    {
        /// <summary>Mikrotik-Recv-Limit (Type 1). Integer. Receive byte limit.</summary>
        RECV_LIMIT = 1,

        /// <summary>Mikrotik-Xmit-Limit (Type 2). Integer. Transmit byte limit.</summary>
        XMIT_LIMIT = 2,

        /// <summary>Mikrotik-Group (Type 3). String. RouterOS user group name.</summary>
        GROUP = 3,

        /// <summary>Mikrotik-Wireless-Forward (Type 4). Integer. Wireless client forwarding flag.</summary>
        WIRELESS_FORWARD = 4,

        /// <summary>Mikrotik-Wireless-Skip-Dot1x (Type 5). Integer. Skip 802.1X authentication flag.</summary>
        WIRELESS_SKIP_DOT1X = 5,

        /// <summary>Mikrotik-Wireless-Enc-Algo (Type 6). Integer. Wireless encryption algorithm.</summary>
        WIRELESS_ENC_ALGO = 6,

        /// <summary>Mikrotik-Wireless-Enc-Key (Type 7). String. Wireless encryption key.</summary>
        WIRELESS_ENC_KEY = 7,

        /// <summary>Mikrotik-Rate-Limit (Type 8). String. Rate limit string (rx/tx format).</summary>
        RATE_LIMIT = 8,

        /// <summary>Mikrotik-Realm (Type 9). String. User realm.</summary>
        REALM = 9,

        /// <summary>Mikrotik-Host-IP (Type 10). IP address. Host IP address.</summary>
        HOST_IP = 10,

        /// <summary>Mikrotik-Mark-Id (Type 11). String. Firewall mark identifier.</summary>
        MARK_ID = 11,

        /// <summary>Mikrotik-Advertise-URL (Type 12). String. Hotspot advertise URL.</summary>
        ADVERTISE_URL = 12,

        /// <summary>Mikrotik-Advertise-Interval (Type 13). Integer. Hotspot advertise interval in seconds.</summary>
        ADVERTISE_INTERVAL = 13,

        /// <summary>Mikrotik-Recv-Limit-Gigawords (Type 14). Integer. Receive limit gigawords (high 32 bits).</summary>
        RECV_LIMIT_GIGAWORDS = 14,

        /// <summary>Mikrotik-Xmit-Limit-Gigawords (Type 15). Integer. Transmit limit gigawords (high 32 bits).</summary>
        XMIT_LIMIT_GIGAWORDS = 15,

        /// <summary>Mikrotik-Wireless-PSK (Type 16). String. Wireless Pre-Shared Key.</summary>
        WIRELESS_PSK = 16,

        /// <summary>Mikrotik-Total-Limit (Type 17). Integer. Total (rx+tx) byte limit.</summary>
        TOTAL_LIMIT = 17,

        /// <summary>Mikrotik-Total-Limit-Gigawords (Type 18). Integer. Total limit gigawords (high 32 bits).</summary>
        TOTAL_LIMIT_GIGAWORDS = 18,

        /// <summary>Mikrotik-Address-List (Type 19). String. Address list name.</summary>
        ADDRESS_LIST = 19,

        /// <summary>Mikrotik-Wireless-MPKey (Type 20). String. Wireless management protection key.</summary>
        WIRELESS_MPKEY = 20,

        /// <summary>Mikrotik-Wireless-Comment (Type 21). String. Wireless access list comment.</summary>
        WIRELESS_COMMENT = 21,

        /// <summary>Mikrotik-Delegated-IPv6-Pool (Type 22). String. Delegated IPv6 prefix pool name.</summary>
        DELEGATED_IPV6_POOL = 22,

        /// <summary>Mikrotik-DHCP-Option-Set (Type 23). String. DHCP option set name.</summary>
        DHCP_OPTION_SET = 23,

        /// <summary>Mikrotik-DHCP-Option-Param-STR1 (Type 24). String. DHCP option parameter string 1.</summary>
        DHCP_OPTION_PARAM_STR1 = 24,

        /// <summary>Mikrotik-DHCP-Option-ParamSTR2 (Type 25). String. DHCP option parameter string 2.</summary>
        DHCP_OPTION_PARAM_STR2 = 25,

        /// <summary>Mikrotik-Wireless-VLANID (Type 26). Integer. Wireless VLAN identifier.</summary>
        WIRELESS_VLANID = 26,

        /// <summary>Mikrotik-Wireless-VLANIDtype (Type 27). Integer. Wireless VLAN ID type.</summary>
        WIRELESS_VLANIDTYPE = 27,

        /// <summary>Mikrotik-Wireless-Minsignal (Type 28). String. Wireless minimum signal level.</summary>
        WIRELESS_MINSIGNAL = 28,

        /// <summary>Mikrotik-Wireless-Maxsignal (Type 29). String. Wireless maximum signal level.</summary>
        WIRELESS_MAXSIGNAL = 29,

        /// <summary>Mikrotik-Switching-Filter (Type 30). String. Switching filter rule.</summary>
        SWITCHING_FILTER = 30
    }

    /// <summary>
    /// Mikrotik-Wireless-Enc-Algo attribute values (Type 6).
    /// </summary>
    public enum MIKROTIK_WIRELESS_ENC_ALGO
    {
        /// <summary>No encryption.</summary>
        NO_ENCRYPTION = 0,

        /// <summary>40-bit WEP.</summary>
        WEP_40BIT = 1,

        /// <summary>104-bit WEP.</summary>
        WEP_104BIT = 2,

        /// <summary>AES-CCM encryption.</summary>
        AES_CCM = 3,

        /// <summary>TKIP encryption.</summary>
        TKIP = 4
    }

    /// <summary>
    /// Mikrotik-Wireless-VLANIDtype attribute values (Type 27).
    /// </summary>
    public enum MIKROTIK_WIRELESS_VLANIDTYPE
    {
        /// <summary>802.1Q VLAN tagging.</summary>
        DOT1Q = 0,

        /// <summary>802.1ad (QinQ) VLAN tagging.</summary>
        DOT1AD = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing MikroTik
    /// (IANA PEN 14988) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.mikrotik</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// MikroTik's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14988</c>.
    /// </para>
    /// <para>
    /// These attributes are used by MikroTik RouterOS routers, switches, and wireless
    /// access points for RADIUS-based user group assignment, rate limiting (per-user
    /// rx/tx/total bandwidth and byte limits with gigaword extensions), wireless
    /// configuration (encryption algorithm, PSK, VLAN ID/type, management protection
    /// key, forwarding, 802.1X skip, signal limits, comments), hotspot management
    /// (advertise URL and interval), firewall mark and address list assignment,
    /// DHCP option set configuration, delegated IPv6 prefix pool selection,
    /// switching filter rules, host IP, and realm assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(MikrotikAttributes.Group("full"));
    /// packet.SetAttribute(MikrotikAttributes.RateLimit("10M/20M"));
    /// packet.SetAttribute(MikrotikAttributes.AddressList("allowed-users"));
    /// packet.SetAttribute(MikrotikAttributes.WirelessPsk("MySecureKey123"));
    /// packet.SetAttribute(MikrotikAttributes.WirelessVlanId(100));
    /// packet.SetAttribute(MikrotikAttributes.HostIp(IPAddress.Parse("10.0.0.1")));
    /// </code>
    /// </remarks>
    public static class MikrotikAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for MikroTik.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14988;

        #region Integer Attributes

        /// <summary>Creates a Mikrotik-Recv-Limit attribute (Type 1).</summary>
        /// <param name="value">The receive byte limit.</param>
        public static VendorSpecificAttributes RecvLimit(int value) => CreateInteger(MikrotikAttributeType.RECV_LIMIT, value);

        /// <summary>Creates a Mikrotik-Xmit-Limit attribute (Type 2).</summary>
        /// <param name="value">The transmit byte limit.</param>
        public static VendorSpecificAttributes XmitLimit(int value) => CreateInteger(MikrotikAttributeType.XMIT_LIMIT, value);

        /// <summary>Creates a Mikrotik-Wireless-Forward attribute (Type 4).</summary>
        /// <param name="value">The wireless client forwarding flag (0 = disabled, 1 = enabled).</param>
        public static VendorSpecificAttributes WirelessForward(int value) => CreateInteger(MikrotikAttributeType.WIRELESS_FORWARD, value);

        /// <summary>Creates a Mikrotik-Wireless-Skip-Dot1x attribute (Type 5).</summary>
        /// <param name="value">The skip 802.1X authentication flag (0 = disabled, 1 = enabled).</param>
        public static VendorSpecificAttributes WirelessSkipDot1x(int value) => CreateInteger(MikrotikAttributeType.WIRELESS_SKIP_DOT1X, value);

        /// <summary>Creates a Mikrotik-Wireless-Enc-Algo attribute (Type 6).</summary>
        /// <param name="value">The wireless encryption algorithm. See <see cref="MIKROTIK_WIRELESS_ENC_ALGO"/>.</param>
        public static VendorSpecificAttributes WirelessEncAlgo(MIKROTIK_WIRELESS_ENC_ALGO value) => CreateInteger(MikrotikAttributeType.WIRELESS_ENC_ALGO, (int)value);

        /// <summary>Creates a Mikrotik-Advertise-Interval attribute (Type 13).</summary>
        /// <param name="value">The hotspot advertise interval in seconds.</param>
        public static VendorSpecificAttributes AdvertiseInterval(int value) => CreateInteger(MikrotikAttributeType.ADVERTISE_INTERVAL, value);

        /// <summary>Creates a Mikrotik-Recv-Limit-Gigawords attribute (Type 14).</summary>
        /// <param name="value">The receive limit gigawords (high 32 bits of 64-bit counter).</param>
        public static VendorSpecificAttributes RecvLimitGigawords(int value) => CreateInteger(MikrotikAttributeType.RECV_LIMIT_GIGAWORDS, value);

        /// <summary>Creates a Mikrotik-Xmit-Limit-Gigawords attribute (Type 15).</summary>
        /// <param name="value">The transmit limit gigawords (high 32 bits of 64-bit counter).</param>
        public static VendorSpecificAttributes XmitLimitGigawords(int value) => CreateInteger(MikrotikAttributeType.XMIT_LIMIT_GIGAWORDS, value);

        /// <summary>Creates a Mikrotik-Total-Limit attribute (Type 17).</summary>
        /// <param name="value">The total (rx+tx) byte limit.</param>
        public static VendorSpecificAttributes TotalLimit(int value) => CreateInteger(MikrotikAttributeType.TOTAL_LIMIT, value);

        /// <summary>Creates a Mikrotik-Total-Limit-Gigawords attribute (Type 18).</summary>
        /// <param name="value">The total limit gigawords (high 32 bits of 64-bit counter).</param>
        public static VendorSpecificAttributes TotalLimitGigawords(int value) => CreateInteger(MikrotikAttributeType.TOTAL_LIMIT_GIGAWORDS, value);

        /// <summary>Creates a Mikrotik-Wireless-VLANID attribute (Type 26).</summary>
        /// <param name="value">The wireless VLAN identifier.</param>
        public static VendorSpecificAttributes WirelessVlanId(int value) => CreateInteger(MikrotikAttributeType.WIRELESS_VLANID, value);

        /// <summary>Creates a Mikrotik-Wireless-VLANIDtype attribute (Type 27).</summary>
        /// <param name="value">The wireless VLAN ID type. See <see cref="MIKROTIK_WIRELESS_VLANIDTYPE"/>.</param>
        public static VendorSpecificAttributes WirelessVlanIdType(MIKROTIK_WIRELESS_VLANIDTYPE value) => CreateInteger(MikrotikAttributeType.WIRELESS_VLANIDTYPE, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Mikrotik-Group attribute (Type 3).</summary>
        /// <param name="value">The RouterOS user group name (e.g. "full", "read", "write"). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Group(string value) => CreateString(MikrotikAttributeType.GROUP, value);

        /// <summary>Creates a Mikrotik-Wireless-Enc-Key attribute (Type 7).</summary>
        /// <param name="value">The wireless encryption key. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WirelessEncKey(string value) => CreateString(MikrotikAttributeType.WIRELESS_ENC_KEY, value);

        /// <summary>Creates a Mikrotik-Rate-Limit attribute (Type 8).</summary>
        /// <param name="value">The rate limit string in RouterOS rx/tx format (e.g. "10M/20M", "1M/2M 2M/4M 512k/1M 10/20 8 64k/128k"). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RateLimit(string value) => CreateString(MikrotikAttributeType.RATE_LIMIT, value);

        /// <summary>Creates a Mikrotik-Realm attribute (Type 9).</summary>
        /// <param name="value">The user realm. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Realm(string value) => CreateString(MikrotikAttributeType.REALM, value);

        /// <summary>Creates a Mikrotik-Mark-Id attribute (Type 11).</summary>
        /// <param name="value">The firewall mark identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MarkId(string value) => CreateString(MikrotikAttributeType.MARK_ID, value);

        /// <summary>Creates a Mikrotik-Advertise-URL attribute (Type 12).</summary>
        /// <param name="value">The hotspot advertise URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdvertiseUrl(string value) => CreateString(MikrotikAttributeType.ADVERTISE_URL, value);

        /// <summary>Creates a Mikrotik-Wireless-PSK attribute (Type 16).</summary>
        /// <param name="value">The wireless Pre-Shared Key. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WirelessPsk(string value) => CreateString(MikrotikAttributeType.WIRELESS_PSK, value);

        /// <summary>Creates a Mikrotik-Address-List attribute (Type 19).</summary>
        /// <param name="value">The address list name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AddressList(string value) => CreateString(MikrotikAttributeType.ADDRESS_LIST, value);

        /// <summary>Creates a Mikrotik-Wireless-MPKey attribute (Type 20).</summary>
        /// <param name="value">The wireless management protection key. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WirelessMpKey(string value) => CreateString(MikrotikAttributeType.WIRELESS_MPKEY, value);

        /// <summary>Creates a Mikrotik-Wireless-Comment attribute (Type 21).</summary>
        /// <param name="value">The wireless access list comment. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WirelessComment(string value) => CreateString(MikrotikAttributeType.WIRELESS_COMMENT, value);

        /// <summary>Creates a Mikrotik-Delegated-IPv6-Pool attribute (Type 22).</summary>
        /// <param name="value">The delegated IPv6 prefix pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DelegatedIpv6Pool(string value) => CreateString(MikrotikAttributeType.DELEGATED_IPV6_POOL, value);

        /// <summary>Creates a Mikrotik-DHCP-Option-Set attribute (Type 23).</summary>
        /// <param name="value">The DHCP option set name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpOptionSet(string value) => CreateString(MikrotikAttributeType.DHCP_OPTION_SET, value);

        /// <summary>Creates a Mikrotik-DHCP-Option-Param-STR1 attribute (Type 24).</summary>
        /// <param name="value">The DHCP option parameter string 1. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpOptionParamStr1(string value) => CreateString(MikrotikAttributeType.DHCP_OPTION_PARAM_STR1, value);

        /// <summary>Creates a Mikrotik-DHCP-Option-ParamSTR2 attribute (Type 25).</summary>
        /// <param name="value">The DHCP option parameter string 2. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpOptionParamStr2(string value) => CreateString(MikrotikAttributeType.DHCP_OPTION_PARAM_STR2, value);

        /// <summary>Creates a Mikrotik-Wireless-Minsignal attribute (Type 28).</summary>
        /// <param name="value">The wireless minimum signal level. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WirelessMinsignal(string value) => CreateString(MikrotikAttributeType.WIRELESS_MINSIGNAL, value);

        /// <summary>Creates a Mikrotik-Wireless-Maxsignal attribute (Type 29).</summary>
        /// <param name="value">The wireless maximum signal level. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WirelessMaxsignal(string value) => CreateString(MikrotikAttributeType.WIRELESS_MAXSIGNAL, value);

        /// <summary>Creates a Mikrotik-Switching-Filter attribute (Type 30).</summary>
        /// <param name="value">The switching filter rule. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SwitchingFilter(string value) => CreateString(MikrotikAttributeType.SWITCHING_FILTER, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates a Mikrotik-Host-IP attribute (Type 10) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The host IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes HostIp(IPAddress value)
        {
            return CreateIpv4(MikrotikAttributeType.HOST_IP, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(MikrotikAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(MikrotikAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(MikrotikAttributeType type, IPAddress value)
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