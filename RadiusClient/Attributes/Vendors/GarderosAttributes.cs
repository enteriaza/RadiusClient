using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Garderos (IANA PEN 45014) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.garderos</c>.
    /// </summary>
    public enum GarderosAttributeType : byte
    {
        /// <summary>Garderos-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Garderos-User-Role (Type 2). String. User role name.</summary>
        USER_ROLE = 2,

        /// <summary>Garderos-Privilege-Level (Type 3). Integer. CLI privilege level.</summary>
        PRIVILEGE_LEVEL = 3
    }

    /// <summary>
    /// Garderos-Privilege-Level attribute values (Type 3).
    /// </summary>
    public enum GARDEROS_PRIVILEGE_LEVEL
    {
        /// <summary>Read-only user access.</summary>
        USER = 0,

        /// <summary>Read-write operator access.</summary>
        OPERATOR = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Garderos
    /// (IANA PEN 45014) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.garderos</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Garderos' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 45014</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Garderos Networks routers and network appliances
    /// for RADIUS-based general-purpose attribute-value pair configuration, user
    /// role assignment, and CLI privilege level mapping during administrative
    /// authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(GarderosAttributes.PrivilegeLevel(GARDEROS_PRIVILEGE_LEVEL.ADMIN));
    /// packet.SetAttribute(GarderosAttributes.UserRole("network-admin"));
    /// packet.SetAttribute(GarderosAttributes.AvPair("shell:priv-lvl=15"));
    /// </code>
    /// </remarks>
    public static class GarderosAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Garderos.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 45014;

        #region Integer Attributes

        /// <summary>
        /// Creates a Garderos-Privilege-Level attribute (Type 3) with the specified level.
        /// </summary>
        /// <param name="value">The CLI privilege level. See <see cref="GARDEROS_PRIVILEGE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(GARDEROS_PRIVILEGE_LEVEL value)
        {
            return CreateInteger(GarderosAttributeType.PRIVILEGE_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Garderos-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(GarderosAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Garderos-User-Role attribute (Type 2) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(GarderosAttributeType.USER_ROLE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Garderos attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(GarderosAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Garderos attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(GarderosAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}