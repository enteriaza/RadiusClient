using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Lucent Technologies / Ascend (IANA PEN 4846) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.lucent</c>.
    /// </summary>
    /// <remarks>
    /// Lucent Technologies (formerly part of AT&amp;T, later merged with Alcatel to
    /// form Alcatel-Lucent, now Nokia) produced the MAX TNT, Pipeline, and APX
    /// remote access and broadband access concentrator platforms. Many of these
    /// attributes overlap with legacy Ascend attributes under PEN 529.
    /// </remarks>
    public enum LucentAttributeType : byte
    {
        /// <summary>Lucent-Max-Shared-Users (Type 2). Integer. Maximum shared users per connection.</summary>
        MAX_SHARED_USERS = 2,

        /// <summary>Lucent-IP-DSCP (Type 3). Integer. IP DSCP value.</summary>
        IP_DSCP = 3,

        /// <summary>Lucent-Xmit-Rate (Type 5). Integer. Transmit rate in bps.</summary>
        XMIT_RATE = 5,

        /// <summary>Lucent-Dialout-Allowed (Type 6). Integer. Dialout allowed flag.</summary>
        DIALOUT_ALLOWED = 6,

        /// <summary>Lucent-Client-Gateway (Type 7). IP address. Client gateway address.</summary>
        CLIENT_GATEWAY = 7,

        /// <summary>Lucent-Client-Primary-DNS (Type 8). IP address. Client primary DNS server.</summary>
        CLIENT_PRIMARY_DNS = 8,

        /// <summary>Lucent-Client-Secondary-DNS (Type 9). IP address. Client secondary DNS server.</summary>
        CLIENT_SECONDARY_DNS = 9,

        /// <summary>Lucent-Multicast-Client (Type 11). Integer. Multicast client flag.</summary>
        MULTICAST_CLIENT = 11,

        /// <summary>Lucent-FR-Circuit-Name (Type 14). String. Frame relay circuit name.</summary>
        FR_CIRCUIT_NAME = 14,

        /// <summary>Lucent-FR-LinkUp (Type 15). Integer. Frame relay link up status.</summary>
        FR_LINKUP = 15,

        /// <summary>Lucent-FR-Nailed-Grp (Type 16). Integer. Frame relay nailed group.</summary>
        FR_NAILED_GRP = 16,

        /// <summary>Lucent-FR-Type (Type 17). Integer. Frame relay type.</summary>
        FR_TYPE = 17,

        /// <summary>Lucent-FR-Link-Mgt (Type 18). Integer. Frame relay link management type.</summary>
        FR_LINK_MGT = 18,

        /// <summary>Lucent-FR-N391 (Type 19). Integer. Frame relay N391 counter.</summary>
        FR_N391 = 19,

        /// <summary>Lucent-FR-DCE-N392 (Type 20). Integer. Frame relay DCE N392 counter.</summary>
        FR_DCE_N392 = 20,

        /// <summary>Lucent-FR-DTE-N392 (Type 21). Integer. Frame relay DTE N392 counter.</summary>
        FR_DTE_N392 = 21,

        /// <summary>Lucent-FR-DCE-N393 (Type 22). Integer. Frame relay DCE N393 counter.</summary>
        FR_DCE_N393 = 22,

        /// <summary>Lucent-FR-DTE-N393 (Type 23). Integer. Frame relay DTE N393 counter.</summary>
        FR_DTE_N393 = 23,

        /// <summary>Lucent-FR-T391 (Type 24). Integer. Frame relay T391 timer.</summary>
        FR_T391 = 24,

        /// <summary>Lucent-FR-T392 (Type 25). Integer. Frame relay T392 timer.</summary>
        FR_T392 = 25,

        /// <summary>Lucent-Bridge-Address (Type 26). String. Bridge address.</summary>
        BRIDGE_ADDRESS = 26,

        /// <summary>Lucent-TS-Idle-Limit (Type 27). Integer. Idle timeout limit in seconds.</summary>
        TS_IDLE_LIMIT = 27,

        /// <summary>Lucent-TS-Idle-Mode (Type 28). Integer. Idle timeout mode.</summary>
        TS_IDLE_MODE = 28,

        /// <summary>Lucent-DBA-Monitor (Type 29). Integer. DBA monitor flag.</summary>
        DBA_MONITOR = 29,

        /// <summary>Lucent-Base-Channel-Count (Type 30). Integer. Base channel count.</summary>
        BASE_CHANNEL_COUNT = 30,

        /// <summary>Lucent-Minimum-Channels (Type 31). Integer. Minimum channels.</summary>
        MINIMUM_CHANNELS = 31,

        /// <summary>Lucent-PPP-Address (Type 33). IP address. PPP address.</summary>
        PPP_ADDRESS = 33,

        /// <summary>Lucent-MPP-Idle-Percent (Type 34). Integer. MPP idle percent threshold.</summary>
        MPP_IDLE_PERCENT = 34,

        /// <summary>Lucent-Xmit-Idle-Percent (Type 35). Integer. Transmit idle percent.</summary>
        XMIT_IDLE_PERCENT = 35,

        /// <summary>Lucent-Pre-Input-Octets (Type 39). Integer. Pre-input octets.</summary>
        PRE_INPUT_OCTETS = 39,

        /// <summary>Lucent-Pre-Output-Octets (Type 40). Integer. Pre-output octets.</summary>
        PRE_OUTPUT_OCTETS = 40,

        /// <summary>Lucent-Pre-Input-Packets (Type 41). Integer. Pre-input packets.</summary>
        PRE_INPUT_PACKETS = 41,

        /// <summary>Lucent-Pre-Output-Packets (Type 42). Integer. Pre-output packets.</summary>
        PRE_OUTPUT_PACKETS = 42,

        /// <summary>Lucent-Maximum-Time (Type 43). Integer. Maximum session time in seconds.</summary>
        MAXIMUM_TIME = 43,

        /// <summary>Lucent-Disconnect-Cause (Type 45). Integer. Disconnect cause code.</summary>
        DISCONNECT_CAUSE = 45,

        /// <summary>Lucent-Connect-Progress (Type 46). Integer. Connect progress code.</summary>
        CONNECT_PROGRESS = 46,

        /// <summary>Lucent-Data-Rate (Type 47). Integer. Data rate in bps.</summary>
        DATA_RATE = 47,

        /// <summary>Lucent-PreSession-Time (Type 48). Integer. Pre-session time in seconds.</summary>
        PRESESSION_TIME = 48,

        /// <summary>Lucent-Assign-IP-Pool (Type 49). Integer. Assign IP pool number.</summary>
        ASSIGN_IP_POOL = 49,

        /// <summary>Lucent-Maximum-Channels (Type 51). Integer. Maximum channels.</summary>
        MAXIMUM_CHANNELS = 51,

        /// <summary>Lucent-Data-Filter (Type 52). String. Data filter rule.</summary>
        DATA_FILTER = 52,

        /// <summary>Lucent-Call-Filter (Type 53). String. Call filter rule.</summary>
        CALL_FILTER = 53,

        /// <summary>Lucent-Idle-Limit (Type 54). Integer. Idle limit in seconds.</summary>
        IDLE_LIMIT = 54,

        /// <summary>Lucent-Preempt-Limit (Type 55). Integer. Preempt limit.</summary>
        PREEMPT_LIMIT = 55,

        /// <summary>Lucent-Callback (Type 56). Integer. Callback mode.</summary>
        CALLBACK = 56,

        /// <summary>Lucent-Data-Svc (Type 57). Integer. Data service type.</summary>
        DATA_SVC = 57,

        /// <summary>Lucent-Force-56 (Type 58). Integer. Force 56K flag.</summary>
        FORCE_56 = 58,

        /// <summary>Lucent-Billing-Number (Type 59). String. Billing phone number.</summary>
        BILLING_NUMBER = 59,

        /// <summary>Lucent-Call-By-Call (Type 60). Integer. Call-by-call flag.</summary>
        CALL_BY_CALL = 60,

        /// <summary>Lucent-Transit-Number (Type 61). String. Transit number.</summary>
        TRANSIT_NUMBER = 61,

        /// <summary>Lucent-Host-Info (Type 62). String. Host information string.</summary>
        HOST_INFO = 62,

        /// <summary>Lucent-PPP-VJ-1172 (Type 63). Integer. PPP VJ 1172 compression flag.</summary>
        PPP_VJ_1172 = 63,

        /// <summary>Lucent-PPP-VJ-Slot-Comp (Type 64). Integer. PPP VJ slot compression flag.</summary>
        PPP_VJ_SLOT_COMP = 64,

        /// <summary>Lucent-PPP-Async-Map (Type 65). Integer. PPP async control character map.</summary>
        PPP_ASYNC_MAP = 65,

        /// <summary>Lucent-Third-Prompt (Type 66). String. Third prompt string.</summary>
        THIRD_PROMPT = 66,

        /// <summary>Lucent-Send-Secret (Type 67). String. Send secret (password).</summary>
        SEND_SECRET = 67,

        /// <summary>Lucent-Receive-Secret (Type 68). String. Receive secret (password).</summary>
        RECEIVE_SECRET = 68,

        /// <summary>Lucent-IPX-Peer-Mode (Type 69). Integer. IPX peer mode.</summary>
        IPX_PEER_MODE = 69,

        /// <summary>Lucent-IP-Pool-Definition (Type 70). String. IP pool definition.</summary>
        IP_POOL_DEFINITION = 70,

        /// <summary>Lucent-Assign-IP-Client (Type 71). IP address. Assigned client IP address.</summary>
        ASSIGN_IP_CLIENT = 71,

        /// <summary>Lucent-Assign-IP-Server (Type 72). IP address. Assigned server IP address.</summary>
        ASSIGN_IP_SERVER = 72,

        /// <summary>Lucent-Assign-IP-Global-Pool (Type 73). String. Assigned global IP pool name.</summary>
        ASSIGN_IP_GLOBAL_POOL = 73,

        /// <summary>Lucent-Client-Assign-DNS (Type 81). Integer. Client assign DNS flag.</summary>
        CLIENT_ASSIGN_DNS = 81,

        /// <summary>Lucent-Multicast-Rate-Limit (Type 84). Integer. Multicast rate limit in bps.</summary>
        MULTICAST_RATE_LIMIT = 84,

        /// <summary>Lucent-IF-Netmask (Type 85). IP address. Interface netmask.</summary>
        IF_NETMASK = 85,

        /// <summary>Lucent-Remote-Addr (Type 86). IP address. Remote address.</summary>
        REMOTE_ADDR = 86,

        /// <summary>Lucent-Multicast-GLeave-Delay (Type 87). Integer. Multicast group leave delay.</summary>
        MULTICAST_GLEAVE_DELAY = 87,

        /// <summary>Lucent-Num-In-Multilink (Type 90). Integer. Number in multilink bundle.</summary>
        NUM_IN_MULTILINK = 90,

        /// <summary>Lucent-First-Dest (Type 93). IP address. First destination IP.</summary>
        FIRST_DEST = 93,

        /// <summary>Lucent-Pre-Input-Octets-64 (Type 94). Integer. Pre-input octets (64-bit high).</summary>
        PRE_INPUT_OCTETS_64 = 94,

        /// <summary>Lucent-Pre-Output-Octets-64 (Type 95). Integer. Pre-output octets (64-bit high).</summary>
        PRE_OUTPUT_OCTETS_64 = 95,

        /// <summary>Lucent-Client-Primary-WINS (Type 135). IP address. Client primary WINS server.</summary>
        CLIENT_PRIMARY_WINS = 135,

        /// <summary>Lucent-Client-Secondary-WINS (Type 136). IP address. Client secondary WINS server.</summary>
        CLIENT_SECONDARY_WINS = 136,

        /// <summary>Lucent-Multicast-Client-Max-Groups (Type 141). Integer. Max multicast groups.</summary>
        MULTICAST_CLIENT_MAX_GROUPS = 141,

        /// <summary>Lucent-Menu-Selector (Type 150). String. Menu selector string.</summary>
        MENU_SELECTOR = 150,

        /// <summary>Lucent-Menu-Item (Type 151). String. Menu item string.</summary>
        MENU_ITEM = 151,

        /// <summary>Lucent-Third-Prompt-Format (Type 176). Integer. Third prompt format.</summary>
        THIRD_PROMPT_FORMAT = 176,

        /// <summary>Lucent-Send-Name (Type 177). String. Send name string.</summary>
        SEND_NAME = 177,

        /// <summary>Lucent-Recv-Name (Type 178). String. Receive name string.</summary>
        RECV_NAME = 178,

        /// <summary>Lucent-User-Acct-Type (Type 180). Integer. User accounting type.</summary>
        USER_ACCT_TYPE = 180,

        /// <summary>Lucent-User-Acct-Host (Type 181). IP address. User accounting host.</summary>
        USER_ACCT_HOST = 181,

        /// <summary>Lucent-User-Acct-Port (Type 182). Integer. User accounting port.</summary>
        USER_ACCT_PORT = 182,

        /// <summary>Lucent-User-Acct-Key (Type 183). String. User accounting key.</summary>
        USER_ACCT_KEY = 183,

        /// <summary>Lucent-User-Acct-Base (Type 184). Integer. User accounting base.</summary>
        USER_ACCT_BASE = 184,

        /// <summary>Lucent-User-Acct-Time (Type 185). Integer. User accounting time.</summary>
        USER_ACCT_TIME = 185,

        /// <summary>Lucent-Assign-IP-Pool-Name (Type 218). String. Assign IP pool name.</summary>
        ASSIGN_IP_POOL_NAME = 218,

        /// <summary>Lucent-Numbering-Plan-ID (Type 219). Integer. Numbering plan identifier.</summary>
        NUMBERING_PLAN_ID = 219,

        /// <summary>Lucent-FR-SVC-Addr (Type 220). String. Frame relay SVC address.</summary>
        FR_SVC_ADDR = 220,

        /// <summary>Lucent-ATM-Fault-Management (Type 227). Integer. ATM fault management type.</summary>
        ATM_FAULT_MANAGEMENT = 227,

        /// <summary>Lucent-ATM-Loopback-Cell-Loss (Type 228). Integer. ATM loopback cell loss count.</summary>
        ATM_LOOPBACK_CELL_LOSS = 228,

        /// <summary>Lucent-ATM-Vci (Type 230). Integer. ATM Virtual Channel Identifier.</summary>
        ATM_VCI = 230,

        /// <summary>Lucent-ATM-Vpi (Type 231). Integer. ATM Virtual Path Identifier.</summary>
        ATM_VPI = 231,

        /// <summary>Lucent-Source-IP-Check (Type 234). Integer. Source IP check flag.</summary>
        SOURCE_IP_CHECK = 234
    }

    /// <summary>
    /// Lucent-Dialout-Allowed attribute values (Type 6).
    /// </summary>
    public enum LUCENT_DIALOUT_ALLOWED
    {
        /// <summary>Dialout not allowed.</summary>
        NOT_ALLOWED = 0,

        /// <summary>Dialout allowed.</summary>
        ALLOWED = 1
    }

    /// <summary>
    /// Lucent-Multicast-Client attribute values (Type 11).
    /// </summary>
    public enum LUCENT_MULTICAST_CLIENT
    {
        /// <summary>Multicast client disabled.</summary>
        DISABLED = 0,

        /// <summary>Multicast client enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Lucent-FR-Type attribute values (Type 17).
    /// </summary>
    public enum LUCENT_FR_TYPE
    {
        /// <summary>Frame relay DTE.</summary>
        DTE = 0,

        /// <summary>Frame relay DCE.</summary>
        DCE = 1,

        /// <summary>Frame relay NNI.</summary>
        NNI = 2
    }

    /// <summary>
    /// Lucent-FR-Link-Mgt attribute values (Type 18).
    /// </summary>
    public enum LUCENT_FR_LINK_MGT
    {
        /// <summary>No link management.</summary>
        NO_LMI = 0,

        /// <summary>Autosense LMI.</summary>
        AUTOSENSE = 1,

        /// <summary>ANSI T1.617 Annex D.</summary>
        ANSI = 2,

        /// <summary>ITU Q.933 Annex A.</summary>
        Q933A = 3,

        /// <summary>Original (Group of Four) LMI.</summary>
        LMI = 4
    }

    /// <summary>
    /// Lucent-Callback attribute values (Type 56).
    /// </summary>
    public enum LUCENT_CALLBACK
    {
        /// <summary>No callback.</summary>
        NONE = 0,

        /// <summary>Callback using CBCP.</summary>
        CBCP = 1,

        /// <summary>Callback using calling station ID.</summary>
        CALLERID = 2
    }

    /// <summary>
    /// Lucent-Data-Svc attribute values (Type 57).
    /// </summary>
    public enum LUCENT_DATA_SVC
    {
        /// <summary>Switched voice.</summary>
        SWITCHED_VOICE = 0,

        /// <summary>Nailed 56K.</summary>
        NAILED_56 = 1,

        /// <summary>Nailed 64K.</summary>
        NAILED_64 = 2,

        /// <summary>Switched 56K.</summary>
        SWITCHED_56 = 3,

        /// <summary>Switched 64K.</summary>
        SWITCHED_64 = 4,

        /// <summary>Switched 384K.</summary>
        SWITCHED_384 = 5,

        /// <summary>Switched 1536K.</summary>
        SWITCHED_1536 = 6,

        /// <summary>Switched 128K.</summary>
        SWITCHED_128 = 7
    }

    /// <summary>
    /// Lucent-Force-56 attribute values (Type 58).
    /// </summary>
    public enum LUCENT_FORCE_56
    {
        /// <summary>Force 56K disabled.</summary>
        DISABLED = 0,

        /// <summary>Force 56K enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Lucent-Source-IP-Check attribute values (Type 234).
    /// </summary>
    public enum LUCENT_SOURCE_IP_CHECK
    {
        /// <summary>Source IP check disabled.</summary>
        DISABLED = 0,

        /// <summary>Source IP check enabled.</summary>
        ENABLED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Lucent Technologies /
    /// Ascend (IANA PEN 4846) Vendor-Specific Attributes (VSAs), as defined in the
    /// FreeRADIUS <c>dictionary.lucent</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Lucent's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 4846</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Lucent Technologies (Ascend) MAX TNT, Pipeline,
    /// and APX broadband access concentrators for RADIUS-based session management
    /// including IP pool and address assignment, DNS/WINS provisioning, frame relay
    /// circuit configuration, ATM VPI/VCI and fault management, data/call filters,
    /// bandwidth management (transmit rate, channel counts, DBA), PPP configuration,
    /// multicast control, callback settings, idle timeout management, modem diagnostics,
    /// pre-session accounting, billing number, host info, user accounting, source IP
    /// checking, menu systems, and ISDN data service type selection.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(LucentAttributes.AssignIpPoolName("subscriber-pool"));
    /// packet.SetAttribute(LucentAttributes.ClientPrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(LucentAttributes.ClientSecondaryDns(IPAddress.Parse("8.8.4.4")));
    /// packet.SetAttribute(LucentAttributes.DataRate(100000000));
    /// packet.SetAttribute(LucentAttributes.IdleLimit(300));
    /// </code>
    /// </remarks>
    public static class LucentAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Lucent Technologies.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 4846;

        #region Integer Attributes

        /// <summary>Creates a Lucent-Max-Shared-Users attribute (Type 2).</summary>
        public static VendorSpecificAttributes MaxSharedUsers(int value) => CreateInteger(LucentAttributeType.MAX_SHARED_USERS, value);

        /// <summary>Creates a Lucent-IP-DSCP attribute (Type 3).</summary>
        public static VendorSpecificAttributes IpDscp(int value) => CreateInteger(LucentAttributeType.IP_DSCP, value);

        /// <summary>Creates a Lucent-Xmit-Rate attribute (Type 5).</summary>
        public static VendorSpecificAttributes XmitRate(int value) => CreateInteger(LucentAttributeType.XMIT_RATE, value);

        /// <summary>Creates a Lucent-Dialout-Allowed attribute (Type 6).</summary>
        public static VendorSpecificAttributes DialoutAllowed(LUCENT_DIALOUT_ALLOWED value) => CreateInteger(LucentAttributeType.DIALOUT_ALLOWED, (int)value);

        /// <summary>Creates a Lucent-Multicast-Client attribute (Type 11).</summary>
        public static VendorSpecificAttributes MulticastClient(LUCENT_MULTICAST_CLIENT value) => CreateInteger(LucentAttributeType.MULTICAST_CLIENT, (int)value);

        /// <summary>Creates a Lucent-FR-LinkUp attribute (Type 15).</summary>
        public static VendorSpecificAttributes FrLinkUp(int value) => CreateInteger(LucentAttributeType.FR_LINKUP, value);

        /// <summary>Creates a Lucent-FR-Nailed-Grp attribute (Type 16).</summary>
        public static VendorSpecificAttributes FrNailedGrp(int value) => CreateInteger(LucentAttributeType.FR_NAILED_GRP, value);

        /// <summary>Creates a Lucent-FR-Type attribute (Type 17).</summary>
        public static VendorSpecificAttributes FrType(LUCENT_FR_TYPE value) => CreateInteger(LucentAttributeType.FR_TYPE, (int)value);

        /// <summary>Creates a Lucent-FR-Link-Mgt attribute (Type 18).</summary>
        public static VendorSpecificAttributes FrLinkMgt(LUCENT_FR_LINK_MGT value) => CreateInteger(LucentAttributeType.FR_LINK_MGT, (int)value);

        /// <summary>Creates a Lucent-FR-N391 attribute (Type 19).</summary>
        public static VendorSpecificAttributes FrN391(int value) => CreateInteger(LucentAttributeType.FR_N391, value);

        /// <summary>Creates a Lucent-FR-DCE-N392 attribute (Type 20).</summary>
        public static VendorSpecificAttributes FrDceN392(int value) => CreateInteger(LucentAttributeType.FR_DCE_N392, value);

        /// <summary>Creates a Lucent-FR-DTE-N392 attribute (Type 21).</summary>
        public static VendorSpecificAttributes FrDteN392(int value) => CreateInteger(LucentAttributeType.FR_DTE_N392, value);

        /// <summary>Creates a Lucent-FR-DCE-N393 attribute (Type 22).</summary>
        public static VendorSpecificAttributes FrDceN393(int value) => CreateInteger(LucentAttributeType.FR_DCE_N393, value);

        /// <summary>Creates a Lucent-FR-DTE-N393 attribute (Type 23).</summary>
        public static VendorSpecificAttributes FrDteN393(int value) => CreateInteger(LucentAttributeType.FR_DTE_N393, value);

        /// <summary>Creates a Lucent-FR-T391 attribute (Type 24).</summary>
        public static VendorSpecificAttributes FrT391(int value) => CreateInteger(LucentAttributeType.FR_T391, value);

        /// <summary>Creates a Lucent-FR-T392 attribute (Type 25).</summary>
        public static VendorSpecificAttributes FrT392(int value) => CreateInteger(LucentAttributeType.FR_T392, value);

        /// <summary>Creates a Lucent-TS-Idle-Limit attribute (Type 27).</summary>
        public static VendorSpecificAttributes TsIdleLimit(int value) => CreateInteger(LucentAttributeType.TS_IDLE_LIMIT, value);

        /// <summary>Creates a Lucent-TS-Idle-Mode attribute (Type 28).</summary>
        public static VendorSpecificAttributes TsIdleMode(int value) => CreateInteger(LucentAttributeType.TS_IDLE_MODE, value);

        /// <summary>Creates a Lucent-DBA-Monitor attribute (Type 29).</summary>
        public static VendorSpecificAttributes DbaMonitor(int value) => CreateInteger(LucentAttributeType.DBA_MONITOR, value);

        /// <summary>Creates a Lucent-Base-Channel-Count attribute (Type 30).</summary>
        public static VendorSpecificAttributes BaseChannelCount(int value) => CreateInteger(LucentAttributeType.BASE_CHANNEL_COUNT, value);

        /// <summary>Creates a Lucent-Minimum-Channels attribute (Type 31).</summary>
        public static VendorSpecificAttributes MinimumChannels(int value) => CreateInteger(LucentAttributeType.MINIMUM_CHANNELS, value);

        /// <summary>Creates a Lucent-MPP-Idle-Percent attribute (Type 34).</summary>
        public static VendorSpecificAttributes MppIdlePercent(int value) => CreateInteger(LucentAttributeType.MPP_IDLE_PERCENT, value);

        /// <summary>Creates a Lucent-Xmit-Idle-Percent attribute (Type 35).</summary>
        public static VendorSpecificAttributes XmitIdlePercent(int value) => CreateInteger(LucentAttributeType.XMIT_IDLE_PERCENT, value);

        /// <summary>Creates a Lucent-Pre-Input-Octets attribute (Type 39).</summary>
        public static VendorSpecificAttributes PreInputOctets(int value) => CreateInteger(LucentAttributeType.PRE_INPUT_OCTETS, value);

        /// <summary>Creates a Lucent-Pre-Output-Octets attribute (Type 40).</summary>
        public static VendorSpecificAttributes PreOutputOctets(int value) => CreateInteger(LucentAttributeType.PRE_OUTPUT_OCTETS, value);

        /// <summary>Creates a Lucent-Pre-Input-Packets attribute (Type 41).</summary>
        public static VendorSpecificAttributes PreInputPackets(int value) => CreateInteger(LucentAttributeType.PRE_INPUT_PACKETS, value);

        /// <summary>Creates a Lucent-Pre-Output-Packets attribute (Type 42).</summary>
        public static VendorSpecificAttributes PreOutputPackets(int value) => CreateInteger(LucentAttributeType.PRE_OUTPUT_PACKETS, value);

        /// <summary>Creates a Lucent-Maximum-Time attribute (Type 43).</summary>
        public static VendorSpecificAttributes MaximumTime(int value) => CreateInteger(LucentAttributeType.MAXIMUM_TIME, value);

        /// <summary>Creates a Lucent-Disconnect-Cause attribute (Type 45).</summary>
        public static VendorSpecificAttributes DisconnectCause(int value) => CreateInteger(LucentAttributeType.DISCONNECT_CAUSE, value);

        /// <summary>Creates a Lucent-Connect-Progress attribute (Type 46).</summary>
        public static VendorSpecificAttributes ConnectProgress(int value) => CreateInteger(LucentAttributeType.CONNECT_PROGRESS, value);

        /// <summary>Creates a Lucent-Data-Rate attribute (Type 47).</summary>
        public static VendorSpecificAttributes DataRate(int value) => CreateInteger(LucentAttributeType.DATA_RATE, value);

        /// <summary>Creates a Lucent-PreSession-Time attribute (Type 48).</summary>
        public static VendorSpecificAttributes PreSessionTime(int value) => CreateInteger(LucentAttributeType.PRESESSION_TIME, value);

        /// <summary>Creates a Lucent-Assign-IP-Pool attribute (Type 49).</summary>
        public static VendorSpecificAttributes AssignIpPool(int value) => CreateInteger(LucentAttributeType.ASSIGN_IP_POOL, value);

        /// <summary>Creates a Lucent-Maximum-Channels attribute (Type 51).</summary>
        public static VendorSpecificAttributes MaximumChannels(int value) => CreateInteger(LucentAttributeType.MAXIMUM_CHANNELS, value);

        /// <summary>Creates a Lucent-Idle-Limit attribute (Type 54).</summary>
        public static VendorSpecificAttributes IdleLimit(int value) => CreateInteger(LucentAttributeType.IDLE_LIMIT, value);

        /// <summary>Creates a Lucent-Preempt-Limit attribute (Type 55).</summary>
        public static VendorSpecificAttributes PreemptLimit(int value) => CreateInteger(LucentAttributeType.PREEMPT_LIMIT, value);

        /// <summary>Creates a Lucent-Callback attribute (Type 56).</summary>
        public static VendorSpecificAttributes Callback(LUCENT_CALLBACK value) => CreateInteger(LucentAttributeType.CALLBACK, (int)value);

        /// <summary>Creates a Lucent-Data-Svc attribute (Type 57).</summary>
        public static VendorSpecificAttributes DataSvc(LUCENT_DATA_SVC value) => CreateInteger(LucentAttributeType.DATA_SVC, (int)value);

        /// <summary>Creates a Lucent-Force-56 attribute (Type 58).</summary>
        public static VendorSpecificAttributes Force56(LUCENT_FORCE_56 value) => CreateInteger(LucentAttributeType.FORCE_56, (int)value);

        /// <summary>Creates a Lucent-Call-By-Call attribute (Type 60).</summary>
        public static VendorSpecificAttributes CallByCall(int value) => CreateInteger(LucentAttributeType.CALL_BY_CALL, value);

        /// <summary>Creates a Lucent-PPP-VJ-1172 attribute (Type 63).</summary>
        public static VendorSpecificAttributes PppVj1172(int value) => CreateInteger(LucentAttributeType.PPP_VJ_1172, value);

        /// <summary>Creates a Lucent-PPP-VJ-Slot-Comp attribute (Type 64).</summary>
        public static VendorSpecificAttributes PppVjSlotComp(int value) => CreateInteger(LucentAttributeType.PPP_VJ_SLOT_COMP, value);

        /// <summary>Creates a Lucent-PPP-Async-Map attribute (Type 65).</summary>
        public static VendorSpecificAttributes PppAsyncMap(int value) => CreateInteger(LucentAttributeType.PPP_ASYNC_MAP, value);

        /// <summary>Creates a Lucent-IPX-Peer-Mode attribute (Type 69).</summary>
        public static VendorSpecificAttributes IpxPeerMode(int value) => CreateInteger(LucentAttributeType.IPX_PEER_MODE, value);

        /// <summary>Creates a Lucent-Client-Assign-DNS attribute (Type 81).</summary>
        public static VendorSpecificAttributes ClientAssignDns(int value) => CreateInteger(LucentAttributeType.CLIENT_ASSIGN_DNS, value);

        /// <summary>Creates a Lucent-Multicast-Rate-Limit attribute (Type 84).</summary>
        public static VendorSpecificAttributes MulticastRateLimit(int value) => CreateInteger(LucentAttributeType.MULTICAST_RATE_LIMIT, value);

        /// <summary>Creates a Lucent-Multicast-GLeave-Delay attribute (Type 87).</summary>
        public static VendorSpecificAttributes MulticastGLeaveDelay(int value) => CreateInteger(LucentAttributeType.MULTICAST_GLEAVE_DELAY, value);

        /// <summary>Creates a Lucent-Num-In-Multilink attribute (Type 90).</summary>
        public static VendorSpecificAttributes NumInMultilink(int value) => CreateInteger(LucentAttributeType.NUM_IN_MULTILINK, value);

        /// <summary>Creates a Lucent-Pre-Input-Octets-64 attribute (Type 94).</summary>
        public static VendorSpecificAttributes PreInputOctets64(int value) => CreateInteger(LucentAttributeType.PRE_INPUT_OCTETS_64, value);

        /// <summary>Creates a Lucent-Pre-Output-Octets-64 attribute (Type 95).</summary>
        public static VendorSpecificAttributes PreOutputOctets64(int value) => CreateInteger(LucentAttributeType.PRE_OUTPUT_OCTETS_64, value);

        /// <summary>Creates a Lucent-Multicast-Client-Max-Groups attribute (Type 141).</summary>
        public static VendorSpecificAttributes MulticastClientMaxGroups(int value) => CreateInteger(LucentAttributeType.MULTICAST_CLIENT_MAX_GROUPS, value);

        /// <summary>Creates a Lucent-Third-Prompt-Format attribute (Type 176).</summary>
        public static VendorSpecificAttributes ThirdPromptFormat(int value) => CreateInteger(LucentAttributeType.THIRD_PROMPT_FORMAT, value);

        /// <summary>Creates a Lucent-User-Acct-Type attribute (Type 180).</summary>
        public static VendorSpecificAttributes UserAcctType(int value) => CreateInteger(LucentAttributeType.USER_ACCT_TYPE, value);

        /// <summary>Creates a Lucent-User-Acct-Port attribute (Type 182).</summary>
        public static VendorSpecificAttributes UserAcctPort(int value) => CreateInteger(LucentAttributeType.USER_ACCT_PORT, value);

        /// <summary>Creates a Lucent-User-Acct-Base attribute (Type 184).</summary>
        public static VendorSpecificAttributes UserAcctBase(int value) => CreateInteger(LucentAttributeType.USER_ACCT_BASE, value);

        /// <summary>Creates a Lucent-User-Acct-Time attribute (Type 185).</summary>
        public static VendorSpecificAttributes UserAcctTime(int value) => CreateInteger(LucentAttributeType.USER_ACCT_TIME, value);

        /// <summary>Creates a Lucent-Numbering-Plan-ID attribute (Type 219).</summary>
        public static VendorSpecificAttributes NumberingPlanId(int value) => CreateInteger(LucentAttributeType.NUMBERING_PLAN_ID, value);

        /// <summary>Creates a Lucent-ATM-Fault-Management attribute (Type 227).</summary>
        public static VendorSpecificAttributes AtmFaultManagement(int value) => CreateInteger(LucentAttributeType.ATM_FAULT_MANAGEMENT, value);

        /// <summary>Creates a Lucent-ATM-Loopback-Cell-Loss attribute (Type 228).</summary>
        public static VendorSpecificAttributes AtmLoopbackCellLoss(int value) => CreateInteger(LucentAttributeType.ATM_LOOPBACK_CELL_LOSS, value);

        /// <summary>Creates a Lucent-ATM-Vci attribute (Type 230).</summary>
        public static VendorSpecificAttributes AtmVci(int value) => CreateInteger(LucentAttributeType.ATM_VCI, value);

        /// <summary>Creates a Lucent-ATM-Vpi attribute (Type 231).</summary>
        public static VendorSpecificAttributes AtmVpi(int value) => CreateInteger(LucentAttributeType.ATM_VPI, value);

        /// <summary>Creates a Lucent-Source-IP-Check attribute (Type 234).</summary>
        public static VendorSpecificAttributes SourceIpCheck(LUCENT_SOURCE_IP_CHECK value) => CreateInteger(LucentAttributeType.SOURCE_IP_CHECK, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Lucent-FR-Circuit-Name attribute (Type 14).</summary>
        public static VendorSpecificAttributes FrCircuitName(string value) => CreateString(LucentAttributeType.FR_CIRCUIT_NAME, value);

        /// <summary>Creates a Lucent-Bridge-Address attribute (Type 26).</summary>
        public static VendorSpecificAttributes BridgeAddress(string value) => CreateString(LucentAttributeType.BRIDGE_ADDRESS, value);

        /// <summary>Creates a Lucent-Data-Filter attribute (Type 52).</summary>
        public static VendorSpecificAttributes DataFilter(string value) => CreateString(LucentAttributeType.DATA_FILTER, value);

        /// <summary>Creates a Lucent-Call-Filter attribute (Type 53).</summary>
        public static VendorSpecificAttributes CallFilter(string value) => CreateString(LucentAttributeType.CALL_FILTER, value);

        /// <summary>Creates a Lucent-Billing-Number attribute (Type 59).</summary>
        public static VendorSpecificAttributes BillingNumber(string value) => CreateString(LucentAttributeType.BILLING_NUMBER, value);

        /// <summary>Creates a Lucent-Transit-Number attribute (Type 61).</summary>
        public static VendorSpecificAttributes TransitNumber(string value) => CreateString(LucentAttributeType.TRANSIT_NUMBER, value);

        /// <summary>Creates a Lucent-Host-Info attribute (Type 62).</summary>
        public static VendorSpecificAttributes HostInfo(string value) => CreateString(LucentAttributeType.HOST_INFO, value);

        /// <summary>Creates a Lucent-Third-Prompt attribute (Type 66).</summary>
        public static VendorSpecificAttributes ThirdPrompt(string value) => CreateString(LucentAttributeType.THIRD_PROMPT, value);

        /// <summary>Creates a Lucent-Send-Secret attribute (Type 67).</summary>
        public static VendorSpecificAttributes SendSecret(string value) => CreateString(LucentAttributeType.SEND_SECRET, value);

        /// <summary>Creates a Lucent-Receive-Secret attribute (Type 68).</summary>
        public static VendorSpecificAttributes ReceiveSecret(string value) => CreateString(LucentAttributeType.RECEIVE_SECRET, value);

        /// <summary>Creates a Lucent-IP-Pool-Definition attribute (Type 70).</summary>
        public static VendorSpecificAttributes IpPoolDefinition(string value) => CreateString(LucentAttributeType.IP_POOL_DEFINITION, value);

        /// <summary>Creates a Lucent-Assign-IP-Global-Pool attribute (Type 73).</summary>
        public static VendorSpecificAttributes AssignIpGlobalPool(string value) => CreateString(LucentAttributeType.ASSIGN_IP_GLOBAL_POOL, value);

        /// <summary>Creates a Lucent-Menu-Selector attribute (Type 150).</summary>
        public static VendorSpecificAttributes MenuSelector(string value) => CreateString(LucentAttributeType.MENU_SELECTOR, value);

        /// <summary>Creates a Lucent-Menu-Item attribute (Type 151).</summary>
        public static VendorSpecificAttributes MenuItem(string value) => CreateString(LucentAttributeType.MENU_ITEM, value);

        /// <summary>Creates a Lucent-Send-Name attribute (Type 177).</summary>
        public static VendorSpecificAttributes SendName(string value) => CreateString(LucentAttributeType.SEND_NAME, value);

        /// <summary>Creates a Lucent-Recv-Name attribute (Type 178).</summary>
        public static VendorSpecificAttributes RecvName(string value) => CreateString(LucentAttributeType.RECV_NAME, value);

        /// <summary>Creates a Lucent-User-Acct-Key attribute (Type 183).</summary>
        public static VendorSpecificAttributes UserAcctKey(string value) => CreateString(LucentAttributeType.USER_ACCT_KEY, value);

        /// <summary>Creates a Lucent-Assign-IP-Pool-Name attribute (Type 218).</summary>
        public static VendorSpecificAttributes AssignIpPoolName(string value) => CreateString(LucentAttributeType.ASSIGN_IP_POOL_NAME, value);

        /// <summary>Creates a Lucent-FR-SVC-Addr attribute (Type 220).</summary>
        public static VendorSpecificAttributes FrSvcAddr(string value) => CreateString(LucentAttributeType.FR_SVC_ADDR, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Lucent-Client-Gateway attribute (Type 7).</summary>
        public static VendorSpecificAttributes ClientGateway(IPAddress value) => CreateIpv4(LucentAttributeType.CLIENT_GATEWAY, value);

        /// <summary>Creates a Lucent-Client-Primary-DNS attribute (Type 8).</summary>
        public static VendorSpecificAttributes ClientPrimaryDns(IPAddress value) => CreateIpv4(LucentAttributeType.CLIENT_PRIMARY_DNS, value);

        /// <summary>Creates a Lucent-Client-Secondary-DNS attribute (Type 9).</summary>
        public static VendorSpecificAttributes ClientSecondaryDns(IPAddress value) => CreateIpv4(LucentAttributeType.CLIENT_SECONDARY_DNS, value);

        /// <summary>Creates a Lucent-PPP-Address attribute (Type 33).</summary>
        public static VendorSpecificAttributes PppAddress(IPAddress value) => CreateIpv4(LucentAttributeType.PPP_ADDRESS, value);

        /// <summary>Creates a Lucent-Assign-IP-Client attribute (Type 71).</summary>
        public static VendorSpecificAttributes AssignIpClient(IPAddress value) => CreateIpv4(LucentAttributeType.ASSIGN_IP_CLIENT, value);

        /// <summary>Creates a Lucent-Assign-IP-Server attribute (Type 72).</summary>
        public static VendorSpecificAttributes AssignIpServer(IPAddress value) => CreateIpv4(LucentAttributeType.ASSIGN_IP_SERVER, value);

        /// <summary>Creates a Lucent-IF-Netmask attribute (Type 85).</summary>
        public static VendorSpecificAttributes IfNetmask(IPAddress value) => CreateIpv4(LucentAttributeType.IF_NETMASK, value);

        /// <summary>Creates a Lucent-Remote-Addr attribute (Type 86).</summary>
        public static VendorSpecificAttributes RemoteAddr(IPAddress value) => CreateIpv4(LucentAttributeType.REMOTE_ADDR, value);

        /// <summary>Creates a Lucent-First-Dest attribute (Type 93).</summary>
        public static VendorSpecificAttributes FirstDest(IPAddress value) => CreateIpv4(LucentAttributeType.FIRST_DEST, value);

        /// <summary>Creates a Lucent-Client-Primary-WINS attribute (Type 135).</summary>
        public static VendorSpecificAttributes ClientPrimaryWins(IPAddress value) => CreateIpv4(LucentAttributeType.CLIENT_PRIMARY_WINS, value);

        /// <summary>Creates a Lucent-Client-Secondary-WINS attribute (Type 136).</summary>
        public static VendorSpecificAttributes ClientSecondaryWins(IPAddress value) => CreateIpv4(LucentAttributeType.CLIENT_SECONDARY_WINS, value);

        /// <summary>Creates a Lucent-User-Acct-Host attribute (Type 181).</summary>
        public static VendorSpecificAttributes UserAcctHost(IPAddress value) => CreateIpv4(LucentAttributeType.USER_ACCT_HOST, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(LucentAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(LucentAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(LucentAttributeType type, IPAddress value)
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