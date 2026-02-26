using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Actelis Networks (IANA PEN 5468) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.actelis</c>.
    /// </summary>
    public enum ActelisAttributeType : byte
    {
        /// <summary>Actelis-Access-Level-1 (Type 1). Integer. User access privilege level.</summary>
        ACCESS_LEVEL_1 = 1,

        /// <summary>Actelis-Access-Level-2 (Type 2). Integer. Secondary access privilege level.</summary>
        ACCESS_LEVEL_2 = 2,

        /// <summary>Actelis-Access-Level-3 (Type 3). Integer. Tertiary access privilege level.</summary>
        ACCESS_LEVEL_3 = 3,

        /// <summary>Actelis-Service-Profile (Type 4). String. Service profile name.</summary>
        SERVICE_PROFILE = 4,

        /// <summary>Actelis-Management-Priority (Type 5). Integer. Management session priority.</summary>
        MANAGEMENT_PRIORITY = 5
    }

    /// <summary>
    /// Actelis-Access-Level attribute values (Types 1, 2, 3).
    /// </summary>
    public enum ACTELIS_ACCESS_LEVEL
    {
        /// <summary>No access.</summary>
        NONE = 0,

        /// <summary>Read-only access.</summary>
        READ_ONLY = 1,

        /// <summary>Read-write access.</summary>
        READ_WRITE = 2,

        /// <summary>Administrator access (full control).</summary>
        ADMIN = 3
    }

    /// <summary>
    /// Actelis-Management-Priority attribute values (Type 5).
    /// </summary>
    public enum ACTELIS_MANAGEMENT_PRIORITY
    {
        /// <summary>Low management priority.</summary>
        LOW = 0,

        /// <summary>Normal management priority.</summary>
        NORMAL = 1,

        /// <summary>High management priority.</summary>
        HIGH = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Actelis Networks
    /// (IANA PEN 5468) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.actelis</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Actelis Networks' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 5468</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Actelis Networks EFM (Ethernet in the First Mile)
    /// and bonded-copper equipment for access level control and service profile assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ActelisAttributes.AccessLevel1(ACTELIS_ACCESS_LEVEL.ADMIN));
    /// packet.SetAttribute(ActelisAttributes.ServiceProfile("premium"));
    /// packet.SetAttribute(ActelisAttributes.ManagementPriority(ACTELIS_MANAGEMENT_PRIORITY.HIGH));
    /// </code>
    /// </remarks>
    public static class ActelisAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Actelis Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 5468;

        #region Integer Attributes

        /// <summary>
        /// Creates an Actelis-Access-Level-1 attribute (Type 1) with the specified access level.
        /// </summary>
        /// <param name="value">The user access privilege level. See <see cref="ACTELIS_ACCESS_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLevel1(ACTELIS_ACCESS_LEVEL value)
        {
            return CreateInteger(ActelisAttributeType.ACCESS_LEVEL_1, (int)value);
        }

        /// <summary>
        /// Creates an Actelis-Access-Level-2 attribute (Type 2) with the specified access level.
        /// </summary>
        /// <param name="value">The secondary access privilege level. See <see cref="ACTELIS_ACCESS_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLevel2(ACTELIS_ACCESS_LEVEL value)
        {
            return CreateInteger(ActelisAttributeType.ACCESS_LEVEL_2, (int)value);
        }

        /// <summary>
        /// Creates an Actelis-Access-Level-3 attribute (Type 3) with the specified access level.
        /// </summary>
        /// <param name="value">The tertiary access privilege level. See <see cref="ACTELIS_ACCESS_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLevel3(ACTELIS_ACCESS_LEVEL value)
        {
            return CreateInteger(ActelisAttributeType.ACCESS_LEVEL_3, (int)value);
        }

        /// <summary>
        /// Creates an Actelis-Management-Priority attribute (Type 5) with the specified priority.
        /// </summary>
        /// <param name="value">The management session priority. See <see cref="ACTELIS_MANAGEMENT_PRIORITY"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ManagementPriority(ACTELIS_MANAGEMENT_PRIORITY value)
        {
            return CreateInteger(ActelisAttributeType.MANAGEMENT_PRIORITY, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Actelis-Service-Profile attribute (Type 4) with the specified profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(ActelisAttributeType.SERVICE_PROFILE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Actelis attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ActelisAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Actelis attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ActelisAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}