using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Alcatel-Lucent AAA / Nokia SR OS (IANA PEN 6527) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.alcatel-lucent.aaa</c>.
    /// </summary>
    public enum AlcatelLucentAaaAttributeType : byte
    {
        /// <summary>Alc-Client-Hardware-Addr (Type 1). String. Client hardware (MAC) address.</summary>
        CLIENT_HARDWARE_ADDR = 1,

        /// <summary>Alc-Subsc-ID-Str (Type 11). String. Subscriber identifier string.</summary>
        SUBSC_ID_STR = 11,

        /// <summary>Alc-Subsc-Prof-Str (Type 12). String. Subscriber profile string.</summary>
        SUBSC_PROF_STR = 12,

        /// <summary>Alc-SLA-Prof-Str (Type 13). String. SLA profile string.</summary>
        SLA_PROF_STR = 13,

        /// <summary>Alc-Act-Data-Filter (Type 14). String. Active data filter name.</summary>
        ACT_DATA_FILTER = 14,

        /// <summary>Alc-Act-Dest-IP-Filter (Type 15). String. Active destination IP filter name.</summary>
        ACT_DEST_IP_FILTER = 15,

        /// <summary>Alc-Act-Source-IP-Filter (Type 16). String. Active source IP filter name.</summary>
        ACT_SOURCE_IP_FILTER = 16,

        /// <summary>Alc-Acct-I-Inprof-Octets-64 (Type 19). Octets. Ingress in-profile octets (64-bit).</summary>
        ACCT_I_INPROF_OCTETS_64 = 19,

        /// <summary>Alc-Acct-I-Outprof-Octets-64 (Type 20). Octets. Ingress out-of-profile octets (64-bit).</summary>
        ACCT_I_OUTPROF_OCTETS_64 = 20,

        /// <summary>Alc-Acct-O-Inprof-Octets-64 (Type 21). Octets. Egress in-profile octets (64-bit).</summary>
        ACCT_O_INPROF_OCTETS_64 = 21,

        /// <summary>Alc-Acct-O-Outprof-Octets-64 (Type 22). Octets. Egress out-of-profile octets (64-bit).</summary>
        ACCT_O_OUTPROF_OCTETS_64 = 22,

        /// <summary>Alc-Acct-I-Inprof-Pkts-64 (Type 23). Octets. Ingress in-profile packets (64-bit).</summary>
        ACCT_I_INPROF_PKTS_64 = 23,

        /// <summary>Alc-Acct-I-Outprof-Pkts-64 (Type 24). Octets. Ingress out-of-profile packets (64-bit).</summary>
        ACCT_I_OUTPROF_PKTS_64 = 24,

        /// <summary>Alc-Acct-O-Inprof-Pkts-64 (Type 25). Octets. Egress in-profile packets (64-bit).</summary>
        ACCT_O_INPROF_PKTS_64 = 25,

        /// <summary>Alc-Acct-O-Outprof-Pkts-64 (Type 26). Octets. Egress out-of-profile packets (64-bit).</summary>
        ACCT_O_OUTPROF_PKTS_64 = 26,

        /// <summary>Alc-App-Prof-Str (Type 27). String. Application profile string.</summary>
        APP_PROF_STR = 27,

        /// <summary>Alc-Acct-Triggered-Reason (Type 28). Integer. Triggered accounting reason code.</summary>
        ACCT_TRIGGERED_REASON = 28,

        /// <summary>Alc-SLA-String (Type 29). String. SLA string value.</summary>
        SLA_STRING = 29,

        /// <summary>Alc-Acct-I-statmode (Type 30). Integer. Ingress statistics mode.</summary>
        ACCT_I_STATMODE = 30,

        /// <summary>Alc-Acct-O-statmode (Type 31). Integer. Egress statistics mode.</summary>
        ACCT_O_STATMODE = 31,

        /// <summary>Alc-Force-Renew (Type 32). String. Force DHCP renew.</summary>
        FORCE_RENEW = 32,

        /// <summary>Alc-Create-Host (Type 33). String. Create host binding parameter.</summary>
        CREATE_HOST = 33,

        /// <summary>Alc-ANCP-Str (Type 34). String. ANCP (Access Node Control Protocol) string.</summary>
        ANCP_STR = 34,

        /// <summary>Alc-Retail-Serv-Id (Type 35). Integer. Retail service identifier.</summary>
        RETAIL_SERV_ID = 35,

        /// <summary>Alc-Default-Router (Type 36). IP address. Default router (gateway) IP address.</summary>
        DEFAULT_ROUTER = 36,

        /// <summary>Alc-Acct-Interim-IvI-64 (Type 39). Octets. Interim accounting interval (64-bit).</summary>
        ACCT_INTERIM_IVL_64 = 39,

        /// <summary>Alc-Lease-Time (Type 40). Integer. IP address lease time in seconds.</summary>
        LEASE_TIME = 40,

        /// <summary>Alc-DSL-Line-State (Type 41). Integer. DSL line state indicator.</summary>
        DSL_LINE_STATE = 41,

        /// <summary>Alc-Carbon-Install-Policy (Type 42). String. Carbon install policy name.</summary>
        CARBON_INSTALL_POLICY = 42,

        /// <summary>Alc-Tunnel-Group (Type 43). String. Tunnel group name.</summary>
        TUNNEL_GROUP = 43,

        /// <summary>Alc-Tunnel-Algorithm (Type 44). Integer. Tunnel selection algorithm.</summary>
        TUNNEL_ALGORITHM = 44,

        /// <summary>Alc-Tunnel-Max-Sessions (Type 45). Integer. Maximum tunnel sessions.</summary>
        TUNNEL_MAX_SESSIONS = 45,

        /// <summary>Alc-Tunnel-Idle-Timeout (Type 46). Integer. Tunnel idle timeout in seconds.</summary>
        TUNNEL_IDLE_TIMEOUT = 46,

        /// <summary>Alc-Tunnel-Hello-Interval (Type 47). Integer. Tunnel keepalive hello interval in seconds.</summary>
        TUNNEL_HELLO_INTERVAL = 47,

        /// <summary>Alc-Tunnel-Destruct-Timeout (Type 48). Integer. Tunnel destruction timeout in seconds.</summary>
        TUNNEL_DESTRUCT_TIMEOUT = 48,

        /// <summary>Alc-Tunnel-Max-Retry (Type 49). Integer. Maximum tunnel setup retries.</summary>
        TUNNEL_MAX_RETRY = 49,

        /// <summary>Alc-Tunnel-AVP-Hidden (Type 50). Integer. Whether tunnel AVPs are hidden.</summary>
        TUNNEL_AVP_HIDDEN = 50,

        /// <summary>Alc-BGP-Policy (Type 51). String. BGP policy name.</summary>
        BGP_POLICY = 51,

        /// <summary>Alc-BGP-Auth-Keychain (Type 52). String. BGP authentication keychain name.</summary>
        BGP_AUTH_KEYCHAIN = 52,

        /// <summary>Alc-BGP-Auth-Key (Type 53). String. BGP authentication key.</summary>
        BGP_AUTH_KEY = 53,

        /// <summary>Alc-BGP-Export-Policy (Type 54). String. BGP export policy name.</summary>
        BGP_EXPORT_POLICY = 54,

        /// <summary>Alc-BGP-Import-Policy (Type 55). String. BGP import policy name.</summary>
        BGP_IMPORT_POLICY = 55,

        /// <summary>Alc-BGP-PeerAS (Type 56). Integer. BGP peer autonomous system number.</summary>
        BGP_PEERAS = 56,

        /// <summary>Alc-IPsec-Serv-Id (Type 57). Integer. IPsec service identifier.</summary>
        IPSEC_SERV_ID = 57,

        /// <summary>Alc-IPsec-Interface (Type 58). String. IPsec interface name.</summary>
        IPSEC_INTERFACE = 58,

        /// <summary>Alc-IPsec-Tunnel-Template-Id (Type 59). Integer. IPsec tunnel template identifier.</summary>
        IPSEC_TUNNEL_TEMPLATE_ID = 59,

        /// <summary>Alc-IPsec-SA-Lifetime (Type 60). Integer. IPsec SA lifetime in seconds.</summary>
        IPSEC_SA_LIFETIME = 60,

        /// <summary>Alc-IPsec-SA-PFS-Group (Type 61). Integer. IPsec SA PFS Diffie-Hellman group.</summary>
        IPSEC_SA_PFS_GROUP = 61,

        /// <summary>Alc-IPsec-SA-Encr-Algorithm (Type 62). Integer. IPsec SA encryption algorithm.</summary>
        IPSEC_SA_ENCR_ALGORITHM = 62,

        /// <summary>Alc-IPsec-SA-Auth-Algorithm (Type 63). Integer. IPsec SA authentication algorithm.</summary>
        IPSEC_SA_AUTH_ALGORITHM = 63,

        /// <summary>Alc-IPsec-SA-Replay-Window (Type 64). Integer. IPsec SA anti-replay window size.</summary>
        IPSEC_SA_REPLAY_WINDOW = 64,

        /// <summary>Alc-Acct-I-High-Octets-Drop-64 (Type 65). Octets. Ingress high-priority dropped octets (64-bit).</summary>
        ACCT_I_HIGH_OCTETS_DROP_64 = 65,

        /// <summary>Alc-Acct-I-Low-Octets-Drop-64 (Type 66). Octets. Ingress low-priority dropped octets (64-bit).</summary>
        ACCT_I_LOW_OCTETS_DROP_64 = 66,

        /// <summary>Alc-Acct-I-High-Pack-Drop-64 (Type 67). Octets. Ingress high-priority dropped packets (64-bit).</summary>
        ACCT_I_HIGH_PACK_DROP_64 = 67,

        /// <summary>Alc-Acct-I-Low-Pack-Drop-64 (Type 68). Octets. Ingress low-priority dropped packets (64-bit).</summary>
        ACCT_I_LOW_PACK_DROP_64 = 68,

        /// <summary>Alc-Acct-I-High-Octets-Offer-64 (Type 69). Octets. Ingress high-priority offered octets (64-bit).</summary>
        ACCT_I_HIGH_OCTETS_OFFER_64 = 69,

        /// <summary>Alc-Acct-I-Low-Octets-Offer-64 (Type 70). Octets. Ingress low-priority offered octets (64-bit).</summary>
        ACCT_I_LOW_OCTETS_OFFER_64 = 70,

        /// <summary>Alc-Acct-I-High-Pack-Offer-64 (Type 71). Octets. Ingress high-priority offered packets (64-bit).</summary>
        ACCT_I_HIGH_PACK_OFFER_64 = 71,

        /// <summary>Alc-Acct-I-Low-Pack-Offer-64 (Type 72). Octets. Ingress low-priority offered packets (64-bit).</summary>
        ACCT_I_LOW_PACK_OFFER_64 = 72,

        /// <summary>Alc-MSAP-Serv-Id (Type 78). Integer. MSAP service identifier.</summary>
        MSAP_SERV_ID = 78,

        /// <summary>Alc-MSAP-Policy (Type 79). String. MSAP policy name.</summary>
        MSAP_POLICY = 79,

        /// <summary>Alc-MSAP-Interface (Type 80). String. MSAP interface name.</summary>
        MSAP_INTERFACE = 80,

        /// <summary>Alc-PPPoE-Service-Name (Type 81). String. PPPoE service name.</summary>
        PPPOE_SERVICE_NAME = 81,

        /// <summary>Alc-Tunnel-Acct-Policy (Type 83). String. Tunnel accounting policy name.</summary>
        TUNNEL_ACCT_POLICY = 83,

        /// <summary>Alc-Acct-I-Unc-Octets-Offer-64 (Type 84). Octets. Ingress uncoloured offered octets (64-bit).</summary>
        ACCT_I_UNC_OCTETS_OFFER_64 = 84,

        /// <summary>Alc-Acct-I-Unc-Pack-Offer-64 (Type 85). Octets. Ingress uncoloured offered packets (64-bit).</summary>
        ACCT_I_UNC_PACK_OFFER_64 = 85,

        /// <summary>Alc-Acct-I-All-Octets-Offer-64 (Type 86). Octets. Ingress all offered octets (64-bit).</summary>
        ACCT_I_ALL_OCTETS_OFFER_64 = 86,

        /// <summary>Alc-Acct-I-All-Pack-Offer-64 (Type 87). Octets. Ingress all offered packets (64-bit).</summary>
        ACCT_I_ALL_PACK_OFFER_64 = 87,

        /// <summary>Alc-Acct-O-Inprof-Pack-Drop-64 (Type 88). Octets. Egress in-profile dropped packets (64-bit).</summary>
        ACCT_O_INPROF_PACK_DROP_64 = 88,

        /// <summary>Alc-Acct-O-Outprof-Pack-Drop-64 (Type 89). Octets. Egress out-of-profile dropped packets (64-bit).</summary>
        ACCT_O_OUTPROF_PACK_DROP_64 = 89,

        /// <summary>Alc-Acct-O-Inprof-Octs-Drop-64 (Type 90). Octets. Egress in-profile dropped octets (64-bit).</summary>
        ACCT_O_INPROF_OCTS_DROP_64 = 90,

        /// <summary>Alc-Acct-O-Outprof-Octs-Drop-64 (Type 91). Octets. Egress out-of-profile dropped octets (64-bit).</summary>
        ACCT_O_OUTPROF_OCTS_DROP_64 = 91,

        /// <summary>Alc-Sub-Serv-Activate (Type 92). String. Subscriber service activate command.</summary>
        SUB_SERV_ACTIVATE = 92,

        /// <summary>Alc-Sub-Serv-Deactivate (Type 93). String. Subscriber service deactivate command.</summary>
        SUB_SERV_DEACTIVATE = 93,

        /// <summary>Alc-Sub-Serv-Acct (Type 94). String. Subscriber service accounting command.</summary>
        SUB_SERV_ACCT = 94,

        /// <summary>Alc-Sub-Serv-ID (Type 95). Integer. Subscriber service identifier.</summary>
        SUB_SERV_ID = 95,

        /// <summary>Alc-Svc-Id (Type 100). Integer. Service identifier.</summary>
        SVC_ID = 100,

        /// <summary>Alc-Interface (Type 101). String. Interface name.</summary>
        INTERFACE = 101,

        /// <summary>Alc-ToServer-Dhcp-Options (Type 104). Octets. DHCP options sent to server.</summary>
        TOSERVER_DHCP_OPTIONS = 104,

        /// <summary>Alc-ToClient-Dhcp-Options (Type 105). Octets. DHCP options sent to client.</summary>
        TOCLIENT_DHCP_OPTIONS = 105,

        /// <summary>Alc-Tunnel-Serv-Id (Type 106). Integer. Tunnel service identifier.</summary>
        TUNNEL_SERV_ID = 106,

        /// <summary>Alc-IPCP-Extensions (Type 107). Octets. IPCP extension options.</summary>
        IPCP_EXTENSIONS = 107,

        /// <summary>Alc-PPP-Force-IPv6CP (Type 108). Integer. Force IPv6CP negotiation.</summary>
        PPP_FORCE_IPV6CP = 108,

        /// <summary>Alc-Onetime-Http-Redirection-Filter-Id (Type 111). String. One-time HTTP redirect filter ID.</summary>
        ONETIME_HTTP_REDIRECTION_FILTER_ID = 111,

        /// <summary>Alc-Acct-Input-Octets-64 (Type 113). Octets. Accounting input octets (64-bit).</summary>
        ACCT_INPUT_OCTETS_64 = 113,

        /// <summary>Alc-Acct-Output-Octets-64 (Type 114). Octets. Accounting output octets (64-bit).</summary>
        ACCT_OUTPUT_OCTETS_64 = 114,

        /// <summary>Alc-Acct-Input-Packets-64 (Type 115). Octets. Accounting input packets (64-bit).</summary>
        ACCT_INPUT_PACKETS_64 = 115,

        /// <summary>Alc-Acct-Output-Packets-64 (Type 116). Octets. Accounting output packets (64-bit).</summary>
        ACCT_OUTPUT_PACKETS_64 = 116,

        /// <summary>Alc-IPv6-Address (Type 121). String. IPv6 address.</summary>
        IPV6_ADDRESS = 121,

        /// <summary>Alc-Serv-Id (Type 122). Integer. Service identifier (alternate).</summary>
        SERV_ID = 122,

        /// <summary>Alc-Interface-Type (Type 123). Integer. Interface type code.</summary>
        INTERFACE_TYPE = 123,

        /// <summary>Alc-ToServer-Dhcp6-Options (Type 124). Octets. DHCPv6 options sent to server.</summary>
        TOSERVER_DHCP6_OPTIONS = 124,

        /// <summary>Alc-ToClient-Dhcp6-Options (Type 125). Octets. DHCPv6 options sent to client.</summary>
        TOCLIENT_DHCP6_OPTIONS = 125,

        /// <summary>Alc-Ipv6-Primary-Dns (Type 126). String. IPv6 primary DNS server address.</summary>
        IPV6_PRIMARY_DNS = 126,

        /// <summary>Alc-Ipv6-Secondary-Dns (Type 127). String. IPv6 secondary DNS server address.</summary>
        IPV6_SECONDARY_DNS = 127,

        /// <summary>Alc-Delegated-IPv6-Pool (Type 131). String. Delegated IPv6 prefix pool name.</summary>
        DELEGATED_IPV6_POOL = 131,

        /// <summary>Alc-Access-Loop-Rate-Down (Type 132). Integer. Access loop downstream rate in kbps.</summary>
        ACCESS_LOOP_RATE_DOWN = 132,

        /// <summary>Alc-Access-Loop-Encap-Offset (Type 133). Octets. Access loop encapsulation offset.</summary>
        ACCESS_LOOP_ENCAP_OFFSET = 133,

        /// <summary>Alc-NAT-Port-Range (Type 134). String. NAT port range specification.</summary>
        NAT_PORT_RANGE = 134,

        /// <summary>Alc-GTP-APN-Name (Type 135). String. GTP APN name.</summary>
        GTP_APN_NAME = 135,

        /// <summary>Alc-GTP-APN-Resolve (Type 136). Integer. GTP APN resolution method.</summary>
        GTP_APN_RESOLVE = 136,

        /// <summary>Alc-GTP-APN-Ultimate-Id (Type 137). Integer. GTP APN ultimate service identifier.</summary>
        GTP_APN_ULTIMATE_ID = 137,

        /// <summary>Alc-WLAN-APN-Name (Type 138). String. WLAN gateway APN name.</summary>
        WLAN_APN_NAME = 138,

        /// <summary>Alc-MsIsdn (Type 139). String. MSISDN (Mobile Station International Subscriber Directory Number).</summary>
        MSISDN = 139,

        /// <summary>Alc-RSSI (Type 141). Integer. Received Signal Strength Indicator.</summary>
        RSSI = 141,

        /// <summary>Alc-IMEI (Type 142). String. International Mobile Equipment Identity.</summary>
        IMEI = 142,

        /// <summary>Alc-Wifi-SSID (Type 143). String. Wi-Fi SSID.</summary>
        WIFI_SSID = 143,

        /// <summary>Alc-Wifi-AP-MAC-Address (Type 144). String. Wi-Fi access point MAC address.</summary>
        WIFI_AP_MAC_ADDRESS = 144,

        /// <summary>Alc-Access-Loop-Rate-Up (Type 145). Integer. Access loop upstream rate in kbps.</summary>
        ACCESS_LOOP_RATE_UP = 145,

        /// <summary>Alc-Nat-Outside-Serv-Id (Type 146). Integer. NAT outside service identifier.</summary>
        NAT_OUTSIDE_SERV_ID = 146,

        /// <summary>Alc-Nat-Outside-Ip-Addr (Type 147). IP address. NAT outside IPv4 address.</summary>
        NAT_OUTSIDE_IP_ADDR = 147,

        /// <summary>Alc-Host-LECID (Type 148). Integer. Host Logical Ethernet Circuit ID.</summary>
        HOST_LECID = 148,

        /// <summary>Alc-SLAAC-IPv6-Pool (Type 149). String. SLAAC IPv6 pool name.</summary>
        SLAAC_IPV6_POOL = 149,

        /// <summary>Alc-UPnP-Sub-Override-Policy (Type 150). String. UPnP subscriber override policy.</summary>
        UPNP_SUB_OVERRIDE_POLICY = 150,

        /// <summary>Alc-Trigger-Acct-Interim (Type 155). String. Trigger interim accounting string.</summary>
        TRIGGER_ACCT_INTERIM = 155,

        /// <summary>Alc-Access-Loop-Encap1 (Type 156). Octets. Access loop encapsulation data link.</summary>
        ACCESS_LOOP_ENCAP1 = 156,

        /// <summary>Alc-Access-Loop-Encap2 (Type 157). Octets. Access loop encapsulation encap 1.</summary>
        ACCESS_LOOP_ENCAP2 = 157
    }

    /// <summary>
    /// Alc-Acct-Triggered-Reason attribute values (Type 28).
    /// </summary>
    public enum ALC_ACCT_TRIGGERED_REASON
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
    /// Alc-DSL-Line-State attribute values (Type 41).
    /// </summary>
    public enum ALC_DSL_LINE_STATE
    {
        /// <summary>DSL line is not active.</summary>
        NONE = 0,

        /// <summary>DSL line is in showtime (connected).</summary>
        SHOW_TIME = 1,

        /// <summary>DSL line is idle.</summary>
        IDLE = 2,

        /// <summary>DSL line is in a silent state.</summary>
        SILENT = 3
    }

    /// <summary>
    /// Alc-Tunnel-Algorithm attribute values (Type 44).
    /// </summary>
    public enum ALC_TUNNEL_ALGORITHM
    {
        /// <summary>First available tunnel.</summary>
        FIRST_AVAILABLE = 0,

        /// <summary>Load-balanced across tunnels.</summary>
        LOAD_BALANCED = 1,

        /// <summary>Weighted load balancing.</summary>
        WEIGHTED = 2
    }

    /// <summary>
    /// Alc-Tunnel-AVP-Hidden attribute values (Type 50).
    /// </summary>
    public enum ALC_TUNNEL_AVP_HIDDEN
    {
        /// <summary>AVP hiding disabled.</summary>
        NOTHING = 0,

        /// <summary>Sensitive AVPs are hidden.</summary>
        SENSITIVE_ONLY = 1,

        /// <summary>All AVPs are hidden.</summary>
        ALL = 2
    }

    /// <summary>
    /// Alc-IPsec-SA-PFS-Group attribute values (Type 61).
    /// </summary>
    public enum ALC_IPSEC_SA_PFS_GROUP
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
    /// Alc-IPsec-SA-Encr-Algorithm attribute values (Type 62).
    /// </summary>
    public enum ALC_IPSEC_SA_ENCR_ALGORITHM
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
    /// Alc-IPsec-SA-Auth-Algorithm attribute values (Type 63).
    /// </summary>
    public enum ALC_IPSEC_SA_AUTH_ALGORITHM
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

    /// <summary>
    /// Alc-PPP-Force-IPv6CP attribute values (Type 108).
    /// </summary>
    public enum ALC_PPP_FORCE_IPV6CP
    {
        /// <summary>Do not force IPv6CP.</summary>
        DISABLED = 0,

        /// <summary>Force IPv6CP negotiation.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Alc-Interface-Type attribute values (Type 123).
    /// </summary>
    public enum ALC_INTERFACE_TYPE
    {
        /// <summary>Point-to-point interface.</summary>
        POINT_TO_POINT = 1,

        /// <summary>Multipoint interface.</summary>
        MULTIPOINT = 2
    }

    /// <summary>
    /// Alc-GTP-APN-Resolve attribute values (Type 136).
    /// </summary>
    public enum ALC_GTP_APN_RESOLVE
    {
        /// <summary>Resolve APN via DNS.</summary>
        DNS = 1,

        /// <summary>Resolve APN via static configuration.</summary>
        STATIC = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Alcatel-Lucent AAA /
    /// Nokia SR OS (IANA PEN 6527) Vendor-Specific Attributes (VSAs), as defined in the
    /// FreeRADIUS <c>dictionary.alcatel-lucent.aaa</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Alcatel-Lucent AAA's vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 6527</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Nokia (formerly Alcatel-Lucent) Service Routers
    /// (SR OS) for AAA subscriber management, SLA/QoS profile assignment, IPsec
    /// configuration, BGP policy, tunnel management, NAT, GTP/WLAN gateway, DHCPv6,
    /// and extensive 64-bit accounting counters.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AlcatelLucentAaaAttributes.SubscIdStr("subscriber-001"));
    /// packet.SetAttribute(AlcatelLucentAaaAttributes.SlaProfStr("10Mbps-SLA"));
    /// packet.SetAttribute(AlcatelLucentAaaAttributes.DefaultRouter(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(AlcatelLucentAaaAttributes.SvcId(100));
    /// </code>
    /// </remarks>
    public static class AlcatelLucentAaaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Alcatel-Lucent AAA (Nokia).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 6527;

        #region Integer Attributes

        /// <summary>
        /// Creates an Alc-Acct-Triggered-Reason attribute (Type 28) with the specified reason.
        /// </summary>
        /// <param name="value">The triggered accounting reason code. See <see cref="ALC_ACCT_TRIGGERED_REASON"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctTriggeredReason(ALC_ACCT_TRIGGERED_REASON value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.ACCT_TRIGGERED_REASON, (int)value);
        }

        /// <summary>
        /// Creates an Alc-Acct-I-statmode attribute (Type 30) with the specified mode.
        /// </summary>
        /// <param name="value">The ingress statistics mode.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctIStatmode(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.ACCT_I_STATMODE, value);
        }

        /// <summary>
        /// Creates an Alc-Acct-O-statmode attribute (Type 31) with the specified mode.
        /// </summary>
        /// <param name="value">The egress statistics mode.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctOStatmode(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.ACCT_O_STATMODE, value);
        }

        /// <summary>
        /// Creates an Alc-Retail-Serv-Id attribute (Type 35) with the specified service identifier.
        /// </summary>
        /// <param name="value">The retail service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RetailServId(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.RETAIL_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alc-Lease-Time attribute (Type 40) with the specified lease time.
        /// </summary>
        /// <param name="value">The IP address lease time in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LeaseTime(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.LEASE_TIME, value);
        }

        /// <summary>
        /// Creates an Alc-DSL-Line-State attribute (Type 41) with the specified state.
        /// </summary>
        /// <param name="value">The DSL line state. See <see cref="ALC_DSL_LINE_STATE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DslLineState(ALC_DSL_LINE_STATE value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.DSL_LINE_STATE, (int)value);
        }

        /// <summary>
        /// Creates an Alc-Tunnel-Algorithm attribute (Type 44) with the specified algorithm.
        /// </summary>
        /// <param name="value">The tunnel selection algorithm. See <see cref="ALC_TUNNEL_ALGORITHM"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelAlgorithm(ALC_TUNNEL_ALGORITHM value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.TUNNEL_ALGORITHM, (int)value);
        }

        /// <summary>
        /// Creates an Alc-Tunnel-Max-Sessions attribute (Type 45) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum tunnel sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelMaxSessions(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.TUNNEL_MAX_SESSIONS, value);
        }

        /// <summary>
        /// Creates an Alc-Tunnel-Idle-Timeout attribute (Type 46) with the specified timeout.
        /// </summary>
        /// <param name="value">The tunnel idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelIdleTimeout(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.TUNNEL_IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates an Alc-Tunnel-Hello-Interval attribute (Type 47) with the specified interval.
        /// </summary>
        /// <param name="value">The tunnel keepalive hello interval in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelHelloInterval(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.TUNNEL_HELLO_INTERVAL, value);
        }

        /// <summary>
        /// Creates an Alc-Tunnel-Destruct-Timeout attribute (Type 48) with the specified timeout.
        /// </summary>
        /// <param name="value">The tunnel destruction timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelDestructTimeout(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.TUNNEL_DESTRUCT_TIMEOUT, value);
        }

        /// <summary>
        /// Creates an Alc-Tunnel-Max-Retry attribute (Type 49) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum tunnel setup retries.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelMaxRetry(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.TUNNEL_MAX_RETRY, value);
        }

        /// <summary>
        /// Creates an Alc-Tunnel-AVP-Hidden attribute (Type 50) with the specified hiding mode.
        /// </summary>
        /// <param name="value">The AVP hiding mode. See <see cref="ALC_TUNNEL_AVP_HIDDEN"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelAvpHidden(ALC_TUNNEL_AVP_HIDDEN value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.TUNNEL_AVP_HIDDEN, (int)value);
        }

        /// <summary>
        /// Creates an Alc-BGP-PeerAS attribute (Type 56) with the specified AS number.
        /// </summary>
        /// <param name="value">The BGP peer autonomous system number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BgpPeerAs(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.BGP_PEERAS, value);
        }

        /// <summary>
        /// Creates an Alc-IPsec-Serv-Id attribute (Type 57) with the specified identifier.
        /// </summary>
        /// <param name="value">The IPsec service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecServId(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.IPSEC_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alc-IPsec-Tunnel-Template-Id attribute (Type 59) with the specified template ID.
        /// </summary>
        /// <param name="value">The IPsec tunnel template identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecTunnelTemplateId(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.IPSEC_TUNNEL_TEMPLATE_ID, value);
        }

        /// <summary>
        /// Creates an Alc-IPsec-SA-Lifetime attribute (Type 60) with the specified lifetime.
        /// </summary>
        /// <param name="value">The IPsec SA lifetime in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaLifetime(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.IPSEC_SA_LIFETIME, value);
        }

        /// <summary>
        /// Creates an Alc-IPsec-SA-PFS-Group attribute (Type 61) with the specified DH group.
        /// </summary>
        /// <param name="value">The IPsec SA PFS DH group. See <see cref="ALC_IPSEC_SA_PFS_GROUP"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaPfsGroup(ALC_IPSEC_SA_PFS_GROUP value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.IPSEC_SA_PFS_GROUP, (int)value);
        }

        /// <summary>
        /// Creates an Alc-IPsec-SA-Encr-Algorithm attribute (Type 62) with the specified algorithm.
        /// </summary>
        /// <param name="value">The IPsec SA encryption algorithm. See <see cref="ALC_IPSEC_SA_ENCR_ALGORITHM"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaEncrAlgorithm(ALC_IPSEC_SA_ENCR_ALGORITHM value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.IPSEC_SA_ENCR_ALGORITHM, (int)value);
        }

        /// <summary>
        /// Creates an Alc-IPsec-SA-Auth-Algorithm attribute (Type 63) with the specified algorithm.
        /// </summary>
        /// <param name="value">The IPsec SA authentication algorithm. See <see cref="ALC_IPSEC_SA_AUTH_ALGORITHM"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaAuthAlgorithm(ALC_IPSEC_SA_AUTH_ALGORITHM value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.IPSEC_SA_AUTH_ALGORITHM, (int)value);
        }

        /// <summary>
        /// Creates an Alc-IPsec-SA-Replay-Window attribute (Type 64) with the specified window size.
        /// </summary>
        /// <param name="value">The IPsec SA anti-replay window size.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaReplayWindow(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.IPSEC_SA_REPLAY_WINDOW, value);
        }

        /// <summary>
        /// Creates an Alc-MSAP-Serv-Id attribute (Type 78) with the specified identifier.
        /// </summary>
        /// <param name="value">The MSAP service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MsapServId(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.MSAP_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alc-Sub-Serv-ID attribute (Type 95) with the specified identifier.
        /// </summary>
        /// <param name="value">The subscriber service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SubServId(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.SUB_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alc-Svc-Id attribute (Type 100) with the specified service identifier.
        /// </summary>
        /// <param name="value">The service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SvcId(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.SVC_ID, value);
        }

        /// <summary>
        /// Creates an Alc-Tunnel-Serv-Id attribute (Type 106) with the specified identifier.
        /// </summary>
        /// <param name="value">The tunnel service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelServId(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.TUNNEL_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alc-PPP-Force-IPv6CP attribute (Type 108) with the specified value.
        /// </summary>
        /// <param name="value">Whether to force IPv6CP negotiation. See <see cref="ALC_PPP_FORCE_IPV6CP"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PppForceIpv6Cp(ALC_PPP_FORCE_IPV6CP value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.PPP_FORCE_IPV6CP, (int)value);
        }

        /// <summary>
        /// Creates an Alc-Serv-Id attribute (Type 122) with the specified identifier.
        /// </summary>
        /// <param name="value">The service identifier (alternate).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServId(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alc-Interface-Type attribute (Type 123) with the specified type.
        /// </summary>
        /// <param name="value">The interface type code. See <see cref="ALC_INTERFACE_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes InterfaceType(ALC_INTERFACE_TYPE value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.INTERFACE_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Alc-Access-Loop-Rate-Down attribute (Type 132) with the specified rate.
        /// </summary>
        /// <param name="value">The access loop downstream rate in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLoopRateDown(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.ACCESS_LOOP_RATE_DOWN, value);
        }

        /// <summary>
        /// Creates an Alc-GTP-APN-Resolve attribute (Type 136) with the specified resolution method.
        /// </summary>
        /// <param name="value">The GTP APN resolution method. See <see cref="ALC_GTP_APN_RESOLVE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes GtpApnResolve(ALC_GTP_APN_RESOLVE value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.GTP_APN_RESOLVE, (int)value);
        }

        /// <summary>
        /// Creates an Alc-GTP-APN-Ultimate-Id attribute (Type 137) with the specified identifier.
        /// </summary>
        /// <param name="value">The GTP APN ultimate service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes GtpApnUltimateId(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.GTP_APN_ULTIMATE_ID, value);
        }

        /// <summary>
        /// Creates an Alc-RSSI attribute (Type 141) with the specified signal strength.
        /// </summary>
        /// <param name="value">The Received Signal Strength Indicator.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Rssi(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.RSSI, value);
        }

        /// <summary>
        /// Creates an Alc-Access-Loop-Rate-Up attribute (Type 145) with the specified rate.
        /// </summary>
        /// <param name="value">The access loop upstream rate in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLoopRateUp(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.ACCESS_LOOP_RATE_UP, value);
        }

        /// <summary>
        /// Creates an Alc-Nat-Outside-Serv-Id attribute (Type 146) with the specified identifier.
        /// </summary>
        /// <param name="value">The NAT outside service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NatOutsideServId(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.NAT_OUTSIDE_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alc-Host-LECID attribute (Type 148) with the specified circuit ID.
        /// </summary>
        /// <param name="value">The Host Logical Ethernet Circuit ID.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes HostLecid(int value)
        {
            return CreateInteger(AlcatelLucentAaaAttributeType.HOST_LECID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Alc-Client-Hardware-Addr attribute (Type 1) with the specified MAC address.
        /// </summary>
        /// <param name="value">The client hardware (MAC) address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientHardwareAddr(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.CLIENT_HARDWARE_ADDR, value);
        }

        /// <summary>
        /// Creates an Alc-Subsc-ID-Str attribute (Type 11) with the specified subscriber identifier.
        /// </summary>
        /// <param name="value">The subscriber identifier string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscIdStr(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.SUBSC_ID_STR, value);
        }

        /// <summary>
        /// Creates an Alc-Subsc-Prof-Str attribute (Type 12) with the specified profile.
        /// </summary>
        /// <param name="value">The subscriber profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscProfStr(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.SUBSC_PROF_STR, value);
        }

        /// <summary>
        /// Creates an Alc-SLA-Prof-Str attribute (Type 13) with the specified SLA profile.
        /// </summary>
        /// <param name="value">The SLA profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SlaProfStr(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.SLA_PROF_STR, value);
        }

        /// <summary>
        /// Creates an Alc-Act-Data-Filter attribute (Type 14) with the specified filter name.
        /// </summary>
        /// <param name="value">The active data filter name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ActDataFilter(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.ACT_DATA_FILTER, value);
        }

        /// <summary>
        /// Creates an Alc-Act-Dest-IP-Filter attribute (Type 15) with the specified filter name.
        /// </summary>
        /// <param name="value">The active destination IP filter name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ActDestIpFilter(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.ACT_DEST_IP_FILTER, value);
        }

        /// <summary>
        /// Creates an Alc-Act-Source-IP-Filter attribute (Type 16) with the specified filter name.
        /// </summary>
        /// <param name="value">The active source IP filter name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ActSourceIpFilter(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.ACT_SOURCE_IP_FILTER, value);
        }

        /// <summary>
        /// Creates an Alc-App-Prof-Str attribute (Type 27) with the specified application profile.
        /// </summary>
        /// <param name="value">The application profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AppProfStr(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.APP_PROF_STR, value);
        }

        /// <summary>
        /// Creates an Alc-SLA-String attribute (Type 29) with the specified SLA string.
        /// </summary>
        /// <param name="value">The SLA string value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SlaString(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.SLA_STRING, value);
        }

        /// <summary>
        /// Creates an Alc-Force-Renew attribute (Type 32) with the specified parameter.
        /// </summary>
        /// <param name="value">The force DHCP renew parameter. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ForceRenew(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.FORCE_RENEW, value);
        }

        /// <summary>
        /// Creates an Alc-Create-Host attribute (Type 33) with the specified parameter.
        /// </summary>
        /// <param name="value">The create host binding parameter. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CreateHost(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.CREATE_HOST, value);
        }

        /// <summary>
        /// Creates an Alc-ANCP-Str attribute (Type 34) with the specified ANCP string.
        /// </summary>
        /// <param name="value">The ANCP string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AncpStr(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.ANCP_STR, value);
        }

        /// <summary>
        /// Creates an Alc-Carbon-Install-Policy attribute (Type 42) with the specified policy name.
        /// </summary>
        /// <param name="value">The carbon install policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CarbonInstallPolicy(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.CARBON_INSTALL_POLICY, value);
        }

        /// <summary>
        /// Creates an Alc-Tunnel-Group attribute (Type 43) with the specified group name.
        /// </summary>
        /// <param name="value">The tunnel group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelGroup(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.TUNNEL_GROUP, value);
        }

        /// <summary>
        /// Creates an Alc-BGP-Policy attribute (Type 51) with the specified policy name.
        /// </summary>
        /// <param name="value">The BGP policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpPolicy(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.BGP_POLICY, value);
        }

        /// <summary>
        /// Creates an Alc-BGP-Auth-Keychain attribute (Type 52) with the specified keychain name.
        /// </summary>
        /// <param name="value">The BGP authentication keychain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpAuthKeychain(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.BGP_AUTH_KEYCHAIN, value);
        }

        /// <summary>
        /// Creates an Alc-BGP-Auth-Key attribute (Type 53) with the specified key.
        /// </summary>
        /// <param name="value">The BGP authentication key. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpAuthKey(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.BGP_AUTH_KEY, value);
        }

        /// <summary>
        /// Creates an Alc-BGP-Export-Policy attribute (Type 54) with the specified policy name.
        /// </summary>
        /// <param name="value">The BGP export policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpExportPolicy(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.BGP_EXPORT_POLICY, value);
        }

        /// <summary>
        /// Creates an Alc-BGP-Import-Policy attribute (Type 55) with the specified policy name.
        /// </summary>
        /// <param name="value">The BGP import policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpImportPolicy(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.BGP_IMPORT_POLICY, value);
        }

        /// <summary>
        /// Creates an Alc-IPsec-Interface attribute (Type 58) with the specified interface name.
        /// </summary>
        /// <param name="value">The IPsec interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecInterface(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.IPSEC_INTERFACE, value);
        }

        /// <summary>
        /// Creates an Alc-MSAP-Policy attribute (Type 79) with the specified policy name.
        /// </summary>
        /// <param name="value">The MSAP policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsapPolicy(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.MSAP_POLICY, value);
        }

        /// <summary>
        /// Creates an Alc-MSAP-Interface attribute (Type 80) with the specified interface name.
        /// </summary>
        /// <param name="value">The MSAP interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsapInterface(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.MSAP_INTERFACE, value);
        }

        /// <summary>
        /// Creates an Alc-PPPoE-Service-Name attribute (Type 81) with the specified service name.
        /// </summary>
        /// <param name="value">The PPPoE service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PppoeServiceName(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.PPPOE_SERVICE_NAME, value);
        }

        /// <summary>
        /// Creates an Alc-Tunnel-Acct-Policy attribute (Type 83) with the specified policy name.
        /// </summary>
        /// <param name="value">The tunnel accounting policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelAcctPolicy(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.TUNNEL_ACCT_POLICY, value);
        }

        /// <summary>
        /// Creates an Alc-Sub-Serv-Activate attribute (Type 92) with the specified command.
        /// </summary>
        /// <param name="value">The subscriber service activate command. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubServActivate(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.SUB_SERV_ACTIVATE, value);
        }

        /// <summary>
        /// Creates an Alc-Sub-Serv-Deactivate attribute (Type 93) with the specified command.
        /// </summary>
        /// <param name="value">The subscriber service deactivate command. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubServDeactivate(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.SUB_SERV_DEACTIVATE, value);
        }

        /// <summary>
        /// Creates an Alc-Sub-Serv-Acct attribute (Type 94) with the specified command.
        /// </summary>
        /// <param name="value">The subscriber service accounting command. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubServAcct(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.SUB_SERV_ACCT, value);
        }

        /// <summary>
        /// Creates an Alc-Interface attribute (Type 101) with the specified interface name.
        /// </summary>
        /// <param name="value">The interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Interface(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.INTERFACE, value);
        }

        /// <summary>
        /// Creates an Alc-Onetime-Http-Redirection-Filter-Id attribute (Type 111) with the specified filter ID.
        /// </summary>
        /// <param name="value">The one-time HTTP redirect filter ID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OnetimeHttpRedirectionFilterId(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.ONETIME_HTTP_REDIRECTION_FILTER_ID, value);
        }

        /// <summary>
        /// Creates an Alc-IPv6-Address attribute (Type 121) with the specified IPv6 address string.
        /// </summary>
        /// <param name="value">The IPv6 address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ipv6Address(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.IPV6_ADDRESS, value);
        }

        /// <summary>
        /// Creates an Alc-Ipv6-Primary-Dns attribute (Type 126) with the specified DNS address.
        /// </summary>
        /// <param name="value">The IPv6 primary DNS server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ipv6PrimaryDns(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.IPV6_PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates an Alc-Ipv6-Secondary-Dns attribute (Type 127) with the specified DNS address.
        /// </summary>
        /// <param name="value">The IPv6 secondary DNS server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ipv6SecondaryDns(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.IPV6_SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates an Alc-Delegated-IPv6-Pool attribute (Type 131) with the specified pool name.
        /// </summary>
        /// <param name="value">The delegated IPv6 prefix pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DelegatedIpv6Pool(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.DELEGATED_IPV6_POOL, value);
        }

        /// <summary>
        /// Creates an Alc-NAT-Port-Range attribute (Type 134) with the specified range specification.
        /// </summary>
        /// <param name="value">The NAT port range specification. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatPortRange(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.NAT_PORT_RANGE, value);
        }

        /// <summary>
        /// Creates an Alc-GTP-APN-Name attribute (Type 135) with the specified APN name.
        /// </summary>
        /// <param name="value">The GTP APN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GtpApnName(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.GTP_APN_NAME, value);
        }

        /// <summary>
        /// Creates an Alc-WLAN-APN-Name attribute (Type 138) with the specified APN name.
        /// </summary>
        /// <param name="value">The WLAN gateway APN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WlanApnName(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.WLAN_APN_NAME, value);
        }

        /// <summary>
        /// Creates an Alc-MsIsdn attribute (Type 139) with the specified MSISDN.
        /// </summary>
        /// <param name="value">The MSISDN. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsIsdn(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.MSISDN, value);
        }

        /// <summary>
        /// Creates an Alc-IMEI attribute (Type 142) with the specified IMEI.
        /// </summary>
        /// <param name="value">The International Mobile Equipment Identity. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imei(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.IMEI, value);
        }

        /// <summary>
        /// Creates an Alc-Wifi-SSID attribute (Type 143) with the specified SSID.
        /// </summary>
        /// <param name="value">The Wi-Fi SSID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WifiSsid(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.WIFI_SSID, value);
        }

        /// <summary>
        /// Creates an Alc-Wifi-AP-MAC-Address attribute (Type 144) with the specified MAC address.
        /// </summary>
        /// <param name="value">The Wi-Fi access point MAC address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WifiApMacAddress(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.WIFI_AP_MAC_ADDRESS, value);
        }

        /// <summary>
        /// Creates an Alc-SLAAC-IPv6-Pool attribute (Type 149) with the specified pool name.
        /// </summary>
        /// <param name="value">The SLAAC IPv6 pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SlaacIpv6Pool(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.SLAAC_IPV6_POOL, value);
        }

        /// <summary>
        /// Creates an Alc-UPnP-Sub-Override-Policy attribute (Type 150) with the specified policy.
        /// </summary>
        /// <param name="value">The UPnP subscriber override policy. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UpnpSubOverridePolicy(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.UPNP_SUB_OVERRIDE_POLICY, value);
        }

        /// <summary>
        /// Creates an Alc-Trigger-Acct-Interim attribute (Type 155) with the specified string.
        /// </summary>
        /// <param name="value">The trigger interim accounting string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TriggerAcctInterim(string value)
        {
            return CreateString(AlcatelLucentAaaAttributeType.TRIGGER_ACCT_INTERIM, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Alc-Default-Router attribute (Type 36) with the specified IPv4 address.
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
            return CreateIpv4(AlcatelLucentAaaAttributeType.DEFAULT_ROUTER, value);
        }

        /// <summary>
        /// Creates an Alc-Nat-Outside-Ip-Addr attribute (Type 147) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The NAT outside IPv4 address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes NatOutsideIpAddr(IPAddress value)
        {
            return CreateIpv4(AlcatelLucentAaaAttributeType.NAT_OUTSIDE_IP_ADDR, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates an Alc-Acct-I-Inprof-Octets-64 attribute (Type 19) with the specified raw value.
        /// </summary>
        /// <param name="value">The ingress in-profile octets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctIInprofOctets64(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCT_I_INPROF_OCTETS_64, value);
        }

        /// <summary>
        /// Creates an Alc-Acct-I-Outprof-Octets-64 attribute (Type 20) with the specified raw value.
        /// </summary>
        /// <param name="value">The ingress out-of-profile octets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctIOutprofOctets64(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCT_I_OUTPROF_OCTETS_64, value);
        }

        /// <summary>
        /// Creates an Alc-Acct-O-Inprof-Octets-64 attribute (Type 21) with the specified raw value.
        /// </summary>
        /// <param name="value">The egress in-profile octets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctOInprofOctets64(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCT_O_INPROF_OCTETS_64, value);
        }

        /// <summary>
        /// Creates an Alc-Acct-O-Outprof-Octets-64 attribute (Type 22) with the specified raw value.
        /// </summary>
        /// <param name="value">The egress out-of-profile octets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctOOutprofOctets64(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCT_O_OUTPROF_OCTETS_64, value);
        }

        /// <summary>
        /// Creates an Alc-Acct-I-Inprof-Pkts-64 attribute (Type 23) with the specified raw value.
        /// </summary>
        /// <param name="value">The ingress in-profile packets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctIInprofPkts64(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCT_I_INPROF_PKTS_64, value);
        }

        /// <summary>
        /// Creates an Alc-Acct-I-Outprof-Pkts-64 attribute (Type 24) with the specified raw value.
        /// </summary>
        /// <param name="value">The ingress out-of-profile packets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctIOutprofPkts64(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCT_I_OUTPROF_PKTS_64, value);
        }

        /// <summary>
        /// Creates an Alc-Acct-O-Inprof-Pkts-64 attribute (Type 25) with the specified raw value.
        /// </summary>
        /// <param name="value">The egress in-profile packets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctOInprofPkts64(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCT_O_INPROF_PKTS_64, value);
        }

        /// <summary>
        /// Creates an Alc-Acct-O-Outprof-Pkts-64 attribute (Type 26) with the specified raw value.
        /// </summary>
        /// <param name="value">The egress out-of-profile packets (64-bit counter). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctOOutprofPkts64(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCT_O_OUTPROF_PKTS_64, value);
        }

        /// <summary>
        /// Creates an Alc-Acct-Interim-IvI-64 attribute (Type 39) with the specified raw value.
        /// </summary>
        /// <param name="value">The interim accounting interval (64-bit). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctInterimIvl64(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCT_INTERIM_IVL_64, value);
        }

        /// <summary>
        /// Creates an Alc-ToServer-Dhcp-Options attribute (Type 104) with the specified raw value.
        /// </summary>
        /// <param name="value">The DHCP options sent to server. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ToServerDhcpOptions(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.TOSERVER_DHCP_OPTIONS, value);
        }

        /// <summary>
        /// Creates an Alc-ToClient-Dhcp-Options attribute (Type 105) with the specified raw value.
        /// </summary>
        /// <param name="value">The DHCP options sent to client. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ToClientDhcpOptions(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.TOCLIENT_DHCP_OPTIONS, value);
        }

        /// <summary>
        /// Creates an Alc-IPCP-Extensions attribute (Type 107) with the specified raw value.
        /// </summary>
        /// <param name="value">The IPCP extension options. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpcpExtensions(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.IPCP_EXTENSIONS, value);
        }

        /// <summary>
        /// Creates an Alc-ToServer-Dhcp6-Options attribute (Type 124) with the specified raw value.
        /// </summary>
        /// <param name="value">The DHCPv6 options sent to server. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ToServerDhcp6Options(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.TOSERVER_DHCP6_OPTIONS, value);
        }

        /// <summary>
        /// Creates an Alc-ToClient-Dhcp6-Options attribute (Type 125) with the specified raw value.
        /// </summary>
        /// <param name="value">The DHCPv6 options sent to client. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ToClientDhcp6Options(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.TOCLIENT_DHCP6_OPTIONS, value);
        }

        /// <summary>
        /// Creates an Alc-Access-Loop-Encap-Offset attribute (Type 133) with the specified raw value.
        /// </summary>
        /// <param name="value">The access loop encapsulation offset. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessLoopEncapOffset(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCESS_LOOP_ENCAP_OFFSET, value);
        }

        /// <summary>
        /// Creates an Alc-Access-Loop-Encap1 attribute (Type 156) with the specified raw value.
        /// </summary>
        /// <param name="value">The access loop encapsulation data link. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessLoopEncap1(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCESS_LOOP_ENCAP1, value);
        }

        /// <summary>
        /// Creates an Alc-Access-Loop-Encap2 attribute (Type 157) with the specified raw value.
        /// </summary>
        /// <param name="value">The access loop encapsulation encap 1. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessLoopEncap2(byte[] value)
        {
            return CreateOctets(AlcatelLucentAaaAttributeType.ACCESS_LOOP_ENCAP2, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Alcatel-Lucent AAA attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AlcatelLucentAaaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Alcatel-Lucent AAA attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AlcatelLucentAaaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a raw byte array value for the specified Alcatel-Lucent AAA attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateOctets(AlcatelLucentAaaAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified Alcatel-Lucent AAA attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(AlcatelLucentAaaAttributeType type, IPAddress value)
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