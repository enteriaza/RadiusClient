using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a SoftBank (IANA PEN 22197) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.softbank</c>.
    /// </summary>
    /// <remarks>
    /// SoftBank Corp. (formerly SoftBank Telecom, SoftBank Mobile, Vodafone Japan)
    /// is a major Japanese telecommunications and internet corporation providing
    /// mobile, broadband, and fixed-line services. These RADIUS attributes are
    /// used by SoftBank's broadband and mobile network infrastructure.
    /// </remarks>
    public enum SoftbankAttributeType : byte
    {
        /// <summary>SoftBank-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>SoftBank-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>SoftBank-Service-Profile (Type 3). String. Service profile name.</summary>
        SERVICE_PROFILE = 3,

        /// <summary>SoftBank-Bandwidth-Max-Up (Type 4). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 4,

        /// <summary>SoftBank-Bandwidth-Max-Down (Type 5). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 5,

        /// <summary>SoftBank-Session-Timeout (Type 6). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 6,

        /// <summary>SoftBank-Idle-Timeout (Type 7). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 7,

        /// <summary>SoftBank-VLAN-Id (Type 8). Integer. VLAN identifier.</summary>
        VLAN_ID = 8,

        /// <summary>SoftBank-Redirect-URL (Type 9). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 9,

        /// <summary>SoftBank-Max-Sessions (Type 10). Integer. Maximum simultaneous sessions.</summary>
        MAX_SESSIONS = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing SoftBank
    /// (IANA PEN 22197) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.softbank</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// SoftBank's vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 22197</c>.
    /// </para>
    /// <para>
    /// These attributes are used by SoftBank Corp. broadband and mobile network
    /// infrastructure for RADIUS-based user group assignment, service profile
    /// selection, upstream/downstream bandwidth provisioning, session and idle
    /// timeout management, VLAN assignment, captive portal URL redirection,
    /// maximum simultaneous session enforcement, and general-purpose
    /// attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(SoftbankAttributes.ServiceProfile("broadband-100m"));
    /// packet.SetAttribute(SoftbankAttributes.BandwidthMaxUp(50000));
    /// packet.SetAttribute(SoftbankAttributes.BandwidthMaxDown(100000));
    /// packet.SetAttribute(SoftbankAttributes.VlanId(200));
    /// packet.SetAttribute(SoftbankAttributes.SessionTimeout(86400));
    /// </code>
    /// </remarks>
    public static class SoftbankAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for SoftBank.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 22197;

        #region Integer Attributes

        /// <summary>
        /// Creates a SoftBank-Bandwidth-Max-Up attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(SoftbankAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a SoftBank-Bandwidth-Max-Down attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(SoftbankAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a SoftBank-Session-Timeout attribute (Type 6) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(SoftbankAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a SoftBank-Idle-Timeout attribute (Type 7) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(SoftbankAttributeType.IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a SoftBank-VLAN-Id attribute (Type 8) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(SoftbankAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a SoftBank-Max-Sessions attribute (Type 10) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum number of simultaneous sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSessions(int value)
        {
            return CreateInteger(SoftbankAttributeType.MAX_SESSIONS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a SoftBank-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(SoftbankAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a SoftBank-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(SoftbankAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a SoftBank-Service-Profile attribute (Type 3) with the specified profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(SoftbankAttributeType.SERVICE_PROFILE, value);
        }

        /// <summary>
        /// Creates a SoftBank-Redirect-URL attribute (Type 9) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(SoftbankAttributeType.REDIRECT_URL, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified SoftBank attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(SoftbankAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified SoftBank attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(SoftbankAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}