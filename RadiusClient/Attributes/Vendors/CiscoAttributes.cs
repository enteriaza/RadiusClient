using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Cisco Systems (IANA PEN 9) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.cisco</c>.
    /// </summary>
    public enum CiscoAttributeType : byte
    {
        /// <summary>Cisco-AVPair (Type 1). String. General-purpose attribute-value pair.</summary>
        AVPAIR = 1,

        /// <summary>Cisco-NAS-Port (Type 2). String. NAS port information string.</summary>
        NAS_PORT = 2,

        /// <summary>Cisco-Fax-Account-Id-Origin (Type 3). String. Fax account ID origin.</summary>
        FAX_ACCOUNT_ID_ORIGIN = 3,

        /// <summary>Cisco-Fax-Msg-Id (Type 4). String. Fax message identifier.</summary>
        FAX_MSG_ID = 4,

        /// <summary>Cisco-Fax-Pages (Type 5). String. Fax page count.</summary>
        FAX_PAGES = 5,

        /// <summary>Cisco-Fax-Coverpage-Flag (Type 6). String. Fax cover page flag.</summary>
        FAX_COVERPAGE_FLAG = 6,

        /// <summary>Cisco-Fax-Modem-Time (Type 7). String. Fax modem time.</summary>
        FAX_MODEM_TIME = 7,

        /// <summary>Cisco-Fax-Connect-Speed (Type 8). String. Fax connection speed.</summary>
        FAX_CONNECT_SPEED = 8,

        /// <summary>Cisco-Fax-Recipient-Count (Type 9). String. Fax recipient count.</summary>
        FAX_RECIPIENT_COUNT = 9,

        /// <summary>Cisco-Fax-Process-Abort-Flag (Type 10). String. Fax process abort flag.</summary>
        FAX_PROCESS_ABORT_FLAG = 10,

        /// <summary>Cisco-Fax-Dsn-Address (Type 11). String. Fax DSN address.</summary>
        FAX_DSN_ADDRESS = 11,

        /// <summary>Cisco-Fax-Dsn-Flag (Type 12). String. Fax DSN flag.</summary>
        FAX_DSN_FLAG = 12,

        /// <summary>Cisco-Fax-Mdn-Address (Type 13). String. Fax MDN address.</summary>
        FAX_MDN_ADDRESS = 13,

        /// <summary>Cisco-Fax-Mdn-Flag (Type 14). String. Fax MDN flag.</summary>
        FAX_MDN_FLAG = 14,

        /// <summary>Cisco-Fax-Auth-Status (Type 15). String. Fax authentication status.</summary>
        FAX_AUTH_STATUS = 15,

        /// <summary>Cisco-Email-Server-Address (Type 16). String. E-mail server address.</summary>
        EMAIL_SERVER_ADDRESS = 16,

        /// <summary>Cisco-Email-Server-Ack-Flag (Type 17). String. E-mail server acknowledgement flag.</summary>
        EMAIL_SERVER_ACK_FLAG = 17,

        /// <summary>Cisco-Gateway-Id (Type 18). String. Gateway identifier.</summary>
        GATEWAY_ID = 18,

        /// <summary>Cisco-Call-Type (Type 19). String. Call type string.</summary>
        CALL_TYPE = 19,

        /// <summary>Cisco-Port-Used (Type 20). String. Port used for connection.</summary>
        PORT_USED = 20,

        /// <summary>Cisco-Abort-Cause (Type 21). String. Abort cause string.</summary>
        ABORT_CAUSE = 21,

        /// <summary>Cisco-Multilink-ID (Type 187). Integer. Multilink bundle identifier.</summary>
        MULTILINK_ID = 187,

        /// <summary>Cisco-Num-In-Multilink (Type 188). Integer. Number of links in multilink bundle.</summary>
        NUM_IN_MULTILINK = 188,

        /// <summary>Cisco-Pre-Input-Octets (Type 190). Integer. Pre-session input octets.</summary>
        PRE_INPUT_OCTETS = 190,

        /// <summary>Cisco-Pre-Output-Octets (Type 191). Integer. Pre-session output octets.</summary>
        PRE_OUTPUT_OCTETS = 191,

        /// <summary>Cisco-Pre-Input-Packets (Type 192). Integer. Pre-session input packets.</summary>
        PRE_INPUT_PACKETS = 192,

        /// <summary>Cisco-Pre-Output-Packets (Type 193). Integer. Pre-session output packets.</summary>
        PRE_OUTPUT_PACKETS = 193,

        /// <summary>Cisco-Maximum-Time (Type 194). Integer. Maximum session time in seconds.</summary>
        MAXIMUM_TIME = 194,

        /// <summary>Cisco-Disconnect-Cause (Type 195). Integer. Disconnect cause code.</summary>
        DISCONNECT_CAUSE = 195,

        /// <summary>Cisco-Data-Rate (Type 197). Integer. Data rate in bps.</summary>
        DATA_RATE = 197,

        /// <summary>Cisco-PreSession-Time (Type 198). Integer. Pre-session time in seconds.</summary>
        PRESESSION_TIME = 198,

        /// <summary>Cisco-PW-Lifetime (Type 208). Integer. Password lifetime in days.</summary>
        PW_LIFETIME = 208,

        /// <summary>Cisco-IP-Direct (Type 209). Integer. IP direct flag.</summary>
        IP_DIRECT = 209,

        /// <summary>Cisco-PPP-VJ-Slot-Comp (Type 210). Integer. PPP VJ slot compression.</summary>
        PPP_VJ_SLOT_COMP = 210,

        /// <summary>Cisco-PPP-Async-Map (Type 212). Integer. PPP async control character map.</summary>
        PPP_ASYNC_MAP = 212,

        /// <summary>Cisco-IP-Pool-Definition (Type 217). String. IP pool definition.</summary>
        IP_POOL_DEFINITION = 217,

        /// <summary>Cisco-Assign-IP-Pool (Type 218). Integer. Assign IP pool number.</summary>
        ASSIGN_IP_POOL = 218,

        /// <summary>Cisco-Route-IP (Type 228). Integer. Route IP traffic flag.</summary>
        ROUTE_IP = 228,

        /// <summary>Cisco-Link-Compression (Type 233). Integer. Link compression type.</summary>
        LINK_COMPRESSION = 233,

        /// <summary>Cisco-Target-Util (Type 234). Integer. Target utilisation percentage.</summary>
        TARGET_UTIL = 234,

        /// <summary>Cisco-Maximum-Channels (Type 235). Integer. Maximum channels.</summary>
        MAXIMUM_CHANNELS = 235,

        /// <summary>Cisco-Data-Filter (Type 242). Integer. Data filter number.</summary>
        DATA_FILTER = 242,

        /// <summary>Cisco-Call-Filter (Type 243). Integer. Call filter number.</summary>
        CALL_FILTER = 243,

        /// <summary>Cisco-Idle-Limit (Type 244). Integer. Idle limit in seconds.</summary>
        IDLE_LIMIT = 244,

        /// <summary>Cisco-Xmit-Rate (Type 255). Integer. Transmit rate in bps.</summary>
        XMIT_RATE = 255,

        /// <summary>Cisco-Account-Info (Type 250). String. Account information string.</summary>
        ACCOUNT_INFO = 250,

        /// <summary>Cisco-Service-Info (Type 251). String. Service information string.</summary>
        SERVICE_INFO = 251,

        /// <summary>Cisco-Command-Code (Type 252). String. Command code string.</summary>
        COMMAND_CODE = 252,

        /// <summary>Cisco-Control-Info (Type 253). String. Control information string.</summary>
        CONTROL_INFO = 253
    }

    /// <summary>
    /// Cisco-Disconnect-Cause attribute values (Type 195).
    /// </summary>
    public enum CISCO_DISCONNECT_CAUSE
    {
        /// <summary>No reason.</summary>
        NO_REASON = 0,

        /// <summary>No disconnect.</summary>
        NO_DISCONNECT = 1,

        /// <summary>Unknown.</summary>
        UNKNOWN = 2,

        /// <summary>Call disconnected.</summary>
        CALL_DISCONNECT = 3,

        /// <summary>CLID authentication failure.</summary>
        CLID_AUTHENTICATION_FAILURE = 4,

        /// <summary>No modem available.</summary>
        NO_MODEM_AVAILABLE = 9,

        /// <summary>No carrier.</summary>
        NO_CARRIER = 10,

        /// <summary>Lost carrier.</summary>
        LOST_CARRIER = 11,

        /// <summary>No detected result codes.</summary>
        NO_DETECTED_RESULT_CODES = 12,

        /// <summary>User ends session.</summary>
        USER_ENDS_SESSION = 20,

        /// <summary>Idle timeout.</summary>
        IDLE_TIMEOUT = 21,

        /// <summary>Exit Telnet session.</summary>
        EXIT_TELNET_SESSION = 22,

        /// <summary>No remote IP address.</summary>
        NO_REMOTE_IP_ADDR = 23,

        /// <summary>Full/no remote IP address.</summary>
        NO_REMOTE_IP_ADDR_FULL = 24,

        /// <summary>No port available.</summary>
        NO_PORT_AVAILABLE = 25,

        /// <summary>Session timeout.</summary>
        SESSION_TIMEOUT = 100,

        /// <summary>PPP LCP timeout.</summary>
        PPP_LCP_TIMEOUT = 102,

        /// <summary>PPP LCP negotiation failed.</summary>
        PPP_LCP_NEGOTIATION_FAILED = 103,

        /// <summary>PPP PAP authentication failed.</summary>
        PPP_PAP_AUTH_FAILED = 104,

        /// <summary>PPP CHAP authentication failed.</summary>
        PPP_CHAP_AUTH_FAILED = 105,

        /// <summary>PPP remote terminated.</summary>
        PPP_REMOTE_TERMINATED = 106,

        /// <summary>Service unavailable.</summary>
        SERVICE_UNAVAILABLE = 150,

        /// <summary>Callback.</summary>
        CALLBACK = 151,

        /// <summary>User error.</summary>
        USER_ERROR = 152,

        /// <summary>Host request.</summary>
        HOST_REQUEST = 153
    }

    /// <summary>
    /// Cisco-Route-IP attribute values (Type 228).
    /// </summary>
    public enum CISCO_ROUTE_IP
    {
        /// <summary>Route IP disabled.</summary>
        NO = 0,

        /// <summary>Route IP enabled.</summary>
        YES = 1
    }

    /// <summary>
    /// Cisco-Link-Compression attribute values (Type 233).
    /// </summary>
    public enum CISCO_LINK_COMPRESSION
    {
        /// <summary>No compression.</summary>
        NONE = 0,

        /// <summary>Stac compression.</summary>
        STAC = 1,

        /// <summary>Stac draft-9 compression.</summary>
        STAC_DRAFT_9 = 2,

        /// <summary>MS-Stac (MPPC) compression.</summary>
        MS_STAC = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Cisco Systems
    /// (IANA PEN 9) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.cisco</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Cisco's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 9</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Cisco IOS routers, ASA firewalls, ISE,
    /// ACS, wireless LAN controllers, voice gateways, and other Cisco platforms
    /// for RADIUS-based AAA. The <c>Cisco-AVPair</c> attribute (Type 1) is the
    /// most commonly used, supporting flexible key=value configuration for
    /// features such as <c>shell:priv-lvl=15</c>, <c>ip:inacl#</c> filters,
    /// VPN group policies, and ISE authorisation profiles.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CiscoAttributes.AvPair("shell:priv-lvl=15"));
    /// packet.SetAttribute(CiscoAttributes.AvPair("ip:inacl#1=permit ip any any"));
    /// packet.SetAttribute(CiscoAttributes.IdleLimit(300));
    /// packet.SetAttribute(CiscoAttributes.DisconnectCause(CISCO_DISCONNECT_CAUSE.SESSION_TIMEOUT));
    /// </code>
    /// </remarks>
    public static class CiscoAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Cisco Systems.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 9;

        #region Integer Attributes

        /// <summary>Creates a Cisco-Multilink-ID attribute (Type 187).</summary>
        public static VendorSpecificAttributes MultilinkId(int value) => CreateInteger(CiscoAttributeType.MULTILINK_ID, value);

        /// <summary>Creates a Cisco-Num-In-Multilink attribute (Type 188).</summary>
        public static VendorSpecificAttributes NumInMultilink(int value) => CreateInteger(CiscoAttributeType.NUM_IN_MULTILINK, value);

        /// <summary>Creates a Cisco-Pre-Input-Octets attribute (Type 190).</summary>
        public static VendorSpecificAttributes PreInputOctets(int value) => CreateInteger(CiscoAttributeType.PRE_INPUT_OCTETS, value);

        /// <summary>Creates a Cisco-Pre-Output-Octets attribute (Type 191).</summary>
        public static VendorSpecificAttributes PreOutputOctets(int value) => CreateInteger(CiscoAttributeType.PRE_OUTPUT_OCTETS, value);

        /// <summary>Creates a Cisco-Pre-Input-Packets attribute (Type 192).</summary>
        public static VendorSpecificAttributes PreInputPackets(int value) => CreateInteger(CiscoAttributeType.PRE_INPUT_PACKETS, value);

        /// <summary>Creates a Cisco-Pre-Output-Packets attribute (Type 193).</summary>
        public static VendorSpecificAttributes PreOutputPackets(int value) => CreateInteger(CiscoAttributeType.PRE_OUTPUT_PACKETS, value);

        /// <summary>Creates a Cisco-Maximum-Time attribute (Type 194).</summary>
        /// <param name="value">The maximum session time in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaximumTime(int value) => CreateInteger(CiscoAttributeType.MAXIMUM_TIME, value);

        /// <summary>
        /// Creates a Cisco-Disconnect-Cause attribute (Type 195) with the specified cause.
        /// </summary>
        /// <param name="value">The disconnect cause. See <see cref="CISCO_DISCONNECT_CAUSE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DisconnectCause(CISCO_DISCONNECT_CAUSE value) => CreateInteger(CiscoAttributeType.DISCONNECT_CAUSE, (int)value);

        /// <summary>Creates a Cisco-Data-Rate attribute (Type 197).</summary>
        /// <param name="value">The data rate in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DataRate(int value) => CreateInteger(CiscoAttributeType.DATA_RATE, value);

        /// <summary>Creates a Cisco-PreSession-Time attribute (Type 198).</summary>
        public static VendorSpecificAttributes PreSessionTime(int value) => CreateInteger(CiscoAttributeType.PRESESSION_TIME, value);

        /// <summary>Creates a Cisco-PW-Lifetime attribute (Type 208).</summary>
        /// <param name="value">The password lifetime in days.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PwLifetime(int value) => CreateInteger(CiscoAttributeType.PW_LIFETIME, value);

        /// <summary>Creates a Cisco-IP-Direct attribute (Type 209).</summary>
        public static VendorSpecificAttributes IpDirect(int value) => CreateInteger(CiscoAttributeType.IP_DIRECT, value);

        /// <summary>Creates a Cisco-PPP-VJ-Slot-Comp attribute (Type 210).</summary>
        public static VendorSpecificAttributes PppVjSlotComp(int value) => CreateInteger(CiscoAttributeType.PPP_VJ_SLOT_COMP, value);

        /// <summary>Creates a Cisco-PPP-Async-Map attribute (Type 212).</summary>
        public static VendorSpecificAttributes PppAsyncMap(int value) => CreateInteger(CiscoAttributeType.PPP_ASYNC_MAP, value);

        /// <summary>Creates a Cisco-Assign-IP-Pool attribute (Type 218).</summary>
        /// <param name="value">The IP pool number to assign.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AssignIpPool(int value) => CreateInteger(CiscoAttributeType.ASSIGN_IP_POOL, value);

        /// <summary>
        /// Creates a Cisco-Route-IP attribute (Type 228) with the specified setting.
        /// </summary>
        /// <param name="value">The route IP setting. See <see cref="CISCO_ROUTE_IP"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RouteIp(CISCO_ROUTE_IP value) => CreateInteger(CiscoAttributeType.ROUTE_IP, (int)value);

        /// <summary>
        /// Creates a Cisco-Link-Compression attribute (Type 233) with the specified type.
        /// </summary>
        /// <param name="value">The link compression type. See <see cref="CISCO_LINK_COMPRESSION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LinkCompression(CISCO_LINK_COMPRESSION value) => CreateInteger(CiscoAttributeType.LINK_COMPRESSION, (int)value);

        /// <summary>Creates a Cisco-Target-Util attribute (Type 234).</summary>
        public static VendorSpecificAttributes TargetUtil(int value) => CreateInteger(CiscoAttributeType.TARGET_UTIL, value);

        /// <summary>Creates a Cisco-Maximum-Channels attribute (Type 235).</summary>
        public static VendorSpecificAttributes MaximumChannels(int value) => CreateInteger(CiscoAttributeType.MAXIMUM_CHANNELS, value);

        /// <summary>Creates a Cisco-Data-Filter attribute (Type 242).</summary>
        public static VendorSpecificAttributes DataFilter(int value) => CreateInteger(CiscoAttributeType.DATA_FILTER, value);

        /// <summary>Creates a Cisco-Call-Filter attribute (Type 243).</summary>
        public static VendorSpecificAttributes CallFilter(int value) => CreateInteger(CiscoAttributeType.CALL_FILTER, value);

        /// <summary>Creates a Cisco-Idle-Limit attribute (Type 244).</summary>
        /// <param name="value">The idle limit in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleLimit(int value) => CreateInteger(CiscoAttributeType.IDLE_LIMIT, value);

        /// <summary>Creates a Cisco-Xmit-Rate attribute (Type 255).</summary>
        /// <param name="value">The transmit rate in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes XmitRate(int value) => CreateInteger(CiscoAttributeType.XMIT_RATE, value);

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Cisco-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">
        /// The attribute-value pair string (e.g. <c>"shell:priv-lvl=15"</c>,
        /// <c>"ip:inacl#1=permit ip any any"</c>). Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(CiscoAttributeType.AVPAIR, value);

        /// <summary>Creates a Cisco-NAS-Port attribute (Type 2).</summary>
        /// <param name="value">The NAS port information string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NasPort(string value) => CreateString(CiscoAttributeType.NAS_PORT, value);

        /// <summary>Creates a Cisco-Fax-Account-Id-Origin attribute (Type 3).</summary>
        /// <param name="value">The fax account ID origin. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxAccountIdOrigin(string value) => CreateString(CiscoAttributeType.FAX_ACCOUNT_ID_ORIGIN, value);

        /// <summary>Creates a Cisco-Fax-Msg-Id attribute (Type 4).</summary>
        /// <param name="value">The fax message identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxMsgId(string value) => CreateString(CiscoAttributeType.FAX_MSG_ID, value);

        /// <summary>Creates a Cisco-Fax-Pages attribute (Type 5).</summary>
        /// <param name="value">The fax page count. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxPages(string value) => CreateString(CiscoAttributeType.FAX_PAGES, value);

        /// <summary>Creates a Cisco-Fax-Coverpage-Flag attribute (Type 6).</summary>
        /// <param name="value">The fax cover page flag. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxCoverpageFlag(string value) => CreateString(CiscoAttributeType.FAX_COVERPAGE_FLAG, value);

        /// <summary>Creates a Cisco-Fax-Modem-Time attribute (Type 7).</summary>
        /// <param name="value">The fax modem time. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxModemTime(string value) => CreateString(CiscoAttributeType.FAX_MODEM_TIME, value);

        /// <summary>Creates a Cisco-Fax-Connect-Speed attribute (Type 8).</summary>
        /// <param name="value">The fax connection speed. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxConnectSpeed(string value) => CreateString(CiscoAttributeType.FAX_CONNECT_SPEED, value);

        /// <summary>Creates a Cisco-Fax-Recipient-Count attribute (Type 9).</summary>
        /// <param name="value">The fax recipient count. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxRecipientCount(string value) => CreateString(CiscoAttributeType.FAX_RECIPIENT_COUNT, value);

        /// <summary>Creates a Cisco-Fax-Process-Abort-Flag attribute (Type 10).</summary>
        /// <param name="value">The fax process abort flag. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxProcessAbortFlag(string value) => CreateString(CiscoAttributeType.FAX_PROCESS_ABORT_FLAG, value);

        /// <summary>Creates a Cisco-Fax-Dsn-Address attribute (Type 11).</summary>
        /// <param name="value">The fax DSN address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxDsnAddress(string value) => CreateString(CiscoAttributeType.FAX_DSN_ADDRESS, value);

        /// <summary>Creates a Cisco-Fax-Dsn-Flag attribute (Type 12).</summary>
        /// <param name="value">The fax DSN flag. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxDsnFlag(string value) => CreateString(CiscoAttributeType.FAX_DSN_FLAG, value);

        /// <summary>Creates a Cisco-Fax-Mdn-Address attribute (Type 13).</summary>
        /// <param name="value">The fax MDN address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxMdnAddress(string value) => CreateString(CiscoAttributeType.FAX_MDN_ADDRESS, value);

        /// <summary>Creates a Cisco-Fax-Mdn-Flag attribute (Type 14).</summary>
        /// <param name="value">The fax MDN flag. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxMdnFlag(string value) => CreateString(CiscoAttributeType.FAX_MDN_FLAG, value);

        /// <summary>Creates a Cisco-Fax-Auth-Status attribute (Type 15).</summary>
        /// <param name="value">The fax authentication status. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FaxAuthStatus(string value) => CreateString(CiscoAttributeType.FAX_AUTH_STATUS, value);

        /// <summary>Creates a Cisco-Email-Server-Address attribute (Type 16).</summary>
        /// <param name="value">The e-mail server address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EmailServerAddress(string value) => CreateString(CiscoAttributeType.EMAIL_SERVER_ADDRESS, value);

        /// <summary>Creates a Cisco-Email-Server-Ack-Flag attribute (Type 17).</summary>
        /// <param name="value">The e-mail server acknowledgement flag. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EmailServerAckFlag(string value) => CreateString(CiscoAttributeType.EMAIL_SERVER_ACK_FLAG, value);

        /// <summary>Creates a Cisco-Gateway-Id attribute (Type 18).</summary>
        /// <param name="value">The gateway identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GatewayId(string value) => CreateString(CiscoAttributeType.GATEWAY_ID, value);

        /// <summary>Creates a Cisco-Call-Type attribute (Type 19).</summary>
        /// <param name="value">The call type string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallType(string value) => CreateString(CiscoAttributeType.CALL_TYPE, value);

        /// <summary>Creates a Cisco-Port-Used attribute (Type 20).</summary>
        /// <param name="value">The port used for connection. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PortUsed(string value) => CreateString(CiscoAttributeType.PORT_USED, value);

        /// <summary>Creates a Cisco-Abort-Cause attribute (Type 21).</summary>
        /// <param name="value">The abort cause string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AbortCause(string value) => CreateString(CiscoAttributeType.ABORT_CAUSE, value);

        /// <summary>Creates a Cisco-IP-Pool-Definition attribute (Type 217).</summary>
        /// <param name="value">The IP pool definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPoolDefinition(string value) => CreateString(CiscoAttributeType.IP_POOL_DEFINITION, value);

        /// <summary>Creates a Cisco-Account-Info attribute (Type 250).</summary>
        /// <param name="value">The account information string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccountInfo(string value) => CreateString(CiscoAttributeType.ACCOUNT_INFO, value);

        /// <summary>Creates a Cisco-Service-Info attribute (Type 251).</summary>
        /// <param name="value">The service information string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceInfo(string value) => CreateString(CiscoAttributeType.SERVICE_INFO, value);

        /// <summary>Creates a Cisco-Command-Code attribute (Type 252).</summary>
        /// <param name="value">The command code string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CommandCode(string value) => CreateString(CiscoAttributeType.COMMAND_CODE, value);

        /// <summary>Creates a Cisco-Control-Info attribute (Type 253).</summary>
        /// <param name="value">The control information string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ControlInfo(string value) => CreateString(CiscoAttributeType.CONTROL_INFO, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(CiscoAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(CiscoAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}