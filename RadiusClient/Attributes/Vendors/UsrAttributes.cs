using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a US Robotics / 3Com (IANA PEN 429) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.usr</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// US Robotics (acquired by 3Com in 1997) produced modems, remote access servers
    /// (Total Control, HiPer), and network access equipment widely deployed by ISPs
    /// and enterprises for dial-up networking.
    /// </para>
    /// <para>
    /// USR uses a non-standard VSA sub-attribute encoding (<c>format=4,0</c>): 4-byte
    /// big-endian Vendor-Type with no Vendor-Length field. All type codes are 32-bit
    /// values and require <see cref="VendorSpecificFormat.Type4Len0"/>.
    /// </para>
    /// </remarks>
    public enum UsrAttributeType : uint
    {
        /// <summary>USR-Default-DTE-Data-Rate (0x9000). Integer.</summary>
        DEFAULT_DTE_DATA_RATE = 0x9000,

        /// <summary>USR-Initial-Rx-Link-Data-Rate (0x9002). Integer.</summary>
        INITIAL_RX_LINK_DATA_RATE = 0x9002,

        /// <summary>USR-Final-Rx-Link-Data-Rate (0x9003). Integer.</summary>
        FINAL_RX_LINK_DATA_RATE = 0x9003,

        /// <summary>USR-Initial-Tx-Link-Data-Rate (0x9006). Integer.</summary>
        INITIAL_TX_LINK_DATA_RATE = 0x9006,

        /// <summary>USR-Final-Tx-Link-Data-Rate (0x9007). Integer.</summary>
        FINAL_TX_LINK_DATA_RATE = 0x9007,

        /// <summary>USR-Chassis-Temperature (0x9008). Integer.</summary>
        CHASSIS_TEMPERATURE = 0x9008,

        /// <summary>USR-Sync-Async-Mode (0x9009). Integer.</summary>
        SYNC_ASYNC_MODE = 0x9009,

        /// <summary>USR-Originate-Answer-Mode (0x900A). Integer.</summary>
        ORIGINATE_ANSWER_MODE = 0x900A,

        /// <summary>USR-Modulation-Type (0x900B). Integer.</summary>
        MODULATION_TYPE = 0x900B,

        /// <summary>USR-Initial-Modulation-Type (0x900C). Integer.</summary>
        INITIAL_MODULATION_TYPE = 0x900C,

        /// <summary>USR-Connect-Term-Reason (0x9050). Integer.</summary>
        CONNECT_TERM_REASON = 0x9050,

        /// <summary>USR-Failure-to-Connect-Reason (0x9051). Integer.</summary>
        FAILURE_TO_CONNECT_REASON = 0x9051,

        /// <summary>USR-Equalization-Type (0x900D). Integer.</summary>
        EQUALIZATION_TYPE = 0x900D,

        /// <summary>USR-Fallback-Enabled (0x900E). Integer.</summary>
        FALLBACK_ENABLED = 0x900E,

        /// <summary>USR-Characters-Sent (0x9010). Integer.</summary>
        CHARACTERS_SENT = 0x9010,

        /// <summary>USR-Characters-Received (0x9011). Integer.</summary>
        CHARACTERS_RECEIVED = 0x9011,

        /// <summary>USR-Blocks-Sent (0x9012). Integer.</summary>
        BLOCKS_SENT = 0x9012,

        /// <summary>USR-Blocks-Received (0x9013). Integer.</summary>
        BLOCKS_RECEIVED = 0x9013,

        /// <summary>USR-Blocks-Resent (0x9014). Integer.</summary>
        BLOCKS_RESENT = 0x9014,

        /// <summary>USR-Retrains-Requested (0x9015). Integer.</summary>
        RETRAINS_REQUESTED = 0x9015,

        /// <summary>USR-Retrains-Granted (0x9016). Integer.</summary>
        RETRAINS_GRANTED = 0x9016,

        /// <summary>USR-Line-Reversals (0x9017). Integer.</summary>
        LINE_REVERSALS = 0x9017,

        /// <summary>USR-Number-Of-Characters-Lost (0x9018). Integer.</summary>
        NUMBER_OF_CHARACTERS_LOST = 0x9018,

        /// <summary>USR-Number-Of-Blers (0x9019). Integer.</summary>
        NUMBER_OF_BLERS = 0x9019,

        /// <summary>USR-Number-Of-Link-Timeouts (0x901A). Integer.</summary>
        NUMBER_OF_LINK_TIMEOUTS = 0x901A,

        /// <summary>USR-Number-Of-Fallbacks (0x901B). Integer.</summary>
        NUMBER_OF_FALLBACKS = 0x901B,

        /// <summary>USR-Number-Of-Upshifts (0x901C). Integer.</summary>
        NUMBER_OF_UPSHIFTS = 0x901C,

        /// <summary>USR-Number-Of-Link-NAKs (0x901D). Integer.</summary>
        NUMBER_OF_LINK_NAKS = 0x901D,

        /// <summary>USR-DTR-False-Timeout (0x9023). Integer.</summary>
        DTR_FALSE_TIMEOUT = 0x9023,

        /// <summary>USR-Fallback-Limit (0x9024). Integer.</summary>
        FALLBACK_LIMIT = 0x9024,

        /// <summary>USR-Block-Error-Count-Limit (0x9025). Integer.</summary>
        BLOCK_ERROR_COUNT_LIMIT = 0x9025,

        /// <summary>USR-DTR-True-Timeout (0x9026). Integer.</summary>
        DTR_TRUE_TIMEOUT = 0x9026,

        /// <summary>USR-Security-Login-Limit (0x9027). Integer.</summary>
        SECURITY_LOGIN_LIMIT = 0x9027,

        /// <summary>USR-Security-Login-Delay (0x9028). Integer.</summary>
        SECURITY_LOGIN_DELAY = 0x9028,

        /// <summary>USR-Card-Type (0x9029). Integer.</summary>
        CARD_TYPE = 0x9029,

        /// <summary>USR-Connect-Speed (0x9023). Integer.</summary>
        CONNECT_SPEED = 0x9030,

        /// <summary>USR-Simplified-MNP-Levels (0x9031). Integer.</summary>
        SIMPLIFIED_MNP_LEVELS = 0x9031,

        /// <summary>USR-Simplified-V42bis-Usage (0x9032). Integer.</summary>
        SIMPLIFIED_V42BIS_USAGE = 0x9032,

        /// <summary>USR-Number-Of-Link-Timeouts-2 (0x9033). Integer.</summary>
        NUMBER_OF_LINK_TIMEOUTS_2 = 0x9033,

        /// <summary>USR-IDS0-Call-Type (0x9034). Integer.</summary>
        IDS0_CALL_TYPE = 0x9034,

        /// <summary>USR-Bearer-Capabilities (0x9035). Integer.</summary>
        BEARER_CAPABILITIES = 0x9035,

        /// <summary>USR-Speed-Of-Connection (0x9036). Integer.</summary>
        SPEED_OF_CONNECTION = 0x9036,

        /// <summary>USR-Max-Link-Noise (0x9037). Integer.</summary>
        MAX_LINK_NOISE = 0x9037,

        /// <summary>USR-Default-DTE-Data-Rate-2 (0x9038). Integer.</summary>
        DEFAULT_DTE_DATA_RATE_2 = 0x9038,

        /// <summary>USR-Reply-Message (0x9039). String.</summary>
        REPLY_MESSAGE = 0x9039,

        /// <summary>USR-Num-Of-Rings-Limit (0x903A). Integer.</summary>
        NUM_OF_RINGS_LIMIT = 0x903A,

        /// <summary>USR-Alternate-Number (0x903B). String.</summary>
        ALTERNATE_NUMBER = 0x903B,

        /// <summary>USR-Force-Rev (0x903C). Integer.</summary>
        FORCE_REV = 0x903C,

        /// <summary>USR-Modem-Training-Time (0x903D). Integer.</summary>
        MODEM_TRAINING_TIME = 0x903D,

        /// <summary>USR-S7-Timer (0x903E). Integer.</summary>
        S7_TIMER = 0x903E,

        /// <summary>USR-CDL-Modem-Type (0x9040). Integer.</summary>
        CDL_MODEM_TYPE = 0x9040,

        /// <summary>USR-CDL-Last-Status-Change (0x9041). Integer.</summary>
        CDL_LAST_STATUS_CHANGE = 0x9041,

        /// <summary>USR-CDL-Signal-Quality (0x9042). Integer.</summary>
        CDL_SIGNAL_QUALITY = 0x9042,

        /// <summary>USR-Receive-Baud-Rate (0x9043). Integer.</summary>
        RECEIVE_BAUD_RATE = 0x9043,

        /// <summary>USR-Transmit-Baud-Rate (0x9044). Integer.</summary>
        TRANSMIT_BAUD_RATE = 0x9044,

        /// <summary>USR-S10-Timer (0x9046). Integer.</summary>
        S10_TIMER = 0x9046,

        /// <summary>USR-Log-Filter-Packets (0x9054). String.</summary>
        LOG_FILTER_PACKETS = 0x9054,

        /// <summary>USR-Event-Id (0xBE49). Integer.</summary>
        EVENT_ID = 0xBE49,

        /// <summary>USR-Event-Date-Time (0xBE4A). String.</summary>
        EVENT_DATE_TIME = 0xBE4A,

        /// <summary>USR-Call-Start-Date-Time (0xBE4B). String.</summary>
        CALL_START_DATE_TIME = 0xBE4B,

        /// <summary>USR-Call-End-Date-Time (0xBE4C). String.</summary>
        CALL_END_DATE_TIME = 0xBE4C,

        /// <summary>USR-Default-Gateway (0xBE4D). IP address.</summary>
        DEFAULT_GATEWAY = 0xBE4D,

        /// <summary>USR-Framed-IP-Address-Pool-Name (0xBE6C). String.</summary>
        FRAMED_IP_ADDRESS_POOL_NAME = 0xBE6C,

        /// <summary>USR-MP-EDO (0xBE6E). String.</summary>
        MP_EDO = 0xBE6E,

        /// <summary>USR-Local-Framed-IP-Addr (0xBE6F). IP address.</summary>
        LOCAL_FRAMED_IP_ADDR = 0xBE6F,

        /// <summary>USR-Framed-IPX-Route (0xBE70). String.</summary>
        FRAMED_IPX_ROUTE = 0xBE70,

        /// <summary>USR-MPIP-Tunnel-Originator (0xBE71). IP address.</summary>
        MPIP_TUNNEL_ORIGINATOR = 0xBE71,

        /// <summary>USR-Primary-DNS-Server (0xBE72). IP address.</summary>
        PRIMARY_DNS_SERVER = 0xBE72,

        /// <summary>USR-Secondary-DNS-Server (0xBE73). IP address.</summary>
        SECONDARY_DNS_SERVER = 0xBE73,

        /// <summary>USR-Primary-NBNS-Server (0xBE74). IP address.</summary>
        PRIMARY_NBNS_SERVER = 0xBE74,

        /// <summary>USR-Secondary-NBNS-Server (0xBE75). IP address.</summary>
        SECONDARY_NBNS_SERVER = 0xBE75,

        /// <summary>USR-Syslog-Tap (0xBE76). Integer.</summary>
        SYSLOG_TAP = 0xBE76,

        /// <summary>USR-Log-Filter-Callstart (0xBE77). String.</summary>
        LOG_FILTER_CALLSTART = 0xBE77,

        /// <summary>USR-Log-Filter-Callend (0xBE78). String.</summary>
        LOG_FILTER_CALLEND = 0xBE78,

        /// <summary>USR-Log-Filter-Function (0xBE79). String.</summary>
        LOG_FILTER_FUNCTION = 0xBE79,

        /// <summary>USR-Chassis-Call-Slot (0xBE7A). Integer.</summary>
        CHASSIS_CALL_SLOT = 0xBE7A,

        /// <summary>USR-Chassis-Call-Span (0xBE7B). Integer.</summary>
        CHASSIS_CALL_SPAN = 0xBE7B,

        /// <summary>USR-Chassis-Call-Channel (0xBE7C). Integer.</summary>
        CHASSIS_CALL_CHANNEL = 0xBE7C,

        /// <summary>USR-Keypress-Timeout (0xBE7D). Integer.</summary>
        KEYPRESS_TIMEOUT = 0xBE7D,

        /// <summary>USR-Unauthenticated-Time (0xBE7E). Integer.</summary>
        UNAUTHENTICATED_TIME = 0xBE7E,

        /// <summary>USR-VPN-Encryptor (0xBE7F). String.</summary>
        VPN_ENCRYPTOR = 0xBE7F,

        /// <summary>USR-VPN-GW-Location-Id (0xBE80). String.</summary>
        VPN_GW_LOCATION_ID = 0xBE80,

        /// <summary>USR-Re-Chap-Timeout (0xBE81). Integer.</summary>
        RE_CHAP_TIMEOUT = 0xBE81,

        /// <summary>USR-CCP-Algorithm (0xBE82). Integer.</summary>
        CCP_ALGORITHM = 0xBE82,

        /// <summary>USR-ACCM-Type (0xBE83). Integer.</summary>
        ACCM_TYPE = 0xBE83,

        /// <summary>USR-Connect-Time-Limit (0xBE84). Integer.</summary>
        CONNECT_TIME_LIMIT = 0xBE84,

        /// <summary>USR-Startup-Expression (0xBE85). String.</summary>
        STARTUP_EXPRESSION = 0xBE85,

        /// <summary>USR-Bacp-Enable (0xBE86). Integer.</summary>
        BACP_ENABLE = 0xBE86,

        /// <summary>USR-DHCP-Maximum-Leases (0xBE87). Integer.</summary>
        DHCP_MAXIMUM_LEASES = 0xBE87,

        /// <summary>USR-Primary-DHCP-Server (0xBE88). IP address.</summary>
        PRIMARY_DHCP_SERVER = 0xBE88,

        /// <summary>USR-Secondary-DHCP-Server (0xBE89). IP address.</summary>
        SECONDARY_DHCP_SERVER = 0xBE89,

        /// <summary>USR-DHCP-Pool-Number (0xBE8A). Integer.</summary>
        DHCP_POOL_NUMBER = 0xBE8A,

        /// <summary>USR-MIC (0xBE8B). String.</summary>
        MIC = 0xBE8B,

        /// <summary>USR-MIC-2 (0xBE8C). String.</summary>
        MIC_2 = 0xBE8C,

        /// <summary>USR-Multicast-Proxy (0xBE8D). Integer.</summary>
        MULTICAST_PROXY = 0xBE8D,

        /// <summary>USR-Multicast-Forwarding (0xBE8E). Integer.</summary>
        MULTICAST_FORWARDING = 0xBE8E,

        /// <summary>USR-IGMP-Query-Interval (0xBE8F). Integer.</summary>
        IGMP_QUERY_INTERVAL = 0xBE8F,

        /// <summary>USR-IGMP-Maximum-Response-Time (0xBE90). Integer.</summary>
        IGMP_MAXIMUM_RESPONSE_TIME = 0xBE90,

        /// <summary>USR-IGMP-Robustness (0xBE91). Integer.</summary>
        IGMP_ROBUSTNESS = 0xBE91,

        /// <summary>USR-IGMP-Version (0xBE92). Integer.</summary>
        IGMP_VERSION = 0xBE92,

        /// <summary>USR-Rad-Multicast-Routing-Ttl (0xBE93). Integer.</summary>
        RAD_MULTICAST_ROUTING_TTL = 0xBE93,

        /// <summary>USR-Rad-Multicast-Routing-RtLim (0xBE94). Integer.</summary>
        RAD_MULTICAST_ROUTING_RTLIM = 0xBE94,

        /// <summary>USR-Rad-Multicast-Routing-Proto (0xBE95). Integer.</summary>
        RAD_MULTICAST_ROUTING_PROTO = 0xBE95,

        /// <summary>USR-Rad-Multicast-Routing-Bound (0xBE96). String.</summary>
        RAD_MULTICAST_ROUTING_BOUND = 0xBE96,

        /// <summary>USR-MN-HA-Shared-Key (0xBE97). String.</summary>
        MN_HA_SHARED_KEY = 0xBE97,

        /// <summary>USR-IP-Address-Pool-Name (0xBE98). String.</summary>
        IP_ADDRESS_POOL_NAME = 0xBE98,

        /// <summary>USR-PPP-Log-Mask (0xBE99). Integer.</summary>
        PPP_LOG_MASK = 0xBE99,

        /// <summary>USR-Tunnel-Switch-Endpoint (0xBE9A). String.</summary>
        TUNNEL_SWITCH_ENDPOINT = 0xBE9A,

        /// <summary>USR-DS0-Byte (0xBE9B). Integer.</summary>
        DS0_BYTE = 0xBE9B,

        /// <summary>USR-DS0 (0xBE9C). Integer.</summary>
        DS0 = 0xBE9C,

        /// <summary>USR-Gateway-IP-Address (0xBE9D). IP address.</summary>
        GATEWAY_IP_ADDRESS = 0xBE9D,

        /// <summary>USR-PW-USR-IFilter (0xBE9E). String.</summary>
        PW_USR_IFILTER = 0xBE9E,

        /// <summary>USR-PW-USR-OFilter (0xBE9F). String.</summary>
        PW_USR_OFILTER = 0xBE9F,

        /// <summary>USR-QoS-Queue (0xBEA0). Integer.</summary>
        QOS_QUEUE = 0xBEA0,

        /// <summary>USR-ET-Bridge-Name (0xBEA1). String.</summary>
        ET_BRIDGE_NAME = 0xBEA1,

        /// <summary>USR-ET-Bridge-Id (0xBEA2). Integer.</summary>
        ET_BRIDGE_ID = 0xBEA2,

        /// <summary>USR-ET-Bridge-Port-Session-No (0xBEA3). Integer.</summary>
        ET_BRIDGE_PORT_SESSION_NO = 0xBEA3,

        /// <summary>USR-Last-Number-Dialed-Out (0xBE45). String.</summary>
        LAST_NUMBER_DIALED_OUT = 0xBE45,

        /// <summary>USR-Last-Number-Dialed-In-DNIS (0xBE46). String.</summary>
        LAST_NUMBER_DIALED_IN_DNIS = 0xBE46,

        /// <summary>USR-Last-Callers-Number-ANI (0xBE47). String.</summary>
        LAST_CALLERS_NUMBER_ANI = 0xBE47,

        /// <summary>USR-Channel (0xBE48). Integer.</summary>
        CHANNEL = 0xBE48,

        /// <summary>USR-Port-Number (0xBE57). Integer.</summary>
        PORT_NUMBER = 0xBE57,

        /// <summary>USR-Slot-Number (0xBE58). Integer.</summary>
        SLOT_NUMBER = 0xBE58,

        /// <summary>USR-Chassis-Temp-Threshold (0xBE55). Integer.</summary>
        CHASSIS_TEMP_THRESHOLD = 0xBE55,

        /// <summary>USR-Call-Event-Code (0xBE56). Integer.</summary>
        CALL_EVENT_CODE = 0xBE56,

        /// <summary>USR-DS1-Facility (0xBE59). Integer.</summary>
        DS1_FACILITY = 0xBE59,

        /// <summary>USR-Physical-State (0xBE5A). Integer.</summary>
        PHYSICAL_STATE = 0xBE5A,

        /// <summary>USR-Packet-Bus-Session (0xBE5B). Integer.</summary>
        PACKET_BUS_SESSION = 0xBE5B,

        /// <summary>USR-Server-Time (0xBE5C). Integer.</summary>
        SERVER_TIME = 0xBE5C,

        /// <summary>USR-Channel-Connected-To (0xBE5D). Integer.</summary>
        CHANNEL_CONNECTED_TO = 0xBE5D,

        /// <summary>USR-Slot-Connected-To (0xBE5E). Integer.</summary>
        SLOT_CONNECTED_TO = 0xBE5E,

        /// <summary>USR-Device-Connected-To (0xBE5F). Integer.</summary>
        DEVICE_CONNECTED_TO = 0xBE5F,

        /// <summary>USR-NFAS-ID (0xBE60). Integer.</summary>
        NFAS_ID = 0xBE60,

        /// <summary>USR-Q931-Call-Reference (0xBE61). Integer.</summary>
        Q931_CALL_REFERENCE = 0xBE61,

        /// <summary>USR-Call-Arrival-in-GMT (0xBE62). Integer.</summary>
        CALL_ARRIVAL_IN_GMT = 0xBE62,

        /// <summary>USR-Call-Connect-in-GMT (0xBE63). Integer.</summary>
        CALL_CONNECT_IN_GMT = 0xBE63,

        /// <summary>USR-Call-Terminate-in-GMT (0xBE64). Integer.</summary>
        CALL_TERMINATE_IN_GMT = 0xBE64,

        /// <summary>USR-IDS0-Call-Type-2 (0xBE65). Integer.</summary>
        IDS0_CALL_TYPE_2 = 0xBE65,

        /// <summary>USR-Call-Reference-Number (0xBE66). Integer.</summary>
        CALL_REFERENCE_NUMBER = 0xBE66,

        /// <summary>USR-CHAP-Challenge (0xBE67). String.</summary>
        CHAP_CHALLENGE = 0xBE67,

        /// <summary>USR-PPP-NCP-Type (0xBE68). Integer.</summary>
        PPP_NCP_TYPE = 0xBE68,

        /// <summary>USR-Tunnel-Authentication (0xBEA4). String.</summary>
        TUNNEL_AUTHENTICATION = 0xBEA4,

        /// <summary>USR-Index (0xBEA5). String.</summary>
        INDEX = 0xBEA5,

        /// <summary>USR-Cutoff (0xBEA6). String.</summary>
        CUTOFF = 0xBEA6,

        /// <summary>USR-RMMIE-Status (0xBEA7). Integer.</summary>
        RMMIE_STATUS = 0xBEA7,

        /// <summary>USR-RMMIE-x2-Status (0xBEA8). Integer.</summary>
        RMMIE_X2_STATUS = 0xBEA8,

        /// <summary>USR-RMMIE-Planned-Disconnect (0xBEA9). Integer.</summary>
        RMMIE_PLANNED_DISCONNECT = 0xBEA9,

        /// <summary>USR-RMMIE-Last-Update-Time (0xBEAA). Integer.</summary>
        RMMIE_LAST_UPDATE_TIME = 0xBEAA,

        /// <summary>USR-RMMIE-Last-Update-Event (0xBEAB). Integer.</summary>
        RMMIE_LAST_UPDATE_EVENT = 0xBEAB,

        /// <summary>USR-RMMIE-Rcv-Tot-PwrLvl (0xBEAC). Integer.</summary>
        RMMIE_RCV_TOT_PWRLVL = 0xBEAC,

        /// <summary>USR-RMMIE-Rcv-PwrLvl-3300Hz (0xBEAD). Integer.</summary>
        RMMIE_RCV_PWRLVL_3300HZ = 0xBEAD,

        /// <summary>USR-RMMIE-Rcv-PwrLvl-3750Hz (0xBEAE). Integer.</summary>
        RMMIE_RCV_PWRLVL_3750HZ = 0xBEAE,

        /// <summary>USR-RMMIE-PwrLvl-NearEcho-Canc (0xBEAF). Integer.</summary>
        RMMIE_PWRLVL_NEARECHO_CANC = 0xBEAF,

        /// <summary>USR-RMMIE-PwrLvl-FarEcho-Canc (0xBEB0). Integer.</summary>
        RMMIE_PWRLVL_FARECHO_CANC = 0xBEB0,

        /// <summary>USR-RMMIE-PwrLvl-Noise-Lvl (0xBEB1). Integer.</summary>
        RMMIE_PWRLVL_NOISE_LVL = 0xBEB1,

        /// <summary>USR-RMMIE-PwrLvl-Xmit-Lvl (0xBEB2). Integer.</summary>
        RMMIE_PWRLVL_XMIT_LVL = 0xBEB2,

        /// <summary>USR-Mbi-Ct-PRI-Card-Slot (0xBEB8). Integer.</summary>
        MBI_CT_PRI_CARD_SLOT = 0xBEB8,

        /// <summary>USR-Mbi-Ct-TDM-Time-Slot (0xBEB9). Integer.</summary>
        MBI_CT_TDM_TIME_SLOT = 0xBEB9,

        /// <summary>USR-Mbi-Ct-PRI-Card-Span-Line (0xBEBA). Integer.</summary>
        MBI_CT_PRI_CARD_SPAN_LINE = 0xBEBA,

        /// <summary>USR-Mbi-Ct-BChannel-Used (0xBEBB). Integer.</summary>
        MBI_CT_BCHANNEL_USED = 0xBEBB,

        /// <summary>USR-Physical-Port-Number (0xBEBC). Integer.</summary>
        PHYSICAL_PORT_NUMBER = 0xBEBC,

        /// <summary>USR-Tunnel-Security (0xBE69). Integer.</summary>
        TUNNEL_SECURITY = 0xBE69,

        /// <summary>USR-Port-Tap (0xBE6A). Integer.</summary>
        PORT_TAP = 0xBE6A,

        /// <summary>USR-Port-Tap-Format (0xBE6B). Integer.</summary>
        PORT_TAP_FORMAT = 0xBE6B,

        /// <summary>USR-Port-Tap-Output (0xBE6D). Integer.</summary>
        PORT_TAP_OUTPUT = 0xBE6D,

        /// <summary>USR-Port-Tap-Facility (0xBEBD). Integer.</summary>
        PORT_TAP_FACILITY = 0xBEBD,

        /// <summary>USR-Port-Tap-Priority (0xBEBE). Integer.</summary>
        PORT_TAP_PRIORITY = 0xBEBE,

        /// <summary>USR-RMMIE-Num-Of-Updates (0xBEB3). Integer.</summary>
        RMMIE_NUM_OF_UPDATES = 0xBEB3,

        /// <summary>USR-RMMIE-Manufacturer-ID (0xBEB4). Integer.</summary>
        RMMIE_MANUFACTURER_ID = 0xBEB4,

        /// <summary>USR-RMMIE-Product-Code (0xBEB5). String.</summary>
        RMMIE_PRODUCT_CODE = 0xBEB5,

        /// <summary>USR-RMMIE-Serial-Number (0xBEB6). String.</summary>
        RMMIE_SERIAL_NUMBER = 0xBEB6,

        /// <summary>USR-RMMIE-Firmware-Version (0xBEB7). String.</summary>
        RMMIE_FIRMWARE_VERSION = 0xBEB7,

        /// <summary>USR-RMMIE-Firmware-Build-Date (0xBEBF). String.</summary>
        RMMIE_FIRMWARE_BUILD_DATE = 0xBEBF,

        /// <summary>USR-Mobile-IP-Home-Agent-Address (0xBEC0). IP address.</summary>
        MOBILE_IP_HOME_AGENT_ADDRESS = 0xBEC0,

        /// <summary>USR-Tunnel-Session-Id (0xBEC1). String.</summary>
        TUNNEL_SESSION_ID = 0xBEC1,

        /// <summary>USR-PPP-Bridge-NCP-Type (0xBEC2). Integer.</summary>
        PPP_BRIDGE_NCP_TYPE = 0xBEC2,

        /// <summary>USR-Local-IP-Address (0xBEC3). IP address.</summary>
        LOCAL_IP_ADDRESS = 0xBEC3,

        /// <summary>USR-Dsx1-Line-Mode (0xBEC4). Integer.</summary>
        DSX1_LINE_MODE = 0xBEC4,

        /// <summary>USR-Auth-Mode (0xBEC5). Integer.</summary>
        AUTH_MODE = 0xBEC5
    }

    /// <summary>
    /// USR-Connect-Term-Reason attribute values (0x9050).
    /// </summary>
    public enum USR_CONNECT_TERM_REASON
    {
        /// <summary>No reason available.</summary>
        NO_REASON = 0,
        /// <summary>User request.</summary>
        USER_REQUEST = 1,
        /// <summary>Lost carrier.</summary>
        LOST_CARRIER = 2,
        /// <summary>No carrier detected.</summary>
        NO_CARRIER = 3,
        /// <summary>No answer detect.</summary>
        NO_ANSWER_DETECT = 4,
        /// <summary>Invalid destination.</summary>
        INVALID_DESTINATION = 5,
        /// <summary>Port disabled.</summary>
        PORT_DISABLED = 6,
        /// <summary>Session timeout.</summary>
        SESSION_TIMEOUT = 7,
        /// <summary>NAS request.</summary>
        NAS_REQUEST = 8,
        /// <summary>NAS reboot.</summary>
        NAS_REBOOT = 9,
        /// <summary>Port unneeded.</summary>
        PORT_UNNEEDED = 10,
        /// <summary>Port pre-empted.</summary>
        PORT_PREEMPTED = 11,
        /// <summary>Port suspended.</summary>
        PORT_SUSPENDED = 12,
        /// <summary>Service unavailable.</summary>
        SERVICE_UNAVAILABLE = 13,
        /// <summary>Callback.</summary>
        CALLBACK = 14,
        /// <summary>User error.</summary>
        USER_ERROR = 15,
        /// <summary>Host request.</summary>
        HOST_REQUEST = 16,
        /// <summary>Retransmit limit reached.</summary>
        RETRANSMIT_LIMIT = 17,
        /// <summary>Negotiation failure.</summary>
        NEGOTIATION_FAILURE = 18,
        /// <summary>Peer not responding.</summary>
        PEER_NOT_RESPONDING = 19,
        /// <summary>LCP termination received.</summary>
        LCP_TERMINATION_RECEIVED = 20,
        /// <summary>Physical disconnect.</summary>
        PHYSICAL_DISCONNECT = 21,
        /// <summary>Login mode failed.</summary>
        LOGIN_MODE_FAILED = 22,
        /// <summary>Dtr drop timeout.</summary>
        DTR_DROP_TIMEOUT = 23
    }

    /// <summary>
    /// USR-Sync-Async-Mode attribute values (0x9009).
    /// </summary>
    public enum USR_SYNC_ASYNC_MODE
    {
        /// <summary>Async mode.</summary>
        ASYNC = 1,
        /// <summary>Sync mode.</summary>
        SYNC = 2
    }

    /// <summary>
    /// USR-Originate-Answer-Mode attribute values (0x900A).
    /// </summary>
    public enum USR_ORIGINATE_ANSWER_MODE
    {
        /// <summary>Originate in originate mode.</summary>
        ORIGINATE = 0,
        /// <summary>Originate in answer mode.</summary>
        ANSWER = 1
    }

    /// <summary>
    /// USR-IDS0-Call-Type attribute values (0x9034).
    /// </summary>
    public enum USR_IDS0_CALL_TYPE
    {
        /// <summary>Analog call.</summary>
        ANALOG = 0,
        /// <summary>Digital call.</summary>
        DIGITAL = 1
    }

    /// <summary>
    /// USR-Syslog-Tap attribute values (0xBE76).
    /// </summary>
    public enum USR_SYSLOG_TAP
    {
        /// <summary>Syslog tap at global level.</summary>
        GLOBAL = 0,
        /// <summary>Syslog tap at session level.</summary>
        SESSION = 1
    }

    /// <summary>
    /// USR-CCP-Algorithm attribute values (0xBE82).
    /// </summary>
    public enum USR_CCP_ALGORITHM
    {
        /// <summary>No compression.</summary>
        NONE = 0,
        /// <summary>STAC LZS compression.</summary>
        STAC = 1,
        /// <summary>Microsoft Point-to-Point Compression.</summary>
        MPPC = 2
    }

    /// <summary>
    /// USR-ACCM-Type attribute values (0xBE83).
    /// </summary>
    public enum USR_ACCM_TYPE
    {
        /// <summary>Dynamic ACCM.</summary>
        DYNAMIC = 0,
        /// <summary>Static ACCM.</summary>
        STATIC = 1
    }

    /// <summary>
    /// USR-Auth-Mode attribute values (0xBEC5).
    /// </summary>
    public enum USR_AUTH_MODE
    {
        /// <summary>PAP authentication.</summary>
        PAP = 0,
        /// <summary>CHAP authentication.</summary>
        CHAP = 1,
        /// <summary>CHAP-PAP authentication.</summary>
        CHAP_PAP = 2,
        /// <summary>No authentication.</summary>
        NONE = 3,
        /// <summary>EAP authentication.</summary>
        EAP = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing US Robotics / 3Com
    /// (IANA PEN 429) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.usr</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// USR uses a non-standard VSA sub-attribute encoding (<c>format=4,0</c>): the
    /// Vendor-Type field is 4 bytes (big-endian) with no Vendor-Length field. All
    /// attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 429</c> and
    /// <see cref="VendorSpecificFormat.Type4Len0"/>.
    /// </para>
    /// <para>
    /// These attributes are used by US Robotics / 3Com Total Control and HiPer
    /// remote access servers for RADIUS-based call detail records including
    /// called/calling number identification (DNIS/ANI), call timestamps, modem
    /// link rates, connect speed, channel/port/slot identification, bearer
    /// capabilities, connection termination reasons, IP address pool assignment,
    /// DNS/NBNS/DHCP server provisioning, VPN tunnel configuration, compression
    /// settings, multicast routing, RMMIE modem diagnostics, and extensive
    /// modem statistics.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(UsrAttributes.LastNumberDialedInDnis("+15551234567"));
    /// packet.SetAttribute(UsrAttributes.LastCallersNumberAni("+15559876543"));
    /// packet.SetAttribute(UsrAttributes.ConnectSpeed(56000));
    /// packet.SetAttribute(UsrAttributes.ConnectTermReason(USR_CONNECT_TERM_REASON.USER_REQUEST));
    /// packet.SetAttribute(UsrAttributes.FramedIpAddressPoolName("dialup-pool"));
    /// packet.SetAttribute(UsrAttributes.PrimaryDnsServer(IPAddress.Parse("10.0.0.1")));
    /// </code>
    /// </remarks>
    public static class UsrAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for US Robotics / 3Com.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 429;

        #region Integer Attributes

        /// <summary>Creates a USR-Default-DTE-Data-Rate attribute (0x9000).</summary>
        /// <param name="value">The default DTE data rate in bps.</param>
        public static VendorSpecificAttributes DefaultDteDataRate(int value) => CreateInteger(UsrAttributeType.DEFAULT_DTE_DATA_RATE, value);

        /// <summary>Creates a USR-Initial-Rx-Link-Data-Rate attribute (0x9002).</summary>
        /// <param name="value">The initial receive link data rate in bps.</param>
        public static VendorSpecificAttributes InitialRxLinkDataRate(int value) => CreateInteger(UsrAttributeType.INITIAL_RX_LINK_DATA_RATE, value);

        /// <summary>Creates a USR-Final-Rx-Link-Data-Rate attribute (0x9003).</summary>
        /// <param name="value">The final receive link data rate in bps.</param>
        public static VendorSpecificAttributes FinalRxLinkDataRate(int value) => CreateInteger(UsrAttributeType.FINAL_RX_LINK_DATA_RATE, value);

        /// <summary>Creates a USR-Initial-Tx-Link-Data-Rate attribute (0x9006).</summary>
        /// <param name="value">The initial transmit link data rate in bps.</param>
        public static VendorSpecificAttributes InitialTxLinkDataRate(int value) => CreateInteger(UsrAttributeType.INITIAL_TX_LINK_DATA_RATE, value);

        /// <summary>Creates a USR-Final-Tx-Link-Data-Rate attribute (0x9007).</summary>
        /// <param name="value">The final transmit link data rate in bps.</param>
        public static VendorSpecificAttributes FinalTxLinkDataRate(int value) => CreateInteger(UsrAttributeType.FINAL_TX_LINK_DATA_RATE, value);

        /// <summary>Creates a USR-Chassis-Temperature attribute (0x9008).</summary>
        /// <param name="value">The chassis temperature.</param>
        public static VendorSpecificAttributes ChassisTemperature(int value) => CreateInteger(UsrAttributeType.CHASSIS_TEMPERATURE, value);

        /// <summary>Creates a USR-Sync-Async-Mode attribute (0x9009).</summary>
        /// <param name="value">The sync/async mode. See <see cref="USR_SYNC_ASYNC_MODE"/>.</param>
        public static VendorSpecificAttributes SyncAsyncMode(USR_SYNC_ASYNC_MODE value) => CreateInteger(UsrAttributeType.SYNC_ASYNC_MODE, (int)value);

        /// <summary>Creates a USR-Originate-Answer-Mode attribute (0x900A).</summary>
        /// <param name="value">The originate/answer mode. See <see cref="USR_ORIGINATE_ANSWER_MODE"/>.</param>
        public static VendorSpecificAttributes OriginateAnswerMode(USR_ORIGINATE_ANSWER_MODE value) => CreateInteger(UsrAttributeType.ORIGINATE_ANSWER_MODE, (int)value);

        /// <summary>Creates a USR-Modulation-Type attribute (0x900B).</summary>
        /// <param name="value">The modulation type.</param>
        public static VendorSpecificAttributes ModulationType(int value) => CreateInteger(UsrAttributeType.MODULATION_TYPE, value);

        /// <summary>Creates a USR-Initial-Modulation-Type attribute (0x900C).</summary>
        /// <param name="value">The initial modulation type.</param>
        public static VendorSpecificAttributes InitialModulationType(int value) => CreateInteger(UsrAttributeType.INITIAL_MODULATION_TYPE, value);

        /// <summary>Creates a USR-Connect-Term-Reason attribute (0x9050).</summary>
        /// <param name="value">The connect termination reason. See <see cref="USR_CONNECT_TERM_REASON"/>.</param>
        public static VendorSpecificAttributes ConnectTermReason(USR_CONNECT_TERM_REASON value) => CreateInteger(UsrAttributeType.CONNECT_TERM_REASON, (int)value);

        /// <summary>Creates a USR-Failure-to-Connect-Reason attribute (0x9051).</summary>
        /// <param name="value">The failure-to-connect reason code.</param>
        public static VendorSpecificAttributes FailureToConnectReason(int value) => CreateInteger(UsrAttributeType.FAILURE_TO_CONNECT_REASON, value);

        /// <summary>Creates a USR-Equalization-Type attribute (0x900D).</summary>
        /// <param name="value">The equalization type.</param>
        public static VendorSpecificAttributes EqualizationType(int value) => CreateInteger(UsrAttributeType.EQUALIZATION_TYPE, value);

        /// <summary>Creates a USR-Fallback-Enabled attribute (0x900E).</summary>
        /// <param name="value">The fallback enabled flag.</param>
        public static VendorSpecificAttributes FallbackEnabled(int value) => CreateInteger(UsrAttributeType.FALLBACK_ENABLED, value);

        /// <summary>Creates a USR-Characters-Sent attribute (0x9010).</summary>
        /// <param name="value">The number of characters sent.</param>
        public static VendorSpecificAttributes CharactersSent(int value) => CreateInteger(UsrAttributeType.CHARACTERS_SENT, value);

        /// <summary>Creates a USR-Characters-Received attribute (0x9011).</summary>
        /// <param name="value">The number of characters received.</param>
        public static VendorSpecificAttributes CharactersReceived(int value) => CreateInteger(UsrAttributeType.CHARACTERS_RECEIVED, value);

        /// <summary>Creates a USR-Blocks-Sent attribute (0x9012).</summary>
        /// <param name="value">The number of blocks sent.</param>
        public static VendorSpecificAttributes BlocksSent(int value) => CreateInteger(UsrAttributeType.BLOCKS_SENT, value);

        /// <summary>Creates a USR-Blocks-Received attribute (0x9013).</summary>
        /// <param name="value">The number of blocks received.</param>
        public static VendorSpecificAttributes BlocksReceived(int value) => CreateInteger(UsrAttributeType.BLOCKS_RECEIVED, value);

        /// <summary>Creates a USR-Blocks-Resent attribute (0x9014).</summary>
        /// <param name="value">The number of blocks resent.</param>
        public static VendorSpecificAttributes BlocksResent(int value) => CreateInteger(UsrAttributeType.BLOCKS_RESENT, value);

        /// <summary>Creates a USR-Retrains-Requested attribute (0x9015).</summary>
        /// <param name="value">The number of retrains requested.</param>
        public static VendorSpecificAttributes RetrainsRequested(int value) => CreateInteger(UsrAttributeType.RETRAINS_REQUESTED, value);

        /// <summary>Creates a USR-Retrains-Granted attribute (0x9016).</summary>
        /// <param name="value">The number of retrains granted.</param>
        public static VendorSpecificAttributes RetrainsGranted(int value) => CreateInteger(UsrAttributeType.RETRAINS_GRANTED, value);

        /// <summary>Creates a USR-Line-Reversals attribute (0x9017).</summary>
        /// <param name="value">The number of line reversals.</param>
        public static VendorSpecificAttributes LineReversals(int value) => CreateInteger(UsrAttributeType.LINE_REVERSALS, value);

        /// <summary>Creates a USR-Number-Of-Characters-Lost attribute (0x9018).</summary>
        /// <param name="value">The number of characters lost.</param>
        public static VendorSpecificAttributes NumberOfCharactersLost(int value) => CreateInteger(UsrAttributeType.NUMBER_OF_CHARACTERS_LOST, value);

        /// <summary>Creates a USR-Number-Of-Blers attribute (0x9019).</summary>
        /// <param name="value">The number of block errors.</param>
        public static VendorSpecificAttributes NumberOfBlers(int value) => CreateInteger(UsrAttributeType.NUMBER_OF_BLERS, value);

        /// <summary>Creates a USR-Number-Of-Link-Timeouts attribute (0x901A).</summary>
        /// <param name="value">The number of link timeouts.</param>
        public static VendorSpecificAttributes NumberOfLinkTimeouts(int value) => CreateInteger(UsrAttributeType.NUMBER_OF_LINK_TIMEOUTS, value);

        /// <summary>Creates a USR-Number-Of-Fallbacks attribute (0x901B).</summary>
        /// <param name="value">The number of fallbacks.</param>
        public static VendorSpecificAttributes NumberOfFallbacks(int value) => CreateInteger(UsrAttributeType.NUMBER_OF_FALLBACKS, value);

        /// <summary>Creates a USR-Number-Of-Upshifts attribute (0x901C).</summary>
        /// <param name="value">The number of upshifts.</param>
        public static VendorSpecificAttributes NumberOfUpshifts(int value) => CreateInteger(UsrAttributeType.NUMBER_OF_UPSHIFTS, value);

        /// <summary>Creates a USR-Number-Of-Link-NAKs attribute (0x901D).</summary>
        /// <param name="value">The number of link NAKs.</param>
        public static VendorSpecificAttributes NumberOfLinkNaks(int value) => CreateInteger(UsrAttributeType.NUMBER_OF_LINK_NAKS, value);

        /// <summary>Creates a USR-DTR-False-Timeout attribute (0x9023).</summary>
        /// <param name="value">The DTR false timeout in seconds.</param>
        public static VendorSpecificAttributes DtrFalseTimeout(int value) => CreateInteger(UsrAttributeType.DTR_FALSE_TIMEOUT, value);

        /// <summary>Creates a USR-Fallback-Limit attribute (0x9024).</summary>
        /// <param name="value">The fallback limit.</param>
        public static VendorSpecificAttributes FallbackLimit(int value) => CreateInteger(UsrAttributeType.FALLBACK_LIMIT, value);

        /// <summary>Creates a USR-Block-Error-Count-Limit attribute (0x9025).</summary>
        /// <param name="value">The block error count limit.</param>
        public static VendorSpecificAttributes BlockErrorCountLimit(int value) => CreateInteger(UsrAttributeType.BLOCK_ERROR_COUNT_LIMIT, value);

        /// <summary>Creates a USR-DTR-True-Timeout attribute (0x9026).</summary>
        /// <param name="value">The DTR true timeout in seconds.</param>
        public static VendorSpecificAttributes DtrTrueTimeout(int value) => CreateInteger(UsrAttributeType.DTR_TRUE_TIMEOUT, value);

        /// <summary>Creates a USR-Security-Login-Limit attribute (0x9027).</summary>
        /// <param name="value">The security login limit.</param>
        public static VendorSpecificAttributes SecurityLoginLimit(int value) => CreateInteger(UsrAttributeType.SECURITY_LOGIN_LIMIT, value);

        /// <summary>Creates a USR-Security-Login-Delay attribute (0x9028).</summary>
        /// <param name="value">The security login delay in seconds.</param>
        public static VendorSpecificAttributes SecurityLoginDelay(int value) => CreateInteger(UsrAttributeType.SECURITY_LOGIN_DELAY, value);

        /// <summary>Creates a USR-Card-Type attribute (0x9029).</summary>
        /// <param name="value">The card type.</param>
        public static VendorSpecificAttributes CardType(int value) => CreateInteger(UsrAttributeType.CARD_TYPE, value);

        /// <summary>Creates a USR-Connect-Speed attribute (0x9030).</summary>
        /// <param name="value">The connect speed in bps.</param>
        public static VendorSpecificAttributes ConnectSpeed(int value) => CreateInteger(UsrAttributeType.CONNECT_SPEED, value);

        /// <summary>Creates a USR-Simplified-MNP-Levels attribute (0x9031).</summary>
        /// <param name="value">The simplified MNP levels.</param>
        public static VendorSpecificAttributes SimplifiedMnpLevels(int value) => CreateInteger(UsrAttributeType.SIMPLIFIED_MNP_LEVELS, value);

        /// <summary>Creates a USR-Simplified-V42bis-Usage attribute (0x9032).</summary>
        /// <param name="value">The simplified V.42bis usage.</param>
        public static VendorSpecificAttributes SimplifiedV42bisUsage(int value) => CreateInteger(UsrAttributeType.SIMPLIFIED_V42BIS_USAGE, value);

        /// <summary>Creates a USR-Number-Of-Link-Timeouts-2 attribute (0x9033).</summary>
        /// <param name="value">The number of link timeouts (alternate).</param>
        public static VendorSpecificAttributes NumberOfLinkTimeouts2(int value) => CreateInteger(UsrAttributeType.NUMBER_OF_LINK_TIMEOUTS_2, value);

        /// <summary>Creates a USR-IDS0-Call-Type attribute (0x9034).</summary>
        /// <param name="value">The ISDN B-channel call type. See <see cref="USR_IDS0_CALL_TYPE"/>.</param>
        public static VendorSpecificAttributes Ids0CallType(USR_IDS0_CALL_TYPE value) => CreateInteger(UsrAttributeType.IDS0_CALL_TYPE, (int)value);

        /// <summary>Creates a USR-Bearer-Capabilities attribute (0x9035).</summary>
        /// <param name="value">The bearer capabilities.</param>
        public static VendorSpecificAttributes BearerCapabilities(int value) => CreateInteger(UsrAttributeType.BEARER_CAPABILITIES, value);

        /// <summary>Creates a USR-Speed-Of-Connection attribute (0x9036).</summary>
        /// <param name="value">The speed of connection in bps.</param>
        public static VendorSpecificAttributes SpeedOfConnection(int value) => CreateInteger(UsrAttributeType.SPEED_OF_CONNECTION, value);

        /// <summary>Creates a USR-Max-Link-Noise attribute (0x9037).</summary>
        /// <param name="value">The maximum link noise.</param>
        public static VendorSpecificAttributes MaxLinkNoise(int value) => CreateInteger(UsrAttributeType.MAX_LINK_NOISE, value);

        /// <summary>Creates a USR-Default-DTE-Data-Rate-2 attribute (0x9038).</summary>
        /// <param name="value">The default DTE data rate (alternate).</param>
        public static VendorSpecificAttributes DefaultDteDataRate2(int value) => CreateInteger(UsrAttributeType.DEFAULT_DTE_DATA_RATE_2, value);

        /// <summary>Creates a USR-Num-Of-Rings-Limit attribute (0x903A).</summary>
        /// <param name="value">The number of rings limit.</param>
        public static VendorSpecificAttributes NumOfRingsLimit(int value) => CreateInteger(UsrAttributeType.NUM_OF_RINGS_LIMIT, value);

        /// <summary>Creates a USR-Force-Rev attribute (0x903C).</summary>
        /// <param name="value">The force reverse flag.</param>
        public static VendorSpecificAttributes ForceRev(int value) => CreateInteger(UsrAttributeType.FORCE_REV, value);

        /// <summary>Creates a USR-Modem-Training-Time attribute (0x903D).</summary>
        /// <param name="value">The modem training time in milliseconds.</param>
        public static VendorSpecificAttributes ModemTrainingTime(int value) => CreateInteger(UsrAttributeType.MODEM_TRAINING_TIME, value);

        /// <summary>Creates a USR-S7-Timer attribute (0x903E).</summary>
        /// <param name="value">The S7 timer value.</param>
        public static VendorSpecificAttributes S7Timer(int value) => CreateInteger(UsrAttributeType.S7_TIMER, value);

        /// <summary>Creates a USR-CDL-Modem-Type attribute (0x9040).</summary>
        /// <param name="value">The CDL modem type.</param>
        public static VendorSpecificAttributes CdlModemType(int value) => CreateInteger(UsrAttributeType.CDL_MODEM_TYPE, value);

        /// <summary>Creates a USR-CDL-Last-Status-Change attribute (0x9041).</summary>
        /// <param name="value">The CDL last status change.</param>
        public static VendorSpecificAttributes CdlLastStatusChange(int value) => CreateInteger(UsrAttributeType.CDL_LAST_STATUS_CHANGE, value);

        /// <summary>Creates a USR-CDL-Signal-Quality attribute (0x9042).</summary>
        /// <param name="value">The CDL signal quality.</param>
        public static VendorSpecificAttributes CdlSignalQuality(int value) => CreateInteger(UsrAttributeType.CDL_SIGNAL_QUALITY, value);

        /// <summary>Creates a USR-Receive-Baud-Rate attribute (0x9043).</summary>
        /// <param name="value">The receive baud rate.</param>
        public static VendorSpecificAttributes ReceiveBaudRate(int value) => CreateInteger(UsrAttributeType.RECEIVE_BAUD_RATE, value);

        /// <summary>Creates a USR-Transmit-Baud-Rate attribute (0x9044).</summary>
        /// <param name="value">The transmit baud rate.</param>
        public static VendorSpecificAttributes TransmitBaudRate(int value) => CreateInteger(UsrAttributeType.TRANSMIT_BAUD_RATE, value);

        /// <summary>Creates a USR-S10-Timer attribute (0x9046).</summary>
        /// <param name="value">The S10 timer value.</param>
        public static VendorSpecificAttributes S10Timer(int value) => CreateInteger(UsrAttributeType.S10_TIMER, value);

        /// <summary>Creates a USR-Event-Id attribute (0xBE49).</summary>
        /// <param name="value">The event identifier.</param>
        public static VendorSpecificAttributes EventId(int value) => CreateInteger(UsrAttributeType.EVENT_ID, value);

        /// <summary>Creates a USR-Channel attribute (0xBE48).</summary>
        /// <param name="value">The channel number.</param>
        public static VendorSpecificAttributes Channel(int value) => CreateInteger(UsrAttributeType.CHANNEL, value);

        /// <summary>Creates a USR-Port-Number attribute (0xBE57).</summary>
        /// <param name="value">The physical port number.</param>
        public static VendorSpecificAttributes PortNumber(int value) => CreateInteger(UsrAttributeType.PORT_NUMBER, value);

        /// <summary>Creates a USR-Slot-Number attribute (0xBE58).</summary>
        /// <param name="value">The slot number.</param>
        public static VendorSpecificAttributes SlotNumber(int value) => CreateInteger(UsrAttributeType.SLOT_NUMBER, value);

        /// <summary>Creates a USR-Chassis-Temp-Threshold attribute (0xBE55).</summary>
        /// <param name="value">The chassis temperature threshold.</param>
        public static VendorSpecificAttributes ChassisTempThreshold(int value) => CreateInteger(UsrAttributeType.CHASSIS_TEMP_THRESHOLD, value);

        /// <summary>Creates a USR-Call-Event-Code attribute (0xBE56).</summary>
        /// <param name="value">The call event code.</param>
        public static VendorSpecificAttributes CallEventCode(int value) => CreateInteger(UsrAttributeType.CALL_EVENT_CODE, value);

        /// <summary>Creates a USR-DS1-Facility attribute (0xBE59).</summary>
        /// <param name="value">The DS1 facility number.</param>
        public static VendorSpecificAttributes Ds1Facility(int value) => CreateInteger(UsrAttributeType.DS1_FACILITY, value);

        /// <summary>Creates a USR-Physical-State attribute (0xBE5A).</summary>
        /// <param name="value">The physical state.</param>
        public static VendorSpecificAttributes PhysicalState(int value) => CreateInteger(UsrAttributeType.PHYSICAL_STATE, value);

        /// <summary>Creates a USR-Packet-Bus-Session attribute (0xBE5B).</summary>
        /// <param name="value">The packet bus session number.</param>
        public static VendorSpecificAttributes PacketBusSession(int value) => CreateInteger(UsrAttributeType.PACKET_BUS_SESSION, value);

        /// <summary>Creates a USR-Server-Time attribute (0xBE5C).</summary>
        /// <param name="value">The server time value.</param>
        public static VendorSpecificAttributes ServerTime(int value) => CreateInteger(UsrAttributeType.SERVER_TIME, value);

        /// <summary>Creates a USR-Channel-Connected-To attribute (0xBE5D).</summary>
        /// <param name="value">The channel connected to.</param>
        public static VendorSpecificAttributes ChannelConnectedTo(int value) => CreateInteger(UsrAttributeType.CHANNEL_CONNECTED_TO, value);

        /// <summary>Creates a USR-Slot-Connected-To attribute (0xBE5E).</summary>
        /// <param name="value">The slot connected to.</param>
        public static VendorSpecificAttributes SlotConnectedTo(int value) => CreateInteger(UsrAttributeType.SLOT_CONNECTED_TO, value);

        /// <summary>Creates a USR-Device-Connected-To attribute (0xBE5F).</summary>
        /// <param name="value">The device connected to.</param>
        public static VendorSpecificAttributes DeviceConnectedTo(int value) => CreateInteger(UsrAttributeType.DEVICE_CONNECTED_TO, value);

        /// <summary>Creates a USR-NFAS-ID attribute (0xBE60).</summary>
        /// <param name="value">The NFAS identifier.</param>
        public static VendorSpecificAttributes NfasId(int value) => CreateInteger(UsrAttributeType.NFAS_ID, value);

        /// <summary>Creates a USR-Q931-Call-Reference attribute (0xBE61).</summary>
        /// <param name="value">The Q.931 call reference.</param>
        public static VendorSpecificAttributes Q931CallReference(int value) => CreateInteger(UsrAttributeType.Q931_CALL_REFERENCE, value);

        /// <summary>Creates a USR-Call-Arrival-in-GMT attribute (0xBE62).</summary>
        /// <param name="value">The call arrival time in GMT (Unix timestamp).</param>
        public static VendorSpecificAttributes CallArrivalInGmt(int value) => CreateInteger(UsrAttributeType.CALL_ARRIVAL_IN_GMT, value);

        /// <summary>Creates a USR-Call-Connect-in-GMT attribute (0xBE63).</summary>
        /// <param name="value">The call connect time in GMT (Unix timestamp).</param>
        public static VendorSpecificAttributes CallConnectInGmt(int value) => CreateInteger(UsrAttributeType.CALL_CONNECT_IN_GMT, value);

        /// <summary>Creates a USR-Call-Terminate-in-GMT attribute (0xBE64).</summary>
        /// <param name="value">The call terminate time in GMT (Unix timestamp).</param>
        public static VendorSpecificAttributes CallTerminateInGmt(int value) => CreateInteger(UsrAttributeType.CALL_TERMINATE_IN_GMT, value);

        /// <summary>Creates a USR-IDS0-Call-Type-2 attribute (0xBE65).</summary>
        /// <param name="value">The ISDN B-channel call type (alternate).</param>
        public static VendorSpecificAttributes Ids0CallType2(int value) => CreateInteger(UsrAttributeType.IDS0_CALL_TYPE_2, value);

        /// <summary>Creates a USR-Call-Reference-Number attribute (0xBE66).</summary>
        /// <param name="value">The call reference number.</param>
        public static VendorSpecificAttributes CallReferenceNumber(int value) => CreateInteger(UsrAttributeType.CALL_REFERENCE_NUMBER, value);

        /// <summary>Creates a USR-PPP-NCP-Type attribute (0xBE68).</summary>
        /// <param name="value">The PPP NCP type.</param>
        public static VendorSpecificAttributes PppNcpType(int value) => CreateInteger(UsrAttributeType.PPP_NCP_TYPE, value);

        /// <summary>Creates a USR-Tunnel-Security attribute (0xBE69).</summary>
        /// <param name="value">The tunnel security setting.</param>
        public static VendorSpecificAttributes TunnelSecurity(int value) => CreateInteger(UsrAttributeType.TUNNEL_SECURITY, value);

        /// <summary>Creates a USR-Port-Tap attribute (0xBE6A).</summary>
        /// <param name="value">The port tap setting.</param>
        public static VendorSpecificAttributes PortTap(int value) => CreateInteger(UsrAttributeType.PORT_TAP, value);

        /// <summary>Creates a USR-Port-Tap-Format attribute (0xBE6B).</summary>
        /// <param name="value">The port tap format.</param>
        public static VendorSpecificAttributes PortTapFormat(int value) => CreateInteger(UsrAttributeType.PORT_TAP_FORMAT, value);

        /// <summary>Creates a USR-Port-Tap-Output attribute (0xBE6D).</summary>
        /// <param name="value">The port tap output.</param>
        public static VendorSpecificAttributes PortTapOutput(int value) => CreateInteger(UsrAttributeType.PORT_TAP_OUTPUT, value);

        /// <summary>Creates a USR-Syslog-Tap attribute (0xBE76).</summary>
        /// <param name="value">The syslog tap level. See <see cref="USR_SYSLOG_TAP"/>.</param>
        public static VendorSpecificAttributes SyslogTap(USR_SYSLOG_TAP value) => CreateInteger(UsrAttributeType.SYSLOG_TAP, (int)value);

        /// <summary>Creates a USR-Chassis-Call-Slot attribute (0xBE7A).</summary>
        /// <param name="value">The chassis call slot number.</param>
        public static VendorSpecificAttributes ChassisCallSlot(int value) => CreateInteger(UsrAttributeType.CHASSIS_CALL_SLOT, value);

        /// <summary>Creates a USR-Chassis-Call-Span attribute (0xBE7B).</summary>
        /// <param name="value">The chassis call span number.</param>
        public static VendorSpecificAttributes ChassisCallSpan(int value) => CreateInteger(UsrAttributeType.CHASSIS_CALL_SPAN, value);

        /// <summary>Creates a USR-Chassis-Call-Channel attribute (0xBE7C).</summary>
        /// <param name="value">The chassis call channel number.</param>
        public static VendorSpecificAttributes ChassisCallChannel(int value) => CreateInteger(UsrAttributeType.CHASSIS_CALL_CHANNEL, value);

        /// <summary>Creates a USR-Keypress-Timeout attribute (0xBE7D).</summary>
        /// <param name="value">The keypress timeout in seconds.</param>
        public static VendorSpecificAttributes KeypressTimeout(int value) => CreateInteger(UsrAttributeType.KEYPRESS_TIMEOUT, value);

        /// <summary>Creates a USR-Unauthenticated-Time attribute (0xBE7E).</summary>
        /// <param name="value">The unauthenticated time in seconds.</param>
        public static VendorSpecificAttributes UnauthenticatedTime(int value) => CreateInteger(UsrAttributeType.UNAUTHENTICATED_TIME, value);

        /// <summary>Creates a USR-Re-Chap-Timeout attribute (0xBE81).</summary>
        /// <param name="value">The re-CHAP timeout in seconds.</param>
        public static VendorSpecificAttributes ReChapTimeout(int value) => CreateInteger(UsrAttributeType.RE_CHAP_TIMEOUT, value);

        /// <summary>Creates a USR-CCP-Algorithm attribute (0xBE82).</summary>
        /// <param name="value">The CCP algorithm. See <see cref="USR_CCP_ALGORITHM"/>.</param>
        public static VendorSpecificAttributes CcpAlgorithm(USR_CCP_ALGORITHM value) => CreateInteger(UsrAttributeType.CCP_ALGORITHM, (int)value);

        /// <summary>Creates a USR-ACCM-Type attribute (0xBE83).</summary>
        /// <param name="value">The ACCM type. See <see cref="USR_ACCM_TYPE"/>.</param>
        public static VendorSpecificAttributes AccmType(USR_ACCM_TYPE value) => CreateInteger(UsrAttributeType.ACCM_TYPE, (int)value);

        /// <summary>Creates a USR-Connect-Time-Limit attribute (0xBE84).</summary>
        /// <param name="value">The connect time limit in seconds.</param>
        public static VendorSpecificAttributes ConnectTimeLimit(int value) => CreateInteger(UsrAttributeType.CONNECT_TIME_LIMIT, value);

        /// <summary>Creates a USR-Bacp-Enable attribute (0xBE86).</summary>
        /// <param name="value">The BACP enable flag (0 = disabled, 1 = enabled).</param>
        public static VendorSpecificAttributes BacpEnable(int value) => CreateInteger(UsrAttributeType.BACP_ENABLE, value);

        /// <summary>Creates a USR-DHCP-Maximum-Leases attribute (0xBE87).</summary>
        /// <param name="value">The maximum DHCP leases.</param>
        public static VendorSpecificAttributes DhcpMaximumLeases(int value) => CreateInteger(UsrAttributeType.DHCP_MAXIMUM_LEASES, value);

        /// <summary>Creates a USR-DHCP-Pool-Number attribute (0xBE8A).</summary>
        /// <param name="value">The DHCP pool number.</param>
        public static VendorSpecificAttributes DhcpPoolNumber(int value) => CreateInteger(UsrAttributeType.DHCP_POOL_NUMBER, value);

        /// <summary>Creates a USR-Multicast-Proxy attribute (0xBE8D).</summary>
        /// <param name="value">The multicast proxy setting.</param>
        public static VendorSpecificAttributes MulticastProxy(int value) => CreateInteger(UsrAttributeType.MULTICAST_PROXY, value);

        /// <summary>Creates a USR-Multicast-Forwarding attribute (0xBE8E).</summary>
        /// <param name="value">The multicast forwarding setting.</param>
        public static VendorSpecificAttributes MulticastForwarding(int value) => CreateInteger(UsrAttributeType.MULTICAST_FORWARDING, value);

        /// <summary>Creates a USR-IGMP-Query-Interval attribute (0xBE8F).</summary>
        /// <param name="value">The IGMP query interval in seconds.</param>
        public static VendorSpecificAttributes IgmpQueryInterval(int value) => CreateInteger(UsrAttributeType.IGMP_QUERY_INTERVAL, value);

        /// <summary>Creates a USR-IGMP-Maximum-Response-Time attribute (0xBE90).</summary>
        /// <param name="value">The IGMP maximum response time in seconds.</param>
        public static VendorSpecificAttributes IgmpMaximumResponseTime(int value) => CreateInteger(UsrAttributeType.IGMP_MAXIMUM_RESPONSE_TIME, value);

        /// <summary>Creates a USR-IGMP-Robustness attribute (0xBE91).</summary>
        /// <param name="value">The IGMP robustness variable.</param>
        public static VendorSpecificAttributes IgmpRobustness(int value) => CreateInteger(UsrAttributeType.IGMP_ROBUSTNESS, value);

        /// <summary>Creates a USR-IGMP-Version attribute (0xBE92).</summary>
        /// <param name="value">The IGMP version.</param>
        public static VendorSpecificAttributes IgmpVersion(int value) => CreateInteger(UsrAttributeType.IGMP_VERSION, value);

        /// <summary>Creates a USR-Rad-Multicast-Routing-Ttl attribute (0xBE93).</summary>
        /// <param name="value">The multicast routing TTL.</param>
        public static VendorSpecificAttributes RadMulticastRoutingTtl(int value) => CreateInteger(UsrAttributeType.RAD_MULTICAST_ROUTING_TTL, value);

        /// <summary>Creates a USR-Rad-Multicast-Routing-RtLim attribute (0xBE94).</summary>
        /// <param name="value">The multicast routing rate limit.</param>
        public static VendorSpecificAttributes RadMulticastRoutingRtLim(int value) => CreateInteger(UsrAttributeType.RAD_MULTICAST_ROUTING_RTLIM, value);

        /// <summary>Creates a USR-Rad-Multicast-Routing-Proto attribute (0xBE95).</summary>
        /// <param name="value">The multicast routing protocol.</param>
        public static VendorSpecificAttributes RadMulticastRoutingProto(int value) => CreateInteger(UsrAttributeType.RAD_MULTICAST_ROUTING_PROTO, value);

        /// <summary>Creates a USR-PPP-Log-Mask attribute (0xBE99).</summary>
        /// <param name="value">The PPP log mask.</param>
        public static VendorSpecificAttributes PppLogMask(int value) => CreateInteger(UsrAttributeType.PPP_LOG_MASK, value);

        /// <summary>Creates a USR-DS0-Byte attribute (0xBE9B).</summary>
        /// <param name="value">The DS0 byte value.</param>
        public static VendorSpecificAttributes Ds0Byte(int value) => CreateInteger(UsrAttributeType.DS0_BYTE, value);

        /// <summary>Creates a USR-DS0 attribute (0xBE9C).</summary>
        /// <param name="value">The DS0 value.</param>
        public static VendorSpecificAttributes Ds0(int value) => CreateInteger(UsrAttributeType.DS0, value);

        /// <summary>Creates a USR-QoS-Queue attribute (0xBEA0).</summary>
        /// <param name="value">The QoS queue number.</param>
        public static VendorSpecificAttributes QosQueue(int value) => CreateInteger(UsrAttributeType.QOS_QUEUE, value);

        /// <summary>Creates a USR-ET-Bridge-Id attribute (0xBEA2).</summary>
        /// <param name="value">The Ethernet bridge ID.</param>
        public static VendorSpecificAttributes EtBridgeId(int value) => CreateInteger(UsrAttributeType.ET_BRIDGE_ID, value);

        /// <summary>Creates a USR-ET-Bridge-Port-Session-No attribute (0xBEA3).</summary>
        /// <param name="value">The Ethernet bridge port session number.</param>
        public static VendorSpecificAttributes EtBridgePortSessionNo(int value) => CreateInteger(UsrAttributeType.ET_BRIDGE_PORT_SESSION_NO, value);

        /// <summary>Creates a USR-RMMIE-Status attribute (0xBEA7).</summary>
        /// <param name="value">The RMMIE status.</param>
        public static VendorSpecificAttributes RmmieStatus(int value) => CreateInteger(UsrAttributeType.RMMIE_STATUS, value);

        /// <summary>Creates a USR-RMMIE-x2-Status attribute (0xBEA8).</summary>
        /// <param name="value">The RMMIE x2 status.</param>
        public static VendorSpecificAttributes RmmieX2Status(int value) => CreateInteger(UsrAttributeType.RMMIE_X2_STATUS, value);

        /// <summary>Creates a USR-RMMIE-Planned-Disconnect attribute (0xBEA9).</summary>
        /// <param name="value">The RMMIE planned disconnect code.</param>
        public static VendorSpecificAttributes RmmiePlannedDisconnect(int value) => CreateInteger(UsrAttributeType.RMMIE_PLANNED_DISCONNECT, value);

        /// <summary>Creates a USR-RMMIE-Last-Update-Time attribute (0xBEAA).</summary>
        /// <param name="value">The RMMIE last update time.</param>
        public static VendorSpecificAttributes RmmieLastUpdateTime(int value) => CreateInteger(UsrAttributeType.RMMIE_LAST_UPDATE_TIME, value);

        /// <summary>Creates a USR-RMMIE-Last-Update-Event attribute (0xBEAB).</summary>
        /// <param name="value">The RMMIE last update event.</param>
        public static VendorSpecificAttributes RmmieLastUpdateEvent(int value) => CreateInteger(UsrAttributeType.RMMIE_LAST_UPDATE_EVENT, value);

        /// <summary>Creates a USR-RMMIE-Rcv-Tot-PwrLvl attribute (0xBEAC).</summary>
        /// <param name="value">The RMMIE receive total power level.</param>
        public static VendorSpecificAttributes RmmieRcvTotPwrLvl(int value) => CreateInteger(UsrAttributeType.RMMIE_RCV_TOT_PWRLVL, value);

        /// <summary>Creates a USR-RMMIE-Rcv-PwrLvl-3300Hz attribute (0xBEAD).</summary>
        /// <param name="value">The RMMIE receive power level at 3300 Hz.</param>
        public static VendorSpecificAttributes RmmieRcvPwrLvl3300Hz(int value) => CreateInteger(UsrAttributeType.RMMIE_RCV_PWRLVL_3300HZ, value);

        /// <summary>Creates a USR-RMMIE-Rcv-PwrLvl-3750Hz attribute (0xBEAE).</summary>
        /// <param name="value">The RMMIE receive power level at 3750 Hz.</param>
        public static VendorSpecificAttributes RmmieRcvPwrLvl3750Hz(int value) => CreateInteger(UsrAttributeType.RMMIE_RCV_PWRLVL_3750HZ, value);

        /// <summary>Creates a USR-RMMIE-PwrLvl-NearEcho-Canc attribute (0xBEAF).</summary>
        /// <param name="value">The RMMIE power level near-echo cancellation.</param>
        public static VendorSpecificAttributes RmmiePwrLvlNearEchoCanc(int value) => CreateInteger(UsrAttributeType.RMMIE_PWRLVL_NEARECHO_CANC, value);

        /// <summary>Creates a USR-RMMIE-PwrLvl-FarEcho-Canc attribute (0xBEB0).</summary>
        /// <param name="value">The RMMIE power level far-echo cancellation.</param>
        public static VendorSpecificAttributes RmmiePwrLvlFarEchoCanc(int value) => CreateInteger(UsrAttributeType.RMMIE_PWRLVL_FARECHO_CANC, value);

        /// <summary>Creates a USR-RMMIE-PwrLvl-Noise-Lvl attribute (0xBEB1).</summary>
        /// <param name="value">The RMMIE power level noise level.</param>
        public static VendorSpecificAttributes RmmiePwrLvlNoiseLvl(int value) => CreateInteger(UsrAttributeType.RMMIE_PWRLVL_NOISE_LVL, value);

        /// <summary>Creates a USR-RMMIE-PwrLvl-Xmit-Lvl attribute (0xBEB2).</summary>
        /// <param name="value">The RMMIE power level transmit level.</param>
        public static VendorSpecificAttributes RmmiePwrLvlXmitLvl(int value) => CreateInteger(UsrAttributeType.RMMIE_PWRLVL_XMIT_LVL, value);

        /// <summary>Creates a USR-RMMIE-Num-Of-Updates attribute (0xBEB3).</summary>
        /// <param name="value">The RMMIE number of updates.</param>
        public static VendorSpecificAttributes RmmieNumOfUpdates(int value) => CreateInteger(UsrAttributeType.RMMIE_NUM_OF_UPDATES, value);

        /// <summary>Creates a USR-RMMIE-Manufacturer-ID attribute (0xBEB4).</summary>
        /// <param name="value">The RMMIE manufacturer ID.</param>
        public static VendorSpecificAttributes RmmieManufacturerId(int value) => CreateInteger(UsrAttributeType.RMMIE_MANUFACTURER_ID, value);

        /// <summary>Creates a USR-Mbi-Ct-PRI-Card-Slot attribute (0xBEB8).</summary>
        /// <param name="value">The MBI CT PRI card slot.</param>
        public static VendorSpecificAttributes MbiCtPriCardSlot(int value) => CreateInteger(UsrAttributeType.MBI_CT_PRI_CARD_SLOT, value);

        /// <summary>Creates a USR-Mbi-Ct-TDM-Time-Slot attribute (0xBEB9).</summary>
        /// <param name="value">The MBI CT TDM time slot.</param>
        public static VendorSpecificAttributes MbiCtTdmTimeSlot(int value) => CreateInteger(UsrAttributeType.MBI_CT_TDM_TIME_SLOT, value);

        /// <summary>Creates a USR-Mbi-Ct-PRI-Card-Span-Line attribute (0xBEBA).</summary>
        /// <param name="value">The MBI CT PRI card span line.</param>
        public static VendorSpecificAttributes MbiCtPriCardSpanLine(int value) => CreateInteger(UsrAttributeType.MBI_CT_PRI_CARD_SPAN_LINE, value);

        /// <summary>Creates a USR-Mbi-Ct-BChannel-Used attribute (0xBEBB).</summary>
        /// <param name="value">The MBI CT B-channel used.</param>
        public static VendorSpecificAttributes MbiCtBChannelUsed(int value) => CreateInteger(UsrAttributeType.MBI_CT_BCHANNEL_USED, value);

        /// <summary>Creates a USR-Physical-Port-Number attribute (0xBEBC).</summary>
        /// <param name="value">The physical port number.</param>
        public static VendorSpecificAttributes PhysicalPortNumber(int value) => CreateInteger(UsrAttributeType.PHYSICAL_PORT_NUMBER, value);

        /// <summary>Creates a USR-Port-Tap-Facility attribute (0xBEBD).</summary>
        /// <param name="value">The port tap facility.</param>
        public static VendorSpecificAttributes PortTapFacility(int value) => CreateInteger(UsrAttributeType.PORT_TAP_FACILITY, value);

        /// <summary>Creates a USR-Port-Tap-Priority attribute (0xBEBE).</summary>
        /// <param name="value">The port tap priority.</param>
        public static VendorSpecificAttributes PortTapPriority(int value) => CreateInteger(UsrAttributeType.PORT_TAP_PRIORITY, value);

        /// <summary>Creates a USR-PPP-Bridge-NCP-Type attribute (0xBEC2).</summary>
        /// <param name="value">The PPP bridge NCP type.</param>
        public static VendorSpecificAttributes PppBridgeNcpType(int value) => CreateInteger(UsrAttributeType.PPP_BRIDGE_NCP_TYPE, value);

        /// <summary>Creates a USR-Dsx1-Line-Mode attribute (0xBEC4).</summary>
        /// <param name="value">The DSX1 line mode.</param>
        public static VendorSpecificAttributes Dsx1LineMode(int value) => CreateInteger(UsrAttributeType.DSX1_LINE_MODE, value);

        /// <summary>Creates a USR-Auth-Mode attribute (0xBEC5).</summary>
        /// <param name="value">The authentication mode. See <see cref="USR_AUTH_MODE"/>.</param>
        public static VendorSpecificAttributes AuthMode(USR_AUTH_MODE value) => CreateInteger(UsrAttributeType.AUTH_MODE, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates a USR-Reply-Message attribute (0x9039).</summary>
        /// <param name="value">The reply message text. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ReplyMessage(string value) => CreateString(UsrAttributeType.REPLY_MESSAGE, value);

        /// <summary>Creates a USR-Alternate-Number attribute (0x903B).</summary>
        /// <param name="value">The alternate phone number. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AlternateNumber(string value) => CreateString(UsrAttributeType.ALTERNATE_NUMBER, value);

        /// <summary>Creates a USR-Log-Filter-Packets attribute (0x9054).</summary>
        /// <param name="value">The log filter packets. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LogFilterPackets(string value) => CreateString(UsrAttributeType.LOG_FILTER_PACKETS, value);

        /// <summary>Creates a USR-Last-Number-Dialed-Out attribute (0xBE45).</summary>
        /// <param name="value">The last number dialed out. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LastNumberDialedOut(string value) => CreateString(UsrAttributeType.LAST_NUMBER_DIALED_OUT, value);

        /// <summary>Creates a USR-Last-Number-Dialed-In-DNIS attribute (0xBE46).</summary>
        /// <param name="value">The last DNIS number dialed in. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LastNumberDialedInDnis(string value) => CreateString(UsrAttributeType.LAST_NUMBER_DIALED_IN_DNIS, value);

        /// <summary>Creates a USR-Last-Callers-Number-ANI attribute (0xBE47).</summary>
        /// <param name="value">The last caller's ANI number. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LastCallersNumberAni(string value) => CreateString(UsrAttributeType.LAST_CALLERS_NUMBER_ANI, value);

        /// <summary>Creates a USR-Event-Date-Time attribute (0xBE4A).</summary>
        /// <param name="value">The event date and time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EventDateTime(string value) => CreateString(UsrAttributeType.EVENT_DATE_TIME, value);

        /// <summary>Creates a USR-Call-Start-Date-Time attribute (0xBE4B).</summary>
        /// <param name="value">The call start date/time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallStartDateTime(string value) => CreateString(UsrAttributeType.CALL_START_DATE_TIME, value);

        /// <summary>Creates a USR-Call-End-Date-Time attribute (0xBE4C).</summary>
        /// <param name="value">The call end date/time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallEndDateTime(string value) => CreateString(UsrAttributeType.CALL_END_DATE_TIME, value);

        /// <summary>Creates a USR-Framed-IP-Address-Pool-Name attribute (0xBE6C).</summary>
        /// <param name="value">The framed IP address pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedIpAddressPoolName(string value) => CreateString(UsrAttributeType.FRAMED_IP_ADDRESS_POOL_NAME, value);

        /// <summary>Creates a USR-MP-EDO attribute (0xBE6E).</summary>
        /// <param name="value">The multilink PPP EDO. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MpEdo(string value) => CreateString(UsrAttributeType.MP_EDO, value);

        /// <summary>Creates a USR-Framed-IPX-Route attribute (0xBE70).</summary>
        /// <param name="value">The framed IPX route. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedIpxRoute(string value) => CreateString(UsrAttributeType.FRAMED_IPX_ROUTE, value);

        /// <summary>Creates a USR-CHAP-Challenge attribute (0xBE67).</summary>
        /// <param name="value">The CHAP challenge. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChapChallenge(string value) => CreateString(UsrAttributeType.CHAP_CHALLENGE, value);

        /// <summary>Creates a USR-Log-Filter-Callstart attribute (0xBE77).</summary>
        /// <param name="value">The log filter call start. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LogFilterCallstart(string value) => CreateString(UsrAttributeType.LOG_FILTER_CALLSTART, value);

        /// <summary>Creates a USR-Log-Filter-Callend attribute (0xBE78).</summary>
        /// <param name="value">The log filter call end. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LogFilterCallend(string value) => CreateString(UsrAttributeType.LOG_FILTER_CALLEND, value);

        /// <summary>Creates a USR-Log-Filter-Function attribute (0xBE79).</summary>
        /// <param name="value">The log filter function. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LogFilterFunction(string value) => CreateString(UsrAttributeType.LOG_FILTER_FUNCTION, value);

        /// <summary>Creates a USR-VPN-Encryptor attribute (0xBE7F).</summary>
        /// <param name="value">The VPN encryptor name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnEncryptor(string value) => CreateString(UsrAttributeType.VPN_ENCRYPTOR, value);

        /// <summary>Creates a USR-VPN-GW-Location-Id attribute (0xBE80).</summary>
        /// <param name="value">The VPN gateway location ID. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnGwLocationId(string value) => CreateString(UsrAttributeType.VPN_GW_LOCATION_ID, value);

        /// <summary>Creates a USR-Startup-Expression attribute (0xBE85).</summary>
        /// <param name="value">The startup expression. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes StartupExpression(string value) => CreateString(UsrAttributeType.STARTUP_EXPRESSION, value);

        /// <summary>Creates a USR-MIC attribute (0xBE8B).</summary>
        /// <param name="value">The MIC value. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Mic(string value) => CreateString(UsrAttributeType.MIC, value);

        /// <summary>Creates a USR-MIC-2 attribute (0xBE8C).</summary>
        /// <param name="value">The MIC-2 value. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Mic2(string value) => CreateString(UsrAttributeType.MIC_2, value);

        /// <summary>Creates a USR-Rad-Multicast-Routing-Bound attribute (0xBE96).</summary>
        /// <param name="value">The multicast routing boundary. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RadMulticastRoutingBound(string value) => CreateString(UsrAttributeType.RAD_MULTICAST_ROUTING_BOUND, value);

        /// <summary>Creates a USR-MN-HA-Shared-Key attribute (0xBE97).</summary>
        /// <param name="value">The Mobile IP MN-HA shared key. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MnHaSharedKey(string value) => CreateString(UsrAttributeType.MN_HA_SHARED_KEY, value);

        /// <summary>Creates a USR-IP-Address-Pool-Name attribute (0xBE98).</summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpAddressPoolName(string value) => CreateString(UsrAttributeType.IP_ADDRESS_POOL_NAME, value);

        /// <summary>Creates a USR-Tunnel-Switch-Endpoint attribute (0xBE9A).</summary>
        /// <param name="value">The tunnel switch endpoint. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelSwitchEndpoint(string value) => CreateString(UsrAttributeType.TUNNEL_SWITCH_ENDPOINT, value);

        /// <summary>Creates a USR-PW-USR-IFilter attribute (0xBE9E).</summary>
        /// <param name="value">The input filter. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PwUsrIFilter(string value) => CreateString(UsrAttributeType.PW_USR_IFILTER, value);

        /// <summary>Creates a USR-PW-USR-OFilter attribute (0xBE9F).</summary>
        /// <param name="value">The output filter. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PwUsrOFilter(string value) => CreateString(UsrAttributeType.PW_USR_OFILTER, value);

        /// <summary>Creates a USR-ET-Bridge-Name attribute (0xBEA1).</summary>
        /// <param name="value">The Ethernet bridge name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EtBridgeName(string value) => CreateString(UsrAttributeType.ET_BRIDGE_NAME, value);

        /// <summary>Creates a USR-Tunnel-Authentication attribute (0xBEA4).</summary>
        /// <param name="value">The tunnel authentication. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelAuthentication(string value) => CreateString(UsrAttributeType.TUNNEL_AUTHENTICATION, value);

        /// <summary>Creates a USR-Index attribute (0xBEA5).</summary>
        /// <param name="value">The index value. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Index(string value) => CreateString(UsrAttributeType.INDEX, value);

        /// <summary>Creates a USR-Cutoff attribute (0xBEA6).</summary>
        /// <param name="value">The cutoff value. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Cutoff(string value) => CreateString(UsrAttributeType.CUTOFF, value);

        /// <summary>Creates a USR-RMMIE-Product-Code attribute (0xBEB5).</summary>
        /// <param name="value">The RMMIE product code. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RmmieProductCode(string value) => CreateString(UsrAttributeType.RMMIE_PRODUCT_CODE, value);

        /// <summary>Creates a USR-RMMIE-Serial-Number attribute (0xBEB6).</summary>
        /// <param name="value">The RMMIE serial number. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RmmieSerialNumber(string value) => CreateString(UsrAttributeType.RMMIE_SERIAL_NUMBER, value);

        /// <summary>Creates a USR-RMMIE-Firmware-Version attribute (0xBEB7).</summary>
        /// <param name="value">The RMMIE firmware version. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RmmieFirmwareVersion(string value) => CreateString(UsrAttributeType.RMMIE_FIRMWARE_VERSION, value);

        /// <summary>Creates a USR-RMMIE-Firmware-Build-Date attribute (0xBEBF).</summary>
        /// <param name="value">The RMMIE firmware build date. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RmmieFirmwareBuildDate(string value) => CreateString(UsrAttributeType.RMMIE_FIRMWARE_BUILD_DATE, value);

        /// <summary>Creates a USR-Tunnel-Session-Id attribute (0xBEC1).</summary>
        /// <param name="value">The tunnel session ID. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelSessionId(string value) => CreateString(UsrAttributeType.TUNNEL_SESSION_ID, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a USR-Default-Gateway attribute (0xBE4D).</summary>
        /// <param name="value">The default gateway. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes DefaultGateway(IPAddress value) => CreateIpv4(UsrAttributeType.DEFAULT_GATEWAY, value);

        /// <summary>Creates a USR-Local-Framed-IP-Addr attribute (0xBE6F).</summary>
        /// <param name="value">The local framed IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes LocalFramedIpAddr(IPAddress value) => CreateIpv4(UsrAttributeType.LOCAL_FRAMED_IP_ADDR, value);

        /// <summary>Creates a USR-MPIP-Tunnel-Originator attribute (0xBE71).</summary>
        /// <param name="value">The MPIP tunnel originator. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes MpipTunnelOriginator(IPAddress value) => CreateIpv4(UsrAttributeType.MPIP_TUNNEL_ORIGINATOR, value);

        /// <summary>Creates a USR-Primary-DNS-Server attribute (0xBE72).</summary>
        /// <param name="value">The primary DNS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDnsServer(IPAddress value) => CreateIpv4(UsrAttributeType.PRIMARY_DNS_SERVER, value);

        /// <summary>Creates a USR-Secondary-DNS-Server attribute (0xBE73).</summary>
        /// <param name="value">The secondary DNS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDnsServer(IPAddress value) => CreateIpv4(UsrAttributeType.SECONDARY_DNS_SERVER, value);

        /// <summary>Creates a USR-Primary-NBNS-Server attribute (0xBE74).</summary>
        /// <param name="value">The primary NBNS (WINS) server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryNbnsServer(IPAddress value) => CreateIpv4(UsrAttributeType.PRIMARY_NBNS_SERVER, value);

        /// <summary>Creates a USR-Secondary-NBNS-Server attribute (0xBE75).</summary>
        /// <param name="value">The secondary NBNS (WINS) server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryNbnsServer(IPAddress value) => CreateIpv4(UsrAttributeType.SECONDARY_NBNS_SERVER, value);

        /// <summary>Creates a USR-Primary-DHCP-Server attribute (0xBE88).</summary>
        /// <param name="value">The primary DHCP server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDhcpServer(IPAddress value) => CreateIpv4(UsrAttributeType.PRIMARY_DHCP_SERVER, value);

        /// <summary>Creates a USR-Secondary-DHCP-Server attribute (0xBE89).</summary>
        /// <param name="value">The secondary DHCP server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDhcpServer(IPAddress value) => CreateIpv4(UsrAttributeType.SECONDARY_DHCP_SERVER, value);

        /// <summary>Creates a USR-Gateway-IP-Address attribute (0xBE9D).</summary>
        /// <param name="value">The gateway IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes GatewayIpAddress(IPAddress value) => CreateIpv4(UsrAttributeType.GATEWAY_IP_ADDRESS, value);

        /// <summary>Creates a USR-Mobile-IP-Home-Agent-Address attribute (0xBEC0).</summary>
        /// <param name="value">The Mobile IP Home Agent address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes MobileIpHomeAgentAddress(IPAddress value) => CreateIpv4(UsrAttributeType.MOBILE_IP_HOME_AGENT_ADDRESS, value);

        /// <summary>Creates a USR-Local-IP-Address attribute (0xBEC3).</summary>
        /// <param name="value">The local IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes LocalIpAddress(IPAddress value) => CreateIpv4(UsrAttributeType.LOCAL_IP_ADDRESS, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(UsrAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (uint)type, data, VendorSpecificFormat.Type4Len0);
        }

        private static VendorSpecificAttributes CreateString(UsrAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (uint)type, data, VendorSpecificFormat.Type4Len0);
        }

        private static VendorSpecificAttributes CreateIpv4(UsrAttributeType type, IPAddress value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException(
                    $"Expected an IPv4 (InterNetwork) address, got '{value.AddressFamily}'.",
                    nameof(value));

            byte[] addrBytes = new byte[4];
            value.TryWriteBytes(addrBytes, out _);

            return new VendorSpecificAttributes(VENDOR_ID, (uint)type, addrBytes, VendorSpecificFormat.Type4Len0);
        }

        #endregion
    }
}