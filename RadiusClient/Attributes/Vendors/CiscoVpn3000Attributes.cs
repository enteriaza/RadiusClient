using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Cisco VPN 3000 / Altiga (IANA PEN 3076) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.cisco.vpn3000</c>.
    /// </summary>
    /// <remarks>
    /// The Cisco VPN 3000 concentrator series (originally Altiga Networks) uses
    /// vendor ID 3076. This dictionary covers legacy VPN 3000 attributes that
    /// overlap with <c>dictionary.cisco.asa</c> but may include additional or
    /// differently named entries from earlier firmware revisions.
    /// </remarks>
    public enum CiscoVpn3000AttributeType : byte
    {
        /// <summary>CVPN3000-Access-Hours (Type 1). String. Access hours definition.</summary>
        ACCESS_HOURS = 1,

        /// <summary>CVPN3000-Simultaneous-Logins (Type 2). Integer. Maximum simultaneous logins.</summary>
        SIMULTANEOUS_LOGINS = 2,

        /// <summary>CVPN3000-Min-Password-Length (Type 3). Integer. Minimum password length.</summary>
        MIN_PASSWORD_LENGTH = 3,

        /// <summary>CVPN3000-Allow-Alpha-Only-Passwords (Type 4). Integer. Allow alpha-only passwords flag.</summary>
        ALLOW_ALPHA_ONLY_PASSWORDS = 4,

        /// <summary>CVPN3000-Primary-DNS (Type 5). IP address. Primary DNS server address.</summary>
        PRIMARY_DNS = 5,

        /// <summary>CVPN3000-Secondary-DNS (Type 6). IP address. Secondary DNS server address.</summary>
        SECONDARY_DNS = 6,

        /// <summary>CVPN3000-Primary-WINS (Type 7). IP address. Primary WINS server address.</summary>
        PRIMARY_WINS = 7,

        /// <summary>CVPN3000-Secondary-WINS (Type 8). IP address. Secondary WINS server address.</summary>
        SECONDARY_WINS = 8,

        /// <summary>CVPN3000-SEP-Card-Assignment (Type 9). Integer. SEP card assignment.</summary>
        SEP_CARD_ASSIGNMENT = 9,

        /// <summary>CVPN3000-Tunneling-Protocols (Type 11). Integer. Tunnelling protocols bitmask.</summary>
        TUNNELING_PROTOCOLS = 11,

        /// <summary>CVPN3000-IPSec-Sec-Association (Type 12). String. IPsec security association name.</summary>
        IPSEC_SEC_ASSOCIATION = 12,

        /// <summary>CVPN3000-IPSec-Authentication (Type 13). Integer. IPsec authentication type.</summary>
        IPSEC_AUTHENTICATION = 13,

        /// <summary>CVPN3000-Banner1 (Type 15). String. Login banner part 1.</summary>
        BANNER1 = 15,

        /// <summary>CVPN3000-IPSec-Allow-Passwd-Store (Type 16). Integer. IPsec allow password store flag.</summary>
        IPSEC_ALLOW_PASSWD_STORE = 16,

        /// <summary>CVPN3000-Use-Client-Address (Type 17). Integer. Use client address flag.</summary>
        USE_CLIENT_ADDRESS = 17,

        /// <summary>CVPN3000-PPTP-Encryption (Type 20). Integer. PPTP encryption bitmask.</summary>
        PPTP_ENCRYPTION = 20,

        /// <summary>CVPN3000-L2TP-Encryption (Type 21). Integer. L2TP encryption bitmask.</summary>
        L2TP_ENCRYPTION = 21,

        /// <summary>CVPN3000-IPSec-Banner2 (Type 36). String. IPsec banner part 2.</summary>
        IPSEC_BANNER2 = 36,

        /// <summary>CVPN3000-Group-Policy (Type 25). String. Group policy name.</summary>
        GROUP_POLICY = 25,

        /// <summary>CVPN3000-IPSec-Split-Tunnel-List (Type 27). String. IPsec split tunnel ACL name.</summary>
        IPSEC_SPLIT_TUNNEL_LIST = 27,

        /// <summary>CVPN3000-IPSec-Default-Domain (Type 28). String. IPsec default domain name.</summary>
        IPSEC_DEFAULT_DOMAIN = 28,

        /// <summary>CVPN3000-IPSec-Split-DNS-Names (Type 29). String. IPsec split DNS domain names.</summary>
        IPSEC_SPLIT_DNS_NAMES = 29,

        /// <summary>CVPN3000-IPSec-Tunnel-Type (Type 30). Integer. IPsec tunnel type.</summary>
        IPSEC_TUNNEL_TYPE = 30,

        /// <summary>CVPN3000-IPSec-Mode-Config (Type 31). Integer. IPsec mode config flag.</summary>
        IPSEC_MODE_CONFIG = 31,

        /// <summary>CVPN3000-Auth-On-Rekey (Type 42). Integer. Re-authenticate on rekey flag.</summary>
        AUTH_ON_REKEY = 42,

        /// <summary>CVPN3000-Required-Client-Firewall-Vendor-Code (Type 45). Integer. Required client firewall vendor code.</summary>
        REQUIRED_CLIENT_FIREWALL_VENDOR_CODE = 45,

        /// <summary>CVPN3000-Required-Client-Firewall-Product-Code (Type 46). Integer. Required client firewall product code.</summary>
        REQUIRED_CLIENT_FIREWALL_PRODUCT_CODE = 46,

        /// <summary>CVPN3000-Required-Client-Firewall-Description (Type 47). String. Required client firewall description.</summary>
        REQUIRED_CLIENT_FIREWALL_DESCRIPTION = 47,

        /// <summary>CVPN3000-Require-HW-Client-Auth (Type 48). Integer. Require hardware client auth flag.</summary>
        REQUIRE_HW_CLIENT_AUTH = 48,

        /// <summary>CVPN3000-Required-Individual-User-Auth (Type 49). Integer. Require individual user auth flag.</summary>
        REQUIRED_INDIVIDUAL_USER_AUTH = 49,

        /// <summary>CVPN3000-Authenticated-User-Idle-Timeout (Type 50). Integer. Idle timeout in minutes.</summary>
        AUTHENTICATED_USER_IDLE_TIMEOUT = 50,

        /// <summary>CVPN3000-Cisco-IP-Phone-Bypass (Type 51). Integer. Cisco IP phone bypass flag.</summary>
        CISCO_IP_PHONE_BYPASS = 51,

        /// <summary>CVPN3000-User-Auth-Server-Name (Type 52). String. User auth server name.</summary>
        USER_AUTH_SERVER_NAME = 52,

        /// <summary>CVPN3000-User-Auth-Server-Port (Type 53). Integer. User auth server port.</summary>
        USER_AUTH_SERVER_PORT = 53,

        /// <summary>CVPN3000-User-Auth-Server-Secret (Type 54). String. User auth server secret.</summary>
        USER_AUTH_SERVER_SECRET = 54,

        /// <summary>CVPN3000-IPSec-Split-Tunneling-Policy (Type 55). Integer. IPsec split tunnelling policy.</summary>
        IPSEC_SPLIT_TUNNELING_POLICY = 55,

        /// <summary>CVPN3000-IPSec-Required-Client-Firewall-Capability (Type 56). Integer. Required client firewall capability.</summary>
        IPSEC_REQUIRED_CLIENT_FIREWALL_CAPABILITY = 56,

        /// <summary>CVPN3000-IPSec-Client-Firewall-Filter-Name (Type 57). String. Client firewall filter name.</summary>
        IPSEC_CLIENT_FIREWALL_FILTER_NAME = 57,

        /// <summary>CVPN3000-IPSec-Client-Firewall-Filter-Optional (Type 58). Integer. Client firewall filter optional flag.</summary>
        IPSEC_CLIENT_FIREWALL_FILTER_OPTIONAL = 58,

        /// <summary>CVPN3000-IPSec-Backup-Servers (Type 59). Integer. IPsec backup servers option.</summary>
        IPSEC_BACKUP_SERVERS = 59,

        /// <summary>CVPN3000-IPSec-Backup-Server-List (Type 60). String. IPsec backup server list.</summary>
        IPSEC_BACKUP_SERVER_LIST = 60,

        /// <summary>CVPN3000-DHCP-Network-Scope (Type 61). IP address. DHCP network scope address.</summary>
        DHCP_NETWORK_SCOPE = 61,

        /// <summary>CVPN3000-Intercept-DHCP-Configure-Msg (Type 62). Integer. Intercept DHCP configure message flag.</summary>
        INTERCEPT_DHCP_CONFIGURE_MSG = 62,

        /// <summary>CVPN3000-MS-Client-Subnet-Mask (Type 63). IP address. MS client subnet mask.</summary>
        MS_CLIENT_SUBNET_MASK = 63,

        /// <summary>CVPN3000-Allow-Network-Extension-Mode (Type 64). Integer. Allow network extension mode flag.</summary>
        ALLOW_NETWORK_EXTENSION_MODE = 64,

        /// <summary>CVPN3000-Authorization-Type (Type 65). Integer. Authorization type.</summary>
        AUTHORIZATION_TYPE = 65,

        /// <summary>CVPN3000-Authorization-Required (Type 66). Integer. Authorization required flag.</summary>
        AUTHORIZATION_REQUIRED = 66,

        /// <summary>CVPN3000-Authorization-DN-Field (Type 67). String. Authorization DN field.</summary>
        AUTHORIZATION_DN_FIELD = 67,

        /// <summary>CVPN3000-IKE-Keepalive-Confidence-Interval (Type 68). Integer. IKE keepalive confidence interval in seconds.</summary>
        IKE_KEEPALIVE_CONFIDENCE_INTERVAL = 68,

        /// <summary>CVPN3000-WebVPN-Content-Filter-Parameters (Type 69). Integer. WebVPN content filter parameters.</summary>
        WEBVPN_CONTENT_FILTER_PARAMETERS = 69,

        /// <summary>CVPN3000-WebVPN-Enable-Functions (Type 70). Integer. WebVPN enable functions bitmask.</summary>
        WEBVPN_ENABLE_FUNCTIONS = 70,

        /// <summary>CVPN3000-WebVPN-URL-List (Type 71). String. WebVPN URL list name.</summary>
        WEBVPN_URL_LIST = 71,

        /// <summary>CVPN3000-WebVPN-Port-Forwarding-List (Type 72). String. WebVPN port forwarding list name.</summary>
        WEBVPN_PORT_FORWARDING_LIST = 72,

        /// <summary>CVPN3000-WebVPN-Access-List (Type 73). String. WebVPN access list name.</summary>
        WEBVPN_ACCESS_LIST = 73,

        /// <summary>CVPN3000-Cisco-LEAP-Bypass (Type 75). Integer. Cisco LEAP bypass flag.</summary>
        CISCO_LEAP_BYPASS = 75,

        /// <summary>CVPN3000-WebVPN-Homepage (Type 76). String. WebVPN homepage URL.</summary>
        WEBVPN_HOMEPAGE = 76,

        /// <summary>CVPN3000-Client-Type-Version-Limiting (Type 77). String. Client type version limiting string.</summary>
        CLIENT_TYPE_VERSION_LIMITING = 77,

        /// <summary>CVPN3000-WebVPN-Port-Forwarding-Name (Type 79). String. WebVPN port forwarding name.</summary>
        WEBVPN_PORT_FORWARDING_NAME = 79,

        /// <summary>CVPN3000-IE-Proxy-Server (Type 80). String. IE proxy server address.</summary>
        IE_PROXY_SERVER = 80,

        /// <summary>CVPN3000-IE-Proxy-Server-Policy (Type 81). Integer. IE proxy server policy.</summary>
        IE_PROXY_SERVER_POLICY = 81,

        /// <summary>CVPN3000-IE-Proxy-Exception-List (Type 82). String. IE proxy exception list.</summary>
        IE_PROXY_EXCEPTION_LIST = 82,

        /// <summary>CVPN3000-IE-Proxy-Bypass-Local (Type 83). Integer. IE proxy bypass local flag.</summary>
        IE_PROXY_BYPASS_LOCAL = 83,

        /// <summary>CVPN3000-IKE-Keepalive-Retry-Interval (Type 84). Integer. IKE keepalive retry interval in seconds.</summary>
        IKE_KEEPALIVE_RETRY_INTERVAL = 84,

        /// <summary>CVPN3000-Tunnel-Group-Lock (Type 85). String. Tunnel group lock name.</summary>
        TUNNEL_GROUP_LOCK = 85,

        /// <summary>CVPN3000-Access-List-Inbound (Type 86). String. Inbound access list name.</summary>
        ACCESS_LIST_INBOUND = 86,

        /// <summary>CVPN3000-Access-List-Outbound (Type 87). String. Outbound access list name.</summary>
        ACCESS_LIST_OUTBOUND = 87,

        /// <summary>CVPN3000-Perfect-Forward-Secrecy-Enable (Type 88). Integer. PFS enable flag.</summary>
        PERFECT_FORWARD_SECRECY_ENABLE = 88,

        /// <summary>CVPN3000-NAC-Enable (Type 89). Integer. NAC enable flag.</summary>
        NAC_ENABLE = 89,

        /// <summary>CVPN3000-NAC-Status-Query-Timer (Type 90). Integer. NAC status query timer in seconds.</summary>
        NAC_STATUS_QUERY_TIMER = 90,

        /// <summary>CVPN3000-NAC-Revalidation-Timer (Type 91). Integer. NAC revalidation timer in seconds.</summary>
        NAC_REVALIDATION_TIMER = 91,

        /// <summary>CVPN3000-NAC-Default-ACL (Type 92). String. NAC default ACL name.</summary>
        NAC_DEFAULT_ACL = 92,

        /// <summary>CVPN3000-Strip-Realm (Type 135). Integer. Strip realm flag.</summary>
        STRIP_REALM = 135
    }

    /// <summary>
    /// CVPN3000-Tunneling-Protocols attribute values (Type 11). Bitmask.
    /// </summary>
    [Flags]
    public enum CVPN3000_TUNNELING_PROTOCOLS
    {
        /// <summary>PPTP tunnelling.</summary>
        PPTP = 1,

        /// <summary>L2TP tunnelling.</summary>
        L2TP = 2,

        /// <summary>IPsec tunnelling.</summary>
        IPSEC = 4,

        /// <summary>L2TP/IPsec tunnelling.</summary>
        L2TP_IPSEC = 8,

        /// <summary>WebVPN / SSL VPN.</summary>
        WEBVPN = 16
    }

    /// <summary>
    /// CVPN3000-IPSec-Authentication attribute values (Type 13).
    /// </summary>
    public enum CVPN3000_IPSEC_AUTHENTICATION
    {
        /// <summary>No authentication.</summary>
        NONE = 0,

        /// <summary>RADIUS authentication.</summary>
        RADIUS = 1,

        /// <summary>LDAP authentication.</summary>
        LDAP = 2,

        /// <summary>NT Domain authentication.</summary>
        NT_DOMAIN = 3,

        /// <summary>SDI/RSA SecurID authentication.</summary>
        SDI = 4,

        /// <summary>Internal authentication.</summary>
        INTERNAL = 5,

        /// <summary>Kerberos/Active Directory authentication.</summary>
        KERBEROS = 7
    }

    /// <summary>
    /// CVPN3000-IPSec-Split-Tunneling-Policy attribute values (Type 55).
    /// </summary>
    public enum CVPN3000_IPSEC_SPLIT_TUNNELING_POLICY
    {
        /// <summary>Tunnel all traffic.</summary>
        TUNNEL_ALL = 0,

        /// <summary>Only tunnel traffic for specified networks.</summary>
        SPLIT_INCLUDE = 1,

        /// <summary>Tunnel all except specified networks.</summary>
        SPLIT_EXCLUDE = 2
    }

    /// <summary>
    /// CVPN3000-IPSec-Tunnel-Type attribute values (Type 30).
    /// </summary>
    public enum CVPN3000_IPSEC_TUNNEL_TYPE
    {
        /// <summary>LAN-to-LAN tunnel.</summary>
        LAN_TO_LAN = 1,

        /// <summary>Remote access tunnel.</summary>
        REMOTE_ACCESS = 2
    }

    /// <summary>
    /// CVPN3000-IPSec-Mode-Config attribute values (Type 31).
    /// </summary>
    public enum CVPN3000_IPSEC_MODE_CONFIG
    {
        /// <summary>Mode config disabled.</summary>
        DISABLED = 0,

        /// <summary>Mode config enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// CVPN3000-IE-Proxy-Server-Policy attribute values (Type 81).
    /// </summary>
    public enum CVPN3000_IE_PROXY_SERVER_POLICY
    {
        /// <summary>No proxy modification.</summary>
        NO_MODIFY = 0,

        /// <summary>No proxy.</summary>
        NO_PROXY = 1,

        /// <summary>Auto detect proxy.</summary>
        AUTO_DETECT = 2,

        /// <summary>Use concentrator setting.</summary>
        USE_CONCENTRATOR = 3
    }

    /// <summary>
    /// CVPN3000-Strip-Realm attribute values (Type 135).
    /// </summary>
    public enum CVPN3000_STRIP_REALM
    {
        /// <summary>Do not strip realm.</summary>
        NO = 0,

        /// <summary>Strip realm.</summary>
        YES = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Cisco VPN 3000 / Altiga
    /// (IANA PEN 3076) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.cisco.vpn3000</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Cisco VPN 3000's vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3076</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Cisco VPN 3000 series concentrators (originally
    /// Altiga Networks) for remote access VPN configuration including IPsec, PPTP,
    /// L2TP, WebVPN, split tunnelling, DNS/WINS assignment, group policies,
    /// banner messages, NAC, client firewall requirements, IE proxy settings,
    /// and access list enforcement.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CiscoVpn3000Attributes.GroupPolicy("RemoteAccessPolicy"));
    /// packet.SetAttribute(CiscoVpn3000Attributes.TunnelingProtocols(
    ///     CVPN3000_TUNNELING_PROTOCOLS.IPSEC | CVPN3000_TUNNELING_PROTOCOLS.L2TP));
    /// packet.SetAttribute(CiscoVpn3000Attributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(CiscoVpn3000Attributes.SimultaneousLogins(3));
    /// </code>
    /// </remarks>
    public static class CiscoVpn3000Attributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Cisco VPN 3000 (Altiga Networks).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3076;

        #region Integer Attributes

        /// <summary>Creates a CVPN3000-Simultaneous-Logins attribute (Type 2).</summary>
        public static VendorSpecificAttributes SimultaneousLogins(int value) => CreateInteger(CiscoVpn3000AttributeType.SIMULTANEOUS_LOGINS, value);

        /// <summary>Creates a CVPN3000-Min-Password-Length attribute (Type 3).</summary>
        public static VendorSpecificAttributes MinPasswordLength(int value) => CreateInteger(CiscoVpn3000AttributeType.MIN_PASSWORD_LENGTH, value);

        /// <summary>Creates a CVPN3000-Allow-Alpha-Only-Passwords attribute (Type 4).</summary>
        public static VendorSpecificAttributes AllowAlphaOnlyPasswords(int value) => CreateInteger(CiscoVpn3000AttributeType.ALLOW_ALPHA_ONLY_PASSWORDS, value);

        /// <summary>Creates a CVPN3000-SEP-Card-Assignment attribute (Type 9).</summary>
        public static VendorSpecificAttributes SepCardAssignment(int value) => CreateInteger(CiscoVpn3000AttributeType.SEP_CARD_ASSIGNMENT, value);

        /// <summary>
        /// Creates a CVPN3000-Tunneling-Protocols attribute (Type 11).
        /// </summary>
        /// <param name="value">The tunnelling protocols bitmask. See <see cref="CVPN3000_TUNNELING_PROTOCOLS"/>.</param>
        public static VendorSpecificAttributes TunnelingProtocols(CVPN3000_TUNNELING_PROTOCOLS value) => CreateInteger(CiscoVpn3000AttributeType.TUNNELING_PROTOCOLS, (int)value);

        /// <summary>
        /// Creates a CVPN3000-IPSec-Authentication attribute (Type 13).
        /// </summary>
        /// <param name="value">The IPsec authentication type. See <see cref="CVPN3000_IPSEC_AUTHENTICATION"/>.</param>
        public static VendorSpecificAttributes IpsecAuthentication(CVPN3000_IPSEC_AUTHENTICATION value) => CreateInteger(CiscoVpn3000AttributeType.IPSEC_AUTHENTICATION, (int)value);

        /// <summary>Creates a CVPN3000-IPSec-Allow-Passwd-Store attribute (Type 16).</summary>
        public static VendorSpecificAttributes IpsecAllowPasswdStore(int value) => CreateInteger(CiscoVpn3000AttributeType.IPSEC_ALLOW_PASSWD_STORE, value);

        /// <summary>Creates a CVPN3000-Use-Client-Address attribute (Type 17).</summary>
        public static VendorSpecificAttributes UseClientAddress(int value) => CreateInteger(CiscoVpn3000AttributeType.USE_CLIENT_ADDRESS, value);

        /// <summary>Creates a CVPN3000-PPTP-Encryption attribute (Type 20).</summary>
        public static VendorSpecificAttributes PptpEncryption(int value) => CreateInteger(CiscoVpn3000AttributeType.PPTP_ENCRYPTION, value);

        /// <summary>Creates a CVPN3000-L2TP-Encryption attribute (Type 21).</summary>
        public static VendorSpecificAttributes L2tpEncryption(int value) => CreateInteger(CiscoVpn3000AttributeType.L2TP_ENCRYPTION, value);

        /// <summary>
        /// Creates a CVPN3000-IPSec-Tunnel-Type attribute (Type 30).
        /// </summary>
        /// <param name="value">The IPsec tunnel type. See <see cref="CVPN3000_IPSEC_TUNNEL_TYPE"/>.</param>
        public static VendorSpecificAttributes IpsecTunnelType(CVPN3000_IPSEC_TUNNEL_TYPE value) => CreateInteger(CiscoVpn3000AttributeType.IPSEC_TUNNEL_TYPE, (int)value);

        /// <summary>
        /// Creates a CVPN3000-IPSec-Mode-Config attribute (Type 31).
        /// </summary>
        /// <param name="value">The IPsec mode config setting. See <see cref="CVPN3000_IPSEC_MODE_CONFIG"/>.</param>
        public static VendorSpecificAttributes IpsecModeConfig(CVPN3000_IPSEC_MODE_CONFIG value) => CreateInteger(CiscoVpn3000AttributeType.IPSEC_MODE_CONFIG, (int)value);

        /// <summary>Creates a CVPN3000-Auth-On-Rekey attribute (Type 42).</summary>
        public static VendorSpecificAttributes AuthOnRekey(int value) => CreateInteger(CiscoVpn3000AttributeType.AUTH_ON_REKEY, value);

        /// <summary>Creates a CVPN3000-Required-Client-Firewall-Vendor-Code attribute (Type 45).</summary>
        public static VendorSpecificAttributes RequiredClientFirewallVendorCode(int value) => CreateInteger(CiscoVpn3000AttributeType.REQUIRED_CLIENT_FIREWALL_VENDOR_CODE, value);

        /// <summary>Creates a CVPN3000-Required-Client-Firewall-Product-Code attribute (Type 46).</summary>
        public static VendorSpecificAttributes RequiredClientFirewallProductCode(int value) => CreateInteger(CiscoVpn3000AttributeType.REQUIRED_CLIENT_FIREWALL_PRODUCT_CODE, value);

        /// <summary>Creates a CVPN3000-Require-HW-Client-Auth attribute (Type 48).</summary>
        public static VendorSpecificAttributes RequireHwClientAuth(int value) => CreateInteger(CiscoVpn3000AttributeType.REQUIRE_HW_CLIENT_AUTH, value);

        /// <summary>Creates a CVPN3000-Required-Individual-User-Auth attribute (Type 49).</summary>
        public static VendorSpecificAttributes RequiredIndividualUserAuth(int value) => CreateInteger(CiscoVpn3000AttributeType.REQUIRED_INDIVIDUAL_USER_AUTH, value);

        /// <summary>Creates a CVPN3000-Authenticated-User-Idle-Timeout attribute (Type 50).</summary>
        /// <param name="value">The idle timeout in minutes.</param>
        public static VendorSpecificAttributes AuthenticatedUserIdleTimeout(int value) => CreateInteger(CiscoVpn3000AttributeType.AUTHENTICATED_USER_IDLE_TIMEOUT, value);

        /// <summary>Creates a CVPN3000-Cisco-IP-Phone-Bypass attribute (Type 51).</summary>
        public static VendorSpecificAttributes CiscoIpPhoneBypass(int value) => CreateInteger(CiscoVpn3000AttributeType.CISCO_IP_PHONE_BYPASS, value);

        /// <summary>Creates a CVPN3000-User-Auth-Server-Port attribute (Type 53).</summary>
        public static VendorSpecificAttributes UserAuthServerPort(int value) => CreateInteger(CiscoVpn3000AttributeType.USER_AUTH_SERVER_PORT, value);

        /// <summary>
        /// Creates a CVPN3000-IPSec-Split-Tunneling-Policy attribute (Type 55).
        /// </summary>
        /// <param name="value">The split tunnelling policy. See <see cref="CVPN3000_IPSEC_SPLIT_TUNNELING_POLICY"/>.</param>
        public static VendorSpecificAttributes IpsecSplitTunnelingPolicy(CVPN3000_IPSEC_SPLIT_TUNNELING_POLICY value) => CreateInteger(CiscoVpn3000AttributeType.IPSEC_SPLIT_TUNNELING_POLICY, (int)value);

        /// <summary>Creates a CVPN3000-IPSec-Required-Client-Firewall-Capability attribute (Type 56).</summary>
        public static VendorSpecificAttributes IpsecRequiredClientFirewallCapability(int value) => CreateInteger(CiscoVpn3000AttributeType.IPSEC_REQUIRED_CLIENT_FIREWALL_CAPABILITY, value);

        /// <summary>Creates a CVPN3000-IPSec-Client-Firewall-Filter-Optional attribute (Type 58).</summary>
        public static VendorSpecificAttributes IpsecClientFirewallFilterOptional(int value) => CreateInteger(CiscoVpn3000AttributeType.IPSEC_CLIENT_FIREWALL_FILTER_OPTIONAL, value);

        /// <summary>Creates a CVPN3000-IPSec-Backup-Servers attribute (Type 59).</summary>
        public static VendorSpecificAttributes IpsecBackupServers(int value) => CreateInteger(CiscoVpn3000AttributeType.IPSEC_BACKUP_SERVERS, value);

        /// <summary>Creates a CVPN3000-Intercept-DHCP-Configure-Msg attribute (Type 62).</summary>
        public static VendorSpecificAttributes InterceptDhcpConfigureMsg(int value) => CreateInteger(CiscoVpn3000AttributeType.INTERCEPT_DHCP_CONFIGURE_MSG, value);

        /// <summary>Creates a CVPN3000-Allow-Network-Extension-Mode attribute (Type 64).</summary>
        public static VendorSpecificAttributes AllowNetworkExtensionMode(int value) => CreateInteger(CiscoVpn3000AttributeType.ALLOW_NETWORK_EXTENSION_MODE, value);

        /// <summary>Creates a CVPN3000-Authorization-Type attribute (Type 65).</summary>
        public static VendorSpecificAttributes AuthorizationType(int value) => CreateInteger(CiscoVpn3000AttributeType.AUTHORIZATION_TYPE, value);

        /// <summary>Creates a CVPN3000-Authorization-Required attribute (Type 66).</summary>
        public static VendorSpecificAttributes AuthorizationRequired(int value) => CreateInteger(CiscoVpn3000AttributeType.AUTHORIZATION_REQUIRED, value);

        /// <summary>Creates a CVPN3000-IKE-Keepalive-Confidence-Interval attribute (Type 68).</summary>
        public static VendorSpecificAttributes IkeKeepaliveConfidenceInterval(int value) => CreateInteger(CiscoVpn3000AttributeType.IKE_KEEPALIVE_CONFIDENCE_INTERVAL, value);

        /// <summary>Creates a CVPN3000-WebVPN-Content-Filter-Parameters attribute (Type 69).</summary>
        public static VendorSpecificAttributes WebVpnContentFilterParameters(int value) => CreateInteger(CiscoVpn3000AttributeType.WEBVPN_CONTENT_FILTER_PARAMETERS, value);

        /// <summary>Creates a CVPN3000-WebVPN-Enable-Functions attribute (Type 70).</summary>
        public static VendorSpecificAttributes WebVpnEnableFunctions(int value) => CreateInteger(CiscoVpn3000AttributeType.WEBVPN_ENABLE_FUNCTIONS, value);

        /// <summary>Creates a CVPN3000-Cisco-LEAP-Bypass attribute (Type 75).</summary>
        public static VendorSpecificAttributes CiscoLeapBypass(int value) => CreateInteger(CiscoVpn3000AttributeType.CISCO_LEAP_BYPASS, value);

        /// <summary>
        /// Creates a CVPN3000-IE-Proxy-Server-Policy attribute (Type 81).
        /// </summary>
        /// <param name="value">The IE proxy server policy. See <see cref="CVPN3000_IE_PROXY_SERVER_POLICY"/>.</param>
        public static VendorSpecificAttributes IeProxyServerPolicy(CVPN3000_IE_PROXY_SERVER_POLICY value) => CreateInteger(CiscoVpn3000AttributeType.IE_PROXY_SERVER_POLICY, (int)value);

        /// <summary>Creates a CVPN3000-IE-Proxy-Bypass-Local attribute (Type 83).</summary>
        public static VendorSpecificAttributes IeProxyBypassLocal(int value) => CreateInteger(CiscoVpn3000AttributeType.IE_PROXY_BYPASS_LOCAL, value);

        /// <summary>Creates a CVPN3000-IKE-Keepalive-Retry-Interval attribute (Type 84).</summary>
        public static VendorSpecificAttributes IkeKeepaliveRetryInterval(int value) => CreateInteger(CiscoVpn3000AttributeType.IKE_KEEPALIVE_RETRY_INTERVAL, value);

        /// <summary>Creates a CVPN3000-Perfect-Forward-Secrecy-Enable attribute (Type 88).</summary>
        public static VendorSpecificAttributes PerfectForwardSecrecyEnable(int value) => CreateInteger(CiscoVpn3000AttributeType.PERFECT_FORWARD_SECRECY_ENABLE, value);

        /// <summary>Creates a CVPN3000-NAC-Enable attribute (Type 89).</summary>
        public static VendorSpecificAttributes NacEnable(int value) => CreateInteger(CiscoVpn3000AttributeType.NAC_ENABLE, value);

        /// <summary>Creates a CVPN3000-NAC-Status-Query-Timer attribute (Type 90).</summary>
        public static VendorSpecificAttributes NacStatusQueryTimer(int value) => CreateInteger(CiscoVpn3000AttributeType.NAC_STATUS_QUERY_TIMER, value);

        /// <summary>Creates a CVPN3000-NAC-Revalidation-Timer attribute (Type 91).</summary>
        public static VendorSpecificAttributes NacRevalidationTimer(int value) => CreateInteger(CiscoVpn3000AttributeType.NAC_REVALIDATION_TIMER, value);

        /// <summary>
        /// Creates a CVPN3000-Strip-Realm attribute (Type 135).
        /// </summary>
        /// <param name="value">The strip realm setting. See <see cref="CVPN3000_STRIP_REALM"/>.</param>
        public static VendorSpecificAttributes StripRealm(CVPN3000_STRIP_REALM value) => CreateInteger(CiscoVpn3000AttributeType.STRIP_REALM, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates a CVPN3000-Access-Hours attribute (Type 1).</summary>
        /// <param name="value">The access hours definition. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AccessHours(string value) => CreateString(CiscoVpn3000AttributeType.ACCESS_HOURS, value);

        /// <summary>Creates a CVPN3000-IPSec-Sec-Association attribute (Type 12).</summary>
        /// <param name="value">The IPsec security association name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecSecAssociation(string value) => CreateString(CiscoVpn3000AttributeType.IPSEC_SEC_ASSOCIATION, value);

        /// <summary>Creates a CVPN3000-Banner1 attribute (Type 15).</summary>
        /// <param name="value">The login banner part 1. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Banner1(string value) => CreateString(CiscoVpn3000AttributeType.BANNER1, value);

        /// <summary>Creates a CVPN3000-Group-Policy attribute (Type 25).</summary>
        /// <param name="value">The group policy name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes GroupPolicy(string value) => CreateString(CiscoVpn3000AttributeType.GROUP_POLICY, value);

        /// <summary>Creates a CVPN3000-IPSec-Split-Tunnel-List attribute (Type 27).</summary>
        /// <param name="value">The IPsec split tunnel ACL name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecSplitTunnelList(string value) => CreateString(CiscoVpn3000AttributeType.IPSEC_SPLIT_TUNNEL_LIST, value);

        /// <summary>Creates a CVPN3000-IPSec-Default-Domain attribute (Type 28).</summary>
        /// <param name="value">The IPsec default domain name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecDefaultDomain(string value) => CreateString(CiscoVpn3000AttributeType.IPSEC_DEFAULT_DOMAIN, value);

        /// <summary>Creates a CVPN3000-IPSec-Split-DNS-Names attribute (Type 29).</summary>
        /// <param name="value">The IPsec split DNS domain names. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecSplitDnsNames(string value) => CreateString(CiscoVpn3000AttributeType.IPSEC_SPLIT_DNS_NAMES, value);

        /// <summary>Creates a CVPN3000-IPSec-Banner2 attribute (Type 36).</summary>
        /// <param name="value">The IPsec banner part 2. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecBanner2(string value) => CreateString(CiscoVpn3000AttributeType.IPSEC_BANNER2, value);

        /// <summary>Creates a CVPN3000-Required-Client-Firewall-Description attribute (Type 47).</summary>
        /// <param name="value">The required client firewall description. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes RequiredClientFirewallDescription(string value) => CreateString(CiscoVpn3000AttributeType.REQUIRED_CLIENT_FIREWALL_DESCRIPTION, value);

        /// <summary>Creates a CVPN3000-User-Auth-Server-Name attribute (Type 52).</summary>
        /// <param name="value">The user auth server name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes UserAuthServerName(string value) => CreateString(CiscoVpn3000AttributeType.USER_AUTH_SERVER_NAME, value);

        /// <summary>Creates a CVPN3000-User-Auth-Server-Secret attribute (Type 54).</summary>
        /// <param name="value">The user auth server secret. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes UserAuthServerSecret(string value) => CreateString(CiscoVpn3000AttributeType.USER_AUTH_SERVER_SECRET, value);

        /// <summary>Creates a CVPN3000-IPSec-Client-Firewall-Filter-Name attribute (Type 57).</summary>
        /// <param name="value">The client firewall filter name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecClientFirewallFilterName(string value) => CreateString(CiscoVpn3000AttributeType.IPSEC_CLIENT_FIREWALL_FILTER_NAME, value);

        /// <summary>Creates a CVPN3000-IPSec-Backup-Server-List attribute (Type 60).</summary>
        /// <param name="value">The IPsec backup server list. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecBackupServerList(string value) => CreateString(CiscoVpn3000AttributeType.IPSEC_BACKUP_SERVER_LIST, value);

        /// <summary>Creates a CVPN3000-Authorization-DN-Field attribute (Type 67).</summary>
        /// <param name="value">The authorization DN field. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AuthorizationDnField(string value) => CreateString(CiscoVpn3000AttributeType.AUTHORIZATION_DN_FIELD, value);

        /// <summary>Creates a CVPN3000-WebVPN-URL-List attribute (Type 71).</summary>
        /// <param name="value">The WebVPN URL list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnUrlList(string value) => CreateString(CiscoVpn3000AttributeType.WEBVPN_URL_LIST, value);

        /// <summary>Creates a CVPN3000-WebVPN-Port-Forwarding-List attribute (Type 72).</summary>
        /// <param name="value">The WebVPN port forwarding list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnPortForwardingList(string value) => CreateString(CiscoVpn3000AttributeType.WEBVPN_PORT_FORWARDING_LIST, value);

        /// <summary>Creates a CVPN3000-WebVPN-Access-List attribute (Type 73).</summary>
        /// <param name="value">The WebVPN access list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnAccessList(string value) => CreateString(CiscoVpn3000AttributeType.WEBVPN_ACCESS_LIST, value);

        /// <summary>Creates a CVPN3000-WebVPN-Homepage attribute (Type 76).</summary>
        /// <param name="value">The WebVPN homepage URL. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnHomepage(string value) => CreateString(CiscoVpn3000AttributeType.WEBVPN_HOMEPAGE, value);

        /// <summary>Creates a CVPN3000-Client-Type-Version-Limiting attribute (Type 77).</summary>
        /// <param name="value">The client type version limiting string. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes ClientTypeVersionLimiting(string value) => CreateString(CiscoVpn3000AttributeType.CLIENT_TYPE_VERSION_LIMITING, value);

        /// <summary>Creates a CVPN3000-WebVPN-Port-Forwarding-Name attribute (Type 79).</summary>
        /// <param name="value">The WebVPN port forwarding name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnPortForwardingName(string value) => CreateString(CiscoVpn3000AttributeType.WEBVPN_PORT_FORWARDING_NAME, value);

        /// <summary>Creates a CVPN3000-IE-Proxy-Server attribute (Type 80).</summary>
        /// <param name="value">The IE proxy server address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IeProxyServer(string value) => CreateString(CiscoVpn3000AttributeType.IE_PROXY_SERVER, value);

        /// <summary>Creates a CVPN3000-IE-Proxy-Exception-List attribute (Type 82).</summary>
        /// <param name="value">The IE proxy exception list. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IeProxyExceptionList(string value) => CreateString(CiscoVpn3000AttributeType.IE_PROXY_EXCEPTION_LIST, value);

        /// <summary>Creates a CVPN3000-Tunnel-Group-Lock attribute (Type 85).</summary>
        /// <param name="value">The tunnel group lock name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes TunnelGroupLock(string value) => CreateString(CiscoVpn3000AttributeType.TUNNEL_GROUP_LOCK, value);

        /// <summary>Creates a CVPN3000-Access-List-Inbound attribute (Type 86).</summary>
        /// <param name="value">The inbound access list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AccessListInbound(string value) => CreateString(CiscoVpn3000AttributeType.ACCESS_LIST_INBOUND, value);

        /// <summary>Creates a CVPN3000-Access-List-Outbound attribute (Type 87).</summary>
        /// <param name="value">The outbound access list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AccessListOutbound(string value) => CreateString(CiscoVpn3000AttributeType.ACCESS_LIST_OUTBOUND, value);

        /// <summary>Creates a CVPN3000-NAC-Default-ACL attribute (Type 92).</summary>
        /// <param name="value">The NAC default ACL name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes NacDefaultAcl(string value) => CreateString(CiscoVpn3000AttributeType.NAC_DEFAULT_ACL, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates a CVPN3000-Primary-DNS attribute (Type 5) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary DNS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes PrimaryDns(System.Net.IPAddress value) => CreateIpv4(CiscoVpn3000AttributeType.PRIMARY_DNS, value);

        /// <summary>
        /// Creates a CVPN3000-Secondary-DNS attribute (Type 6) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary DNS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes SecondaryDns(System.Net.IPAddress value) => CreateIpv4(CiscoVpn3000AttributeType.SECONDARY_DNS, value);

        /// <summary>
        /// Creates a CVPN3000-Primary-WINS attribute (Type 7) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary WINS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes PrimaryWins(System.Net.IPAddress value) => CreateIpv4(CiscoVpn3000AttributeType.PRIMARY_WINS, value);

        /// <summary>
        /// Creates a CVPN3000-Secondary-WINS attribute (Type 8) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary WINS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes SecondaryWins(System.Net.IPAddress value) => CreateIpv4(CiscoVpn3000AttributeType.SECONDARY_WINS, value);

        /// <summary>
        /// Creates a CVPN3000-DHCP-Network-Scope attribute (Type 61) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The DHCP network scope address. Must be IPv4. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes DhcpNetworkScope(System.Net.IPAddress value) => CreateIpv4(CiscoVpn3000AttributeType.DHCP_NETWORK_SCOPE, value);

        /// <summary>
        /// Creates a CVPN3000-MS-Client-Subnet-Mask attribute (Type 63) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The MS client subnet mask. Must be IPv4. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes MsClientSubnetMask(System.Net.IPAddress value) => CreateIpv4(CiscoVpn3000AttributeType.MS_CLIENT_SUBNET_MASK, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(CiscoVpn3000AttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(CiscoVpn3000AttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(CiscoVpn3000AttributeType type, System.Net.IPAddress value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
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