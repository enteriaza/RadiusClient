using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Dell EMC (IANA PEN 674) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.dellemc</c>.
    /// </summary>
    public enum DellEmcAttributeType : byte
    {
        /// <summary>Dell-EMC-Privilege-Level (Type 1). Integer. CLI privilege level.</summary>
        PRIVILEGE_LEVEL = 1,

        /// <summary>Dell-EMC-User-Role (Type 2). String. User role name.</summary>
        USER_ROLE = 2,

        /// <summary>Dell-EMC-AVPair (Type 3). String. Attribute-value pair string.</summary>
        AVPAIR = 3,

        /// <summary>Dell-EMC-VLAN-Name (Type 4). String. VLAN name to assign.</summary>
        VLAN_NAME = 4,

        /// <summary>Dell-EMC-Bandwidth-Max-Up (Type 5). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 5,

        /// <summary>Dell-EMC-Bandwidth-Max-Down (Type 6). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 6
    }

    /// <summary>
    /// Dell-EMC-Privilege-Level attribute values (Type 1).
    /// </summary>
    public enum DELLEMC_PRIVILEGE_LEVEL
    {
        /// <summary>User level (read-only access).</summary>
        USER = 0,

        /// <summary>Operator level (limited configuration).</summary>
        OPERATOR = 1,

        /// <summary>Network administrator level.</summary>
        NETWORK_ADMIN = 14,

        /// <summary>Super-user / system administrator level.</summary>
        SYSADMIN = 15
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Dell EMC
    /// (IANA PEN 674) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.dellemc</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Dell EMC's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 674</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Dell EMC (formerly Force10 / Dell Networking)
    /// switches and routers, including the OS10, OS9, and PowerSwitch platforms,
    /// for RADIUS-based CLI privilege level assignment, user role mapping,
    /// VLAN assignment, bandwidth provisioning, and general-purpose
    /// attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(DellEmcAttributes.PrivilegeLevel(DELLEMC_PRIVILEGE_LEVEL.SYSADMIN));
    /// packet.SetAttribute(DellEmcAttributes.UserRole("network-admin"));
    /// packet.SetAttribute(DellEmcAttributes.VlanName("management"));
    /// packet.SetAttribute(DellEmcAttributes.BandwidthMaxDown(100000));
    /// </code>
    /// </remarks>
    public static class DellEmcAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Dell EMC (Force10 / Dell Networking).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 674;

        #region Integer Attributes

        /// <summary>
        /// Creates a Dell-EMC-Privilege-Level attribute (Type 1) with the specified privilege level.
        /// </summary>
        /// <param name="value">The CLI privilege level. See <see cref="DELLEMC_PRIVILEGE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(DELLEMC_PRIVILEGE_LEVEL value)
        {
            return CreateInteger(DellEmcAttributeType.PRIVILEGE_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Dell-EMC-Bandwidth-Max-Up attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(DellEmcAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Dell-EMC-Bandwidth-Max-Down attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(DellEmcAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Dell-EMC-User-Role attribute (Type 2) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(DellEmcAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates a Dell-EMC-AVPair attribute (Type 3) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(DellEmcAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Dell-EMC-VLAN-Name attribute (Type 4) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(DellEmcAttributeType.VLAN_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Dell EMC attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(DellEmcAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Dell EMC attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(DellEmcAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}