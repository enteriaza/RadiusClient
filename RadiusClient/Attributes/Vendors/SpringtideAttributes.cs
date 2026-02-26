using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Spring Tide Networks (IANA PEN 3551) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.springtide</c>.
    /// </summary>
    /// <remarks>
    /// Spring Tide Networks (acquired by Nokia in 2002) produced IP service
    /// switches and subscriber management platforms (IP Service Switch 5000)
    /// for service provider edge deployments, providing per-subscriber policy
    /// enforcement, VPN services, and broadband aggregation.
    /// </remarks>
    public enum SpringtideAttributeType : byte
    {
        /// <summary>Springtide-PHB (Type 1). Integer. Per-hop behavior (DiffServ) value.</summary>
        PHB = 1,

        /// <summary>Springtide-Service-Name (Type 2). String. Service name.</summary>
        SERVICE_NAME = 2,

        /// <summary>Springtide-VPN-Name (Type 3). String. VPN name.</summary>
        VPN_NAME = 3,

        /// <summary>Springtide-Service-Domain (Type 4). String. Service domain name.</summary>
        SERVICE_DOMAIN = 4,

        /// <summary>Springtide-VPN-Id (Type 5). Integer. VPN identifier.</summary>
        VPN_ID = 5,

        /// <summary>Springtide-Context-Name (Type 6). String. Context name.</summary>
        CONTEXT_NAME = 6,

        /// <summary>Springtide-Interface-Id (Type 7). Integer. Interface identifier.</summary>
        INTERFACE_ID = 7,

        /// <summary>Springtide-Subscriber-Name (Type 8). String. Subscriber name.</summary>
        SUBSCRIBER_NAME = 8,

        /// <summary>Springtide-Command (Type 9). String. CLI command string.</summary>
        COMMAND = 9,

        /// <summary>Springtide-Priority (Type 10). Integer. Priority value.</summary>
        PRIORITY = 10,

        /// <summary>Springtide-Primary-DNS (Type 11). String. Primary DNS server address.</summary>
        PRIMARY_DNS = 11,

        /// <summary>Springtide-Secondary-DNS (Type 12). String. Secondary DNS server address.</summary>
        SECONDARY_DNS = 12,

        /// <summary>Springtide-Upstream-Rate (Type 13). Integer. Upstream rate in Kbps.</summary>
        UPSTREAM_RATE = 13,

        /// <summary>Springtide-Downstream-Rate (Type 14). Integer. Downstream rate in Kbps.</summary>
        DOWNSTREAM_RATE = 14,

        /// <summary>Springtide-Admin-Privilege (Type 15). Integer. Administrative privilege level.</summary>
        ADMIN_PRIVILEGE = 15
    }

    /// <summary>
    /// Springtide-Admin-Privilege attribute values (Type 15).
    /// </summary>
    public enum SPRINGTIDE_ADMIN_PRIVILEGE
    {
        /// <summary>Standard user access.</summary>
        USER = 0,

        /// <summary>Super-user (administrative) access.</summary>
        SUPER_USER = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Spring Tide Networks
    /// (IANA PEN 3551) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.springtide</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Spring Tide's vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3551</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Spring Tide Networks (Nokia) IP Service Switch
    /// platforms for RADIUS-based per-hop behavior (DiffServ) assignment, service
    /// and VPN name/domain/ID configuration, context and interface selection,
    /// subscriber naming, CLI command authorization, priority assignment, DNS
    /// server provisioning, upstream/downstream rate provisioning, and
    /// administrative privilege level assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(SpringtideAttributes.ServiceName("broadband-residential"));
    /// packet.SetAttribute(SpringtideAttributes.VpnName("corporate-vpn"));
    /// packet.SetAttribute(SpringtideAttributes.UpstreamRate(10000));
    /// packet.SetAttribute(SpringtideAttributes.DownstreamRate(50000));
    /// packet.SetAttribute(SpringtideAttributes.AdminPrivilege(SPRINGTIDE_ADMIN_PRIVILEGE.SUPER_USER));
    /// </code>
    /// </remarks>
    public static class SpringtideAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Spring Tide Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3551;

        #region Integer Attributes

        /// <summary>
        /// Creates a Springtide-PHB attribute (Type 1) with the specified DiffServ value.
        /// </summary>
        /// <param name="value">The per-hop behavior (DiffServ) value.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Phb(int value)
        {
            return CreateInteger(SpringtideAttributeType.PHB, value);
        }

        /// <summary>
        /// Creates a Springtide-VPN-Id attribute (Type 5) with the specified VPN identifier.
        /// </summary>
        /// <param name="value">The VPN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VpnId(int value)
        {
            return CreateInteger(SpringtideAttributeType.VPN_ID, value);
        }

        /// <summary>
        /// Creates a Springtide-Interface-Id attribute (Type 7) with the specified interface ID.
        /// </summary>
        /// <param name="value">The interface identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes InterfaceId(int value)
        {
            return CreateInteger(SpringtideAttributeType.INTERFACE_ID, value);
        }

        /// <summary>
        /// Creates a Springtide-Priority attribute (Type 10) with the specified priority.
        /// </summary>
        /// <param name="value">The priority value.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Priority(int value)
        {
            return CreateInteger(SpringtideAttributeType.PRIORITY, value);
        }

        /// <summary>
        /// Creates a Springtide-Upstream-Rate attribute (Type 13) with the specified rate.
        /// </summary>
        /// <param name="value">The upstream rate in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UpstreamRate(int value)
        {
            return CreateInteger(SpringtideAttributeType.UPSTREAM_RATE, value);
        }

        /// <summary>
        /// Creates a Springtide-Downstream-Rate attribute (Type 14) with the specified rate.
        /// </summary>
        /// <param name="value">The downstream rate in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DownstreamRate(int value)
        {
            return CreateInteger(SpringtideAttributeType.DOWNSTREAM_RATE, value);
        }

        /// <summary>
        /// Creates a Springtide-Admin-Privilege attribute (Type 15) with the specified level.
        /// </summary>
        /// <param name="value">The administrative privilege level. See <see cref="SPRINGTIDE_ADMIN_PRIVILEGE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminPrivilege(SPRINGTIDE_ADMIN_PRIVILEGE value)
        {
            return CreateInteger(SpringtideAttributeType.ADMIN_PRIVILEGE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Springtide-Service-Name attribute (Type 2) with the specified service name.
        /// </summary>
        /// <param name="value">The service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceName(string value)
        {
            return CreateString(SpringtideAttributeType.SERVICE_NAME, value);
        }

        /// <summary>
        /// Creates a Springtide-VPN-Name attribute (Type 3) with the specified VPN name.
        /// </summary>
        /// <param name="value">The VPN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnName(string value)
        {
            return CreateString(SpringtideAttributeType.VPN_NAME, value);
        }

        /// <summary>
        /// Creates a Springtide-Service-Domain attribute (Type 4) with the specified domain name.
        /// </summary>
        /// <param name="value">The service domain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceDomain(string value)
        {
            return CreateString(SpringtideAttributeType.SERVICE_DOMAIN, value);
        }

        /// <summary>
        /// Creates a Springtide-Context-Name attribute (Type 6) with the specified context name.
        /// </summary>
        /// <param name="value">The context name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ContextName(string value)
        {
            return CreateString(SpringtideAttributeType.CONTEXT_NAME, value);
        }

        /// <summary>
        /// Creates a Springtide-Subscriber-Name attribute (Type 8) with the specified subscriber name.
        /// </summary>
        /// <param name="value">The subscriber name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberName(string value)
        {
            return CreateString(SpringtideAttributeType.SUBSCRIBER_NAME, value);
        }

        /// <summary>
        /// Creates a Springtide-Command attribute (Type 9) with the specified CLI command.
        /// </summary>
        /// <param name="value">The CLI command string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Command(string value)
        {
            return CreateString(SpringtideAttributeType.COMMAND, value);
        }

        /// <summary>
        /// Creates a Springtide-Primary-DNS attribute (Type 11) with the specified DNS address.
        /// </summary>
        /// <param name="value">The primary DNS server address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrimaryDns(string value)
        {
            return CreateString(SpringtideAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates a Springtide-Secondary-DNS attribute (Type 12) with the specified DNS address.
        /// </summary>
        /// <param name="value">The secondary DNS server address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SecondaryDns(string value)
        {
            return CreateString(SpringtideAttributeType.SECONDARY_DNS, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Spring Tide attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(SpringtideAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Spring Tide attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(SpringtideAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}