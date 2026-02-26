using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Wichorus (IANA PEN 14525) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.wichorus</c>.
    /// </summary>
    /// <remarks>
    /// Wichorus, Inc. (acquired by Tellabs in 2010, later acquired by Coriant/Infinera)
    /// produced mobile packet core gateway platforms for 3G/4G/LTE networks including
    /// GGSN, PGW, SGW, and HA functions, providing carrier-grade subscriber management,
    /// policy enforcement, and session control.
    /// </remarks>
    public enum WichorusAttributeType : byte
    {
        /// <summary>Wichorus-User-Name (Type 1). String. User name.</summary>
        USER_NAME = 1,

        /// <summary>Wichorus-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>Wichorus-Service-Profile (Type 3). String. Service profile name.</summary>
        SERVICE_PROFILE = 3,

        /// <summary>Wichorus-APN (Type 4). String. Access Point Name.</summary>
        APN = 4,

        /// <summary>Wichorus-IMSI (Type 5). String. International Mobile Subscriber Identity.</summary>
        IMSI = 5,

        /// <summary>Wichorus-MSISDN (Type 6). String. Mobile Station ISDN Number.</summary>
        MSISDN = 6,

        /// <summary>Wichorus-Session-Timeout (Type 7). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 7,

        /// <summary>Wichorus-Idle-Timeout (Type 8). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 8,

        /// <summary>Wichorus-Bandwidth-Max-Up (Type 9). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 9,

        /// <summary>Wichorus-Bandwidth-Max-Down (Type 10). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 10,

        /// <summary>Wichorus-VLAN-Id (Type 11). Integer. VLAN identifier.</summary>
        VLAN_ID = 11,

        /// <summary>Wichorus-QoS-Profile (Type 12). String. QoS profile name.</summary>
        QOS_PROFILE = 12,

        /// <summary>Wichorus-IP-Pool (Type 13). String. IP address pool name.</summary>
        IP_POOL = 13,

        /// <summary>Wichorus-Primary-DNS (Type 14). IP address. Primary DNS server.</summary>
        PRIMARY_DNS = 14,

        /// <summary>Wichorus-Secondary-DNS (Type 15). IP address. Secondary DNS server.</summary>
        SECONDARY_DNS = 15,

        /// <summary>Wichorus-Redirect-URL (Type 16). String. Redirect URL.</summary>
        REDIRECT_URL = 16,

        /// <summary>Wichorus-Rulebase (Type 17). String. Rulebase name.</summary>
        RULEBASE = 17,

        /// <summary>Wichorus-NAT-Pool (Type 18). String. NAT pool name.</summary>
        NAT_POOL = 18
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Wichorus
    /// (IANA PEN 14525) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.wichorus</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Wichorus' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14525</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Wichorus (Tellabs / Coriant / Infinera) mobile
    /// packet core gateway platforms for RADIUS-based subscriber identification
    /// (username, IMSI, MSISDN), user group and service profile assignment, APN
    /// configuration, session and idle timeout management, upstream/downstream
    /// bandwidth provisioning, VLAN assignment, QoS profile selection, IP address
    /// pool assignment, DNS server provisioning, URL redirection, rulebase
    /// selection, and NAT pool configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(WichorusAttributes.Imsi("310260123456789"));
    /// packet.SetAttribute(WichorusAttributes.Apn("internet.example.com"));
    /// packet.SetAttribute(WichorusAttributes.ServiceProfile("lte-premium"));
    /// packet.SetAttribute(WichorusAttributes.BandwidthMaxDown(100000));
    /// packet.SetAttribute(WichorusAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(WichorusAttributes.Rulebase("default"));
    /// </code>
    /// </remarks>
    public static class WichorusAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Wichorus.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14525;

        #region Integer Attributes

        /// <summary>Creates a Wichorus-Session-Timeout attribute (Type 7).</summary>
        /// <param name="value">The session timeout in seconds.</param>
        public static VendorSpecificAttributes SessionTimeout(int value) => CreateInteger(WichorusAttributeType.SESSION_TIMEOUT, value);

        /// <summary>Creates a Wichorus-Idle-Timeout attribute (Type 8).</summary>
        /// <param name="value">The idle timeout in seconds.</param>
        public static VendorSpecificAttributes IdleTimeout(int value) => CreateInteger(WichorusAttributeType.IDLE_TIMEOUT, value);

        /// <summary>Creates a Wichorus-Bandwidth-Max-Up attribute (Type 9).</summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        public static VendorSpecificAttributes BandwidthMaxUp(int value) => CreateInteger(WichorusAttributeType.BANDWIDTH_MAX_UP, value);

        /// <summary>Creates a Wichorus-Bandwidth-Max-Down attribute (Type 10).</summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        public static VendorSpecificAttributes BandwidthMaxDown(int value) => CreateInteger(WichorusAttributeType.BANDWIDTH_MAX_DOWN, value);

        /// <summary>Creates a Wichorus-VLAN-Id attribute (Type 11).</summary>
        /// <param name="value">The VLAN identifier.</param>
        public static VendorSpecificAttributes VlanId(int value) => CreateInteger(WichorusAttributeType.VLAN_ID, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Wichorus-User-Name attribute (Type 1).</summary>
        /// <param name="value">The user name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserName(string value) => CreateString(WichorusAttributeType.USER_NAME, value);

        /// <summary>Creates a Wichorus-User-Group attribute (Type 2).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value) => CreateString(WichorusAttributeType.USER_GROUP, value);

        /// <summary>Creates a Wichorus-Service-Profile attribute (Type 3).</summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value) => CreateString(WichorusAttributeType.SERVICE_PROFILE, value);

        /// <summary>Creates a Wichorus-APN attribute (Type 4).</summary>
        /// <param name="value">The Access Point Name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Apn(string value) => CreateString(WichorusAttributeType.APN, value);

        /// <summary>Creates a Wichorus-IMSI attribute (Type 5).</summary>
        /// <param name="value">The IMSI. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imsi(string value) => CreateString(WichorusAttributeType.IMSI, value);

        /// <summary>Creates a Wichorus-MSISDN attribute (Type 6).</summary>
        /// <param name="value">The MSISDN. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Msisdn(string value) => CreateString(WichorusAttributeType.MSISDN, value);

        /// <summary>Creates a Wichorus-QoS-Profile attribute (Type 12).</summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value) => CreateString(WichorusAttributeType.QOS_PROFILE, value);

        /// <summary>Creates a Wichorus-IP-Pool attribute (Type 13).</summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPool(string value) => CreateString(WichorusAttributeType.IP_POOL, value);

        /// <summary>Creates a Wichorus-Redirect-URL attribute (Type 16).</summary>
        /// <param name="value">The redirect URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value) => CreateString(WichorusAttributeType.REDIRECT_URL, value);

        /// <summary>Creates a Wichorus-Rulebase attribute (Type 17).</summary>
        /// <param name="value">The rulebase name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Rulebase(string value) => CreateString(WichorusAttributeType.RULEBASE, value);

        /// <summary>Creates a Wichorus-NAT-Pool attribute (Type 18).</summary>
        /// <param name="value">The NAT pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatPool(string value) => CreateString(WichorusAttributeType.NAT_POOL, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Wichorus-Primary-DNS attribute (Type 14).</summary>
        /// <param name="value">The primary DNS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value) => CreateIpv4(WichorusAttributeType.PRIMARY_DNS, value);

        /// <summary>Creates a Wichorus-Secondary-DNS attribute (Type 15).</summary>
        /// <param name="value">The secondary DNS server. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value) => CreateIpv4(WichorusAttributeType.SECONDARY_DNS, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(WichorusAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(WichorusAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(WichorusAttributeType type, IPAddress value)
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