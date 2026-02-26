using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Hewlett-Packard / HPE (IANA PEN 11) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.hp</c>.
    /// </summary>
    /// <remarks>
    /// Hewlett-Packard (now Hewlett Packard Enterprise / HPE) uses vendor ID 11
    /// for its ProCurve / Aruba switch product line RADIUS attributes. This is
    /// separate from the HP MSM / Colubris (PEN 8744) and Aruba Networks (PEN 14823)
    /// vendor dictionaries.
    /// </remarks>
    public enum HpAttributeType : byte
    {
        /// <summary>HP-Privilege-Level (Type 1). Integer. CLI privilege level.</summary>
        PRIVILEGE_LEVEL = 1,

        /// <summary>HP-Command-String (Type 2). String. Authorized command string.</summary>
        COMMAND_STRING = 2,

        /// <summary>HP-Command-Exception (Type 3). Integer. Command exception (permit/deny) flag.</summary>
        COMMAND_EXCEPTION = 3,

        /// <summary>HP-Management-Protocol (Type 26). Integer. Management protocol type.</summary>
        MANAGEMENT_PROTOCOL = 26,

        /// <summary>HP-Port-Client-Limit-Dot1x (Type 10). Integer. 802.1X client limit per port.</summary>
        PORT_CLIENT_LIMIT_DOT1X = 10,

        /// <summary>HP-Port-Client-Limit-MA (Type 11). Integer. MAC authentication client limit per port.</summary>
        PORT_CLIENT_LIMIT_MA = 11,

        /// <summary>HP-Port-Client-Limit-WA (Type 12). Integer. Web authentication client limit per port.</summary>
        PORT_CLIENT_LIMIT_WA = 12,

        /// <summary>HP-Port-Auth-Mode-Dot1x (Type 13). Integer. 802.1X port authentication mode.</summary>
        PORT_AUTH_MODE_DOT1X = 13,

        /// <summary>HP-Port-MA-Port-Mode (Type 14). Integer. MAC authentication port mode.</summary>
        PORT_MA_PORT_MODE = 14,

        /// <summary>HP-Port-Bounce-Host (Type 23). Integer. Port bounce host flag.</summary>
        PORT_BOUNCE_HOST = 23,

        /// <summary>HP-Captive-Portal-URL (Type 24). String. Captive portal URL.</summary>
        CAPTIVE_PORTAL_URL = 24,

        /// <summary>HP-User-Role (Type 25). String. User role name.</summary>
        USER_ROLE = 25,

        /// <summary>HP-Port-Priority-Regeneration-Table (Type 40). String. 802.1p priority regeneration table.</summary>
        PORT_PRIORITY_REGENERATION_TABLE = 40,

        // /// <summary>HP-Cos (Type 40). String. Class of service (alias for priority regeneration table).</summary>
        // COS = 40,

        /// <summary>HP-Bandwidth-Max-Ingress (Type 46). Integer. Maximum ingress bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_INGRESS = 46,

        /// <summary>HP-Bandwidth-Max-Egress (Type 48). Integer. Maximum egress bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_EGRESS = 48,

        /// <summary>HP-Nas-Filter-Rule (Type 61). String. NAS filter rule.</summary>
        NAS_FILTER_RULE = 61,

        /// <summary>HP-NAS-Rules-IPv6 (Type 63). String. NAS filter rule for IPv6.</summary>
        NAS_RULES_IPV6 = 63,

        /// <summary>HP-Egress-VLANID (Type 64). Integer. Egress VLAN identifier.</summary>
        EGRESS_VLANID = 64,

        /// <summary>HP-Egress-VLAN-Name (Type 65). String. Egress VLAN name.</summary>
        EGRESS_VLAN_NAME = 65
    }

    /// <summary>
    /// HP-Privilege-Level attribute values (Type 1).
    /// </summary>
    public enum HP_PRIVILEGE_LEVEL
    {
        /// <summary>User level (read-only access).</summary>
        USER = 0,

        /// <summary>Operator level (limited configuration).</summary>
        OPERATOR = 1,

        /// <summary>Manager level (full access).</summary>
        MANAGER = 2,

        /// <summary>Super-user level.</summary>
        SUPER_USER = 15
    }

    /// <summary>
    /// HP-Command-Exception attribute values (Type 3).
    /// </summary>
    public enum HP_COMMAND_EXCEPTION
    {
        /// <summary>Permit the command.</summary>
        PERMIT = 0,

        /// <summary>Deny the command.</summary>
        DENY = 1
    }

    /// <summary>
    /// HP-Management-Protocol attribute values (Type 26).
    /// </summary>
    public enum HP_MANAGEMENT_PROTOCOL
    {
        /// <summary>HTTP management protocol.</summary>
        HTTP = 5,

        /// <summary>HTTPS management protocol.</summary>
        HTTPS = 6,

        /// <summary>SSH management protocol.</summary>
        SSH = 7,

        /// <summary>Telnet management protocol.</summary>
        TELNET = 8,

        /// <summary>Console management protocol.</summary>
        CONSOLE = 9,

        /// <summary>SNMP management protocol.</summary>
        SNMP = 10
    }

    /// <summary>
    /// HP-Port-Auth-Mode-Dot1x attribute values (Type 13).
    /// </summary>
    public enum HP_PORT_AUTH_MODE_DOT1X
    {
        /// <summary>Port-based authentication mode.</summary>
        PORT_BASED = 1,

        /// <summary>User-based (MAC-based) authentication mode.</summary>
        USER_BASED = 2
    }

    /// <summary>
    /// HP-Port-MA-Port-Mode attribute values (Type 14).
    /// </summary>
    public enum HP_PORT_MA_PORT_MODE
    {
        /// <summary>MAC authentication disabled on port.</summary>
        DISABLED = 0,

        /// <summary>MAC authentication enabled on port.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// HP-Port-Bounce-Host attribute values (Type 23).
    /// </summary>
    public enum HP_PORT_BOUNCE_HOST
    {
        /// <summary>Port bounce disabled.</summary>
        DISABLED = 0,

        /// <summary>Port bounce enabled.</summary>
        ENABLED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Hewlett-Packard / HPE
    /// (IANA PEN 11) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.hp</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// HP's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 11</c>.
    /// </para>
    /// <para>
    /// These attributes are used by HPE / Aruba ProCurve and ArubaOS-Switch (formerly
    /// HP ProCurve) managed switches for RADIUS-based CLI privilege level assignment,
    /// command authorization (permit/deny), management protocol restriction, 802.1X
    /// and MAC authentication port modes and client limits, user role assignment,
    /// captive portal URL redirection, ingress/egress bandwidth provisioning,
    /// NAS filter rules (IPv4/IPv6), egress VLAN assignment (by ID and name),
    /// 802.1p priority regeneration, port bounce, and class of service mapping.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(HpAttributes.PrivilegeLevel(HP_PRIVILEGE_LEVEL.MANAGER));
    /// packet.SetAttribute(HpAttributes.UserRole("network-admin"));
    /// packet.SetAttribute(HpAttributes.ManagementProtocol(HP_MANAGEMENT_PROTOCOL.SSH));
    /// packet.SetAttribute(HpAttributes.BandwidthMaxIngress(100000));
    /// packet.SetAttribute(HpAttributes.BandwidthMaxEgress(50000));
    /// packet.SetAttribute(HpAttributes.EgressVlanName("corp-vlan"));
    /// </code>
    /// </remarks>
    public static class HpAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Hewlett-Packard (HPE).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 11;

        #region Integer Attributes

        /// <summary>
        /// Creates an HP-Privilege-Level attribute (Type 1) with the specified level.
        /// </summary>
        /// <param name="value">The CLI privilege level. See <see cref="HP_PRIVILEGE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(HP_PRIVILEGE_LEVEL value)
        {
            return CreateInteger(HpAttributeType.PRIVILEGE_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates an HP-Command-Exception attribute (Type 3) with the specified flag.
        /// </summary>
        /// <param name="value">The command exception flag. See <see cref="HP_COMMAND_EXCEPTION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CommandException(HP_COMMAND_EXCEPTION value)
        {
            return CreateInteger(HpAttributeType.COMMAND_EXCEPTION, (int)value);
        }

        /// <summary>
        /// Creates an HP-Port-Client-Limit-Dot1x attribute (Type 10) with the specified limit.
        /// </summary>
        /// <param name="value">The 802.1X client limit per port.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortClientLimitDot1x(int value)
        {
            return CreateInteger(HpAttributeType.PORT_CLIENT_LIMIT_DOT1X, value);
        }

        /// <summary>
        /// Creates an HP-Port-Client-Limit-MA attribute (Type 11) with the specified limit.
        /// </summary>
        /// <param name="value">The MAC authentication client limit per port.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortClientLimitMa(int value)
        {
            return CreateInteger(HpAttributeType.PORT_CLIENT_LIMIT_MA, value);
        }

        /// <summary>
        /// Creates an HP-Port-Client-Limit-WA attribute (Type 12) with the specified limit.
        /// </summary>
        /// <param name="value">The web authentication client limit per port.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortClientLimitWa(int value)
        {
            return CreateInteger(HpAttributeType.PORT_CLIENT_LIMIT_WA, value);
        }

        /// <summary>
        /// Creates an HP-Port-Auth-Mode-Dot1x attribute (Type 13) with the specified mode.
        /// </summary>
        /// <param name="value">The 802.1X port authentication mode. See <see cref="HP_PORT_AUTH_MODE_DOT1X"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortAuthModeDot1x(HP_PORT_AUTH_MODE_DOT1X value)
        {
            return CreateInteger(HpAttributeType.PORT_AUTH_MODE_DOT1X, (int)value);
        }

        /// <summary>
        /// Creates an HP-Port-MA-Port-Mode attribute (Type 14) with the specified mode.
        /// </summary>
        /// <param name="value">The MAC authentication port mode. See <see cref="HP_PORT_MA_PORT_MODE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortMaPortMode(HP_PORT_MA_PORT_MODE value)
        {
            return CreateInteger(HpAttributeType.PORT_MA_PORT_MODE, (int)value);
        }

        /// <summary>
        /// Creates an HP-Port-Bounce-Host attribute (Type 23) with the specified flag.
        /// </summary>
        /// <param name="value">The port bounce host flag. See <see cref="HP_PORT_BOUNCE_HOST"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortBounceHost(HP_PORT_BOUNCE_HOST value)
        {
            return CreateInteger(HpAttributeType.PORT_BOUNCE_HOST, (int)value);
        }

        /// <summary>
        /// Creates an HP-Management-Protocol attribute (Type 26) with the specified protocol.
        /// </summary>
        /// <param name="value">The management protocol type. See <see cref="HP_MANAGEMENT_PROTOCOL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ManagementProtocol(HP_MANAGEMENT_PROTOCOL value)
        {
            return CreateInteger(HpAttributeType.MANAGEMENT_PROTOCOL, (int)value);
        }

        /// <summary>
        /// Creates an HP-Bandwidth-Max-Ingress attribute (Type 46) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum ingress bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxIngress(int value)
        {
            return CreateInteger(HpAttributeType.BANDWIDTH_MAX_INGRESS, value);
        }

        /// <summary>
        /// Creates an HP-Bandwidth-Max-Egress attribute (Type 48) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum egress bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxEgress(int value)
        {
            return CreateInteger(HpAttributeType.BANDWIDTH_MAX_EGRESS, value);
        }

        /// <summary>
        /// Creates an HP-Egress-VLANID attribute (Type 64) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The egress VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes EgressVlanId(int value)
        {
            return CreateInteger(HpAttributeType.EGRESS_VLANID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an HP-Command-String attribute (Type 2) with the specified command.
        /// </summary>
        /// <param name="value">The authorized command string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CommandString(string value)
        {
            return CreateString(HpAttributeType.COMMAND_STRING, value);
        }

        /// <summary>
        /// Creates an HP-Captive-Portal-URL attribute (Type 24) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CaptivePortalUrl(string value)
        {
            return CreateString(HpAttributeType.CAPTIVE_PORTAL_URL, value);
        }

        /// <summary>
        /// Creates an HP-User-Role attribute (Type 25) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(HpAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates an HP-Port-Priority-Regeneration-Table attribute (Type 40) with the specified table.
        /// </summary>
        /// <param name="value">The 802.1p priority regeneration table. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PortPriorityRegenerationTable(string value)
        {
            return CreateString(HpAttributeType.PORT_PRIORITY_REGENERATION_TABLE, value);
        }

        /// <summary>
        /// Creates an HP-Nas-Filter-Rule attribute (Type 61) with the specified filter rule.
        /// </summary>
        /// <param name="value">The NAS filter rule. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NasFilterRule(string value)
        {
            return CreateString(HpAttributeType.NAS_FILTER_RULE, value);
        }

        /// <summary>
        /// Creates an HP-NAS-Rules-IPv6 attribute (Type 63) with the specified IPv6 filter rule.
        /// </summary>
        /// <param name="value">The NAS filter rule for IPv6. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NasRulesIpv6(string value)
        {
            return CreateString(HpAttributeType.NAS_RULES_IPV6, value);
        }

        /// <summary>
        /// Creates an HP-Egress-VLAN-Name attribute (Type 65) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The egress VLAN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EgressVlanName(string value)
        {
            return CreateString(HpAttributeType.EGRESS_VLAN_NAME, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(HpAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(HpAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}