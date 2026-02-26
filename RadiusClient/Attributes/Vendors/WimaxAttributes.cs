using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a WiMAX Forum (IANA PEN 24757) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.wimax</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The WiMAX Forum defines RADIUS attributes for IEEE 802.16 (WiMAX) broadband
    /// wireless access networks, used by WiMAX base stations (BS), access service
    /// networks (ASN), connectivity service networks (CSN), and AAA servers for
    /// subscriber authentication, session management, QoS provisioning, handoff
    /// control, and mobility management.
    /// </para>
    /// <para>
    /// WiMAX uses a non-standard VSA sub-attribute encoding (<c>format=1,1,c</c>):
    /// 1-byte Vendor-Type, 1-byte Vendor-Length, plus a 1-byte continuation flag
    /// (RFC 6929 §2.4). The high bit (<c>0x80</c>) of the continuation byte indicates
    /// whether additional fragments follow, enabling attributes that exceed the
    /// standard 253-byte RADIUS attribute value limit.
    /// </para>
    /// <para>
    /// Some WiMAX attributes contain nested sub-TLV structures (compound attributes).
    /// These are exposed as raw byte factory methods; callers are responsible for
    /// encoding the inner TLV payload.
    /// </para>
    /// </remarks>
    public enum WimaxAttributeType : byte
    {
        /// <summary>WiMAX-Capability (Type 1). TLV. Device capability information (compound).</summary>
        CAPABILITY = 1,

        /// <summary>WiMAX-Release (Type 2). String. WiMAX release version.</summary>
        RELEASE = 2,

        /// <summary>WiMAX-Accounting-Capabilities (Type 3). Integer. Accounting capabilities bitmask.</summary>
        ACCOUNTING_CAPABILITIES = 3,

        /// <summary>WiMAX-Hotlining-Capabilities (Type 4). Integer. Hotlining capabilities bitmask.</summary>
        HOTLINING_CAPABILITIES = 4,

        /// <summary>WiMAX-Idle-Mode-Notification-Cap (Type 5). Integer. Idle mode notification capability.</summary>
        IDLE_MODE_NOTIFICATION_CAP = 5,

        /// <summary>WiMAX-Device-Authentication-Indicator (Type 6). Integer. Device authentication indicator.</summary>
        DEVICE_AUTHENTICATION_INDICATOR = 6,

        /// <summary>WiMAX-GMT-Timezone-Offset (Type 7). Integer. GMT timezone offset in seconds.</summary>
        GMT_TIMEZONE_OFFSET = 7,

        /// <summary>WiMAX-AAA-Session-Id (Type 8). String (octets). AAA session identifier.</summary>
        AAA_SESSION_ID = 8,

        /// <summary>WiMAX-MSK-Lifetime (Type 9). Integer. Master Session Key lifetime in seconds.</summary>
        MSK_LIFETIME = 9,

        /// <summary>WiMAX-MSK (Type 10). String (octets). Master Session Key.</summary>
        MSK = 10,

        /// <summary>WiMAX-Compact-Mode (Type 11). Integer. Compact mode indicator.</summary>
        COMPACT_MODE = 11,

        /// <summary>WiMAX-DHCP-RK (Type 12). String (octets). DHCP Root Key.</summary>
        DHCP_RK = 12,

        /// <summary>WiMAX-DHCP-RK-Key-Id (Type 13). Integer. DHCP Root Key identifier.</summary>
        DHCP_RK_KEY_ID = 13,

        /// <summary>WiMAX-DHCP-RK-Lifetime (Type 14). Integer. DHCP Root Key lifetime in seconds.</summary>
        DHCP_RK_LIFETIME = 14,

        /// <summary>WiMAX-DHCP-Msg-Server-IP (Type 15). IP address. DHCP message server IP.</summary>
        DHCP_MSG_SERVER_IP = 15,

        /// <summary>WiMAX-Idle-Mode-Transition (Type 16). Integer. Idle mode transition flag.</summary>
        IDLE_MODE_TRANSITION = 16,

        /// <summary>WiMAX-NAP-Id (Type 17). String (octets). Network Access Provider identifier.</summary>
        NAP_ID = 17,

        /// <summary>WiMAX-NSP-Id (Type 18). String (octets). Network Service Provider identifier.</summary>
        NSP_ID = 18,

        /// <summary>WiMAX-BS-Id (Type 19). String (octets). Base Station identifier.</summary>
        BS_ID = 19,

        /// <summary>WiMAX-Location (Type 20). String (octets). Location information.</summary>
        LOCATION = 20,

        /// <summary>WiMAX-QoS-Descriptor (Type 21). TLV. QoS descriptor (compound).</summary>
        QOS_DESCRIPTOR = 21,

        /// <summary>WiMAX-Uplink-QoS-Descriptor (Type 22). TLV. Uplink QoS descriptor (compound).</summary>
        UPLINK_QOS_DESCRIPTOR = 22,

        /// <summary>WiMAX-Downlink-QoS-Descriptor (Type 23). TLV. Downlink QoS descriptor (compound).</summary>
        DOWNLINK_QOS_DESCRIPTOR = 23,

        /// <summary>WiMAX-Uplink-Granted-QoS (Type 24). TLV. Uplink granted QoS (compound).</summary>
        UPLINK_GRANTED_QOS = 24,

        /// <summary>WiMAX-Control-Packets-In (Type 25). Integer. Control packets received.</summary>
        CONTROL_PACKETS_IN = 25,

        /// <summary>WiMAX-Control-Octets-In (Type 26). Integer. Control octets received.</summary>
        CONTROL_OCTETS_IN = 26,

        /// <summary>WiMAX-Control-Packets-Out (Type 27). Integer. Control packets sent.</summary>
        CONTROL_PACKETS_OUT = 27,

        /// <summary>WiMAX-Control-Octets-Out (Type 28). Integer. Control octets sent.</summary>
        CONTROL_OCTETS_OUT = 28,

        /// <summary>WiMAX-PPAC (Type 29). TLV. Pre-Paid Accounting Capability (compound).</summary>
        PPAC = 29,

        /// <summary>WiMAX-Session-Termination-Cause (Type 30). Integer. Session termination cause.</summary>
        SESSION_TERMINATION_CAUSE = 30,

        /// <summary>WiMAX-PPAQ (Type 31). TLV. Pre-Paid Accounting Quota (compound).</summary>
        PPAQ = 31,

        /// <summary>WiMAX-Prepaid-Tariff-Switching (Type 32). TLV. Pre-paid tariff switching (compound).</summary>
        PREPAID_TARIFF_SWITCHING = 32,

        /// <summary>WiMAX-Active-Time-Duration (Type 33). Integer. Active time duration in seconds.</summary>
        ACTIVE_TIME_DURATION = 33,

        /// <summary>WiMAX-DHCP-Server-Parameters (Type 34). TLV. DHCP server parameters (compound).</summary>
        DHCP_SERVER_PARAMETERS = 34,

        /// <summary>WiMAX-DHCPV4-Server-Address (Type 35). IP address. DHCPv4 server address.</summary>
        DHCPV4_SERVER_ADDRESS = 35,

        /// <summary>WiMAX-DHCPV6-Server-Address (Type 36). String (octets). DHCPv6 server address (IPv6, 16 bytes).</summary>
        DHCPV6_SERVER_ADDRESS = 36,

        /// <summary>WiMAX-MN-hHA-MIP4-Key (Type 37). String (octets). MN-HA MIP4 key.</summary>
        MN_HHA_MIP4_KEY = 37,

        /// <summary>WiMAX-MN-hHA-MIP4-SPI (Type 38). Integer. MN-HA MIP4 SPI.</summary>
        MN_HHA_MIP4_SPI = 38,

        /// <summary>WiMAX-MN-hHA-MIP6-Key (Type 39). String (octets). MN-HA MIP6 key.</summary>
        MN_HHA_MIP6_KEY = 39,

        /// <summary>WiMAX-MN-hHA-MIP6-SPI (Type 40). Integer. MN-HA MIP6 SPI.</summary>
        MN_HHA_MIP6_SPI = 40,

        /// <summary>WiMAX-FA-RK-Key (Type 41). String (octets). FA Root Key.</summary>
        FA_RK_KEY = 41,

        /// <summary>WiMAX-HA-RK-Key (Type 42). String (octets). HA Root Key.</summary>
        HA_RK_KEY = 42,

        /// <summary>WiMAX-HA-RK-SPI (Type 43). Integer. HA Root Key SPI.</summary>
        HA_RK_SPI = 43,

        /// <summary>WiMAX-HA-RK-Lifetime (Type 44). Integer. HA Root Key lifetime in seconds.</summary>
        HA_RK_LIFETIME = 44,

        /// <summary>WiMAX-RRQ-HA-IP (Type 45). IP address. Registration Request HA IP address.</summary>
        RRQ_HA_IP = 45,

        /// <summary>WiMAX-RRQ-MN-HA-Key (Type 46). String (octets). RRQ MN-HA key.</summary>
        RRQ_MN_HA_KEY = 46,

        /// <summary>WiMAX-RRQ-MN-HA-SPI (Type 47). Integer. RRQ MN-HA SPI.</summary>
        RRQ_MN_HA_SPI = 47,

        /// <summary>WiMAX-Session-Continue (Type 48). Integer. Session continue flag.</summary>
        SESSION_CONTINUE = 48,

        /// <summary>WiMAX-Beginning-Of-Session (Type 49). Integer. Beginning of session flag.</summary>
        BEGINNING_OF_SESSION = 49,

        /// <summary>WiMAX-IP-Technology (Type 50). Integer. IP technology type.</summary>
        IP_TECHNOLOGY = 50,

        /// <summary>WiMAX-Hotline-Indicator (Type 51). String. Hotline indicator.</summary>
        HOTLINE_INDICATOR = 51,

        /// <summary>WiMAX-HTTP-Redirection-Rule (Type 52). String. HTTP redirection rule.</summary>
        HTTP_REDIRECTION_RULE = 52,

        /// <summary>WiMAX-IP-Redirection-Rule (Type 53). String. IP redirection rule.</summary>
        IP_REDIRECTION_RULE = 53,

        /// <summary>WiMAX-Hotline-Profile-Id (Type 54). String. Hotline profile identifier.</summary>
        HOTLINE_PROFILE_ID = 54,

        /// <summary>WiMAX-Hotline-Session-Timer (Type 55). Integer. Hotline session timer in seconds.</summary>
        HOTLINE_SESSION_TIMER = 55,

        /// <summary>WiMAX-NSP-Change-Count (Type 56). Integer. NSP change count.</summary>
        NSP_CHANGE_COUNT = 56,

        /// <summary>WiMAX-Packet-Flow-Descriptor-V2 (Type 57). TLV. Packet flow descriptor v2 (compound).</summary>
        PACKET_FLOW_DESCRIPTOR_V2 = 57,

        /// <summary>WiMAX-Visited-Fraction-IPGW-Address (Type 58). IP address. Visited fraction IPGW address.</summary>
        VISITED_FRACTION_IPGW_ADDRESS = 58,

        /// <summary>WiMAX-MN-vHA-MIP4-Key (Type 59). String (octets). MN-vHA MIP4 key.</summary>
        MN_VHA_MIP4_KEY = 59,

        /// <summary>WiMAX-MN-vHA-MIP4-SPI (Type 60). Integer. MN-vHA MIP4 SPI.</summary>
        MN_VHA_MIP4_SPI = 60,

        /// <summary>WiMAX-MN-vHA-MIP6-Key (Type 61). String (octets). MN-vHA MIP6 key.</summary>
        MN_VHA_MIP6_KEY = 61,

        /// <summary>WiMAX-MN-vHA-MIP6-SPI (Type 62). Integer. MN-vHA MIP6 SPI.</summary>
        MN_VHA_MIP6_SPI = 62,

        /// <summary>WiMAX-Redirect-URL (Type 63). String. Redirect URL.</summary>
        REDIRECT_URL = 63,

        /// <summary>WiMAX-Accounting-Input-Packets (Type 64). Integer. Accounting input packets.</summary>
        ACCOUNTING_INPUT_PACKETS = 64,

        /// <summary>WiMAX-Accounting-Output-Packets (Type 65). Integer. Accounting output packets.</summary>
        ACCOUNTING_OUTPUT_PACKETS = 65,

        /// <summary>WiMAX-Accounting-Input-Octets (Type 66). Integer. Accounting input octets.</summary>
        ACCOUNTING_INPUT_OCTETS = 66,

        /// <summary>WiMAX-Accounting-Output-Octets (Type 67). Integer. Accounting output octets.</summary>
        ACCOUNTING_OUTPUT_OCTETS = 67,

        /// <summary>WiMAX-Uplink-Flow-Description (Type 68). String. Uplink flow description classifier.</summary>
        UPLINK_FLOW_DESCRIPTION = 68,

        /// <summary>WiMAX-Downlink-Flow-Description (Type 69). String. Downlink flow description classifier.</summary>
        DOWNLINK_FLOW_DESCRIPTION = 69,

        /// <summary>WiMAX-Granted-QoS-Parameters (Type 70). TLV. Granted QoS parameters (compound).</summary>
        GRANTED_QOS_PARAMETERS = 70,

        /// <summary>WiMAX-Downlink-Granted-QoS (Type 71). TLV. Downlink granted QoS (compound).</summary>
        DOWNLINK_GRANTED_QOS = 71,

        /// <summary>WiMAX-Uplink-Data-Packets-In (Type 72). Integer. Uplink data packets received.</summary>
        UPLINK_DATA_PACKETS_IN = 72,

        /// <summary>WiMAX-Uplink-Data-Octets-In (Type 73). Integer. Uplink data octets received.</summary>
        UPLINK_DATA_OCTETS_IN = 73,

        /// <summary>WiMAX-Downlink-Data-Packets-In (Type 74). Integer. Downlink data packets received.</summary>
        DOWNLINK_DATA_PACKETS_IN = 74,

        /// <summary>WiMAX-Downlink-Data-Octets-In (Type 75). Integer. Downlink data octets received.</summary>
        DOWNLINK_DATA_OCTETS_IN = 75,

        /// <summary>WiMAX-Uplink-Data-Packets-Out (Type 76). Integer. Uplink data packets sent.</summary>
        UPLINK_DATA_PACKETS_OUT = 76,

        /// <summary>WiMAX-Uplink-Data-Octets-Out (Type 77). Integer. Uplink data octets sent.</summary>
        UPLINK_DATA_OCTETS_OUT = 77,

        /// <summary>WiMAX-Downlink-Data-Packets-Out (Type 78). Integer. Downlink data packets sent.</summary>
        DOWNLINK_DATA_PACKETS_OUT = 78,

        /// <summary>WiMAX-Downlink-Data-Octets-Out (Type 79). Integer. Downlink data octets sent.</summary>
        DOWNLINK_DATA_OCTETS_OUT = 79,

        /// <summary>WiMAX-Uplink-Delay-Jitter-Time (Type 80). Integer. Uplink delay/jitter in milliseconds.</summary>
        UPLINK_DELAY_JITTER_TIME = 80,

        /// <summary>WiMAX-Downlink-Delay-Jitter-Time (Type 81). Integer. Downlink delay/jitter in milliseconds.</summary>
        DOWNLINK_DELAY_JITTER_TIME = 81,

        /// <summary>WiMAX-Uplink-Granted-Packets (Type 82). Integer. Uplink granted packets count.</summary>
        UPLINK_GRANTED_PACKETS = 82,

        /// <summary>WiMAX-Downlink-Granted-Packets (Type 83). Integer. Downlink granted packets count.</summary>
        DOWNLINK_GRANTED_PACKETS = 83
    }

    /// <summary>
    /// WiMAX-Session-Termination-Cause attribute values (Type 30).
    /// </summary>
    /// <remarks>
    /// Defined in NWG_R1.0-SP-5_v1.0.0 §4.8.10.
    /// </remarks>
    public enum WIMAX_SESSION_TERMINATION_CAUSE
    {
        /// <summary>Normal session termination (subscriber request).</summary>
        NORMAL = 0,

        /// <summary>Network initiated disconnect.</summary>
        NETWORK_DISCONNECT = 1,

        /// <summary>Session timeout expired.</summary>
        SESSION_TIMEOUT = 2,

        /// <summary>AAA server request.</summary>
        AAA_REQUEST = 3,

        /// <summary>Network error.</summary>
        NETWORK_ERROR = 4,

        /// <summary>Service not authorized.</summary>
        NOT_AUTHORIZED = 5,

        /// <summary>MS request (mobile station initiated).</summary>
        MS_REQUEST = 6,

        /// <summary>AAA session timeout.</summary>
        AAA_SESSION_TIMEOUT = 7
    }

    /// <summary>
    /// WiMAX-IP-Technology attribute values (Type 50).
    /// </summary>
    public enum WIMAX_IP_TECHNOLOGY
    {
        /// <summary>Simple IPv4.</summary>
        SIMPLE_IPV4 = 0,

        /// <summary>Simple IPv6.</summary>
        SIMPLE_IPV6 = 1,

        /// <summary>PMIP4 (Proxy MIPv4).</summary>
        PMIP4 = 2,

        /// <summary>CMIP4 (Client MIPv4).</summary>
        CMIP4 = 3,

        /// <summary>CMIP6 (Client MIPv6).</summary>
        CMIP6 = 4,

        /// <summary>Simple Ethernet.</summary>
        SIMPLE_ETHERNET = 5
    }

    /// <summary>
    /// WiMAX-Device-Authentication-Indicator attribute values (Type 6).
    /// </summary>
    public enum WIMAX_DEVICE_AUTHENTICATION_INDICATOR
    {
        /// <summary>Device not authenticated.</summary>
        NOT_AUTHENTICATED = 0,

        /// <summary>Device authenticated.</summary>
        AUTHENTICATED = 1
    }

    /// <summary>
    /// WiMAX-Session-Continue attribute values (Type 48).
    /// </summary>
    public enum WIMAX_SESSION_CONTINUE
    {
        /// <summary>Session not continued.</summary>
        NOT_CONTINUED = 0,

        /// <summary>Session continued after re-authentication or handoff.</summary>
        CONTINUED = 1
    }

    /// <summary>
    /// WiMAX-Beginning-Of-Session attribute values (Type 49).
    /// </summary>
    public enum WIMAX_BEGINNING_OF_SESSION
    {
        /// <summary>Not the beginning of a session.</summary>
        NO = 0,

        /// <summary>Beginning of a new session.</summary>
        YES = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing WiMAX Forum
    /// (IANA PEN 24757) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.wimax</c> and the WiMAX Forum NWG specifications.
    /// </summary>
    /// <remarks>
    /// <para>
    /// WiMAX uses a non-standard VSA sub-attribute encoding (<c>format=1,1,c</c>): 1-byte
    /// Vendor-Type, 1-byte Vendor-Length, plus a 1-byte continuation flag byte (RFC 6929
    /// §2.4). All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 24757</c> and
    /// <see cref="VendorSpecificFormat.Type1Len1Continuation"/>.
    /// </para>
    /// <para>
    /// Some WiMAX attributes (marked as TLV/compound) contain nested sub-TLV structures.
    /// These are exposed as raw byte factory methods via <see cref="CreateCompound"/>;
    /// callers are responsible for assembling the inner TLV payload.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(WimaxAttributes.MskLifetime(86400));
    /// packet.SetAttribute(WimaxAttributes.IpTechnology(WIMAX_IP_TECHNOLOGY.SIMPLE_IPV4));
    /// packet.SetAttribute(WimaxAttributes.SessionContinue(WIMAX_SESSION_CONTINUE.CONTINUED));
    /// packet.SetAttribute(WimaxAttributes.DhcpV4ServerAddress(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(WimaxAttributes.HotlineProfileId("restricted-profile"));
    /// </code>
    /// </remarks>
    public static class WimaxAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for the WiMAX Forum.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 24757;

        #region Integer Attributes

        /// <summary>Creates a WiMAX-Accounting-Capabilities attribute (Type 3).</summary>
        /// <param name="value">The accounting capabilities bitmask.</param>
        public static VendorSpecificAttributes AccountingCapabilities(int value) => CreateInteger(WimaxAttributeType.ACCOUNTING_CAPABILITIES, value);

        /// <summary>Creates a WiMAX-Hotlining-Capabilities attribute (Type 4).</summary>
        /// <param name="value">The hotlining capabilities bitmask.</param>
        public static VendorSpecificAttributes HotliningCapabilities(int value) => CreateInteger(WimaxAttributeType.HOTLINING_CAPABILITIES, value);

        /// <summary>Creates a WiMAX-Idle-Mode-Notification-Cap attribute (Type 5).</summary>
        /// <param name="value">The idle mode notification capability.</param>
        public static VendorSpecificAttributes IdleModeNotificationCap(int value) => CreateInteger(WimaxAttributeType.IDLE_MODE_NOTIFICATION_CAP, value);

        /// <summary>Creates a WiMAX-Device-Authentication-Indicator attribute (Type 6).</summary>
        /// <param name="value">The device authentication indicator. See <see cref="WIMAX_DEVICE_AUTHENTICATION_INDICATOR"/>.</param>
        public static VendorSpecificAttributes DeviceAuthenticationIndicator(WIMAX_DEVICE_AUTHENTICATION_INDICATOR value) => CreateInteger(WimaxAttributeType.DEVICE_AUTHENTICATION_INDICATOR, (int)value);

        /// <summary>Creates a WiMAX-GMT-Timezone-Offset attribute (Type 7).</summary>
        /// <param name="value">The GMT timezone offset in seconds.</param>
        public static VendorSpecificAttributes GmtTimezoneOffset(int value) => CreateInteger(WimaxAttributeType.GMT_TIMEZONE_OFFSET, value);

        /// <summary>Creates a WiMAX-MSK-Lifetime attribute (Type 9).</summary>
        /// <param name="value">The Master Session Key lifetime in seconds.</param>
        public static VendorSpecificAttributes MskLifetime(int value) => CreateInteger(WimaxAttributeType.MSK_LIFETIME, value);

        /// <summary>Creates a WiMAX-Compact-Mode attribute (Type 11).</summary>
        /// <param name="value">The compact mode indicator.</param>
        public static VendorSpecificAttributes CompactMode(int value) => CreateInteger(WimaxAttributeType.COMPACT_MODE, value);

        /// <summary>Creates a WiMAX-DHCP-RK-Key-Id attribute (Type 13).</summary>
        /// <param name="value">The DHCP Root Key identifier.</param>
        public static VendorSpecificAttributes DhcpRkKeyId(int value) => CreateInteger(WimaxAttributeType.DHCP_RK_KEY_ID, value);

        /// <summary>Creates a WiMAX-DHCP-RK-Lifetime attribute (Type 14).</summary>
        /// <param name="value">The DHCP Root Key lifetime in seconds.</param>
        public static VendorSpecificAttributes DhcpRkLifetime(int value) => CreateInteger(WimaxAttributeType.DHCP_RK_LIFETIME, value);

        /// <summary>Creates a WiMAX-Idle-Mode-Transition attribute (Type 16).</summary>
        /// <param name="value">The idle mode transition flag.</param>
        public static VendorSpecificAttributes IdleModeTransition(int value) => CreateInteger(WimaxAttributeType.IDLE_MODE_TRANSITION, value);

        /// <summary>Creates a WiMAX-Control-Packets-In attribute (Type 25).</summary>
        /// <param name="value">The control packets received count.</param>
        public static VendorSpecificAttributes ControlPacketsIn(int value) => CreateInteger(WimaxAttributeType.CONTROL_PACKETS_IN, value);

        /// <summary>Creates a WiMAX-Control-Octets-In attribute (Type 26).</summary>
        /// <param name="value">The control octets received count.</param>
        public static VendorSpecificAttributes ControlOctetsIn(int value) => CreateInteger(WimaxAttributeType.CONTROL_OCTETS_IN, value);

        /// <summary>Creates a WiMAX-Control-Packets-Out attribute (Type 27).</summary>
        /// <param name="value">The control packets sent count.</param>
        public static VendorSpecificAttributes ControlPacketsOut(int value) => CreateInteger(WimaxAttributeType.CONTROL_PACKETS_OUT, value);

        /// <summary>Creates a WiMAX-Control-Octets-Out attribute (Type 28).</summary>
        /// <param name="value">The control octets sent count.</param>
        public static VendorSpecificAttributes ControlOctetsOut(int value) => CreateInteger(WimaxAttributeType.CONTROL_OCTETS_OUT, value);

        /// <summary>Creates a WiMAX-Session-Termination-Cause attribute (Type 30).</summary>
        /// <param name="value">The session termination cause. See <see cref="WIMAX_SESSION_TERMINATION_CAUSE"/>.</param>
        public static VendorSpecificAttributes SessionTerminationCause(WIMAX_SESSION_TERMINATION_CAUSE value) => CreateInteger(WimaxAttributeType.SESSION_TERMINATION_CAUSE, (int)value);

        /// <summary>Creates a WiMAX-Active-Time-Duration attribute (Type 33).</summary>
        /// <param name="value">The active time duration in seconds.</param>
        public static VendorSpecificAttributes ActiveTimeDuration(int value) => CreateInteger(WimaxAttributeType.ACTIVE_TIME_DURATION, value);

        /// <summary>Creates a WiMAX-MN-hHA-MIP4-SPI attribute (Type 38).</summary>
        /// <param name="value">The MN-HA MIP4 Security Parameter Index.</param>
        public static VendorSpecificAttributes MnHhaMip4Spi(int value) => CreateInteger(WimaxAttributeType.MN_HHA_MIP4_SPI, value);

        /// <summary>Creates a WiMAX-MN-hHA-MIP6-SPI attribute (Type 40).</summary>
        /// <param name="value">The MN-HA MIP6 Security Parameter Index.</param>
        public static VendorSpecificAttributes MnHhaMip6Spi(int value) => CreateInteger(WimaxAttributeType.MN_HHA_MIP6_SPI, value);

        /// <summary>Creates a WiMAX-HA-RK-SPI attribute (Type 43).</summary>
        /// <param name="value">The HA Root Key Security Parameter Index.</param>
        public static VendorSpecificAttributes HaRkSpi(int value) => CreateInteger(WimaxAttributeType.HA_RK_SPI, value);

        /// <summary>Creates a WiMAX-HA-RK-Lifetime attribute (Type 44).</summary>
        /// <param name="value">The HA Root Key lifetime in seconds.</param>
        public static VendorSpecificAttributes HaRkLifetime(int value) => CreateInteger(WimaxAttributeType.HA_RK_LIFETIME, value);

        /// <summary>Creates a WiMAX-RRQ-MN-HA-SPI attribute (Type 47).</summary>
        /// <param name="value">The RRQ MN-HA Security Parameter Index.</param>
        public static VendorSpecificAttributes RrqMnHaSpi(int value) => CreateInteger(WimaxAttributeType.RRQ_MN_HA_SPI, value);

        /// <summary>Creates a WiMAX-Session-Continue attribute (Type 48).</summary>
        /// <param name="value">The session continue flag. See <see cref="WIMAX_SESSION_CONTINUE"/>.</param>
        public static VendorSpecificAttributes SessionContinue(WIMAX_SESSION_CONTINUE value) => CreateInteger(WimaxAttributeType.SESSION_CONTINUE, (int)value);

        /// <summary>Creates a WiMAX-Beginning-Of-Session attribute (Type 49).</summary>
        /// <param name="value">The beginning of session flag. See <see cref="WIMAX_BEGINNING_OF_SESSION"/>.</param>
        public static VendorSpecificAttributes BeginningOfSession(WIMAX_BEGINNING_OF_SESSION value) => CreateInteger(WimaxAttributeType.BEGINNING_OF_SESSION, (int)value);

        /// <summary>Creates a WiMAX-IP-Technology attribute (Type 50).</summary>
        /// <param name="value">The IP technology type. See <see cref="WIMAX_IP_TECHNOLOGY"/>.</param>
        public static VendorSpecificAttributes IpTechnology(WIMAX_IP_TECHNOLOGY value) => CreateInteger(WimaxAttributeType.IP_TECHNOLOGY, (int)value);

        /// <summary>Creates a WiMAX-Hotline-Session-Timer attribute (Type 55).</summary>
        /// <param name="value">The hotline session timer in seconds.</param>
        public static VendorSpecificAttributes HotlineSessionTimer(int value) => CreateInteger(WimaxAttributeType.HOTLINE_SESSION_TIMER, value);

        /// <summary>Creates a WiMAX-NSP-Change-Count attribute (Type 56).</summary>
        /// <param name="value">The NSP change count.</param>
        public static VendorSpecificAttributes NspChangeCount(int value) => CreateInteger(WimaxAttributeType.NSP_CHANGE_COUNT, value);

        /// <summary>Creates a WiMAX-MN-vHA-MIP4-SPI attribute (Type 60).</summary>
        /// <param name="value">The MN-vHA MIP4 Security Parameter Index.</param>
        public static VendorSpecificAttributes MnVhaMip4Spi(int value) => CreateInteger(WimaxAttributeType.MN_VHA_MIP4_SPI, value);

        /// <summary>Creates a WiMAX-MN-vHA-MIP6-SPI attribute (Type 62).</summary>
        /// <param name="value">The MN-vHA MIP6 Security Parameter Index.</param>
        public static VendorSpecificAttributes MnVhaMip6Spi(int value) => CreateInteger(WimaxAttributeType.MN_VHA_MIP6_SPI, value);

        /// <summary>Creates a WiMAX-Accounting-Input-Packets attribute (Type 64).</summary>
        /// <param name="value">The accounting input packets count.</param>
        public static VendorSpecificAttributes AccountingInputPackets(int value) => CreateInteger(WimaxAttributeType.ACCOUNTING_INPUT_PACKETS, value);

        /// <summary>Creates a WiMAX-Accounting-Output-Packets attribute (Type 65).</summary>
        /// <param name="value">The accounting output packets count.</param>
        public static VendorSpecificAttributes AccountingOutputPackets(int value) => CreateInteger(WimaxAttributeType.ACCOUNTING_OUTPUT_PACKETS, value);

        /// <summary>Creates a WiMAX-Accounting-Input-Octets attribute (Type 66).</summary>
        /// <param name="value">The accounting input octets count.</param>
        public static VendorSpecificAttributes AccountingInputOctets(int value) => CreateInteger(WimaxAttributeType.ACCOUNTING_INPUT_OCTETS, value);

        /// <summary>Creates a WiMAX-Accounting-Output-Octets attribute (Type 67).</summary>
        /// <param name="value">The accounting output octets count.</param>
        public static VendorSpecificAttributes AccountingOutputOctets(int value) => CreateInteger(WimaxAttributeType.ACCOUNTING_OUTPUT_OCTETS, value);

        /// <summary>Creates a WiMAX-Uplink-Data-Packets-In attribute (Type 72).</summary>
        /// <param name="value">The uplink data packets received count.</param>
        public static VendorSpecificAttributes UplinkDataPacketsIn(int value) => CreateInteger(WimaxAttributeType.UPLINK_DATA_PACKETS_IN, value);

        /// <summary>Creates a WiMAX-Uplink-Data-Octets-In attribute (Type 73).</summary>
        /// <param name="value">The uplink data octets received count.</param>
        public static VendorSpecificAttributes UplinkDataOctetsIn(int value) => CreateInteger(WimaxAttributeType.UPLINK_DATA_OCTETS_IN, value);

        /// <summary>Creates a WiMAX-Downlink-Data-Packets-In attribute (Type 74).</summary>
        /// <param name="value">The downlink data packets received count.</param>
        public static VendorSpecificAttributes DownlinkDataPacketsIn(int value) => CreateInteger(WimaxAttributeType.DOWNLINK_DATA_PACKETS_IN, value);

        /// <summary>Creates a WiMAX-Downlink-Data-Octets-In attribute (Type 75).</summary>
        /// <param name="value">The downlink data octets received count.</param>
        public static VendorSpecificAttributes DownlinkDataOctetsIn(int value) => CreateInteger(WimaxAttributeType.DOWNLINK_DATA_OCTETS_IN, value);

        /// <summary>Creates a WiMAX-Uplink-Data-Packets-Out attribute (Type 76).</summary>
        /// <param name="value">The uplink data packets sent count.</param>
        public static VendorSpecificAttributes UplinkDataPacketsOut(int value) => CreateInteger(WimaxAttributeType.UPLINK_DATA_PACKETS_OUT, value);

        /// <summary>Creates a WiMAX-Uplink-Data-Octets-Out attribute (Type 77).</summary>
        /// <param name="value">The uplink data octets sent count.</param>
        public static VendorSpecificAttributes UplinkDataOctetsOut(int value) => CreateInteger(WimaxAttributeType.UPLINK_DATA_OCTETS_OUT, value);

        /// <summary>Creates a WiMAX-Downlink-Data-Packets-Out attribute (Type 78).</summary>
        /// <param name="value">The downlink data packets sent count.</param>
        public static VendorSpecificAttributes DownlinkDataPacketsOut(int value) => CreateInteger(WimaxAttributeType.DOWNLINK_DATA_PACKETS_OUT, value);

        /// <summary>Creates a WiMAX-Downlink-Data-Octets-Out attribute (Type 79).</summary>
        /// <param name="value">The downlink data octets sent count.</param>
        public static VendorSpecificAttributes DownlinkDataOctetsOut(int value) => CreateInteger(WimaxAttributeType.DOWNLINK_DATA_OCTETS_OUT, value);

        /// <summary>Creates a WiMAX-Uplink-Delay-Jitter-Time attribute (Type 80).</summary>
        /// <param name="value">The uplink delay/jitter in milliseconds.</param>
        public static VendorSpecificAttributes UplinkDelayJitterTime(int value) => CreateInteger(WimaxAttributeType.UPLINK_DELAY_JITTER_TIME, value);

        /// <summary>Creates a WiMAX-Downlink-Delay-Jitter-Time attribute (Type 81).</summary>
        /// <param name="value">The downlink delay/jitter in milliseconds.</param>
        public static VendorSpecificAttributes DownlinkDelayJitterTime(int value) => CreateInteger(WimaxAttributeType.DOWNLINK_DELAY_JITTER_TIME, value);

        /// <summary>Creates a WiMAX-Uplink-Granted-Packets attribute (Type 82).</summary>
        /// <param name="value">The uplink granted packets count.</param>
        public static VendorSpecificAttributes UplinkGrantedPackets(int value) => CreateInteger(WimaxAttributeType.UPLINK_GRANTED_PACKETS, value);

        /// <summary>Creates a WiMAX-Downlink-Granted-Packets attribute (Type 83).</summary>
        /// <param name="value">The downlink granted packets count.</param>
        public static VendorSpecificAttributes DownlinkGrantedPackets(int value) => CreateInteger(WimaxAttributeType.DOWNLINK_GRANTED_PACKETS, value);

        #endregion

        #region String / Octets Attributes

        /// <summary>Creates a WiMAX-Release attribute (Type 2).</summary>
        /// <param name="value">The WiMAX release version string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Release(string value) => CreateString(WimaxAttributeType.RELEASE, value);

        /// <summary>Creates a WiMAX-AAA-Session-Id attribute (Type 8) from raw octets.</summary>
        /// <param name="value">The AAA session identifier bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AaaSessionId(byte[] value) => CreateOctets(WimaxAttributeType.AAA_SESSION_ID, value);

        /// <summary>Creates a WiMAX-MSK attribute (Type 10) from raw key octets.</summary>
        /// <param name="value">The Master Session Key bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Msk(byte[] value) => CreateOctets(WimaxAttributeType.MSK, value);

        /// <summary>Creates a WiMAX-DHCP-RK attribute (Type 12) from raw key octets.</summary>
        /// <param name="value">The DHCP Root Key bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpRk(byte[] value) => CreateOctets(WimaxAttributeType.DHCP_RK, value);

        /// <summary>Creates a WiMAX-NAP-Id attribute (Type 17) from raw octets.</summary>
        /// <param name="value">The Network Access Provider identifier bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NapId(byte[] value) => CreateOctets(WimaxAttributeType.NAP_ID, value);

        /// <summary>Creates a WiMAX-NSP-Id attribute (Type 18) from raw octets.</summary>
        /// <param name="value">The Network Service Provider identifier bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NspId(byte[] value) => CreateOctets(WimaxAttributeType.NSP_ID, value);

        /// <summary>Creates a WiMAX-BS-Id attribute (Type 19) from raw octets.</summary>
        /// <param name="value">The Base Station identifier bytes (typically 6 bytes). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BsId(byte[] value) => CreateOctets(WimaxAttributeType.BS_ID, value);

        /// <summary>Creates a WiMAX-Location attribute (Type 20) from raw octets.</summary>
        /// <param name="value">The location information bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Location(byte[] value) => CreateOctets(WimaxAttributeType.LOCATION, value);

        /// <summary>Creates a WiMAX-MN-hHA-MIP4-Key attribute (Type 37) from raw key octets.</summary>
        /// <param name="value">The MN-HA MIP4 key bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MnHhaMip4Key(byte[] value) => CreateOctets(WimaxAttributeType.MN_HHA_MIP4_KEY, value);

        /// <summary>Creates a WiMAX-MN-hHA-MIP6-Key attribute (Type 39) from raw key octets.</summary>
        /// <param name="value">The MN-HA MIP6 key bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MnHhaMip6Key(byte[] value) => CreateOctets(WimaxAttributeType.MN_HHA_MIP6_KEY, value);

        /// <summary>Creates a WiMAX-FA-RK-Key attribute (Type 41) from raw key octets.</summary>
        /// <param name="value">The FA Root Key bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaRkKey(byte[] value) => CreateOctets(WimaxAttributeType.FA_RK_KEY, value);

        /// <summary>Creates a WiMAX-HA-RK-Key attribute (Type 42) from raw key octets.</summary>
        /// <param name="value">The HA Root Key bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HaRkKey(byte[] value) => CreateOctets(WimaxAttributeType.HA_RK_KEY, value);

        /// <summary>Creates a WiMAX-RRQ-MN-HA-Key attribute (Type 46) from raw key octets.</summary>
        /// <param name="value">The RRQ MN-HA key bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RrqMnHaKey(byte[] value) => CreateOctets(WimaxAttributeType.RRQ_MN_HA_KEY, value);

        /// <summary>Creates a WiMAX-Hotline-Indicator attribute (Type 51).</summary>
        /// <param name="value">The hotline indicator string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HotlineIndicator(string value) => CreateString(WimaxAttributeType.HOTLINE_INDICATOR, value);

        /// <summary>Creates a WiMAX-HTTP-Redirection-Rule attribute (Type 52).</summary>
        /// <param name="value">The HTTP redirection rule string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HttpRedirectionRule(string value) => CreateString(WimaxAttributeType.HTTP_REDIRECTION_RULE, value);

        /// <summary>Creates a WiMAX-IP-Redirection-Rule attribute (Type 53).</summary>
        /// <param name="value">The IP redirection rule string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpRedirectionRule(string value) => CreateString(WimaxAttributeType.IP_REDIRECTION_RULE, value);

        /// <summary>Creates a WiMAX-Hotline-Profile-Id attribute (Type 54).</summary>
        /// <param name="value">The hotline profile identifier string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HotlineProfileId(string value) => CreateString(WimaxAttributeType.HOTLINE_PROFILE_ID, value);

        /// <summary>Creates a WiMAX-MN-vHA-MIP4-Key attribute (Type 59) from raw key octets.</summary>
        /// <param name="value">The MN-vHA MIP4 key bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MnVhaMip4Key(byte[] value) => CreateOctets(WimaxAttributeType.MN_VHA_MIP4_KEY, value);

        /// <summary>Creates a WiMAX-MN-vHA-MIP6-Key attribute (Type 61) from raw key octets.</summary>
        /// <param name="value">The MN-vHA MIP6 key bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MnVhaMip6Key(byte[] value) => CreateOctets(WimaxAttributeType.MN_VHA_MIP6_KEY, value);

        /// <summary>Creates a WiMAX-Redirect-URL attribute (Type 63).</summary>
        /// <param name="value">The redirect URL string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value) => CreateString(WimaxAttributeType.REDIRECT_URL, value);

        /// <summary>Creates a WiMAX-Uplink-Flow-Description attribute (Type 68).</summary>
        /// <param name="value">The uplink flow description classifier string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UplinkFlowDescription(string value) => CreateString(WimaxAttributeType.UPLINK_FLOW_DESCRIPTION, value);

        /// <summary>Creates a WiMAX-Downlink-Flow-Description attribute (Type 69).</summary>
        /// <param name="value">The downlink flow description classifier string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DownlinkFlowDescription(string value) => CreateString(WimaxAttributeType.DOWNLINK_FLOW_DESCRIPTION, value);

        /// <summary>Creates a WiMAX-DHCPV6-Server-Address attribute (Type 36) from raw octets (16-byte IPv6 address).</summary>
        /// <param name="value">The DHCPv6 server address bytes (16 bytes). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpV6ServerAddress(byte[] value) => CreateOctets(WimaxAttributeType.DHCPV6_SERVER_ADDRESS, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a WiMAX-DHCP-Msg-Server-IP attribute (Type 15).</summary>
        /// <param name="value">The DHCP message server IP. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes DhcpMsgServerIp(IPAddress value) => CreateIpv4(WimaxAttributeType.DHCP_MSG_SERVER_IP, value);

        /// <summary>Creates a WiMAX-DHCPV4-Server-Address attribute (Type 35).</summary>
        /// <param name="value">The DHCPv4 server address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes DhcpV4ServerAddress(IPAddress value) => CreateIpv4(WimaxAttributeType.DHCPV4_SERVER_ADDRESS, value);

        /// <summary>Creates a WiMAX-RRQ-HA-IP attribute (Type 45).</summary>
        /// <param name="value">The Registration Request HA IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes RrqHaIp(IPAddress value) => CreateIpv4(WimaxAttributeType.RRQ_HA_IP, value);

        /// <summary>Creates a WiMAX-Visited-Fraction-IPGW-Address attribute (Type 58).</summary>
        /// <param name="value">The visited fraction IPGW address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes VisitedFractionIpgwAddress(IPAddress value) => CreateIpv4(WimaxAttributeType.VISITED_FRACTION_IPGW_ADDRESS, value);

        #endregion

        #region Compound (TLV) Attributes

        /// <summary>
        /// Creates a WiMAX compound (TLV) attribute from a pre-assembled inner TLV payload.
        /// </summary>
        /// <remarks>
        /// WiMAX compound attributes (Capability, QoS-Descriptor, PPAC, PPAQ, etc.) contain
        /// nested sub-TLV structures. The caller is responsible for encoding the inner TLV
        /// payload. This method wraps the payload in the correct WiMAX continuation-byte
        /// VSA envelope.
        /// </remarks>
        /// <param name="type">The compound attribute type code.</param>
        /// <param name="innerTlv">The pre-assembled inner TLV payload bytes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="innerTlv"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CreateCompound(WimaxAttributeType type, byte[] innerTlv)
        {
            ArgumentNullException.ThrowIfNull(innerTlv);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, innerTlv, VendorSpecificFormat.Type1Len1Continuation);
        }

        /// <summary>Creates a WiMAX-Capability attribute (Type 1) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Capability(byte[] innerTlv) => CreateCompound(WimaxAttributeType.CAPABILITY, innerTlv);

        /// <summary>Creates a WiMAX-QoS-Descriptor attribute (Type 21) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes QosDescriptor(byte[] innerTlv) => CreateCompound(WimaxAttributeType.QOS_DESCRIPTOR, innerTlv);

        /// <summary>Creates a WiMAX-Uplink-QoS-Descriptor attribute (Type 22) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes UplinkQosDescriptor(byte[] innerTlv) => CreateCompound(WimaxAttributeType.UPLINK_QOS_DESCRIPTOR, innerTlv);

        /// <summary>Creates a WiMAX-Downlink-QoS-Descriptor attribute (Type 23) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes DownlinkQosDescriptor(byte[] innerTlv) => CreateCompound(WimaxAttributeType.DOWNLINK_QOS_DESCRIPTOR, innerTlv);

        /// <summary>Creates a WiMAX-Uplink-Granted-QoS attribute (Type 24) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes UplinkGrantedQos(byte[] innerTlv) => CreateCompound(WimaxAttributeType.UPLINK_GRANTED_QOS, innerTlv);

        /// <summary>Creates a WiMAX-PPAC attribute (Type 29) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Ppac(byte[] innerTlv) => CreateCompound(WimaxAttributeType.PPAC, innerTlv);

        /// <summary>Creates a WiMAX-PPAQ attribute (Type 31) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Ppaq(byte[] innerTlv) => CreateCompound(WimaxAttributeType.PPAQ, innerTlv);

        /// <summary>Creates a WiMAX-Prepaid-Tariff-Switching attribute (Type 32) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes PrepaidTariffSwitching(byte[] innerTlv) => CreateCompound(WimaxAttributeType.PREPAID_TARIFF_SWITCHING, innerTlv);

        /// <summary>Creates a WiMAX-DHCP-Server-Parameters attribute (Type 34) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes DhcpServerParameters(byte[] innerTlv) => CreateCompound(WimaxAttributeType.DHCP_SERVER_PARAMETERS, innerTlv);

        /// <summary>Creates a WiMAX-Packet-Flow-Descriptor-V2 attribute (Type 57) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes PacketFlowDescriptorV2(byte[] innerTlv) => CreateCompound(WimaxAttributeType.PACKET_FLOW_DESCRIPTOR_V2, innerTlv);

        /// <summary>Creates a WiMAX-Granted-QoS-Parameters attribute (Type 70) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes GrantedQosParameters(byte[] innerTlv) => CreateCompound(WimaxAttributeType.GRANTED_QOS_PARAMETERS, innerTlv);

        /// <summary>Creates a WiMAX-Downlink-Granted-QoS attribute (Type 71) from a pre-assembled inner TLV payload.</summary>
        /// <param name="innerTlv">The inner TLV payload. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes DownlinkGrantedQos(byte[] innerTlv) => CreateCompound(WimaxAttributeType.DOWNLINK_GRANTED_QOS, innerTlv);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(WimaxAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data, VendorSpecificFormat.Type1Len1Continuation);
        }

        private static VendorSpecificAttributes CreateString(WimaxAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data, VendorSpecificFormat.Type1Len1Continuation);
        }

        private static VendorSpecificAttributes CreateOctets(WimaxAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value, VendorSpecificFormat.Type1Len1Continuation);
        }

        private static VendorSpecificAttributes CreateIpv4(WimaxAttributeType type, IPAddress value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException(
                    $"Expected an IPv4 (InterNetwork) address, got '{value.AddressFamily}'.",
                    nameof(value));

            byte[] addrBytes = new byte[4];
            value.TryWriteBytes(addrBytes, out _);

            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, addrBytes, VendorSpecificFormat.Type1Len1Continuation);
        }

        #endregion
    }
}