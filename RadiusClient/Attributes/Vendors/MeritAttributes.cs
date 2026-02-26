using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Merit Network (IANA PEN 61) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.merit</c>.
    /// </summary>
    /// <remarks>
    /// Merit Network, Inc. is a non-profit networking consortium operating Michigan's
    /// research and education network. Merit was instrumental in the early development
    /// of RADIUS and operated the original NSFNET backbone. These attributes are
    /// used by Merit AAA servers and associated NAS equipment.
    /// </remarks>
    public enum MeritAttributeType : byte
    {
        /// <summary>Merit-User-Id (Type 1). String. User identifier.</summary>
        USER_ID = 1,

        /// <summary>Merit-User-Realm (Type 2). String. User realm (domain).</summary>
        USER_REALM = 2,

        /// <summary>Merit-Proxy-Action (Type 10). String. Proxy action directive.</summary>
        PROXY_ACTION = 10,

        /// <summary>Merit-User-Name (Type 11). String. User name string.</summary>
        USER_NAME = 11,

        /// <summary>Merit-User-Password (Type 12). String. User password string.</summary>
        USER_PASSWORD = 12,

        /// <summary>Merit-CHAP-Password (Type 13). String. CHAP password.</summary>
        CHAP_PASSWORD = 13,

        /// <summary>Merit-NAS-IP-Address (Type 14). String. NAS IP address string.</summary>
        NAS_IP_ADDRESS = 14,

        /// <summary>Merit-Service-Type (Type 15). String. Service type string.</summary>
        SERVICE_TYPE = 15,

        /// <summary>Merit-Framed-Protocol (Type 16). String. Framed protocol string.</summary>
        FRAMED_PROTOCOL = 16,

        /// <summary>Merit-Framed-IP-Address (Type 17). String. Framed IP address string.</summary>
        FRAMED_IP_ADDRESS = 17,

        /// <summary>Merit-Framed-IP-Netmask (Type 18). String. Framed IP netmask string.</summary>
        FRAMED_IP_NETMASK = 18,

        /// <summary>Merit-Framed-Routing (Type 19). String. Framed routing method string.</summary>
        FRAMED_ROUTING = 19,

        /// <summary>Merit-Filter-Id (Type 20). String. Filter identifier.</summary>
        FILTER_ID = 20,

        /// <summary>Merit-Framed-MTU (Type 21). String. Framed MTU string.</summary>
        FRAMED_MTU = 21,

        /// <summary>Merit-Framed-Compression (Type 22). String. Framed compression string.</summary>
        FRAMED_COMPRESSION = 22,

        /// <summary>Merit-Login-IP-Host (Type 23). String. Login IP host string.</summary>
        LOGIN_IP_HOST = 23,

        /// <summary>Merit-Login-Service (Type 24). String. Login service string.</summary>
        LOGIN_SERVICE = 24,

        /// <summary>Merit-Login-TCP-Port (Type 25). String. Login TCP port string.</summary>
        LOGIN_TCP_PORT = 25,

        /// <summary>Merit-Reply-Message (Type 26). String. Reply message.</summary>
        REPLY_MESSAGE = 26,

        /// <summary>Merit-Callback-Number (Type 27). String. Callback number.</summary>
        CALLBACK_NUMBER = 27,

        /// <summary>Merit-Callback-Id (Type 28). String. Callback identifier.</summary>
        CALLBACK_ID = 28,

        /// <summary>Merit-Framed-Route (Type 29). String. Framed route string.</summary>
        FRAMED_ROUTE = 29,

        /// <summary>Merit-Framed-IPX-Network (Type 30). String. Framed IPX network string.</summary>
        FRAMED_IPX_NETWORK = 30,

        /// <summary>Merit-State (Type 31). String. State information.</summary>
        STATE = 31,

        /// <summary>Merit-Class (Type 32). String. Class string.</summary>
        CLASS = 32,

        /// <summary>Merit-Session-Timeout (Type 33). String. Session timeout string.</summary>
        SESSION_TIMEOUT = 33,

        /// <summary>Merit-Idle-Timeout (Type 34). String. Idle timeout string.</summary>
        IDLE_TIMEOUT = 34,

        /// <summary>Merit-Termination-Action (Type 35). String. Termination action string.</summary>
        TERMINATION_ACTION = 35
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Merit Network
    /// (IANA PEN 61) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.merit</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Merit's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 61</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Merit Network AAA servers for RADIUS-based
    /// proxy processing, user identity and realm management, and vendor-specific
    /// copies of standard RADIUS attributes for use in proxy chaining and
    /// attribute mapping scenarios.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_REQUEST);
    /// packet.SetAttribute(MeritAttributes.UserId("jdoe"));
    /// packet.SetAttribute(MeritAttributes.UserRealm("example.edu"));
    /// packet.SetAttribute(MeritAttributes.ProxyAction("PROXY"));
    /// </code>
    /// </remarks>
    public static class MeritAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Merit Network.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 61;

        #region String Attributes

        /// <summary>Creates a Merit-User-Id attribute (Type 1).</summary>
        /// <param name="value">The user identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserId(string value) => CreateString(MeritAttributeType.USER_ID, value);

        /// <summary>Creates a Merit-User-Realm attribute (Type 2).</summary>
        /// <param name="value">The user realm (domain). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRealm(string value) => CreateString(MeritAttributeType.USER_REALM, value);

        /// <summary>Creates a Merit-Proxy-Action attribute (Type 10).</summary>
        /// <param name="value">The proxy action directive. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProxyAction(string value) => CreateString(MeritAttributeType.PROXY_ACTION, value);

        /// <summary>Creates a Merit-User-Name attribute (Type 11).</summary>
        /// <param name="value">The user name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserName(string value) => CreateString(MeritAttributeType.USER_NAME, value);

        /// <summary>Creates a Merit-User-Password attribute (Type 12).</summary>
        /// <param name="value">The user password. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserPassword(string value) => CreateString(MeritAttributeType.USER_PASSWORD, value);

        /// <summary>Creates a Merit-CHAP-Password attribute (Type 13).</summary>
        /// <param name="value">The CHAP password. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChapPassword(string value) => CreateString(MeritAttributeType.CHAP_PASSWORD, value);

        /// <summary>Creates a Merit-NAS-IP-Address attribute (Type 14).</summary>
        /// <param name="value">The NAS IP address string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NasIpAddress(string value) => CreateString(MeritAttributeType.NAS_IP_ADDRESS, value);

        /// <summary>Creates a Merit-Service-Type attribute (Type 15).</summary>
        /// <param name="value">The service type string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceType(string value) => CreateString(MeritAttributeType.SERVICE_TYPE, value);

        /// <summary>Creates a Merit-Framed-Protocol attribute (Type 16).</summary>
        /// <param name="value">The framed protocol string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedProtocol(string value) => CreateString(MeritAttributeType.FRAMED_PROTOCOL, value);

        /// <summary>Creates a Merit-Framed-IP-Address attribute (Type 17).</summary>
        /// <param name="value">The framed IP address string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedIpAddress(string value) => CreateString(MeritAttributeType.FRAMED_IP_ADDRESS, value);

        /// <summary>Creates a Merit-Framed-IP-Netmask attribute (Type 18).</summary>
        /// <param name="value">The framed IP netmask string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedIpNetmask(string value) => CreateString(MeritAttributeType.FRAMED_IP_NETMASK, value);

        /// <summary>Creates a Merit-Framed-Routing attribute (Type 19).</summary>
        /// <param name="value">The framed routing method string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedRouting(string value) => CreateString(MeritAttributeType.FRAMED_ROUTING, value);

        /// <summary>Creates a Merit-Filter-Id attribute (Type 20).</summary>
        /// <param name="value">The filter identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FilterId(string value) => CreateString(MeritAttributeType.FILTER_ID, value);

        /// <summary>Creates a Merit-Framed-MTU attribute (Type 21).</summary>
        /// <param name="value">The framed MTU string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedMtu(string value) => CreateString(MeritAttributeType.FRAMED_MTU, value);

        /// <summary>Creates a Merit-Framed-Compression attribute (Type 22).</summary>
        /// <param name="value">The framed compression string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedCompression(string value) => CreateString(MeritAttributeType.FRAMED_COMPRESSION, value);

        /// <summary>Creates a Merit-Login-IP-Host attribute (Type 23).</summary>
        /// <param name="value">The login IP host string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LoginIpHost(string value) => CreateString(MeritAttributeType.LOGIN_IP_HOST, value);

        /// <summary>Creates a Merit-Login-Service attribute (Type 24).</summary>
        /// <param name="value">The login service string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LoginService(string value) => CreateString(MeritAttributeType.LOGIN_SERVICE, value);

        /// <summary>Creates a Merit-Login-TCP-Port attribute (Type 25).</summary>
        /// <param name="value">The login TCP port string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LoginTcpPort(string value) => CreateString(MeritAttributeType.LOGIN_TCP_PORT, value);

        /// <summary>Creates a Merit-Reply-Message attribute (Type 26).</summary>
        /// <param name="value">The reply message. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ReplyMessage(string value) => CreateString(MeritAttributeType.REPLY_MESSAGE, value);

        /// <summary>Creates a Merit-Callback-Number attribute (Type 27).</summary>
        /// <param name="value">The callback number. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallbackNumber(string value) => CreateString(MeritAttributeType.CALLBACK_NUMBER, value);

        /// <summary>Creates a Merit-Callback-Id attribute (Type 28).</summary>
        /// <param name="value">The callback identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallbackId(string value) => CreateString(MeritAttributeType.CALLBACK_ID, value);

        /// <summary>Creates a Merit-Framed-Route attribute (Type 29).</summary>
        /// <param name="value">The framed route string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedRoute(string value) => CreateString(MeritAttributeType.FRAMED_ROUTE, value);

        /// <summary>Creates a Merit-Framed-IPX-Network attribute (Type 30).</summary>
        /// <param name="value">The framed IPX network string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedIpxNetwork(string value) => CreateString(MeritAttributeType.FRAMED_IPX_NETWORK, value);

        /// <summary>Creates a Merit-State attribute (Type 31).</summary>
        /// <param name="value">The state information. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes State(string value) => CreateString(MeritAttributeType.STATE, value);

        /// <summary>Creates a Merit-Class attribute (Type 32).</summary>
        /// <param name="value">The class string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Class(string value) => CreateString(MeritAttributeType.CLASS, value);

        /// <summary>Creates a Merit-Session-Timeout attribute (Type 33).</summary>
        /// <param name="value">The session timeout string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionTimeout(string value) => CreateString(MeritAttributeType.SESSION_TIMEOUT, value);

        /// <summary>Creates a Merit-Idle-Timeout attribute (Type 34).</summary>
        /// <param name="value">The idle timeout string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IdleTimeout(string value) => CreateString(MeritAttributeType.IDLE_TIMEOUT, value);

        /// <summary>Creates a Merit-Termination-Action attribute (Type 35).</summary>
        /// <param name="value">The termination action string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TerminationAction(string value) => CreateString(MeritAttributeType.TERMINATION_ACTION, value);

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Merit attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(MeritAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}