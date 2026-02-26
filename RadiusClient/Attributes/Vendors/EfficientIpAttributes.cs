using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an EfficientIP (IANA PEN 2440) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.efficientip</c>.
    /// </summary>
    public enum EfficientIpAttributeType : byte
    {
        /// <summary>EfficientIP-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>EfficientIP-Group (Type 2). String. User group name.</summary>
        GROUP = 2,

        /// <summary>EfficientIP-Role (Type 3). String. User role name.</summary>
        ROLE = 3,

        /// <summary>EfficientIP-Admin (Type 4). Integer. Administrative access flag.</summary>
        ADMIN = 4
    }

    /// <summary>
    /// EfficientIP-Admin attribute values (Type 4).
    /// </summary>
    public enum EFFICIENTIP_ADMIN
    {
        /// <summary>No administrative access.</summary>
        NO = 0,

        /// <summary>Administrative access granted.</summary>
        YES = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing EfficientIP
    /// (IANA PEN 2440) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.efficientip</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// EfficientIP's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2440</c>.
    /// </para>
    /// <para>
    /// These attributes are used by EfficientIP SOLIDserver DDI (DNS, DHCP, IPAM)
    /// platforms for RADIUS-based user group assignment, role mapping,
    /// administrative access control, and general-purpose attribute-value pair
    /// configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(EfficientIpAttributes.Group("dns-admins"));
    /// packet.SetAttribute(EfficientIpAttributes.Role("operator"));
    /// packet.SetAttribute(EfficientIpAttributes.Admin(EFFICIENTIP_ADMIN.YES));
    /// </code>
    /// </remarks>
    public static class EfficientIpAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for EfficientIP.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2440;

        #region Integer Attributes

        /// <summary>
        /// Creates an EfficientIP-Admin attribute (Type 4) with the specified flag.
        /// </summary>
        /// <param name="value">Whether administrative access is granted. See <see cref="EFFICIENTIP_ADMIN"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Admin(EFFICIENTIP_ADMIN value)
        {
            return CreateInteger(EfficientIpAttributeType.ADMIN, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an EfficientIP-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(EfficientIpAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates an EfficientIP-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Group(string value)
        {
            return CreateString(EfficientIpAttributeType.GROUP, value);
        }

        /// <summary>
        /// Creates an EfficientIP-Role attribute (Type 3) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Role(string value)
        {
            return CreateString(EfficientIpAttributeType.ROLE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified EfficientIP attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(EfficientIpAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified EfficientIP attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(EfficientIpAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}