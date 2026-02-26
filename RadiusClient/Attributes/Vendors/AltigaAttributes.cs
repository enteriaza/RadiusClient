using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Altiga / Cisco VPN 3000 (IANA PEN 3076) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.altiga</c>.
    /// </summary>
    public enum AltigaAttributeType : byte
    {
        /// <summary>Altiga-Access-Hours-G/U (Type 1). String. Access hours group/user restriction.</summary>
        ACCESS_HOURS = 1,

        /// <summary>Altiga-Simultaneous-Logins-G/U (Type 2). Integer. Maximum simultaneous logins.</summary>
        SIMULTANEOUS_LOGINS = 2,

        /// <summary>Altiga-Min-Password-Length-G (Type 3). Integer. Minimum password length.</summary>
        MIN_PASSWORD_LENGTH = 3,

        /// <summary>Altiga-Allow-Alpha-Only-Passwords-G (Type 4). Integer. Allow alpha-only passwords.</summary>
        ALLOW_ALPHA_ONLY_PASSWORDS = 4,

        /// <summary>Altiga-Primary-DNS-G (Type 5). IP address. Primary DNS server address.</summary>
        PRIMARY_DNS = 5,

        /// <summary>Altiga-Secondary-DNS-G (Type 6). IP address. Secondary DNS server address.</summary>
        SECONDARY_DNS = 6,

        /// <summary>Altiga-Primary-WINS-G (Type 7). IP address. Primary WINS server address.</summary>
        PRIMARY_WINS = 7,

        /// <summary>Altiga-Secondary-WINS-G (Type 8). IP address. Secondary WINS server address.</summary>
        SECONDARY_WINS = 8,

        /// <summary>Altiga-SEP-Card-Assignment-G/U (Type 9). Integer. SEP card assignment.</summary>
        SEP_CARD_ASSIGNMENT = 9,

        /// <summary>Altiga-Priority-On-SEP-G/U (Type 10). Integer. Priority on SEP card.</summary>
        PRIORITY_ON_SEP = 10,

        /// <summary>Altiga-Tunneling-Protocols-G/U (Type 11). Integer. Tunnelling protocols allowed.</summary>
        TUNNELING_PROTOCOLS = 11,

        /// <summary>Altiga-IPSec-Sec-Association-G/U (Type 12). String. IPsec security association name.</summary>
        IPSEC_SEC_ASSOCIATION = 12,

        /// <summary>Altiga-IPSec-Authentication-G (Type 13). Integer. IPsec authentication method.</summary>
        IPSEC_AUTHENTICATION = 13,

        /// <summary>Altiga-IPSec-Banner-G (Type 15). String. IPsec login banner text.</summary>
        IPSEC_BANNER = 15,

        /// <summary>Altiga-IPSec-Allow-Passwd-Store-G/U (Type 16). Integer. Allow password store for IPsec.</summary>
        IPSEC_ALLOW_PASSWD_STORE = 16,

        /// <summary>Altiga-Use-Client-Address-G/U (Type 17). Integer. Use client-specified address.</summary>
        USE_CLIENT_ADDRESS = 17,

        /// <summary>Altiga-PPTP-Min-Authentication-G/U (Type 18). Integer. PPTP minimum authentication protocol.</summary>
        PPTP_MIN_AUTHENTICATION = 18,

        /// <summary>Altiga-L2TP-Min-Authentication-G/U (Type 19). Integer. L2TP minimum authentication protocol.</summary>
        L2TP_MIN_AUTHENTICATION = 19,

        /// <summary>Altiga-PPTP-Encryption-G (Type 20). Integer. PPTP encryption method.</summary>
        PPTP_ENCRYPTION = 20,

        /// <summary>Altiga-L2TP-Encryption-G (Type 21). Integer. L2TP encryption method.</summary>
        L2TP_ENCRYPTION = 21,

        /// <summary>Altiga-IPSec-Split-Tunnel-List-G/U (Type 27). String. IPsec split-tunnel network list.</summary>
        IPSEC_SPLIT_TUNNEL_LIST = 27,

        /// <summary>Altiga-IPSec-Default-Domain-G/U (Type 28). String. IPsec default domain name.</summary>
        IPSEC_DEFAULT_DOMAIN = 28,

        /// <summary>Altiga-IPSec-Split-DNS-Names-G/U (Type 29). String. IPsec split-DNS domain names.</summary>
        IPSEC_SPLIT_DNS_NAMES = 29,

        /// <summary>Altiga-IPSec-Tunnel-Type-G/U (Type 30). Integer. IPsec tunnel type.</summary>
        IPSEC_TUNNEL_TYPE = 30,

        /// <summary>Altiga-IPSec-Mode-Config-G/U (Type 31). Integer. IPsec mode config enabled.</summary>
        IPSEC_MODE_CONFIG = 31,

        /// <summary>Altiga-Auth-Server-Type-G (Type 32). Integer. Authentication server type.</summary>
        AUTH_SERVER_TYPE = 32,

        /// <summary>Altiga-IPSec-Group-Name (Type 33). String. IPsec group name.</summary>
        IPSEC_GROUP_NAME = 33,

        /// <summary>Altiga-IPSec-Backup-Servers (Type 34). Integer. IPsec backup server configuration.</summary>
        IPSEC_BACKUP_SERVERS = 34,

        /// <summary>Altiga-IPSec-Backup-Server-List (Type 35). String. IPsec backup server address list.</summary>
        IPSEC_BACKUP_SERVER_LIST = 35,

        /// <summary>Altiga-DHCP-Network-Scope (Type 36). IP address. DHCP scope network address.</summary>
        DHCP_NETWORK_SCOPE = 36,

        /// <summary>Altiga-Intercept-DHCP-Configure-Msg-G (Type 37). Integer. Intercept DHCP configure messages.</summary>
        INTERCEPT_DHCP_CONFIGURE_MSG = 37,

        /// <summary>Altiga-MS-Client-Subnet-Mask (Type 38). IP address. Microsoft client subnet mask.</summary>
        MS_CLIENT_SUBNET_MASK = 38,

        /// <summary>Altiga-Allow-Network-Extension-Mode-G (Type 39). Integer. Allow network extension mode.</summary>
        ALLOW_NETWORK_EXTENSION_MODE = 39,

        /// <summary>Altiga-IPSec-Authorization-Type (Type 40). Integer. IPsec authorisation type.</summary>
        IPSEC_AUTHORIZATION_TYPE = 40,

        /// <summary>Altiga-IPSec-Authorization-Required (Type 41). Integer. IPsec authorisation required.</summary>
        IPSEC_AUTHORIZATION_REQUIRED = 41,

        /// <summary>Altiga-IPSec-DN-Field (Type 42). String. IPsec Distinguished Name field.</summary>
        IPSEC_DN_FIELD = 42,

        /// <summary>Altiga-IPSec-Confidence-Level (Type 43). Integer. IPsec confidence level.</summary>
        IPSEC_CONFIDENCE_LEVEL = 43,

        /// <summary>Altiga-WebVPN-Content-Filter-Parameters (Type 44). Integer. WebVPN content filter parameters.</summary>
        WEBVPN_CONTENT_FILTER_PARAMETERS = 44,

        /// <summary>Altiga-IPSec-Banner2-G (Type 46). String. IPsec secondary banner text.</summary>
        IPSEC_BANNER2 = 46,

        /// <summary>Altiga-PPTP-MPPC-Compression-G (Type 47). Integer. PPTP MPPC compression setting.</summary>
        PPTP_MPPC_COMPRESSION = 47,

        /// <summary>Altiga-L2TP-MPPC-Compression-G (Type 48). Integer. L2TP MPPC compression setting.</summary>
        L2TP_MPPC_COMPRESSION = 48,

        /// <summary>Altiga-IPSec-IP-Compression-G (Type 49). Integer. IPsec IP compression (IPComp).</summary>
        IPSEC_IP_COMPRESSION = 49,

        /// <summary>Altiga-IPSec-IKE-Peer-ID-Check-G (Type 50). Integer. IPsec IKE peer ID check mode.</summary>
        IPSEC_IKE_PEER_ID_CHECK = 50,

        /// <summary>Altiga-IKE-Keep-Alive-Conf-ID-G (Type 51). Integer. IKE keepalive confidence ID.</summary>
        IKE_KEEP_ALIVE_CONF_ID = 51,

        /// <summary>Altiga-IKE-Keepalive-Count-G (Type 52). Integer. IKE keepalive count.</summary>
        IKE_KEEPALIVE_COUNT = 52,

        /// <summary>Altiga-IKE-Keepalive-Interval-G (Type 53). Integer. IKE keepalive interval in seconds.</summary>
        IKE_KEEPALIVE_INTERVAL = 53,

        /// <summary>Altiga-IPSec-Over-UDP-G (Type 54). Integer. IPsec over UDP enabled.</summary>
        IPSEC_OVER_UDP = 54,

        /// <summary>Altiga-IPSec-Over-UDP-Port-G (Type 55). Integer. IPsec over UDP port number.</summary>
        IPSEC_OVER_UDP_PORT = 55,

        /// <summary>Altiga-IPSec-Banner-Regex-G (Type 56). String. IPsec banner regex pattern.</summary>
        IPSEC_BANNER_REGEX = 56,

        /// <summary>Altiga-Cisco-IP-Phone-Bypass-G (Type 57). Integer. Cisco IP Phone bypass setting.</summary>
        CISCO_IP_PHONE_BYPASS = 57,

        /// <summary>Altiga-IPSec-Split-Tunneling-Policy (Type 58). Integer. IPsec split-tunnelling policy.</summary>
        IPSEC_SPLIT_TUNNELING_POLICY = 58,

        /// <summary>Altiga-IPSec-Required-Client-Firewall-Vendor-Code (Type 59). Integer. Required client firewall vendor code.</summary>
        IPSEC_REQUIRED_CLIENT_FIREWALL_VENDOR_CODE = 59,

        /// <summary>Altiga-IPSec-Required-Client-Firewall-Product-Code (Type 60). Integer. Required client firewall product code.</summary>
        IPSEC_REQUIRED_CLIENT_FIREWALL_PRODUCT_CODE = 60,

        /// <summary>Altiga-IPSec-Required-Client-Firewall-Description (Type 61). String. Required client firewall description.</summary>
        IPSEC_REQUIRED_CLIENT_FIREWALL_DESCRIPTION = 61,

        /// <summary>Altiga-Require-HW-Client-Auth-G (Type 62). Integer. Require hardware client authentication.</summary>
        REQUIRE_HW_CLIENT_AUTH = 62,

        /// <summary>Altiga-Require-Individual-User-Auth-G (Type 63). Integer. Require individual user authentication.</summary>
        REQUIRE_INDIVIDUAL_USER_AUTH = 63,

        /// <summary>Altiga-Authenticated-User-Idle-Timeout (Type 64). Integer. Authenticated user idle timeout in minutes.</summary>
        AUTHENTICATED_USER_IDLE_TIMEOUT = 64,

        /// <summary>Altiga-Cisco-LEAP-Bypass-G (Type 65). Integer. Cisco LEAP bypass setting.</summary>
        CISCO_LEAP_BYPASS = 65,

        /// <summary>Altiga-WebVPN-Port-Forwarding-List (Type 66). String. WebVPN port forwarding list name.</summary>
        WEBVPN_PORT_FORWARDING_LIST = 66,

        /// <summary>Altiga-Client-Type-Version-Limiting (Type 67). String. Client type version limiting string.</summary>
        CLIENT_TYPE_VERSION_LIMITING = 67,

        /// <summary>Altiga-WebVPN-Group-Based-HTTP/HTTPS-Proxy-Exception-List (Type 68). String. WebVPN proxy exception list.</summary>
        WEBVPN_GROUP_BASED_HTTP_HTTPS_PROXY_EXCEPTION_LIST = 68,

        /// <summary>Altiga-WebVPN-Port-Forwarding-Name (Type 69). String. WebVPN port forwarding name.</summary>
        WEBVPN_PORT_FORWARDING_NAME = 69,

        /// <summary>Altiga-IE-Proxy-Server (Type 70). String. Internet Explorer proxy server address.</summary>
        IE_PROXY_SERVER = 70,

        /// <summary>Altiga-IE-Proxy-Server-Policy (Type 71). Integer. IE proxy server policy.</summary>
        IE_PROXY_SERVER_POLICY = 71,

        /// <summary>Altiga-IE-Proxy-Exception-List (Type 72). String. IE proxy exception list.</summary>
        IE_PROXY_EXCEPTION_LIST = 72,

        /// <summary>Altiga-IE-Proxy-Bypass-Local (Type 73). Integer. IE proxy bypass local addresses.</summary>
        IE_PROXY_BYPASS_LOCAL = 73,

        /// <summary>Altiga-IKE-Keepalive-Retry-Interval (Type 74). Integer. IKE keepalive retry interval in seconds.</summary>
        IKE_KEEPALIVE_RETRY_INTERVAL = 74,

        /// <summary>Altiga-Tunnel-Group-Lock (Type 75). String. Tunnel group lock name.</summary>
        TUNNEL_GROUP_LOCK = 75,

        /// <summary>Altiga-Access-List-Inbound (Type 76). String. Inbound access list name.</summary>
        ACCESS_LIST_INBOUND = 76,

        /// <summary>Altiga-Access-List-Outbound (Type 77). String. Outbound access list name.</summary>
        ACCESS_LIST_OUTBOUND = 77,

        /// <summary>Altiga-Perfect-Forward-Secrecy-Enable-G (Type 78). Integer. Perfect Forward Secrecy enabled.</summary>
        PERFECT_FORWARD_SECRECY_ENABLE = 78,

        /// <summary>Altiga-NAC-Enable (Type 79). Integer. Network Admission Control enabled.</summary>
        NAC_ENABLE = 79,

        /// <summary>Altiga-NAC-Status-Query-Timer (Type 80). Integer. NAC status query timer in seconds.</summary>
        NAC_STATUS_QUERY_TIMER = 80,

        /// <summary>Altiga-NAC-Revalidation-Timer (Type 81). Integer. NAC revalidation timer in seconds.</summary>
        NAC_REVALIDATION_TIMER = 81,

        /// <summary>Altiga-NAC-Default-ACL (Type 82). String. NAC default access list name.</summary>
        NAC_DEFAULT_ACL = 82,

        /// <summary>Altiga-WebVPN-URL-Entry-Enable (Type 83). Integer. WebVPN URL entry enabled.</summary>
        WEBVPN_URL_ENTRY_ENABLE = 83,

        /// <summary>Altiga-WebVPN-File-Access-Enable (Type 84). Integer. WebVPN file access enabled.</summary>
        WEBVPN_FILE_ACCESS_ENABLE = 84,

        /// <summary>Altiga-WebVPN-File-Server-Entry-Enable (Type 85). Integer. WebVPN file server entry enabled.</summary>
        WEBVPN_FILE_SERVER_ENTRY_ENABLE = 85,

        /// <summary>Altiga-WebVPN-File-Server-Browsing-Enable (Type 86). Integer. WebVPN file server browsing enabled.</summary>
        WEBVPN_FILE_SERVER_BROWSING_ENABLE = 86,

        /// <summary>Altiga-WebVPN-Port-Forwarding-Enable (Type 87). Integer. WebVPN port forwarding enabled.</summary>
        WEBVPN_PORT_FORWARDING_ENABLE = 87,

        /// <summary>Altiga-WebVPN-Outlook-Exchange-Proxy-Enable (Type 88). Integer. WebVPN Outlook/Exchange proxy enabled.</summary>
        WEBVPN_OUTLOOK_EXCHANGE_PROXY_ENABLE = 88,

        /// <summary>Altiga-WebVPN-Port-Forwarding-HTTP-Proxy (Type 89). Integer. WebVPN port forwarding HTTP proxy enabled.</summary>
        WEBVPN_PORT_FORWARDING_HTTP_PROXY = 89,

        /// <summary>Altiga-WebVPN-Auto-Applet-Download-List (Type 90). String. WebVPN auto applet download list.</summary>
        WEBVPN_AUTO_APPLET_DOWNLOAD_LIST = 90,

        /// <summary>Altiga-WebVPN-Citrix-MetaFrame-Enable (Type 91). Integer. WebVPN Citrix MetaFrame enabled.</summary>
        WEBVPN_CITRIX_METAFRAME_ENABLE = 91,

        /// <summary>Altiga-WebVPN-Apply-ACL (Type 92). Integer. WebVPN apply ACL setting.</summary>
        WEBVPN_APPLY_ACL = 92,

        /// <summary>Altiga-WebVPN-SSL-VPN-Client-Enable (Type 93). Integer. WebVPN SSL VPN client enabled.</summary>
        WEBVPN_SSL_VPN_CLIENT_ENABLE = 93,

        /// <summary>Altiga-WebVPN-SSL-VPN-Client-Required (Type 94). Integer. WebVPN SSL VPN client required.</summary>
        WEBVPN_SSL_VPN_CLIENT_REQUIRED = 94,

        /// <summary>Altiga-WebVPN-SSL-VPN-Client-Keep-Installation (Type 95). Integer. WebVPN keep SSL VPN client installation.</summary>
        WEBVPN_SSL_VPN_CLIENT_KEEP_INSTALLATION = 95,

        /// <summary>Altiga-Strip-Realm (Type 135). Integer. Strip realm from username.</summary>
        STRIP_REALM = 135,

        /// <summary>Altiga-Group-Name (Type 150). Integer. Group name identifier.</summary>
        GROUP_NAME = 150
    }

    /// <summary>
    /// Altiga-Tunneling-Protocols attribute values (Type 11).
    /// </summary>
    [Flags]
    public enum ALTIGA_TUNNELING_PROTOCOLS
    {
        /// <summary>PPTP tunnelling.</summary>
        PPTP = 1,

        /// <summary>L2TP tunnelling.</summary>
        L2TP = 2,

        /// <summary>IPsec tunnelling.</summary>
        IPSEC = 4,

        /// <summary>L2TP/IPsec tunnelling.</summary>
        L2TP_IPSEC = 8,

        /// <summary>WebVPN tunnelling.</summary>
        WEBVPN = 16
    }

    /// <summary>
    /// Altiga-IPSec-Authentication attribute values (Type 13).
    /// </summary>
    public enum ALTIGA_IPSEC_AUTHENTICATION
    {
        /// <summary>No authentication.</summary>
        NONE = 0,

        /// <summary>RADIUS authentication.</summary>
        RADIUS = 1,

        /// <summary>LDAP authentication.</summary>
        LDAP = 2,

        /// <summary>NT Domain authentication.</summary>
        NT_DOMAIN = 3,

        /// <summary>SDI / RSA SecurID authentication.</summary>
        SDI = 4,

        /// <summary>Internal (local) authentication.</summary>
        INTERNAL = 5
    }

    /// <summary>
    /// Altiga-IPSec-Tunnel-Type attribute values (Type 30).
    /// </summary>
    public enum ALTIGA_IPSEC_TUNNEL_TYPE
    {
        /// <summary>LAN-to-LAN tunnel.</summary>
        LAN_TO_LAN = 1,

        /// <summary>Remote access tunnel.</summary>
        REMOTE_ACCESS = 2
    }

    /// <summary>
    /// Altiga-IPSec-Mode-Config attribute values (Type 31).
    /// </summary>
    public enum ALTIGA_IPSEC_MODE_CONFIG
    {
        /// <summary>Mode config disabled.</summary>
        DISABLED = 0,

        /// <summary>Mode config enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Altiga-IPSec-Split-Tunneling-Policy attribute values (Type 58).
    /// </summary>
    public enum ALTIGA_IPSEC_SPLIT_TUNNELING_POLICY
    {
        /// <summary>Tunnel everything.</summary>
        NO_SPLIT = 0,

        /// <summary>Split tunnel — only specified networks go through the tunnel.</summary>
        SPLIT_TUNNEL = 1,

        /// <summary>Local LAN permitted — all traffic tunnelled except local LAN.</summary>
        LOCAL_LAN = 2
    }

    /// <summary>
    /// Altiga-IPSec-IKE-Peer-ID-Check attribute values (Type 50).
    /// </summary>
    public enum ALTIGA_IPSEC_IKE_PEER_ID_CHECK
    {
        /// <summary>Required — peer ID must match.</summary>
        REQUIRED = 1,

        /// <summary>If supported by certificate — check if available.</summary>
        IF_SUPPORTED = 2,

        /// <summary>Do not check peer ID.</summary>
        DO_NOT_CHECK = 3
    }

    /// <summary>
    /// Altiga-IE-Proxy-Server-Policy attribute values (Type 71).
    /// </summary>
    public enum ALTIGA_IE_PROXY_SERVER_POLICY
    {
        /// <summary>No modification to proxy settings.</summary>
        NO_MODIFY = 0,

        /// <summary>No proxy.</summary>
        NO_PROXY = 1,

        /// <summary>Auto-detect proxy.</summary>
        AUTO_DETECT = 2,

        /// <summary>Use concentrator setting.</summary>
        USE_CONCENTRATOR = 3
    }

    /// <summary>
    /// Altiga-Allow-Alpha-Only-Passwords attribute values (Type 4).
    /// </summary>
    public enum ALTIGA_ALLOW_ALPHA_ONLY_PASSWORDS
    {
        /// <summary>Alpha-only passwords not allowed.</summary>
        DISALLOW = 0,

        /// <summary>Alpha-only passwords allowed.</summary>
        ALLOW = 1
    }

    /// <summary>
    /// Altiga-IPSec-Backup-Servers attribute values (Type 34).
    /// </summary>
    public enum ALTIGA_IPSEC_BACKUP_SERVERS
    {
        /// <summary>Use client-configured list.</summary>
        USE_CLIENT_CONFIGURED = 1,

        /// <summary>Disable and clear client list.</summary>
        DISABLE_AND_CLEAR = 2,

        /// <summary>Use backup server list.</summary>
        USE_BACKUP_LIST = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Altiga / Cisco VPN 3000
    /// (IANA PEN 3076) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.altiga</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Altiga's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3076</c>.
    /// </para>
    /// <para>
    /// These attributes are used by the Cisco VPN 3000 Concentrator (formerly Altiga
    /// Networks) for VPN tunnel configuration, IPsec/PPTP/L2TP settings, WebVPN
    /// parameters, split-tunnelling, IKE keepalive, NAC, firewall requirements,
    /// and user privilege management.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AltigaAttributes.TunnelingProtocols(ALTIGA_TUNNELING_PROTOCOLS.IPSEC));
    /// packet.SetAttribute(AltigaAttributes.IpsecSplitTunnelList("10.0.0.0/8"));
    /// packet.SetAttribute(AltigaAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(AltigaAttributes.SimultaneousLogins(3));
    /// </code>
    /// </remarks>
    public static class AltigaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Altiga Networks (Cisco VPN 3000).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3076;

        #region Integer Attributes

        /// <summary>
        /// Creates an Altiga-Simultaneous-Logins attribute (Type 2) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum simultaneous logins.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SimultaneousLogins(int value)
        {
            return CreateInteger(AltigaAttributeType.SIMULTANEOUS_LOGINS, value);
        }

        /// <summary>
        /// Creates an Altiga-Min-Password-Length attribute (Type 3) with the specified minimum.
        /// </summary>
        /// <param name="value">The minimum password length.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MinPasswordLength(int value)
        {
            return CreateInteger(AltigaAttributeType.MIN_PASSWORD_LENGTH, value);
        }

        /// <summary>
        /// Creates an Altiga-Allow-Alpha-Only-Passwords attribute (Type 4) with the specified setting.
        /// </summary>
        /// <param name="value">Whether alpha-only passwords are allowed. See <see cref="ALTIGA_ALLOW_ALPHA_ONLY_PASSWORDS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AllowAlphaOnlyPasswords(ALTIGA_ALLOW_ALPHA_ONLY_PASSWORDS value)
        {
            return CreateInteger(AltigaAttributeType.ALLOW_ALPHA_ONLY_PASSWORDS, (int)value);
        }

        /// <summary>
        /// Creates an Altiga-SEP-Card-Assignment attribute (Type 9) with the specified assignment.
        /// </summary>
        /// <param name="value">The SEP card assignment.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SepCardAssignment(int value)
        {
            return CreateInteger(AltigaAttributeType.SEP_CARD_ASSIGNMENT, value);
        }

        /// <summary>
        /// Creates an Altiga-Priority-On-SEP attribute (Type 10) with the specified priority.
        /// </summary>
        /// <param name="value">The priority on the SEP card.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PriorityOnSep(int value)
        {
            return CreateInteger(AltigaAttributeType.PRIORITY_ON_SEP, value);
        }

        /// <summary>
        /// Creates an Altiga-Tunneling-Protocols attribute (Type 11) with the specified protocols.
        /// </summary>
        /// <param name="value">The tunnelling protocols allowed. See <see cref="ALTIGA_TUNNELING_PROTOCOLS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelingProtocols(ALTIGA_TUNNELING_PROTOCOLS value)
        {
            return CreateInteger(AltigaAttributeType.TUNNELING_PROTOCOLS, (int)value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Authentication attribute (Type 13) with the specified method.
        /// </summary>
        /// <param name="value">The IPsec authentication method. See <see cref="ALTIGA_IPSEC_AUTHENTICATION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecAuthentication(ALTIGA_IPSEC_AUTHENTICATION value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_AUTHENTICATION, (int)value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Allow-Passwd-Store attribute (Type 16) with the specified setting.
        /// </summary>
        /// <param name="value">Whether password store is allowed (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecAllowPasswdStore(int value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_ALLOW_PASSWD_STORE, value);
        }

        /// <summary>
        /// Creates an Altiga-Use-Client-Address attribute (Type 17) with the specified setting.
        /// </summary>
        /// <param name="value">Whether to use client-specified address (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UseClientAddress(int value)
        {
            return CreateInteger(AltigaAttributeType.USE_CLIENT_ADDRESS, value);
        }

        /// <summary>
        /// Creates an Altiga-PPTP-Min-Authentication attribute (Type 18) with the specified protocol.
        /// </summary>
        /// <param name="value">The PPTP minimum authentication protocol.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PptpMinAuthentication(int value)
        {
            return CreateInteger(AltigaAttributeType.PPTP_MIN_AUTHENTICATION, value);
        }

        /// <summary>
        /// Creates an Altiga-L2TP-Min-Authentication attribute (Type 19) with the specified protocol.
        /// </summary>
        /// <param name="value">The L2TP minimum authentication protocol.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes L2tpMinAuthentication(int value)
        {
            return CreateInteger(AltigaAttributeType.L2TP_MIN_AUTHENTICATION, value);
        }

        /// <summary>
        /// Creates an Altiga-PPTP-Encryption attribute (Type 20) with the specified method.
        /// </summary>
        /// <param name="value">The PPTP encryption method.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PptpEncryption(int value)
        {
            return CreateInteger(AltigaAttributeType.PPTP_ENCRYPTION, value);
        }

        /// <summary>
        /// Creates an Altiga-L2TP-Encryption attribute (Type 21) with the specified method.
        /// </summary>
        /// <param name="value">The L2TP encryption method.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes L2tpEncryption(int value)
        {
            return CreateInteger(AltigaAttributeType.L2TP_ENCRYPTION, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Tunnel-Type attribute (Type 30) with the specified tunnel type.
        /// </summary>
        /// <param name="value">The IPsec tunnel type. See <see cref="ALTIGA_IPSEC_TUNNEL_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecTunnelType(ALTIGA_IPSEC_TUNNEL_TYPE value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_TUNNEL_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Mode-Config attribute (Type 31) with the specified setting.
        /// </summary>
        /// <param name="value">Whether mode config is enabled. See <see cref="ALTIGA_IPSEC_MODE_CONFIG"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecModeConfig(ALTIGA_IPSEC_MODE_CONFIG value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_MODE_CONFIG, (int)value);
        }

        /// <summary>
        /// Creates an Altiga-Auth-Server-Type attribute (Type 32) with the specified server type.
        /// </summary>
        /// <param name="value">The authentication server type.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthServerType(int value)
        {
            return CreateInteger(AltigaAttributeType.AUTH_SERVER_TYPE, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Backup-Servers attribute (Type 34) with the specified configuration.
        /// </summary>
        /// <param name="value">The backup server configuration. See <see cref="ALTIGA_IPSEC_BACKUP_SERVERS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecBackupServers(ALTIGA_IPSEC_BACKUP_SERVERS value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_BACKUP_SERVERS, (int)value);
        }

        /// <summary>
        /// Creates an Altiga-Intercept-DHCP-Configure-Msg attribute (Type 37) with the specified setting.
        /// </summary>
        /// <param name="value">Whether to intercept DHCP configure messages (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes InterceptDhcpConfigureMsg(int value)
        {
            return CreateInteger(AltigaAttributeType.INTERCEPT_DHCP_CONFIGURE_MSG, value);
        }

        /// <summary>
        /// Creates an Altiga-Allow-Network-Extension-Mode attribute (Type 39) with the specified setting.
        /// </summary>
        /// <param name="value">Whether network extension mode is allowed (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AllowNetworkExtensionMode(int value)
        {
            return CreateInteger(AltigaAttributeType.ALLOW_NETWORK_EXTENSION_MODE, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Authorization-Type attribute (Type 40) with the specified type.
        /// </summary>
        /// <param name="value">The IPsec authorisation type.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecAuthorizationType(int value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_AUTHORIZATION_TYPE, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Authorization-Required attribute (Type 41) with the specified setting.
        /// </summary>
        /// <param name="value">Whether IPsec authorisation is required (0 = no, 1 = yes).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecAuthorizationRequired(int value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_AUTHORIZATION_REQUIRED, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Confidence-Level attribute (Type 43) with the specified level.
        /// </summary>
        /// <param name="value">The IPsec confidence level.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecConfidenceLevel(int value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_CONFIDENCE_LEVEL, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-Content-Filter-Parameters attribute (Type 44) with the specified parameters.
        /// </summary>
        /// <param name="value">The WebVPN content filter parameters bitmask.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnContentFilterParameters(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_CONTENT_FILTER_PARAMETERS, value);
        }

        /// <summary>
        /// Creates an Altiga-PPTP-MPPC-Compression attribute (Type 47) with the specified setting.
        /// </summary>
        /// <param name="value">The PPTP MPPC compression setting (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PptpMppcCompression(int value)
        {
            return CreateInteger(AltigaAttributeType.PPTP_MPPC_COMPRESSION, value);
        }

        /// <summary>
        /// Creates an Altiga-L2TP-MPPC-Compression attribute (Type 48) with the specified setting.
        /// </summary>
        /// <param name="value">The L2TP MPPC compression setting (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes L2tpMppcCompression(int value)
        {
            return CreateInteger(AltigaAttributeType.L2TP_MPPC_COMPRESSION, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-IP-Compression attribute (Type 49) with the specified setting.
        /// </summary>
        /// <param name="value">The IPsec IP compression (IPComp) setting (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecIpCompression(int value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_IP_COMPRESSION, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-IKE-Peer-ID-Check attribute (Type 50) with the specified check mode.
        /// </summary>
        /// <param name="value">The IKE peer ID check mode. See <see cref="ALTIGA_IPSEC_IKE_PEER_ID_CHECK"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecIkePeerIdCheck(ALTIGA_IPSEC_IKE_PEER_ID_CHECK value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_IKE_PEER_ID_CHECK, (int)value);
        }

        /// <summary>
        /// Creates an Altiga-IKE-Keep-Alive-Conf-ID attribute (Type 51) with the specified confidence ID.
        /// </summary>
        /// <param name="value">The IKE keepalive confidence ID.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IkeKeepAliveConfId(int value)
        {
            return CreateInteger(AltigaAttributeType.IKE_KEEP_ALIVE_CONF_ID, value);
        }

        /// <summary>
        /// Creates an Altiga-IKE-Keepalive-Count attribute (Type 52) with the specified count.
        /// </summary>
        /// <param name="value">The IKE keepalive count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IkeKeepaliveCount(int value)
        {
            return CreateInteger(AltigaAttributeType.IKE_KEEPALIVE_COUNT, value);
        }

        /// <summary>
        /// Creates an Altiga-IKE-Keepalive-Interval attribute (Type 53) with the specified interval.
        /// </summary>
        /// <param name="value">The IKE keepalive interval in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IkeKeepaliveInterval(int value)
        {
            return CreateInteger(AltigaAttributeType.IKE_KEEPALIVE_INTERVAL, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Over-UDP attribute (Type 54) with the specified setting.
        /// </summary>
        /// <param name="value">Whether IPsec over UDP is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecOverUdp(int value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_OVER_UDP, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Over-UDP-Port attribute (Type 55) with the specified port.
        /// </summary>
        /// <param name="value">The IPsec over UDP port number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecOverUdpPort(int value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_OVER_UDP_PORT, value);
        }

        /// <summary>
        /// Creates an Altiga-Cisco-IP-Phone-Bypass attribute (Type 57) with the specified setting.
        /// </summary>
        /// <param name="value">The Cisco IP Phone bypass setting (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CiscoIpPhoneBypass(int value)
        {
            return CreateInteger(AltigaAttributeType.CISCO_IP_PHONE_BYPASS, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Split-Tunneling-Policy attribute (Type 58) with the specified policy.
        /// </summary>
        /// <param name="value">The split-tunnelling policy. See <see cref="ALTIGA_IPSEC_SPLIT_TUNNELING_POLICY"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecSplitTunnelingPolicy(ALTIGA_IPSEC_SPLIT_TUNNELING_POLICY value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_SPLIT_TUNNELING_POLICY, (int)value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Required-Client-Firewall-Vendor-Code attribute (Type 59) with the specified code.
        /// </summary>
        /// <param name="value">The required client firewall vendor code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecRequiredClientFirewallVendorCode(int value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_REQUIRED_CLIENT_FIREWALL_VENDOR_CODE, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Required-Client-Firewall-Product-Code attribute (Type 60) with the specified code.
        /// </summary>
        /// <param name="value">The required client firewall product code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IpsecRequiredClientFirewallProductCode(int value)
        {
            return CreateInteger(AltigaAttributeType.IPSEC_REQUIRED_CLIENT_FIREWALL_PRODUCT_CODE, value);
        }

        /// <summary>
        /// Creates an Altiga-Require-HW-Client-Auth attribute (Type 62) with the specified setting.
        /// </summary>
        /// <param name="value">Whether hardware client authentication is required (0 = no, 1 = yes).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RequireHwClientAuth(int value)
        {
            return CreateInteger(AltigaAttributeType.REQUIRE_HW_CLIENT_AUTH, value);
        }

        /// <summary>
        /// Creates an Altiga-Require-Individual-User-Auth attribute (Type 63) with the specified setting.
        /// </summary>
        /// <param name="value">Whether individual user authentication is required (0 = no, 1 = yes).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RequireIndividualUserAuth(int value)
        {
            return CreateInteger(AltigaAttributeType.REQUIRE_INDIVIDUAL_USER_AUTH, value);
        }

        /// <summary>
        /// Creates an Altiga-Authenticated-User-Idle-Timeout attribute (Type 64) with the specified timeout.
        /// </summary>
        /// <param name="value">The authenticated user idle timeout in minutes.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthenticatedUserIdleTimeout(int value)
        {
            return CreateInteger(AltigaAttributeType.AUTHENTICATED_USER_IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates an Altiga-Cisco-LEAP-Bypass attribute (Type 65) with the specified setting.
        /// </summary>
        /// <param name="value">The Cisco LEAP bypass setting (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CiscoLeapBypass(int value)
        {
            return CreateInteger(AltigaAttributeType.CISCO_LEAP_BYPASS, value);
        }

        /// <summary>
        /// Creates an Altiga-IE-Proxy-Server-Policy attribute (Type 71) with the specified policy.
        /// </summary>
        /// <param name="value">The IE proxy server policy. See <see cref="ALTIGA_IE_PROXY_SERVER_POLICY"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IeProxyServerPolicy(ALTIGA_IE_PROXY_SERVER_POLICY value)
        {
            return CreateInteger(AltigaAttributeType.IE_PROXY_SERVER_POLICY, (int)value);
        }

        /// <summary>
        /// Creates an Altiga-IE-Proxy-Bypass-Local attribute (Type 73) with the specified setting.
        /// </summary>
        /// <param name="value">Whether IE proxy bypasses local addresses (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IeProxyBypassLocal(int value)
        {
            return CreateInteger(AltigaAttributeType.IE_PROXY_BYPASS_LOCAL, value);
        }

        /// <summary>
        /// Creates an Altiga-IKE-Keepalive-Retry-Interval attribute (Type 74) with the specified interval.
        /// </summary>
        /// <param name="value">The IKE keepalive retry interval in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IkeKeepaliveRetryInterval(int value)
        {
            return CreateInteger(AltigaAttributeType.IKE_KEEPALIVE_RETRY_INTERVAL, value);
        }

        /// <summary>
        /// Creates an Altiga-Perfect-Forward-Secrecy-Enable attribute (Type 78) with the specified setting.
        /// </summary>
        /// <param name="value">Whether PFS is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PerfectForwardSecrecyEnable(int value)
        {
            return CreateInteger(AltigaAttributeType.PERFECT_FORWARD_SECRECY_ENABLE, value);
        }

        /// <summary>
        /// Creates an Altiga-NAC-Enable attribute (Type 79) with the specified setting.
        /// </summary>
        /// <param name="value">Whether NAC is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NacEnable(int value)
        {
            return CreateInteger(AltigaAttributeType.NAC_ENABLE, value);
        }

        /// <summary>
        /// Creates an Altiga-NAC-Status-Query-Timer attribute (Type 80) with the specified timer.
        /// </summary>
        /// <param name="value">The NAC status query timer in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NacStatusQueryTimer(int value)
        {
            return CreateInteger(AltigaAttributeType.NAC_STATUS_QUERY_TIMER, value);
        }

        /// <summary>
        /// Creates an Altiga-NAC-Revalidation-Timer attribute (Type 81) with the specified timer.
        /// </summary>
        /// <param name="value">The NAC revalidation timer in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NacRevalidationTimer(int value)
        {
            return CreateInteger(AltigaAttributeType.NAC_REVALIDATION_TIMER, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-URL-Entry-Enable attribute (Type 83) with the specified setting.
        /// </summary>
        /// <param name="value">Whether WebVPN URL entry is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnUrlEntryEnable(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_URL_ENTRY_ENABLE, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-File-Access-Enable attribute (Type 84) with the specified setting.
        /// </summary>
        /// <param name="value">Whether WebVPN file access is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnFileAccessEnable(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_FILE_ACCESS_ENABLE, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-File-Server-Entry-Enable attribute (Type 85) with the specified setting.
        /// </summary>
        /// <param name="value">Whether WebVPN file server entry is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnFileServerEntryEnable(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_FILE_SERVER_ENTRY_ENABLE, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-File-Server-Browsing-Enable attribute (Type 86) with the specified setting.
        /// </summary>
        /// <param name="value">Whether WebVPN file server browsing is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnFileServerBrowsingEnable(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_FILE_SERVER_BROWSING_ENABLE, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-Port-Forwarding-Enable attribute (Type 87) with the specified setting.
        /// </summary>
        /// <param name="value">Whether WebVPN port forwarding is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnPortForwardingEnable(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_PORT_FORWARDING_ENABLE, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-Outlook-Exchange-Proxy-Enable attribute (Type 88) with the specified setting.
        /// </summary>
        /// <param name="value">Whether WebVPN Outlook/Exchange proxy is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnOutlookExchangeProxyEnable(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_OUTLOOK_EXCHANGE_PROXY_ENABLE, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-Port-Forwarding-HTTP-Proxy attribute (Type 89) with the specified setting.
        /// </summary>
        /// <param name="value">Whether WebVPN port forwarding HTTP proxy is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnPortForwardingHttpProxy(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_PORT_FORWARDING_HTTP_PROXY, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-Citrix-MetaFrame-Enable attribute (Type 91) with the specified setting.
        /// </summary>
        /// <param name="value">Whether WebVPN Citrix MetaFrame is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnCitrixMetaFrameEnable(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_CITRIX_METAFRAME_ENABLE, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-Apply-ACL attribute (Type 92) with the specified setting.
        /// </summary>
        /// <param name="value">The WebVPN apply ACL setting.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnApplyAcl(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_APPLY_ACL, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-SSL-VPN-Client-Enable attribute (Type 93) with the specified setting.
        /// </summary>
        /// <param name="value">Whether WebVPN SSL VPN client is enabled (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnSslVpnClientEnable(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_SSL_VPN_CLIENT_ENABLE, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-SSL-VPN-Client-Required attribute (Type 94) with the specified setting.
        /// </summary>
        /// <param name="value">Whether WebVPN SSL VPN client is required (0 = no, 1 = yes).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnSslVpnClientRequired(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_SSL_VPN_CLIENT_REQUIRED, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-SSL-VPN-Client-Keep-Installation attribute (Type 95) with the specified setting.
        /// </summary>
        /// <param name="value">Whether to keep SSL VPN client installation (0 = no, 1 = yes).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WebvpnSslVpnClientKeepInstallation(int value)
        {
            return CreateInteger(AltigaAttributeType.WEBVPN_SSL_VPN_CLIENT_KEEP_INSTALLATION, value);
        }

        /// <summary>
        /// Creates an Altiga-Strip-Realm attribute (Type 135) with the specified setting.
        /// </summary>
        /// <param name="value">Whether to strip realm from username (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes StripRealm(int value)
        {
            return CreateInteger(AltigaAttributeType.STRIP_REALM, value);
        }

        /// <summary>
        /// Creates an Altiga-Group-Name attribute (Type 150) with the specified identifier.
        /// </summary>
        /// <param name="value">The group name identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes GroupName(int value)
        {
            return CreateInteger(AltigaAttributeType.GROUP_NAME, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Altiga-Access-Hours attribute (Type 1) with the specified restriction.
        /// </summary>
        /// <param name="value">The access hours group/user restriction. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessHours(string value)
        {
            return CreateString(AltigaAttributeType.ACCESS_HOURS, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Sec-Association attribute (Type 12) with the specified SA name.
        /// </summary>
        /// <param name="value">The IPsec security association name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecSecAssociation(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_SEC_ASSOCIATION, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Banner attribute (Type 15) with the specified banner text.
        /// </summary>
        /// <param name="value">The IPsec login banner text. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecBanner(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_BANNER, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Split-Tunnel-List attribute (Type 27) with the specified network list.
        /// </summary>
        /// <param name="value">The IPsec split-tunnel network list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecSplitTunnelList(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_SPLIT_TUNNEL_LIST, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Default-Domain attribute (Type 28) with the specified domain name.
        /// </summary>
        /// <param name="value">The IPsec default domain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecDefaultDomain(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_DEFAULT_DOMAIN, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Split-DNS-Names attribute (Type 29) with the specified domain names.
        /// </summary>
        /// <param name="value">The IPsec split-DNS domain names. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecSplitDnsNames(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_SPLIT_DNS_NAMES, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Group-Name attribute (Type 33) with the specified group name.
        /// </summary>
        /// <param name="value">The IPsec group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecGroupName(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_GROUP_NAME, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Backup-Server-List attribute (Type 35) with the specified address list.
        /// </summary>
        /// <param name="value">The IPsec backup server address list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecBackupServerList(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_BACKUP_SERVER_LIST, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-DN-Field attribute (Type 42) with the specified DN field.
        /// </summary>
        /// <param name="value">The IPsec Distinguished Name field. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecDnField(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_DN_FIELD, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Banner2 attribute (Type 46) with the specified banner text.
        /// </summary>
        /// <param name="value">The IPsec secondary banner text. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecBanner2(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_BANNER2, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Banner-Regex attribute (Type 56) with the specified regex pattern.
        /// </summary>
        /// <param name="value">The IPsec banner regex pattern. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecBannerRegex(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_BANNER_REGEX, value);
        }

        /// <summary>
        /// Creates an Altiga-IPSec-Required-Client-Firewall-Description attribute (Type 61) with the specified description.
        /// </summary>
        /// <param name="value">The required client firewall description. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecRequiredClientFirewallDescription(string value)
        {
            return CreateString(AltigaAttributeType.IPSEC_REQUIRED_CLIENT_FIREWALL_DESCRIPTION, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-Port-Forwarding-List attribute (Type 66) with the specified list name.
        /// </summary>
        /// <param name="value">The WebVPN port forwarding list name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WebvpnPortForwardingList(string value)
        {
            return CreateString(AltigaAttributeType.WEBVPN_PORT_FORWARDING_LIST, value);
        }

        /// <summary>
        /// Creates an Altiga-Client-Type-Version-Limiting attribute (Type 67) with the specified version string.
        /// </summary>
        /// <param name="value">The client type version limiting string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientTypeVersionLimiting(string value)
        {
            return CreateString(AltigaAttributeType.CLIENT_TYPE_VERSION_LIMITING, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-Group-Based-HTTP/HTTPS-Proxy-Exception-List attribute (Type 68) with the specified list.
        /// </summary>
        /// <param name="value">The WebVPN proxy exception list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WebvpnGroupBasedHttpHttpsProxyExceptionList(string value)
        {
            return CreateString(AltigaAttributeType.WEBVPN_GROUP_BASED_HTTP_HTTPS_PROXY_EXCEPTION_LIST, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-Port-Forwarding-Name attribute (Type 69) with the specified name.
        /// </summary>
        /// <param name="value">The WebVPN port forwarding name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WebvpnPortForwardingName(string value)
        {
            return CreateString(AltigaAttributeType.WEBVPN_PORT_FORWARDING_NAME, value);
        }

        /// <summary>
        /// Creates an Altiga-IE-Proxy-Server attribute (Type 70) with the specified address.
        /// </summary>
        /// <param name="value">The Internet Explorer proxy server address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IeProxyServer(string value)
        {
            return CreateString(AltigaAttributeType.IE_PROXY_SERVER, value);
        }

        /// <summary>
        /// Creates an Altiga-IE-Proxy-Exception-List attribute (Type 72) with the specified list.
        /// </summary>
        /// <param name="value">The IE proxy exception list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IeProxyExceptionList(string value)
        {
            return CreateString(AltigaAttributeType.IE_PROXY_EXCEPTION_LIST, value);
        }

        /// <summary>
        /// Creates an Altiga-Tunnel-Group-Lock attribute (Type 75) with the specified lock name.
        /// </summary>
        /// <param name="value">The tunnel group lock name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelGroupLock(string value)
        {
            return CreateString(AltigaAttributeType.TUNNEL_GROUP_LOCK, value);
        }

        /// <summary>
        /// Creates an Altiga-Access-List-Inbound attribute (Type 76) with the specified ACL name.
        /// </summary>
        /// <param name="value">The inbound access list name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessListInbound(string value)
        {
            return CreateString(AltigaAttributeType.ACCESS_LIST_INBOUND, value);
        }

        /// <summary>
        /// Creates an Altiga-Access-List-Outbound attribute (Type 77) with the specified ACL name.
        /// </summary>
        /// <param name="value">The outbound access list name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessListOutbound(string value)
        {
            return CreateString(AltigaAttributeType.ACCESS_LIST_OUTBOUND, value);
        }

        /// <summary>
        /// Creates an Altiga-NAC-Default-ACL attribute (Type 82) with the specified ACL name.
        /// </summary>
        /// <param name="value">The NAC default access list name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NacDefaultAcl(string value)
        {
            return CreateString(AltigaAttributeType.NAC_DEFAULT_ACL, value);
        }

        /// <summary>
        /// Creates an Altiga-WebVPN-Auto-Applet-Download-List attribute (Type 90) with the specified list.
        /// </summary>
        /// <param name="value">The WebVPN auto applet download list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WebvpnAutoAppletDownloadList(string value)
        {
            return CreateString(AltigaAttributeType.WEBVPN_AUTO_APPLET_DOWNLOAD_LIST, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Altiga-Primary-DNS attribute (Type 5) with the specified IPv4 address.
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
            return CreateIpv4(AltigaAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates an Altiga-Secondary-DNS attribute (Type 6) with the specified IPv4 address.
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
            return CreateIpv4(AltigaAttributeType.SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates an Altiga-Primary-WINS attribute (Type 7) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The primary WINS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryWins(IPAddress value)
        {
            return CreateIpv4(AltigaAttributeType.PRIMARY_WINS, value);
        }

        /// <summary>
        /// Creates an Altiga-Secondary-WINS attribute (Type 8) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The secondary WINS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryWins(IPAddress value)
        {
            return CreateIpv4(AltigaAttributeType.SECONDARY_WINS, value);
        }

        /// <summary>
        /// Creates an Altiga-DHCP-Network-Scope attribute (Type 36) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The DHCP scope network address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes DhcpNetworkScope(IPAddress value)
        {
            return CreateIpv4(AltigaAttributeType.DHCP_NETWORK_SCOPE, value);
        }

        /// <summary>
        /// Creates an Altiga-MS-Client-Subnet-Mask attribute (Type 38) with the specified subnet mask.
        /// </summary>
        /// <param name="value">
        /// The Microsoft client subnet mask. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes MsClientSubnetMask(IPAddress value)
        {
            return CreateIpv4(AltigaAttributeType.MS_CLIENT_SUBNET_MASK, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Altiga attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AltigaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Altiga attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AltigaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified Altiga attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(AltigaAttributeType type, IPAddress value)
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