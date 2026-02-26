using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Bay Networks / Nortel (IANA PEN 1584) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.bay</c>.
    /// </summary>
    public enum BayAttributeType : byte
    {
        /// <summary>Annex-Filter (Type 28). String. Filter name to apply.</summary>
        FILTER = 28,

        /// <summary>Annex-CLI-Command (Type 29). String. CLI command authorisation string.</summary>
        CLI_COMMAND = 29,

        /// <summary>Annex-CLI-Filter (Type 30). String. CLI filter name.</summary>
        CLI_FILTER = 30,

        /// <summary>Annex-Host-Restrict (Type 31). String. Host restriction string.</summary>
        HOST_RESTRICT = 31,

        /// <summary>Annex-Host-Allow (Type 32). String. Host allow string.</summary>
        HOST_ALLOW = 32,

        /// <summary>Annex-Product-Name (Type 33). String. Product name string.</summary>
        PRODUCT_NAME = 33,

        /// <summary>Annex-SW-Version (Type 34). String. Software version string.</summary>
        SW_VERSION = 34,

        /// <summary>Annex-Local-IP-Address (Type 35). IP address. Annex local IP address.</summary>
        LOCAL_IP_ADDRESS = 35,

        /// <summary>Annex-Callback-Portlist (Type 36). Integer. Callback port list.</summary>
        CALLBACK_PORTLIST = 36,

        /// <summary>Annex-Sec-Profile-Index (Type 37). Integer. Security profile index.</summary>
        SEC_PROFILE_INDEX = 37,

        /// <summary>Annex-Tunnel-Authen-Type (Type 38). Integer. Tunnel authentication type.</summary>
        TUNNEL_AUTHEN_TYPE = 38,

        /// <summary>Annex-Tunnel-Authen-Mode (Type 39). Integer. Tunnel authentication mode.</summary>
        TUNNEL_AUTHEN_MODE = 39,

        /// <summary>Annex-Authen-Servers (Type 40). String. Authentication servers list.</summary>
        AUTHEN_SERVERS = 40,

        /// <summary>Annex-Acct-Servers (Type 41). String. Accounting servers list.</summary>
        ACCT_SERVERS = 41,

        /// <summary>Annex-User-Server-Location (Type 42). Integer. User server location.</summary>
        USER_SERVER_LOCATION = 42,

        /// <summary>Annex-Local-Username (Type 43). String. Local username override.</summary>
        LOCAL_USERNAME = 43,

        /// <summary>Annex-System-Disc-Reason (Type 44). Integer. System disconnect reason code.</summary>
        SYSTEM_DISC_REASON = 44,

        /// <summary>Annex-Modem-Disc-Reason (Type 45). Integer. Modem disconnect reason code.</summary>
        MODEM_DISC_REASON = 45,

        /// <summary>Annex-Disconnect-Reason (Type 46). Integer. General disconnect reason code.</summary>
        DISCONNECT_REASON = 46,

        /// <summary>Annex-Addr-Resolution-Protocol (Type 47). Integer. Address resolution protocol type.</summary>
        ADDR_RESOLUTION_PROTOCOL = 47,

        /// <summary>Annex-Addr-Resolution-Servers (Type 48). String. Address resolution servers.</summary>
        ADDR_RESOLUTION_SERVERS = 48,

        /// <summary>Annex-Domain-Name (Type 49). String. Domain name to assign.</summary>
        DOMAIN_NAME = 49,

        /// <summary>Annex-Transmit-Speed (Type 50). Integer. Transmit speed in bps.</summary>
        TRANSMIT_SPEED = 50,

        /// <summary>Annex-Receive-Speed (Type 51). Integer. Receive speed in bps.</summary>
        RECEIVE_SPEED = 51,

        /// <summary>Annex-Input-Filter (Type 52). String. Input filter name.</summary>
        INPUT_FILTER = 52,

        /// <summary>Annex-Output-Filter (Type 53). String. Output filter name.</summary>
        OUTPUT_FILTER = 53,

        /// <summary>Annex-Primary-DNS-Server (Type 54). IP address. Primary DNS server address.</summary>
        PRIMARY_DNS_SERVER = 54,

        /// <summary>Annex-Secondary-DNS-Server (Type 55). IP address. Secondary DNS server address.</summary>
        SECONDARY_DNS_SERVER = 55,

        /// <summary>Annex-Primary-NBNS-Server (Type 56). IP address. Primary NBNS server address.</summary>
        PRIMARY_NBNS_SERVER = 56,

        /// <summary>Annex-Secondary-NBNS-Server (Type 57). IP address. Secondary NBNS server address.</summary>
        SECONDARY_NBNS_SERVER = 57,

        /// <summary>Annex-Syslog-Tap (Type 58). Integer. Syslog tap port number.</summary>
        SYSLOG_TAP = 58,

        /// <summary>Annex-Keypress-Timeout (Type 59). Integer. Keypress timeout in seconds.</summary>
        KEYPRESS_TIMEOUT = 59,

        /// <summary>Annex-Unauthenticated-Time (Type 60). Integer. Unauthenticated time in seconds.</summary>
        UNAUTHENTICATED_TIME = 60,

        /// <summary>Annex-Re-CHAP-Timeout (Type 61). Integer. Re-CHAP timeout in seconds.</summary>
        RE_CHAP_TIMEOUT = 61,

        /// <summary>Annex-MRRU (Type 62). Integer. Maximum Received Reconstructed Unit.</summary>
        MRRU = 62,

        /// <summary>Annex-EDO (Type 63). String. Endpoint Discriminator Option.</summary>
        EDO = 63,

        /// <summary>Annex-Pool-Number (Type 64). Integer. IP address pool number.</summary>
        POOL_NUMBER = 64,

        /// <summary>Annex-PPP-Trace-Level (Type 65). Integer. PPP trace level.</summary>
        PPP_TRACE_LEVEL = 65,

        /// <summary>Annex-Pre-Input-Octets (Type 66). Integer. Pre-session input octets.</summary>
        PRE_INPUT_OCTETS = 66,

        /// <summary>Annex-Pre-Output-Octets (Type 67). Integer. Pre-session output octets.</summary>
        PRE_OUTPUT_OCTETS = 67,

        /// <summary>Annex-Pre-Input-Packets (Type 68). Integer. Pre-session input packets.</summary>
        PRE_INPUT_PACKETS = 68,

        /// <summary>Annex-Pre-Output-Packets (Type 69). Integer. Pre-session output packets.</summary>
        PRE_OUTPUT_PACKETS = 69,

        /// <summary>Annex-Connect-Progress (Type 70). Integer. Connection progress indicator.</summary>
        CONNECT_PROGRESS = 70,

        /// <summary>Annex-Multicast-Rate-Limit (Type 73). Integer. Multicast rate limit in bps.</summary>
        MULTICAST_RATE_LIMIT = 73,

        /// <summary>Annex-Maximum-Call-Duration (Type 74). Integer. Maximum call duration in seconds.</summary>
        MAXIMUM_CALL_DURATION = 74,

        /// <summary>Annex-Multilink-Id (Type 75). Integer. Multilink bundle identifier.</summary>
        MULTILINK_ID = 75,

        /// <summary>Annex-Num-In-Multilink (Type 76). Integer. Number of links in multilink bundle.</summary>
        NUM_IN_MULTILINK = 76,

        /// <summary>Annex-Secondary-Srv-Endpoint (Type 78). String. Secondary server endpoint.</summary>
        SECONDARY_SRV_ENDPOINT = 78,

        /// <summary>Annex-Gwy-Selection-Mode (Type 79). Integer. Gateway selection mode.</summary>
        GWY_SELECTION_MODE = 79,

        /// <summary>Annex-Logical-Channel-Number (Type 81). Integer. Logical channel number.</summary>
        LOGICAL_CHANNEL_NUMBER = 81,

        /// <summary>Annex-Wan-Number (Type 82). Integer. WAN number.</summary>
        WAN_NUMBER = 82,

        /// <summary>Annex-Port (Type 83). Integer. Annex port number.</summary>
        PORT = 83,

        /// <summary>Annex-Pool-Member-IP (Type 84). IP address. Pool member IP address.</summary>
        POOL_MEMBER_IP = 84,

        /// <summary>Annex-Begin-Modulation (Type 85). String. Begin modulation parameters.</summary>
        BEGIN_MODULATION = 85,

        /// <summary>Annex-Error-Correction-Prot (Type 86). String. Error correction protocol.</summary>
        ERROR_CORRECTION_PROT = 86,

        /// <summary>Annex-End-Modulation (Type 87). String. End modulation parameters.</summary>
        END_MODULATION = 87,

        /// <summary>Annex-Compression-Type (Type 88). String. Compression type string.</summary>
        COMPRESSION_TYPE = 88,

        /// <summary>Annex-Modem-Error-Count (Type 89). Integer. Modem error count.</summary>
        MODEM_ERROR_COUNT = 89,

        /// <summary>Annex-Modem-SBuf-Count (Type 90). Integer. Modem send buffer count.</summary>
        MODEM_SBUF_COUNT = 90,

        /// <summary>Annex-Modem-RBuf-Count (Type 91). Integer. Modem receive buffer count.</summary>
        MODEM_RBUF_COUNT = 91,

        /// <summary>Annex-User-Level (Type 100). Integer. User privilege level.</summary>
        USER_LEVEL = 100,

        /// <summary>Annex-Audit-Level (Type 101). Integer. Audit level.</summary>
        AUDIT_LEVEL = 101
    }

    /// <summary>
    /// Annex-Tunnel-Authen-Type attribute values (Type 38).
    /// </summary>
    public enum BAY_TUNNEL_AUTHEN_TYPE
    {
        /// <summary>No tunnel authentication.</summary>
        NONE = 0,

        /// <summary>Kerberos v5 tunnel authentication.</summary>
        KERBEROS_V5 = 1
    }

    /// <summary>
    /// Annex-Tunnel-Authen-Mode attribute values (Type 39).
    /// </summary>
    public enum BAY_TUNNEL_AUTHEN_MODE
    {
        /// <summary>No tunnel authentication mode.</summary>
        NONE = 0,

        /// <summary>Prefix mode.</summary>
        PREFIX = 1,

        /// <summary>Suffix mode.</summary>
        SUFFIX = 2
    }

    /// <summary>
    /// Annex-User-Server-Location attribute values (Type 42).
    /// </summary>
    public enum BAY_USER_SERVER_LOCATION
    {
        /// <summary>Local user server.</summary>
        LOCAL = 1,

        /// <summary>Remote user server.</summary>
        REMOTE = 2
    }

    /// <summary>
    /// Annex-Addr-Resolution-Protocol attribute values (Type 47).
    /// </summary>
    public enum BAY_ADDR_RESOLUTION_PROTOCOL
    {
        /// <summary>No address resolution.</summary>
        NONE = 0,

        /// <summary>DHCP address resolution.</summary>
        DHCP = 1
    }

    /// <summary>
    /// Annex-Gwy-Selection-Mode attribute values (Type 79).
    /// </summary>
    public enum BAY_GWY_SELECTION_MODE
    {
        /// <summary>Normal gateway selection.</summary>
        NORMAL = 0,

        /// <summary>Roundrobin gateway selection.</summary>
        ROUNDROBIN = 1
    }

    /// <summary>
    /// Annex-User-Level attribute values (Type 100).
    /// </summary>
    public enum BAY_USER_LEVEL
    {
        /// <summary>Manager level.</summary>
        MANAGER = 2,

        /// <summary>User level.</summary>
        USER = 4,

        /// <summary>Operator level.</summary>
        OPERATOR = 8
    }

    /// <summary>
    /// Annex-Audit-Level attribute values (Type 101).
    /// </summary>
    public enum BAY_AUDIT_LEVEL
    {
        /// <summary>Manager audit level.</summary>
        MANAGER = 2,

        /// <summary>User audit level.</summary>
        USER = 4,

        /// <summary>Operator audit level.</summary>
        OPERATOR = 8
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Bay Networks / Nortel
    /// (IANA PEN 1584) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.bay</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Bay Networks' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 1584</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Bay Networks (Nortel) Annex terminal servers and
    /// remote access concentrators for CLI command authorisation, port/input/output
    /// filtering, host restrictions, DNS/NBNS assignment, tunnel authentication,
    /// modem diagnostics, multilink PPP, disconnect reason reporting, and user
    /// privilege/audit level assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(BayAttributes.UserLevel(BAY_USER_LEVEL.MANAGER));
    /// packet.SetAttribute(BayAttributes.Filter("admin-filter"));
    /// packet.SetAttribute(BayAttributes.PrimaryDnsServer(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(BayAttributes.MaximumCallDuration(3600));
    /// </code>
    /// </remarks>
    public static class BayAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Bay Networks (Nortel).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 1584;

        #region Integer Attributes

        /// <summary>Creates an Annex-Callback-Portlist attribute (Type 36).</summary>
        /// <param name="value">The callback port list.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallbackPortlist(int value) => CreateInteger(BayAttributeType.CALLBACK_PORTLIST, value);

        /// <summary>Creates an Annex-Sec-Profile-Index attribute (Type 37).</summary>
        /// <param name="value">The security profile index.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SecProfileIndex(int value) => CreateInteger(BayAttributeType.SEC_PROFILE_INDEX, value);

        /// <summary>
        /// Creates an Annex-Tunnel-Authen-Type attribute (Type 38).
        /// </summary>
        /// <param name="value">The tunnel authentication type. See <see cref="BAY_TUNNEL_AUTHEN_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelAuthenType(BAY_TUNNEL_AUTHEN_TYPE value) => CreateInteger(BayAttributeType.TUNNEL_AUTHEN_TYPE, (int)value);

        /// <summary>
        /// Creates an Annex-Tunnel-Authen-Mode attribute (Type 39).
        /// </summary>
        /// <param name="value">The tunnel authentication mode. See <see cref="BAY_TUNNEL_AUTHEN_MODE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelAuthenMode(BAY_TUNNEL_AUTHEN_MODE value) => CreateInteger(BayAttributeType.TUNNEL_AUTHEN_MODE, (int)value);

        /// <summary>
        /// Creates an Annex-User-Server-Location attribute (Type 42).
        /// </summary>
        /// <param name="value">The user server location. See <see cref="BAY_USER_SERVER_LOCATION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserServerLocation(BAY_USER_SERVER_LOCATION value) => CreateInteger(BayAttributeType.USER_SERVER_LOCATION, (int)value);

        /// <summary>Creates an Annex-System-Disc-Reason attribute (Type 44).</summary>
        public static VendorSpecificAttributes SystemDiscReason(int value) => CreateInteger(BayAttributeType.SYSTEM_DISC_REASON, value);

        /// <summary>Creates an Annex-Modem-Disc-Reason attribute (Type 45).</summary>
        public static VendorSpecificAttributes ModemDiscReason(int value) => CreateInteger(BayAttributeType.MODEM_DISC_REASON, value);

        /// <summary>Creates an Annex-Disconnect-Reason attribute (Type 46).</summary>
        public static VendorSpecificAttributes DisconnectReason(int value) => CreateInteger(BayAttributeType.DISCONNECT_REASON, value);

        /// <summary>
        /// Creates an Annex-Addr-Resolution-Protocol attribute (Type 47).
        /// </summary>
        /// <param name="value">The address resolution protocol. See <see cref="BAY_ADDR_RESOLUTION_PROTOCOL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AddrResolutionProtocol(BAY_ADDR_RESOLUTION_PROTOCOL value) => CreateInteger(BayAttributeType.ADDR_RESOLUTION_PROTOCOL, (int)value);

        /// <summary>Creates an Annex-Transmit-Speed attribute (Type 50).</summary>
        /// <param name="value">The transmit speed in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TransmitSpeed(int value) => CreateInteger(BayAttributeType.TRANSMIT_SPEED, value);

        /// <summary>Creates an Annex-Receive-Speed attribute (Type 51).</summary>
        /// <param name="value">The receive speed in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReceiveSpeed(int value) => CreateInteger(BayAttributeType.RECEIVE_SPEED, value);

        /// <summary>Creates an Annex-Syslog-Tap attribute (Type 58).</summary>
        public static VendorSpecificAttributes SyslogTap(int value) => CreateInteger(BayAttributeType.SYSLOG_TAP, value);

        /// <summary>Creates an Annex-Keypress-Timeout attribute (Type 59).</summary>
        /// <param name="value">The keypress timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes KeypressTimeout(int value) => CreateInteger(BayAttributeType.KEYPRESS_TIMEOUT, value);

        /// <summary>Creates an Annex-Unauthenticated-Time attribute (Type 60).</summary>
        /// <param name="value">The unauthenticated time in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UnauthenticatedTime(int value) => CreateInteger(BayAttributeType.UNAUTHENTICATED_TIME, value);

        /// <summary>Creates an Annex-Re-CHAP-Timeout attribute (Type 61).</summary>
        /// <param name="value">The re-CHAP timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReChapTimeout(int value) => CreateInteger(BayAttributeType.RE_CHAP_TIMEOUT, value);

        /// <summary>Creates an Annex-MRRU attribute (Type 62).</summary>
        /// <param name="value">The Maximum Received Reconstructed Unit.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Mrru(int value) => CreateInteger(BayAttributeType.MRRU, value);

        /// <summary>Creates an Annex-Pool-Number attribute (Type 64).</summary>
        /// <param name="value">The IP address pool number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PoolNumber(int value) => CreateInteger(BayAttributeType.POOL_NUMBER, value);

        /// <summary>Creates an Annex-PPP-Trace-Level attribute (Type 65).</summary>
        public static VendorSpecificAttributes PppTraceLevel(int value) => CreateInteger(BayAttributeType.PPP_TRACE_LEVEL, value);

        /// <summary>Creates an Annex-Pre-Input-Octets attribute (Type 66).</summary>
        public static VendorSpecificAttributes PreInputOctets(int value) => CreateInteger(BayAttributeType.PRE_INPUT_OCTETS, value);

        /// <summary>Creates an Annex-Pre-Output-Octets attribute (Type 67).</summary>
        public static VendorSpecificAttributes PreOutputOctets(int value) => CreateInteger(BayAttributeType.PRE_OUTPUT_OCTETS, value);

        /// <summary>Creates an Annex-Pre-Input-Packets attribute (Type 68).</summary>
        public static VendorSpecificAttributes PreInputPackets(int value) => CreateInteger(BayAttributeType.PRE_INPUT_PACKETS, value);

        /// <summary>Creates an Annex-Pre-Output-Packets attribute (Type 69).</summary>
        public static VendorSpecificAttributes PreOutputPackets(int value) => CreateInteger(BayAttributeType.PRE_OUTPUT_PACKETS, value);

        /// <summary>Creates an Annex-Connect-Progress attribute (Type 70).</summary>
        public static VendorSpecificAttributes ConnectProgress(int value) => CreateInteger(BayAttributeType.CONNECT_PROGRESS, value);

        /// <summary>Creates an Annex-Multicast-Rate-Limit attribute (Type 73).</summary>
        /// <param name="value">The multicast rate limit in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MulticastRateLimit(int value) => CreateInteger(BayAttributeType.MULTICAST_RATE_LIMIT, value);

        /// <summary>Creates an Annex-Maximum-Call-Duration attribute (Type 74).</summary>
        /// <param name="value">The maximum call duration in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaximumCallDuration(int value) => CreateInteger(BayAttributeType.MAXIMUM_CALL_DURATION, value);

        /// <summary>Creates an Annex-Multilink-Id attribute (Type 75).</summary>
        public static VendorSpecificAttributes MultilinkId(int value) => CreateInteger(BayAttributeType.MULTILINK_ID, value);

        /// <summary>Creates an Annex-Num-In-Multilink attribute (Type 76).</summary>
        public static VendorSpecificAttributes NumInMultilink(int value) => CreateInteger(BayAttributeType.NUM_IN_MULTILINK, value);

        /// <summary>
        /// Creates an Annex-Gwy-Selection-Mode attribute (Type 79).
        /// </summary>
        /// <param name="value">The gateway selection mode. See <see cref="BAY_GWY_SELECTION_MODE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes GwySelectionMode(BAY_GWY_SELECTION_MODE value) => CreateInteger(BayAttributeType.GWY_SELECTION_MODE, (int)value);

        /// <summary>Creates an Annex-Logical-Channel-Number attribute (Type 81).</summary>
        public static VendorSpecificAttributes LogicalChannelNumber(int value) => CreateInteger(BayAttributeType.LOGICAL_CHANNEL_NUMBER, value);

        /// <summary>Creates an Annex-Wan-Number attribute (Type 82).</summary>
        public static VendorSpecificAttributes WanNumber(int value) => CreateInteger(BayAttributeType.WAN_NUMBER, value);

        /// <summary>Creates an Annex-Port attribute (Type 83).</summary>
        /// <param name="value">The Annex port number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Port(int value) => CreateInteger(BayAttributeType.PORT, value);

        /// <summary>Creates an Annex-Modem-Error-Count attribute (Type 89).</summary>
        public static VendorSpecificAttributes ModemErrorCount(int value) => CreateInteger(BayAttributeType.MODEM_ERROR_COUNT, value);

        /// <summary>Creates an Annex-Modem-SBuf-Count attribute (Type 90).</summary>
        public static VendorSpecificAttributes ModemSBufCount(int value) => CreateInteger(BayAttributeType.MODEM_SBUF_COUNT, value);

        /// <summary>Creates an Annex-Modem-RBuf-Count attribute (Type 91).</summary>
        public static VendorSpecificAttributes ModemRBufCount(int value) => CreateInteger(BayAttributeType.MODEM_RBUF_COUNT, value);

        /// <summary>
        /// Creates an Annex-User-Level attribute (Type 100) with the specified privilege level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="BAY_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserLevel(BAY_USER_LEVEL value) => CreateInteger(BayAttributeType.USER_LEVEL, (int)value);

        /// <summary>
        /// Creates an Annex-Audit-Level attribute (Type 101) with the specified audit level.
        /// </summary>
        /// <param name="value">The audit level. See <see cref="BAY_AUDIT_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuditLevel(BAY_AUDIT_LEVEL value) => CreateInteger(BayAttributeType.AUDIT_LEVEL, (int)value);

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Annex-Filter attribute (Type 28) with the specified filter name.
        /// </summary>
        /// <param name="value">The filter name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Filter(string value) => CreateString(BayAttributeType.FILTER, value);

        /// <summary>Creates an Annex-CLI-Command attribute (Type 29).</summary>
        /// <param name="value">The CLI command authorisation string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CliCommand(string value) => CreateString(BayAttributeType.CLI_COMMAND, value);

        /// <summary>Creates an Annex-CLI-Filter attribute (Type 30).</summary>
        /// <param name="value">The CLI filter name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CliFilter(string value) => CreateString(BayAttributeType.CLI_FILTER, value);

        /// <summary>Creates an Annex-Host-Restrict attribute (Type 31).</summary>
        /// <param name="value">The host restriction string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HostRestrict(string value) => CreateString(BayAttributeType.HOST_RESTRICT, value);

        /// <summary>Creates an Annex-Host-Allow attribute (Type 32).</summary>
        /// <param name="value">The host allow string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HostAllow(string value) => CreateString(BayAttributeType.HOST_ALLOW, value);

        /// <summary>Creates an Annex-Product-Name attribute (Type 33).</summary>
        /// <param name="value">The product name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProductName(string value) => CreateString(BayAttributeType.PRODUCT_NAME, value);

        /// <summary>Creates an Annex-SW-Version attribute (Type 34).</summary>
        /// <param name="value">The software version. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SwVersion(string value) => CreateString(BayAttributeType.SW_VERSION, value);

        /// <summary>Creates an Annex-Authen-Servers attribute (Type 40).</summary>
        /// <param name="value">The authentication servers list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthenServers(string value) => CreateString(BayAttributeType.AUTHEN_SERVERS, value);

        /// <summary>Creates an Annex-Acct-Servers attribute (Type 41).</summary>
        /// <param name="value">The accounting servers list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctServers(string value) => CreateString(BayAttributeType.ACCT_SERVERS, value);

        /// <summary>Creates an Annex-Local-Username attribute (Type 43).</summary>
        /// <param name="value">The local username override. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LocalUsername(string value) => CreateString(BayAttributeType.LOCAL_USERNAME, value);

        /// <summary>Creates an Annex-Addr-Resolution-Servers attribute (Type 48).</summary>
        /// <param name="value">The address resolution servers. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AddrResolutionServers(string value) => CreateString(BayAttributeType.ADDR_RESOLUTION_SERVERS, value);

        /// <summary>Creates an Annex-Domain-Name attribute (Type 49).</summary>
        /// <param name="value">The domain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DomainName(string value) => CreateString(BayAttributeType.DOMAIN_NAME, value);

        /// <summary>Creates an Annex-Input-Filter attribute (Type 52).</summary>
        /// <param name="value">The input filter name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes InputFilter(string value) => CreateString(BayAttributeType.INPUT_FILTER, value);

        /// <summary>Creates an Annex-Output-Filter attribute (Type 53).</summary>
        /// <param name="value">The output filter name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OutputFilter(string value) => CreateString(BayAttributeType.OUTPUT_FILTER, value);

        /// <summary>Creates an Annex-EDO attribute (Type 63).</summary>
        /// <param name="value">The Endpoint Discriminator Option. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Edo(string value) => CreateString(BayAttributeType.EDO, value);

        /// <summary>Creates an Annex-Secondary-Srv-Endpoint attribute (Type 78).</summary>
        /// <param name="value">The secondary server endpoint. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SecondarySrvEndpoint(string value) => CreateString(BayAttributeType.SECONDARY_SRV_ENDPOINT, value);

        /// <summary>Creates an Annex-Begin-Modulation attribute (Type 85).</summary>
        /// <param name="value">The begin modulation parameters. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BeginModulation(string value) => CreateString(BayAttributeType.BEGIN_MODULATION, value);

        /// <summary>Creates an Annex-Error-Correction-Prot attribute (Type 86).</summary>
        /// <param name="value">The error correction protocol. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ErrorCorrectionProt(string value) => CreateString(BayAttributeType.ERROR_CORRECTION_PROT, value);

        /// <summary>Creates an Annex-End-Modulation attribute (Type 87).</summary>
        /// <param name="value">The end modulation parameters. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EndModulation(string value) => CreateString(BayAttributeType.END_MODULATION, value);

        /// <summary>Creates an Annex-Compression-Type attribute (Type 88).</summary>
        /// <param name="value">The compression type. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CompressionType(string value) => CreateString(BayAttributeType.COMPRESSION_TYPE, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Annex-Local-IP-Address attribute (Type 35) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The Annex local IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes LocalIpAddress(IPAddress value) => CreateIpv4(BayAttributeType.LOCAL_IP_ADDRESS, value);

        /// <summary>
        /// Creates an Annex-Primary-DNS-Server attribute (Type 54) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary DNS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDnsServer(IPAddress value) => CreateIpv4(BayAttributeType.PRIMARY_DNS_SERVER, value);

        /// <summary>
        /// Creates an Annex-Secondary-DNS-Server attribute (Type 55) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary DNS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDnsServer(IPAddress value) => CreateIpv4(BayAttributeType.SECONDARY_DNS_SERVER, value);

        /// <summary>
        /// Creates an Annex-Primary-NBNS-Server attribute (Type 56) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary NBNS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryNbnsServer(IPAddress value) => CreateIpv4(BayAttributeType.PRIMARY_NBNS_SERVER, value);

        /// <summary>
        /// Creates an Annex-Secondary-NBNS-Server attribute (Type 57) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary NBNS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryNbnsServer(IPAddress value) => CreateIpv4(BayAttributeType.SECONDARY_NBNS_SERVER, value);

        /// <summary>
        /// Creates an Annex-Pool-Member-IP attribute (Type 84) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The pool member IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PoolMemberIp(IPAddress value) => CreateIpv4(BayAttributeType.POOL_MEMBER_IP, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(BayAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(BayAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(BayAttributeType type, IPAddress value)
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