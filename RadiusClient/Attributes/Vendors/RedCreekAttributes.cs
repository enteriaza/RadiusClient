using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a RedCreek Communications (IANA PEN 1958) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.redcreek</c>.
    /// </summary>
    /// <remarks>
    /// RedCreek Communications produced VPN and network security appliances
    /// (Ravlin series) for enterprise IPSec VPN and encrypted communications.
    /// </remarks>
    public enum RedCreekAttributeType : byte
    {
        /// <summary>RedCreek-Tunneled-DNS-Server (Type 1). IP address. Tunneled DNS server.</summary>
        TUNNELED_DNS_SERVER = 1,

        /// <summary>RedCreek-Tunneled-WINS-Server1 (Type 2). IP address. Tunneled primary WINS server.</summary>
        TUNNELED_WINS_SERVER1 = 2,

        /// <summary>RedCreek-Tunneled-WINS-Server2 (Type 3). IP address. Tunneled secondary WINS server.</summary>
        TUNNELED_WINS_SERVER2 = 3,

        /// <summary>RedCreek-Tunneled-Gateway (Type 4). IP address. Tunneled gateway address.</summary>
        TUNNELED_GATEWAY = 4,

        /// <summary>RedCreek-Tunneled-Search-List (Type 5). String. Tunneled DNS search list.</summary>
        TUNNELED_SEARCH_LIST = 5,

        /// <summary>RedCreek-Tunneled-Domain-Name (Type 6). String. Tunneled domain name.</summary>
        TUNNELED_DOMAIN_NAME = 6,

        /// <summary>RedCreek-Tunneled-IP-Netmask (Type 7). IP address. Tunneled IP netmask.</summary>
        TUNNELED_IP_NETMASK = 7,

        /// <summary>RedCreek-Tunneled-IP-Address (Type 8). IP address. Tunneled IP address.</summary>
        TUNNELED_IP_ADDRESS = 8
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing RedCreek Communications
    /// (IANA PEN 1958) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.redcreek</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RedCreek's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 1958</c>.
    /// </para>
    /// <para>
    /// These attributes are used by RedCreek Communications Ravlin VPN security
    /// appliances for RADIUS-based IPSec VPN tunnel provisioning including tunneled
    /// DNS server, WINS servers (primary and secondary), gateway address, DNS
    /// search list, domain name, IP netmask, and tunneled IP address assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(RedCreekAttributes.TunneledIpAddress(IPAddress.Parse("10.10.0.100")));
    /// packet.SetAttribute(RedCreekAttributes.TunneledIpNetmask(IPAddress.Parse("255.255.255.0")));
    /// packet.SetAttribute(RedCreekAttributes.TunneledGateway(IPAddress.Parse("10.10.0.1")));
    /// packet.SetAttribute(RedCreekAttributes.TunneledDnsServer(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(RedCreekAttributes.TunneledDomainName("corp.example.com"));
    /// </code>
    /// </remarks>
    public static class RedCreekAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for RedCreek Communications.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 1958;

        #region IP Address Attributes

        /// <summary>
        /// Creates a RedCreek-Tunneled-DNS-Server attribute (Type 1) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The tunneled DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes TunneledDnsServer(IPAddress value)
        {
            return CreateIpv4(RedCreekAttributeType.TUNNELED_DNS_SERVER, value);
        }

        /// <summary>
        /// Creates a RedCreek-Tunneled-WINS-Server1 attribute (Type 2) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The tunneled primary WINS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes TunneledWinsServer1(IPAddress value)
        {
            return CreateIpv4(RedCreekAttributeType.TUNNELED_WINS_SERVER1, value);
        }

        /// <summary>
        /// Creates a RedCreek-Tunneled-WINS-Server2 attribute (Type 3) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The tunneled secondary WINS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes TunneledWinsServer2(IPAddress value)
        {
            return CreateIpv4(RedCreekAttributeType.TUNNELED_WINS_SERVER2, value);
        }

        /// <summary>
        /// Creates a RedCreek-Tunneled-Gateway attribute (Type 4) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The tunneled gateway address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes TunneledGateway(IPAddress value)
        {
            return CreateIpv4(RedCreekAttributeType.TUNNELED_GATEWAY, value);
        }

        /// <summary>
        /// Creates a RedCreek-Tunneled-IP-Netmask attribute (Type 7) with the specified IPv4 netmask.
        /// </summary>
        /// <param name="value">The tunneled IP netmask. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes TunneledIpNetmask(IPAddress value)
        {
            return CreateIpv4(RedCreekAttributeType.TUNNELED_IP_NETMASK, value);
        }

        /// <summary>
        /// Creates a RedCreek-Tunneled-IP-Address attribute (Type 8) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The tunneled IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes TunneledIpAddress(IPAddress value)
        {
            return CreateIpv4(RedCreekAttributeType.TUNNELED_IP_ADDRESS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a RedCreek-Tunneled-Search-List attribute (Type 5) with the specified search list.
        /// </summary>
        /// <param name="value">The tunneled DNS search list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunneledSearchList(string value)
        {
            return CreateString(RedCreekAttributeType.TUNNELED_SEARCH_LIST, value);
        }

        /// <summary>
        /// Creates a RedCreek-Tunneled-Domain-Name attribute (Type 6) with the specified domain name.
        /// </summary>
        /// <param name="value">The tunneled domain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunneledDomainName(string value)
        {
            return CreateString(RedCreekAttributeType.TUNNELED_DOMAIN_NAME, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateString(RedCreekAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(RedCreekAttributeType type, IPAddress value)
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