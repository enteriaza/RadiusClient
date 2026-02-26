using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Ascend Communications "illegal" (IANA PEN 529) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.ascend.illegal</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// These attributes were historically placed directly in the top-level RADIUS
    /// attribute space (types 128–255) by older Ascend MAX/Pipeline equipment instead
    /// of being properly encoded as VSAs under vendor ID 529. FreeRADIUS maps them
    /// into the vendor 529 namespace for interoperability.
    /// </para>
    /// <para>
    /// This enum uses a separate set of type codes from <see cref="AscendAttributeType"/>
    /// to avoid conflicts.
    /// </para>
    /// </remarks>
    public enum AscendIllegalAttributeType : byte
    {
        /// <summary>X-Ascend-Modem-PortNo (Type 128). Integer. Modem port number.</summary>
        MODEM_PORTNO = 128,

        /// <summary>X-Ascend-Modem-SlotNo (Type 129). Integer. Modem slot number.</summary>
        MODEM_SLOTNO = 129,

        /// <summary>X-Ascend-Modem-ShelfNo (Type 130). Integer. Modem shelf number.</summary>
        MODEM_SHELFNO = 130,

        /// <summary>X-Ascend-Call-Attempt-Limit (Type 131). Integer. Call attempt limit.</summary>
        CALL_ATTEMPT_LIMIT = 131,

        /// <summary>X-Ascend-Call-Block-Duration (Type 132). Integer. Call block duration in seconds.</summary>
        CALL_BLOCK_DURATION = 132,

        /// <summary>X-Ascend-Maximum-Call-Duration (Type 133). Integer. Maximum call duration in seconds.</summary>
        MAXIMUM_CALL_DURATION = 133,

        /// <summary>X-Ascend-Temporary-Rtes (Type 134). Integer. Temporary routes flag.</summary>
        TEMPORARY_RTES = 134,

        /// <summary>X-Ascend-Tunneling-Protocol (Type 135). Integer. Tunnelling protocol type.</summary>
        TUNNELING_PROTOCOL = 135,

        /// <summary>X-Ascend-Shared-Profile-Enable (Type 136). Integer. Shared profile enable flag.</summary>
        SHARED_PROFILE_ENABLE = 136,

        /// <summary>X-Ascend-Primary-Home-Agent (Type 137). String. Primary home agent address.</summary>
        PRIMARY_HOME_AGENT = 137,

        /// <summary>X-Ascend-Secondary-Home-Agent (Type 138). String. Secondary home agent address.</summary>
        SECONDARY_HOME_AGENT = 138,

        /// <summary>X-Ascend-Dialout-Allowed (Type 139). Integer. Dialout allowed flag.</summary>
        DIALOUT_ALLOWED = 139,

        /// <summary>X-Ascend-Client-Gateway (Type 140). IP address. Client gateway address.</summary>
        CLIENT_GATEWAY = 140,

        /// <summary>X-Ascend-BACP-Enable (Type 141). Integer. BACP enable flag.</summary>
        BACP_ENABLE = 141,

        /// <summary>X-Ascend-DHCP-Maximum-Leases (Type 142). Integer. DHCP maximum leases.</summary>
        DHCP_MAXIMUM_LEASES = 142,

        /// <summary>X-Ascend-Client-Primary-DNS (Type 143). IP address. Client primary DNS address.</summary>
        CLIENT_PRIMARY_DNS = 143,

        /// <summary>X-Ascend-Client-Secondary-DNS (Type 144). IP address. Client secondary DNS address.</summary>
        CLIENT_SECONDARY_DNS = 144,

        /// <summary>X-Ascend-Client-Assign-DNS (Type 145). Integer. Client assign DNS flag.</summary>
        CLIENT_ASSIGN_DNS = 145,

        /// <summary>X-Ascend-User-Acct-Type (Type 146). Integer. User accounting type.</summary>
        USER_ACCT_TYPE = 146,

        /// <summary>X-Ascend-User-Acct-Host (Type 147). IP address. User accounting host address.</summary>
        USER_ACCT_HOST = 147,

        /// <summary>X-Ascend-User-Acct-Port (Type 148). Integer. User accounting port number.</summary>
        USER_ACCT_PORT = 148,

        /// <summary>X-Ascend-User-Acct-Key (Type 149). String. User accounting key.</summary>
        USER_ACCT_KEY = 149,

        /// <summary>X-Ascend-User-Acct-Base (Type 150). Integer. User accounting base.</summary>
        USER_ACCT_BASE = 150,

        /// <summary>X-Ascend-User-Acct-Time (Type 151). Integer. User accounting time in seconds.</summary>
        USER_ACCT_TIME = 151,

        /// <summary>X-Ascend-Assign-IP-Client (Type 152). IP address. Assign IP to client address.</summary>
        ASSIGN_IP_CLIENT = 152,

        /// <summary>X-Ascend-Assign-IP-Server (Type 153). IP address. Assign IP to server address.</summary>
        ASSIGN_IP_SERVER = 153,

        /// <summary>X-Ascend-Assign-IP-Global-Pool (Type 154). String. Global IP pool name.</summary>
        ASSIGN_IP_GLOBAL_POOL = 154,

        /// <summary>X-Ascend-DHCP-Reply (Type 155). Integer. DHCP reply flag.</summary>
        DHCP_REPLY = 155,

        /// <summary>X-Ascend-DHCP-Pool-Number (Type 156). Integer. DHCP pool number.</summary>
        DHCP_POOL_NUMBER = 156,

        /// <summary>X-Ascend-Expect-Callback (Type 157). Integer. Expect callback flag.</summary>
        EXPECT_CALLBACK = 157,

        /// <summary>X-Ascend-Event-Type (Type 158). Integer. Event type code.</summary>
        EVENT_TYPE = 158,

        /// <summary>X-Ascend-Session-Svr-Key (Type 159). String. Session server key.</summary>
        SESSION_SVR_KEY = 159,

        /// <summary>X-Ascend-Multicast-Rate-Limit (Type 160). Integer. Multicast rate limit in bps.</summary>
        MULTICAST_RATE_LIMIT = 160,

        /// <summary>X-Ascend-IF-Netmask (Type 161). IP address. Interface netmask.</summary>
        IF_NETMASK = 161,

        /// <summary>X-Ascend-Remote-Addr (Type 162). IP address. Remote peer address.</summary>
        REMOTE_ADDR = 162,

        /// <summary>X-Ascend-Multicast-Client (Type 163). Integer. Multicast client flag.</summary>
        MULTICAST_CLIENT = 163,

        /// <summary>X-Ascend-FR-Multicast-DLCI (Type 164). Integer. Frame Relay multicast DLCI.</summary>
        FR_MULTICAST_DLCI = 164,

        /// <summary>X-Ascend-FR-SVC-Addr (Type 165). String. Frame Relay SVC address.</summary>
        FR_SVC_ADDR = 165,

        /// <summary>X-Ascend-Source-IP-Check (Type 166). Integer. Source IP check flag.</summary>
        SOURCE_IP_CHECK = 166,

        /// <summary>X-Ascend-IS-Number (Type 167). String. IS number.</summary>
        IS_NUMBER = 167,

        /// <summary>X-Ascend-FR-SetPrio (Type 168). Integer. Frame Relay set priority.</summary>
        FR_SETPRIO = 168,

        /// <summary>X-Ascend-FR-DLCI (Type 169). Integer. Frame Relay DLCI number.</summary>
        FR_DLCI = 169,

        /// <summary>X-Ascend-Handle-IPX (Type 170). Integer. Handle IPX traffic mode.</summary>
        HANDLE_IPX = 170,

        /// <summary>X-Ascend-Netware-timeout (Type 171). Integer. NetWare timeout in seconds.</summary>
        NETWARE_TIMEOUT = 171,

        /// <summary>X-Ascend-IPX-Alias (Type 172). Integer. IPX alias number.</summary>
        IPX_ALIAS = 172,

        /// <summary>X-Ascend-Metric (Type 173). Integer. Route metric.</summary>
        METRIC = 173,

        /// <summary>X-Ascend-PRI-Number-Type (Type 174). Integer. PRI number type.</summary>
        PRI_NUMBER_TYPE = 174,

        /// <summary>X-Ascend-Dial-Number (Type 175). String. Dial number string.</summary>
        DIAL_NUMBER = 175,

        /// <summary>X-Ascend-Route-IP (Type 176). Integer. Route IP traffic flag.</summary>
        ROUTE_IP = 176,

        /// <summary>X-Ascend-Route-IPX (Type 177). Integer. Route IPX traffic flag.</summary>
        ROUTE_IPX = 177,

        /// <summary>X-Ascend-Bridge (Type 178). Integer. Bridge mode flag.</summary>
        BRIDGE = 178,

        /// <summary>X-Ascend-Send-Auth (Type 179). Integer. Send authentication method.</summary>
        SEND_AUTH = 179,

        /// <summary>X-Ascend-Send-Passwd (Type 180). String. Send password string.</summary>
        SEND_PASSWD = 180,

        /// <summary>X-Ascend-Link-Compression (Type 181). Integer. Link compression type.</summary>
        LINK_COMPRESSION = 181,

        /// <summary>X-Ascend-Target-Util (Type 182). Integer. Target utilisation percentage.</summary>
        TARGET_UTIL = 182,

        /// <summary>X-Ascend-Maximum-Channels (Type 183). Integer. Maximum ISDN channels.</summary>
        MAXIMUM_CHANNELS = 183,

        /// <summary>X-Ascend-Inc-Channel-Count (Type 184). Integer. Incremental channel count.</summary>
        INC_CHANNEL_COUNT = 184,

        /// <summary>X-Ascend-Dec-Channel-Count (Type 185). Integer. Decremental channel count.</summary>
        DEC_CHANNEL_COUNT = 185,

        /// <summary>X-Ascend-Seconds-Of-History (Type 186). Integer. Seconds of history for DBA.</summary>
        SECONDS_OF_HISTORY = 186,

        /// <summary>X-Ascend-History-Weigh-Type (Type 187). Integer. History weight type for DBA.</summary>
        HISTORY_WEIGH_TYPE = 187,

        /// <summary>X-Ascend-Add-Seconds (Type 188). Integer. Add seconds threshold for DBA.</summary>
        ADD_SECONDS = 188,

        /// <summary>X-Ascend-Remove-Seconds (Type 189). Integer. Remove seconds threshold for DBA.</summary>
        REMOVE_SECONDS = 189,

        /// <summary>X-Ascend-Data-Filter (Type 190). Octets. Data filter definition.</summary>
        DATA_FILTER = 190,

        /// <summary>X-Ascend-Call-Filter (Type 191). Octets. Call filter definition.</summary>
        CALL_FILTER = 191,

        /// <summary>X-Ascend-Idle-Limit (Type 192). Integer. Idle limit in seconds.</summary>
        IDLE_LIMIT = 192,

        /// <summary>X-Ascend-Preempt-Limit (Type 193). Integer. Preempt limit in seconds.</summary>
        PREEMPT_LIMIT = 193,

        /// <summary>X-Ascend-Callback (Type 194). Integer. Callback mode.</summary>
        CALLBACK = 194,

        /// <summary>X-Ascend-Data-Svc (Type 195). Integer. Data service type.</summary>
        DATA_SVC = 195,

        /// <summary>X-Ascend-Force-56 (Type 196). Integer. Force 56K mode.</summary>
        FORCE_56 = 196,

        /// <summary>X-Ascend-Billing-Number (Type 197). String. Billing number string.</summary>
        BILLING_NUMBER = 197,

        /// <summary>X-Ascend-Call-By-Call (Type 198). Integer. Call-by-call transit index.</summary>
        CALL_BY_CALL = 198,

        /// <summary>X-Ascend-Transit-Number (Type 199). String. Transit number string.</summary>
        TRANSIT_NUMBER = 199,

        /// <summary>X-Ascend-Host-Info (Type 200). String. Host information string.</summary>
        HOST_INFO = 200,

        /// <summary>X-Ascend-PPP-Address (Type 201). IP address. PPP peer address.</summary>
        PPP_ADDRESS = 201,

        /// <summary>X-Ascend-MPP-Idle-Percent (Type 202). Integer. MPP idle percentage threshold.</summary>
        MPP_IDLE_PERCENT = 202,

        /// <summary>X-Ascend-Xmit-Rate (Type 203). Integer. Transmit rate in bps.</summary>
        XMIT_RATE = 203,

        /// <summary>X-Ascend-FR-Direct (Type 204). Integer. Frame Relay direct mode.</summary>
        FR_DIRECT = 204,

        /// <summary>X-Ascend-FR-Direct-Profile (Type 205). String. Frame Relay direct profile name.</summary>
        FR_DIRECT_PROFILE = 205,

        /// <summary>X-Ascend-FR-Direct-DLCI (Type 206). Integer. Frame Relay direct DLCI.</summary>
        FR_DIRECT_DLCI = 206,

        /// <summary>X-Ascend-IPX-Node-Addr (Type 207). String. IPX node address.</summary>
        IPX_NODE_ADDR = 207,

        /// <summary>X-Ascend-Home-Agent-IP-Addr (Type 208). IP address. Home agent IP address.</summary>
        HOME_AGENT_IP_ADDR = 208,

        /// <summary>X-Ascend-Home-Agent-Password (Type 209). String. Home agent password.</summary>
        HOME_AGENT_PASSWORD = 209,

        /// <summary>X-Ascend-Home-Network-Name (Type 210). String. Home network name.</summary>
        HOME_NETWORK_NAME = 210,

        /// <summary>X-Ascend-Home-Agent-UDP-Port (Type 211). Integer. Home agent UDP port.</summary>
        HOME_AGENT_UDP_PORT = 211,

        /// <summary>X-Ascend-Multilink-ID (Type 212). Integer. Multilink bundle identifier.</summary>
        MULTILINK_ID = 212,

        /// <summary>X-Ascend-Num-In-Multilink (Type 213). Integer. Number of links in multilink bundle.</summary>
        NUM_IN_MULTILINK = 213,

        /// <summary>X-Ascend-First-Dest (Type 214). IP address. First destination IP address.</summary>
        FIRST_DEST = 214,

        /// <summary>X-Ascend-Pre-Input-Octets (Type 215). Integer. Pre-session input octets.</summary>
        PRE_INPUT_OCTETS = 215,

        /// <summary>X-Ascend-Pre-Output-Octets (Type 216). Integer. Pre-session output octets.</summary>
        PRE_OUTPUT_OCTETS = 216,

        /// <summary>X-Ascend-Pre-Input-Packets (Type 217). Integer. Pre-session input packets.</summary>
        PRE_INPUT_PACKETS = 217,

        /// <summary>X-Ascend-Pre-Output-Packets (Type 218). Integer. Pre-session output packets.</summary>
        PRE_OUTPUT_PACKETS = 218,

        /// <summary>X-Ascend-Maximum-Time (Type 219). Integer. Maximum session time in seconds.</summary>
        MAXIMUM_TIME = 219,

        /// <summary>X-Ascend-Disconnect-Cause (Type 220). Integer. Disconnect cause code.</summary>
        DISCONNECT_CAUSE = 220,

        /// <summary>X-Ascend-Connect-Progress (Type 221). Integer. Connection progress indicator.</summary>
        CONNECT_PROGRESS = 221,

        /// <summary>X-Ascend-Data-Rate (Type 222). Integer. Data rate in bps.</summary>
        DATA_RATE = 222,

        /// <summary>X-Ascend-PreSession-Time (Type 223). Integer. Pre-session time in seconds.</summary>
        PRESESSION_TIME = 223,

        /// <summary>X-Ascend-Token-Idle (Type 224). Integer. Token idle timeout in seconds.</summary>
        TOKEN_IDLE = 224,

        /// <summary>X-Ascend-Token-Immediate (Type 225). Integer. Token immediate mode.</summary>
        TOKEN_IMMEDIATE = 225,

        /// <summary>X-Ascend-Require-Auth (Type 226). Integer. Require authentication flag.</summary>
        REQUIRE_AUTH = 226,

        /// <summary>X-Ascend-Number-Sessions (Type 227). String. Number of sessions string.</summary>
        NUMBER_SESSIONS = 227,

        /// <summary>X-Ascend-Authen-Alias (Type 228). String. Authentication alias.</summary>
        AUTHEN_ALIAS = 228,

        /// <summary>X-Ascend-Token-Expiry (Type 229). Integer. Token expiry time in seconds.</summary>
        TOKEN_EXPIRY = 229,

        /// <summary>X-Ascend-Menu-Selector (Type 230). String. Menu selector string.</summary>
        MENU_SELECTOR = 230,

        /// <summary>X-Ascend-Menu-Item (Type 231). String. Menu item definition.</summary>
        MENU_ITEM = 231,

        /// <summary>X-Ascend-PW-Warntime (Type 232). Integer. Password warning time in days.</summary>
        PW_WARNTIME = 232,

        /// <summary>X-Ascend-PW-Lifetime (Type 233). Integer. Password lifetime in days.</summary>
        PW_LIFETIME = 233,

        /// <summary>X-Ascend-IP-Direct (Type 234). IP address. IP direct address for routing.</summary>
        IP_DIRECT = 234,

        /// <summary>X-Ascend-PPP-VJ-Slot-Comp (Type 235). Integer. PPP VJ slot compression.</summary>
        PPP_VJ_SLOT_COMP = 235,

        /// <summary>X-Ascend-PPP-VJ-1172 (Type 236). Integer. PPP VJ RFC 1172 mode.</summary>
        PPP_VJ_1172 = 236,

        /// <summary>X-Ascend-PPP-Async-Map (Type 237). Integer. PPP async control character map.</summary>
        PPP_ASYNC_MAP = 237,

        /// <summary>X-Ascend-Third-Prompt (Type 238). String. Third login prompt string.</summary>
        THIRD_PROMPT = 238,

        /// <summary>X-Ascend-Send-Secret (Type 239). String. Send secret (CHAP).</summary>
        SEND_SECRET = 239,

        /// <summary>X-Ascend-Receive-Secret (Type 240). String. Receive secret (CHAP).</summary>
        RECEIVE_SECRET = 240,

        /// <summary>X-Ascend-IPX-Peer-Mode (Type 241). Integer. IPX peer mode.</summary>
        IPX_PEER_MODE = 241,

        /// <summary>X-Ascend-IP-Pool-Definition (Type 242). String. IP pool definition.</summary>
        IP_POOL_DEFINITION = 242,

        /// <summary>X-Ascend-Assign-IP-Pool (Type 243). Integer. Assign IP pool number.</summary>
        ASSIGN_IP_POOL = 243,

        /// <summary>X-Ascend-FR-Direct-2 (Type 244). Integer. Frame Relay direct mode (alternate).</summary>
        FR_DIRECT_2 = 244,

        /// <summary>X-Ascend-FR-Direct-Profile-2 (Type 245). String. Frame Relay direct profile (alternate).</summary>
        FR_DIRECT_PROFILE_2 = 245,

        /// <summary>X-Ascend-FR-Direct-DLCI-2 (Type 246). Integer. Frame Relay direct DLCI (alternate).</summary>
        FR_DIRECT_DLCI_2 = 246,

        /// <summary>X-Ascend-IPX-Node-Addr-2 (Type 247). String. IPX node address (alternate).</summary>
        IPX_NODE_ADDR_2 = 247,

        /// <summary>X-Ascend-IP-Pool-Chaining (Type 248). Integer. IP pool chaining flag.</summary>
        IP_POOL_CHAINING = 248,

        /// <summary>X-Ascend-Owner-IP-Addr (Type 249). IP address. Owner IP address.</summary>
        OWNER_IP_ADDR = 249,

        /// <summary>X-Ascend-IP-TOS (Type 250). Integer. IP Type of Service field.</summary>
        IP_TOS = 250,

        /// <summary>X-Ascend-IP-TOS-Precedence (Type 251). Integer. IP TOS precedence value.</summary>
        IP_TOS_PRECEDENCE = 251,

        /// <summary>X-Ascend-IP-TOS-Apply-To (Type 252). Integer. IP TOS apply-to target.</summary>
        IP_TOS_APPLY_TO = 252,

        /// <summary>X-Ascend-Filter (Type 253). String. Filter definition string.</summary>
        FILTER = 253,

        /// <summary>X-Ascend-Telnet-Profile (Type 254). String. Telnet profile name.</summary>
        TELNET_PROFILE = 254,

        /// <summary>X-Ascend-Client-Primary-WINS (Type 255). IP address. Client primary WINS address.</summary>
        CLIENT_PRIMARY_WINS = 255
    }

    /// <summary>
    /// X-Ascend-Tunneling-Protocol attribute values (Type 135).
    /// </summary>
    public enum ASCEND_ILLEGAL_TUNNELING_PROTOCOL
    {
        /// <summary>ATMP tunnelling.</summary>
        ATMP = 0,

        /// <summary>No tunnelling.</summary>
        NONE = 1
    }

    /// <summary>
    /// X-Ascend-Shared-Profile-Enable attribute values (Type 136).
    /// </summary>
    public enum ASCEND_ILLEGAL_SHARED_PROFILE_ENABLE
    {
        /// <summary>Shared profile disabled.</summary>
        DISABLED = 0,

        /// <summary>Shared profile enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// X-Ascend-Temporary-Rtes attribute values (Type 134).
    /// </summary>
    public enum ASCEND_ILLEGAL_TEMPORARY_RTES
    {
        /// <summary>Temporary routes disabled.</summary>
        DISABLED = 0,

        /// <summary>Temporary routes enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// X-Ascend-BACP-Enable attribute values (Type 141).
    /// </summary>
    public enum ASCEND_ILLEGAL_BACP_ENABLE
    {
        /// <summary>BACP disabled.</summary>
        DISABLED = 0,

        /// <summary>BACP enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// X-Ascend-Dialout-Allowed attribute values (Type 139).
    /// </summary>
    public enum ASCEND_ILLEGAL_DIALOUT_ALLOWED
    {
        /// <summary>Dialout not allowed.</summary>
        NOT_ALLOWED = 0,

        /// <summary>Dialout allowed.</summary>
        ALLOWED = 1
    }

    /// <summary>
    /// X-Ascend-DHCP-Reply attribute values (Type 155).
    /// </summary>
    public enum ASCEND_ILLEGAL_DHCP_REPLY
    {
        /// <summary>DHCP reply disabled.</summary>
        DISABLED = 0,

        /// <summary>DHCP reply enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// X-Ascend-Expect-Callback attribute values (Type 157).
    /// </summary>
    public enum ASCEND_ILLEGAL_EXPECT_CALLBACK
    {
        /// <summary>No callback expected.</summary>
        NO = 0,

        /// <summary>Callback expected.</summary>
        YES = 1
    }

    /// <summary>
    /// X-Ascend-Source-IP-Check attribute values (Type 166).
    /// </summary>
    public enum ASCEND_ILLEGAL_SOURCE_IP_CHECK
    {
        /// <summary>Source IP check disabled.</summary>
        DISABLED = 0,

        /// <summary>Source IP check enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// X-Ascend-IP-TOS-Apply-To attribute values (Type 252).
    /// </summary>
    public enum ASCEND_ILLEGAL_IP_TOS_APPLY_TO
    {
        /// <summary>Apply TOS to incoming packets.</summary>
        INCOMING = 0,

        /// <summary>Apply TOS to outgoing packets.</summary>
        OUTGOING = 1,

        /// <summary>Apply TOS to both directions.</summary>
        BOTH = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Ascend Communications
    /// "illegal" (IANA PEN 529) Vendor-Specific Attributes (VSAs), as defined in the
    /// FreeRADIUS <c>dictionary.ascend.illegal</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// These attributes were historically encoded directly in the top-level RADIUS
    /// attribute space (types 128–255) by older Ascend MAX/Pipeline equipment,
    /// rather than using the proper VSA encoding under vendor ID 529.
    /// FreeRADIUS remaps them into the vendor 529 namespace.
    /// </para>
    /// <para>
    /// All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 529</c>.
    /// </para>
    /// <para>
    /// <b>Note:</b> This class shares Vendor ID 529 with <see cref="AscendAttributes"/>
    /// but defines the "illegal" (X-Ascend-*) attributes from the separate
    /// <c>dictionary.ascend.illegal</c> dictionary file.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AscendIllegalAttributes.MaximumChannels(2));
    /// packet.SetAttribute(AscendIllegalAttributes.ClientPrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(AscendIllegalAttributes.MaximumCallDuration(3600));
    /// </code>
    /// </remarks>
    public static class AscendIllegalAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Ascend Communications (Lucent).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 529;

        #region Integer Attributes

        /// <summary>Creates an X-Ascend-Modem-PortNo attribute (Type 128).</summary>
        public static VendorSpecificAttributes ModemPortNo(int value) => CreateInteger(AscendIllegalAttributeType.MODEM_PORTNO, value);

        /// <summary>Creates an X-Ascend-Modem-SlotNo attribute (Type 129).</summary>
        public static VendorSpecificAttributes ModemSlotNo(int value) => CreateInteger(AscendIllegalAttributeType.MODEM_SLOTNO, value);

        /// <summary>Creates an X-Ascend-Modem-ShelfNo attribute (Type 130).</summary>
        public static VendorSpecificAttributes ModemShelfNo(int value) => CreateInteger(AscendIllegalAttributeType.MODEM_SHELFNO, value);

        /// <summary>Creates an X-Ascend-Call-Attempt-Limit attribute (Type 131).</summary>
        public static VendorSpecificAttributes CallAttemptLimit(int value) => CreateInteger(AscendIllegalAttributeType.CALL_ATTEMPT_LIMIT, value);

        /// <summary>Creates an X-Ascend-Call-Block-Duration attribute (Type 132).</summary>
        public static VendorSpecificAttributes CallBlockDuration(int value) => CreateInteger(AscendIllegalAttributeType.CALL_BLOCK_DURATION, value);

        /// <summary>Creates an X-Ascend-Maximum-Call-Duration attribute (Type 133).</summary>
        public static VendorSpecificAttributes MaximumCallDuration(int value) => CreateInteger(AscendIllegalAttributeType.MAXIMUM_CALL_DURATION, value);

        /// <summary>
        /// Creates an X-Ascend-Temporary-Rtes attribute (Type 134).
        /// </summary>
        /// <param name="value">See <see cref="ASCEND_ILLEGAL_TEMPORARY_RTES"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TemporaryRtes(ASCEND_ILLEGAL_TEMPORARY_RTES value) => CreateInteger(AscendIllegalAttributeType.TEMPORARY_RTES, (int)value);

        /// <summary>
        /// Creates an X-Ascend-Tunneling-Protocol attribute (Type 135).
        /// </summary>
        /// <param name="value">See <see cref="ASCEND_ILLEGAL_TUNNELING_PROTOCOL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelingProtocol(ASCEND_ILLEGAL_TUNNELING_PROTOCOL value) => CreateInteger(AscendIllegalAttributeType.TUNNELING_PROTOCOL, (int)value);

        /// <summary>
        /// Creates an X-Ascend-Shared-Profile-Enable attribute (Type 136).
        /// </summary>
        /// <param name="value">See <see cref="ASCEND_ILLEGAL_SHARED_PROFILE_ENABLE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SharedProfileEnable(ASCEND_ILLEGAL_SHARED_PROFILE_ENABLE value) => CreateInteger(AscendIllegalAttributeType.SHARED_PROFILE_ENABLE, (int)value);

        /// <summary>
        /// Creates an X-Ascend-Dialout-Allowed attribute (Type 139).
        /// </summary>
        /// <param name="value">See <see cref="ASCEND_ILLEGAL_DIALOUT_ALLOWED"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DialoutAllowed(ASCEND_ILLEGAL_DIALOUT_ALLOWED value) => CreateInteger(AscendIllegalAttributeType.DIALOUT_ALLOWED, (int)value);

        /// <summary>
        /// Creates an X-Ascend-BACP-Enable attribute (Type 141).
        /// </summary>
        /// <param name="value">See <see cref="ASCEND_ILLEGAL_BACP_ENABLE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BacpEnable(ASCEND_ILLEGAL_BACP_ENABLE value) => CreateInteger(AscendIllegalAttributeType.BACP_ENABLE, (int)value);

        /// <summary>Creates an X-Ascend-DHCP-Maximum-Leases attribute (Type 142).</summary>
        public static VendorSpecificAttributes DhcpMaximumLeases(int value) => CreateInteger(AscendIllegalAttributeType.DHCP_MAXIMUM_LEASES, value);

        /// <summary>Creates an X-Ascend-Client-Assign-DNS attribute (Type 145).</summary>
        public static VendorSpecificAttributes ClientAssignDns(int value) => CreateInteger(AscendIllegalAttributeType.CLIENT_ASSIGN_DNS, value);

        /// <summary>Creates an X-Ascend-User-Acct-Type attribute (Type 146).</summary>
        public static VendorSpecificAttributes UserAcctType(int value) => CreateInteger(AscendIllegalAttributeType.USER_ACCT_TYPE, value);

        /// <summary>Creates an X-Ascend-User-Acct-Port attribute (Type 148).</summary>
        public static VendorSpecificAttributes UserAcctPort(int value) => CreateInteger(AscendIllegalAttributeType.USER_ACCT_PORT, value);

        /// <summary>Creates an X-Ascend-User-Acct-Base attribute (Type 150).</summary>
        public static VendorSpecificAttributes UserAcctBase(int value) => CreateInteger(AscendIllegalAttributeType.USER_ACCT_BASE, value);

        /// <summary>Creates an X-Ascend-User-Acct-Time attribute (Type 151).</summary>
        public static VendorSpecificAttributes UserAcctTime(int value) => CreateInteger(AscendIllegalAttributeType.USER_ACCT_TIME, value);

        /// <summary>
        /// Creates an X-Ascend-DHCP-Reply attribute (Type 155).
        /// </summary>
        /// <param name="value">See <see cref="ASCEND_ILLEGAL_DHCP_REPLY"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DhcpReply(ASCEND_ILLEGAL_DHCP_REPLY value) => CreateInteger(AscendIllegalAttributeType.DHCP_REPLY, (int)value);

        /// <summary>Creates an X-Ascend-DHCP-Pool-Number attribute (Type 156).</summary>
        public static VendorSpecificAttributes DhcpPoolNumber(int value) => CreateInteger(AscendIllegalAttributeType.DHCP_POOL_NUMBER, value);

        /// <summary>
        /// Creates an X-Ascend-Expect-Callback attribute (Type 157).
        /// </summary>
        /// <param name="value">See <see cref="ASCEND_ILLEGAL_EXPECT_CALLBACK"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ExpectCallback(ASCEND_ILLEGAL_EXPECT_CALLBACK value) => CreateInteger(AscendIllegalAttributeType.EXPECT_CALLBACK, (int)value);

        /// <summary>Creates an X-Ascend-Event-Type attribute (Type 158).</summary>
        public static VendorSpecificAttributes EventType(int value) => CreateInteger(AscendIllegalAttributeType.EVENT_TYPE, value);

        /// <summary>Creates an X-Ascend-Multicast-Rate-Limit attribute (Type 160).</summary>
        public static VendorSpecificAttributes MulticastRateLimit(int value) => CreateInteger(AscendIllegalAttributeType.MULTICAST_RATE_LIMIT, value);

        /// <summary>Creates an X-Ascend-Multicast-Client attribute (Type 163).</summary>
        public static VendorSpecificAttributes MulticastClient(int value) => CreateInteger(AscendIllegalAttributeType.MULTICAST_CLIENT, value);

        /// <summary>Creates an X-Ascend-FR-Multicast-DLCI attribute (Type 164).</summary>
        public static VendorSpecificAttributes FrMulticastDlci(int value) => CreateInteger(AscendIllegalAttributeType.FR_MULTICAST_DLCI, value);

        /// <summary>
        /// Creates an X-Ascend-Source-IP-Check attribute (Type 166).
        /// </summary>
        /// <param name="value">See <see cref="ASCEND_ILLEGAL_SOURCE_IP_CHECK"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SourceIpCheck(ASCEND_ILLEGAL_SOURCE_IP_CHECK value) => CreateInteger(AscendIllegalAttributeType.SOURCE_IP_CHECK, (int)value);

        /// <summary>Creates an X-Ascend-FR-SetPrio attribute (Type 168).</summary>
        public static VendorSpecificAttributes FrSetPrio(int value) => CreateInteger(AscendIllegalAttributeType.FR_SETPRIO, value);

        /// <summary>Creates an X-Ascend-FR-DLCI attribute (Type 169).</summary>
        public static VendorSpecificAttributes FrDlci(int value) => CreateInteger(AscendIllegalAttributeType.FR_DLCI, value);

        /// <summary>Creates an X-Ascend-Handle-IPX attribute (Type 170).</summary>
        public static VendorSpecificAttributes HandleIpx(int value) => CreateInteger(AscendIllegalAttributeType.HANDLE_IPX, value);

        /// <summary>Creates an X-Ascend-Netware-timeout attribute (Type 171).</summary>
        public static VendorSpecificAttributes NetwareTimeout(int value) => CreateInteger(AscendIllegalAttributeType.NETWARE_TIMEOUT, value);

        /// <summary>Creates an X-Ascend-IPX-Alias attribute (Type 172).</summary>
        public static VendorSpecificAttributes IpxAlias(int value) => CreateInteger(AscendIllegalAttributeType.IPX_ALIAS, value);

        /// <summary>Creates an X-Ascend-Metric attribute (Type 173).</summary>
        public static VendorSpecificAttributes Metric(int value) => CreateInteger(AscendIllegalAttributeType.METRIC, value);

        /// <summary>Creates an X-Ascend-PRI-Number-Type attribute (Type 174).</summary>
        public static VendorSpecificAttributes PriNumberType(int value) => CreateInteger(AscendIllegalAttributeType.PRI_NUMBER_TYPE, value);

        /// <summary>Creates an X-Ascend-Route-IP attribute (Type 176).</summary>
        public static VendorSpecificAttributes RouteIp(int value) => CreateInteger(AscendIllegalAttributeType.ROUTE_IP, value);

        /// <summary>Creates an X-Ascend-Route-IPX attribute (Type 177).</summary>
        public static VendorSpecificAttributes RouteIpx(int value) => CreateInteger(AscendIllegalAttributeType.ROUTE_IPX, value);

        /// <summary>Creates an X-Ascend-Bridge attribute (Type 178).</summary>
        public static VendorSpecificAttributes Bridge(int value) => CreateInteger(AscendIllegalAttributeType.BRIDGE, value);

        /// <summary>Creates an X-Ascend-Send-Auth attribute (Type 179).</summary>
        public static VendorSpecificAttributes SendAuth(int value) => CreateInteger(AscendIllegalAttributeType.SEND_AUTH, value);

        /// <summary>Creates an X-Ascend-Link-Compression attribute (Type 181).</summary>
        public static VendorSpecificAttributes LinkCompression(int value) => CreateInteger(AscendIllegalAttributeType.LINK_COMPRESSION, value);

        /// <summary>Creates an X-Ascend-Target-Util attribute (Type 182).</summary>
        public static VendorSpecificAttributes TargetUtil(int value) => CreateInteger(AscendIllegalAttributeType.TARGET_UTIL, value);

        /// <summary>Creates an X-Ascend-Maximum-Channels attribute (Type 183).</summary>
        public static VendorSpecificAttributes MaximumChannels(int value) => CreateInteger(AscendIllegalAttributeType.MAXIMUM_CHANNELS, value);

        /// <summary>Creates an X-Ascend-Inc-Channel-Count attribute (Type 184).</summary>
        public static VendorSpecificAttributes IncChannelCount(int value) => CreateInteger(AscendIllegalAttributeType.INC_CHANNEL_COUNT, value);

        /// <summary>Creates an X-Ascend-Dec-Channel-Count attribute (Type 185).</summary>
        public static VendorSpecificAttributes DecChannelCount(int value) => CreateInteger(AscendIllegalAttributeType.DEC_CHANNEL_COUNT, value);

        /// <summary>Creates an X-Ascend-Seconds-Of-History attribute (Type 186).</summary>
        public static VendorSpecificAttributes SecondsOfHistory(int value) => CreateInteger(AscendIllegalAttributeType.SECONDS_OF_HISTORY, value);

        /// <summary>Creates an X-Ascend-History-Weigh-Type attribute (Type 187).</summary>
        public static VendorSpecificAttributes HistoryWeighType(int value) => CreateInteger(AscendIllegalAttributeType.HISTORY_WEIGH_TYPE, value);

        /// <summary>Creates an X-Ascend-Add-Seconds attribute (Type 188).</summary>
        public static VendorSpecificAttributes AddSeconds(int value) => CreateInteger(AscendIllegalAttributeType.ADD_SECONDS, value);

        /// <summary>Creates an X-Ascend-Remove-Seconds attribute (Type 189).</summary>
        public static VendorSpecificAttributes RemoveSeconds(int value) => CreateInteger(AscendIllegalAttributeType.REMOVE_SECONDS, value);

        /// <summary>Creates an X-Ascend-Idle-Limit attribute (Type 192).</summary>
        public static VendorSpecificAttributes IdleLimit(int value) => CreateInteger(AscendIllegalAttributeType.IDLE_LIMIT, value);

        /// <summary>Creates an X-Ascend-Preempt-Limit attribute (Type 193).</summary>
        public static VendorSpecificAttributes PreemptLimit(int value) => CreateInteger(AscendIllegalAttributeType.PREEMPT_LIMIT, value);

        /// <summary>Creates an X-Ascend-Callback attribute (Type 194).</summary>
        public static VendorSpecificAttributes Callback(int value) => CreateInteger(AscendIllegalAttributeType.CALLBACK, value);

        /// <summary>Creates an X-Ascend-Data-Svc attribute (Type 195).</summary>
        public static VendorSpecificAttributes DataSvc(int value) => CreateInteger(AscendIllegalAttributeType.DATA_SVC, value);

        /// <summary>Creates an X-Ascend-Force-56 attribute (Type 196).</summary>
        public static VendorSpecificAttributes Force56(int value) => CreateInteger(AscendIllegalAttributeType.FORCE_56, value);

        /// <summary>Creates an X-Ascend-Call-By-Call attribute (Type 198).</summary>
        public static VendorSpecificAttributes CallByCall(int value) => CreateInteger(AscendIllegalAttributeType.CALL_BY_CALL, value);

        /// <summary>Creates an X-Ascend-MPP-Idle-Percent attribute (Type 202).</summary>
        public static VendorSpecificAttributes MppIdlePercent(int value) => CreateInteger(AscendIllegalAttributeType.MPP_IDLE_PERCENT, value);

        /// <summary>Creates an X-Ascend-Xmit-Rate attribute (Type 203).</summary>
        public static VendorSpecificAttributes XmitRate(int value) => CreateInteger(AscendIllegalAttributeType.XMIT_RATE, value);

        /// <summary>Creates an X-Ascend-FR-Direct attribute (Type 204).</summary>
        public static VendorSpecificAttributes FrDirect(int value) => CreateInteger(AscendIllegalAttributeType.FR_DIRECT, value);

        /// <summary>Creates an X-Ascend-FR-Direct-DLCI attribute (Type 206).</summary>
        public static VendorSpecificAttributes FrDirectDlci(int value) => CreateInteger(AscendIllegalAttributeType.FR_DIRECT_DLCI, value);

        /// <summary>Creates an X-Ascend-Home-Agent-UDP-Port attribute (Type 211).</summary>
        public static VendorSpecificAttributes HomeAgentUdpPort(int value) => CreateInteger(AscendIllegalAttributeType.HOME_AGENT_UDP_PORT, value);

        /// <summary>Creates an X-Ascend-Multilink-ID attribute (Type 212).</summary>
        public static VendorSpecificAttributes MultilinkId(int value) => CreateInteger(AscendIllegalAttributeType.MULTILINK_ID, value);

        /// <summary>Creates an X-Ascend-Num-In-Multilink attribute (Type 213).</summary>
        public static VendorSpecificAttributes NumInMultilink(int value) => CreateInteger(AscendIllegalAttributeType.NUM_IN_MULTILINK, value);

        /// <summary>Creates an X-Ascend-Pre-Input-Octets attribute (Type 215).</summary>
        public static VendorSpecificAttributes PreInputOctets(int value) => CreateInteger(AscendIllegalAttributeType.PRE_INPUT_OCTETS, value);

        /// <summary>Creates an X-Ascend-Pre-Output-Octets attribute (Type 216).</summary>
        public static VendorSpecificAttributes PreOutputOctets(int value) => CreateInteger(AscendIllegalAttributeType.PRE_OUTPUT_OCTETS, value);

        /// <summary>Creates an X-Ascend-Pre-Input-Packets attribute (Type 217).</summary>
        public static VendorSpecificAttributes PreInputPackets(int value) => CreateInteger(AscendIllegalAttributeType.PRE_INPUT_PACKETS, value);

        /// <summary>Creates an X-Ascend-Pre-Output-Packets attribute (Type 218).</summary>
        public static VendorSpecificAttributes PreOutputPackets(int value) => CreateInteger(AscendIllegalAttributeType.PRE_OUTPUT_PACKETS, value);

        /// <summary>Creates an X-Ascend-Maximum-Time attribute (Type 219).</summary>
        public static VendorSpecificAttributes MaximumTime(int value) => CreateInteger(AscendIllegalAttributeType.MAXIMUM_TIME, value);

        /// <summary>Creates an X-Ascend-Disconnect-Cause attribute (Type 220).</summary>
        public static VendorSpecificAttributes DisconnectCause(int value) => CreateInteger(AscendIllegalAttributeType.DISCONNECT_CAUSE, value);

        /// <summary>Creates an X-Ascend-Connect-Progress attribute (Type 221).</summary>
        public static VendorSpecificAttributes ConnectProgress(int value) => CreateInteger(AscendIllegalAttributeType.CONNECT_PROGRESS, value);

        /// <summary>Creates an X-Ascend-Data-Rate attribute (Type 222).</summary>
        public static VendorSpecificAttributes DataRate(int value) => CreateInteger(AscendIllegalAttributeType.DATA_RATE, value);

        /// <summary>Creates an X-Ascend-PreSession-Time attribute (Type 223).</summary>
        public static VendorSpecificAttributes PreSessionTime(int value) => CreateInteger(AscendIllegalAttributeType.PRESESSION_TIME, value);

        /// <summary>Creates an X-Ascend-Token-Idle attribute (Type 224).</summary>
        public static VendorSpecificAttributes TokenIdle(int value) => CreateInteger(AscendIllegalAttributeType.TOKEN_IDLE, value);

        /// <summary>Creates an X-Ascend-Token-Immediate attribute (Type 225).</summary>
        public static VendorSpecificAttributes TokenImmediate(int value) => CreateInteger(AscendIllegalAttributeType.TOKEN_IMMEDIATE, value);

        /// <summary>Creates an X-Ascend-Require-Auth attribute (Type 226).</summary>
        public static VendorSpecificAttributes RequireAuth(int value) => CreateInteger(AscendIllegalAttributeType.REQUIRE_AUTH, value);

        /// <summary>Creates an X-Ascend-Token-Expiry attribute (Type 229).</summary>
        public static VendorSpecificAttributes TokenExpiry(int value) => CreateInteger(AscendIllegalAttributeType.TOKEN_EXPIRY, value);

        /// <summary>Creates an X-Ascend-PW-Warntime attribute (Type 232).</summary>
        public static VendorSpecificAttributes PwWarntime(int value) => CreateInteger(AscendIllegalAttributeType.PW_WARNTIME, value);

        /// <summary>Creates an X-Ascend-PW-Lifetime attribute (Type 233).</summary>
        public static VendorSpecificAttributes PwLifetime(int value) => CreateInteger(AscendIllegalAttributeType.PW_LIFETIME, value);

        /// <summary>Creates an X-Ascend-PPP-VJ-Slot-Comp attribute (Type 235).</summary>
        public static VendorSpecificAttributes PppVjSlotComp(int value) => CreateInteger(AscendIllegalAttributeType.PPP_VJ_SLOT_COMP, value);

        /// <summary>Creates an X-Ascend-PPP-VJ-1172 attribute (Type 236).</summary>
        public static VendorSpecificAttributes PppVj1172(int value) => CreateInteger(AscendIllegalAttributeType.PPP_VJ_1172, value);

        /// <summary>Creates an X-Ascend-PPP-Async-Map attribute (Type 237).</summary>
        public static VendorSpecificAttributes PppAsyncMap(int value) => CreateInteger(AscendIllegalAttributeType.PPP_ASYNC_MAP, value);

        /// <summary>Creates an X-Ascend-IPX-Peer-Mode attribute (Type 241).</summary>
        public static VendorSpecificAttributes IpxPeerMode(int value) => CreateInteger(AscendIllegalAttributeType.IPX_PEER_MODE, value);

        /// <summary>Creates an X-Ascend-Assign-IP-Pool attribute (Type 243).</summary>
        public static VendorSpecificAttributes AssignIpPool(int value) => CreateInteger(AscendIllegalAttributeType.ASSIGN_IP_POOL, value);

        /// <summary>Creates an X-Ascend-FR-Direct-2 attribute (Type 244).</summary>
        public static VendorSpecificAttributes FrDirect2(int value) => CreateInteger(AscendIllegalAttributeType.FR_DIRECT_2, value);

        /// <summary>Creates an X-Ascend-FR-Direct-DLCI-2 attribute (Type 246).</summary>
        public static VendorSpecificAttributes FrDirectDlci2(int value) => CreateInteger(AscendIllegalAttributeType.FR_DIRECT_DLCI_2, value);

        /// <summary>Creates an X-Ascend-IP-Pool-Chaining attribute (Type 248).</summary>
        public static VendorSpecificAttributes IpPoolChaining(int value) => CreateInteger(AscendIllegalAttributeType.IP_POOL_CHAINING, value);

        /// <summary>Creates an X-Ascend-IP-TOS attribute (Type 250).</summary>
        public static VendorSpecificAttributes IpTos(int value) => CreateInteger(AscendIllegalAttributeType.IP_TOS, value);

        /// <summary>Creates an X-Ascend-IP-TOS-Precedence attribute (Type 251).</summary>
        public static VendorSpecificAttributes IpTosPrecedence(int value) => CreateInteger(AscendIllegalAttributeType.IP_TOS_PRECEDENCE, value);

        /// <summary>
        /// Creates an X-Ascend-IP-TOS-Apply-To attribute (Type 252).
        /// </summary>
        /// <param name="value">See <see cref="ASCEND_ILLEGAL_IP_TOS_APPLY_TO"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpTosApplyTo(ASCEND_ILLEGAL_IP_TOS_APPLY_TO value) => CreateInteger(AscendIllegalAttributeType.IP_TOS_APPLY_TO, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates an X-Ascend-Primary-Home-Agent attribute (Type 137).</summary>
        /// <param name="value">The primary home agent address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrimaryHomeAgent(string value) => CreateString(AscendIllegalAttributeType.PRIMARY_HOME_AGENT, value);

        /// <summary>Creates an X-Ascend-Secondary-Home-Agent attribute (Type 138).</summary>
        /// <param name="value">The secondary home agent address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SecondaryHomeAgent(string value) => CreateString(AscendIllegalAttributeType.SECONDARY_HOME_AGENT, value);

        /// <summary>Creates an X-Ascend-User-Acct-Key attribute (Type 149).</summary>
        /// <param name="value">The user accounting key. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserAcctKey(string value) => CreateString(AscendIllegalAttributeType.USER_ACCT_KEY, value);

        /// <summary>Creates an X-Ascend-Assign-IP-Global-Pool attribute (Type 154).</summary>
        /// <param name="value">The global IP pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AssignIpGlobalPool(string value) => CreateString(AscendIllegalAttributeType.ASSIGN_IP_GLOBAL_POOL, value);

        /// <summary>Creates an X-Ascend-Session-Svr-Key attribute (Type 159).</summary>
        /// <param name="value">The session server key. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionSvrKey(string value) => CreateString(AscendIllegalAttributeType.SESSION_SVR_KEY, value);

        /// <summary>Creates an X-Ascend-FR-SVC-Addr attribute (Type 165).</summary>
        /// <param name="value">The FR SVC address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FrSvcAddr(string value) => CreateString(AscendIllegalAttributeType.FR_SVC_ADDR, value);

        /// <summary>Creates an X-Ascend-IS-Number attribute (Type 167).</summary>
        /// <param name="value">The IS number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IsNumber(string value) => CreateString(AscendIllegalAttributeType.IS_NUMBER, value);

        /// <summary>Creates an X-Ascend-Dial-Number attribute (Type 175).</summary>
        /// <param name="value">The dial number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DialNumber(string value) => CreateString(AscendIllegalAttributeType.DIAL_NUMBER, value);

        /// <summary>Creates an X-Ascend-Send-Passwd attribute (Type 180).</summary>
        /// <param name="value">The send password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SendPasswd(string value) => CreateString(AscendIllegalAttributeType.SEND_PASSWD, value);

        /// <summary>Creates an X-Ascend-Billing-Number attribute (Type 197).</summary>
        /// <param name="value">The billing number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BillingNumber(string value) => CreateString(AscendIllegalAttributeType.BILLING_NUMBER, value);

        /// <summary>Creates an X-Ascend-Transit-Number attribute (Type 199).</summary>
        /// <param name="value">The transit number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TransitNumber(string value) => CreateString(AscendIllegalAttributeType.TRANSIT_NUMBER, value);

        /// <summary>Creates an X-Ascend-Host-Info attribute (Type 200).</summary>
        /// <param name="value">The host information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HostInfo(string value) => CreateString(AscendIllegalAttributeType.HOST_INFO, value);

        /// <summary>Creates an X-Ascend-FR-Direct-Profile attribute (Type 205).</summary>
        /// <param name="value">The FR direct profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FrDirectProfile(string value) => CreateString(AscendIllegalAttributeType.FR_DIRECT_PROFILE, value);

        /// <summary>Creates an X-Ascend-IPX-Node-Addr attribute (Type 207).</summary>
        /// <param name="value">The IPX node address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpxNodeAddr(string value) => CreateString(AscendIllegalAttributeType.IPX_NODE_ADDR, value);

        /// <summary>Creates an X-Ascend-Home-Agent-Password attribute (Type 209).</summary>
        /// <param name="value">The home agent password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HomeAgentPassword(string value) => CreateString(AscendIllegalAttributeType.HOME_AGENT_PASSWORD, value);

        /// <summary>Creates an X-Ascend-Home-Network-Name attribute (Type 210).</summary>
        /// <param name="value">The home network name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HomeNetworkName(string value) => CreateString(AscendIllegalAttributeType.HOME_NETWORK_NAME, value);

        /// <summary>Creates an X-Ascend-Number-Sessions attribute (Type 227).</summary>
        /// <param name="value">The number of sessions. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NumberSessions(string value) => CreateString(AscendIllegalAttributeType.NUMBER_SESSIONS, value);

        /// <summary>Creates an X-Ascend-Authen-Alias attribute (Type 228).</summary>
        /// <param name="value">The authentication alias. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthenAlias(string value) => CreateString(AscendIllegalAttributeType.AUTHEN_ALIAS, value);

        /// <summary>Creates an X-Ascend-Menu-Selector attribute (Type 230).</summary>
        /// <param name="value">The menu selector. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MenuSelector(string value) => CreateString(AscendIllegalAttributeType.MENU_SELECTOR, value);

        /// <summary>Creates an X-Ascend-Menu-Item attribute (Type 231).</summary>
        /// <param name="value">The menu item definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MenuItem(string value) => CreateString(AscendIllegalAttributeType.MENU_ITEM, value);

        /// <summary>Creates an X-Ascend-Third-Prompt attribute (Type 238).</summary>
        /// <param name="value">The third login prompt. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ThirdPrompt(string value) => CreateString(AscendIllegalAttributeType.THIRD_PROMPT, value);

        /// <summary>Creates an X-Ascend-Send-Secret attribute (Type 239).</summary>
        /// <param name="value">The send secret. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SendSecret(string value) => CreateString(AscendIllegalAttributeType.SEND_SECRET, value);

        /// <summary>Creates an X-Ascend-Receive-Secret attribute (Type 240).</summary>
        /// <param name="value">The receive secret. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ReceiveSecret(string value) => CreateString(AscendIllegalAttributeType.RECEIVE_SECRET, value);

        /// <summary>Creates an X-Ascend-IP-Pool-Definition attribute (Type 242).</summary>
        /// <param name="value">The IP pool definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPoolDefinition(string value) => CreateString(AscendIllegalAttributeType.IP_POOL_DEFINITION, value);

        /// <summary>Creates an X-Ascend-FR-Direct-Profile-2 attribute (Type 245).</summary>
        /// <param name="value">The FR direct profile (alternate). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FrDirectProfile2(string value) => CreateString(AscendIllegalAttributeType.FR_DIRECT_PROFILE_2, value);

        /// <summary>Creates an X-Ascend-IPX-Node-Addr-2 attribute (Type 247).</summary>
        /// <param name="value">The IPX node address (alternate). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpxNodeAddr2(string value) => CreateString(AscendIllegalAttributeType.IPX_NODE_ADDR_2, value);

        /// <summary>Creates an X-Ascend-Filter attribute (Type 253).</summary>
        /// <param name="value">The filter definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Filter(string value) => CreateString(AscendIllegalAttributeType.FILTER, value);

        /// <summary>Creates an X-Ascend-Telnet-Profile attribute (Type 254).</summary>
        /// <param name="value">The Telnet profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TelnetProfile(string value) => CreateString(AscendIllegalAttributeType.TELNET_PROFILE, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates an X-Ascend-Client-Gateway attribute (Type 140).</summary>
        public static VendorSpecificAttributes ClientGateway(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.CLIENT_GATEWAY, value);

        /// <summary>Creates an X-Ascend-Client-Primary-DNS attribute (Type 143).</summary>
        public static VendorSpecificAttributes ClientPrimaryDns(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.CLIENT_PRIMARY_DNS, value);

        /// <summary>Creates an X-Ascend-Client-Secondary-DNS attribute (Type 144).</summary>
        public static VendorSpecificAttributes ClientSecondaryDns(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.CLIENT_SECONDARY_DNS, value);

        /// <summary>Creates an X-Ascend-User-Acct-Host attribute (Type 147).</summary>
        public static VendorSpecificAttributes UserAcctHost(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.USER_ACCT_HOST, value);

        /// <summary>Creates an X-Ascend-Assign-IP-Client attribute (Type 152).</summary>
        public static VendorSpecificAttributes AssignIpClient(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.ASSIGN_IP_CLIENT, value);

        /// <summary>Creates an X-Ascend-Assign-IP-Server attribute (Type 153).</summary>
        public static VendorSpecificAttributes AssignIpServer(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.ASSIGN_IP_SERVER, value);

        /// <summary>Creates an X-Ascend-IF-Netmask attribute (Type 161).</summary>
        public static VendorSpecificAttributes IfNetmask(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.IF_NETMASK, value);

        /// <summary>Creates an X-Ascend-Remote-Addr attribute (Type 162).</summary>
        public static VendorSpecificAttributes RemoteAddr(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.REMOTE_ADDR, value);

        /// <summary>Creates an X-Ascend-PPP-Address attribute (Type 201).</summary>
        public static VendorSpecificAttributes PppAddress(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.PPP_ADDRESS, value);

        /// <summary>Creates an X-Ascend-Home-Agent-IP-Addr attribute (Type 208).</summary>
        public static VendorSpecificAttributes HomeAgentIpAddr(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.HOME_AGENT_IP_ADDR, value);

        /// <summary>Creates an X-Ascend-First-Dest attribute (Type 214).</summary>
        public static VendorSpecificAttributes FirstDest(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.FIRST_DEST, value);

        /// <summary>Creates an X-Ascend-IP-Direct attribute (Type 234).</summary>
        public static VendorSpecificAttributes IpDirect(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.IP_DIRECT, value);

        /// <summary>Creates an X-Ascend-Owner-IP-Addr attribute (Type 249).</summary>
        public static VendorSpecificAttributes OwnerIpAddr(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.OWNER_IP_ADDR, value);

        /// <summary>Creates an X-Ascend-Client-Primary-WINS attribute (Type 255).</summary>
        public static VendorSpecificAttributes ClientPrimaryWins(IPAddress value) => CreateIpv4(AscendIllegalAttributeType.CLIENT_PRIMARY_WINS, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates an X-Ascend-Data-Filter attribute (Type 190).</summary>
        /// <param name="value">The data filter definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DataFilter(byte[] value) => CreateOctets(AscendIllegalAttributeType.DATA_FILTER, value);

        /// <summary>Creates an X-Ascend-Call-Filter attribute (Type 191).</summary>
        /// <param name="value">The call filter definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallFilter(byte[] value) => CreateOctets(AscendIllegalAttributeType.CALL_FILTER, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(AscendIllegalAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(AscendIllegalAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(AscendIllegalAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        private static VendorSpecificAttributes CreateIpv4(AscendIllegalAttributeType type, IPAddress value)
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