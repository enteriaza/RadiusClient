using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Navini Networks (IANA PEN 3055) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.navini</c>.
    /// </summary>
    /// <remarks>
    /// Navini Networks (acquired by Cisco Systems in 2007) developed fixed and
    /// mobile WiMAX / pre-WiMAX broadband wireless access equipment, including
    /// the Ripwave base station and CPE platforms.
    /// </remarks>
    public enum NaviniAttributeType : byte
    {
        /// <summary>Navini-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Navini-Down-Rate (Type 2). Integer. Downstream rate in bps.</summary>
        DOWN_RATE = 2,

        /// <summary>Navini-Up-Rate (Type 3). Integer. Upstream rate in bps.</summary>
        UP_RATE = 3,

        /// <summary>Navini-Down-Burst-Rate (Type 4). Integer. Downstream burst rate in bps.</summary>
        DOWN_BURST_RATE = 4,

        /// <summary>Navini-Up-Burst-Rate (Type 5). Integer. Upstream burst rate in bps.</summary>
        UP_BURST_RATE = 5,

        /// <summary>Navini-CPE-IP-Address (Type 6). IP address. CPE IP address.</summary>
        CPE_IP_ADDRESS = 6,

        /// <summary>Navini-CPE-Subnet-Mask (Type 7). IP address. CPE subnet mask.</summary>
        CPE_SUBNET_MASK = 7,

        /// <summary>Navini-Gateway-IP-Address (Type 8). IP address. Gateway IP address.</summary>
        GATEWAY_IP_ADDRESS = 8,

        /// <summary>Navini-DNS-Server-Primary (Type 9). IP address. Primary DNS server.</summary>
        DNS_SERVER_PRIMARY = 9,

        /// <summary>Navini-DNS-Server-Secondary (Type 10). IP address. Secondary DNS server.</summary>
        DNS_SERVER_SECONDARY = 10,

        /// <summary>Navini-DHCP-Relay (Type 11). IP address. DHCP relay address.</summary>
        DHCP_RELAY = 11,

        /// <summary>Navini-VLAN-Id (Type 12). Integer. VLAN identifier.</summary>
        VLAN_ID = 12,

        /// <summary>Navini-CPE-MAC-Address (Type 13). String. CPE MAC address.</summary>
        CPE_MAC_ADDRESS = 13,

        /// <summary>Navini-Service-Profile (Type 14). String. Service profile name.</summary>
        SERVICE_PROFILE = 14,

        /// <summary>Navini-Class-Name (Type 15). String. Service class name.</summary>
        CLASS_NAME = 15,

        /// <summary>Navini-Session-Timeout (Type 16). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 16,

        /// <summary>Navini-Idle-Timeout (Type 17). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 17
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Navini Networks
    /// (IANA PEN 3055) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.navini</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Navini's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3055</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Navini Networks (now Cisco) Ripwave broadband
    /// wireless base stations and CPE equipment for RADIUS-based upstream/downstream
    /// rate provisioning (sustained and burst), CPE IP/subnet/gateway/MAC addressing,
    /// DNS server provisioning, DHCP relay configuration, VLAN assignment, service
    /// profile and class name selection, session/idle timeout management, and
    /// general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(NaviniAttributes.DownRate(10000000));
    /// packet.SetAttribute(NaviniAttributes.UpRate(2000000));
    /// packet.SetAttribute(NaviniAttributes.ServiceProfile("residential-10m"));
    /// packet.SetAttribute(NaviniAttributes.DnsServerPrimary(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(NaviniAttributes.VlanId(100));
    /// packet.SetAttribute(NaviniAttributes.SessionTimeout(86400));
    /// </code>
    /// </remarks>
    public static class NaviniAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Navini Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3055;

        #region Integer Attributes

        /// <summary>Creates a Navini-Down-Rate attribute (Type 2).</summary>
        /// <param name="value">The downstream rate in bps.</param>
        public static VendorSpecificAttributes DownRate(int value) => CreateInteger(NaviniAttributeType.DOWN_RATE, value);

        /// <summary>Creates a Navini-Up-Rate attribute (Type 3).</summary>
        /// <param name="value">The upstream rate in bps.</param>
        public static VendorSpecificAttributes UpRate(int value) => CreateInteger(NaviniAttributeType.UP_RATE, value);

        /// <summary>Creates a Navini-Down-Burst-Rate attribute (Type 4).</summary>
        /// <param name="value">The downstream burst rate in bps.</param>
        public static VendorSpecificAttributes DownBurstRate(int value) => CreateInteger(NaviniAttributeType.DOWN_BURST_RATE, value);

        /// <summary>Creates a Navini-Up-Burst-Rate attribute (Type 5).</summary>
        /// <param name="value">The upstream burst rate in bps.</param>
        public static VendorSpecificAttributes UpBurstRate(int value) => CreateInteger(NaviniAttributeType.UP_BURST_RATE, value);

        /// <summary>Creates a Navini-VLAN-Id attribute (Type 12).</summary>
        /// <param name="value">The VLAN identifier.</param>
        public static VendorSpecificAttributes VlanId(int value) => CreateInteger(NaviniAttributeType.VLAN_ID, value);

        /// <summary>Creates a Navini-Session-Timeout attribute (Type 16).</summary>
        /// <param name="value">The session timeout in seconds.</param>
        public static VendorSpecificAttributes SessionTimeout(int value) => CreateInteger(NaviniAttributeType.SESSION_TIMEOUT, value);

        /// <summary>Creates a Navini-Idle-Timeout attribute (Type 17).</summary>
        /// <param name="value">The idle timeout in seconds.</param>
        public static VendorSpecificAttributes IdleTimeout(int value) => CreateInteger(NaviniAttributeType.IDLE_TIMEOUT, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Navini-AVPair attribute (Type 1).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(NaviniAttributeType.AVPAIR, value);

        /// <summary>Creates a Navini-CPE-MAC-Address attribute (Type 13).</summary>
        /// <param name="value">The CPE MAC address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CpeMacAddress(string value) => CreateString(NaviniAttributeType.CPE_MAC_ADDRESS, value);

        /// <summary>Creates a Navini-Service-Profile attribute (Type 14).</summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value) => CreateString(NaviniAttributeType.SERVICE_PROFILE, value);

        /// <summary>Creates a Navini-Class-Name attribute (Type 15).</summary>
        /// <param name="value">The service class name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClassName(string value) => CreateString(NaviniAttributeType.CLASS_NAME, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Navini-CPE-IP-Address attribute (Type 6).</summary>
        /// <param name="value">The CPE IP address. Must be IPv4.</param>
        public static VendorSpecificAttributes CpeIpAddress(IPAddress value) => CreateIpv4(NaviniAttributeType.CPE_IP_ADDRESS, value);

        /// <summary>Creates a Navini-CPE-Subnet-Mask attribute (Type 7).</summary>
        /// <param name="value">The CPE subnet mask. Must be IPv4.</param>
        public static VendorSpecificAttributes CpeSubnetMask(IPAddress value) => CreateIpv4(NaviniAttributeType.CPE_SUBNET_MASK, value);

        /// <summary>Creates a Navini-Gateway-IP-Address attribute (Type 8).</summary>
        /// <param name="value">The gateway IP address. Must be IPv4.</param>
        public static VendorSpecificAttributes GatewayIpAddress(IPAddress value) => CreateIpv4(NaviniAttributeType.GATEWAY_IP_ADDRESS, value);

        /// <summary>Creates a Navini-DNS-Server-Primary attribute (Type 9).</summary>
        /// <param name="value">The primary DNS server. Must be IPv4.</param>
        public static VendorSpecificAttributes DnsServerPrimary(IPAddress value) => CreateIpv4(NaviniAttributeType.DNS_SERVER_PRIMARY, value);

        /// <summary>Creates a Navini-DNS-Server-Secondary attribute (Type 10).</summary>
        /// <param name="value">The secondary DNS server. Must be IPv4.</param>
        public static VendorSpecificAttributes DnsServerSecondary(IPAddress value) => CreateIpv4(NaviniAttributeType.DNS_SERVER_SECONDARY, value);

        /// <summary>Creates a Navini-DHCP-Relay attribute (Type 11).</summary>
        /// <param name="value">The DHCP relay address. Must be IPv4.</param>
        public static VendorSpecificAttributes DhcpRelay(IPAddress value) => CreateIpv4(NaviniAttributeType.DHCP_RELAY, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(NaviniAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(NaviniAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(NaviniAttributeType type, IPAddress value)
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