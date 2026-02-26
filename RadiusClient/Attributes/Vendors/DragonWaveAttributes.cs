using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a DragonWave (IANA PEN 7262) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.dragonwave</c>.
    /// </summary>
    public enum DragonWaveAttributeType : byte
    {
        /// <summary>DragonWave-Privilege-Level (Type 1). Integer. User privilege level.</summary>
        PRIVILEGE_LEVEL = 1,

        /// <summary>DragonWave-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2
    }

    /// <summary>
    /// DragonWave-Privilege-Level attribute values (Type 1).
    /// </summary>
    public enum DRAGONWAVE_PRIVILEGE_LEVEL
    {
        /// <summary>Read-only user access.</summary>
        USER = 0,

        /// <summary>Read-write operator access.</summary>
        OPERATOR = 1,

        /// <summary>Super-user / administrator access.</summary>
        SUPER_USER = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing DragonWave
    /// (IANA PEN 7262) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.dragonwave</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// DragonWave's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 7262</c>.
    /// </para>
    /// <para>
    /// These attributes are used by DragonWave (now part of CommScope) point-to-point
    /// microwave radio platforms for RADIUS-based privilege level assignment and
    /// user group mapping during administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(DragonWaveAttributes.PrivilegeLevel(DRAGONWAVE_PRIVILEGE_LEVEL.SUPER_USER));
    /// packet.SetAttribute(DragonWaveAttributes.UserGroup("rf-engineers"));
    /// </code>
    /// </remarks>
    public static class DragonWaveAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for DragonWave (CommScope).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 7262;

        #region Integer Attributes

        /// <summary>
        /// Creates a DragonWave-Privilege-Level attribute (Type 1) with the specified level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="DRAGONWAVE_PRIVILEGE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(DRAGONWAVE_PRIVILEGE_LEVEL value)
        {
            return CreateInteger(DragonWaveAttributeType.PRIVILEGE_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a DragonWave-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(DragonWaveAttributeType.USER_GROUP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified DragonWave attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(DragonWaveAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified DragonWave attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(DragonWaveAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}