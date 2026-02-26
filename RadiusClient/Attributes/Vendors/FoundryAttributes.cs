using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Foundry Networks (IANA PEN 1991) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.foundry</c>.
    /// </summary>
    /// <remarks>
    /// Foundry Networks was acquired by Brocade Communications, which was later
    /// acquired by Ruckus Networks (now CommScope RUCKUS). These attributes are
    /// used by Foundry/Brocade/Ruckus ICX, FastIron, and NetIron platforms.
    /// </remarks>
    public enum FoundryAttributeType : byte
    {
        /// <summary>Foundry-Privilege-Level (Type 1). Integer. CLI privilege level.</summary>
        PRIVILEGE_LEVEL = 1,

        /// <summary>Foundry-Command-String (Type 2). String. Authorized command string.</summary>
        COMMAND_STRING = 2,

        /// <summary>Foundry-Command-Exception-Flag (Type 3). Integer. Command exception flag.</summary>
        COMMAND_EXCEPTION_FLAG = 3,

        /// <summary>Foundry-INM-Privilege (Type 4). Integer. INM privilege level.</summary>
        INM_PRIVILEGE = 4,

        /// <summary>Foundry-Access-List (Type 5). String. Access list name or number.</summary>
        ACCESS_LIST = 5,

        /// <summary>Foundry-MAC-Authent-Needs-Reset (Type 6). Integer. MAC authentication needs reset flag.</summary>
        MAC_AUTHENT_NEEDS_RESET = 6,

        /// <summary>Foundry-AP-Name (Type 7). String. Access point name.</summary>
        AP_NAME = 7,

        /// <summary>Foundry-AP-Location (Type 8). String. Access point location.</summary>
        AP_LOCATION = 8,

        /// <summary>Foundry-MAC-Based-VLAN-QoS (Type 9). Integer. MAC-based VLAN QoS priority.</summary>
        MAC_BASED_VLAN_QOS = 9,

        /// <summary>Foundry-ACCT-Input-Octets-64 (Type 10). Integer. 64-bit accounting input octets (high 32 bits).</summary>
        ACCT_INPUT_OCTETS_64 = 10,

        /// <summary>Foundry-ACCT-Output-Octets-64 (Type 11). Integer. 64-bit accounting output octets (high 32 bits).</summary>
        ACCT_OUTPUT_OCTETS_64 = 11,

        /// <summary>Foundry-Assigned-VLAN-ID (Type 12). Integer. Assigned VLAN identifier.</summary>
        ASSIGNED_VLAN_ID = 12,

        /// <summary>Foundry-INM-Role-Aor-List (Type 13). String. INM role AOR list.</summary>
        INM_ROLE_AOR_LIST = 13,

        /// <summary>Foundry-SI-Context-Role (Type 14). String. SI context role name.</summary>
        SI_CONTEXT_ROLE = 14,

        /// <summary>Foundry-SI-Role-Template (Type 15). String. SI role template name.</summary>
        SI_ROLE_TEMPLATE = 15
    }

    /// <summary>
    /// Foundry-Privilege-Level attribute values (Type 1).
    /// </summary>
    public enum FOUNDRY_PRIVILEGE_LEVEL
    {
        /// <summary>User level (read-only access, level 0).</summary>
        USER = 0,

        /// <summary>Port configuration level (level 4).</summary>
        PORT_CONFIG = 4,

        /// <summary>Read-only level (level 5).</summary>
        READ_ONLY = 5,

        /// <summary>Super-user / administrator level (full access).</summary>
        SUPER_USER = 15
    }

    /// <summary>
    /// Foundry-Command-Exception-Flag attribute values (Type 3).
    /// </summary>
    public enum FOUNDRY_COMMAND_EXCEPTION_FLAG
    {
        /// <summary>Permit the command (allow list).</summary>
        PERMIT = 0,

        /// <summary>Deny the command (deny list).</summary>
        DENY = 1
    }

    /// <summary>
    /// Foundry-MAC-Authent-Needs-Reset attribute values (Type 6).
    /// </summary>
    public enum FOUNDRY_MAC_AUTHENT_NEEDS_RESET
    {
        /// <summary>No reset needed.</summary>
        NO = 0,

        /// <summary>Reset needed.</summary>
        YES = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Foundry Networks
    /// (IANA PEN 1991) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.foundry</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Foundry's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 1991</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Foundry Networks (now Brocade / Ruckus / CommScope)
    /// FastIron, NetIron, and ICX switches and routers for RADIUS-based CLI privilege
    /// level assignment, command authorization (with permit/deny exception lists),
    /// INM privilege and role management, access list enforcement, MAC authentication
    /// reset control, wireless AP identification (name and location), MAC-based
    /// VLAN QoS, 64-bit accounting counters, VLAN assignment, and SI context
    /// role/template mapping.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(FoundryAttributes.PrivilegeLevel(FOUNDRY_PRIVILEGE_LEVEL.SUPER_USER));
    /// packet.SetAttribute(FoundryAttributes.CommandString("show running-config"));
    /// packet.SetAttribute(FoundryAttributes.CommandExceptionFlag(FOUNDRY_COMMAND_EXCEPTION_FLAG.PERMIT));
    /// packet.SetAttribute(FoundryAttributes.AssignedVlanId(100));
    /// packet.SetAttribute(FoundryAttributes.AccessList("mgmt-acl"));
    /// </code>
    /// </remarks>
    public static class FoundryAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Foundry Networks (Brocade / Ruckus).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 1991;

        #region Integer Attributes

        /// <summary>
        /// Creates a Foundry-Privilege-Level attribute (Type 1) with the specified level.
        /// </summary>
        /// <param name="value">The CLI privilege level. See <see cref="FOUNDRY_PRIVILEGE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(FOUNDRY_PRIVILEGE_LEVEL value)
        {
            return CreateInteger(FoundryAttributeType.PRIVILEGE_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Foundry-Command-Exception-Flag attribute (Type 3) with the specified flag.
        /// </summary>
        /// <param name="value">The command exception flag. See <see cref="FOUNDRY_COMMAND_EXCEPTION_FLAG"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CommandExceptionFlag(FOUNDRY_COMMAND_EXCEPTION_FLAG value)
        {
            return CreateInteger(FoundryAttributeType.COMMAND_EXCEPTION_FLAG, (int)value);
        }

        /// <summary>
        /// Creates a Foundry-INM-Privilege attribute (Type 4) with the specified level.
        /// </summary>
        /// <param name="value">The INM privilege level.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes InmPrivilege(int value)
        {
            return CreateInteger(FoundryAttributeType.INM_PRIVILEGE, value);
        }

        /// <summary>
        /// Creates a Foundry-MAC-Authent-Needs-Reset attribute (Type 6) with the specified flag.
        /// </summary>
        /// <param name="value">The MAC authentication needs reset flag. See <see cref="FOUNDRY_MAC_AUTHENT_NEEDS_RESET"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MacAuthentNeedsReset(FOUNDRY_MAC_AUTHENT_NEEDS_RESET value)
        {
            return CreateInteger(FoundryAttributeType.MAC_AUTHENT_NEEDS_RESET, (int)value);
        }

        /// <summary>
        /// Creates a Foundry-MAC-Based-VLAN-QoS attribute (Type 9) with the specified priority.
        /// </summary>
        /// <param name="value">The MAC-based VLAN QoS priority (0–7).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MacBasedVlanQos(int value)
        {
            return CreateInteger(FoundryAttributeType.MAC_BASED_VLAN_QOS, value);
        }

        /// <summary>
        /// Creates a Foundry-ACCT-Input-Octets-64 attribute (Type 10) with the specified value.
        /// </summary>
        /// <param name="value">The 64-bit accounting input octets (high 32 bits).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctInputOctets64(int value)
        {
            return CreateInteger(FoundryAttributeType.ACCT_INPUT_OCTETS_64, value);
        }

        /// <summary>
        /// Creates a Foundry-ACCT-Output-Octets-64 attribute (Type 11) with the specified value.
        /// </summary>
        /// <param name="value">The 64-bit accounting output octets (high 32 bits).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctOutputOctets64(int value)
        {
            return CreateInteger(FoundryAttributeType.ACCT_OUTPUT_OCTETS_64, value);
        }

        /// <summary>
        /// Creates a Foundry-Assigned-VLAN-ID attribute (Type 12) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The assigned VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AssignedVlanId(int value)
        {
            return CreateInteger(FoundryAttributeType.ASSIGNED_VLAN_ID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Foundry-Command-String attribute (Type 2) with the specified command.
        /// </summary>
        /// <param name="value">The authorized command string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CommandString(string value)
        {
            return CreateString(FoundryAttributeType.COMMAND_STRING, value);
        }

        /// <summary>
        /// Creates a Foundry-Access-List attribute (Type 5) with the specified access list.
        /// </summary>
        /// <param name="value">The access list name or number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessList(string value)
        {
            return CreateString(FoundryAttributeType.ACCESS_LIST, value);
        }

        /// <summary>
        /// Creates a Foundry-AP-Name attribute (Type 7) with the specified access point name.
        /// </summary>
        /// <param name="value">The access point name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApName(string value)
        {
            return CreateString(FoundryAttributeType.AP_NAME, value);
        }

        /// <summary>
        /// Creates a Foundry-AP-Location attribute (Type 8) with the specified location.
        /// </summary>
        /// <param name="value">The access point location. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApLocation(string value)
        {
            return CreateString(FoundryAttributeType.AP_LOCATION, value);
        }

        /// <summary>
        /// Creates a Foundry-INM-Role-Aor-List attribute (Type 13) with the specified AOR list.
        /// </summary>
        /// <param name="value">The INM role AOR list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes InmRoleAorList(string value)
        {
            return CreateString(FoundryAttributeType.INM_ROLE_AOR_LIST, value);
        }

        /// <summary>
        /// Creates a Foundry-SI-Context-Role attribute (Type 14) with the specified context role.
        /// </summary>
        /// <param name="value">The SI context role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SiContextRole(string value)
        {
            return CreateString(FoundryAttributeType.SI_CONTEXT_ROLE, value);
        }

        /// <summary>
        /// Creates a Foundry-SI-Role-Template attribute (Type 15) with the specified template name.
        /// </summary>
        /// <param name="value">The SI role template name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SiRoleTemplate(string value)
        {
            return CreateString(FoundryAttributeType.SI_ROLE_TEMPLATE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Foundry attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(FoundryAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Foundry attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(FoundryAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}