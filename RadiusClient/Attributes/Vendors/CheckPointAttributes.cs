using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Check Point Software Technologies (IANA PEN 2620) vendor-specific
    /// RADIUS attribute type, as defined in the FreeRADIUS <c>dictionary.checkpoint</c>.
    /// </summary>
    public enum CheckPointAttributeType : byte
    {
        /// <summary>CP-Firewall-1-Group (Type 1). String. Firewall-1 / SmartCenter group name.</summary>
        FIREWALL_1_GROUP = 1,

        /// <summary>CP-CUID (Type 2). String. Connection Unique Identifier.</summary>
        CUID = 2,

        /// <summary>CP-MSID (Type 3). String. Machine Session Identifier.</summary>
        MSID = 3,

        /// <summary>CP-User-Group (Type 4). String. User group name.</summary>
        USER_GROUP = 4,

        /// <summary>CP-Gaia-User-Role (Type 229). String. Gaia OS user role.</summary>
        GAIA_USER_ROLE = 229,

        /// <summary>CP-Gaia-SuperUser-Access (Type 230). Integer. Gaia super-user access flag.</summary>
        GAIA_SUPERUSER_ACCESS = 230,

        /// <summary>CP-Max-Concurrent-Logins (Type 90). Integer. Maximum concurrent logins allowed.</summary>
        MAX_CONCURRENT_LOGINS = 90,

        /// <summary>CP-User-Domain (Type 5). String. User authentication domain.</summary>
        USER_DOMAIN = 5
    }

    /// <summary>
    /// CP-Gaia-SuperUser-Access attribute values (Type 230).
    /// </summary>
    public enum CP_GAIA_SUPERUSER_ACCESS
    {
        /// <summary>Super-user access disabled.</summary>
        DISABLED = 0,

        /// <summary>Super-user access enabled.</summary>
        ENABLED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Check Point Software
    /// Technologies (IANA PEN 2620) Vendor-Specific Attributes (VSAs), as defined in the
    /// FreeRADIUS <c>dictionary.checkpoint</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Check Point's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2620</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Check Point Firewall-1, SmartCenter / SmartConsole,
    /// Gaia OS, and VPN gateway platforms for RADIUS-based group assignment, connection/
    /// session identification (CUID/MSID), user role and domain mapping, Gaia super-user
    /// access control, and concurrent login limits.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CheckPointAttributes.Firewall1Group("vpn-users"));
    /// packet.SetAttribute(CheckPointAttributes.GaiaUserRole("adminRole"));
    /// packet.SetAttribute(CheckPointAttributes.GaiaSuperUserAccess(CP_GAIA_SUPERUSER_ACCESS.ENABLED));
    /// packet.SetAttribute(CheckPointAttributes.MaxConcurrentLogins(3));
    /// </code>
    /// </remarks>
    public static class CheckPointAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Check Point Software Technologies.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2620;

        #region Integer Attributes

        /// <summary>
        /// Creates a CP-Gaia-SuperUser-Access attribute (Type 230) with the specified setting.
        /// </summary>
        /// <param name="value">Whether Gaia super-user access is enabled. See <see cref="CP_GAIA_SUPERUSER_ACCESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes GaiaSuperUserAccess(CP_GAIA_SUPERUSER_ACCESS value)
        {
            return CreateInteger(CheckPointAttributeType.GAIA_SUPERUSER_ACCESS, (int)value);
        }

        /// <summary>
        /// Creates a CP-Max-Concurrent-Logins attribute (Type 90) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum concurrent logins allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxConcurrentLogins(int value)
        {
            return CreateInteger(CheckPointAttributeType.MAX_CONCURRENT_LOGINS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a CP-Firewall-1-Group attribute (Type 1) with the specified group name.
        /// </summary>
        /// <param name="value">The Firewall-1 / SmartCenter group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Firewall1Group(string value)
        {
            return CreateString(CheckPointAttributeType.FIREWALL_1_GROUP, value);
        }

        /// <summary>
        /// Creates a CP-CUID attribute (Type 2) with the specified Connection Unique Identifier.
        /// </summary>
        /// <param name="value">The Connection Unique Identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Cuid(string value)
        {
            return CreateString(CheckPointAttributeType.CUID, value);
        }

        /// <summary>
        /// Creates a CP-MSID attribute (Type 3) with the specified Machine Session Identifier.
        /// </summary>
        /// <param name="value">The Machine Session Identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Msid(string value)
        {
            return CreateString(CheckPointAttributeType.MSID, value);
        }

        /// <summary>
        /// Creates a CP-User-Group attribute (Type 4) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(CheckPointAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a CP-User-Domain attribute (Type 5) with the specified domain.
        /// </summary>
        /// <param name="value">The user authentication domain. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserDomain(string value)
        {
            return CreateString(CheckPointAttributeType.USER_DOMAIN, value);
        }

        /// <summary>
        /// Creates a CP-Gaia-User-Role attribute (Type 229) with the specified Gaia role.
        /// </summary>
        /// <param name="value">The Gaia OS user role. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GaiaUserRole(string value)
        {
            return CreateString(CheckPointAttributeType.GAIA_USER_ROLE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Check Point attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(CheckPointAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Check Point attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(CheckPointAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}