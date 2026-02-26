using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Shiva (IANA PEN 166) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.shiva</c>.
    /// </summary>
    /// <remarks>
    /// Shiva Corporation (acquired by Intel in 1999) produced remote access
    /// concentrators, LAN rovers, and dial-up networking equipment (AccessPort,
    /// LanRover, ShivaRemote) for enterprise remote access and WAN connectivity.
    /// </remarks>
    public enum ShivaAttributeType : byte
    {
        /// <summary>Shiva-User-Attributes (Type 1). String. User attributes string.</summary>
        USER_ATTRIBUTES = 1,

        /// <summary>Shiva-Compression (Type 30). Integer. Compression type.</summary>
        COMPRESSION = 30,

        /// <summary>Shiva-Dialback-Delay (Type 31). Integer. Dialback delay in seconds.</summary>
        DIALBACK_DELAY = 31,

        /// <summary>Shiva-Call-Durn-Trap (Type 32). Integer. Call duration trap threshold in seconds.</summary>
        CALL_DURN_TRAP = 32,

        /// <summary>Shiva-Bandwidth-Trap (Type 33). Integer. Bandwidth trap threshold.</summary>
        BANDWIDTH_TRAP = 33,

        /// <summary>Shiva-Minimum-Call (Type 34). Integer. Minimum call duration in seconds.</summary>
        MINIMUM_CALL = 34,

        /// <summary>Shiva-Default-Host (Type 35). String. Default host address.</summary>
        DEFAULT_HOST = 35,

        /// <summary>Shiva-Menu-Name (Type 36). String. Menu name.</summary>
        MENU_NAME = 36,

        /// <summary>Shiva-User-Flags (Type 37). String. User flags.</summary>
        USER_FLAGS = 37,

        /// <summary>Shiva-Termtype (Type 38). String. Terminal type.</summary>
        TERMTYPE = 38,

        /// <summary>Shiva-Break-Key (Type 39). String. Break key sequence.</summary>
        BREAK_KEY = 39,

        /// <summary>Shiva-Fwd-Key (Type 40). String. Forward key sequence.</summary>
        FWD_KEY = 40,

        /// <summary>Shiva-Bak-Key (Type 41). String. Backward key sequence.</summary>
        BAK_KEY = 41,

        /// <summary>Shiva-Dial-Timeout (Type 42). Integer. Dial timeout in seconds.</summary>
        DIAL_TIMEOUT = 42,

        /// <summary>Shiva-LAT-Port (Type 43). String. LAT port name.</summary>
        LAT_PORT = 43,

        /// <summary>Shiva-Max-VCs (Type 44). Integer. Maximum virtual circuits.</summary>
        MAX_VCS = 44,

        /// <summary>Shiva-DHCP-Leasetime (Type 45). Integer. DHCP lease time in seconds.</summary>
        DHCP_LEASETIME = 45,

        /// <summary>Shiva-LAT-Groups (Type 46). String. LAT group codes.</summary>
        LAT_GROUPS = 46,

        /// <summary>Shiva-RTC-Timestamp (Type 60). Integer. RTC timestamp.</summary>
        RTC_TIMESTAMP = 60,

        /// <summary>Shiva-Circuit-Type (Type 61). Integer. Circuit type.</summary>
        CIRCUIT_TYPE = 61,

        /// <summary>Shiva-Called-Number (Type 90). String. Called number.</summary>
        CALLED_NUMBER = 90,

        /// <summary>Shiva-Calling-Number (Type 91). String. Calling number.</summary>
        CALLING_NUMBER = 91,

        /// <summary>Shiva-Customer-Id (Type 92). String. Customer identifier.</summary>
        CUSTOMER_ID = 92,

        /// <summary>Shiva-Type-Of-Service (Type 93). Integer. Type of service.</summary>
        TYPE_OF_SERVICE = 93,

        /// <summary>Shiva-Link-Speed (Type 94). Integer. Link speed in bps.</summary>
        LINK_SPEED = 94,

        /// <summary>Shiva-Links-In-Bundle (Type 95). Integer. Number of links in bundle.</summary>
        LINKS_IN_BUNDLE = 95,

        /// <summary>Shiva-Compression-Type (Type 96). Integer. Compression type code.</summary>
        COMPRESSION_TYPE = 96,

        /// <summary>Shiva-Link-Protocol (Type 97). Integer. Link protocol type.</summary>
        LINK_PROTOCOL = 97,

        /// <summary>Shiva-Network-Protocols (Type 98). Integer. Network protocols bitmap.</summary>
        NETWORK_PROTOCOLS = 98,

        /// <summary>Shiva-Session-Id (Type 99). Integer. Session identifier.</summary>
        SESSION_ID = 99,

        /// <summary>Shiva-Disconnect-Reason (Type 100). Integer. Disconnect reason code.</summary>
        DISCONNECT_REASON = 100,

        /// <summary>Shiva-Acct-Serv-Switch (Type 101). IP address. Accounting server switch address.</summary>
        ACCT_SERV_SWITCH = 101,

        /// <summary>Shiva-Event-Flags (Type 102). Integer. Event flags bitmap.</summary>
        EVENT_FLAGS = 102,

        /// <summary>Shiva-Function (Type 103). Integer. Function code.</summary>
        FUNCTION = 103,

        /// <summary>Shiva-Connect-Reason (Type 104). Integer. Connect reason code.</summary>
        CONNECT_REASON = 104
    }

    /// <summary>
    /// Shiva-Type-Of-Service attribute values (Type 93).
    /// </summary>
    public enum SHIVA_TYPE_OF_SERVICE
    {
        /// <summary>Analog/modem connection.</summary>
        ANALOG = 1,

        /// <summary>ISDN digitally switched connection.</summary>
        DIGITALLY_SWITCHED = 2,

        /// <summary>Virtual/tunneled connection.</summary>
        VIRTUAL = 3
    }

    /// <summary>
    /// Shiva-Link-Protocol attribute values (Type 97).
    /// </summary>
    public enum SHIVA_LINK_PROTOCOL
    {
        /// <summary>Async character mode.</summary>
        ASYNC = 1,

        /// <summary>ARAP (AppleTalk Remote Access Protocol).</summary>
        ARAP = 2,

        /// <summary>PPP (Point-to-Point Protocol).</summary>
        PPP = 3,

        /// <summary>SLIP (Serial Line Internet Protocol).</summary>
        SLIP = 4,

        /// <summary>Shell/CLI mode.</summary>
        SHELL = 5
    }

    /// <summary>
    /// Shiva-Disconnect-Reason attribute values (Type 100).
    /// </summary>
    public enum SHIVA_DISCONNECT_REASON
    {
        /// <summary>No disconnect reason.</summary>
        NONE = 0,

        /// <summary>No carrier detected.</summary>
        NO_CARRIER = 1,

        /// <summary>No detection.</summary>
        NO_DETECT = 2,

        /// <summary>Administrative reset.</summary>
        ADMIN_RESET = 3,

        /// <summary>User request.</summary>
        USER_REQUEST = 4,

        /// <summary>Idle timeout expired.</summary>
        IDLE_TIMEOUT = 5,

        /// <summary>Exit telnet session.</summary>
        EXIT_TELNET = 6,

        /// <summary>Protocol violation.</summary>
        PROTOCOL_VIOLATION = 7,

        /// <summary>Session timeout expired.</summary>
        SESSION_TIMEOUT = 8
    }

    /// <summary>
    /// Shiva-Function attribute values (Type 103).
    /// </summary>
    public enum SHIVA_FUNCTION
    {
        /// <summary>Dialin function.</summary>
        DIALIN = 0,

        /// <summary>Dialout function.</summary>
        DIALOUT = 1,

        /// <summary>LAN-to-LAN function.</summary>
        LAN_TO_LAN = 2
    }

    /// <summary>
    /// Shiva-Connect-Reason attribute values (Type 104).
    /// </summary>
    public enum SHIVA_CONNECT_REASON
    {
        /// <summary>Remote initiated connection.</summary>
        REMOTE = 0,

        /// <summary>Local initiated connection.</summary>
        LOCAL = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Shiva
    /// (IANA PEN 166) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.shiva</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Shiva's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 166</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Shiva Corporation (Intel) AccessPort and
    /// LanRover remote access concentrators for RADIUS-based user attribute
    /// configuration, compression settings, dialback delay, call duration and
    /// bandwidth traps, default host and menu selection, terminal settings
    /// (type, break/forward/backward keys), dial timeout, LAT port and group
    /// configuration, DHCP lease time, virtual circuit limits, call detail
    /// records (called/calling number, link speed, protocol, session ID, connect
    /// and disconnect reasons), accounting server switching, and event flags.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ShivaAttributes.UserAttributes("ip-pool=corporate"));
    /// packet.SetAttribute(ShivaAttributes.LinkSpeed(56000));
    /// packet.SetAttribute(ShivaAttributes.LinkProtocol(SHIVA_LINK_PROTOCOL.PPP));
    /// packet.SetAttribute(ShivaAttributes.TypeOfService(SHIVA_TYPE_OF_SERVICE.ANALOG));
    /// </code>
    /// </remarks>
    public static class ShivaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Shiva Corporation.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 166;

        #region Integer Attributes

        /// <summary>Creates a Shiva-Compression attribute (Type 30).</summary>
        /// <param name="value">The compression type.</param>
        public static VendorSpecificAttributes Compression(int value) => CreateInteger(ShivaAttributeType.COMPRESSION, value);

        /// <summary>Creates a Shiva-Dialback-Delay attribute (Type 31).</summary>
        /// <param name="value">The dialback delay in seconds.</param>
        public static VendorSpecificAttributes DialbackDelay(int value) => CreateInteger(ShivaAttributeType.DIALBACK_DELAY, value);

        /// <summary>Creates a Shiva-Call-Durn-Trap attribute (Type 32).</summary>
        /// <param name="value">The call duration trap threshold in seconds.</param>
        public static VendorSpecificAttributes CallDurnTrap(int value) => CreateInteger(ShivaAttributeType.CALL_DURN_TRAP, value);

        /// <summary>Creates a Shiva-Bandwidth-Trap attribute (Type 33).</summary>
        /// <param name="value">The bandwidth trap threshold.</param>
        public static VendorSpecificAttributes BandwidthTrap(int value) => CreateInteger(ShivaAttributeType.BANDWIDTH_TRAP, value);

        /// <summary>Creates a Shiva-Minimum-Call attribute (Type 34).</summary>
        /// <param name="value">The minimum call duration in seconds.</param>
        public static VendorSpecificAttributes MinimumCall(int value) => CreateInteger(ShivaAttributeType.MINIMUM_CALL, value);

        /// <summary>Creates a Shiva-Dial-Timeout attribute (Type 42).</summary>
        /// <param name="value">The dial timeout in seconds.</param>
        public static VendorSpecificAttributes DialTimeout(int value) => CreateInteger(ShivaAttributeType.DIAL_TIMEOUT, value);

        /// <summary>Creates a Shiva-Max-VCs attribute (Type 44).</summary>
        /// <param name="value">The maximum virtual circuits.</param>
        public static VendorSpecificAttributes MaxVcs(int value) => CreateInteger(ShivaAttributeType.MAX_VCS, value);

        /// <summary>Creates a Shiva-DHCP-Leasetime attribute (Type 45).</summary>
        /// <param name="value">The DHCP lease time in seconds.</param>
        public static VendorSpecificAttributes DhcpLeasetime(int value) => CreateInteger(ShivaAttributeType.DHCP_LEASETIME, value);

        /// <summary>Creates a Shiva-RTC-Timestamp attribute (Type 60).</summary>
        /// <param name="value">The RTC timestamp.</param>
        public static VendorSpecificAttributes RtcTimestamp(int value) => CreateInteger(ShivaAttributeType.RTC_TIMESTAMP, value);

        /// <summary>Creates a Shiva-Circuit-Type attribute (Type 61).</summary>
        /// <param name="value">The circuit type.</param>
        public static VendorSpecificAttributes CircuitType(int value) => CreateInteger(ShivaAttributeType.CIRCUIT_TYPE, value);

        /// <summary>Creates a Shiva-Type-Of-Service attribute (Type 93).</summary>
        /// <param name="value">The type of service. See <see cref="SHIVA_TYPE_OF_SERVICE"/>.</param>
        public static VendorSpecificAttributes TypeOfService(SHIVA_TYPE_OF_SERVICE value) => CreateInteger(ShivaAttributeType.TYPE_OF_SERVICE, (int)value);

        /// <summary>Creates a Shiva-Link-Speed attribute (Type 94).</summary>
        /// <param name="value">The link speed in bps.</param>
        public static VendorSpecificAttributes LinkSpeed(int value) => CreateInteger(ShivaAttributeType.LINK_SPEED, value);

        /// <summary>Creates a Shiva-Links-In-Bundle attribute (Type 95).</summary>
        /// <param name="value">The number of links in bundle.</param>
        public static VendorSpecificAttributes LinksInBundle(int value) => CreateInteger(ShivaAttributeType.LINKS_IN_BUNDLE, value);

        /// <summary>Creates a Shiva-Compression-Type attribute (Type 96).</summary>
        /// <param name="value">The compression type code.</param>
        public static VendorSpecificAttributes CompressionType(int value) => CreateInteger(ShivaAttributeType.COMPRESSION_TYPE, value);

        /// <summary>Creates a Shiva-Link-Protocol attribute (Type 97).</summary>
        /// <param name="value">The link protocol type. See <see cref="SHIVA_LINK_PROTOCOL"/>.</param>
        public static VendorSpecificAttributes LinkProtocol(SHIVA_LINK_PROTOCOL value) => CreateInteger(ShivaAttributeType.LINK_PROTOCOL, (int)value);

        /// <summary>Creates a Shiva-Network-Protocols attribute (Type 98).</summary>
        /// <param name="value">The network protocols bitmap.</param>
        public static VendorSpecificAttributes NetworkProtocols(int value) => CreateInteger(ShivaAttributeType.NETWORK_PROTOCOLS, value);

        /// <summary>Creates a Shiva-Session-Id attribute (Type 99).</summary>
        /// <param name="value">The session identifier.</param>
        public static VendorSpecificAttributes SessionId(int value) => CreateInteger(ShivaAttributeType.SESSION_ID, value);

        /// <summary>Creates a Shiva-Disconnect-Reason attribute (Type 100).</summary>
        /// <param name="value">The disconnect reason code. See <see cref="SHIVA_DISCONNECT_REASON"/>.</param>
        public static VendorSpecificAttributes DisconnectReason(SHIVA_DISCONNECT_REASON value) => CreateInteger(ShivaAttributeType.DISCONNECT_REASON, (int)value);

        /// <summary>Creates a Shiva-Event-Flags attribute (Type 102).</summary>
        /// <param name="value">The event flags bitmap.</param>
        public static VendorSpecificAttributes EventFlags(int value) => CreateInteger(ShivaAttributeType.EVENT_FLAGS, value);

        /// <summary>Creates a Shiva-Function attribute (Type 103).</summary>
        /// <param name="value">The function code. See <see cref="SHIVA_FUNCTION"/>.</param>
        public static VendorSpecificAttributes Function(SHIVA_FUNCTION value) => CreateInteger(ShivaAttributeType.FUNCTION, (int)value);

        /// <summary>Creates a Shiva-Connect-Reason attribute (Type 104).</summary>
        /// <param name="value">The connect reason code. See <see cref="SHIVA_CONNECT_REASON"/>.</param>
        public static VendorSpecificAttributes ConnectReason(SHIVA_CONNECT_REASON value) => CreateInteger(ShivaAttributeType.CONNECT_REASON, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Shiva-User-Attributes attribute (Type 1).</summary>
        /// <param name="value">The user attributes string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserAttributes(string value) => CreateString(ShivaAttributeType.USER_ATTRIBUTES, value);

        /// <summary>Creates a Shiva-Default-Host attribute (Type 35).</summary>
        /// <param name="value">The default host address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DefaultHost(string value) => CreateString(ShivaAttributeType.DEFAULT_HOST, value);

        /// <summary>Creates a Shiva-Menu-Name attribute (Type 36).</summary>
        /// <param name="value">The menu name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MenuName(string value) => CreateString(ShivaAttributeType.MENU_NAME, value);

        /// <summary>Creates a Shiva-User-Flags attribute (Type 37).</summary>
        /// <param name="value">The user flags. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserFlags(string value) => CreateString(ShivaAttributeType.USER_FLAGS, value);

        /// <summary>Creates a Shiva-Termtype attribute (Type 38).</summary>
        /// <param name="value">The terminal type. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Termtype(string value) => CreateString(ShivaAttributeType.TERMTYPE, value);

        /// <summary>Creates a Shiva-Break-Key attribute (Type 39).</summary>
        /// <param name="value">The break key sequence. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BreakKey(string value) => CreateString(ShivaAttributeType.BREAK_KEY, value);

        /// <summary>Creates a Shiva-Fwd-Key attribute (Type 40).</summary>
        /// <param name="value">The forward key sequence. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FwdKey(string value) => CreateString(ShivaAttributeType.FWD_KEY, value);

        /// <summary>Creates a Shiva-Bak-Key attribute (Type 41).</summary>
        /// <param name="value">The backward key sequence. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BakKey(string value) => CreateString(ShivaAttributeType.BAK_KEY, value);

        /// <summary>Creates a Shiva-LAT-Port attribute (Type 43).</summary>
        /// <param name="value">The LAT port name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LatPort(string value) => CreateString(ShivaAttributeType.LAT_PORT, value);

        /// <summary>Creates a Shiva-LAT-Groups attribute (Type 46).</summary>
        /// <param name="value">The LAT group codes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LatGroups(string value) => CreateString(ShivaAttributeType.LAT_GROUPS, value);

        /// <summary>Creates a Shiva-Called-Number attribute (Type 90).</summary>
        /// <param name="value">The called number. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledNumber(string value) => CreateString(ShivaAttributeType.CALLED_NUMBER, value);

        /// <summary>Creates a Shiva-Calling-Number attribute (Type 91).</summary>
        /// <param name="value">The calling number. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingNumber(string value) => CreateString(ShivaAttributeType.CALLING_NUMBER, value);

        /// <summary>Creates a Shiva-Customer-Id attribute (Type 92).</summary>
        /// <param name="value">The customer identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CustomerId(string value) => CreateString(ShivaAttributeType.CUSTOMER_ID, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Shiva-Acct-Serv-Switch attribute (Type 101).</summary>
        /// <param name="value">The accounting server switch address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes AcctServSwitch(IPAddress value) => CreateIpv4(ShivaAttributeType.ACCT_SERV_SWITCH, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(ShivaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(ShivaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(ShivaAttributeType type, IPAddress value)
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