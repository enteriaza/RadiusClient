using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a T-Systems Nova (IANA PEN 16787) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.t_systems_nova</c>.
    /// </summary>
    /// <remarks>
    /// T-Systems Nova GmbH (a subsidiary of Deutsche Telekom / T-Systems International)
    /// provides broadband and enterprise networking solutions for the German and
    /// European telecommunications market. These RADIUS attributes are used by
    /// T-Systems Nova subscriber management and broadband access platforms.
    /// </remarks>
    public enum TSystemsNovaAttributeType : byte
    {
        /// <summary>T-Systems-Nova-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>T-Systems-Nova-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>T-Systems-Nova-Service-Profile (Type 3). String. Service profile name.</summary>
        SERVICE_PROFILE = 3,

        /// <summary>T-Systems-Nova-Bandwidth-Max-Up (Type 4). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 4,

        /// <summary>T-Systems-Nova-Bandwidth-Max-Down (Type 5). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 5,

        /// <summary>T-Systems-Nova-Session-Timeout (Type 6). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 6,

        /// <summary>T-Systems-Nova-Idle-Timeout (Type 7). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 7,

        /// <summary>T-Systems-Nova-VLAN-Id (Type 8). Integer. VLAN identifier.</summary>
        VLAN_ID = 8,

        /// <summary>T-Systems-Nova-Redirect-URL (Type 9). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 9,

        /// <summary>T-Systems-Nova-DNS-Primary (Type 10). IP address. Primary DNS server.</summary>
        DNS_PRIMARY = 10,

        /// <summary>T-Systems-Nova-DNS-Secondary (Type 11). IP address. Secondary DNS server.</summary>
        DNS_SECONDARY = 11,

        /// <summary>T-Systems-Nova-IP-Pool (Type 12). String. IP address pool name.</summary>
        IP_POOL = 12,

        /// <summary>T-Systems-Nova-Max-Sessions (Type 13). Integer. Maximum simultaneous sessions.</summary>
        MAX_SESSIONS = 13
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing T-Systems Nova
    /// (IANA PEN 16787) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.t_systems_nova</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// T-Systems Nova's vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped
    /// in a <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 16787</c>.
    /// </para>
    /// <para>
    /// These attributes are used by T-Systems Nova (Deutsche Telekom) subscriber
    /// management and broadband access platforms for RADIUS-based user group
    /// assignment, service profile selection, upstream/downstream bandwidth
    /// provisioning, session and idle timeout management, VLAN assignment,
    /// captive portal URL redirection, DNS server provisioning, IP address
    /// pool assignment, maximum simultaneous session enforcement, and
    /// general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(TSystemsNovaAttributes.ServiceProfile("dsl-100m"));
    /// packet.SetAttribute(TSystemsNovaAttributes.BandwidthMaxUp(40000));
    /// packet.SetAttribute(TSystemsNovaAttributes.BandwidthMaxDown(100000));
    /// packet.SetAttribute(TSystemsNovaAttributes.VlanId(200));
    /// packet.SetAttribute(TSystemsNovaAttributes.DnsPrimary(IPAddress.Parse("194.25.0.60")));
    /// packet.SetAttribute(TSystemsNovaAttributes.IpPool("residential-pool"));
    /// </code>
    /// </remarks>
    public static class TSystemsNovaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for T-Systems Nova.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 16787;

        #region Integer Attributes

        /// <summary>
        /// Creates a T-Systems-Nova-Bandwidth-Max-Up attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(TSystemsNovaAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a T-Systems-Nova-Bandwidth-Max-Down attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(TSystemsNovaAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a T-Systems-Nova-Session-Timeout attribute (Type 6) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(TSystemsNovaAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a T-Systems-Nova-Idle-Timeout attribute (Type 7) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(TSystemsNovaAttributeType.IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a T-Systems-Nova-VLAN-Id attribute (Type 8) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(TSystemsNovaAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a T-Systems-Nova-Max-Sessions attribute (Type 13) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum number of simultaneous sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSessions(int value)
        {
            return CreateInteger(TSystemsNovaAttributeType.MAX_SESSIONS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a T-Systems-Nova-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(TSystemsNovaAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a T-Systems-Nova-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(TSystemsNovaAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a T-Systems-Nova-Service-Profile attribute (Type 3) with the specified profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(TSystemsNovaAttributeType.SERVICE_PROFILE, value);
        }

        /// <summary>
        /// Creates a T-Systems-Nova-Redirect-URL attribute (Type 9) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(TSystemsNovaAttributeType.REDIRECT_URL, value);
        }

        /// <summary>
        /// Creates a T-Systems-Nova-IP-Pool attribute (Type 12) with the specified pool name.
        /// </summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPool(string value)
        {
            return CreateString(TSystemsNovaAttributeType.IP_POOL, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates a T-Systems-Nova-DNS-Primary attribute (Type 10) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes DnsPrimary(IPAddress value)
        {
            return CreateIpv4(TSystemsNovaAttributeType.DNS_PRIMARY, value);
        }

        /// <summary>
        /// Creates a T-Systems-Nova-DNS-Secondary attribute (Type 11) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes DnsSecondary(IPAddress value)
        {
            return CreateIpv4(TSystemsNovaAttributeType.DNS_SECONDARY, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(TSystemsNovaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(TSystemsNovaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(TSystemsNovaAttributeType type, IPAddress value)
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