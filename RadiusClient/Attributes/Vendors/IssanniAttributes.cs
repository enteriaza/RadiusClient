using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Issanni Communications (IANA PEN 5765) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.issanni</c>.
    /// </summary>
    public enum IssanniAttributeType : byte
    {
        /// <summary>Issanni-SoftFlow-Template (Type 1). String. SoftFlow template name.</summary>
        SOFTFLOW_TEMPLATE = 1,

        /// <summary>Issanni-NAT-Support (Type 2). Integer. NAT support flag.</summary>
        NAT_SUPPORT = 2,

        /// <summary>Issanni-Routing-Context (Type 3). String. Routing context name.</summary>
        ROUTING_CONTEXT = 3,

        /// <summary>Issanni-Tunnel-Name (Type 4). String. Tunnel name.</summary>
        TUNNEL_NAME = 4,

        /// <summary>Issanni-IP-Pool-Name (Type 5). String. IP address pool name.</summary>
        IP_POOL_NAME = 5,

        /// <summary>Issanni-PPPoE-URL (Type 6). String. PPPoE service URL.</summary>
        PPPOE_URL = 6,

        /// <summary>Issanni-PPPoE-MOTM (Type 7). String. PPPoE Message of the Moment.</summary>
        PPPOE_MOTM = 7,

        /// <summary>Issanni-Service (Type 8). String. Service name.</summary>
        SERVICE = 8,

        /// <summary>Issanni-Primary-DNS (Type 9). IP address. Primary DNS server.</summary>
        PRIMARY_DNS = 9,

        /// <summary>Issanni-Secondary-DNS (Type 10). IP address. Secondary DNS server.</summary>
        SECONDARY_DNS = 10,

        /// <summary>Issanni-Primary-NBNS (Type 11). IP address. Primary NBNS/WINS server.</summary>
        PRIMARY_NBNS = 11,

        /// <summary>Issanni-Secondary-NBNS (Type 12). IP address. Secondary NBNS/WINS server.</summary>
        SECONDARY_NBNS = 12,

        /// <summary>Issanni-Policing-Traffic-Class (Type 13). String. Policing traffic class.</summary>
        POLICING_TRAFFIC_CLASS = 13,

        /// <summary>Issanni-Tunnel-Type (Type 14). Integer. Tunnel type.</summary>
        TUNNEL_TYPE = 14,

        /// <summary>Issanni-NAT-Inside-Source-List (Type 15). String. NAT inside source access list.</summary>
        NAT_INSIDE_SOURCE_LIST = 15,

        /// <summary>Issanni-NAT-Outside-Source-List (Type 16). String. NAT outside source access list.</summary>
        NAT_OUTSIDE_SOURCE_LIST = 16,

        /// <summary>Issanni-NAT-Pool-Name (Type 17). String. NAT pool name.</summary>
        NAT_POOL_NAME = 17
    }

    /// <summary>
    /// Issanni-NAT-Support attribute values (Type 2).
    /// </summary>
    public enum ISSANNI_NAT_SUPPORT
    {
        /// <summary>NAT support disabled.</summary>
        DISABLED = 0,

        /// <summary>NAT support enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Issanni-Tunnel-Type attribute values (Type 14).
    /// </summary>
    public enum ISSANNI_TUNNEL_TYPE
    {
        /// <summary>IP-in-IP tunnel.</summary>
        IP_IP = 1,

        /// <summary>GRE tunnel.</summary>
        GRE = 2,

        /// <summary>L2TP tunnel.</summary>
        L2TP = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Issanni Communications
    /// (IANA PEN 5765) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.issanni</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Issanni's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 5765</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Issanni Communications broadband access
    /// equipment for RADIUS-based SoftFlow template assignment, NAT configuration
    /// (support flag, inside/outside source lists, pool name), routing context
    /// selection, tunnel configuration (name and type), IP address pool selection,
    /// PPPoE service URL and MOTM, service name assignment, DNS/NBNS provisioning,
    /// and policing traffic class enforcement.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(IssanniAttributes.RoutingContext("default"));
    /// packet.SetAttribute(IssanniAttributes.IpPoolName("subscriber-pool"));
    /// packet.SetAttribute(IssanniAttributes.NatSupport(ISSANNI_NAT_SUPPORT.ENABLED));
    /// packet.SetAttribute(IssanniAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(IssanniAttributes.Service("internet-basic"));
    /// </code>
    /// </remarks>
    public static class IssanniAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Issanni Communications.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 5765;

        #region Integer Attributes

        /// <summary>
        /// Creates an Issanni-NAT-Support attribute (Type 2) with the specified setting.
        /// </summary>
        /// <param name="value">The NAT support flag. See <see cref="ISSANNI_NAT_SUPPORT"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NatSupport(ISSANNI_NAT_SUPPORT value)
        {
            return CreateInteger(IssanniAttributeType.NAT_SUPPORT, (int)value);
        }

        /// <summary>
        /// Creates an Issanni-Tunnel-Type attribute (Type 14) with the specified type.
        /// </summary>
        /// <param name="value">The tunnel type. See <see cref="ISSANNI_TUNNEL_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelType(ISSANNI_TUNNEL_TYPE value)
        {
            return CreateInteger(IssanniAttributeType.TUNNEL_TYPE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Issanni-SoftFlow-Template attribute (Type 1) with the specified template name.
        /// </summary>
        /// <param name="value">The SoftFlow template name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SoftFlowTemplate(string value)
        {
            return CreateString(IssanniAttributeType.SOFTFLOW_TEMPLATE, value);
        }

        /// <summary>
        /// Creates an Issanni-Routing-Context attribute (Type 3) with the specified context name.
        /// </summary>
        /// <param name="value">The routing context name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RoutingContext(string value)
        {
            return CreateString(IssanniAttributeType.ROUTING_CONTEXT, value);
        }

        /// <summary>
        /// Creates an Issanni-Tunnel-Name attribute (Type 4) with the specified tunnel name.
        /// </summary>
        /// <param name="value">The tunnel name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelName(string value)
        {
            return CreateString(IssanniAttributeType.TUNNEL_NAME, value);
        }

        /// <summary>
        /// Creates an Issanni-IP-Pool-Name attribute (Type 5) with the specified pool name.
        /// </summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPoolName(string value)
        {
            return CreateString(IssanniAttributeType.IP_POOL_NAME, value);
        }

        /// <summary>
        /// Creates an Issanni-PPPoE-URL attribute (Type 6) with the specified URL.
        /// </summary>
        /// <param name="value">The PPPoE service URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PppoeUrl(string value)
        {
            return CreateString(IssanniAttributeType.PPPOE_URL, value);
        }

        /// <summary>
        /// Creates an Issanni-PPPoE-MOTM attribute (Type 7) with the specified message.
        /// </summary>
        /// <param name="value">The PPPoE Message of the Moment. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PppoeMotm(string value)
        {
            return CreateString(IssanniAttributeType.PPPOE_MOTM, value);
        }

        /// <summary>
        /// Creates an Issanni-Service attribute (Type 8) with the specified service name.
        /// </summary>
        /// <param name="value">The service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Service(string value)
        {
            return CreateString(IssanniAttributeType.SERVICE, value);
        }

        /// <summary>
        /// Creates an Issanni-Policing-Traffic-Class attribute (Type 13) with the specified class.
        /// </summary>
        /// <param name="value">The policing traffic class. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicingTrafficClass(string value)
        {
            return CreateString(IssanniAttributeType.POLICING_TRAFFIC_CLASS, value);
        }

        /// <summary>
        /// Creates an Issanni-NAT-Inside-Source-List attribute (Type 15) with the specified list.
        /// </summary>
        /// <param name="value">The NAT inside source access list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatInsideSourceList(string value)
        {
            return CreateString(IssanniAttributeType.NAT_INSIDE_SOURCE_LIST, value);
        }

        /// <summary>
        /// Creates an Issanni-NAT-Outside-Source-List attribute (Type 16) with the specified list.
        /// </summary>
        /// <param name="value">The NAT outside source access list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatOutsideSourceList(string value)
        {
            return CreateString(IssanniAttributeType.NAT_OUTSIDE_SOURCE_LIST, value);
        }

        /// <summary>
        /// Creates an Issanni-NAT-Pool-Name attribute (Type 17) with the specified pool name.
        /// </summary>
        /// <param name="value">The NAT pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatPoolName(string value)
        {
            return CreateString(IssanniAttributeType.NAT_POOL_NAME, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Issanni-Primary-DNS attribute (Type 9) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value)
        {
            return CreateIpv4(IssanniAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates an Issanni-Secondary-DNS attribute (Type 10) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value)
        {
            return CreateIpv4(IssanniAttributeType.SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates an Issanni-Primary-NBNS attribute (Type 11) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary NBNS/WINS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryNbns(IPAddress value)
        {
            return CreateIpv4(IssanniAttributeType.PRIMARY_NBNS, value);
        }

        /// <summary>
        /// Creates an Issanni-Secondary-NBNS attribute (Type 12) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary NBNS/WINS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryNbns(IPAddress value)
        {
            return CreateIpv4(IssanniAttributeType.SECONDARY_NBNS, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(IssanniAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(IssanniAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(IssanniAttributeType type, IPAddress value)
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