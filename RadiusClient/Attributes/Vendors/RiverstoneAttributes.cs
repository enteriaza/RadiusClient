using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Riverstone Networks (IANA PEN 5567) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.riverstone</c>.
    /// </summary>
    /// <remarks>
    /// Riverstone Networks (acquired by Lucent Technologies / Alcatel-Lucent)
    /// produced carrier-class and enterprise Ethernet switches and routers
    /// (RS series) for metro Ethernet, service provider, and enterprise
    /// aggregation deployments.
    /// </remarks>
    public enum RiverstoneAttributeType : byte
    {
        /// <summary>Riverstone-Command (Type 1). String. CLI command string.</summary>
        COMMAND = 1,

        /// <summary>Riverstone-System-Event (Type 2). String. System event string.</summary>
        SYSTEM_EVENT = 2,

        /// <summary>Riverstone-SNMP-Config-Change (Type 3). String. SNMP configuration change.</summary>
        SNMP_CONFIG_CHANGE = 3,

        /// <summary>Riverstone-User-Level (Type 4). Integer. User privilege level.</summary>
        USER_LEVEL = 4
    }

    /// <summary>
    /// Riverstone-User-Level attribute values (Type 4).
    /// </summary>
    public enum RIVERSTONE_USER_LEVEL
    {
        /// <summary>Read-only (monitoring) access.</summary>
        READ_ONLY = 0,

        /// <summary>Read-write (operator) access.</summary>
        READ_WRITE = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Riverstone Networks
    /// (IANA PEN 5567) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.riverstone</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Riverstone's vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 5567</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Riverstone Networks (now Alcatel-Lucent) RS
    /// series carrier-class Ethernet switches and routers for RADIUS-based CLI
    /// command authorization, system event logging, SNMP configuration change
    /// tracking, and user privilege level assignment during administrative
    /// authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(RiverstoneAttributes.UserLevel(RIVERSTONE_USER_LEVEL.ADMIN));
    /// packet.SetAttribute(RiverstoneAttributes.Command("show running-config"));
    /// </code>
    /// </remarks>
    public static class RiverstoneAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Riverstone Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 5567;

        #region Integer Attributes

        /// <summary>
        /// Creates a Riverstone-User-Level attribute (Type 4) with the specified level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="RIVERSTONE_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserLevel(RIVERSTONE_USER_LEVEL value)
        {
            return CreateInteger(RiverstoneAttributeType.USER_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Riverstone-Command attribute (Type 1) with the specified CLI command.
        /// </summary>
        /// <param name="value">The CLI command string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Command(string value)
        {
            return CreateString(RiverstoneAttributeType.COMMAND, value);
        }

        /// <summary>
        /// Creates a Riverstone-System-Event attribute (Type 2) with the specified event string.
        /// </summary>
        /// <param name="value">The system event string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SystemEvent(string value)
        {
            return CreateString(RiverstoneAttributeType.SYSTEM_EVENT, value);
        }

        /// <summary>
        /// Creates a Riverstone-SNMP-Config-Change attribute (Type 3) with the specified change string.
        /// </summary>
        /// <param name="value">The SNMP configuration change string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SnmpConfigChange(string value)
        {
            return CreateString(RiverstoneAttributeType.SNMP_CONFIG_CHANGE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Riverstone attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(RiverstoneAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Riverstone attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(RiverstoneAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}