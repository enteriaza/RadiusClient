using System.Buffers.Binary;
using System.Net;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a 3Com (IANA PEN 43) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.3com</c>.
    /// </summary>
    public enum ThreeComAttributeType : byte
    {
        /// <summary>3Com-User-Access-Level (Type 1). Integer. Access privilege level.</summary>
        USER_ACCESS_LEVEL = 1,

        /// <summary>3Com-VLAN-Name (Type 2). String. VLAN name to assign.</summary>
        VLAN_NAME = 2,

        /// <summary>3Com-Mobile-IP-Home (Type 3). String. Mobile IP home agent address.</summary>
        MOBILE_IP_HOME = 3,

        /// <summary>3Com-Require-Auth (Type 4). Integer. Whether authentication is required.</summary>
        REQUIRE_AUTH = 4,

        /// <summary>3Com-IP-Pool-Name (Type 5). String. Name of the IP address pool.</summary>
        IP_POOL_NAME = 5,

        /// <summary>3Com-Security-Alliance (Type 6). Integer. Security alliance identifier.</summary>
        SECURITY_ALLIANCE = 6,

        /// <summary>3Com-Audit-Trail (Type 7). Integer. Audit trail level.</summary>
        AUDIT_TRAIL = 7,

        /// <summary>3Com-NAS-Startup-Timestamp (Type 8). Integer (date). NAS startup time.</summary>
        NAS_STARTUP_TIMESTAMP = 8,

        /// <summary>3Com-IP-Host-Addr (Type 9). String. IP host address assigned to the user.</summary>
        IP_HOST_ADDR = 9,

        /// <summary>3Com-IP-Host-Mask (Type 10). String. IP host subnet mask.</summary>
        IP_HOST_MASK = 10,

        /// <summary>3Com-IP-Default-Gateway (Type 11). String. Default gateway IP address.</summary>
        IP_DEFAULT_GATEWAY = 11,

        /// <summary>3Com-IP-Primary-DNS (Type 12). String. Primary DNS server address.</summary>
        IP_PRIMARY_DNS = 12,

        /// <summary>3Com-IP-Secondary-DNS (Type 13). String. Secondary DNS server address.</summary>
        IP_SECONDARY_DNS = 13,

        /// <summary>3Com-Disconnect-Cause (Type 14). Integer. Reason for session disconnection.</summary>
        DISCONNECT_CAUSE = 14,

        /// <summary>3Com-Connect-Id (Type 15). Integer. Connection identifier.</summary>
        CONNECT_ID = 15,

        /// <summary>3Com-NAS-IP-Address (Type 16). IP address. NAS IP address.</summary>
        NAS_IP_ADDRESS = 16,

        /// <summary>3Com-Transition-To-VLAN-Id (Type 17). Integer. Target VLAN for session transition.</summary>
        TRANSITION_TO_VLAN_ID = 17,

        /// <summary>3Com-Logoff-URL (Type 18). String. URL to redirect to upon logoff.</summary>
        LOGOFF_URL = 18,

        /// <summary>3Com-Policy-Name (Type 19). String. Name of the applied policy.</summary>
        POLICY_NAME = 19,

        /// <summary>3Com-SmartTrunk-Rule (Type 20). String. SmartTrunk load balancing rule.</summary>
        SMARTTRUNK_RULE = 20,

        /// <summary>3Com-Tunnel-VLAN-Id (Type 21). String. VLAN identifier for tunnel traffic.</summary>
        TUNNEL_VLAN_ID = 21,

        /// <summary>3Com-VLAN-Id (Type 22). Integer. VLAN identifier to assign to the port.</summary>
        VLAN_ID = 22,

        /// <summary>3Com-Bandwidth-Control (Type 23). String. Bandwidth control parameters.</summary>
        BANDWIDTH_CONTROL = 23,

        /// <summary>3Com-IP-Primary-WINS (Type 24). String. Primary WINS server address.</summary>
        IP_PRIMARY_WINS = 24,

        /// <summary>3Com-IP-Secondary-WINS (Type 25). String. Secondary WINS server address.</summary>
        IP_SECONDARY_WINS = 25,

        /// <summary>3Com-Privacy-User-Name (Type 26). String. Privacy-protected user name.</summary>
        PRIVACY_USER_NAME = 26,

        /// <summary>3Com-Connect-Type (Type 27). Integer. Connection type for the session.</summary>
        CONNECT_TYPE = 27,

        /// <summary>3Com-NAS-Callback-Num (Type 28). String. Callback telephone number.</summary>
        NAS_CALLBACK_NUM = 28,

        /// <summary>3Com-Product-Name (Type 29). String. Product name of the NAS.</summary>
        PRODUCT_NAME = 29,

        /// <summary>3Com-NAS-Serial-Number (Type 30). String. Serial number of the NAS.</summary>
        NAS_SERIAL_NUMBER = 30
    }

    /// <summary>
    /// 3Com-User-Access-Level attribute values (Type 1).
    /// </summary>
    public enum THREECOM_USER_ACCESS_LEVEL
    {
        /// <summary>Visitor access (read-only).</summary>
        VISITOR = 0,

        /// <summary>Monitor access (read-only with diagnostics).</summary>
        MONITOR = 1,

        /// <summary>Manager access (full management).</summary>
        MANAGER = 2,

        /// <summary>Administrator access (full control including security).</summary>
        ADMINISTRATOR = 3
    }

    /// <summary>
    /// 3Com-Require-Auth attribute values (Type 4).
    /// </summary>
    public enum THREECOM_REQUIRE_AUTH
    {
        /// <summary>No re-authentication required.</summary>
        NOT_REQUIRED = 0,

        /// <summary>Re-authentication required.</summary>
        REQUIRED = 1
    }

    /// <summary>
    /// 3Com-Audit-Trail attribute values (Type 7).
    /// </summary>
    public enum THREECOM_AUDIT_TRAIL
    {
        /// <summary>Audit trail disabled.</summary>
        DISABLED = 0,

        /// <summary>Audit trail enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// 3Com-Disconnect-Cause attribute values (Type 14).
    /// </summary>
    public enum THREECOM_DISCONNECT_CAUSE
    {
        /// <summary>Unknown disconnect cause.</summary>
        UNKNOWN = 0,

        /// <summary>Session timed out normally.</summary>
        NORMAL_TIMEOUT = 1,

        /// <summary>Remote end hung up / closed connection.</summary>
        REMOTE_HANGUP = 2,

        /// <summary>Service was unavailable.</summary>
        SERVICE_UNAVAILABLE = 3,

        /// <summary>Callback initiated.</summary>
        CALLBACK = 4,

        /// <summary>Session was preempted.</summary>
        PREEMPTED = 5,

        /// <summary>Higher priority connection took over.</summary>
        HIGHER_PRIORITY = 6,

        /// <summary>No modem available.</summary>
        NO_MODEM = 7,

        /// <summary>No carrier detected.</summary>
        NO_CARRIER = 8,

        /// <summary>No protocol detected.</summary>
        NO_DETECT = 9,

        /// <summary>Bad line quality.</summary>
        BAD_LINE = 10,

        /// <summary>Max retries reached.</summary>
        MAX_RETRIES = 11,

        /// <summary>Timer expired.</summary>
        TIMER_EXPIRED = 12,

        /// <summary>Admin reset of session.</summary>
        ADMIN_RESET = 13,

        /// <summary>User request to disconnect.</summary>
        USER_REQUEST = 14,

        /// <summary>Host request to disconnect.</summary>
        HOST_REQUEST = 15,

        /// <summary>Administrative reboot.</summary>
        ADMIN_REBOOT = 16,

        /// <summary>Port error detected.</summary>
        PORT_ERROR = 17,

        /// <summary>NAS error detected.</summary>
        NAS_ERROR = 18,

        /// <summary>NAS request to disconnect.</summary>
        NAS_REQUEST = 19,

        /// <summary>Undefined reason code 20.</summary>
        UNDEFINED_20 = 20,

        /// <summary>Undefined reason code 21.</summary>
        UNDEFINED_21 = 21,

        /// <summary>No resource available.</summary>
        NO_RESOURCE = 22,

        /// <summary>Invalid destination.</summary>
        INVALID_DESTINATION = 23,

        /// <summary>Port suspended.</summary>
        PORT_SUSPENDED = 24,

        /// <summary>Service disabled.</summary>
        SERVICE_DISABLED = 25,

        /// <summary>Degraded signal.</summary>
        DEGRADED_SIGNAL = 26,

        /// <summary>Connection closed due to callback.</summary>
        CONNECTION_CALLBACK = 27,

        /// <summary>User error.</summary>
        USER_ERROR = 28,

        /// <summary>Host error.</summary>
        HOST_ERROR = 29,

        /// <summary>Negotiation failure.</summary>
        NEGOTIATION_FAILURE = 30,

        /// <summary>Keep-alive failure.</summary>
        KEEPALIVE_FAILURE = 31,

        /// <summary>Protocol error.</summary>
        PROTOCOL_ERROR = 32,

        /// <summary>Init failure.</summary>
        INIT_FAILURE = 33,

        /// <summary>Facility failure.</summary>
        FACILITY_FAILURE = 34,

        /// <summary>Number changed.</summary>
        NUMBER_CHANGED = 35,

        /// <summary>Retry failure.</summary>
        RETRY_FAILURE = 36
    }

    /// <summary>
    /// 3Com-Connect-Type attribute values (Type 27).
    /// </summary>
    public enum THREECOM_CONNECT_TYPE
    {
        /// <summary>Not connected.</summary>
        NOT_CONNECTED = 0,

        /// <summary>Connected (generic).</summary>
        CONNECTED = 1,

        /// <summary>Console session.</summary>
        CONSOLE = 2,

        /// <summary>Remote access session.</summary>
        REMOTE_ACCESS = 3,

        /// <summary>Telnet session.</summary>
        TELNET = 4,

        /// <summary>Message digest session.</summary>
        MESSAGE_DIGEST = 5,

        /// <summary>Login authentication session.</summary>
        LOGIN = 6,

        /// <summary>EXEC shell session.</summary>
        EXEC = 7,

        /// <summary>IAPP session (Inter-Access Point Protocol).</summary>
        IAPP = 8,

        /// <summary>Dot1X session (IEEE 802.1X).</summary>
        DOT1X = 9
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing 3Com (IANA PEN 43)
    /// Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.3com</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// 3Com's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 43</c>.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_REQUEST);
    /// packet.SetAttribute(ThreeComAttributes.UserAccessLevel(THREECOM_USER_ACCESS_LEVEL.MANAGER));
    /// packet.SetAttribute(ThreeComAttributes.VlanName("Engineering"));
    /// packet.SetAttribute(ThreeComAttributes.NasIpAddress(IPAddress.Parse("10.0.0.1")));
    /// </code>
    /// </remarks>
    public static class ThreeComAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for 3Com Corporation.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 43;

        #region Integer Attributes

        /// <summary>
        /// Creates a 3Com-User-Access-Level attribute (Type 1) with the specified access level.
        /// </summary>
        /// <param name="value">The access level to assign. See <see cref="THREECOM_USER_ACCESS_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserAccessLevel(THREECOM_USER_ACCESS_LEVEL value)
        {
            return CreateInteger(ThreeComAttributeType.USER_ACCESS_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a 3Com-Require-Auth attribute (Type 4) indicating whether re-authentication is required.
        /// </summary>
        /// <param name="value">Whether authentication is required. See <see cref="THREECOM_REQUIRE_AUTH"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RequireAuth(THREECOM_REQUIRE_AUTH value)
        {
            return CreateInteger(ThreeComAttributeType.REQUIRE_AUTH, (int)value);
        }

        /// <summary>
        /// Creates a 3Com-Security-Alliance attribute (Type 6) with the specified security alliance identifier.
        /// </summary>
        /// <param name="value">The security alliance identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SecurityAlliance(int value)
        {
            return CreateInteger(ThreeComAttributeType.SECURITY_ALLIANCE, value);
        }

        /// <summary>
        /// Creates a 3Com-Audit-Trail attribute (Type 7) with the specified audit trail setting.
        /// </summary>
        /// <param name="value">Whether the audit trail is enabled. See <see cref="THREECOM_AUDIT_TRAIL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuditTrail(THREECOM_AUDIT_TRAIL value)
        {
            return CreateInteger(ThreeComAttributeType.AUDIT_TRAIL, (int)value);
        }

        /// <summary>
        /// Creates a 3Com-NAS-Startup-Timestamp attribute (Type 8) with the specified timestamp value.
        /// </summary>
        /// <param name="value">The NAS startup timestamp as a 32-bit Unix timestamp.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NasStartupTimestamp(int value)
        {
            return CreateInteger(ThreeComAttributeType.NAS_STARTUP_TIMESTAMP, value);
        }

        /// <summary>
        /// Creates a 3Com-Disconnect-Cause attribute (Type 14) with the specified cause code.
        /// </summary>
        /// <param name="value">The disconnect cause. See <see cref="THREECOM_DISCONNECT_CAUSE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DisconnectCause(THREECOM_DISCONNECT_CAUSE value)
        {
            return CreateInteger(ThreeComAttributeType.DISCONNECT_CAUSE, (int)value);
        }

        /// <summary>
        /// Creates a 3Com-Connect-Id attribute (Type 15) with the specified connection identifier.
        /// </summary>
        /// <param name="value">The connection identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ConnectId(int value)
        {
            return CreateInteger(ThreeComAttributeType.CONNECT_ID, value);
        }

        /// <summary>
        /// Creates a 3Com-Transition-To-VLAN-Id attribute (Type 17) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The target VLAN identifier for session transition.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TransitionToVlanId(int value)
        {
            return CreateInteger(ThreeComAttributeType.TRANSITION_TO_VLAN_ID, value);
        }

        /// <summary>
        /// Creates a 3Com-VLAN-Id attribute (Type 22) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier to assign to the port.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(ThreeComAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a 3Com-Connect-Type attribute (Type 27) with the specified connection type.
        /// </summary>
        /// <param name="value">The connection type. See <see cref="THREECOM_CONNECT_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ConnectType(THREECOM_CONNECT_TYPE value)
        {
            return CreateInteger(ThreeComAttributeType.CONNECT_TYPE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a 3Com-VLAN-Name attribute (Type 2) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(ThreeComAttributeType.VLAN_NAME, value);
        }

        /// <summary>
        /// Creates a 3Com-Mobile-IP-Home attribute (Type 3) with the specified mobile IP home agent.
        /// </summary>
        /// <param name="value">The mobile IP home agent address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MobileIpHome(string value)
        {
            return CreateString(ThreeComAttributeType.MOBILE_IP_HOME, value);
        }

        /// <summary>
        /// Creates a 3Com-IP-Pool-Name attribute (Type 5) with the specified pool name.
        /// </summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPoolName(string value)
        {
            return CreateString(ThreeComAttributeType.IP_POOL_NAME, value);
        }

        /// <summary>
        /// Creates a 3Com-IP-Host-Addr attribute (Type 9) with the specified host address string.
        /// </summary>
        /// <param name="value">The IP host address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpHostAddr(string value)
        {
            return CreateString(ThreeComAttributeType.IP_HOST_ADDR, value);
        }

        /// <summary>
        /// Creates a 3Com-IP-Host-Mask attribute (Type 10) with the specified subnet mask string.
        /// </summary>
        /// <param name="value">The IP host subnet mask string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpHostMask(string value)
        {
            return CreateString(ThreeComAttributeType.IP_HOST_MASK, value);
        }

        /// <summary>
        /// Creates a 3Com-IP-Default-Gateway attribute (Type 11) with the specified gateway address string.
        /// </summary>
        /// <param name="value">The default gateway IP address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpDefaultGateway(string value)
        {
            return CreateString(ThreeComAttributeType.IP_DEFAULT_GATEWAY, value);
        }

        /// <summary>
        /// Creates a 3Com-IP-Primary-DNS attribute (Type 12) with the specified DNS server address string.
        /// </summary>
        /// <param name="value">The primary DNS server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPrimaryDns(string value)
        {
            return CreateString(ThreeComAttributeType.IP_PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates a 3Com-IP-Secondary-DNS attribute (Type 13) with the specified DNS server address string.
        /// </summary>
        /// <param name="value">The secondary DNS server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpSecondaryDns(string value)
        {
            return CreateString(ThreeComAttributeType.IP_SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates a 3Com-Logoff-URL attribute (Type 18) with the specified logoff redirect URL.
        /// </summary>
        /// <param name="value">The URL to redirect to upon logoff. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LogoffUrl(string value)
        {
            return CreateString(ThreeComAttributeType.LOGOFF_URL, value);
        }

        /// <summary>
        /// Creates a 3Com-Policy-Name attribute (Type 19) with the specified policy name.
        /// </summary>
        /// <param name="value">The policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(ThreeComAttributeType.POLICY_NAME, value);
        }

        /// <summary>
        /// Creates a 3Com-SmartTrunk-Rule attribute (Type 20) with the specified rule string.
        /// </summary>
        /// <param name="value">The SmartTrunk load balancing rule. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SmartTrunkRule(string value)
        {
            return CreateString(ThreeComAttributeType.SMARTTRUNK_RULE, value);
        }

        /// <summary>
        /// Creates a 3Com-Tunnel-VLAN-Id attribute (Type 21) with the specified tunnel VLAN identifier string.
        /// </summary>
        /// <param name="value">The tunnel VLAN identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelVlanId(string value)
        {
            return CreateString(ThreeComAttributeType.TUNNEL_VLAN_ID, value);
        }

        /// <summary>
        /// Creates a 3Com-Bandwidth-Control attribute (Type 23) with the specified bandwidth control parameters.
        /// </summary>
        /// <param name="value">The bandwidth control parameters. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BandwidthControl(string value)
        {
            return CreateString(ThreeComAttributeType.BANDWIDTH_CONTROL, value);
        }

        /// <summary>
        /// Creates a 3Com-IP-Primary-WINS attribute (Type 24) with the specified WINS server address string.
        /// </summary>
        /// <param name="value">The primary WINS server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPrimaryWins(string value)
        {
            return CreateString(ThreeComAttributeType.IP_PRIMARY_WINS, value);
        }

        /// <summary>
        /// Creates a 3Com-IP-Secondary-WINS attribute (Type 25) with the specified WINS server address string.
        /// </summary>
        /// <param name="value">The secondary WINS server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpSecondaryWins(string value)
        {
            return CreateString(ThreeComAttributeType.IP_SECONDARY_WINS, value);
        }

        /// <summary>
        /// Creates a 3Com-Privacy-User-Name attribute (Type 26) with the specified privacy user name.
        /// </summary>
        /// <param name="value">The privacy-protected user name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrivacyUserName(string value)
        {
            return CreateString(ThreeComAttributeType.PRIVACY_USER_NAME, value);
        }

        /// <summary>
        /// Creates a 3Com-NAS-Callback-Num attribute (Type 28) with the specified callback number.
        /// </summary>
        /// <param name="value">The callback telephone number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NasCallbackNum(string value)
        {
            return CreateString(ThreeComAttributeType.NAS_CALLBACK_NUM, value);
        }

        /// <summary>
        /// Creates a 3Com-Product-Name attribute (Type 29) with the specified product name.
        /// </summary>
        /// <param name="value">The NAS product name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProductName(string value)
        {
            return CreateString(ThreeComAttributeType.PRODUCT_NAME, value);
        }

        /// <summary>
        /// Creates a 3Com-NAS-Serial-Number attribute (Type 30) with the specified serial number.
        /// </summary>
        /// <param name="value">The NAS serial number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NasSerialNumber(string value)
        {
            return CreateString(ThreeComAttributeType.NAS_SERIAL_NUMBER, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates a 3Com-NAS-IP-Address attribute (Type 16) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The NAS IP address. Must be an IPv4 (<see cref="System.Net.Sockets.AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes NasIpAddress(IPAddress value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                throw new ArgumentException(
                    $"Expected an IPv4 (InterNetwork) address, got '{value.AddressFamily}'.",
                    nameof(value));

            byte[] addrBytes = new byte[4];
            value.TryWriteBytes(addrBytes, out _);

            return new VendorSpecificAttributes(VENDOR_ID, (byte)ThreeComAttributeType.NAS_IP_ADDRESS, addrBytes);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified 3Com attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ThreeComAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified 3Com attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ThreeComAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}