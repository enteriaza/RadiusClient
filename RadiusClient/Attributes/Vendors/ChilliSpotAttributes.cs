using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a ChilliSpot / CoovaChilli (IANA PEN 14559) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.chillispot</c>.
    /// </summary>
    /// <remarks>
    /// ChilliSpot shares IANA PEN 14559 with Boingo in some dictionary sources.
    /// The FreeRADIUS <c>dictionary.chillispot</c> uses vendor ID 14559.
    /// </remarks>
    public enum ChilliSpotAttributeType : byte
    {
        /// <summary>ChilliSpot-Max-Input-Octets (Type 1). Integer. Maximum input octets allowed.</summary>
        MAX_INPUT_OCTETS = 1,

        /// <summary>ChilliSpot-Max-Output-Octets (Type 2). Integer. Maximum output octets allowed.</summary>
        MAX_OUTPUT_OCTETS = 2,

        /// <summary>ChilliSpot-Max-Total-Octets (Type 3). Integer. Maximum total octets allowed.</summary>
        MAX_TOTAL_OCTETS = 3,

        /// <summary>ChilliSpot-Bandwidth-Max-Up (Type 4). Integer. Maximum upstream bandwidth in bps.</summary>
        BANDWIDTH_MAX_UP = 4,

        /// <summary>ChilliSpot-Bandwidth-Max-Down (Type 5). Integer. Maximum downstream bandwidth in bps.</summary>
        BANDWIDTH_MAX_DOWN = 5,

        /// <summary>ChilliSpot-Config (Type 6). String. Configuration string.</summary>
        CONFIG = 6,

        /// <summary>ChilliSpot-Lang (Type 7). String. Language code.</summary>
        LANG = 7,

        /// <summary>ChilliSpot-Version (Type 8). String. ChilliSpot version string.</summary>
        VERSION = 8,

        /// <summary>ChilliSpot-OriginalURL (Type 9). String. Original URL before redirect.</summary>
        ORIGINAL_URL = 9,

        /// <summary>ChilliSpot-Acct-View-Point (Type 10). Integer. Accounting view point.</summary>
        ACCT_VIEW_POINT = 10,

        /// <summary>ChilliSpot-DHCP-Vendor-Class-Id (Type 11). String. DHCP vendor class identifier.</summary>
        DHCP_VENDOR_CLASS_ID = 11,

        /// <summary>ChilliSpot-DHCP-Client-Id (Type 12). Octets. DHCP client identifier.</summary>
        DHCP_CLIENT_ID = 12,

        /// <summary>ChilliSpot-DHCP-Options (Type 13). Octets. DHCP options data.</summary>
        DHCP_OPTIONS = 13,

        /// <summary>ChilliSpot-DHCP-Filename (Type 14). String. DHCP boot filename.</summary>
        DHCP_FILENAME = 14,

        /// <summary>ChilliSpot-DHCP-Hostname (Type 15). String. DHCP hostname.</summary>
        DHCP_HOSTNAME = 15,

        /// <summary>ChilliSpot-DHCP-Server-Name (Type 16). String. DHCP server name.</summary>
        DHCP_SERVER_NAME = 16,

        /// <summary>ChilliSpot-DHCP-Client-FQDN (Type 17). String. DHCP client FQDN.</summary>
        DHCP_CLIENT_FQDN = 17,

        /// <summary>ChilliSpot-DHCP-Parameter-Request-List (Type 18). Octets. DHCP parameter request list.</summary>
        DHCP_PARAMETER_REQUEST_LIST = 18
    }

    /// <summary>
    /// ChilliSpot-Acct-View-Point attribute values (Type 10).
    /// </summary>
    public enum CHILLISPOT_ACCT_VIEW_POINT
    {
        /// <summary>Accounting from the client (user) perspective.</summary>
        CLIENT = 1,

        /// <summary>Accounting from the NAS (access controller) perspective.</summary>
        NAS = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing ChilliSpot / CoovaChilli
    /// (IANA PEN 14559) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.chillispot</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ChilliSpot's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14559</c>.
    /// </para>
    /// <para>
    /// These attributes are used by ChilliSpot and CoovaChilli captive portal /
    /// access controller software for bandwidth limiting, data volume caps,
    /// original URL capture, DHCP relay information, language selection, and
    /// accounting view point control.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ChilliSpotAttributes.BandwidthMaxUp(1000000));
    /// packet.SetAttribute(ChilliSpotAttributes.BandwidthMaxDown(5000000));
    /// packet.SetAttribute(ChilliSpotAttributes.MaxTotalOctets(1073741824));
    /// packet.SetAttribute(ChilliSpotAttributes.Config("uamallowed=example.com"));
    /// </code>
    /// </remarks>
    public static class ChilliSpotAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for ChilliSpot / CoovaChilli.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14559;

        #region Integer Attributes

        /// <summary>
        /// Creates a ChilliSpot-Max-Input-Octets attribute (Type 1) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum input octets allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxInputOctets(int value)
        {
            return CreateInteger(ChilliSpotAttributeType.MAX_INPUT_OCTETS, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-Max-Output-Octets attribute (Type 2) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum output octets allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxOutputOctets(int value)
        {
            return CreateInteger(ChilliSpotAttributeType.MAX_OUTPUT_OCTETS, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-Max-Total-Octets attribute (Type 3) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum total octets allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxTotalOctets(int value)
        {
            return CreateInteger(ChilliSpotAttributeType.MAX_TOTAL_OCTETS, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-Bandwidth-Max-Up attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(ChilliSpotAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-Bandwidth-Max-Down attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(ChilliSpotAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-Acct-View-Point attribute (Type 10) with the specified view point.
        /// </summary>
        /// <param name="value">The accounting view point. See <see cref="CHILLISPOT_ACCT_VIEW_POINT"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctViewPoint(CHILLISPOT_ACCT_VIEW_POINT value)
        {
            return CreateInteger(ChilliSpotAttributeType.ACCT_VIEW_POINT, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a ChilliSpot-Config attribute (Type 6) with the specified configuration.
        /// </summary>
        /// <param name="value">The configuration string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Config(string value)
        {
            return CreateString(ChilliSpotAttributeType.CONFIG, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-Lang attribute (Type 7) with the specified language code.
        /// </summary>
        /// <param name="value">The language code. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Lang(string value)
        {
            return CreateString(ChilliSpotAttributeType.LANG, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-Version attribute (Type 8) with the specified version.
        /// </summary>
        /// <param name="value">The ChilliSpot version string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Version(string value)
        {
            return CreateString(ChilliSpotAttributeType.VERSION, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-OriginalURL attribute (Type 9) with the specified URL.
        /// </summary>
        /// <param name="value">The original URL before redirect. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OriginalUrl(string value)
        {
            return CreateString(ChilliSpotAttributeType.ORIGINAL_URL, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-DHCP-Vendor-Class-Id attribute (Type 11) with the specified class ID.
        /// </summary>
        /// <param name="value">The DHCP vendor class identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpVendorClassId(string value)
        {
            return CreateString(ChilliSpotAttributeType.DHCP_VENDOR_CLASS_ID, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-DHCP-Filename attribute (Type 14) with the specified filename.
        /// </summary>
        /// <param name="value">The DHCP boot filename. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpFilename(string value)
        {
            return CreateString(ChilliSpotAttributeType.DHCP_FILENAME, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-DHCP-Hostname attribute (Type 15) with the specified hostname.
        /// </summary>
        /// <param name="value">The DHCP hostname. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpHostname(string value)
        {
            return CreateString(ChilliSpotAttributeType.DHCP_HOSTNAME, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-DHCP-Server-Name attribute (Type 16) with the specified server name.
        /// </summary>
        /// <param name="value">The DHCP server name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpServerName(string value)
        {
            return CreateString(ChilliSpotAttributeType.DHCP_SERVER_NAME, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-DHCP-Client-FQDN attribute (Type 17) with the specified FQDN.
        /// </summary>
        /// <param name="value">The DHCP client FQDN. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpClientFqdn(string value)
        {
            return CreateString(ChilliSpotAttributeType.DHCP_CLIENT_FQDN, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates a ChilliSpot-DHCP-Client-Id attribute (Type 12) with the specified data.
        /// </summary>
        /// <param name="value">The DHCP client identifier data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpClientId(byte[] value)
        {
            return CreateOctets(ChilliSpotAttributeType.DHCP_CLIENT_ID, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-DHCP-Options attribute (Type 13) with the specified data.
        /// </summary>
        /// <param name="value">The DHCP options data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpOptions(byte[] value)
        {
            return CreateOctets(ChilliSpotAttributeType.DHCP_OPTIONS, value);
        }

        /// <summary>
        /// Creates a ChilliSpot-DHCP-Parameter-Request-List attribute (Type 18) with the specified data.
        /// </summary>
        /// <param name="value">The DHCP parameter request list data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpParameterRequestList(byte[] value)
        {
            return CreateOctets(ChilliSpotAttributeType.DHCP_PARAMETER_REQUEST_LIST, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(ChilliSpotAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(ChilliSpotAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(ChilliSpotAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        #endregion
    }
}