using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Alcatel-Lucent SR / Nokia SR OS (IANA PEN 6527) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.alcatel.sr</c>.
    /// </summary>
    /// <remarks>
    /// These attribute types are defined under the same vendor ID (6527) as the
    /// <c>dictionary.alcatel-lucent.aaa</c>, but represent a separate set of attributes
    /// specific to the Alcatel-Lucent Service Router platform.
    /// </remarks>
    public enum AlcatelSrAttributeType : byte
    {
        /// <summary>Alcatel-SR-Access-Level-Data-Filter (Type 1). String. Data filter applied to the access level.</summary>
        ACCESS_LEVEL_DATA_FILTER = 1,

        /// <summary>Alcatel-SR-Home-Directory (Type 2). String. Home directory path for the user.</summary>
        HOME_DIRECTORY = 2,

        /// <summary>Alcatel-SR-Profile-String (Type 3). String. CLI profile string.</summary>
        PROFILE_STRING = 3,

        /// <summary>Alcatel-SR-PPP-Address (Type 4). IP address. PPP peer IP address.</summary>
        PPP_ADDRESS = 4,

        /// <summary>Alcatel-SR-PPP-Netmask (Type 5). IP address. PPP peer subnet mask.</summary>
        PPP_NETMASK = 5,

        /// <summary>Alcatel-SR-Primary-DNS (Type 9). IP address. Primary DNS server address.</summary>
        PRIMARY_DNS = 9,

        /// <summary>Alcatel-SR-Secondary-DNS (Type 10). IP address. Secondary DNS server address.</summary>
        SECONDARY_DNS = 10,

        /// <summary>Alcatel-SR-Primary-NBNS (Type 11). IP address. Primary NBNS (WINS) server address.</summary>
        PRIMARY_NBNS = 11,

        /// <summary>Alcatel-SR-Secondary-NBNS (Type 12). IP address. Secondary NBNS (WINS) server address.</summary>
        SECONDARY_NBNS = 12,

        /// <summary>Alcatel-SR-Sub-ID-String (Type 13). String. Subscriber identifier string.</summary>
        SUB_ID_STRING = 13,

        /// <summary>Alcatel-SR-Sub-Prof-String (Type 14). String. Subscriber profile string.</summary>
        SUB_PROF_STRING = 14,

        /// <summary>Alcatel-SR-SLA-Prof-String (Type 15). String. SLA profile string.</summary>
        SLA_PROF_STRING = 15,

        /// <summary>Alcatel-SR-Force-Renew (Type 16). String. Force DHCP renew parameter.</summary>
        FORCE_RENEW = 16,

        /// <summary>Alcatel-SR-Create-Host (Type 17). String. Create host binding parameter.</summary>
        CREATE_HOST = 17,

        /// <summary>Alcatel-SR-ANCP-String (Type 18). String. ANCP (Access Node Control Protocol) string.</summary>
        ANCP_STRING = 18,

        /// <summary>Alcatel-SR-Retail-Svc-Id (Type 19). Integer. Retail service identifier.</summary>
        RETAIL_SVC_ID = 19,

        /// <summary>Alcatel-SR-Default-Router (Type 20). IP address. Default router (gateway) IP address.</summary>
        DEFAULT_ROUTER = 20,

        /// <summary>Alcatel-SR-Client-Hardware-Addr (Type 27). String. Client hardware (MAC) address.</summary>
        CLIENT_HARDWARE_ADDR = 27,

        /// <summary>Alcatel-SR-Acct-Triggered-Reason (Type 28). Integer. Triggered accounting reason code.</summary>
        ACCT_TRIGGERED_REASON = 28,

        /// <summary>Alcatel-SR-Svc-Id (Type 29). Integer. Service identifier.</summary>
        SVC_ID = 29,

        /// <summary>Alcatel-SR-SAP-Id (Type 30). String. Service Access Point identifier.</summary>
        SAP_ID = 30,

        /// <summary>Alcatel-SR-Resource-Pool-Id (Type 31). Integer. Resource pool identifier.</summary>
        RESOURCE_POOL_ID = 31,

        /// <summary>Alcatel-SR-Multicast-Package (Type 32). String. Multicast package name.</summary>
        MULTICAST_PACKAGE = 32,

        /// <summary>Alcatel-SR-Lease-Time (Type 33). Integer. IP address lease time in seconds.</summary>
        LEASE_TIME = 33,

        /// <summary>Alcatel-SR-DSL-Line-State (Type 34). Integer. DSL line state indicator.</summary>
        DSL_LINE_STATE = 34,

        /// <summary>Alcatel-SR-Carbon-Install-Policy (Type 35). String. Carbon install policy name.</summary>
        CARBON_INSTALL_POLICY = 35,

        /// <summary>Alcatel-SR-Tunnel-Group (Type 36). String. Tunnel group name.</summary>
        TUNNEL_GROUP = 36,

        /// <summary>Alcatel-SR-Tunnel-Algorithm (Type 37). Integer. Tunnel selection algorithm.</summary>
        TUNNEL_ALGORITHM = 37,

        /// <summary>Alcatel-SR-Tunnel-Max-Sessions (Type 38). Integer. Maximum tunnel sessions.</summary>
        TUNNEL_MAX_SESSIONS = 38,

        /// <summary>Alcatel-SR-Tunnel-Idle-Timeout (Type 39). Integer. Tunnel idle timeout in seconds.</summary>
        TUNNEL_IDLE_TIMEOUT = 39,

        /// <summary>Alcatel-SR-Tunnel-Hello-Interval (Type 40). Integer. Tunnel keepalive hello interval in seconds.</summary>
        TUNNEL_HELLO_INTERVAL = 40,

        /// <summary>Alcatel-SR-Tunnel-Destruct-Timeout (Type 41). Integer. Tunnel destruction timeout in seconds.</summary>
        TUNNEL_DESTRUCT_TIMEOUT = 41,

        /// <summary>Alcatel-SR-Tunnel-Max-Retry (Type 42). Integer. Maximum tunnel setup retries.</summary>
        TUNNEL_MAX_RETRY = 42,

        /// <summary>Alcatel-SR-Tunnel-AVP-Hidden (Type 43). Integer. Whether tunnel AVPs are hidden.</summary>
        TUNNEL_AVP_HIDDEN = 43,

        /// <summary>Alcatel-SR-BGP-Policy (Type 44). String. BGP policy name.</summary>
        BGP_POLICY = 44,

        /// <summary>Alcatel-SR-BGP-Auth-Keychain (Type 45). String. BGP authentication keychain name.</summary>
        BGP_AUTH_KEYCHAIN = 45,

        /// <summary>Alcatel-SR-BGP-Auth-Key (Type 46). String. BGP authentication key.</summary>
        BGP_AUTH_KEY = 46,

        /// <summary>Alcatel-SR-BGP-Export-Policy (Type 47). String. BGP export policy name.</summary>
        BGP_EXPORT_POLICY = 47,

        /// <summary>Alcatel-SR-BGP-Import-Policy (Type 48). String. BGP import policy name.</summary>
        BGP_IMPORT_POLICY = 48,

        /// <summary>Alcatel-SR-BGP-PeerAS (Type 49). Integer. BGP peer autonomous system number.</summary>
        BGP_PEERAS = 49,

        /// <summary>Alcatel-SR-IPsec-Serv-Id (Type 50). Integer. IPsec service identifier.</summary>
        IPSEC_SERV_ID = 50,

        /// <summary>Alcatel-SR-IPsec-Interface (Type 51). String. IPsec interface name.</summary>
        IPSEC_INTERFACE = 51,

        /// <summary>Alcatel-SR-IPsec-Tunnel-Template-Id (Type 52). Integer. IPsec tunnel template identifier.</summary>
        IPSEC_TUNNEL_TEMPLATE_ID = 52,

        /// <summary>Alcatel-SR-IPsec-SA-Lifetime (Type 53). Integer. IPsec SA lifetime in seconds.</summary>
        IPSEC_SA_LIFETIME = 53,

        /// <summary>Alcatel-SR-IPsec-SA-PFS-Group (Type 54). Integer. IPsec SA PFS Diffie-Hellman group.</summary>
        IPSEC_SA_PFS_GROUP = 54,

        /// <summary>Alcatel-SR-IPsec-SA-Encr-Algorithm (Type 55). Integer. IPsec SA encryption algorithm.</summary>
        IPSEC_SA_ENCR_ALGORITHM = 55,

        /// <summary>Alcatel-SR-IPsec-SA-Auth-Algorithm (Type 56). Integer. IPsec SA authentication algorithm.</summary>
        IPSEC_SA_AUTH_ALGORITHM = 56,

        /// <summary>Alcatel-SR-IPsec-SA-Replay-Window (Type 57). Integer. IPsec SA anti-replay window size.</summary>
        IPSEC_SA_REPLAY_WINDOW = 57,

        /// <summary>Alcatel-SR-MSAP-Serv-Id (Type 70). Integer. MSAP service identifier.</summary>
        MSAP_SERV_ID = 70,

        /// <summary>Alcatel-SR-MSAP-Policy (Type 71). String. MSAP policy name.</summary>
        MSAP_POLICY = 71,

        /// <summary>Alcatel-SR-MSAP-Interface (Type 72). String. MSAP interface name.</summary>
        MSAP_INTERFACE = 72,

        /// <summary>Alcatel-SR-PPPoE-Service-Name (Type 73). String. PPPoE service name.</summary>
        PPPOE_SERVICE_NAME = 73,

        /// <summary>Alcatel-SR-Tunnel-Acct-Policy (Type 74). String. Tunnel accounting policy name.</summary>
        TUNNEL_ACCT_POLICY = 74,

        /// <summary>Alcatel-SR-Sub-Serv-Activate (Type 80). String. Subscriber service activate command.</summary>
        SUB_SERV_ACTIVATE = 80,

        /// <summary>Alcatel-SR-Sub-Serv-Deactivate (Type 81). String. Subscriber service deactivate command.</summary>
        SUB_SERV_DEACTIVATE = 81,

        /// <summary>Alcatel-SR-Sub-Serv-Acct (Type 82). String. Subscriber service accounting command.</summary>
        SUB_SERV_ACCT = 82,

        /// <summary>Alcatel-SR-Sub-Serv-ID (Type 83). Integer. Subscriber service identifier.</summary>
        SUB_SERV_ID = 83,

        /// <summary>Alcatel-SR-Interface (Type 90). String. Interface name.</summary>
        INTERFACE = 90,

        /// <summary>Alcatel-SR-ToServer-Dhcp-Options (Type 91). Octets. DHCP options sent to server.</summary>
        TOSERVER_DHCP_OPTIONS = 91,

        /// <summary>Alcatel-SR-ToClient-Dhcp-Options (Type 92). Octets. DHCP options sent to client.</summary>
        TOCLIENT_DHCP_OPTIONS = 92,

        /// <summary>Alcatel-SR-Tunnel-Serv-Id (Type 93). Integer. Tunnel service identifier.</summary>
        TUNNEL_SERV_ID = 93,

        /// <summary>Alcatel-SR-IPCP-Extensions (Type 94). Octets. IPCP extension options.</summary>
        IPCP_EXTENSIONS = 94,

        /// <summary>Alcatel-SR-PPP-Force-IPv6CP (Type 95). Integer. Force IPv6CP negotiation.</summary>
        PPP_FORCE_IPV6CP = 95,

        /// <summary>Alcatel-SR-Onetime-Http-Redirection-Filter-Id (Type 96). String. One-time HTTP redirect filter ID.</summary>
        ONETIME_HTTP_REDIRECTION_FILTER_ID = 96,

        /// <summary>Alcatel-SR-IPv6-Address (Type 99). String. IPv6 address.</summary>
        IPV6_ADDRESS = 99,

        /// <summary>Alcatel-SR-Serv-Id (Type 100). Integer. Service identifier (alternate).</summary>
        SERV_ID = 100,

        /// <summary>Alcatel-SR-Interface-Type (Type 101). Integer. Interface type code.</summary>
        INTERFACE_TYPE = 101,

        /// <summary>Alcatel-SR-ToServer-Dhcp6-Options (Type 102). Octets. DHCPv6 options sent to server.</summary>
        TOSERVER_DHCP6_OPTIONS = 102,

        /// <summary>Alcatel-SR-ToClient-Dhcp6-Options (Type 103). Octets. DHCPv6 options sent to client.</summary>
        TOCLIENT_DHCP6_OPTIONS = 103,

        /// <summary>Alcatel-SR-Ipv6-Primary-Dns (Type 105). String. IPv6 primary DNS server address.</summary>
        IPV6_PRIMARY_DNS = 105,

        /// <summary>Alcatel-SR-Ipv6-Secondary-Dns (Type 106). String. IPv6 secondary DNS server address.</summary>
        IPV6_SECONDARY_DNS = 106,

        /// <summary>Alcatel-SR-Delegated-IPv6-Pool (Type 131). String. Delegated IPv6 prefix pool name.</summary>
        DELEGATED_IPV6_POOL = 131,

        /// <summary>Alcatel-SR-Access-Loop-Rate-Down (Type 132). Integer. Access loop downstream rate in kbps.</summary>
        ACCESS_LOOP_RATE_DOWN = 132,

        /// <summary>Alcatel-SR-Access-Loop-Encap-Offset (Type 133). Octets. Access loop encapsulation offset.</summary>
        ACCESS_LOOP_ENCAP_OFFSET = 133,

        /// <summary>Alcatel-SR-Access-Loop-Rate-Up (Type 134). Integer. Access loop upstream rate in kbps.</summary>
        ACCESS_LOOP_RATE_UP = 134,

        /// <summary>Alcatel-SR-Nat-Outside-Serv-Id (Type 135). Integer. NAT outside service identifier.</summary>
        NAT_OUTSIDE_SERV_ID = 135,

        /// <summary>Alcatel-SR-Nat-Outside-Ip-Addr (Type 136). IP address. NAT outside IPv4 address.</summary>
        NAT_OUTSIDE_IP_ADDR = 136,

        /// <summary>Alcatel-SR-Host-LECID (Type 137). Integer. Host Logical Ethernet Circuit ID.</summary>
        HOST_LECID = 137,

        /// <summary>Alcatel-SR-SLAAC-IPv6-Pool (Type 138). String. SLAAC IPv6 pool name.</summary>
        SLAAC_IPV6_POOL = 138,

        /// <summary>Alcatel-SR-UPnP-Sub-Override-Policy (Type 139). String. UPnP subscriber override policy.</summary>
        UPNP_SUB_OVERRIDE_POLICY = 139,

        /// <summary>Alcatel-SR-App-Prof-String (Type 140). String. Application profile string.</summary>
        APP_PROF_STRING = 140,

        /// <summary>Alcatel-SR-Trigger-Acct-Interim (Type 149). String. Trigger interim accounting string.</summary>
        TRIGGER_ACCT_INTERIM = 149,

        /// <summary>Alcatel-SR-Access-Loop-Encap1 (Type 150). Octets. Access loop encapsulation data link.</summary>
        ACCESS_LOOP_ENCAP1 = 150,

        /// <summary>Alcatel-SR-Access-Loop-Encap2 (Type 151). Octets. Access loop encapsulation encap 1.</summary>
        ACCESS_LOOP_ENCAP2 = 151,

        /// <summary>Alcatel-SR-NAT-Port-Range (Type 160). String. NAT port range specification.</summary>
        NAT_PORT_RANGE = 160
    }

    /// <summary>
    /// Alcatel-SR-Acct-Triggered-Reason attribute values (Type 28).
    /// </summary>
    public enum ALCATEL_SR_ACCT_TRIGGERED_REASON
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
    /// Alcatel-SR-DSL-Line-State attribute values (Type 34).
    /// </summary>
    public enum ALCATEL_SR_DSL_LINE_STATE
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
    /// Alcatel-SR-Tunnel-Algorithm attribute values (Type 37).
    /// </summary>
    public enum ALCATEL_SR_TUNNEL_ALGORITHM
    {
        /// <summary>First available tunnel.</summary>
        FIRST_AVAILABLE = 0,

        /// <summary>Load-balanced across tunnels.</summary>
        LOAD_BALANCED = 1,

        /// <summary>Weighted load balancing.</summary>
        WEIGHTED = 2
    }

    /// <summary>
    /// Alcatel-SR-Tunnel-AVP-Hidden attribute values (Type 43).
    /// </summary>
    public enum ALCATEL_SR_TUNNEL_AVP_HIDDEN
    {
        /// <summary>AVP hiding disabled.</summary>
        NOTHING = 0,

        /// <summary>Sensitive AVPs are hidden.</summary>
        SENSITIVE_ONLY = 1,

        /// <summary>All AVPs are hidden.</summary>
        ALL = 2
    }

    /// <summary>
    /// Alcatel-SR-IPsec-SA-PFS-Group attribute values (Type 54).
    /// </summary>
    public enum ALCATEL_SR_IPSEC_SA_PFS_GROUP
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
    /// Alcatel-SR-IPsec-SA-Encr-Algorithm attribute values (Type 55).
    /// </summary>
    public enum ALCATEL_SR_IPSEC_SA_ENCR_ALGORITHM
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
    /// Alcatel-SR-IPsec-SA-Auth-Algorithm attribute values (Type 56).
    /// </summary>
    public enum ALCATEL_SR_IPSEC_SA_AUTH_ALGORITHM
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
    /// Alcatel-SR-PPP-Force-IPv6CP attribute values (Type 95).
    /// </summary>
    public enum ALCATEL_SR_PPP_FORCE_IPV6CP
    {
        /// <summary>Do not force IPv6CP.</summary>
        DISABLED = 0,

        /// <summary>Force IPv6CP negotiation.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Alcatel-SR-Interface-Type attribute values (Type 101).
    /// </summary>
    public enum ALCATEL_SR_INTERFACE_TYPE
    {
        /// <summary>Point-to-point interface.</summary>
        POINT_TO_POINT = 1,

        /// <summary>Multipoint interface.</summary>
        MULTIPOINT = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Alcatel-Lucent SR /
    /// Nokia SR OS (IANA PEN 6527) Vendor-Specific Attributes (VSAs), as defined in the
    /// FreeRADIUS <c>dictionary.alcatel.sr</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Alcatel-Lucent SR's vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 6527</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Nokia (formerly Alcatel-Lucent) Service Routers
    /// (SR OS) for CLI access profiles, subscriber management, SLA/QoS profiles,
    /// IPsec configuration, BGP policy, tunnel management, NAT, DHCPv4/v6, IPv6
    /// addressing, and access loop configuration.
    /// </para>
    /// <para>
    /// <b>Note:</b> This class shares the same Vendor ID (6527) with
    /// <see cref="AlcatelLucentAaaAttributes"/> but defines attributes from the
    /// separate <c>dictionary.alcatel.sr</c> dictionary file.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AlcatelSrAttributes.ProfileString("admin"));
    /// packet.SetAttribute(AlcatelSrAttributes.SlaProfString("10Mbps-SLA"));
    /// packet.SetAttribute(AlcatelSrAttributes.DefaultRouter(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(AlcatelSrAttributes.SvcId(100));
    /// </code>
    /// </remarks>
    public static class AlcatelSrAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Alcatel-Lucent SR (Nokia).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 6527;

        #region Integer Attributes

        /// <summary>
        /// Creates an Alcatel-SR-Retail-Svc-Id attribute (Type 19) with the specified service identifier.
        /// </summary>
        /// <param name="value">The retail service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RetailSvcId(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.RETAIL_SVC_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Acct-Triggered-Reason attribute (Type 28) with the specified reason.
        /// </summary>
        /// <param name="value">The triggered accounting reason code. See <see cref="ALCATEL_SR_ACCT_TRIGGERED_REASON"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctTriggeredReason(ALCATEL_SR_ACCT_TRIGGERED_REASON value)
        {
            return CreateInteger(AlcatelSrAttributeType.ACCT_TRIGGERED_REASON, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Svc-Id attribute (Type 29) with the specified service identifier.
        /// </summary>
        /// <param name="value">The service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SvcId(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.SVC_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Resource-Pool-Id attribute (Type 31) with the specified pool identifier.
        /// </summary>
        /// <param name="value">The resource pool identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ResourcePoolId(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.RESOURCE_POOL_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Lease-Time attribute (Type 33) with the specified lease time.
        /// </summary>
        /// <param name="value">The IP address lease time in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LeaseTime(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.LEASE_TIME, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-DSL-Line-State attribute (Type 34) with the specified state.
        /// </summary>
        /// <param name="value">The DSL line state. See <see cref="ALCATEL_SR_DSL_LINE_STATE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DslLineState(ALCATEL_SR_DSL_LINE_STATE value)
        {
            return CreateInteger(AlcatelSrAttributeType.DSL_LINE_STATE, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Tunnel-Algorithm attribute (Type 37) with the specified algorithm.
        /// </summary>
        /// <param name="value">The tunnel selection algorithm. See <see cref="ALCATEL_SR_TUNNEL_ALGORITHM"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelAlgorithm(ALCATEL_SR_TUNNEL_ALGORITHM value)
        {
            return CreateInteger(AlcatelSrAttributeType.TUNNEL_ALGORITHM, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Tunnel-Max-Sessions attribute (Type 38) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum tunnel sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelMaxSessions(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.TUNNEL_MAX_SESSIONS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Tunnel-Idle-Timeout attribute (Type 39) with the specified timeout.
        /// </summary>
        /// <param name="value">The tunnel idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelIdleTimeout(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.TUNNEL_IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Tunnel-Hello-Interval attribute (Type 40) with the specified interval.
        /// </summary>
        /// <param name="value">The tunnel keepalive hello interval in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelHelloInterval(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.TUNNEL_HELLO_INTERVAL, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Tunnel-Destruct-Timeout attribute (Type 41) with the specified timeout.
        /// </summary>
        /// <param name="value">The tunnel destruction timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelDestructTimeout(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.TUNNEL_DESTRUCT_TIMEOUT, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Tunnel-Max-Retry attribute (Type 42) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum tunnel setup retries.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelMaxRetry(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.TUNNEL_MAX_RETRY, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Tunnel-AVP-Hidden attribute (Type 43) with the specified hiding mode.
        /// </summary>
        /// <param name="value">The AVP hiding mode. See <see cref="ALCATEL_SR_TUNNEL_AVP_HIDDEN"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelAvpHidden(ALCATEL_SR_TUNNEL_AVP_HIDDEN value)
        {
            return CreateInteger(AlcatelSrAttributeType.TUNNEL_AVP_HIDDEN, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-BGP-PeerAS attribute (Type 49) with the specified AS number.
        /// </summary>
        /// <param name="value">The BGP peer autonomous system number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BgpPeerAs(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.BGP_PEERAS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-IPsec-Serv-Id attribute (Type 50) with the specified identifier.
        /// </summary>
        /// <param name="value">The IPsec service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecServId(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.IPSEC_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-IPsec-Tunnel-Template-Id attribute (Type 52) with the specified template ID.
        /// </summary>
        /// <param name="value">The IPsec tunnel template identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecTunnelTemplateId(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.IPSEC_TUNNEL_TEMPLATE_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-IPsec-SA-Lifetime attribute (Type 53) with the specified lifetime.
        /// </summary>
        /// <param name="value">The IPsec SA lifetime in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaLifetime(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.IPSEC_SA_LIFETIME, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-IPsec-SA-PFS-Group attribute (Type 54) with the specified DH group.
        /// </summary>
        /// <param name="value">The IPsec SA PFS DH group. See <see cref="ALCATEL_SR_IPSEC_SA_PFS_GROUP"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaPfsGroup(ALCATEL_SR_IPSEC_SA_PFS_GROUP value)
        {
            return CreateInteger(AlcatelSrAttributeType.IPSEC_SA_PFS_GROUP, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-IPsec-SA-Encr-Algorithm attribute (Type 55) with the specified algorithm.
        /// </summary>
        /// <param name="value">The IPsec SA encryption algorithm. See <see cref="ALCATEL_SR_IPSEC_SA_ENCR_ALGORITHM"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaEncrAlgorithm(ALCATEL_SR_IPSEC_SA_ENCR_ALGORITHM value)
        {
            return CreateInteger(AlcatelSrAttributeType.IPSEC_SA_ENCR_ALGORITHM, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-IPsec-SA-Auth-Algorithm attribute (Type 56) with the specified algorithm.
        /// </summary>
        /// <param name="value">The IPsec SA authentication algorithm. See <see cref="ALCATEL_SR_IPSEC_SA_AUTH_ALGORITHM"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaAuthAlgorithm(ALCATEL_SR_IPSEC_SA_AUTH_ALGORITHM value)
        {
            return CreateInteger(AlcatelSrAttributeType.IPSEC_SA_AUTH_ALGORITHM, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-IPsec-SA-Replay-Window attribute (Type 57) with the specified window size.
        /// </summary>
        /// <param name="value">The IPsec SA anti-replay window size.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSaReplayWindow(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.IPSEC_SA_REPLAY_WINDOW, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-MSAP-Serv-Id attribute (Type 70) with the specified identifier.
        /// </summary>
        /// <param name="value">The MSAP service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MsapServId(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.MSAP_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Sub-Serv-ID attribute (Type 83) with the specified identifier.
        /// </summary>
        /// <param name="value">The subscriber service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SubServId(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.SUB_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Tunnel-Serv-Id attribute (Type 93) with the specified identifier.
        /// </summary>
        /// <param name="value">The tunnel service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelServId(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.TUNNEL_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-PPP-Force-IPv6CP attribute (Type 95) with the specified value.
        /// </summary>
        /// <param name="value">Whether to force IPv6CP negotiation. See <see cref="ALCATEL_SR_PPP_FORCE_IPV6CP"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PppForceIpv6Cp(ALCATEL_SR_PPP_FORCE_IPV6CP value)
        {
            return CreateInteger(AlcatelSrAttributeType.PPP_FORCE_IPV6CP, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Serv-Id attribute (Type 100) with the specified identifier.
        /// </summary>
        /// <param name="value">The service identifier (alternate).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServId(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Interface-Type attribute (Type 101) with the specified type.
        /// </summary>
        /// <param name="value">The interface type code. See <see cref="ALCATEL_SR_INTERFACE_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes InterfaceType(ALCATEL_SR_INTERFACE_TYPE value)
        {
            return CreateInteger(AlcatelSrAttributeType.INTERFACE_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Access-Loop-Rate-Down attribute (Type 132) with the specified rate.
        /// </summary>
        /// <param name="value">The access loop downstream rate in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLoopRateDown(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.ACCESS_LOOP_RATE_DOWN, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Access-Loop-Rate-Up attribute (Type 134) with the specified rate.
        /// </summary>
        /// <param name="value">The access loop upstream rate in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLoopRateUp(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.ACCESS_LOOP_RATE_UP, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Nat-Outside-Serv-Id attribute (Type 135) with the specified identifier.
        /// </summary>
        /// <param name="value">The NAT outside service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NatOutsideServId(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.NAT_OUTSIDE_SERV_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Host-LECID attribute (Type 137) with the specified circuit ID.
        /// </summary>
        /// <param name="value">The Host Logical Ethernet Circuit ID.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes HostLecid(int value)
        {
            return CreateInteger(AlcatelSrAttributeType.HOST_LECID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Alcatel-SR-Access-Level-Data-Filter attribute (Type 1) with the specified filter.
        /// </summary>
        /// <param name="value">The data filter applied to the access level. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessLevelDataFilter(string value)
        {
            return CreateString(AlcatelSrAttributeType.ACCESS_LEVEL_DATA_FILTER, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Home-Directory attribute (Type 2) with the specified path.
        /// </summary>
        /// <param name="value">The home directory path. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HomeDirectory(string value)
        {
            return CreateString(AlcatelSrAttributeType.HOME_DIRECTORY, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Profile-String attribute (Type 3) with the specified profile.
        /// </summary>
        /// <param name="value">The CLI profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProfileString(string value)
        {
            return CreateString(AlcatelSrAttributeType.PROFILE_STRING, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Sub-ID-String attribute (Type 13) with the specified identifier.
        /// </summary>
        /// <param name="value">The subscriber identifier string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubIdString(string value)
        {
            return CreateString(AlcatelSrAttributeType.SUB_ID_STRING, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Sub-Prof-String attribute (Type 14) with the specified profile.
        /// </summary>
        /// <param name="value">The subscriber profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubProfString(string value)
        {
            return CreateString(AlcatelSrAttributeType.SUB_PROF_STRING, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-SLA-Prof-String attribute (Type 15) with the specified SLA profile.
        /// </summary>
        /// <param name="value">The SLA profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SlaProfString(string value)
        {
            return CreateString(AlcatelSrAttributeType.SLA_PROF_STRING, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Force-Renew attribute (Type 16) with the specified parameter.
        /// </summary>
        /// <param name="value">The force DHCP renew parameter. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ForceRenew(string value)
        {
            return CreateString(AlcatelSrAttributeType.FORCE_RENEW, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Create-Host attribute (Type 17) with the specified parameter.
        /// </summary>
        /// <param name="value">The create host binding parameter. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CreateHost(string value)
        {
            return CreateString(AlcatelSrAttributeType.CREATE_HOST, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-ANCP-String attribute (Type 18) with the specified ANCP string.
        /// </summary>
        /// <param name="value">The ANCP string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AncpString(string value)
        {
            return CreateString(AlcatelSrAttributeType.ANCP_STRING, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Client-Hardware-Addr attribute (Type 27) with the specified MAC address.
        /// </summary>
        /// <param name="value">The client hardware (MAC) address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientHardwareAddr(string value)
        {
            return CreateString(AlcatelSrAttributeType.CLIENT_HARDWARE_ADDR, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-SAP-Id attribute (Type 30) with the specified SAP identifier.
        /// </summary>
        /// <param name="value">The Service Access Point identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SapId(string value)
        {
            return CreateString(AlcatelSrAttributeType.SAP_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Multicast-Package attribute (Type 32) with the specified package name.
        /// </summary>
        /// <param name="value">The multicast package name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MulticastPackage(string value)
        {
            return CreateString(AlcatelSrAttributeType.MULTICAST_PACKAGE, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Carbon-Install-Policy attribute (Type 35) with the specified policy name.
        /// </summary>
        /// <param name="value">The carbon install policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CarbonInstallPolicy(string value)
        {
            return CreateString(AlcatelSrAttributeType.CARBON_INSTALL_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Tunnel-Group attribute (Type 36) with the specified group name.
        /// </summary>
        /// <param name="value">The tunnel group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelGroup(string value)
        {
            return CreateString(AlcatelSrAttributeType.TUNNEL_GROUP, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-BGP-Policy attribute (Type 44) with the specified policy name.
        /// </summary>
        /// <param name="value">The BGP policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpPolicy(string value)
        {
            return CreateString(AlcatelSrAttributeType.BGP_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-BGP-Auth-Keychain attribute (Type 45) with the specified keychain name.
        /// </summary>
        /// <param name="value">The BGP authentication keychain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpAuthKeychain(string value)
        {
            return CreateString(AlcatelSrAttributeType.BGP_AUTH_KEYCHAIN, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-BGP-Auth-Key attribute (Type 46) with the specified key.
        /// </summary>
        /// <param name="value">The BGP authentication key. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpAuthKey(string value)
        {
            return CreateString(AlcatelSrAttributeType.BGP_AUTH_KEY, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-BGP-Export-Policy attribute (Type 47) with the specified policy name.
        /// </summary>
        /// <param name="value">The BGP export policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpExportPolicy(string value)
        {
            return CreateString(AlcatelSrAttributeType.BGP_EXPORT_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-BGP-Import-Policy attribute (Type 48) with the specified policy name.
        /// </summary>
        /// <param name="value">The BGP import policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BgpImportPolicy(string value)
        {
            return CreateString(AlcatelSrAttributeType.BGP_IMPORT_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-IPsec-Interface attribute (Type 51) with the specified interface name.
        /// </summary>
        /// <param name="value">The IPsec interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecInterface(string value)
        {
            return CreateString(AlcatelSrAttributeType.IPSEC_INTERFACE, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-MSAP-Policy attribute (Type 71) with the specified policy name.
        /// </summary>
        /// <param name="value">The MSAP policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsapPolicy(string value)
        {
            return CreateString(AlcatelSrAttributeType.MSAP_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-MSAP-Interface attribute (Type 72) with the specified interface name.
        /// </summary>
        /// <param name="value">The MSAP interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MsapInterface(string value)
        {
            return CreateString(AlcatelSrAttributeType.MSAP_INTERFACE, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-PPPoE-Service-Name attribute (Type 73) with the specified service name.
        /// </summary>
        /// <param name="value">The PPPoE service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PppoeServiceName(string value)
        {
            return CreateString(AlcatelSrAttributeType.PPPOE_SERVICE_NAME, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Tunnel-Acct-Policy attribute (Type 74) with the specified policy name.
        /// </summary>
        /// <param name="value">The tunnel accounting policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelAcctPolicy(string value)
        {
            return CreateString(AlcatelSrAttributeType.TUNNEL_ACCT_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Sub-Serv-Activate attribute (Type 80) with the specified command.
        /// </summary>
        /// <param name="value">The subscriber service activate command. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubServActivate(string value)
        {
            return CreateString(AlcatelSrAttributeType.SUB_SERV_ACTIVATE, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Sub-Serv-Deactivate attribute (Type 81) with the specified command.
        /// </summary>
        /// <param name="value">The subscriber service deactivate command. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubServDeactivate(string value)
        {
            return CreateString(AlcatelSrAttributeType.SUB_SERV_DEACTIVATE, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Sub-Serv-Acct attribute (Type 82) with the specified command.
        /// </summary>
        /// <param name="value">The subscriber service accounting command. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubServAcct(string value)
        {
            return CreateString(AlcatelSrAttributeType.SUB_SERV_ACCT, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Interface attribute (Type 90) with the specified interface name.
        /// </summary>
        /// <param name="value">The interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Interface(string value)
        {
            return CreateString(AlcatelSrAttributeType.INTERFACE, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Onetime-Http-Redirection-Filter-Id attribute (Type 96) with the specified filter ID.
        /// </summary>
        /// <param name="value">The one-time HTTP redirect filter ID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OnetimeHttpRedirectionFilterId(string value)
        {
            return CreateString(AlcatelSrAttributeType.ONETIME_HTTP_REDIRECTION_FILTER_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-IPv6-Address attribute (Type 99) with the specified IPv6 address string.
        /// </summary>
        /// <param name="value">The IPv6 address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ipv6Address(string value)
        {
            return CreateString(AlcatelSrAttributeType.IPV6_ADDRESS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Ipv6-Primary-Dns attribute (Type 105) with the specified DNS address.
        /// </summary>
        /// <param name="value">The IPv6 primary DNS server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ipv6PrimaryDns(string value)
        {
            return CreateString(AlcatelSrAttributeType.IPV6_PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Ipv6-Secondary-Dns attribute (Type 106) with the specified DNS address.
        /// </summary>
        /// <param name="value">The IPv6 secondary DNS server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ipv6SecondaryDns(string value)
        {
            return CreateString(AlcatelSrAttributeType.IPV6_SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Delegated-IPv6-Pool attribute (Type 131) with the specified pool name.
        /// </summary>
        /// <param name="value">The delegated IPv6 prefix pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DelegatedIpv6Pool(string value)
        {
            return CreateString(AlcatelSrAttributeType.DELEGATED_IPV6_POOL, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-SLAAC-IPv6-Pool attribute (Type 138) with the specified pool name.
        /// </summary>
        /// <param name="value">The SLAAC IPv6 pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SlaacIpv6Pool(string value)
        {
            return CreateString(AlcatelSrAttributeType.SLAAC_IPV6_POOL, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-UPnP-Sub-Override-Policy attribute (Type 139) with the specified policy.
        /// </summary>
        /// <param name="value">The UPnP subscriber override policy. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UpnpSubOverridePolicy(string value)
        {
            return CreateString(AlcatelSrAttributeType.UPNP_SUB_OVERRIDE_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-App-Prof-String attribute (Type 140) with the specified application profile.
        /// </summary>
        /// <param name="value">The application profile string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AppProfString(string value)
        {
            return CreateString(AlcatelSrAttributeType.APP_PROF_STRING, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Trigger-Acct-Interim attribute (Type 149) with the specified string.
        /// </summary>
        /// <param name="value">The trigger interim accounting string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TriggerAcctInterim(string value)
        {
            return CreateString(AlcatelSrAttributeType.TRIGGER_ACCT_INTERIM, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-NAT-Port-Range attribute (Type 160) with the specified range specification.
        /// </summary>
        /// <param name="value">The NAT port range specification. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatPortRange(string value)
        {
            return CreateString(AlcatelSrAttributeType.NAT_PORT_RANGE, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Alcatel-SR-PPP-Address attribute (Type 4) with the specified IPv4 address.
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
            return CreateIpv4(AlcatelSrAttributeType.PPP_ADDRESS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-PPP-Netmask attribute (Type 5) with the specified subnet mask.
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
            return CreateIpv4(AlcatelSrAttributeType.PPP_NETMASK, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Primary-DNS attribute (Type 9) with the specified IPv4 address.
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
            return CreateIpv4(AlcatelSrAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Secondary-DNS attribute (Type 10) with the specified IPv4 address.
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
            return CreateIpv4(AlcatelSrAttributeType.SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Primary-NBNS attribute (Type 11) with the specified IPv4 address.
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
            return CreateIpv4(AlcatelSrAttributeType.PRIMARY_NBNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Secondary-NBNS attribute (Type 12) with the specified IPv4 address.
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
            return CreateIpv4(AlcatelSrAttributeType.SECONDARY_NBNS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Default-Router attribute (Type 20) with the specified IPv4 address.
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
            return CreateIpv4(AlcatelSrAttributeType.DEFAULT_ROUTER, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Nat-Outside-Ip-Addr attribute (Type 136) with the specified IPv4 address.
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
            return CreateIpv4(AlcatelSrAttributeType.NAT_OUTSIDE_IP_ADDR, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates an Alcatel-SR-ToServer-Dhcp-Options attribute (Type 91) with the specified raw value.
        /// </summary>
        /// <param name="value">The DHCP options sent to server. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ToServerDhcpOptions(byte[] value)
        {
            return CreateOctets(AlcatelSrAttributeType.TOSERVER_DHCP_OPTIONS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-ToClient-Dhcp-Options attribute (Type 92) with the specified raw value.
        /// </summary>
        /// <param name="value">The DHCP options sent to client. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ToClientDhcpOptions(byte[] value)
        {
            return CreateOctets(AlcatelSrAttributeType.TOCLIENT_DHCP_OPTIONS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-IPCP-Extensions attribute (Type 94) with the specified raw value.
        /// </summary>
        /// <param name="value">The IPCP extension options. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpcpExtensions(byte[] value)
        {
            return CreateOctets(AlcatelSrAttributeType.IPCP_EXTENSIONS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-ToServer-Dhcp6-Options attribute (Type 102) with the specified raw value.
        /// </summary>
        /// <param name="value">The DHCPv6 options sent to server. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ToServerDhcp6Options(byte[] value)
        {
            return CreateOctets(AlcatelSrAttributeType.TOSERVER_DHCP6_OPTIONS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-ToClient-Dhcp6-Options attribute (Type 103) with the specified raw value.
        /// </summary>
        /// <param name="value">The DHCPv6 options sent to client. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ToClientDhcp6Options(byte[] value)
        {
            return CreateOctets(AlcatelSrAttributeType.TOCLIENT_DHCP6_OPTIONS, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Access-Loop-Encap-Offset attribute (Type 133) with the specified raw value.
        /// </summary>
        /// <param name="value">The access loop encapsulation offset. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessLoopEncapOffset(byte[] value)
        {
            return CreateOctets(AlcatelSrAttributeType.ACCESS_LOOP_ENCAP_OFFSET, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Access-Loop-Encap1 attribute (Type 150) with the specified raw value.
        /// </summary>
        /// <param name="value">The access loop encapsulation data link. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessLoopEncap1(byte[] value)
        {
            return CreateOctets(AlcatelSrAttributeType.ACCESS_LOOP_ENCAP1, value);
        }

        /// <summary>
        /// Creates an Alcatel-SR-Access-Loop-Encap2 attribute (Type 151) with the specified raw value.
        /// </summary>
        /// <param name="value">The access loop encapsulation encap 1. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessLoopEncap2(byte[] value)
        {
            return CreateOctets(AlcatelSrAttributeType.ACCESS_LOOP_ENCAP2, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Alcatel SR attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AlcatelSrAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Alcatel SR attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AlcatelSrAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a raw byte array value for the specified Alcatel SR attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateOctets(AlcatelSrAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified Alcatel SR attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(AlcatelSrAttributeType type, IPAddress value)
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