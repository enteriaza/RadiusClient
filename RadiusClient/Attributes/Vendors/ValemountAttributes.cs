using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Valemount Networks (IANA PEN 16313) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.valemount</c>.
    /// </summary>
    /// <remarks>
    /// Valemount Networks Corporation is a Canadian telecommunications provider
    /// offering broadband internet, telephone, and television services. These
    /// RADIUS attributes are used by Valemount's subscriber management and
    /// broadband access infrastructure.
    /// </remarks>
    public enum ValemountAttributeType : byte
    {
        /// <summary>Valemount-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Valemount-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>Valemount-Service-Profile (Type 3). String. Service profile name.</summary>
        SERVICE_PROFILE = 3,

        /// <summary>Valemount-Bandwidth-Max-Up (Type 4). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 4,

        /// <summary>Valemount-Bandwidth-Max-Down (Type 5). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 5,

        /// <summary>Valemount-Session-Timeout (Type 6). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 6,

        /// <summary>Valemount-Idle-Timeout (Type 7). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 7,

        /// <summary>Valemount-VLAN-Id (Type 8). Integer. VLAN identifier.</summary>
        VLAN_ID = 8,

        /// <summary>Valemount-Redirect-URL (Type 9). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 9,

        /// <summary>Valemount-Max-Sessions (Type 10). Integer. Maximum simultaneous sessions.</summary>
        MAX_SESSIONS = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Valemount Networks
    /// (IANA PEN 16313) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.valemount</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Valemount's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 16313</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Valemount Networks broadband subscriber management
    /// infrastructure for RADIUS-based user group assignment, service profile selection,
    /// upstream/downstream bandwidth provisioning, session and idle timeout management,
    /// VLAN assignment, captive portal URL redirection, maximum simultaneous session
    /// enforcement, and general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ValemountAttributes.ServiceProfile("residential-50m"));
    /// packet.SetAttribute(ValemountAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(ValemountAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(ValemountAttributes.VlanId(100));
    /// packet.SetAttribute(ValemountAttributes.SessionTimeout(86400));
    /// </code>
    /// </remarks>
    public static class ValemountAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Valemount Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 16313;

        #region Integer Attributes

        /// <summary>
        /// Creates a Valemount-Bandwidth-Max-Up attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(ValemountAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Valemount-Bandwidth-Max-Down attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(ValemountAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a Valemount-Session-Timeout attribute (Type 6) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(ValemountAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Valemount-Idle-Timeout attribute (Type 7) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(ValemountAttributeType.IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Valemount-VLAN-Id attribute (Type 8) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(ValemountAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Valemount-Max-Sessions attribute (Type 10) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum number of simultaneous sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSessions(int value)
        {
            return CreateInteger(ValemountAttributeType.MAX_SESSIONS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Valemount-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(ValemountAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Valemount-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(ValemountAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Valemount-Service-Profile attribute (Type 3) with the specified profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(ValemountAttributeType.SERVICE_PROFILE, value);
        }

        /// <summary>
        /// Creates a Valemount-Redirect-URL attribute (Type 9) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(ValemountAttributeType.REDIRECT_URL, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(ValemountAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(ValemountAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}