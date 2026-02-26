using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Nexans (IANA PEN 266) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.nexans</c>.
    /// </summary>
    /// <remarks>
    /// Nexans is a global cable and connectivity manufacturer that also produces
    /// industrial managed Ethernet switches and networking equipment for harsh
    /// environments.
    /// </remarks>
    public enum NexansAttributeType : byte
    {
        /// <summary>Nexans-Port-Default-VLAN (Type 1). Integer. Default VLAN for the port.</summary>
        PORT_DEFAULT_VLAN = 1,

        /// <summary>Nexans-Port-Security-Enabled (Type 2). Integer. Port security enabled flag.</summary>
        PORT_SECURITY_ENABLED = 2,

        /// <summary>Nexans-Admin-Access-Level (Type 3). Integer. Administrative access level.</summary>
        ADMIN_ACCESS_LEVEL = 3,

        /// <summary>Nexans-VLAN-Name (Type 4). String. VLAN name.</summary>
        VLAN_NAME = 4,

        /// <summary>Nexans-Port-Speed (Type 5). Integer. Port speed setting.</summary>
        PORT_SPEED = 5,

        /// <summary>Nexans-Port-Status (Type 6). Integer. Port status.</summary>
        PORT_STATUS = 6
    }

    /// <summary>
    /// Nexans-Port-Security-Enabled attribute values (Type 2).
    /// </summary>
    public enum NEXANS_PORT_SECURITY_ENABLED
    {
        /// <summary>Port security disabled.</summary>
        DISABLED = 0,

        /// <summary>Port security enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Nexans-Admin-Access-Level attribute values (Type 3).
    /// </summary>
    public enum NEXANS_ADMIN_ACCESS_LEVEL
    {
        /// <summary>Read-only access.</summary>
        READ_ONLY = 0,

        /// <summary>Read-write access.</summary>
        READ_WRITE = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    /// <summary>
    /// Nexans-Port-Speed attribute values (Type 5).
    /// </summary>
    public enum NEXANS_PORT_SPEED
    {
        /// <summary>Auto-negotiation.</summary>
        AUTO = 0,

        /// <summary>10 Mbps.</summary>
        SPEED_10M = 1,

        /// <summary>100 Mbps.</summary>
        SPEED_100M = 2,

        /// <summary>1 Gbps.</summary>
        SPEED_1G = 3
    }

    /// <summary>
    /// Nexans-Port-Status attribute values (Type 6).
    /// </summary>
    public enum NEXANS_PORT_STATUS
    {
        /// <summary>Port disabled.</summary>
        DISABLED = 0,

        /// <summary>Port enabled.</summary>
        ENABLED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Nexans
    /// (IANA PEN 266) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.nexans</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Nexans' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 266</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Nexans industrial managed Ethernet switches
    /// for RADIUS-based port default VLAN assignment, port security control,
    /// administrative access level assignment, VLAN naming, port speed
    /// configuration, and port status management.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(NexansAttributes.AdminAccessLevel(NEXANS_ADMIN_ACCESS_LEVEL.ADMIN));
    /// packet.SetAttribute(NexansAttributes.PortDefaultVlan(100));
    /// packet.SetAttribute(NexansAttributes.PortSecurityEnabled(NEXANS_PORT_SECURITY_ENABLED.ENABLED));
    /// packet.SetAttribute(NexansAttributes.VlanName("production"));
    /// </code>
    /// </remarks>
    public static class NexansAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Nexans.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 266;

        #region Integer Attributes

        /// <summary>
        /// Creates a Nexans-Port-Default-VLAN attribute (Type 1) with the specified VLAN.
        /// </summary>
        /// <param name="value">The default VLAN for the port.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortDefaultVlan(int value)
        {
            return CreateInteger(NexansAttributeType.PORT_DEFAULT_VLAN, value);
        }

        /// <summary>
        /// Creates a Nexans-Port-Security-Enabled attribute (Type 2) with the specified setting.
        /// </summary>
        /// <param name="value">The port security enabled flag. See <see cref="NEXANS_PORT_SECURITY_ENABLED"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortSecurityEnabled(NEXANS_PORT_SECURITY_ENABLED value)
        {
            return CreateInteger(NexansAttributeType.PORT_SECURITY_ENABLED, (int)value);
        }

        /// <summary>
        /// Creates a Nexans-Admin-Access-Level attribute (Type 3) with the specified level.
        /// </summary>
        /// <param name="value">The administrative access level. See <see cref="NEXANS_ADMIN_ACCESS_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminAccessLevel(NEXANS_ADMIN_ACCESS_LEVEL value)
        {
            return CreateInteger(NexansAttributeType.ADMIN_ACCESS_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Nexans-Port-Speed attribute (Type 5) with the specified speed.
        /// </summary>
        /// <param name="value">The port speed setting. See <see cref="NEXANS_PORT_SPEED"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortSpeed(NEXANS_PORT_SPEED value)
        {
            return CreateInteger(NexansAttributeType.PORT_SPEED, (int)value);
        }

        /// <summary>
        /// Creates a Nexans-Port-Status attribute (Type 6) with the specified status.
        /// </summary>
        /// <param name="value">The port status. See <see cref="NEXANS_PORT_STATUS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortStatus(NEXANS_PORT_STATUS value)
        {
            return CreateInteger(NexansAttributeType.PORT_STATUS, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Nexans-VLAN-Name attribute (Type 4) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(NexansAttributeType.VLAN_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Nexans attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(NexansAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Nexans attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(NexansAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}