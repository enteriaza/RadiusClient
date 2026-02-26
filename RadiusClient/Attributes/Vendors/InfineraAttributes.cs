using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Infinera (IANA PEN 21399) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.infinera</c>.
    /// </summary>
    /// <remarks>
    /// Infinera is a manufacturer of optical transport networking equipment
    /// including DWDM, OTN, and packet-optical platforms.
    /// </remarks>
    public enum InfineraAttributeType : byte
    {
        /// <summary>Infinera-Access-Level (Type 1). Integer. CLI access level.</summary>
        ACCESS_LEVEL = 1,

        /// <summary>Infinera-User-Role (Type 2). String. User role name.</summary>
        USER_ROLE = 2,

        /// <summary>Infinera-User-Group (Type 3). String. User group name.</summary>
        USER_GROUP = 3,

        /// <summary>Infinera-AVPair (Type 4). String. Attribute-value pair string.</summary>
        AVPAIR = 4,

        /// <summary>Infinera-Command-String (Type 5). String. Authorized command string.</summary>
        COMMAND_STRING = 5
    }

    /// <summary>
    /// Infinera-Access-Level attribute values (Type 1).
    /// </summary>
    public enum INFINERA_ACCESS_LEVEL
    {
        /// <summary>Read-only user access.</summary>
        RETRIEVE = 0,

        /// <summary>Maintenance level access.</summary>
        MAINTENANCE = 1,

        /// <summary>Provisioning level access.</summary>
        PROVISIONING = 2,

        /// <summary>Super-user / administrative access.</summary>
        SUPER_USER = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Infinera
    /// (IANA PEN 21399) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.infinera</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Infinera's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 21399</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Infinera optical transport networking equipment
    /// (DTN-X, Cloud Xpress, GX Series) for RADIUS-based CLI access level assignment,
    /// user role and group mapping, general-purpose attribute-value pair configuration,
    /// and command authorization.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(InfineraAttributes.AccessLevel(INFINERA_ACCESS_LEVEL.SUPER_USER));
    /// packet.SetAttribute(InfineraAttributes.UserRole("network-admin"));
    /// packet.SetAttribute(InfineraAttributes.UserGroup("optical-engineers"));
    /// </code>
    /// </remarks>
    public static class InfineraAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Infinera.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 21399;

        #region Integer Attributes

        /// <summary>
        /// Creates an Infinera-Access-Level attribute (Type 1) with the specified level.
        /// </summary>
        /// <param name="value">The CLI access level. See <see cref="INFINERA_ACCESS_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLevel(INFINERA_ACCESS_LEVEL value)
        {
            return CreateInteger(InfineraAttributeType.ACCESS_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Infinera-User-Role attribute (Type 2) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(InfineraAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates an Infinera-User-Group attribute (Type 3) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(InfineraAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates an Infinera-AVPair attribute (Type 4) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(InfineraAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates an Infinera-Command-String attribute (Type 5) with the specified command.
        /// </summary>
        /// <param name="value">The authorized command string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CommandString(string value)
        {
            return CreateString(InfineraAttributeType.COMMAND_STRING, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Infinera attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(InfineraAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Infinera attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(InfineraAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}