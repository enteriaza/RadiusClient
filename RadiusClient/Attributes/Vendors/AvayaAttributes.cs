using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Avaya (IANA PEN 6813) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.avaya</c>.
    /// </summary>
    public enum AvayaAttributeType : byte
    {
        /// <summary>Avaya-Privilege-Level (Type 1). Integer. CLI privilege level.</summary>
        PRIVILEGE_LEVEL = 1,

        /// <summary>Avaya-Profile-Name (Type 2). String. User profile name.</summary>
        PROFILE_NAME = 2,

        /// <summary>Avaya-L2-Priority (Type 3). Integer. Layer 2 (802.1p) priority.</summary>
        L2_PRIORITY = 3,

        /// <summary>Avaya-VLAN-ID (Type 4). Integer. VLAN identifier to assign.</summary>
        VLAN_ID = 4,

        /// <summary>Avaya-Modular-CLI-Command (Type 5). String. Modular CLI command authorisation string.</summary>
        MODULAR_CLI_COMMAND = 5,

        /// <summary>Avaya-Access-Level (Type 6). Integer. Access level for management.</summary>
        ACCESS_LEVEL = 6,

        /// <summary>Avaya-Access-Level-Name (Type 7). String. Access level name.</summary>
        ACCESS_LEVEL_NAME = 7,

        /// <summary>Avaya-Policy-Name (Type 8). String. Policy name to apply.</summary>
        POLICY_NAME = 8,

        /// <summary>Avaya-Role-Name (Type 9). String. Role name to assign.</summary>
        ROLE_NAME = 9,

        /// <summary>Avaya-Access-Restriction (Type 10). String. Access restriction definition.</summary>
        ACCESS_RESTRICTION = 10,

        /// <summary>Avaya-MGMT-VLAN-ID (Type 11). Integer. Management VLAN identifier.</summary>
        MGMT_VLAN_ID = 11,

        /// <summary>Avaya-Fabric-Attach-Service-ID (Type 12). Integer. Fabric Attach I-SID assignment.</summary>
        FABRIC_ATTACH_SERVICE_ID = 12,

        /// <summary>Avaya-Fabric-Attach-VLAN (Type 13). Integer. Fabric Attach VLAN assignment.</summary>
        FABRIC_ATTACH_VLAN = 13,

        /// <summary>Avaya-Fabric-Attach-Data (Type 14). String. Fabric Attach data payload.</summary>
        FABRIC_ATTACH_DATA = 14
    }

    /// <summary>
    /// Avaya-Privilege-Level attribute values (Type 1).
    /// </summary>
    public enum AVAYA_PRIVILEGE_LEVEL
    {
        /// <summary>Non-privileged (read-only) access.</summary>
        NON_PRIVILEGED = 0,

        /// <summary>Privileged (read-write) access.</summary>
        PRIVILEGED = 1
    }

    /// <summary>
    /// Avaya-Access-Level attribute values (Type 6).
    /// </summary>
    public enum AVAYA_ACCESS_LEVEL
    {
        /// <summary>Read-only access.</summary>
        READ_ONLY = 0,

        /// <summary>Layer 2 read-write access.</summary>
        L2_READ_WRITE = 1,

        /// <summary>Layer 3 read-write access.</summary>
        L3_READ_WRITE = 2,

        /// <summary>Full read-write access.</summary>
        READ_WRITE = 3,

        /// <summary>Full read-write-all access.</summary>
        READ_WRITE_ALL = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Avaya
    /// (IANA PEN 6813) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.avaya</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Avaya's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 6813</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Avaya ERS (Ethernet Routing Switch), VSP
    /// (Virtual Services Platform), and communication platforms for RADIUS-based
    /// CLI privilege level assignment, VLAN/management VLAN mapping, policy and
    /// role enforcement, Fabric Attach I-SID assignment, and modular CLI command
    /// authorisation.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AvayaAttributes.PrivilegeLevel(AVAYA_PRIVILEGE_LEVEL.PRIVILEGED));
    /// packet.SetAttribute(AvayaAttributes.VlanId(100));
    /// packet.SetAttribute(AvayaAttributes.RoleName("network-admin"));
    /// packet.SetAttribute(AvayaAttributes.AccessLevel(AVAYA_ACCESS_LEVEL.READ_WRITE_ALL));
    /// </code>
    /// </remarks>
    public static class AvayaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Avaya.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 6813;

        #region Integer Attributes

        /// <summary>
        /// Creates an Avaya-Privilege-Level attribute (Type 1) with the specified privilege level.
        /// </summary>
        /// <param name="value">The CLI privilege level. See <see cref="AVAYA_PRIVILEGE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(AVAYA_PRIVILEGE_LEVEL value)
        {
            return CreateInteger(AvayaAttributeType.PRIVILEGE_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates an Avaya-L2-Priority attribute (Type 3) with the specified 802.1p priority.
        /// </summary>
        /// <param name="value">The Layer 2 (802.1p) priority (0–7).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes L2Priority(int value)
        {
            return CreateInteger(AvayaAttributeType.L2_PRIORITY, value);
        }

        /// <summary>
        /// Creates an Avaya-VLAN-ID attribute (Type 4) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier to assign.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(AvayaAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates an Avaya-Access-Level attribute (Type 6) with the specified access level.
        /// </summary>
        /// <param name="value">The management access level. See <see cref="AVAYA_ACCESS_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLevel(AVAYA_ACCESS_LEVEL value)
        {
            return CreateInteger(AvayaAttributeType.ACCESS_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates an Avaya-MGMT-VLAN-ID attribute (Type 11) with the specified management VLAN.
        /// </summary>
        /// <param name="value">The management VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MgmtVlanId(int value)
        {
            return CreateInteger(AvayaAttributeType.MGMT_VLAN_ID, value);
        }

        /// <summary>
        /// Creates an Avaya-Fabric-Attach-Service-ID attribute (Type 12) with the specified I-SID.
        /// </summary>
        /// <param name="value">The Fabric Attach I-SID assignment.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FabricAttachServiceId(int value)
        {
            return CreateInteger(AvayaAttributeType.FABRIC_ATTACH_SERVICE_ID, value);
        }

        /// <summary>
        /// Creates an Avaya-Fabric-Attach-VLAN attribute (Type 13) with the specified VLAN.
        /// </summary>
        /// <param name="value">The Fabric Attach VLAN assignment.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FabricAttachVlan(int value)
        {
            return CreateInteger(AvayaAttributeType.FABRIC_ATTACH_VLAN, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Avaya-Profile-Name attribute (Type 2) with the specified profile name.
        /// </summary>
        /// <param name="value">The user profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProfileName(string value)
        {
            return CreateString(AvayaAttributeType.PROFILE_NAME, value);
        }

        /// <summary>
        /// Creates an Avaya-Modular-CLI-Command attribute (Type 5) with the specified command string.
        /// </summary>
        /// <param name="value">The modular CLI command authorisation string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModularCliCommand(string value)
        {
            return CreateString(AvayaAttributeType.MODULAR_CLI_COMMAND, value);
        }

        /// <summary>
        /// Creates an Avaya-Access-Level-Name attribute (Type 7) with the specified name.
        /// </summary>
        /// <param name="value">The access level name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessLevelName(string value)
        {
            return CreateString(AvayaAttributeType.ACCESS_LEVEL_NAME, value);
        }

        /// <summary>
        /// Creates an Avaya-Policy-Name attribute (Type 8) with the specified policy name.
        /// </summary>
        /// <param name="value">The policy name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(AvayaAttributeType.POLICY_NAME, value);
        }

        /// <summary>
        /// Creates an Avaya-Role-Name attribute (Type 9) with the specified role name.
        /// </summary>
        /// <param name="value">The role name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RoleName(string value)
        {
            return CreateString(AvayaAttributeType.ROLE_NAME, value);
        }

        /// <summary>
        /// Creates an Avaya-Access-Restriction attribute (Type 10) with the specified restriction.
        /// </summary>
        /// <param name="value">The access restriction definition. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessRestriction(string value)
        {
            return CreateString(AvayaAttributeType.ACCESS_RESTRICTION, value);
        }

        /// <summary>
        /// Creates an Avaya-Fabric-Attach-Data attribute (Type 14) with the specified data.
        /// </summary>
        /// <param name="value">The Fabric Attach data payload. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FabricAttachData(string value)
        {
            return CreateString(AvayaAttributeType.FABRIC_ATTACH_DATA, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Avaya attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AvayaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Avaya attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AvayaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}