using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Purewave Networks (IANA PEN 21015) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.purewave</c>.
    /// </summary>
    /// <remarks>
    /// Purewave Networks (acquired by SAF Tehnika) produced fixed and mobile
    /// broadband wireless access platforms including WiMAX and LTE small cell
    /// base stations for urban, suburban, and rural deployments.
    /// </remarks>
    public enum PurewaveAttributeType : byte
    {
        /// <summary>Purewave-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Purewave-Client-Profile (Type 2). String. Client profile name.</summary>
        CLIENT_PROFILE = 2,

        /// <summary>Purewave-Max-Downlink-Rate (Type 3). Integer. Maximum downlink rate in bps.</summary>
        MAX_DOWNLINK_RATE = 3,

        /// <summary>Purewave-Max-Uplink-Rate (Type 4). Integer. Maximum uplink rate in bps.</summary>
        MAX_UPLINK_RATE = 4,

        /// <summary>Purewave-Service-Class (Type 5). String. Service class name.</summary>
        SERVICE_CLASS = 5,

        /// <summary>Purewave-VLAN-Id (Type 6). Integer. VLAN identifier.</summary>
        VLAN_ID = 6,

        /// <summary>Purewave-QoS-Profile (Type 7). String. QoS profile name.</summary>
        QOS_PROFILE = 7,

        /// <summary>Purewave-Session-Timeout (Type 8). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 8,

        /// <summary>Purewave-Idle-Timeout (Type 9). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 9,

        /// <summary>Purewave-Admin-Access (Type 10). Integer. Administrative access level.</summary>
        ADMIN_ACCESS = 10
    }

    /// <summary>
    /// Purewave-Admin-Access attribute values (Type 10).
    /// </summary>
    public enum PUREWAVE_ADMIN_ACCESS
    {
        /// <summary>No administrative access.</summary>
        NONE = 0,

        /// <summary>Read-only access.</summary>
        READ_ONLY = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Purewave Networks
    /// (IANA PEN 21015) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.purewave</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Purewave's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 21015</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Purewave Networks (now SAF Tehnika) broadband
    /// wireless base stations for RADIUS-based client profile assignment, maximum
    /// uplink/downlink rate provisioning, service class and QoS profile selection,
    /// VLAN assignment, session and idle timeout management, administrative access
    /// level control, and general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(PurewaveAttributes.ClientProfile("residential-premium"));
    /// packet.SetAttribute(PurewaveAttributes.MaxDownlinkRate(50000000));
    /// packet.SetAttribute(PurewaveAttributes.MaxUplinkRate(10000000));
    /// packet.SetAttribute(PurewaveAttributes.VlanId(200));
    /// packet.SetAttribute(PurewaveAttributes.ServiceClass("gold"));
    /// packet.SetAttribute(PurewaveAttributes.SessionTimeout(86400));
    /// </code>
    /// </remarks>
    public static class PurewaveAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Purewave Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 21015;

        #region Integer Attributes

        /// <summary>
        /// Creates a Purewave-Max-Downlink-Rate attribute (Type 3) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downlink rate in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxDownlinkRate(int value)
        {
            return CreateInteger(PurewaveAttributeType.MAX_DOWNLINK_RATE, value);
        }

        /// <summary>
        /// Creates a Purewave-Max-Uplink-Rate attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum uplink rate in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxUplinkRate(int value)
        {
            return CreateInteger(PurewaveAttributeType.MAX_UPLINK_RATE, value);
        }

        /// <summary>
        /// Creates a Purewave-VLAN-Id attribute (Type 6) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(PurewaveAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Purewave-Session-Timeout attribute (Type 8) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(PurewaveAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Purewave-Idle-Timeout attribute (Type 9) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(PurewaveAttributeType.IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Purewave-Admin-Access attribute (Type 10) with the specified level.
        /// </summary>
        /// <param name="value">The administrative access level. See <see cref="PUREWAVE_ADMIN_ACCESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminAccess(PUREWAVE_ADMIN_ACCESS value)
        {
            return CreateInteger(PurewaveAttributeType.ADMIN_ACCESS, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Purewave-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(PurewaveAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Purewave-Client-Profile attribute (Type 2) with the specified profile name.
        /// </summary>
        /// <param name="value">The client profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientProfile(string value)
        {
            return CreateString(PurewaveAttributeType.CLIENT_PROFILE, value);
        }

        /// <summary>
        /// Creates a Purewave-Service-Class attribute (Type 5) with the specified class name.
        /// </summary>
        /// <param name="value">The service class name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceClass(string value)
        {
            return CreateString(PurewaveAttributeType.SERVICE_CLASS, value);
        }

        /// <summary>
        /// Creates a Purewave-QoS-Profile attribute (Type 7) with the specified profile name.
        /// </summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value)
        {
            return CreateString(PurewaveAttributeType.QOS_PROFILE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Purewave attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(PurewaveAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Purewave attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(PurewaveAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}