using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Nomadix (IANA PEN 3309) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.nomadix</c>.
    /// </summary>
    /// <remarks>
    /// Nomadix (now part of ASSA ABLOY Global Solutions) produces internet gateway
    /// and subscriber management platforms (AG series, NSE) used in hospitality,
    /// healthcare, MDU/MTU, and public venue environments for captive portal,
    /// bandwidth management, and walled garden services.
    /// </remarks>
    public enum NomadixAttributeType : byte
    {
        /// <summary>Nomadix-Bw-Up (Type 1). Integer. Upstream bandwidth limit in Kbps.</summary>
        BW_UP = 1,

        /// <summary>Nomadix-Bw-Down (Type 2). Integer. Downstream bandwidth limit in Kbps.</summary>
        BW_DOWN = 2,

        /// <summary>Nomadix-URL-Redirection (Type 3). String. URL redirection target.</summary>
        URL_REDIRECTION = 3,

        /// <summary>Nomadix-IP-Upsell (Type 4). Integer. IP upsell flag.</summary>
        IP_UPSELL = 4,

        /// <summary>Nomadix-Expiration (Type 5). String. Session expiration time.</summary>
        EXPIRATION = 5,

        /// <summary>Nomadix-Subnet (Type 6). String. Subnet assignment.</summary>
        SUBNET = 6,

        /// <summary>Nomadix-MaxBytesUp (Type 7). Integer. Maximum upstream bytes.</summary>
        MAX_BYTES_UP = 7,

        /// <summary>Nomadix-MaxBytesDown (Type 8). Integer. Maximum downstream bytes.</summary>
        MAX_BYTES_DOWN = 8,

        /// <summary>Nomadix-EndOfSession (Type 9). Integer. End of session action.</summary>
        END_OF_SESSION = 9,

        /// <summary>Nomadix-Logoff-URL (Type 10). String. Logoff URL.</summary>
        LOGOFF_URL = 10,

        /// <summary>Nomadix-Net-VLAN (Type 11). Integer. Network VLAN identifier.</summary>
        NET_VLAN = 11,

        /// <summary>Nomadix-Config-URL (Type 12). String. Configuration URL.</summary>
        CONFIG_URL = 12,

        /// <summary>Nomadix-Goodbye-URL (Type 13). String. Goodbye/logout redirect URL.</summary>
        GOODBYE_URL = 13,

        /// <summary>Nomadix-QoS-Policy (Type 14). String. QoS policy name.</summary>
        QOS_POLICY = 14,

        /// <summary>Nomadix-SMTP-Redirect (Type 15). IP address. SMTP redirect server.</summary>
        SMTP_REDIRECT = 15,

        /// <summary>Nomadix-Group (Type 16). String. User group name.</summary>
        GROUP = 16,

        /// <summary>Nomadix-WallGarden (Type 17). String. Walled garden rule.</summary>
        WALL_GARDEN = 17,

        /// <summary>Nomadix-Idle-Timeout (Type 18). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 18,

        /// <summary>Nomadix-Session-Timeout (Type 19). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 19,

        /// <summary>Nomadix-CoAPolicy (Type 20). String. Change of Authorization policy name.</summary>
        COA_POLICY = 20,

        /// <summary>Nomadix-DHCP-Option (Type 21). Octets. DHCP option data.</summary>
        DHCP_OPTION = 21,

        /// <summary>Nomadix-Location-Id (Type 22). String. Location identifier.</summary>
        LOCATION_ID = 22,

        /// <summary>Nomadix-MaxBytesTotal (Type 23). Integer. Maximum total (up+down) bytes.</summary>
        MAX_BYTES_TOTAL = 23,

        /// <summary>Nomadix-DHCP-Pool-Name (Type 24). String. DHCP pool name.</summary>
        DHCP_POOL_NAME = 24,

        /// <summary>Nomadix-DNS-Server (Type 25). IP address. DNS server address.</summary>
        DNS_SERVER = 25
    }

    /// <summary>
    /// Nomadix-End-Of-Session attribute values (Type 9).
    /// </summary>
    public enum NOMADIX_END_OF_SESSION
    {
        /// <summary>No action at end of session.</summary>
        NONE = 0,

        /// <summary>Redirect user at end of session.</summary>
        REDIRECT = 1,

        /// <summary>Disconnect user at end of session.</summary>
        DISCONNECT = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Nomadix
    /// (IANA PEN 3309) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.nomadix</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Nomadix's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3309</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Nomadix (ASSA ABLOY) internet gateway platforms
    /// (AG series, NSE) for RADIUS-based subscriber management in hospitality,
    /// healthcare, MDU/MTU, and public venue deployments, including upstream/downstream
    /// bandwidth provisioning, URL redirection (login, logoff, goodbye, config),
    /// walled garden rules, session and idle timeout management, byte quotas
    /// (up/down/total), VLAN assignment, IP upsell, end-of-session actions, QoS
    /// and CoA policy assignment, SMTP redirect, user group and location
    /// identification, DHCP option and pool configuration, and DNS server
    /// provisioning.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(NomadixAttributes.BwUp(5000));
    /// packet.SetAttribute(NomadixAttributes.BwDown(20000));
    /// packet.SetAttribute(NomadixAttributes.UrlRedirection("https://portal.hotel.com/login"));
    /// packet.SetAttribute(NomadixAttributes.SessionTimeout(86400));
    /// packet.SetAttribute(NomadixAttributes.WallGarden("*.hotel.com"));
    /// packet.SetAttribute(NomadixAttributes.NetVlan(100));
    /// </code>
    /// </remarks>
    public static class NomadixAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Nomadix.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3309;

        #region Integer Attributes

        /// <summary>Creates a Nomadix-Bw-Up attribute (Type 1).</summary>
        /// <param name="value">The upstream bandwidth limit in Kbps.</param>
        public static VendorSpecificAttributes BwUp(int value) => CreateInteger(NomadixAttributeType.BW_UP, value);

        /// <summary>Creates a Nomadix-Bw-Down attribute (Type 2).</summary>
        /// <param name="value">The downstream bandwidth limit in Kbps.</param>
        public static VendorSpecificAttributes BwDown(int value) => CreateInteger(NomadixAttributeType.BW_DOWN, value);

        /// <summary>Creates a Nomadix-IP-Upsell attribute (Type 4).</summary>
        /// <param name="value">The IP upsell flag (0 = disabled, 1 = enabled).</param>
        public static VendorSpecificAttributes IpUpsell(int value) => CreateInteger(NomadixAttributeType.IP_UPSELL, value);

        /// <summary>Creates a Nomadix-MaxBytesUp attribute (Type 7).</summary>
        /// <param name="value">The maximum upstream bytes.</param>
        public static VendorSpecificAttributes MaxBytesUp(int value) => CreateInteger(NomadixAttributeType.MAX_BYTES_UP, value);

        /// <summary>Creates a Nomadix-MaxBytesDown attribute (Type 8).</summary>
        /// <param name="value">The maximum downstream bytes.</param>
        public static VendorSpecificAttributes MaxBytesDown(int value) => CreateInteger(NomadixAttributeType.MAX_BYTES_DOWN, value);

        /// <summary>Creates a Nomadix-EndOfSession attribute (Type 9).</summary>
        /// <param name="value">The end of session action. See <see cref="NOMADIX_END_OF_SESSION"/>.</param>
        public static VendorSpecificAttributes EndOfSession(NOMADIX_END_OF_SESSION value) => CreateInteger(NomadixAttributeType.END_OF_SESSION, (int)value);

        /// <summary>Creates a Nomadix-Net-VLAN attribute (Type 11).</summary>
        /// <param name="value">The network VLAN identifier.</param>
        public static VendorSpecificAttributes NetVlan(int value) => CreateInteger(NomadixAttributeType.NET_VLAN, value);

        /// <summary>Creates a Nomadix-Idle-Timeout attribute (Type 18).</summary>
        /// <param name="value">The idle timeout in seconds.</param>
        public static VendorSpecificAttributes IdleTimeout(int value) => CreateInteger(NomadixAttributeType.IDLE_TIMEOUT, value);

        /// <summary>Creates a Nomadix-Session-Timeout attribute (Type 19).</summary>
        /// <param name="value">The session timeout in seconds.</param>
        public static VendorSpecificAttributes SessionTimeout(int value) => CreateInteger(NomadixAttributeType.SESSION_TIMEOUT, value);

        /// <summary>Creates a Nomadix-MaxBytesTotal attribute (Type 23).</summary>
        /// <param name="value">The maximum total (up+down) bytes.</param>
        public static VendorSpecificAttributes MaxBytesTotal(int value) => CreateInteger(NomadixAttributeType.MAX_BYTES_TOTAL, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Nomadix-URL-Redirection attribute (Type 3).</summary>
        /// <param name="value">The URL redirection target. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UrlRedirection(string value) => CreateString(NomadixAttributeType.URL_REDIRECTION, value);

        /// <summary>Creates a Nomadix-Expiration attribute (Type 5).</summary>
        /// <param name="value">The session expiration time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Expiration(string value) => CreateString(NomadixAttributeType.EXPIRATION, value);

        /// <summary>Creates a Nomadix-Subnet attribute (Type 6).</summary>
        /// <param name="value">The subnet assignment. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Subnet(string value) => CreateString(NomadixAttributeType.SUBNET, value);

        /// <summary>Creates a Nomadix-Logoff-URL attribute (Type 10).</summary>
        /// <param name="value">The logoff URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LogoffUrl(string value) => CreateString(NomadixAttributeType.LOGOFF_URL, value);

        /// <summary>Creates a Nomadix-Config-URL attribute (Type 12).</summary>
        /// <param name="value">The configuration URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ConfigUrl(string value) => CreateString(NomadixAttributeType.CONFIG_URL, value);

        /// <summary>Creates a Nomadix-Goodbye-URL attribute (Type 13).</summary>
        /// <param name="value">The goodbye/logout redirect URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GoodbyeUrl(string value) => CreateString(NomadixAttributeType.GOODBYE_URL, value);

        /// <summary>Creates a Nomadix-QoS-Policy attribute (Type 14).</summary>
        /// <param name="value">The QoS policy name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosPolicy(string value) => CreateString(NomadixAttributeType.QOS_POLICY, value);

        /// <summary>Creates a Nomadix-Group attribute (Type 16).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Group(string value) => CreateString(NomadixAttributeType.GROUP, value);

        /// <summary>Creates a Nomadix-WallGarden attribute (Type 17).</summary>
        /// <param name="value">The walled garden rule (e.g. "*.hotel.com"). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WallGarden(string value) => CreateString(NomadixAttributeType.WALL_GARDEN, value);

        /// <summary>Creates a Nomadix-CoAPolicy attribute (Type 20).</summary>
        /// <param name="value">The Change of Authorization policy name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CoaPolicy(string value) => CreateString(NomadixAttributeType.COA_POLICY, value);

        /// <summary>Creates a Nomadix-Location-Id attribute (Type 22).</summary>
        /// <param name="value">The location identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LocationId(string value) => CreateString(NomadixAttributeType.LOCATION_ID, value);

        /// <summary>Creates a Nomadix-DHCP-Pool-Name attribute (Type 24).</summary>
        /// <param name="value">The DHCP pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpPoolName(string value) => CreateString(NomadixAttributeType.DHCP_POOL_NAME, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Nomadix-SMTP-Redirect attribute (Type 15).</summary>
        /// <param name="value">The SMTP redirect server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SmtpRedirect(IPAddress value) => CreateIpv4(NomadixAttributeType.SMTP_REDIRECT, value);

        /// <summary>Creates a Nomadix-DNS-Server attribute (Type 25).</summary>
        /// <param name="value">The DNS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes DnsServer(IPAddress value) => CreateIpv4(NomadixAttributeType.DNS_SERVER, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates a Nomadix-DHCP-Option attribute (Type 21).</summary>
        /// <param name="value">The DHCP option data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpOption(byte[] value) => CreateOctets(NomadixAttributeType.DHCP_OPTION, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(NomadixAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(NomadixAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(NomadixAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        private static VendorSpecificAttributes CreateIpv4(NomadixAttributeType type, IPAddress value)
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