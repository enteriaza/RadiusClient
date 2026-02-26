using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Cisco ASA (IANA PEN 3076) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.cisco.asa</c>.
    /// </summary>
    /// <remarks>
    /// Cisco ASA uses vendor ID 3076 (Altiga Networks, acquired by Cisco) for its
    /// VPN-specific RADIUS attributes, separate from the main Cisco vendor ID 9.
    /// </remarks>
    public enum CiscoAsaAttributeType : byte
    {
        /// <summary>Cisco-ASA-Access-Hours (Type 1). String. Access hours definition.</summary>
        ACCESS_HOURS = 1,

        /// <summary>Cisco-ASA-Simultaneous-Logins (Type 2). Integer. Maximum simultaneous logins.</summary>
        SIMULTANEOUS_LOGINS = 2,

        /// <summary>Cisco-ASA-Min-Password-Length (Type 3). Integer. Minimum password length.</summary>
        MIN_PASSWORD_LENGTH = 3,

        /// <summary>Cisco-ASA-Allow-Alpha-Only-Passwords (Type 4). Integer. Allow alpha-only passwords flag.</summary>
        ALLOW_ALPHA_ONLY_PASSWORDS = 4,

        /// <summary>Cisco-ASA-Primary-DNS (Type 5). String. Primary DNS server address.</summary>
        PRIMARY_DNS = 5,

        /// <summary>Cisco-ASA-Secondary-DNS (Type 6). String. Secondary DNS server address.</summary>
        SECONDARY_DNS = 6,

        /// <summary>Cisco-ASA-Primary-WINS (Type 7). String. Primary WINS server address.</summary>
        PRIMARY_WINS = 7,

        /// <summary>Cisco-ASA-Secondary-WINS (Type 8). String. Secondary WINS server address.</summary>
        SECONDARY_WINS = 8,

        /// <summary>Cisco-ASA-SEP-Card-Assignment (Type 9). Integer. SEP card assignment.</summary>
        SEP_CARD_ASSIGNMENT = 9,

        /// <summary>Cisco-ASA-Tunneling-Protocols (Type 11). Integer. Tunnelling protocols bitmask.</summary>
        TUNNELING_PROTOCOLS = 11,

        /// <summary>Cisco-ASA-IPSec-Sec-Association (Type 12). String. IPsec security association name.</summary>
        IPSEC_SEC_ASSOCIATION = 12,

        /// <summary>Cisco-ASA-IPSec-Authentication (Type 13). Integer. IPsec authentication type.</summary>
        IPSEC_AUTHENTICATION = 13,

        /// <summary>Cisco-ASA-Banner1 (Type 15). String. Login banner part 1.</summary>
        BANNER1 = 15,

        /// <summary>Cisco-ASA-IPSec-Allow-Passwd-Store (Type 16). Integer. IPsec allow password store flag.</summary>
        IPSEC_ALLOW_PASSWD_STORE = 16,

        /// <summary>Cisco-ASA-Use-Client-Address (Type 17). Integer. Use client address flag.</summary>
        USE_CLIENT_ADDRESS = 17,

        /// <summary>Cisco-ASA-PPTP-Encryption (Type 20). Integer. PPTP encryption bitmask.</summary>
        PPTP_ENCRYPTION = 20,

        /// <summary>Cisco-ASA-L2TP-Encryption (Type 21). Integer. L2TP encryption bitmask.</summary>
        L2TP_ENCRYPTION = 21,

        /// <summary>Cisco-ASA-Group-Policy (Type 25). String. Group policy name.</summary>
        GROUP_POLICY = 25,

        /// <summary>Cisco-ASA-IPSec-Split-Tunnel-List (Type 27). String. IPsec split tunnel ACL name.</summary>
        IPSEC_SPLIT_TUNNEL_LIST = 27,

        /// <summary>Cisco-ASA-IPSec-Default-Domain (Type 28). String. IPsec default domain name.</summary>
        IPSEC_DEFAULT_DOMAIN = 28,

        /// <summary>Cisco-ASA-IPSec-Split-DNS-Names (Type 29). String. IPsec split DNS domain names.</summary>
        IPSEC_SPLIT_DNS_NAMES = 29,

        /// <summary>Cisco-ASA-IPSec-Tunnel-Type (Type 30). Integer. IPsec tunnel type.</summary>
        IPSEC_TUNNEL_TYPE = 30,

        /// <summary>Cisco-ASA-IPSec-Mode-Config (Type 31). Integer. IPsec mode config flag.</summary>
        IPSEC_MODE_CONFIG = 31,

        /// <summary>Cisco-ASA-Auth-On-Rekey (Type 42). Integer. Re-authenticate on rekey flag.</summary>
        AUTH_ON_REKEY = 42,

        /// <summary>Cisco-ASA-Required-Client-Firewall-Vendor-Code (Type 45). Integer. Required client firewall vendor code.</summary>
        REQUIRED_CLIENT_FIREWALL_VENDOR_CODE = 45,

        /// <summary>Cisco-ASA-Required-Client-Firewall-Product-Code (Type 46). Integer. Required client firewall product code.</summary>
        REQUIRED_CLIENT_FIREWALL_PRODUCT_CODE = 46,

        /// <summary>Cisco-ASA-Required-Client-Firewall-Description (Type 47). String. Required client firewall description.</summary>
        REQUIRED_CLIENT_FIREWALL_DESCRIPTION = 47,

        /// <summary>Cisco-ASA-Require-HW-Client-Auth (Type 48). Integer. Require hardware client authentication flag.</summary>
        REQUIRE_HW_CLIENT_AUTH = 48,

        /// <summary>Cisco-ASA-Required-Individual-User-Auth (Type 49). Integer. Require individual user authentication flag.</summary>
        REQUIRED_INDIVIDUAL_USER_AUTH = 49,

        /// <summary>Cisco-ASA-Authenticated-User-Idle-Timeout (Type 50). Integer. Authenticated user idle timeout in minutes.</summary>
        AUTHENTICATED_USER_IDLE_TIMEOUT = 50,

        /// <summary>Cisco-ASA-Cisco-IP-Phone-Bypass (Type 51). Integer. Cisco IP phone bypass flag.</summary>
        CISCO_IP_PHONE_BYPASS = 51,

        /// <summary>Cisco-ASA-IPSec-Split-Tunneling-Policy (Type 55). Integer. IPsec split tunnelling policy.</summary>
        IPSEC_SPLIT_TUNNELING_POLICY = 55,

        /// <summary>Cisco-ASA-IPSec-Required-Client-Firewall-Capability (Type 56). Integer. IPsec required client firewall capability.</summary>
        IPSEC_REQUIRED_CLIENT_FIREWALL_CAPABILITY = 56,

        /// <summary>Cisco-ASA-IPSec-Client-Firewall-Filter-Name (Type 57). String. IPsec client firewall filter name.</summary>
        IPSEC_CLIENT_FIREWALL_FILTER_NAME = 57,

        /// <summary>Cisco-ASA-IPSec-Client-Firewall-Filter-Optional (Type 58). Integer. IPsec client firewall filter optional flag.</summary>
        IPSEC_CLIENT_FIREWALL_FILTER_OPTIONAL = 58,

        /// <summary>Cisco-ASA-IPSec-Backup-Servers (Type 59). Integer. IPsec backup servers option.</summary>
        IPSEC_BACKUP_SERVERS = 59,

        /// <summary>Cisco-ASA-IPSec-Backup-Server-List (Type 60). String. IPsec backup server list.</summary>
        IPSEC_BACKUP_SERVER_LIST = 60,

        /// <summary>Cisco-ASA-DHCP-Network-Scope (Type 61). String. DHCP network scope address.</summary>
        DHCP_NETWORK_SCOPE = 61,

        /// <summary>Cisco-ASA-Intercept-DHCP-Configure-Msg (Type 62). Integer. Intercept DHCP configure message flag.</summary>
        INTERCEPT_DHCP_CONFIGURE_MSG = 62,

        /// <summary>Cisco-ASA-MS-Client-Subnet-Mask (Type 63). String. MS client subnet mask.</summary>
        MS_CLIENT_SUBNET_MASK = 63,

        /// <summary>Cisco-ASA-Allow-Network-Extension-Mode (Type 64). Integer. Allow network extension mode flag.</summary>
        ALLOW_NETWORK_EXTENSION_MODE = 64,

        /// <summary>Cisco-ASA-Authorization-Type (Type 65). Integer. Authorization type.</summary>
        AUTHORIZATION_TYPE = 65,

        /// <summary>Cisco-ASA-Authorization-Required (Type 66). Integer. Authorization required flag.</summary>
        AUTHORIZATION_REQUIRED = 66,

        /// <summary>Cisco-ASA-Authorization-DN-Field (Type 67). String. Authorization DN field.</summary>
        AUTHORIZATION_DN_FIELD = 67,

        /// <summary>Cisco-ASA-IKE-KeepAlive-Confidence-Interval (Type 68). Integer. IKE keepalive confidence interval in seconds.</summary>
        IKE_KEEPALIVE_CONFIDENCE_INTERVAL = 68,

        /// <summary>Cisco-ASA-WebVPN-Content-Filter-Parameters (Type 69). Integer. WebVPN content filter parameters.</summary>
        WEBVPN_CONTENT_FILTER_PARAMETERS = 69,

        /// <summary>Cisco-ASA-WebVPN-URL-List (Type 71). String. WebVPN URL list name.</summary>
        WEBVPN_URL_LIST = 71,

        /// <summary>Cisco-ASA-WebVPN-Port-Forwarding-List (Type 72). String. WebVPN port forwarding list name.</summary>
        WEBVPN_PORT_FORWARDING_LIST = 72,

        /// <summary>Cisco-ASA-WebVPN-Access-List (Type 73). String. WebVPN access list name.</summary>
        WEBVPN_ACCESS_LIST = 73,

        /// <summary>Cisco-ASA-WebVPN-HTTP-Proxy-IP-Address (Type 74). String. WebVPN HTTP proxy IP address.</summary>
        WEBVPN_HTTP_PROXY_IP_ADDRESS = 74,

        /// <summary>Cisco-ASA-Cisco-LEAP-Bypass (Type 75). Integer. Cisco LEAP bypass flag.</summary>
        CISCO_LEAP_BYPASS = 75,

        /// <summary>Cisco-ASA-WebVPN-Apply-ACL (Type 76). Integer. WebVPN apply ACL flag.</summary>
        WEBVPN_APPLY_ACL = 76,

        /// <summary>Cisco-ASA-Client-Type-Version-Limiting (Type 77). String. Client type version limiting string.</summary>
        CLIENT_TYPE_VERSION_LIMITING = 77,

        /// <summary>Cisco-ASA-WebVPN-Group-Based-HTTP/HTTPS-Proxy-Exception-List (Type 78). String. WebVPN group-based proxy exception list.</summary>
        WEBVPN_GROUP_BASED_PROXY_EXCEPTION_LIST = 78,

        /// <summary>Cisco-ASA-WebVPN-Port-Forwarding-Name (Type 79). String. WebVPN port forwarding name.</summary>
        WEBVPN_PORT_FORWARDING_NAME = 79,

        /// <summary>Cisco-ASA-IE-Proxy-Server (Type 80). String. IE proxy server address.</summary>
        IE_PROXY_SERVER = 80,

        /// <summary>Cisco-ASA-IE-Proxy-Server-Policy (Type 81). Integer. IE proxy server policy.</summary>
        IE_PROXY_SERVER_POLICY = 81,

        /// <summary>Cisco-ASA-IE-Proxy-Exception-List (Type 82). String. IE proxy exception list.</summary>
        IE_PROXY_EXCEPTION_LIST = 82,

        /// <summary>Cisco-ASA-IE-Proxy-Bypass-Local (Type 83). Integer. IE proxy bypass local flag.</summary>
        IE_PROXY_BYPASS_LOCAL = 83,

        /// <summary>Cisco-ASA-IKE-KeepAlive-Retry-Interval (Type 84). Integer. IKE keepalive retry interval in seconds.</summary>
        IKE_KEEPALIVE_RETRY_INTERVAL = 84,

        /// <summary>Cisco-ASA-Tunnel-Group-Lock (Type 85). String. Tunnel group lock name.</summary>
        TUNNEL_GROUP_LOCK = 85,

        /// <summary>Cisco-ASA-Access-List-Inbound (Type 86). String. Inbound access list name.</summary>
        ACCESS_LIST_INBOUND = 86,

        /// <summary>Cisco-ASA-Access-List-Outbound (Type 87). String. Outbound access list name.</summary>
        ACCESS_LIST_OUTBOUND = 87,

        /// <summary>Cisco-ASA-Perfect-Forward-Secrecy-Enable (Type 88). Integer. PFS enable flag.</summary>
        PERFECT_FORWARD_SECRECY_ENABLE = 88,

        /// <summary>Cisco-ASA-NAC-Enable (Type 89). Integer. NAC enable flag.</summary>
        NAC_ENABLE = 89,

        /// <summary>Cisco-ASA-NAC-Status-Query-Timer (Type 90). Integer. NAC status query timer in seconds.</summary>
        NAC_STATUS_QUERY_TIMER = 90,

        /// <summary>Cisco-ASA-NAC-Revalidation-Timer (Type 91). Integer. NAC revalidation timer in seconds.</summary>
        NAC_REVALIDATION_TIMER = 91,

        /// <summary>Cisco-ASA-NAC-Default-ACL (Type 92). String. NAC default ACL name.</summary>
        NAC_DEFAULT_ACL = 92,

        /// <summary>Cisco-ASA-WebVPN-URL-Entry-Enable (Type 93). Integer. WebVPN URL entry enable flag.</summary>
        WEBVPN_URL_ENTRY_ENABLE = 93,

        /// <summary>Cisco-ASA-WebVPN-File-Access-Enable (Type 94). Integer. WebVPN file access enable flag.</summary>
        WEBVPN_FILE_ACCESS_ENABLE = 94,

        /// <summary>Cisco-ASA-WebVPN-File-Server-Entry-Enable (Type 95). Integer. WebVPN file server entry enable flag.</summary>
        WEBVPN_FILE_SERVER_ENTRY_ENABLE = 95,

        /// <summary>Cisco-ASA-WebVPN-File-Server-Browsing-Enable (Type 96). Integer. WebVPN file server browsing enable flag.</summary>
        WEBVPN_FILE_SERVER_BROWSING_ENABLE = 96,

        /// <summary>Cisco-ASA-WebVPN-Port-Forwarding-Enable (Type 97). Integer. WebVPN port forwarding enable flag.</summary>
        WEBVPN_PORT_FORWARDING_ENABLE = 97,

        /// <summary>Cisco-ASA-WebVPN-Outlook-Exchange-Proxy-Enable (Type 98). Integer. WebVPN Outlook Exchange proxy enable flag.</summary>
        WEBVPN_OUTLOOK_EXCHANGE_PROXY_ENABLE = 98,

        /// <summary>Cisco-ASA-WebVPN-Port-Forwarding-HTTP-Proxy (Type 99). Integer. WebVPN port forwarding HTTP proxy flag.</summary>
        WEBVPN_PORT_FORWARDING_HTTP_PROXY = 99,

        /// <summary>Cisco-ASA-WebVPN-Auto-Applet-Download-List (Type 100). String. WebVPN auto applet download list.</summary>
        WEBVPN_AUTO_APPLET_DOWNLOAD_LIST = 100,

        /// <summary>Cisco-ASA-WebVPN-Citrix-Metaframe-Enable (Type 101). Integer. WebVPN Citrix MetaFrame enable flag.</summary>
        WEBVPN_CITRIX_METAFRAME_ENABLE = 101,

        /// <summary>Cisco-ASA-WebVPN-Apply-ACL-Inbound (Type 102). Integer. WebVPN apply ACL inbound flag.</summary>
        WEBVPN_APPLY_ACL_INBOUND = 102,

        /// <summary>Cisco-ASA-WebVPN-SSL-VPN-Client-Enable (Type 103). Integer. WebVPN SSL VPN client enable flag.</summary>
        WEBVPN_SSL_VPN_CLIENT_ENABLE = 103,

        /// <summary>Cisco-ASA-WebVPN-SSL-VPN-Client-Required (Type 104). Integer. WebVPN SSL VPN client required flag.</summary>
        WEBVPN_SSL_VPN_CLIENT_REQUIRED = 104,

        /// <summary>Cisco-ASA-WebVPN-SSL-VPN-Client-Keep-Installation (Type 105). Integer. WebVPN SSL VPN client keep installation flag.</summary>
        WEBVPN_SSL_VPN_CLIENT_KEEP_INSTALLATION = 105,

        /// <summary>Cisco-ASA-WebVPN-Homepage (Type 116). String. WebVPN homepage URL.</summary>
        WEBVPN_HOMEPAGE = 116,

        /// <summary>Cisco-ASA-Client-Bypass-Protocol (Type 117). Integer. Client bypass protocol flag.</summary>
        CLIENT_BYPASS_PROTOCOL = 117,

        /// <summary>Cisco-ASA-WebVPN-Smart-Tunnel (Type 136). String. WebVPN smart tunnel list name.</summary>
        WEBVPN_SMART_TUNNEL = 136,

        /// <summary>Cisco-ASA-WebVPN-Smart-Tunnel-Auto (Type 137). Integer. WebVPN smart tunnel auto-start flag.</summary>
        WEBVPN_SMART_TUNNEL_AUTO = 137,

        /// <summary>Cisco-ASA-WebVPN-Smart-Tunnel-Auto-Signon-Enable (Type 139). String. WebVPN smart tunnel auto sign-on enable.</summary>
        WEBVPN_SMART_TUNNEL_AUTO_SIGNON_ENABLE = 139,

        /// <summary>Cisco-ASA-WebVPN-Deny-Message (Type 140). String. WebVPN deny message string.</summary>
        WEBVPN_DENY_MESSAGE = 140,

        /// <summary>Cisco-ASA-Extended-Authentication-On-Rekey (Type 152). Integer. Extended authentication on rekey flag.</summary>
        EXTENDED_AUTHENTICATION_ON_REKEY = 152,

        /// <summary>Cisco-ASA-WebVPN-Auto-HTTP-Signon (Type 153). String. WebVPN auto HTTP sign-on configuration.</summary>
        WEBVPN_AUTO_HTTP_SIGNON = 153,

        /// <summary>Cisco-ASA-Username-Password-Delta (Type 154). Integer. Username password delta in days.</summary>
        USERNAME_PASSWORD_DELTA = 154,

        /// <summary>Cisco-ASA-WebVPN-Macro-Value1 (Type 223). String. WebVPN macro substitution value 1.</summary>
        WEBVPN_MACRO_VALUE1 = 223,

        /// <summary>Cisco-ASA-WebVPN-Macro-Value2 (Type 224). String. WebVPN macro substitution value 2.</summary>
        WEBVPN_MACRO_VALUE2 = 224
    }

    /// <summary>
    /// Cisco-ASA-Tunneling-Protocols attribute values (Type 11). Bitmask.
    /// </summary>
    [Flags]
    public enum CISCO_ASA_TUNNELING_PROTOCOLS
    {
        /// <summary>PPTP tunnelling.</summary>
        PPTP = 1,

        /// <summary>L2TP tunnelling.</summary>
        L2TP = 2,

        /// <summary>IPsec (IKEv1) tunnelling.</summary>
        IPSEC = 4,

        /// <summary>L2TP/IPsec tunnelling.</summary>
        L2TP_IPSEC = 8,

        /// <summary>WebVPN / SSL VPN.</summary>
        WEBVPN = 16,

        /// <summary>AnyConnect / SSL VPN Client.</summary>
        SVC = 32,

        /// <summary>IKEv2 IPsec.</summary>
        IKEV2 = 64
    }

    /// <summary>
    /// Cisco-ASA-IPSec-Authentication attribute values (Type 13).
    /// </summary>
    public enum CISCO_ASA_IPSEC_AUTHENTICATION
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
    /// Cisco-ASA-IPSec-Split-Tunneling-Policy attribute values (Type 55).
    /// </summary>
    public enum CISCO_ASA_IPSEC_SPLIT_TUNNELING_POLICY
    {
        /// <summary>Tunnel all traffic.</summary>
        TUNNEL_ALL = 0,

        /// <summary>Only tunnel traffic for specified networks.</summary>
        SPLIT_INCLUDE = 1,

        /// <summary>Tunnel all except specified networks.</summary>
        SPLIT_EXCLUDE = 2
    }

    /// <summary>
    /// Cisco-ASA-IE-Proxy-Server-Policy attribute values (Type 81).
    /// </summary>
    public enum CISCO_ASA_IE_PROXY_SERVER_POLICY
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

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Cisco ASA
    /// (IANA PEN 3076, Altiga Networks) Vendor-Specific Attributes (VSAs), as defined
    /// in the FreeRADIUS <c>dictionary.cisco.asa</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Cisco ASA's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3076</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Cisco ASA firewalls and VPN concentrators
    /// (originally Altiga Networks) for remote access VPN configuration including
    /// IPsec, SSL/WebVPN, AnyConnect, L2TP, PPTP, split tunnelling, DNS/WINS
    /// assignment, group policies, banner messages, NAC, firewall requirements,
    /// proxy configuration, and WebVPN portal customisation.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CiscoAsaAttributes.GroupPolicy("DfltGrpPolicy"));
    /// packet.SetAttribute(CiscoAsaAttributes.TunnelingProtocols(
    ///     CISCO_ASA_TUNNELING_PROTOCOLS.IPSEC | CISCO_ASA_TUNNELING_PROTOCOLS.SVC));
    /// packet.SetAttribute(CiscoAsaAttributes.PrimaryDns("8.8.8.8"));
    /// packet.SetAttribute(CiscoAsaAttributes.IpsecSplitTunnelList("split-acl"));
    /// packet.SetAttribute(CiscoAsaAttributes.SimultaneousLogins(3));
    /// </code>
    /// </remarks>
    public static class CiscoAsaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Cisco ASA (Altiga Networks).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3076;

        #region Integer Attributes

        /// <summary>Creates a Cisco-ASA-Simultaneous-Logins attribute (Type 2).</summary>
        public static VendorSpecificAttributes SimultaneousLogins(int value) => CreateInteger(CiscoAsaAttributeType.SIMULTANEOUS_LOGINS, value);

        /// <summary>Creates a Cisco-ASA-Min-Password-Length attribute (Type 3).</summary>
        public static VendorSpecificAttributes MinPasswordLength(int value) => CreateInteger(CiscoAsaAttributeType.MIN_PASSWORD_LENGTH, value);

        /// <summary>Creates a Cisco-ASA-Allow-Alpha-Only-Passwords attribute (Type 4).</summary>
        public static VendorSpecificAttributes AllowAlphaOnlyPasswords(int value) => CreateInteger(CiscoAsaAttributeType.ALLOW_ALPHA_ONLY_PASSWORDS, value);

        /// <summary>Creates a Cisco-ASA-SEP-Card-Assignment attribute (Type 9).</summary>
        public static VendorSpecificAttributes SepCardAssignment(int value) => CreateInteger(CiscoAsaAttributeType.SEP_CARD_ASSIGNMENT, value);

        /// <summary>
        /// Creates a Cisco-ASA-Tunneling-Protocols attribute (Type 11).
        /// </summary>
        /// <param name="value">The tunnelling protocols bitmask. See <see cref="CISCO_ASA_TUNNELING_PROTOCOLS"/>.</param>
        public static VendorSpecificAttributes TunnelingProtocols(CISCO_ASA_TUNNELING_PROTOCOLS value) => CreateInteger(CiscoAsaAttributeType.TUNNELING_PROTOCOLS, (int)value);

        /// <summary>
        /// Creates a Cisco-ASA-IPSec-Authentication attribute (Type 13).
        /// </summary>
        /// <param name="value">The IPsec authentication type. See <see cref="CISCO_ASA_IPSEC_AUTHENTICATION"/>.</param>
        public static VendorSpecificAttributes IpsecAuthentication(CISCO_ASA_IPSEC_AUTHENTICATION value) => CreateInteger(CiscoAsaAttributeType.IPSEC_AUTHENTICATION, (int)value);

        /// <summary>Creates a Cisco-ASA-IPSec-Allow-Passwd-Store attribute (Type 16).</summary>
        public static VendorSpecificAttributes IpsecAllowPasswdStore(int value) => CreateInteger(CiscoAsaAttributeType.IPSEC_ALLOW_PASSWD_STORE, value);

        /// <summary>Creates a Cisco-ASA-Use-Client-Address attribute (Type 17).</summary>
        public static VendorSpecificAttributes UseClientAddress(int value) => CreateInteger(CiscoAsaAttributeType.USE_CLIENT_ADDRESS, value);

        /// <summary>Creates a Cisco-ASA-PPTP-Encryption attribute (Type 20).</summary>
        public static VendorSpecificAttributes PptpEncryption(int value) => CreateInteger(CiscoAsaAttributeType.PPTP_ENCRYPTION, value);

        /// <summary>Creates a Cisco-ASA-L2TP-Encryption attribute (Type 21).</summary>
        public static VendorSpecificAttributes L2tpEncryption(int value) => CreateInteger(CiscoAsaAttributeType.L2TP_ENCRYPTION, value);

        /// <summary>Creates a Cisco-ASA-IPSec-Tunnel-Type attribute (Type 30).</summary>
        public static VendorSpecificAttributes IpsecTunnelType(int value) => CreateInteger(CiscoAsaAttributeType.IPSEC_TUNNEL_TYPE, value);

        /// <summary>Creates a Cisco-ASA-IPSec-Mode-Config attribute (Type 31).</summary>
        public static VendorSpecificAttributes IpsecModeConfig(int value) => CreateInteger(CiscoAsaAttributeType.IPSEC_MODE_CONFIG, value);

        /// <summary>Creates a Cisco-ASA-Auth-On-Rekey attribute (Type 42).</summary>
        public static VendorSpecificAttributes AuthOnRekey(int value) => CreateInteger(CiscoAsaAttributeType.AUTH_ON_REKEY, value);

        /// <summary>Creates a Cisco-ASA-Required-Client-Firewall-Vendor-Code attribute (Type 45).</summary>
        public static VendorSpecificAttributes RequiredClientFirewallVendorCode(int value) => CreateInteger(CiscoAsaAttributeType.REQUIRED_CLIENT_FIREWALL_VENDOR_CODE, value);

        /// <summary>Creates a Cisco-ASA-Required-Client-Firewall-Product-Code attribute (Type 46).</summary>
        public static VendorSpecificAttributes RequiredClientFirewallProductCode(int value) => CreateInteger(CiscoAsaAttributeType.REQUIRED_CLIENT_FIREWALL_PRODUCT_CODE, value);

        /// <summary>Creates a Cisco-ASA-Require-HW-Client-Auth attribute (Type 48).</summary>
        public static VendorSpecificAttributes RequireHwClientAuth(int value) => CreateInteger(CiscoAsaAttributeType.REQUIRE_HW_CLIENT_AUTH, value);

        /// <summary>Creates a Cisco-ASA-Required-Individual-User-Auth attribute (Type 49).</summary>
        public static VendorSpecificAttributes RequiredIndividualUserAuth(int value) => CreateInteger(CiscoAsaAttributeType.REQUIRED_INDIVIDUAL_USER_AUTH, value);

        /// <summary>Creates a Cisco-ASA-Authenticated-User-Idle-Timeout attribute (Type 50).</summary>
        /// <param name="value">The idle timeout in minutes.</param>
        public static VendorSpecificAttributes AuthenticatedUserIdleTimeout(int value) => CreateInteger(CiscoAsaAttributeType.AUTHENTICATED_USER_IDLE_TIMEOUT, value);

        /// <summary>Creates a Cisco-ASA-Cisco-IP-Phone-Bypass attribute (Type 51).</summary>
        public static VendorSpecificAttributes CiscoIpPhoneBypass(int value) => CreateInteger(CiscoAsaAttributeType.CISCO_IP_PHONE_BYPASS, value);

        /// <summary>
        /// Creates a Cisco-ASA-IPSec-Split-Tunneling-Policy attribute (Type 55).
        /// </summary>
        /// <param name="value">The split tunnelling policy. See <see cref="CISCO_ASA_IPSEC_SPLIT_TUNNELING_POLICY"/>.</param>
        public static VendorSpecificAttributes IpsecSplitTunnelingPolicy(CISCO_ASA_IPSEC_SPLIT_TUNNELING_POLICY value) => CreateInteger(CiscoAsaAttributeType.IPSEC_SPLIT_TUNNELING_POLICY, (int)value);

        /// <summary>Creates a Cisco-ASA-IPSec-Required-Client-Firewall-Capability attribute (Type 56).</summary>
        public static VendorSpecificAttributes IpsecRequiredClientFirewallCapability(int value) => CreateInteger(CiscoAsaAttributeType.IPSEC_REQUIRED_CLIENT_FIREWALL_CAPABILITY, value);

        /// <summary>Creates a Cisco-ASA-IPSec-Client-Firewall-Filter-Optional attribute (Type 58).</summary>
        public static VendorSpecificAttributes IpsecClientFirewallFilterOptional(int value) => CreateInteger(CiscoAsaAttributeType.IPSEC_CLIENT_FIREWALL_FILTER_OPTIONAL, value);

        /// <summary>Creates a Cisco-ASA-IPSec-Backup-Servers attribute (Type 59).</summary>
        public static VendorSpecificAttributes IpsecBackupServers(int value) => CreateInteger(CiscoAsaAttributeType.IPSEC_BACKUP_SERVERS, value);

        /// <summary>Creates a Cisco-ASA-Intercept-DHCP-Configure-Msg attribute (Type 62).</summary>
        public static VendorSpecificAttributes InterceptDhcpConfigureMsg(int value) => CreateInteger(CiscoAsaAttributeType.INTERCEPT_DHCP_CONFIGURE_MSG, value);

        /// <summary>Creates a Cisco-ASA-Allow-Network-Extension-Mode attribute (Type 64).</summary>
        public static VendorSpecificAttributes AllowNetworkExtensionMode(int value) => CreateInteger(CiscoAsaAttributeType.ALLOW_NETWORK_EXTENSION_MODE, value);

        /// <summary>Creates a Cisco-ASA-Authorization-Type attribute (Type 65).</summary>
        public static VendorSpecificAttributes AuthorizationType(int value) => CreateInteger(CiscoAsaAttributeType.AUTHORIZATION_TYPE, value);

        /// <summary>Creates a Cisco-ASA-Authorization-Required attribute (Type 66).</summary>
        public static VendorSpecificAttributes AuthorizationRequired(int value) => CreateInteger(CiscoAsaAttributeType.AUTHORIZATION_REQUIRED, value);

        /// <summary>Creates a Cisco-ASA-IKE-KeepAlive-Confidence-Interval attribute (Type 68).</summary>
        public static VendorSpecificAttributes IkeKeepAliveConfidenceInterval(int value) => CreateInteger(CiscoAsaAttributeType.IKE_KEEPALIVE_CONFIDENCE_INTERVAL, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Content-Filter-Parameters attribute (Type 69).</summary>
        public static VendorSpecificAttributes WebVpnContentFilterParameters(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_CONTENT_FILTER_PARAMETERS, value);

        /// <summary>Creates a Cisco-ASA-Cisco-LEAP-Bypass attribute (Type 75).</summary>
        public static VendorSpecificAttributes CiscoLeapBypass(int value) => CreateInteger(CiscoAsaAttributeType.CISCO_LEAP_BYPASS, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Apply-ACL attribute (Type 76).</summary>
        public static VendorSpecificAttributes WebVpnApplyAcl(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_APPLY_ACL, value);

        /// <summary>
        /// Creates a Cisco-ASA-IE-Proxy-Server-Policy attribute (Type 81).
        /// </summary>
        /// <param name="value">The IE proxy server policy. See <see cref="CISCO_ASA_IE_PROXY_SERVER_POLICY"/>.</param>
        public static VendorSpecificAttributes IeProxyServerPolicy(CISCO_ASA_IE_PROXY_SERVER_POLICY value) => CreateInteger(CiscoAsaAttributeType.IE_PROXY_SERVER_POLICY, (int)value);

        /// <summary>Creates a Cisco-ASA-IE-Proxy-Bypass-Local attribute (Type 83).</summary>
        public static VendorSpecificAttributes IeProxyBypassLocal(int value) => CreateInteger(CiscoAsaAttributeType.IE_PROXY_BYPASS_LOCAL, value);

        /// <summary>Creates a Cisco-ASA-IKE-KeepAlive-Retry-Interval attribute (Type 84).</summary>
        public static VendorSpecificAttributes IkeKeepAliveRetryInterval(int value) => CreateInteger(CiscoAsaAttributeType.IKE_KEEPALIVE_RETRY_INTERVAL, value);

        /// <summary>Creates a Cisco-ASA-Perfect-Forward-Secrecy-Enable attribute (Type 88).</summary>
        public static VendorSpecificAttributes PerfectForwardSecrecyEnable(int value) => CreateInteger(CiscoAsaAttributeType.PERFECT_FORWARD_SECRECY_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-NAC-Enable attribute (Type 89).</summary>
        public static VendorSpecificAttributes NacEnable(int value) => CreateInteger(CiscoAsaAttributeType.NAC_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-NAC-Status-Query-Timer attribute (Type 90).</summary>
        public static VendorSpecificAttributes NacStatusQueryTimer(int value) => CreateInteger(CiscoAsaAttributeType.NAC_STATUS_QUERY_TIMER, value);

        /// <summary>Creates a Cisco-ASA-NAC-Revalidation-Timer attribute (Type 91).</summary>
        public static VendorSpecificAttributes NacRevalidationTimer(int value) => CreateInteger(CiscoAsaAttributeType.NAC_REVALIDATION_TIMER, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-URL-Entry-Enable attribute (Type 93).</summary>
        public static VendorSpecificAttributes WebVpnUrlEntryEnable(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_URL_ENTRY_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-File-Access-Enable attribute (Type 94).</summary>
        public static VendorSpecificAttributes WebVpnFileAccessEnable(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_FILE_ACCESS_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-File-Server-Entry-Enable attribute (Type 95).</summary>
        public static VendorSpecificAttributes WebVpnFileServerEntryEnable(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_FILE_SERVER_ENTRY_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-File-Server-Browsing-Enable attribute (Type 96).</summary>
        public static VendorSpecificAttributes WebVpnFileServerBrowsingEnable(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_FILE_SERVER_BROWSING_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Port-Forwarding-Enable attribute (Type 97).</summary>
        public static VendorSpecificAttributes WebVpnPortForwardingEnable(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_PORT_FORWARDING_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Outlook-Exchange-Proxy-Enable attribute (Type 98).</summary>
        public static VendorSpecificAttributes WebVpnOutlookExchangeProxyEnable(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_OUTLOOK_EXCHANGE_PROXY_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Port-Forwarding-HTTP-Proxy attribute (Type 99).</summary>
        public static VendorSpecificAttributes WebVpnPortForwardingHttpProxy(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_PORT_FORWARDING_HTTP_PROXY, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Citrix-Metaframe-Enable attribute (Type 101).</summary>
        public static VendorSpecificAttributes WebVpnCitrixMetaframeEnable(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_CITRIX_METAFRAME_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Apply-ACL-Inbound attribute (Type 102).</summary>
        public static VendorSpecificAttributes WebVpnApplyAclInbound(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_APPLY_ACL_INBOUND, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-SSL-VPN-Client-Enable attribute (Type 103).</summary>
        public static VendorSpecificAttributes WebVpnSslVpnClientEnable(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_SSL_VPN_CLIENT_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-SSL-VPN-Client-Required attribute (Type 104).</summary>
        public static VendorSpecificAttributes WebVpnSslVpnClientRequired(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_SSL_VPN_CLIENT_REQUIRED, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-SSL-VPN-Client-Keep-Installation attribute (Type 105).</summary>
        public static VendorSpecificAttributes WebVpnSslVpnClientKeepInstallation(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_SSL_VPN_CLIENT_KEEP_INSTALLATION, value);

        /// <summary>Creates a Cisco-ASA-Client-Bypass-Protocol attribute (Type 117).</summary>
        public static VendorSpecificAttributes ClientBypassProtocol(int value) => CreateInteger(CiscoAsaAttributeType.CLIENT_BYPASS_PROTOCOL, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Smart-Tunnel-Auto attribute (Type 137).</summary>
        public static VendorSpecificAttributes WebVpnSmartTunnelAuto(int value) => CreateInteger(CiscoAsaAttributeType.WEBVPN_SMART_TUNNEL_AUTO, value);

        /// <summary>Creates a Cisco-ASA-Extended-Authentication-On-Rekey attribute (Type 152).</summary>
        public static VendorSpecificAttributes ExtendedAuthenticationOnRekey(int value) => CreateInteger(CiscoAsaAttributeType.EXTENDED_AUTHENTICATION_ON_REKEY, value);

        /// <summary>Creates a Cisco-ASA-Username-Password-Delta attribute (Type 154).</summary>
        public static VendorSpecificAttributes UsernamePasswordDelta(int value) => CreateInteger(CiscoAsaAttributeType.USERNAME_PASSWORD_DELTA, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Cisco-ASA-Access-Hours attribute (Type 1).</summary>
        /// <param name="value">The access hours definition. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AccessHours(string value) => CreateString(CiscoAsaAttributeType.ACCESS_HOURS, value);

        /// <summary>Creates a Cisco-ASA-Primary-DNS attribute (Type 5).</summary>
        /// <param name="value">The primary DNS server address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes PrimaryDns(string value) => CreateString(CiscoAsaAttributeType.PRIMARY_DNS, value);

        /// <summary>Creates a Cisco-ASA-Secondary-DNS attribute (Type 6).</summary>
        /// <param name="value">The secondary DNS server address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes SecondaryDns(string value) => CreateString(CiscoAsaAttributeType.SECONDARY_DNS, value);

        /// <summary>Creates a Cisco-ASA-Primary-WINS attribute (Type 7).</summary>
        /// <param name="value">The primary WINS server address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes PrimaryWins(string value) => CreateString(CiscoAsaAttributeType.PRIMARY_WINS, value);

        /// <summary>Creates a Cisco-ASA-Secondary-WINS attribute (Type 8).</summary>
        /// <param name="value">The secondary WINS server address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes SecondaryWins(string value) => CreateString(CiscoAsaAttributeType.SECONDARY_WINS, value);

        /// <summary>Creates a Cisco-ASA-IPSec-Sec-Association attribute (Type 12).</summary>
        /// <param name="value">The IPsec security association name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecSecAssociation(string value) => CreateString(CiscoAsaAttributeType.IPSEC_SEC_ASSOCIATION, value);

        /// <summary>Creates a Cisco-ASA-Banner1 attribute (Type 15).</summary>
        /// <param name="value">The login banner part 1. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes Banner1(string value) => CreateString(CiscoAsaAttributeType.BANNER1, value);

        /// <summary>Creates a Cisco-ASA-Group-Policy attribute (Type 25).</summary>
        /// <param name="value">The group policy name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes GroupPolicy(string value) => CreateString(CiscoAsaAttributeType.GROUP_POLICY, value);

        /// <summary>Creates a Cisco-ASA-IPSec-Split-Tunnel-List attribute (Type 27).</summary>
        /// <param name="value">The IPsec split tunnel ACL name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecSplitTunnelList(string value) => CreateString(CiscoAsaAttributeType.IPSEC_SPLIT_TUNNEL_LIST, value);

        /// <summary>Creates a Cisco-ASA-IPSec-Default-Domain attribute (Type 28).</summary>
        /// <param name="value">The IPsec default domain name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecDefaultDomain(string value) => CreateString(CiscoAsaAttributeType.IPSEC_DEFAULT_DOMAIN, value);

        /// <summary>Creates a Cisco-ASA-IPSec-Split-DNS-Names attribute (Type 29).</summary>
        /// <param name="value">The IPsec split DNS domain names. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecSplitDnsNames(string value) => CreateString(CiscoAsaAttributeType.IPSEC_SPLIT_DNS_NAMES, value);

        /// <summary>Creates a Cisco-ASA-Required-Client-Firewall-Description attribute (Type 47).</summary>
        /// <param name="value">The required client firewall description. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes RequiredClientFirewallDescription(string value) => CreateString(CiscoAsaAttributeType.REQUIRED_CLIENT_FIREWALL_DESCRIPTION, value);

        /// <summary>Creates a Cisco-ASA-IPSec-Client-Firewall-Filter-Name attribute (Type 57).</summary>
        /// <param name="value">The IPsec client firewall filter name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecClientFirewallFilterName(string value) => CreateString(CiscoAsaAttributeType.IPSEC_CLIENT_FIREWALL_FILTER_NAME, value);

        /// <summary>Creates a Cisco-ASA-IPSec-Backup-Server-List attribute (Type 60).</summary>
        /// <param name="value">The IPsec backup server list. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IpsecBackupServerList(string value) => CreateString(CiscoAsaAttributeType.IPSEC_BACKUP_SERVER_LIST, value);

        /// <summary>Creates a Cisco-ASA-DHCP-Network-Scope attribute (Type 61).</summary>
        /// <param name="value">The DHCP network scope address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes DhcpNetworkScope(string value) => CreateString(CiscoAsaAttributeType.DHCP_NETWORK_SCOPE, value);

        /// <summary>Creates a Cisco-ASA-MS-Client-Subnet-Mask attribute (Type 63).</summary>
        /// <param name="value">The MS client subnet mask. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes MsClientSubnetMask(string value) => CreateString(CiscoAsaAttributeType.MS_CLIENT_SUBNET_MASK, value);

        /// <summary>Creates a Cisco-ASA-Authorization-DN-Field attribute (Type 67).</summary>
        /// <param name="value">The authorization DN field. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AuthorizationDnField(string value) => CreateString(CiscoAsaAttributeType.AUTHORIZATION_DN_FIELD, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-URL-List attribute (Type 71).</summary>
        /// <param name="value">The WebVPN URL list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnUrlList(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_URL_LIST, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Port-Forwarding-List attribute (Type 72).</summary>
        /// <param name="value">The WebVPN port forwarding list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnPortForwardingList(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_PORT_FORWARDING_LIST, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Access-List attribute (Type 73).</summary>
        /// <param name="value">The WebVPN access list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnAccessList(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_ACCESS_LIST, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-HTTP-Proxy-IP-Address attribute (Type 74).</summary>
        /// <param name="value">The WebVPN HTTP proxy IP address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnHttpProxyIpAddress(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_HTTP_PROXY_IP_ADDRESS, value);

        /// <summary>Creates a Cisco-ASA-Client-Type-Version-Limiting attribute (Type 77).</summary>
        /// <param name="value">The client type version limiting string. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes ClientTypeVersionLimiting(string value) => CreateString(CiscoAsaAttributeType.CLIENT_TYPE_VERSION_LIMITING, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Group-Based-HTTP/HTTPS-Proxy-Exception-List attribute (Type 78).</summary>
        /// <param name="value">The WebVPN group-based proxy exception list. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnGroupBasedProxyExceptionList(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_GROUP_BASED_PROXY_EXCEPTION_LIST, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Port-Forwarding-Name attribute (Type 79).</summary>
        /// <param name="value">The WebVPN port forwarding name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnPortForwardingName(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_PORT_FORWARDING_NAME, value);

        /// <summary>Creates a Cisco-ASA-IE-Proxy-Server attribute (Type 80).</summary>
        /// <param name="value">The IE proxy server address. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IeProxyServer(string value) => CreateString(CiscoAsaAttributeType.IE_PROXY_SERVER, value);

        /// <summary>Creates a Cisco-ASA-IE-Proxy-Exception-List attribute (Type 82).</summary>
        /// <param name="value">The IE proxy exception list. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes IeProxyExceptionList(string value) => CreateString(CiscoAsaAttributeType.IE_PROXY_EXCEPTION_LIST, value);

        /// <summary>Creates a Cisco-ASA-Tunnel-Group-Lock attribute (Type 85).</summary>
        /// <param name="value">The tunnel group lock name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes TunnelGroupLock(string value) => CreateString(CiscoAsaAttributeType.TUNNEL_GROUP_LOCK, value);

        /// <summary>Creates a Cisco-ASA-Access-List-Inbound attribute (Type 86).</summary>
        /// <param name="value">The inbound access list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AccessListInbound(string value) => CreateString(CiscoAsaAttributeType.ACCESS_LIST_INBOUND, value);

        /// <summary>Creates a Cisco-ASA-Access-List-Outbound attribute (Type 87).</summary>
        /// <param name="value">The outbound access list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes AccessListOutbound(string value) => CreateString(CiscoAsaAttributeType.ACCESS_LIST_OUTBOUND, value);

        /// <summary>Creates a Cisco-ASA-NAC-Default-ACL attribute (Type 92).</summary>
        /// <param name="value">The NAC default ACL name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes NacDefaultAcl(string value) => CreateString(CiscoAsaAttributeType.NAC_DEFAULT_ACL, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Auto-Applet-Download-List attribute (Type 100).</summary>
        /// <param name="value">The WebVPN auto applet download list. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnAutoAppletDownloadList(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_AUTO_APPLET_DOWNLOAD_LIST, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Homepage attribute (Type 116).</summary>
        /// <param name="value">The WebVPN homepage URL. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnHomepage(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_HOMEPAGE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Smart-Tunnel attribute (Type 136).</summary>
        /// <param name="value">The WebVPN smart tunnel list name. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnSmartTunnel(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_SMART_TUNNEL, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Smart-Tunnel-Auto-Signon-Enable attribute (Type 139).</summary>
        /// <param name="value">The WebVPN smart tunnel auto sign-on enable string. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnSmartTunnelAutoSignonEnable(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_SMART_TUNNEL_AUTO_SIGNON_ENABLE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Deny-Message attribute (Type 140).</summary>
        /// <param name="value">The WebVPN deny message. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnDenyMessage(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_DENY_MESSAGE, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Auto-HTTP-Signon attribute (Type 153).</summary>
        /// <param name="value">The WebVPN auto HTTP sign-on configuration. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnAutoHttpSignon(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_AUTO_HTTP_SIGNON, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Macro-Value1 attribute (Type 223).</summary>
        /// <param name="value">The WebVPN macro substitution value 1. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnMacroValue1(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_MACRO_VALUE1, value);

        /// <summary>Creates a Cisco-ASA-WebVPN-Macro-Value2 attribute (Type 224).</summary>
        /// <param name="value">The WebVPN macro substitution value 2. Must not be <see langword="null"/>.</param>
        public static VendorSpecificAttributes WebVpnMacroValue2(string value) => CreateString(CiscoAsaAttributeType.WEBVPN_MACRO_VALUE2, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(CiscoAsaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(CiscoAsaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}