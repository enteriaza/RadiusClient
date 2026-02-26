using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a UTStarcom (IANA PEN 7064) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.utstarcom</c>.
    /// </summary>
    /// <remarks>
    /// UTStarcom, Inc. produced broadband access equipment, IP DSLAM/MSAN platforms,
    /// IPTV systems, and mobile softswitch/media gateway solutions for
    /// telecommunications carriers and service providers worldwide.
    /// </remarks>
    public enum UtstarcomAttributeType : byte
    {
        /// <summary>UTStarcom-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>UTStarcom-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>UTStarcom-Service-Profile (Type 3). String. Service profile name.</summary>
        SERVICE_PROFILE = 3,

        /// <summary>UTStarcom-Bandwidth-Max-Up (Type 4). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 4,

        /// <summary>UTStarcom-Bandwidth-Max-Down (Type 5). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 5,

        /// <summary>UTStarcom-Session-Timeout (Type 6). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 6,

        /// <summary>UTStarcom-Idle-Timeout (Type 7). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 7,

        /// <summary>UTStarcom-VLAN-Id (Type 8). Integer. VLAN identifier.</summary>
        VLAN_ID = 8,

        /// <summary>UTStarcom-Redirect-URL (Type 9). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 9,

        /// <summary>UTStarcom-Primary-DNS (Type 10). IP address. Primary DNS server.</summary>
        PRIMARY_DNS = 10,

        /// <summary>UTStarcom-Secondary-DNS (Type 11). IP address. Secondary DNS server.</summary>
        SECONDARY_DNS = 11,

        /// <summary>UTStarcom-IP-Pool (Type 12). String. IP address pool name.</summary>
        IP_POOL = 12,

        /// <summary>UTStarcom-QoS-Profile (Type 13). String. QoS profile name.</summary>
        QOS_PROFILE = 13,

        /// <summary>UTStarcom-Max-Sessions (Type 14). Integer. Maximum simultaneous sessions.</summary>
        MAX_SESSIONS = 14
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing UTStarcom
    /// (IANA PEN 7064) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.utstarcom</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// UTStarcom's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 7064</c>.
    /// </para>
    /// <para>
    /// These attributes are used by UTStarcom broadband access equipment, IP DSLAM/MSAN
    /// platforms, and mobile softswitch solutions for RADIUS-based user group assignment,
    /// service profile selection, upstream/downstream bandwidth provisioning, session
    /// and idle timeout management, VLAN assignment, captive portal URL redirection,
    /// DNS server provisioning, IP address pool assignment, QoS profile selection,
    /// maximum simultaneous session enforcement, and general-purpose attribute-value
    /// pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(UtstarcomAttributes.ServiceProfile("dsl-premium"));
    /// packet.SetAttribute(UtstarcomAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(UtstarcomAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(UtstarcomAttributes.VlanId(200));
    /// packet.SetAttribute(UtstarcomAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(UtstarcomAttributes.QosProfile("premium-qos"));
    /// </code>
    /// </remarks>
    public static class UtstarcomAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for UTStarcom.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 7064;

        #region Integer Attributes

        /// <summary>
        /// Creates a UTStarcom-Bandwidth-Max-Up attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(UtstarcomAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a UTStarcom-Bandwidth-Max-Down attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(UtstarcomAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a UTStarcom-Session-Timeout attribute (Type 6) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(UtstarcomAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a UTStarcom-Idle-Timeout attribute (Type 7) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(UtstarcomAttributeType.IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a UTStarcom-VLAN-Id attribute (Type 8) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(UtstarcomAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a UTStarcom-Max-Sessions attribute (Type 14) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum number of simultaneous sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSessions(int value)
        {
            return CreateInteger(UtstarcomAttributeType.MAX_SESSIONS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a UTStarcom-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(UtstarcomAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a UTStarcom-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(UtstarcomAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a UTStarcom-Service-Profile attribute (Type 3) with the specified profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(UtstarcomAttributeType.SERVICE_PROFILE, value);
        }

        /// <summary>
        /// Creates a UTStarcom-Redirect-URL attribute (Type 9) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(UtstarcomAttributeType.REDIRECT_URL, value);
        }

        /// <summary>
        /// Creates a UTStarcom-IP-Pool attribute (Type 12) with the specified pool name.
        /// </summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPool(string value)
        {
            return CreateString(UtstarcomAttributeType.IP_POOL, value);
        }

        /// <summary>
        /// Creates a UTStarcom-QoS-Profile attribute (Type 13) with the specified profile name.
        /// </summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value)
        {
            return CreateString(UtstarcomAttributeType.QOS_PROFILE, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates a UTStarcom-Primary-DNS attribute (Type 10) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value)
        {
            return CreateIpv4(UtstarcomAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates a UTStarcom-Secondary-DNS attribute (Type 11) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value)
        {
            return CreateIpv4(UtstarcomAttributeType.SECONDARY_DNS, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(UtstarcomAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(UtstarcomAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(UtstarcomAttributeType type, IPAddress value)
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