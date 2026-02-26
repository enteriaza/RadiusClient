using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Slipstream Data (IANA PEN 3551) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.slipstream</c>.
    /// </summary>
    /// <remarks>
    /// Slipstream Data produced broadband access and subscriber management
    /// platforms for ISPs, providing subscriber authentication, bandwidth
    /// management, and session control capabilities.
    /// </remarks>
    public enum SlipstreamAttributeType : byte
    {
        /// <summary>Slipstream-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Slipstream-Service-Profile (Type 2). String. Service profile name.</summary>
        SERVICE_PROFILE = 2,

        /// <summary>Slipstream-Bandwidth-Up (Type 3). Integer. Upstream bandwidth in Kbps.</summary>
        BANDWIDTH_UP = 3,

        /// <summary>Slipstream-Bandwidth-Down (Type 4). Integer. Downstream bandwidth in Kbps.</summary>
        BANDWIDTH_DOWN = 4,

        /// <summary>Slipstream-Session-Timeout (Type 5). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 5,

        /// <summary>Slipstream-Idle-Timeout (Type 6). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 6,

        /// <summary>Slipstream-VLAN-Id (Type 7). Integer. VLAN identifier.</summary>
        VLAN_ID = 7,

        /// <summary>Slipstream-User-Group (Type 8). String. User group name.</summary>
        USER_GROUP = 8,

        /// <summary>Slipstream-Redirect-URL (Type 9). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 9,

        /// <summary>Slipstream-Max-Sessions (Type 10). Integer. Maximum simultaneous sessions.</summary>
        MAX_SESSIONS = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Slipstream Data
    /// (IANA PEN 3551) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.slipstream</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Slipstream's vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3551</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Slipstream Data broadband access and subscriber
    /// management platforms for RADIUS-based service profile selection,
    /// upstream/downstream bandwidth provisioning, session and idle timeout
    /// management, VLAN assignment, user group mapping, captive portal URL
    /// redirection, maximum simultaneous session enforcement, and general-purpose
    /// attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(SlipstreamAttributes.ServiceProfile("residential-50m"));
    /// packet.SetAttribute(SlipstreamAttributes.BandwidthUp(10000));
    /// packet.SetAttribute(SlipstreamAttributes.BandwidthDown(50000));
    /// packet.SetAttribute(SlipstreamAttributes.VlanId(200));
    /// packet.SetAttribute(SlipstreamAttributes.SessionTimeout(86400));
    /// packet.SetAttribute(SlipstreamAttributes.MaxSessions(1));
    /// </code>
    /// </remarks>
    public static class SlipstreamAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Slipstream Data.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3551;

        #region Integer Attributes

        /// <summary>
        /// Creates a Slipstream-Bandwidth-Up attribute (Type 3) with the specified rate.
        /// </summary>
        /// <param name="value">The upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthUp(int value)
        {
            return CreateInteger(SlipstreamAttributeType.BANDWIDTH_UP, value);
        }

        /// <summary>
        /// Creates a Slipstream-Bandwidth-Down attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthDown(int value)
        {
            return CreateInteger(SlipstreamAttributeType.BANDWIDTH_DOWN, value);
        }

        /// <summary>
        /// Creates a Slipstream-Session-Timeout attribute (Type 5) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(SlipstreamAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Slipstream-Idle-Timeout attribute (Type 6) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(SlipstreamAttributeType.IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Slipstream-VLAN-Id attribute (Type 7) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(SlipstreamAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Slipstream-Max-Sessions attribute (Type 10) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum number of simultaneous sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSessions(int value)
        {
            return CreateInteger(SlipstreamAttributeType.MAX_SESSIONS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Slipstream-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(SlipstreamAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Slipstream-Service-Profile attribute (Type 2) with the specified profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(SlipstreamAttributeType.SERVICE_PROFILE, value);
        }

        /// <summary>
        /// Creates a Slipstream-User-Group attribute (Type 8) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(SlipstreamAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Slipstream-Redirect-URL attribute (Type 9) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(SlipstreamAttributeType.REDIRECT_URL, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Slipstream attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(SlipstreamAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Slipstream attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(SlipstreamAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}