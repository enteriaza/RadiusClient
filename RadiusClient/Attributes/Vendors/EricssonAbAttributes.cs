using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Ericsson AB (IANA PEN 2352) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.ericsson.ab</c>.
    /// </summary>
    /// <remarks>
    /// Ericsson AB uses vendor ID 2352, separate from the Ericsson / Redback Networks
    /// vendor ID 193 defined in <c>dictionary.ericsson</c>. These attributes are specific
    /// to Ericsson mobile packet core equipment (GGSN, PGW, SAPC).
    /// </remarks>
    public enum EricssonAbAttributeType : byte
    {
        /// <summary>Ericsson-AB-Product-Profile (Type 1). String. Product profile name.</summary>
        PRODUCT_PROFILE = 1,

        /// <summary>Ericsson-AB-PDP-Context-Type (Type 2). Integer. PDP context type.</summary>
        PDP_CONTEXT_TYPE = 2,

        /// <summary>Ericsson-AB-SAE-Temporary-User-Id (Type 3). Octets. SAE temporary user identifier.</summary>
        SAE_TEMPORARY_USER_ID = 3,

        /// <summary>Ericsson-AB-Service-Area-Code (Type 4). Octets. Service area code.</summary>
        SERVICE_AREA_CODE = 4,

        /// <summary>Ericsson-AB-APN (Type 5). String. Access Point Name.</summary>
        APN = 5,

        /// <summary>Ericsson-AB-Charging-Id (Type 6). Integer. Charging identifier.</summary>
        CHARGING_ID = 6,

        /// <summary>Ericsson-AB-Request-Type (Type 7). Integer. Request type code.</summary>
        REQUEST_TYPE = 7,

        /// <summary>Ericsson-AB-NAS-IP-Address (Type 8). IP address. NAS IP address.</summary>
        NAS_IP_ADDRESS = 8,

        /// <summary>Ericsson-AB-Zone-Id (Type 9). String. Zone identifier.</summary>
        ZONE_ID = 9,

        /// <summary>Ericsson-AB-Charging-Characteristics (Type 10). String. 3GPP charging characteristics.</summary>
        CHARGING_CHARACTERISTICS = 10,

        /// <summary>Ericsson-AB-Result-Code (Type 11). Integer. Result code.</summary>
        RESULT_CODE = 11,

        /// <summary>Ericsson-AB-SGSN-IP-Address (Type 12). IP address. SGSN IP address.</summary>
        SGSN_IP_ADDRESS = 12,

        /// <summary>Ericsson-AB-GGSN-IP-Address (Type 13). IP address. GGSN IP address.</summary>
        GGSN_IP_ADDRESS = 13,

        /// <summary>Ericsson-AB-IMSI (Type 14). String. International Mobile Subscriber Identity.</summary>
        IMSI = 14,

        /// <summary>Ericsson-AB-MSISDN (Type 15). String. Mobile Station International Subscriber Directory Number.</summary>
        MSISDN = 15,

        /// <summary>Ericsson-AB-IMEI (Type 16). String. International Mobile Equipment Identity.</summary>
        IMEI = 16,

        /// <summary>Ericsson-AB-User-Location-Info (Type 17). Octets. User location information.</summary>
        USER_LOCATION_INFO = 17,

        /// <summary>Ericsson-AB-RAT-Type (Type 18). Integer. Radio Access Technology type.</summary>
        RAT_TYPE = 18,

        /// <summary>Ericsson-AB-IMEISV (Type 19). String. IMEI Software Version.</summary>
        IMEISV = 19,

        /// <summary>Ericsson-AB-Service-Type (Type 20). Integer. Service type code.</summary>
        SERVICE_TYPE = 20,

        /// <summary>Ericsson-AB-QoS-Profile (Type 21). Octets. QoS profile data.</summary>
        QOS_PROFILE = 21,

        /// <summary>Ericsson-AB-QoS-Subscribed (Type 22). Octets. Subscribed QoS profile data.</summary>
        QOS_SUBSCRIBED = 22,

        /// <summary>Ericsson-AB-QoS-Requested (Type 23). Octets. Requested QoS profile data.</summary>
        QOS_REQUESTED = 23,

        /// <summary>Ericsson-AB-QoS-Negotiated (Type 24). Octets. Negotiated QoS profile data.</summary>
        QOS_NEGOTIATED = 24,

        /// <summary>Ericsson-AB-Session-Stop-Indicator (Type 25). Integer. Session stop indicator.</summary>
        SESSION_STOP_INDICATOR = 25,

        /// <summary>Ericsson-AB-MIP-HA-Address (Type 26). IP address. Mobile IP Home Agent address.</summary>
        MIP_HA_ADDRESS = 26,

        /// <summary>Ericsson-AB-Accounting-Input-Octets (Type 27). Integer. Accounting input octets.</summary>
        ACCOUNTING_INPUT_OCTETS = 27,

        /// <summary>Ericsson-AB-Accounting-Output-Octets (Type 28). Integer. Accounting output octets.</summary>
        ACCOUNTING_OUTPUT_OCTETS = 28,

        /// <summary>Ericsson-AB-Bearer-Id (Type 29). Integer. EPS bearer identifier.</summary>
        BEARER_ID = 29,

        /// <summary>Ericsson-AB-PCRF-Address (Type 30). String. PCRF (Policy and Charging Rules Function) address.</summary>
        PCRF_ADDRESS = 30
    }

    /// <summary>
    /// Ericsson-AB-PDP-Context-Type attribute values (Type 2).
    /// </summary>
    public enum ERICSSON_AB_PDP_CONTEXT_TYPE
    {
        /// <summary>Primary PDP context.</summary>
        PRIMARY = 0,

        /// <summary>Secondary PDP context.</summary>
        SECONDARY = 1
    }

    /// <summary>
    /// Ericsson-AB-RAT-Type attribute values (Type 18).
    /// </summary>
    public enum ERICSSON_AB_RAT_TYPE
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

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Ericsson AB
    /// (IANA PEN 2352) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.ericsson.ab</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Ericsson AB's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2352</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Ericsson AB mobile packet core equipment including
    /// GGSN (Gateway GPRS Support Node), PGW (Packet Data Network Gateway), and
    /// SAPC (Service Aware Policy Controller) for RADIUS-based subscriber
    /// authentication and accounting in 2G/3G/4G mobile networks, covering
    /// PDP context management, 3GPP subscriber identity (IMSI, MSISDN, IMEI),
    /// APN selection, charging, QoS profiles, user location, Radio Access Technology
    /// type, bearer management, and PCRF integration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(EricssonAbAttributes.Apn("internet.example.com"));
    /// packet.SetAttribute(EricssonAbAttributes.Imsi("310260123456789"));
    /// packet.SetAttribute(EricssonAbAttributes.Msisdn("+15551234567"));
    /// packet.SetAttribute(EricssonAbAttributes.RatType(ERICSSON_AB_RAT_TYPE.EUTRAN));
    /// packet.SetAttribute(EricssonAbAttributes.PdpContextType(ERICSSON_AB_PDP_CONTEXT_TYPE.PRIMARY));
    /// </code>
    /// </remarks>
    public static class EricssonAbAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Ericsson AB.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2352;

        #region Integer Attributes

        /// <summary>
        /// Creates an Ericsson-AB-PDP-Context-Type attribute (Type 2).
        /// </summary>
        /// <param name="value">The PDP context type. See <see cref="ERICSSON_AB_PDP_CONTEXT_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PdpContextType(ERICSSON_AB_PDP_CONTEXT_TYPE value) => CreateInteger(EricssonAbAttributeType.PDP_CONTEXT_TYPE, (int)value);

        /// <summary>Creates an Ericsson-AB-Charging-Id attribute (Type 6).</summary>
        /// <param name="value">The charging identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ChargingId(int value) => CreateInteger(EricssonAbAttributeType.CHARGING_ID, value);

        /// <summary>Creates an Ericsson-AB-Request-Type attribute (Type 7).</summary>
        /// <param name="value">The request type code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RequestType(int value) => CreateInteger(EricssonAbAttributeType.REQUEST_TYPE, value);

        /// <summary>Creates an Ericsson-AB-Result-Code attribute (Type 11).</summary>
        /// <param name="value">The result code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ResultCode(int value) => CreateInteger(EricssonAbAttributeType.RESULT_CODE, value);

        /// <summary>
        /// Creates an Ericsson-AB-RAT-Type attribute (Type 18).
        /// </summary>
        /// <param name="value">The Radio Access Technology type. See <see cref="ERICSSON_AB_RAT_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RatType(ERICSSON_AB_RAT_TYPE value) => CreateInteger(EricssonAbAttributeType.RAT_TYPE, (int)value);

        /// <summary>Creates an Ericsson-AB-Service-Type attribute (Type 20).</summary>
        /// <param name="value">The service type code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServiceType(int value) => CreateInteger(EricssonAbAttributeType.SERVICE_TYPE, value);

        /// <summary>Creates an Ericsson-AB-Session-Stop-Indicator attribute (Type 25).</summary>
        /// <param name="value">The session stop indicator.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionStopIndicator(int value) => CreateInteger(EricssonAbAttributeType.SESSION_STOP_INDICATOR, value);

        /// <summary>Creates an Ericsson-AB-Accounting-Input-Octets attribute (Type 27).</summary>
        /// <param name="value">The accounting input octets.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccountingInputOctets(int value) => CreateInteger(EricssonAbAttributeType.ACCOUNTING_INPUT_OCTETS, value);

        /// <summary>Creates an Ericsson-AB-Accounting-Output-Octets attribute (Type 28).</summary>
        /// <param name="value">The accounting output octets.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccountingOutputOctets(int value) => CreateInteger(EricssonAbAttributeType.ACCOUNTING_OUTPUT_OCTETS, value);

        /// <summary>Creates an Ericsson-AB-Bearer-Id attribute (Type 29).</summary>
        /// <param name="value">The EPS bearer identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BearerId(int value) => CreateInteger(EricssonAbAttributeType.BEARER_ID, value);

        #endregion

        #region String Attributes

        /// <summary>Creates an Ericsson-AB-Product-Profile attribute (Type 1).</summary>
        /// <param name="value">The product profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProductProfile(string value) => CreateString(EricssonAbAttributeType.PRODUCT_PROFILE, value);

        /// <summary>Creates an Ericsson-AB-APN attribute (Type 5).</summary>
        /// <param name="value">The Access Point Name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Apn(string value) => CreateString(EricssonAbAttributeType.APN, value);

        /// <summary>Creates an Ericsson-AB-Zone-Id attribute (Type 9).</summary>
        /// <param name="value">The zone identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ZoneId(string value) => CreateString(EricssonAbAttributeType.ZONE_ID, value);

        /// <summary>Creates an Ericsson-AB-Charging-Characteristics attribute (Type 10).</summary>
        /// <param name="value">The 3GPP charging characteristics. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChargingCharacteristics(string value) => CreateString(EricssonAbAttributeType.CHARGING_CHARACTERISTICS, value);

        /// <summary>Creates an Ericsson-AB-IMSI attribute (Type 14).</summary>
        /// <param name="value">The International Mobile Subscriber Identity. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imsi(string value) => CreateString(EricssonAbAttributeType.IMSI, value);

        /// <summary>Creates an Ericsson-AB-MSISDN attribute (Type 15).</summary>
        /// <param name="value">The Mobile Station International Subscriber Directory Number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Msisdn(string value) => CreateString(EricssonAbAttributeType.MSISDN, value);

        /// <summary>Creates an Ericsson-AB-IMEI attribute (Type 16).</summary>
        /// <param name="value">The International Mobile Equipment Identity. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imei(string value) => CreateString(EricssonAbAttributeType.IMEI, value);

        /// <summary>Creates an Ericsson-AB-IMEISV attribute (Type 19).</summary>
        /// <param name="value">The IMEI Software Version. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imeisv(string value) => CreateString(EricssonAbAttributeType.IMEISV, value);

        /// <summary>Creates an Ericsson-AB-PCRF-Address attribute (Type 30).</summary>
        /// <param name="value">The PCRF address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PcrfAddress(string value) => CreateString(EricssonAbAttributeType.PCRF_ADDRESS, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates an Ericsson-AB-SAE-Temporary-User-Id attribute (Type 3).</summary>
        /// <param name="value">The SAE temporary user identifier data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SaeTemporaryUserId(byte[] value) => CreateOctets(EricssonAbAttributeType.SAE_TEMPORARY_USER_ID, value);

        /// <summary>Creates an Ericsson-AB-Service-Area-Code attribute (Type 4).</summary>
        /// <param name="value">The service area code data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceAreaCode(byte[] value) => CreateOctets(EricssonAbAttributeType.SERVICE_AREA_CODE, value);

        /// <summary>Creates an Ericsson-AB-User-Location-Info attribute (Type 17).</summary>
        /// <param name="value">The user location information data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserLocationInfo(byte[] value) => CreateOctets(EricssonAbAttributeType.USER_LOCATION_INFO, value);

        /// <summary>Creates an Ericsson-AB-QoS-Profile attribute (Type 21).</summary>
        /// <param name="value">The QoS profile data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(byte[] value) => CreateOctets(EricssonAbAttributeType.QOS_PROFILE, value);

        /// <summary>Creates an Ericsson-AB-QoS-Subscribed attribute (Type 22).</summary>
        /// <param name="value">The subscribed QoS profile data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosSubscribed(byte[] value) => CreateOctets(EricssonAbAttributeType.QOS_SUBSCRIBED, value);

        /// <summary>Creates an Ericsson-AB-QoS-Requested attribute (Type 23).</summary>
        /// <param name="value">The requested QoS profile data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosRequested(byte[] value) => CreateOctets(EricssonAbAttributeType.QOS_REQUESTED, value);

        /// <summary>Creates an Ericsson-AB-QoS-Negotiated attribute (Type 24).</summary>
        /// <param name="value">The negotiated QoS profile data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosNegotiated(byte[] value) => CreateOctets(EricssonAbAttributeType.QOS_NEGOTIATED, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Ericsson-AB-NAS-IP-Address attribute (Type 8) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The NAS IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes NasIpAddress(IPAddress value) => CreateIpv4(EricssonAbAttributeType.NAS_IP_ADDRESS, value);

        /// <summary>
        /// Creates an Ericsson-AB-SGSN-IP-Address attribute (Type 12) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The SGSN IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SgsnIpAddress(IPAddress value) => CreateIpv4(EricssonAbAttributeType.SGSN_IP_ADDRESS, value);

        /// <summary>
        /// Creates an Ericsson-AB-GGSN-IP-Address attribute (Type 13) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The GGSN IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes GgsnIpAddress(IPAddress value) => CreateIpv4(EricssonAbAttributeType.GGSN_IP_ADDRESS, value);

        /// <summary>
        /// Creates an Ericsson-AB-MIP-HA-Address attribute (Type 26) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The Mobile IP Home Agent address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes MipHaAddress(IPAddress value) => CreateIpv4(EricssonAbAttributeType.MIP_HA_ADDRESS, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(EricssonAbAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(EricssonAbAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(EricssonAbAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        private static VendorSpecificAttributes CreateIpv4(EricssonAbAttributeType type, IPAddress value)
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