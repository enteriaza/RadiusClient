using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Tripp Lite (IANA PEN 850) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.tripplite</c>.
    /// </summary>
    /// <remarks>
    /// Tripp Lite (acquired by Eaton Corporation in 2021) produces UPS (uninterruptible
    /// power supply) systems, PDUs (power distribution units), KVM switches, console
    /// servers, and surge protectors for data center, enterprise, and edge computing
    /// environments. These RADIUS attributes are used by Tripp Lite's network-managed
    /// KVM switches and console servers for administrative authentication.
    /// </remarks>
    public enum TrippliteAttributeType : byte
    {
        /// <summary>Tripplite-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Tripplite-User-Level (Type 2). Integer. User privilege level.</summary>
        USER_LEVEL = 2,

        /// <summary>Tripplite-User-Group (Type 3). String. User group name.</summary>
        USER_GROUP = 3,

        /// <summary>Tripplite-Port-Access-List (Type 4). String. Port access list.</summary>
        PORT_ACCESS_LIST = 4,

        /// <summary>Tripplite-Session-Timeout (Type 5). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 5,

        /// <summary>Tripplite-Idle-Timeout (Type 6). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 6
    }

    /// <summary>
    /// Tripplite-User-Level attribute values (Type 2).
    /// </summary>
    public enum TRIPPLITE_USER_LEVEL
    {
        /// <summary>View-only (monitoring) access.</summary>
        VIEW_ONLY = 0,

        /// <summary>Standard user access.</summary>
        USER = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Tripp Lite
    /// (IANA PEN 850) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.tripplite</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Tripp Lite's vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 850</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Tripp Lite (Eaton) network-managed KVM switches,
    /// console servers, and PDUs for RADIUS-based user privilege level assignment,
    /// user group mapping, port access list configuration, session and idle timeout
    /// management, and general-purpose attribute-value pair configuration during
    /// administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(TrippliteAttributes.UserLevel(TRIPPLITE_USER_LEVEL.ADMIN));
    /// packet.SetAttribute(TrippliteAttributes.UserGroup("datacenter-ops"));
    /// packet.SetAttribute(TrippliteAttributes.PortAccessList("1-8,12,16"));
    /// packet.SetAttribute(TrippliteAttributes.SessionTimeout(3600));
    /// </code>
    /// </remarks>
    public static class TrippliteAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Tripp Lite.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 850;

        #region Integer Attributes

        /// <summary>
        /// Creates a Tripplite-User-Level attribute (Type 2) with the specified level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="TRIPPLITE_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserLevel(TRIPPLITE_USER_LEVEL value)
        {
            return CreateInteger(TrippliteAttributeType.USER_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Tripplite-Session-Timeout attribute (Type 5) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(TrippliteAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Tripplite-Idle-Timeout attribute (Type 6) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(TrippliteAttributeType.IDLE_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Tripplite-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(TrippliteAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Tripplite-User-Group attribute (Type 3) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(TrippliteAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Tripplite-Port-Access-List attribute (Type 4) with the specified port list.
        /// </summary>
        /// <param name="value">The port access list (e.g. "1-8,12,16"). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PortAccessList(string value)
        {
            return CreateString(TrippliteAttributeType.PORT_ACCESS_LIST, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Tripp Lite attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(TrippliteAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Tripp Lite attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(TrippliteAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}