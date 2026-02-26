using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Kineto Wireless (IANA PEN 16445) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.kineto</c>.
    /// </summary>
    /// <remarks>
    /// Kineto Wireless developed UMA (Unlicensed Mobile Access) and GAN
    /// (Generic Access Network) technology for mobile-to-WiFi handoff,
    /// enabling GSM/UMTS services over IP networks.
    /// </remarks>
    public enum KinetoAttributeType : byte
    {
        /// <summary>Kineto-UMA-Release-Indicator (Type 1). Integer. UMA release indicator.</summary>
        UMA_RELEASE_INDICATOR = 1,

        /// <summary>Kineto-UMA-AP-Radio-Identity (Type 2). String. UMA access point radio identity.</summary>
        UMA_AP_RADIO_IDENTITY = 2,

        /// <summary>Kineto-UMA-Cell-Identity (Type 3). Integer. UMA cell identity.</summary>
        UMA_CELL_IDENTITY = 3,

        /// <summary>Kineto-UMA-Location-Area-Id (Type 4). String. UMA location area identifier.</summary>
        UMA_LOCATION_AREA_ID = 4,

        /// <summary>Kineto-UMA-Coverage-Indicator (Type 5). Integer. UMA coverage indicator.</summary>
        UMA_COVERAGE_INDICATOR = 5,

        /// <summary>Kineto-UMA-Classmark (Type 6). Octets. UMA mobile classmark.</summary>
        UMA_CLASSMARK = 6,

        /// <summary>Kineto-UMA-Geographical-Location (Type 7). Octets. UMA geographical location data.</summary>
        UMA_GEOGRAPHICAL_LOCATION = 7,

        /// <summary>Kineto-UMA-SGW-IP-Address (Type 8). String. UMA security gateway IP address.</summary>
        UMA_SGW_IP_ADDRESS = 8,

        /// <summary>Kineto-UMA-SGW-FQDN (Type 9). String. UMA security gateway FQDN.</summary>
        UMA_SGW_FQDN = 9,

        /// <summary>Kineto-UMA-Redirection-Counter (Type 10). Integer. UMA redirection counter.</summary>
        UMA_REDIRECTION_COUNTER = 10,

        /// <summary>Kineto-UMA-Discovery-Reject-Cause (Type 11). Integer. UMA discovery reject cause.</summary>
        UMA_DISCOVERY_REJECT_CAUSE = 11,

        /// <summary>Kineto-UMA-RRC-State (Type 12). Integer. UMA RRC state.</summary>
        UMA_RRC_STATE = 12,

        /// <summary>Kineto-UMA-RU-Handling-Indicator (Type 13). Integer. UMA registration update handling indicator.</summary>
        UMA_RU_HANDLING_INDICATOR = 13,

        /// <summary>Kineto-UMA-Service-Zone-Information (Type 14). String. UMA service zone information.</summary>
        UMA_SERVICE_ZONE_INFORMATION = 14,

        /// <summary>Kineto-UMA-Serving-UNC-Table-Indicator (Type 15). Integer. UMA serving UNC table indicator.</summary>
        UMA_SERVING_UNC_TABLE_INDICATOR = 15,

        /// <summary>Kineto-UMA-Registration-Indicators (Type 16). Integer. UMA registration indicators.</summary>
        UMA_REGISTRATION_INDICATORS = 16,

        /// <summary>Kineto-UMA-UMA-PLMN-List (Type 17). String. UMA PLMN list.</summary>
        UMA_UMA_PLMN_LIST = 17,

        /// <summary>Kineto-UMA-Required-UMA-Services (Type 18). Integer. Required UMA services.</summary>
        UMA_REQUIRED_UMA_SERVICES = 18,

        /// <summary>Kineto-UMA-3G-Cell-Identity (Type 19). Integer. UMA 3G cell identity.</summary>
        UMA_3G_CELL_IDENTITY = 19,

        /// <summary>Kineto-UMA-MS-Radio-Identity (Type 20). Octets. UMA mobile station radio identity.</summary>
        UMA_MS_RADIO_IDENTITY = 20,

        /// <summary>Kineto-UMA-UNC-IP-Address (Type 21). String. UMA UNC IP address.</summary>
        UMA_UNC_IP_ADDRESS = 21,

        /// <summary>Kineto-UMA-UNC-FQDN (Type 22). String. UMA UNC FQDN.</summary>
        UMA_UNC_FQDN = 22
    }

    /// <summary>
    /// Kineto-UMA-Release-Indicator attribute values (Type 1).
    /// </summary>
    public enum KINETO_UMA_RELEASE_INDICATOR
    {
        /// <summary>UMA Release 1.0.0.</summary>
        UMA_R1_0_0 = 0,

        /// <summary>UMA Release 1.0.1.</summary>
        UMA_R1_0_1 = 1,

        /// <summary>UMA Release 1.0.2.</summary>
        UMA_R1_0_2 = 2,

        /// <summary>UMA Release 1.0.3.</summary>
        UMA_R1_0_3 = 3,

        /// <summary>UMA Release 1.0.4.</summary>
        UMA_R1_0_4 = 4
    }

    /// <summary>
    /// Kineto-UMA-Coverage-Indicator attribute values (Type 5).
    /// </summary>
    public enum KINETO_UMA_COVERAGE_INDICATOR
    {
        /// <summary>Normal coverage.</summary>
        NORMAL = 0,

        /// <summary>Limited coverage.</summary>
        LIMITED = 1,

        /// <summary>No coverage.</summary>
        NO_COVERAGE = 2
    }

    /// <summary>
    /// Kineto-UMA-RRC-State attribute values (Type 12).
    /// </summary>
    public enum KINETO_UMA_RRC_STATE
    {
        /// <summary>RRC connected state.</summary>
        RRC_CONNECTED = 0,

        /// <summary>RRC idle state.</summary>
        RRC_IDLE = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Kineto Wireless
    /// (IANA PEN 16445) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.kineto</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Kineto's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 16445</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Kineto Wireless UMA/GAN (Unlicensed Mobile
    /// Access / Generic Access Network) equipment for RADIUS-based mobile-to-WiFi
    /// handoff management, including UMA release identification, access point and
    /// mobile station radio identity, cell and 3G cell identity, location area
    /// and geographical location, coverage indication, security gateway (SGW)
    /// and UNC addressing, RRC state, registration handling, service zone
    /// configuration, PLMN lists, and required UMA services.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(KinetoAttributes.UmaReleaseIndicator(KINETO_UMA_RELEASE_INDICATOR.UMA_R1_0_4));
    /// packet.SetAttribute(KinetoAttributes.UmaSgwFqdn("sgw.example.com"));
    /// packet.SetAttribute(KinetoAttributes.UmaCoverageIndicator(KINETO_UMA_COVERAGE_INDICATOR.NORMAL));
    /// packet.SetAttribute(KinetoAttributes.UmaRrcState(KINETO_UMA_RRC_STATE.RRC_CONNECTED));
    /// </code>
    /// </remarks>
    public static class KinetoAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Kineto Wireless.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 16445;

        #region Integer Attributes

        /// <summary>
        /// Creates a Kineto-UMA-Release-Indicator attribute (Type 1).
        /// </summary>
        /// <param name="value">The UMA release indicator. See <see cref="KINETO_UMA_RELEASE_INDICATOR"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UmaReleaseIndicator(KINETO_UMA_RELEASE_INDICATOR value)
        {
            return CreateInteger(KinetoAttributeType.UMA_RELEASE_INDICATOR, (int)value);
        }

        /// <summary>Creates a Kineto-UMA-Cell-Identity attribute (Type 3).</summary>
        /// <param name="value">The UMA cell identity.</param>
        public static VendorSpecificAttributes UmaCellIdentity(int value)
        {
            return CreateInteger(KinetoAttributeType.UMA_CELL_IDENTITY, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-Coverage-Indicator attribute (Type 5).
        /// </summary>
        /// <param name="value">The UMA coverage indicator. See <see cref="KINETO_UMA_COVERAGE_INDICATOR"/>.</param>
        public static VendorSpecificAttributes UmaCoverageIndicator(KINETO_UMA_COVERAGE_INDICATOR value)
        {
            return CreateInteger(KinetoAttributeType.UMA_COVERAGE_INDICATOR, (int)value);
        }

        /// <summary>Creates a Kineto-UMA-Redirection-Counter attribute (Type 10).</summary>
        /// <param name="value">The UMA redirection counter.</param>
        public static VendorSpecificAttributes UmaRedirectionCounter(int value)
        {
            return CreateInteger(KinetoAttributeType.UMA_REDIRECTION_COUNTER, value);
        }

        /// <summary>Creates a Kineto-UMA-Discovery-Reject-Cause attribute (Type 11).</summary>
        /// <param name="value">The UMA discovery reject cause.</param>
        public static VendorSpecificAttributes UmaDiscoveryRejectCause(int value)
        {
            return CreateInteger(KinetoAttributeType.UMA_DISCOVERY_REJECT_CAUSE, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-RRC-State attribute (Type 12).
        /// </summary>
        /// <param name="value">The UMA RRC state. See <see cref="KINETO_UMA_RRC_STATE"/>.</param>
        public static VendorSpecificAttributes UmaRrcState(KINETO_UMA_RRC_STATE value)
        {
            return CreateInteger(KinetoAttributeType.UMA_RRC_STATE, (int)value);
        }

        /// <summary>Creates a Kineto-UMA-RU-Handling-Indicator attribute (Type 13).</summary>
        /// <param name="value">The UMA registration update handling indicator.</param>
        public static VendorSpecificAttributes UmaRuHandlingIndicator(int value)
        {
            return CreateInteger(KinetoAttributeType.UMA_RU_HANDLING_INDICATOR, value);
        }

        /// <summary>Creates a Kineto-UMA-Serving-UNC-Table-Indicator attribute (Type 15).</summary>
        /// <param name="value">The UMA serving UNC table indicator.</param>
        public static VendorSpecificAttributes UmaServingUncTableIndicator(int value)
        {
            return CreateInteger(KinetoAttributeType.UMA_SERVING_UNC_TABLE_INDICATOR, value);
        }

        /// <summary>Creates a Kineto-UMA-Registration-Indicators attribute (Type 16).</summary>
        /// <param name="value">The UMA registration indicators.</param>
        public static VendorSpecificAttributes UmaRegistrationIndicators(int value)
        {
            return CreateInteger(KinetoAttributeType.UMA_REGISTRATION_INDICATORS, value);
        }

        /// <summary>Creates a Kineto-UMA-Required-UMA-Services attribute (Type 18).</summary>
        /// <param name="value">The required UMA services.</param>
        public static VendorSpecificAttributes UmaRequiredUmaServices(int value)
        {
            return CreateInteger(KinetoAttributeType.UMA_REQUIRED_UMA_SERVICES, value);
        }

        /// <summary>Creates a Kineto-UMA-3G-Cell-Identity attribute (Type 19).</summary>
        /// <param name="value">The UMA 3G cell identity.</param>
        public static VendorSpecificAttributes Uma3gCellIdentity(int value)
        {
            return CreateInteger(KinetoAttributeType.UMA_3G_CELL_IDENTITY, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Kineto-UMA-AP-Radio-Identity attribute (Type 2).
        /// </summary>
        /// <param name="value">The UMA access point radio identity. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaApRadioIdentity(string value)
        {
            return CreateString(KinetoAttributeType.UMA_AP_RADIO_IDENTITY, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-Location-Area-Id attribute (Type 4).
        /// </summary>
        /// <param name="value">The UMA location area identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaLocationAreaId(string value)
        {
            return CreateString(KinetoAttributeType.UMA_LOCATION_AREA_ID, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-SGW-IP-Address attribute (Type 8).
        /// </summary>
        /// <param name="value">The UMA security gateway IP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaSgwIpAddress(string value)
        {
            return CreateString(KinetoAttributeType.UMA_SGW_IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-SGW-FQDN attribute (Type 9).
        /// </summary>
        /// <param name="value">The UMA security gateway FQDN. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaSgwFqdn(string value)
        {
            return CreateString(KinetoAttributeType.UMA_SGW_FQDN, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-Service-Zone-Information attribute (Type 14).
        /// </summary>
        /// <param name="value">The UMA service zone information. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaServiceZoneInformation(string value)
        {
            return CreateString(KinetoAttributeType.UMA_SERVICE_ZONE_INFORMATION, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-UMA-PLMN-List attribute (Type 17).
        /// </summary>
        /// <param name="value">The UMA PLMN list. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaUmaPlmnList(string value)
        {
            return CreateString(KinetoAttributeType.UMA_UMA_PLMN_LIST, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-UNC-IP-Address attribute (Type 21).
        /// </summary>
        /// <param name="value">The UMA UNC IP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaUncIpAddress(string value)
        {
            return CreateString(KinetoAttributeType.UMA_UNC_IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-UNC-FQDN attribute (Type 22).
        /// </summary>
        /// <param name="value">The UMA UNC FQDN. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaUncFqdn(string value)
        {
            return CreateString(KinetoAttributeType.UMA_UNC_FQDN, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates a Kineto-UMA-Classmark attribute (Type 6).
        /// </summary>
        /// <param name="value">The UMA mobile classmark data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaClassmark(byte[] value)
        {
            return CreateOctets(KinetoAttributeType.UMA_CLASSMARK, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-Geographical-Location attribute (Type 7).
        /// </summary>
        /// <param name="value">The UMA geographical location data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaGeographicalLocation(byte[] value)
        {
            return CreateOctets(KinetoAttributeType.UMA_GEOGRAPHICAL_LOCATION, value);
        }

        /// <summary>
        /// Creates a Kineto-UMA-MS-Radio-Identity attribute (Type 20).
        /// </summary>
        /// <param name="value">The UMA mobile station radio identity data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UmaMsRadioIdentity(byte[] value)
        {
            return CreateOctets(KinetoAttributeType.UMA_MS_RADIO_IDENTITY, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(KinetoAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(KinetoAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(KinetoAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        #endregion
    }
}