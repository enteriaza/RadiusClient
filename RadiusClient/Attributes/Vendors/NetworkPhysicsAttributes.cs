using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Network Physics (IANA PEN 10686) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.networkphysics</c>.
    /// </summary>
    /// <remarks>
    /// Network Physics (acquired by Opnet Technologies, then Riverbed Technology)
    /// produced network performance monitoring and analytics solutions.
    /// </remarks>
    public enum NetworkPhysicsAttributeType : byte
    {
        /// <summary>NetworkPhysics-User-Group (Type 1). String. User group name.</summary>
        USER_GROUP = 1,

        /// <summary>NetworkPhysics-Admin-Privilege (Type 2). Integer. Administrative privilege level.</summary>
        ADMIN_PRIVILEGE = 2,

        /// <summary>NetworkPhysics-AVPair (Type 3). String. Attribute-value pair string.</summary>
        AVPAIR = 3
    }

    /// <summary>
    /// NetworkPhysics-Admin-Privilege attribute values (Type 2).
    /// </summary>
    public enum NETWORKPHYSICS_ADMIN_PRIVILEGE
    {
        /// <summary>Read-only user access.</summary>
        READ_ONLY = 0,

        /// <summary>Standard user access.</summary>
        USER = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Network Physics
    /// (IANA PEN 10686) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.networkphysics</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Network Physics' vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped
    /// in a <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 10686</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Network Physics (now Riverbed) network
    /// performance monitoring appliances for RADIUS-based user group assignment,
    /// administrative privilege level control, and general-purpose attribute-value
    /// pair configuration during administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(NetworkPhysicsAttributes.AdminPrivilege(NETWORKPHYSICS_ADMIN_PRIVILEGE.ADMIN));
    /// packet.SetAttribute(NetworkPhysicsAttributes.UserGroup("noc-engineers"));
    /// </code>
    /// </remarks>
    public static class NetworkPhysicsAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Network Physics.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 10686;

        #region Integer Attributes

        /// <summary>
        /// Creates a NetworkPhysics-Admin-Privilege attribute (Type 2) with the specified level.
        /// </summary>
        /// <param name="value">The administrative privilege level. See <see cref="NETWORKPHYSICS_ADMIN_PRIVILEGE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminPrivilege(NETWORKPHYSICS_ADMIN_PRIVILEGE value)
        {
            return CreateInteger(NetworkPhysicsAttributeType.ADMIN_PRIVILEGE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a NetworkPhysics-User-Group attribute (Type 1) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(NetworkPhysicsAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a NetworkPhysics-AVPair attribute (Type 3) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(NetworkPhysicsAttributeType.AVPAIR, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Network Physics attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(NetworkPhysicsAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Network Physics attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(NetworkPhysicsAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}