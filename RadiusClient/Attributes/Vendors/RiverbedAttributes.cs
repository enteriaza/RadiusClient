using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Riverbed Technology (IANA PEN 17163) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.riverbed</c>.
    /// </summary>
    /// <remarks>
    /// Riverbed Technology (acquired by Aternity/Alluvio, now part of Riverbed
    /// Technology Inc.) produces WAN optimization appliances (SteelHead),
    /// application performance management (SteelCentral), SD-WAN (SteelConnect),
    /// and network visibility platforms.
    /// </remarks>
    public enum RiverbedAttributeType : byte
    {
        /// <summary>Riverbed-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Riverbed-User-Role (Type 2). String. User role name.</summary>
        USER_ROLE = 2,

        /// <summary>Riverbed-Admin-Level (Type 3). Integer. Administrative privilege level.</summary>
        ADMIN_LEVEL = 3,

        /// <summary>Riverbed-Local-User (Type 4). Integer. Local user flag.</summary>
        LOCAL_USER = 4,

        /// <summary>Riverbed-Group-Name (Type 5). String. User group name.</summary>
        GROUP_NAME = 5,

        /// <summary>Riverbed-RBAC-Role (Type 6). String. RBAC role name.</summary>
        RBAC_ROLE = 6
    }

    /// <summary>
    /// Riverbed-Admin-Level attribute values (Type 3).
    /// </summary>
    public enum RIVERBED_ADMIN_LEVEL
    {
        /// <summary>Monitor (read-only) access.</summary>
        MONITOR = 0,

        /// <summary>Standard operator access.</summary>
        OPERATOR = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    /// <summary>
    /// Riverbed-Local-User attribute values (Type 4).
    /// </summary>
    public enum RIVERBED_LOCAL_USER
    {
        /// <summary>User is not a local user.</summary>
        NO = 0,

        /// <summary>User is a local user.</summary>
        YES = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Riverbed Technology
    /// (IANA PEN 17163) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.riverbed</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Riverbed's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 17163</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Riverbed Technology SteelHead WAN optimization
    /// appliances, SteelCentral performance management, and SteelConnect SD-WAN
    /// platforms for RADIUS-based user role assignment, administrative privilege
    /// level control, local user identification, user group mapping, RBAC role
    /// assignment, and general-purpose attribute-value pair configuration during
    /// administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(RiverbedAttributes.AdminLevel(RIVERBED_ADMIN_LEVEL.ADMIN));
    /// packet.SetAttribute(RiverbedAttributes.UserRole("network-admin"));
    /// packet.SetAttribute(RiverbedAttributes.RbacRole("steelhead-admin"));
    /// packet.SetAttribute(RiverbedAttributes.GroupName("wan-ops"));
    /// </code>
    /// </remarks>
    public static class RiverbedAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Riverbed Technology.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 17163;

        #region Integer Attributes

        /// <summary>
        /// Creates a Riverbed-Admin-Level attribute (Type 3) with the specified level.
        /// </summary>
        /// <param name="value">The administrative privilege level. See <see cref="RIVERBED_ADMIN_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminLevel(RIVERBED_ADMIN_LEVEL value)
        {
            return CreateInteger(RiverbedAttributeType.ADMIN_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Riverbed-Local-User attribute (Type 4) with the specified flag.
        /// </summary>
        /// <param name="value">The local user flag. See <see cref="RIVERBED_LOCAL_USER"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LocalUser(RIVERBED_LOCAL_USER value)
        {
            return CreateInteger(RiverbedAttributeType.LOCAL_USER, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Riverbed-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(RiverbedAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Riverbed-User-Role attribute (Type 2) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(RiverbedAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates a Riverbed-Group-Name attribute (Type 5) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GroupName(string value)
        {
            return CreateString(RiverbedAttributeType.GROUP_NAME, value);
        }

        /// <summary>
        /// Creates a Riverbed-RBAC-Role attribute (Type 6) with the specified RBAC role name.
        /// </summary>
        /// <param name="value">The RBAC role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RbacRole(string value)
        {
            return CreateString(RiverbedAttributeType.RBAC_ROLE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Riverbed attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(RiverbedAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Riverbed attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(RiverbedAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}