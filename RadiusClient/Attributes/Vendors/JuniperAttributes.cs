using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Juniper Networks (IANA PEN 2636) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.juniper</c>.
    /// </summary>
    /// <remarks>
    /// Juniper Networks uses vendor ID 2636 for its Junos-based routers,
    /// switches, and security appliances (SRX, EX, MX, QFX, PTX).
    /// This is separate from the Juniper ERX / Unisphere (PEN 4874) dictionary.
    /// </remarks>
    public enum JuniperAttributeType : byte
    {
        /// <summary>Juniper-Local-User-Name (Type 1). String. Local user name mapping.</summary>
        LOCAL_USER_NAME = 1,

        /// <summary>Juniper-Allow-Commands (Type 2). String. Allowed CLI command regex.</summary>
        ALLOW_COMMANDS = 2,

        /// <summary>Juniper-Deny-Commands (Type 3). String. Denied CLI command regex.</summary>
        DENY_COMMANDS = 3,

        /// <summary>Juniper-Allow-Configuration (Type 4). String. Allowed configuration hierarchy regex.</summary>
        ALLOW_CONFIGURATION = 4,

        /// <summary>Juniper-Deny-Configuration (Type 5). String. Denied configuration hierarchy regex.</summary>
        DENY_CONFIGURATION = 5,

        /// <summary>Juniper-Interactive-Command (Type 8). String. Interactive command string.</summary>
        INTERACTIVE_COMMAND = 8,

        /// <summary>Juniper-Configuration-Change (Type 9). String. Configuration change information.</summary>
        CONFIGURATION_CHANGE = 9,

        /// <summary>Juniper-User-Permissions (Type 10). String. User permission flags.</summary>
        USER_PERMISSIONS = 10,

        /// <summary>Juniper-Authentication-Type (Type 11). Integer. Authentication type.</summary>
        AUTHENTICATION_TYPE = 11,

        /// <summary>Juniper-Session-Port (Type 12). Integer. Session port number.</summary>
        SESSION_PORT = 12,

        /// <summary>Juniper-Allow-Commands-Regexps (Type 13). String. Allowed commands extended regex.</summary>
        ALLOW_COMMANDS_REGEXPS = 13,

        /// <summary>Juniper-Deny-Commands-Regexps (Type 14). String. Denied commands extended regex.</summary>
        DENY_COMMANDS_REGEXPS = 14,

        /// <summary>Juniper-Allow-Configuration-Regexps (Type 15). String. Allowed configuration extended regex.</summary>
        ALLOW_CONFIGURATION_REGEXPS = 15,

        /// <summary>Juniper-Deny-Configuration-Regexps (Type 16). String. Denied configuration extended regex.</summary>
        DENY_CONFIGURATION_REGEXPS = 16,

        /// <summary>Juniper-Switching-Filter (Type 17). String. Switching filter string.</summary>
        SWITCHING_FILTER = 17,

        /// <summary>Juniper-VoIP-Vlan (Type 18). String. VoIP VLAN assignment.</summary>
        VOIP_VLAN = 18,

        /// <summary>Juniper-CWA-Redirect-URL (Type 19). String. CWA redirect URL.</summary>
        CWA_REDIRECT_URL = 19,

        /// <summary>Juniper-AV-Pair (Type 20). String. Attribute-value pair string.</summary>
        AV_PAIR = 20,

        /// <summary>Juniper-Primary-DNS (Type 21). IP address. Primary DNS server.</summary>
        PRIMARY_DNS = 21,

        /// <summary>Juniper-Secondary-DNS (Type 22). IP address. Secondary DNS server.</summary>
        SECONDARY_DNS = 22,

        /// <summary>Juniper-Primary-WINS (Type 23). IP address. Primary WINS server.</summary>
        PRIMARY_WINS = 23,

        /// <summary>Juniper-Secondary-WINS (Type 24). IP address. Secondary WINS server.</summary>
        SECONDARY_WINS = 24,

        /// <summary>Juniper-Interface-Id (Type 25). String. Interface identifier.</summary>
        INTERFACE_ID = 25,

        /// <summary>Juniper-IP-Pool-Name (Type 26). String. IP address pool name.</summary>
        IP_POOL_NAME = 26,

        /// <summary>Juniper-Keep-Alive (Type 27). Integer. Keep-alive interval in seconds.</summary>
        KEEP_ALIVE = 27,

        /// <summary>Juniper-CoS-Traffic-Control-Profile (Type 28). String. CoS traffic control profile.</summary>
        COS_TRAFFIC_CONTROL_PROFILE = 28,

        /// <summary>Juniper-CoS-Parameter (Type 29). String. CoS parameter string.</summary>
        COS_PARAMETER = 29,

        /// <summary>Juniper-Encapsulation-Overhead (Type 30). Integer. Encapsulation overhead in bytes.</summary>
        ENCAPSULATION_OVERHEAD = 30,

        /// <summary>Juniper-Cell-Overhead (Type 31). Integer. ATM cell overhead.</summary>
        CELL_OVERHEAD = 31,

        /// <summary>Juniper-TX-Connect-Speed (Type 32). Integer. Transmit connect speed in bps.</summary>
        TX_CONNECT_SPEED = 32,

        /// <summary>Juniper-RX-Connect-Speed (Type 33). Integer. Receive connect speed in bps.</summary>
        RX_CONNECT_SPEED = 33,

        /// <summary>Juniper-Firewall-Filter-Name (Type 34). String. Firewall filter name.</summary>
        FIREWALL_FILTER_NAME = 34,

        /// <summary>Juniper-Policer-Parameter (Type 35). String. Policer parameter string.</summary>
        POLICER_PARAMETER = 35,

        /// <summary>Juniper-Client-Ingress-Limit (Type 36). Integer. Client ingress rate limit in Kbps.</summary>
        CLIENT_INGRESS_LIMIT = 36,

        /// <summary>Juniper-Client-Egress-Limit (Type 37). Integer. Client egress rate limit in Kbps.</summary>
        CLIENT_EGRESS_LIMIT = 37,

        /// <summary>Juniper-Cos-Scheduler-Parameter (Type 38). String. CoS scheduler parameter.</summary>
        COS_SCHEDULER_PARAMETER = 38,

        /// <summary>Juniper-DHCP-Options (Type 39). Octets. DHCP options data.</summary>
        DHCP_OPTIONS = 39,

        /// <summary>Juniper-Client-Profile-Name (Type 40). String. Client profile name.</summary>
        CLIENT_PROFILE_NAME = 40,

        /// <summary>Juniper-Physical-Port-Speed (Type 41). String. Physical port speed.</summary>
        PHYSICAL_PORT_SPEED = 41,

        /// <summary>Juniper-Routing-Instance (Type 42). String. Routing instance name.</summary>
        ROUTING_INSTANCE = 42,

        /// <summary>Juniper-Cos-Rule (Type 43). String. CoS rule string.</summary>
        COS_RULE = 43,

        /// <summary>Juniper-Cos-Rule-Revert (Type 44). String. CoS rule revert string.</summary>
        COS_RULE_REVERT = 44,

        /// <summary>Juniper-Acct-Update-48 (Type 48). Integer. Accounting update value.</summary>
        ACCT_UPDATE_48 = 48,

        /// <summary>Juniper-Virtual-Router (Type 49). String. Virtual router name.</summary>
        VIRTUAL_ROUTER = 49,

        /// <summary>Juniper-Ingress-Policy-Name (Type 50). String. Ingress policy name.</summary>
        INGRESS_POLICY_NAME = 50,

        /// <summary>Juniper-Egress-Policy-Name (Type 51). String. Egress policy name.</summary>
        EGRESS_POLICY_NAME = 51,

        /// <summary>Juniper-Ingress-Statistics-Name (Type 52). String. Ingress statistics name.</summary>
        INGRESS_STATISTICS_NAME = 52,

        /// <summary>Juniper-Egress-Statistics-Name (Type 53). String. Egress statistics name.</summary>
        EGRESS_STATISTICS_NAME = 53,

        /// <summary>Juniper-Session-Volume-Quota (Type 54). String. Session volume quota.</summary>
        SESSION_VOLUME_QUOTA = 54,

        /// <summary>Juniper-Acc-Loop-Cir-Id (Type 55). String. Access loop circuit identifier.</summary>
        ACC_LOOP_CIR_ID = 55,

        /// <summary>Juniper-Acc-Aggr-Cir-Id-Bin (Type 56). Octets. Access aggregation circuit ID binary.</summary>
        ACC_AGGR_CIR_ID_BIN = 56,

        /// <summary>Juniper-Acc-Aggr-Cir-Id-Asc (Type 57). String. Access aggregation circuit ID ASCII.</summary>
        ACC_AGGR_CIR_ID_ASC = 57,

        /// <summary>Juniper-Acc-Act-Data-Rate-Up (Type 58). Integer. Actual data rate upstream in Kbps.</summary>
        ACC_ACT_DATA_RATE_UP = 58,

        /// <summary>Juniper-Acc-Act-Data-Rate-Dn (Type 59). Integer. Actual data rate downstream in Kbps.</summary>
        ACC_ACT_DATA_RATE_DN = 59,

        /// <summary>Juniper-Act-Data-Rate-Up (Type 60). Integer. Active data rate upstream in Kbps.</summary>
        ACT_DATA_RATE_UP = 60,

        /// <summary>Juniper-Act-Data-Rate-Dn (Type 61). Integer. Active data rate downstream in Kbps.</summary>
        ACT_DATA_RATE_DN = 61,

        /// <summary>Juniper-Min-Data-Rate-Up (Type 62). Integer. Minimum data rate upstream in Kbps.</summary>
        MIN_DATA_RATE_UP = 62,

        /// <summary>Juniper-Min-Data-Rate-Dn (Type 63). Integer. Minimum data rate downstream in Kbps.</summary>
        MIN_DATA_RATE_DN = 63,

        /// <summary>Juniper-Att-Data-Rate-Up (Type 64). Integer. Attainable data rate upstream in Kbps.</summary>
        ATT_DATA_RATE_UP = 64,

        /// <summary>Juniper-Att-Data-Rate-Dn (Type 65). Integer. Attainable data rate downstream in Kbps.</summary>
        ATT_DATA_RATE_DN = 65,

        /// <summary>Juniper-Max-Data-Rate-Up (Type 66). Integer. Maximum data rate upstream in Kbps.</summary>
        MAX_DATA_RATE_UP = 66,

        /// <summary>Juniper-Max-Data-Rate-Dn (Type 67). Integer. Maximum data rate downstream in Kbps.</summary>
        MAX_DATA_RATE_DN = 67,

        /// <summary>Juniper-Min-LP-Data-Rate-Up (Type 68). Integer. Minimum low-priority data rate upstream.</summary>
        MIN_LP_DATA_RATE_UP = 68,

        /// <summary>Juniper-Min-LP-Data-Rate-Dn (Type 69). Integer. Minimum low-priority data rate downstream.</summary>
        MIN_LP_DATA_RATE_DN = 69,

        /// <summary>Juniper-Max-Interlv-Delay-Up (Type 70). Integer. Maximum interleaving delay upstream in ms.</summary>
        MAX_INTERLV_DELAY_UP = 70,

        /// <summary>Juniper-Act-Interlv-Delay-Up (Type 71). Integer. Actual interleaving delay upstream in ms.</summary>
        ACT_INTERLV_DELAY_UP = 71,

        /// <summary>Juniper-Max-Interlv-Delay-Dn (Type 72). Integer. Maximum interleaving delay downstream in ms.</summary>
        MAX_INTERLV_DELAY_DN = 72,

        /// <summary>Juniper-Act-Interlv-Delay-Dn (Type 73). Integer. Actual interleaving delay downstream in ms.</summary>
        ACT_INTERLV_DELAY_DN = 73,

        /// <summary>Juniper-DSL-Line-State (Type 74). Integer. DSL line state.</summary>
        DSL_LINE_STATE = 74,

        /// <summary>Juniper-DSL-Type (Type 75). Integer. DSL type.</summary>
        DSL_TYPE = 75
    }

    /// <summary>
    /// Juniper-Authentication-Type attribute values (Type 11).
    /// </summary>
    public enum JUNIPER_AUTHENTICATION_TYPE
    {
        /// <summary>RADIUS authentication.</summary>
        RADIUS = 0,

        /// <summary>LDAP authentication.</summary>
        LDAP = 1,

        /// <summary>SecurID authentication.</summary>
        SECURID = 2,

        /// <summary>Local authentication.</summary>
        LOCAL = 3
    }

    /// <summary>
    /// Juniper-DSL-Line-State attribute values (Type 74).
    /// </summary>
    public enum JUNIPER_DSL_LINE_STATE
    {
        /// <summary>Showtime (line up).</summary>
        SHOWTIME = 1,

        /// <summary>Idle (line down).</summary>
        IDLE = 2,

        /// <summary>Silent (no signal).</summary>
        SILENT = 3
    }

    /// <summary>
    /// Juniper-DSL-Type attribute values (Type 75).
    /// </summary>
    public enum JUNIPER_DSL_TYPE
    {
        /// <summary>ADSL1.</summary>
        ADSL1 = 1,

        /// <summary>ADSL2.</summary>
        ADSL2 = 2,

        /// <summary>ADSL2+.</summary>
        ADSL2PLUS = 3,

        /// <summary>VDSL1.</summary>
        VDSL1 = 4,

        /// <summary>VDSL2.</summary>
        VDSL2 = 5,

        /// <summary>SDSL.</summary>
        SDSL = 6,

        /// <summary>Other DSL type.</summary>
        OTHER = 7
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Juniper Networks
    /// (IANA PEN 2636) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.juniper</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Juniper's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2636</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Juniper Networks Junos-based routers, switches,
    /// and security appliances (MX, EX, SRX, QFX, PTX) for RADIUS-based CLI
    /// command and configuration authorization (allow/deny with regex), local
    /// user mapping, user permissions, interactive command logging, firewall
    /// filter and policer assignment, CoS profiles/rules/scheduler parameters,
    /// ingress/egress policy and statistics, client rate limiting, DNS/WINS
    /// provisioning, IP pool selection, routing instance and virtual router
    /// assignment, DSL forum access loop attributes (circuit ID, data rates,
    /// interleaving delays, line state, DSL type), switching filters, VoIP VLAN,
    /// CWA redirect URL, DHCP options, and session volume quotas.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(JuniperAttributes.LocalUserName("admin-class"));
    /// packet.SetAttribute(JuniperAttributes.AllowCommands("show.*|ping.*"));
    /// packet.SetAttribute(JuniperAttributes.DenyCommands("request system.*"));
    /// packet.SetAttribute(JuniperAttributes.FirewallFilterName("subscriber-filter"));
    /// packet.SetAttribute(JuniperAttributes.ClientIngressLimit(100000));
    /// packet.SetAttribute(JuniperAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// </code>
    /// </remarks>
    public static class JuniperAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Juniper Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2636;

        #region Integer Attributes

        /// <summary>Creates a Juniper-Authentication-Type attribute (Type 11).</summary>
        /// <param name="value">The authentication type. See <see cref="JUNIPER_AUTHENTICATION_TYPE"/>.</param>
        public static VendorSpecificAttributes AuthenticationType(JUNIPER_AUTHENTICATION_TYPE value) => CreateInteger(JuniperAttributeType.AUTHENTICATION_TYPE, (int)value);

        /// <summary>Creates a Juniper-Session-Port attribute (Type 12).</summary>
        public static VendorSpecificAttributes SessionPort(int value) => CreateInteger(JuniperAttributeType.SESSION_PORT, value);

        /// <summary>Creates a Juniper-Keep-Alive attribute (Type 27).</summary>
        public static VendorSpecificAttributes KeepAlive(int value) => CreateInteger(JuniperAttributeType.KEEP_ALIVE, value);

        /// <summary>Creates a Juniper-Encapsulation-Overhead attribute (Type 30).</summary>
        public static VendorSpecificAttributes EncapsulationOverhead(int value) => CreateInteger(JuniperAttributeType.ENCAPSULATION_OVERHEAD, value);

        /// <summary>Creates a Juniper-Cell-Overhead attribute (Type 31).</summary>
        public static VendorSpecificAttributes CellOverhead(int value) => CreateInteger(JuniperAttributeType.CELL_OVERHEAD, value);

        /// <summary>Creates a Juniper-TX-Connect-Speed attribute (Type 32).</summary>
        public static VendorSpecificAttributes TxConnectSpeed(int value) => CreateInteger(JuniperAttributeType.TX_CONNECT_SPEED, value);

        /// <summary>Creates a Juniper-RX-Connect-Speed attribute (Type 33).</summary>
        public static VendorSpecificAttributes RxConnectSpeed(int value) => CreateInteger(JuniperAttributeType.RX_CONNECT_SPEED, value);

        /// <summary>Creates a Juniper-Client-Ingress-Limit attribute (Type 36).</summary>
        public static VendorSpecificAttributes ClientIngressLimit(int value) => CreateInteger(JuniperAttributeType.CLIENT_INGRESS_LIMIT, value);

        /// <summary>Creates a Juniper-Client-Egress-Limit attribute (Type 37).</summary>
        public static VendorSpecificAttributes ClientEgressLimit(int value) => CreateInteger(JuniperAttributeType.CLIENT_EGRESS_LIMIT, value);

        /// <summary>Creates a Juniper-Acct-Update-48 attribute (Type 48).</summary>
        public static VendorSpecificAttributes AcctUpdate48(int value) => CreateInteger(JuniperAttributeType.ACCT_UPDATE_48, value);

        /// <summary>Creates a Juniper-Acc-Act-Data-Rate-Up attribute (Type 58).</summary>
        public static VendorSpecificAttributes AccActDataRateUp(int value) => CreateInteger(JuniperAttributeType.ACC_ACT_DATA_RATE_UP, value);

        /// <summary>Creates a Juniper-Acc-Act-Data-Rate-Dn attribute (Type 59).</summary>
        public static VendorSpecificAttributes AccActDataRateDn(int value) => CreateInteger(JuniperAttributeType.ACC_ACT_DATA_RATE_DN, value);

        /// <summary>Creates a Juniper-Act-Data-Rate-Up attribute (Type 60).</summary>
        public static VendorSpecificAttributes ActDataRateUp(int value) => CreateInteger(JuniperAttributeType.ACT_DATA_RATE_UP, value);

        /// <summary>Creates a Juniper-Act-Data-Rate-Dn attribute (Type 61).</summary>
        public static VendorSpecificAttributes ActDataRateDn(int value) => CreateInteger(JuniperAttributeType.ACT_DATA_RATE_DN, value);

        /// <summary>Creates a Juniper-Min-Data-Rate-Up attribute (Type 62).</summary>
        public static VendorSpecificAttributes MinDataRateUp(int value) => CreateInteger(JuniperAttributeType.MIN_DATA_RATE_UP, value);

        /// <summary>Creates a Juniper-Min-Data-Rate-Dn attribute (Type 63).</summary>
        public static VendorSpecificAttributes MinDataRateDn(int value) => CreateInteger(JuniperAttributeType.MIN_DATA_RATE_DN, value);

        /// <summary>Creates a Juniper-Att-Data-Rate-Up attribute (Type 64).</summary>
        public static VendorSpecificAttributes AttDataRateUp(int value) => CreateInteger(JuniperAttributeType.ATT_DATA_RATE_UP, value);

        /// <summary>Creates a Juniper-Att-Data-Rate-Dn attribute (Type 65).</summary>
        public static VendorSpecificAttributes AttDataRateDn(int value) => CreateInteger(JuniperAttributeType.ATT_DATA_RATE_DN, value);

        /// <summary>Creates a Juniper-Max-Data-Rate-Up attribute (Type 66).</summary>
        public static VendorSpecificAttributes MaxDataRateUp(int value) => CreateInteger(JuniperAttributeType.MAX_DATA_RATE_UP, value);

        /// <summary>Creates a Juniper-Max-Data-Rate-Dn attribute (Type 67).</summary>
        public static VendorSpecificAttributes MaxDataRateDn(int value) => CreateInteger(JuniperAttributeType.MAX_DATA_RATE_DN, value);

        /// <summary>Creates a Juniper-Min-LP-Data-Rate-Up attribute (Type 68).</summary>
        public static VendorSpecificAttributes MinLpDataRateUp(int value) => CreateInteger(JuniperAttributeType.MIN_LP_DATA_RATE_UP, value);

        /// <summary>Creates a Juniper-Min-LP-Data-Rate-Dn attribute (Type 69).</summary>
        public static VendorSpecificAttributes MinLpDataRateDn(int value) => CreateInteger(JuniperAttributeType.MIN_LP_DATA_RATE_DN, value);

        /// <summary>Creates a Juniper-Max-Interlv-Delay-Up attribute (Type 70).</summary>
        public static VendorSpecificAttributes MaxInterlvDelayUp(int value) => CreateInteger(JuniperAttributeType.MAX_INTERLV_DELAY_UP, value);

        /// <summary>Creates a Juniper-Act-Interlv-Delay-Up attribute (Type 71).</summary>
        public static VendorSpecificAttributes ActInterlvDelayUp(int value) => CreateInteger(JuniperAttributeType.ACT_INTERLV_DELAY_UP, value);

        /// <summary>Creates a Juniper-Max-Interlv-Delay-Dn attribute (Type 72).</summary>
        public static VendorSpecificAttributes MaxInterlvDelayDn(int value) => CreateInteger(JuniperAttributeType.MAX_INTERLV_DELAY_DN, value);

        /// <summary>Creates a Juniper-Act-Interlv-Delay-Dn attribute (Type 73).</summary>
        public static VendorSpecificAttributes ActInterlvDelayDn(int value) => CreateInteger(JuniperAttributeType.ACT_INTERLV_DELAY_DN, value);

        /// <summary>Creates a Juniper-DSL-Line-State attribute (Type 74).</summary>
        /// <param name="value">The DSL line state. See <see cref="JUNIPER_DSL_LINE_STATE"/>.</param>
        public static VendorSpecificAttributes DslLineState(JUNIPER_DSL_LINE_STATE value) => CreateInteger(JuniperAttributeType.DSL_LINE_STATE, (int)value);

        /// <summary>Creates a Juniper-DSL-Type attribute (Type 75).</summary>
        /// <param name="value">The DSL type. See <see cref="JUNIPER_DSL_TYPE"/>.</param>
        public static VendorSpecificAttributes DslType(JUNIPER_DSL_TYPE value) => CreateInteger(JuniperAttributeType.DSL_TYPE, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Juniper-Local-User-Name attribute (Type 1).</summary>
        public static VendorSpecificAttributes LocalUserName(string value) => CreateString(JuniperAttributeType.LOCAL_USER_NAME, value);

        /// <summary>Creates a Juniper-Allow-Commands attribute (Type 2).</summary>
        public static VendorSpecificAttributes AllowCommands(string value) => CreateString(JuniperAttributeType.ALLOW_COMMANDS, value);

        /// <summary>Creates a Juniper-Deny-Commands attribute (Type 3).</summary>
        public static VendorSpecificAttributes DenyCommands(string value) => CreateString(JuniperAttributeType.DENY_COMMANDS, value);

        /// <summary>Creates a Juniper-Allow-Configuration attribute (Type 4).</summary>
        public static VendorSpecificAttributes AllowConfiguration(string value) => CreateString(JuniperAttributeType.ALLOW_CONFIGURATION, value);

        /// <summary>Creates a Juniper-Deny-Configuration attribute (Type 5).</summary>
        public static VendorSpecificAttributes DenyConfiguration(string value) => CreateString(JuniperAttributeType.DENY_CONFIGURATION, value);

        /// <summary>Creates a Juniper-Interactive-Command attribute (Type 8).</summary>
        public static VendorSpecificAttributes InteractiveCommand(string value) => CreateString(JuniperAttributeType.INTERACTIVE_COMMAND, value);

        /// <summary>Creates a Juniper-Configuration-Change attribute (Type 9).</summary>
        public static VendorSpecificAttributes ConfigurationChange(string value) => CreateString(JuniperAttributeType.CONFIGURATION_CHANGE, value);

        /// <summary>Creates a Juniper-User-Permissions attribute (Type 10).</summary>
        public static VendorSpecificAttributes UserPermissions(string value) => CreateString(JuniperAttributeType.USER_PERMISSIONS, value);

        /// <summary>Creates a Juniper-Allow-Commands-Regexps attribute (Type 13).</summary>
        public static VendorSpecificAttributes AllowCommandsRegexps(string value) => CreateString(JuniperAttributeType.ALLOW_COMMANDS_REGEXPS, value);

        /// <summary>Creates a Juniper-Deny-Commands-Regexps attribute (Type 14).</summary>
        public static VendorSpecificAttributes DenyCommandsRegexps(string value) => CreateString(JuniperAttributeType.DENY_COMMANDS_REGEXPS, value);

        /// <summary>Creates a Juniper-Allow-Configuration-Regexps attribute (Type 15).</summary>
        public static VendorSpecificAttributes AllowConfigurationRegexps(string value) => CreateString(JuniperAttributeType.ALLOW_CONFIGURATION_REGEXPS, value);

        /// <summary>Creates a Juniper-Deny-Configuration-Regexps attribute (Type 16).</summary>
        public static VendorSpecificAttributes DenyConfigurationRegexps(string value) => CreateString(JuniperAttributeType.DENY_CONFIGURATION_REGEXPS, value);

        /// <summary>Creates a Juniper-Switching-Filter attribute (Type 17).</summary>
        public static VendorSpecificAttributes SwitchingFilter(string value) => CreateString(JuniperAttributeType.SWITCHING_FILTER, value);

        /// <summary>Creates a Juniper-VoIP-Vlan attribute (Type 18).</summary>
        public static VendorSpecificAttributes VoipVlan(string value) => CreateString(JuniperAttributeType.VOIP_VLAN, value);

        /// <summary>Creates a Juniper-CWA-Redirect-URL attribute (Type 19).</summary>
        public static VendorSpecificAttributes CwaRedirectUrl(string value) => CreateString(JuniperAttributeType.CWA_REDIRECT_URL, value);

        /// <summary>Creates a Juniper-AV-Pair attribute (Type 20).</summary>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(JuniperAttributeType.AV_PAIR, value);

        /// <summary>Creates a Juniper-Interface-Id attribute (Type 25).</summary>
        public static VendorSpecificAttributes InterfaceId(string value) => CreateString(JuniperAttributeType.INTERFACE_ID, value);

        /// <summary>Creates a Juniper-IP-Pool-Name attribute (Type 26).</summary>
        public static VendorSpecificAttributes IpPoolName(string value) => CreateString(JuniperAttributeType.IP_POOL_NAME, value);

        /// <summary>Creates a Juniper-CoS-Traffic-Control-Profile attribute (Type 28).</summary>
        public static VendorSpecificAttributes CosTrafficControlProfile(string value) => CreateString(JuniperAttributeType.COS_TRAFFIC_CONTROL_PROFILE, value);

        /// <summary>Creates a Juniper-CoS-Parameter attribute (Type 29).</summary>
        public static VendorSpecificAttributes CosParameter(string value) => CreateString(JuniperAttributeType.COS_PARAMETER, value);

        /// <summary>Creates a Juniper-Firewall-Filter-Name attribute (Type 34).</summary>
        public static VendorSpecificAttributes FirewallFilterName(string value) => CreateString(JuniperAttributeType.FIREWALL_FILTER_NAME, value);

        /// <summary>Creates a Juniper-Policer-Parameter attribute (Type 35).</summary>
        public static VendorSpecificAttributes PolicerParameter(string value) => CreateString(JuniperAttributeType.POLICER_PARAMETER, value);

        /// <summary>Creates a Juniper-Cos-Scheduler-Parameter attribute (Type 38).</summary>
        public static VendorSpecificAttributes CosSchedulerParameter(string value) => CreateString(JuniperAttributeType.COS_SCHEDULER_PARAMETER, value);

        /// <summary>Creates a Juniper-Client-Profile-Name attribute (Type 40).</summary>
        public static VendorSpecificAttributes ClientProfileName(string value) => CreateString(JuniperAttributeType.CLIENT_PROFILE_NAME, value);

        /// <summary>Creates a Juniper-Physical-Port-Speed attribute (Type 41).</summary>
        public static VendorSpecificAttributes PhysicalPortSpeed(string value) => CreateString(JuniperAttributeType.PHYSICAL_PORT_SPEED, value);

        /// <summary>Creates a Juniper-Routing-Instance attribute (Type 42).</summary>
        public static VendorSpecificAttributes RoutingInstance(string value) => CreateString(JuniperAttributeType.ROUTING_INSTANCE, value);

        /// <summary>Creates a Juniper-Cos-Rule attribute (Type 43).</summary>
        public static VendorSpecificAttributes CosRule(string value) => CreateString(JuniperAttributeType.COS_RULE, value);

        /// <summary>Creates a Juniper-Cos-Rule-Revert attribute (Type 44).</summary>
        public static VendorSpecificAttributes CosRuleRevert(string value) => CreateString(JuniperAttributeType.COS_RULE_REVERT, value);

        /// <summary>Creates a Juniper-Virtual-Router attribute (Type 49).</summary>
        public static VendorSpecificAttributes VirtualRouter(string value) => CreateString(JuniperAttributeType.VIRTUAL_ROUTER, value);

        /// <summary>Creates a Juniper-Ingress-Policy-Name attribute (Type 50).</summary>
        public static VendorSpecificAttributes IngressPolicyName(string value) => CreateString(JuniperAttributeType.INGRESS_POLICY_NAME, value);

        /// <summary>Creates a Juniper-Egress-Policy-Name attribute (Type 51).</summary>
        public static VendorSpecificAttributes EgressPolicyName(string value) => CreateString(JuniperAttributeType.EGRESS_POLICY_NAME, value);

        /// <summary>Creates a Juniper-Ingress-Statistics-Name attribute (Type 52).</summary>
        public static VendorSpecificAttributes IngressStatisticsName(string value) => CreateString(JuniperAttributeType.INGRESS_STATISTICS_NAME, value);

        /// <summary>Creates a Juniper-Egress-Statistics-Name attribute (Type 53).</summary>
        public static VendorSpecificAttributes EgressStatisticsName(string value) => CreateString(JuniperAttributeType.EGRESS_STATISTICS_NAME, value);

        /// <summary>Creates a Juniper-Session-Volume-Quota attribute (Type 54).</summary>
        public static VendorSpecificAttributes SessionVolumeQuota(string value) => CreateString(JuniperAttributeType.SESSION_VOLUME_QUOTA, value);

        /// <summary>Creates a Juniper-Acc-Loop-Cir-Id attribute (Type 55).</summary>
        public static VendorSpecificAttributes AccLoopCirId(string value) => CreateString(JuniperAttributeType.ACC_LOOP_CIR_ID, value);

        /// <summary>Creates a Juniper-Acc-Aggr-Cir-Id-Asc attribute (Type 57).</summary>
        public static VendorSpecificAttributes AccAggrCirIdAsc(string value) => CreateString(JuniperAttributeType.ACC_AGGR_CIR_ID_ASC, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Juniper-Primary-DNS attribute (Type 21).</summary>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value) => CreateIpv4(JuniperAttributeType.PRIMARY_DNS, value);

        /// <summary>Creates a Juniper-Secondary-DNS attribute (Type 22).</summary>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value) => CreateIpv4(JuniperAttributeType.SECONDARY_DNS, value);

        /// <summary>Creates a Juniper-Primary-WINS attribute (Type 23).</summary>
        public static VendorSpecificAttributes PrimaryWins(IPAddress value) => CreateIpv4(JuniperAttributeType.PRIMARY_WINS, value);

        /// <summary>Creates a Juniper-Secondary-WINS attribute (Type 24).</summary>
        public static VendorSpecificAttributes SecondaryWins(IPAddress value) => CreateIpv4(JuniperAttributeType.SECONDARY_WINS, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates a Juniper-DHCP-Options attribute (Type 39).</summary>
        /// <param name="value">The DHCP options data. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes DhcpOptions(byte[] value) => CreateOctets(JuniperAttributeType.DHCP_OPTIONS, value);

        /// <summary>Creates a Juniper-Acc-Aggr-Cir-Id-Bin attribute (Type 56).</summary>
        /// <param name="value">The access aggregation circuit ID binary data. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AccAggrCirIdBin(byte[] value) => CreateOctets(JuniperAttributeType.ACC_AGGR_CIR_ID_BIN, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(JuniperAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(JuniperAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(JuniperAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        private static VendorSpecificAttributes CreateIpv4(JuniperAttributeType type, IPAddress value)
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