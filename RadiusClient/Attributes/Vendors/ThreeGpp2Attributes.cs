using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a 3GPP2 (IANA PEN 5535) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.3gpp2</c> and 3GPP2 X.S0011.
    /// </summary>
    public enum ThreeGpp2AttributeType : byte
    {
        /// <summary>3GPP2-Ike-Preshared-Secret-Request (Type 1). Integer. IKE pre-shared secret request indicator.</summary>
        IKE_PRESHARED_SECRET_REQUEST = 1,

        /// <summary>3GPP2-Security-Level (Type 2). Integer. Security level for the session.</summary>
        SECURITY_LEVEL = 2,

        /// <summary>3GPP2-Pre-Shared-Secret (Type 3). String. Pre-shared secret value.</summary>
        PRE_SHARED_SECRET = 3,

        /// <summary>3GPP2-Reverse-Tunnel-Spec (Type 4). Integer. Reverse tunnel specification.</summary>
        REVERSE_TUNNEL_SPEC = 4,

        /// <summary>3GPP2-Diffserv-Class-Option (Type 5). Integer. DiffServ class option.</summary>
        DIFFSERV_CLASS_OPTION = 5,

        /// <summary>3GPP2-Accounting-Container (Type 6). Octets. Accounting information container.</summary>
        ACCOUNTING_CONTAINER = 6,

        /// <summary>3GPP2-Home-Agent-IP-Address (Type 7). IP address. Home Agent IPv4 address.</summary>
        HOME_AGENT_IP_ADDRESS = 7,

        /// <summary>3GPP2-Airlink-Priority (Type 8). Integer. Air link priority for the session.</summary>
        AIRLINK_PRIORITY = 8,

        /// <summary>3GPP2-Airlink-Record-Type (Type 9). Integer. Air link record type for accounting.</summary>
        AIRLINK_RECORD_TYPE = 9,

        /// <summary>3GPP2-R-P-Session-ID (Type 10). Octets. R-P session identifier.</summary>
        R_P_SESSION_ID = 10,

        /// <summary>3GPP2-Airlink-Sequence-Number (Type 11). Integer. Air link sequence number.</summary>
        AIRLINK_SEQUENCE_NUMBER = 11,

        /// <summary>3GPP2-Received-HDLC-Octets (Type 12). Integer. Received HDLC octets count.</summary>
        RECEIVED_HDLC_OCTETS = 12,

        /// <summary>3GPP2-Correlation-Id (Type 13). Octets. Correlation identifier.</summary>
        CORRELATION_ID = 13,

        /// <summary>3GPP2-Module-Orig-IP-Address (Type 14). IP address. Module origination IPv4 address.</summary>
        MODULE_ORIG_IP_ADDRESS = 14,

        /// <summary>3GPP2-Module-Term-IP-Address (Type 15). IP address. Module termination IPv4 address.</summary>
        MODULE_TERM_IP_ADDRESS = 15,

        /// <summary>3GPP2-ESN (Type 16). String. Electronic Serial Number.</summary>
        ESN = 16,

        /// <summary>3GPP2-Auth-Flow-Id (Type 17). Octets. Authentication flow identifier.</summary>
        AUTH_FLOW_ID = 17,

        /// <summary>3GPP2-Granted-QoS-Parameters (Type 18). Octets. Granted QoS parameters.</summary>
        GRANTED_QOS_PARAMETERS = 18,

        /// <summary>3GPP2-Maximum-Authorized-Aggr-Bandwidth (Type 19). Integer. Maximum authorised aggregate bandwidth.</summary>
        MAXIMUM_AUTHORIZED_AGGR_BANDWIDTH = 19,

        /// <summary>3GPP2-IP-Technology (Type 20). Integer. IP technology type.</summary>
        IP_TECHNOLOGY = 20,

        /// <summary>3GPP2-QoS-Id (Type 21). Integer. QoS flow identifier.</summary>
        QOS_ID = 21,

        /// <summary>3GPP2-QoS-Description (Type 22). Integer. QoS flow description.</summary>
        QOS_DESCRIPTION = 22,

        /// <summary>3GPP2-QoS-Enforced-Rule (Type 23). Octets. Enforced QoS rule.</summary>
        QOS_ENFORCED_RULE = 23,

        /// <summary>3GPP2-Session-Termination-Capability (Type 24). Integer. Session termination capability.</summary>
        SESSION_TERMINATION_CAPABILITY = 24,

        /// <summary>3GPP2-Prepaid-Acct-Quota (Type 25). Octets. Prepaid accounting quota.</summary>
        PREPAID_ACCT_QUOTA = 25,

        /// <summary>3GPP2-Prepaid-Acct-Capability (Type 26). Octets. Prepaid accounting capability.</summary>
        PREPAID_ACCT_CAPABILITY = 26,

        /// <summary>3GPP2-MIP-Lifetime (Type 27). Octets. Mobile IP registration lifetime.</summary>
        MIP_LIFETIME = 27,

        /// <summary>3GPP2-Acct-Stop-Trigger (Type 28). Integer. Accounting stop trigger type.</summary>
        ACCT_STOP_TRIGGER = 28,

        /// <summary>3GPP2-Service-Reference-Id (Type 29). Integer. Service reference identifier.</summary>
        SERVICE_REFERENCE_ID = 29,

        /// <summary>3GPP2-DNS-Update-Required (Type 30). Integer. DNS update required indicator.</summary>
        DNS_UPDATE_REQUIRED = 30,

        /// <summary>3GPP2-Always-On (Type 31). Integer. Always-on service indicator.</summary>
        ALWAYS_ON = 31,

        /// <summary>3GPP2-Foreign-Agent-Address (Type 32). IP address. Foreign Agent IPv4 address.</summary>
        FOREIGN_AGENT_ADDRESS = 32,

        /// <summary>3GPP2-Last-User-Activity-Time (Type 33). Integer. Last user activity timestamp.</summary>
        LAST_USER_ACTIVITY_TIME = 33,

        /// <summary>3GPP2-MN-AAA-Removal-Indication (Type 34). Integer. MN-AAA removal indication.</summary>
        MN_AAA_REMOVAL_INDICATION = 34,

        /// <summary>3GPP2-RN-Packet-Data-Inactivity-Timer (Type 35). Integer. RN packet data inactivity timer.</summary>
        RN_PACKET_DATA_INACTIVITY_TIMER = 35,

        /// <summary>3GPP2-Forward-PDCH-RC (Type 36). Integer. Forward PDCH Radio Configuration.</summary>
        FORWARD_PDCH_RC = 36,

        /// <summary>3GPP2-Forward-DCCH-Mux-Option (Type 37). Integer. Forward DCCH multiplex option.</summary>
        FORWARD_DCCH_MUX_OPTION = 37,

        /// <summary>3GPP2-Reverse-DCCH-Mux-Option (Type 38). Integer. Reverse DCCH multiplex option.</summary>
        REVERSE_DCCH_MUX_OPTION = 38,

        /// <summary>3GPP2-Forward-FCH-Mux-Option (Type 39). Integer. Forward FCH multiplex option.</summary>
        FORWARD_FCH_MUX_OPTION = 39,

        /// <summary>3GPP2-Reverse-FCH-Mux-Option (Type 40). Integer. Reverse FCH multiplex option.</summary>
        REVERSE_FCH_MUX_OPTION = 40,

        /// <summary>3GPP2-IP-Address (Type 41). IP address. Assigned IPv4 address.</summary>
        IP_ADDRESS = 41,

        /// <summary>3GPP2-Home-Agent-IP-Address-v6 (Type 42). IPv6 address. Home Agent IPv6 address.</summary>
        HOME_AGENT_IP_ADDRESS_V6 = 42,

        /// <summary>3GPP2-Foreign-Agent-IP-Address-v6 (Type 43). IPv6 address. Foreign Agent IPv6 address.</summary>
        FOREIGN_AGENT_IP_ADDRESS_V6 = 43,

        /// <summary>3GPP2-Service-Option (Type 44). Integer. CDMA service option code.</summary>
        SERVICE_OPTION = 44,

        /// <summary>3GPP2-Forward-Traffic-Type (Type 45). Integer. Forward traffic type.</summary>
        FORWARD_TRAFFIC_TYPE = 45,

        /// <summary>3GPP2-Reverse-Traffic-Type (Type 46). Integer. Reverse traffic type.</summary>
        REVERSE_TRAFFIC_TYPE = 46,

        /// <summary>3GPP2-FCH-Frame-Size (Type 47). Integer. FCH frame size in bits.</summary>
        FCH_FRAME_SIZE = 47,

        /// <summary>3GPP2-Forward-FCH-RC (Type 48). Integer. Forward FCH Radio Configuration.</summary>
        FORWARD_FCH_RC = 48,

        /// <summary>3GPP2-Reverse-FCH-RC (Type 49). Integer. Reverse FCH Radio Configuration.</summary>
        REVERSE_FCH_RC = 49,

        /// <summary>3GPP2-IP-Quality-Of-Service (Type 50). Integer. IP quality of service level.</summary>
        IP_QUALITY_OF_SERVICE = 50,

        /// <summary>3GPP2-Primary-DNS-Server-Address (Type 51). IP address. Primary DNS server IPv4 address.</summary>
        PRIMARY_DNS_SERVER_ADDRESS = 51,

        /// <summary>3GPP2-Secondary-DNS-Server-Address (Type 52). IP address. Secondary DNS server IPv4 address.</summary>
        SECONDARY_DNS_SERVER_ADDRESS = 52,

        /// <summary>3GPP2-Remote-IP-Address (Type 53). IP address. Remote IPv4 address.</summary>
        REMOTE_IP_ADDRESS = 53,

        /// <summary>3GPP2-MIP-Filter-Rule (Type 54). Octets. Mobile IP filter rule.</summary>
        MIP_FILTER_RULE = 54,

        /// <summary>3GPP2-Prepaid-Tariff-Switching (Type 55). Octets. Prepaid tariff switching data.</summary>
        PREPAID_TARIFF_SWITCHING = 55,

        /// <summary>3GPP2-MEID (Type 56). String. Mobile Equipment Identifier.</summary>
        MEID = 56,

        /// <summary>3GPP2-DNS-Server-IP-Address-v6 (Type 57). IPv6 address. DNS server IPv6 address.</summary>
        DNS_SERVER_IP_ADDRESS_V6 = 57,

        /// <summary>3GPP2-MIP6-Home-Agent-Address-From-BU (Type 58). IPv6 address. MIPv6 Home Agent address from Binding Update.</summary>
        MIP6_HOME_AGENT_ADDRESS_FROM_BU = 58,

        /// <summary>3GPP2-MIP6-Care-Of-Address (Type 59). IPv6 address. MIPv6 Care-of Address.</summary>
        MIP6_CARE_OF_ADDRESS = 59,

        /// <summary>3GPP2-Home-Agent-Not-Authorized (Type 60). Integer. Home Agent not authorised indicator.</summary>
        HOME_AGENT_NOT_AUTHORIZED = 60,

        /// <summary>3GPP2-PCF-IP-Address (Type 61). IP address. PCF (Packet Control Function) IPv4 address.</summary>
        PCF_IP_ADDRESS = 61,

        /// <summary>3GPP2-BSID (Type 62). String. Base Station Identifier.</summary>
        BSID = 62,

        /// <summary>3GPP2-User-Id (Type 63). Integer. User identifier.</summary>
        USER_ID = 63,

        /// <summary>3GPP2-Forward-PDCH-RC-v2 (Type 64). Integer. Forward PDCH Radio Configuration version 2.</summary>
        FORWARD_PDCH_RC_V2 = 64,

        /// <summary>3GPP2-Reverse-PDCH-RC (Type 65). Integer. Reverse PDCH Radio Configuration.</summary>
        REVERSE_PDCH_RC = 65,

        /// <summary>3GPP2-VSNCP-Disconnect-Cause-Code (Type 66). Integer. VSNCP disconnect cause code.</summary>
        VSNCP_DISCONNECT_CAUSE_CODE = 66,

        /// <summary>3GPP2-Subnet (Type 68). Octets. IPv6 subnet prefix.</summary>
        SUBNET = 68,

        /// <summary>3GPP2-Reverse-PDCH-RC-v2 (Type 69). Integer. Reverse PDCH Radio Configuration version 2.</summary>
        REVERSE_PDCH_RC_V2 = 69,

        /// <summary>3GPP2-Remote-IPv6-Address (Type 70). IPv6 address. Remote IPv6 address.</summary>
        REMOTE_IPV6_ADDRESS = 70,

        /// <summary>3GPP2-Remote-Address-Table-Index (Type 71). Octets. Remote address table index.</summary>
        REMOTE_ADDRESS_TABLE_INDEX = 71,

        /// <summary>3GPP2-Remote-IPv4-Addr-Octet-Count (Type 72). Octets. Remote IPv4 address octet count.</summary>
        REMOTE_IPV4_ADDR_OCTET_COUNT = 72,

        /// <summary>3GPP2-Allowed-Diffserv-Marking (Type 73). Octets. Allowed DiffServ markings.</summary>
        ALLOWED_DIFFSERV_MARKING = 73,

        /// <summary>3GPP2-Service-Option-Profile (Type 74). Octets. Service option profile.</summary>
        SERVICE_OPTION_PROFILE = 74,

        /// <summary>3GPP2-DNS-Update-Capability (Type 75). Integer. DNS update capability indicator.</summary>
        DNS_UPDATE_CAPABILITY = 75,

        /// <summary>3GPP2-Carrier-Id (Type 142). Octets. Carrier identifier.</summary>
        CARRIER_ID = 142,

        /// <summary>3GPP2-GMT-Time-Zone-Offset (Type 143). Integer. GMT timezone offset in minutes.</summary>
        GMT_TIME_ZONE_OFFSET = 143,

        /// <summary>3GPP2-HA-Request (Type 168). Integer. Home Agent request indicator.</summary>
        HA_REQUEST = 168,

        /// <summary>3GPP2-HA-Authorised (Type 169). Integer. Home Agent authorised indicator.</summary>
        HA_AUTHORISED = 169,

        /// <summary>3GPP2-IP-Ver-Authorised (Type 172). Integer. IP version authorised.</summary>
        IP_VER_AUTHORISED = 172,

        /// <summary>3GPP2-MIPv4-Mesg-Id (Type 173). Integer. MIPv4 message identifier.</summary>
        MIPV4_MESG_ID = 173,

        /// <summary>3GPP2-MN-HA-SPI (Type 174). Integer. MN-HA Security Parameter Index.</summary>
        MN_HA_SPI = 174,

        /// <summary>3GPP2-MN-HA-Shared-Key (Type 175). String. MN-HA shared key.</summary>
        MN_HA_SHARED_KEY = 175,

        /// <summary>3GPP2-Remote-IP-v6-Addr-Octet-Count (Type 176). Octets. Remote IPv6 address octet count.</summary>
        REMOTE_IPV6_ADDR_OCTET_COUNT = 176
    }

    /// <summary>
    /// 3GPP2-Diffserv-Class-Option attribute values (Type 5).
    /// </summary>
    public enum THREEGPP2_DIFFSERV_CLASS_OPTION
    {
        /// <summary>Best Effort (BE) class.</summary>
        BEST_EFFORT = 0,

        /// <summary>Assured Forwarding 11 (AF11) class.</summary>
        AF11 = 10,

        /// <summary>Assured Forwarding 12 (AF12) class.</summary>
        AF12 = 12,

        /// <summary>Assured Forwarding 13 (AF13) class.</summary>
        AF13 = 14,

        /// <summary>Assured Forwarding 21 (AF21) class.</summary>
        AF21 = 18,

        /// <summary>Assured Forwarding 22 (AF22) class.</summary>
        AF22 = 20,

        /// <summary>Assured Forwarding 23 (AF23) class.</summary>
        AF23 = 22,

        /// <summary>Assured Forwarding 31 (AF31) class.</summary>
        AF31 = 26,

        /// <summary>Assured Forwarding 32 (AF32) class.</summary>
        AF32 = 28,

        /// <summary>Assured Forwarding 33 (AF33) class.</summary>
        AF33 = 30,

        /// <summary>Assured Forwarding 41 (AF41) class.</summary>
        AF41 = 34,

        /// <summary>Assured Forwarding 42 (AF42) class.</summary>
        AF42 = 36,

        /// <summary>Assured Forwarding 43 (AF43) class.</summary>
        AF43 = 38,

        /// <summary>Expedited Forwarding (EF) class.</summary>
        EF = 46
    }

    /// <summary>
    /// 3GPP2-Airlink-Record-Type attribute values (Type 9).
    /// </summary>
    public enum THREEGPP2_AIRLINK_RECORD_TYPE
    {
        /// <summary>Connection setup record.</summary>
        CONNECTION_SETUP = 1,

        /// <summary>Active start record.</summary>
        ACTIVE_START = 2,

        /// <summary>Active stop record.</summary>
        ACTIVE_STOP = 3,

        /// <summary>Short data burst record.</summary>
        SHORT_DATA_BURST = 4
    }

    /// <summary>
    /// 3GPP2-IP-Technology attribute values (Type 20).
    /// </summary>
    public enum THREEGPP2_IP_TECHNOLOGY
    {
        /// <summary>Simple IP.</summary>
        SIMPLE_IP = 1,

        /// <summary>Mobile IP.</summary>
        MOBILE_IP = 2,

        /// <summary>Proxy Mobile IP.</summary>
        PROXY_MOBILE_IP = 3
    }

    /// <summary>
    /// 3GPP2-Session-Termination-Capability attribute values (Type 24).
    /// </summary>
    public enum THREEGPP2_SESSION_TERMINATION_CAPABILITY
    {
        /// <summary>Dynamic authorisation not supported.</summary>
        DYNAMIC_AUTHORIZATION_NOT_SUPPORTED = 0,

        /// <summary>Dynamic authorisation supported.</summary>
        DYNAMIC_AUTHORIZATION_SUPPORTED = 1
    }

    /// <summary>
    /// 3GPP2-Acct-Stop-Trigger attribute values (Type 28).
    /// </summary>
    public enum THREEGPP2_ACCT_STOP_TRIGGER
    {
        /// <summary>Service reference identifier change.</summary>
        SERVICE_REFERENCE_ID_CHANGE = 1,

        /// <summary>Transfer of service.</summary>
        SERVICE_TRANSFER = 2,

        /// <summary>Service option change.</summary>
        SERVICE_OPTION_CHANGE = 3,

        /// <summary>A10 retransmission.</summary>
        A10_RETRANSMISSION = 4,

        /// <summary>Inter-PCF handoff.</summary>
        INTER_PCF_HANDOFF = 5,

        /// <summary>Inter-PDSN handoff.</summary>
        INTER_PDSN_HANDOFF = 6
    }

    /// <summary>
    /// 3GPP2-DNS-Update-Required attribute values (Type 30).
    /// </summary>
    public enum THREEGPP2_DNS_UPDATE_REQUIRED
    {
        /// <summary>DNS update not required.</summary>
        NOT_REQUIRED = 0,

        /// <summary>DNS update required.</summary>
        REQUIRED = 1
    }

    /// <summary>
    /// 3GPP2-Always-On attribute values (Type 31).
    /// </summary>
    public enum THREEGPP2_ALWAYS_ON
    {
        /// <summary>Always-on not supported.</summary>
        NOT_SUPPORTED = 0,

        /// <summary>Always-on supported.</summary>
        SUPPORTED = 1
    }

    /// <summary>
    /// 3GPP2-Forward-Traffic-Type attribute values (Type 45).
    /// </summary>
    public enum THREEGPP2_FORWARD_TRAFFIC_TYPE
    {
        /// <summary>Primary traffic.</summary>
        PRIMARY_TRAFFIC = 1,

        /// <summary>Secondary traffic.</summary>
        SECONDARY_TRAFFIC = 2
    }

    /// <summary>
    /// 3GPP2-Reverse-Traffic-Type attribute values (Type 46).
    /// </summary>
    public enum THREEGPP2_REVERSE_TRAFFIC_TYPE
    {
        /// <summary>Primary traffic.</summary>
        PRIMARY_TRAFFIC = 1,

        /// <summary>Secondary traffic.</summary>
        SECONDARY_TRAFFIC = 2
    }

    /// <summary>
    /// 3GPP2-FCH-Frame-Size attribute values (Type 47).
    /// </summary>
    public enum THREEGPP2_FCH_FRAME_SIZE
    {
        /// <summary>20 millisecond frame size.</summary>
        FRAME_20_MS = 1,

        /// <summary>5 millisecond frame size.</summary>
        FRAME_5_MS = 2
    }

    /// <summary>
    /// 3GPP2-VSNCP-Disconnect-Cause-Code attribute values (Type 66).
    /// </summary>
    public enum THREEGPP2_VSNCP_DISCONNECT_CAUSE_CODE
    {
        /// <summary>General error.</summary>
        GENERAL_ERROR = 1,

        /// <summary>Unauthorised APN.</summary>
        UNAUTHORIZED_APN = 2,

        /// <summary>PDN limit exceeded.</summary>
        PDN_LIMIT_EXCEEDED = 3,

        /// <summary>No PDN gateway available.</summary>
        NO_PDN_GW = 4,

        /// <summary>PDN gateway unreachable.</summary>
        PDN_GW_UNREACHABLE = 5,

        /// <summary>PDN gateway rejected.</summary>
        PDN_GW_REJECTED = 6,

        /// <summary>Insufficient parameters.</summary>
        INSUFFICIENT_PARAMETERS = 7,

        /// <summary>Resource unavailable.</summary>
        RESOURCE_UNAVAILABLE = 8,

        /// <summary>Admin prohibited.</summary>
        ADMIN_PROHIBITED = 9,

        /// <summary>PDN-Id already in use.</summary>
        PDN_ID_IN_USE = 10,

        /// <summary>Subscription limitation.</summary>
        SUBSCRIPTION_LIMITATION = 11,

        /// <summary>PDN connection already exists for the APN.</summary>
        PDN_CONNECTION_ALREADY_EXISTS = 12,

        /// <summary>Emergency services not supported.</summary>
        EMERGENCY_SERVICES_NOT_SUPPORTED = 13,

        /// <summary>Reconnection not allowed.</summary>
        RECONNECT_NOT_ALLOWED = 14
    }

    /// <summary>
    /// 3GPP2-HA-Request attribute values (Type 168).
    /// </summary>
    public enum THREEGPP2_HA_REQUEST
    {
        /// <summary>HA not requested.</summary>
        NOT_REQUESTED = 1,

        /// <summary>HA requested.</summary>
        REQUESTED = 2,

        /// <summary>No preference for HA.</summary>
        NO_PREFERENCE = 3
    }

    /// <summary>
    /// 3GPP2-HA-Authorised attribute values (Type 169).
    /// </summary>
    public enum THREEGPP2_HA_AUTHORISED
    {
        /// <summary>HA not authorised.</summary>
        NOT_AUTHORISED = 0,

        /// <summary>HA authorised.</summary>
        AUTHORISED = 1
    }

    /// <summary>
    /// 3GPP2-IP-Ver-Authorised attribute values (Type 172).
    /// </summary>
    public enum THREEGPP2_IP_VER_AUTHORISED
    {
        /// <summary>IPv4 authorised.</summary>
        IPV4 = 1,

        /// <summary>IPv6 authorised.</summary>
        IPV6 = 2,

        /// <summary>IPv4 and IPv6 authorised.</summary>
        IPV4_IPV6 = 3
    }

    /// <summary>
    /// 3GPP2-MN-AAA-Removal-Indication attribute values (Type 34).
    /// </summary>
    public enum THREEGPP2_MN_AAA_REMOVAL_INDICATION
    {
        /// <summary>Removal not required.</summary>
        NOT_REQUIRED = 0,

        /// <summary>Removal required.</summary>
        REQUIRED = 1
    }

    /// <summary>
    /// 3GPP2-DNS-Update-Capability attribute values (Type 75).
    /// </summary>
    public enum THREEGPP2_DNS_UPDATE_CAPABILITY
    {
        /// <summary>DNS update not supported.</summary>
        NOT_SUPPORTED = 0,

        /// <summary>DNS update supported.</summary>
        SUPPORTED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing 3GPP2 (IANA PEN 5535)
    /// Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.3gpp2</c> and 3GPP2 X.S0011.
    /// </summary>
    /// <remarks>
    /// <para>
    /// 3GPP2's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 5535</c>.
    /// </para>
    /// <para>
    /// These attributes are used in CDMA2000 and EV-DO mobile network RADIUS
    /// deployments for session management, Mobile IP, QoS, and accounting.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_REQUEST);
    /// packet.SetAttribute(ThreeGpp2Attributes.Esn("A0000001"));
    /// packet.SetAttribute(ThreeGpp2Attributes.IpTechnology(THREEGPP2_IP_TECHNOLOGY.MOBILE_IP));
    /// packet.SetAttribute(ThreeGpp2Attributes.HomeAgentIpAddress(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(ThreeGpp2Attributes.AlwaysOn(THREEGPP2_ALWAYS_ON.SUPPORTED));
    /// </code>
    /// </remarks>
    public static class ThreeGpp2Attributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for 3GPP2 (3rd Generation Partnership Project 2).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 5535;

        #region Integer Attributes

        /// <summary>
        /// Creates a 3GPP2-Ike-Preshared-Secret-Request attribute (Type 1) with the specified value.
        /// </summary>
        /// <param name="value">The IKE pre-shared secret request indicator.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IkePresharedSecretRequest(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.IKE_PRESHARED_SECRET_REQUEST, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Security-Level attribute (Type 2) with the specified security level.
        /// </summary>
        /// <param name="value">The security level for the session.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SecurityLevel(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.SECURITY_LEVEL, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Reverse-Tunnel-Spec attribute (Type 4) with the specified value.
        /// </summary>
        /// <param name="value">The reverse tunnel specification.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReverseTunnelSpec(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.REVERSE_TUNNEL_SPEC, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Diffserv-Class-Option attribute (Type 5) with the specified DiffServ class.
        /// </summary>
        /// <param name="value">The DiffServ class option. See <see cref="THREEGPP2_DIFFSERV_CLASS_OPTION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DiffservClassOption(THREEGPP2_DIFFSERV_CLASS_OPTION value)
        {
            return CreateInteger(ThreeGpp2AttributeType.DIFFSERV_CLASS_OPTION, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-Airlink-Priority attribute (Type 8) with the specified priority.
        /// </summary>
        /// <param name="value">The air link priority for the session.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AirlinkPriority(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.AIRLINK_PRIORITY, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Airlink-Record-Type attribute (Type 9) with the specified record type.
        /// </summary>
        /// <param name="value">The air link record type. See <see cref="THREEGPP2_AIRLINK_RECORD_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AirlinkRecordType(THREEGPP2_AIRLINK_RECORD_TYPE value)
        {
            return CreateInteger(ThreeGpp2AttributeType.AIRLINK_RECORD_TYPE, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-Airlink-Sequence-Number attribute (Type 11) with the specified sequence number.
        /// </summary>
        /// <param name="value">The air link sequence number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AirlinkSequenceNumber(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.AIRLINK_SEQUENCE_NUMBER, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Received-HDLC-Octets attribute (Type 12) with the specified count.
        /// </summary>
        /// <param name="value">The received HDLC octets count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReceivedHdlcOctets(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.RECEIVED_HDLC_OCTETS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Maximum-Authorized-Aggr-Bandwidth attribute (Type 19) with the specified value.
        /// </summary>
        /// <param name="value">The maximum authorised aggregate bandwidth.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaximumAuthorizedAggrBandwidth(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.MAXIMUM_AUTHORIZED_AGGR_BANDWIDTH, value);
        }

        /// <summary>
        /// Creates a 3GPP2-IP-Technology attribute (Type 20) with the specified technology type.
        /// </summary>
        /// <param name="value">The IP technology type. See <see cref="THREEGPP2_IP_TECHNOLOGY"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpTechnology(THREEGPP2_IP_TECHNOLOGY value)
        {
            return CreateInteger(ThreeGpp2AttributeType.IP_TECHNOLOGY, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-QoS-Id attribute (Type 21) with the specified identifier.
        /// </summary>
        /// <param name="value">The QoS flow identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes QosId(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.QOS_ID, value);
        }

        /// <summary>
        /// Creates a 3GPP2-QoS-Description attribute (Type 22) with the specified value.
        /// </summary>
        /// <param name="value">The QoS flow description.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes QosDescription(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.QOS_DESCRIPTION, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Session-Termination-Capability attribute (Type 24) with the specified value.
        /// </summary>
        /// <param name="value">The session termination capability. See <see cref="THREEGPP2_SESSION_TERMINATION_CAPABILITY"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTerminationCapability(THREEGPP2_SESSION_TERMINATION_CAPABILITY value)
        {
            return CreateInteger(ThreeGpp2AttributeType.SESSION_TERMINATION_CAPABILITY, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-Acct-Stop-Trigger attribute (Type 28) with the specified trigger.
        /// </summary>
        /// <param name="value">The accounting stop trigger type. See <see cref="THREEGPP2_ACCT_STOP_TRIGGER"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctStopTrigger(THREEGPP2_ACCT_STOP_TRIGGER value)
        {
            return CreateInteger(ThreeGpp2AttributeType.ACCT_STOP_TRIGGER, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-Service-Reference-Id attribute (Type 29) with the specified identifier.
        /// </summary>
        /// <param name="value">The service reference identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServiceReferenceId(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.SERVICE_REFERENCE_ID, value);
        }

        /// <summary>
        /// Creates a 3GPP2-DNS-Update-Required attribute (Type 30) with the specified value.
        /// </summary>
        /// <param name="value">The DNS update required indicator. See <see cref="THREEGPP2_DNS_UPDATE_REQUIRED"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DnsUpdateRequired(THREEGPP2_DNS_UPDATE_REQUIRED value)
        {
            return CreateInteger(ThreeGpp2AttributeType.DNS_UPDATE_REQUIRED, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-Always-On attribute (Type 31) with the specified value.
        /// </summary>
        /// <param name="value">The always-on service indicator. See <see cref="THREEGPP2_ALWAYS_ON"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AlwaysOn(THREEGPP2_ALWAYS_ON value)
        {
            return CreateInteger(ThreeGpp2AttributeType.ALWAYS_ON, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-Last-User-Activity-Time attribute (Type 33) with the specified timestamp.
        /// </summary>
        /// <param name="value">The last user activity timestamp.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LastUserActivityTime(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.LAST_USER_ACTIVITY_TIME, value);
        }

        /// <summary>
        /// Creates a 3GPP2-MN-AAA-Removal-Indication attribute (Type 34) with the specified value.
        /// </summary>
        /// <param name="value">The MN-AAA removal indication. See <see cref="THREEGPP2_MN_AAA_REMOVAL_INDICATION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MnAaaRemovalIndication(THREEGPP2_MN_AAA_REMOVAL_INDICATION value)
        {
            return CreateInteger(ThreeGpp2AttributeType.MN_AAA_REMOVAL_INDICATION, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-RN-Packet-Data-Inactivity-Timer attribute (Type 35) with the specified value.
        /// </summary>
        /// <param name="value">The RN packet data inactivity timer in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RnPacketDataInactivityTimer(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.RN_PACKET_DATA_INACTIVITY_TIMER, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Forward-PDCH-RC attribute (Type 36) with the specified radio configuration.
        /// </summary>
        /// <param name="value">The Forward PDCH Radio Configuration.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ForwardPdchRc(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.FORWARD_PDCH_RC, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Forward-DCCH-Mux-Option attribute (Type 37) with the specified value.
        /// </summary>
        /// <param name="value">The Forward DCCH multiplex option.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ForwardDcchMuxOption(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.FORWARD_DCCH_MUX_OPTION, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Reverse-DCCH-Mux-Option attribute (Type 38) with the specified value.
        /// </summary>
        /// <param name="value">The Reverse DCCH multiplex option.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReverseDcchMuxOption(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.REVERSE_DCCH_MUX_OPTION, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Forward-FCH-Mux-Option attribute (Type 39) with the specified value.
        /// </summary>
        /// <param name="value">The Forward FCH multiplex option.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ForwardFchMuxOption(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.FORWARD_FCH_MUX_OPTION, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Reverse-FCH-Mux-Option attribute (Type 40) with the specified value.
        /// </summary>
        /// <param name="value">The Reverse FCH multiplex option.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReverseFchMuxOption(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.REVERSE_FCH_MUX_OPTION, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Service-Option attribute (Type 44) with the specified service option code.
        /// </summary>
        /// <param name="value">The CDMA service option code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServiceOption(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.SERVICE_OPTION, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Forward-Traffic-Type attribute (Type 45) with the specified traffic type.
        /// </summary>
        /// <param name="value">The forward traffic type. See <see cref="THREEGPP2_FORWARD_TRAFFIC_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ForwardTrafficType(THREEGPP2_FORWARD_TRAFFIC_TYPE value)
        {
            return CreateInteger(ThreeGpp2AttributeType.FORWARD_TRAFFIC_TYPE, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-Reverse-Traffic-Type attribute (Type 46) with the specified traffic type.
        /// </summary>
        /// <param name="value">The reverse traffic type. See <see cref="THREEGPP2_REVERSE_TRAFFIC_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReverseTrafficType(THREEGPP2_REVERSE_TRAFFIC_TYPE value)
        {
            return CreateInteger(ThreeGpp2AttributeType.REVERSE_TRAFFIC_TYPE, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-FCH-Frame-Size attribute (Type 47) with the specified frame size.
        /// </summary>
        /// <param name="value">The FCH frame size. See <see cref="THREEGPP2_FCH_FRAME_SIZE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FchFrameSize(THREEGPP2_FCH_FRAME_SIZE value)
        {
            return CreateInteger(ThreeGpp2AttributeType.FCH_FRAME_SIZE, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-Forward-FCH-RC attribute (Type 48) with the specified radio configuration.
        /// </summary>
        /// <param name="value">The Forward FCH Radio Configuration.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ForwardFchRc(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.FORWARD_FCH_RC, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Reverse-FCH-RC attribute (Type 49) with the specified radio configuration.
        /// </summary>
        /// <param name="value">The Reverse FCH Radio Configuration.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReverseFchRc(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.REVERSE_FCH_RC, value);
        }

        /// <summary>
        /// Creates a 3GPP2-IP-Quality-Of-Service attribute (Type 50) with the specified QoS level.
        /// </summary>
        /// <param name="value">The IP quality of service level.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpQualityOfService(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.IP_QUALITY_OF_SERVICE, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Home-Agent-Not-Authorized attribute (Type 60) with the specified value.
        /// </summary>
        /// <param name="value">The Home Agent not authorised indicator.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes HomeAgentNotAuthorized(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.HOME_AGENT_NOT_AUTHORIZED, value);
        }

        /// <summary>
        /// Creates a 3GPP2-User-Id attribute (Type 63) with the specified user identifier.
        /// </summary>
        /// <param name="value">The user identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserId(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.USER_ID, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Forward-PDCH-RC-v2 attribute (Type 64) with the specified radio configuration.
        /// </summary>
        /// <param name="value">The Forward PDCH Radio Configuration version 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ForwardPdchRcV2(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.FORWARD_PDCH_RC_V2, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Reverse-PDCH-RC attribute (Type 65) with the specified radio configuration.
        /// </summary>
        /// <param name="value">The Reverse PDCH Radio Configuration.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReversePdchRc(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.REVERSE_PDCH_RC, value);
        }

        /// <summary>
        /// Creates a 3GPP2-VSNCP-Disconnect-Cause-Code attribute (Type 66) with the specified cause code.
        /// </summary>
        /// <param name="value">The VSNCP disconnect cause code. See <see cref="THREEGPP2_VSNCP_DISCONNECT_CAUSE_CODE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VsncpDisconnectCauseCode(THREEGPP2_VSNCP_DISCONNECT_CAUSE_CODE value)
        {
            return CreateInteger(ThreeGpp2AttributeType.VSNCP_DISCONNECT_CAUSE_CODE, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-Reverse-PDCH-RC-v2 attribute (Type 69) with the specified radio configuration.
        /// </summary>
        /// <param name="value">The Reverse PDCH Radio Configuration version 2.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReversePdchRcV2(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.REVERSE_PDCH_RC_V2, value);
        }

        /// <summary>
        /// Creates a 3GPP2-DNS-Update-Capability attribute (Type 75) with the specified value.
        /// </summary>
        /// <param name="value">The DNS update capability indicator. See <see cref="THREEGPP2_DNS_UPDATE_CAPABILITY"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DnsUpdateCapability(THREEGPP2_DNS_UPDATE_CAPABILITY value)
        {
            return CreateInteger(ThreeGpp2AttributeType.DNS_UPDATE_CAPABILITY, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-GMT-Time-Zone-Offset attribute (Type 143) with the specified offset.
        /// </summary>
        /// <param name="value">The GMT timezone offset in minutes.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes GmtTimeZoneOffset(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.GMT_TIME_ZONE_OFFSET, value);
        }

        /// <summary>
        /// Creates a 3GPP2-HA-Request attribute (Type 168) with the specified value.
        /// </summary>
        /// <param name="value">The Home Agent request indicator. See <see cref="THREEGPP2_HA_REQUEST"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes HaRequest(THREEGPP2_HA_REQUEST value)
        {
            return CreateInteger(ThreeGpp2AttributeType.HA_REQUEST, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-HA-Authorised attribute (Type 169) with the specified value.
        /// </summary>
        /// <param name="value">The Home Agent authorised indicator. See <see cref="THREEGPP2_HA_AUTHORISED"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes HaAuthorised(THREEGPP2_HA_AUTHORISED value)
        {
            return CreateInteger(ThreeGpp2AttributeType.HA_AUTHORISED, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-IP-Ver-Authorised attribute (Type 172) with the specified value.
        /// </summary>
        /// <param name="value">The IP version authorised. See <see cref="THREEGPP2_IP_VER_AUTHORISED"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpVerAuthorised(THREEGPP2_IP_VER_AUTHORISED value)
        {
            return CreateInteger(ThreeGpp2AttributeType.IP_VER_AUTHORISED, (int)value);
        }

        /// <summary>
        /// Creates a 3GPP2-MIPv4-Mesg-Id attribute (Type 173) with the specified message identifier.
        /// </summary>
        /// <param name="value">The MIPv4 message identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Mipv4MesgId(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.MIPV4_MESG_ID, value);
        }

        /// <summary>
        /// Creates a 3GPP2-MN-HA-SPI attribute (Type 174) with the specified Security Parameter Index.
        /// </summary>
        /// <param name="value">The MN-HA Security Parameter Index.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MnHaSpi(int value)
        {
            return CreateInteger(ThreeGpp2AttributeType.MN_HA_SPI, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a 3GPP2-Pre-Shared-Secret attribute (Type 3) with the specified pre-shared secret.
        /// </summary>
        /// <param name="value">The pre-shared secret value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PreSharedSecret(string value)
        {
            return CreateString(ThreeGpp2AttributeType.PRE_SHARED_SECRET, value);
        }

        /// <summary>
        /// Creates a 3GPP2-ESN attribute (Type 16) with the specified Electronic Serial Number.
        /// </summary>
        /// <param name="value">The Electronic Serial Number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Esn(string value)
        {
            return CreateString(ThreeGpp2AttributeType.ESN, value);
        }

        /// <summary>
        /// Creates a 3GPP2-MEID attribute (Type 56) with the specified Mobile Equipment Identifier.
        /// </summary>
        /// <param name="value">The Mobile Equipment Identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Meid(string value)
        {
            return CreateString(ThreeGpp2AttributeType.MEID, value);
        }

        /// <summary>
        /// Creates a 3GPP2-BSID attribute (Type 62) with the specified Base Station Identifier.
        /// </summary>
        /// <param name="value">The Base Station Identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Bsid(string value)
        {
            return CreateString(ThreeGpp2AttributeType.BSID, value);
        }

        /// <summary>
        /// Creates a 3GPP2-MN-HA-Shared-Key attribute (Type 175) with the specified shared key.
        /// </summary>
        /// <param name="value">The MN-HA shared key. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MnHaSharedKey(string value)
        {
            return CreateString(ThreeGpp2AttributeType.MN_HA_SHARED_KEY, value);
        }

        #endregion

        #region IPv4 Address Attributes

        /// <summary>
        /// Creates a 3GPP2-Home-Agent-IP-Address attribute (Type 7) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The Home Agent IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes HomeAgentIpAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGpp2AttributeType.HOME_AGENT_IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Module-Orig-IP-Address attribute (Type 14) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The module origination IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ModuleOrigIpAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGpp2AttributeType.MODULE_ORIG_IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Module-Term-IP-Address attribute (Type 15) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The module termination IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ModuleTermIpAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGpp2AttributeType.MODULE_TERM_IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Foreign-Agent-Address attribute (Type 32) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The Foreign Agent IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ForeignAgentAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGpp2AttributeType.FOREIGN_AGENT_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-IP-Address attribute (Type 41) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The assigned IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes IpAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGpp2AttributeType.IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Primary-DNS-Server-Address attribute (Type 51) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The primary DNS server IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDnsServerAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGpp2AttributeType.PRIMARY_DNS_SERVER_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Secondary-DNS-Server-Address attribute (Type 52) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The secondary DNS server IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDnsServerAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGpp2AttributeType.SECONDARY_DNS_SERVER_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Remote-IP-Address attribute (Type 53) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The remote IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes RemoteIpAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGpp2AttributeType.REMOTE_IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-PCF-IP-Address attribute (Type 61) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The PCF IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PcfIpAddress(IPAddress value)
        {
            return CreateIpv4(ThreeGpp2AttributeType.PCF_IP_ADDRESS, value);
        }

        #endregion

        #region IPv6 Address Attributes

        /// <summary>
        /// Creates a 3GPP2-Home-Agent-IP-Address-v6 attribute (Type 42) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">
        /// The Home Agent IPv6 address. Must be an IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv6 address.</exception>
        public static VendorSpecificAttributes HomeAgentIpAddressV6(IPAddress value)
        {
            return CreateIpv6(ThreeGpp2AttributeType.HOME_AGENT_IP_ADDRESS_V6, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Foreign-Agent-IP-Address-v6 attribute (Type 43) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">
        /// The Foreign Agent IPv6 address. Must be an IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv6 address.</exception>
        public static VendorSpecificAttributes ForeignAgentIpAddressV6(IPAddress value)
        {
            return CreateIpv6(ThreeGpp2AttributeType.FOREIGN_AGENT_IP_ADDRESS_V6, value);
        }

        /// <summary>
        /// Creates a 3GPP2-DNS-Server-IP-Address-v6 attribute (Type 57) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">
        /// The DNS server IPv6 address. Must be an IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv6 address.</exception>
        public static VendorSpecificAttributes DnsServerIpAddressV6(IPAddress value)
        {
            return CreateIpv6(ThreeGpp2AttributeType.DNS_SERVER_IP_ADDRESS_V6, value);
        }

        /// <summary>
        /// Creates a 3GPP2-MIP6-Home-Agent-Address-From-BU attribute (Type 58) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">
        /// The MIPv6 Home Agent address from Binding Update. Must be an IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv6 address.</exception>
        public static VendorSpecificAttributes Mip6HomeAgentAddressFromBu(IPAddress value)
        {
            return CreateIpv6(ThreeGpp2AttributeType.MIP6_HOME_AGENT_ADDRESS_FROM_BU, value);
        }

        /// <summary>
        /// Creates a 3GPP2-MIP6-Care-Of-Address attribute (Type 59) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">
        /// The MIPv6 Care-of Address. Must be an IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv6 address.</exception>
        public static VendorSpecificAttributes Mip6CareOfAddress(IPAddress value)
        {
            return CreateIpv6(ThreeGpp2AttributeType.MIP6_CARE_OF_ADDRESS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Remote-IPv6-Address attribute (Type 70) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">
        /// The remote IPv6 address. Must be an IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv6 address.</exception>
        public static VendorSpecificAttributes RemoteIpv6Address(IPAddress value)
        {
            return CreateIpv6(ThreeGpp2AttributeType.REMOTE_IPV6_ADDRESS, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates a 3GPP2-Accounting-Container attribute (Type 6) with the specified raw value.
        /// </summary>
        /// <param name="value">The accounting information container octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccountingContainer(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.ACCOUNTING_CONTAINER, value);
        }

        /// <summary>
        /// Creates a 3GPP2-R-P-Session-ID attribute (Type 10) with the specified raw value.
        /// </summary>
        /// <param name="value">The R-P session identifier octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RPSessionId(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.R_P_SESSION_ID, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Correlation-Id attribute (Type 13) with the specified raw value.
        /// </summary>
        /// <param name="value">The correlation identifier octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CorrelationId(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.CORRELATION_ID, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Auth-Flow-Id attribute (Type 17) with the specified raw value.
        /// </summary>
        /// <param name="value">The authentication flow identifier octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthFlowId(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.AUTH_FLOW_ID, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Granted-QoS-Parameters attribute (Type 18) with the specified raw value.
        /// </summary>
        /// <param name="value">The granted QoS parameters octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GrantedQosParameters(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.GRANTED_QOS_PARAMETERS, value);
        }

        /// <summary>
        /// Creates a 3GPP2-QoS-Enforced-Rule attribute (Type 23) with the specified raw value.
        /// </summary>
        /// <param name="value">The enforced QoS rule octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosEnforcedRule(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.QOS_ENFORCED_RULE, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Prepaid-Acct-Quota attribute (Type 25) with the specified raw value.
        /// </summary>
        /// <param name="value">The prepaid accounting quota octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrepaidAcctQuota(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.PREPAID_ACCT_QUOTA, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Prepaid-Acct-Capability attribute (Type 26) with the specified raw value.
        /// </summary>
        /// <param name="value">The prepaid accounting capability octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrepaidAcctCapability(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.PREPAID_ACCT_CAPABILITY, value);
        }

        /// <summary>
        /// Creates a 3GPP2-MIP-Lifetime attribute (Type 27) with the specified raw value.
        /// </summary>
        /// <param name="value">The Mobile IP registration lifetime octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MipLifetime(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.MIP_LIFETIME, value);
        }

        /// <summary>
        /// Creates a 3GPP2-MIP-Filter-Rule attribute (Type 54) with the specified raw value.
        /// </summary>
        /// <param name="value">The Mobile IP filter rule octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MipFilterRule(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.MIP_FILTER_RULE, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Prepaid-Tariff-Switching attribute (Type 55) with the specified raw value.
        /// </summary>
        /// <param name="value">The prepaid tariff switching data octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrepaidTariffSwitching(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.PREPAID_TARIFF_SWITCHING, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Subnet attribute (Type 68) with the specified raw value.
        /// </summary>
        /// <param name="value">The IPv6 subnet prefix octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Subnet(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.SUBNET, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Remote-Address-Table-Index attribute (Type 71) with the specified raw value.
        /// </summary>
        /// <param name="value">The remote address table index octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RemoteAddressTableIndex(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.REMOTE_ADDRESS_TABLE_INDEX, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Remote-IPv4-Addr-Octet-Count attribute (Type 72) with the specified raw value.
        /// </summary>
        /// <param name="value">The remote IPv4 address octet count octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RemoteIpv4AddrOctetCount(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.REMOTE_IPV4_ADDR_OCTET_COUNT, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Allowed-Diffserv-Marking attribute (Type 73) with the specified raw value.
        /// </summary>
        /// <param name="value">The allowed DiffServ markings octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AllowedDiffservMarking(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.ALLOWED_DIFFSERV_MARKING, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Service-Option-Profile attribute (Type 74) with the specified raw value.
        /// </summary>
        /// <param name="value">The service option profile octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceOptionProfile(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.SERVICE_OPTION_PROFILE, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Carrier-Id attribute (Type 142) with the specified raw value.
        /// </summary>
        /// <param name="value">The carrier identifier octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CarrierId(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.CARRIER_ID, value);
        }

        /// <summary>
        /// Creates a 3GPP2-Remote-IP-v6-Addr-Octet-Count attribute (Type 176) with the specified raw value.
        /// </summary>
        /// <param name="value">The remote IPv6 address octet count octets. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RemoteIpv6AddrOctetCount(byte[] value)
        {
            return CreateOctets(ThreeGpp2AttributeType.REMOTE_IPV6_ADDR_OCTET_COUNT, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified 3GPP2 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ThreeGpp2AttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified 3GPP2 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ThreeGpp2AttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a raw byte array value for the specified 3GPP2 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateOctets(ThreeGpp2AttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified 3GPP2 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(ThreeGpp2AttributeType type, IPAddress value)
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
        /// Creates a VSA with an IPv6 address value for the specified 3GPP2 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv6(ThreeGpp2AttributeType type, IPAddress value)
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