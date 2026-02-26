using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a 3GPP (IANA PEN 10415) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.3gpp</c> and 3GPP TS 29.061.
    /// </summary>
    public enum ThreeGppAttributeType : byte
    {
        /// <summary>3GPP-IMSI (Type 1). String. International Mobile Subscriber Identity.</summary>
        IMSI = 1,

        /// <summary>3GPP-Charging-Id (Type 2). Integer. Charging identifier for the PDP context.</summary>
        CHARGING_ID = 2,

        /// <summary>3GPP-PDP-Type (Type 3). Integer. PDP context type (IPv4, IPv6, etc.).</summary>
        PDP_TYPE = 3,

        /// <summary>3GPP-Charging-Gateway-Address (Type 4). IP address. Charging gateway IPv4 address.</summary>
        CHARGING_GATEWAY_ADDRESS = 4,

        /// <summary>3GPP-GPRS-Negotiated-QoS-Profile (Type 5). String. Negotiated QoS profile for the PDP context.</summary>
        GPRS_NEGOTIATED_QOS_PROFILE = 5,

        /// <summary>3GPP-SGSN-Address (Type 6). IP address. SGSN IPv4 address.</summary>
        SGSN_ADDRESS = 6,

        /// <summary>3GPP-GGSN-Address (Type 7). IP address. GGSN IPv4 address.</summary>
        GGSN_ADDRESS = 7,

        /// <summary>3GPP-IMSI-MCC-MNC (Type 8). String. MCC and MNC extracted from the IMSI.</summary>
        IMSI_MCC_MNC = 8,

        /// <summary>3GPP-GGSN-MCC-MNC (Type 9). String. MCC and MNC of the GGSN.</summary>
        GGSN_MCC_MNC = 9,

        /// <summary>3GPP-NSAPI (Type 10). String. Network Service Access Point Identifier.</summary>
        NSAPI = 10,

        /// <summary>3GPP-Session-Stop-Indicator (Type 11). Octets. Indicates session termination.</summary>
        SESSION_STOP_INDICATOR = 11,

        /// <summary>3GPP-Selection-Mode (Type 12). String. APN selection mode.</summary>
        SELECTION_MODE = 12,

        /// <summary>3GPP-Charging-Characteristics (Type 13). String. Charging characteristics for the PDP context.</summary>
        CHARGING_CHARACTERISTICS = 13,

        /// <summary>3GPP-Charging-Gateway-IPv6-Address (Type 14). IPv6 address. Charging gateway IPv6 address.</summary>
        CHARGING_GATEWAY_IPV6_ADDRESS = 14,

        /// <summary>3GPP-SGSN-IPv6-Address (Type 15). IPv6 address. SGSN IPv6 address.</summary>
        SGSN_IPV6_ADDRESS = 15,

        /// <summary>3GPP-GGSN-IPv6-Address (Type 16). IPv6 address. GGSN IPv6 address.</summary>
        GGSN_IPV6_ADDRESS = 16,

        /// <summary>3GPP-IPv6-DNS-Servers (Type 17). IPv6 address. IPv6 DNS server addresses.</summary>
        IPV6_DNS_SERVERS = 17,

        /// <summary>3GPP-SGSN-MCC-MNC (Type 18). String. MCC and MNC of the SGSN.</summary>
        SGSN_MCC_MNC = 18,

        /// <summary>3GPP-Teardown-Indicator (Type 19). Octets. Indicates PDP context teardown.</summary>
        TEARDOWN_INDICATOR = 19,

        /// <summary>3GPP-IMEISV (Type 20). String. International Mobile Equipment Identity with Software Version.</summary>
        IMEISV = 20,

        /// <summary>3GPP-RAT-Type (Type 21). Octets. Radio Access Technology type.</summary>
        RAT_TYPE = 21,

        /// <summary>3GPP-User-Location-Info (Type 22). Octets. User location information (CGI, SAI, RAI, TAI, ECGI).</summary>
        USER_LOCATION_INFO = 22,

        /// <summary>3GPP-MS-TimeZone (Type 23). Octets. Mobile station timezone offset.</summary>
        MS_TIMEZONE = 23,

        /// <summary>3GPP-CAMEL-Charging-Info (Type 24). Octets. CAMEL charging information.</summary>
        CAMEL_CHARGING_INFO = 24,

        /// <summary>3GPP-Packet-Filter (Type 25). Octets. Packet filter definition.</summary>
        PACKET_FILTER = 25,

        /// <summary>3GPP-Negotiated-DSCP (Type 26). Octets. Negotiated DiffServ Code Point.</summary>
        NEGOTIATED_DSCP = 26,

        /// <summary>3GPP-Allocate-IP-Type (Type 27). Octets. Requested IP allocation type.</summary>
        ALLOCATE_IP_TYPE = 27,

        /// <summary>External-Identifier (Type 28). String. External subscription identifier.</summary>
        EXTERNAL_IDENTIFIER = 28,

        /// <summary>3GPP-TWAN-Identifier (Type 29). Octets. Trusted WLAN Access Network identifier.</summary>
        TWAN_IDENTIFIER = 29,

        /// <summary>3GPP-User-Location-Info-Time (Type 30). Octets. Timestamp of the user location information.</summary>
        USER_LOCATION_INFO_TIME = 30,

        /// <summary>3GPP-Secondary-RAT-Usage-Report (Type 31). Octets. Secondary RAT usage report data.</summary>
        SECONDARY_RAT_USAGE_REPORT = 31
    }

    /// <summary>
    /// 3GPP-PDP-Type attribute values (Type 3), as defined in 3GPP TS 29.061.
    /// </summary>
    public enum THREEGPP_PDP_TYPE
    {
        /// <summary>IPv4 PDP type.</summary>
        IPV4 = 0,

        /// <summary>PPP PDP type.</summary>
        PPP = 1,

        /// <summary>IPv6 PDP type.</summary>
        IPV6 = 2,

        /// <summary>IPv4v6 dual-stack PDP type.</summary>
        IPV4V6 = 3,

        /// <summary>Non-IP PDP type.</summary>
        NON_IP = 4
    }

    /// <summary>
    /// 3GPP-RAT-Type attribute values (Type 21), as defined in 3GPP TS 29.061 §16.4.7.2.
    /// </summary>
    public enum THREEGPP_RAT_TYPE
    {
        /// <summary>Reserved value.</summary>
        RESERVED = 0,

        /// <summary>UTRAN (UMTS Terrestrial Radio Access Network).</summary>
        UTRAN = 1,

        /// <summary>GERAN (GSM/EDGE Radio Access Network).</summary>
        GERAN = 2,

        /// <summary>WLAN (Wireless Local Area Network).</summary>
        WLAN = 3,

        /// <summary>GAN (Generic Access Network).</summary>
        GAN = 4,

        /// <summary>HSPA Evolution.</summary>
        HSPA_EVOLUTION = 5,

        /// <summary>E-UTRAN (Evolved UTRAN / LTE).</summary>
        EUTRAN = 6,

        /// <summary>Virtual access network.</summary>
        VIRTUAL = 7,

        /// <summary>E-UTRAN with NB-IoT (Narrowband IoT).</summary>
        EUTRAN_NB_IOT = 8,

        /// <summary>LTE-M (Long Term Evolution for Machines).</summary>
        LTE_M = 9,

        /// <summary>NR (5G New Radio).</summary>
        NR = 10,

        /// <summary>NR in unlicensed spectrum (NR-U).</summary>
        NR_UNLICENSED = 11,

        /// <summary>Trusted WLAN access.</summary>
        TRUSTED_WLAN = 12,

        /// <summary>Trusted non-3GPP access.</summary>
        TRUSTED_N3GA = 13,

        /// <summary>Wireline access.</summary>
        WIRELINE = 14,

        /// <summary>Wireline access via cable.</summary>
        WIRELINE_CABLE = 15,

        /// <summary>Wireline access via BBF (Broadband Forum).</summary>
        WIRELINE_BBF = 16,

        /// <summary>LTE-M with NB-IoT.</summary>
        LTE_M_NB_IOT = 17,

        /// <summary>NR RedCap (Reduced Capability).</summary>
        NR_REDCAP = 18,

        /// <summary>NR LEO satellite access.</summary>
        NR_LEO = 19,

        /// <summary>NR MEO satellite access.</summary>
        NR_MEO = 20,

        /// <summary>NR GEO satellite access.</summary>
        NR_GEO = 21,

        /// <summary>NR other satellite access.</summary>
        NR_OTHER_SAT = 22,

        /// <summary>E-UTRAN LEO satellite access.</summary>
        EUTRAN_LEO = 23,

        /// <summary>E-UTRAN MEO satellite access.</summary>
        EUTRAN_MEO = 24,

        /// <summary>E-UTRAN GEO satellite access.</summary>
        EUTRAN_GEO = 25,

        /// <summary>E-UTRAN other satellite access.</summary>
        EUTRAN_OTHER_SAT = 26,

        /// <summary>LTE-M LEO satellite access.</summary>
        LTE_M_LEO = 27,

        /// <summary>LTE-M MEO satellite access.</summary>
        LTE_M_MEO = 28,

        /// <summary>LTE-M GEO satellite access.</summary>
        LTE_M_GEO = 29,

        /// <summary>LTE-M other satellite access.</summary>
        LTE_M_OTHER_SAT = 30
    }

    /// <summary>
    /// 3GPP-Allocate-IP-Type attribute values (Type 27), as defined in 3GPP TS 29.061.
    /// </summary>
    public enum THREEGPP_ALLOCATE_IP_TYPE
    {
        /// <summary>Do not allocate an IP address.</summary>
        DO_NOT_ALLOCATE = 0,

        /// <summary>Allocate an IPv4 address.</summary>
        ALLOCATE_IPV4 = 1,

        /// <summary>Allocate an IPv6 prefix.</summary>
        ALLOCATE_IPV6 = 2,

        /// <summary>Allocate both IPv4 and IPv6.</summary>
        ALLOCATE_IPV4V6 = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing 3GPP (IANA PEN 10415)
    /// Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.3gpp</c> and 3GPP TS 29.061.
    /// </summary>
    /// <remarks>
    /// <para>
    /// 3GPP's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 10415</c>.
    /// </para>
    /// <para>
    /// These attributes are widely used in mobile/telecom RADIUS deployments for
    /// GPRS, UMTS, LTE, and 5G PDP context and session management.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(ThreeGppAttributes.Imsi("310260000000001"));
    /// packet.SetAttribute(ThreeGppAttributes.ChargingId(12345));
    /// packet.SetAttribute(ThreeGppAttributes.SgsnAddress(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(ThreeGppAttributes.RatType(THREEGPP_RAT_TYPE.EUTRAN));
    /// </code>
    /// </remarks>
    public static class ThreeGppAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for 3GPP (3rd Generation Partnership Project).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 10415;

        #region String Attributes

        /// <summary>
        /// Creates a 3GPP-IMSI attribute (Type 1) with the specified IMSI value.
        /// </summary>
        /// <param name="value">The International Mobile Subscriber Identity. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imsi(string value)
        {
            return CreateString(ThreeGppAttributeType.IMSI, value);
        }

        /// <summary>
        /// Creates a 3GPP-GPRS-Negotiated-QoS-Profile attribute (Type 5) with the specified QoS profile.
        /// </summary>
        /// <param name="value">The negotiated QoS profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GprsNegotiatedQosProfile(string value)
        {
            return CreateString(ThreeGppAttributeType.GPRS_NEGOTIATED_QOS_PROFILE, value);
        }

        /// <summary>
        /// Creates a 3GPP-IMSI-MCC-MNC attribute (Type 8) with the specified MCC/MNC value.
        /// </summary>
        /// <param name="value">The MCC and MNC extracted from the IMSI (e.g. "310260"). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ImsiMccMnc(string value)
        {
            return CreateString(ThreeGppAttributeType.IMSI_MCC_MNC, value);
        }

        /// <summary>
        /// Creates a 3GPP-GGSN-MCC-MNC attribute (Type 9) with the specified MCC/MNC value.
        /// </summary>
        /// <param name="value">The MCC and MNC of the GGSN. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GgsnMccMnc(string value)
        {
            return CreateString(ThreeGppAttributeType.GGSN_MCC_MNC, value);
        }

        /// <summary>
        /// Creates a 3GPP-NSAPI attribute (Type 10) with the specified NSAPI value.
        /// </summary>
        /// <param name="value">The Network Service Access Point Identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Nsapi(string value)
        {
            return CreateString(ThreeGppAttributeType.NSAPI, value);
        }

        /// <summary>
        /// Creates a 3GPP-Selection-Mode attribute (Type 12) with the specified selection mode.
        /// </summary>
        /// <param name="value">The APN selection mode string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SelectionMode(string value)
        {
            return CreateString(ThreeGppAttributeType.SELECTION_MODE, value);
        }

        /// <summary>
        /// Creates a 3GPP-Charging-Characteristics attribute (Type 13) with the specified charging characteristics.
        /// </summary>
        /// <param name="value">The charging characteristics string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChargingCharacteristics(string value)
        {
            return CreateString(ThreeGppAttributeType.CHARGING_CHARACTERISTICS, value);
        }

        /// <summary>
        /// Creates a 3GPP-SGSN-MCC-MNC attribute (Type 18) with the specified MCC/MNC value.
        /// </summary>
        /// <param name="value">The MCC and MNC of the SGSN. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SgsnMccMnc(string value)
        {
            return CreateString(ThreeGppAttributeType.SGSN_MCC_MNC, value);
        }

        /// <summary>
        /// Creates a 3GPP-IMEISV attribute (Type 20) with the specified IMEISV value.
        /// </summary>
        /// <param name="value">The International Mobile Equipment Identity with Software Version. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imeisv(string value)
        {
            return CreateString(ThreeGppAttributeType.IMEISV, value);
        }

        /// <summary>
        /// Creates an External-Identifier attribute (Type 28) with the specified external identifier.
        /// </summary>
        /// <param name="value">The external subscription identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ExternalIdentifier(string value)
        {
            return CreateString(ThreeGppAttributeType.EXTERNAL_IDENTIFIER, value);
        }

        #endregion

        #region Integer Attributes

        /// <summary>
        /// Creates a 3GPP-Charging-Id attribute (Type 2) with the specified charging identifier.
        /// </summary>
        /// <param name="value">The charging identifier for the PDP context.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ChargingId(int value)
        {
            return CreateInteger(ThreeGppAttributeType.CHARGING_ID, value);
        }

        /// <summary>
        /// Creates a 3GPP-PDP-Type attribute (Type 3) with the specified PDP type.
        /// </summary>
        /// <param name="value">The PDP context type. See <see cref="THREEGPP_PDP_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PdpType(THREEGPP_PDP_TYPE value)
        {
            return CreateInteger(ThreeGppAttributeType.PDP_TYPE, (int)value);
        }

        #endregion

        #region IPv4 Address Attributes

        /// <summary>
        /// Creates a 3GPP-Charging-Gateway-Address attribute (Type 4) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The charging gateway IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ChargingGatewayAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGppAttributeType.CHARGING_GATEWAY_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP-SGSN-Address attribute (Type 6) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The SGSN IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SgsnAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGppAttributeType.SGSN_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP-GGSN-Address attribute (Type 7) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The GGSN IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes GgsnAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGppAttributeType.GGSN_ADDRESS, value);
        }

        #endregion

        #region IPv6 Address Attributes

        /// <summary>
        /// Creates a 3GPP-Charging-Gateway-IPv6-Address attribute (Type 14) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">
        /// The charging gateway IPv6 address. Must be an IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv6 address.</exception>
        public static VendorSpecificAttributes ChargingGatewayIpv6Address(IPAddress value)
        {
            return CreateIpv6(ThreeGppAttributeType.CHARGING_GATEWAY_IPV6_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP-SGSN-IPv6-Address attribute (Type 15) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">
        /// The SGSN IPv6 address. Must be an IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv6 address.</exception>
        public static VendorSpecificAttributes SgsnIpv6Address(IPAddress value)
        {
            return CreateIpv6(ThreeGppAttributeType.SGSN_IPV6_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP-GGSN-IPv6-Address attribute (Type 16) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">
        /// The GGSN IPv6 address. Must be an IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv6 address.</exception>
        public static VendorSpecificAttributes GgsnIpv6Address(IPAddress value)
        {
            return CreateIpv6(ThreeGppAttributeType.GGSN_IPV6_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP-IPv6-DNS-Servers attribute (Type 17) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">
        /// The IPv6 DNS server address. Must be an IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv6 address.</exception>
        public static VendorSpecificAttributes Ipv6DnsServers(IPAddress value)
        {
            return CreateIpv6(ThreeGppAttributeType.IPV6_DNS_SERVERS, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates a 3GPP-Session-Stop-Indicator attribute (Type 11) with the specified raw value.
        /// </summary>
        /// <param name="value">The raw octets value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionStopIndicator(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.SESSION_STOP_INDICATOR, value);
        }

        /// <summary>
        /// Creates a 3GPP-Teardown-Indicator attribute (Type 19) with the specified raw value.
        /// </summary>
        /// <param name="value">The raw octets value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TeardownIndicator(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.TEARDOWN_INDICATOR, value);
        }

        /// <summary>
        /// Creates a 3GPP-RAT-Type attribute (Type 21) with the specified RAT type.
        /// </summary>
        /// <param name="value">The Radio Access Technology type. See <see cref="THREEGPP_RAT_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RatType(THREEGPP_RAT_TYPE value)
        {
            return CreateOctets(ThreeGppAttributeType.RAT_TYPE, [(byte)value]);
        }

        /// <summary>
        /// Creates a 3GPP-RAT-Type attribute (Type 21) with the specified raw octets value.
        /// </summary>
        /// <param name="value">The raw octets value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RatType(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.RAT_TYPE, value);
        }

        /// <summary>
        /// Creates a 3GPP-User-Location-Info attribute (Type 22) with the specified raw value.
        /// </summary>
        /// <param name="value">The user location information octets (CGI, SAI, RAI, TAI, ECGI). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserLocationInfo(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.USER_LOCATION_INFO, value);
        }

        /// <summary>
        /// Creates a 3GPP-MS-TimeZone attribute (Type 23) with the specified raw value.
        /// </summary>
        /// <param name="value">The mobile station timezone octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsTimezone(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.MS_TIMEZONE, value);
        }

        /// <summary>
        /// Creates a 3GPP-CAMEL-Charging-Info attribute (Type 24) with the specified raw value.
        /// </summary>
        /// <param name="value">The CAMEL charging information octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CamelChargingInfo(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.CAMEL_CHARGING_INFO, value);
        }

        /// <summary>
        /// Creates a 3GPP-Packet-Filter attribute (Type 25) with the specified raw value.
        /// </summary>
        /// <param name="value">The packet filter definition octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PacketFilter(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.PACKET_FILTER, value);
        }

        /// <summary>
        /// Creates a 3GPP-Negotiated-DSCP attribute (Type 26) with the specified raw value.
        /// </summary>
        /// <param name="value">The negotiated DSCP octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NegotiatedDscp(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.NEGOTIATED_DSCP, value);
        }

        /// <summary>
        /// Creates a 3GPP-Allocate-IP-Type attribute (Type 27) with the specified allocation type.
        /// </summary>
        /// <param name="value">The requested IP allocation type. See <see cref="THREEGPP_ALLOCATE_IP_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AllocateIpType(THREEGPP_ALLOCATE_IP_TYPE value)
        {
            return CreateOctets(ThreeGppAttributeType.ALLOCATE_IP_TYPE, [(byte)value]);
        }

        /// <summary>
        /// Creates a 3GPP-Allocate-IP-Type attribute (Type 27) with the specified raw octets value.
        /// </summary>
        /// <param name="value">The raw octets value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AllocateIpType(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.ALLOCATE_IP_TYPE, value);
        }

        /// <summary>
        /// Creates a 3GPP-TWAN-Identifier attribute (Type 29) with the specified raw value.
        /// </summary>
        /// <param name="value">The Trusted WLAN Access Network identifier octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TwanIdentifier(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.TWAN_IDENTIFIER, value);
        }

        /// <summary>
        /// Creates a 3GPP-User-Location-Info-Time attribute (Type 30) with the specified raw value.
        /// </summary>
        /// <param name="value">The user location information timestamp octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserLocationInfoTime(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.USER_LOCATION_INFO_TIME, value);
        }

        /// <summary>
        /// Creates a 3GPP-Secondary-RAT-Usage-Report attribute (Type 31) with the specified raw value.
        /// </summary>
        /// <param name="value">The secondary RAT usage report octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SecondaryRatUsageReport(byte[] value)
        {
            return CreateOctets(ThreeGppAttributeType.SECONDARY_RAT_USAGE_REPORT, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified 3GPP attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ThreeGppAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified 3GPP attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ThreeGppAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a raw byte array value for the specified 3GPP attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateOctets(ThreeGppAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified 3GPP attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(ThreeGppAttributeType type, IPAddress value)
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

        /// <summary>
        /// Creates a VSA with an IPv6 address value for the specified 3GPP attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv6(ThreeGppAttributeType type, IPAddress value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.AddressFamily != AddressFamily.InterNetworkV6)
                throw new ArgumentException(
                    $"Expected an IPv6 (InterNetworkV6) address, got '{value.AddressFamily}'.",
                    nameof(value));

            byte[] addrBytes = new byte[16];
            value.TryWriteBytes(addrBytes, out _);

            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, addrBytes);
        }

        #endregion
    }
}