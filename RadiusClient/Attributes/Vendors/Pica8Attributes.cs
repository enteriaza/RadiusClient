using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Pica8 (IANA PEN 35098) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.pica8</c>.
    /// </summary>
    /// <remarks>
    /// Pica8 produces open networking switches running PicOS, a Linux-based
    /// network operating system that supports both OpenFlow/SDN and traditional
    /// Layer 2/Layer 3 switching modes on bare-metal hardware.
    /// </remarks>
    public enum Pica8AttributeType : byte
    {
        /// <summary>Pica8-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Pica8-Privilege-Level (Type 2). Integer. CLI privilege level.</summary>
        PRIVILEGE_LEVEL = 2,

        /// <summary>Pica8-User-Role (Type 3). String. User role name.</summary>
        USER_ROLE = 3
    }

    /// <summary>
    /// Pica8-Privilege-Level attribute values (Type 2).
    /// </summary>
    public enum PICA8_PRIVILEGE_LEVEL
    {
        /// <summary>Read-only access.</summary>
        READ_ONLY = 0,

        /// <summary>Standard operator access.</summary>
        OPERATOR = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 15
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Pica8
    /// (IANA PEN 35098) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.pica8</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Pica8's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 35098</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Pica8 PicOS open networking switches for
    /// RADIUS-based CLI privilege level assignment, user role mapping, and
    /// general-purpose attribute-value pair configuration during administrative
    /// authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(Pica8Attributes.PrivilegeLevel(PICA8_PRIVILEGE_LEVEL.ADMIN));
    /// packet.SetAttribute(Pica8Attributes.UserRole("network-admin"));
    /// </code>
    /// </remarks>
    public static class Pica8Attributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Pica8.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 35098;

        #region Integer Attributes

        /// <summary>
        /// Creates a Pica8-Privilege-Level attribute (Type 2) with the specified level.
        /// </summary>
        /// <param name="value">The CLI privilege level. See <see cref="PICA8_PRIVILEGE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(PICA8_PRIVILEGE_LEVEL value)
        {
            return CreateInteger(Pica8AttributeType.PRIVILEGE_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Pica8-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(Pica8AttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Pica8-User-Role attribute (Type 3) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(Pica8AttributeType.USER_ROLE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Pica8 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(Pica8AttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Pica8 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(Pica8AttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}