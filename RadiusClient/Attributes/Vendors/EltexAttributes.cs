using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Eltex (IANA PEN 35265) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.eltex</c>.
    /// </summary>
    public enum EltexAttributeType : byte
    {
        /// <summary>Eltex-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Eltex-Privilege-Level (Type 2). Integer. CLI privilege level.</summary>
        PRIVILEGE_LEVEL = 2,

        /// <summary>Eltex-User-Role (Type 3). String. User role name.</summary>
        USER_ROLE = 3,

        /// <summary>Eltex-VLAN-Name (Type 4). String. VLAN name to assign.</summary>
        VLAN_NAME = 4,

        /// <summary>Eltex-Rate-Limit-In (Type 5). Integer. Ingress rate limit in Kbps.</summary>
        RATE_LIMIT_IN = 5,

        /// <summary>Eltex-Rate-Limit-Out (Type 6). Integer. Egress rate limit in Kbps.</summary>
        RATE_LIMIT_OUT = 6,

        /// <summary>Eltex-ACL-Name (Type 7). String. ACL name to apply.</summary>
        ACL_NAME = 7
    }

    /// <summary>
    /// Eltex-Privilege-Level attribute values (Type 2).
    /// </summary>
    public enum ELTEX_PRIVILEGE_LEVEL
    {
        /// <summary>User level (read-only access).</summary>
        USER = 1,

        /// <summary>Operator level (limited configuration).</summary>
        OPERATOR = 7,

        /// <summary>Administrator level (full access).</summary>
        ADMIN = 15
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Eltex
    /// (IANA PEN 35265) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.eltex</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Eltex's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 35265</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Eltex switches, routers, and OLT/ONU platforms
    /// for RADIUS-based CLI privilege level assignment, user role mapping, VLAN
    /// assignment by name, ACL enforcement, ingress/egress rate limiting, and
    /// general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(EltexAttributes.PrivilegeLevel(ELTEX_PRIVILEGE_LEVEL.ADMIN));
    /// packet.SetAttribute(EltexAttributes.UserRole("network-admin"));
    /// packet.SetAttribute(EltexAttributes.VlanName("management"));
    /// packet.SetAttribute(EltexAttributes.RateLimitIn(100000));
    /// packet.SetAttribute(EltexAttributes.RateLimitOut(50000));
    /// </code>
    /// </remarks>
    public static class EltexAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Eltex.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 35265;

        #region Integer Attributes

        /// <summary>
        /// Creates an Eltex-Privilege-Level attribute (Type 2) with the specified level.
        /// </summary>
        /// <param name="value">The CLI privilege level. See <see cref="ELTEX_PRIVILEGE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(ELTEX_PRIVILEGE_LEVEL value)
        {
            return CreateInteger(EltexAttributeType.PRIVILEGE_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates an Eltex-Rate-Limit-In attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The ingress rate limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RateLimitIn(int value)
        {
            return CreateInteger(EltexAttributeType.RATE_LIMIT_IN, value);
        }

        /// <summary>
        /// Creates an Eltex-Rate-Limit-Out attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The egress rate limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RateLimitOut(int value)
        {
            return CreateInteger(EltexAttributeType.RATE_LIMIT_OUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Eltex-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(EltexAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates an Eltex-User-Role attribute (Type 3) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(EltexAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates an Eltex-VLAN-Name attribute (Type 4) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(EltexAttributeType.VLAN_NAME, value);
        }

        /// <summary>
        /// Creates an Eltex-ACL-Name attribute (Type 7) with the specified ACL name.
        /// </summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value)
        {
            return CreateString(EltexAttributeType.ACL_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Eltex attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(EltexAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Eltex attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(EltexAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}