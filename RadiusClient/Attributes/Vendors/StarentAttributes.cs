using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Starent Networks (IANA PEN 8164) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.starent</c>.
    /// </summary>
    /// <remarks>
    /// Starent Networks (acquired by Cisco Systems in 2009) produced mobile packet
    /// core platforms (ST series, later Cisco ASR 5000/5500) for 3G/4G/LTE mobile
    /// core networks including GGSN, PDSN, HA, SGSN, PGW, SGW, MME, and ePDG
    /// functions. These attributes are used extensively in mobile carrier
    /// RADIUS accounting and policy enforcement.
    /// </remarks>
    public enum StarentAttributeType : byte
    {
        /// <summary>SN-VPN-Name (Type 1). String. VPN context name.</summary>
        VPN_NAME = 1,

        /// <summary>SN-VPN-Id (Type 2). Integer. VPN context identifier.</summary>
        VPN_ID = 2,

        /// <summary>SN-Subscriber-Name (Type 3). String. Subscriber name.</summary>
        SUBSCRIBER_NAME = 3,

        /// <summary>SN-Context-Name (Type 4). String. Context name.</summary>
        CONTEXT_NAME = 4,

        /// <summary>SN-Serving-Node (Type 5). String. Serving node name.</summary>
        SERVING_NODE = 5,

        /// <summary>SN-APN (Type 6). String. Access Point Name.</summary>
        APN = 6,

        /// <summary>SN-Bearer-Type (Type 7). Integer. Bearer type.</summary>
        BEARER_TYPE = 7,

        /// <summary>SN-IMSI (Type 8). String. International Mobile Subscriber Identity.</summary>
        IMSI = 8,

        /// <summary>SN-MSISDN (Type 9). String. Mobile Station ISDN Number.</summary>
        MSISDN = 9,

        /// <summary>SN-Session-Type (Type 10). Integer. Session type.</summary>
        SESSION_TYPE = 10,

        /// <summary>SN-IP-Addr-Pool (Type 11). String. IP address pool name.</summary>
        IP_ADDR_POOL = 11,

        /// <summary>SN-PPP-Progress-Code (Type 12). Integer. PPP progress code.</summary>
        PPP_PROGRESS_CODE = 12,

        /// <summary>SN-Subscriber-CSA (Type 13). String. Subscriber charging service agreement.</summary>
        SUBSCRIBER_CSA = 13,

        /// <summary>SN-SGSN-Address (Type 14). IP address. SGSN IP address.</summary>
        SGSN_ADDRESS = 14,

        /// <summary>SN-QoS-Profile (Type 15). String. QoS profile name.</summary>
        QOS_PROFILE = 15,

        /// <summary>SN-Subscriber-Acct-Policy (Type 16). String. Subscriber accounting policy.</summary>
        SUBSCRIBER_ACCT_POLICY = 16,

        /// <summary>SN-Charging-Id (Type 17). Integer. Charging identifier.</summary>
        CHARGING_ID = 17,

        /// <summary>SN-NSAPI (Type 18). Integer. Network Service Access Point Identifier.</summary>
        NSAPI = 18,

        /// <summary>SN-Selection-Mode (Type 19). Integer. APN selection mode.</summary>
        SELECTION_MODE = 19,

        /// <summary>SN-PDP-Type (Type 20). Integer. PDP context type.</summary>
        PDP_TYPE = 20,

        /// <summary>SN-Charging-Chars (Type 21). Integer. Charging characteristics.</summary>
        CHARGING_CHARS = 21,

        /// <summary>SN-SGSN-MCC-MNC (Type 22). String. SGSN MCC-MNC (PLMN ID).</summary>
        SGSN_MCC_MNC = 22,

        /// <summary>SN-Subscriber-AMBR (Type 23). String. Subscriber Aggregate Maximum Bit Rate.</summary>
        SUBSCRIBER_AMBR = 23,

        /// <summary>SN-RAT-Type (Type 24). Integer. Radio Access Technology type.</summary>
        RAT_TYPE = 24,

        /// <summary>SN-UE-Timezone (Type 25). String. UE timezone.</summary>
        UE_TIMEZONE = 25,

        /// <summary>SN-IMEISV (Type 26). String. IMEI Software Version.</summary>
        IMEISV = 26,

        /// <summary>SN-User-Location-Info (Type 27). Octets. User location information.</summary>
        USER_LOCATION_INFO = 27,

        /// <summary>SN-Rulebase (Type 28). String. Rulebase name.</summary>
        RULEBASE = 28,

        /// <summary>SN-Charging-Rule-Base-Name (Type 29). String. Charging rule base name.</summary>
        CHARGING_RULE_BASE_NAME = 29,

        /// <summary>SN-AVPair (Type 30). String. Attribute-value pair string.</summary>
        AVPAIR = 30,

        /// <summary>SN-GGSN-Address (Type 31). IP address. GGSN IP address.</summary>
        GGSN_ADDRESS = 31,

        /// <summary>SN-SGW-Address (Type 32). IP address. SGW IP address.</summary>
        SGW_ADDRESS = 32,

        /// <summary>SN-PGW-Address (Type 33). IP address. PGW IP address.</summary>
        PGW_ADDRESS = 33,

        /// <summary>SN-Serving-Node-Type (Type 34). Integer. Serving node type.</summary>
        SERVING_NODE_TYPE = 34
    }

    /// <summary>
    /// SN-Bearer-Type attribute values (Type 7).
    /// </summary>
    public enum SN_BEARER_TYPE
    {
        /// <summary>Default bearer.</summary>
        DEFAULT = 0,

        /// <summary>Dedicated bearer.</summary>
        DEDICATED = 1
    }

    /// <summary>
    /// SN-PDP-Type attribute values (Type 20).
    /// </summary>
    public enum SN_PDP_TYPE
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
    /// SN-RAT-Type attribute values (Type 24).
    /// </summary>
    public enum SN_RAT_TYPE
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
    /// SN-Serving-Node-Type attribute values (Type 34).
    /// </summary>
    public enum SN_SERVING_NODE_TYPE
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

    /// <summary>
    /// SN-Selection-Mode attribute values (Type 19).
    /// </summary>
    public enum SN_SELECTION_MODE
    {
        /// <summary>MS or network provided APN, subscribed verified.</summary>
        SUBSCRIBED = 0,

        /// <summary>MS provided APN, subscription not verified.</summary>
        MS_NOT_VERIFIED = 1,

        /// <summary>Network provided APN, subscription not verified.</summary>
        NETWORK_NOT_VERIFIED = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Starent Networks
    /// (IANA PEN 8164) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.starent</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Starent's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 8164</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Starent Networks (Cisco ASR 5000/5500) mobile
    /// packet core platforms for RADIUS-based 3GPP/LTE subscriber management including
    /// VPN context identification, subscriber naming, APN configuration, IMSI/MSISDN/IMEISV
    /// identification, PDP context type, bearer type, charging (ID, characteristics,
    /// rule base), QoS profile, SGSN/GGSN/SGW/PGW addressing, RAT type, user location
    /// and timezone tracking, APN selection mode, SGSN PLMN ID, subscriber AMBR,
    /// serving node type, IP address pool assignment, PPP progress code, accounting
    /// policy, rulebase, and general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(StarentAttributes.Imsi("310260123456789"));
    /// packet.SetAttribute(StarentAttributes.Apn("internet.example.com"));
    /// packet.SetAttribute(StarentAttributes.PdpType(SN_PDP_TYPE.IPV4));
    /// packet.SetAttribute(StarentAttributes.RatType(SN_RAT_TYPE.EUTRAN));
    /// packet.SetAttribute(StarentAttributes.Rulebase("default"));
    /// packet.SetAttribute(StarentAttributes.PgwAddress(IPAddress.Parse("10.1.0.1")));
    /// </code>
    /// </remarks>
    public static class StarentAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Starent Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 8164;

        #region Integer Attributes

        /// <summary>Creates an SN-VPN-Id attribute (Type 2).</summary>
        /// <param name="value">The VPN context identifier.</param>
        public static VendorSpecificAttributes VpnId(int value) => CreateInteger(StarentAttributeType.VPN_ID, value);

        /// <summary>Creates an SN-Bearer-Type attribute (Type 7).</summary>
        /// <param name="value">The bearer type. See <see cref="SN_BEARER_TYPE"/>.</param>
        public static VendorSpecificAttributes BearerType(SN_BEARER_TYPE value) => CreateInteger(StarentAttributeType.BEARER_TYPE, (int)value);

        /// <summary>Creates an SN-Session-Type attribute (Type 10).</summary>
        /// <param name="value">The session type.</param>
        public static VendorSpecificAttributes SessionType(int value) => CreateInteger(StarentAttributeType.SESSION_TYPE, value);

        /// <summary>Creates an SN-PPP-Progress-Code attribute (Type 12).</summary>
        /// <param name="value">The PPP progress code.</param>
        public static VendorSpecificAttributes PppProgressCode(int value) => CreateInteger(StarentAttributeType.PPP_PROGRESS_CODE, value);

        /// <summary>Creates an SN-Charging-Id attribute (Type 17).</summary>
        /// <param name="value">The charging identifier.</param>
        public static VendorSpecificAttributes ChargingId(int value) => CreateInteger(StarentAttributeType.CHARGING_ID, value);

        /// <summary>Creates an SN-NSAPI attribute (Type 18).</summary>
        /// <param name="value">The Network Service Access Point Identifier.</param>
        public static VendorSpecificAttributes Nsapi(int value) => CreateInteger(StarentAttributeType.NSAPI, value);

        /// <summary>Creates an SN-Selection-Mode attribute (Type 19).</summary>
        /// <param name="value">The APN selection mode. See <see cref="SN_SELECTION_MODE"/>.</param>
        public static VendorSpecificAttributes SelectionMode(SN_SELECTION_MODE value) => CreateInteger(StarentAttributeType.SELECTION_MODE, (int)value);

        /// <summary>Creates an SN-PDP-Type attribute (Type 20).</summary>
        /// <param name="value">The PDP context type. See <see cref="SN_PDP_TYPE"/>.</param>
        public static VendorSpecificAttributes PdpType(SN_PDP_TYPE value) => CreateInteger(StarentAttributeType.PDP_TYPE, (int)value);

        /// <summary>Creates an SN-Charging-Chars attribute (Type 21).</summary>
        /// <param name="value">The charging characteristics.</param>
        public static VendorSpecificAttributes ChargingChars(int value) => CreateInteger(StarentAttributeType.CHARGING_CHARS, value);

        /// <summary>Creates an SN-RAT-Type attribute (Type 24).</summary>
        /// <param name="value">The Radio Access Technology type. See <see cref="SN_RAT_TYPE"/>.</param>
        public static VendorSpecificAttributes RatType(SN_RAT_TYPE value) => CreateInteger(StarentAttributeType.RAT_TYPE, (int)value);

        /// <summary>Creates an SN-Serving-Node-Type attribute (Type 34).</summary>
        /// <param name="value">The serving node type. See <see cref="SN_SERVING_NODE_TYPE"/>.</param>
        public static VendorSpecificAttributes ServingNodeType(SN_SERVING_NODE_TYPE value) => CreateInteger(StarentAttributeType.SERVING_NODE_TYPE, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates an SN-VPN-Name attribute (Type 1).</summary>
        /// <param name="value">The VPN context name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnName(string value) => CreateString(StarentAttributeType.VPN_NAME, value);

        /// <summary>Creates an SN-Subscriber-Name attribute (Type 3).</summary>
        /// <param name="value">The subscriber name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberName(string value) => CreateString(StarentAttributeType.SUBSCRIBER_NAME, value);

        /// <summary>Creates an SN-Context-Name attribute (Type 4).</summary>
        /// <param name="value">The context name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ContextName(string value) => CreateString(StarentAttributeType.CONTEXT_NAME, value);

        /// <summary>Creates an SN-Serving-Node attribute (Type 5).</summary>
        /// <param name="value">The serving node name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServingNode(string value) => CreateString(StarentAttributeType.SERVING_NODE, value);

        /// <summary>Creates an SN-APN attribute (Type 6).</summary>
        /// <param name="value">The Access Point Name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Apn(string value) => CreateString(StarentAttributeType.APN, value);

        /// <summary>Creates an SN-IMSI attribute (Type 8).</summary>
        /// <param name="value">The IMSI. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imsi(string value) => CreateString(StarentAttributeType.IMSI, value);

        /// <summary>Creates an SN-MSISDN attribute (Type 9).</summary>
        /// <param name="value">The MSISDN. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Msisdn(string value) => CreateString(StarentAttributeType.MSISDN, value);

        /// <summary>Creates an SN-IP-Addr-Pool attribute (Type 11).</summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpAddrPool(string value) => CreateString(StarentAttributeType.IP_ADDR_POOL, value);

        /// <summary>Creates an SN-Subscriber-CSA attribute (Type 13).</summary>
        /// <param name="value">The subscriber charging service agreement. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberCsa(string value) => CreateString(StarentAttributeType.SUBSCRIBER_CSA, value);

        /// <summary>Creates an SN-QoS-Profile attribute (Type 15).</summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value) => CreateString(StarentAttributeType.QOS_PROFILE, value);

        /// <summary>Creates an SN-Subscriber-Acct-Policy attribute (Type 16).</summary>
        /// <param name="value">The subscriber accounting policy. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberAcctPolicy(string value) => CreateString(StarentAttributeType.SUBSCRIBER_ACCT_POLICY, value);

        /// <summary>Creates an SN-SGSN-MCC-MNC attribute (Type 22).</summary>
        /// <param name="value">The SGSN MCC-MNC (PLMN ID). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SgsnMccMnc(string value) => CreateString(StarentAttributeType.SGSN_MCC_MNC, value);

        /// <summary>Creates an SN-Subscriber-AMBR attribute (Type 23).</summary>
        /// <param name="value">The subscriber Aggregate Maximum Bit Rate. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberAmbr(string value) => CreateString(StarentAttributeType.SUBSCRIBER_AMBR, value);

        /// <summary>Creates an SN-UE-Timezone attribute (Type 25).</summary>
        /// <param name="value">The UE timezone. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UeTimezone(string value) => CreateString(StarentAttributeType.UE_TIMEZONE, value);

        /// <summary>Creates an SN-IMEISV attribute (Type 26).</summary>
        /// <param name="value">The IMEI Software Version. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imeisv(string value) => CreateString(StarentAttributeType.IMEISV, value);

        /// <summary>Creates an SN-Rulebase attribute (Type 28).</summary>
        /// <param name="value">The rulebase name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Rulebase(string value) => CreateString(StarentAttributeType.RULEBASE, value);

        /// <summary>Creates an SN-Charging-Rule-Base-Name attribute (Type 29).</summary>
        /// <param name="value">The charging rule base name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChargingRuleBaseName(string value) => CreateString(StarentAttributeType.CHARGING_RULE_BASE_NAME, value);

        /// <summary>Creates an SN-AVPair attribute (Type 30).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(StarentAttributeType.AVPAIR, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates an SN-SGSN-Address attribute (Type 14).</summary>
        /// <param name="value">The SGSN IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SgsnAddress(IPAddress value) => CreateIpv4(StarentAttributeType.SGSN_ADDRESS, value);

        /// <summary>Creates an SN-GGSN-Address attribute (Type 31).</summary>
        /// <param name="value">The GGSN IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes GgsnAddress(IPAddress value) => CreateIpv4(StarentAttributeType.GGSN_ADDRESS, value);

        /// <summary>Creates an SN-SGW-Address attribute (Type 32).</summary>
        /// <param name="value">The SGW IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SgwAddress(IPAddress value) => CreateIpv4(StarentAttributeType.SGW_ADDRESS, value);

        /// <summary>Creates an SN-PGW-Address attribute (Type 33).</summary>
        /// <param name="value">The PGW IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PgwAddress(IPAddress value) => CreateIpv4(StarentAttributeType.PGW_ADDRESS, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates an SN-User-Location-Info attribute (Type 27).</summary>
        /// <param name="value">The user location information data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserLocationInfo(byte[] value) => CreateOctets(StarentAttributeType.USER_LOCATION_INFO, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(StarentAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(StarentAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(StarentAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        private static VendorSpecificAttributes CreateIpv4(StarentAttributeType type, IPAddress value)
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