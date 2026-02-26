using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Huawei Technologies (IANA PEN 2011) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.huawei</c>.
    /// </summary>
    /// <remarks>
    /// Huawei Technologies is a major telecommunications equipment vendor.
    /// These attributes are used by Huawei routers, switches, BRAS platforms,
    /// GGSN/PGW, wireless controllers, and firewalls.
    /// </remarks>
    public enum HuaweiAttributeType : byte
    {
        /// <summary>Huawei-Input-Burst-Size (Type 1). Integer. Input burst size in bytes.</summary>
        INPUT_BURST_SIZE = 1,

        /// <summary>Huawei-Input-Average-Rate (Type 2). Integer. Input average rate in bps.</summary>
        INPUT_AVERAGE_RATE = 2,

        /// <summary>Huawei-Input-Peak-Rate (Type 3). Integer. Input peak rate in bps.</summary>
        INPUT_PEAK_RATE = 3,

        /// <summary>Huawei-Output-Burst-Size (Type 4). Integer. Output burst size in bytes.</summary>
        OUTPUT_BURST_SIZE = 4,

        /// <summary>Huawei-Output-Average-Rate (Type 5). Integer. Output average rate in bps.</summary>
        OUTPUT_AVERAGE_RATE = 5,

        /// <summary>Huawei-Output-Peak-Rate (Type 6). Integer. Output peak rate in bps.</summary>
        OUTPUT_PEAK_RATE = 6,

        /// <summary>Huawei-In-Kb-Before-T-Switch (Type 7). Integer. Input kilobytes before tariff switch.</summary>
        IN_KB_BEFORE_T_SWITCH = 7,

        /// <summary>Huawei-Out-Kb-Before-T-Switch (Type 8). Integer. Output kilobytes before tariff switch.</summary>
        OUT_KB_BEFORE_T_SWITCH = 8,

        /// <summary>Huawei-In-Pkt-Before-T-Switch (Type 9). Integer. Input packets before tariff switch.</summary>
        IN_PKT_BEFORE_T_SWITCH = 9,

        /// <summary>Huawei-Out-Pkt-Before-T-Switch (Type 10). Integer. Output packets before tariff switch.</summary>
        OUT_PKT_BEFORE_T_SWITCH = 10,

        /// <summary>Huawei-In-Kb-After-T-Switch (Type 11). Integer. Input kilobytes after tariff switch.</summary>
        IN_KB_AFTER_T_SWITCH = 11,

        /// <summary>Huawei-Out-Kb-After-T-Switch (Type 12). Integer. Output kilobytes after tariff switch.</summary>
        OUT_KB_AFTER_T_SWITCH = 12,

        /// <summary>Huawei-In-Pkt-After-T-Switch (Type 13). Integer. Input packets after tariff switch.</summary>
        IN_PKT_AFTER_T_SWITCH = 13,

        /// <summary>Huawei-Out-Pkt-After-T-Switch (Type 14). Integer. Output packets after tariff switch.</summary>
        OUT_PKT_AFTER_T_SWITCH = 14,

        /// <summary>Huawei-Remanent-Volume (Type 15). Integer. Remanent (remaining) volume in KB.</summary>
        REMANENT_VOLUME = 15,

        /// <summary>Huawei-Tariff-Switch-Interval (Type 16). Integer. Tariff switch interval in seconds.</summary>
        TARIFF_SWITCH_INTERVAL = 16,

        /// <summary>Huawei-ISP-ID (Type 17). String. ISP identifier.</summary>
        ISP_ID = 17,

        /// <summary>Huawei-Max-Users-Per-Logic-Port (Type 18). Integer. Maximum users per logical port.</summary>
        MAX_USERS_PER_LOGIC_PORT = 18,

        /// <summary>Huawei-Command (Type 20). Integer. Command authorization level.</summary>
        COMMAND = 20,

        /// <summary>Huawei-Domain-Name (Type 21). String. Domain name.</summary>
        DOMAIN_NAME = 21,

        /// <summary>Huawei-Priority (Type 22). Integer. User priority.</summary>
        PRIORITY = 22,

        /// <summary>Huawei-Control-Identifier (Type 24). Integer. Control identifier.</summary>
        CONTROL_IDENTIFIER = 24,

        /// <summary>Huawei-Result-Code (Type 25). Integer. Result code.</summary>
        RESULT_CODE = 25,

        /// <summary>Huawei-Connect-ID (Type 26). Integer. Connection identifier.</summary>
        CONNECT_ID = 26,

        /// <summary>Huawei-PortalURL (Type 27). String. Portal URL.</summary>
        PORTAL_URL = 27,

        /// <summary>Huawei-FTP-Directory (Type 28). String. FTP directory path.</summary>
        FTP_DIRECTORY = 28,

        /// <summary>Huawei-Exec-Privilege (Type 29). Integer. CLI exec privilege level.</summary>
        EXEC_PRIVILEGE = 29,

        /// <summary>Huawei-IP-Address (Type 30). IP address. Subscriber IP address.</summary>
        IP_ADDRESS = 30,

        /// <summary>Huawei-Qos-Profile-Name (Type 31). String. QoS profile name.</summary>
        QOS_PROFILE_NAME = 31,

        /// <summary>Huawei-SIP-Server (Type 32). String. SIP server address.</summary>
        SIP_SERVER = 32,

        /// <summary>Huawei-User-Password (Type 33). String. User password.</summary>
        USER_PASSWORD = 33,

        /// <summary>Huawei-Command-Mode (Type 34). String. Command mode.</summary>
        COMMAND_MODE = 34,

        /// <summary>Huawei-Renewal-Time (Type 35). Integer. DHCP renewal time in seconds.</summary>
        RENEWAL_TIME = 35,

        /// <summary>Huawei-Rebinding-Time (Type 36). Integer. DHCP rebinding time in seconds.</summary>
        REBINDING_TIME = 36,

        /// <summary>Huawei-Igmp-Enable (Type 37). Integer. IGMP enable flag.</summary>
        IGMP_ENABLE = 37,

        /// <summary>Huawei-Destnation-IP-Addr (Type 39). String. Destination IP address.</summary>
        DESTINATION_IP_ADDR = 39,

        /// <summary>Huawei-Destnation-Volume (Type 40). String. Destination volume.</summary>
        DESTINATION_VOLUME = 40,

        /// <summary>Huawei-Startup-Stamp (Type 59). Integer. Startup timestamp.</summary>
        STARTUP_STAMP = 59,

        /// <summary>Huawei-IPHost-Addr (Type 60). String. IP host address.</summary>
        IPHOST_ADDR = 60,

        /// <summary>Huawei-Up-Priority (Type 61). Integer. Upstream priority.</summary>
        UP_PRIORITY = 61,

        /// <summary>Huawei-Down-Priority (Type 62). Integer. Downstream priority.</summary>
        DOWN_PRIORITY = 62,

        /// <summary>Huawei-Tunnel-VPN-Instance (Type 63). String. Tunnel VPN instance name.</summary>
        TUNNEL_VPN_INSTANCE = 63,

        /// <summary>Huawei-VT-Name (Type 64). Integer. VT name index.</summary>
        VT_NAME = 64,

        /// <summary>Huawei-User-Date (Type 65). String. User date string.</summary>
        USER_DATE = 65,

        /// <summary>Huawei-User-Class (Type 66). String. User class string.</summary>
        USER_CLASS = 66,

        /// <summary>Huawei-PPP-NCP-Type (Type 70). Integer. PPP NCP type.</summary>
        PPP_NCP_TYPE = 70,

        /// <summary>Huawei-VSI-Name (Type 71). String. Virtual Switch Instance name.</summary>
        VSI_NAME = 71,

        /// <summary>Huawei-Subnet-Mask (Type 72). IP address. Subscriber subnet mask.</summary>
        SUBNET_MASK = 72,

        /// <summary>Huawei-Gateway-Address (Type 73). IP address. Gateway address.</summary>
        GATEWAY_ADDRESS = 73,

        /// <summary>Huawei-Lease-Time (Type 74). Integer. DHCP lease time in seconds.</summary>
        LEASE_TIME = 74,

        /// <summary>Huawei-Primary-WINS (Type 75). IP address. Primary WINS server.</summary>
        PRIMARY_WINS = 75,

        /// <summary>Huawei-Secondary-WINS (Type 76). IP address. Secondary WINS server.</summary>
        SECONDARY_WINS = 76,

        /// <summary>Huawei-Input-Peak-Burst-Size (Type 77). Integer. Input peak burst size in bytes.</summary>
        INPUT_PEAK_BURST_SIZE = 77,

        /// <summary>Huawei-Output-Peak-Burst-Size (Type 78). Integer. Output peak burst size in bytes.</summary>
        OUTPUT_PEAK_BURST_SIZE = 78,

        /// <summary>Huawei-Reduced-CIR (Type 79). Integer. Reduced Committed Information Rate in bps.</summary>
        REDUCED_CIR = 79,

        /// <summary>Huawei-Tunnel-Session-Limit (Type 80). Integer. Tunnel session limit.</summary>
        TUNNEL_SESSION_LIMIT = 80,

        /// <summary>Huawei-Zone-Name (Type 81). String. Security zone name.</summary>
        ZONE_NAME = 81,

        /// <summary>Huawei-Data-Filter (Type 82). String. Data filter name.</summary>
        DATA_FILTER = 82,

        /// <summary>Huawei-Access-Service (Type 83). String. Access service name.</summary>
        ACCESS_SERVICE = 83,

        /// <summary>Huawei-Accounting-Level (Type 84). Integer. Accounting level.</summary>
        ACCOUNTING_LEVEL = 84,

        /// <summary>Huawei-HW-Portal-Mode (Type 85). Integer. Portal mode.</summary>
        HW_PORTAL_MODE = 85,

        /// <summary>Huawei-DPI-Policy-Name (Type 86). String. DPI policy name.</summary>
        DPI_POLICY_NAME = 86,

        /// <summary>Huawei-Policy-Route (Type 87). IP address. Policy route next hop.</summary>
        POLICY_ROUTE = 87,

        /// <summary>Huawei-Framed-Pool (Type 88). String. Framed address pool name.</summary>
        FRAMED_POOL = 88,

        /// <summary>Huawei-L2TP-Terminate-Cause (Type 89). String. L2TP terminate cause.</summary>
        L2TP_TERMINATE_CAUSE = 89,

        /// <summary>Huawei-Multi-Account-Mode (Type 90). Integer. Multi-account mode.</summary>
        MULTI_ACCOUNT_MODE = 90,

        /// <summary>Huawei-Queue-Profile (Type 91). String. Queue profile name.</summary>
        QUEUE_PROFILE = 91,

        /// <summary>Huawei-Layer4-Session-Limit (Type 92). Integer. Layer 4 session limit.</summary>
        LAYER4_SESSION_LIMIT = 92,

        /// <summary>Huawei-Multicast-Profile (Type 93). String. Multicast profile name.</summary>
        MULTICAST_PROFILE = 93,

        /// <summary>Huawei-VPN-Instance (Type 94). String. VPN instance name.</summary>
        VPN_INSTANCE = 94,

        /// <summary>Huawei-Policy-Name (Type 95). String. Policy name.</summary>
        POLICY_NAME = 95,

        /// <summary>Huawei-Tunnel-Group-Name (Type 96). String. Tunnel group name.</summary>
        TUNNEL_GROUP_NAME = 96,

        /// <summary>Huawei-Multicast-Source-Group (Type 97). String. Multicast source group.</summary>
        MULTICAST_SOURCE_GROUP = 97,

        /// <summary>Huawei-Multicast-Receive-Group (Type 98). String. Multicast receive group.</summary>
        MULTICAST_RECEIVE_GROUP = 98,

        /// <summary>Huawei-User-Multicast-Type (Type 99). Integer. User multicast type.</summary>
        USER_MULTICAST_TYPE = 99,

        /// <summary>Huawei-DNS-Server-IPv6-Address (Type 100). String. DNS server IPv6 address.</summary>
        DNS_SERVER_IPV6_ADDRESS = 100,

        /// <summary>Huawei-DHCPv4-Option121 (Type 101). String. DHCPv4 classless static route option.</summary>
        DHCPV4_OPTION121 = 101,

        /// <summary>Huawei-DHCPv4-Option43 (Type 102). String. DHCPv4 vendor-specific information option.</summary>
        DHCPV4_OPTION43 = 102,

        /// <summary>Huawei-HW-Sub-Service-Policy (Type 103). String. Sub-service policy name.</summary>
        HW_SUB_SERVICE_POLICY = 103,

        /// <summary>Huawei-HW-Subscribers-Limit (Type 104). Integer. Subscribers limit per interface.</summary>
        HW_SUBSCRIBERS_LIMIT = 104,

        /// <summary>Huawei-Primary-DNS (Type 135). IP address. Primary DNS server.</summary>
        PRIMARY_DNS = 135,

        /// <summary>Huawei-Secondary-DNS (Type 136). IP address. Secondary DNS server.</summary>
        SECONDARY_DNS = 136,

        /// <summary>Huawei-Domain-Name-V2 (Type 138). String. Domain name (version 2).</summary>
        DOMAIN_NAME_V2 = 138,

        /// <summary>Huawei-User-Group-Name (Type 140). String. User group name.</summary>
        USER_GROUP_NAME = 140,

        /// <summary>Huawei-Security-Level (Type 141). Integer. Security level.</summary>
        SECURITY_LEVEL = 141,

        /// <summary>Huawei-Acct-IPv6-Input-Octets (Type 144). Integer. IPv6 accounting input octets.</summary>
        ACCT_IPV6_INPUT_OCTETS = 144,

        /// <summary>Huawei-Acct-IPv6-Output-Octets (Type 145). Integer. IPv6 accounting output octets.</summary>
        ACCT_IPV6_OUTPUT_OCTETS = 145,

        /// <summary>Huawei-Acct-IPv6-Input-Packets (Type 146). Integer. IPv6 accounting input packets.</summary>
        ACCT_IPV6_INPUT_PACKETS = 146,

        /// <summary>Huawei-Acct-IPv6-Output-Packets (Type 147). Integer. IPv6 accounting output packets.</summary>
        ACCT_IPV6_OUTPUT_PACKETS = 147,

        /// <summary>Huawei-Acct-IPv6-Input-Gigawords (Type 148). Integer. IPv6 accounting input gigawords.</summary>
        ACCT_IPV6_INPUT_GIGAWORDS = 148,

        /// <summary>Huawei-Acct-IPv6-Output-Gigawords (Type 149). Integer. IPv6 accounting output gigawords.</summary>
        ACCT_IPV6_OUTPUT_GIGAWORDS = 149,

        /// <summary>Huawei-AVPair (Type 200). String. Attribute-value pair string.</summary>
        AVPAIR = 200,

        /// <summary>Huawei-Delegated-IPv6-Prefix-Pool (Type 201). String. Delegated IPv6 prefix pool name.</summary>
        DELEGATED_IPV6_PREFIX_POOL = 201,

        /// <summary>Huawei-IPv6-Address-Pool (Type 202). String. IPv6 address pool name.</summary>
        IPV6_ADDRESS_POOL = 202,

        /// <summary>Huawei-NAT-Policy-Name (Type 203). String. NAT policy name.</summary>
        NAT_POLICY_NAME = 203,

        /// <summary>Huawei-NAT-Public-Address (Type 204). String. NAT public address.</summary>
        NAT_PUBLIC_ADDRESS = 204,

        /// <summary>Huawei-NAT-Start-Port (Type 205). Integer. NAT start port.</summary>
        NAT_START_PORT = 205,

        /// <summary>Huawei-NAT-End-Port (Type 206). Integer. NAT end port.</summary>
        NAT_END_PORT = 206,

        /// <summary>Huawei-NAT-Port-Forwarding (Type 207). String. NAT port forwarding rule.</summary>
        NAT_PORT_FORWARDING = 207,

        /// <summary>Huawei-Product-ID (Type 255). String. Product identifier string.</summary>
        PRODUCT_ID = 255
    }

    /// <summary>
    /// Huawei-Exec-Privilege attribute values (Type 29).
    /// </summary>
    public enum HUAWEI_EXEC_PRIVILEGE
    {
        /// <summary>Visit level (level 0).</summary>
        VISIT = 0,

        /// <summary>Monitor level (level 1).</summary>
        MONITOR = 1,

        /// <summary>System level (level 2).</summary>
        SYSTEM = 2,

        /// <summary>Management level (level 3).</summary>
        MANAGE = 3,

        /// <summary>Level 15 (maximum).</summary>
        LEVEL_15 = 15
    }

    /// <summary>
    /// Huawei-Igmp-Enable attribute values (Type 37).
    /// </summary>
    public enum HUAWEI_IGMP_ENABLE
    {
        /// <summary>IGMP disabled.</summary>
        DISABLED = 0,

        /// <summary>IGMP enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Huawei-Accounting-Level attribute values (Type 84).
    /// </summary>
    public enum HUAWEI_ACCOUNTING_LEVEL
    {
        /// <summary>User-level accounting.</summary>
        USER = 0,

        /// <summary>IP-level accounting.</summary>
        IP = 1,

        /// <summary>IP-user-level accounting.</summary>
        IP_USER = 2
    }

    /// <summary>
    /// Huawei-HW-Portal-Mode attribute values (Type 85).
    /// </summary>
    public enum HUAWEI_HW_PORTAL_MODE
    {
        /// <summary>No portal.</summary>
        NONE = 0,

        /// <summary>Layer 2 portal mode.</summary>
        LAYER2 = 1,

        /// <summary>Layer 3 portal mode.</summary>
        LAYER3 = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Huawei Technologies
    /// (IANA PEN 2011) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.huawei</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Huawei's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2011</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Huawei Technologies routers, switches, BRAS/BNG
    /// platforms (ME60, MA5200G), GGSN/PGW, USG firewalls, and wireless controllers
    /// for RADIUS-based subscriber session management including QoS rate limiting
    /// (input/output peak, average, burst rates), CLI exec privilege, command
    /// authorization, ISP/domain management, tariff switching, IGMP multicast,
    /// DNS/WINS provisioning, DHCP options, IP address pools (v4/v6), VPN instance
    /// mapping, NAT policy configuration, portal mode, DPI policies, security
    /// zones and levels, data filtering, queue/multicast profiles, L2TP management,
    /// IPv6 accounting, and subscriber limits.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(HuaweiAttributes.ExecPrivilege(HUAWEI_EXEC_PRIVILEGE.MANAGE));
    /// packet.SetAttribute(HuaweiAttributes.QosProfileName("premium-50m"));
    /// packet.SetAttribute(HuaweiAttributes.InputAverageRate(50000000));
    /// packet.SetAttribute(HuaweiAttributes.OutputAverageRate(100000000));
    /// packet.SetAttribute(HuaweiAttributes.DomainName("isp-example"));
    /// packet.SetAttribute(HuaweiAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// </code>
    /// </remarks>
    public static class HuaweiAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Huawei Technologies.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2011;

        #region Integer Attributes

        /// <summary>Creates a Huawei-Input-Burst-Size attribute (Type 1).</summary>
        public static VendorSpecificAttributes InputBurstSize(int value) => CreateInteger(HuaweiAttributeType.INPUT_BURST_SIZE, value);

        /// <summary>Creates a Huawei-Input-Average-Rate attribute (Type 2).</summary>
        public static VendorSpecificAttributes InputAverageRate(int value) => CreateInteger(HuaweiAttributeType.INPUT_AVERAGE_RATE, value);

        /// <summary>Creates a Huawei-Input-Peak-Rate attribute (Type 3).</summary>
        public static VendorSpecificAttributes InputPeakRate(int value) => CreateInteger(HuaweiAttributeType.INPUT_PEAK_RATE, value);

        /// <summary>Creates a Huawei-Output-Burst-Size attribute (Type 4).</summary>
        public static VendorSpecificAttributes OutputBurstSize(int value) => CreateInteger(HuaweiAttributeType.OUTPUT_BURST_SIZE, value);

        /// <summary>Creates a Huawei-Output-Average-Rate attribute (Type 5).</summary>
        public static VendorSpecificAttributes OutputAverageRate(int value) => CreateInteger(HuaweiAttributeType.OUTPUT_AVERAGE_RATE, value);

        /// <summary>Creates a Huawei-Output-Peak-Rate attribute (Type 6).</summary>
        public static VendorSpecificAttributes OutputPeakRate(int value) => CreateInteger(HuaweiAttributeType.OUTPUT_PEAK_RATE, value);

        /// <summary>Creates a Huawei-In-Kb-Before-T-Switch attribute (Type 7).</summary>
        public static VendorSpecificAttributes InKbBeforeTSwitch(int value) => CreateInteger(HuaweiAttributeType.IN_KB_BEFORE_T_SWITCH, value);

        /// <summary>Creates a Huawei-Out-Kb-Before-T-Switch attribute (Type 8).</summary>
        public static VendorSpecificAttributes OutKbBeforeTSwitch(int value) => CreateInteger(HuaweiAttributeType.OUT_KB_BEFORE_T_SWITCH, value);

        /// <summary>Creates a Huawei-In-Pkt-Before-T-Switch attribute (Type 9).</summary>
        public static VendorSpecificAttributes InPktBeforeTSwitch(int value) => CreateInteger(HuaweiAttributeType.IN_PKT_BEFORE_T_SWITCH, value);

        /// <summary>Creates a Huawei-Out-Pkt-Before-T-Switch attribute (Type 10).</summary>
        public static VendorSpecificAttributes OutPktBeforeTSwitch(int value) => CreateInteger(HuaweiAttributeType.OUT_PKT_BEFORE_T_SWITCH, value);

        /// <summary>Creates a Huawei-In-Kb-After-T-Switch attribute (Type 11).</summary>
        public static VendorSpecificAttributes InKbAfterTSwitch(int value) => CreateInteger(HuaweiAttributeType.IN_KB_AFTER_T_SWITCH, value);

        /// <summary>Creates a Huawei-Out-Kb-After-T-Switch attribute (Type 12).</summary>
        public static VendorSpecificAttributes OutKbAfterTSwitch(int value) => CreateInteger(HuaweiAttributeType.OUT_KB_AFTER_T_SWITCH, value);

        /// <summary>Creates a Huawei-In-Pkt-After-T-Switch attribute (Type 13).</summary>
        public static VendorSpecificAttributes InPktAfterTSwitch(int value) => CreateInteger(HuaweiAttributeType.IN_PKT_AFTER_T_SWITCH, value);

        /// <summary>Creates a Huawei-Out-Pkt-After-T-Switch attribute (Type 14).</summary>
        public static VendorSpecificAttributes OutPktAfterTSwitch(int value) => CreateInteger(HuaweiAttributeType.OUT_PKT_AFTER_T_SWITCH, value);

        /// <summary>Creates a Huawei-Remanent-Volume attribute (Type 15).</summary>
        public static VendorSpecificAttributes RemanentVolume(int value) => CreateInteger(HuaweiAttributeType.REMANENT_VOLUME, value);

        /// <summary>Creates a Huawei-Tariff-Switch-Interval attribute (Type 16).</summary>
        public static VendorSpecificAttributes TariffSwitchInterval(int value) => CreateInteger(HuaweiAttributeType.TARIFF_SWITCH_INTERVAL, value);

        /// <summary>Creates a Huawei-Max-Users-Per-Logic-Port attribute (Type 18).</summary>
        public static VendorSpecificAttributes MaxUsersPerLogicPort(int value) => CreateInteger(HuaweiAttributeType.MAX_USERS_PER_LOGIC_PORT, value);

        /// <summary>Creates a Huawei-Command attribute (Type 20).</summary>
        public static VendorSpecificAttributes Command(int value) => CreateInteger(HuaweiAttributeType.COMMAND, value);

        /// <summary>Creates a Huawei-Priority attribute (Type 22).</summary>
        public static VendorSpecificAttributes Priority(int value) => CreateInteger(HuaweiAttributeType.PRIORITY, value);

        /// <summary>Creates a Huawei-Control-Identifier attribute (Type 24).</summary>
        public static VendorSpecificAttributes ControlIdentifier(int value) => CreateInteger(HuaweiAttributeType.CONTROL_IDENTIFIER, value);

        /// <summary>Creates a Huawei-Result-Code attribute (Type 25).</summary>
        public static VendorSpecificAttributes ResultCode(int value) => CreateInteger(HuaweiAttributeType.RESULT_CODE, value);

        /// <summary>Creates a Huawei-Connect-ID attribute (Type 26).</summary>
        public static VendorSpecificAttributes ConnectId(int value) => CreateInteger(HuaweiAttributeType.CONNECT_ID, value);

        /// <summary>Creates a Huawei-Exec-Privilege attribute (Type 29).</summary>
        /// <param name="value">The CLI exec privilege level. See <see cref="HUAWEI_EXEC_PRIVILEGE"/>.</param>
        public static VendorSpecificAttributes ExecPrivilege(HUAWEI_EXEC_PRIVILEGE value) => CreateInteger(HuaweiAttributeType.EXEC_PRIVILEGE, (int)value);

        /// <summary>Creates a Huawei-Renewal-Time attribute (Type 35).</summary>
        public static VendorSpecificAttributes RenewalTime(int value) => CreateInteger(HuaweiAttributeType.RENEWAL_TIME, value);

        /// <summary>Creates a Huawei-Rebinding-Time attribute (Type 36).</summary>
        public static VendorSpecificAttributes RebindingTime(int value) => CreateInteger(HuaweiAttributeType.REBINDING_TIME, value);

        /// <summary>Creates a Huawei-Igmp-Enable attribute (Type 37).</summary>
        /// <param name="value">The IGMP enable setting. See <see cref="HUAWEI_IGMP_ENABLE"/>.</param>
        public static VendorSpecificAttributes IgmpEnable(HUAWEI_IGMP_ENABLE value) => CreateInteger(HuaweiAttributeType.IGMP_ENABLE, (int)value);

        /// <summary>Creates a Huawei-Startup-Stamp attribute (Type 59).</summary>
        public static VendorSpecificAttributes StartupStamp(int value) => CreateInteger(HuaweiAttributeType.STARTUP_STAMP, value);

        /// <summary>Creates a Huawei-Up-Priority attribute (Type 61).</summary>
        public static VendorSpecificAttributes UpPriority(int value) => CreateInteger(HuaweiAttributeType.UP_PRIORITY, value);

        /// <summary>Creates a Huawei-Down-Priority attribute (Type 62).</summary>
        public static VendorSpecificAttributes DownPriority(int value) => CreateInteger(HuaweiAttributeType.DOWN_PRIORITY, value);

        /// <summary>Creates a Huawei-VT-Name attribute (Type 64).</summary>
        public static VendorSpecificAttributes VtName(int value) => CreateInteger(HuaweiAttributeType.VT_NAME, value);

        /// <summary>Creates a Huawei-PPP-NCP-Type attribute (Type 70).</summary>
        public static VendorSpecificAttributes PppNcpType(int value) => CreateInteger(HuaweiAttributeType.PPP_NCP_TYPE, value);

        /// <summary>Creates a Huawei-Lease-Time attribute (Type 74).</summary>
        public static VendorSpecificAttributes LeaseTime(int value) => CreateInteger(HuaweiAttributeType.LEASE_TIME, value);

        /// <summary>Creates a Huawei-Input-Peak-Burst-Size attribute (Type 77).</summary>
        public static VendorSpecificAttributes InputPeakBurstSize(int value) => CreateInteger(HuaweiAttributeType.INPUT_PEAK_BURST_SIZE, value);

        /// <summary>Creates a Huawei-Output-Peak-Burst-Size attribute (Type 78).</summary>
        public static VendorSpecificAttributes OutputPeakBurstSize(int value) => CreateInteger(HuaweiAttributeType.OUTPUT_PEAK_BURST_SIZE, value);

        /// <summary>Creates a Huawei-Reduced-CIR attribute (Type 79).</summary>
        public static VendorSpecificAttributes ReducedCir(int value) => CreateInteger(HuaweiAttributeType.REDUCED_CIR, value);

        /// <summary>Creates a Huawei-Tunnel-Session-Limit attribute (Type 80).</summary>
        public static VendorSpecificAttributes TunnelSessionLimit(int value) => CreateInteger(HuaweiAttributeType.TUNNEL_SESSION_LIMIT, value);

        /// <summary>Creates a Huawei-Accounting-Level attribute (Type 84).</summary>
        /// <param name="value">The accounting level. See <see cref="HUAWEI_ACCOUNTING_LEVEL"/>.</param>
        public static VendorSpecificAttributes AccountingLevel(HUAWEI_ACCOUNTING_LEVEL value) => CreateInteger(HuaweiAttributeType.ACCOUNTING_LEVEL, (int)value);

        /// <summary>Creates a Huawei-HW-Portal-Mode attribute (Type 85).</summary>
        /// <param name="value">The portal mode. See <see cref="HUAWEI_HW_PORTAL_MODE"/>.</param>
        public static VendorSpecificAttributes HwPortalMode(HUAWEI_HW_PORTAL_MODE value) => CreateInteger(HuaweiAttributeType.HW_PORTAL_MODE, (int)value);

        /// <summary>Creates a Huawei-Multi-Account-Mode attribute (Type 90).</summary>
        public static VendorSpecificAttributes MultiAccountMode(int value) => CreateInteger(HuaweiAttributeType.MULTI_ACCOUNT_MODE, value);

        /// <summary>Creates a Huawei-Layer4-Session-Limit attribute (Type 92).</summary>
        public static VendorSpecificAttributes Layer4SessionLimit(int value) => CreateInteger(HuaweiAttributeType.LAYER4_SESSION_LIMIT, value);

        /// <summary>Creates a Huawei-User-Multicast-Type attribute (Type 99).</summary>
        public static VendorSpecificAttributes UserMulticastType(int value) => CreateInteger(HuaweiAttributeType.USER_MULTICAST_TYPE, value);

        /// <summary>Creates a Huawei-HW-Subscribers-Limit attribute (Type 104).</summary>
        public static VendorSpecificAttributes HwSubscribersLimit(int value) => CreateInteger(HuaweiAttributeType.HW_SUBSCRIBERS_LIMIT, value);

        /// <summary>Creates a Huawei-Security-Level attribute (Type 141).</summary>
        public static VendorSpecificAttributes SecurityLevel(int value) => CreateInteger(HuaweiAttributeType.SECURITY_LEVEL, value);

        /// <summary>Creates a Huawei-Acct-IPv6-Input-Octets attribute (Type 144).</summary>
        public static VendorSpecificAttributes AcctIpv6InputOctets(int value) => CreateInteger(HuaweiAttributeType.ACCT_IPV6_INPUT_OCTETS, value);

        /// <summary>Creates a Huawei-Acct-IPv6-Output-Octets attribute (Type 145).</summary>
        public static VendorSpecificAttributes AcctIpv6OutputOctets(int value) => CreateInteger(HuaweiAttributeType.ACCT_IPV6_OUTPUT_OCTETS, value);

        /// <summary>Creates a Huawei-Acct-IPv6-Input-Packets attribute (Type 146).</summary>
        public static VendorSpecificAttributes AcctIpv6InputPackets(int value) => CreateInteger(HuaweiAttributeType.ACCT_IPV6_INPUT_PACKETS, value);

        /// <summary>Creates a Huawei-Acct-IPv6-Output-Packets attribute (Type 147).</summary>
        public static VendorSpecificAttributes AcctIpv6OutputPackets(int value) => CreateInteger(HuaweiAttributeType.ACCT_IPV6_OUTPUT_PACKETS, value);

        /// <summary>Creates a Huawei-Acct-IPv6-Input-Gigawords attribute (Type 148).</summary>
        public static VendorSpecificAttributes AcctIpv6InputGigawords(int value) => CreateInteger(HuaweiAttributeType.ACCT_IPV6_INPUT_GIGAWORDS, value);

        /// <summary>Creates a Huawei-Acct-IPv6-Output-Gigawords attribute (Type 149).</summary>
        public static VendorSpecificAttributes AcctIpv6OutputGigawords(int value) => CreateInteger(HuaweiAttributeType.ACCT_IPV6_OUTPUT_GIGAWORDS, value);

        /// <summary>Creates a Huawei-NAT-Start-Port attribute (Type 205).</summary>
        public static VendorSpecificAttributes NatStartPort(int value) => CreateInteger(HuaweiAttributeType.NAT_START_PORT, value);

        /// <summary>Creates a Huawei-NAT-End-Port attribute (Type 206).</summary>
        public static VendorSpecificAttributes NatEndPort(int value) => CreateInteger(HuaweiAttributeType.NAT_END_PORT, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Huawei-ISP-ID attribute (Type 17).</summary>
        public static VendorSpecificAttributes IspId(string value) => CreateString(HuaweiAttributeType.ISP_ID, value);

        /// <summary>Creates a Huawei-Domain-Name attribute (Type 21).</summary>
        public static VendorSpecificAttributes DomainName(string value) => CreateString(HuaweiAttributeType.DOMAIN_NAME, value);

        /// <summary>Creates a Huawei-PortalURL attribute (Type 27).</summary>
        public static VendorSpecificAttributes PortalUrl(string value) => CreateString(HuaweiAttributeType.PORTAL_URL, value);

        /// <summary>Creates a Huawei-FTP-Directory attribute (Type 28).</summary>
        public static VendorSpecificAttributes FtpDirectory(string value) => CreateString(HuaweiAttributeType.FTP_DIRECTORY, value);

        /// <summary>Creates a Huawei-Qos-Profile-Name attribute (Type 31).</summary>
        public static VendorSpecificAttributes QosProfileName(string value) => CreateString(HuaweiAttributeType.QOS_PROFILE_NAME, value);

        /// <summary>Creates a Huawei-SIP-Server attribute (Type 32).</summary>
        public static VendorSpecificAttributes SipServer(string value) => CreateString(HuaweiAttributeType.SIP_SERVER, value);

        /// <summary>Creates a Huawei-User-Password attribute (Type 33).</summary>
        public static VendorSpecificAttributes UserPassword(string value) => CreateString(HuaweiAttributeType.USER_PASSWORD, value);

        /// <summary>Creates a Huawei-Command-Mode attribute (Type 34).</summary>
        public static VendorSpecificAttributes CommandMode(string value) => CreateString(HuaweiAttributeType.COMMAND_MODE, value);

        /// <summary>Creates a Huawei-Destnation-IP-Addr attribute (Type 39).</summary>
        public static VendorSpecificAttributes DestinationIpAddr(string value) => CreateString(HuaweiAttributeType.DESTINATION_IP_ADDR, value);

        /// <summary>Creates a Huawei-Destnation-Volume attribute (Type 40).</summary>
        public static VendorSpecificAttributes DestinationVolume(string value) => CreateString(HuaweiAttributeType.DESTINATION_VOLUME, value);

        /// <summary>Creates a Huawei-IPHost-Addr attribute (Type 60).</summary>
        public static VendorSpecificAttributes IpHostAddr(string value) => CreateString(HuaweiAttributeType.IPHOST_ADDR, value);

        /// <summary>Creates a Huawei-Tunnel-VPN-Instance attribute (Type 63).</summary>
        public static VendorSpecificAttributes TunnelVpnInstance(string value) => CreateString(HuaweiAttributeType.TUNNEL_VPN_INSTANCE, value);

        /// <summary>Creates a Huawei-User-Date attribute (Type 65).</summary>
        public static VendorSpecificAttributes UserDate(string value) => CreateString(HuaweiAttributeType.USER_DATE, value);

        /// <summary>Creates a Huawei-User-Class attribute (Type 66).</summary>
        public static VendorSpecificAttributes UserClass(string value) => CreateString(HuaweiAttributeType.USER_CLASS, value);

        /// <summary>Creates a Huawei-VSI-Name attribute (Type 71).</summary>
        public static VendorSpecificAttributes VsiName(string value) => CreateString(HuaweiAttributeType.VSI_NAME, value);

        /// <summary>Creates a Huawei-Zone-Name attribute (Type 81).</summary>
        public static VendorSpecificAttributes ZoneName(string value) => CreateString(HuaweiAttributeType.ZONE_NAME, value);

        /// <summary>Creates a Huawei-Data-Filter attribute (Type 82).</summary>
        public static VendorSpecificAttributes DataFilter(string value) => CreateString(HuaweiAttributeType.DATA_FILTER, value);

        /// <summary>Creates a Huawei-Access-Service attribute (Type 83).</summary>
        public static VendorSpecificAttributes AccessService(string value) => CreateString(HuaweiAttributeType.ACCESS_SERVICE, value);

        /// <summary>Creates a Huawei-DPI-Policy-Name attribute (Type 86).</summary>
        public static VendorSpecificAttributes DpiPolicyName(string value) => CreateString(HuaweiAttributeType.DPI_POLICY_NAME, value);

        /// <summary>Creates a Huawei-Framed-Pool attribute (Type 88).</summary>
        public static VendorSpecificAttributes FramedPool(string value) => CreateString(HuaweiAttributeType.FRAMED_POOL, value);

        /// <summary>Creates a Huawei-L2TP-Terminate-Cause attribute (Type 89).</summary>
        public static VendorSpecificAttributes L2tpTerminateCause(string value) => CreateString(HuaweiAttributeType.L2TP_TERMINATE_CAUSE, value);

        /// <summary>Creates a Huawei-Queue-Profile attribute (Type 91).</summary>
        public static VendorSpecificAttributes QueueProfile(string value) => CreateString(HuaweiAttributeType.QUEUE_PROFILE, value);

        /// <summary>Creates a Huawei-Multicast-Profile attribute (Type 93).</summary>
        public static VendorSpecificAttributes MulticastProfile(string value) => CreateString(HuaweiAttributeType.MULTICAST_PROFILE, value);

        /// <summary>Creates a Huawei-VPN-Instance attribute (Type 94).</summary>
        public static VendorSpecificAttributes VpnInstance(string value) => CreateString(HuaweiAttributeType.VPN_INSTANCE, value);

        /// <summary>Creates a Huawei-Policy-Name attribute (Type 95).</summary>
        public static VendorSpecificAttributes PolicyName(string value) => CreateString(HuaweiAttributeType.POLICY_NAME, value);

        /// <summary>Creates a Huawei-Tunnel-Group-Name attribute (Type 96).</summary>
        public static VendorSpecificAttributes TunnelGroupName(string value) => CreateString(HuaweiAttributeType.TUNNEL_GROUP_NAME, value);

        /// <summary>Creates a Huawei-Multicast-Source-Group attribute (Type 97).</summary>
        public static VendorSpecificAttributes MulticastSourceGroup(string value) => CreateString(HuaweiAttributeType.MULTICAST_SOURCE_GROUP, value);

        /// <summary>Creates a Huawei-Multicast-Receive-Group attribute (Type 98).</summary>
        public static VendorSpecificAttributes MulticastReceiveGroup(string value) => CreateString(HuaweiAttributeType.MULTICAST_RECEIVE_GROUP, value);

        /// <summary>Creates a Huawei-DNS-Server-IPv6-Address attribute (Type 100).</summary>
        public static VendorSpecificAttributes DnsServerIpv6Address(string value) => CreateString(HuaweiAttributeType.DNS_SERVER_IPV6_ADDRESS, value);

        /// <summary>Creates a Huawei-DHCPv4-Option121 attribute (Type 101).</summary>
        public static VendorSpecificAttributes Dhcpv4Option121(string value) => CreateString(HuaweiAttributeType.DHCPV4_OPTION121, value);

        /// <summary>Creates a Huawei-DHCPv4-Option43 attribute (Type 102).</summary>
        public static VendorSpecificAttributes Dhcpv4Option43(string value) => CreateString(HuaweiAttributeType.DHCPV4_OPTION43, value);

        /// <summary>Creates a Huawei-HW-Sub-Service-Policy attribute (Type 103).</summary>
        public static VendorSpecificAttributes HwSubServicePolicy(string value) => CreateString(HuaweiAttributeType.HW_SUB_SERVICE_POLICY, value);

        /// <summary>Creates a Huawei-Domain-Name-V2 attribute (Type 138).</summary>
        public static VendorSpecificAttributes DomainNameV2(string value) => CreateString(HuaweiAttributeType.DOMAIN_NAME_V2, value);

        /// <summary>Creates a Huawei-User-Group-Name attribute (Type 140).</summary>
        public static VendorSpecificAttributes UserGroupName(string value) => CreateString(HuaweiAttributeType.USER_GROUP_NAME, value);

        /// <summary>Creates a Huawei-AVPair attribute (Type 200).</summary>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(HuaweiAttributeType.AVPAIR, value);

        /// <summary>Creates a Huawei-Delegated-IPv6-Prefix-Pool attribute (Type 201).</summary>
        public static VendorSpecificAttributes DelegatedIpv6PrefixPool(string value) => CreateString(HuaweiAttributeType.DELEGATED_IPV6_PREFIX_POOL, value);

        /// <summary>Creates a Huawei-IPv6-Address-Pool attribute (Type 202).</summary>
        public static VendorSpecificAttributes Ipv6AddressPool(string value) => CreateString(HuaweiAttributeType.IPV6_ADDRESS_POOL, value);

        /// <summary>Creates a Huawei-NAT-Policy-Name attribute (Type 203).</summary>
        public static VendorSpecificAttributes NatPolicyName(string value) => CreateString(HuaweiAttributeType.NAT_POLICY_NAME, value);

        /// <summary>Creates a Huawei-NAT-Public-Address attribute (Type 204).</summary>
        public static VendorSpecificAttributes NatPublicAddress(string value) => CreateString(HuaweiAttributeType.NAT_PUBLIC_ADDRESS, value);

        /// <summary>Creates a Huawei-NAT-Port-Forwarding attribute (Type 207).</summary>
        public static VendorSpecificAttributes NatPortForwarding(string value) => CreateString(HuaweiAttributeType.NAT_PORT_FORWARDING, value);

        /// <summary>Creates a Huawei-Product-ID attribute (Type 255).</summary>
        public static VendorSpecificAttributes ProductId(string value) => CreateString(HuaweiAttributeType.PRODUCT_ID, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Huawei-IP-Address attribute (Type 30).</summary>
        public static VendorSpecificAttributes IpAddress(IPAddress value) => CreateIpv4(HuaweiAttributeType.IP_ADDRESS, value);

        /// <summary>Creates a Huawei-Subnet-Mask attribute (Type 72).</summary>
        public static VendorSpecificAttributes SubnetMask(IPAddress value) => CreateIpv4(HuaweiAttributeType.SUBNET_MASK, value);

        /// <summary>Creates a Huawei-Gateway-Address attribute (Type 73).</summary>
        public static VendorSpecificAttributes GatewayAddress(IPAddress value) => CreateIpv4(HuaweiAttributeType.GATEWAY_ADDRESS, value);

        /// <summary>Creates a Huawei-Primary-WINS attribute (Type 75).</summary>
        public static VendorSpecificAttributes PrimaryWins(IPAddress value) => CreateIpv4(HuaweiAttributeType.PRIMARY_WINS, value);

        /// <summary>Creates a Huawei-Secondary-WINS attribute (Type 76).</summary>
        public static VendorSpecificAttributes SecondaryWins(IPAddress value) => CreateIpv4(HuaweiAttributeType.SECONDARY_WINS, value);

        /// <summary>Creates a Huawei-Policy-Route attribute (Type 87).</summary>
        public static VendorSpecificAttributes PolicyRoute(IPAddress value) => CreateIpv4(HuaweiAttributeType.POLICY_ROUTE, value);

        /// <summary>Creates a Huawei-Primary-DNS attribute (Type 135).</summary>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value) => CreateIpv4(HuaweiAttributeType.PRIMARY_DNS, value);

        /// <summary>Creates a Huawei-Secondary-DNS attribute (Type 136).</summary>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value) => CreateIpv4(HuaweiAttributeType.SECONDARY_DNS, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(HuaweiAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(HuaweiAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(HuaweiAttributeType type, IPAddress value)
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