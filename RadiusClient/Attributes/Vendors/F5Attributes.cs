using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an F5 Networks (IANA PEN 3375) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.f5</c>.
    /// </summary>
    public enum F5AttributeType : byte
    {
        /// <summary>F5-LTM-User-Role (Type 1). Integer. LTM user role.</summary>
        LTM_USER_ROLE = 1,

        /// <summary>F5-LTM-User-Role-Universal (Type 2). Integer. LTM user role universal flag.</summary>
        LTM_USER_ROLE_UNIVERSAL = 2,

        /// <summary>F5-LTM-User-Partition (Type 3). String. LTM administrative partition name.</summary>
        LTM_USER_PARTITION = 3,

        /// <summary>F5-LTM-User-Console (Type 4). Integer. LTM user console access flag.</summary>
        LTM_USER_CONSOLE = 4,

        /// <summary>F5-LTM-User-Shell (Type 5). String. LTM user shell.</summary>
        LTM_USER_SHELL = 5,

        /// <summary>F5-LTM-User-Context-1 (Type 10). Integer. LTM user context 1.</summary>
        LTM_USER_CONTEXT_1 = 10,

        /// <summary>F5-LTM-User-Context-2 (Type 11). Integer. LTM user context 2.</summary>
        LTM_USER_CONTEXT_2 = 11,

        /// <summary>F5-LTM-User-Info-1 (Type 12). String. LTM user info 1.</summary>
        LTM_USER_INFO_1 = 12,

        /// <summary>F5-LTM-User-Info-2 (Type 13). String. LTM user info 2.</summary>
        LTM_USER_INFO_2 = 13,

        /// <summary>F5-LTM-Audit-Msg (Type 14). String. LTM audit message.</summary>
        LTM_AUDIT_MSG = 14
    }

    /// <summary>
    /// F5-LTM-User-Role attribute values (Type 1).
    /// </summary>
    public enum F5_LTM_USER_ROLE
    {
        /// <summary>Administrator role (full access).</summary>
        ADMINISTRATOR = 0,

        /// <summary>Resource administrator role.</summary>
        RESOURCE_ADMINISTRATOR = 20,

        /// <summary>User manager role.</summary>
        USER_MANAGER = 40,

        /// <summary>Manager role.</summary>
        MANAGER = 100,

        /// <summary>Application editor role.</summary>
        APPLICATION_EDITOR = 300,

        /// <summary>Operator role.</summary>
        OPERATOR = 400,

        /// <summary>Guest role (read-only).</summary>
        GUEST = 500,

        /// <summary>Policy editor role.</summary>
        POLICY_EDITOR = 600,

        /// <summary>iRules manager role.</summary>
        IRULES_MANAGER = 700,

        /// <summary>Application security policy editor role.</summary>
        APPLICATION_SECURITY_POLICY_EDITOR = 800,

        /// <summary>No access.</summary>
        NO_ACCESS = 900
    }

    /// <summary>
    /// F5-LTM-User-Role-Universal attribute values (Type 2).
    /// </summary>
    public enum F5_LTM_USER_ROLE_UNIVERSAL
    {
        /// <summary>Role is not universal (partition-specific).</summary>
        DISABLED = 0,

        /// <summary>Role is universal (applies to all partitions).</summary>
        ENABLED = 1
    }

    /// <summary>
    /// F5-LTM-User-Console attribute values (Type 4).
    /// </summary>
    public enum F5_LTM_USER_CONSOLE
    {
        /// <summary>Console access disabled.</summary>
        DISABLED = 0,

        /// <summary>Console access enabled.</summary>
        ENABLED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing F5 Networks
    /// (IANA PEN 3375) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.f5</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// F5's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3375</c>.
    /// </para>
    /// <para>
    /// These attributes are used by F5 Networks BIG-IP LTM (Local Traffic Manager)
    /// and other BIG-IP modules for RADIUS-based administrative authentication,
    /// covering user role assignment, universal role scope, administrative
    /// partition selection, console access control, user shell assignment,
    /// user context/info fields, and audit message logging.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(F5Attributes.LtmUserRole(F5_LTM_USER_ROLE.ADMINISTRATOR));
    /// packet.SetAttribute(F5Attributes.LtmUserRoleUniversal(F5_LTM_USER_ROLE_UNIVERSAL.ENABLED));
    /// packet.SetAttribute(F5Attributes.LtmUserPartition("Common"));
    /// packet.SetAttribute(F5Attributes.LtmUserConsole(F5_LTM_USER_CONSOLE.ENABLED));
    /// packet.SetAttribute(F5Attributes.LtmUserShell("/bin/tmsh"));
    /// </code>
    /// </remarks>
    public static class F5Attributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for F5 Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3375;

        #region Integer Attributes

        /// <summary>
        /// Creates an F5-LTM-User-Role attribute (Type 1) with the specified role.
        /// </summary>
        /// <param name="value">The LTM user role. See <see cref="F5_LTM_USER_ROLE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LtmUserRole(F5_LTM_USER_ROLE value)
        {
            return CreateInteger(F5AttributeType.LTM_USER_ROLE, (int)value);
        }

        /// <summary>
        /// Creates an F5-LTM-User-Role-Universal attribute (Type 2) with the specified setting.
        /// </summary>
        /// <param name="value">The LTM user role universal flag. See <see cref="F5_LTM_USER_ROLE_UNIVERSAL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LtmUserRoleUniversal(F5_LTM_USER_ROLE_UNIVERSAL value)
        {
            return CreateInteger(F5AttributeType.LTM_USER_ROLE_UNIVERSAL, (int)value);
        }

        /// <summary>
        /// Creates an F5-LTM-User-Console attribute (Type 4) with the specified setting.
        /// </summary>
        /// <param name="value">The console access flag. See <see cref="F5_LTM_USER_CONSOLE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LtmUserConsole(F5_LTM_USER_CONSOLE value)
        {
            return CreateInteger(F5AttributeType.LTM_USER_CONSOLE, (int)value);
        }

        /// <summary>
        /// Creates an F5-LTM-User-Context-1 attribute (Type 10) with the specified value.
        /// </summary>
        /// <param name="value">The LTM user context 1 value.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LtmUserContext1(int value)
        {
            return CreateInteger(F5AttributeType.LTM_USER_CONTEXT_1, value);
        }

        /// <summary>
        /// Creates an F5-LTM-User-Context-2 attribute (Type 11) with the specified value.
        /// </summary>
        /// <param name="value">The LTM user context 2 value.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LtmUserContext2(int value)
        {
            return CreateInteger(F5AttributeType.LTM_USER_CONTEXT_2, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an F5-LTM-User-Partition attribute (Type 3) with the specified partition name.
        /// </summary>
        /// <param name="value">The LTM administrative partition name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LtmUserPartition(string value)
        {
            return CreateString(F5AttributeType.LTM_USER_PARTITION, value);
        }

        /// <summary>
        /// Creates an F5-LTM-User-Shell attribute (Type 5) with the specified shell.
        /// </summary>
        /// <param name="value">The LTM user shell (e.g. <c>/bin/tmsh</c>, <c>/bin/bash</c>). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LtmUserShell(string value)
        {
            return CreateString(F5AttributeType.LTM_USER_SHELL, value);
        }

        /// <summary>
        /// Creates an F5-LTM-User-Info-1 attribute (Type 12) with the specified information.
        /// </summary>
        /// <param name="value">The LTM user info 1 string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LtmUserInfo1(string value)
        {
            return CreateString(F5AttributeType.LTM_USER_INFO_1, value);
        }

        /// <summary>
        /// Creates an F5-LTM-User-Info-2 attribute (Type 13) with the specified information.
        /// </summary>
        /// <param name="value">The LTM user info 2 string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LtmUserInfo2(string value)
        {
            return CreateString(F5AttributeType.LTM_USER_INFO_2, value);
        }

        /// <summary>
        /// Creates an F5-LTM-Audit-Msg attribute (Type 14) with the specified audit message.
        /// </summary>
        /// <param name="value">The LTM audit message. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LtmAuditMsg(string value)
        {
            return CreateString(F5AttributeType.LTM_AUDIT_MSG, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified F5 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(F5AttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified F5 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(F5AttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}