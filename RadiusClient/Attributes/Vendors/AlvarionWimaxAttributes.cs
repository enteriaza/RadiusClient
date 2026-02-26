using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Alvarion WiMAX v2.2 (IANA PEN 12394) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.alvarion.wimax.v2_2</c>.
    /// </summary>
    /// <remarks>
    /// These attribute types are defined under the same vendor ID (12394) as the
    /// base <c>dictionary.alvarion</c>, but represent a separate set of attributes
    /// specific to Alvarion WiMAX v2.2 deployments.
    /// </remarks>
    public enum AlvarionWimaxAttributeType : byte
    {
        /// <summary>Alvarion-WiMAX-R3-IF-ID (Type 1). Integer. R3 interface identifier.</summary>
        R3_IF_ID = 1,

        /// <summary>Alvarion-WiMAX-R3-IF-Name (Type 2). String. R3 interface name.</summary>
        R3_IF_NAME = 2,

        /// <summary>Alvarion-WiMAX-PDFID (Type 3). Integer. Packet Data Flow identifier.</summary>
        PDFID = 3,

        /// <summary>Alvarion-WiMAX-SDFID (Type 4). Integer. Service Data Flow identifier.</summary>
        SDFID = 4,

        /// <summary>Alvarion-WiMAX-Packet-Flow-Descriptor (Type 11). Octets. Packet flow descriptor.</summary>
        PACKET_FLOW_DESCRIPTOR = 11,

        /// <summary>Alvarion-WiMAX-QoS-Descriptor (Type 12). Octets. QoS descriptor.</summary>
        QOS_DESCRIPTOR = 12,

        /// <summary>Alvarion-WiMAX-UL-QoS-Descriptor (Type 13). Octets. Uplink QoS descriptor.</summary>
        UL_QOS_DESCRIPTOR = 13,

        /// <summary>Alvarion-WiMAX-DL-QoS-Descriptor (Type 14). Octets. Downlink QoS descriptor.</summary>
        DL_QOS_DESCRIPTOR = 14,

        /// <summary>Alvarion-WiMAX-UL-Scheduler-Type (Type 15). Integer. Uplink scheduler type.</summary>
        UL_SCHEDULER_TYPE = 15,

        /// <summary>Alvarion-WiMAX-DL-Scheduler-Type (Type 16). Integer. Downlink scheduler type.</summary>
        DL_SCHEDULER_TYPE = 16,

        /// <summary>Alvarion-WiMAX-Service-Flow-Type (Type 17). Integer. Service flow type.</summary>
        SERVICE_FLOW_TYPE = 17,

        /// <summary>Alvarion-WiMAX-Max-Sustained-Traffic-Rate (Type 18). Integer. Maximum sustained traffic rate in bps.</summary>
        MAX_SUSTAINED_TRAFFIC_RATE = 18,

        /// <summary>Alvarion-WiMAX-Max-Traffic-Burst (Type 19). Integer. Maximum traffic burst in bytes.</summary>
        MAX_TRAFFIC_BURST = 19,

        /// <summary>Alvarion-WiMAX-Min-Reserved-Traffic-Rate (Type 20). Integer. Minimum reserved traffic rate in bps.</summary>
        MIN_RESERVED_TRAFFIC_RATE = 20,

        /// <summary>Alvarion-WiMAX-Min-Tolerable-Traffic-Rate (Type 21). Integer. Minimum tolerable traffic rate in bps.</summary>
        MIN_TOLERABLE_TRAFFIC_RATE = 21,

        /// <summary>Alvarion-WiMAX-Bandwidth-Request (Type 22). Integer. Bandwidth request in bps.</summary>
        BANDWIDTH_REQUEST = 22,

        /// <summary>Alvarion-WiMAX-Tolerated-Jitter (Type 23). Integer. Tolerated jitter in milliseconds.</summary>
        TOLERATED_JITTER = 23,

        /// <summary>Alvarion-WiMAX-Max-Latency (Type 24). Integer. Maximum latency in milliseconds.</summary>
        MAX_LATENCY = 24,

        /// <summary>Alvarion-WiMAX-Reduced-Resources-Code (Type 25). Integer. Reduced resources code.</summary>
        REDUCED_RESOURCES_CODE = 25,

        /// <summary>Alvarion-WiMAX-Unsolicited-Grant-Interval (Type 26). Integer. Unsolicited grant interval in milliseconds.</summary>
        UNSOLICITED_GRANT_INTERVAL = 26,

        /// <summary>Alvarion-WiMAX-Unsolicited-Polling-Interval (Type 27). Integer. Unsolicited polling interval in milliseconds.</summary>
        UNSOLICITED_POLLING_INTERVAL = 27,

        /// <summary>Alvarion-WiMAX-PDU-Size (Type 28). Integer. PDU size in bytes.</summary>
        PDU_SIZE = 28,

        /// <summary>Alvarion-WiMAX-SDU-Size (Type 29). Integer. SDU size in bytes.</summary>
        SDU_SIZE = 29,

        /// <summary>Alvarion-WiMAX-Target-SAID (Type 30). Integer. Target Security Association ID.</summary>
        TARGET_SAID = 30,

        /// <summary>Alvarion-WiMAX-ARQ-Enable (Type 31). Integer. ARQ enabled.</summary>
        ARQ_ENABLE = 31,

        /// <summary>Alvarion-WiMAX-ARQ-Window-Size (Type 32). Integer. ARQ window size.</summary>
        ARQ_WINDOW_SIZE = 32,

        /// <summary>Alvarion-WiMAX-ARQ-Transmitter-Delay (Type 33). Integer. ARQ transmitter delay in microseconds.</summary>
        ARQ_TRANSMITTER_DELAY = 33,

        /// <summary>Alvarion-WiMAX-ARQ-Receiver-Delay (Type 34). Integer. ARQ receiver delay in microseconds.</summary>
        ARQ_RECEIVER_DELAY = 34,

        /// <summary>Alvarion-WiMAX-ARQ-Block-Lifetime (Type 35). Integer. ARQ block lifetime in microseconds.</summary>
        ARQ_BLOCK_LIFETIME = 35,

        /// <summary>Alvarion-WiMAX-ARQ-Sync-Loss-Timeout (Type 36). Integer. ARQ sync loss timeout in microseconds.</summary>
        ARQ_SYNC_LOSS_TIMEOUT = 36,

        /// <summary>Alvarion-WiMAX-ARQ-Deliver-In-Order (Type 37). Integer. ARQ deliver in order.</summary>
        ARQ_DELIVER_IN_ORDER = 37,

        /// <summary>Alvarion-WiMAX-ARQ-Purge-Timeout (Type 38). Integer. ARQ purge timeout in microseconds.</summary>
        ARQ_PURGE_TIMEOUT = 38,

        /// <summary>Alvarion-WiMAX-ARQ-Block-Size (Type 39). Integer. ARQ block size.</summary>
        ARQ_BLOCK_SIZE = 39,

        /// <summary>Alvarion-WiMAX-CS-Specification (Type 40). Integer. Convergence sublayer specification.</summary>
        CS_SPECIFICATION = 40,

        /// <summary>Alvarion-WiMAX-Type-Of-Data-Delivery-Services (Type 41). Integer. Type of data delivery services.</summary>
        TYPE_OF_DATA_DELIVERY_SERVICES = 41,

        /// <summary>Alvarion-WiMAX-Paging-Preference (Type 42). Integer. Paging preference.</summary>
        PAGING_PREFERENCE = 42,

        /// <summary>Alvarion-WiMAX-MBS-Zone-ID (Type 43). Integer. MBS zone identifier.</summary>
        MBS_ZONE_ID = 43,

        /// <summary>Alvarion-WiMAX-Traffic-Priority (Type 44). Integer. Traffic priority level.</summary>
        TRAFFIC_PRIORITY = 44,

        /// <summary>Alvarion-WiMAX-Media-Flow-Type (Type 45). Integer. Media flow type.</summary>
        MEDIA_FLOW_TYPE = 45,

        /// <summary>Alvarion-WiMAX-HARQ-Service-Flows (Type 46). Integer. HARQ service flows configuration.</summary>
        HARQ_SERVICE_FLOWS = 46,

        /// <summary>Alvarion-WiMAX-SN-Feedback-Enabled (Type 47). Integer. SN feedback enabled.</summary>
        SN_FEEDBACK_ENABLED = 47,

        /// <summary>Alvarion-WiMAX-HARQ-Channel-Mapping (Type 48). Integer. HARQ channel mapping.</summary>
        HARQ_CHANNEL_MAPPING = 48,

        /// <summary>Alvarion-WiMAX-DSx-Retry-Timer (Type 49). Integer. DSx retry timer in seconds.</summary>
        DSX_RETRY_TIMER = 49,

        /// <summary>Alvarion-WiMAX-DSx-Response-Timer (Type 50). Integer. DSx response timer in seconds.</summary>
        DSX_RESPONSE_TIMER = 50,

        /// <summary>Alvarion-WiMAX-Service-Class-Name (Type 100). String. Service class name.</summary>
        SERVICE_CLASS_NAME = 100,

        /// <summary>Alvarion-WiMAX-QoS-Profile-Name (Type 101). String. QoS profile name.</summary>
        QOS_PROFILE_NAME = 101,

        /// <summary>Alvarion-WiMAX-BS-ID (Type 150). String. Base station identifier.</summary>
        BS_ID = 150,

        /// <summary>Alvarion-WiMAX-MS-ID (Type 151). String. Mobile station identifier.</summary>
        MS_ID = 151,

        /// <summary>Alvarion-WiMAX-NSP-ID (Type 152). Integer. Network Service Provider identifier.</summary>
        NSP_ID = 152,

        /// <summary>Alvarion-WiMAX-HA-IP (Type 153). IP address. Home Agent IP address.</summary>
        HA_IP = 153,

        /// <summary>Alvarion-WiMAX-DHCPv4-Server (Type 154). IP address. DHCPv4 server address.</summary>
        DHCPV4_SERVER = 154,

        /// <summary>Alvarion-WiMAX-Auth-Policy (Type 155). Integer. Authentication policy.</summary>
        AUTH_POLICY = 155,

        /// <summary>Alvarion-WiMAX-Visited-NSP-ID (Type 156). Integer. Visited NSP identifier.</summary>
        VISITED_NSP_ID = 156
    }

    /// <summary>
    /// Alvarion-WiMAX-UL/DL-Scheduler-Type attribute values (Types 15, 16).
    /// </summary>
    public enum ALVARION_WIMAX_SCHEDULER_TYPE
    {
        /// <summary>Undefined scheduler.</summary>
        UNDEFINED = 0,

        /// <summary>Best Effort scheduler.</summary>
        BEST_EFFORT = 2,

        /// <summary>Non-real-time Polling Service.</summary>
        NRTPS = 3,

        /// <summary>Real-time Polling Service.</summary>
        RTPS = 4,

        /// <summary>Extended Real-time Polling Service.</summary>
        ERTPS = 5,

        /// <summary>Unsolicited Grant Service.</summary>
        UGS = 6
    }

    /// <summary>
    /// Alvarion-WiMAX-Service-Flow-Type attribute values (Type 17).
    /// </summary>
    public enum ALVARION_WIMAX_SERVICE_FLOW_TYPE
    {
        /// <summary>Provisioned service flow.</summary>
        PROVISIONED = 0,

        /// <summary>Admitted service flow.</summary>
        ADMITTED = 1,

        /// <summary>Active service flow.</summary>
        ACTIVE = 2
    }

    /// <summary>
    /// Alvarion-WiMAX-ARQ-Enable attribute values (Type 31).
    /// </summary>
    public enum ALVARION_WIMAX_ARQ_ENABLE
    {
        /// <summary>ARQ disabled.</summary>
        DISABLED = 0,

        /// <summary>ARQ enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Alvarion-WiMAX-ARQ-Deliver-In-Order attribute values (Type 37).
    /// </summary>
    public enum ALVARION_WIMAX_ARQ_DELIVER_IN_ORDER
    {
        /// <summary>Out-of-order delivery allowed.</summary>
        OUT_OF_ORDER = 0,

        /// <summary>In-order delivery required.</summary>
        IN_ORDER = 1
    }

    /// <summary>
    /// Alvarion-WiMAX-CS-Specification attribute values (Type 40).
    /// </summary>
    public enum ALVARION_WIMAX_CS_SPECIFICATION
    {
        /// <summary>ATM CS.</summary>
        ATM = 1,

        /// <summary>IPv4 CS.</summary>
        IPV4 = 2,

        /// <summary>IPv6 CS.</summary>
        IPV6 = 3,

        /// <summary>Ethernet CS.</summary>
        ETHERNET = 4,

        /// <summary>802.3 CS.</summary>
        DOT_802_3 = 5,

        /// <summary>VLAN (802.1Q) CS.</summary>
        VLAN = 6
    }

    /// <summary>
    /// Alvarion-WiMAX-Type-Of-Data-Delivery-Services attribute values (Type 41).
    /// </summary>
    public enum ALVARION_WIMAX_TYPE_OF_DATA_DELIVERY_SERVICES
    {
        /// <summary>Unicast data delivery.</summary>
        UNICAST = 0,

        /// <summary>Multicast data delivery.</summary>
        MULTICAST = 1
    }

    /// <summary>
    /// Alvarion-WiMAX-Paging-Preference attribute values (Type 42).
    /// </summary>
    public enum ALVARION_WIMAX_PAGING_PREFERENCE
    {
        /// <summary>No paging.</summary>
        NO_PAGING = 0,

        /// <summary>Paging enabled.</summary>
        PAGING = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Alvarion WiMAX v2.2
    /// (IANA PEN 12394) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.alvarion.wimax.v2_2</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Alvarion WiMAX's vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 12394</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Alvarion WiMAX v2.2 base stations and ASN
    /// gateways for service flow configuration, QoS descriptors, ARQ parameters,
    /// scheduler types, convergence sublayer specification, and mobile station
    /// management.
    /// </para>
    /// <para>
    /// <b>Note:</b> This class shares the same Vendor ID (12394) with
    /// <see cref="AlvarionAttributes"/> but defines attributes from the separate
    /// <c>dictionary.alvarion.wimax.v2_2</c> dictionary file.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AlvarionWimaxAttributes.ServiceClassName("gold-service"));
    /// packet.SetAttribute(AlvarionWimaxAttributes.MaxSustainedTrafficRate(10000000));
    /// packet.SetAttribute(AlvarionWimaxAttributes.UlSchedulerType(ALVARION_WIMAX_SCHEDULER_TYPE.RTPS));
    /// packet.SetAttribute(AlvarionWimaxAttributes.DlSchedulerType(ALVARION_WIMAX_SCHEDULER_TYPE.BEST_EFFORT));
    /// </code>
    /// </remarks>
    public static class AlvarionWimaxAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Alvarion (shared with base dictionary).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 12394;

        #region Integer Attributes

        /// <summary>
        /// Creates an Alvarion-WiMAX-R3-IF-ID attribute (Type 1) with the specified identifier.
        /// </summary>
        /// <param name="value">The R3 interface identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes R3IfId(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.R3_IF_ID, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-PDFID attribute (Type 3) with the specified identifier.
        /// </summary>
        /// <param name="value">The Packet Data Flow identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Pdfid(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.PDFID, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-SDFID attribute (Type 4) with the specified identifier.
        /// </summary>
        /// <param name="value">The Service Data Flow identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Sdfid(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.SDFID, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-UL-Scheduler-Type attribute (Type 15) with the specified type.
        /// </summary>
        /// <param name="value">The uplink scheduler type. See <see cref="ALVARION_WIMAX_SCHEDULER_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UlSchedulerType(ALVARION_WIMAX_SCHEDULER_TYPE value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.UL_SCHEDULER_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-DL-Scheduler-Type attribute (Type 16) with the specified type.
        /// </summary>
        /// <param name="value">The downlink scheduler type. See <see cref="ALVARION_WIMAX_SCHEDULER_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DlSchedulerType(ALVARION_WIMAX_SCHEDULER_TYPE value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.DL_SCHEDULER_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Service-Flow-Type attribute (Type 17) with the specified type.
        /// </summary>
        /// <param name="value">The service flow type. See <see cref="ALVARION_WIMAX_SERVICE_FLOW_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServiceFlowType(ALVARION_WIMAX_SERVICE_FLOW_TYPE value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.SERVICE_FLOW_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Max-Sustained-Traffic-Rate attribute (Type 18) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum sustained traffic rate in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSustainedTrafficRate(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.MAX_SUSTAINED_TRAFFIC_RATE, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Max-Traffic-Burst attribute (Type 19) with the specified burst size.
        /// </summary>
        /// <param name="value">The maximum traffic burst in bytes.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxTrafficBurst(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.MAX_TRAFFIC_BURST, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Min-Reserved-Traffic-Rate attribute (Type 20) with the specified rate.
        /// </summary>
        /// <param name="value">The minimum reserved traffic rate in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MinReservedTrafficRate(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.MIN_RESERVED_TRAFFIC_RATE, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Min-Tolerable-Traffic-Rate attribute (Type 21) with the specified rate.
        /// </summary>
        /// <param name="value">The minimum tolerable traffic rate in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MinTolerableTrafficRate(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.MIN_TOLERABLE_TRAFFIC_RATE, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Bandwidth-Request attribute (Type 22) with the specified bandwidth.
        /// </summary>
        /// <param name="value">The bandwidth request in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthRequest(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.BANDWIDTH_REQUEST, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Tolerated-Jitter attribute (Type 23) with the specified jitter.
        /// </summary>
        /// <param name="value">The tolerated jitter in milliseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ToleratedJitter(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.TOLERATED_JITTER, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Max-Latency attribute (Type 24) with the specified latency.
        /// </summary>
        /// <param name="value">The maximum latency in milliseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxLatency(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.MAX_LATENCY, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Reduced-Resources-Code attribute (Type 25) with the specified code.
        /// </summary>
        /// <param name="value">The reduced resources code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReducedResourcesCode(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.REDUCED_RESOURCES_CODE, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Unsolicited-Grant-Interval attribute (Type 26) with the specified interval.
        /// </summary>
        /// <param name="value">The unsolicited grant interval in milliseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UnsolicitedGrantInterval(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.UNSOLICITED_GRANT_INTERVAL, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Unsolicited-Polling-Interval attribute (Type 27) with the specified interval.
        /// </summary>
        /// <param name="value">The unsolicited polling interval in milliseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UnsolicitedPollingInterval(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.UNSOLICITED_POLLING_INTERVAL, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-PDU-Size attribute (Type 28) with the specified size.
        /// </summary>
        /// <param name="value">The PDU size in bytes.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PduSize(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.PDU_SIZE, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-SDU-Size attribute (Type 29) with the specified size.
        /// </summary>
        /// <param name="value">The SDU size in bytes.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SduSize(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.SDU_SIZE, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Target-SAID attribute (Type 30) with the specified SAID.
        /// </summary>
        /// <param name="value">The target Security Association ID.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TargetSaid(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.TARGET_SAID, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-ARQ-Enable attribute (Type 31) with the specified setting.
        /// </summary>
        /// <param name="value">Whether ARQ is enabled. See <see cref="ALVARION_WIMAX_ARQ_ENABLE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ArqEnable(ALVARION_WIMAX_ARQ_ENABLE value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.ARQ_ENABLE, (int)value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-ARQ-Window-Size attribute (Type 32) with the specified window size.
        /// </summary>
        /// <param name="value">The ARQ window size.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ArqWindowSize(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.ARQ_WINDOW_SIZE, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-ARQ-Transmitter-Delay attribute (Type 33) with the specified delay.
        /// </summary>
        /// <param name="value">The ARQ transmitter delay in microseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ArqTransmitterDelay(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.ARQ_TRANSMITTER_DELAY, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-ARQ-Receiver-Delay attribute (Type 34) with the specified delay.
        /// </summary>
        /// <param name="value">The ARQ receiver delay in microseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ArqReceiverDelay(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.ARQ_RECEIVER_DELAY, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-ARQ-Block-Lifetime attribute (Type 35) with the specified lifetime.
        /// </summary>
        /// <param name="value">The ARQ block lifetime in microseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ArqBlockLifetime(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.ARQ_BLOCK_LIFETIME, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-ARQ-Sync-Loss-Timeout attribute (Type 36) with the specified timeout.
        /// </summary>
        /// <param name="value">The ARQ sync loss timeout in microseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ArqSyncLossTimeout(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.ARQ_SYNC_LOSS_TIMEOUT, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-ARQ-Deliver-In-Order attribute (Type 37) with the specified setting.
        /// </summary>
        /// <param name="value">Whether ARQ delivers in order. See <see cref="ALVARION_WIMAX_ARQ_DELIVER_IN_ORDER"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ArqDeliverInOrder(ALVARION_WIMAX_ARQ_DELIVER_IN_ORDER value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.ARQ_DELIVER_IN_ORDER, (int)value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-ARQ-Purge-Timeout attribute (Type 38) with the specified timeout.
        /// </summary>
        /// <param name="value">The ARQ purge timeout in microseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ArqPurgeTimeout(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.ARQ_PURGE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-ARQ-Block-Size attribute (Type 39) with the specified block size.
        /// </summary>
        /// <param name="value">The ARQ block size.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ArqBlockSize(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.ARQ_BLOCK_SIZE, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-CS-Specification attribute (Type 40) with the specified CS.
        /// </summary>
        /// <param name="value">The convergence sublayer specification. See <see cref="ALVARION_WIMAX_CS_SPECIFICATION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CsSpecification(ALVARION_WIMAX_CS_SPECIFICATION value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.CS_SPECIFICATION, (int)value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Type-Of-Data-Delivery-Services attribute (Type 41) with the specified type.
        /// </summary>
        /// <param name="value">The data delivery services type. See <see cref="ALVARION_WIMAX_TYPE_OF_DATA_DELIVERY_SERVICES"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TypeOfDataDeliveryServices(ALVARION_WIMAX_TYPE_OF_DATA_DELIVERY_SERVICES value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.TYPE_OF_DATA_DELIVERY_SERVICES, (int)value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Paging-Preference attribute (Type 42) with the specified preference.
        /// </summary>
        /// <param name="value">The paging preference. See <see cref="ALVARION_WIMAX_PAGING_PREFERENCE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PagingPreference(ALVARION_WIMAX_PAGING_PREFERENCE value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.PAGING_PREFERENCE, (int)value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-MBS-Zone-ID attribute (Type 43) with the specified zone ID.
        /// </summary>
        /// <param name="value">The MBS zone identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MbsZoneId(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.MBS_ZONE_ID, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Traffic-Priority attribute (Type 44) with the specified priority.
        /// </summary>
        /// <param name="value">The traffic priority level.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TrafficPriority(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.TRAFFIC_PRIORITY, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Media-Flow-Type attribute (Type 45) with the specified type.
        /// </summary>
        /// <param name="value">The media flow type.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MediaFlowType(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.MEDIA_FLOW_TYPE, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-HARQ-Service-Flows attribute (Type 46) with the specified configuration.
        /// </summary>
        /// <param name="value">The HARQ service flows configuration.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes HarqServiceFlows(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.HARQ_SERVICE_FLOWS, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-SN-Feedback-Enabled attribute (Type 47) with the specified setting.
        /// </summary>
        /// <param name="value">Whether SN feedback is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SnFeedbackEnabled(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.SN_FEEDBACK_ENABLED, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-HARQ-Channel-Mapping attribute (Type 48) with the specified mapping.
        /// </summary>
        /// <param name="value">The HARQ channel mapping.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes HarqChannelMapping(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.HARQ_CHANNEL_MAPPING, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-DSx-Retry-Timer attribute (Type 49) with the specified timer.
        /// </summary>
        /// <param name="value">The DSx retry timer in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DsxRetryTimer(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.DSX_RETRY_TIMER, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-DSx-Response-Timer attribute (Type 50) with the specified timer.
        /// </summary>
        /// <param name="value">The DSx response timer in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DsxResponseTimer(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.DSX_RESPONSE_TIMER, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-NSP-ID attribute (Type 152) with the specified identifier.
        /// </summary>
        /// <param name="value">The Network Service Provider identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NspId(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.NSP_ID, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Auth-Policy attribute (Type 155) with the specified policy.
        /// </summary>
        /// <param name="value">The authentication policy.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthPolicy(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.AUTH_POLICY, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Visited-NSP-ID attribute (Type 156) with the specified identifier.
        /// </summary>
        /// <param name="value">The Visited NSP identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VisitedNspId(int value)
        {
            return CreateInteger(AlvarionWimaxAttributeType.VISITED_NSP_ID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Alvarion-WiMAX-R3-IF-Name attribute (Type 2) with the specified name.
        /// </summary>
        /// <param name="value">The R3 interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes R3IfName(string value)
        {
            return CreateString(AlvarionWimaxAttributeType.R3_IF_NAME, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-Service-Class-Name attribute (Type 100) with the specified name.
        /// </summary>
        /// <param name="value">The service class name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceClassName(string value)
        {
            return CreateString(AlvarionWimaxAttributeType.SERVICE_CLASS_NAME, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-QoS-Profile-Name attribute (Type 101) with the specified name.
        /// </summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfileName(string value)
        {
            return CreateString(AlvarionWimaxAttributeType.QOS_PROFILE_NAME, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-BS-ID attribute (Type 150) with the specified identifier.
        /// </summary>
        /// <param name="value">The base station identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BsId(string value)
        {
            return CreateString(AlvarionWimaxAttributeType.BS_ID, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-MS-ID attribute (Type 151) with the specified identifier.
        /// </summary>
        /// <param name="value">The mobile station identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsId(string value)
        {
            return CreateString(AlvarionWimaxAttributeType.MS_ID, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Alvarion-WiMAX-HA-IP attribute (Type 153) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The Home Agent IP address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes HaIp(IPAddress value)
        {
            return CreateIpv4(AlvarionWimaxAttributeType.HA_IP, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-DHCPv4-Server attribute (Type 154) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The DHCPv4 server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes Dhcpv4Server(IPAddress value)
        {
            return CreateIpv4(AlvarionWimaxAttributeType.DHCPV4_SERVER, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates an Alvarion-WiMAX-Packet-Flow-Descriptor attribute (Type 11) with the specified raw value.
        /// </summary>
        /// <param name="value">The packet flow descriptor. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PacketFlowDescriptor(byte[] value)
        {
            return CreateOctets(AlvarionWimaxAttributeType.PACKET_FLOW_DESCRIPTOR, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-QoS-Descriptor attribute (Type 12) with the specified raw value.
        /// </summary>
        /// <param name="value">The QoS descriptor. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosDescriptor(byte[] value)
        {
            return CreateOctets(AlvarionWimaxAttributeType.QOS_DESCRIPTOR, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-UL-QoS-Descriptor attribute (Type 13) with the specified raw value.
        /// </summary>
        /// <param name="value">The uplink QoS descriptor. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UlQosDescriptor(byte[] value)
        {
            return CreateOctets(AlvarionWimaxAttributeType.UL_QOS_DESCRIPTOR, value);
        }

        /// <summary>
        /// Creates an Alvarion-WiMAX-DL-QoS-Descriptor attribute (Type 14) with the specified raw value.
        /// </summary>
        /// <param name="value">The downlink QoS descriptor. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DlQosDescriptor(byte[] value)
        {
            return CreateOctets(AlvarionWimaxAttributeType.DL_QOS_DESCRIPTOR, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Alvarion WiMAX attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AlvarionWimaxAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Alvarion WiMAX attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AlvarionWimaxAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a raw byte array value for the specified Alvarion WiMAX attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateOctets(AlvarionWimaxAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified Alvarion WiMAX attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(AlvarionWimaxAttributeType type, IPAddress value)
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