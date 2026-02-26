using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Travelping (IANA PEN 18681) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.travelping</c>.
    /// </summary>
    /// <remarks>
    /// Travelping GmbH produces software-defined networking (SDN) and network
    /// functions virtualization (NFV) solutions for mobile and fixed-line
    /// operators, including virtual PGW/GGSN (ergw), AAA, and subscriber
    /// management platforms.
    /// </remarks>
    public enum TravelpingAttributeType : byte
    {
        /// <summary>TP-Gateway-Version (Type 1). String. Gateway software version.</summary>
        GATEWAY_VERSION = 1,

        /// <summary>TP-Firmware-Variant (Type 2). String. Firmware variant identifier.</summary>
        FIRMWARE_VARIANT = 2,

        /// <summary>TP-Gateway-IP (Type 3). IP address. Gateway IP address.</summary>
        GATEWAY_IP = 3,

        /// <summary>TP-Max-Input-Octets (Type 10). Integer. Maximum input octets.</summary>
        MAX_INPUT_OCTETS = 10,

        /// <summary>TP-Max-Output-Octets (Type 11). Integer. Maximum output octets.</summary>
        MAX_OUTPUT_OCTETS = 11,

        /// <summary>TP-Max-Total-Octets (Type 12). Integer. Maximum total octets.</summary>
        MAX_TOTAL_OCTETS = 12,

        /// <summary>TP-Exit-Access-Class-Id (Type 13). String. Exit access class identifier.</summary>
        EXIT_ACCESS_CLASS_ID = 13,

        /// <summary>TP-Access-Class-Id (Type 14). String. Access class identifier.</summary>
        ACCESS_CLASS_ID = 14,

        /// <summary>TP-Access-Rule (Type 20). String. Access rule definition.</summary>
        ACCESS_RULE = 20,

        /// <summary>TP-Access-Group (Type 21). String. Access group name.</summary>
        ACCESS_GROUP = 21,

        /// <summary>TP-NAT-IP-Address (Type 22). IP address. NAT IP address.</summary>
        NAT_IP_ADDRESS = 22,

        /// <summary>TP-NAT-Pool-Id (Type 23). String. NAT pool identifier.</summary>
        NAT_POOL_ID = 23,

        /// <summary>TP-NAT-Port-Start (Type 24). Integer. NAT port range start.</summary>
        NAT_PORT_START = 24,

        /// <summary>TP-NAT-Port-End (Type 25). Integer. NAT port range end.</summary>
        NAT_PORT_END = 25,

        /// <summary>TP-Keep-Alive-Timeout (Type 26). Integer. Keep-alive timeout in seconds.</summary>
        KEEP_ALIVE_TIMEOUT = 26,

        /// <summary>TP-TLS-Auth-Type (Type 30). Integer. TLS authentication type.</summary>
        TLS_AUTH_TYPE = 30,

        /// <summary>TP-TLS-Pre-Shared-Key (Type 31). String. TLS pre-shared key.</summary>
        TLS_PRE_SHARED_KEY = 31,

        /// <summary>TP-CAPWAP-Timestamp (Type 32). Integer. CAPWAP timestamp.</summary>
        CAPWAP_TIMESTAMP = 32,

        /// <summary>TP-CAPWAP-WTP-Version (Type 33). String. CAPWAP WTP version.</summary>
        CAPWAP_WTP_VERSION = 33,

        /// <summary>TP-Location-Id (Type 34). String. Location identifier.</summary>
        LOCATION_ID = 34,

        /// <summary>TP-Excess-Input-Octets (Type 35). Integer. Excess input octets.</summary>
        EXCESS_INPUT_OCTETS = 35,

        /// <summary>TP-Excess-Output-Octets (Type 36). Integer. Excess output octets.</summary>
        EXCESS_OUTPUT_OCTETS = 36,

        /// <summary>TP-Excess-Total-Octets (Type 37). Integer. Excess total octets.</summary>
        EXCESS_TOTAL_OCTETS = 37,

        /// <summary>TP-Trace-Id (Type 38). String. Trace identifier.</summary>
        TRACE_ID = 38,

        /// <summary>TP-IMSI (Type 40). String. International Mobile Subscriber Identity.</summary>
        IMSI = 40,

        /// <summary>TP-MSISDN (Type 41). String. Mobile Station ISDN Number.</summary>
        MSISDN = 41,

        /// <summary>TP-APN (Type 42). String. Access Point Name.</summary>
        APN = 42
    }

    /// <summary>
    /// TP-TLS-Auth-Type attribute values (Type 30).
    /// </summary>
    public enum TP_TLS_AUTH_TYPE
    {
        /// <summary>Pre-shared key authentication.</summary>
        PRE_SHARED_KEY = 0,

        /// <summary>X.509 certificate authentication.</summary>
        X509_CERTIFICATE = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Travelping
    /// (IANA PEN 18681) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.travelping</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Travelping's vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 18681</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Travelping SDN/NFV platforms (ergw virtual
    /// PGW/GGSN) for RADIUS-based gateway version and firmware identification,
    /// data volume quotas (input/output/total octets and excess counters), access
    /// class and access group/rule assignment, NAT IP address and port range
    /// provisioning, keep-alive timeout management, TLS authentication
    /// configuration, CAPWAP timestamp and WTP version tracking, location
    /// identification, trace ID for diagnostics, and 3GPP subscriber
    /// identification (IMSI, MSISDN, APN).
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(TravelpingAttributes.Imsi("310260123456789"));
    /// packet.SetAttribute(TravelpingAttributes.Apn("internet.example.com"));
    /// packet.SetAttribute(TravelpingAttributes.AccessClassId("gold"));
    /// packet.SetAttribute(TravelpingAttributes.MaxTotalOctets(1073741824));
    /// packet.SetAttribute(TravelpingAttributes.NatPoolId("public-pool-1"));
    /// </code>
    /// </remarks>
    public static class TravelpingAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Travelping.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 18681;

        #region Integer Attributes

        /// <summary>Creates a TP-Max-Input-Octets attribute (Type 10).</summary>
        /// <param name="value">The maximum input octets.</param>
        public static VendorSpecificAttributes MaxInputOctets(int value) => CreateInteger(TravelpingAttributeType.MAX_INPUT_OCTETS, value);

        /// <summary>Creates a TP-Max-Output-Octets attribute (Type 11).</summary>
        /// <param name="value">The maximum output octets.</param>
        public static VendorSpecificAttributes MaxOutputOctets(int value) => CreateInteger(TravelpingAttributeType.MAX_OUTPUT_OCTETS, value);

        /// <summary>Creates a TP-Max-Total-Octets attribute (Type 12).</summary>
        /// <param name="value">The maximum total octets.</param>
        public static VendorSpecificAttributes MaxTotalOctets(int value) => CreateInteger(TravelpingAttributeType.MAX_TOTAL_OCTETS, value);

        /// <summary>Creates a TP-NAT-Port-Start attribute (Type 24).</summary>
        /// <param name="value">The NAT port range start.</param>
        public static VendorSpecificAttributes NatPortStart(int value) => CreateInteger(TravelpingAttributeType.NAT_PORT_START, value);

        /// <summary>Creates a TP-NAT-Port-End attribute (Type 25).</summary>
        /// <param name="value">The NAT port range end.</param>
        public static VendorSpecificAttributes NatPortEnd(int value) => CreateInteger(TravelpingAttributeType.NAT_PORT_END, value);

        /// <summary>Creates a TP-Keep-Alive-Timeout attribute (Type 26).</summary>
        /// <param name="value">The keep-alive timeout in seconds.</param>
        public static VendorSpecificAttributes KeepAliveTimeout(int value) => CreateInteger(TravelpingAttributeType.KEEP_ALIVE_TIMEOUT, value);

        /// <summary>Creates a TP-TLS-Auth-Type attribute (Type 30).</summary>
        /// <param name="value">The TLS authentication type. See <see cref="TP_TLS_AUTH_TYPE"/>.</param>
        public static VendorSpecificAttributes TlsAuthType(TP_TLS_AUTH_TYPE value) => CreateInteger(TravelpingAttributeType.TLS_AUTH_TYPE, (int)value);

        /// <summary>Creates a TP-CAPWAP-Timestamp attribute (Type 32).</summary>
        /// <param name="value">The CAPWAP timestamp.</param>
        public static VendorSpecificAttributes CapwapTimestamp(int value) => CreateInteger(TravelpingAttributeType.CAPWAP_TIMESTAMP, value);

        /// <summary>Creates a TP-Excess-Input-Octets attribute (Type 35).</summary>
        /// <param name="value">The excess input octets.</param>
        public static VendorSpecificAttributes ExcessInputOctets(int value) => CreateInteger(TravelpingAttributeType.EXCESS_INPUT_OCTETS, value);

        /// <summary>Creates a TP-Excess-Output-Octets attribute (Type 36).</summary>
        /// <param name="value">The excess output octets.</param>
        public static VendorSpecificAttributes ExcessOutputOctets(int value) => CreateInteger(TravelpingAttributeType.EXCESS_OUTPUT_OCTETS, value);

        /// <summary>Creates a TP-Excess-Total-Octets attribute (Type 37).</summary>
        /// <param name="value">The excess total octets.</param>
        public static VendorSpecificAttributes ExcessTotalOctets(int value) => CreateInteger(TravelpingAttributeType.EXCESS_TOTAL_OCTETS, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a TP-Gateway-Version attribute (Type 1).</summary>
        /// <param name="value">The gateway software version. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GatewayVersion(string value) => CreateString(TravelpingAttributeType.GATEWAY_VERSION, value);

        /// <summary>Creates a TP-Firmware-Variant attribute (Type 2).</summary>
        /// <param name="value">The firmware variant identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FirmwareVariant(string value) => CreateString(TravelpingAttributeType.FIRMWARE_VARIANT, value);

        /// <summary>Creates a TP-Exit-Access-Class-Id attribute (Type 13).</summary>
        /// <param name="value">The exit access class identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ExitAccessClassId(string value) => CreateString(TravelpingAttributeType.EXIT_ACCESS_CLASS_ID, value);

        /// <summary>Creates a TP-Access-Class-Id attribute (Type 14).</summary>
        /// <param name="value">The access class identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessClassId(string value) => CreateString(TravelpingAttributeType.ACCESS_CLASS_ID, value);

        /// <summary>Creates a TP-Access-Rule attribute (Type 20).</summary>
        /// <param name="value">The access rule definition. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessRule(string value) => CreateString(TravelpingAttributeType.ACCESS_RULE, value);

        /// <summary>Creates a TP-Access-Group attribute (Type 21).</summary>
        /// <param name="value">The access group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessGroup(string value) => CreateString(TravelpingAttributeType.ACCESS_GROUP, value);

        /// <summary>Creates a TP-NAT-Pool-Id attribute (Type 23).</summary>
        /// <param name="value">The NAT pool identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatPoolId(string value) => CreateString(TravelpingAttributeType.NAT_POOL_ID, value);

        /// <summary>Creates a TP-TLS-Pre-Shared-Key attribute (Type 31).</summary>
        /// <param name="value">The TLS pre-shared key. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TlsPreSharedKey(string value) => CreateString(TravelpingAttributeType.TLS_PRE_SHARED_KEY, value);

        /// <summary>Creates a TP-CAPWAP-WTP-Version attribute (Type 33).</summary>
        /// <param name="value">The CAPWAP WTP version. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CapwapWtpVersion(string value) => CreateString(TravelpingAttributeType.CAPWAP_WTP_VERSION, value);

        /// <summary>Creates a TP-Location-Id attribute (Type 34).</summary>
        /// <param name="value">The location identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LocationId(string value) => CreateString(TravelpingAttributeType.LOCATION_ID, value);

        /// <summary>Creates a TP-Trace-Id attribute (Type 38).</summary>
        /// <param name="value">The trace identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TraceId(string value) => CreateString(TravelpingAttributeType.TRACE_ID, value);

        /// <summary>Creates a TP-IMSI attribute (Type 40).</summary>
        /// <param name="value">The IMSI. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imsi(string value) => CreateString(TravelpingAttributeType.IMSI, value);

        /// <summary>Creates a TP-MSISDN attribute (Type 41).</summary>
        /// <param name="value">The MSISDN. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Msisdn(string value) => CreateString(TravelpingAttributeType.MSISDN, value);

        /// <summary>Creates a TP-APN attribute (Type 42).</summary>
        /// <param name="value">The Access Point Name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Apn(string value) => CreateString(TravelpingAttributeType.APN, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a TP-Gateway-IP attribute (Type 3).</summary>
        /// <param name="value">The gateway IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes GatewayIp(IPAddress value) => CreateIpv4(TravelpingAttributeType.GATEWAY_IP, value);

        /// <summary>Creates a TP-NAT-IP-Address attribute (Type 22).</summary>
        /// <param name="value">The NAT IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes NatIpAddress(IPAddress value) => CreateIpv4(TravelpingAttributeType.NAT_IP_ADDRESS, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(TravelpingAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(TravelpingAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(TravelpingAttributeType type, IPAddress value)
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