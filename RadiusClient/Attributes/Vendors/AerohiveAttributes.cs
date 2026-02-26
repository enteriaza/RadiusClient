using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Aerohive Networks (IANA PEN 26928) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.aerohive</c>.
    /// </summary>
    public enum AerohiveAttributeType : byte
    {
        /// <summary>Aerohive-User-Profile (Type 1). String. User profile name to apply.</summary>
        USER_PROFILE = 1,

        /// <summary>Aerohive-User-Profile-Attribute (Type 2). Integer. User profile attribute identifier.</summary>
        USER_PROFILE_ATTRIBUTE = 2,

        /// <summary>Aerohive-VLAN (Type 3). Integer. VLAN identifier to assign to the user.</summary>
        VLAN = 3,

        /// <summary>Aerohive-Attribute (Type 4). String. Generic attribute string.</summary>
        ATTRIBUTE = 4,

        /// <summary>Aerohive-Device-Location (Type 5). String. Device location string.</summary>
        DEVICE_LOCATION = 5,

        /// <summary>Aerohive-Port (Type 6). Integer. Port number assignment.</summary>
        PORT = 6,

        /// <summary>Aerohive-Libsip-Proxy-Port (Type 7). Integer. LibSIP proxy port number.</summary>
        LIBSIP_PROXY_PORT = 7,

        /// <summary>Aerohive-Registration-Port (Type 8). Integer. Registration port number.</summary>
        REGISTRATION_PORT = 8,

        /// <summary>Aerohive-Time-Zone (Type 9). String. Timezone designation string.</summary>
        TIME_ZONE = 9,

        /// <summary>Aerohive-Data-Label (Type 10). String. Data classification label.</summary>
        DATA_LABEL = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Aerohive Networks
    /// (IANA PEN 26928) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.aerohive</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Aerohive Networks' (now Extreme Networks) vendor-specific attributes follow the
    /// standard VSA layout defined in RFC 2865 §5.26. All attributes produced by this
    /// class are wrapped in a <see cref="VendorSpecificAttributes"/> instance with
    /// <c>VendorId = 26928</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Aerohive / Extreme Networks wireless access points
    /// and controllers for user profile assignment, VLAN mapping, device location
    /// identification, and data classification labelling.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AerohiveAttributes.UserProfile("employee"));
    /// packet.SetAttribute(AerohiveAttributes.Vlan(100));
    /// packet.SetAttribute(AerohiveAttributes.DataLabel("confidential"));
    /// </code>
    /// </remarks>
    public static class AerohiveAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Aerohive Networks (Extreme Networks).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 26928;

        #region Integer Attributes

        /// <summary>
        /// Creates an Aerohive-User-Profile-Attribute attribute (Type 2) with the specified identifier.
        /// </summary>
        /// <param name="value">The user profile attribute identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserProfileAttribute(int value)
        {
            return CreateInteger(AerohiveAttributeType.USER_PROFILE_ATTRIBUTE, value);
        }

        /// <summary>
        /// Creates an Aerohive-VLAN attribute (Type 3) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier to assign to the user.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Vlan(int value)
        {
            return CreateInteger(AerohiveAttributeType.VLAN, value);
        }

        /// <summary>
        /// Creates an Aerohive-Port attribute (Type 6) with the specified port number.
        /// </summary>
        /// <param name="value">The port number assignment.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Port(int value)
        {
            return CreateInteger(AerohiveAttributeType.PORT, value);
        }

        /// <summary>
        /// Creates an Aerohive-Libsip-Proxy-Port attribute (Type 7) with the specified port number.
        /// </summary>
        /// <param name="value">The LibSIP proxy port number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LibsipProxyPort(int value)
        {
            return CreateInteger(AerohiveAttributeType.LIBSIP_PROXY_PORT, value);
        }

        /// <summary>
        /// Creates an Aerohive-Registration-Port attribute (Type 8) with the specified port number.
        /// </summary>
        /// <param name="value">The registration port number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RegistrationPort(int value)
        {
            return CreateInteger(AerohiveAttributeType.REGISTRATION_PORT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Aerohive-User-Profile attribute (Type 1) with the specified profile name.
        /// </summary>
        /// <param name="value">The user profile name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserProfile(string value)
        {
            return CreateString(AerohiveAttributeType.USER_PROFILE, value);
        }

        /// <summary>
        /// Creates an Aerohive-Attribute attribute (Type 4) with the specified attribute string.
        /// </summary>
        /// <param name="value">The generic attribute string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Attribute(string value)
        {
            return CreateString(AerohiveAttributeType.ATTRIBUTE, value);
        }

        /// <summary>
        /// Creates an Aerohive-Device-Location attribute (Type 5) with the specified location.
        /// </summary>
        /// <param name="value">The device location string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DeviceLocation(string value)
        {
            return CreateString(AerohiveAttributeType.DEVICE_LOCATION, value);
        }

        /// <summary>
        /// Creates an Aerohive-Time-Zone attribute (Type 9) with the specified timezone.
        /// </summary>
        /// <param name="value">The timezone designation string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TimeZone(string value)
        {
            return CreateString(AerohiveAttributeType.TIME_ZONE, value);
        }

        /// <summary>
        /// Creates an Aerohive-Data-Label attribute (Type 10) with the specified label.
        /// </summary>
        /// <param name="value">The data classification label. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DataLabel(string value)
        {
            return CreateString(AerohiveAttributeType.DATA_LABEL, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Aerohive attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AerohiveAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Aerohive attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AerohiveAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}