using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Lantronix (IANA PEN 244) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.lantronix</c>.
    /// </summary>
    /// <remarks>
    /// Lantronix is a manufacturer of networking and connectivity solutions
    /// including serial-to-Ethernet device servers, console servers, and
    /// IoT gateways (SLC, Spider, xPrintServer, EDS, UDS).
    /// </remarks>
    public enum LantronixAttributeType : byte
    {
        /// <summary>Lantronix-Access (Type 1). Integer. Access level.</summary>
        ACCESS = 1,

        /// <summary>Lantronix-Port (Type 2). Integer. Port number.</summary>
        PORT = 2,

        /// <summary>Lantronix-Prompt (Type 3). String. Login prompt string.</summary>
        PROMPT = 3,

        /// <summary>Lantronix-Auth-Server-Passwd (Type 4). String. Auth server password.</summary>
        AUTH_SERVER_PASSWD = 4,

        /// <summary>Lantronix-Dscp (Type 5). Integer. DSCP value.</summary>
        DSCP = 5,

        /// <summary>Lantronix-User-Group (Type 6). String. User group name.</summary>
        USER_GROUP = 6,

        /// <summary>Lantronix-Port-List (Type 7). String. Port list string.</summary>
        PORT_LIST = 7,

        /// <summary>Lantronix-Power-Group (Type 8). String. Power group name.</summary>
        POWER_GROUP = 8,

        /// <summary>Lantronix-CLI-Filter (Type 9). String. CLI command filter.</summary>
        CLI_FILTER = 9,

        /// <summary>Lantronix-Custom-Menu (Type 10). String. Custom menu name.</summary>
        CUSTOM_MENU = 10
    }

    /// <summary>
    /// Lantronix-Access attribute values (Type 1).
    /// </summary>
    public enum LANTRONIX_ACCESS
    {
        /// <summary>User-level access.</summary>
        USER = 0,

        /// <summary>System administrator access.</summary>
        SYSADMIN = 1,

        /// <summary>Port administrator access.</summary>
        PORTADMIN = 2,

        /// <summary>Power administrator access.</summary>
        POWERADMIN = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Lantronix
    /// (IANA PEN 244) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.lantronix</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Lantronix's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 244</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Lantronix console servers, device servers,
    /// and IoT gateways (SLC, Spider, xPrintServer) for RADIUS-based administrative
    /// access level assignment (user, sysadmin, portadmin, poweradmin), serial port
    /// and port list selection, login prompt customization, DSCP marking, user
    /// and power group assignment, CLI command filtering, and custom menu
    /// configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(LantronixAttributes.Access(LANTRONIX_ACCESS.SYSADMIN));
    /// packet.SetAttribute(LantronixAttributes.UserGroup("noc-engineers"));
    /// packet.SetAttribute(LantronixAttributes.PortList("1-8,12"));
    /// packet.SetAttribute(LantronixAttributes.CliFilter("show|ping|traceroute"));
    /// </code>
    /// </remarks>
    public static class LantronixAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Lantronix.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 244;

        #region Integer Attributes

        /// <summary>
        /// Creates a Lantronix-Access attribute (Type 1) with the specified access level.
        /// </summary>
        /// <param name="value">The access level. See <see cref="LANTRONIX_ACCESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Access(LANTRONIX_ACCESS value)
        {
            return CreateInteger(LantronixAttributeType.ACCESS, (int)value);
        }

        /// <summary>
        /// Creates a Lantronix-Port attribute (Type 2) with the specified port number.
        /// </summary>
        /// <param name="value">The port number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Port(int value)
        {
            return CreateInteger(LantronixAttributeType.PORT, value);
        }

        /// <summary>
        /// Creates a Lantronix-Dscp attribute (Type 5) with the specified DSCP value.
        /// </summary>
        /// <param name="value">The DSCP value (0–63).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Dscp(int value)
        {
            return CreateInteger(LantronixAttributeType.DSCP, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Lantronix-Prompt attribute (Type 3) with the specified prompt string.
        /// </summary>
        /// <param name="value">The login prompt string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Prompt(string value)
        {
            return CreateString(LantronixAttributeType.PROMPT, value);
        }

        /// <summary>
        /// Creates a Lantronix-Auth-Server-Passwd attribute (Type 4) with the specified password.
        /// </summary>
        /// <param name="value">The auth server password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthServerPasswd(string value)
        {
            return CreateString(LantronixAttributeType.AUTH_SERVER_PASSWD, value);
        }

        /// <summary>
        /// Creates a Lantronix-User-Group attribute (Type 6) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(LantronixAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Lantronix-Port-List attribute (Type 7) with the specified port list.
        /// </summary>
        /// <param name="value">The port list string (e.g. "1-8,12"). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PortList(string value)
        {
            return CreateString(LantronixAttributeType.PORT_LIST, value);
        }

        /// <summary>
        /// Creates a Lantronix-Power-Group attribute (Type 8) with the specified power group name.
        /// </summary>
        /// <param name="value">The power group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PowerGroup(string value)
        {
            return CreateString(LantronixAttributeType.POWER_GROUP, value);
        }

        /// <summary>
        /// Creates a Lantronix-CLI-Filter attribute (Type 9) with the specified filter.
        /// </summary>
        /// <param name="value">The CLI command filter. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CliFilter(string value)
        {
            return CreateString(LantronixAttributeType.CLI_FILTER, value);
        }

        /// <summary>
        /// Creates a Lantronix-Custom-Menu attribute (Type 10) with the specified menu name.
        /// </summary>
        /// <param name="value">The custom menu name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CustomMenu(string value)
        {
            return CreateString(LantronixAttributeType.CUSTOM_MENU, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Lantronix attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(LantronixAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Lantronix attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(LantronixAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}