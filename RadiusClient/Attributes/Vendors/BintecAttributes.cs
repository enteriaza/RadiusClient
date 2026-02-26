using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a BinTec Communications (IANA PEN 272) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.bintec</c>.
    /// </summary>
    public enum BintecAttributeType : byte
    {
        /// <summary>BinTec-biboPPPTable (Type 224). String. PPP table configuration.</summary>
        BIBO_PPP_TABLE = 224,

        /// <summary>BinTec-biboDialTable (Type 225). String. Dial table configuration.</summary>
        BIBO_DIAL_TABLE = 225,

        /// <summary>BinTec-bipboBRTable (Type 226). String. Bandwidth reservation table configuration.</summary>
        BIPBO_BR_TABLE = 226,

        /// <summary>BinTec-biboIPRouteTable (Type 227). String. IP route table configuration.</summary>
        BIBO_IP_ROUTE_TABLE = 227,

        /// <summary>BinTec-biboIPExtIfTable (Type 228). String. IP extended interface table configuration.</summary>
        BIBO_IP_EXT_IF_TABLE = 228,

        /// <summary>BinTec-biboIPNATPresetTable (Type 229). String. IP NAT preset table configuration.</summary>
        BIBO_IP_NAT_PRESET_TABLE = 229,

        /// <summary>BinTec-biboIPXRouteTable (Type 230). String. IPX route table configuration.</summary>
        BIBO_IPX_ROUTE_TABLE = 230,

        /// <summary>BinTec-biboIPXSAPTable (Type 231). String. IPX SAP table configuration.</summary>
        BIBO_IPX_SAP_TABLE = 231,

        /// <summary>BinTec-biboBRITable (Type 232). String. BRI (Basic Rate Interface) table configuration.</summary>
        BIBO_BRI_TABLE = 232,

        /// <summary>BinTec-biboPRITable (Type 233). String. PRI (Primary Rate Interface) table configuration.</summary>
        BIBO_PRI_TABLE = 233,

        /// <summary>BinTec-biboIPFilterTable (Type 234). String. IP filter table configuration.</summary>
        BIBO_IP_FILTER_TABLE = 234,

        /// <summary>BinTec-biboIPXFilterTable (Type 235). String. IPX filter table configuration.</summary>
        BIBO_IPX_FILTER_TABLE = 235,

        /// <summary>BinTec-biboQoSPolicyTable (Type 236). String. QoS policy table configuration.</summary>
        BIBO_QOS_POLICY_TABLE = 236,

        /// <summary>BinTec-biboQoSIfTable (Type 237). String. QoS interface table configuration.</summary>
        BIBO_QOS_IF_TABLE = 237,

        /// <summary>BinTec-biboIPNATCfgTable (Type 238). String. IP NAT configuration table.</summary>
        BIBO_IP_NAT_CFG_TABLE = 238,

        /// <summary>BinTec-biboAdmLoginTable (Type 239). String. Admin login table configuration.</summary>
        BIBO_ADM_LOGIN_TABLE = 239,

        /// <summary>BinTec-biboAdmRadiusTable (Type 240). String. Admin RADIUS table configuration.</summary>
        BIBO_ADM_RADIUS_TABLE = 240,

        /// <summary>BinTec-biboSyslogServerTable (Type 241). String. Syslog server table configuration.</summary>
        BIBO_SYSLOG_SERVER_TABLE = 241,

        /// <summary>BinTec-biboSyslogHostTable (Type 242). String. Syslog host table configuration.</summary>
        BIBO_SYSLOG_HOST_TABLE = 242,

        /// <summary>BinTec-biboVPNPeerTable (Type 243). String. VPN peer table configuration.</summary>
        BIBO_VPN_PEER_TABLE = 243,

        /// <summary>BinTec-biboIPStaticRouteTable (Type 244). String. IP static route table configuration.</summary>
        BIBO_IP_STATIC_ROUTE_TABLE = 244
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing BinTec Communications
    /// (IANA PEN 272) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.bintec</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// BinTec's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 272</c>.
    /// </para>
    /// <para>
    /// These attributes are used by BinTec (now Teldat/Funkwerk) ISDN/DSL routers
    /// and access gateways for RADIUS-based configuration provisioning of PPP,
    /// dial, bandwidth reservation, IP/IPX routing, NAT, BRI/PRI interfaces,
    /// IP/IPX filtering, QoS policies, admin login, syslog, VPN peers, and
    /// static routes.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(BintecAttributes.BiboPppTable("ppp-config-string"));
    /// packet.SetAttribute(BintecAttributes.BiboIpRouteTable("route-config-string"));
    /// </code>
    /// </remarks>
    public static class BintecAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for BinTec Communications.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 272;

        #region String Attributes

        /// <summary>
        /// Creates a BinTec-biboPPPTable attribute (Type 224) with the specified configuration.
        /// </summary>
        /// <param name="value">The PPP table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboPppTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_PPP_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboDialTable attribute (Type 225) with the specified configuration.
        /// </summary>
        /// <param name="value">The dial table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboDialTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_DIAL_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-bipboBRTable attribute (Type 226) with the specified configuration.
        /// </summary>
        /// <param name="value">The bandwidth reservation table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BipboBrTable(string value)
        {
            return CreateString(BintecAttributeType.BIPBO_BR_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboIPRouteTable attribute (Type 227) with the specified configuration.
        /// </summary>
        /// <param name="value">The IP route table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboIpRouteTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_IP_ROUTE_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboIPExtIfTable attribute (Type 228) with the specified configuration.
        /// </summary>
        /// <param name="value">The IP extended interface table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboIpExtIfTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_IP_EXT_IF_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboIPNATPresetTable attribute (Type 229) with the specified configuration.
        /// </summary>
        /// <param name="value">The IP NAT preset table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboIpNatPresetTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_IP_NAT_PRESET_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboIPXRouteTable attribute (Type 230) with the specified configuration.
        /// </summary>
        /// <param name="value">The IPX route table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboIpxRouteTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_IPX_ROUTE_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboIPXSAPTable attribute (Type 231) with the specified configuration.
        /// </summary>
        /// <param name="value">The IPX SAP table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboIpxSapTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_IPX_SAP_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboBRITable attribute (Type 232) with the specified configuration.
        /// </summary>
        /// <param name="value">The BRI table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboBriTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_BRI_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboPRITable attribute (Type 233) with the specified configuration.
        /// </summary>
        /// <param name="value">The PRI table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboPriTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_PRI_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboIPFilterTable attribute (Type 234) with the specified configuration.
        /// </summary>
        /// <param name="value">The IP filter table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboIpFilterTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_IP_FILTER_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboIPXFilterTable attribute (Type 235) with the specified configuration.
        /// </summary>
        /// <param name="value">The IPX filter table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboIpxFilterTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_IPX_FILTER_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboQoSPolicyTable attribute (Type 236) with the specified configuration.
        /// </summary>
        /// <param name="value">The QoS policy table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboQosPolicyTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_QOS_POLICY_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboQoSIfTable attribute (Type 237) with the specified configuration.
        /// </summary>
        /// <param name="value">The QoS interface table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboQosIfTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_QOS_IF_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboIPNATCfgTable attribute (Type 238) with the specified configuration.
        /// </summary>
        /// <param name="value">The IP NAT configuration table. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboIpNatCfgTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_IP_NAT_CFG_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboAdmLoginTable attribute (Type 239) with the specified configuration.
        /// </summary>
        /// <param name="value">The admin login table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboAdmLoginTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_ADM_LOGIN_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboAdmRadiusTable attribute (Type 240) with the specified configuration.
        /// </summary>
        /// <param name="value">The admin RADIUS table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboAdmRadiusTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_ADM_RADIUS_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboSyslogServerTable attribute (Type 241) with the specified configuration.
        /// </summary>
        /// <param name="value">The syslog server table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboSyslogServerTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_SYSLOG_SERVER_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboSyslogHostTable attribute (Type 242) with the specified configuration.
        /// </summary>
        /// <param name="value">The syslog host table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboSyslogHostTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_SYSLOG_HOST_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboVPNPeerTable attribute (Type 243) with the specified configuration.
        /// </summary>
        /// <param name="value">The VPN peer table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboVpnPeerTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_VPN_PEER_TABLE, value);
        }

        /// <summary>
        /// Creates a BinTec-biboIPStaticRouteTable attribute (Type 244) with the specified configuration.
        /// </summary>
        /// <param name="value">The IP static route table configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BiboIpStaticRouteTable(string value)
        {
            return CreateString(BintecAttributeType.BIBO_IP_STATIC_ROUTE_TABLE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified BinTec attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(BintecAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}