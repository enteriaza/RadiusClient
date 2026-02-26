using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Ruckus Wireless (IANA PEN 25053) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.ruckus</c>.
    /// </summary>
    /// <remarks>
    /// Ruckus Wireless (formerly Ruckus Networks, acquired by CommScope in 2019)
    /// produces enterprise Wi-Fi access points, wireless controllers (SmartZone,
    /// ZoneDirector, vSZ), ICX switches, and cloud-managed networking platforms
    /// (Ruckus Cloud / RUCKUS One).
    /// </remarks>
    public enum RuckusAttributeType : byte
    {
        /// <summary>Ruckus-User-Groups (Type 1). String. User group name(s).</summary>
        USER_GROUPS = 1,

        /// <summary>Ruckus-Sta-RSSI (Type 2). Integer. Station RSSI value.</summary>
        STA_RSSI = 2,

        /// <summary>Ruckus-SSID (Type 3). String. Wireless SSID name.</summary>
        SSID = 3,

        /// <summary>Ruckus-Wlan-Id (Type 4). Integer. WLAN identifier.</summary>
        WLAN_ID = 4,

        /// <summary>Ruckus-Location (Type 5). String. AP location string.</summary>
        LOCATION = 5,

        /// <summary>Ruckus-Grace-Period (Type 6). Integer. Grace period in seconds.</summary>
        GRACE_PERIOD = 6,

        /// <summary>Ruckus-SCG-CBlade-IP (Type 7). IP address. SmartCell Gateway control blade IP.</summary>
        SCG_CBLADE_IP = 7,

        /// <summary>Ruckus-SCG-DBlade-IP (Type 8). IP address. SmartCell Gateway data blade IP.</summary>
        SCG_DBLADE_IP = 8,

        /// <summary>Ruckus-VLAN-ID (Type 9). Integer. VLAN identifier to assign.</summary>
        VLAN_ID = 9,

        /// <summary>Ruckus-Sta-Expiration (Type 10). Integer. Station session expiration in seconds.</summary>
        STA_EXPIRATION = 10,

        /// <summary>Ruckus-Sta-UUID (Type 11). String. Station UUID.</summary>
        STA_UUID = 11,

        /// <summary>Ruckus-Accept-Enhancement-Reason (Type 12). Integer. Accept enhancement reason code.</summary>
        ACCEPT_ENHANCEMENT_REASON = 12,

        /// <summary>Ruckus-Sta-Inner-Id (Type 13). String. Station inner identity (EAP).</summary>
        STA_INNER_ID = 13,

        /// <summary>Ruckus-BSSID (Type 14). String. AP BSSID (MAC address).</summary>
        BSSID = 14,

        /// <summary>Ruckus-Sta-DPKey (Type 15). String. Station data plane key.</summary>
        STA_DPKEY = 15,

        /// <summary>Ruckus-Triplets (Type 16). Octets. Authentication triplets.</summary>
        TRIPLETS = 16,

        /// <summary>Ruckus-IMSI (Type 17). String. International Mobile Subscriber Identity.</summary>
        IMSI = 17,

        /// <summary>Ruckus-AVPair (Type 18). String. Attribute-value pair string.</summary>
        AVPAIR = 18,

        /// <summary>Ruckus-NAS-Id (Type 19). String. NAS identifier.</summary>
        NAS_ID = 19,

        /// <summary>Ruckus-AP-Name (Type 20). String. Access point name.</summary>
        AP_NAME = 20,

        /// <summary>Ruckus-Rate-Limit-UL (Type 21). Integer. Upstream rate limit in Kbps.</summary>
        RATE_LIMIT_UL = 21,

        /// <summary>Ruckus-Rate-Limit-DL (Type 22). Integer. Downstream rate limit in Kbps.</summary>
        RATE_LIMIT_DL = 22,

        /// <summary>Ruckus-ACL-Name (Type 23). String. ACL name to apply.</summary>
        ACL_NAME = 23,

        /// <summary>Ruckus-Role-Name (Type 24). String. User role name.</summary>
        ROLE_NAME = 24,

        /// <summary>Ruckus-DPSK (Type 25). String. Dynamic Pre-Shared Key.</summary>
        DPSK = 25,

        /// <summary>Ruckus-CP-Token (Type 26). String. Captive portal token.</summary>
        CP_TOKEN = 26,

        /// <summary>Ruckus-Redirect-URL (Type 27). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 27,

        /// <summary>Ruckus-Accounting-Service-Enabled (Type 28). Integer. Accounting service enabled flag.</summary>
        ACCOUNTING_SERVICE_ENABLED = 28,

        /// <summary>Ruckus-FlexAuth-Tag (Type 29). String. FlexAuth tag.</summary>
        FLEXAUTH_TAG = 29,

        /// <summary>Ruckus-AP-MAC (Type 30). String. Access point MAC address.</summary>
        AP_MAC = 30
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Ruckus Wireless
    /// (IANA PEN 25053) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.ruckus</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Ruckus' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 25053</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Ruckus Wireless (CommScope) SmartZone,
    /// ZoneDirector, and Ruckus Cloud managed access points and controllers for
    /// RADIUS-based user group assignment, VLAN assignment, upstream/downstream
    /// rate limiting, dynamic PSK (DPSK), ACL and role name enforcement, captive
    /// portal redirection and token, SSID/BSSID/AP name/AP MAC identification,
    /// station RSSI/UUID/inner identity/expiration tracking, SCG control/data
    /// blade IP addressing, grace period, IMSI, FlexAuth tagging, accounting
    /// service control, NAS identification, and general-purpose attribute-value
    /// pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(RuckusAttributes.UserGroups("employees"));
    /// packet.SetAttribute(RuckusAttributes.VlanId(100));
    /// packet.SetAttribute(RuckusAttributes.RateLimitUl(10000));
    /// packet.SetAttribute(RuckusAttributes.RateLimitDl(50000));
    /// packet.SetAttribute(RuckusAttributes.RoleName("corporate-user"));
    /// packet.SetAttribute(RuckusAttributes.Dpsk("MyDynamicPSK123"));
    /// </code>
    /// </remarks>
    public static class RuckusAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Ruckus Wireless.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 25053;

        #region Integer Attributes

        /// <summary>Creates a Ruckus-Sta-RSSI attribute (Type 2).</summary>
        /// <param name="value">The station RSSI value.</param>
        public static VendorSpecificAttributes StaRssi(int value) => CreateInteger(RuckusAttributeType.STA_RSSI, value);

        /// <summary>Creates a Ruckus-Wlan-Id attribute (Type 4).</summary>
        /// <param name="value">The WLAN identifier.</param>
        public static VendorSpecificAttributes WlanId(int value) => CreateInteger(RuckusAttributeType.WLAN_ID, value);

        /// <summary>Creates a Ruckus-Grace-Period attribute (Type 6).</summary>
        /// <param name="value">The grace period in seconds.</param>
        public static VendorSpecificAttributes GracePeriod(int value) => CreateInteger(RuckusAttributeType.GRACE_PERIOD, value);

        /// <summary>Creates a Ruckus-VLAN-ID attribute (Type 9).</summary>
        /// <param name="value">The VLAN identifier to assign.</param>
        public static VendorSpecificAttributes VlanId(int value) => CreateInteger(RuckusAttributeType.VLAN_ID, value);

        /// <summary>Creates a Ruckus-Sta-Expiration attribute (Type 10).</summary>
        /// <param name="value">The station session expiration in seconds.</param>
        public static VendorSpecificAttributes StaExpiration(int value) => CreateInteger(RuckusAttributeType.STA_EXPIRATION, value);

        /// <summary>Creates a Ruckus-Accept-Enhancement-Reason attribute (Type 12).</summary>
        /// <param name="value">The accept enhancement reason code.</param>
        public static VendorSpecificAttributes AcceptEnhancementReason(int value) => CreateInteger(RuckusAttributeType.ACCEPT_ENHANCEMENT_REASON, value);

        /// <summary>Creates a Ruckus-Rate-Limit-UL attribute (Type 21).</summary>
        /// <param name="value">The upstream rate limit in Kbps.</param>
        public static VendorSpecificAttributes RateLimitUl(int value) => CreateInteger(RuckusAttributeType.RATE_LIMIT_UL, value);

        /// <summary>Creates a Ruckus-Rate-Limit-DL attribute (Type 22).</summary>
        /// <param name="value">The downstream rate limit in Kbps.</param>
        public static VendorSpecificAttributes RateLimitDl(int value) => CreateInteger(RuckusAttributeType.RATE_LIMIT_DL, value);

        /// <summary>Creates a Ruckus-Accounting-Service-Enabled attribute (Type 28).</summary>
        /// <param name="value">The accounting service enabled flag (0 = disabled, 1 = enabled).</param>
        public static VendorSpecificAttributes AccountingServiceEnabled(int value) => CreateInteger(RuckusAttributeType.ACCOUNTING_SERVICE_ENABLED, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Ruckus-User-Groups attribute (Type 1).</summary>
        /// <param name="value">The user group name(s). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroups(string value) => CreateString(RuckusAttributeType.USER_GROUPS, value);

        /// <summary>Creates a Ruckus-SSID attribute (Type 3).</summary>
        /// <param name="value">The wireless SSID name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ssid(string value) => CreateString(RuckusAttributeType.SSID, value);

        /// <summary>Creates a Ruckus-Location attribute (Type 5).</summary>
        /// <param name="value">The AP location string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Location(string value) => CreateString(RuckusAttributeType.LOCATION, value);

        /// <summary>Creates a Ruckus-Sta-UUID attribute (Type 11).</summary>
        /// <param name="value">The station UUID. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes StaUuid(string value) => CreateString(RuckusAttributeType.STA_UUID, value);

        /// <summary>Creates a Ruckus-Sta-Inner-Id attribute (Type 13).</summary>
        /// <param name="value">The station inner identity (EAP). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes StaInnerId(string value) => CreateString(RuckusAttributeType.STA_INNER_ID, value);

        /// <summary>Creates a Ruckus-BSSID attribute (Type 14).</summary>
        /// <param name="value">The AP BSSID (MAC address). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Bssid(string value) => CreateString(RuckusAttributeType.BSSID, value);

        /// <summary>Creates a Ruckus-Sta-DPKey attribute (Type 15).</summary>
        /// <param name="value">The station data plane key. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes StaDpKey(string value) => CreateString(RuckusAttributeType.STA_DPKEY, value);

        /// <summary>Creates a Ruckus-IMSI attribute (Type 17).</summary>
        /// <param name="value">The International Mobile Subscriber Identity. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imsi(string value) => CreateString(RuckusAttributeType.IMSI, value);

        /// <summary>Creates a Ruckus-AVPair attribute (Type 18).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(RuckusAttributeType.AVPAIR, value);

        /// <summary>Creates a Ruckus-NAS-Id attribute (Type 19).</summary>
        /// <param name="value">The NAS identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NasId(string value) => CreateString(RuckusAttributeType.NAS_ID, value);

        /// <summary>Creates a Ruckus-AP-Name attribute (Type 20).</summary>
        /// <param name="value">The access point name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApName(string value) => CreateString(RuckusAttributeType.AP_NAME, value);

        /// <summary>Creates a Ruckus-ACL-Name attribute (Type 23).</summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value) => CreateString(RuckusAttributeType.ACL_NAME, value);

        /// <summary>Creates a Ruckus-Role-Name attribute (Type 24).</summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RoleName(string value) => CreateString(RuckusAttributeType.ROLE_NAME, value);

        /// <summary>Creates a Ruckus-DPSK attribute (Type 25).</summary>
        /// <param name="value">The Dynamic Pre-Shared Key. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Dpsk(string value) => CreateString(RuckusAttributeType.DPSK, value);

        /// <summary>Creates a Ruckus-CP-Token attribute (Type 26).</summary>
        /// <param name="value">The captive portal token. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CpToken(string value) => CreateString(RuckusAttributeType.CP_TOKEN, value);

        /// <summary>Creates a Ruckus-Redirect-URL attribute (Type 27).</summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value) => CreateString(RuckusAttributeType.REDIRECT_URL, value);

        /// <summary>Creates a Ruckus-FlexAuth-Tag attribute (Type 29).</summary>
        /// <param name="value">The FlexAuth tag. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FlexAuthTag(string value) => CreateString(RuckusAttributeType.FLEXAUTH_TAG, value);

        /// <summary>Creates a Ruckus-AP-MAC attribute (Type 30).</summary>
        /// <param name="value">The access point MAC address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApMac(string value) => CreateString(RuckusAttributeType.AP_MAC, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Ruckus-SCG-CBlade-IP attribute (Type 7).</summary>
        /// <param name="value">The SmartCell Gateway control blade IP. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ScgCBladeIp(IPAddress value) => CreateIpv4(RuckusAttributeType.SCG_CBLADE_IP, value);

        /// <summary>Creates a Ruckus-SCG-DBlade-IP attribute (Type 8).</summary>
        /// <param name="value">The SmartCell Gateway data blade IP. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ScgDBladeIp(IPAddress value) => CreateIpv4(RuckusAttributeType.SCG_DBLADE_IP, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates a Ruckus-Triplets attribute (Type 16).</summary>
        /// <param name="value">The authentication triplets data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Triplets(byte[] value) => CreateOctets(RuckusAttributeType.TRIPLETS, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(RuckusAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(RuckusAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(RuckusAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        private static VendorSpecificAttributes CreateIpv4(RuckusAttributeType type, IPAddress value)
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