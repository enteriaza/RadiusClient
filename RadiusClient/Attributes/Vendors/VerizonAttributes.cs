using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Verizon (IANA PEN 12382) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.verizon</c>.
    /// </summary>
    /// <remarks>
    /// Verizon Communications Inc. is a major US telecommunications company providing
    /// wireless (Verizon Wireless), FiOS fiber-to-the-premises, DSL, and enterprise
    /// networking services. These RADIUS attributes are used by Verizon's broadband
    /// subscriber management, DSL/FiOS access, and enterprise service platforms.
    /// </remarks>
    public enum VerizonAttributeType : byte
    {
        /// <summary>Verizon-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Verizon-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>Verizon-Service-Profile (Type 3). String. Service profile name.</summary>
        SERVICE_PROFILE = 3,

        /// <summary>Verizon-Bandwidth-Max-Up (Type 4). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 4,

        /// <summary>Verizon-Bandwidth-Max-Down (Type 5). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 5,

        /// <summary>Verizon-Session-Timeout (Type 6). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 6,

        /// <summary>Verizon-Idle-Timeout (Type 7). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 7,

        /// <summary>Verizon-VLAN-Id (Type 8). Integer. VLAN identifier.</summary>
        VLAN_ID = 8,

        /// <summary>Verizon-Redirect-URL (Type 9). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 9,

        /// <summary>Verizon-Primary-DNS (Type 10). IP address. Primary DNS server.</summary>
        PRIMARY_DNS = 10,

        /// <summary>Verizon-Secondary-DNS (Type 11). IP address. Secondary DNS server.</summary>
        SECONDARY_DNS = 11,

        /// <summary>Verizon-IP-Pool (Type 12). String. IP address pool name.</summary>
        IP_POOL = 12,

        /// <summary>Verizon-Max-Sessions (Type 13). Integer. Maximum simultaneous sessions.</summary>
        MAX_SESSIONS = 13,

        /// <summary>Verizon-QoS-Profile (Type 14). String. QoS profile name.</summary>
        QOS_PROFILE = 14
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Verizon
    /// (IANA PEN 12382) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.verizon</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Verizon's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 12382</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Verizon broadband subscriber management, DSL/FiOS
    /// access, and enterprise service platforms for RADIUS-based user group assignment,
    /// service profile selection, upstream/downstream bandwidth provisioning, session
    /// and idle timeout management, VLAN assignment, captive portal URL redirection,
    /// DNS server provisioning, IP address pool assignment, maximum simultaneous session
    /// enforcement, QoS profile selection, and general-purpose attribute-value pair
    /// configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(VerizonAttributes.ServiceProfile("fios-gigabit"));
    /// packet.SetAttribute(VerizonAttributes.BandwidthMaxUp(880000));
    /// packet.SetAttribute(VerizonAttributes.BandwidthMaxDown(940000));
    /// packet.SetAttribute(VerizonAttributes.VlanId(200));
    /// packet.SetAttribute(VerizonAttributes.PrimaryDns(IPAddress.Parse("4.2.2.1")));
    /// </code>
    /// </remarks>
    public static class VerizonAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Verizon.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 12382;

        #region Integer Attributes

        /// <summary>Creates a Verizon-Bandwidth-Max-Up attribute (Type 4).</summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        public static VendorSpecificAttributes BandwidthMaxUp(int value) => CreateInteger(VerizonAttributeType.BANDWIDTH_MAX_UP, value);

        /// <summary>Creates a Verizon-Bandwidth-Max-Down attribute (Type 5).</summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        public static VendorSpecificAttributes BandwidthMaxDown(int value) => CreateInteger(VerizonAttributeType.BANDWIDTH_MAX_DOWN, value);

        /// <summary>Creates a Verizon-Session-Timeout attribute (Type 6).</summary>
        /// <param name="value">The session timeout in seconds.</param>
        public static VendorSpecificAttributes SessionTimeout(int value) => CreateInteger(VerizonAttributeType.SESSION_TIMEOUT, value);

        /// <summary>Creates a Verizon-Idle-Timeout attribute (Type 7).</summary>
        /// <param name="value">The idle timeout in seconds.</param>
        public static VendorSpecificAttributes IdleTimeout(int value) => CreateInteger(VerizonAttributeType.IDLE_TIMEOUT, value);

        /// <summary>Creates a Verizon-VLAN-Id attribute (Type 8).</summary>
        /// <param name="value">The VLAN identifier.</param>
        public static VendorSpecificAttributes VlanId(int value) => CreateInteger(VerizonAttributeType.VLAN_ID, value);

        /// <summary>Creates a Verizon-Max-Sessions attribute (Type 13).</summary>
        /// <param name="value">The maximum number of simultaneous sessions.</param>
        public static VendorSpecificAttributes MaxSessions(int value) => CreateInteger(VerizonAttributeType.MAX_SESSIONS, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Verizon-AVPair attribute (Type 1).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(VerizonAttributeType.AVPAIR, value);

        /// <summary>Creates a Verizon-User-Group attribute (Type 2).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value) => CreateString(VerizonAttributeType.USER_GROUP, value);

        /// <summary>Creates a Verizon-Service-Profile attribute (Type 3).</summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value) => CreateString(VerizonAttributeType.SERVICE_PROFILE, value);

        /// <summary>Creates a Verizon-Redirect-URL attribute (Type 9).</summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value) => CreateString(VerizonAttributeType.REDIRECT_URL, value);

        /// <summary>Creates a Verizon-IP-Pool attribute (Type 12).</summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPool(string value) => CreateString(VerizonAttributeType.IP_POOL, value);

        /// <summary>Creates a Verizon-QoS-Profile attribute (Type 14).</summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value) => CreateString(VerizonAttributeType.QOS_PROFILE, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Verizon-Primary-DNS attribute (Type 10).</summary>
        /// <param name="value">The primary DNS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value) => CreateIpv4(VerizonAttributeType.PRIMARY_DNS, value);

        /// <summary>Creates a Verizon-Secondary-DNS attribute (Type 11).</summary>
        /// <param name="value">The secondary DNS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value) => CreateIpv4(VerizonAttributeType.SECONDARY_DNS, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(VerizonAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(VerizonAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(VerizonAttributeType type, IPAddress value)
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