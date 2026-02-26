using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Ericsson Packet Core Networks (IANA PEN 10923) vendor-specific
    /// RADIUS attribute type, as defined in the FreeRADIUS
    /// <c>dictionary.ericsson.packet.core.networks</c>.
    /// </summary>
    /// <remarks>
    /// Ericsson Packet Core Networks uses vendor ID 10923, separate from
    /// Ericsson / Redback (PEN 193) and Ericsson AB (PEN 2352). These attributes
    /// are specific to Ericsson evolved packet core (EPC), IMS, and mobile
    /// broadband gateway platforms.
    /// </remarks>
    public enum EricssonPacketCoreAttributeType : byte
    {
        /// <summary>EPCN-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>EPCN-APN (Type 2). String. Access Point Name.</summary>
        APN = 2,

        /// <summary>EPCN-IMSI (Type 3). String. International Mobile Subscriber Identity.</summary>
        IMSI = 3,

        /// <summary>EPCN-MSISDN (Type 4). String. Mobile Station International Subscriber Directory Number.</summary>
        MSISDN = 4,

        /// <summary>EPCN-IMEI (Type 5). String. International Mobile Equipment Identity.</summary>
        IMEI = 5,

        /// <summary>EPCN-IMEISV (Type 6). String. IMEI Software Version.</summary>
        IMEISV = 6,

        /// <summary>EPCN-Charging-Id (Type 7). Integer. Charging identifier.</summary>
        CHARGING_ID = 7,

        /// <summary>EPCN-PDP-Type (Type 8). Integer. PDP/PDN type.</summary>
        PDP_TYPE = 8,

        /// <summary>EPCN-Charging-Characteristics (Type 9). String. 3GPP charging characteristics.</summary>
        CHARGING_CHARACTERISTICS = 9,

        /// <summary>EPCN-SGSN-Address (Type 10). IP address. SGSN/MME IP address.</summary>
        SGSN_ADDRESS = 10,

        /// <summary>EPCN-GGSN-Address (Type 11). IP address. GGSN/PGW IP address.</summary>
        GGSN_ADDRESS = 11,

        /// <summary>EPCN-RAT-Type (Type 12). Integer. Radio Access Technology type.</summary>
        RAT_TYPE = 12,

        /// <summary>EPCN-User-Location-Info (Type 13). Octets. User location information.</summary>
        USER_LOCATION_INFO = 13,

        /// <summary>EPCN-MS-TimeZone (Type 14). Octets. MS time zone information.</summary>
        MS_TIMEZONE = 14,

        /// <summary>EPCN-QoS-Subscribed (Type 15). Octets. Subscribed QoS profile.</summary>
        QOS_SUBSCRIBED = 15,

        /// <summary>EPCN-QoS-Requested (Type 16). Octets. Requested QoS profile.</summary>
        QOS_REQUESTED = 16,

        /// <summary>EPCN-QoS-Negotiated (Type 17). Octets. Negotiated QoS profile.</summary>
        QOS_NEGOTIATED = 17,

        /// <summary>EPCN-Selection-Mode (Type 18). Integer. APN selection mode.</summary>
        SELECTION_MODE = 18,

        /// <summary>EPCN-Result-Code (Type 19). Integer. Result code.</summary>
        RESULT_CODE = 19,

        /// <summary>EPCN-MIP-HA-Address (Type 20). IP address. Mobile IP Home Agent address.</summary>
        MIP_HA_ADDRESS = 20,

        /// <summary>EPCN-Bearer-Id (Type 21). Integer. EPS bearer identifier.</summary>
        BEARER_ID = 21,

        /// <summary>EPCN-Bearer-Type (Type 22). Integer. Bearer type.</summary>
        BEARER_TYPE = 22,

        /// <summary>EPCN-Bearer-Operation (Type 23). Integer. Bearer operation.</summary>
        BEARER_OPERATION = 23,

        /// <summary>EPCN-APN-AMBR-DL (Type 24). Integer. APN Aggregate Maximum Bit Rate downlink in Kbps.</summary>
        APN_AMBR_DL = 24,

        /// <summary>EPCN-APN-AMBR-UL (Type 25). Integer. APN Aggregate Maximum Bit Rate uplink in Kbps.</summary>
        APN_AMBR_UL = 25,

        /// <summary>EPCN-APN-Restriction (Type 26). Integer. APN restriction value.</summary>
        APN_RESTRICTION = 26,

        /// <summary>EPCN-Called-Station-Id (Type 27). String. Called station identifier.</summary>
        CALLED_STATION_ID = 27,

        /// <summary>EPCN-Calling-Station-Id (Type 28). String. Calling station identifier.</summary>
        CALLING_STATION_ID = 28,

        /// <summary>EPCN-Input-Octets (Type 29). Integer. Input octets count.</summary>
        INPUT_OCTETS = 29,

        /// <summary>EPCN-Output-Octets (Type 30). Integer. Output octets count.</summary>
        OUTPUT_OCTETS = 30,

        /// <summary>EPCN-Input-Packets (Type 31). Integer. Input packets count.</summary>
        INPUT_PACKETS = 31,

        /// <summary>EPCN-Output-Packets (Type 32). Integer. Output packets count.</summary>
        OUTPUT_PACKETS = 32,

        /// <summary>EPCN-Session-Stop-Indicator (Type 33). Integer. Session stop indicator.</summary>
        SESSION_STOP_INDICATOR = 33,

        /// <summary>EPCN-Service-Type (Type 34). Integer. Service type code.</summary>
        SERVICE_TYPE = 34,

        /// <summary>EPCN-PCRF-Address (Type 35). String. PCRF address.</summary>
        PCRF_ADDRESS = 35,

        /// <summary>EPCN-SGW-Address (Type 36). IP address. Serving Gateway address.</summary>
        SGW_ADDRESS = 36,

        /// <summary>EPCN-PDN-Connection-Id (Type 37). Integer. PDN connection identifier.</summary>
        PDN_CONNECTION_ID = 37
    }

    /// <summary>
    /// EPCN-PDP-Type attribute values (Type 8).
    /// </summary>
    public enum EPCN_PDP_TYPE
    {
        /// <summary>IPv4 PDP/PDN type.</summary>
        IPV4 = 0,

        /// <summary>PPP PDP type.</summary>
        PPP = 1,

        /// <summary>IPv6 PDP/PDN type.</summary>
        IPV6 = 2,

        /// <summary>IPv4v6 (dual-stack) PDN type.</summary>
        IPV4V6 = 3
    }

    /// <summary>
    /// EPCN-RAT-Type attribute values (Type 12).
    /// </summary>
    public enum EPCN_RAT_TYPE
    {
        /// <summary>UTRAN (3G UMTS).</summary>
        UTRAN = 1,

        /// <summary>GERAN (2G GSM/EDGE).</summary>
        GERAN = 2,

        /// <summary>WLAN.</summary>
        WLAN = 3,

        /// <summary>GAN (Generic Access Network).</summary>
        GAN = 4,

        /// <summary>HSPA Evolution.</summary>
        HSPA_EVOLUTION = 5,

        /// <summary>E-UTRAN (4G LTE).</summary>
        EUTRAN = 6
    }

    /// <summary>
    /// EPCN-Selection-Mode attribute values (Type 18).
    /// </summary>
    public enum EPCN_SELECTION_MODE
    {
        /// <summary>MS or network provided APN, subscription verified.</summary>
        MS_OR_NETWORK_PROVIDED_SUBSCRIPTION_VERIFIED = 0,

        /// <summary>MS provided APN, subscription not verified.</summary>
        MS_PROVIDED_SUBSCRIPTION_NOT_VERIFIED = 1,

        /// <summary>Network provided APN, subscription not verified.</summary>
        NETWORK_PROVIDED_SUBSCRIPTION_NOT_VERIFIED = 2
    }

    /// <summary>
    /// EPCN-Bearer-Type attribute values (Type 22).
    /// </summary>
    public enum EPCN_BEARER_TYPE
    {
        /// <summary>Default bearer.</summary>
        DEFAULT = 0,

        /// <summary>Dedicated bearer.</summary>
        DEDICATED = 1
    }

    /// <summary>
    /// EPCN-Bearer-Operation attribute values (Type 23).
    /// </summary>
    public enum EPCN_BEARER_OPERATION
    {
        /// <summary>Bearer establishment.</summary>
        ESTABLISHMENT = 0,

        /// <summary>Bearer modification.</summary>
        MODIFICATION = 1,

        /// <summary>Bearer termination.</summary>
        TERMINATION = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Ericsson Packet Core Networks
    /// (IANA PEN 10923) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.ericsson.packet.core.networks</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Ericsson Packet Core Networks' vendor-specific attributes follow the standard
    /// VSA layout defined in RFC 2865 §5.26. All attributes produced by this class
    /// are wrapped in a <see cref="VendorSpecificAttributes"/> instance with
    /// <c>VendorId = 10923</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Ericsson evolved packet core (EPC) platforms
    /// including PGW (PDN Gateway), SGW (Serving Gateway), MME (Mobility Management
    /// Entity), and PCEF (Policy and Charging Enforcement Function) for RADIUS-based
    /// subscriber authentication and accounting in 2G/3G/4G mobile networks, covering
    /// APN and PDN connection management, 3GPP subscriber identity (IMSI, MSISDN,
    /// IMEI, IMEISV), charging, QoS profiles (subscribed/requested/negotiated),
    /// APN AMBR, EPS bearer lifecycle management, user location, Radio Access
    /// Technology type, session accounting, and PCRF integration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(EricssonPacketCoreAttributes.Apn("internet.example.com"));
    /// packet.SetAttribute(EricssonPacketCoreAttributes.Imsi("310260123456789"));
    /// packet.SetAttribute(EricssonPacketCoreAttributes.RatType(EPCN_RAT_TYPE.EUTRAN));
    /// packet.SetAttribute(EricssonPacketCoreAttributes.ApnAmbrDl(50000));
    /// packet.SetAttribute(EricssonPacketCoreAttributes.ApnAmbrUl(10000));
    /// packet.SetAttribute(EricssonPacketCoreAttributes.BearerType(EPCN_BEARER_TYPE.DEFAULT));
    /// </code>
    /// </remarks>
    public static class EricssonPacketCoreAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Ericsson Packet Core Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 10923;

        #region Integer Attributes

        /// <summary>Creates an EPCN-Charging-Id attribute (Type 7).</summary>
        /// <param name="value">The charging identifier.</param>
        public static VendorSpecificAttributes ChargingId(int value) => CreateInteger(EricssonPacketCoreAttributeType.CHARGING_ID, value);

        /// <summary>
        /// Creates an EPCN-PDP-Type attribute (Type 8).
        /// </summary>
        /// <param name="value">The PDP/PDN type. See <see cref="EPCN_PDP_TYPE"/>.</param>
        public static VendorSpecificAttributes PdpType(EPCN_PDP_TYPE value) => CreateInteger(EricssonPacketCoreAttributeType.PDP_TYPE, (int)value);

        /// <summary>
        /// Creates an EPCN-RAT-Type attribute (Type 12).
        /// </summary>
        /// <param name="value">The Radio Access Technology type. See <see cref="EPCN_RAT_TYPE"/>.</param>
        public static VendorSpecificAttributes RatType(EPCN_RAT_TYPE value) => CreateInteger(EricssonPacketCoreAttributeType.RAT_TYPE, (int)value);

        /// <summary>
        /// Creates an EPCN-Selection-Mode attribute (Type 18).
        /// </summary>
        /// <param name="value">The APN selection mode. See <see cref="EPCN_SELECTION_MODE"/>.</param>
        public static VendorSpecificAttributes SelectionMode(EPCN_SELECTION_MODE value) => CreateInteger(EricssonPacketCoreAttributeType.SELECTION_MODE, (int)value);

        /// <summary>Creates an EPCN-Result-Code attribute (Type 19).</summary>
        /// <param name="value">The result code.</param>
        public static VendorSpecificAttributes ResultCode(int value) => CreateInteger(EricssonPacketCoreAttributeType.RESULT_CODE, value);

        /// <summary>Creates an EPCN-Bearer-Id attribute (Type 21).</summary>
        /// <param name="value">The EPS bearer identifier.</param>
        public static VendorSpecificAttributes BearerId(int value) => CreateInteger(EricssonPacketCoreAttributeType.BEARER_ID, value);

        /// <summary>
        /// Creates an EPCN-Bearer-Type attribute (Type 22).
        /// </summary>
        /// <param name="value">The bearer type. See <see cref="EPCN_BEARER_TYPE"/>.</param>
        public static VendorSpecificAttributes BearerType(EPCN_BEARER_TYPE value) => CreateInteger(EricssonPacketCoreAttributeType.BEARER_TYPE, (int)value);

        /// <summary>
        /// Creates an EPCN-Bearer-Operation attribute (Type 23).
        /// </summary>
        /// <param name="value">The bearer operation. See <see cref="EPCN_BEARER_OPERATION"/>.</param>
        public static VendorSpecificAttributes BearerOperation(EPCN_BEARER_OPERATION value) => CreateInteger(EricssonPacketCoreAttributeType.BEARER_OPERATION, (int)value);

        /// <summary>Creates an EPCN-APN-AMBR-DL attribute (Type 24).</summary>
        /// <param name="value">The APN Aggregate Maximum Bit Rate downlink in Kbps.</param>
        public static VendorSpecificAttributes ApnAmbrDl(int value) => CreateInteger(EricssonPacketCoreAttributeType.APN_AMBR_DL, value);

        /// <summary>Creates an EPCN-APN-AMBR-UL attribute (Type 25).</summary>
        /// <param name="value">The APN Aggregate Maximum Bit Rate uplink in Kbps.</param>
        public static VendorSpecificAttributes ApnAmbrUl(int value) => CreateInteger(EricssonPacketCoreAttributeType.APN_AMBR_UL, value);

        /// <summary>Creates an EPCN-APN-Restriction attribute (Type 26).</summary>
        /// <param name="value">The APN restriction value.</param>
        public static VendorSpecificAttributes ApnRestriction(int value) => CreateInteger(EricssonPacketCoreAttributeType.APN_RESTRICTION, value);

        /// <summary>Creates an EPCN-Input-Octets attribute (Type 29).</summary>
        /// <param name="value">The input octets count.</param>
        public static VendorSpecificAttributes InputOctets(int value) => CreateInteger(EricssonPacketCoreAttributeType.INPUT_OCTETS, value);

        /// <summary>Creates an EPCN-Output-Octets attribute (Type 30).</summary>
        /// <param name="value">The output octets count.</param>
        public static VendorSpecificAttributes OutputOctets(int value) => CreateInteger(EricssonPacketCoreAttributeType.OUTPUT_OCTETS, value);

        /// <summary>Creates an EPCN-Input-Packets attribute (Type 31).</summary>
        /// <param name="value">The input packets count.</param>
        public static VendorSpecificAttributes InputPackets(int value) => CreateInteger(EricssonPacketCoreAttributeType.INPUT_PACKETS, value);

        /// <summary>Creates an EPCN-Output-Packets attribute (Type 32).</summary>
        /// <param name="value">The output packets count.</param>
        public static VendorSpecificAttributes OutputPackets(int value) => CreateInteger(EricssonPacketCoreAttributeType.OUTPUT_PACKETS, value);

        /// <summary>Creates an EPCN-Session-Stop-Indicator attribute (Type 33).</summary>
        /// <param name="value">The session stop indicator.</param>
        public static VendorSpecificAttributes SessionStopIndicator(int value) => CreateInteger(EricssonPacketCoreAttributeType.SESSION_STOP_INDICATOR, value);

        /// <summary>Creates an EPCN-Service-Type attribute (Type 34).</summary>
        /// <param name="value">The service type code.</param>
        public static VendorSpecificAttributes ServiceType(int value) => CreateInteger(EricssonPacketCoreAttributeType.SERVICE_TYPE, value);

        /// <summary>Creates an EPCN-PDN-Connection-Id attribute (Type 37).</summary>
        /// <param name="value">The PDN connection identifier.</param>
        public static VendorSpecificAttributes PdnConnectionId(int value) => CreateInteger(EricssonPacketCoreAttributeType.PDN_CONNECTION_ID, value);

        #endregion

        #region String Attributes

        /// <summary>Creates an EPCN-AVPair attribute (Type 1).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(EricssonPacketCoreAttributeType.AVPAIR, value);

        /// <summary>Creates an EPCN-APN attribute (Type 2).</summary>
        /// <param name="value">The Access Point Name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Apn(string value) => CreateString(EricssonPacketCoreAttributeType.APN, value);

        /// <summary>Creates an EPCN-IMSI attribute (Type 3).</summary>
        /// <param name="value">The International Mobile Subscriber Identity. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imsi(string value) => CreateString(EricssonPacketCoreAttributeType.IMSI, value);

        /// <summary>Creates an EPCN-MSISDN attribute (Type 4).</summary>
        /// <param name="value">The MSISDN. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Msisdn(string value) => CreateString(EricssonPacketCoreAttributeType.MSISDN, value);

        /// <summary>Creates an EPCN-IMEI attribute (Type 5).</summary>
        /// <param name="value">The International Mobile Equipment Identity. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imei(string value) => CreateString(EricssonPacketCoreAttributeType.IMEI, value);

        /// <summary>Creates an EPCN-IMEISV attribute (Type 6).</summary>
        /// <param name="value">The IMEI Software Version. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imeisv(string value) => CreateString(EricssonPacketCoreAttributeType.IMEISV, value);

        /// <summary>Creates an EPCN-Charging-Characteristics attribute (Type 9).</summary>
        /// <param name="value">The 3GPP charging characteristics. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChargingCharacteristics(string value) => CreateString(EricssonPacketCoreAttributeType.CHARGING_CHARACTERISTICS, value);

        /// <summary>Creates an EPCN-Called-Station-Id attribute (Type 27).</summary>
        /// <param name="value">The called station identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledStationId(string value) => CreateString(EricssonPacketCoreAttributeType.CALLED_STATION_ID, value);

        /// <summary>Creates an EPCN-Calling-Station-Id attribute (Type 28).</summary>
        /// <param name="value">The calling station identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingStationId(string value) => CreateString(EricssonPacketCoreAttributeType.CALLING_STATION_ID, value);

        /// <summary>Creates an EPCN-PCRF-Address attribute (Type 35).</summary>
        /// <param name="value">The PCRF address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PcrfAddress(string value) => CreateString(EricssonPacketCoreAttributeType.PCRF_ADDRESS, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates an EPCN-User-Location-Info attribute (Type 13).</summary>
        /// <param name="value">The user location information data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserLocationInfo(byte[] value) => CreateOctets(EricssonPacketCoreAttributeType.USER_LOCATION_INFO, value);

        /// <summary>Creates an EPCN-MS-TimeZone attribute (Type 14).</summary>
        /// <param name="value">The MS time zone information data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsTimezone(byte[] value) => CreateOctets(EricssonPacketCoreAttributeType.MS_TIMEZONE, value);

        /// <summary>Creates an EPCN-QoS-Subscribed attribute (Type 15).</summary>
        /// <param name="value">The subscribed QoS profile data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosSubscribed(byte[] value) => CreateOctets(EricssonPacketCoreAttributeType.QOS_SUBSCRIBED, value);

        /// <summary>Creates an EPCN-QoS-Requested attribute (Type 16).</summary>
        /// <param name="value">The requested QoS profile data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosRequested(byte[] value) => CreateOctets(EricssonPacketCoreAttributeType.QOS_REQUESTED, value);

        /// <summary>Creates an EPCN-QoS-Negotiated attribute (Type 17).</summary>
        /// <param name="value">The negotiated QoS profile data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosNegotiated(byte[] value) => CreateOctets(EricssonPacketCoreAttributeType.QOS_NEGOTIATED, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an EPCN-SGSN-Address attribute (Type 10) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The SGSN/MME IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SgsnAddress(IPAddress value) => CreateIpv4(EricssonPacketCoreAttributeType.SGSN_ADDRESS, value);

        /// <summary>
        /// Creates an EPCN-GGSN-Address attribute (Type 11) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The GGSN/PGW IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes GgsnAddress(IPAddress value) => CreateIpv4(EricssonPacketCoreAttributeType.GGSN_ADDRESS, value);

        /// <summary>
        /// Creates an EPCN-MIP-HA-Address attribute (Type 20) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The Mobile IP Home Agent address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes MipHaAddress(IPAddress value) => CreateIpv4(EricssonPacketCoreAttributeType.MIP_HA_ADDRESS, value);

        /// <summary>
        /// Creates an EPCN-SGW-Address attribute (Type 36) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The Serving Gateway address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SgwAddress(IPAddress value) => CreateIpv4(EricssonPacketCoreAttributeType.SGW_ADDRESS, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(EricssonPacketCoreAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(EricssonPacketCoreAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(EricssonPacketCoreAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        private static VendorSpecificAttributes CreateIpv4(EricssonPacketCoreAttributeType type, IPAddress value)
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