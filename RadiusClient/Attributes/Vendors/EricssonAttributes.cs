using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Ericsson (IANA PEN 193) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.ericsson</c>.
    /// </summary>
    public enum EricssonAttributeType : byte
    {
        /// <summary>Ericsson-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Ericsson-Policy-Name (Type 2). String. Policy name to apply.</summary>
        POLICY_NAME = 2,

        /// <summary>Ericsson-Service-Name (Type 3). String. Service name.</summary>
        SERVICE_NAME = 3,

        /// <summary>Ericsson-Service-Activation (Type 4). String. Service activation string.</summary>
        SERVICE_ACTIVATION = 4,

        /// <summary>Ericsson-Service-Deactivation (Type 5). String. Service deactivation string.</summary>
        SERVICE_DEACTIVATION = 5,

        /// <summary>Ericsson-VPN-Name (Type 6). String. VPN name.</summary>
        VPN_NAME = 6,

        /// <summary>Ericsson-VPN-Id (Type 7). Integer. VPN identifier.</summary>
        VPN_ID = 7,

        /// <summary>Ericsson-IP-Pool-Name (Type 8). String. IP address pool name.</summary>
        IP_POOL_NAME = 8,

        /// <summary>Ericsson-QoS-Profile-Name (Type 9). String. QoS profile name.</summary>
        QOS_PROFILE_NAME = 9,

        /// <summary>Ericsson-Rate-Limit-In (Type 10). Integer. Ingress rate limit in Kbps.</summary>
        RATE_LIMIT_IN = 10,

        /// <summary>Ericsson-Rate-Limit-Out (Type 11). Integer. Egress rate limit in Kbps.</summary>
        RATE_LIMIT_OUT = 11,

        /// <summary>Ericsson-Rate-Limit-In-Burst (Type 12). Integer. Ingress burst size in bytes.</summary>
        RATE_LIMIT_IN_BURST = 12,

        /// <summary>Ericsson-Rate-Limit-Out-Burst (Type 13). Integer. Egress burst size in bytes.</summary>
        RATE_LIMIT_OUT_BURST = 13,

        /// <summary>Ericsson-Filter-Id-In (Type 14). String. Ingress filter identifier.</summary>
        FILTER_ID_IN = 14,

        /// <summary>Ericsson-Filter-Id-Out (Type 15). String. Egress filter identifier.</summary>
        FILTER_ID_OUT = 15,

        /// <summary>Ericsson-VLAN-Id (Type 16). Integer. VLAN identifier.</summary>
        VLAN_ID = 16,

        /// <summary>Ericsson-Inner-VLAN-Id (Type 17). Integer. Inner (QinQ) VLAN identifier.</summary>
        INNER_VLAN_ID = 17,

        /// <summary>Ericsson-Domain-Name (Type 18). String. Domain name.</summary>
        DOMAIN_NAME = 18,

        /// <summary>Ericsson-Tunnel-Name (Type 19). String. Tunnel name.</summary>
        TUNNEL_NAME = 19,

        /// <summary>Ericsson-NAT-Pool-Name (Type 20). String. NAT pool name.</summary>
        NAT_POOL_NAME = 20,

        /// <summary>Ericsson-Framed-IP-Address (Type 21). IP address. Framed IP address.</summary>
        FRAMED_IP_ADDRESS = 21,

        /// <summary>Ericsson-Framed-IP-Netmask (Type 22). IP address. Framed IP netmask.</summary>
        FRAMED_IP_NETMASK = 22,

        /// <summary>Ericsson-Primary-DNS (Type 23). IP address. Primary DNS server address.</summary>
        PRIMARY_DNS = 23,

        /// <summary>Ericsson-Secondary-DNS (Type 24). IP address. Secondary DNS server address.</summary>
        SECONDARY_DNS = 24,

        /// <summary>Ericsson-Primary-NBNS (Type 25). IP address. Primary NBNS/WINS server address.</summary>
        PRIMARY_NBNS = 25,

        /// <summary>Ericsson-Secondary-NBNS (Type 26). IP address. Secondary NBNS/WINS server address.</summary>
        SECONDARY_NBNS = 26,

        /// <summary>Ericsson-Session-Timeout (Type 27). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 27,

        /// <summary>Ericsson-Idle-Timeout (Type 28). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 28,

        /// <summary>Ericsson-Acct-Interim-Interval (Type 29). Integer. Accounting interim interval in seconds.</summary>
        ACCT_INTERIM_INTERVAL = 29,

        /// <summary>Ericsson-Context-Name (Type 30). String. Context/virtual router name.</summary>
        CONTEXT_NAME = 30,

        /// <summary>Ericsson-Subscriber-Profile-Name (Type 31). String. Subscriber profile name.</summary>
        SUBSCRIBER_PROFILE_NAME = 31
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Ericsson
    /// (IANA PEN 193) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.ericsson</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Ericsson's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 193</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Ericsson (formerly Redback Networks) SmartEdge
    /// routers, BRAS platforms, and mobile packet core equipment for RADIUS-based
    /// subscriber session management, QoS profile and rate limiting, service
    /// activation/deactivation, VPN/VLAN assignment (including QinQ), IP pool
    /// and NAT pool selection, DNS/NBNS assignment, filter enforcement,
    /// tunnel configuration, and subscriber profile mapping.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(EricssonAttributes.QosProfileName("premium"));
    /// packet.SetAttribute(EricssonAttributes.RateLimitIn(100000));
    /// packet.SetAttribute(EricssonAttributes.RateLimitOut(50000));
    /// packet.SetAttribute(EricssonAttributes.IpPoolName("subscriber-pool"));
    /// packet.SetAttribute(EricssonAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(EricssonAttributes.VlanId(200));
    /// </code>
    /// </remarks>
    public static class EricssonAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Ericsson (Redback Networks).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 193;

        #region Integer Attributes

        /// <summary>Creates an Ericsson-VPN-Id attribute (Type 7).</summary>
        /// <param name="value">The VPN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VpnId(int value) => CreateInteger(EricssonAttributeType.VPN_ID, value);

        /// <summary>Creates an Ericsson-Rate-Limit-In attribute (Type 10).</summary>
        /// <param name="value">The ingress rate limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RateLimitIn(int value) => CreateInteger(EricssonAttributeType.RATE_LIMIT_IN, value);

        /// <summary>Creates an Ericsson-Rate-Limit-Out attribute (Type 11).</summary>
        /// <param name="value">The egress rate limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RateLimitOut(int value) => CreateInteger(EricssonAttributeType.RATE_LIMIT_OUT, value);

        /// <summary>Creates an Ericsson-Rate-Limit-In-Burst attribute (Type 12).</summary>
        /// <param name="value">The ingress burst size in bytes.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RateLimitInBurst(int value) => CreateInteger(EricssonAttributeType.RATE_LIMIT_IN_BURST, value);

        /// <summary>Creates an Ericsson-Rate-Limit-Out-Burst attribute (Type 13).</summary>
        /// <param name="value">The egress burst size in bytes.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RateLimitOutBurst(int value) => CreateInteger(EricssonAttributeType.RATE_LIMIT_OUT_BURST, value);

        /// <summary>Creates an Ericsson-VLAN-Id attribute (Type 16).</summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value) => CreateInteger(EricssonAttributeType.VLAN_ID, value);

        /// <summary>Creates an Ericsson-Inner-VLAN-Id attribute (Type 17).</summary>
        /// <param name="value">The inner (QinQ) VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes InnerVlanId(int value) => CreateInteger(EricssonAttributeType.INNER_VLAN_ID, value);

        /// <summary>Creates an Ericsson-Session-Timeout attribute (Type 27).</summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value) => CreateInteger(EricssonAttributeType.SESSION_TIMEOUT, value);

        /// <summary>Creates an Ericsson-Idle-Timeout attribute (Type 28).</summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value) => CreateInteger(EricssonAttributeType.IDLE_TIMEOUT, value);

        /// <summary>Creates an Ericsson-Acct-Interim-Interval attribute (Type 29).</summary>
        /// <param name="value">The accounting interim interval in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctInterimInterval(int value) => CreateInteger(EricssonAttributeType.ACCT_INTERIM_INTERVAL, value);

        #endregion

        #region String Attributes

        /// <summary>Creates an Ericsson-AVPair attribute (Type 1).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(EricssonAttributeType.AVPAIR, value);

        /// <summary>Creates an Ericsson-Policy-Name attribute (Type 2).</summary>
        /// <param name="value">The policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value) => CreateString(EricssonAttributeType.POLICY_NAME, value);

        /// <summary>Creates an Ericsson-Service-Name attribute (Type 3).</summary>
        /// <param name="value">The service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceName(string value) => CreateString(EricssonAttributeType.SERVICE_NAME, value);

        /// <summary>Creates an Ericsson-Service-Activation attribute (Type 4).</summary>
        /// <param name="value">The service activation string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceActivation(string value) => CreateString(EricssonAttributeType.SERVICE_ACTIVATION, value);

        /// <summary>Creates an Ericsson-Service-Deactivation attribute (Type 5).</summary>
        /// <param name="value">The service deactivation string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceDeactivation(string value) => CreateString(EricssonAttributeType.SERVICE_DEACTIVATION, value);

        /// <summary>Creates an Ericsson-VPN-Name attribute (Type 6).</summary>
        /// <param name="value">The VPN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnName(string value) => CreateString(EricssonAttributeType.VPN_NAME, value);

        /// <summary>Creates an Ericsson-IP-Pool-Name attribute (Type 8).</summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPoolName(string value) => CreateString(EricssonAttributeType.IP_POOL_NAME, value);

        /// <summary>Creates an Ericsson-QoS-Profile-Name attribute (Type 9).</summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfileName(string value) => CreateString(EricssonAttributeType.QOS_PROFILE_NAME, value);

        /// <summary>Creates an Ericsson-Filter-Id-In attribute (Type 14).</summary>
        /// <param name="value">The ingress filter identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FilterIdIn(string value) => CreateString(EricssonAttributeType.FILTER_ID_IN, value);

        /// <summary>Creates an Ericsson-Filter-Id-Out attribute (Type 15).</summary>
        /// <param name="value">The egress filter identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FilterIdOut(string value) => CreateString(EricssonAttributeType.FILTER_ID_OUT, value);

        /// <summary>Creates an Ericsson-Domain-Name attribute (Type 18).</summary>
        /// <param name="value">The domain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DomainName(string value) => CreateString(EricssonAttributeType.DOMAIN_NAME, value);

        /// <summary>Creates an Ericsson-Tunnel-Name attribute (Type 19).</summary>
        /// <param name="value">The tunnel name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelName(string value) => CreateString(EricssonAttributeType.TUNNEL_NAME, value);

        /// <summary>Creates an Ericsson-NAT-Pool-Name attribute (Type 20).</summary>
        /// <param name="value">The NAT pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatPoolName(string value) => CreateString(EricssonAttributeType.NAT_POOL_NAME, value);

        /// <summary>Creates an Ericsson-Context-Name attribute (Type 30).</summary>
        /// <param name="value">The context/virtual router name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ContextName(string value) => CreateString(EricssonAttributeType.CONTEXT_NAME, value);

        /// <summary>Creates an Ericsson-Subscriber-Profile-Name attribute (Type 31).</summary>
        /// <param name="value">The subscriber profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberProfileName(string value) => CreateString(EricssonAttributeType.SUBSCRIBER_PROFILE_NAME, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Ericsson-Framed-IP-Address attribute (Type 21) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The framed IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes FramedIpAddress(IPAddress value) => CreateIpv4(EricssonAttributeType.FRAMED_IP_ADDRESS, value);

        /// <summary>
        /// Creates an Ericsson-Framed-IP-Netmask attribute (Type 22) with the specified IPv4 netmask.
        /// </summary>
        /// <param name="value">The framed IP netmask. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes FramedIpNetmask(IPAddress value) => CreateIpv4(EricssonAttributeType.FRAMED_IP_NETMASK, value);

        /// <summary>
        /// Creates an Ericsson-Primary-DNS attribute (Type 23) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary DNS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value) => CreateIpv4(EricssonAttributeType.PRIMARY_DNS, value);

        /// <summary>
        /// Creates an Ericsson-Secondary-DNS attribute (Type 24) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary DNS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value) => CreateIpv4(EricssonAttributeType.SECONDARY_DNS, value);

        /// <summary>
        /// Creates an Ericsson-Primary-NBNS attribute (Type 25) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary NBNS/WINS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryNbns(IPAddress value) => CreateIpv4(EricssonAttributeType.PRIMARY_NBNS, value);

        /// <summary>
        /// Creates an Ericsson-Secondary-NBNS attribute (Type 26) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary NBNS/WINS server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryNbns(IPAddress value) => CreateIpv4(EricssonAttributeType.SECONDARY_NBNS, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(EricssonAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(EricssonAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(EricssonAttributeType type, IPAddress value)
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