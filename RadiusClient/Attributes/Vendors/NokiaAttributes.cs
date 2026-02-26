using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Nokia (IANA PEN 94) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.nokia</c>.
    /// </summary>
    /// <remarks>
    /// Nokia (formerly Alcatel-Lucent / Alcatel) uses vendor ID 94 for its IP/MPLS
    /// service routers (7750 SR, 7450 ESS, 7210 SAS, 7705 SAR) running SR OS
    /// (formerly TiMOS). These are separate from the Nokia/Alcatel-Lucent SROS
    /// attributes (PEN 6527).
    /// </remarks>
    public enum NokiaAttributeType : byte
    {
        /// <summary>Nokia-IMSI (Type 1). String. International Mobile Subscriber Identity.</summary>
        IMSI = 1,

        /// <summary>Nokia-MSISDN (Type 2). String. Mobile Station International Subscriber Directory Number.</summary>
        MSISDN = 2,

        /// <summary>Nokia-APN (Type 3). String. Access Point Name.</summary>
        APN = 3,

        /// <summary>Nokia-SGSN-IP-Address (Type 4). IP address. SGSN IP address.</summary>
        SGSN_IP_ADDRESS = 4,

        /// <summary>Nokia-GGSN-IP-Address (Type 5). IP address. GGSN IP address.</summary>
        GGSN_IP_ADDRESS = 5,

        /// <summary>Nokia-Charging-ID (Type 6). Integer. Charging identifier.</summary>
        CHARGING_ID = 6,

        /// <summary>Nokia-PDP-Type (Type 7). Integer. PDP context type.</summary>
        PDP_TYPE = 7,

        /// <summary>Nokia-Charging-Char (Type 8). String. Charging characteristics.</summary>
        CHARGING_CHAR = 8,

        /// <summary>Nokia-QoS-Profile (Type 9). String. QoS profile name.</summary>
        QOS_PROFILE = 9,

        /// <summary>Nokia-Service-Type (Type 10). Integer. Nokia service type.</summary>
        SERVICE_TYPE = 10,

        /// <summary>Nokia-NSAPI (Type 11). Integer. Network Service Access Point Identifier.</summary>
        NSAPI = 11,

        /// <summary>Nokia-Session-Stop-Indicator (Type 12). Integer. Session stop indicator.</summary>
        SESSION_STOP_INDICATOR = 12,

        /// <summary>Nokia-Selection-Mode (Type 13). Integer. APN selection mode.</summary>
        SELECTION_MODE = 13,

        /// <summary>Nokia-Operator-Name (Type 14). String. Operator name.</summary>
        OPERATOR_NAME = 14,

        /// <summary>Nokia-Charging-Gateway-Address (Type 15). IP address. Charging gateway address.</summary>
        CHARGING_GATEWAY_ADDRESS = 15,

        /// <summary>Nokia-SCP-Realm (Type 16). String. SCP realm.</summary>
        SCP_REALM = 16,

        /// <summary>Nokia-SCP-Address (Type 17). String. SCP address.</summary>
        SCP_ADDRESS = 17,

        /// <summary>Nokia-UE-Timezone (Type 18). String. UE timezone.</summary>
        UE_TIMEZONE = 18,

        /// <summary>Nokia-IMEISV (Type 19). String. International Mobile Equipment Identity and Software Version Number.</summary>
        IMEISV = 19,

        /// <summary>Nokia-RAT-Type (Type 20). Integer. Radio Access Technology type.</summary>
        RAT_TYPE = 20,

        /// <summary>Nokia-User-Location-Info (Type 21). Octets. User location information.</summary>
        USER_LOCATION_INFO = 21,

        /// <summary>Nokia-MS-Timezone (Type 22). Octets. Mobile station timezone.</summary>
        MS_TIMEZONE = 22,

        /// <summary>Nokia-Camel-Charging-Info (Type 24). Octets. CAMEL charging information.</summary>
        CAMEL_CHARGING_INFO = 24,

        /// <summary>Nokia-Serving-Node-Type (Type 25). Integer. Serving node type.</summary>
        SERVING_NODE_TYPE = 25,

        /// <summary>Nokia-SGW-Address (Type 26). IP address. Serving Gateway address.</summary>
        SGW_ADDRESS = 26,

        /// <summary>Nokia-PGW-Address (Type 27). IP address. PDN Gateway address.</summary>
        PGW_ADDRESS = 27,

        /// <summary>Nokia-Charging-Rule-Base-Name (Type 28). String. Charging rule base name.</summary>
        CHARGING_RULE_BASE_NAME = 28,

        /// <summary>Nokia-AVPair (Type 29). String. Attribute-value pair string.</summary>
        AVPAIR = 29
    }

    /// <summary>
    /// Nokia-PDP-Type attribute values (Type 7).
    /// </summary>
    public enum NOKIA_PDP_TYPE
    {
        /// <summary>IPv4 PDP context.</summary>
        IPV4 = 0,

        /// <summary>PPP PDP context.</summary>
        PPP = 1,

        /// <summary>IPv6 PDP context.</summary>
        IPV6 = 2,

        /// <summary>IPv4v6 PDP context.</summary>
        IPV4V6 = 3
    }

    /// <summary>
    /// Nokia-Selection-Mode attribute values (Type 13).
    /// </summary>
    public enum NOKIA_SELECTION_MODE
    {
        /// <summary>MS or network provided APN, subscribed verified.</summary>
        SUBSCRIBED = 0,

        /// <summary>MS provided APN, subscription not verified.</summary>
        MS_NOT_VERIFIED = 1,

        /// <summary>Network provided APN, subscription not verified.</summary>
        NETWORK_NOT_VERIFIED = 2
    }

    /// <summary>
    /// Nokia-RAT-Type attribute values (Type 20).
    /// </summary>
    public enum NOKIA_RAT_TYPE
    {
        /// <summary>UTRAN (3G).</summary>
        UTRAN = 1,

        /// <summary>GERAN (2G/GSM).</summary>
        GERAN = 2,

        /// <summary>WLAN.</summary>
        WLAN = 3,

        /// <summary>GAN (Generic Access Network).</summary>
        GAN = 4,

        /// <summary>HSPA Evolution.</summary>
        HSPA_EVOLUTION = 5,

        /// <summary>E-UTRAN (4G/LTE).</summary>
        EUTRAN = 6
    }

    /// <summary>
    /// Nokia-Serving-Node-Type attribute values (Type 25).
    /// </summary>
    public enum NOKIA_SERVING_NODE_TYPE
    {
        /// <summary>SGSN serving node.</summary>
        SGSN = 0,

        /// <summary>PMIP-SGW serving node.</summary>
        PMIP_SGW = 1,

        /// <summary>GTP-SGW serving node.</summary>
        GTP_SGW = 2,

        /// <summary>ePDG serving node.</summary>
        EPDG = 3,

        /// <summary>HSGW serving node.</summary>
        HSGW = 4,

        /// <summary>MME serving node.</summary>
        MME = 5
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Nokia
    /// (IANA PEN 94) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.nokia</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Nokia's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 94</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Nokia mobile core network elements (GGSN, PGW,
    /// SGSN, SGW, MME) for RADIUS-based 3GPP/LTE subscriber management including
    /// IMSI/MSISDN/IMEISV identification, APN configuration, PDP context type,
    /// charging (ID, characteristics, gateway address, rule base, CAMEL info),
    /// QoS profile assignment, SGSN/GGSN/SGW/PGW addressing, RAT type identification,
    /// user location and timezone tracking, APN selection mode, operator naming,
    /// SCP realm/address, session stop indication, serving node type, NSAPI, and
    /// general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(NokiaAttributes.Imsi("310260123456789"));
    /// packet.SetAttribute(NokiaAttributes.Apn("internet.example.com"));
    /// packet.SetAttribute(NokiaAttributes.PdpType(NOKIA_PDP_TYPE.IPV4));
    /// packet.SetAttribute(NokiaAttributes.QosProfile("gold"));
    /// packet.SetAttribute(NokiaAttributes.RatType(NOKIA_RAT_TYPE.EUTRAN));
    /// packet.SetAttribute(NokiaAttributes.PgwAddress(IPAddress.Parse("10.1.0.1")));
    /// </code>
    /// </remarks>
    public static class NokiaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Nokia.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 94;

        #region Integer Attributes

        /// <summary>Creates a Nokia-Charging-ID attribute (Type 6).</summary>
        /// <param name="value">The charging identifier.</param>
        public static VendorSpecificAttributes ChargingId(int value) => CreateInteger(NokiaAttributeType.CHARGING_ID, value);

        /// <summary>Creates a Nokia-PDP-Type attribute (Type 7).</summary>
        /// <param name="value">The PDP context type. See <see cref="NOKIA_PDP_TYPE"/>.</param>
        public static VendorSpecificAttributes PdpType(NOKIA_PDP_TYPE value) => CreateInteger(NokiaAttributeType.PDP_TYPE, (int)value);

        /// <summary>Creates a Nokia-Service-Type attribute (Type 10).</summary>
        /// <param name="value">The Nokia service type.</param>
        public static VendorSpecificAttributes ServiceType(int value) => CreateInteger(NokiaAttributeType.SERVICE_TYPE, value);

        /// <summary>Creates a Nokia-NSAPI attribute (Type 11).</summary>
        /// <param name="value">The Network Service Access Point Identifier.</param>
        public static VendorSpecificAttributes Nsapi(int value) => CreateInteger(NokiaAttributeType.NSAPI, value);

        /// <summary>Creates a Nokia-Session-Stop-Indicator attribute (Type 12).</summary>
        /// <param name="value">The session stop indicator.</param>
        public static VendorSpecificAttributes SessionStopIndicator(int value) => CreateInteger(NokiaAttributeType.SESSION_STOP_INDICATOR, value);

        /// <summary>Creates a Nokia-Selection-Mode attribute (Type 13).</summary>
        /// <param name="value">The APN selection mode. See <see cref="NOKIA_SELECTION_MODE"/>.</param>
        public static VendorSpecificAttributes SelectionMode(NOKIA_SELECTION_MODE value) => CreateInteger(NokiaAttributeType.SELECTION_MODE, (int)value);

        /// <summary>Creates a Nokia-RAT-Type attribute (Type 20).</summary>
        /// <param name="value">The Radio Access Technology type. See <see cref="NOKIA_RAT_TYPE"/>.</param>
        public static VendorSpecificAttributes RatType(NOKIA_RAT_TYPE value) => CreateInteger(NokiaAttributeType.RAT_TYPE, (int)value);

        /// <summary>Creates a Nokia-Serving-Node-Type attribute (Type 25).</summary>
        /// <param name="value">The serving node type. See <see cref="NOKIA_SERVING_NODE_TYPE"/>.</param>
        public static VendorSpecificAttributes ServingNodeType(NOKIA_SERVING_NODE_TYPE value) => CreateInteger(NokiaAttributeType.SERVING_NODE_TYPE, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Nokia-IMSI attribute (Type 1).</summary>
        /// <param name="value">The International Mobile Subscriber Identity. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imsi(string value) => CreateString(NokiaAttributeType.IMSI, value);

        /// <summary>Creates a Nokia-MSISDN attribute (Type 2).</summary>
        /// <param name="value">The MSISDN. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Msisdn(string value) => CreateString(NokiaAttributeType.MSISDN, value);

        /// <summary>Creates a Nokia-APN attribute (Type 3).</summary>
        /// <param name="value">The Access Point Name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Apn(string value) => CreateString(NokiaAttributeType.APN, value);

        /// <summary>Creates a Nokia-Charging-Char attribute (Type 8).</summary>
        /// <param name="value">The charging characteristics. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChargingChar(string value) => CreateString(NokiaAttributeType.CHARGING_CHAR, value);

        /// <summary>Creates a Nokia-QoS-Profile attribute (Type 9).</summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value) => CreateString(NokiaAttributeType.QOS_PROFILE, value);

        /// <summary>Creates a Nokia-Operator-Name attribute (Type 14).</summary>
        /// <param name="value">The operator name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OperatorName(string value) => CreateString(NokiaAttributeType.OPERATOR_NAME, value);

        /// <summary>Creates a Nokia-SCP-Realm attribute (Type 16).</summary>
        /// <param name="value">The SCP realm. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ScpRealm(string value) => CreateString(NokiaAttributeType.SCP_REALM, value);

        /// <summary>Creates a Nokia-SCP-Address attribute (Type 17).</summary>
        /// <param name="value">The SCP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ScpAddress(string value) => CreateString(NokiaAttributeType.SCP_ADDRESS, value);

        /// <summary>Creates a Nokia-UE-Timezone attribute (Type 18).</summary>
        /// <param name="value">The UE timezone. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UeTimezone(string value) => CreateString(NokiaAttributeType.UE_TIMEZONE, value);

        /// <summary>Creates a Nokia-IMEISV attribute (Type 19).</summary>
        /// <param name="value">The IMEISV. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imeisv(string value) => CreateString(NokiaAttributeType.IMEISV, value);

        /// <summary>Creates a Nokia-Charging-Rule-Base-Name attribute (Type 28).</summary>
        /// <param name="value">The charging rule base name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChargingRuleBaseName(string value) => CreateString(NokiaAttributeType.CHARGING_RULE_BASE_NAME, value);

        /// <summary>Creates a Nokia-AVPair attribute (Type 29).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(NokiaAttributeType.AVPAIR, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Nokia-SGSN-IP-Address attribute (Type 4).</summary>
        /// <param name="value">The SGSN IP address. Must be IPv4.</param>
        public static VendorSpecificAttributes SgsnIpAddress(IPAddress value) => CreateIpv4(NokiaAttributeType.SGSN_IP_ADDRESS, value);

        /// <summary>Creates a Nokia-GGSN-IP-Address attribute (Type 5).</summary>
        /// <param name="value">The GGSN IP address. Must be IPv4.</param>
        public static VendorSpecificAttributes GgsnIpAddress(IPAddress value) => CreateIpv4(NokiaAttributeType.GGSN_IP_ADDRESS, value);

        /// <summary>Creates a Nokia-Charging-Gateway-Address attribute (Type 15).</summary>
        /// <param name="value">The charging gateway address. Must be IPv4.</param>
        public static VendorSpecificAttributes ChargingGatewayAddress(IPAddress value) => CreateIpv4(NokiaAttributeType.CHARGING_GATEWAY_ADDRESS, value);

        /// <summary>Creates a Nokia-SGW-Address attribute (Type 26).</summary>
        /// <param name="value">The Serving Gateway address. Must be IPv4.</param>
        public static VendorSpecificAttributes SgwAddress(IPAddress value) => CreateIpv4(NokiaAttributeType.SGW_ADDRESS, value);

        /// <summary>Creates a Nokia-PGW-Address attribute (Type 27).</summary>
        /// <param name="value">The PDN Gateway address. Must be IPv4.</param>
        public static VendorSpecificAttributes PgwAddress(IPAddress value) => CreateIpv4(NokiaAttributeType.PGW_ADDRESS, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates a Nokia-User-Location-Info attribute (Type 21).</summary>
        /// <param name="value">The user location information data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserLocationInfo(byte[] value) => CreateOctets(NokiaAttributeType.USER_LOCATION_INFO, value);

        /// <summary>Creates a Nokia-MS-Timezone attribute (Type 22).</summary>
        /// <param name="value">The mobile station timezone data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsTimezone(byte[] value) => CreateOctets(NokiaAttributeType.MS_TIMEZONE, value);

        /// <summary>Creates a Nokia-Camel-Charging-Info attribute (Type 24).</summary>
        /// <param name="value">The CAMEL charging information data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CamelChargingInfo(byte[] value) => CreateOctets(NokiaAttributeType.CAMEL_CHARGING_INFO, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(NokiaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(NokiaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(NokiaAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        private static VendorSpecificAttributes CreateIpv4(NokiaAttributeType type, IPAddress value)
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