using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Brocade / Foundry Networks (IANA PEN 1991) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.brocade</c>.
    /// </summary>
    public enum BrocadeAttributeType : byte
    {
        /// <summary>Brocade-Privilege-Level (Type 1). Integer. CLI privilege level.</summary>
        PRIVILEGE_LEVEL = 1,

        /// <summary>Brocade-Fabric-Role (Type 2). String. Fabric OS role name.</summary>
        FABRIC_ROLE = 2,

        /// <summary>Brocade-Access-List (Type 3). String. Access list name to apply.</summary>
        ACCESS_LIST = 3,

        /// <summary>Brocade-VLAN-List (Type 4). String. VLAN list to assign.</summary>
        VLAN_LIST = 4,

        /// <summary>Brocade-Home-LF-Role (Type 5). String. Home logical fabric role.</summary>
        HOME_LF_ROLE = 5,

        /// <summary>Brocade-LF-Access-List (Type 6). String. Logical fabric access list.</summary>
        LF_ACCESS_LIST = 6,

        /// <summary>Brocade-Role-Name (Type 7). String. Role name to assign.</summary>
        ROLE_NAME = 7,

        /// <summary>Brocade-AVPair1 (Type 8). String. Attribute-value pair 1.</summary>
        AVPAIR1 = 8,

        /// <summary>Brocade-AVPair2 (Type 9). String. Attribute-value pair 2.</summary>
        AVPAIR2 = 9,

        /// <summary>Brocade-AVPair3 (Type 10). String. Attribute-value pair 3.</summary>
        AVPAIR3 = 10,

        /// <summary>Brocade-AVPair4 (Type 11). String. Attribute-value pair 4.</summary>
        AVPAIR4 = 11,

        /// <summary>Brocade-AVPair5 (Type 12). String. Attribute-value pair 5.</summary>
        AVPAIR5 = 12
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Brocade / Foundry Networks
    /// (IANA PEN 1991) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.brocade</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Brocade's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 1991</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Brocade (now Broadcom) ICX, VDX, and Fabric OS
    /// switches and routers for RADIUS-based CLI privilege level assignment, Fabric
    /// OS role mapping, VLAN assignment, access list enforcement, logical fabric
    /// access control, and general attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(BrocadeAttributes.PrivilegeLevel(0));
    /// packet.SetAttribute(BrocadeAttributes.RoleName("network-admin"));
    /// packet.SetAttribute(BrocadeAttributes.VlanList("100,200,300"));
    /// </code>
    /// </remarks>
    public static class BrocadeAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Brocade / Foundry Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 1991;

        #region Integer Attributes

        /// <summary>
        /// Creates a Brocade-Privilege-Level attribute (Type 1) with the specified privilege level.
        /// </summary>
        /// <param name="value">The CLI privilege level (0 = super-user, 4 = port-config, 5 = read-only).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(int value)
        {
            return CreateInteger(BrocadeAttributeType.PRIVILEGE_LEVEL, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Brocade-Fabric-Role attribute (Type 2) with the specified role name.
        /// </summary>
        /// <param name="value">The Fabric OS role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FabricRole(string value)
        {
            return CreateString(BrocadeAttributeType.FABRIC_ROLE, value);
        }

        /// <summary>
        /// Creates a Brocade-Access-List attribute (Type 3) with the specified access list name.
        /// </summary>
        /// <param name="value">The access list name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessList(string value)
        {
            return CreateString(BrocadeAttributeType.ACCESS_LIST, value);
        }

        /// <summary>
        /// Creates a Brocade-VLAN-List attribute (Type 4) with the specified VLAN list.
        /// </summary>
        /// <param name="value">The VLAN list to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanList(string value)
        {
            return CreateString(BrocadeAttributeType.VLAN_LIST, value);
        }

        /// <summary>
        /// Creates a Brocade-Home-LF-Role attribute (Type 5) with the specified role.
        /// </summary>
        /// <param name="value">The home logical fabric role. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HomeLfRole(string value)
        {
            return CreateString(BrocadeAttributeType.HOME_LF_ROLE, value);
        }

        /// <summary>
        /// Creates a Brocade-LF-Access-List attribute (Type 6) with the specified access list.
        /// </summary>
        /// <param name="value">The logical fabric access list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LfAccessList(string value)
        {
            return CreateString(BrocadeAttributeType.LF_ACCESS_LIST, value);
        }

        /// <summary>
        /// Creates a Brocade-Role-Name attribute (Type 7) with the specified role name.
        /// </summary>
        /// <param name="value">The role name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RoleName(string value)
        {
            return CreateString(BrocadeAttributeType.ROLE_NAME, value);
        }

        /// <summary>
        /// Creates a Brocade-AVPair1 attribute (Type 8) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair1(string value)
        {
            return CreateString(BrocadeAttributeType.AVPAIR1, value);
        }

        /// <summary>
        /// Creates a Brocade-AVPair2 attribute (Type 9) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair2(string value)
        {
            return CreateString(BrocadeAttributeType.AVPAIR2, value);
        }

        /// <summary>
        /// Creates a Brocade-AVPair3 attribute (Type 10) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair3(string value)
        {
            return CreateString(BrocadeAttributeType.AVPAIR3, value);
        }

        /// <summary>
        /// Creates a Brocade-AVPair4 attribute (Type 11) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair4(string value)
        {
            return CreateString(BrocadeAttributeType.AVPAIR4, value);
        }

        /// <summary>
        /// Creates a Brocade-AVPair5 attribute (Type 12) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair5(string value)
        {
            return CreateString(BrocadeAttributeType.AVPAIR5, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Brocade attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(BrocadeAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Brocade attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(BrocadeAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}