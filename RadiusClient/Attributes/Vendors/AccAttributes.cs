using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an ACC (IANA PEN 5) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.acc</c>.
    /// </summary>
    public enum AccAttributeType : byte
    {
        /// <summary>Acc-Reason-Code (Type 1). Integer. Reason code for the event.</summary>
        REASON_CODE = 1,

        /// <summary>Acc-Ccp-Option (Type 2). Integer. CCP compression option negotiated.</summary>
        CCP_OPTION = 2,

        /// <summary>Acc-Input-Errors (Type 3). Integer. Number of input errors.</summary>
        INPUT_ERRORS = 3,

        /// <summary>Acc-Output-Errors (Type 4). Integer. Number of output errors.</summary>
        OUTPUT_ERRORS = 4,

        /// <summary>Acc-Access-Partition (Type 5). String. Access partition name.</summary>
        ACCESS_PARTITION = 5,

        /// <summary>Acc-Customer-Id (Type 6). String. Customer identifier.</summary>
        CUSTOMER_ID = 6,

        /// <summary>Acc-Ip-Gateway-Pri (Type 7). IP address. Primary IP gateway address.</summary>
        IP_GATEWAY_PRI = 7,

        /// <summary>Acc-Ip-Gateway-Sec (Type 8). IP address. Secondary IP gateway address.</summary>
        IP_GATEWAY_SEC = 8,

        /// <summary>Acc-Route-Policy (Type 9). Integer. Route policy to apply.</summary>
        ROUTE_POLICY = 9,

        /// <summary>Acc-ML-MLX-Admin-State (Type 10). Integer. Multilink MLX admin state.</summary>
        ML_MLX_ADMIN_STATE = 10,

        /// <summary>Acc-ML-Call-Threshold (Type 11). Integer. Multilink call threshold.</summary>
        ML_CALL_THRESHOLD = 11,

        /// <summary>Acc-ML-Clear-Threshold (Type 12). Integer. Multilink clear threshold.</summary>
        ML_CLEAR_THRESHOLD = 12,

        /// <summary>Acc-ML-Damping-Factor (Type 13). Integer. Multilink damping factor.</summary>
        ML_DAMPING_FACTOR = 13,

        /// <summary>Acc-Tunnel-Secret (Type 14). String. Tunnel shared secret.</summary>
        TUNNEL_SECRET = 14,

        /// <summary>Acc-Clearing-Cause (Type 15). Integer. Call clearing cause code.</summary>
        CLEARING_CAUSE = 15,

        /// <summary>Acc-Clearing-Location (Type 16). Integer. Call clearing location.</summary>
        CLEARING_LOCATION = 16,

        /// <summary>Acc-Service-Profile (Type 17). String. Service profile name.</summary>
        SERVICE_PROFILE = 17,

        /// <summary>Acc-Request-Type (Type 18). Integer. Request type code.</summary>
        REQUEST_TYPE = 18,

        /// <summary>Acc-Bridging-Support (Type 19). Integer. Bridging support indicator.</summary>
        BRIDGING_SUPPORT = 19,

        /// <summary>Acc-Apsm-Oversubscribed (Type 20). Integer. APSM oversubscription indicator.</summary>
        APSM_OVERSUBSCRIBED = 20,

        /// <summary>Acc-Acct-On-Off-Reason (Type 21). Integer. Accounting on/off reason code.</summary>
        ACCT_ON_OFF_REASON = 21,

        /// <summary>Acc-Tunnel-Port (Type 22). Integer. Tunnel port number.</summary>
        TUNNEL_PORT = 22,

        /// <summary>Acc-Dns-Server-Pri (Type 23). IP address. Primary DNS server address.</summary>
        DNS_SERVER_PRI = 23,

        /// <summary>Acc-Dns-Server-Sec (Type 24). IP address. Secondary DNS server address.</summary>
        DNS_SERVER_SEC = 24,

        /// <summary>Acc-Nbns-Server-Pri (Type 25). IP address. Primary NBNS (WINS) server address.</summary>
        NBNS_SERVER_PRI = 25,

        /// <summary>Acc-Nbns-Server-Sec (Type 26). IP address. Secondary NBNS (WINS) server address.</summary>
        NBNS_SERVER_SEC = 26,

        /// <summary>Acc-Dial-Port-Index (Type 27). Integer. Dial port index.</summary>
        DIAL_PORT_INDEX = 27,

        /// <summary>Acc-Ip-Pool-Index (Type 28). Integer. IP address pool index.</summary>
        IP_POOL_INDEX = 28,

        /// <summary>Acc-Multilink-Id (Type 29). Integer. Multilink bundle identifier.</summary>
        MULTILINK_ID = 29,

        /// <summary>Acc-Num-In-Multilink (Type 30). Integer. Number of links in the multilink bundle.</summary>
        NUM_IN_MULTILINK = 30,

        /// <summary>Acc-Callback-Delay (Type 31). Integer. Callback delay in seconds.</summary>
        CALLBACK_DELAY = 31,

        /// <summary>Acc-Callback-Num-Valid (Type 32). Integer. Callback number validity indicator.</summary>
        CALLBACK_NUM_VALID = 32,

        /// <summary>Acc-Callback-Line (Type 33). Integer. Callback line number.</summary>
        CALLBACK_LINE = 33,

        /// <summary>Acc-Callback-CLID (Type 34). String. Callback Calling Line ID.</summary>
        CALLBACK_CLID = 34,

        /// <summary>Acc-Callback-Mode (Type 35). Integer. Callback mode.</summary>
        CALLBACK_MODE = 35,

        /// <summary>Acc-Callback-CBCP-Type (Type 36). Integer. CBCP callback type.</summary>
        CALLBACK_CBCP_TYPE = 36,

        /// <summary>Acc-Dialout-Auth-Mode (Type 37). Integer. Dial-out authentication mode.</summary>
        DIALOUT_AUTH_MODE = 37,

        /// <summary>Acc-Dialout-Auth-Password (Type 38). String. Dial-out authentication password.</summary>
        DIALOUT_AUTH_PASSWORD = 38,

        /// <summary>Acc-Dialout-Auth-Username (Type 39). String. Dial-out authentication username.</summary>
        DIALOUT_AUTH_USERNAME = 39,

        /// <summary>Acc-Access-Community (Type 42). Integer. Access community identifier.</summary>
        ACCESS_COMMUNITY = 42,

        /// <summary>Acc-Vpsm-Reject-Cause (Type 43). Integer. VPSM rejection cause code.</summary>
        VPSM_REJECT_CAUSE = 43,

        /// <summary>Acc-Ace-Token (Type 44). String. ACE/Server token value.</summary>
        ACE_TOKEN = 44,

        /// <summary>Acc-Ace-Token-Ttl (Type 45). Integer. ACE/Server token time-to-live.</summary>
        ACE_TOKEN_TTL = 45,

        /// <summary>Acc-Ip-Pool-Name (Type 46). String. IP address pool name.</summary>
        IP_POOL_NAME = 46,

        /// <summary>Acc-Igmp-Admin-State (Type 47). Integer. IGMP administrative state.</summary>
        IGMP_ADMIN_STATE = 47,

        /// <summary>Acc-Igmp-Version (Type 48). Integer. IGMP version.</summary>
        IGMP_VERSION = 48
    }

    /// <summary>
    /// Acc-Reason-Code attribute values (Type 1).
    /// </summary>
    public enum ACC_REASON_CODE
    {
        /// <summary>No reason specified.</summary>
        NO_REASON = 0,

        /// <summary>No retry resources available.</summary>
        NO_RETRY_RESOURCES = 1,

        /// <summary>No route available.</summary>
        NO_ROUTE = 2,

        /// <summary>Out of resources.</summary>
        OUT_OF_RESOURCES = 3,

        /// <summary>Request denied by peer.</summary>
        PEER_DENIED = 4,

        /// <summary>Request timed out waiting for peer.</summary>
        PEER_TIMEOUT = 5,

        /// <summary>Peer rejected the request.</summary>
        PEER_REJECTED = 6,

        /// <summary>Remote IP address unavailable.</summary>
        NO_REMOTE_IP = 7,

        /// <summary>Administrative action.</summary>
        ADMIN_ACTION = 8
    }

    /// <summary>
    /// Acc-Ccp-Option attribute values (Type 2).
    /// </summary>
    public enum ACC_CCP_OPTION
    {
        /// <summary>No compression.</summary>
        NONE = 0,

        /// <summary>Proprietary ACC compression.</summary>
        ACC_PROPRIETARY = 1,

        /// <summary>Stacker LZS compression (RFC 1974).</summary>
        STACKER_LZS = 2,

        /// <summary>MPPC compression (RFC 2118).</summary>
        MPPC = 3
    }

    /// <summary>
    /// Acc-Route-Policy attribute values (Type 9).
    /// </summary>
    public enum ACC_ROUTE_POLICY
    {
        /// <summary>Funnel policy — all traffic through a single route.</summary>
        FUNNEL = 1,

        /// <summary>Direct policy — route traffic directly.</summary>
        DIRECT = 2
    }

    /// <summary>
    /// Acc-ML-MLX-Admin-State attribute values (Type 10).
    /// </summary>
    public enum ACC_ML_MLX_ADMIN_STATE
    {
        /// <summary>Multilink MLX enabled.</summary>
        ENABLED = 1,

        /// <summary>Multilink MLX disabled.</summary>
        DISABLED = 2
    }

    /// <summary>
    /// Acc-Clearing-Cause attribute values (Type 15).
    /// </summary>
    public enum ACC_CLEARING_CAUSE
    {
        /// <summary>No cause specified.</summary>
        NONE = 0,

        /// <summary>Unassigned number.</summary>
        UNASSIGNED_NUMBER = 1,

        /// <summary>No route to specified transit network.</summary>
        NO_ROUTE_TO_TRANSIT = 3,

        /// <summary>No route to destination.</summary>
        NO_ROUTE_TO_DESTINATION = 5,

        /// <summary>Normal call clearing.</summary>
        NORMAL_CLEARING = 16,

        /// <summary>User busy.</summary>
        USER_BUSY = 17,

        /// <summary>No user responding.</summary>
        NO_USER_RESPONDING = 18,

        /// <summary>User alerting, no answer.</summary>
        NO_ANSWER = 19,

        /// <summary>Call rejected.</summary>
        CALL_REJECTED = 21,

        /// <summary>Number changed.</summary>
        NUMBER_CHANGED = 22,

        /// <summary>Destination out of order.</summary>
        DESTINATION_OUT_OF_ORDER = 27,

        /// <summary>Invalid number format (incomplete number).</summary>
        INVALID_NUMBER_FORMAT = 28,

        /// <summary>Normal, unspecified.</summary>
        NORMAL_UNSPECIFIED = 31,

        /// <summary>No circuit/channel available.</summary>
        NO_CIRCUIT = 34,

        /// <summary>Temporary failure.</summary>
        TEMPORARY_FAILURE = 41,

        /// <summary>Switching equipment congestion.</summary>
        SWITCHING_CONGESTION = 42,

        /// <summary>Requested circuit/channel not available.</summary>
        REQUESTED_CIRCUIT_NOT_AVAILABLE = 44,

        /// <summary>Bearer capability not authorised.</summary>
        BEARER_NOT_AUTHORIZED = 57,

        /// <summary>Bearer capability not presently available.</summary>
        BEARER_NOT_AVAILABLE = 58,

        /// <summary>Bearer capability not implemented.</summary>
        BEARER_NOT_IMPLEMENTED = 65,

        /// <summary>Incompatible destination.</summary>
        INCOMPATIBLE_DESTINATION = 88,

        /// <summary>Protocol error, unspecified.</summary>
        PROTOCOL_ERROR = 111,

        /// <summary>Interworking, unspecified.</summary>
        INTERWORKING_UNSPECIFIED = 127
    }

    /// <summary>
    /// Acc-Clearing-Location attribute values (Type 16).
    /// </summary>
    public enum ACC_CLEARING_LOCATION
    {
        /// <summary>Cleared locally.</summary>
        LOCAL = 0,

        /// <summary>Cleared remotely.</summary>
        REMOTE = 1
    }

    /// <summary>
    /// Acc-Request-Type attribute values (Type 18).
    /// </summary>
    public enum ACC_REQUEST_TYPE
    {
        /// <summary>Remote access request.</summary>
        REMOTE_ACCESS = 1,

        /// <summary>Calling line identification request.</summary>
        CLID = 2,

        /// <summary>Dial-out request.</summary>
        DIALOUT = 3
    }

    /// <summary>
    /// Acc-Bridging-Support attribute values (Type 19).
    /// </summary>
    public enum ACC_BRIDGING_SUPPORT
    {
        /// <summary>Bridging disabled.</summary>
        DISABLED = 1,

        /// <summary>Bridging enabled.</summary>
        ENABLED = 2
    }

    /// <summary>
    /// Acc-Apsm-Oversubscribed attribute values (Type 20).
    /// </summary>
    public enum ACC_APSM_OVERSUBSCRIBED
    {
        /// <summary>Not oversubscribed.</summary>
        FALSE = 1,

        /// <summary>Oversubscribed.</summary>
        TRUE = 2
    }

    /// <summary>
    /// Acc-Acct-On-Off-Reason attribute values (Type 21).
    /// </summary>
    public enum ACC_ACCT_ON_OFF_REASON
    {
        /// <summary>NAS started.</summary>
        NAS_STARTED = 0,

        /// <summary>NAS rebooted.</summary>
        NAS_REBOOTED = 1,

        /// <summary>Power failure.</summary>
        POWER_FAILURE = 2,

        /// <summary>NAS reloaded.</summary>
        NAS_RELOADED = 3,

        /// <summary>Administrative reset.</summary>
        ADMIN_RESET = 4,

        /// <summary>Administrative reboot.</summary>
        ADMIN_REBOOT = 5
    }

    /// <summary>
    /// Acc-Callback-Mode attribute values (Type 35).
    /// </summary>
    public enum ACC_CALLBACK_MODE
    {
        /// <summary>Normal callback.</summary>
        NORMAL = 0,

        /// <summary>CBCP-negotiated callback.</summary>
        CBCP = 3
    }

    /// <summary>
    /// Acc-Callback-CBCP-Type attribute values (Type 36).
    /// </summary>
    public enum ACC_CALLBACK_CBCP_TYPE
    {
        /// <summary>CBCP — no callback.</summary>
        CBCP_NONE = 1,

        /// <summary>CBCP — user-specified callback number.</summary>
        CBCP_USER_SPECIFIED = 2,

        /// <summary>CBCP — pre-specified callback number.</summary>
        CBCP_PRE_SPECIFIED = 3
    }

    /// <summary>
    /// Acc-Dialout-Auth-Mode attribute values (Type 37).
    /// </summary>
    public enum ACC_DIALOUT_AUTH_MODE
    {
        /// <summary>PAP authentication.</summary>
        PAP = 0,

        /// <summary>CHAP authentication.</summary>
        CHAP = 1,

        /// <summary>MS-CHAP authentication.</summary>
        MS_CHAP = 2,

        /// <summary>MS-CHAPv2 authentication.</summary>
        MS_CHAP_V2 = 3
    }

    /// <summary>
    /// Acc-Igmp-Admin-State attribute values (Type 47).
    /// </summary>
    public enum ACC_IGMP_ADMIN_STATE
    {
        /// <summary>IGMP enabled.</summary>
        ENABLED = 1,

        /// <summary>IGMP disabled.</summary>
        DISABLED = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing ACC (IANA PEN 5)
    /// Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.acc</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ACC's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 5</c>.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_REQUEST);
    /// packet.SetAttribute(AccAttributes.ReasonCode(ACC_REASON_CODE.NO_ROUTE));
    /// packet.SetAttribute(AccAttributes.ServiceProfile("default"));
    /// packet.SetAttribute(AccAttributes.DnsServerPri(IPAddress.Parse("8.8.8.8")));
    /// </code>
    /// </remarks>
    public static class AccAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for ACC (Ericsson).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 5;

        #region Integer Attributes

        /// <summary>
        /// Creates an Acc-Reason-Code attribute (Type 1) with the specified reason code.
        /// </summary>
        /// <param name="value">The reason code for the event. See <see cref="ACC_REASON_CODE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ReasonCode(ACC_REASON_CODE value)
        {
            return CreateInteger(AccAttributeType.REASON_CODE, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Ccp-Option attribute (Type 2) with the specified compression option.
        /// </summary>
        /// <param name="value">The CCP compression option. See <see cref="ACC_CCP_OPTION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CcpOption(ACC_CCP_OPTION value)
        {
            return CreateInteger(AccAttributeType.CCP_OPTION, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Input-Errors attribute (Type 3) with the specified error count.
        /// </summary>
        /// <param name="value">The number of input errors.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes InputErrors(int value)
        {
            return CreateInteger(AccAttributeType.INPUT_ERRORS, value);
        }

        /// <summary>
        /// Creates an Acc-Output-Errors attribute (Type 4) with the specified error count.
        /// </summary>
        /// <param name="value">The number of output errors.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes OutputErrors(int value)
        {
            return CreateInteger(AccAttributeType.OUTPUT_ERRORS, value);
        }

        /// <summary>
        /// Creates an Acc-Route-Policy attribute (Type 9) with the specified route policy.
        /// </summary>
        /// <param name="value">The route policy to apply. See <see cref="ACC_ROUTE_POLICY"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RoutePolicy(ACC_ROUTE_POLICY value)
        {
            return CreateInteger(AccAttributeType.ROUTE_POLICY, (int)value);
        }

        /// <summary>
        /// Creates an Acc-ML-MLX-Admin-State attribute (Type 10) with the specified admin state.
        /// </summary>
        /// <param name="value">The multilink MLX admin state. See <see cref="ACC_ML_MLX_ADMIN_STATE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MlMlxAdminState(ACC_ML_MLX_ADMIN_STATE value)
        {
            return CreateInteger(AccAttributeType.ML_MLX_ADMIN_STATE, (int)value);
        }

        /// <summary>
        /// Creates an Acc-ML-Call-Threshold attribute (Type 11) with the specified threshold.
        /// </summary>
        /// <param name="value">The multilink call threshold.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MlCallThreshold(int value)
        {
            return CreateInteger(AccAttributeType.ML_CALL_THRESHOLD, value);
        }

        /// <summary>
        /// Creates an Acc-ML-Clear-Threshold attribute (Type 12) with the specified threshold.
        /// </summary>
        /// <param name="value">The multilink clear threshold.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MlClearThreshold(int value)
        {
            return CreateInteger(AccAttributeType.ML_CLEAR_THRESHOLD, value);
        }

        /// <summary>
        /// Creates an Acc-ML-Damping-Factor attribute (Type 13) with the specified damping factor.
        /// </summary>
        /// <param name="value">The multilink damping factor.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MlDampingFactor(int value)
        {
            return CreateInteger(AccAttributeType.ML_DAMPING_FACTOR, value);
        }

        /// <summary>
        /// Creates an Acc-Clearing-Cause attribute (Type 15) with the specified clearing cause.
        /// </summary>
        /// <param name="value">The call clearing cause code. See <see cref="ACC_CLEARING_CAUSE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ClearingCause(ACC_CLEARING_CAUSE value)
        {
            return CreateInteger(AccAttributeType.CLEARING_CAUSE, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Clearing-Location attribute (Type 16) with the specified clearing location.
        /// </summary>
        /// <param name="value">The call clearing location. See <see cref="ACC_CLEARING_LOCATION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ClearingLocation(ACC_CLEARING_LOCATION value)
        {
            return CreateInteger(AccAttributeType.CLEARING_LOCATION, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Request-Type attribute (Type 18) with the specified request type.
        /// </summary>
        /// <param name="value">The request type code. See <see cref="ACC_REQUEST_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RequestType(ACC_REQUEST_TYPE value)
        {
            return CreateInteger(AccAttributeType.REQUEST_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Bridging-Support attribute (Type 19) with the specified bridging support indicator.
        /// </summary>
        /// <param name="value">The bridging support indicator. See <see cref="ACC_BRIDGING_SUPPORT"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BridgingSupport(ACC_BRIDGING_SUPPORT value)
        {
            return CreateInteger(AccAttributeType.BRIDGING_SUPPORT, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Apsm-Oversubscribed attribute (Type 20) with the specified oversubscription indicator.
        /// </summary>
        /// <param name="value">The APSM oversubscription indicator. See <see cref="ACC_APSM_OVERSUBSCRIBED"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ApsmOversubscribed(ACC_APSM_OVERSUBSCRIBED value)
        {
            return CreateInteger(AccAttributeType.APSM_OVERSUBSCRIBED, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Acct-On-Off-Reason attribute (Type 21) with the specified reason.
        /// </summary>
        /// <param name="value">The accounting on/off reason code. See <see cref="ACC_ACCT_ON_OFF_REASON"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctOnOffReason(ACC_ACCT_ON_OFF_REASON value)
        {
            return CreateInteger(AccAttributeType.ACCT_ON_OFF_REASON, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Tunnel-Port attribute (Type 22) with the specified port number.
        /// </summary>
        /// <param name="value">The tunnel port number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelPort(int value)
        {
            return CreateInteger(AccAttributeType.TUNNEL_PORT, value);
        }

        /// <summary>
        /// Creates an Acc-Dial-Port-Index attribute (Type 27) with the specified index.
        /// </summary>
        /// <param name="value">The dial port index.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DialPortIndex(int value)
        {
            return CreateInteger(AccAttributeType.DIAL_PORT_INDEX, value);
        }

        /// <summary>
        /// Creates an Acc-Ip-Pool-Index attribute (Type 28) with the specified pool index.
        /// </summary>
        /// <param name="value">The IP address pool index.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpPoolIndex(int value)
        {
            return CreateInteger(AccAttributeType.IP_POOL_INDEX, value);
        }

        /// <summary>
        /// Creates an Acc-Multilink-Id attribute (Type 29) with the specified bundle identifier.
        /// </summary>
        /// <param name="value">The multilink bundle identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MultilinkId(int value)
        {
            return CreateInteger(AccAttributeType.MULTILINK_ID, value);
        }

        /// <summary>
        /// Creates an Acc-Num-In-Multilink attribute (Type 30) with the specified count.
        /// </summary>
        /// <param name="value">The number of links in the multilink bundle.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NumInMultilink(int value)
        {
            return CreateInteger(AccAttributeType.NUM_IN_MULTILINK, value);
        }

        /// <summary>
        /// Creates an Acc-Callback-Delay attribute (Type 31) with the specified delay.
        /// </summary>
        /// <param name="value">The callback delay in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallbackDelay(int value)
        {
            return CreateInteger(AccAttributeType.CALLBACK_DELAY, value);
        }

        /// <summary>
        /// Creates an Acc-Callback-Num-Valid attribute (Type 32) with the specified validity indicator.
        /// </summary>
        /// <param name="value">The callback number validity indicator.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallbackNumValid(int value)
        {
            return CreateInteger(AccAttributeType.CALLBACK_NUM_VALID, value);
        }

        /// <summary>
        /// Creates an Acc-Callback-Line attribute (Type 33) with the specified line number.
        /// </summary>
        /// <param name="value">The callback line number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallbackLine(int value)
        {
            return CreateInteger(AccAttributeType.CALLBACK_LINE, value);
        }

        /// <summary>
        /// Creates an Acc-Callback-Mode attribute (Type 35) with the specified callback mode.
        /// </summary>
        /// <param name="value">The callback mode. See <see cref="ACC_CALLBACK_MODE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallbackMode(ACC_CALLBACK_MODE value)
        {
            return CreateInteger(AccAttributeType.CALLBACK_MODE, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Callback-CBCP-Type attribute (Type 36) with the specified CBCP type.
        /// </summary>
        /// <param name="value">The CBCP callback type. See <see cref="ACC_CALLBACK_CBCP_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallbackCbcpType(ACC_CALLBACK_CBCP_TYPE value)
        {
            return CreateInteger(AccAttributeType.CALLBACK_CBCP_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Dialout-Auth-Mode attribute (Type 37) with the specified authentication mode.
        /// </summary>
        /// <param name="value">The dial-out authentication mode. See <see cref="ACC_DIALOUT_AUTH_MODE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DialoutAuthMode(ACC_DIALOUT_AUTH_MODE value)
        {
            return CreateInteger(AccAttributeType.DIALOUT_AUTH_MODE, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Access-Community attribute (Type 42) with the specified community identifier.
        /// </summary>
        /// <param name="value">The access community identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessCommunity(int value)
        {
            return CreateInteger(AccAttributeType.ACCESS_COMMUNITY, value);
        }

        /// <summary>
        /// Creates an Acc-Vpsm-Reject-Cause attribute (Type 43) with the specified rejection cause.
        /// </summary>
        /// <param name="value">The VPSM rejection cause code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VpsmRejectCause(int value)
        {
            return CreateInteger(AccAttributeType.VPSM_REJECT_CAUSE, value);
        }

        /// <summary>
        /// Creates an Acc-Ace-Token-Ttl attribute (Type 45) with the specified time-to-live.
        /// </summary>
        /// <param name="value">The ACE/Server token time-to-live in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AceTokenTtl(int value)
        {
            return CreateInteger(AccAttributeType.ACE_TOKEN_TTL, value);
        }

        /// <summary>
        /// Creates an Acc-Igmp-Admin-State attribute (Type 47) with the specified admin state.
        /// </summary>
        /// <param name="value">The IGMP administrative state. See <see cref="ACC_IGMP_ADMIN_STATE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IgmpAdminState(ACC_IGMP_ADMIN_STATE value)
        {
            return CreateInteger(AccAttributeType.IGMP_ADMIN_STATE, (int)value);
        }

        /// <summary>
        /// Creates an Acc-Igmp-Version attribute (Type 48) with the specified IGMP version.
        /// </summary>
        /// <param name="value">The IGMP version number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IgmpVersion(int value)
        {
            return CreateInteger(AccAttributeType.IGMP_VERSION, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Acc-Access-Partition attribute (Type 5) with the specified partition name.
        /// </summary>
        /// <param name="value">The access partition name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessPartition(string value)
        {
            return CreateString(AccAttributeType.ACCESS_PARTITION, value);
        }

        /// <summary>
        /// Creates an Acc-Customer-Id attribute (Type 6) with the specified customer identifier.
        /// </summary>
        /// <param name="value">The customer identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CustomerId(string value)
        {
            return CreateString(AccAttributeType.CUSTOMER_ID, value);
        }

        /// <summary>
        /// Creates an Acc-Tunnel-Secret attribute (Type 14) with the specified tunnel shared secret.
        /// </summary>
        /// <param name="value">The tunnel shared secret. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelSecret(string value)
        {
            return CreateString(AccAttributeType.TUNNEL_SECRET, value);
        }

        /// <summary>
        /// Creates an Acc-Service-Profile attribute (Type 17) with the specified service profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(AccAttributeType.SERVICE_PROFILE, value);
        }

        /// <summary>
        /// Creates an Acc-Callback-CLID attribute (Type 34) with the specified Calling Line ID.
        /// </summary>
        /// <param name="value">The callback Calling Line ID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallbackClid(string value)
        {
            return CreateString(AccAttributeType.CALLBACK_CLID, value);
        }

        /// <summary>
        /// Creates an Acc-Dialout-Auth-Password attribute (Type 38) with the specified password.
        /// </summary>
        /// <param name="value">The dial-out authentication password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DialoutAuthPassword(string value)
        {
            return CreateString(AccAttributeType.DIALOUT_AUTH_PASSWORD, value);
        }

        /// <summary>
        /// Creates an Acc-Dialout-Auth-Username attribute (Type 39) with the specified username.
        /// </summary>
        /// <param name="value">The dial-out authentication username. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DialoutAuthUsername(string value)
        {
            return CreateString(AccAttributeType.DIALOUT_AUTH_USERNAME, value);
        }

        /// <summary>
        /// Creates an Acc-Ace-Token attribute (Type 44) with the specified token value.
        /// </summary>
        /// <param name="value">The ACE/Server token value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AceToken(string value)
        {
            return CreateString(AccAttributeType.ACE_TOKEN, value);
        }

        /// <summary>
        /// Creates an Acc-Ip-Pool-Name attribute (Type 46) with the specified pool name.
        /// </summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPoolName(string value)
        {
            return CreateString(AccAttributeType.IP_POOL_NAME, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Acc-Ip-Gateway-Pri attribute (Type 7) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The primary IP gateway address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes IpGatewayPri(IPAddress value)
        {
            return CreateIpv4(AccAttributeType.IP_GATEWAY_PRI, value);
        }

        /// <summary>
        /// Creates an Acc-Ip-Gateway-Sec attribute (Type 8) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The secondary IP gateway address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes IpGatewaySec(IPAddress value)
        {
            return CreateIpv4(AccAttributeType.IP_GATEWAY_SEC, value);
        }

        /// <summary>
        /// Creates an Acc-Dns-Server-Pri attribute (Type 23) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The primary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes DnsServerPri(IPAddress value)
        {
            return CreateIpv4(AccAttributeType.DNS_SERVER_PRI, value);
        }

        /// <summary>
        /// Creates an Acc-Dns-Server-Sec attribute (Type 24) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The secondary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes DnsServerSec(IPAddress value)
        {
            return CreateIpv4(AccAttributeType.DNS_SERVER_SEC, value);
        }

        /// <summary>
        /// Creates an Acc-Nbns-Server-Pri attribute (Type 25) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The primary NBNS (WINS) server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes NbnsServerPri(IPAddress value)
        {
            return CreateIpv4(AccAttributeType.NBNS_SERVER_PRI, value);
        }

        /// <summary>
        /// Creates an Acc-Nbns-Server-Sec attribute (Type 26) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The secondary NBNS (WINS) server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes NbnsServerSec(IPAddress value)
        {
            return CreateIpv4(AccAttributeType.NBNS_SERVER_SEC, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified ACC attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AccAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified ACC attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AccAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified ACC attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(AccAttributeType type, IPAddress value)
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