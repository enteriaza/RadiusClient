using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a ZTE Corporation (IANA PEN 3902) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.zte</c>.
    /// </summary>
    /// <remarks>
    /// ZTE Corporation is a major Chinese multinational telecommunications equipment
    /// and systems company producing OLT/ONU (GPON/EPON), BRAS/BNG, routers, switches,
    /// mobile core (GGSN/PGW/SGW), and broadband access platforms for carrier and
    /// enterprise deployments worldwide.
    /// </remarks>
    public enum ZteAttributeType : byte
    {
        /// <summary>ZTE-Client-DNS-Pri (Type 1). IP address. Primary DNS server for client.</summary>
        CLIENT_DNS_PRI = 1,

        /// <summary>ZTE-Client-DNS-Sec (Type 2). IP address. Secondary DNS server for client.</summary>
        CLIENT_DNS_SEC = 2,

        /// <summary>ZTE-Context-Name (Type 4). String. VRF/context name.</summary>
        CONTEXT_NAME = 4,

        /// <summary>ZTE-Tunnel-Max-Sessions (Type 5). Integer. Maximum tunnel sessions.</summary>
        TUNNEL_MAX_SESSIONS = 5,

        /// <summary>ZTE-Tunnel-Max-Tunnels (Type 6). Integer. Maximum tunnels.</summary>
        TUNNEL_MAX_TUNNELS = 6,

        /// <summary>ZTE-Tunnel-Window (Type 7). Integer. Tunnel receive window size.</summary>
        TUNNEL_WINDOW = 7,

        /// <summary>ZTE-Tunnel-Retransmit (Type 8). Integer. Tunnel retransmit count.</summary>
        TUNNEL_RETRANSMIT = 8,

        /// <summary>ZTE-Tunnel-Cmd-Timeout (Type 9). Integer. Tunnel command timeout in seconds.</summary>
        TUNNEL_CMD_TIMEOUT = 9,

        /// <summary>ZTE-PPPOE-URL (Type 10). String. PPPoE URL for redirection.</summary>
        PPPOE_URL = 10,

        /// <summary>ZTE-TCP-MSS-Adjust (Type 11). Integer. TCP MSS adjustment value.</summary>
        TCP_MSS_ADJUST = 11,

        /// <summary>ZTE-Rate-Ctrl-Scr-Up (Type 30). Integer. Upstream sustained cell rate in Kbps.</summary>
        RATE_CTRL_SCR_UP = 30,

        /// <summary>ZTE-Rate-Ctrl-Scr-Down (Type 31). Integer. Downstream sustained cell rate in Kbps.</summary>
        RATE_CTRL_SCR_DOWN = 31,

        /// <summary>ZTE-Rate-Ctrl-Burst-Up (Type 32). Integer. Upstream burst size in bytes.</summary>
        RATE_CTRL_BURST_UP = 32,

        /// <summary>ZTE-Rate-Ctrl-Burst-Down (Type 33). Integer. Downstream burst size in bytes.</summary>
        RATE_CTRL_BURST_DOWN = 33,

        /// <summary>ZTE-Police-Cir (Type 34). Integer. Committed information rate in Kbps.</summary>
        POLICE_CIR = 34,

        /// <summary>ZTE-Police-Pir (Type 35). Integer. Peak information rate in Kbps.</summary>
        POLICE_PIR = 35,

        /// <summary>ZTE-Police-Cbs (Type 36). Integer. Committed burst size in bytes.</summary>
        POLICE_CBS = 36,

        /// <summary>ZTE-Police-Pbs (Type 37). Integer. Peak burst size in bytes.</summary>
        POLICE_PBS = 37,

        /// <summary>ZTE-Rate-Ctrl-Pcr-Up (Type 38). Integer. Upstream peak cell rate in Kbps.</summary>
        RATE_CTRL_PCR_UP = 38,

        /// <summary>ZTE-Rate-Ctrl-Pcr-Down (Type 39). Integer. Downstream peak cell rate in Kbps.</summary>
        RATE_CTRL_PCR_DOWN = 39,

        /// <summary>ZTE-QoS-Profile-Up (Type 40). String. Upstream QoS profile name.</summary>
        QOS_PROFILE_UP = 40,

        /// <summary>ZTE-QoS-Profile-Down (Type 41). String. Downstream QoS profile name.</summary>
        QOS_PROFILE_DOWN = 41,

        /// <summary>ZTE-Access-Type (Type 50). Integer. Access type.</summary>
        ACCESS_TYPE = 50,

        /// <summary>ZTE-Rate-Bust-DPIR (Type 55). Integer. Downstream peak information rate burst.</summary>
        RATE_BUST_DPIR = 55,

        /// <summary>ZTE-Rate-Bust-UPIR (Type 56). Integer. Upstream peak information rate burst.</summary>
        RATE_BUST_UPIR = 56,

        /// <summary>ZTE-Rate-Bust-DCIR (Type 57). Integer. Downstream committed information rate burst.</summary>
        RATE_BUST_DCIR = 57,

        /// <summary>ZTE-Rate-Bust-UCIR (Type 58). Integer. Upstream committed information rate burst.</summary>
        RATE_BUST_UCIR = 58,

        /// <summary>ZTE-VPN-ID (Type 68). String. VPN identifier.</summary>
        VPN_ID = 68,

        /// <summary>ZTE-Rate-Ctrl-SCR-Up-v2 (Type 80). Integer. Upstream SCR v2 in Kbps.</summary>
        RATE_CTRL_SCR_UP_V2 = 80,

        /// <summary>ZTE-Rate-Ctrl-SCR-Down-v2 (Type 81). Integer. Downstream SCR v2 in Kbps.</summary>
        RATE_CTRL_SCR_DOWN_V2 = 81,

        /// <summary>ZTE-Rate-Ctrl-PCR-Up-v2 (Type 82). Integer. Upstream PCR v2 in Kbps.</summary>
        RATE_CTRL_PCR_UP_V2 = 82,

        /// <summary>ZTE-Rate-Ctrl-PCR-Down-v2 (Type 83). Integer. Downstream PCR v2 in Kbps.</summary>
        RATE_CTRL_PCR_DOWN_V2 = 83,

        /// <summary>ZTE-Rate-Ctrl-Burst-Up-v2 (Type 84). Integer. Upstream burst v2 in bytes.</summary>
        RATE_CTRL_BURST_UP_V2 = 84,

        /// <summary>ZTE-Rate-Ctrl-Burst-Down-v2 (Type 85). Integer. Downstream burst v2 in bytes.</summary>
        RATE_CTRL_BURST_DOWN_V2 = 85,

        /// <summary>ZTE-DHCP-Option60 (Type 90). String. DHCP Option 60 (Vendor Class Identifier).</summary>
        DHCP_OPTION60 = 90,

        /// <summary>ZTE-PPP-Sservice-Type (Type 101). Integer. PPP service type.</summary>
        PPP_SSERVICE_TYPE = 101,

        /// <summary>ZTE-SW-Privilege (Type 102). Integer. Switch privilege level.</summary>
        SW_PRIVILEGE = 102,

        /// <summary>ZTE-Access-Domain (Type 151). String. Access domain name.</summary>
        ACCESS_DOMAIN = 151,

        /// <summary>ZTE-CoA-Type (Type 152). Integer. Change of Authorization type.</summary>
        COA_TYPE = 152,

        /// <summary>ZTE-VLAN-ID (Type 153). Integer. VLAN identifier.</summary>
        VLAN_ID = 153,

        /// <summary>ZTE-Subscriber-ID (Type 160). String. Subscriber identifier.</summary>
        SUBSCRIBER_ID = 160,

        /// <summary>ZTE-IPv6-DNS-Pri (Type 161). String. Primary IPv6 DNS server.</summary>
        IPV6_DNS_PRI = 161,

        /// <summary>ZTE-IPv6-DNS-Sec (Type 162). String. Secondary IPv6 DNS server.</summary>
        IPV6_DNS_SEC = 162,

        /// <summary>ZTE-Delegated-IPv6-Prefix-Pool (Type 163). String. Delegated IPv6 prefix pool name.</summary>
        DELEGATED_IPV6_PREFIX_POOL = 163,

        /// <summary>ZTE-IPv6-Address-Pool (Type 164). String. IPv6 address pool name.</summary>
        IPV6_ADDRESS_POOL = 164,

        /// <summary>ZTE-Framed-Pool (Type 170). String. Framed IP address pool name.</summary>
        FRAMED_POOL = 170,

        /// <summary>ZTE-NAT-Service-Profile (Type 180). String. NAT service profile name.</summary>
        NAT_SERVICE_PROFILE = 180,

        /// <summary>ZTE-NAT-Public-Address (Type 181). IP address. NAT public address.</summary>
        NAT_PUBLIC_ADDRESS = 181,

        /// <summary>ZTE-Acct-Input-Packets-64 (Type 200). String. 64-bit input packets counter.</summary>
        ACCT_INPUT_PACKETS_64 = 200,

        /// <summary>ZTE-Acct-Output-Packets-64 (Type 201). String. 64-bit output packets counter.</summary>
        ACCT_OUTPUT_PACKETS_64 = 201,

        /// <summary>ZTE-Acct-Input-Octets-64 (Type 202). String. 64-bit input octets counter.</summary>
        ACCT_INPUT_OCTETS_64 = 202,

        /// <summary>ZTE-Acct-Output-Octets-64 (Type 203). String. 64-bit output octets counter.</summary>
        ACCT_OUTPUT_OCTETS_64 = 203,

        /// <summary>ZTE-Inner-VLAN-ID (Type 254). Integer. Inner (QinQ) VLAN identifier.</summary>
        INNER_VLAN_ID = 254
    }

    /// <summary>
    /// ZTE-SW-Privilege attribute values (Type 102).
    /// </summary>
    public enum ZTE_SW_PRIVILEGE
    {
        /// <summary>Monitor (read-only) access.</summary>
        MONITOR = 0,

        /// <summary>Standard user access.</summary>
        USER = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    /// <summary>
    /// ZTE-Access-Type attribute values (Type 50).
    /// </summary>
    public enum ZTE_ACCESS_TYPE
    {
        /// <summary>PPPoE access.</summary>
        PPPOE = 0,

        /// <summary>PPPoA access.</summary>
        PPPOA = 1,

        /// <summary>IPoE/DHCP access.</summary>
        IPOE = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing ZTE Corporation
    /// (IANA PEN 3902) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.zte</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ZTE's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3902</c>.
    /// </para>
    /// <para>
    /// These attributes are used by ZTE OLT/ONU, BRAS/BNG, mobile core, and
    /// broadband access platforms for RADIUS-based DNS server provisioning (IPv4
    /// and IPv6), VRF/context naming, L2TP tunnel configuration, PPPoE URL
    /// redirection, TCP MSS adjustment, upstream/downstream rate control (SCR,
    /// PCR, burst, CIR, PIR, CBS, PBS), QoS profile assignment, access type
    /// identification, VPN ID assignment, switch privilege level assignment,
    /// access domain configuration, CoA type control, VLAN and inner VLAN (QinQ)
    /// assignment, subscriber identification, IPv6 prefix/address pool assignment,
    /// framed pool assignment, NAT service profile and public address provisioning,
    /// 64-bit accounting counters, and DHCP option 60 handling.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ZteAttributes.ClientDnsPri(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(ZteAttributes.ClientDnsSec(IPAddress.Parse("8.8.4.4")));
    /// packet.SetAttribute(ZteAttributes.RateCtrlScrDown(100000));
    /// packet.SetAttribute(ZteAttributes.RateCtrlScrUp(50000));
    /// packet.SetAttribute(ZteAttributes.VlanId(200));
    /// packet.SetAttribute(ZteAttributes.ContextName("vrf-residential"));
    /// packet.SetAttribute(ZteAttributes.FramedPool("pool-residential"));
    /// </code>
    /// </remarks>
    public static class ZteAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for ZTE Corporation.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3902;

        #region Integer Attributes

        /// <summary>Creates a ZTE-Tunnel-Max-Sessions attribute (Type 5).</summary>
        /// <param name="value">The maximum tunnel sessions.</param>
        public static VendorSpecificAttributes TunnelMaxSessions(int value) => CreateInteger(ZteAttributeType.TUNNEL_MAX_SESSIONS, value);

        /// <summary>Creates a ZTE-Tunnel-Max-Tunnels attribute (Type 6).</summary>
        /// <param name="value">The maximum tunnels.</param>
        public static VendorSpecificAttributes TunnelMaxTunnels(int value) => CreateInteger(ZteAttributeType.TUNNEL_MAX_TUNNELS, value);

        /// <summary>Creates a ZTE-Tunnel-Window attribute (Type 7).</summary>
        /// <param name="value">The tunnel receive window size.</param>
        public static VendorSpecificAttributes TunnelWindow(int value) => CreateInteger(ZteAttributeType.TUNNEL_WINDOW, value);

        /// <summary>Creates a ZTE-Tunnel-Retransmit attribute (Type 8).</summary>
        /// <param name="value">The tunnel retransmit count.</param>
        public static VendorSpecificAttributes TunnelRetransmit(int value) => CreateInteger(ZteAttributeType.TUNNEL_RETRANSMIT, value);

        /// <summary>Creates a ZTE-Tunnel-Cmd-Timeout attribute (Type 9).</summary>
        /// <param name="value">The tunnel command timeout in seconds.</param>
        public static VendorSpecificAttributes TunnelCmdTimeout(int value) => CreateInteger(ZteAttributeType.TUNNEL_CMD_TIMEOUT, value);

        /// <summary>Creates a ZTE-TCP-MSS-Adjust attribute (Type 11).</summary>
        /// <param name="value">The TCP MSS adjustment value.</param>
        public static VendorSpecificAttributes TcpMssAdjust(int value) => CreateInteger(ZteAttributeType.TCP_MSS_ADJUST, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-Scr-Up attribute (Type 30).</summary>
        /// <param name="value">The upstream sustained cell rate in Kbps.</param>
        public static VendorSpecificAttributes RateCtrlScrUp(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_SCR_UP, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-Scr-Down attribute (Type 31).</summary>
        /// <param name="value">The downstream sustained cell rate in Kbps.</param>
        public static VendorSpecificAttributes RateCtrlScrDown(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_SCR_DOWN, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-Burst-Up attribute (Type 32).</summary>
        /// <param name="value">The upstream burst size in bytes.</param>
        public static VendorSpecificAttributes RateCtrlBurstUp(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_BURST_UP, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-Burst-Down attribute (Type 33).</summary>
        /// <param name="value">The downstream burst size in bytes.</param>
        public static VendorSpecificAttributes RateCtrlBurstDown(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_BURST_DOWN, value);

        /// <summary>Creates a ZTE-Police-Cir attribute (Type 34).</summary>
        /// <param name="value">The committed information rate in Kbps.</param>
        public static VendorSpecificAttributes PoliceCir(int value) => CreateInteger(ZteAttributeType.POLICE_CIR, value);

        /// <summary>Creates a ZTE-Police-Pir attribute (Type 35).</summary>
        /// <param name="value">The peak information rate in Kbps.</param>
        public static VendorSpecificAttributes PolicePir(int value) => CreateInteger(ZteAttributeType.POLICE_PIR, value);

        /// <summary>Creates a ZTE-Police-Cbs attribute (Type 36).</summary>
        /// <param name="value">The committed burst size in bytes.</param>
        public static VendorSpecificAttributes PoliceCbs(int value) => CreateInteger(ZteAttributeType.POLICE_CBS, value);

        /// <summary>Creates a ZTE-Police-Pbs attribute (Type 37).</summary>
        /// <param name="value">The peak burst size in bytes.</param>
        public static VendorSpecificAttributes PolicePbs(int value) => CreateInteger(ZteAttributeType.POLICE_PBS, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-Pcr-Up attribute (Type 38).</summary>
        /// <param name="value">The upstream peak cell rate in Kbps.</param>
        public static VendorSpecificAttributes RateCtrlPcrUp(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_PCR_UP, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-Pcr-Down attribute (Type 39).</summary>
        /// <param name="value">The downstream peak cell rate in Kbps.</param>
        public static VendorSpecificAttributes RateCtrlPcrDown(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_PCR_DOWN, value);

        /// <summary>Creates a ZTE-Access-Type attribute (Type 50).</summary>
        /// <param name="value">The access type. See <see cref="ZTE_ACCESS_TYPE"/>.</param>
        public static VendorSpecificAttributes AccessType(ZTE_ACCESS_TYPE value) => CreateInteger(ZteAttributeType.ACCESS_TYPE, (int)value);

        /// <summary>Creates a ZTE-Rate-Bust-DPIR attribute (Type 55).</summary>
        /// <param name="value">The downstream PIR burst.</param>
        public static VendorSpecificAttributes RateBustDpir(int value) => CreateInteger(ZteAttributeType.RATE_BUST_DPIR, value);

        /// <summary>Creates a ZTE-Rate-Bust-UPIR attribute (Type 56).</summary>
        /// <param name="value">The upstream PIR burst.</param>
        public static VendorSpecificAttributes RateBustUpir(int value) => CreateInteger(ZteAttributeType.RATE_BUST_UPIR, value);

        /// <summary>Creates a ZTE-Rate-Bust-DCIR attribute (Type 57).</summary>
        /// <param name="value">The downstream CIR burst.</param>
        public static VendorSpecificAttributes RateBustDcir(int value) => CreateInteger(ZteAttributeType.RATE_BUST_DCIR, value);

        /// <summary>Creates a ZTE-Rate-Bust-UCIR attribute (Type 58).</summary>
        /// <param name="value">The upstream CIR burst.</param>
        public static VendorSpecificAttributes RateBustUcir(int value) => CreateInteger(ZteAttributeType.RATE_BUST_UCIR, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-SCR-Up-v2 attribute (Type 80).</summary>
        /// <param name="value">The upstream SCR v2 in Kbps.</param>
        public static VendorSpecificAttributes RateCtrlScrUpV2(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_SCR_UP_V2, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-SCR-Down-v2 attribute (Type 81).</summary>
        /// <param name="value">The downstream SCR v2 in Kbps.</param>
        public static VendorSpecificAttributes RateCtrlScrDownV2(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_SCR_DOWN_V2, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-PCR-Up-v2 attribute (Type 82).</summary>
        /// <param name="value">The upstream PCR v2 in Kbps.</param>
        public static VendorSpecificAttributes RateCtrlPcrUpV2(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_PCR_UP_V2, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-PCR-Down-v2 attribute (Type 83).</summary>
        /// <param name="value">The downstream PCR v2 in Kbps.</param>
        public static VendorSpecificAttributes RateCtrlPcrDownV2(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_PCR_DOWN_V2, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-Burst-Up-v2 attribute (Type 84).</summary>
        /// <param name="value">The upstream burst v2 in bytes.</param>
        public static VendorSpecificAttributes RateCtrlBurstUpV2(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_BURST_UP_V2, value);

        /// <summary>Creates a ZTE-Rate-Ctrl-Burst-Down-v2 attribute (Type 85).</summary>
        /// <param name="value">The downstream burst v2 in bytes.</param>
        public static VendorSpecificAttributes RateCtrlBurstDownV2(int value) => CreateInteger(ZteAttributeType.RATE_CTRL_BURST_DOWN_V2, value);

        /// <summary>Creates a ZTE-PPP-Sservice-Type attribute (Type 101).</summary>
        /// <param name="value">The PPP service type.</param>
        public static VendorSpecificAttributes PppSserviceType(int value) => CreateInteger(ZteAttributeType.PPP_SSERVICE_TYPE, value);

        /// <summary>Creates a ZTE-SW-Privilege attribute (Type 102).</summary>
        /// <param name="value">The switch privilege level. See <see cref="ZTE_SW_PRIVILEGE"/>.</param>
        public static VendorSpecificAttributes SwPrivilege(ZTE_SW_PRIVILEGE value) => CreateInteger(ZteAttributeType.SW_PRIVILEGE, (int)value);

        /// <summary>Creates a ZTE-CoA-Type attribute (Type 152).</summary>
        /// <param name="value">The Change of Authorization type.</param>
        public static VendorSpecificAttributes CoaType(int value) => CreateInteger(ZteAttributeType.COA_TYPE, value);

        /// <summary>Creates a ZTE-VLAN-ID attribute (Type 153).</summary>
        /// <param name="value">The VLAN identifier.</param>
        public static VendorSpecificAttributes VlanId(int value) => CreateInteger(ZteAttributeType.VLAN_ID, value);

        /// <summary>Creates a ZTE-Inner-VLAN-ID attribute (Type 254).</summary>
        /// <param name="value">The inner (QinQ) VLAN identifier.</param>
        public static VendorSpecificAttributes InnerVlanId(int value) => CreateInteger(ZteAttributeType.INNER_VLAN_ID, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a ZTE-Context-Name attribute (Type 4).</summary>
        /// <param name="value">The VRF/context name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ContextName(string value) => CreateString(ZteAttributeType.CONTEXT_NAME, value);

        /// <summary>Creates a ZTE-PPPOE-URL attribute (Type 10).</summary>
        /// <param name="value">The PPPoE URL for redirection. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PppoeUrl(string value) => CreateString(ZteAttributeType.PPPOE_URL, value);

        /// <summary>Creates a ZTE-QoS-Profile-Up attribute (Type 40).</summary>
        /// <param name="value">The upstream QoS profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfileUp(string value) => CreateString(ZteAttributeType.QOS_PROFILE_UP, value);

        /// <summary>Creates a ZTE-QoS-Profile-Down attribute (Type 41).</summary>
        /// <param name="value">The downstream QoS profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfileDown(string value) => CreateString(ZteAttributeType.QOS_PROFILE_DOWN, value);

        /// <summary>Creates a ZTE-VPN-ID attribute (Type 68).</summary>
        /// <param name="value">The VPN identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnId(string value) => CreateString(ZteAttributeType.VPN_ID, value);

        /// <summary>Creates a ZTE-DHCP-Option60 attribute (Type 90).</summary>
        /// <param name="value">The DHCP Option 60 vendor class identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpOption60(string value) => CreateString(ZteAttributeType.DHCP_OPTION60, value);

        /// <summary>Creates a ZTE-Access-Domain attribute (Type 151).</summary>
        /// <param name="value">The access domain name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessDomain(string value) => CreateString(ZteAttributeType.ACCESS_DOMAIN, value);

        /// <summary>Creates a ZTE-Subscriber-ID attribute (Type 160).</summary>
        /// <param name="value">The subscriber identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberId(string value) => CreateString(ZteAttributeType.SUBSCRIBER_ID, value);

        /// <summary>Creates a ZTE-IPv6-DNS-Pri attribute (Type 161).</summary>
        /// <param name="value">The primary IPv6 DNS server. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ipv6DnsPri(string value) => CreateString(ZteAttributeType.IPV6_DNS_PRI, value);

        /// <summary>Creates a ZTE-IPv6-DNS-Sec attribute (Type 162).</summary>
        /// <param name="value">The secondary IPv6 DNS server. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ipv6DnsSec(string value) => CreateString(ZteAttributeType.IPV6_DNS_SEC, value);

        /// <summary>Creates a ZTE-Delegated-IPv6-Prefix-Pool attribute (Type 163).</summary>
        /// <param name="value">The delegated IPv6 prefix pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DelegatedIpv6PrefixPool(string value) => CreateString(ZteAttributeType.DELEGATED_IPV6_PREFIX_POOL, value);

        /// <summary>Creates a ZTE-IPv6-Address-Pool attribute (Type 164).</summary>
        /// <param name="value">The IPv6 address pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ipv6AddressPool(string value) => CreateString(ZteAttributeType.IPV6_ADDRESS_POOL, value);

        /// <summary>Creates a ZTE-Framed-Pool attribute (Type 170).</summary>
        /// <param name="value">The framed IP address pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedPool(string value) => CreateString(ZteAttributeType.FRAMED_POOL, value);

        /// <summary>Creates a ZTE-NAT-Service-Profile attribute (Type 180).</summary>
        /// <param name="value">The NAT service profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatServiceProfile(string value) => CreateString(ZteAttributeType.NAT_SERVICE_PROFILE, value);

        /// <summary>Creates a ZTE-Acct-Input-Packets-64 attribute (Type 200).</summary>
        /// <param name="value">The 64-bit input packets counter. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctInputPackets64(string value) => CreateString(ZteAttributeType.ACCT_INPUT_PACKETS_64, value);

        /// <summary>Creates a ZTE-Acct-Output-Packets-64 attribute (Type 201).</summary>
        /// <param name="value">The 64-bit output packets counter. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctOutputPackets64(string value) => CreateString(ZteAttributeType.ACCT_OUTPUT_PACKETS_64, value);

        /// <summary>Creates a ZTE-Acct-Input-Octets-64 attribute (Type 202).</summary>
        /// <param name="value">The 64-bit input octets counter. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctInputOctets64(string value) => CreateString(ZteAttributeType.ACCT_INPUT_OCTETS_64, value);

        /// <summary>Creates a ZTE-Acct-Output-Octets-64 attribute (Type 203).</summary>
        /// <param name="value">The 64-bit output octets counter. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctOutputOctets64(string value) => CreateString(ZteAttributeType.ACCT_OUTPUT_OCTETS_64, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a ZTE-Client-DNS-Pri attribute (Type 1).</summary>
        /// <param name="value">The primary DNS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientDnsPri(IPAddress value) => CreateIpv4(ZteAttributeType.CLIENT_DNS_PRI, value);

        /// <summary>Creates a ZTE-Client-DNS-Sec attribute (Type 2).</summary>
        /// <param name="value">The secondary DNS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientDnsSec(IPAddress value) => CreateIpv4(ZteAttributeType.CLIENT_DNS_SEC, value);

        /// <summary>Creates a ZTE-NAT-Public-Address attribute (Type 181).</summary>
        /// <param name="value">The NAT public address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes NatPublicAddress(IPAddress value) => CreateIpv4(ZteAttributeType.NAT_PUBLIC_ADDRESS, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(ZteAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(ZteAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(ZteAttributeType type, IPAddress value)
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