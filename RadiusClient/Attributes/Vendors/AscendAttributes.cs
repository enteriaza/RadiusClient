using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Ascend Communications / Lucent (IANA PEN 529) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.ascend</c>.
    /// </summary>
    public enum AscendAttributeType : byte
    {
        /// <summary>Ascend-Max-Shared-Users (Type 2). Integer. Maximum shared users on a connection.</summary>
        MAX_SHARED_USERS = 2,

        /// <summary>Ascend-UU-Info (Type 7). String. User-to-User information element.</summary>
        UU_INFO = 7,

        /// <summary>Ascend-CIR-Timer (Type 9). Integer. Committed Information Rate timer in seconds.</summary>
        CIR_TIMER = 9,

        /// <summary>Ascend-FR-Circuit-Name (Type 10). String. Frame Relay circuit name.</summary>
        FR_CIRCUIT_NAME = 10,

        /// <summary>Ascend-FR-LinkUp (Type 11). Integer. Frame Relay link up behaviour.</summary>
        FR_LINKUP = 11,

        /// <summary>Ascend-FR-Nailed-Grp (Type 12). Integer. Frame Relay nailed group number.</summary>
        FR_NAILED_GRP = 12,

        /// <summary>Ascend-FR-Type (Type 13). Integer. Frame Relay encapsulation type.</summary>
        FR_TYPE = 13,

        /// <summary>Ascend-FR-Link-Mgt (Type 14). Integer. Frame Relay link management type.</summary>
        FR_LINK_MGT = 14,

        /// <summary>Ascend-FR-N391 (Type 15). Integer. Frame Relay N391 full status polling counter.</summary>
        FR_N391 = 15,

        /// <summary>Ascend-FR-DCE-N392 (Type 16). Integer. Frame Relay DCE N392 error threshold.</summary>
        FR_DCE_N392 = 16,

        /// <summary>Ascend-FR-DTE-N392 (Type 17). Integer. Frame Relay DTE N392 error threshold.</summary>
        FR_DTE_N392 = 17,

        /// <summary>Ascend-FR-DCE-N393 (Type 18). Integer. Frame Relay DCE N393 monitored events.</summary>
        FR_DCE_N393 = 18,

        /// <summary>Ascend-FR-DTE-N393 (Type 19). Integer. Frame Relay DTE N393 monitored events.</summary>
        FR_DTE_N393 = 19,

        /// <summary>Ascend-FR-T391 (Type 20). Integer. Frame Relay T391 link integrity timer in seconds.</summary>
        FR_T391 = 20,

        /// <summary>Ascend-FR-T392 (Type 21). Integer. Frame Relay T392 polling verification timer in seconds.</summary>
        FR_T392 = 21,

        /// <summary>Ascend-Bridge-Address (Type 22). String. Bridge address filter.</summary>
        BRIDGE_ADDRESS = 22,

        /// <summary>Ascend-TS-Idle-Limit (Type 23). Integer. Terminal server idle limit in seconds.</summary>
        TS_IDLE_LIMIT = 23,

        /// <summary>Ascend-TS-Idle-Mode (Type 24). Integer. Terminal server idle mode.</summary>
        TS_IDLE_MODE = 24,

        /// <summary>Ascend-DBA-Monitor (Type 25). Integer. Dynamic Bandwidth Allocation monitor mode.</summary>
        DBA_MONITOR = 25,

        /// <summary>Ascend-Base-Channel-Count (Type 26). Integer. Base channel count for ISDN.</summary>
        BASE_CHANNEL_COUNT = 26,

        /// <summary>Ascend-Minimum-Channels (Type 27). Integer. Minimum channels for ISDN.</summary>
        MINIMUM_CHANNELS = 27,

        /// <summary>Ascend-IPX-Route (Type 28). String. IPX route entry.</summary>
        IPX_ROUTE = 28,

        /// <summary>Ascend-FT1-Caller (Type 29). Integer. FT1 caller flag.</summary>
        FT1_CALLER = 29,

        /// <summary>Ascend-Backup (Type 30). String. Backup profile name.</summary>
        BACKUP = 30,

        /// <summary>Ascend-Call-Type (Type 31). Integer. Call type (nailed/switched).</summary>
        CALL_TYPE = 31,

        /// <summary>Ascend-Group (Type 32). String. Group name.</summary>
        GROUP = 32,

        /// <summary>Ascend-FR-DLCI (Type 33). Integer. Frame Relay DLCI number.</summary>
        FR_DLCI = 33,

        /// <summary>Ascend-FR-Profile-Name (Type 34). String. Frame Relay profile name.</summary>
        FR_PROFILE_NAME = 34,

        /// <summary>Ascend-Ara-PW (Type 35). String. AppleTalk Remote Access password.</summary>
        ARA_PW = 35,

        /// <summary>Ascend-IPX-Node-Addr (Type 36). String. IPX node address.</summary>
        IPX_NODE_ADDR = 36,

        /// <summary>Ascend-Home-Agent-IP-Addr (Type 37). IP address. Home agent IP address.</summary>
        HOME_AGENT_IP_ADDR = 37,

        /// <summary>Ascend-Home-Agent-Password (Type 38). String. Home agent password.</summary>
        HOME_AGENT_PASSWORD = 38,

        /// <summary>Ascend-Home-Network-Name (Type 39). String. Home network name.</summary>
        HOME_NETWORK_NAME = 39,

        /// <summary>Ascend-Home-Agent-UDP-Port (Type 40). Integer. Home agent UDP port number.</summary>
        HOME_AGENT_UDP_PORT = 40,

        /// <summary>Ascend-Multilink-ID (Type 41). Integer. Multilink bundle identifier.</summary>
        MULTILINK_ID = 41,

        /// <summary>Ascend-Num-In-Multilink (Type 42). Integer. Number of links in multilink bundle.</summary>
        NUM_IN_MULTILINK = 42,

        /// <summary>Ascend-First-Dest (Type 43). IP address. First destination IP address.</summary>
        FIRST_DEST = 43,

        /// <summary>Ascend-Pre-Input-Octets (Type 44). Integer. Pre-session input octets.</summary>
        PRE_INPUT_OCTETS = 44,

        /// <summary>Ascend-Pre-Output-Octets (Type 45). Integer. Pre-session output octets.</summary>
        PRE_OUTPUT_OCTETS = 45,

        /// <summary>Ascend-Pre-Input-Packets (Type 46). Integer. Pre-session input packets.</summary>
        PRE_INPUT_PACKETS = 46,

        /// <summary>Ascend-Pre-Output-Packets (Type 47). Integer. Pre-session output packets.</summary>
        PRE_OUTPUT_PACKETS = 47,

        /// <summary>Ascend-Maximum-Time (Type 48). Integer. Maximum session time in seconds.</summary>
        MAXIMUM_TIME = 48,

        /// <summary>Ascend-Disconnect-Cause (Type 49). Integer. Disconnect cause code.</summary>
        DISCONNECT_CAUSE = 49,

        /// <summary>Ascend-Connect-Progress (Type 50). Integer. Connection progress indicator.</summary>
        CONNECT_PROGRESS = 50,

        /// <summary>Ascend-Data-Rate (Type 51). Integer. Data rate in bps.</summary>
        DATA_RATE = 51,

        /// <summary>Ascend-PreSession-Time (Type 52). Integer. Pre-session time in seconds.</summary>
        PRESESSION_TIME = 52,

        /// <summary>Ascend-Token-Idle (Type 53). Integer. Token idle timeout in seconds.</summary>
        TOKEN_IDLE = 53,

        /// <summary>Ascend-Token-Immediate (Type 54). Integer. Token immediate mode.</summary>
        TOKEN_IMMEDIATE = 54,

        /// <summary>Ascend-Require-Auth (Type 55). Integer. Require authentication flag.</summary>
        REQUIRE_AUTH = 55,

        /// <summary>Ascend-Number-Sessions (Type 56). String. Number of sessions string.</summary>
        NUMBER_SESSIONS = 56,

        /// <summary>Ascend-Authen-Alias (Type 57). String. Authentication alias.</summary>
        AUTHEN_ALIAS = 57,

        /// <summary>Ascend-Token-Expiry (Type 58). Integer. Token expiry time in seconds.</summary>
        TOKEN_EXPIRY = 58,

        /// <summary>Ascend-Menu-Selector (Type 59). String. Menu selector string.</summary>
        MENU_SELECTOR = 59,

        /// <summary>Ascend-Menu-Item (Type 60). String. Menu item definition.</summary>
        MENU_ITEM = 60,

        /// <summary>Ascend-PW-Warntime (Type 61). Integer. Password warning time in days.</summary>
        PW_WARNTIME = 61,

        /// <summary>Ascend-PW-Lifetime (Type 62). Integer. Password lifetime in days.</summary>
        PW_LIFETIME = 62,

        /// <summary>Ascend-IP-Direct (Type 63). IP address. IP direct address for routing.</summary>
        IP_DIRECT = 63,

        /// <summary>Ascend-PPP-VJ-Slot-Comp (Type 64). Integer. PPP VJ slot compression.</summary>
        PPP_VJ_SLOT_COMP = 64,

        /// <summary>Ascend-PPP-VJ-1172 (Type 65). Integer. PPP VJ RFC 1172 mode.</summary>
        PPP_VJ_1172 = 65,

        /// <summary>Ascend-PPP-Async-Map (Type 66). Integer. PPP async control character map.</summary>
        PPP_ASYNC_MAP = 66,

        /// <summary>Ascend-Third-Prompt (Type 67). String. Third login prompt string.</summary>
        THIRD_PROMPT = 67,

        /// <summary>Ascend-Send-Secret (Type 68). String. Send secret (CHAP).</summary>
        SEND_SECRET = 68,

        /// <summary>Ascend-Receive-Secret (Type 69). String. Receive secret (CHAP).</summary>
        RECEIVE_SECRET = 69,

        /// <summary>Ascend-IPX-Peer-Mode (Type 70). Integer. IPX peer mode.</summary>
        IPX_PEER_MODE = 70,

        /// <summary>Ascend-IP-Pool-Definition (Type 71). String. IP pool definition.</summary>
        IP_POOL_DEFINITION = 71,

        /// <summary>Ascend-Assign-IP-Pool (Type 72). Integer. Assign IP pool number.</summary>
        ASSIGN_IP_POOL = 72,

        /// <summary>Ascend-FR-Direct (Type 73). Integer. Frame Relay direct mode.</summary>
        FR_DIRECT = 73,

        /// <summary>Ascend-FR-Direct-Profile (Type 74). String. Frame Relay direct profile name.</summary>
        FR_DIRECT_PROFILE = 74,

        /// <summary>Ascend-FR-Direct-DLCI (Type 75). Integer. Frame Relay direct DLCI.</summary>
        FR_DIRECT_DLCI = 75,

        /// <summary>Ascend-Handle-IPX (Type 76). Integer. Handle IPX traffic mode.</summary>
        HANDLE_IPX = 76,

        /// <summary>Ascend-Netware-timeout (Type 77). Integer. NetWare timeout in seconds.</summary>
        NETWARE_TIMEOUT = 77,

        /// <summary>Ascend-IPX-Alias (Type 78). Integer. IPX alias number.</summary>
        IPX_ALIAS = 78,

        /// <summary>Ascend-Metric (Type 79). Integer. Route metric.</summary>
        METRIC = 79,

        /// <summary>Ascend-PRI-Number-Type (Type 80). Integer. PRI number type.</summary>
        PRI_NUMBER_TYPE = 80,

        /// <summary>Ascend-Dial-Number (Type 81). String. Dial number string.</summary>
        DIAL_NUMBER = 81,

        /// <summary>Ascend-Route-IP (Type 82). Integer. Route IP traffic flag.</summary>
        ROUTE_IP = 82,

        /// <summary>Ascend-Route-IPX (Type 83). Integer. Route IPX traffic flag.</summary>
        ROUTE_IPX = 83,

        /// <summary>Ascend-Bridge (Type 84). Integer. Bridge mode flag.</summary>
        BRIDGE = 84,

        /// <summary>Ascend-Send-Auth (Type 85). Integer. Send authentication method.</summary>
        SEND_AUTH = 85,

        /// <summary>Ascend-Send-Passwd (Type 86). String. Send password string.</summary>
        SEND_PASSWD = 86,

        /// <summary>Ascend-Link-Compression (Type 87). Integer. Link compression type.</summary>
        LINK_COMPRESSION = 87,

        /// <summary>Ascend-Target-Util (Type 88). Integer. Target utilisation percentage.</summary>
        TARGET_UTIL = 88,

        /// <summary>Ascend-Maximum-Channels (Type 89). Integer. Maximum ISDN channels.</summary>
        MAXIMUM_CHANNELS = 89,

        /// <summary>Ascend-Inc-Channel-Count (Type 90). Integer. Incremental channel count.</summary>
        INC_CHANNEL_COUNT = 90,

        /// <summary>Ascend-Dec-Channel-Count (Type 91). Integer. Decremental channel count.</summary>
        DEC_CHANNEL_COUNT = 91,

        /// <summary>Ascend-Seconds-Of-History (Type 92). Integer. Seconds of history for DBA.</summary>
        SECONDS_OF_HISTORY = 92,

        /// <summary>Ascend-History-Weigh-Type (Type 93). Integer. History weight type for DBA.</summary>
        HISTORY_WEIGH_TYPE = 93,

        /// <summary>Ascend-Add-Seconds (Type 94). Integer. Add seconds threshold for DBA.</summary>
        ADD_SECONDS = 94,

        /// <summary>Ascend-Remove-Seconds (Type 95). Integer. Remove seconds threshold for DBA.</summary>
        REMOVE_SECONDS = 95,

        /// <summary>Ascend-Data-Filter (Type 242). Octets. Data filter definition.</summary>
        DATA_FILTER = 242,

        /// <summary>Ascend-Call-Filter (Type 243). Octets. Call filter definition.</summary>
        CALL_FILTER = 243,

        /// <summary>Ascend-Idle-Limit (Type 244). Integer. Idle limit in seconds.</summary>
        IDLE_LIMIT = 244,

        /// <summary>Ascend-Preempt-Limit (Type 245). Integer. Preempt limit in seconds.</summary>
        PREEMPT_LIMIT = 245,

        /// <summary>Ascend-Callback (Type 246). Integer. Callback mode.</summary>
        CALLBACK = 246,

        /// <summary>Ascend-Data-Svc (Type 247). Integer. Data service type.</summary>
        DATA_SVC = 247,

        /// <summary>Ascend-Force-56 (Type 248). Integer. Force 56K mode.</summary>
        FORCE_56 = 248,

        /// <summary>Ascend-Billing-Number (Type 249). String. Billing number string.</summary>
        BILLING_NUMBER = 249,

        /// <summary>Ascend-Call-By-Call (Type 250). Integer. Call-by-call transit number index.</summary>
        CALL_BY_CALL = 250,

        /// <summary>Ascend-Transit-Number (Type 251). String. Transit number string.</summary>
        TRANSIT_NUMBER = 251,

        /// <summary>Ascend-Host-Info (Type 252). String. Host information string.</summary>
        HOST_INFO = 252,

        /// <summary>Ascend-PPP-Address (Type 253). IP address. PPP peer address.</summary>
        PPP_ADDRESS = 253,

        /// <summary>Ascend-MPP-Idle-Percent (Type 254). Integer. MPP idle percentage threshold.</summary>
        MPP_IDLE_PERCENT = 254,

        /// <summary>Ascend-Xmit-Rate (Type 255). Integer. Transmit rate in bps.</summary>
        XMIT_RATE = 255
    }

    /// <summary>
    /// Ascend-FR-LinkUp attribute values (Type 11).
    /// </summary>
    public enum ASCEND_FR_LINKUP
    {
        /// <summary>Link starts down.</summary>
        LINK_UP_DEFAULT = 0,

        /// <summary>Link starts up.</summary>
        LINK_UP_STARTUP = 1
    }

    /// <summary>
    /// Ascend-FR-Type attribute values (Type 13).
    /// </summary>
    public enum ASCEND_FR_TYPE
    {
        /// <summary>Ascend FR DTE mode.</summary>
        FR_DTE = 0,

        /// <summary>Ascend FR DCE mode.</summary>
        FR_DCE = 1,

        /// <summary>Ascend FR NNI mode.</summary>
        FR_NNI = 2
    }

    /// <summary>
    /// Ascend-FR-Link-Mgt attribute values (Type 14).
    /// </summary>
    public enum ASCEND_FR_LINK_MGT
    {
        /// <summary>Auto-detect link management.</summary>
        FR_LMI_AUTO = 0,

        /// <summary>ANSI Annex D link management.</summary>
        FR_LMI_ANSI = 1,

        /// <summary>Q.933 Annex A link management.</summary>
        FR_LMI_Q933A = 2,

        /// <summary>No link management.</summary>
        FR_LMI_NONE = 3
    }

    /// <summary>
    /// Ascend-Call-Type attribute values (Type 31).
    /// </summary>
    public enum ASCEND_CALL_TYPE
    {
        /// <summary>Nailed (permanent) call.</summary>
        NAILED = 0,

        /// <summary>Nailed/MPP call.</summary>
        NAILED_MPP = 1,

        /// <summary>Perm/switched call.</summary>
        PERM_SWITCHED = 2,

        /// <summary>Switched call.</summary>
        SWITCHED = 3
    }

    /// <summary>
    /// Ascend-Send-Auth attribute values (Type 85).
    /// </summary>
    public enum ASCEND_SEND_AUTH
    {
        /// <summary>Send PAP authentication.</summary>
        SEND_AUTH_PAP = 0,

        /// <summary>Send CHAP authentication.</summary>
        SEND_AUTH_CHAP = 1,

        /// <summary>Send MS-CHAP authentication.</summary>
        SEND_AUTH_MSCHAP = 2,

        /// <summary>No authentication.</summary>
        SEND_AUTH_NONE = 3
    }

    /// <summary>
    /// Ascend-Link-Compression attribute values (Type 87).
    /// </summary>
    public enum ASCEND_LINK_COMPRESSION
    {
        /// <summary>No compression.</summary>
        LINK_COMP_NONE = 0,

        /// <summary>Stac compression.</summary>
        LINK_COMP_STAC = 1,

        /// <summary>Stac draft-9 compression.</summary>
        LINK_COMP_STAC_DRAFT_9 = 2,

        /// <summary>MPPC compression.</summary>
        LINK_COMP_MPPC = 3
    }

    /// <summary>
    /// Ascend-History-Weigh-Type attribute values (Type 93).
    /// </summary>
    public enum ASCEND_HISTORY_WEIGH_TYPE
    {
        /// <summary>Equal weighting.</summary>
        HISTORY_CONSTANT = 0,

        /// <summary>Linear weighting.</summary>
        HISTORY_LINEAR = 1,

        /// <summary>Quadratic weighting.</summary>
        HISTORY_QUADRATIC = 2
    }

    /// <summary>
    /// Ascend-Callback attribute values (Type 246).
    /// </summary>
    public enum ASCEND_CALLBACK
    {
        /// <summary>No callback.</summary>
        CALLBACK_NO = 0,

        /// <summary>Callback with CBCP negotiation.</summary>
        CALLBACK_CBCP = 1,

        /// <summary>Callback using provided number.</summary>
        CALLBACK_YES = 2
    }

    /// <summary>
    /// Ascend-Data-Svc attribute values (Type 247).
    /// </summary>
    public enum ASCEND_DATA_SVC
    {
        /// <summary>Switched voice call.</summary>
        SWITCHED_VOICE_BEARER = 0,

        /// <summary>Switched 56K data.</summary>
        SWITCHED_56K = 1,

        /// <summary>Switched 64K data.</summary>
        SWITCHED_64K = 2,

        /// <summary>Switched 64K restricted.</summary>
        SWITCHED_64K_RESTRICTED = 3,

        /// <summary>Clear channel (transparent) 64K.</summary>
        CLEAR_CHANNEL = 4,

        /// <summary>Switched 384K data.</summary>
        SWITCHED_384K = 5,

        /// <summary>Digital multirate.</summary>
        DIGITAL_MULTIRATE = 6,

        /// <summary>Switched 1536K data.</summary>
        SWITCHED_1536K = 7,

        /// <summary>Switched V.110 data.</summary>
        SWITCHED_V110 = 8
    }

    /// <summary>
    /// Ascend-Force-56 attribute values (Type 248).
    /// </summary>
    public enum ASCEND_FORCE_56
    {
        /// <summary>Force 56K disabled.</summary>
        FORCE_56_NO = 0,

        /// <summary>Force 56K enabled.</summary>
        FORCE_56_YES = 1
    }

    /// <summary>
    /// Ascend-DBA-Monitor attribute values (Type 25).
    /// </summary>
    public enum ASCEND_DBA_MONITOR
    {
        /// <summary>DBA transmit monitoring.</summary>
        DBA_TRANSMIT = 0,

        /// <summary>DBA transmit and receive monitoring.</summary>
        DBA_TRANSMIT_RECV = 1,

        /// <summary>DBA disabled.</summary>
        DBA_NONE = 2
    }

    /// <summary>
    /// Ascend-Route-IP attribute values (Type 82).
    /// </summary>
    public enum ASCEND_ROUTE_IP
    {
        /// <summary>Route IP disabled.</summary>
        ROUTE_IP_NO = 0,

        /// <summary>Route IP enabled.</summary>
        ROUTE_IP_YES = 1
    }

    /// <summary>
    /// Ascend-Route-IPX attribute values (Type 83).
    /// </summary>
    public enum ASCEND_ROUTE_IPX
    {
        /// <summary>Route IPX disabled.</summary>
        ROUTE_IPX_NO = 0,

        /// <summary>Route IPX enabled.</summary>
        ROUTE_IPX_YES = 1
    }

    /// <summary>
    /// Ascend-Bridge attribute values (Type 84).
    /// </summary>
    public enum ASCEND_BRIDGE
    {
        /// <summary>Bridging disabled.</summary>
        BRIDGE_NO = 0,

        /// <summary>Bridging enabled.</summary>
        BRIDGE_YES = 1
    }

    /// <summary>
    /// Ascend-TS-Idle-Mode attribute values (Type 24).
    /// </summary>
    public enum ASCEND_TS_IDLE_MODE
    {
        /// <summary>No idle mode.</summary>
        TS_IDLE_NONE = 0,

        /// <summary>Input idle mode.</summary>
        TS_IDLE_INPUT = 1,

        /// <summary>Input/Output idle mode.</summary>
        TS_IDLE_INPUT_OUTPUT = 2
    }

    /// <summary>
    /// Ascend-Token-Immediate attribute values (Type 54).
    /// </summary>
    public enum ASCEND_TOKEN_IMMEDIATE
    {
        /// <summary>Token immediate disabled.</summary>
        TOKEN_IMMEDIATE_NO = 0,

        /// <summary>Token immediate enabled.</summary>
        TOKEN_IMMEDIATE_YES = 1
    }

    /// <summary>
    /// Ascend-Require-Auth attribute values (Type 55).
    /// </summary>
    public enum ASCEND_REQUIRE_AUTH
    {
        /// <summary>Authentication not required.</summary>
        NOT_REQUIRE_AUTH = 0,

        /// <summary>Authentication required.</summary>
        REQUIRE_AUTH = 1
    }

    /// <summary>
    /// Ascend-PPP-VJ-Slot-Comp attribute values (Type 64).
    /// </summary>
    public enum ASCEND_PPP_VJ_SLOT_COMP
    {
        /// <summary>VJ slot compression disabled.</summary>
        VJ_SLOT_COMP_NO = 0,

        /// <summary>VJ slot compression enabled.</summary>
        VJ_SLOT_COMP_YES = 1
    }

    /// <summary>
    /// Ascend-PPP-VJ-1172 attribute values (Type 65).
    /// </summary>
    public enum ASCEND_PPP_VJ_1172
    {
        /// <summary>VJ RFC 1172 mode disabled.</summary>
        PPP_VJ_1172_NO = 0,

        /// <summary>VJ RFC 1172 mode enabled.</summary>
        PPP_VJ_1172_YES = 1
    }

    /// <summary>
    /// Ascend-Handle-IPX attribute values (Type 76).
    /// </summary>
    public enum ASCEND_HANDLE_IPX
    {
        /// <summary>Handle IPX disabled.</summary>
        HANDLE_IPX_NONE = 0,

        /// <summary>Handle IPX enabled.</summary>
        HANDLE_IPX_CLIENT = 1,

        /// <summary>Handle IPX server mode.</summary>
        HANDLE_IPX_SERVER = 2
    }

    /// <summary>
    /// Ascend-PRI-Number-Type attribute values (Type 80).
    /// </summary>
    public enum ASCEND_PRI_NUMBER_TYPE
    {
        /// <summary>Unknown number type.</summary>
        UNKNOWN_NUMBER = 0,

        /// <summary>International number type.</summary>
        INTL_NUMBER = 1,

        /// <summary>National number type.</summary>
        NATIONAL_NUMBER = 2,

        /// <summary>Network-specific number type.</summary>
        NET_SPECIFIC_NUMBER = 3,

        /// <summary>Local number type.</summary>
        LOCAL_NUMBER = 4,

        /// <summary>Abbreviated number type.</summary>
        ABBREV_NUMBER = 5
    }

    /// <summary>
    /// Ascend-Disconnect-Cause attribute values (Type 49).
    /// </summary>
    public enum ASCEND_DISCONNECT_CAUSE
    {
        /// <summary>No reason given.</summary>
        NO_REASON = 0,

        /// <summary>Not applicable.</summary>
        NOT_APPLICABLE = 1,

        /// <summary>Unknown.</summary>
        UNKNOWN = 2,

        /// <summary>Call disconnected.</summary>
        CALL_DISCONNECTED = 3,

        /// <summary>CLID authentication failure.</summary>
        CLID_AUTH_FAILED = 4,

        /// <summary>CLID RADIUS request.</summary>
        CLID_RADIUS_REQUEST = 5,

        /// <summary>Resource unavailable.</summary>
        RESOURCE_UNAVAILABLE = 6,

        /// <summary>Timer expired.</summary>
        TIMER_EXPIRED = 7,

        /// <summary>No reason given (alternate).</summary>
        NO_REASON_2 = 8
    }

    /// <summary>
    /// Ascend-Connect-Progress attribute values (Type 50).
    /// </summary>
    public enum ASCEND_CONNECT_PROGRESS
    {
        /// <summary>No progress.</summary>
        NO_PROGRESS = 0,

        /// <summary>Call up.</summary>
        CALL_UP = 10,

        /// <summary>Modem up.</summary>
        MODEM_UP = 30,

        /// <summary>Modem awaiting DCD.</summary>
        MODEM_AWAITING_DCD = 31,

        /// <summary>Modem awaiting codes.</summary>
        MODEM_AWAITING_CODES = 32,

        /// <summary>Terminal server started.</summary>
        TERMINAL_SERVER_STARTED = 40,

        /// <summary>Terminal server raw TCP established.</summary>
        TERMINAL_SERVER_RAW_TCP = 41,

        /// <summary>Terminal server Telnet started.</summary>
        TERMINAL_SERVER_TELNET = 42,

        /// <summary>Terminal server raw TCP connected.</summary>
        TERMINAL_SERVER_RAW_TCP_CONNECTED = 43,

        /// <summary>Terminal server Telnet connected.</summary>
        TERMINAL_SERVER_TELNET_CONNECTED = 44,

        /// <summary>LAN session up.</summary>
        LAN_SESSION_UP = 60,

        /// <summary>LCP opening.</summary>
        LCP_OPENING = 61,

        /// <summary>CCP opening.</summary>
        CCP_OPENING = 62,

        /// <summary>IPNCP opening.</summary>
        IPNCP_OPENING = 63,

        /// <summary>BNCP opening.</summary>
        BNCP_OPENING = 64,

        /// <summary>LCP opened.</summary>
        LCP_OPENED = 65,

        /// <summary>CCP opened.</summary>
        CCP_OPENED = 66,

        /// <summary>IPNCP opened.</summary>
        IPNCP_OPENED = 67,

        /// <summary>BNCP opened.</summary>
        BNCP_OPENED = 68,

        /// <summary>LCP state initial.</summary>
        LCP_STATE_INITIAL = 71,

        /// <summary>LCP state starting.</summary>
        LCP_STATE_STARTING = 72,

        /// <summary>LCP state closed.</summary>
        LCP_STATE_CLOSED = 73,

        /// <summary>LCP state stopped.</summary>
        LCP_STATE_STOPPED = 74,

        /// <summary>LCP state closing.</summary>
        LCP_STATE_CLOSING = 75,

        /// <summary>LCP state stopping.</summary>
        LCP_STATE_STOPPING = 76,

        /// <summary>LCP state request sent.</summary>
        LCP_STATE_REQ_SENT = 77,

        /// <summary>LCP state ack received.</summary>
        LCP_STATE_ACK_RCVD = 78,

        /// <summary>LCP state ack sent.</summary>
        LCP_STATE_ACK_SENT = 79,

        /// <summary>LCP state opened.</summary>
        LCP_STATE_OPENED = 80
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Ascend Communications /
    /// Lucent (IANA PEN 529) Vendor-Specific Attributes (VSAs), as defined in the
    /// FreeRADIUS <c>dictionary.ascend</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Ascend's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 529</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Ascend (Lucent) MAX and Pipeline access
    /// concentrators for ISDN/dial-up session management, Frame Relay configuration,
    /// Dynamic Bandwidth Allocation (DBA), PPP/VJ/MPPC negotiation, IPX routing,
    /// call filtering, bridging, callback, and multilink PPP.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AscendAttributes.MaximumChannels(2));
    /// packet.SetAttribute(AscendAttributes.DataRate(64000));
    /// packet.SetAttribute(AscendAttributes.DataSvc(ASCEND_DATA_SVC.SWITCHED_64K));
    /// packet.SetAttribute(AscendAttributes.IdleLimit(300));
    /// </code>
    /// </remarks>
    public static class AscendAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Ascend Communications (Lucent).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 529;

        #region Integer Attributes

        /// <summary>
        /// Creates an Ascend-Max-Shared-Users attribute (Type 2) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum shared users on a connection.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSharedUsers(int value)
        {
            return CreateInteger(AscendAttributeType.MAX_SHARED_USERS, value);
        }

        /// <summary>
        /// Creates an Ascend-CIR-Timer attribute (Type 9) with the specified timer.
        /// </summary>
        /// <param name="value">The CIR timer in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CirTimer(int value)
        {
            return CreateInteger(AscendAttributeType.CIR_TIMER, value);
        }

        /// <summary>
        /// Creates an Ascend-FR-LinkUp attribute (Type 11) with the specified behaviour.
        /// </summary>
        /// <param name="value">The FR link up behaviour. See <see cref="ASCEND_FR_LINKUP"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FrLinkUp(ASCEND_FR_LINKUP value)
        {
            return CreateInteger(AscendAttributeType.FR_LINKUP, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-FR-Nailed-Grp attribute (Type 12) with the specified group number.
        /// </summary>
        /// <param name="value">The Frame Relay nailed group number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FrNailedGrp(int value)
        {
            return CreateInteger(AscendAttributeType.FR_NAILED_GRP, value);
        }

        /// <summary>
        /// Creates an Ascend-FR-Type attribute (Type 13) with the specified type.
        /// </summary>
        /// <param name="value">The FR encapsulation type. See <see cref="ASCEND_FR_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FrType(ASCEND_FR_TYPE value)
        {
            return CreateInteger(AscendAttributeType.FR_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-FR-Link-Mgt attribute (Type 14) with the specified management type.
        /// </summary>
        /// <param name="value">The FR link management type. See <see cref="ASCEND_FR_LINK_MGT"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FrLinkMgt(ASCEND_FR_LINK_MGT value)
        {
            return CreateInteger(AscendAttributeType.FR_LINK_MGT, (int)value);
        }

        /// <summary>Creates an Ascend-FR-N391 attribute (Type 15).</summary>
        public static VendorSpecificAttributes FrN391(int value) => CreateInteger(AscendAttributeType.FR_N391, value);

        /// <summary>Creates an Ascend-FR-DCE-N392 attribute (Type 16).</summary>
        public static VendorSpecificAttributes FrDceN392(int value) => CreateInteger(AscendAttributeType.FR_DCE_N392, value);

        /// <summary>Creates an Ascend-FR-DTE-N392 attribute (Type 17).</summary>
        public static VendorSpecificAttributes FrDteN392(int value) => CreateInteger(AscendAttributeType.FR_DTE_N392, value);

        /// <summary>Creates an Ascend-FR-DCE-N393 attribute (Type 18).</summary>
        public static VendorSpecificAttributes FrDceN393(int value) => CreateInteger(AscendAttributeType.FR_DCE_N393, value);

        /// <summary>Creates an Ascend-FR-DTE-N393 attribute (Type 19).</summary>
        public static VendorSpecificAttributes FrDteN393(int value) => CreateInteger(AscendAttributeType.FR_DTE_N393, value);

        /// <summary>Creates an Ascend-FR-T391 attribute (Type 20).</summary>
        public static VendorSpecificAttributes FrT391(int value) => CreateInteger(AscendAttributeType.FR_T391, value);

        /// <summary>Creates an Ascend-FR-T392 attribute (Type 21).</summary>
        public static VendorSpecificAttributes FrT392(int value) => CreateInteger(AscendAttributeType.FR_T392, value);

        /// <summary>Creates an Ascend-TS-Idle-Limit attribute (Type 23).</summary>
        public static VendorSpecificAttributes TsIdleLimit(int value) => CreateInteger(AscendAttributeType.TS_IDLE_LIMIT, value);

        /// <summary>
        /// Creates an Ascend-TS-Idle-Mode attribute (Type 24) with the specified mode.
        /// </summary>
        /// <param name="value">The TS idle mode. See <see cref="ASCEND_TS_IDLE_MODE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TsIdleMode(ASCEND_TS_IDLE_MODE value)
        {
            return CreateInteger(AscendAttributeType.TS_IDLE_MODE, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-DBA-Monitor attribute (Type 25) with the specified mode.
        /// </summary>
        /// <param name="value">The DBA monitor mode. See <see cref="ASCEND_DBA_MONITOR"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DbaMonitor(ASCEND_DBA_MONITOR value)
        {
            return CreateInteger(AscendAttributeType.DBA_MONITOR, (int)value);
        }

        /// <summary>Creates an Ascend-Base-Channel-Count attribute (Type 26).</summary>
        public static VendorSpecificAttributes BaseChannelCount(int value) => CreateInteger(AscendAttributeType.BASE_CHANNEL_COUNT, value);

        /// <summary>Creates an Ascend-Minimum-Channels attribute (Type 27).</summary>
        public static VendorSpecificAttributes MinimumChannels(int value) => CreateInteger(AscendAttributeType.MINIMUM_CHANNELS, value);

        /// <summary>Creates an Ascend-FT1-Caller attribute (Type 29).</summary>
        public static VendorSpecificAttributes Ft1Caller(int value) => CreateInteger(AscendAttributeType.FT1_CALLER, value);

        /// <summary>
        /// Creates an Ascend-Call-Type attribute (Type 31) with the specified call type.
        /// </summary>
        /// <param name="value">The call type. See <see cref="ASCEND_CALL_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallType(ASCEND_CALL_TYPE value)
        {
            return CreateInteger(AscendAttributeType.CALL_TYPE, (int)value);
        }

        /// <summary>Creates an Ascend-FR-DLCI attribute (Type 33).</summary>
        public static VendorSpecificAttributes FrDlci(int value) => CreateInteger(AscendAttributeType.FR_DLCI, value);

        /// <summary>Creates an Ascend-Home-Agent-UDP-Port attribute (Type 40).</summary>
        public static VendorSpecificAttributes HomeAgentUdpPort(int value) => CreateInteger(AscendAttributeType.HOME_AGENT_UDP_PORT, value);

        /// <summary>Creates an Ascend-Multilink-ID attribute (Type 41).</summary>
        public static VendorSpecificAttributes MultilinkId(int value) => CreateInteger(AscendAttributeType.MULTILINK_ID, value);

        /// <summary>Creates an Ascend-Num-In-Multilink attribute (Type 42).</summary>
        public static VendorSpecificAttributes NumInMultilink(int value) => CreateInteger(AscendAttributeType.NUM_IN_MULTILINK, value);

        /// <summary>Creates an Ascend-Pre-Input-Octets attribute (Type 44).</summary>
        public static VendorSpecificAttributes PreInputOctets(int value) => CreateInteger(AscendAttributeType.PRE_INPUT_OCTETS, value);

        /// <summary>Creates an Ascend-Pre-Output-Octets attribute (Type 45).</summary>
        public static VendorSpecificAttributes PreOutputOctets(int value) => CreateInteger(AscendAttributeType.PRE_OUTPUT_OCTETS, value);

        /// <summary>Creates an Ascend-Pre-Input-Packets attribute (Type 46).</summary>
        public static VendorSpecificAttributes PreInputPackets(int value) => CreateInteger(AscendAttributeType.PRE_INPUT_PACKETS, value);

        /// <summary>Creates an Ascend-Pre-Output-Packets attribute (Type 47).</summary>
        public static VendorSpecificAttributes PreOutputPackets(int value) => CreateInteger(AscendAttributeType.PRE_OUTPUT_PACKETS, value);

        /// <summary>Creates an Ascend-Maximum-Time attribute (Type 48).</summary>
        public static VendorSpecificAttributes MaximumTime(int value) => CreateInteger(AscendAttributeType.MAXIMUM_TIME, value);

        /// <summary>
        /// Creates an Ascend-Disconnect-Cause attribute (Type 49) with the specified cause.
        /// </summary>
        /// <param name="value">The disconnect cause. See <see cref="ASCEND_DISCONNECT_CAUSE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DisconnectCause(ASCEND_DISCONNECT_CAUSE value)
        {
            return CreateInteger(AscendAttributeType.DISCONNECT_CAUSE, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-Connect-Progress attribute (Type 50) with the specified progress.
        /// </summary>
        /// <param name="value">The connect progress. See <see cref="ASCEND_CONNECT_PROGRESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ConnectProgress(ASCEND_CONNECT_PROGRESS value)
        {
            return CreateInteger(AscendAttributeType.CONNECT_PROGRESS, (int)value);
        }

        /// <summary>Creates an Ascend-Data-Rate attribute (Type 51).</summary>
        public static VendorSpecificAttributes DataRate(int value) => CreateInteger(AscendAttributeType.DATA_RATE, value);

        /// <summary>Creates an Ascend-PreSession-Time attribute (Type 52).</summary>
        public static VendorSpecificAttributes PreSessionTime(int value) => CreateInteger(AscendAttributeType.PRESESSION_TIME, value);

        /// <summary>Creates an Ascend-Token-Idle attribute (Type 53).</summary>
        public static VendorSpecificAttributes TokenIdle(int value) => CreateInteger(AscendAttributeType.TOKEN_IDLE, value);

        /// <summary>
        /// Creates an Ascend-Token-Immediate attribute (Type 54) with the specified setting.
        /// </summary>
        /// <param name="value">The token immediate setting. See <see cref="ASCEND_TOKEN_IMMEDIATE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TokenImmediate(ASCEND_TOKEN_IMMEDIATE value)
        {
            return CreateInteger(AscendAttributeType.TOKEN_IMMEDIATE, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-Require-Auth attribute (Type 55) with the specified setting.
        /// </summary>
        /// <param name="value">The require auth setting. See <see cref="ASCEND_REQUIRE_AUTH"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RequireAuth(ASCEND_REQUIRE_AUTH value)
        {
            return CreateInteger(AscendAttributeType.REQUIRE_AUTH, (int)value);
        }

        /// <summary>Creates an Ascend-Token-Expiry attribute (Type 58).</summary>
        public static VendorSpecificAttributes TokenExpiry(int value) => CreateInteger(AscendAttributeType.TOKEN_EXPIRY, value);

        /// <summary>Creates an Ascend-PW-Warntime attribute (Type 61).</summary>
        public static VendorSpecificAttributes PwWarntime(int value) => CreateInteger(AscendAttributeType.PW_WARNTIME, value);

        /// <summary>Creates an Ascend-PW-Lifetime attribute (Type 62).</summary>
        public static VendorSpecificAttributes PwLifetime(int value) => CreateInteger(AscendAttributeType.PW_LIFETIME, value);

        /// <summary>
        /// Creates an Ascend-PPP-VJ-Slot-Comp attribute (Type 64) with the specified setting.
        /// </summary>
        /// <param name="value">The VJ slot compression setting. See <see cref="ASCEND_PPP_VJ_SLOT_COMP"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PppVjSlotComp(ASCEND_PPP_VJ_SLOT_COMP value)
        {
            return CreateInteger(AscendAttributeType.PPP_VJ_SLOT_COMP, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-PPP-VJ-1172 attribute (Type 65) with the specified setting.
        /// </summary>
        /// <param name="value">The VJ 1172 setting. See <see cref="ASCEND_PPP_VJ_1172"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PppVj1172(ASCEND_PPP_VJ_1172 value)
        {
            return CreateInteger(AscendAttributeType.PPP_VJ_1172, (int)value);
        }

        /// <summary>Creates an Ascend-PPP-Async-Map attribute (Type 66).</summary>
        public static VendorSpecificAttributes PppAsyncMap(int value) => CreateInteger(AscendAttributeType.PPP_ASYNC_MAP, value);

        /// <summary>
        /// Creates an Ascend-IPX-Peer-Mode attribute (Type 70) with the specified mode.
        /// </summary>
        /// <param name="value">The IPX peer mode.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpxPeerMode(int value)
        {
            return CreateInteger(AscendAttributeType.IPX_PEER_MODE, value);
        }

        /// <summary>Creates an Ascend-Assign-IP-Pool attribute (Type 72).</summary>
        public static VendorSpecificAttributes AssignIpPool(int value) => CreateInteger(AscendAttributeType.ASSIGN_IP_POOL, value);

        /// <summary>Creates an Ascend-FR-Direct attribute (Type 73).</summary>
        public static VendorSpecificAttributes FrDirect(int value) => CreateInteger(AscendAttributeType.FR_DIRECT, value);

        /// <summary>Creates an Ascend-FR-Direct-DLCI attribute (Type 75).</summary>
        public static VendorSpecificAttributes FrDirectDlci(int value) => CreateInteger(AscendAttributeType.FR_DIRECT_DLCI, value);

        /// <summary>
        /// Creates an Ascend-Handle-IPX attribute (Type 76) with the specified mode.
        /// </summary>
        /// <param name="value">The handle IPX mode. See <see cref="ASCEND_HANDLE_IPX"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes HandleIpx(ASCEND_HANDLE_IPX value)
        {
            return CreateInteger(AscendAttributeType.HANDLE_IPX, (int)value);
        }

        /// <summary>Creates an Ascend-Netware-timeout attribute (Type 77).</summary>
        public static VendorSpecificAttributes NetwareTimeout(int value) => CreateInteger(AscendAttributeType.NETWARE_TIMEOUT, value);

        /// <summary>Creates an Ascend-IPX-Alias attribute (Type 78).</summary>
        public static VendorSpecificAttributes IpxAlias(int value) => CreateInteger(AscendAttributeType.IPX_ALIAS, value);

        /// <summary>Creates an Ascend-Metric attribute (Type 79).</summary>
        public static VendorSpecificAttributes Metric(int value) => CreateInteger(AscendAttributeType.METRIC, value);

        /// <summary>
        /// Creates an Ascend-PRI-Number-Type attribute (Type 80) with the specified type.
        /// </summary>
        /// <param name="value">The PRI number type. See <see cref="ASCEND_PRI_NUMBER_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PriNumberType(ASCEND_PRI_NUMBER_TYPE value)
        {
            return CreateInteger(AscendAttributeType.PRI_NUMBER_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-Route-IP attribute (Type 82) with the specified setting.
        /// </summary>
        /// <param name="value">The route IP setting. See <see cref="ASCEND_ROUTE_IP"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RouteIp(ASCEND_ROUTE_IP value)
        {
            return CreateInteger(AscendAttributeType.ROUTE_IP, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-Route-IPX attribute (Type 83) with the specified setting.
        /// </summary>
        /// <param name="value">The route IPX setting. See <see cref="ASCEND_ROUTE_IPX"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RouteIpx(ASCEND_ROUTE_IPX value)
        {
            return CreateInteger(AscendAttributeType.ROUTE_IPX, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-Bridge attribute (Type 84) with the specified setting.
        /// </summary>
        /// <param name="value">The bridge mode. See <see cref="ASCEND_BRIDGE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Bridge(ASCEND_BRIDGE value)
        {
            return CreateInteger(AscendAttributeType.BRIDGE, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-Send-Auth attribute (Type 85) with the specified method.
        /// </summary>
        /// <param name="value">The send authentication method. See <see cref="ASCEND_SEND_AUTH"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SendAuth(ASCEND_SEND_AUTH value)
        {
            return CreateInteger(AscendAttributeType.SEND_AUTH, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-Link-Compression attribute (Type 87) with the specified type.
        /// </summary>
        /// <param name="value">The link compression type. See <see cref="ASCEND_LINK_COMPRESSION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LinkCompression(ASCEND_LINK_COMPRESSION value)
        {
            return CreateInteger(AscendAttributeType.LINK_COMPRESSION, (int)value);
        }

        /// <summary>Creates an Ascend-Target-Util attribute (Type 88).</summary>
        public static VendorSpecificAttributes TargetUtil(int value) => CreateInteger(AscendAttributeType.TARGET_UTIL, value);

        /// <summary>Creates an Ascend-Maximum-Channels attribute (Type 89).</summary>
        public static VendorSpecificAttributes MaximumChannels(int value) => CreateInteger(AscendAttributeType.MAXIMUM_CHANNELS, value);

        /// <summary>Creates an Ascend-Inc-Channel-Count attribute (Type 90).</summary>
        public static VendorSpecificAttributes IncChannelCount(int value) => CreateInteger(AscendAttributeType.INC_CHANNEL_COUNT, value);

        /// <summary>Creates an Ascend-Dec-Channel-Count attribute (Type 91).</summary>
        public static VendorSpecificAttributes DecChannelCount(int value) => CreateInteger(AscendAttributeType.DEC_CHANNEL_COUNT, value);

        /// <summary>Creates an Ascend-Seconds-Of-History attribute (Type 92).</summary>
        public static VendorSpecificAttributes SecondsOfHistory(int value) => CreateInteger(AscendAttributeType.SECONDS_OF_HISTORY, value);

        /// <summary>
        /// Creates an Ascend-History-Weigh-Type attribute (Type 93) with the specified type.
        /// </summary>
        /// <param name="value">The history weight type. See <see cref="ASCEND_HISTORY_WEIGH_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes HistoryWeighType(ASCEND_HISTORY_WEIGH_TYPE value)
        {
            return CreateInteger(AscendAttributeType.HISTORY_WEIGH_TYPE, (int)value);
        }

        /// <summary>Creates an Ascend-Add-Seconds attribute (Type 94).</summary>
        public static VendorSpecificAttributes AddSeconds(int value) => CreateInteger(AscendAttributeType.ADD_SECONDS, value);

        /// <summary>Creates an Ascend-Remove-Seconds attribute (Type 95).</summary>
        public static VendorSpecificAttributes RemoveSeconds(int value) => CreateInteger(AscendAttributeType.REMOVE_SECONDS, value);

        /// <summary>Creates an Ascend-Idle-Limit attribute (Type 244).</summary>
        public static VendorSpecificAttributes IdleLimit(int value) => CreateInteger(AscendAttributeType.IDLE_LIMIT, value);

        /// <summary>Creates an Ascend-Preempt-Limit attribute (Type 245).</summary>
        public static VendorSpecificAttributes PreemptLimit(int value) => CreateInteger(AscendAttributeType.PREEMPT_LIMIT, value);

        /// <summary>
        /// Creates an Ascend-Callback attribute (Type 246) with the specified mode.
        /// </summary>
        /// <param name="value">The callback mode. See <see cref="ASCEND_CALLBACK"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Callback(ASCEND_CALLBACK value)
        {
            return CreateInteger(AscendAttributeType.CALLBACK, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-Data-Svc attribute (Type 247) with the specified service type.
        /// </summary>
        /// <param name="value">The data service type. See <see cref="ASCEND_DATA_SVC"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DataSvc(ASCEND_DATA_SVC value)
        {
            return CreateInteger(AscendAttributeType.DATA_SVC, (int)value);
        }

        /// <summary>
        /// Creates an Ascend-Force-56 attribute (Type 248) with the specified setting.
        /// </summary>
        /// <param name="value">The force 56K setting. See <see cref="ASCEND_FORCE_56"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Force56(ASCEND_FORCE_56 value)
        {
            return CreateInteger(AscendAttributeType.FORCE_56, (int)value);
        }

        /// <summary>Creates an Ascend-Call-By-Call attribute (Type 250).</summary>
        public static VendorSpecificAttributes CallByCall(int value) => CreateInteger(AscendAttributeType.CALL_BY_CALL, value);

        /// <summary>Creates an Ascend-MPP-Idle-Percent attribute (Type 254).</summary>
        public static VendorSpecificAttributes MppIdlePercent(int value) => CreateInteger(AscendAttributeType.MPP_IDLE_PERCENT, value);

        /// <summary>Creates an Ascend-Xmit-Rate attribute (Type 255).</summary>
        public static VendorSpecificAttributes XmitRate(int value) => CreateInteger(AscendAttributeType.XMIT_RATE, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a CVX-UU-Info attribute (Type 7).</summary>
        /// <param name="value">The user-to-user information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UuInfo(string value) => CreateString(AscendAttributeType.UU_INFO, value);

        /// <summary>Creates an Ascend-FR-Circuit-Name attribute (Type 10).</summary>
        /// <param name="value">The FR circuit name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FrCircuitName(string value) => CreateString(AscendAttributeType.FR_CIRCUIT_NAME, value);

        /// <summary>Creates an Ascend-Bridge-Address attribute (Type 22).</summary>
        /// <param name="value">The bridge address filter. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BridgeAddress(string value) => CreateString(AscendAttributeType.BRIDGE_ADDRESS, value);

        /// <summary>Creates an Ascend-IPX-Route attribute (Type 28).</summary>
        /// <param name="value">The IPX route entry. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpxRoute(string value) => CreateString(AscendAttributeType.IPX_ROUTE, value);

        /// <summary>Creates an Ascend-Backup attribute (Type 30).</summary>
        /// <param name="value">The backup profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Backup(string value) => CreateString(AscendAttributeType.BACKUP, value);

        /// <summary>Creates an Ascend-Group attribute (Type 32).</summary>
        /// <param name="value">The group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Group(string value) => CreateString(AscendAttributeType.GROUP, value);

        /// <summary>Creates an Ascend-FR-Profile-Name attribute (Type 34).</summary>
        /// <param name="value">The FR profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FrProfileName(string value) => CreateString(AscendAttributeType.FR_PROFILE_NAME, value);

        /// <summary>Creates an Ascend-Ara-PW attribute (Type 35).</summary>
        /// <param name="value">The ARA password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AraPw(string value) => CreateString(AscendAttributeType.ARA_PW, value);

        /// <summary>Creates an Ascend-IPX-Node-Addr attribute (Type 36).</summary>
        /// <param name="value">The IPX node address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpxNodeAddr(string value) => CreateString(AscendAttributeType.IPX_NODE_ADDR, value);

        /// <summary>Creates an Ascend-Home-Agent-Password attribute (Type 38).</summary>
        /// <param name="value">The home agent password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HomeAgentPassword(string value) => CreateString(AscendAttributeType.HOME_AGENT_PASSWORD, value);

        /// <summary>Creates an Ascend-Home-Network-Name attribute (Type 39).</summary>
        /// <param name="value">The home network name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HomeNetworkName(string value) => CreateString(AscendAttributeType.HOME_NETWORK_NAME, value);

        /// <summary>Creates an Ascend-Number-Sessions attribute (Type 56).</summary>
        /// <param name="value">The number of sessions string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NumberSessions(string value) => CreateString(AscendAttributeType.NUMBER_SESSIONS, value);

        /// <summary>Creates an Ascend-Authen-Alias attribute (Type 57).</summary>
        /// <param name="value">The authentication alias. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthenAlias(string value) => CreateString(AscendAttributeType.AUTHEN_ALIAS, value);

        /// <summary>Creates an Ascend-Menu-Selector attribute (Type 59).</summary>
        /// <param name="value">The menu selector string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MenuSelector(string value) => CreateString(AscendAttributeType.MENU_SELECTOR, value);

        /// <summary>Creates an Ascend-Menu-Item attribute (Type 60).</summary>
        /// <param name="value">The menu item definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MenuItem(string value) => CreateString(AscendAttributeType.MENU_ITEM, value);

        /// <summary>Creates an Ascend-Third-Prompt attribute (Type 67).</summary>
        /// <param name="value">The third login prompt. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ThirdPrompt(string value) => CreateString(AscendAttributeType.THIRD_PROMPT, value);

        /// <summary>Creates an Ascend-Send-Secret attribute (Type 68).</summary>
        /// <param name="value">The send secret (CHAP). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SendSecret(string value) => CreateString(AscendAttributeType.SEND_SECRET, value);

        /// <summary>Creates an Ascend-Receive-Secret attribute (Type 69).</summary>
        /// <param name="value">The receive secret (CHAP). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ReceiveSecret(string value) => CreateString(AscendAttributeType.RECEIVE_SECRET, value);

        /// <summary>Creates an Ascend-IP-Pool-Definition attribute (Type 71).</summary>
        /// <param name="value">The IP pool definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPoolDefinition(string value) => CreateString(AscendAttributeType.IP_POOL_DEFINITION, value);

        /// <summary>Creates an Ascend-FR-Direct-Profile attribute (Type 74).</summary>
        /// <param name="value">The FR direct profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FrDirectProfile(string value) => CreateString(AscendAttributeType.FR_DIRECT_PROFILE, value);

        /// <summary>Creates an Ascend-Dial-Number attribute (Type 81).</summary>
        /// <param name="value">The dial number string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DialNumber(string value) => CreateString(AscendAttributeType.DIAL_NUMBER, value);

        /// <summary>Creates an Ascend-Send-Passwd attribute (Type 86).</summary>
        /// <param name="value">The send password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SendPasswd(string value) => CreateString(AscendAttributeType.SEND_PASSWD, value);

        /// <summary>Creates an Ascend-Billing-Number attribute (Type 249).</summary>
        /// <param name="value">The billing number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BillingNumber(string value) => CreateString(AscendAttributeType.BILLING_NUMBER, value);

        /// <summary>Creates an Ascend-Transit-Number attribute (Type 251).</summary>
        /// <param name="value">The transit number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TransitNumber(string value) => CreateString(AscendAttributeType.TRANSIT_NUMBER, value);

        /// <summary>Creates an Ascend-Host-Info attribute (Type 252).</summary>
        /// <param name="value">The host information string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HostInfo(string value) => CreateString(AscendAttributeType.HOST_INFO, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Ascend-Home-Agent-IP-Addr attribute (Type 37) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The home agent IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes HomeAgentIpAddr(IPAddress value) => CreateIpv4(AscendAttributeType.HOME_AGENT_IP_ADDR, value);

        /// <summary>
        /// Creates an Ascend-First-Dest attribute (Type 43) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The first destination IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes FirstDest(IPAddress value) => CreateIpv4(AscendAttributeType.FIRST_DEST, value);

        /// <summary>
        /// Creates an Ascend-IP-Direct attribute (Type 63) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The IP direct address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes IpDirect(IPAddress value) => CreateIpv4(AscendAttributeType.IP_DIRECT, value);

        /// <summary>
        /// Creates an Ascend-PPP-Address attribute (Type 253) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The PPP peer address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PppAddress(IPAddress value) => CreateIpv4(AscendAttributeType.PPP_ADDRESS, value);

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates an Ascend-Data-Filter attribute (Type 242) with the specified raw filter data.
        /// </summary>
        /// <param name="value">The data filter definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DataFilter(byte[] value) => CreateOctets(AscendAttributeType.DATA_FILTER, value);

        /// <summary>
        /// Creates an Ascend-Call-Filter attribute (Type 243) with the specified raw filter data.
        /// </summary>
        /// <param name="value">The call filter definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallFilter(byte[] value) => CreateOctets(AscendAttributeType.CALL_FILTER, value);

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Ascend attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AscendAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Ascend attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AscendAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a raw byte array value for the specified Ascend attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateOctets(AscendAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified Ascend attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(AscendAttributeType type, IPAddress value)
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