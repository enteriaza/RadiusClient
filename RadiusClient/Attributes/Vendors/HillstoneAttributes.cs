using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Hillstone Networks (IANA PEN 28557) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.hillstone</c>.
    /// </summary>
    public enum HillstoneAttributeType : byte
    {
        /// <summary>Hillstone-User-Privilege (Type 1). Integer. User privilege level.</summary>
        USER_PRIVILEGE = 1,

        /// <summary>Hillstone-User-Role (Type 2). String. User role name.</summary>
        USER_ROLE = 2,

        /// <summary>Hillstone-User-Vsys (Type 3). String. Virtual system (VSYS) name.</summary>
        USER_VSYS = 3,

        /// <summary>Hillstone-AVPair (Type 4). String. Attribute-value pair string.</summary>
        AVPAIR = 4
    }

    /// <summary>
    /// Hillstone-User-Privilege attribute values (Type 1).
    /// </summary>
    public enum HILLSTONE_USER_PRIVILEGE
    {
        /// <summary>Read-only user access.</summary>
        READ_ONLY = 0,

        /// <summary>Read-write operator access.</summary>
        READ_WRITE = 1,

        /// <summary>Full administrative (super) access.</summary>
        ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Hillstone Networks
    /// (IANA PEN 28557) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.hillstone</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Hillstone's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 28557</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Hillstone Networks next-generation firewalls
    /// and security appliances (E-Series, T-Series, A-Series, X-Series) for
    /// RADIUS-based user privilege level assignment, user role mapping, virtual
    /// system (VSYS) selection, and general-purpose attribute-value pair
    /// configuration during administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(HillstoneAttributes.UserPrivilege(HILLSTONE_USER_PRIVILEGE.ADMIN));
    /// packet.SetAttribute(HillstoneAttributes.UserRole("security-admin"));
    /// packet.SetAttribute(HillstoneAttributes.UserVsys("root"));
    /// </code>
    /// </remarks>
    public static class HillstoneAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Hillstone Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 28557;

        #region Integer Attributes

        /// <summary>
        /// Creates a Hillstone-User-Privilege attribute (Type 1) with the specified privilege level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="HILLSTONE_USER_PRIVILEGE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserPrivilege(HILLSTONE_USER_PRIVILEGE value)
        {
            return CreateInteger(HillstoneAttributeType.USER_PRIVILEGE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Hillstone-User-Role attribute (Type 2) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(HillstoneAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates a Hillstone-User-Vsys attribute (Type 3) with the specified virtual system name.
        /// </summary>
        /// <param name="value">The virtual system (VSYS) name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserVsys(string value)
        {
            return CreateString(HillstoneAttributeType.USER_VSYS, value);
        }

        /// <summary>
        /// Creates a Hillstone-AVPair attribute (Type 4) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(HillstoneAttributeType.AVPAIR, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Hillstone attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(HillstoneAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Hillstone attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(HillstoneAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}