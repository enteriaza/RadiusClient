using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a CableLabs (IANA PEN 4491) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.cablelabs</c>.
    /// </summary>
    public enum CableLabsAttributeType : byte
    {
        /// <summary>CableLabs-Reserved (Type 0). Octets. Reserved.</summary>
        RESERVED = 0,

        /// <summary>CableLabs-Event-Message (Type 1). Octets. Event message data.</summary>
        EVENT_MESSAGE = 1,

        /// <summary>CableLabs-MTA-Endpoint-Name (Type 3). String. MTA endpoint name.</summary>
        MTA_ENDPOINT_NAME = 3,

        /// <summary>CableLabs-Calling-Party-Number (Type 4). String. Calling party number.</summary>
        CALLING_PARTY_NUMBER = 4,

        /// <summary>CableLabs-Called-Party-Number (Type 5). String. Called party number.</summary>
        CALLED_PARTY_NUMBER = 5,

        /// <summary>CableLabs-Database-ID (Type 6). String. Database identifier.</summary>
        DATABASE_ID = 6,

        /// <summary>CableLabs-Query-Type (Type 7). Integer. Query type code.</summary>
        QUERY_TYPE = 7,

        /// <summary>CableLabs-Returned-Number (Type 9). String. Returned number.</summary>
        RETURNED_NUMBER = 9,

        /// <summary>CableLabs-Financial-Entity-ID (Type 10). String. Financial entity identifier.</summary>
        FINANCIAL_ENTITY_ID = 10,

        /// <summary>CableLabs-Flow-Direction (Type 11). Integer. Flow direction.</summary>
        FLOW_DIRECTION = 11,

        /// <summary>CableLabs-Signal-Type (Type 12). Integer. Signal type code.</summary>
        SIGNAL_TYPE = 12,

        /// <summary>CableLabs-Alerting-Signal (Type 13). Integer. Alerting signal code.</summary>
        ALERTING_SIGNAL = 13,

        /// <summary>CableLabs-Subject-Audited (Type 14). Integer. Subject audited flag.</summary>
        SUBJECT_AUDITED = 14,

        /// <summary>CableLabs-Lossless-Switchover (Type 15). Integer. Lossless switchover flag.</summary>
        LOSSLESS_SWITCHOVER = 15,

        /// <summary>CableLabs-SIP-URL (Type 16). String. SIP URL.</summary>
        SIP_URL = 16,

        /// <summary>CableLabs-MTA-UDP-Portnum (Type 17). Integer. MTA UDP port number.</summary>
        MTA_UDP_PORTNUM = 17,

        /// <summary>CableLabs-QoS-Descriptor (Type 18). Octets. QoS descriptor data.</summary>
        QOS_DESCRIPTOR = 18,

        /// <summary>CableLabs-Direction-Indicator (Type 19). Integer. Direction indicator.</summary>
        DIRECTION_INDICATOR = 19,

        /// <summary>CableLabs-Time-Adjustment (Type 20). Octets. Time adjustment data.</summary>
        TIME_ADJUSTMENT = 20,

        /// <summary>CableLabs-SDP-Upstream (Type 21). String. SDP upstream parameters.</summary>
        SDP_UPSTREAM = 21,

        /// <summary>CableLabs-SDP-Downstream (Type 22). String. SDP downstream parameters.</summary>
        SDP_DOWNSTREAM = 22,

        /// <summary>CableLabs-Source-Context (Type 23). String. Source context.</summary>
        SOURCE_CONTEXT = 23,

        /// <summary>CableLabs-Destination-Context (Type 24). String. Destination context.</summary>
        DESTINATION_CONTEXT = 24,

        /// <summary>CableLabs-MTA-Host (Type 25). String. MTA host name.</summary>
        MTA_HOST = 25,

        /// <summary>CableLabs-Acc-Status-Type (Type 26). Integer. Accounting status type.</summary>
        ACC_STATUS_TYPE = 26,

        /// <summary>CableLabs-Redirected-From-Info (Type 27). String. Redirected from information.</summary>
        REDIRECTED_FROM_INFO = 27,

        /// <summary>CableLabs-Electronic-Surveillance-Indication (Type 28). Octets. Electronic surveillance indication.</summary>
        ELECTRONIC_SURVEILLANCE_INDICATION = 28,

        /// <summary>CableLabs-Redirected-From-Party (Type 29). String. Redirected from party.</summary>
        REDIRECTED_FROM_PARTY = 29,

        /// <summary>CableLabs-Redirected-To-Party (Type 30). String. Redirected to party.</summary>
        REDIRECTED_TO_PARTY = 30,

        /// <summary>CableLabs-Electronic-Surveillance-DF-Security (Type 31). Octets. DF security data.</summary>
        ELECTRONIC_SURVEILLANCE_DF_SECURITY = 31,

        /// <summary>CableLabs-CCC-ID (Type 32). Octets. CCC identifier.</summary>
        CCC_ID = 32,

        /// <summary>CableLabs-Financial-Entity-Addr (Type 33). String. Financial entity address.</summary>
        FINANCIAL_ENTITY_ADDR = 33,

        /// <summary>CableLabs-Flow-Direction-2 (Type 34). Integer. Flow direction (secondary).</summary>
        FLOW_DIRECTION_2 = 34,

        /// <summary>CableLabs-Signal-Type-2 (Type 35). Integer. Signal type (secondary).</summary>
        SIGNAL_TYPE_2 = 35,

        /// <summary>CableLabs-Alerting-Signal-2 (Type 36). Integer. Alerting signal (secondary).</summary>
        ALERTING_SIGNAL_2 = 36,

        /// <summary>CableLabs-MTA-FQDN (Type 37). String. MTA fully qualified domain name.</summary>
        MTA_FQDN = 37,

        /// <summary>CableLabs-Charge-Number (Type 38). String. Charge number.</summary>
        CHARGE_NUMBER = 38,

        /// <summary>CableLabs-Forwarded-Number (Type 39). String. Forwarded number.</summary>
        FORWARDED_NUMBER = 39,

        /// <summary>CableLabs-Service-Name (Type 40). String. Service name.</summary>
        SERVICE_NAME = 40,

        /// <summary>CableLabs-Intl-Code (Type 41). String. International code.</summary>
        INTL_CODE = 41,

        /// <summary>CableLabs-Dial-Around-Code (Type 42). String. Dial-around code.</summary>
        DIAL_AROUND_CODE = 42,

        /// <summary>CableLabs-Location-Type (Type 43). Integer. Location type code.</summary>
        LOCATION_TYPE = 43,

        /// <summary>CableLabs-Routing-Number (Type 44). String. Routing number.</summary>
        ROUTING_NUMBER = 44,

        /// <summary>CableLabs-Trunk-Group-ID (Type 45). Octets. Trunk group identifier.</summary>
        TRUNK_GROUP_ID = 45,

        /// <summary>CableLabs-Pilot-Number (Type 46). String. Pilot number.</summary>
        PILOT_NUMBER = 46,

        /// <summary>CableLabs-Charge-Number-2 (Type 47). String. Charge number (secondary).</summary>
        CHARGE_NUMBER_2 = 47,

        /// <summary>CableLabs-Forwarded-Number-2 (Type 48). String. Forwarded number (secondary).</summary>
        FORWARDED_NUMBER_2 = 48,

        /// <summary>CableLabs-Service-Name-2 (Type 49). String. Service name (secondary).</summary>
        SERVICE_NAME_2 = 49,

        /// <summary>CableLabs-Intl-Code-2 (Type 50). String. International code (secondary).</summary>
        INTL_CODE_2 = 50,

        /// <summary>CableLabs-Dial-Around-Code-2 (Type 51). String. Dial-around code (secondary).</summary>
        DIAL_AROUND_CODE_2 = 51,

        /// <summary>CableLabs-Location-Type-2 (Type 52). Integer. Location type (secondary).</summary>
        LOCATION_TYPE_2 = 52,

        /// <summary>CableLabs-Routing-Number-2 (Type 53). String. Routing number (secondary).</summary>
        ROUTING_NUMBER_2 = 53,

        /// <summary>CableLabs-Trunk-Group-ID-2 (Type 54). Octets. Trunk group ID (secondary).</summary>
        TRUNK_GROUP_ID_2 = 54,

        /// <summary>CableLabs-Pilot-Number-2 (Type 55). String. Pilot number (secondary).</summary>
        PILOT_NUMBER_2 = 55,

        /// <summary>CableLabs-Related-Call-Billing-Correlation-ID (Type 56). Octets. Related call billing correlation ID.</summary>
        RELATED_CALL_BILLING_CORRELATION_ID = 56,

        /// <summary>CableLabs-Related-Call-Direction (Type 57). Integer. Related call direction.</summary>
        RELATED_CALL_DIRECTION = 57
    }

    /// <summary>
    /// CableLabs-Flow-Direction attribute values (Types 11, 34).
    /// </summary>
    public enum CABLELABS_FLOW_DIRECTION
    {
        /// <summary>Upstream flow.</summary>
        UPSTREAM = 0,

        /// <summary>Downstream flow.</summary>
        DOWNSTREAM = 1
    }

    /// <summary>
    /// CableLabs-Direction-Indicator attribute values (Type 19).
    /// </summary>
    public enum CABLELABS_DIRECTION_INDICATOR
    {
        /// <summary>Undefined direction.</summary>
        UNDEFINED = 0,

        /// <summary>Originating direction.</summary>
        ORIGINATING = 1,

        /// <summary>Terminating direction.</summary>
        TERMINATING = 2
    }

    /// <summary>
    /// CableLabs-Related-Call-Direction attribute values (Type 57).
    /// </summary>
    public enum CABLELABS_RELATED_CALL_DIRECTION
    {
        /// <summary>Upstream related call.</summary>
        UPSTREAM = 0,

        /// <summary>Downstream related call.</summary>
        DOWNSTREAM = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing CableLabs
    /// (IANA PEN 4491) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.cablelabs</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// CableLabs' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 4491</c>.
    /// </para>
    /// <para>
    /// These attributes are used by DOCSIS cable modem termination systems (CMTS),
    /// PacketCable multimedia terminal adapters (MTA), and related platforms for
    /// event messaging, QoS provisioning, call detail records (CDR), SIP/SDP
    /// session data, electronic surveillance, billing correlation, trunk group
    /// identification, and call routing/redirection.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CableLabsAttributes.MtaEndpointName("aaln/1@mta.example.com"));
    /// packet.SetAttribute(CableLabsAttributes.CallingPartyNumber("+15551234567"));
    /// packet.SetAttribute(CableLabsAttributes.CalledPartyNumber("+15559876543"));
    /// packet.SetAttribute(CableLabsAttributes.FlowDirection(CABLELABS_FLOW_DIRECTION.UPSTREAM));
    /// </code>
    /// </remarks>
    public static class CableLabsAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for CableLabs.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 4491;

        #region Integer Attributes

        /// <summary>Creates a CableLabs-Query-Type attribute (Type 7).</summary>
        public static VendorSpecificAttributes QueryType(int value) => CreateInteger(CableLabsAttributeType.QUERY_TYPE, value);

        /// <summary>
        /// Creates a CableLabs-Flow-Direction attribute (Type 11).
        /// </summary>
        /// <param name="value">The flow direction. See <see cref="CABLELABS_FLOW_DIRECTION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FlowDirection(CABLELABS_FLOW_DIRECTION value) => CreateInteger(CableLabsAttributeType.FLOW_DIRECTION, (int)value);

        /// <summary>Creates a CableLabs-Signal-Type attribute (Type 12).</summary>
        public static VendorSpecificAttributes SignalType(int value) => CreateInteger(CableLabsAttributeType.SIGNAL_TYPE, value);

        /// <summary>Creates a CableLabs-Alerting-Signal attribute (Type 13).</summary>
        public static VendorSpecificAttributes AlertingSignal(int value) => CreateInteger(CableLabsAttributeType.ALERTING_SIGNAL, value);

        /// <summary>Creates a CableLabs-Subject-Audited attribute (Type 14).</summary>
        public static VendorSpecificAttributes SubjectAudited(int value) => CreateInteger(CableLabsAttributeType.SUBJECT_AUDITED, value);

        /// <summary>Creates a CableLabs-Lossless-Switchover attribute (Type 15).</summary>
        public static VendorSpecificAttributes LosslessSwitchover(int value) => CreateInteger(CableLabsAttributeType.LOSSLESS_SWITCHOVER, value);

        /// <summary>Creates a CableLabs-MTA-UDP-Portnum attribute (Type 17).</summary>
        /// <param name="value">The MTA UDP port number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MtaUdpPortnum(int value) => CreateInteger(CableLabsAttributeType.MTA_UDP_PORTNUM, value);

        /// <summary>
        /// Creates a CableLabs-Direction-Indicator attribute (Type 19).
        /// </summary>
        /// <param name="value">The direction indicator. See <see cref="CABLELABS_DIRECTION_INDICATOR"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DirectionIndicator(CABLELABS_DIRECTION_INDICATOR value) => CreateInteger(CableLabsAttributeType.DIRECTION_INDICATOR, (int)value);

        /// <summary>Creates a CableLabs-Acc-Status-Type attribute (Type 26).</summary>
        public static VendorSpecificAttributes AccStatusType(int value) => CreateInteger(CableLabsAttributeType.ACC_STATUS_TYPE, value);

        /// <summary>
        /// Creates a CableLabs-Flow-Direction-2 attribute (Type 34).
        /// </summary>
        /// <param name="value">The flow direction (secondary). See <see cref="CABLELABS_FLOW_DIRECTION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FlowDirection2(CABLELABS_FLOW_DIRECTION value) => CreateInteger(CableLabsAttributeType.FLOW_DIRECTION_2, (int)value);

        /// <summary>Creates a CableLabs-Signal-Type-2 attribute (Type 35).</summary>
        public static VendorSpecificAttributes SignalType2(int value) => CreateInteger(CableLabsAttributeType.SIGNAL_TYPE_2, value);

        /// <summary>Creates a CableLabs-Alerting-Signal-2 attribute (Type 36).</summary>
        public static VendorSpecificAttributes AlertingSignal2(int value) => CreateInteger(CableLabsAttributeType.ALERTING_SIGNAL_2, value);

        /// <summary>Creates a CableLabs-Location-Type attribute (Type 43).</summary>
        public static VendorSpecificAttributes LocationType(int value) => CreateInteger(CableLabsAttributeType.LOCATION_TYPE, value);

        /// <summary>Creates a CableLabs-Location-Type-2 attribute (Type 52).</summary>
        public static VendorSpecificAttributes LocationType2(int value) => CreateInteger(CableLabsAttributeType.LOCATION_TYPE_2, value);

        /// <summary>
        /// Creates a CableLabs-Related-Call-Direction attribute (Type 57).
        /// </summary>
        /// <param name="value">The related call direction. See <see cref="CABLELABS_RELATED_CALL_DIRECTION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RelatedCallDirection(CABLELABS_RELATED_CALL_DIRECTION value) => CreateInteger(CableLabsAttributeType.RELATED_CALL_DIRECTION, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates a CableLabs-MTA-Endpoint-Name attribute (Type 3).</summary>
        /// <param name="value">The MTA endpoint name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MtaEndpointName(string value) => CreateString(CableLabsAttributeType.MTA_ENDPOINT_NAME, value);

        /// <summary>Creates a CableLabs-Calling-Party-Number attribute (Type 4).</summary>
        /// <param name="value">The calling party number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingPartyNumber(string value) => CreateString(CableLabsAttributeType.CALLING_PARTY_NUMBER, value);

        /// <summary>Creates a CableLabs-Called-Party-Number attribute (Type 5).</summary>
        /// <param name="value">The called party number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledPartyNumber(string value) => CreateString(CableLabsAttributeType.CALLED_PARTY_NUMBER, value);

        /// <summary>Creates a CableLabs-Database-ID attribute (Type 6).</summary>
        /// <param name="value">The database identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DatabaseId(string value) => CreateString(CableLabsAttributeType.DATABASE_ID, value);

        /// <summary>Creates a CableLabs-Returned-Number attribute (Type 9).</summary>
        /// <param name="value">The returned number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ReturnedNumber(string value) => CreateString(CableLabsAttributeType.RETURNED_NUMBER, value);

        /// <summary>Creates a CableLabs-Financial-Entity-ID attribute (Type 10).</summary>
        /// <param name="value">The financial entity identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FinancialEntityId(string value) => CreateString(CableLabsAttributeType.FINANCIAL_ENTITY_ID, value);

        /// <summary>Creates a CableLabs-SIP-URL attribute (Type 16).</summary>
        /// <param name="value">The SIP URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipUrl(string value) => CreateString(CableLabsAttributeType.SIP_URL, value);

        /// <summary>Creates a CableLabs-SDP-Upstream attribute (Type 21).</summary>
        /// <param name="value">The SDP upstream parameters. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SdpUpstream(string value) => CreateString(CableLabsAttributeType.SDP_UPSTREAM, value);

        /// <summary>Creates a CableLabs-SDP-Downstream attribute (Type 22).</summary>
        /// <param name="value">The SDP downstream parameters. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SdpDownstream(string value) => CreateString(CableLabsAttributeType.SDP_DOWNSTREAM, value);

        /// <summary>Creates a CableLabs-Source-Context attribute (Type 23).</summary>
        /// <param name="value">The source context. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SourceContext(string value) => CreateString(CableLabsAttributeType.SOURCE_CONTEXT, value);

        /// <summary>Creates a CableLabs-Destination-Context attribute (Type 24).</summary>
        /// <param name="value">The destination context. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DestinationContext(string value) => CreateString(CableLabsAttributeType.DESTINATION_CONTEXT, value);

        /// <summary>Creates a CableLabs-MTA-Host attribute (Type 25).</summary>
        /// <param name="value">The MTA host name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MtaHost(string value) => CreateString(CableLabsAttributeType.MTA_HOST, value);

        /// <summary>Creates a CableLabs-Redirected-From-Info attribute (Type 27).</summary>
        /// <param name="value">The redirected-from information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectedFromInfo(string value) => CreateString(CableLabsAttributeType.REDIRECTED_FROM_INFO, value);

        /// <summary>Creates a CableLabs-Redirected-From-Party attribute (Type 29).</summary>
        /// <param name="value">The redirected-from party. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectedFromParty(string value) => CreateString(CableLabsAttributeType.REDIRECTED_FROM_PARTY, value);

        /// <summary>Creates a CableLabs-Redirected-To-Party attribute (Type 30).</summary>
        /// <param name="value">The redirected-to party. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectedToParty(string value) => CreateString(CableLabsAttributeType.REDIRECTED_TO_PARTY, value);

        /// <summary>Creates a CableLabs-Financial-Entity-Addr attribute (Type 33).</summary>
        /// <param name="value">The financial entity address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FinancialEntityAddr(string value) => CreateString(CableLabsAttributeType.FINANCIAL_ENTITY_ADDR, value);

        /// <summary>Creates a CableLabs-MTA-FQDN attribute (Type 37).</summary>
        /// <param name="value">The MTA FQDN. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MtaFqdn(string value) => CreateString(CableLabsAttributeType.MTA_FQDN, value);

        /// <summary>Creates a CableLabs-Charge-Number attribute (Type 38).</summary>
        /// <param name="value">The charge number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChargeNumber(string value) => CreateString(CableLabsAttributeType.CHARGE_NUMBER, value);

        /// <summary>Creates a CableLabs-Forwarded-Number attribute (Type 39).</summary>
        /// <param name="value">The forwarded number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ForwardedNumber(string value) => CreateString(CableLabsAttributeType.FORWARDED_NUMBER, value);

        /// <summary>Creates a CableLabs-Service-Name attribute (Type 40).</summary>
        /// <param name="value">The service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceName(string value) => CreateString(CableLabsAttributeType.SERVICE_NAME, value);

        /// <summary>Creates a CableLabs-Intl-Code attribute (Type 41).</summary>
        /// <param name="value">The international code. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IntlCode(string value) => CreateString(CableLabsAttributeType.INTL_CODE, value);

        /// <summary>Creates a CableLabs-Dial-Around-Code attribute (Type 42).</summary>
        /// <param name="value">The dial-around code. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DialAroundCode(string value) => CreateString(CableLabsAttributeType.DIAL_AROUND_CODE, value);

        /// <summary>Creates a CableLabs-Routing-Number attribute (Type 44).</summary>
        /// <param name="value">The routing number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RoutingNumber(string value) => CreateString(CableLabsAttributeType.ROUTING_NUMBER, value);

        /// <summary>Creates a CableLabs-Pilot-Number attribute (Type 46).</summary>
        /// <param name="value">The pilot number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PilotNumber(string value) => CreateString(CableLabsAttributeType.PILOT_NUMBER, value);

        /// <summary>Creates a CableLabs-Charge-Number-2 attribute (Type 47).</summary>
        /// <param name="value">The charge number (secondary). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChargeNumber2(string value) => CreateString(CableLabsAttributeType.CHARGE_NUMBER_2, value);

        /// <summary>Creates a CableLabs-Forwarded-Number-2 attribute (Type 48).</summary>
        /// <param name="value">The forwarded number (secondary). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ForwardedNumber2(string value) => CreateString(CableLabsAttributeType.FORWARDED_NUMBER_2, value);

        /// <summary>Creates a CableLabs-Service-Name-2 attribute (Type 49).</summary>
        /// <param name="value">The service name (secondary). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceName2(string value) => CreateString(CableLabsAttributeType.SERVICE_NAME_2, value);

        /// <summary>Creates a CableLabs-Intl-Code-2 attribute (Type 50).</summary>
        /// <param name="value">The international code (secondary). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IntlCode2(string value) => CreateString(CableLabsAttributeType.INTL_CODE_2, value);

        /// <summary>Creates a CableLabs-Dial-Around-Code-2 attribute (Type 51).</summary>
        /// <param name="value">The dial-around code (secondary). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DialAroundCode2(string value) => CreateString(CableLabsAttributeType.DIAL_AROUND_CODE_2, value);

        /// <summary>Creates a CableLabs-Routing-Number-2 attribute (Type 53).</summary>
        /// <param name="value">The routing number (secondary). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RoutingNumber2(string value) => CreateString(CableLabsAttributeType.ROUTING_NUMBER_2, value);

        /// <summary>Creates a CableLabs-Pilot-Number-2 attribute (Type 55).</summary>
        /// <param name="value">The pilot number (secondary). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PilotNumber2(string value) => CreateString(CableLabsAttributeType.PILOT_NUMBER_2, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates a CableLabs-Reserved attribute (Type 0).</summary>
        /// <param name="value">The reserved data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Reserved(byte[] value) => CreateOctets(CableLabsAttributeType.RESERVED, value);

        /// <summary>Creates a CableLabs-Event-Message attribute (Type 1).</summary>
        /// <param name="value">The event message data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EventMessage(byte[] value) => CreateOctets(CableLabsAttributeType.EVENT_MESSAGE, value);

        /// <summary>Creates a CableLabs-QoS-Descriptor attribute (Type 18).</summary>
        /// <param name="value">The QoS descriptor data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosDescriptor(byte[] value) => CreateOctets(CableLabsAttributeType.QOS_DESCRIPTOR, value);

        /// <summary>Creates a CableLabs-Time-Adjustment attribute (Type 20).</summary>
        /// <param name="value">The time adjustment data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TimeAdjustment(byte[] value) => CreateOctets(CableLabsAttributeType.TIME_ADJUSTMENT, value);

        /// <summary>Creates a CableLabs-Electronic-Surveillance-Indication attribute (Type 28).</summary>
        /// <param name="value">The electronic surveillance indication data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ElectronicSurveillanceIndication(byte[] value) => CreateOctets(CableLabsAttributeType.ELECTRONIC_SURVEILLANCE_INDICATION, value);

        /// <summary>Creates a CableLabs-Electronic-Surveillance-DF-Security attribute (Type 31).</summary>
        /// <param name="value">The DF security data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ElectronicSurveillanceDfSecurity(byte[] value) => CreateOctets(CableLabsAttributeType.ELECTRONIC_SURVEILLANCE_DF_SECURITY, value);

        /// <summary>Creates a CableLabs-CCC-ID attribute (Type 32).</summary>
        /// <param name="value">The CCC identifier data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CccId(byte[] value) => CreateOctets(CableLabsAttributeType.CCC_ID, value);

        /// <summary>Creates a CableLabs-Trunk-Group-ID attribute (Type 45).</summary>
        /// <param name="value">The trunk group identifier data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TrunkGroupId(byte[] value) => CreateOctets(CableLabsAttributeType.TRUNK_GROUP_ID, value);

        /// <summary>Creates a CableLabs-Trunk-Group-ID-2 attribute (Type 54).</summary>
        /// <param name="value">The trunk group ID (secondary) data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TrunkGroupId2(byte[] value) => CreateOctets(CableLabsAttributeType.TRUNK_GROUP_ID_2, value);

        /// <summary>Creates a CableLabs-Related-Call-Billing-Correlation-ID attribute (Type 56).</summary>
        /// <param name="value">The related call billing correlation ID data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RelatedCallBillingCorrelationId(byte[] value) => CreateOctets(CableLabsAttributeType.RELATED_CALL_BILLING_CORRELATION_ID, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(CableLabsAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(CableLabsAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(CableLabsAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        #endregion
    }
}