using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Motorola WiMAX (IANA PEN 161) vendor-specific RADIUS attribute type
    /// from the WiMAX subsystem, as defined in the FreeRADIUS <c>dictionary.motorola.wimax</c>.
    /// </summary>
    /// <remarks>
    /// These WiMAX-specific attributes are used by Motorola's WiMAX base stations
    /// (BSC, ASN-GW) and are separate from the standard Motorola enterprise wireless
    /// LAN attributes in <c>dictionary.motorola</c>. They share the same vendor ID (161)
    /// but use a distinct attribute type range for WiMAX subscriber and session management.
    /// </remarks>
    public enum MotorolaWimaxAttributeType : byte
    {
        /// <summary>Motorola-WiMAX-MIP-MN-HA-SPI (Type 100). Integer. MIP MN-HA Security Parameter Index.</summary>
        MIP_MN_HA_SPI = 100,

        /// <summary>Motorola-WiMAX-MIP-MN-HA-KEY (Type 101). String. MIP MN-HA key.</summary>
        MIP_MN_HA_KEY = 101,

        /// <summary>Motorola-WiMAX-MIP-FA-HA-SPI (Type 102). Integer. MIP FA-HA Security Parameter Index.</summary>
        MIP_FA_HA_SPI = 102,

        /// <summary>Motorola-WiMAX-MIP-FA-HA-KEY (Type 103). String. MIP FA-HA key.</summary>
        MIP_FA_HA_KEY = 103,

        /// <summary>Motorola-WiMAX-MIP-HA-IP (Type 104). IP address. MIP Home Agent IP address.</summary>
        MIP_HA_IP = 104,

        /// <summary>Motorola-WiMAX-MIP-HA-ID (Type 105). String. MIP Home Agent identifier.</summary>
        MIP_HA_ID = 105,

        /// <summary>Motorola-WiMAX-MIP-MN-NAI (Type 106). String. MIP Mobile Node NAI.</summary>
        MIP_MN_NAI = 106,

        /// <summary>Motorola-WiMAX-MN-HoA (Type 107). IP address. Mobile Node Home Address.</summary>
        MN_HOA = 107,

        /// <summary>Motorola-WiMAX-QoS-Descriptor (Type 108). String. QoS descriptor string.</summary>
        QOS_DESCRIPTOR = 108,

        /// <summary>Motorola-WiMAX-SF-ID (Type 109). Integer. Service Flow identifier.</summary>
        SF_ID = 109,

        /// <summary>Motorola-WiMAX-Class-Name (Type 110). String. Service class name.</summary>
        CLASS_NAME = 110,

        /// <summary>Motorola-WiMAX-Direction (Type 111). Integer. Traffic direction.</summary>
        DIRECTION = 111,

        /// <summary>Motorola-WiMAX-Service-Profile-ID (Type 112). Integer. Service profile identifier.</summary>
        SERVICE_PROFILE_ID = 112,

        /// <summary>Motorola-WiMAX-DHCP-Server-IP (Type 113). IP address. DHCP server IP address.</summary>
        DHCP_SERVER_IP = 113,

        /// <summary>Motorola-WiMAX-DHCP-Pool-Name (Type 114). String. DHCP pool name.</summary>
        DHCP_POOL_NAME = 114,

        /// <summary>Motorola-WiMAX-Max-DL-Rate (Type 115). Integer. Maximum downlink rate in bps.</summary>
        MAX_DL_RATE = 115,

        /// <summary>Motorola-WiMAX-Max-UL-Rate (Type 116). Integer. Maximum uplink rate in bps.</summary>
        MAX_UL_RATE = 116,

        /// <summary>Motorola-WiMAX-Domain-Name (Type 117). String. Domain name.</summary>
        DOMAIN_NAME = 117,

        /// <summary>Motorola-WiMAX-DNS-Server-Primary (Type 118). IP address. Primary DNS server.</summary>
        DNS_SERVER_PRIMARY = 118,

        /// <summary>Motorola-WiMAX-DNS-Server-Secondary (Type 119). IP address. Secondary DNS server.</summary>
        DNS_SERVER_SECONDARY = 119,

        /// <summary>Motorola-WiMAX-Acct-Session-Duration (Type 120). Integer. Accounting session duration in seconds.</summary>
        ACCT_SESSION_DURATION = 120,

        /// <summary>Motorola-WiMAX-Idle-Timeout (Type 121). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 121,

        /// <summary>Motorola-WiMAX-Session-Timeout (Type 122). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 122,

        /// <summary>Motorola-WiMAX-Session-Terminate-Cause (Type 123). Integer. Session terminate cause code.</summary>
        SESSION_TERMINATE_CAUSE = 123,

        /// <summary>Motorola-WiMAX-Hot-Lining (Type 124). Integer. Hot-lining flag.</summary>
        HOT_LINING = 124,

        /// <summary>Motorola-WiMAX-Hot-Lining-HTTP-Redirect-URL (Type 125). String. Hot-lining HTTP redirect URL.</summary>
        HOT_LINING_HTTP_REDIRECT_URL = 125,

        /// <summary>Motorola-WiMAX-Hot-Lining-IP-Redirect-IP (Type 126). IP address. Hot-lining IP redirect address.</summary>
        HOT_LINING_IP_REDIRECT_IP = 126
    }

    /// <summary>
    /// Motorola-WiMAX-Direction attribute values (Type 111).
    /// </summary>
    public enum MOTOROLA_WIMAX_DIRECTION
    {
        /// <summary>Uplink direction.</summary>
        UPLINK = 1,

        /// <summary>Downlink direction.</summary>
        DOWNLINK = 2,

        /// <summary>Bidirectional.</summary>
        BIDIRECTIONAL = 3
    }

    /// <summary>
    /// Motorola-WiMAX-Hot-Lining attribute values (Type 124).
    /// </summary>
    public enum MOTOROLA_WIMAX_HOT_LINING
    {
        /// <summary>Hot-lining disabled.</summary>
        DISABLED = 0,

        /// <summary>Hot-lining enabled with HTTP redirect.</summary>
        HTTP_REDIRECT = 1,

        /// <summary>Hot-lining enabled with IP redirect.</summary>
        IP_REDIRECT = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Motorola WiMAX
    /// (IANA PEN 161) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.motorola.wimax</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Motorola WiMAX vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 161</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Motorola WiMAX base stations (BSC) and ASN
    /// gateways for RADIUS-based Mobile IP configuration (MN-HA/FA-HA keys, SPI,
    /// Home Agent addressing, Mobile Node NAI and Home Address), service flow
    /// management (SF ID, class name, QoS descriptor, direction, service profile),
    /// subscriber provisioning (DHCP server/pool, DNS primary/secondary, domain
    /// name, max uplink/downlink rates), session management (idle/session timeouts,
    /// duration, terminate cause), and hot-lining (HTTP/IP redirect for walled
    /// garden enforcement).
    /// </para>
    /// <para>
    /// <b>Note:</b> These attributes share Vendor-Id 161 with
    /// <see cref="MotorolaAttributes"/> but use a distinct attribute type range (100+)
    /// for WiMAX-specific functions.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(MotorolaWimaxAttributes.MaxDlRate(50000000));
    /// packet.SetAttribute(MotorolaWimaxAttributes.MaxUlRate(10000000));
    /// packet.SetAttribute(MotorolaWimaxAttributes.ClassName("gold-service"));
    /// packet.SetAttribute(MotorolaWimaxAttributes.DnsServerPrimary(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(MotorolaWimaxAttributes.SessionTimeout(86400));
    /// </code>
    /// </remarks>
    public static class MotorolaWimaxAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Motorola (shared with <see cref="MotorolaAttributes"/>).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 161;

        #region Integer Attributes

        /// <summary>Creates a Motorola-WiMAX-MIP-MN-HA-SPI attribute (Type 100).</summary>
        /// <param name="value">The MIP MN-HA Security Parameter Index.</param>
        public static VendorSpecificAttributes MipMnHaSpi(int value) => CreateInteger(MotorolaWimaxAttributeType.MIP_MN_HA_SPI, value);

        /// <summary>Creates a Motorola-WiMAX-MIP-FA-HA-SPI attribute (Type 102).</summary>
        /// <param name="value">The MIP FA-HA Security Parameter Index.</param>
        public static VendorSpecificAttributes MipFaHaSpi(int value) => CreateInteger(MotorolaWimaxAttributeType.MIP_FA_HA_SPI, value);

        /// <summary>Creates a Motorola-WiMAX-SF-ID attribute (Type 109).</summary>
        /// <param name="value">The Service Flow identifier.</param>
        public static VendorSpecificAttributes SfId(int value) => CreateInteger(MotorolaWimaxAttributeType.SF_ID, value);

        /// <summary>Creates a Motorola-WiMAX-Direction attribute (Type 111).</summary>
        /// <param name="value">The traffic direction. See <see cref="MOTOROLA_WIMAX_DIRECTION"/>.</param>
        public static VendorSpecificAttributes Direction(MOTOROLA_WIMAX_DIRECTION value) => CreateInteger(MotorolaWimaxAttributeType.DIRECTION, (int)value);

        /// <summary>Creates a Motorola-WiMAX-Service-Profile-ID attribute (Type 112).</summary>
        /// <param name="value">The service profile identifier.</param>
        public static VendorSpecificAttributes ServiceProfileId(int value) => CreateInteger(MotorolaWimaxAttributeType.SERVICE_PROFILE_ID, value);

        /// <summary>Creates a Motorola-WiMAX-Max-DL-Rate attribute (Type 115).</summary>
        /// <param name="value">The maximum downlink rate in bps.</param>
        public static VendorSpecificAttributes MaxDlRate(int value) => CreateInteger(MotorolaWimaxAttributeType.MAX_DL_RATE, value);

        /// <summary>Creates a Motorola-WiMAX-Max-UL-Rate attribute (Type 116).</summary>
        /// <param name="value">The maximum uplink rate in bps.</param>
        public static VendorSpecificAttributes MaxUlRate(int value) => CreateInteger(MotorolaWimaxAttributeType.MAX_UL_RATE, value);

        /// <summary>Creates a Motorola-WiMAX-Acct-Session-Duration attribute (Type 120).</summary>
        /// <param name="value">The accounting session duration in seconds.</param>
        public static VendorSpecificAttributes AcctSessionDuration(int value) => CreateInteger(MotorolaWimaxAttributeType.ACCT_SESSION_DURATION, value);

        /// <summary>Creates a Motorola-WiMAX-Idle-Timeout attribute (Type 121).</summary>
        /// <param name="value">The idle timeout in seconds.</param>
        public static VendorSpecificAttributes IdleTimeout(int value) => CreateInteger(MotorolaWimaxAttributeType.IDLE_TIMEOUT, value);

        /// <summary>Creates a Motorola-WiMAX-Session-Timeout attribute (Type 122).</summary>
        /// <param name="value">The session timeout in seconds.</param>
        public static VendorSpecificAttributes SessionTimeout(int value) => CreateInteger(MotorolaWimaxAttributeType.SESSION_TIMEOUT, value);

        /// <summary>Creates a Motorola-WiMAX-Session-Terminate-Cause attribute (Type 123).</summary>
        /// <param name="value">The session terminate cause code.</param>
        public static VendorSpecificAttributes SessionTerminateCause(int value) => CreateInteger(MotorolaWimaxAttributeType.SESSION_TERMINATE_CAUSE, value);

        /// <summary>Creates a Motorola-WiMAX-Hot-Lining attribute (Type 124).</summary>
        /// <param name="value">The hot-lining flag. See <see cref="MOTOROLA_WIMAX_HOT_LINING"/>.</param>
        public static VendorSpecificAttributes HotLining(MOTOROLA_WIMAX_HOT_LINING value) => CreateInteger(MotorolaWimaxAttributeType.HOT_LINING, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Motorola-WiMAX-MIP-MN-HA-KEY attribute (Type 101).</summary>
        /// <param name="value">The MIP MN-HA key. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MipMnHaKey(string value) => CreateString(MotorolaWimaxAttributeType.MIP_MN_HA_KEY, value);

        /// <summary>Creates a Motorola-WiMAX-MIP-FA-HA-KEY attribute (Type 103).</summary>
        /// <param name="value">The MIP FA-HA key. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MipFaHaKey(string value) => CreateString(MotorolaWimaxAttributeType.MIP_FA_HA_KEY, value);

        /// <summary>Creates a Motorola-WiMAX-MIP-HA-ID attribute (Type 105).</summary>
        /// <param name="value">The MIP Home Agent identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MipHaId(string value) => CreateString(MotorolaWimaxAttributeType.MIP_HA_ID, value);

        /// <summary>Creates a Motorola-WiMAX-MIP-MN-NAI attribute (Type 106).</summary>
        /// <param name="value">The MIP Mobile Node NAI. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MipMnNai(string value) => CreateString(MotorolaWimaxAttributeType.MIP_MN_NAI, value);

        /// <summary>Creates a Motorola-WiMAX-QoS-Descriptor attribute (Type 108).</summary>
        /// <param name="value">The QoS descriptor string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosDescriptor(string value) => CreateString(MotorolaWimaxAttributeType.QOS_DESCRIPTOR, value);

        /// <summary>Creates a Motorola-WiMAX-Class-Name attribute (Type 110).</summary>
        /// <param name="value">The service class name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClassName(string value) => CreateString(MotorolaWimaxAttributeType.CLASS_NAME, value);

        /// <summary>Creates a Motorola-WiMAX-DHCP-Pool-Name attribute (Type 114).</summary>
        /// <param name="value">The DHCP pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpPoolName(string value) => CreateString(MotorolaWimaxAttributeType.DHCP_POOL_NAME, value);

        /// <summary>Creates a Motorola-WiMAX-Domain-Name attribute (Type 117).</summary>
        /// <param name="value">The domain name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DomainName(string value) => CreateString(MotorolaWimaxAttributeType.DOMAIN_NAME, value);

        /// <summary>Creates a Motorola-WiMAX-Hot-Lining-HTTP-Redirect-URL attribute (Type 125).</summary>
        /// <param name="value">The hot-lining HTTP redirect URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HotLiningHttpRedirectUrl(string value) => CreateString(MotorolaWimaxAttributeType.HOT_LINING_HTTP_REDIRECT_URL, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Motorola-WiMAX-MIP-HA-IP attribute (Type 104).</summary>
        /// <param name="value">The MIP Home Agent IP address. Must be IPv4.</param>
        public static VendorSpecificAttributes MipHaIp(IPAddress value) => CreateIpv4(MotorolaWimaxAttributeType.MIP_HA_IP, value);

        /// <summary>Creates a Motorola-WiMAX-MN-HoA attribute (Type 107).</summary>
        /// <param name="value">The Mobile Node Home Address. Must be IPv4.</param>
        public static VendorSpecificAttributes MnHoA(IPAddress value) => CreateIpv4(MotorolaWimaxAttributeType.MN_HOA, value);

        /// <summary>Creates a Motorola-WiMAX-DHCP-Server-IP attribute (Type 113).</summary>
        /// <param name="value">The DHCP server IP address. Must be IPv4.</param>
        public static VendorSpecificAttributes DhcpServerIp(IPAddress value) => CreateIpv4(MotorolaWimaxAttributeType.DHCP_SERVER_IP, value);

        /// <summary>Creates a Motorola-WiMAX-DNS-Server-Primary attribute (Type 118).</summary>
        /// <param name="value">The primary DNS server. Must be IPv4.</param>
        public static VendorSpecificAttributes DnsServerPrimary(IPAddress value) => CreateIpv4(MotorolaWimaxAttributeType.DNS_SERVER_PRIMARY, value);

        /// <summary>Creates a Motorola-WiMAX-DNS-Server-Secondary attribute (Type 119).</summary>
        /// <param name="value">The secondary DNS server. Must be IPv4.</param>
        public static VendorSpecificAttributes DnsServerSecondary(IPAddress value) => CreateIpv4(MotorolaWimaxAttributeType.DNS_SERVER_SECONDARY, value);

        /// <summary>Creates a Motorola-WiMAX-Hot-Lining-IP-Redirect-IP attribute (Type 126).</summary>
        /// <param name="value">The hot-lining IP redirect address. Must be IPv4.</param>
        public static VendorSpecificAttributes HotLiningIpRedirectIp(IPAddress value) => CreateIpv4(MotorolaWimaxAttributeType.HOT_LINING_IP_REDIRECT_IP, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(MotorolaWimaxAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(MotorolaWimaxAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(MotorolaWimaxAttributeType type, IPAddress value)
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