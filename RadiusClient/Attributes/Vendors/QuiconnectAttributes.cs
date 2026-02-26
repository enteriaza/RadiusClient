using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a QuiConnect (IANA PEN 23365) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.quiconnect</c>.
    /// </summary>
    /// <remarks>
    /// QuiConnect produces subscriber management and broadband access platforms
    /// for ISPs and service providers, providing captive portal, bandwidth
    /// management, and subscriber provisioning capabilities.
    /// </remarks>
    public enum QuiconnectAttributeType : byte
    {
        /// <summary>QuiConnect-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>QuiConnect-User-Class (Type 2). String. User class name.</summary>
        USER_CLASS = 2,

        /// <summary>QuiConnect-Bandwidth-Max-Up (Type 3). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 3,

        /// <summary>QuiConnect-Bandwidth-Max-Down (Type 4). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 4,

        /// <summary>QuiConnect-Session-Timeout (Type 5). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 5,

        /// <summary>QuiConnect-Idle-Timeout (Type 6). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 6,

        /// <summary>QuiConnect-Redirect-URL (Type 7). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 7,

        /// <summary>QuiConnect-VLAN-Id (Type 8). Integer. VLAN identifier.</summary>
        VLAN_ID = 8,

        /// <summary>QuiConnect-Service-Profile (Type 9). String. Service profile name.</summary>
        SERVICE_PROFILE = 9,

        /// <summary>QuiConnect-Max-Data (Type 10). Integer. Maximum data usage in bytes.</summary>
        MAX_DATA = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing QuiConnect
    /// (IANA PEN 23365) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.quiconnect</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// QuiConnect's vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 23365</c>.
    /// </para>
    /// <para>
    /// These attributes are used by QuiConnect subscriber management and broadband
    /// access platforms for RADIUS-based user class assignment, upstream/downstream
    /// bandwidth provisioning, session and idle timeout management, captive portal
    /// URL redirection, VLAN assignment, service profile selection, data usage
    /// quotas, and general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(QuiconnectAttributes.UserClass("premium"));
    /// packet.SetAttribute(QuiconnectAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(QuiconnectAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(QuiconnectAttributes.ServiceProfile("residential-50m"));
    /// packet.SetAttribute(QuiconnectAttributes.VlanId(100));
    /// packet.SetAttribute(QuiconnectAttributes.SessionTimeout(86400));
    /// </code>
    /// </remarks>
    public static class QuiconnectAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for QuiConnect.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 23365;

        #region Integer Attributes

        /// <summary>
        /// Creates a QuiConnect-Bandwidth-Max-Up attribute (Type 3) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(QuiconnectAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a QuiConnect-Bandwidth-Max-Down attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(QuiconnectAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a QuiConnect-Session-Timeout attribute (Type 5) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(QuiconnectAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a QuiConnect-Idle-Timeout attribute (Type 6) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(QuiconnectAttributeType.IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a QuiConnect-VLAN-Id attribute (Type 8) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(QuiconnectAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a QuiConnect-Max-Data attribute (Type 10) with the specified data limit.
        /// </summary>
        /// <param name="value">The maximum data usage in bytes.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxData(int value)
        {
            return CreateInteger(QuiconnectAttributeType.MAX_DATA, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a QuiConnect-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(QuiconnectAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a QuiConnect-User-Class attribute (Type 2) with the specified class name.
        /// </summary>
        /// <param name="value">The user class name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserClass(string value)
        {
            return CreateString(QuiconnectAttributeType.USER_CLASS, value);
        }

        /// <summary>
        /// Creates a QuiConnect-Redirect-URL attribute (Type 7) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(QuiconnectAttributeType.REDIRECT_URL, value);
        }

        /// <summary>
        /// Creates a QuiConnect-Service-Profile attribute (Type 9) with the specified profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(QuiconnectAttributeType.SERVICE_PROFILE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified QuiConnect attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(QuiconnectAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified QuiConnect attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(QuiconnectAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}