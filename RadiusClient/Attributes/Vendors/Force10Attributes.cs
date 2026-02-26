using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Force10 Networks (IANA PEN 6027) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.force10</c>.
    /// </summary>
    /// <remarks>
    /// Force10 Networks was acquired by Dell and later became part of Dell EMC Networking.
    /// These attributes are used by Force10 FTOS-based switches and routers
    /// (S-Series, Z-Series, E-Series, C-Series).
    /// </remarks>
    public enum Force10AttributeType : byte
    {
        /// <summary>Force10-Privilege-Level (Type 1). Integer. CLI privilege level.</summary>
        PRIVILEGE_LEVEL = 1,

        /// <summary>Force10-User-Role (Type 2). String. User role name.</summary>
        USER_ROLE = 2,

        /// <summary>Force10-AVPair (Type 3). String. Attribute-value pair string.</summary>
        AVPAIR = 3,

        /// <summary>Force10-VLAN-Name (Type 4). String. VLAN name to assign.</summary>
        VLAN_NAME = 4,

        /// <summary>Force10-Bandwidth-Max-Up (Type 5). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 5,

        /// <summary>Force10-Bandwidth-Max-Down (Type 6). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 6
    }

    /// <summary>
    /// Force10-Privilege-Level attribute values (Type 1).
    /// </summary>
    public enum FORCE10_PRIVILEGE_LEVEL
    {
        /// <summary>User level (read-only access).</summary>
        USER = 0,

        /// <summary>Operator level (limited configuration).</summary>
        OPERATOR = 1,

        /// <summary>Network administrator level.</summary>
        NETWORK_ADMIN = 14,

        /// <summary>Super-user / system administrator level (full access).</summary>
        SYSADMIN = 15
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Force10 Networks
    /// (IANA PEN 6027) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.force10</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Force10's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 6027</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Force10 Networks (now Dell EMC Networking)
    /// FTOS-based switches and routers for RADIUS-based CLI privilege level
    /// assignment, user role mapping, general-purpose attribute-value pair
    /// configuration, VLAN assignment by name, and upstream/downstream
    /// bandwidth provisioning.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(Force10Attributes.PrivilegeLevel(FORCE10_PRIVILEGE_LEVEL.SYSADMIN));
    /// packet.SetAttribute(Force10Attributes.UserRole("network-admin"));
    /// packet.SetAttribute(Force10Attributes.VlanName("management"));
    /// packet.SetAttribute(Force10Attributes.BandwidthMaxDown(100000));
    /// </code>
    /// </remarks>
    public static class Force10Attributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Force10 Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 6027;

        #region Integer Attributes

        /// <summary>
        /// Creates a Force10-Privilege-Level attribute (Type 1) with the specified privilege level.
        /// </summary>
        /// <param name="value">The CLI privilege level. See <see cref="FORCE10_PRIVILEGE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(FORCE10_PRIVILEGE_LEVEL value)
        {
            return CreateInteger(Force10AttributeType.PRIVILEGE_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Force10-Bandwidth-Max-Up attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(Force10AttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Force10-Bandwidth-Max-Down attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(Force10AttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Force10-User-Role attribute (Type 2) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(Force10AttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates a Force10-AVPair attribute (Type 3) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(Force10AttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Force10-VLAN-Name attribute (Type 4) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(Force10AttributeType.VLAN_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Force10 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(Force10AttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Force10 attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(Force10AttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}