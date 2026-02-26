using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Telebit (IANA PEN 117) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.telebit</c>.
    /// </summary>
    /// <remarks>
    /// Telebit Corporation produced modems, remote access servers, and network
    /// access equipment (NetBlazer series) for enterprise and ISP dial-up
    /// networking environments.
    /// </remarks>
    public enum TelebitAttributeType : byte
    {
        /// <summary>Telebit-Login-Command (Type 1). String. Login command to execute.</summary>
        LOGIN_COMMAND = 1,

        /// <summary>Telebit-Port-Name (Type 2). String. Port name.</summary>
        PORT_NAME = 2,

        /// <summary>Telebit-Activate-Command (Type 3). String. Activation command to execute.</summary>
        ACTIVATE_COMMAND = 3,

        /// <summary>Telebit-Deactivate-Command (Type 4). String. Deactivation command to execute.</summary>
        DEACTIVATE_COMMAND = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Telebit
    /// (IANA PEN 117) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.telebit</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Telebit's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 117</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Telebit Corporation NetBlazer remote access
    /// servers for RADIUS-based login command execution, port naming, and
    /// session activation/deactivation command configuration during dial-up
    /// network access.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(TelebitAttributes.LoginCommand("/usr/local/bin/ppp"));
    /// packet.SetAttribute(TelebitAttributes.PortName("S0"));
    /// packet.SetAttribute(TelebitAttributes.ActivateCommand("start-session"));
    /// </code>
    /// </remarks>
    public static class TelebitAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Telebit Corporation.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 117;

        #region String Attributes

        /// <summary>
        /// Creates a Telebit-Login-Command attribute (Type 1) with the specified command.
        /// </summary>
        /// <param name="value">The login command to execute. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LoginCommand(string value)
        {
            return CreateString(TelebitAttributeType.LOGIN_COMMAND, value);
        }

        /// <summary>
        /// Creates a Telebit-Port-Name attribute (Type 2) with the specified port name.
        /// </summary>
        /// <param name="value">The port name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PortName(string value)
        {
            return CreateString(TelebitAttributeType.PORT_NAME, value);
        }

        /// <summary>
        /// Creates a Telebit-Activate-Command attribute (Type 3) with the specified command.
        /// </summary>
        /// <param name="value">The activation command to execute. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ActivateCommand(string value)
        {
            return CreateString(TelebitAttributeType.ACTIVATE_COMMAND, value);
        }

        /// <summary>
        /// Creates a Telebit-Deactivate-Command attribute (Type 4) with the specified command.
        /// </summary>
        /// <param name="value">The deactivation command to execute. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DeactivateCommand(string value)
        {
            return CreateString(TelebitAttributeType.DEACTIVATE_COMMAND, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Telebit attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(TelebitAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}