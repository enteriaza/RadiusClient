using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a CoSine Communications (IANA PEN 3085) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.cosine</c>.
    /// </summary>
    public enum CosineAttributeType : byte
    {
        /// <summary>Cosine-Connection-Profile-Name (Type 1). String. Connection profile name.</summary>
        CONNECTION_PROFILE_NAME = 1,

        /// <summary>Cosine-Enterprise-Id (Type 2). String. Enterprise identifier.</summary>
        ENTERPRISE_ID = 2,

        /// <summary>Cosine-Address-Pool-Name (Type 3). String. Address pool name.</summary>
        ADDRESS_POOL_NAME = 3,

        /// <summary>Cosine-DS-Byte (Type 4). Integer. DiffServ byte value.</summary>
        DS_BYTE = 4,

        /// <summary>Cosine-VPI-VCI (Type 5). Octets. ATM VPI/VCI identifier.</summary>
        VPI_VCI = 5,

        /// <summary>Cosine-DLCI (Type 6). Integer. Frame Relay DLCI.</summary>
        DLCI = 6,

        /// <summary>Cosine-LI-Id (Type 7). Integer. Lawful intercept identifier.</summary>
        LI_ID = 7,

        /// <summary>Cosine-LI-Location (Type 8). String. Lawful intercept location.</summary>
        LI_LOCATION = 8,

        /// <summary>Cosine-Policy-Name (Type 9). String. Policy name to apply.</summary>
        POLICY_NAME = 9,

        /// <summary>Cosine-NAT-Pool-Name (Type 10). String. NAT pool name.</summary>
        NAT_POOL_NAME = 10,

        /// <summary>Cosine-DS-Byte-Policy-Name (Type 11). String. DiffServ byte policy name.</summary>
        DS_BYTE_POLICY_NAME = 11,

        /// <summary>Cosine-NAT-Policy-Name (Type 12). String. NAT policy name.</summary>
        NAT_POLICY_NAME = 12,

        /// <summary>Cosine-Syslog-Policy-Name (Type 13). String. Syslog policy name.</summary>
        SYSLOG_POLICY_NAME = 13,

        /// <summary>Cosine-Session-Policy-Name (Type 14). String. Session policy name.</summary>
        SESSION_POLICY_NAME = 14,

        /// <summary>Cosine-Admin-Status-Int (Type 15). Integer. Administrative status integer.</summary>
        ADMIN_STATUS_INT = 15,

        /// <summary>Cosine-Ingress-Policy-Name (Type 16). String. Ingress policy name.</summary>
        INGRESS_POLICY_NAME = 16,

        /// <summary>Cosine-Egress-Policy-Name (Type 17). String. Egress policy name.</summary>
        EGRESS_POLICY_NAME = 17,

        /// <summary>Cosine-FQDN (Type 18). String. Fully qualified domain name.</summary>
        FQDN = 18,

        /// <summary>Cosine-VLAN-ID (Type 19). Integer. VLAN identifier.</summary>
        VLAN_ID = 19,

        /// <summary>Cosine-Rate-Limit-In (Type 20). Integer. Ingress rate limit in Kbps.</summary>
        RATE_LIMIT_IN = 20,

        /// <summary>Cosine-Rate-Limit-Out (Type 21). Integer. Egress rate limit in Kbps.</summary>
        RATE_LIMIT_OUT = 21
    }

    /// <summary>
    /// Cosine-Admin-Status-Int attribute values (Type 15).
    /// </summary>
    public enum COSINE_ADMIN_STATUS_INT
    {
        /// <summary>Administratively enabled.</summary>
        ENABLED = 1,

        /// <summary>Administratively disabled.</summary>
        DISABLED = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing CoSine Communications
    /// (IANA PEN 3085) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.cosine</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// CoSine's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3085</c>.
    /// </para>
    /// <para>
    /// These attributes are used by CoSine Communications IP service delivery
    /// platforms for RADIUS-based connection profile assignment, address pool
    /// selection, DiffServ/QoS provisioning, ATM VPI/VCI and Frame Relay DLCI
    /// identification, lawful intercept, NAT pool and policy assignment, ingress/egress
    /// policy enforcement, rate limiting, VLAN mapping, syslog and session
    /// policy configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CosineAttributes.ConnectionProfileName("premium-dsl"));
    /// packet.SetAttribute(CosineAttributes.PolicyName("default-policy"));
    /// packet.SetAttribute(CosineAttributes.RateLimitIn(10000));
    /// packet.SetAttribute(CosineAttributes.RateLimitOut(50000));
    /// packet.SetAttribute(CosineAttributes.VlanId(200));
    /// </code>
    /// </remarks>
    public static class CosineAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for CoSine Communications.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3085;

        #region Integer Attributes

        /// <summary>Creates a Cosine-DS-Byte attribute (Type 4).</summary>
        /// <param name="value">The DiffServ byte value.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DsByte(int value)
        {
            return CreateInteger(CosineAttributeType.DS_BYTE, value);
        }

        /// <summary>Creates a Cosine-DLCI attribute (Type 6).</summary>
        /// <param name="value">The Frame Relay DLCI.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Dlci(int value)
        {
            return CreateInteger(CosineAttributeType.DLCI, value);
        }

        /// <summary>Creates a Cosine-LI-Id attribute (Type 7).</summary>
        /// <param name="value">The lawful intercept identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LiId(int value)
        {
            return CreateInteger(CosineAttributeType.LI_ID, value);
        }

        /// <summary>
        /// Creates a Cosine-Admin-Status-Int attribute (Type 15).
        /// </summary>
        /// <param name="value">The administrative status. See <see cref="COSINE_ADMIN_STATUS_INT"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminStatusInt(COSINE_ADMIN_STATUS_INT value)
        {
            return CreateInteger(CosineAttributeType.ADMIN_STATUS_INT, (int)value);
        }

        /// <summary>Creates a Cosine-VLAN-ID attribute (Type 19).</summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(CosineAttributeType.VLAN_ID, value);
        }

        /// <summary>Creates a Cosine-Rate-Limit-In attribute (Type 20).</summary>
        /// <param name="value">The ingress rate limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RateLimitIn(int value)
        {
            return CreateInteger(CosineAttributeType.RATE_LIMIT_IN, value);
        }

        /// <summary>Creates a Cosine-Rate-Limit-Out attribute (Type 21).</summary>
        /// <param name="value">The egress rate limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RateLimitOut(int value)
        {
            return CreateInteger(CosineAttributeType.RATE_LIMIT_OUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>Creates a Cosine-Connection-Profile-Name attribute (Type 1).</summary>
        /// <param name="value">The connection profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ConnectionProfileName(string value)
        {
            return CreateString(CosineAttributeType.CONNECTION_PROFILE_NAME, value);
        }

        /// <summary>Creates a Cosine-Enterprise-Id attribute (Type 2).</summary>
        /// <param name="value">The enterprise identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EnterpriseId(string value)
        {
            return CreateString(CosineAttributeType.ENTERPRISE_ID, value);
        }

        /// <summary>Creates a Cosine-Address-Pool-Name attribute (Type 3).</summary>
        /// <param name="value">The address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AddressPoolName(string value)
        {
            return CreateString(CosineAttributeType.ADDRESS_POOL_NAME, value);
        }

        /// <summary>Creates a Cosine-LI-Location attribute (Type 8).</summary>
        /// <param name="value">The lawful intercept location. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LiLocation(string value)
        {
            return CreateString(CosineAttributeType.LI_LOCATION, value);
        }

        /// <summary>Creates a Cosine-Policy-Name attribute (Type 9).</summary>
        /// <param name="value">The policy name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(CosineAttributeType.POLICY_NAME, value);
        }

        /// <summary>Creates a Cosine-NAT-Pool-Name attribute (Type 10).</summary>
        /// <param name="value">The NAT pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatPoolName(string value)
        {
            return CreateString(CosineAttributeType.NAT_POOL_NAME, value);
        }

        /// <summary>Creates a Cosine-DS-Byte-Policy-Name attribute (Type 11).</summary>
        /// <param name="value">The DiffServ byte policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DsBytePolicyName(string value)
        {
            return CreateString(CosineAttributeType.DS_BYTE_POLICY_NAME, value);
        }

        /// <summary>Creates a Cosine-NAT-Policy-Name attribute (Type 12).</summary>
        /// <param name="value">The NAT policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatPolicyName(string value)
        {
            return CreateString(CosineAttributeType.NAT_POLICY_NAME, value);
        }

        /// <summary>Creates a Cosine-Syslog-Policy-Name attribute (Type 13).</summary>
        /// <param name="value">The syslog policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SyslogPolicyName(string value)
        {
            return CreateString(CosineAttributeType.SYSLOG_POLICY_NAME, value);
        }

        /// <summary>Creates a Cosine-Session-Policy-Name attribute (Type 14).</summary>
        /// <param name="value">The session policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionPolicyName(string value)
        {
            return CreateString(CosineAttributeType.SESSION_POLICY_NAME, value);
        }

        /// <summary>Creates a Cosine-Ingress-Policy-Name attribute (Type 16).</summary>
        /// <param name="value">The ingress policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IngressPolicyName(string value)
        {
            return CreateString(CosineAttributeType.INGRESS_POLICY_NAME, value);
        }

        /// <summary>Creates a Cosine-Egress-Policy-Name attribute (Type 17).</summary>
        /// <param name="value">The egress policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EgressPolicyName(string value)
        {
            return CreateString(CosineAttributeType.EGRESS_POLICY_NAME, value);
        }

        /// <summary>Creates a Cosine-FQDN attribute (Type 18).</summary>
        /// <param name="value">The fully qualified domain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Fqdn(string value)
        {
            return CreateString(CosineAttributeType.FQDN, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates a Cosine-VPI-VCI attribute (Type 5) with the specified ATM VPI/VCI data.
        /// </summary>
        /// <param name="value">The ATM VPI/VCI identifier data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpiVci(byte[] value)
        {
            return CreateOctets(CosineAttributeType.VPI_VCI, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(CosineAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(CosineAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(CosineAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        #endregion
    }
}