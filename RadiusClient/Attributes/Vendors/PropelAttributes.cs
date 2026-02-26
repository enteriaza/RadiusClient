using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Propel (IANA PEN 14895) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.propel</c>.
    /// </summary>
    /// <remarks>
    /// Propel (Propel Networks) produced broadband access and subscriber management
    /// platforms for ISPs and service providers.
    /// </remarks>
    public enum PropelAttributeType : byte
    {
        /// <summary>Propel-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Propel-Accel-Service-Id (Type 2). Integer. Accelerator service identifier.</summary>
        ACCEL_SERVICE_ID = 2,

        /// <summary>Propel-Dialed-Number (Type 3). String. Dialed number.</summary>
        DIALED_NUMBER = 3,

        /// <summary>Propel-Client-IP-Address (Type 4). String. Client IP address.</summary>
        CLIENT_IP_ADDRESS = 4,

        /// <summary>Propel-Client-NAS-IP-Address (Type 5). String. Client NAS IP address.</summary>
        CLIENT_NAS_IP_ADDRESS = 5,

        /// <summary>Propel-Client-Source-ID (Type 6). Integer. Client source identifier.</summary>
        CLIENT_SOURCE_ID = 6,

        /// <summary>Propel-Admin-Access (Type 7). Integer. Administrative access level.</summary>
        ADMIN_ACCESS = 7,

        /// <summary>Propel-Subscriber-Profile-Name (Type 8). String. Subscriber profile name.</summary>
        SUBSCRIBER_PROFILE_NAME = 8,

        /// <summary>Propel-Max-Sessions (Type 9). Integer. Maximum simultaneous sessions.</summary>
        MAX_SESSIONS = 9,

        /// <summary>Propel-Bandwidth-Max-Up (Type 10). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 10,

        /// <summary>Propel-Bandwidth-Max-Down (Type 11). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 11
    }

    /// <summary>
    /// Propel-Admin-Access attribute values (Type 7).
    /// </summary>
    public enum PROPEL_ADMIN_ACCESS
    {
        /// <summary>No administrative access.</summary>
        NONE = 0,

        /// <summary>Read-only access.</summary>
        READ_ONLY = 1,

        /// <summary>Read-write access.</summary>
        READ_WRITE = 2,

        /// <summary>Full administrative access.</summary>
        ADMIN = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Propel
    /// (IANA PEN 14895) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.propel</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Propel's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14895</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Propel Networks broadband access and subscriber
    /// management platforms for RADIUS-based accelerator service identification,
    /// dialed number and client IP/NAS IP tracking, client source identification,
    /// administrative access level assignment, subscriber profile selection,
    /// maximum session limiting, upstream/downstream bandwidth provisioning, and
    /// general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(PropelAttributes.SubscriberProfileName("residential-50m"));
    /// packet.SetAttribute(PropelAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(PropelAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(PropelAttributes.MaxSessions(3));
    /// packet.SetAttribute(PropelAttributes.AdminAccess(PROPEL_ADMIN_ACCESS.ADMIN));
    /// </code>
    /// </remarks>
    public static class PropelAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Propel.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14895;

        #region Integer Attributes

        /// <summary>
        /// Creates a Propel-Accel-Service-Id attribute (Type 2) with the specified service ID.
        /// </summary>
        /// <param name="value">The accelerator service identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccelServiceId(int value)
        {
            return CreateInteger(PropelAttributeType.ACCEL_SERVICE_ID, value);
        }

        /// <summary>
        /// Creates a Propel-Client-Source-ID attribute (Type 6) with the specified source ID.
        /// </summary>
        /// <param name="value">The client source identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ClientSourceId(int value)
        {
            return CreateInteger(PropelAttributeType.CLIENT_SOURCE_ID, value);
        }

        /// <summary>
        /// Creates a Propel-Admin-Access attribute (Type 7) with the specified level.
        /// </summary>
        /// <param name="value">The administrative access level. See <see cref="PROPEL_ADMIN_ACCESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminAccess(PROPEL_ADMIN_ACCESS value)
        {
            return CreateInteger(PropelAttributeType.ADMIN_ACCESS, (int)value);
        }

        /// <summary>
        /// Creates a Propel-Max-Sessions attribute (Type 9) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum number of simultaneous sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSessions(int value)
        {
            return CreateInteger(PropelAttributeType.MAX_SESSIONS, value);
        }

        /// <summary>
        /// Creates a Propel-Bandwidth-Max-Up attribute (Type 10) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(PropelAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Propel-Bandwidth-Max-Down attribute (Type 11) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(PropelAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Propel-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(PropelAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Propel-Dialed-Number attribute (Type 3) with the specified number.
        /// </summary>
        /// <param name="value">The dialed number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DialedNumber(string value)
        {
            return CreateString(PropelAttributeType.DIALED_NUMBER, value);
        }

        /// <summary>
        /// Creates a Propel-Client-IP-Address attribute (Type 4) with the specified address.
        /// </summary>
        /// <param name="value">The client IP address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientIpAddress(string value)
        {
            return CreateString(PropelAttributeType.CLIENT_IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates a Propel-Client-NAS-IP-Address attribute (Type 5) with the specified address.
        /// </summary>
        /// <param name="value">The client NAS IP address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientNasIpAddress(string value)
        {
            return CreateString(PropelAttributeType.CLIENT_NAS_IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates a Propel-Subscriber-Profile-Name attribute (Type 8) with the specified profile.
        /// </summary>
        /// <param name="value">The subscriber profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberProfileName(string value)
        {
            return CreateString(PropelAttributeType.SUBSCRIBER_PROFILE_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Propel attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(PropelAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Propel attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(PropelAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}