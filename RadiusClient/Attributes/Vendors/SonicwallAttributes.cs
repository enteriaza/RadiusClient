using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a SonicWall (IANA PEN 8741) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.sonicwall</c>.
    /// </summary>
    /// <remarks>
    /// SonicWall (formerly a Dell subsidiary, now owned by private equity) produces
    /// next-generation firewalls (TZ, NSA, NSsp, SuperMassive series), VPN
    /// concentrators, secure mobile access (SMA) gateways, email security, and
    /// cloud application security platforms.
    /// </remarks>
    public enum SonicwallAttributeType : byte
    {
        /// <summary>SonicWall-User-Privilege (Type 1). Integer. User privilege level.</summary>
        USER_PRIVILEGE = 1,

        /// <summary>SonicWall-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>SonicWall-AVPair (Type 3). String. Attribute-value pair string.</summary>
        AVPAIR = 3,

        /// <summary>SonicWall-Inactivity-Timeout (Type 4). Integer. Inactivity timeout in minutes.</summary>
        INACTIVITY_TIMEOUT = 4,

        /// <summary>SonicWall-SSLVPN-ACL (Type 5). String. SSL VPN access control list.</summary>
        SSLVPN_ACL = 5,

        /// <summary>SonicWall-CFS-Policy (Type 6). Integer. Content Filtering Service policy ID.</summary>
        CFS_POLICY = 6,

        /// <summary>SonicWall-Bookmark-Name (Type 7). String. SSL VPN bookmark name.</summary>
        BOOKMARK_NAME = 7,

        /// <summary>SonicWall-Bookmark-URL (Type 8). String. SSL VPN bookmark URL.</summary>
        BOOKMARK_URL = 8,

        /// <summary>SonicWall-SSLVPN-Domain (Type 9). String. SSL VPN domain name.</summary>
        SSLVPN_DOMAIN = 9,

        /// <summary>SonicWall-Redirect-URL (Type 10). String. Redirect URL.</summary>
        REDIRECT_URL = 10,

        /// <summary>SonicWall-SSLVPN-Custom-Realm (Type 11). String. SSL VPN custom realm name.</summary>
        SSLVPN_CUSTOM_REALM = 11,

        /// <summary>SonicWall-Session-Lifetime (Type 12). Integer. Session lifetime in minutes.</summary>
        SESSION_LIFETIME = 12,

        /// <summary>SonicWall-Client-IP (Type 13). IP address. Client IP address assignment.</summary>
        CLIENT_IP = 13,

        /// <summary>SonicWall-Client-Netmask (Type 14). IP address. Client subnet mask.</summary>
        CLIENT_NETMASK = 14,

        /// <summary>SonicWall-Client-DNS-Primary (Type 15). IP address. Client primary DNS server.</summary>
        CLIENT_DNS_PRIMARY = 15,

        /// <summary>SonicWall-Client-DNS-Secondary (Type 16). IP address. Client secondary DNS server.</summary>
        CLIENT_DNS_SECONDARY = 16,

        /// <summary>SonicWall-Client-WINS-Primary (Type 17). IP address. Client primary WINS server.</summary>
        CLIENT_WINS_PRIMARY = 17,

        /// <summary>SonicWall-Client-WINS-Secondary (Type 18). IP address. Client secondary WINS server.</summary>
        CLIENT_WINS_SECONDARY = 18,

        /// <summary>SonicWall-Client-Domain-Name (Type 19). String. Client domain name.</summary>
        CLIENT_DOMAIN_NAME = 19,

        /// <summary>SonicWall-Client-Routes (Type 20). String. Client VPN routes.</summary>
        CLIENT_ROUTES = 20,

        /// <summary>SonicWall-SSLVPN-NetExtender-Client-Routes (Type 21). String. NetExtender client routes.</summary>
        SSLVPN_NETEXTENDER_CLIENT_ROUTES = 21,

        /// <summary>SonicWall-SSLVPN-IP-Pool (Type 22). String. SSL VPN IP pool name.</summary>
        SSLVPN_IP_POOL = 22
    }

    /// <summary>
    /// SonicWall-User-Privilege attribute values (Type 1).
    /// </summary>
    public enum SONICWALL_USER_PRIVILEGE
    {
        /// <summary>Standard user (limited access).</summary>
        USER = 0,

        /// <summary>Limited administrator.</summary>
        LIMITED_ADMIN = 1,

        /// <summary>Full administrator access.</summary>
        ADMIN = 2,

        /// <summary>Guest user (restricted access).</summary>
        GUEST = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing SonicWall
    /// (IANA PEN 8741) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.sonicwall</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// SonicWall's vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 8741</c>.
    /// </para>
    /// <para>
    /// These attributes are used by SonicWall next-generation firewalls (TZ, NSA,
    /// NSsp, SuperMassive) and Secure Mobile Access (SMA) gateways for RADIUS-based
    /// user privilege level and group assignment, SSL VPN configuration (ACL, domain,
    /// custom realm, bookmarks, NetExtender routes, IP pool), VPN client provisioning
    /// (IP, netmask, DNS, WINS, domain name, routes), Content Filtering Service
    /// policy selection, inactivity timeout and session lifetime management, URL
    /// redirection, and general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(SonicwallAttributes.UserPrivilege(SONICWALL_USER_PRIVILEGE.ADMIN));
    /// packet.SetAttribute(SonicwallAttributes.UserGroup("VPN-Users"));
    /// packet.SetAttribute(SonicwallAttributes.SslvpnDomain("corporate"));
    /// packet.SetAttribute(SonicwallAttributes.ClientIp(IPAddress.Parse("10.10.0.100")));
    /// packet.SetAttribute(SonicwallAttributes.ClientDnsPrimary(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(SonicwallAttributes.SessionLifetime(480));
    /// </code>
    /// </remarks>
    public static class SonicwallAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for SonicWall.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 8741;

        #region Integer Attributes

        /// <summary>Creates a SonicWall-User-Privilege attribute (Type 1).</summary>
        /// <param name="value">The user privilege level. See <see cref="SONICWALL_USER_PRIVILEGE"/>.</param>
        public static VendorSpecificAttributes UserPrivilege(SONICWALL_USER_PRIVILEGE value)
        {
            return CreateInteger(SonicwallAttributeType.USER_PRIVILEGE, (int)value);
        }

        /// <summary>Creates a SonicWall-Inactivity-Timeout attribute (Type 4).</summary>
        /// <param name="value">The inactivity timeout in minutes.</param>
        public static VendorSpecificAttributes InactivityTimeout(int value)
        {
            return CreateInteger(SonicwallAttributeType.INACTIVITY_TIMEOUT, value);
        }

        /// <summary>Creates a SonicWall-CFS-Policy attribute (Type 6).</summary>
        /// <param name="value">The Content Filtering Service policy ID.</param>
        public static VendorSpecificAttributes CfsPolicy(int value)
        {
            return CreateInteger(SonicwallAttributeType.CFS_POLICY, value);
        }

        /// <summary>Creates a SonicWall-Session-Lifetime attribute (Type 12).</summary>
        /// <param name="value">The session lifetime in minutes.</param>
        public static VendorSpecificAttributes SessionLifetime(int value)
        {
            return CreateInteger(SonicwallAttributeType.SESSION_LIFETIME, value);
        }

        #endregion

        #region String Attributes

        /// <summary>Creates a SonicWall-User-Group attribute (Type 2).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value) => CreateString(SonicwallAttributeType.USER_GROUP, value);

        /// <summary>Creates a SonicWall-AVPair attribute (Type 3).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(SonicwallAttributeType.AVPAIR, value);

        /// <summary>Creates a SonicWall-SSLVPN-ACL attribute (Type 5).</summary>
        /// <param name="value">The SSL VPN access control list. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SslvpnAcl(string value) => CreateString(SonicwallAttributeType.SSLVPN_ACL, value);

        /// <summary>Creates a SonicWall-Bookmark-Name attribute (Type 7).</summary>
        /// <param name="value">The SSL VPN bookmark name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BookmarkName(string value) => CreateString(SonicwallAttributeType.BOOKMARK_NAME, value);

        /// <summary>Creates a SonicWall-Bookmark-URL attribute (Type 8).</summary>
        /// <param name="value">The SSL VPN bookmark URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BookmarkUrl(string value) => CreateString(SonicwallAttributeType.BOOKMARK_URL, value);

        /// <summary>Creates a SonicWall-SSLVPN-Domain attribute (Type 9).</summary>
        /// <param name="value">The SSL VPN domain name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SslvpnDomain(string value) => CreateString(SonicwallAttributeType.SSLVPN_DOMAIN, value);

        /// <summary>Creates a SonicWall-Redirect-URL attribute (Type 10).</summary>
        /// <param name="value">The redirect URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value) => CreateString(SonicwallAttributeType.REDIRECT_URL, value);

        /// <summary>Creates a SonicWall-SSLVPN-Custom-Realm attribute (Type 11).</summary>
        /// <param name="value">The SSL VPN custom realm name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SslvpnCustomRealm(string value) => CreateString(SonicwallAttributeType.SSLVPN_CUSTOM_REALM, value);

        /// <summary>Creates a SonicWall-Client-Domain-Name attribute (Type 19).</summary>
        /// <param name="value">The client domain name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientDomainName(string value) => CreateString(SonicwallAttributeType.CLIENT_DOMAIN_NAME, value);

        /// <summary>Creates a SonicWall-Client-Routes attribute (Type 20).</summary>
        /// <param name="value">The client VPN routes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientRoutes(string value) => CreateString(SonicwallAttributeType.CLIENT_ROUTES, value);

        /// <summary>Creates a SonicWall-SSLVPN-NetExtender-Client-Routes attribute (Type 21).</summary>
        /// <param name="value">The NetExtender client routes. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SslvpnNetExtenderClientRoutes(string value) => CreateString(SonicwallAttributeType.SSLVPN_NETEXTENDER_CLIENT_ROUTES, value);

        /// <summary>Creates a SonicWall-SSLVPN-IP-Pool attribute (Type 22).</summary>
        /// <param name="value">The SSL VPN IP pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SslvpnIpPool(string value) => CreateString(SonicwallAttributeType.SSLVPN_IP_POOL, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a SonicWall-Client-IP attribute (Type 13).</summary>
        /// <param name="value">The client IP address assignment. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientIp(IPAddress value) => CreateIpv4(SonicwallAttributeType.CLIENT_IP, value);

        /// <summary>Creates a SonicWall-Client-Netmask attribute (Type 14).</summary>
        /// <param name="value">The client subnet mask. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientNetmask(IPAddress value) => CreateIpv4(SonicwallAttributeType.CLIENT_NETMASK, value);

        /// <summary>Creates a SonicWall-Client-DNS-Primary attribute (Type 15).</summary>
        /// <param name="value">The client primary DNS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientDnsPrimary(IPAddress value) => CreateIpv4(SonicwallAttributeType.CLIENT_DNS_PRIMARY, value);

        /// <summary>Creates a SonicWall-Client-DNS-Secondary attribute (Type 16).</summary>
        /// <param name="value">The client secondary DNS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientDnsSecondary(IPAddress value) => CreateIpv4(SonicwallAttributeType.CLIENT_DNS_SECONDARY, value);

        /// <summary>Creates a SonicWall-Client-WINS-Primary attribute (Type 17).</summary>
        /// <param name="value">The client primary WINS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientWinsPrimary(IPAddress value) => CreateIpv4(SonicwallAttributeType.CLIENT_WINS_PRIMARY, value);

        /// <summary>Creates a SonicWall-Client-WINS-Secondary attribute (Type 18).</summary>
        /// <param name="value">The client secondary WINS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientWinsSecondary(IPAddress value) => CreateIpv4(SonicwallAttributeType.CLIENT_WINS_SECONDARY, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(SonicwallAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(SonicwallAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(SonicwallAttributeType type, IPAddress value)
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