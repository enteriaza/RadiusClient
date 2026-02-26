using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a SURFnet (IANA PEN 1076) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.surfnet</c>.
    /// </summary>
    /// <remarks>
    /// SURFnet is the Dutch national research and education network (NREN),
    /// providing high-performance networking and internet services to higher
    /// education and research institutions in the Netherlands. These RADIUS
    /// attributes support eduroam and campus network access management.
    /// </remarks>
    public enum SurfnetAttributeType : byte
    {
        /// <summary>Surfnet-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Surfnet-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>Surfnet-VLAN-Id (Type 3). Integer. VLAN identifier.</summary>
        VLAN_ID = 3,

        /// <summary>Surfnet-Bandwidth-Max-Up (Type 4). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 4,

        /// <summary>Surfnet-Bandwidth-Max-Down (Type 5). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 5,

        /// <summary>Surfnet-Session-Timeout (Type 6). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 6,

        /// <summary>Surfnet-Idle-Timeout (Type 7). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 7,

        /// <summary>Surfnet-Redirect-URL (Type 8). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 8,

        /// <summary>Surfnet-Institution (Type 9). String. Institution name.</summary>
        INSTITUTION = 9,

        /// <summary>Surfnet-Service-Profile (Type 10). String. Service profile name.</summary>
        SERVICE_PROFILE = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing SURFnet
    /// (IANA PEN 1076) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.surfnet</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// SURFnet's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 1076</c>.
    /// </para>
    /// <para>
    /// These attributes are used by SURFnet (Dutch NREN) for RADIUS-based eduroam
    /// and campus network access management including user group assignment, VLAN
    /// assignment, upstream/downstream bandwidth provisioning, session and idle
    /// timeout management, captive portal URL redirection, institution
    /// identification, service profile selection, and general-purpose
    /// attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(SurfnetAttributes.Institution("university-of-amsterdam"));
    /// packet.SetAttribute(SurfnetAttributes.VlanId(200));
    /// packet.SetAttribute(SurfnetAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(SurfnetAttributes.BandwidthMaxDown(100000));
    /// packet.SetAttribute(SurfnetAttributes.ServiceProfile("eduroam-staff"));
    /// </code>
    /// </remarks>
    public static class SurfnetAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for SURFnet.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 1076;

        #region Integer Attributes

        /// <summary>
        /// Creates a Surfnet-VLAN-Id attribute (Type 3) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(SurfnetAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Surfnet-Bandwidth-Max-Up attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(SurfnetAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Surfnet-Bandwidth-Max-Down attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(SurfnetAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a Surfnet-Session-Timeout attribute (Type 6) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(SurfnetAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Surfnet-Idle-Timeout attribute (Type 7) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(SurfnetAttributeType.IDLE_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Surfnet-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(SurfnetAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Surfnet-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(SurfnetAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Surfnet-Redirect-URL attribute (Type 8) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(SurfnetAttributeType.REDIRECT_URL, value);
        }

        /// <summary>
        /// Creates a Surfnet-Institution attribute (Type 9) with the specified institution name.
        /// </summary>
        /// <param name="value">The institution name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Institution(string value)
        {
            return CreateString(SurfnetAttributeType.INSTITUTION, value);
        }

        /// <summary>
        /// Creates a Surfnet-Service-Profile attribute (Type 10) with the specified profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(SurfnetAttributeType.SERVICE_PROFILE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified SURFnet attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(SurfnetAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified SURFnet attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(SurfnetAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}