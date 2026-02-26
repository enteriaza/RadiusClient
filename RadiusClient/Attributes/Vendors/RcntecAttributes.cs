using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an RCNTEC (IANA PEN 35807) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.rcntec</c>.
    /// </summary>
    /// <remarks>
    /// RCNTEC is a technology company producing data storage, network security,
    /// and infrastructure solutions including hardware security modules (HSMs),
    /// secure storage appliances, and network monitoring platforms.
    /// </remarks>
    public enum RcntecAttributeType : byte
    {
        /// <summary>RCNTEC-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>RCNTEC-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>RCNTEC-User-Role (Type 3). String. User role name.</summary>
        USER_ROLE = 3,

        /// <summary>RCNTEC-Admin-Access (Type 4). Integer. Administrative access level.</summary>
        ADMIN_ACCESS = 4
    }

    /// <summary>
    /// RCNTEC-Admin-Access attribute values (Type 4).
    /// </summary>
    public enum RCNTEC_ADMIN_ACCESS
    {
        /// <summary>No administrative access.</summary>
        NONE = 0,

        /// <summary>Read-only access.</summary>
        READ_ONLY = 1,

        /// <summary>Read-write access.</summary>
        READ_WRITE = 2,

        /// <summary>Full administrative access.</summary>
        ADMIN = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing RCNTEC
    /// (IANA PEN 35807) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.rcntec</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RCNTEC's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 35807</c>.
    /// </para>
    /// <para>
    /// These attributes are used by RCNTEC secure storage appliances, HSMs, and
    /// network monitoring platforms for RADIUS-based user group and role assignment,
    /// administrative access level control, and general-purpose attribute-value
    /// pair configuration during administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(RcntecAttributes.AdminAccess(RCNTEC_ADMIN_ACCESS.ADMIN));
    /// packet.SetAttribute(RcntecAttributes.UserGroup("security-ops"));
    /// packet.SetAttribute(RcntecAttributes.UserRole("hsm-admin"));
    /// </code>
    /// </remarks>
    public static class RcntecAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for RCNTEC.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 35807;

        #region Integer Attributes

        /// <summary>
        /// Creates an RCNTEC-Admin-Access attribute (Type 4) with the specified level.
        /// </summary>
        /// <param name="value">The administrative access level. See <see cref="RCNTEC_ADMIN_ACCESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminAccess(RCNTEC_ADMIN_ACCESS value)
        {
            return CreateInteger(RcntecAttributeType.ADMIN_ACCESS, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an RCNTEC-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(RcntecAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates an RCNTEC-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(RcntecAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates an RCNTEC-User-Role attribute (Type 3) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(RcntecAttributeType.USER_ROLE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified RCNTEC attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(RcntecAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified RCNTEC attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(RcntecAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}