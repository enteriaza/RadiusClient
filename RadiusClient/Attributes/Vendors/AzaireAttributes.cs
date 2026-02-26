using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Azaire Networks (IANA PEN 7751) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.azaire</c>.
    /// </summary>
    public enum AzaireAttributeType : byte
    {
        /// <summary>Azaire-Triplet (Type 1). Octets. GSM authentication triplet.</summary>
        TRIPLET = 1,

        /// <summary>Azaire-IMSI (Type 2). String. International Mobile Subscriber Identity.</summary>
        IMSI = 2,

        /// <summary>Azaire-MSISDN (Type 3). String. Mobile Station ISDN number.</summary>
        MSISDN = 3,

        /// <summary>Azaire-APN (Type 4). String. Access Point Name.</summary>
        APN = 4,

        /// <summary>Azaire-QoS (Type 5). Integer. Quality of Service class.</summary>
        QOS = 5,

        /// <summary>Azaire-Selection-Mode (Type 6). Integer. APN selection mode.</summary>
        SELECTION_MODE = 6,

        /// <summary>Azaire-APN-Resolution-Req (Type 7). Integer. APN resolution required flag.</summary>
        APN_RESOLUTION_REQ = 7,

        /// <summary>Azaire-Start-Time (Type 8). String. Session start time.</summary>
        START_TIME = 8,

        /// <summary>Azaire-NAS-Type (Type 9). Integer. NAS type identifier.</summary>
        NAS_TYPE = 9,

        /// <summary>Azaire-Status (Type 10). Integer. Session status code.</summary>
        STATUS = 10,

        /// <summary>Azaire-APN-OI (Type 11). String. APN Operator Identifier.</summary>
        APN_OI = 11,

        /// <summary>Azaire-Auth-Type (Type 12). Integer. Authentication type.</summary>
        AUTH_TYPE = 12,

        /// <summary>Azaire-Gn-User-Name (Type 13). String. Gn interface user name.</summary>
        GN_USER_NAME = 13,

        /// <summary>Azaire-Brand-Code (Type 14). String. Brand code identifier.</summary>
        BRAND_CODE = 14,

        /// <summary>Azaire-Policy-Name (Type 15). String. Policy name to apply.</summary>
        POLICY_NAME = 15,

        /// <summary>Azaire-Client-Local-IP (Type 16). String. Client local IP address string.</summary>
        CLIENT_LOCAL_IP = 16,

        /// <summary>Azaire-Quintuplet (Type 17). Octets. UMTS authentication quintuplet.</summary>
        QUINTUPLET = 17,

        /// <summary>Azaire-Wireless-City-Code (Type 18). String. Wireless city code.</summary>
        WIRELESS_CITY_CODE = 18,

        /// <summary>Azaire-Wireless-Network-Code (Type 19). String. Wireless network code.</summary>
        WIRELESS_NETWORK_CODE = 19
    }

    /// <summary>
    /// Azaire-Selection-Mode attribute values (Type 6).
    /// </summary>
    public enum AZAIRE_SELECTION_MODE
    {
        /// <summary>Subscribed APN.</summary>
        SUBSCRIBED = 0,

        /// <summary>MS-provided APN, subscription not verified.</summary>
        MS_NOT_VERIFIED = 1,

        /// <summary>Network-provided APN, subscription not verified.</summary>
        NETWORK_NOT_VERIFIED = 2
    }

    /// <summary>
    /// Azaire-APN-Resolution-Req attribute values (Type 7).
    /// </summary>
    public enum AZAIRE_APN_RESOLUTION_REQ
    {
        /// <summary>APN resolution not required.</summary>
        NOT_REQUIRED = 0,

        /// <summary>APN resolution required.</summary>
        REQUIRED = 1
    }

    /// <summary>
    /// Azaire-Auth-Type attribute values (Type 12).
    /// </summary>
    public enum AZAIRE_AUTH_TYPE
    {
        /// <summary>EAP-SIM authentication.</summary>
        EAP_SIM = 1,

        /// <summary>EAP-AKA authentication.</summary>
        EAP_AKA = 2,

        /// <summary>EAP-AKA-Prime authentication.</summary>
        EAP_AKA_PRIME = 3
    }

    /// <summary>
    /// Azaire-NAS-Type attribute values (Type 9).
    /// </summary>
    public enum AZAIRE_NAS_TYPE
    {
        /// <summary>GGSN NAS type.</summary>
        GGSN = 1,

        /// <summary>WLAN NAS type.</summary>
        WLAN = 2,

        /// <summary>ePDG NAS type.</summary>
        EPDG = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Azaire Networks
    /// (IANA PEN 7751) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.azaire</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Azaire's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 7751</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Azaire Networks (now part of Meru / Fortinet)
    /// carrier Wi-Fi and Hotspot 2.0 platforms for GSM/UMTS subscriber authentication
    /// (EAP-SIM/AKA triplets and quintuplets), IMSI/MSISDN identification, APN
    /// selection and resolution, QoS assignment, policy enforcement, and session
    /// management.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AzaireAttributes.Imsi("310150123456789"));
    /// packet.SetAttribute(AzaireAttributes.Apn("internet"));
    /// packet.SetAttribute(AzaireAttributes.AuthType(AZAIRE_AUTH_TYPE.EAP_SIM));
    /// packet.SetAttribute(AzaireAttributes.PolicyName("carrier-wifi-default"));
    /// </code>
    /// </remarks>
    public static class AzaireAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Azaire Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 7751;

        #region Integer Attributes

        /// <summary>
        /// Creates an Azaire-QoS attribute (Type 5) with the specified QoS class.
        /// </summary>
        /// <param name="value">The Quality of Service class.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Qos(int value)
        {
            return CreateInteger(AzaireAttributeType.QOS, value);
        }

        /// <summary>
        /// Creates an Azaire-Selection-Mode attribute (Type 6) with the specified mode.
        /// </summary>
        /// <param name="value">The APN selection mode. See <see cref="AZAIRE_SELECTION_MODE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SelectionMode(AZAIRE_SELECTION_MODE value)
        {
            return CreateInteger(AzaireAttributeType.SELECTION_MODE, (int)value);
        }

        /// <summary>
        /// Creates an Azaire-APN-Resolution-Req attribute (Type 7) with the specified setting.
        /// </summary>
        /// <param name="value">Whether APN resolution is required. See <see cref="AZAIRE_APN_RESOLUTION_REQ"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ApnResolutionReq(AZAIRE_APN_RESOLUTION_REQ value)
        {
            return CreateInteger(AzaireAttributeType.APN_RESOLUTION_REQ, (int)value);
        }

        /// <summary>
        /// Creates an Azaire-NAS-Type attribute (Type 9) with the specified NAS type.
        /// </summary>
        /// <param name="value">The NAS type identifier. See <see cref="AZAIRE_NAS_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NasType(AZAIRE_NAS_TYPE value)
        {
            return CreateInteger(AzaireAttributeType.NAS_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Azaire-Status attribute (Type 10) with the specified status code.
        /// </summary>
        /// <param name="value">The session status code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Status(int value)
        {
            return CreateInteger(AzaireAttributeType.STATUS, value);
        }

        /// <summary>
        /// Creates an Azaire-Auth-Type attribute (Type 12) with the specified authentication type.
        /// </summary>
        /// <param name="value">The authentication type. See <see cref="AZAIRE_AUTH_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthType(AZAIRE_AUTH_TYPE value)
        {
            return CreateInteger(AzaireAttributeType.AUTH_TYPE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Azaire-IMSI attribute (Type 2) with the specified IMSI.
        /// </summary>
        /// <param name="value">The International Mobile Subscriber Identity. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imsi(string value)
        {
            return CreateString(AzaireAttributeType.IMSI, value);
        }

        /// <summary>
        /// Creates an Azaire-MSISDN attribute (Type 3) with the specified MSISDN.
        /// </summary>
        /// <param name="value">The Mobile Station ISDN number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Msisdn(string value)
        {
            return CreateString(AzaireAttributeType.MSISDN, value);
        }

        /// <summary>
        /// Creates an Azaire-APN attribute (Type 4) with the specified Access Point Name.
        /// </summary>
        /// <param name="value">The Access Point Name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Apn(string value)
        {
            return CreateString(AzaireAttributeType.APN, value);
        }

        /// <summary>
        /// Creates an Azaire-Start-Time attribute (Type 8) with the specified start time.
        /// </summary>
        /// <param name="value">The session start time string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes StartTime(string value)
        {
            return CreateString(AzaireAttributeType.START_TIME, value);
        }

        /// <summary>
        /// Creates an Azaire-APN-OI attribute (Type 11) with the specified operator identifier.
        /// </summary>
        /// <param name="value">The APN Operator Identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApnOi(string value)
        {
            return CreateString(AzaireAttributeType.APN_OI, value);
        }

        /// <summary>
        /// Creates an Azaire-Gn-User-Name attribute (Type 13) with the specified user name.
        /// </summary>
        /// <param name="value">The Gn interface user name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GnUserName(string value)
        {
            return CreateString(AzaireAttributeType.GN_USER_NAME, value);
        }

        /// <summary>
        /// Creates an Azaire-Brand-Code attribute (Type 14) with the specified brand code.
        /// </summary>
        /// <param name="value">The brand code identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BrandCode(string value)
        {
            return CreateString(AzaireAttributeType.BRAND_CODE, value);
        }

        /// <summary>
        /// Creates an Azaire-Policy-Name attribute (Type 15) with the specified policy name.
        /// </summary>
        /// <param name="value">The policy name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(AzaireAttributeType.POLICY_NAME, value);
        }

        /// <summary>
        /// Creates an Azaire-Client-Local-IP attribute (Type 16) with the specified IP address string.
        /// </summary>
        /// <param name="value">The client local IP address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientLocalIp(string value)
        {
            return CreateString(AzaireAttributeType.CLIENT_LOCAL_IP, value);
        }

        /// <summary>
        /// Creates an Azaire-Wireless-City-Code attribute (Type 18) with the specified city code.
        /// </summary>
        /// <param name="value">The wireless city code. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WirelessCityCode(string value)
        {
            return CreateString(AzaireAttributeType.WIRELESS_CITY_CODE, value);
        }

        /// <summary>
        /// Creates an Azaire-Wireless-Network-Code attribute (Type 19) with the specified network code.
        /// </summary>
        /// <param name="value">The wireless network code. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WirelessNetworkCode(string value)
        {
            return CreateString(AzaireAttributeType.WIRELESS_NETWORK_CODE, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates an Azaire-Triplet attribute (Type 1) with the specified GSM authentication triplet.
        /// </summary>
        /// <param name="value">The GSM authentication triplet data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Triplet(byte[] value)
        {
            return CreateOctets(AzaireAttributeType.TRIPLET, value);
        }

        /// <summary>
        /// Creates an Azaire-Quintuplet attribute (Type 17) with the specified UMTS authentication quintuplet.
        /// </summary>
        /// <param name="value">The UMTS authentication quintuplet data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Quintuplet(byte[] value)
        {
            return CreateOctets(AzaireAttributeType.QUINTUPLET, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Azaire attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AzaireAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Azaire attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AzaireAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a raw byte array value for the specified Azaire attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateOctets(AzaireAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        #endregion
    }
}