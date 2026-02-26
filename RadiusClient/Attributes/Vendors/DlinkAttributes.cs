using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a D-Link (IANA PEN 171) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.dlink</c>.
    /// </summary>
    public enum DlinkAttributeType : byte
    {
        /// <summary>Dlink-User-Priv (Type 1). Integer. User privilege level.</summary>
        USER_PRIV = 1,

        /// <summary>Dlink-VLAN-Name (Type 2). String. VLAN name to assign.</summary>
        VLAN_NAME = 2,

        /// <summary>Dlink-ACL-Number (Type 3). Integer. ACL number to apply.</summary>
        ACL_NUMBER = 3,

        /// <summary>Dlink-Ingress-Bandwidth (Type 4). Integer. Ingress bandwidth in Kbps.</summary>
        INGRESS_BANDWIDTH = 4,

        /// <summary>Dlink-Egress-Bandwidth (Type 5). Integer. Egress bandwidth in Kbps.</summary>
        EGRESS_BANDWIDTH = 5,

        /// <summary>Dlink-802.1p-Default-Priority (Type 6). Integer. Default 802.1p priority value.</summary>
        DEFAULT_PRIORITY_8021P = 6
    }

    /// <summary>
    /// Dlink-User-Priv attribute values (Type 1).
    /// </summary>
    public enum DLINK_USER_PRIV
    {
        /// <summary>User level (read-only access).</summary>
        USER = 1,

        /// <summary>Operator level (limited configuration).</summary>
        OPERATOR = 2,

        /// <summary>Administrator level (full access).</summary>
        ADMIN = 3,

        /// <summary>Power user level.</summary>
        POWER_USER = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing D-Link
    /// (IANA PEN 171) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.dlink</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// D-Link's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 171</c>.
    /// </para>
    /// <para>
    /// These attributes are used by D-Link managed switches and wireless access
    /// points for RADIUS-based CLI privilege level assignment, VLAN assignment
    /// by name, ACL enforcement, ingress/egress bandwidth provisioning, and
    /// 802.1p default priority assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(DlinkAttributes.UserPriv(DLINK_USER_PRIV.ADMIN));
    /// packet.SetAttribute(DlinkAttributes.VlanName("management"));
    /// packet.SetAttribute(DlinkAttributes.IngressBandwidth(100000));
    /// packet.SetAttribute(DlinkAttributes.EgressBandwidth(50000));
    /// packet.SetAttribute(DlinkAttributes.DefaultPriority8021p(5));
    /// </code>
    /// </remarks>
    public static class DlinkAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for D-Link.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 171;

        #region Integer Attributes

        /// <summary>
        /// Creates a Dlink-User-Priv attribute (Type 1) with the specified privilege level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="DLINK_USER_PRIV"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserPriv(DLINK_USER_PRIV value)
        {
            return CreateInteger(DlinkAttributeType.USER_PRIV, (int)value);
        }

        /// <summary>
        /// Creates a Dlink-ACL-Number attribute (Type 3) with the specified ACL number.
        /// </summary>
        /// <param name="value">The ACL number to apply.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AclNumber(int value)
        {
            return CreateInteger(DlinkAttributeType.ACL_NUMBER, value);
        }

        /// <summary>
        /// Creates a Dlink-Ingress-Bandwidth attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The ingress bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IngressBandwidth(int value)
        {
            return CreateInteger(DlinkAttributeType.INGRESS_BANDWIDTH, value);
        }

        /// <summary>
        /// Creates a Dlink-Egress-Bandwidth attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The egress bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes EgressBandwidth(int value)
        {
            return CreateInteger(DlinkAttributeType.EGRESS_BANDWIDTH, value);
        }

        /// <summary>
        /// Creates a Dlink-802.1p-Default-Priority attribute (Type 6) with the specified priority.
        /// </summary>
        /// <param name="value">The default 802.1p priority value (0–7).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DefaultPriority8021p(int value)
        {
            return CreateInteger(DlinkAttributeType.DEFAULT_PRIORITY_8021P, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Dlink-VLAN-Name attribute (Type 2) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(DlinkAttributeType.VLAN_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified D-Link attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(DlinkAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified D-Link attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(DlinkAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}