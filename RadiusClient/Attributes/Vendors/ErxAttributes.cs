using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Juniper ERX / Unisphere (IANA PEN 4874) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.erx</c>.
    /// </summary>
    /// <remarks>
    /// Originally registered as Unisphere Networks, now Juniper Networks. These
    /// attributes are used by Juniper ERX/E-series edge routers and BNG platforms.
    /// </remarks>
    public enum ErxAttributeType : byte
    {
        /// <summary>ERX-Virtual-Router-Name (Type 1). String. Virtual router name.</summary>
        VIRTUAL_ROUTER_NAME = 1,

        /// <summary>ERX-Address-Pool-Name (Type 2). String. Address pool name.</summary>
        ADDRESS_POOL_NAME = 2,

        /// <summary>ERX-Local-Loopback-Interface (Type 3). String. Local loopback interface name.</summary>
        LOCAL_LOOPBACK_INTERFACE = 3,

        /// <summary>ERX-Primary-Dns (Type 4). IP address. Primary DNS server.</summary>
        PRIMARY_DNS = 4,

        /// <summary>ERX-Secondary-Dns (Type 5). IP address. Secondary DNS server.</summary>
        SECONDARY_DNS = 5,

        /// <summary>ERX-Primary-Wins (Type 6). IP address. Primary WINS server.</summary>
        PRIMARY_WINS = 6,

        /// <summary>ERX-Secondary-Wins (Type 7). IP address. Secondary WINS server.</summary>
        SECONDARY_WINS = 7,

        /// <summary>ERX-Tunnel-Virtual-Router (Type 8). String. Tunnel virtual router name.</summary>
        TUNNEL_VIRTUAL_ROUTER = 8,

        /// <summary>ERX-Tunnel-Password (Type 9). String. Tunnel password.</summary>
        TUNNEL_PASSWORD = 9,

        /// <summary>ERX-Ingress-Policy-Name (Type 10). String. Ingress policy name.</summary>
        INGRESS_POLICY_NAME = 10,

        /// <summary>ERX-Egress-Policy-Name (Type 11). String. Egress policy name.</summary>
        EGRESS_POLICY_NAME = 11,

        /// <summary>ERX-Ingress-Statistics (Type 12). Integer. Ingress statistics flag.</summary>
        INGRESS_STATISTICS = 12,

        /// <summary>ERX-Egress-Statistics (Type 13). Integer. Egress statistics flag.</summary>
        EGRESS_STATISTICS = 13,

        /// <summary>ERX-Service-Category (Type 14). Integer. ATM service category.</summary>
        SERVICE_CATEGORY = 14,

        /// <summary>ERX-PCR (Type 15). Integer. ATM Peak Cell Rate.</summary>
        PCR = 15,

        /// <summary>ERX-SCR (Type 16). Integer. ATM Sustainable Cell Rate.</summary>
        SCR = 16,

        /// <summary>ERX-MBS (Type 17). Integer. ATM Maximum Burst Size.</summary>
        MBS = 17,

        /// <summary>ERX-Init-CLI-Access-Level (Type 18). String. Initial CLI access level.</summary>
        INIT_CLI_ACCESS_LEVEL = 18,

        /// <summary>ERX-Allow-All-VR-Access (Type 19). Integer. Allow access to all virtual routers.</summary>
        ALLOW_ALL_VR_ACCESS = 19,

        /// <summary>ERX-Alt-CLI-Access-Level (Type 20). String. Alternate CLI access level.</summary>
        ALT_CLI_ACCESS_LEVEL = 20,

        /// <summary>ERX-Alt-CLI-VRouter-Name (Type 21). String. Alternate CLI virtual router name.</summary>
        ALT_CLI_VROUTER_NAME = 21,

        /// <summary>ERX-SA-Validate (Type 22). Integer. Source address validation flag.</summary>
        SA_VALIDATE = 22,

        /// <summary>ERX-Igmp-Enable (Type 23). Integer. IGMP enable flag.</summary>
        IGMP_ENABLE = 23,

        /// <summary>ERX-DHCP-Guidelines-Comply (Type 24). Integer. DHCP guidelines comply flag.</summary>
        DHCP_GUIDELINES_COMPLY = 24,

        /// <summary>ERX-PPP-Auth-Protocol (Type 25). Integer. PPP authentication protocol.</summary>
        PPP_AUTH_PROTOCOL = 25,

        /// <summary>ERX-Tunnel-Min-Bps (Type 26). Integer. Tunnel minimum bps.</summary>
        TUNNEL_MIN_BPS = 26,

        /// <summary>ERX-Tunnel-Max-Bps (Type 27). Integer. Tunnel maximum bps.</summary>
        TUNNEL_MAX_BPS = 27,

        /// <summary>ERX-Tunnel-Bearer-Type (Type 28). Integer. Tunnel bearer type.</summary>
        TUNNEL_BEARER_TYPE = 28,

        /// <summary>ERX-Input-Gigapkts (Type 29). Integer. Input gigapackets counter.</summary>
        INPUT_GIGAPKTS = 29,

        /// <summary>ERX-Output-Gigapkts (Type 30). Integer. Output gigapackets counter.</summary>
        OUTPUT_GIGAPKTS = 30,

        /// <summary>ERX-Tunnel-Interface-Id (Type 31). String. Tunnel interface identifier.</summary>
        TUNNEL_INTERFACE_ID = 31,

        /// <summary>ERX-IpV6-Virtual-Router (Type 32). String. IPv6 virtual router name.</summary>
        IPV6_VIRTUAL_ROUTER = 32,

        /// <summary>ERX-IpV6-Local-Interface (Type 33). String. IPv6 local interface name.</summary>
        IPV6_LOCAL_INTERFACE = 33,

        /// <summary>ERX-Ipv6-Primary-Dns (Type 34). String. IPv6 primary DNS server address.</summary>
        IPV6_PRIMARY_DNS = 34,

        /// <summary>ERX-Ipv6-Secondary-Dns (Type 35). String. IPv6 secondary DNS server address.</summary>
        IPV6_SECONDARY_DNS = 35,

        /// <summary>ERX-Disconnect-Cause (Type 36). Octets. Disconnect cause information.</summary>
        DISCONNECT_CAUSE = 36,

        /// <summary>ERX-Radius-Client-Address (Type 37). IP address. RADIUS client address.</summary>
        RADIUS_CLIENT_ADDRESS = 37,

        /// <summary>ERX-Service-Description (Type 38). String. Service description string.</summary>
        SERVICE_DESCRIPTION = 38,

        /// <summary>ERX-DHCP-Options (Type 39). Octets. DHCP options data.</summary>
        DHCP_OPTIONS = 39,

        /// <summary>ERX-DHCP-MAC-Address (Type 40). String. DHCP client MAC address.</summary>
        DHCP_MAC_ADDRESS = 40,

        /// <summary>ERX-DHCP-GI-Address (Type 41). IP address. DHCP gateway interface address.</summary>
        DHCP_GI_ADDRESS = 41,

        /// <summary>ERX-LI-Action (Type 42). Integer. Lawful intercept action.</summary>
        LI_ACTION = 42,

        /// <summary>ERX-Med-Dev-Handle (Type 43). Integer. Mediation device handle.</summary>
        MED_DEV_HANDLE = 43,

        /// <summary>ERX-Med-Ip-Address (Type 44). IP address. Mediation IP address.</summary>
        MED_IP_ADDRESS = 44,

        /// <summary>ERX-Med-Port-Number (Type 45). Integer. Mediation port number.</summary>
        MED_PORT_NUMBER = 45,

        /// <summary>ERX-MLPPP-Bundle-Name (Type 46). String. MLPPP bundle name.</summary>
        MLPPP_BUNDLE_NAME = 46,

        /// <summary>ERX-Interface-Desc (Type 47). String. Interface description.</summary>
        INTERFACE_DESC = 47,

        /// <summary>ERX-Tunnel-Group (Type 48). String. Tunnel group name.</summary>
        TUNNEL_GROUP = 48,

        /// <summary>ERX-Service-Activate (Type 49). String. Service activation string.</summary>
        SERVICE_ACTIVATE = 49,

        /// <summary>ERX-Service-Deactivate (Type 50). String. Service deactivation string.</summary>
        SERVICE_DEACTIVATE = 50,

        /// <summary>ERX-Service-Volume (Type 51). Integer. Service volume limit.</summary>
        SERVICE_VOLUME = 51,

        /// <summary>ERX-Service-Timeout (Type 52). Integer. Service timeout in seconds.</summary>
        SERVICE_TIMEOUT = 52,

        /// <summary>ERX-Service-Statistics (Type 53). Integer. Service statistics type.</summary>
        SERVICE_STATISTICS = 53,

        /// <summary>ERX-DF-Bit (Type 54). Integer. DF (Don't Fragment) bit setting.</summary>
        DF_BIT = 54,

        /// <summary>ERX-IGMP-Access-Name (Type 55). String. IGMP access list name.</summary>
        IGMP_ACCESS_NAME = 55,

        /// <summary>ERX-IGMP-Access-Src-Name (Type 56). String. IGMP source access list name.</summary>
        IGMP_ACCESS_SRC_NAME = 56,

        /// <summary>ERX-IGMP-OIF-Map-Name (Type 57). String. IGMP outgoing interface map name.</summary>
        IGMP_OIF_MAP_NAME = 57,

        /// <summary>ERX-MLD-Access-Name (Type 58). String. MLD access list name.</summary>
        MLD_ACCESS_NAME = 58,

        /// <summary>ERX-MLD-Access-Src-Name (Type 59). String. MLD source access list name.</summary>
        MLD_ACCESS_SRC_NAME = 59,

        /// <summary>ERX-MLD-OIF-Map-Name (Type 60). String. MLD outgoing interface map name.</summary>
        MLD_OIF_MAP_NAME = 60,

        /// <summary>ERX-MLD-Version (Type 61). Integer. MLD version.</summary>
        MLD_VERSION = 61,

        /// <summary>ERX-IGMP-Version (Type 62). Integer. IGMP version.</summary>
        IGMP_VERSION = 62,

        /// <summary>ERX-IP-Mcast-Adm-Bw-Limit (Type 63). Integer. IP multicast admission bandwidth limit.</summary>
        IP_MCAST_ADM_BW_LIMIT = 63,

        /// <summary>ERX-IPv6-Mcast-Adm-Bw-Limit (Type 64). Integer. IPv6 multicast admission bandwidth limit.</summary>
        IPV6_MCAST_ADM_BW_LIMIT = 64,

        /// <summary>ERX-Qos-Profile-Name (Type 65). String. QoS profile name.</summary>
        QOS_PROFILE_NAME = 65,

        /// <summary>ERX-Service-Session (Type 66). Octets. Service session data.</summary>
        SERVICE_SESSION = 66,

        /// <summary>ERX-Mobile-IP-Algorithm (Type 67). Integer. Mobile IP algorithm.</summary>
        MOBILE_IP_ALGORITHM = 67,

        /// <summary>ERX-Mobile-IP-SPI (Type 68). Integer. Mobile IP Security Parameter Index.</summary>
        MOBILE_IP_SPI = 68,

        /// <summary>ERX-Mobile-IP-Key (Type 69). String. Mobile IP key.</summary>
        MOBILE_IP_KEY = 69,

        /// <summary>ERX-Mobile-IP-Replay (Type 70). Integer. Mobile IP replay method.</summary>
        MOBILE_IP_REPLAY = 70,

        /// <summary>ERX-Mobile-IP-Access-Control (Type 71). String. Mobile IP access control list.</summary>
        MOBILE_IP_ACCESS_CONTROL = 71,

        /// <summary>ERX-Mobile-IP-Lifetime (Type 72). Integer. Mobile IP lifetime in seconds.</summary>
        MOBILE_IP_LIFETIME = 72,

        /// <summary>ERX-ACC-Loop-Circuit-Id (Type 73). String. Access loop circuit identifier.</summary>
        ACC_LOOP_CIRCUIT_ID = 73,

        /// <summary>ERX-ACC-Loop-Remote-Id (Type 74). String. Access loop remote identifier.</summary>
        ACC_LOOP_REMOTE_ID = 74,

        /// <summary>ERX-ACC-Loop-Encap (Type 75). Octets. Access loop encapsulation.</summary>
        ACC_LOOP_ENCAP = 75,

        /// <summary>ERX-Ipv6-Ingress-Policy-Name (Type 76). String. IPv6 ingress policy name.</summary>
        IPV6_INGRESS_POLICY_NAME = 76,

        /// <summary>ERX-Ipv6-Egress-Policy-Name (Type 77). String. IPv6 egress policy name.</summary>
        IPV6_EGRESS_POLICY_NAME = 77,

        /// <summary>ERX-Ipv6-Ingress-Statistics (Type 78). Integer. IPv6 ingress statistics flag.</summary>
        IPV6_INGRESS_STATISTICS = 78,

        /// <summary>ERX-Ipv6-Egress-Statistics (Type 79). Integer. IPv6 egress statistics flag.</summary>
        IPV6_EGRESS_STATISTICS = 79,

        /// <summary>ERX-Service-Acct-Interval (Type 80). Integer. Service accounting interval in seconds.</summary>
        SERVICE_ACCT_INTERVAL = 80,

        /// <summary>ERX-DHCP-Vendor-Class-Id (Type 81). String. DHCP vendor class identifier.</summary>
        DHCP_VENDOR_CLASS_ID = 81
    }

    /// <summary>
    /// ERX-Service-Category attribute values (Type 14).
    /// </summary>
    public enum ERX_SERVICE_CATEGORY
    {
        /// <summary>Undefined Bit Rate.</summary>
        UBR = 1,

        /// <summary>Unspecified Bit Rate Plus.</summary>
        UBRPLUS = 2,

        /// <summary>Variable Bit Rate Real-Time.</summary>
        VBR_RT = 3,

        /// <summary>Variable Bit Rate Non-Real-Time.</summary>
        VBR_NRT = 4,

        /// <summary>Constant Bit Rate.</summary>
        CBR = 6
    }

    /// <summary>
    /// ERX-SA-Validate attribute values (Type 22).
    /// </summary>
    public enum ERX_SA_VALIDATE
    {
        /// <summary>Source address validation disabled.</summary>
        DISABLE = 0,

        /// <summary>Source address validation enabled.</summary>
        ENABLE = 1
    }

    /// <summary>
    /// ERX-Igmp-Enable attribute values (Type 23).
    /// </summary>
    public enum ERX_IGMP_ENABLE
    {
        /// <summary>IGMP disabled.</summary>
        DISABLE = 0,

        /// <summary>IGMP enabled.</summary>
        ENABLE = 1
    }

    /// <summary>
    /// ERX-PPP-Auth-Protocol attribute values (Type 25).
    /// </summary>
    public enum ERX_PPP_AUTH_PROTOCOL
    {
        /// <summary>No authentication.</summary>
        NONE = 0,

        /// <summary>PAP authentication.</summary>
        PAP = 1,

        /// <summary>CHAP authentication.</summary>
        CHAP = 2,

        /// <summary>PAP then CHAP.</summary>
        PAP_CHAP = 3,

        /// <summary>CHAP then PAP.</summary>
        CHAP_PAP = 4
    }

    /// <summary>
    /// ERX-Tunnel-Bearer-Type attribute values (Type 28).
    /// </summary>
    public enum ERX_TUNNEL_BEARER_TYPE
    {
        /// <summary>No bearer.</summary>
        NONE = 0,

        /// <summary>Analog bearer.</summary>
        ANALOG = 1,

        /// <summary>Digital bearer.</summary>
        DIGITAL = 2
    }

    /// <summary>
    /// ERX-LI-Action attribute values (Type 42).
    /// </summary>
    public enum ERX_LI_ACTION
    {
        /// <summary>Disable lawful intercept.</summary>
        OFF = 0,

        /// <summary>Enable lawful intercept.</summary>
        ON = 1
    }

    /// <summary>
    /// ERX-DF-Bit attribute values (Type 54).
    /// </summary>
    public enum ERX_DF_BIT
    {
        /// <summary>DF bit cleared (allow fragmentation).</summary>
        DONT_IGNORE = 0,

        /// <summary>DF bit set (don't fragment).</summary>
        IGNORE = 1
    }

    /// <summary>
    /// ERX-Service-Statistics attribute values (Type 53).
    /// </summary>
    public enum ERX_SERVICE_STATISTICS
    {
        /// <summary>Service statistics disabled.</summary>
        DISABLE = 0,

        /// <summary>Service statistics time-based.</summary>
        TIME = 1,

        /// <summary>Service statistics time and volume-based.</summary>
        TIME_VOLUME = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Juniper ERX /
    /// Unisphere (IANA PEN 4874) Vendor-Specific Attributes (VSAs), as defined in the
    /// FreeRADIUS <c>dictionary.erx</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ERX vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 4874</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Juniper Networks ERX/E-series edge routers
    /// and Broadband Network Gateways (BNG) for RADIUS-based subscriber session
    /// management including virtual router assignment, address pool selection,
    /// DNS/WINS provisioning, ingress/egress policy enforcement, ATM service
    /// categories, QoS profiles, PPP authentication, tunnel configuration,
    /// IGMP/MLD multicast control, service activation/deactivation, lawful
    /// intercept, access loop identification (DSL forum), DHCP relay, Mobile IP,
    /// IPv6 policies, and service accounting.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ErxAttributes.VirtualRouterName("default"));
    /// packet.SetAttribute(ErxAttributes.AddressPoolName("subscriber-pool"));
    /// packet.SetAttribute(ErxAttributes.IngressPolicyName("rate-limit-10m"));
    /// packet.SetAttribute(ErxAttributes.EgressPolicyName("shape-50m"));
    /// packet.SetAttribute(ErxAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(ErxAttributes.ServiceActivate("internet-basic"));
    /// </code>
    /// </remarks>
    public static class ErxAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Juniper ERX (Unisphere Networks).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 4874;

        #region Integer Attributes

        /// <summary>Creates an ERX-Ingress-Statistics attribute (Type 12).</summary>
        public static VendorSpecificAttributes IngressStatistics(int value) => CreateInteger(ErxAttributeType.INGRESS_STATISTICS, value);

        /// <summary>Creates an ERX-Egress-Statistics attribute (Type 13).</summary>
        public static VendorSpecificAttributes EgressStatistics(int value) => CreateInteger(ErxAttributeType.EGRESS_STATISTICS, value);

        /// <summary>Creates an ERX-Service-Category attribute (Type 14).</summary>
        /// <param name="value">The ATM service category. See <see cref="ERX_SERVICE_CATEGORY"/>.</param>
        public static VendorSpecificAttributes ServiceCategory(ERX_SERVICE_CATEGORY value) => CreateInteger(ErxAttributeType.SERVICE_CATEGORY, (int)value);

        /// <summary>Creates an ERX-PCR attribute (Type 15).</summary>
        public static VendorSpecificAttributes Pcr(int value) => CreateInteger(ErxAttributeType.PCR, value);

        /// <summary>Creates an ERX-SCR attribute (Type 16).</summary>
        public static VendorSpecificAttributes Scr(int value) => CreateInteger(ErxAttributeType.SCR, value);

        /// <summary>Creates an ERX-MBS attribute (Type 17).</summary>
        public static VendorSpecificAttributes Mbs(int value) => CreateInteger(ErxAttributeType.MBS, value);

        /// <summary>Creates an ERX-Allow-All-VR-Access attribute (Type 19).</summary>
        public static VendorSpecificAttributes AllowAllVrAccess(int value) => CreateInteger(ErxAttributeType.ALLOW_ALL_VR_ACCESS, value);

        /// <summary>Creates an ERX-SA-Validate attribute (Type 22).</summary>
        /// <param name="value">The source address validation setting. See <see cref="ERX_SA_VALIDATE"/>.</param>
        public static VendorSpecificAttributes SaValidate(ERX_SA_VALIDATE value) => CreateInteger(ErxAttributeType.SA_VALIDATE, (int)value);

        /// <summary>Creates an ERX-Igmp-Enable attribute (Type 23).</summary>
        /// <param name="value">The IGMP enable setting. See <see cref="ERX_IGMP_ENABLE"/>.</param>
        public static VendorSpecificAttributes IgmpEnable(ERX_IGMP_ENABLE value) => CreateInteger(ErxAttributeType.IGMP_ENABLE, (int)value);

        /// <summary>Creates an ERX-DHCP-Guidelines-Comply attribute (Type 24).</summary>
        public static VendorSpecificAttributes DhcpGuidelinesComply(int value) => CreateInteger(ErxAttributeType.DHCP_GUIDELINES_COMPLY, value);

        /// <summary>Creates an ERX-PPP-Auth-Protocol attribute (Type 25).</summary>
        /// <param name="value">The PPP authentication protocol. See <see cref="ERX_PPP_AUTH_PROTOCOL"/>.</param>
        public static VendorSpecificAttributes PppAuthProtocol(ERX_PPP_AUTH_PROTOCOL value) => CreateInteger(ErxAttributeType.PPP_AUTH_PROTOCOL, (int)value);

        /// <summary>Creates an ERX-Tunnel-Min-Bps attribute (Type 26).</summary>
        public static VendorSpecificAttributes TunnelMinBps(int value) => CreateInteger(ErxAttributeType.TUNNEL_MIN_BPS, value);

        /// <summary>Creates an ERX-Tunnel-Max-Bps attribute (Type 27).</summary>
        public static VendorSpecificAttributes TunnelMaxBps(int value) => CreateInteger(ErxAttributeType.TUNNEL_MAX_BPS, value);

        /// <summary>Creates an ERX-Tunnel-Bearer-Type attribute (Type 28).</summary>
        /// <param name="value">The tunnel bearer type. See <see cref="ERX_TUNNEL_BEARER_TYPE"/>.</param>
        public static VendorSpecificAttributes TunnelBearerType(ERX_TUNNEL_BEARER_TYPE value) => CreateInteger(ErxAttributeType.TUNNEL_BEARER_TYPE, (int)value);

        /// <summary>Creates an ERX-Input-Gigapkts attribute (Type 29).</summary>
        public static VendorSpecificAttributes InputGigapkts(int value) => CreateInteger(ErxAttributeType.INPUT_GIGAPKTS, value);

        /// <summary>Creates an ERX-Output-Gigapkts attribute (Type 30).</summary>
        public static VendorSpecificAttributes OutputGigapkts(int value) => CreateInteger(ErxAttributeType.OUTPUT_GIGAPKTS, value);

        /// <summary>Creates an ERX-LI-Action attribute (Type 42).</summary>
        /// <param name="value">The lawful intercept action. See <see cref="ERX_LI_ACTION"/>.</param>
        public static VendorSpecificAttributes LiAction(ERX_LI_ACTION value) => CreateInteger(ErxAttributeType.LI_ACTION, (int)value);

        /// <summary>Creates an ERX-Med-Dev-Handle attribute (Type 43).</summary>
        public static VendorSpecificAttributes MedDevHandle(int value) => CreateInteger(ErxAttributeType.MED_DEV_HANDLE, value);

        /// <summary>Creates an ERX-Med-Port-Number attribute (Type 45).</summary>
        public static VendorSpecificAttributes MedPortNumber(int value) => CreateInteger(ErxAttributeType.MED_PORT_NUMBER, value);

        /// <summary>Creates an ERX-Service-Volume attribute (Type 51).</summary>
        public static VendorSpecificAttributes ServiceVolume(int value) => CreateInteger(ErxAttributeType.SERVICE_VOLUME, value);

        /// <summary>Creates an ERX-Service-Timeout attribute (Type 52).</summary>
        public static VendorSpecificAttributes ServiceTimeout(int value) => CreateInteger(ErxAttributeType.SERVICE_TIMEOUT, value);

        /// <summary>Creates an ERX-Service-Statistics attribute (Type 53).</summary>
        /// <param name="value">The service statistics type. See <see cref="ERX_SERVICE_STATISTICS"/>.</param>
        public static VendorSpecificAttributes ServiceStatistics(ERX_SERVICE_STATISTICS value) => CreateInteger(ErxAttributeType.SERVICE_STATISTICS, (int)value);

        /// <summary>Creates an ERX-DF-Bit attribute (Type 54).</summary>
        /// <param name="value">The DF bit setting. See <see cref="ERX_DF_BIT"/>.</param>
        public static VendorSpecificAttributes DfBit(ERX_DF_BIT value) => CreateInteger(ErxAttributeType.DF_BIT, (int)value);

        /// <summary>Creates an ERX-MLD-Version attribute (Type 61).</summary>
        public static VendorSpecificAttributes MldVersion(int value) => CreateInteger(ErxAttributeType.MLD_VERSION, value);

        /// <summary>Creates an ERX-IGMP-Version attribute (Type 62).</summary>
        public static VendorSpecificAttributes IgmpVersion(int value) => CreateInteger(ErxAttributeType.IGMP_VERSION, value);

        /// <summary>Creates an ERX-IP-Mcast-Adm-Bw-Limit attribute (Type 63).</summary>
        public static VendorSpecificAttributes IpMcastAdmBwLimit(int value) => CreateInteger(ErxAttributeType.IP_MCAST_ADM_BW_LIMIT, value);

        /// <summary>Creates an ERX-IPv6-Mcast-Adm-Bw-Limit attribute (Type 64).</summary>
        public static VendorSpecificAttributes Ipv6McastAdmBwLimit(int value) => CreateInteger(ErxAttributeType.IPV6_MCAST_ADM_BW_LIMIT, value);

        /// <summary>Creates an ERX-Mobile-IP-Algorithm attribute (Type 67).</summary>
        public static VendorSpecificAttributes MobileIpAlgorithm(int value) => CreateInteger(ErxAttributeType.MOBILE_IP_ALGORITHM, value);

        /// <summary>Creates an ERX-Mobile-IP-SPI attribute (Type 68).</summary>
        public static VendorSpecificAttributes MobileIpSpi(int value) => CreateInteger(ErxAttributeType.MOBILE_IP_SPI, value);

        /// <summary>Creates an ERX-Mobile-IP-Replay attribute (Type 70).</summary>
        public static VendorSpecificAttributes MobileIpReplay(int value) => CreateInteger(ErxAttributeType.MOBILE_IP_REPLAY, value);

        /// <summary>Creates an ERX-Mobile-IP-Lifetime attribute (Type 72).</summary>
        public static VendorSpecificAttributes MobileIpLifetime(int value) => CreateInteger(ErxAttributeType.MOBILE_IP_LIFETIME, value);

        /// <summary>Creates an ERX-Ipv6-Ingress-Statistics attribute (Type 78).</summary>
        public static VendorSpecificAttributes Ipv6IngressStatistics(int value) => CreateInteger(ErxAttributeType.IPV6_INGRESS_STATISTICS, value);

        /// <summary>Creates an ERX-Ipv6-Egress-Statistics attribute (Type 79).</summary>
        public static VendorSpecificAttributes Ipv6EgressStatistics(int value) => CreateInteger(ErxAttributeType.IPV6_EGRESS_STATISTICS, value);

        /// <summary>Creates an ERX-Service-Acct-Interval attribute (Type 80).</summary>
        /// <param name="value">The service accounting interval in seconds.</param>
        public static VendorSpecificAttributes ServiceAcctInterval(int value) => CreateInteger(ErxAttributeType.SERVICE_ACCT_INTERVAL, value);

        #endregion

        #region String Attributes

        /// <summary>Creates an ERX-Virtual-Router-Name attribute (Type 1).</summary>
        /// <param name="value">The virtual router name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes VirtualRouterName(string value) => CreateString(ErxAttributeType.VIRTUAL_ROUTER_NAME, value);

        /// <summary>Creates an ERX-Address-Pool-Name attribute (Type 2).</summary>
        /// <param name="value">The address pool name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AddressPoolName(string value) => CreateString(ErxAttributeType.ADDRESS_POOL_NAME, value);

        /// <summary>Creates an ERX-Local-Loopback-Interface attribute (Type 3).</summary>
        /// <param name="value">The local loopback interface name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes LocalLoopbackInterface(string value) => CreateString(ErxAttributeType.LOCAL_LOOPBACK_INTERFACE, value);

        /// <summary>Creates an ERX-Tunnel-Virtual-Router attribute (Type 8).</summary>
        /// <param name="value">The tunnel virtual router name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes TunnelVirtualRouter(string value) => CreateString(ErxAttributeType.TUNNEL_VIRTUAL_ROUTER, value);

        /// <summary>Creates an ERX-Tunnel-Password attribute (Type 9).</summary>
        /// <param name="value">The tunnel password. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes TunnelPassword(string value) => CreateString(ErxAttributeType.TUNNEL_PASSWORD, value);

        /// <summary>Creates an ERX-Ingress-Policy-Name attribute (Type 10).</summary>
        /// <param name="value">The ingress policy name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IngressPolicyName(string value) => CreateString(ErxAttributeType.INGRESS_POLICY_NAME, value);

        /// <summary>Creates an ERX-Egress-Policy-Name attribute (Type 11).</summary>
        /// <param name="value">The egress policy name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes EgressPolicyName(string value) => CreateString(ErxAttributeType.EGRESS_POLICY_NAME, value);

        /// <summary>Creates an ERX-Init-CLI-Access-Level attribute (Type 18).</summary>
        /// <param name="value">The initial CLI access level. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes InitCliAccessLevel(string value) => CreateString(ErxAttributeType.INIT_CLI_ACCESS_LEVEL, value);

        /// <summary>Creates an ERX-Alt-CLI-Access-Level attribute (Type 20).</summary>
        /// <param name="value">The alternate CLI access level. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AltCliAccessLevel(string value) => CreateString(ErxAttributeType.ALT_CLI_ACCESS_LEVEL, value);

        /// <summary>Creates an ERX-Alt-CLI-VRouter-Name attribute (Type 21).</summary>
        /// <param name="value">The alternate CLI virtual router name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AltCliVrouterName(string value) => CreateString(ErxAttributeType.ALT_CLI_VROUTER_NAME, value);

        /// <summary>Creates an ERX-Tunnel-Interface-Id attribute (Type 31).</summary>
        /// <param name="value">The tunnel interface identifier. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes TunnelInterfaceId(string value) => CreateString(ErxAttributeType.TUNNEL_INTERFACE_ID, value);

        /// <summary>Creates an ERX-IpV6-Virtual-Router attribute (Type 32).</summary>
        /// <param name="value">The IPv6 virtual router name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Ipv6VirtualRouter(string value) => CreateString(ErxAttributeType.IPV6_VIRTUAL_ROUTER, value);

        /// <summary>Creates an ERX-IpV6-Local-Interface attribute (Type 33).</summary>
        /// <param name="value">The IPv6 local interface name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Ipv6LocalInterface(string value) => CreateString(ErxAttributeType.IPV6_LOCAL_INTERFACE, value);

        /// <summary>Creates an ERX-Ipv6-Primary-Dns attribute (Type 34).</summary>
        /// <param name="value">The IPv6 primary DNS server address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Ipv6PrimaryDns(string value) => CreateString(ErxAttributeType.IPV6_PRIMARY_DNS, value);

        /// <summary>Creates an ERX-Ipv6-Secondary-Dns attribute (Type 35).</summary>
        /// <param name="value">The IPv6 secondary DNS server address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Ipv6SecondaryDns(string value) => CreateString(ErxAttributeType.IPV6_SECONDARY_DNS, value);

        /// <summary>Creates an ERX-Service-Description attribute (Type 38).</summary>
        /// <param name="value">The service description. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes ServiceDescription(string value) => CreateString(ErxAttributeType.SERVICE_DESCRIPTION, value);

        /// <summary>Creates an ERX-DHCP-MAC-Address attribute (Type 40).</summary>
        /// <param name="value">The DHCP client MAC address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes DhcpMacAddress(string value) => CreateString(ErxAttributeType.DHCP_MAC_ADDRESS, value);

        /// <summary>Creates an ERX-MLPPP-Bundle-Name attribute (Type 46).</summary>
        /// <param name="value">The MLPPP bundle name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes MlpppBundleName(string value) => CreateString(ErxAttributeType.MLPPP_BUNDLE_NAME, value);

        /// <summary>Creates an ERX-Interface-Desc attribute (Type 47).</summary>
        /// <param name="value">The interface description. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes InterfaceDesc(string value) => CreateString(ErxAttributeType.INTERFACE_DESC, value);

        /// <summary>Creates an ERX-Tunnel-Group attribute (Type 48).</summary>
        /// <param name="value">The tunnel group name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes TunnelGroup(string value) => CreateString(ErxAttributeType.TUNNEL_GROUP, value);

        /// <summary>Creates an ERX-Service-Activate attribute (Type 49).</summary>
        /// <param name="value">The service activation string. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes ServiceActivate(string value) => CreateString(ErxAttributeType.SERVICE_ACTIVATE, value);

        /// <summary>Creates an ERX-Service-Deactivate attribute (Type 50).</summary>
        /// <param name="value">The service deactivation string. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes ServiceDeactivate(string value) => CreateString(ErxAttributeType.SERVICE_DEACTIVATE, value);

        /// <summary>Creates an ERX-IGMP-Access-Name attribute (Type 55).</summary>
        /// <param name="value">The IGMP access list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IgmpAccessName(string value) => CreateString(ErxAttributeType.IGMP_ACCESS_NAME, value);

        /// <summary>Creates an ERX-IGMP-Access-Src-Name attribute (Type 56).</summary>
        /// <param name="value">The IGMP source access list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IgmpAccessSrcName(string value) => CreateString(ErxAttributeType.IGMP_ACCESS_SRC_NAME, value);

        /// <summary>Creates an ERX-IGMP-OIF-Map-Name attribute (Type 57).</summary>
        /// <param name="value">The IGMP outgoing interface map name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IgmpOifMapName(string value) => CreateString(ErxAttributeType.IGMP_OIF_MAP_NAME, value);

        /// <summary>Creates an ERX-MLD-Access-Name attribute (Type 58).</summary>
        /// <param name="value">The MLD access list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes MldAccessName(string value) => CreateString(ErxAttributeType.MLD_ACCESS_NAME, value);

        /// <summary>Creates an ERX-MLD-Access-Src-Name attribute (Type 59).</summary>
        /// <param name="value">The MLD source access list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes MldAccessSrcName(string value) => CreateString(ErxAttributeType.MLD_ACCESS_SRC_NAME, value);

        /// <summary>Creates an ERX-MLD-OIF-Map-Name attribute (Type 60).</summary>
        /// <param name="value">The MLD outgoing interface map name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes MldOifMapName(string value) => CreateString(ErxAttributeType.MLD_OIF_MAP_NAME, value);

        /// <summary>Creates an ERX-Qos-Profile-Name attribute (Type 65).</summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes QosProfileName(string value) => CreateString(ErxAttributeType.QOS_PROFILE_NAME, value);

        /// <summary>Creates an ERX-Mobile-IP-Key attribute (Type 69).</summary>
        /// <param name="value">The Mobile IP key. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes MobileIpKey(string value) => CreateString(ErxAttributeType.MOBILE_IP_KEY, value);

        /// <summary>Creates an ERX-Mobile-IP-Access-Control attribute (Type 71).</summary>
        /// <param name="value">The Mobile IP access control list. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes MobileIpAccessControl(string value) => CreateString(ErxAttributeType.MOBILE_IP_ACCESS_CONTROL, value);

        /// <summary>Creates an ERX-ACC-Loop-Circuit-Id attribute (Type 73).</summary>
        /// <param name="value">The access loop circuit identifier. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AccLoopCircuitId(string value) => CreateString(ErxAttributeType.ACC_LOOP_CIRCUIT_ID, value);

        /// <summary>Creates an ERX-ACC-Loop-Remote-Id attribute (Type 74).</summary>
        /// <param name="value">The access loop remote identifier. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AccLoopRemoteId(string value) => CreateString(ErxAttributeType.ACC_LOOP_REMOTE_ID, value);

        /// <summary>Creates an ERX-Ipv6-Ingress-Policy-Name attribute (Type 76).</summary>
        /// <param name="value">The IPv6 ingress policy name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Ipv6IngressPolicyName(string value) => CreateString(ErxAttributeType.IPV6_INGRESS_POLICY_NAME, value);

        /// <summary>Creates an ERX-Ipv6-Egress-Policy-Name attribute (Type 77).</summary>
        /// <param name="value">The IPv6 egress policy name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Ipv6EgressPolicyName(string value) => CreateString(ErxAttributeType.IPV6_EGRESS_POLICY_NAME, value);

        /// <summary>Creates an ERX-DHCP-Vendor-Class-Id attribute (Type 81).</summary>
        /// <param name="value">The DHCP vendor class identifier. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes DhcpVendorClassId(string value) => CreateString(ErxAttributeType.DHCP_VENDOR_CLASS_ID, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates an ERX-Primary-Dns attribute (Type 4) with the specified IPv4 address.</summary>
        /// <param name="value">The primary DNS server. Must be IPv4.</param>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value) => CreateIpv4(ErxAttributeType.PRIMARY_DNS, value);

        /// <summary>Creates an ERX-Secondary-Dns attribute (Type 5) with the specified IPv4 address.</summary>
        /// <param name="value">The secondary DNS server. Must be IPv4.</param>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value) => CreateIpv4(ErxAttributeType.SECONDARY_DNS, value);

        /// <summary>Creates an ERX-Primary-Wins attribute (Type 6) with the specified IPv4 address.</summary>
        /// <param name="value">The primary WINS server. Must be IPv4.</param>
        public static VendorSpecificAttributes PrimaryWins(IPAddress value) => CreateIpv4(ErxAttributeType.PRIMARY_WINS, value);

        /// <summary>Creates an ERX-Secondary-Wins attribute (Type 7) with the specified IPv4 address.</summary>
        /// <param name="value">The secondary WINS server. Must be IPv4.</param>
        public static VendorSpecificAttributes SecondaryWins(IPAddress value) => CreateIpv4(ErxAttributeType.SECONDARY_WINS, value);

        /// <summary>Creates an ERX-Radius-Client-Address attribute (Type 37) with the specified IPv4 address.</summary>
        /// <param name="value">The RADIUS client address. Must be IPv4.</param>
        public static VendorSpecificAttributes RadiusClientAddress(IPAddress value) => CreateIpv4(ErxAttributeType.RADIUS_CLIENT_ADDRESS, value);

        /// <summary>Creates an ERX-DHCP-GI-Address attribute (Type 41) with the specified IPv4 address.</summary>
        /// <param name="value">The DHCP gateway interface address. Must be IPv4.</param>
        public static VendorSpecificAttributes DhcpGiAddress(IPAddress value) => CreateIpv4(ErxAttributeType.DHCP_GI_ADDRESS, value);

        /// <summary>Creates an ERX-Med-Ip-Address attribute (Type 44) with the specified IPv4 address.</summary>
        /// <param name="value">The mediation IP address. Must be IPv4.</param>
        public static VendorSpecificAttributes MedIpAddress(IPAddress value) => CreateIpv4(ErxAttributeType.MED_IP_ADDRESS, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates an ERX-Disconnect-Cause attribute (Type 36).</summary>
        /// <param name="value">The disconnect cause data. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes DisconnectCause(byte[] value) => CreateOctets(ErxAttributeType.DISCONNECT_CAUSE, value);

        /// <summary>Creates an ERX-DHCP-Options attribute (Type 39).</summary>
        /// <param name="value">The DHCP options data. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes DhcpOptions(byte[] value) => CreateOctets(ErxAttributeType.DHCP_OPTIONS, value);

        /// <summary>Creates an ERX-Service-Session attribute (Type 66).</summary>
        /// <param name="value">The service session data. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes ServiceSession(byte[] value) => CreateOctets(ErxAttributeType.SERVICE_SESSION, value);

        /// <summary>Creates an ERX-ACC-Loop-Encap attribute (Type 75).</summary>
        /// <param name="value">The access loop encapsulation data. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AccLoopEncap(byte[] value) => CreateOctets(ErxAttributeType.ACC_LOOP_ENCAP, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(ErxAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(ErxAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(ErxAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        private static VendorSpecificAttributes CreateIpv4(ErxAttributeType type, IPAddress value)
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