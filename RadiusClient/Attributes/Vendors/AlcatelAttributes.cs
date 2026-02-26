using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Alcatel-Lucent / Nokia SR OS (IANA PEN 3041) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.alcatel</c>.
    /// </summary>
    public enum AlcatelAttributeType : byte
    {
        /// <summary>Alcatel-Access-Level-Data-Filter (Type 1). String. Data filter applied to the access level.</summary>
        ACCESS_LEVEL_DATA_FILTER = 1,

        /// <summary>Alcatel-Home-Directory (Type 2). String. Home directory path for the user.</summary>
        HOME_DIRECTORY = 2,

        /// <summary>Alcatel-Profile-String (Type 3). String. CLI profile string.</summary>
        PROFILE_STRING = 3,

        /// <summary>Alcatel-PPP-Address (Type 4). IP address. PPP peer IP address.</summary>
        PPP_ADDRESS = 4,

        /// <summary>Alcatel-PPP-Netmask (Type 5). IP address. PPP peer subnet mask.</summary>
        PPP_NETMASK = 5,

        /// <summary>Alcatel-Primary-DNS (Type 9). IP address. Primary DNS server address.</summary>
        PRIMARY_DNS = 9,

        /// <summary>Alcatel-Secondary-DNS (Type 10). IP address. Secondary DNS server address.</summary>
        SECONDARY_DNS = 10,

        /// <summary>Alcatel-Primary-NBNS (Type 11). IP address. Primary NBNS (WINS) server address.</summary>
        PRIMARY_NBNS = 11,

        /// <summary>Alcatel-Secondary-NBNS (Type 12). IP address. Secondary NBNS (WINS) server address.</summary>
        SECONDARY_NBNS = 12,

        /// <summary>Alcatel-IPX-Node-Number (Type 13). String. IPX node number.</summary>
        IPX_NODE_NUMBER = 13,

        /// <summary>Alcatel-FR-Direct (Type 21). Integer. Frame Relay direct mode indicator.</summary>
        FR_DIRECT = 21,

        /// <summary>Alcatel-FR-Direct-Profile (Type 22). String. Frame Relay direct profile name.</summary>
        FR_DIRECT_PROFILE = 22,

        /// <summary>Alcatel-FR-Direct-DLCI (Type 23). Integer. Frame Relay direct DLCI number.</summary>
        FR_DIRECT_DLCI = 23,

        /// <summary>Alcatel-ATM-Direct (Type 24). Integer. ATM direct mode indicator.</summary>
        ATM_DIRECT = 24,

        /// <summary>Alcatel-ATM-Direct-Profile (Type 25). String. ATM direct profile name.</summary>
        ATM_DIRECT_PROFILE = 25,

        /// <summary>Alcatel-Client-Primary-DNS (Type 26). IP address. Client primary DNS server address.</summary>
        CLIENT_PRIMARY_DNS = 26,

        /// <summary>Alcatel-Client-Secondary-DNS (Type 27). IP address. Client secondary DNS server address.</summary>
        CLIENT_SECONDARY_DNS = 27,

        /// <summary>Alcatel-Client-Primary-WINS-NBNS (Type 28). IP address. Client primary WINS server address.</summary>
        CLIENT_PRIMARY_WINS_NBNS = 28,

        /// <summary>Alcatel-Client-Secondary-WINS-NBNS (Type 29). IP address. Client secondary WINS server address.</summary>
        CLIENT_SECONDARY_WINS_NBNS = 29,

        /// <summary>Alcatel-Inter-Dest-Id (Type 30). String. Interconnect destination identifier.</summary>
        INTER_DEST_ID = 30,

        /// <summary>Alcatel-Primary-Home-Agent (Type 31). String. Primary Mobile IP home agent.</summary>
        PRIMARY_HOME_AGENT = 31,

        /// <summary>Alcatel-Secondary-Home-Agent (Type 32). String. Secondary Mobile IP home agent.</summary>
        SECONDARY_HOME_AGENT = 32,

        /// <summary>Alcatel-Home-Agent-Password (Type 33). String. Mobile IP home agent password.</summary>
        HOME_AGENT_PASSWORD = 33,

        /// <summary>Alcatel-Home-Network-Name (Type 34). String. Mobile IP home network name.</summary>
        HOME_NETWORK_NAME = 34,

        /// <summary>Alcatel-Home-Agent-UDP-Port (Type 35). Integer. Mobile IP home agent UDP port.</summary>
        HOME_AGENT_UDP_PORT = 35,

        /// <summary>Alcatel-IP-Direct (Type 36). IP address. IP direct address.</summary>
        IP_DIRECT = 36,

        /// <summary>Alcatel-FR-Link-Status-DLCI (Type 37). Integer. Frame Relay link status DLCI.</summary>
        FR_LINK_STATUS_DLCI = 37,

        /// <summary>Alcatel-Dest-IP-Filter (Type 38). String. Destination IP filter name.</summary>
        DEST_IP_FILTER = 38,

        /// <summary>Alcatel-Source-IP-Filter (Type 39). String. Source IP filter name.</summary>
        SOURCE_IP_FILTER = 39,

        /// <summary>Alcatel-ATM-Fault-Management (Type 48). String. ATM fault management (OAM) settings.</summary>
        ATM_FAULT_MANAGEMENT = 48,

        /// <summary>Alcatel-ATM-Loopback-Cell-Prd (Type 49). Integer. ATM loopback cell period in seconds.</summary>
        ATM_LOOPBACK_CELL_PRD = 49,

        /// <summary>Alcatel-Svc-Id (Type 51). Integer. Service identifier.</summary>
        SVC_ID = 51,

        /// <summary>Alcatel-Svc-Name (Type 52). String. Service name.</summary>
        SVC_NAME = 52,

        /// <summary>Alcatel-SAP-Id (Type 53). String. Service Access Point identifier.</summary>
        SAP_ID = 53,

        /// <summary>Alcatel-Resource-Pool-Id (Type 54). Integer. Resource pool identifier.</summary>
        RESOURCE_POOL_ID = 54,

        /// <summary>Alcatel-Multicast-Package (Type 55). String. Multicast package name.</summary>
        MULTICAST_PACKAGE = 55,

        /// <summary>Alcatel-Acct-I-Inprof-Octets-64 (Type 56). Octets. Ingress in-profile octets (64-bit counter).</summary>
        ACCT_I_INPROF_OCTETS_64 = 56,

        /// <summary>Alcatel-Acct-I-Outprof-Octets-64 (Type 57). Octets. Ingress out-of-profile octets (64-bit counter).</summary>
        ACCT_I_OUTPROF_OCTETS_64 = 57,

        /// <summary>Alcatel-Acct-O-Inprof-Octets-64 (Type 58). Octets. Egress in-profile octets (64-bit counter).</summary>
        ACCT_O_INPROF_OCTETS_64 = 58,

        /// <summary>Alcatel-Acct-O-Outprof-Octets-64 (Type 59). Octets. Egress out-of-profile octets (64-bit counter).</summary>
        ACCT_O_OUTPROF_OCTETS_64 = 59,

        /// <summary>Alcatel-Acct-I-Inprof-Pkts-64 (Type 60). Octets. Ingress in-profile packets (64-bit counter).</summary>
        ACCT_I_INPROF_PKTS_64 = 60,

        /// <summary>Alcatel-Acct-I-Outprof-Pkts-64 (Type 61). Octets. Ingress out-of-profile packets (64-bit counter).</summary>
        ACCT_I_OUTPROF_PKTS_64 = 61,

        /// <summary>Alcatel-Acct-O-Inprof-Pkts-64 (Type 62). Octets. Egress in-profile packets (64-bit counter).</summary>
        ACCT_O_INPROF_PKTS_64 = 62,

        /// <summary>Alcatel-Acct-O-Outprof-Pkts-64 (Type 63). Octets. Egress out-of-profile packets (64-bit counter).</summary>
        ACCT_O_OUTPROF_PKTS_64 = 63,

        /// <summary>Alcatel-Client-Hardware-Addr (Type 64). String. Client hardware (MAC) address.</summary>
        CLIENT_HARDWARE_ADDR = 64,

        /// <summary>Alcatel-Acct-Triggered-Reason (Type 65). Integer. Triggered accounting reason code.</summary>
        ACCT_TRIGGERED_REASON = 65,

        /// <summary>Alcatel-Mgmt-Server-Id (Type 66). IP address. Management server IP address.</summary>
        MGMT_SERVER_ID = 66,

        /// <summary>Alcatel-Acct-Input-Inprof-Octets-64 (Type 67). Octets. Accounting input in-profile octets (64-bit).</summary>
        ACCT_INPUT_INPROF_OCTETS_64 = 67,

        /// <summary>Alcatel-Acct-Input-Outprof-Octets-64 (Type 68). Octets. Accounting input out-of-profile octets (64-bit).</summary>
        ACCT_INPUT_OUTPROF_OCTETS_64 = 68,

        /// <summary>Alcatel-Acct-Output-Inprof-Octets-64 (Type 69). Octets. Accounting output in-profile octets (64-bit).</summary>
        ACCT_OUTPUT_INPROF_OCTETS_64 = 69,

        /// <summary>Alcatel-Acct-Output-Outprof-Octets-64 (Type 70). Octets. Accounting output out-of-profile octets (64-bit).</summary>
        ACCT_OUTPUT_OUTPROF_OCTETS_64 = 70,

        /// <summary>Alcatel-Acct-Input-Inprof-Pkts-64 (Type 71). Octets. Accounting input in-profile packets (64-bit).</summary>
        ACCT_INPUT_INPROF_PKTS_64 = 71,

        /// <summary>Alcatel-Acct-Input-Outprof-Pkts-64 (Type 72). Octets. Accounting input out-of-profile packets (64-bit).</summary>
        ACCT_INPUT_OUTPROF_PKTS_64 = 72,

        /// <summary>Alcatel-Acct-Output-Inprof-Pkts-64 (Type 73). Octets. Accounting output in-profile packets (64-bit).</summary>
        ACCT_OUTPUT_INPROF_PKTS_64 = 73,

        /// <summary>Alcatel-Acct-Output-Outprof-Pkts-64 (Type 74). Octets. Accounting output out-of-profile packets (64-bit).</summary>
        ACCT_OUTPUT_OUTPROF_PKTS_64 = 74,

        /// <summary>Alcatel-Subscriber-Id (Type 75). String. Subscriber identifier.</summary>
        SUBSCRIBER_ID = 75,

        /// <summary>Alcatel-Subscriber-Prof-String (Type 76). String. Subscriber profile string.</summary>
        SUBSCRIBER_PROF_STRING = 76,

        /// <summary>Alcatel-SLA-Prof-String (Type 77). String. SLA profile string.</summary>
        SLA_PROF_STRING = 77,

        /// <summary>Alcatel-Force-Renew (Type 78). String. Force DHCP renew parameter.</summary>
        FORCE_RENEW = 78,

        /// <summary>Alcatel-Create-Host (Type 79). String. Create host binding parameter.</summary>
        CREATE_HOST = 79,

        /// <summary>Alcatel-ANCP-String (Type 80). String. ANCP (Access Node Control Protocol) string.</summary>
        ANCP_STRING = 80,

        /// <summary>Alcatel-Retail-Svc-Id (Type 81). Integer. Retail service identifier.</summary>
        RETAIL_SVC_ID = 81,

        /// <summary>Alcatel-Default-Router (Type 82). IP address. Default router (gateway) IP address.</summary>
        DEFAULT_ROUTER = 82,

        /// <summary>Alcatel-Acct-Interim-IvI-64 (Type 84). Octets. Interim accounting interval (64-bit).</summary>
        ACCT_INTERIM_IVL_64 = 84,

        /// <summary>Alcatel-Lease-Time (Type 85). Integer. IP address lease time in seconds.</summary>
        LEASE_TIME = 85,

        /// <summary>Alcatel-DSL-Line-State (Type 86). Integer. DSL line state indicator.</summary>
        DSL_LINE_STATE = 86,

        /// <summary>Alcatel-Carbon-Install-Policy (Type 87). String. Carbon install policy name.</summary>
        CARBON_INSTALL_POLICY = 87,

        /// <summary>Alcatel-Tunnel-Group (Type 88). String. Tunnel group name.</summary>
        TUNNEL_GROUP = 88,

        /// <summary>Alcatel-Tunnel-Algorithm (Type 89). Integer. Tunnel selection algorithm.</summary>
        TUNNEL_ALGORITHM = 89,

        /// <summary>Alcatel-Tunnel-Max-Sessions (Type 90). Integer. Maximum tunnel sessions.</summary>
        TUNNEL_MAX_SESSIONS = 90,

        /// <summary>Alcatel-Tunnel-Idle-Timeout (Type 91). Integer. Tunnel idle timeout in seconds.</summary>
        TUNNEL_IDLE_TIMEOUT = 91,

        /// <summary>Alcatel-Tunnel-Hello-Interval (Type 92). Integer. Tunnel keepalive hello interval in seconds.</summary>
        TUNNEL_HELLO_INTERVAL = 92,

        /// <summary>Alcatel-Tunnel-Destruct-Timeout (Type 93). Integer. Tunnel destruction timeout in seconds.</summary>
        TUNNEL_DESTRUCT_TIMEOUT = 93,

        /// <summary>Alcatel-Tunnel-Max-Retry (Type 94). Integer. Maximum tunnel setup retries.</summary>
        TUNNEL_MAX_RETRY = 94,

        /// <summary>Alcatel-Tunnel-AVP-Hidden (Type 95). Integer. Whether tunnel AVPs are hidden.</summary>
        TUNNEL_AVP_HIDDEN = 95,

        /// <summary>Alcatel-BGP-Policy (Type 96). String. BGP policy name.</summary>
        BGP_POLICY = 96,

        /// <summary>Alcatel-BGP-Auth-Keychain (Type 97). String. BGP authentication keychain name.</summary>
        BGP_AUTH_KEYCHAIN = 97,

        /// <summary>Alcatel-BGP-Auth-Key (Type 98). String. BGP authentication key.</summary>
        BGP_AUTH_KEY = 98,

        /// <summary>Alcatel-BGP-Export-Policy (Type 99). String. BGP export policy name.</summary>
        BGP_EXPORT_POLICY = 99,

        /// <summary>Alcatel-BGP-Import-Policy (Type 100). String. BGP import policy name.</summary>
        BGP_IMPORT_POLICY = 100,

        /// <summary>Alcatel-BGP-PeerAS (Type 101). Integer. BGP peer autonomous system number.</summary>
        BGP_PEERAS = 101,

        /// <summary>Alcatel-IPsec-Serv-Id (Type 102). Integer. IPsec service identifier.</summary>
        IPSEC_SERV_ID = 102,

        /// <summary>Alcatel-IPsec-Interface (Type 103). String. IPsec interface name.</summary>
        IPSEC_INTERFACE = 103,

        /// <summary>Alcatel-IPsec-Tunnel-Template-Id (Type 104). Integer. IPsec tunnel template identifier.</summary>
        IPSEC_TUNNEL_TEMPLATE_ID = 104,

        /// <summary>Alcatel-IPsec-SA-Lifetime (Type 105). Integer. IPsec SA lifetime in seconds.</summary>
        IPSEC_SA_LIFETIME = 105,

        /// <summary>Alcatel-IPsec-SA-PFS-Group (Type 106). Integer. IPsec SA PFS Diffie-Hellman group.</summary>
        IPSEC_SA_PFS_GROUP = 106,

        /// <summary>Alcatel-IPsec-SA-Encr-Algorithm (Type 107). Integer. IPsec SA encryption algorithm.</summary>
        IPSEC_SA_ENCR_ALGORITHM = 107,

        /// <summary>Alcatel-IPsec-SA-Auth-Algorithm (Type 108). Integer. IPsec SA authentication algorithm.</summary>
        IPSEC_SA_AUTH_ALGORITHM = 108,

        /// <summary>Alcatel-IPsec-SA-Replay-Window (Type 109). Integer. IPsec SA anti-replay window size.</summary>
        IPSEC_SA_REPLAY_WINDOW = 109,

        /// <summary>Alcatel-Acct-I-High-Octets-Drop-64 (Type 110). Octets. Ingress high-priority dropped octets (64-bit).</summary>
        ACCT_I_HIGH_OCTETS_DROP_64 = 110,

        /// <summary>Alcatel-Acct-I-Low-Octets-Drop-64 (Type 111). Octets. Ingress low-priority dropped octets (64-bit).</summary>
        ACCT_I_LOW_OCTETS_DROP_64 = 111,

        /// <summary>Alcatel-Acct-I-High-Pack-Drop-64 (Type 112). Octets. Ingress high-priority dropped packets (64-bit).</summary>
        ACCT_I_HIGH_PACK_DROP_64 = 112,

        /// <summary>Alcatel-Acct-I-Low-Pack-Drop-64 (Type 113). Octets. Ingress low-priority dropped packets (64-bit).</summary>
        ACCT_I_LOW_PACK_DROP_64 = 113,

        /// <summary>Alcatel-Acct-I-High-Octets-Offer-64 (Type 114). Octets. Ingress high-priority offered octets (64-bit).</summary>
        ACCT_I_HIGH_OCTETS_OFFER_64 = 114,

        /// <summary>Alcatel-Acct-I-Low-Octets-Offer-64 (Type 115). Octets. Ingress low-priority offered octets (64-bit).</summary>
        ACCT_I_LOW_OCTETS_OFFER_64 = 115,

        /// <summary>Alcatel-Acct-I-High-Pack-Offer-64 (Type 116). Octets. Ingress high-priority offered packets (64-bit).</summary>
        ACCT_I_HIGH_PACK_OFFER_64 = 116,

        /// <summary>Alcatel-Acct-I-Low-Pack-Offer-64 (Type 117). Octets. Ingress low-priority offered packets (64-bit).</summary>
        ACCT_I_LOW_PACK_OFFER_64 = 117,

        /// <summary>Alcatel-MSAP-Serv-Id (Type 120). Integer. MSAP service identifier.</summary>
        MSAP_SERV_ID = 120,

        /// <summary>Alcatel-MSAP-Policy (Type 121). String. MSAP policy name.</summary>
        MSAP_POLICY = 121,

        /// <summary>Alcatel-MSAP-Interface (Type 122). String. MSAP interface name.</summary>
        MSAP_INTERFACE = 122,

        /// <summary>Alcatel-PPPoE-Service-Name (Type 123). String. PPPoE service name.</summary>
        PPPOE_SERVICE_NAME = 123,

        /// <summary>Alcatel-Tunnel-Acct-Policy (Type 124). String. Tunnel accounting policy name.</summary>
        TUNNEL_ACCT_POLICY = 124,

        /// <summary>Alcatel-Acct-I-Unc-Octets-Offer-64 (Type 125). Octets. Ingress uncoloured offered octets (64-bit).</summary>
        ACCT_I_UNC_OCTETS_OFFER_64 = 125,

        /// <summary>Alcatel-Acct-I-Unc-Pack-Offer-64 (Type 126). Octets. Ingress uncoloured offered packets (64-bit).</summary>
        ACCT_I_UNC_PACK_OFFER_64 = 126,

        /// <summary>Alcatel-Acct-I-All-Octets-Offer-64 (Type 127). Octets. Ingress all offered octets (64-bit).</summary>
        ACCT_I_ALL_OCTETS_OFFER_64 = 127,

        /// <summary>Alcatel-Acct-I-All-Pack-Offer-64 (Type 128). Octets. Ingress all offered packets (64-bit).</summary>
        ACCT_I_ALL_PACK_OFFER_64 = 128,

        /// <summary>Alcatel-Acct-O-Inprof-Pack-Drop-64 (Type 129). Octets. Egress in-profile dropped packets (64-bit).</summary>
        ACCT_O_INPROF_PACK_DROP_64 = 129,

        /// <summary>Alcatel-Acct-O-Outprof-Pack-Drop-64 (Type 130). Octets. Egress out-of-profile dropped packets (64-bit).</summary>
        ACCT_O_OUTPROF_PACK_DROP_64 = 130,

        /// <summary>Alcatel-Acct-O-Inprof-Octs-Drop-64 (Type 131). Octets. Egress in-profile dropped octets (64-bit).</summary>
        ACCT_O_INPROF_OCTS_DROP_64 = 131,

        /// <summary>Alcatel-Acct-O-Outprof-Octs-Drop-64 (Type 132). Octets. Egress out-of-profile dropped octets (64-bit).</summary>
        ACCT_O_OUTPROF_OCTS_DROP_64 = 132
    }

    /// <summary>
    /// Alcatel-FR-Direct attribute values (Type 21).
    /// </summary>
    public enum ALCATEL_FR_DIRECT
    {
        /// <summary>Frame Relay direct disabled.</summary>
        DISABLED = 0,

        /// <summary>Frame Relay direct enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Alcatel-ATM-Direct attribute values (Type 24).
    /// </summary>
    public enum ALCATEL_ATM_DIRECT
    {
        /// <summary>ATM direct disabled.</summary>
        DISABLED = 0,

        /// <summary>ATM direct enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Alcatel-Acct-Triggered-Reason attribute values (Type 65).
    /// </summary>
    public enum ALCATEL_ACCT_TRIGGERED_REASON
    {
        /// <summary>Rate limit exceeded.</summary>
        RATE_LIMIT = 1,

        /// <summary>IGMP event triggered.</summary>
        IGMP_EVENT = 2,

        /// <summary>Policy change event.</summary>
        POLICY_CHANGE = 3,

        /// <summary>Decoupled accounting.</summary>
        DECOUPLED = 4,

        /// <summary>CoA request received.</summary>
        COA_REQUEST = 5
    }

    /// <summary>
    /// Alcatel-DSL-Line-State attribute values (Type 86).
    /// </summary>
    public enum ALCATEL_DSL_LINE_STATE
    {
        /// <summary>DSL line is idle / not active.</summary>
        NONE = 0,

        /// <summary>DSL line is in showtime (connected).</summary>
        SHOW_TIME = 1,

        /// <summary>DSL line is idle.</summary>
        IDLE = 2,

        /// <summary>DSL line is in a silent state.</summary>
        SILENT = 3
    }

    /// <summary>
    /// Alcatel-Tunnel-Algorithm attribute values (Type 89).
    /// </summary>
    public enum ALCATEL_TUNNEL_ALGORITHM
    {
        /// <summary>First available tunnel.</summary>
        FIRST_AVAILABLE = 0,

        /// <summary>Load-balanced across tunnels.</summary>
        LOAD_BALANCED = 1,

        /// <summary>Weighted load balancing.</summary>
        WEIGHTED = 2
    }

    /// <summary>
    /// Alcatel-Tunnel-AVP-Hidden attribute values (Type 95).
    /// </summary>
    public enum ALCATEL_TUNNEL_AVP_HIDDEN
    {
        /// <summary>AVP hiding disabled.</summary>
        NOTHING = 0,

        /// <summary>Sensitive AVPs are hidden.</summary>
        SENSITIVE_ONLY = 1,

        /// <summary>All AVPs are hidden.</summary>
        ALL = 2
    }

    /// <summary>
    /// Alcatel-IPsec-SA-PFS-Group attribute values (Type 106).
    /// </summary>
    public enum ALCATEL_IPSEC_SA_PFS_GROUP
    {
        /// <summary>No PFS.</summary>
        NONE = 0,

        /// <summary>Diffie-Hellman Group 1 (768-bit).</summary>
        GROUP_1 = 1,

        /// <summary>Diffie-Hellman Group 2 (1024-bit).</summary>
        GROUP_2 = 2,

        /// <summary>Diffie-Hellman Group 5 (1536-bit).</summary>
        GROUP_5 = 5,

        /// <summary>Diffie-Hellman Group 14 (2048-bit).</summary>
        GROUP_14 = 14,

        /// <summary>Diffie-Hellman Group 15 (3072-bit).</summary>
        GROUP_15 = 15
    }

    /// <summary>
    /// Alcatel-IPsec-SA-Encr-Algorithm attribute values (Type 107).
    /// </summary>
    public enum ALCATEL_IPSEC_SA_ENCR_ALGORITHM
    {
        /// <summary>No encryption (null).</summary>
        NULL = 0,

        /// <summary>DES-CBC encryption.</summary>
        DES = 1,

        /// <summary>3DES-CBC encryption.</summary>
        TRIPLE_DES = 2,

        /// <summary>AES-128-CBC encryption.</summary>
        AES128 = 3,

        /// <summary>AES-192-CBC encryption.</summary>
        AES192 = 4,

        /// <summary>AES-256-CBC encryption.</summary>
        AES256 = 5
    }

    /// <summary>
    /// Alcatel-IPsec-SA-Auth-Algorithm attribute values (Type 108).
    /// </summary>
    public enum ALCATEL_IPSEC_SA_AUTH_ALGORITHM
    {
        /// <summary>No authentication.</summary>
        NULL = 0,

        /// <summary>HMAC-MD5-96 authentication.</summary>
        HMAC_MD5_96 = 1,

        /// <summary>HMAC-SHA1-96 authentication.</summary>
        HMAC_SHA1_96 = 2,

        /// <summary>HMAC-SHA-256 authentication.</summary>
        HMAC_SHA256 = 3,

        /// <summary>HMAC-SHA-384 authentication.</summary>
        HMAC_SHA384 = 4,

        /// <summary>HMAC-SHA-512 authentication.</summary>
        HMAC_SHA512 = 5
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Alcatel-Lucent / Nokia SR OS
    /// (IANA PEN 3041) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.alcatel</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Alcatel-Lucent's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3041</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Alcatel-Lucent (now Nokia) Service Routers (SR OS) for
    /// subscriber management, QoS and SLA profile assignment, IPsec configuration, BGP
    /// policy, tunnel management, and 64-bit accounting counters.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AlcatelAttributes.SubscriberProfString("residential"));
    /// packet.SetAttribute(AlcatelAttributes.SlaProfString("10Mbps-SLA"));
    /// packet.SetAttribute(AlcatelAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(AlcatelAttributes.SvcId(100));
    /// </code>
    /// </remarks>
    public static class AlcatelAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Alcatel-Lucent (Nokia).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3041;

        #region Integer Attributes

        /// <summary>
        /// Creates an Alcatel-FR-Direct attribute (Type 21) with the specified mode.
        /// </summary>
        /// <param name="value">The Frame Relay direct mode. See <see cref="ALCATEL_FR_DIRECT"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FrDirect(ALCATEL_FR_DIRECT value)
        {
            return CreateInteger(AlcatelAttributeType.FR_DIRECT, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-FR-Direct-DLCI attribute (Type 23) with the specified DLCI number.
        /// </summary>
        /// <param name="value">The Frame Relay direct DLCI number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FrDirectDlci(int value)
        {
            return CreateInteger(AlcatelAttributeType.FR_DIRECT_DLCI, value);
        }

        /// <summary>
        /// Creates an Alcatel-ATM-Direct attribute (Type 24) with the specified mode.
        /// </summary>
        /// <param name="value">The ATM direct mode. See <see cref="ALCATEL_ATM_DIRECT"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AtmDirect(ALCATEL_ATM_DIRECT value)
        {
            return CreateInteger(AlcatelAttributeType.ATM_DIRECT, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-Home-Agent-UDP-Port attribute (Type 35) with the specified port.
        /// </summary>
        /// <param name="value">The Mobile IP home agent UDP port.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes HomeAgentUdpPort(int value)
        {
            return CreateInteger(AlcatelAttributeType.HOME_AGENT_UDP_PORT, value);
        }

        /// <summary>
        /// Creates an Alcatel-FR-Link-Status-DLCI attribute (Type 37) with the specified DLCI.
        /// </summary>
        /// <param name="value">The Frame Relay link status DLCI.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FrLinkStatusDlci(int value)
        {
            return CreateInteger(AlcatelAttributeType.FR_LINK_STATUS_DLCI, value);
        }

        /// <summary>
        /// Creates an Alcatel-ATM-Loopback-Cell-Prd attribute (Type 49) with the specified period.
        /// </summary>
        /// <param name="value">The ATM loopback cell period in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AtmLoopbackCellPrd(int value)
        {
            return CreateInteger(AlcatelAttributeType.ATM_LOOPBACK_CELL_PRD, value);
        }

        /// <summary>
        /// Creates an Alcatel-Svc-Id attribute (Type 51) with the specified service identifier.
        /// </summary>
        /// <param name="value">The service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SvcId(int value)
        {
            return CreateInteger(AlcatelAttributeType.SVC_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-Resource-Pool-Id attribute (Type 54) with the specified pool identifier.
        /// </summary>
        /// <param name="value">The resource pool identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ResourcePoolId(int value)
        {
            return CreateInteger(AlcatelAttributeType.RESOURCE_POOL_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-Acct-Triggered-Reason attribute (Type 65) with the specified reason.
        /// </summary>
        /// <param name="value">The triggered accounting reason code. See <see cref="ALCATEL_ACCT_TRIGGERED_REASON"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctTriggeredReason(ALCATEL_ACCT_TRIGGERED_REASON value)
        {
            return CreateInteger(AlcatelAttributeType.ACCT_TRIGGERED_REASON, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-Retail-Svc-Id attribute (Type 81) with the specified service identifier.
        /// </summary>
        /// <param name="value">The retail service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RetailSvcId(int value)
        {
            return CreateInteger(AlcatelAttributeType.RETAIL_SVC_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-Lease-Time attribute (Type 85) with the specified lease time.
        /// </summary>
        /// <param name="value">The IP address lease time in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LeaseTime(int value)
        {
            return CreateInteger(AlcatelAttributeType.LEASE_TIME, value);
        }

        /// <summary>
        /// Creates an Alcatel-DSL-Line-State attribute (Type 86) with the specified state.
        /// </summary>
        /// <param name="value">The DSL line state. See <see cref="ALCATEL_DSL_LINE_STATE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DslLineState(ALCATEL_DSL_LINE_STATE value)
        {
            return CreateInteger(AlcatelAttributeType.DSL_LINE_STATE, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-Tunnel-Algorithm attribute (Type 89) with the specified algorithm.
        /// </summary>
        /// <param name="value">The tunnel selection algorithm. See <see cref="ALCATEL_TUNNEL_ALGORITHM"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelAlgorithm(ALCATEL_TUNNEL_ALGORITHM value)
        {
            return CreateInteger(AlcatelAttributeType.TUNNEL_ALGORITHM, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-Tunnel-Max-Sessions attribute (Type 90) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum tunnel sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelMaxSessions(int value)
        {
            return CreateInteger(AlcatelAttributeType.TUNNEL_MAX_SESSIONS, value);
        }

        /// <summary>
        /// Creates an Alcatel-Tunnel-Idle-Timeout attribute (Type 91) with the specified timeout.
        /// </summary>
        /// <param name="value">The tunnel idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelIdleTimeout(int value)
        {
            return CreateInteger(AlcatelAttributeType.TUNNEL_IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates an Alcatel-Tunnel-Hello-Interval attribute (Type 92) with the specified interval.
        /// </summary>
        /// <param name="value">The tunnel keepalive hello interval in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelHelloInterval(int value)
        {
            return CreateInteger(AlcatelAttributeType.TUNNEL_HELLO_INTERVAL, value);
        }

        /// <summary>
        /// Creates an Alcatel-Tunnel-Destruct-Timeout attribute (Type 93) with the specified timeout.
        /// </summary>
        /// <param name="value">The tunnel destruction timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelDestructTimeout(int value)
        {
            return CreateInteger(AlcatelAttributeType.TUNNEL_DESTRUCT_TIMEOUT, value);
        }

        /// <summary>
        /// Creates an Alcatel-Tunnel-Max-Retry attribute (Type 94) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum tunnel setup retries.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelMaxRetry(int value)
        {
            return CreateInteger(AlcatelAttributeType.TUNNEL_MAX_RETRY, value);
        }

        /// <summary>
        /// Creates an Alcatel-Tunnel-AVP-Hidden attribute (Type 95) with the specified hiding mode.
        /// </summary>
        /// <param name="value">The AVP hiding mode. See <see cref="ALCATEL_TUNNEL_AVP_HIDDEN"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelAvpHidden(ALCATEL_TUNNEL_AVP_HIDDEN value)
        {
            return CreateInteger(AlcatelAttributeType.TUNNEL_AVP_HIDDEN, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-BGP-PeerAS attribute (Type 101) with the specified AS number.
        /// </summary>
        /// <param name="value">The BGP peer autonomous system number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BgpPeerAs(int value)
        {
            return CreateInteger(AlcatelAttributeType.BGP_PEERAS, value);
        }

        /// <summary>
        /// Creates an Alcatel-IPsec-Serv-Id attribute (Type 102) with the specified service identifier.
        /// </summary>
        /// <param name="value">The IPsec service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecServId(int value)
        {
            return CreateInteger(AlcatelAttributeType.IPSEC_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-IPsec-Tunnel-Template-Id attribute (Type 104) with the specified template ID.
        /// </summary>
        /// <param name="value">The IPsec tunnel template identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecTunnelTemplateId(int value)
        {
            return CreateInteger(AlcatelAttributeType.IPSEC_TUNNEL_TEMPLATE_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-IPsec-SA-Lifetime attribute (Type 105) with the specified lifetime.
        /// </summary>
        /// <param name="value">The IPsec SA lifetime in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaLifetime(int value)
        {
            return CreateInteger(AlcatelAttributeType.IPSEC_SA_LIFETIME, value);
        }

        /// <summary>
        /// Creates an Alcatel-IPsec-SA-PFS-Group attribute (Type 106) with the specified DH group.
        /// </summary>
        /// <param name="value">The IPsec SA PFS DH group. See <see cref="ALCATEL_IPSEC_SA_PFS_GROUP"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaPfsGroup(ALCATEL_IPSEC_SA_PFS_GROUP value)
        {
            return CreateInteger(AlcatelAttributeType.IPSEC_SA_PFS_GROUP, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-IPsec-SA-Encr-Algorithm attribute (Type 107) with the specified algorithm.
        /// </summary>
        /// <param name="value">The IPsec SA encryption algorithm. See <see cref="ALCATEL_IPSEC_SA_ENCR_ALGORITHM"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaEncrAlgorithm(ALCATEL_IPSEC_SA_ENCR_ALGORITHM value)
        {
            return CreateInteger(AlcatelAttributeType.IPSEC_SA_ENCR_ALGORITHM, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-IPsec-SA-Auth-Algorithm attribute (Type 108) with the specified algorithm.
        /// </summary>
        /// <param name="value">The IPsec SA authentication algorithm. See <see cref="ALCATEL_IPSEC_SA_AUTH_ALGORITHM"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaAuthAlgorithm(ALCATEL_IPSEC_SA_AUTH_ALGORITHM value)
        {
            return CreateInteger(AlcatelAttributeType.IPSEC_SA_AUTH_ALGORITHM, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-IPsec-SA-Replay-Window attribute (Type 109) with the specified window size.
        /// </summary>
        /// <param name="value">The IPsec SA anti-replay window size.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaReplayWindow(int value)
        {
            return CreateInteger(AlcatelAttributeType.IPSEC_SA_REPLAY_WINDOW, value);
        }

        /// <summary>
        /// Creates an Alcatel-MSAP-Serv-Id attribute (Type 120) with the specified identifier.
        /// </summary>
        /// <param name="value">The MSAP service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MsapServId(int value)
        {
            return CreateInteger(AlcatelAttributeType.MSAP_SERV_ID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Alcatel-Access-Level-Data-Filter attribute (Type 1) with the specified filter.
        /// </summary>
        /// <param name="value">The data filter applied to the access level. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessLevelDataFilter(string value)
        {
            return CreateString(AlcatelAttributeType.ACCESS_LEVEL_DATA_FILTER, value);
        }

        /// <summary>
        /// Creates an Alcatel-Home-Directory attribute (Type 2) with the specified path.
        /// </summary>
        /// <param name="value">The home directory path. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HomeDirectory(string value)
        {
            return CreateString(AlcatelAttributeType.HOME_DIRECTORY, value);
        }

        /// <summary>
        /// Creates an Alcatel-Profile-String attribute (Type 3) with the specified profile.
        /// </summary>
        /// <param name="value">The CLI profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProfileString(string value)
        {
            return CreateString(AlcatelAttributeType.PROFILE_STRING, value);
        }

        /// <summary>
        /// Creates an Alcatel-IPX-Node-Number attribute (Type 13) with the specified node number.
        /// </summary>
        /// <param name="value">The IPX node number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpxNodeNumber(string value)
        {
            return CreateString(AlcatelAttributeType.IPX_NODE_NUMBER, value);
        }

        /// <summary>
        /// Creates an Alcatel-FR-Direct-Profile attribute (Type 22) with the specified profile name.
        /// </summary>
        /// <param name="value">The Frame Relay direct profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FrDirectProfile(string value)
        {
            return CreateString(AlcatelAttributeType.FR_DIRECT_PROFILE, value);
        }

        /// <summary>
        /// Creates an Alcatel-ATM-Direct-Profile attribute (Type 25) with the specified profile name.
        /// </summary>
        /// <param name="value">The ATM direct profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AtmDirectProfile(string value)
        {
            return CreateString(AlcatelAttributeType.ATM_DIRECT_PROFILE, value);
        }

        /// <summary>
        /// Creates an Alcatel-Inter-Dest-Id attribute (Type 30) with the specified destination ID.
        /// </summary>
        /// <param name="value">The interconnect destination identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes InterDestId(string value)
        {
            return CreateString(AlcatelAttributeType.INTER_DEST_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-Primary-Home-Agent attribute (Type 31) with the specified home agent.
        /// </summary>
        /// <param name="value">The primary Mobile IP home agent. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrimaryHomeAgent(string value)
        {
            return CreateString(AlcatelAttributeType.PRIMARY_HOME_AGENT, value);
        }

        /// <summary>
        /// Creates an Alcatel-Secondary-Home-Agent attribute (Type 32) with the specified home agent.
        /// </summary>
        /// <param name="value">The secondary Mobile IP home agent. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SecondaryHomeAgent(string value)
        {
            return CreateString(AlcatelAttributeType.SECONDARY_HOME_AGENT, value);
        }

        /// <summary>
        /// Creates an Alcatel-Home-Agent-Password attribute (Type 33) with the specified password.
        /// </summary>
        /// <param name="value">The Mobile IP home agent password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HomeAgentPassword(string value)
        {
            return CreateString(AlcatelAttributeType.HOME_AGENT_PASSWORD, value);
        }

        /// <summary>
        /// Creates an Alcatel-Home-Network-Name attribute (Type 34) with the specified network name.
        /// </summary>
        /// <param name="value">The Mobile IP home network name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HomeNetworkName(string value)
        {
            return CreateString(AlcatelAttributeType.HOME_NETWORK_NAME, value);
        }

        /// <summary>
        /// Creates an Alcatel-Dest-IP-Filter attribute (Type 38) with the specified filter name.
        /// </summary>
        /// <param name="value">The destination IP filter name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DestIpFilter(string value)
        {
            return CreateString(AlcatelAttributeType.DEST_IP_FILTER, value);
        }

        /// <summary>
        /// Creates an Alcatel-Source-IP-Filter attribute (Type 39) with the specified filter name.
        /// </summary>
        /// <param name="value">The source IP filter name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SourceIpFilter(string value)
        {
            return CreateString(AlcatelAttributeType.SOURCE_IP_FILTER, value);
        }

        /// <summary>
        /// Creates an Alcatel-ATM-Fault-Management attribute (Type 48) with the specified settings.
        /// </summary>
        /// <param name="value">The ATM fault management (OAM) settings. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AtmFaultManagement(string value)
        {
            return CreateString(AlcatelAttributeType.ATM_FAULT_MANAGEMENT, value);
        }

        /// <summary>
        /// Creates an Alcatel-Svc-Name attribute (Type 52) with the specified service name.
        /// </summary>
        /// <param name="value">The service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SvcName(string value)
        {
            return CreateString(AlcatelAttributeType.SVC_NAME, value);
        }

        /// <summary>
        /// Creates an Alcatel-SAP-Id attribute (Type 53) with the specified SAP identifier.
        /// </summary>
        /// <param name="value">The Service Access Point identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SapId(string value)
        {
            return CreateString(AlcatelAttributeType.SAP_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-Multicast-Package attribute (Type 55) with the specified package name.
        /// </summary>
        /// <param name="value">The multicast package name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MulticastPackage(string value)
        {
            return CreateString(AlcatelAttributeType.MULTICAST_PACKAGE, value);
        }

        /// <summary>
        /// Creates an Alcatel-Client-Hardware-Addr attribute (Type 64) with the specified MAC address.
        /// </summary>
        /// <param name="value">The client hardware (MAC) address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientHardwareAddr(string value)
        {
            return CreateString(AlcatelAttributeType.CLIENT_HARDWARE_ADDR, value);
        }

        /// <summary>
        /// Creates an Alcatel-Subscriber-Id attribute (Type 75) with the specified identifier.
        /// </summary>
        /// <param name="value">The subscriber identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberId(string value)
        {
            return CreateString(AlcatelAttributeType.SUBSCRIBER_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-Subscriber-Prof-String attribute (Type 76) with the specified profile.
        /// </summary>
        /// <param name="value">The subscriber profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberProfString(string value)
        {
            return CreateString(AlcatelAttributeType.SUBSCRIBER_PROF_STRING, value);
        }

        /// <summary>
        /// Creates an Alcatel-SLA-Prof-String attribute (Type 77) with the specified SLA profile.
        /// </summary>
        /// <param name="value">The SLA profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SlaProfString(string value)
        {
            return CreateString(AlcatelAttributeType.SLA_PROF_STRING, value);
        }

        /// <summary>
        /// Creates an Alcatel-Force-Renew attribute (Type 78) with the specified parameter.
        /// </summary>
        /// <param name="value">The force DHCP renew parameter. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ForceRenew(string value)
        {
            return CreateString(AlcatelAttributeType.FORCE_RENEW, value);
        }

        /// <summary>
        /// Creates an Alcatel-Create-Host attribute (Type 79) with the specified parameter.
        /// </summary>
        /// <param name="value">The create host binding parameter. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CreateHost(string value)
        {
            return CreateString(AlcatelAttributeType.CREATE_HOST, value);
        }

        /// <summary>
        /// Creates an Alcatel-ANCP-String attribute (Type 80) with the specified ANCP string.
        /// </summary>
        /// <param name="value">The ANCP string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AncpString(string value)
        {
            return CreateString(AlcatelAttributeType.ANCP_STRING, value);
        }

        /// <summary>
        /// Creates an Alcatel-Carbon-Install-Policy attribute (Type 87) with the specified policy name.
        /// </summary>
        /// <param name="value">The carbon install policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CarbonInstallPolicy(string value)
        {
            return CreateString(AlcatelAttributeType.CARBON_INSTALL_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-Tunnel-Group attribute (Type 88) with the specified group name.
        /// </summary>
        /// <param name="value">The tunnel group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelGroup(string value)
        {
            return CreateString(AlcatelAttributeType.TUNNEL_GROUP, value);
        }

        /// <summary>
        /// Creates an Alcatel-BGP-Policy attribute (Type 96) with the specified policy name.
        /// </summary>
        /// <param name="value">The BGP policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpPolicy(string value)
        {
            return CreateString(AlcatelAttributeType.BGP_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-BGP-Auth-Keychain attribute (Type 97) with the specified keychain name.
        /// </summary>
        /// <param name="value">The BGP authentication keychain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpAuthKeychain(string value)
        {
            return CreateString(AlcatelAttributeType.BGP_AUTH_KEYCHAIN, value);
        }

        /// <summary>
        /// Creates an Alcatel-BGP-Auth-Key attribute (Type 98) with the specified key.
        /// </summary>
        /// <param name="value">The BGP authentication key. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpAuthKey(string value)
        {
            return CreateString(AlcatelAttributeType.BGP_AUTH_KEY, value);
        }

        /// <summary>
        /// Creates an Alcatel-BGP-Export-Policy attribute (Type 99) with the specified policy name.
        /// </summary>
        /// <param name="value">The BGP export policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpExportPolicy(string value)
        {
            return CreateString(AlcatelAttributeType.BGP_EXPORT_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-BGP-Import-Policy attribute (Type 100) with the specified policy name.
        /// </summary>
        /// <param name="value">The BGP import policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpImportPolicy(string value)
        {
            return CreateString(AlcatelAttributeType.BGP_IMPORT_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-IPsec-Interface attribute (Type 103) with the specified interface name.
        /// </summary>
        /// <param name="value">The IPsec interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecInterface(string value)
        {
            return CreateString(AlcatelAttributeType.IPSEC_INTERFACE, value);
        }

        /// <summary>
        /// Creates an Alcatel-MSAP-Policy attribute (Type 121) with the specified policy name.
        /// </summary>
        /// <param name="value">The MSAP policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsapPolicy(string value)
        {
            return CreateString(AlcatelAttributeType.MSAP_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-MSAP-Interface attribute (Type 122) with the specified interface name.
        /// </summary>
        /// <param name="value">The MSAP interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsapInterface(string value)
        {
            return CreateString(AlcatelAttributeType.MSAP_INTERFACE, value);
        }

        /// <summary>
        /// Creates an Alcatel-PPPoE-Service-Name attribute (Type 123) with the specified service name.
        /// </summary>
        /// <param name="value">The PPPoE service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PppoeServiceName(string value)
        {
            return CreateString(AlcatelAttributeType.PPPOE_SERVICE_NAME, value);
        }

        /// <summary>
        /// Creates an Alcatel-Tunnel-Acct-Policy attribute (Type 124) with the specified policy name.
        /// </summary>
        /// <param name="value">The tunnel accounting policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelAcctPolicy(string value)
        {
            return CreateString(AlcatelAttributeType.TUNNEL_ACCT_POLICY, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Alcatel-PPP-Address attribute (Type 4) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The PPP peer IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PppAddress(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.PPP_ADDRESS, value);
        }

        /// <summary>
        /// Creates an Alcatel-PPP-Netmask attribute (Type 5) with the specified subnet mask.
        /// </summary>
        /// <param name="value">
        /// The PPP peer subnet mask. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PppNetmask(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.PPP_NETMASK, value);
        }

        /// <summary>
        /// Creates an Alcatel-Primary-DNS attribute (Type 9) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The primary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-Secondary-DNS attribute (Type 10) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The secondary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-Primary-NBNS attribute (Type 11) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The primary NBNS (WINS) server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryNbns(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.PRIMARY_NBNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-Secondary-NBNS attribute (Type 12) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The secondary NBNS (WINS) server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryNbns(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.SECONDARY_NBNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-Client-Primary-DNS attribute (Type 26) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The client primary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientPrimaryDns(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.CLIENT_PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-Client-Secondary-DNS attribute (Type 27) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The client secondary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientSecondaryDns(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.CLIENT_SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-Client-Primary-WINS-NBNS attribute (Type 28) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The client primary WINS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientPrimaryWinsNbns(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.CLIENT_PRIMARY_WINS_NBNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-Client-Secondary-WINS-NBNS attribute (Type 29) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The client secondary WINS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientSecondaryWinsNbns(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.CLIENT_SECONDARY_WINS_NBNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-IP-Direct attribute (Type 36) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The IP direct address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes IpDirect(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.IP_DIRECT, value);
        }

        /// <summary>
        /// Creates an Alcatel-Mgmt-Server-Id attribute (Type 66) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The management server IP address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes MgmtServerId(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.MGMT_SERVER_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-Default-Router attribute (Type 82) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The default router (gateway) IP address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes DefaultRouter(IPAddress value)
        {
            return CreateIpv4(AlcatelAttributeType.DEFAULT_ROUTER, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates an Alcatel-Acct-I-Inprof-Octets-64 attribute (Type 56) with the specified raw value.
        /// </summary>
        /// <param name="value">The ingress in-profile octets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctIInprofOctets64(byte[] value)
        {
            return CreateOctets(AlcatelAttributeType.ACCT_I_INPROF_OCTETS_64, value);
        }

        /// <summary>
        /// Creates an Alcatel-Acct-I-Outprof-Octets-64 attribute (Type 57) with the specified raw value.
        /// </summary>
        /// <param name="value">The ingress out-of-profile octets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctIOutprofOctets64(byte[] value)
        {
            return CreateOctets(AlcatelAttributeType.ACCT_I_OUTPROF_OCTETS_64, value);
        }

        /// <summary>
        /// Creates an Alcatel-Acct-O-Inprof-Octets-64 attribute (Type 58) with the specified raw value.
        /// </summary>
        /// <param name="value">The egress in-profile octets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctOInprofOctets64(byte[] value)
        {
            return CreateOctets(AlcatelAttributeType.ACCT_O_INPROF_OCTETS_64, value);
        }

        /// <summary>
        /// Creates an Alcatel-Acct-O-Outprof-Octets-64 attribute (Type 59) with the specified raw value.
        /// </summary>
        /// <param name="value">The egress out-of-profile octets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctOOutprofOctets64(byte[] value)
        {
            return CreateOctets(AlcatelAttributeType.ACCT_O_OUTPROF_OCTETS_64, value);
        }

        /// <summary>
        /// Creates an Alcatel-Acct-I-Inprof-Pkts-64 attribute (Type 60) with the specified raw value.
        /// </summary>
        /// <param name="value">The ingress in-profile packets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctIInprofPkts64(byte[] value)
        {
            return CreateOctets(AlcatelAttributeType.ACCT_I_INPROF_PKTS_64, value);
        }

        /// <summary>
        /// Creates an Alcatel-Acct-I-Outprof-Pkts-64 attribute (Type 61) with the specified raw value.
        /// </summary>
        /// <param name="value">The ingress out-of-profile packets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctIOutprofPkts64(byte[] value)
        {
            return CreateOctets(AlcatelAttributeType.ACCT_I_OUTPROF_PKTS_64, value);
        }

        /// <summary>
        /// Creates an Alcatel-Acct-O-Inprof-Pkts-64 attribute (Type 62) with the specified raw value.
        /// </summary>
        /// <param name="value">The egress in-profile packets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctOInprofPkts64(byte[] value)
        {
            return CreateOctets(AlcatelAttributeType.ACCT_O_INPROF_PKTS_64, value);
        }

        /// <summary>
        /// Creates an Alcatel-Acct-O-Outprof-Pkts-64 attribute (Type 63) with the specified raw value.
        /// </summary>
        /// <param name="value">The egress out-of-profile packets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctOOutprofPkts64(byte[] value)
        {
            return CreateOctets(AlcatelAttributeType.ACCT_O_OUTPROF_PKTS_64, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Alcatel attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AlcatelAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Alcatel attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AlcatelAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a raw byte array value for the specified Alcatel attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateOctets(AlcatelAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified Alcatel attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(AlcatelAttributeType type, IPAddress value)
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