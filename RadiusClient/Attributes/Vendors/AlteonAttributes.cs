using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Alteon / Nortel (IANA PEN 1872) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.alteon</c>.
    /// </summary>
    public enum AlteonAttributeType : byte
    {
        /// <summary>Alteon-Service-Type (Type 1). Integer. Service type for the user.</summary>
        SERVICE_TYPE = 1,

        /// <summary>Alteon-AS-Ip-Address (Type 2). IP address. Application switch IP address.</summary>
        AS_IP_ADDRESS = 2,

        /// <summary>Alteon-AS-VPort (Type 3). Integer. Application switch virtual server port.</summary>
        AS_VPORT = 3,

        /// <summary>Alteon-Group (Type 4). Integer. Group identifier.</summary>
        GROUP = 4,

        /// <summary>Alteon-Service (Type 5). Integer. Real service identifier.</summary>
        SERVICE = 5,

        /// <summary>Alteon-User-Level (Type 6). Integer. User privilege level.</summary>
        USER_LEVEL = 6
    }

    /// <summary>
    /// Alteon-Service-Type attribute values (Type 1).
    /// </summary>
    public enum ALTEON_SERVICE_TYPE
    {
        /// <summary>Layer 4 (L4) service type.</summary>
        L4_SERVICE = 1,

        /// <summary>Global SLB service type.</summary>
        GLOBAL_SLB = 2,

        /// <summary>Remote login service type.</summary>
        REMOTE = 3
    }

    /// <summary>
    /// Alteon-User-Level attribute values (Type 6).
    /// </summary>
    public enum ALTEON_USER_LEVEL
    {
        /// <summary>No access (not authorised).</summary>
        NO_ACCESS = 0,

        /// <summary>User level (basic monitoring).</summary>
        USER = 1,

        /// <summary>SLB operator level.</summary>
        SLB_OPER = 2,

        /// <summary>Layer 4 operator level.</summary>
        L4_OPER = 3,

        /// <summary>SLB administrator level.</summary>
        SLB_ADMIN = 4,

        /// <summary>Layer 4 administrator level.</summary>
        L4_ADMIN = 5,

        /// <summary>Full administrator level (all privileges).</summary>
        ADMIN = 6
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Alteon / Nortel
    /// (IANA PEN 1872) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.alteon</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Alteon's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 1872</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Alteon (Nortel / Radware) Application Switches
    /// for service type selection, virtual server port assignment, group and real
    /// service mapping, and user privilege level authorisation.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AlteonAttributes.ServiceType(ALTEON_SERVICE_TYPE.L4_SERVICE));
    /// packet.SetAttribute(AlteonAttributes.UserLevel(ALTEON_USER_LEVEL.ADMIN));
    /// packet.SetAttribute(AlteonAttributes.AsIpAddress(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(AlteonAttributes.AsVPort(80));
    /// </code>
    /// </remarks>
    public static class AlteonAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Alteon (Nortel / Radware).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 1872;

        #region Integer Attributes

        /// <summary>
        /// Creates an Alteon-Service-Type attribute (Type 1) with the specified service type.
        /// </summary>
        /// <param name="value">The service type. See <see cref="ALTEON_SERVICE_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServiceType(ALTEON_SERVICE_TYPE value)
        {
            return CreateInteger(AlteonAttributeType.SERVICE_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Alteon-AS-VPort attribute (Type 3) with the specified virtual server port.
        /// </summary>
        /// <param name="value">The application switch virtual server port.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AsVPort(int value)
        {
            return CreateInteger(AlteonAttributeType.AS_VPORT, value);
        }

        /// <summary>
        /// Creates an Alteon-Group attribute (Type 4) with the specified group identifier.
        /// </summary>
        /// <param name="value">The group identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Group(int value)
        {
            return CreateInteger(AlteonAttributeType.GROUP, value);
        }

        /// <summary>
        /// Creates an Alteon-Service attribute (Type 5) with the specified real service identifier.
        /// </summary>
        /// <param name="value">The real service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Service(int value)
        {
            return CreateInteger(AlteonAttributeType.SERVICE, value);
        }

        /// <summary>
        /// Creates an Alteon-User-Level attribute (Type 6) with the specified privilege level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="ALTEON_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserLevel(ALTEON_USER_LEVEL value)
        {
            return CreateInteger(AlteonAttributeType.USER_LEVEL, (int)value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Alteon-AS-Ip-Address attribute (Type 2) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The application switch IP address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes AsIpAddress(IPAddress value)
        {
            return CreateIpv4(AlteonAttributeType.AS_IP_ADDRESS, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Alteon attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AlteonAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified Alteon attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(AlteonAttributeType type, IPAddress value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException(
                    $"Expected an IPv4 (InterNetwork) address, got '{value.AddressFamily}'.",
                    nameof(value));

            byte[] addrBytes = new byte[4];
            value.TryWriteBytes(addrBytes, out _);

            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, addrBytes);
        }

        #endregion
    }
}