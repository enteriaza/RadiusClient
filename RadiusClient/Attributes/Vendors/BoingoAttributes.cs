using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Boingo Wireless (IANA PEN 14559) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.boingo</c>.
    /// </summary>
    public enum BoingoAttributeType : byte
    {
        /// <summary>Boingo-Venue-Name (Type 1). String. Venue name.</summary>
        VENUE_NAME = 1,

        /// <summary>Boingo-Venue-Type (Type 2). Integer. Venue type identifier.</summary>
        VENUE_TYPE = 2,

        /// <summary>Boingo-Device-Id (Type 3). String. Device identifier.</summary>
        DEVICE_ID = 3,

        /// <summary>Boingo-Max-Devices (Type 4). Integer. Maximum devices per subscriber.</summary>
        MAX_DEVICES = 4,

        /// <summary>Boingo-Device-Count (Type 5). Integer. Current device count.</summary>
        DEVICE_COUNT = 5,

        /// <summary>Boingo-Boingo-UserId (Type 6). String. Boingo user identifier.</summary>
        BOINGO_USERID = 6,

        /// <summary>Boingo-Qos-Profile-Name (Type 7). String. QoS profile name.</summary>
        QOS_PROFILE_NAME = 7,

        /// <summary>Boingo-Session-Timeout (Type 8). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 8,

        /// <summary>Boingo-Idle-Timeout (Type 9). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 9,

        /// <summary>Boingo-Acct-Policy-Name (Type 10). String. Accounting policy name.</summary>
        ACCT_POLICY_NAME = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Boingo Wireless
    /// (IANA PEN 14559) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.boingo</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Boingo's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14559</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Boingo Wireless hotspot management platforms
    /// for subscriber session management, venue identification, device tracking,
    /// QoS profile assignment, and accounting policy enforcement.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(BoingoAttributes.VenueName("LAX Terminal 4"));
    /// packet.SetAttribute(BoingoAttributes.QosProfileName("premium"));
    /// packet.SetAttribute(BoingoAttributes.MaxDevices(3));
    /// packet.SetAttribute(BoingoAttributes.SessionTimeout(3600));
    /// </code>
    /// </remarks>
    public static class BoingoAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Boingo Wireless.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14559;

        #region Integer Attributes

        /// <summary>
        /// Creates a Boingo-Venue-Type attribute (Type 2) with the specified venue type.
        /// </summary>
        /// <param name="value">The venue type identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VenueType(int value)
        {
            return CreateInteger(BoingoAttributeType.VENUE_TYPE, value);
        }

        /// <summary>
        /// Creates a Boingo-Max-Devices attribute (Type 4) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum devices per subscriber.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxDevices(int value)
        {
            return CreateInteger(BoingoAttributeType.MAX_DEVICES, value);
        }

        /// <summary>
        /// Creates a Boingo-Device-Count attribute (Type 5) with the specified count.
        /// </summary>
        /// <param name="value">The current device count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DeviceCount(int value)
        {
            return CreateInteger(BoingoAttributeType.DEVICE_COUNT, value);
        }

        /// <summary>
        /// Creates a Boingo-Session-Timeout attribute (Type 8) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(BoingoAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Boingo-Idle-Timeout attribute (Type 9) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(BoingoAttributeType.IDLE_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Boingo-Venue-Name attribute (Type 1) with the specified venue name.
        /// </summary>
        /// <param name="value">The venue name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VenueName(string value)
        {
            return CreateString(BoingoAttributeType.VENUE_NAME, value);
        }

        /// <summary>
        /// Creates a Boingo-Device-Id attribute (Type 3) with the specified device identifier.
        /// </summary>
        /// <param name="value">The device identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DeviceId(string value)
        {
            return CreateString(BoingoAttributeType.DEVICE_ID, value);
        }

        /// <summary>
        /// Creates a Boingo-Boingo-UserId attribute (Type 6) with the specified user identifier.
        /// </summary>
        /// <param name="value">The Boingo user identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BoingoUserId(string value)
        {
            return CreateString(BoingoAttributeType.BOINGO_USERID, value);
        }

        /// <summary>
        /// Creates a Boingo-Qos-Profile-Name attribute (Type 7) with the specified QoS profile.
        /// </summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfileName(string value)
        {
            return CreateString(BoingoAttributeType.QOS_PROFILE_NAME, value);
        }

        /// <summary>
        /// Creates a Boingo-Acct-Policy-Name attribute (Type 10) with the specified policy name.
        /// </summary>
        /// <param name="value">The accounting policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctPolicyName(string value)
        {
            return CreateString(BoingoAttributeType.ACCT_POLICY_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Boingo attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(BoingoAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Boingo attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(BoingoAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}