using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a LANCOM Systems (IANA PEN 2356) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.lancom</c>.
    /// </summary>
    /// <remarks>
    /// LANCOM Systems is a German manufacturer of networking equipment including
    /// VPN routers, managed switches, wireless LAN controllers, and access points.
    /// </remarks>
    public enum LancomAttributeType : byte
    {
        /// <summary>LANCOM-LTA-Key (Type 1). String. LTA license key.</summary>
        LTA_KEY = 1,

        /// <summary>LANCOM-Context-Name (Type 2). String. Routing context name.</summary>
        CONTEXT_NAME = 2,

        /// <summary>LANCOM-Access-Rights (Type 3). Integer. Administrative access rights.</summary>
        ACCESS_RIGHTS = 3,

        /// <summary>LANCOM-VPN-Profile (Type 4). String. VPN profile name.</summary>
        VPN_PROFILE = 4,

        /// <summary>LANCOM-DNS-Server (Type 5). String. DNS server address.</summary>
        DNS_SERVER = 5,

        /// <summary>LANCOM-NBNS-Server (Type 6). String. NBNS/WINS server address.</summary>
        NBNS_SERVER = 6,

        /// <summary>LANCOM-Routing-Tag (Type 7). Integer. Routing tag.</summary>
        ROUTING_TAG = 7,

        /// <summary>LANCOM-Firewall-Profile (Type 8). String. Firewall profile name.</summary>
        FIREWALL_PROFILE = 8,

        /// <summary>LANCOM-Comment (Type 9). String. Comment string.</summary>
        COMMENT = 9,

        /// <summary>LANCOM-Network-Name (Type 10). String. Network name.</summary>
        NETWORK_NAME = 10,

        /// <summary>LANCOM-VLAN-Id (Type 11). Integer. VLAN identifier.</summary>
        VLAN_ID = 11,

        /// <summary>LANCOM-Bandwidth-Up (Type 12). Integer. Upstream bandwidth limit in Kbps.</summary>
        BANDWIDTH_UP = 12,

        /// <summary>LANCOM-Bandwidth-Down (Type 13). Integer. Downstream bandwidth limit in Kbps.</summary>
        BANDWIDTH_DOWN = 13,

        /// <summary>LANCOM-Redirect-URL (Type 14). String. Redirect URL for captive portal.</summary>
        REDIRECT_URL = 14,

        /// <summary>LANCOM-Public-Spot-Profile (Type 15). String. Public Spot profile name.</summary>
        PUBLIC_SPOT_PROFILE = 15,

        /// <summary>LANCOM-MAC-Address (Type 16). String. MAC address string.</summary>
        MAC_ADDRESS = 16,

        /// <summary>LANCOM-Traffic-Limit (Type 17). Integer. Traffic limit in MB.</summary>
        TRAFFIC_LIMIT = 17,

        /// <summary>LANCOM-Time-Budget (Type 18). Integer. Time budget in seconds.</summary>
        TIME_BUDGET = 18,

        /// <summary>LANCOM-Volume-Budget (Type 19). Integer. Volume budget in MB.</summary>
        VOLUME_BUDGET = 19,

        /// <summary>LANCOM-Accounting-Period (Type 20). Integer. Accounting period in seconds.</summary>
        ACCOUNTING_PERIOD = 20
    }

    /// <summary>
    /// LANCOM-Access-Rights attribute values (Type 3).
    /// </summary>
    public enum LANCOM_ACCESS_RIGHTS
    {
        /// <summary>No administrative access.</summary>
        NONE = 0,

        /// <summary>Read-only administrative access.</summary>
        READ_ONLY = 1,

        /// <summary>Limited read-write access.</summary>
        LIMITED_ADMIN = 2,

        /// <summary>Full administrative access.</summary>
        ADMIN = 3,

        /// <summary>Super administrator access.</summary>
        SUPER_ADMIN = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing LANCOM Systems
    /// (IANA PEN 2356) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.lancom</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// LANCOM's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2356</c>.
    /// </para>
    /// <para>
    /// These attributes are used by LANCOM Systems routers, switches, wireless
    /// controllers, and access points for RADIUS-based LTA licensing, routing
    /// context and tag assignment, administrative access rights, VPN and firewall
    /// profile selection, DNS/NBNS server provisioning, network naming, VLAN
    /// assignment, upstream/downstream bandwidth provisioning, captive portal
    /// (Public Spot) configuration including redirect URL and profile, MAC
    /// address identification, traffic/volume/time budget enforcement, and
    /// accounting period configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(LancomAttributes.AccessRights(LANCOM_ACCESS_RIGHTS.ADMIN));
    /// packet.SetAttribute(LancomAttributes.VlanId(100));
    /// packet.SetAttribute(LancomAttributes.BandwidthUp(10000));
    /// packet.SetAttribute(LancomAttributes.BandwidthDown(50000));
    /// packet.SetAttribute(LancomAttributes.PublicSpotProfile("guest-hotspot"));
    /// packet.SetAttribute(LancomAttributes.TimeBudget(3600));
    /// </code>
    /// </remarks>
    public static class LancomAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for LANCOM Systems.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2356;

        #region Integer Attributes

        /// <summary>
        /// Creates a LANCOM-Access-Rights attribute (Type 3) with the specified rights.
        /// </summary>
        /// <param name="value">The administrative access rights. See <see cref="LANCOM_ACCESS_RIGHTS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessRights(LANCOM_ACCESS_RIGHTS value)
        {
            return CreateInteger(LancomAttributeType.ACCESS_RIGHTS, (int)value);
        }

        /// <summary>
        /// Creates a LANCOM-Routing-Tag attribute (Type 7) with the specified tag.
        /// </summary>
        /// <param name="value">The routing tag.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RoutingTag(int value)
        {
            return CreateInteger(LancomAttributeType.ROUTING_TAG, value);
        }

        /// <summary>
        /// Creates a LANCOM-VLAN-Id attribute (Type 11) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(LancomAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a LANCOM-Bandwidth-Up attribute (Type 12) with the specified rate.
        /// </summary>
        /// <param name="value">The upstream bandwidth limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthUp(int value)
        {
            return CreateInteger(LancomAttributeType.BANDWIDTH_UP, value);
        }

        /// <summary>
        /// Creates a LANCOM-Bandwidth-Down attribute (Type 13) with the specified rate.
        /// </summary>
        /// <param name="value">The downstream bandwidth limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthDown(int value)
        {
            return CreateInteger(LancomAttributeType.BANDWIDTH_DOWN, value);
        }

        /// <summary>
        /// Creates a LANCOM-Traffic-Limit attribute (Type 17) with the specified limit.
        /// </summary>
        /// <param name="value">The traffic limit in MB.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TrafficLimit(int value)
        {
            return CreateInteger(LancomAttributeType.TRAFFIC_LIMIT, value);
        }

        /// <summary>
        /// Creates a LANCOM-Time-Budget attribute (Type 18) with the specified budget.
        /// </summary>
        /// <param name="value">The time budget in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TimeBudget(int value)
        {
            return CreateInteger(LancomAttributeType.TIME_BUDGET, value);
        }

        /// <summary>
        /// Creates a LANCOM-Volume-Budget attribute (Type 19) with the specified budget.
        /// </summary>
        /// <param name="value">The volume budget in MB.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VolumeBudget(int value)
        {
            return CreateInteger(LancomAttributeType.VOLUME_BUDGET, value);
        }

        /// <summary>
        /// Creates a LANCOM-Accounting-Period attribute (Type 20) with the specified period.
        /// </summary>
        /// <param name="value">The accounting period in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccountingPeriod(int value)
        {
            return CreateInteger(LancomAttributeType.ACCOUNTING_PERIOD, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a LANCOM-LTA-Key attribute (Type 1) with the specified license key.
        /// </summary>
        /// <param name="value">The LTA license key. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LtaKey(string value)
        {
            return CreateString(LancomAttributeType.LTA_KEY, value);
        }

        /// <summary>
        /// Creates a LANCOM-Context-Name attribute (Type 2) with the specified context name.
        /// </summary>
        /// <param name="value">The routing context name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ContextName(string value)
        {
            return CreateString(LancomAttributeType.CONTEXT_NAME, value);
        }

        /// <summary>
        /// Creates a LANCOM-VPN-Profile attribute (Type 4) with the specified profile name.
        /// </summary>
        /// <param name="value">The VPN profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnProfile(string value)
        {
            return CreateString(LancomAttributeType.VPN_PROFILE, value);
        }

        /// <summary>
        /// Creates a LANCOM-DNS-Server attribute (Type 5) with the specified address.
        /// </summary>
        /// <param name="value">The DNS server address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DnsServer(string value)
        {
            return CreateString(LancomAttributeType.DNS_SERVER, value);
        }

        /// <summary>
        /// Creates a LANCOM-NBNS-Server attribute (Type 6) with the specified address.
        /// </summary>
        /// <param name="value">The NBNS/WINS server address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NbnsServer(string value)
        {
            return CreateString(LancomAttributeType.NBNS_SERVER, value);
        }

        /// <summary>
        /// Creates a LANCOM-Firewall-Profile attribute (Type 8) with the specified profile name.
        /// </summary>
        /// <param name="value">The firewall profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FirewallProfile(string value)
        {
            return CreateString(LancomAttributeType.FIREWALL_PROFILE, value);
        }

        /// <summary>
        /// Creates a LANCOM-Comment attribute (Type 9) with the specified comment.
        /// </summary>
        /// <param name="value">The comment string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Comment(string value)
        {
            return CreateString(LancomAttributeType.COMMENT, value);
        }

        /// <summary>
        /// Creates a LANCOM-Network-Name attribute (Type 10) with the specified network name.
        /// </summary>
        /// <param name="value">The network name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NetworkName(string value)
        {
            return CreateString(LancomAttributeType.NETWORK_NAME, value);
        }

        /// <summary>
        /// Creates a LANCOM-Redirect-URL attribute (Type 14) with the specified URL.
        /// </summary>
        /// <param name="value">The redirect URL for captive portal. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(LancomAttributeType.REDIRECT_URL, value);
        }

        /// <summary>
        /// Creates a LANCOM-Public-Spot-Profile attribute (Type 15) with the specified profile name.
        /// </summary>
        /// <param name="value">The Public Spot profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PublicSpotProfile(string value)
        {
            return CreateString(LancomAttributeType.PUBLIC_SPOT_PROFILE, value);
        }

        /// <summary>
        /// Creates a LANCOM-MAC-Address attribute (Type 16) with the specified MAC address.
        /// </summary>
        /// <param name="value">The MAC address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MacAddress(string value)
        {
            return CreateString(LancomAttributeType.MAC_ADDRESS, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified LANCOM attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(LancomAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified LANCOM attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(LancomAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}