using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Acme Packet / Oracle SBC (IANA PEN 9148) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.acme</c>.
    /// </summary>
    public enum AcmeAttributeType : byte
    {
        /// <summary>Acme-FlowID-FS1-F (Type 1). String. Flow ID for the first flow set, forward direction.</summary>
        FLOWID_FS1_F = 1,

        /// <summary>Acme-FlowType-FS1-F (Type 2). Integer. Flow type for the first flow set, forward direction.</summary>
        FLOWTYPE_FS1_F = 2,

        /// <summary>Acme-Session-Ingress-CallId (Type 3). String. SIP Call-ID of the ingress leg.</summary>
        SESSION_INGRESS_CALLID = 3,

        /// <summary>Acme-Session-Egress-CallId (Type 4). String. SIP Call-ID of the egress leg.</summary>
        SESSION_EGRESS_CALLID = 4,

        /// <summary>Acme-FlowID-FS1-R (Type 5). String. Flow ID for the first flow set, reverse direction.</summary>
        FLOWID_FS1_R = 5,

        /// <summary>Acme-FlowType-FS1-R (Type 6). Integer. Flow type for the first flow set, reverse direction.</summary>
        FLOWTYPE_FS1_R = 6,

        /// <summary>Acme-FlowID-FS2-F (Type 7). String. Flow ID for the second flow set, forward direction.</summary>
        FLOWID_FS2_F = 7,

        /// <summary>Acme-FlowType-FS2-F (Type 8). Integer. Flow type for the second flow set, forward direction.</summary>
        FLOWTYPE_FS2_F = 8,

        /// <summary>Acme-FlowID-FS2-R (Type 9). String. Flow ID for the second flow set, reverse direction.</summary>
        FLOWID_FS2_R = 9,

        /// <summary>Acme-FlowType-FS2-R (Type 10). Integer. Flow type for the second flow set, reverse direction.</summary>
        FLOWTYPE_FS2_R = 10,

        /// <summary>Acme-Calling-Octets-FS1 (Type 11). Integer. Calling party octets for flow set 1.</summary>
        CALLING_OCTETS_FS1 = 11,

        /// <summary>Acme-Calling-Packets-FS1 (Type 12). Integer. Calling party packets for flow set 1.</summary>
        CALLING_PACKETS_FS1 = 12,

        /// <summary>Acme-Called-Octets-FS1 (Type 13). Integer. Called party octets for flow set 1.</summary>
        CALLED_OCTETS_FS1 = 13,

        /// <summary>Acme-Called-Packets-FS1 (Type 14). Integer. Called party packets for flow set 1.</summary>
        CALLED_PACKETS_FS1 = 14,

        /// <summary>Acme-Calling-Octets-FS2 (Type 15). Integer. Calling party octets for flow set 2.</summary>
        CALLING_OCTETS_FS2 = 15,

        /// <summary>Acme-Calling-Packets-FS2 (Type 16). Integer. Calling party packets for flow set 2.</summary>
        CALLING_PACKETS_FS2 = 16,

        /// <summary>Acme-Called-Octets-FS2 (Type 17). Integer. Called party octets for flow set 2.</summary>
        CALLED_OCTETS_FS2 = 17,

        /// <summary>Acme-Called-Packets-FS2 (Type 18). Integer. Called party packets for flow set 2.</summary>
        CALLED_PACKETS_FS2 = 18,

        /// <summary>Acme-Route-Retries (Type 19). Integer. Number of route retries attempted.</summary>
        ROUTE_RETRIES = 19,

        /// <summary>Acme-Calling-RTCP-Packets-Lost-FS1 (Type 20). Integer. RTCP packets lost from calling party, flow set 1.</summary>
        CALLING_RTCP_PACKETS_LOST_FS1 = 20,

        /// <summary>Acme-Calling-RTCP-Avg-Jitter-FS1 (Type 21). Integer. Average jitter from calling party, flow set 1.</summary>
        CALLING_RTCP_AVG_JITTER_FS1 = 21,

        /// <summary>Acme-Calling-RTCP-Avg-Latency-FS1 (Type 22). Integer. Average latency from calling party, flow set 1.</summary>
        CALLING_RTCP_AVG_LATENCY_FS1 = 22,

        /// <summary>Acme-Calling-RTCP-MaxJitter-FS1 (Type 23). Integer. Maximum jitter from calling party, flow set 1.</summary>
        CALLING_RTCP_MAXJITTER_FS1 = 23,

        /// <summary>Acme-Calling-RTCP-MaxLatency-FS1 (Type 24). Integer. Maximum latency from calling party, flow set 1.</summary>
        CALLING_RTCP_MAXLATENCY_FS1 = 24,

        /// <summary>Acme-Called-RTCP-Packets-Lost-FS1 (Type 25). Integer. RTCP packets lost from called party, flow set 1.</summary>
        CALLED_RTCP_PACKETS_LOST_FS1 = 25,

        /// <summary>Acme-Called-RTCP-Avg-Jitter-FS1 (Type 26). Integer. Average jitter from called party, flow set 1.</summary>
        CALLED_RTCP_AVG_JITTER_FS1 = 26,

        /// <summary>Acme-Called-RTCP-Avg-Latency-FS1 (Type 27). Integer. Average latency from called party, flow set 1.</summary>
        CALLED_RTCP_AVG_LATENCY_FS1 = 27,

        /// <summary>Acme-Called-RTCP-MaxJitter-FS1 (Type 28). Integer. Maximum jitter from called party, flow set 1.</summary>
        CALLED_RTCP_MAXJITTER_FS1 = 28,

        /// <summary>Acme-Called-RTCP-MaxLatency-FS1 (Type 29). Integer. Maximum latency from called party, flow set 1.</summary>
        CALLED_RTCP_MAXLATENCY_FS1 = 29,

        /// <summary>Acme-Calling-RTCP-Packets-Lost-FS2 (Type 30). Integer. RTCP packets lost from calling party, flow set 2.</summary>
        CALLING_RTCP_PACKETS_LOST_FS2 = 30,

        /// <summary>Acme-Calling-RTCP-Avg-Jitter-FS2 (Type 31). Integer. Average jitter from calling party, flow set 2.</summary>
        CALLING_RTCP_AVG_JITTER_FS2 = 31,

        /// <summary>Acme-Calling-RTCP-Avg-Latency-FS2 (Type 32). Integer. Average latency from calling party, flow set 2.</summary>
        CALLING_RTCP_AVG_LATENCY_FS2 = 32,

        /// <summary>Acme-Calling-RTCP-MaxJitter-FS2 (Type 33). Integer. Maximum jitter from calling party, flow set 2.</summary>
        CALLING_RTCP_MAXJITTER_FS2 = 33,

        /// <summary>Acme-Calling-RTCP-MaxLatency-FS2 (Type 34). Integer. Maximum latency from calling party, flow set 2.</summary>
        CALLING_RTCP_MAXLATENCY_FS2 = 34,

        /// <summary>Acme-Called-RTCP-Packets-Lost-FS2 (Type 35). Integer. RTCP packets lost from called party, flow set 2.</summary>
        CALLED_RTCP_PACKETS_LOST_FS2 = 35,

        /// <summary>Acme-Called-RTCP-Avg-Jitter-FS2 (Type 36). Integer. Average jitter from called party, flow set 2.</summary>
        CALLED_RTCP_AVG_JITTER_FS2 = 36,

        /// <summary>Acme-Called-RTCP-Avg-Latency-FS2 (Type 37). Integer. Average latency from called party, flow set 2.</summary>
        CALLED_RTCP_AVG_LATENCY_FS2 = 37,

        /// <summary>Acme-Called-RTCP-MaxJitter-FS2 (Type 38). Integer. Maximum jitter from called party, flow set 2.</summary>
        CALLED_RTCP_MAXJITTER_FS2 = 38,

        /// <summary>Acme-Called-RTCP-MaxLatency-FS2 (Type 39). Integer. Maximum latency from called party, flow set 2.</summary>
        CALLED_RTCP_MAXLATENCY_FS2 = 39,

        /// <summary>Acme-Session-Protocol-Type (Type 40). String. Session protocol type (e.g. SIP, H.323).</summary>
        SESSION_PROTOCOL_TYPE = 40,

        /// <summary>Acme-Session-Forked-CallId (Type 41). String. SIP Call-ID of the forked leg.</summary>
        SESSION_FORKED_CALLID = 41,

        /// <summary>Acme-Session-Generic-Id (Type 42). String. Generic session identifier.</summary>
        SESSION_GENERIC_ID = 42,

        /// <summary>Acme-Session-Ingress-RPH (Type 43). String. Ingress Resource-Priority header value.</summary>
        SESSION_INGRESS_RPH = 43,

        /// <summary>Acme-Session-Egress-RPH (Type 44). String. Egress Resource-Priority header value.</summary>
        SESSION_EGRESS_RPH = 44,

        /// <summary>Acme-Session-Disconnect-Cause (Type 45). Integer. SIP/Q.850 disconnect cause code.</summary>
        SESSION_DISCONNECT_CAUSE = 45,

        /// <summary>Acme-Session-Disconnect-Initiator (Type 46). Integer. Which side initiated the disconnect.</summary>
        SESSION_DISCONNECT_INITIATOR = 46,

        /// <summary>Acme-P-CSCF-Address (Type 47). String. Proxy-CSCF address for IMS.</summary>
        P_CSCF_ADDRESS = 47,

        /// <summary>Acme-Disconnect-Initiator (Type 48). Integer. Disconnect initiator code.</summary>
        DISCONNECT_INITIATOR = 48,

        /// <summary>Acme-Disconnect-Cause (Type 49). Integer. Disconnect cause code.</summary>
        DISCONNECT_CAUSE = 49,

        /// <summary>Acme-SIP-Status (Type 50). Integer. SIP response status code.</summary>
        SIP_STATUS = 50,

        /// <summary>Acme-Ingress-Local-Addr (Type 51). String. Local address on the ingress side.</summary>
        INGRESS_LOCAL_ADDR = 51,

        /// <summary>Acme-Ingress-Remote-Addr (Type 52). String. Remote address on the ingress side.</summary>
        INGRESS_REMOTE_ADDR = 52,

        /// <summary>Acme-Egress-Local-Addr (Type 53). String. Local address on the egress side.</summary>
        EGRESS_LOCAL_ADDR = 53,

        /// <summary>Acme-Egress-Remote-Addr (Type 54). String. Remote address on the egress side.</summary>
        EGRESS_REMOTE_ADDR = 54,

        /// <summary>Acme-Post-Dial-Delay (Type 55). Integer. Post-dial delay in milliseconds.</summary>
        POST_DIAL_DELAY = 55,

        /// <summary>Acme-CDR-Sequence-Number (Type 56). Integer. CDR sequence number.</summary>
        CDR_SEQUENCE_NUMBER = 56,

        /// <summary>Acme-Session-Disposition (Type 57). Integer. Session disposition code.</summary>
        SESSION_DISPOSITION = 57,

        /// <summary>Acme-Session-Ingress-Realm (Type 58). String. Ingress realm name.</summary>
        SESSION_INGRESS_REALM = 58,

        /// <summary>Acme-Session-Egress-Realm (Type 59). String. Egress realm name.</summary>
        SESSION_EGRESS_REALM = 59,

        /// <summary>Acme-Intermediate-Time (Type 60). String. Intermediate accounting timestamp.</summary>
        INTERMEDIATE_TIME = 60,

        /// <summary>Acme-Calling-R-Factor (Type 61). Integer. Calling party R-Factor voice quality score.</summary>
        CALLING_R_FACTOR = 61,

        /// <summary>Acme-Calling-MOS (Type 62). Integer. Calling party Mean Opinion Score (MOS × 10).</summary>
        CALLING_MOS = 62,

        /// <summary>Acme-Called-R-Factor (Type 63). Integer. Called party R-Factor voice quality score.</summary>
        CALLED_R_FACTOR = 63,

        /// <summary>Acme-Called-MOS (Type 64). Integer. Called party Mean Opinion Score (MOS × 10).</summary>
        CALLED_MOS = 64,

        /// <summary>Acme-Session-Ingress-RPH-Namespace (Type 65). String. Ingress RPH namespace.</summary>
        SESSION_INGRESS_RPH_NAMESPACE = 65,

        /// <summary>Acme-Session-Ingress-RPH-Priority (Type 66). String. Ingress RPH priority.</summary>
        SESSION_INGRESS_RPH_PRIORITY = 66,

        /// <summary>Acme-Session-Egress-RPH-Namespace (Type 67). String. Egress RPH namespace.</summary>
        SESSION_EGRESS_RPH_NAMESPACE = 67,

        /// <summary>Acme-Session-Egress-RPH-Priority (Type 68). String. Egress RPH priority.</summary>
        SESSION_EGRESS_RPH_PRIORITY = 68,

        /// <summary>Acme-Ingress-Realm (Type 69). String. Ingress realm.</summary>
        INGRESS_REALM = 69,

        /// <summary>Acme-Egress-Realm (Type 70). String. Egress realm.</summary>
        EGRESS_REALM = 70,

        /// <summary>Acme-Session-Media-Process (Type 71). Integer. Media processing type.</summary>
        SESSION_MEDIA_PROCESS = 71,

        /// <summary>Acme-Firmware-Version (Type 72). String. SBC firmware version string.</summary>
        FIRMWARE_VERSION = 72,

        /// <summary>Acme-Local-Time-Zone (Type 73). String. Local timezone of the SBC.</summary>
        LOCAL_TIME_ZONE = 73,

        /// <summary>Acme-Session-Media-Type (Type 74). String. Media type (e.g. audio, video).</summary>
        SESSION_MEDIA_TYPE = 74,

        /// <summary>Acme-Session-Charging-Vector (Type 75). String. P-Charging-Vector header value.</summary>
        SESSION_CHARGING_VECTOR = 75,

        /// <summary>Acme-Session-Charging-Function-Address (Type 76). String. P-Charging-Function-Addresses header value.</summary>
        SESSION_CHARGING_FUNCTION_ADDRESS = 76,

        /// <summary>Acme-Primary-Routing-Number (Type 78). String. Primary routing number (LRN).</summary>
        PRIMARY_ROUTING_NUMBER = 78,

        /// <summary>Acme-Originating-Trunk-Group (Type 79). String. Originating trunk group label.</summary>
        ORIGINATING_TRUNK_GROUP = 79,

        /// <summary>Acme-Terminating-Trunk-Group (Type 80). String. Terminating trunk group label.</summary>
        TERMINATING_TRUNK_GROUP = 80,

        /// <summary>Acme-Originating-Trunk-Context (Type 81). String. Originating trunk context.</summary>
        ORIGINATING_TRUNK_CONTEXT = 81,

        /// <summary>Acme-Terminating-Trunk-Context (Type 82). String. Terminating trunk context.</summary>
        TERMINATING_TRUNK_CONTEXT = 82,

        /// <summary>Acme-P-Asserted-ID (Type 83). String. P-Asserted-Identity header value.</summary>
        P_ASSERTED_ID = 83,

        /// <summary>Acme-SIP-Diversion (Type 84). String. SIP Diversion header value.</summary>
        SIP_DIVERSION = 84,

        /// <summary>Acme-SIP-Status-Str (Type 85). String. SIP response status as a text string.</summary>
        SIP_STATUS_STR = 85,

        /// <summary>Acme-Flow-Calling-Media-Stop-Time-FS1 (Type 86). String. Flow set 1 calling media stop time.</summary>
        FLOW_CALLING_MEDIA_STOP_TIME_FS1 = 86,

        /// <summary>Acme-Flow-Called-Media-Stop-Time-FS1 (Type 87). String. Flow set 1 called media stop time.</summary>
        FLOW_CALLED_MEDIA_STOP_TIME_FS1 = 87,

        /// <summary>Acme-Flow-Calling-Media-Stop-Time-FS2 (Type 88). String. Flow set 2 calling media stop time.</summary>
        FLOW_CALLING_MEDIA_STOP_TIME_FS2 = 88,

        /// <summary>Acme-Flow-Called-Media-Stop-Time-FS2 (Type 89). String. Flow set 2 called media stop time.</summary>
        FLOW_CALLED_MEDIA_STOP_TIME_FS2 = 89,

        /// <summary>Acme-Session-Overall-Calling-RTP-Packets-Lost (Type 90). Integer. Total RTP packets lost from calling party.</summary>
        SESSION_OVERALL_CALLING_RTP_PACKETS_LOST = 90,

        /// <summary>Acme-Session-Overall-Called-RTP-Packets-Lost (Type 91). Integer. Total RTP packets lost from called party.</summary>
        SESSION_OVERALL_CALLED_RTP_PACKETS_LOST = 91,

        /// <summary>Acme-Session-Near-End-Addr (Type 92). String. Near-end signalling address.</summary>
        SESSION_NEAR_END_ADDR = 92,

        /// <summary>Acme-Session-Far-End-Addr (Type 93). String. Far-end signalling address.</summary>
        SESSION_FAR_END_ADDR = 93,

        /// <summary>Acme-Egress-Final-Routing-Number (Type 94). String. Final routing number on egress.</summary>
        EGRESS_FINAL_ROUTING_NUMBER = 94,

        /// <summary>Acme-Trunk-Group-URI (Type 95). String. Trunk group URI.</summary>
        TRUNK_GROUP_URI = 95,

        /// <summary>Acme-FlowID-FS1-F-CallId (Type 96). String. Flow set 1 forward Call-ID.</summary>
        FLOWID_FS1_F_CALLID = 96,

        /// <summary>Acme-FlowID-FS1-R-CallId (Type 97). String. Flow set 1 reverse Call-ID.</summary>
        FLOWID_FS1_R_CALLID = 97,

        /// <summary>Acme-FlowID-FS2-F-CallId (Type 98). String. Flow set 2 forward Call-ID.</summary>
        FLOWID_FS2_F_CALLID = 98,

        /// <summary>Acme-FlowID-FS2-R-CallId (Type 99). String. Flow set 2 reverse Call-ID.</summary>
        FLOWID_FS2_R_CALLID = 99
    }

    /// <summary>
    /// Acme-FlowType attribute values (Types 2, 6, 8, 10).
    /// </summary>
    public enum ACME_FLOWTYPE
    {
        /// <summary>No media flow.</summary>
        NO_FLOW = 0,

        /// <summary>Audio media flow.</summary>
        AUDIO = 1,

        /// <summary>Video media flow.</summary>
        VIDEO = 2,

        /// <summary>Fax media flow (T.38).</summary>
        FAX = 3,

        /// <summary>Data media flow.</summary>
        DATA = 4
    }

    /// <summary>
    /// Acme-Session-Disconnect-Initiator attribute values (Type 46).
    /// </summary>
    public enum ACME_SESSION_DISCONNECT_INITIATOR
    {
        /// <summary>Disconnect unknown.</summary>
        UNKNOWN = 0,

        /// <summary>Calling party initiated disconnect.</summary>
        CALLING_PARTY = 1,

        /// <summary>Called party initiated disconnect.</summary>
        CALLED_PARTY = 2,

        /// <summary>Internal disconnect.</summary>
        INTERNAL = 3
    }

    /// <summary>
    /// Acme-Disconnect-Initiator attribute values (Type 48).
    /// </summary>
    public enum ACME_DISCONNECT_INITIATOR
    {
        /// <summary>Remote party initiated disconnect.</summary>
        REMOTE = 0,

        /// <summary>Local (SBC) initiated disconnect.</summary>
        LOCAL = 1,

        /// <summary>Registration expired.</summary>
        REGISTRATION_EXPIRED = 2,

        /// <summary>No resources available.</summary>
        NO_RESOURCES = 3
    }

    /// <summary>
    /// Acme-Session-Disposition attribute values (Type 57).
    /// </summary>
    public enum ACME_SESSION_DISPOSITION
    {
        /// <summary>Call was not connected.</summary>
        NOT_CONNECTED = 0,

        /// <summary>Call was connected.</summary>
        CONNECTED = 1
    }

    /// <summary>
    /// Acme-Session-Media-Process attribute values (Type 71).
    /// </summary>
    public enum ACME_SESSION_MEDIA_PROCESS
    {
        /// <summary>Media was not processed (passthrough).</summary>
        NOT_PROCESSED = 0,

        /// <summary>Media was processed by the SBC.</summary>
        PROCESSED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Acme Packet / Oracle SBC
    /// (IANA PEN 9148) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.acme</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Acme Packet's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 9148</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Acme Packet (Oracle) Session Border Controllers for
    /// SIP/VoIP call detail records (CDRs), media quality metrics (RTCP jitter, latency,
    /// R-Factor, MOS), and trunk group accounting.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(AcmeAttributes.SessionIngressCallId("abc123@10.0.0.1"));
    /// packet.SetAttribute(AcmeAttributes.SipStatus(200));
    /// packet.SetAttribute(AcmeAttributes.DisconnectInitiator(ACME_DISCONNECT_INITIATOR.REMOTE));
    /// packet.SetAttribute(AcmeAttributes.CallingMos(43));
    /// </code>
    /// </remarks>
    public static class AcmeAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Acme Packet (Oracle Communications).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 9148;

        #region Integer Attributes

        /// <summary>
        /// Creates an Acme-FlowType-FS1-F attribute (Type 2) with the specified flow type.
        /// </summary>
        /// <param name="value">The flow type. See <see cref="ACME_FLOWTYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FlowTypeFs1F(ACME_FLOWTYPE value)
        {
            return CreateInteger(AcmeAttributeType.FLOWTYPE_FS1_F, (int)value);
        }

        /// <summary>
        /// Creates an Acme-FlowType-FS1-R attribute (Type 6) with the specified flow type.
        /// </summary>
        /// <param name="value">The flow type. See <see cref="ACME_FLOWTYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FlowTypeFs1R(ACME_FLOWTYPE value)
        {
            return CreateInteger(AcmeAttributeType.FLOWTYPE_FS1_R, (int)value);
        }

        /// <summary>
        /// Creates an Acme-FlowType-FS2-F attribute (Type 8) with the specified flow type.
        /// </summary>
        /// <param name="value">The flow type. See <see cref="ACME_FLOWTYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FlowTypeFs2F(ACME_FLOWTYPE value)
        {
            return CreateInteger(AcmeAttributeType.FLOWTYPE_FS2_F, (int)value);
        }

        /// <summary>
        /// Creates an Acme-FlowType-FS2-R attribute (Type 10) with the specified flow type.
        /// </summary>
        /// <param name="value">The flow type. See <see cref="ACME_FLOWTYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FlowTypeFs2R(ACME_FLOWTYPE value)
        {
            return CreateInteger(AcmeAttributeType.FLOWTYPE_FS2_R, (int)value);
        }

        /// <summary>
        /// Creates an Acme-Calling-Octets-FS1 attribute (Type 11) with the specified octet count.
        /// </summary>
        /// <param name="value">The calling party octets for flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingOctetsFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_OCTETS_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-Packets-FS1 attribute (Type 12) with the specified packet count.
        /// </summary>
        /// <param name="value">The calling party packets for flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingPacketsFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_PACKETS_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Called-Octets-FS1 attribute (Type 13) with the specified octet count.
        /// </summary>
        /// <param name="value">The called party octets for flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledOctetsFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_OCTETS_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Called-Packets-FS1 attribute (Type 14) with the specified packet count.
        /// </summary>
        /// <param name="value">The called party packets for flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledPacketsFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_PACKETS_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-Octets-FS2 attribute (Type 15) with the specified octet count.
        /// </summary>
        /// <param name="value">The calling party octets for flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingOctetsFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_OCTETS_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-Packets-FS2 attribute (Type 16) with the specified packet count.
        /// </summary>
        /// <param name="value">The calling party packets for flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingPacketsFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_PACKETS_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Called-Octets-FS2 attribute (Type 17) with the specified octet count.
        /// </summary>
        /// <param name="value">The called party octets for flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledOctetsFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_OCTETS_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Called-Packets-FS2 attribute (Type 18) with the specified packet count.
        /// </summary>
        /// <param name="value">The called party packets for flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledPacketsFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_PACKETS_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Route-Retries attribute (Type 19) with the specified retry count.
        /// </summary>
        /// <param name="value">The number of route retries attempted.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RouteRetries(int value)
        {
            return CreateInteger(AcmeAttributeType.ROUTE_RETRIES, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-RTCP-Packets-Lost-FS1 attribute (Type 20) with the specified count.
        /// </summary>
        /// <param name="value">The RTCP packets lost from calling party, flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRtcpPacketsLostFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_RTCP_PACKETS_LOST_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-RTCP-Avg-Jitter-FS1 attribute (Type 21) with the specified jitter.
        /// </summary>
        /// <param name="value">The average jitter from calling party, flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRtcpAvgJitterFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_RTCP_AVG_JITTER_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-RTCP-Avg-Latency-FS1 attribute (Type 22) with the specified latency.
        /// </summary>
        /// <param name="value">The average latency from calling party, flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRtcpAvgLatencyFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_RTCP_AVG_LATENCY_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-RTCP-MaxJitter-FS1 attribute (Type 23) with the specified jitter.
        /// </summary>
        /// <param name="value">The maximum jitter from calling party, flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRtcpMaxJitterFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_RTCP_MAXJITTER_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-RTCP-MaxLatency-FS1 attribute (Type 24) with the specified latency.
        /// </summary>
        /// <param name="value">The maximum latency from calling party, flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRtcpMaxLatencyFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_RTCP_MAXLATENCY_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Called-RTCP-Packets-Lost-FS1 attribute (Type 25) with the specified count.
        /// </summary>
        /// <param name="value">The RTCP packets lost from called party, flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRtcpPacketsLostFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_RTCP_PACKETS_LOST_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Called-RTCP-Avg-Jitter-FS1 attribute (Type 26) with the specified jitter.
        /// </summary>
        /// <param name="value">The average jitter from called party, flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRtcpAvgJitterFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_RTCP_AVG_JITTER_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Called-RTCP-Avg-Latency-FS1 attribute (Type 27) with the specified latency.
        /// </summary>
        /// <param name="value">The average latency from called party, flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRtcpAvgLatencyFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_RTCP_AVG_LATENCY_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Called-RTCP-MaxJitter-FS1 attribute (Type 28) with the specified jitter.
        /// </summary>
        /// <param name="value">The maximum jitter from called party, flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRtcpMaxJitterFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_RTCP_MAXJITTER_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Called-RTCP-MaxLatency-FS1 attribute (Type 29) with the specified latency.
        /// </summary>
        /// <param name="value">The maximum latency from called party, flow set 1.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRtcpMaxLatencyFs1(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_RTCP_MAXLATENCY_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-RTCP-Packets-Lost-FS2 attribute (Type 30) with the specified count.
        /// </summary>
        /// <param name="value">The RTCP packets lost from calling party, flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRtcpPacketsLostFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_RTCP_PACKETS_LOST_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-RTCP-Avg-Jitter-FS2 attribute (Type 31) with the specified jitter.
        /// </summary>
        /// <param name="value">The average jitter from calling party, flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRtcpAvgJitterFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_RTCP_AVG_JITTER_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-RTCP-Avg-Latency-FS2 attribute (Type 32) with the specified latency.
        /// </summary>
        /// <param name="value">The average latency from calling party, flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRtcpAvgLatencyFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_RTCP_AVG_LATENCY_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-RTCP-MaxJitter-FS2 attribute (Type 33) with the specified jitter.
        /// </summary>
        /// <param name="value">The maximum jitter from calling party, flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRtcpMaxJitterFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_RTCP_MAXJITTER_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-RTCP-MaxLatency-FS2 attribute (Type 34) with the specified latency.
        /// </summary>
        /// <param name="value">The maximum latency from calling party, flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRtcpMaxLatencyFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_RTCP_MAXLATENCY_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Called-RTCP-Packets-Lost-FS2 attribute (Type 35) with the specified count.
        /// </summary>
        /// <param name="value">The RTCP packets lost from called party, flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRtcpPacketsLostFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_RTCP_PACKETS_LOST_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Called-RTCP-Avg-Jitter-FS2 attribute (Type 36) with the specified jitter.
        /// </summary>
        /// <param name="value">The average jitter from called party, flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRtcpAvgJitterFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_RTCP_AVG_JITTER_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Called-RTCP-Avg-Latency-FS2 attribute (Type 37) with the specified latency.
        /// </summary>
        /// <param name="value">The average latency from called party, flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRtcpAvgLatencyFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_RTCP_AVG_LATENCY_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Called-RTCP-MaxJitter-FS2 attribute (Type 38) with the specified jitter.
        /// </summary>
        /// <param name="value">The maximum jitter from called party, flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRtcpMaxJitterFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_RTCP_MAXJITTER_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Called-RTCP-MaxLatency-FS2 attribute (Type 39) with the specified latency.
        /// </summary>
        /// <param name="value">The maximum latency from called party, flow set 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRtcpMaxLatencyFs2(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_RTCP_MAXLATENCY_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Disconnect-Cause attribute (Type 45) with the specified cause code.
        /// </summary>
        /// <param name="value">The SIP/Q.850 disconnect cause code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionDisconnectCause(int value)
        {
            return CreateInteger(AcmeAttributeType.SESSION_DISCONNECT_CAUSE, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Disconnect-Initiator attribute (Type 46) with the specified initiator.
        /// </summary>
        /// <param name="value">The disconnect initiator. See <see cref="ACME_SESSION_DISCONNECT_INITIATOR"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionDisconnectInitiator(ACME_SESSION_DISCONNECT_INITIATOR value)
        {
            return CreateInteger(AcmeAttributeType.SESSION_DISCONNECT_INITIATOR, (int)value);
        }

        /// <summary>
        /// Creates an Acme-Disconnect-Initiator attribute (Type 48) with the specified initiator.
        /// </summary>
        /// <param name="value">The disconnect initiator code. See <see cref="ACME_DISCONNECT_INITIATOR"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DisconnectInitiator(ACME_DISCONNECT_INITIATOR value)
        {
            return CreateInteger(AcmeAttributeType.DISCONNECT_INITIATOR, (int)value);
        }

        /// <summary>
        /// Creates an Acme-Disconnect-Cause attribute (Type 49) with the specified cause code.
        /// </summary>
        /// <param name="value">The disconnect cause code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DisconnectCause(int value)
        {
            return CreateInteger(AcmeAttributeType.DISCONNECT_CAUSE, value);
        }

        /// <summary>
        /// Creates an Acme-SIP-Status attribute (Type 50) with the specified SIP response code.
        /// </summary>
        /// <param name="value">The SIP response status code (e.g. 200, 404, 503).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SipStatus(int value)
        {
            return CreateInteger(AcmeAttributeType.SIP_STATUS, value);
        }

        /// <summary>
        /// Creates an Acme-Post-Dial-Delay attribute (Type 55) with the specified delay.
        /// </summary>
        /// <param name="value">The post-dial delay in milliseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PostDialDelay(int value)
        {
            return CreateInteger(AcmeAttributeType.POST_DIAL_DELAY, value);
        }

        /// <summary>
        /// Creates an Acme-CDR-Sequence-Number attribute (Type 56) with the specified sequence number.
        /// </summary>
        /// <param name="value">The CDR sequence number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CdrSequenceNumber(int value)
        {
            return CreateInteger(AcmeAttributeType.CDR_SEQUENCE_NUMBER, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Disposition attribute (Type 57) with the specified disposition.
        /// </summary>
        /// <param name="value">The session disposition code. See <see cref="ACME_SESSION_DISPOSITION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionDisposition(ACME_SESSION_DISPOSITION value)
        {
            return CreateInteger(AcmeAttributeType.SESSION_DISPOSITION, (int)value);
        }

        /// <summary>
        /// Creates an Acme-Calling-R-Factor attribute (Type 61) with the specified R-Factor score.
        /// </summary>
        /// <param name="value">The calling party R-Factor voice quality score.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingRFactor(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_R_FACTOR, value);
        }

        /// <summary>
        /// Creates an Acme-Calling-MOS attribute (Type 62) with the specified MOS value.
        /// </summary>
        /// <param name="value">The calling party Mean Opinion Score (MOS × 10, e.g. 43 = 4.3).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallingMos(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLING_MOS, value);
        }

        /// <summary>
        /// Creates an Acme-Called-R-Factor attribute (Type 63) with the specified R-Factor score.
        /// </summary>
        /// <param name="value">The called party R-Factor voice quality score.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledRFactor(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_R_FACTOR, value);
        }

        /// <summary>
        /// Creates an Acme-Called-MOS attribute (Type 64) with the specified MOS value.
        /// </summary>
        /// <param name="value">The called party Mean Opinion Score (MOS × 10, e.g. 43 = 4.3).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CalledMos(int value)
        {
            return CreateInteger(AcmeAttributeType.CALLED_MOS, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Media-Process attribute (Type 71) with the specified processing type.
        /// </summary>
        /// <param name="value">The media processing type. See <see cref="ACME_SESSION_MEDIA_PROCESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionMediaProcess(ACME_SESSION_MEDIA_PROCESS value)
        {
            return CreateInteger(AcmeAttributeType.SESSION_MEDIA_PROCESS, (int)value);
        }

        /// <summary>
        /// Creates an Acme-Session-Overall-Calling-RTP-Packets-Lost attribute (Type 90) with the specified count.
        /// </summary>
        /// <param name="value">The total RTP packets lost from calling party.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionOverallCallingRtpPacketsLost(int value)
        {
            return CreateInteger(AcmeAttributeType.SESSION_OVERALL_CALLING_RTP_PACKETS_LOST, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Overall-Called-RTP-Packets-Lost attribute (Type 91) with the specified count.
        /// </summary>
        /// <param name="value">The total RTP packets lost from called party.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionOverallCalledRtpPacketsLost(int value)
        {
            return CreateInteger(AcmeAttributeType.SESSION_OVERALL_CALLED_RTP_PACKETS_LOST, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Acme-FlowID-FS1-F attribute (Type 1) with the specified flow ID.
        /// </summary>
        /// <param name="value">The flow ID for flow set 1, forward direction. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowIdFs1F(string value)
        {
            return CreateString(AcmeAttributeType.FLOWID_FS1_F, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Ingress-CallId attribute (Type 3) with the specified Call-ID.
        /// </summary>
        /// <param name="value">The SIP Call-ID of the ingress leg. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionIngressCallId(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_INGRESS_CALLID, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Egress-CallId attribute (Type 4) with the specified Call-ID.
        /// </summary>
        /// <param name="value">The SIP Call-ID of the egress leg. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionEgressCallId(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_EGRESS_CALLID, value);
        }

        /// <summary>
        /// Creates an Acme-FlowID-FS1-R attribute (Type 5) with the specified flow ID.
        /// </summary>
        /// <param name="value">The flow ID for flow set 1, reverse direction. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowIdFs1R(string value)
        {
            return CreateString(AcmeAttributeType.FLOWID_FS1_R, value);
        }

        /// <summary>
        /// Creates an Acme-FlowID-FS2-F attribute (Type 7) with the specified flow ID.
        /// </summary>
        /// <param name="value">The flow ID for flow set 2, forward direction. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowIdFs2F(string value)
        {
            return CreateString(AcmeAttributeType.FLOWID_FS2_F, value);
        }

        /// <summary>
        /// Creates an Acme-FlowID-FS2-R attribute (Type 9) with the specified flow ID.
        /// </summary>
        /// <param name="value">The flow ID for flow set 2, reverse direction. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowIdFs2R(string value)
        {
            return CreateString(AcmeAttributeType.FLOWID_FS2_R, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Protocol-Type attribute (Type 40) with the specified protocol.
        /// </summary>
        /// <param name="value">The session protocol type (e.g. "SIP", "H.323"). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionProtocolType(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_PROTOCOL_TYPE, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Forked-CallId attribute (Type 41) with the specified Call-ID.
        /// </summary>
        /// <param name="value">The SIP Call-ID of the forked leg. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionForkedCallId(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_FORKED_CALLID, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Generic-Id attribute (Type 42) with the specified identifier.
        /// </summary>
        /// <param name="value">The generic session identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionGenericId(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_GENERIC_ID, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Ingress-RPH attribute (Type 43) with the specified RPH value.
        /// </summary>
        /// <param name="value">The ingress Resource-Priority header value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionIngressRph(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_INGRESS_RPH, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Egress-RPH attribute (Type 44) with the specified RPH value.
        /// </summary>
        /// <param name="value">The egress Resource-Priority header value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionEgressRph(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_EGRESS_RPH, value);
        }

        /// <summary>
        /// Creates an Acme-P-CSCF-Address attribute (Type 47) with the specified address.
        /// </summary>
        /// <param name="value">The Proxy-CSCF address for IMS. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PCscfAddress(string value)
        {
            return CreateString(AcmeAttributeType.P_CSCF_ADDRESS, value);
        }

        /// <summary>
        /// Creates an Acme-Ingress-Local-Addr attribute (Type 51) with the specified address.
        /// </summary>
        /// <param name="value">The local address on the ingress side. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IngressLocalAddr(string value)
        {
            return CreateString(AcmeAttributeType.INGRESS_LOCAL_ADDR, value);
        }

        /// <summary>
        /// Creates an Acme-Ingress-Remote-Addr attribute (Type 52) with the specified address.
        /// </summary>
        /// <param name="value">The remote address on the ingress side. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IngressRemoteAddr(string value)
        {
            return CreateString(AcmeAttributeType.INGRESS_REMOTE_ADDR, value);
        }

        /// <summary>
        /// Creates an Acme-Egress-Local-Addr attribute (Type 53) with the specified address.
        /// </summary>
        /// <param name="value">The local address on the egress side. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EgressLocalAddr(string value)
        {
            return CreateString(AcmeAttributeType.EGRESS_LOCAL_ADDR, value);
        }

        /// <summary>
        /// Creates an Acme-Egress-Remote-Addr attribute (Type 54) with the specified address.
        /// </summary>
        /// <param name="value">The remote address on the egress side. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EgressRemoteAddr(string value)
        {
            return CreateString(AcmeAttributeType.EGRESS_REMOTE_ADDR, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Ingress-Realm attribute (Type 58) with the specified realm.
        /// </summary>
        /// <param name="value">The ingress realm name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionIngressRealm(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_INGRESS_REALM, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Egress-Realm attribute (Type 59) with the specified realm.
        /// </summary>
        /// <param name="value">The egress realm name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionEgressRealm(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_EGRESS_REALM, value);
        }

        /// <summary>
        /// Creates an Acme-Intermediate-Time attribute (Type 60) with the specified timestamp.
        /// </summary>
        /// <param name="value">The intermediate accounting timestamp. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IntermediateTime(string value)
        {
            return CreateString(AcmeAttributeType.INTERMEDIATE_TIME, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Ingress-RPH-Namespace attribute (Type 65) with the specified namespace.
        /// </summary>
        /// <param name="value">The ingress RPH namespace. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionIngressRphNamespace(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_INGRESS_RPH_NAMESPACE, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Ingress-RPH-Priority attribute (Type 66) with the specified priority.
        /// </summary>
        /// <param name="value">The ingress RPH priority. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionIngressRphPriority(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_INGRESS_RPH_PRIORITY, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Egress-RPH-Namespace attribute (Type 67) with the specified namespace.
        /// </summary>
        /// <param name="value">The egress RPH namespace. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionEgressRphNamespace(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_EGRESS_RPH_NAMESPACE, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Egress-RPH-Priority attribute (Type 68) with the specified priority.
        /// </summary>
        /// <param name="value">The egress RPH priority. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionEgressRphPriority(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_EGRESS_RPH_PRIORITY, value);
        }

        /// <summary>
        /// Creates an Acme-Ingress-Realm attribute (Type 69) with the specified realm.
        /// </summary>
        /// <param name="value">The ingress realm. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IngressRealm(string value)
        {
            return CreateString(AcmeAttributeType.INGRESS_REALM, value);
        }

        /// <summary>
        /// Creates an Acme-Egress-Realm attribute (Type 70) with the specified realm.
        /// </summary>
        /// <param name="value">The egress realm. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EgressRealm(string value)
        {
            return CreateString(AcmeAttributeType.EGRESS_REALM, value);
        }

        /// <summary>
        /// Creates an Acme-Firmware-Version attribute (Type 72) with the specified version string.
        /// </summary>
        /// <param name="value">The SBC firmware version string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FirmwareVersion(string value)
        {
            return CreateString(AcmeAttributeType.FIRMWARE_VERSION, value);
        }

        /// <summary>
        /// Creates an Acme-Local-Time-Zone attribute (Type 73) with the specified timezone.
        /// </summary>
        /// <param name="value">The local timezone of the SBC. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LocalTimeZone(string value)
        {
            return CreateString(AcmeAttributeType.LOCAL_TIME_ZONE, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Media-Type attribute (Type 74) with the specified media type.
        /// </summary>
        /// <param name="value">The media type (e.g. "audio", "video"). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionMediaType(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_MEDIA_TYPE, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Charging-Vector attribute (Type 75) with the specified value.
        /// </summary>
        /// <param name="value">The P-Charging-Vector header value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionChargingVector(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_CHARGING_VECTOR, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Charging-Function-Address attribute (Type 76) with the specified value.
        /// </summary>
        /// <param name="value">The P-Charging-Function-Addresses header value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionChargingFunctionAddress(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_CHARGING_FUNCTION_ADDRESS, value);
        }

        /// <summary>
        /// Creates an Acme-Primary-Routing-Number attribute (Type 78) with the specified routing number.
        /// </summary>
        /// <param name="value">The primary routing number (LRN). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrimaryRoutingNumber(string value)
        {
            return CreateString(AcmeAttributeType.PRIMARY_ROUTING_NUMBER, value);
        }

        /// <summary>
        /// Creates an Acme-Originating-Trunk-Group attribute (Type 79) with the specified trunk group.
        /// </summary>
        /// <param name="value">The originating trunk group label. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OriginatingTrunkGroup(string value)
        {
            return CreateString(AcmeAttributeType.ORIGINATING_TRUNK_GROUP, value);
        }

        /// <summary>
        /// Creates an Acme-Terminating-Trunk-Group attribute (Type 80) with the specified trunk group.
        /// </summary>
        /// <param name="value">The terminating trunk group label. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TerminatingTrunkGroup(string value)
        {
            return CreateString(AcmeAttributeType.TERMINATING_TRUNK_GROUP, value);
        }

        /// <summary>
        /// Creates an Acme-Originating-Trunk-Context attribute (Type 81) with the specified context.
        /// </summary>
        /// <param name="value">The originating trunk context. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OriginatingTrunkContext(string value)
        {
            return CreateString(AcmeAttributeType.ORIGINATING_TRUNK_CONTEXT, value);
        }

        /// <summary>
        /// Creates an Acme-Terminating-Trunk-Context attribute (Type 82) with the specified context.
        /// </summary>
        /// <param name="value">The terminating trunk context. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TerminatingTrunkContext(string value)
        {
            return CreateString(AcmeAttributeType.TERMINATING_TRUNK_CONTEXT, value);
        }

        /// <summary>
        /// Creates an Acme-P-Asserted-ID attribute (Type 83) with the specified identity.
        /// </summary>
        /// <param name="value">The P-Asserted-Identity header value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PAssertedId(string value)
        {
            return CreateString(AcmeAttributeType.P_ASSERTED_ID, value);
        }

        /// <summary>
        /// Creates an Acme-SIP-Diversion attribute (Type 84) with the specified diversion value.
        /// </summary>
        /// <param name="value">The SIP Diversion header value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipDiversion(string value)
        {
            return CreateString(AcmeAttributeType.SIP_DIVERSION, value);
        }

        /// <summary>
        /// Creates an Acme-SIP-Status-Str attribute (Type 85) with the specified status string.
        /// </summary>
        /// <param name="value">The SIP response status as a text string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipStatusStr(string value)
        {
            return CreateString(AcmeAttributeType.SIP_STATUS_STR, value);
        }

        /// <summary>
        /// Creates an Acme-Flow-Calling-Media-Stop-Time-FS1 attribute (Type 86) with the specified time.
        /// </summary>
        /// <param name="value">The flow set 1 calling media stop time. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowCallingMediaStopTimeFs1(string value)
        {
            return CreateString(AcmeAttributeType.FLOW_CALLING_MEDIA_STOP_TIME_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Flow-Called-Media-Stop-Time-FS1 attribute (Type 87) with the specified time.
        /// </summary>
        /// <param name="value">The flow set 1 called media stop time. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowCalledMediaStopTimeFs1(string value)
        {
            return CreateString(AcmeAttributeType.FLOW_CALLED_MEDIA_STOP_TIME_FS1, value);
        }

        /// <summary>
        /// Creates an Acme-Flow-Calling-Media-Stop-Time-FS2 attribute (Type 88) with the specified time.
        /// </summary>
        /// <param name="value">The flow set 2 calling media stop time. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowCallingMediaStopTimeFs2(string value)
        {
            return CreateString(AcmeAttributeType.FLOW_CALLING_MEDIA_STOP_TIME_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Flow-Called-Media-Stop-Time-FS2 attribute (Type 89) with the specified time.
        /// </summary>
        /// <param name="value">The flow set 2 called media stop time. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowCalledMediaStopTimeFs2(string value)
        {
            return CreateString(AcmeAttributeType.FLOW_CALLED_MEDIA_STOP_TIME_FS2, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Near-End-Addr attribute (Type 92) with the specified address.
        /// </summary>
        /// <param name="value">The near-end signalling address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionNearEndAddr(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_NEAR_END_ADDR, value);
        }

        /// <summary>
        /// Creates an Acme-Session-Far-End-Addr attribute (Type 93) with the specified address.
        /// </summary>
        /// <param name="value">The far-end signalling address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionFarEndAddr(string value)
        {
            return CreateString(AcmeAttributeType.SESSION_FAR_END_ADDR, value);
        }

        /// <summary>
        /// Creates an Acme-Egress-Final-Routing-Number attribute (Type 94) with the specified number.
        /// </summary>
        /// <param name="value">The final routing number on egress. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EgressFinalRoutingNumber(string value)
        {
            return CreateString(AcmeAttributeType.EGRESS_FINAL_ROUTING_NUMBER, value);
        }

        /// <summary>
        /// Creates an Acme-Trunk-Group-URI attribute (Type 95) with the specified URI.
        /// </summary>
        /// <param name="value">The trunk group URI. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TrunkGroupUri(string value)
        {
            return CreateString(AcmeAttributeType.TRUNK_GROUP_URI, value);
        }

        /// <summary>
        /// Creates an Acme-FlowID-FS1-F-CallId attribute (Type 96) with the specified Call-ID.
        /// </summary>
        /// <param name="value">The flow set 1 forward Call-ID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowIdFs1FCallId(string value)
        {
            return CreateString(AcmeAttributeType.FLOWID_FS1_F_CALLID, value);
        }

        /// <summary>
        /// Creates an Acme-FlowID-FS1-R-CallId attribute (Type 97) with the specified Call-ID.
        /// </summary>
        /// <param name="value">The flow set 1 reverse Call-ID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowIdFs1RCallId(string value)
        {
            return CreateString(AcmeAttributeType.FLOWID_FS1_R_CALLID, value);
        }

        /// <summary>
        /// Creates an Acme-FlowID-FS2-F-CallId attribute (Type 98) with the specified Call-ID.
        /// </summary>
        /// <param name="value">The flow set 2 forward Call-ID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowIdFs2FCallId(string value)
        {
            return CreateString(AcmeAttributeType.FLOWID_FS2_F_CALLID, value);
        }

        /// <summary>
        /// Creates an Acme-FlowID-FS2-R-CallId attribute (Type 99) with the specified Call-ID.
        /// </summary>
        /// <param name="value">The flow set 2 reverse Call-ID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlowIdFs2RCallId(string value)
        {
            return CreateString(AcmeAttributeType.FLOWID_FS2_R_CALLID, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Acme attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AcmeAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Acme attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AcmeAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}