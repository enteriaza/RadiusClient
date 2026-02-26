using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a WISPr (Wireless ISP roaming, IANA PEN 14122) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.wispr</c>.
    /// </summary>
    /// <remarks>
    /// WISPr (Wireless Internet Service Provider roaming) is a Wi-Fi Alliance
    /// specification that enables seamless roaming between wireless ISPs via
    /// a common captive portal and RADIUS-based authentication framework.
    /// WISPr attributes are widely used by hotspot operators, Wi-Fi roaming
    /// aggregators (e.g. Boingo, iPass), and eduroam-adjacent deployments.
    /// </remarks>
    public enum WisprAttributeType : byte
    {
        /// <summary>WISPr-Location-Id (Type 1). String. Hierarchical location identifier (ISO country code + region + city + zone).</summary>
        LOCATION_ID = 1,

        /// <summary>WISPr-Location-Name (Type 2). String. Human-readable location name.</summary>
        LOCATION_NAME = 2,

        /// <summary>WISPr-Logoff-URL (Type 3). String. URL for session logoff.</summary>
        LOGOFF_URL = 3,

        /// <summary>WISPr-Redirection-URL (Type 4). String. Captive portal redirection URL.</summary>
        REDIRECTION_URL = 4,

        /// <summary>WISPr-Bandwidth-Min-Up (Type 5). Integer. Minimum upstream bandwidth in bps.</summary>
        BANDWIDTH_MIN_UP = 5,

        /// <summary>WISPr-Bandwidth-Min-Down (Type 6). Integer. Minimum downstream bandwidth in bps.</summary>
        BANDWIDTH_MIN_DOWN = 6,

        /// <summary>WISPr-Bandwidth-Max-Up (Type 7). Integer. Maximum upstream bandwidth in bps.</summary>
        BANDWIDTH_MAX_UP = 7,

        /// <summary>WISPr-Bandwidth-Max-Down (Type 8). Integer. Maximum downstream bandwidth in bps.</summary>
        BANDWIDTH_MAX_DOWN = 8,

        /// <summary>WISPr-Session-Terminate-Time (Type 9). String. Absolute session termination time (ISO 8601).</summary>
        SESSION_TERMINATE_TIME = 9,

        /// <summary>WISPr-Session-Terminate-End-Of-Day (Type 10). String. End-of-day session termination flag.</summary>
        SESSION_TERMINATE_END_OF_DAY = 10,

        /// <summary>WISPr-Billing-Class-Of-Service (Type 11). String. Billing class of service identifier.</summary>
        BILLING_CLASS_OF_SERVICE = 11
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing WISPr
    /// (IANA PEN 14122) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.wispr</c> and the Wi-Fi Alliance WISPr specification.
    /// </summary>
    /// <remarks>
    /// <para>
    /// WISPr's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14122</c>.
    /// </para>
    /// <para>
    /// These attributes are used by wireless ISPs, hotspot operators, and Wi-Fi
    /// roaming aggregators for RADIUS-based hierarchical location identification,
    /// human-readable location naming, captive portal redirection and logoff URL
    /// provisioning, minimum and maximum upstream/downstream bandwidth control,
    /// absolute session termination time scheduling (ISO 8601), end-of-day
    /// session termination, and billing class of service assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(WisprAttributes.LocationId("isocc=US,cc=001,ac=12345,network=ExampleWiFi"));
    /// packet.SetAttribute(WisprAttributes.LocationName("Example Coffee Shop - Downtown"));
    /// packet.SetAttribute(WisprAttributes.BandwidthMaxUp(5000000));
    /// packet.SetAttribute(WisprAttributes.BandwidthMaxDown(25000000));
    /// packet.SetAttribute(WisprAttributes.RedirectionUrl("https://portal.example.com/login"));
    /// packet.SetAttribute(WisprAttributes.SessionTerminateTime("2026-02-26T23:59:59Z"));
    /// </code>
    /// </remarks>
    public static class WisprAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for WISPr (Wi-Fi Alliance).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14122;

        #region Integer Attributes

        /// <summary>
        /// Creates a WISPr-Bandwidth-Min-Up attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The minimum upstream bandwidth in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMinUp(int value)
        {
            return CreateInteger(WisprAttributeType.BANDWIDTH_MIN_UP, value);
        }

        /// <summary>
        /// Creates a WISPr-Bandwidth-Min-Down attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The minimum downstream bandwidth in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMinDown(int value)
        {
            return CreateInteger(WisprAttributeType.BANDWIDTH_MIN_DOWN, value);
        }

        /// <summary>
        /// Creates a WISPr-Bandwidth-Max-Up attribute (Type 7) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(WisprAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a WISPr-Bandwidth-Max-Down attribute (Type 8) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(WisprAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a WISPr-Location-Id attribute (Type 1) with the specified location identifier.
        /// </summary>
        /// <param name="value">The hierarchical location identifier (e.g. "isocc=US,cc=001,ac=12345,network=MyWiFi"). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LocationId(string value)
        {
            return CreateString(WisprAttributeType.LOCATION_ID, value);
        }

        /// <summary>
        /// Creates a WISPr-Location-Name attribute (Type 2) with the specified location name.
        /// </summary>
        /// <param name="value">The human-readable location name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LocationName(string value)
        {
            return CreateString(WisprAttributeType.LOCATION_NAME, value);
        }

        /// <summary>
        /// Creates a WISPr-Logoff-URL attribute (Type 3) with the specified URL.
        /// </summary>
        /// <param name="value">The URL for session logoff. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LogoffUrl(string value)
        {
            return CreateString(WisprAttributeType.LOGOFF_URL, value);
        }

        /// <summary>
        /// Creates a WISPr-Redirection-URL attribute (Type 4) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirection URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectionUrl(string value)
        {
            return CreateString(WisprAttributeType.REDIRECTION_URL, value);
        }

        /// <summary>
        /// Creates a WISPr-Session-Terminate-Time attribute (Type 9) with the specified time.
        /// </summary>
        /// <param name="value">The absolute session termination time in ISO 8601 format (e.g. "2026-02-26T23:59:59Z"). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionTerminateTime(string value)
        {
            return CreateString(WisprAttributeType.SESSION_TERMINATE_TIME, value);
        }

        /// <summary>
        /// Creates a WISPr-Session-Terminate-End-Of-Day attribute (Type 10) with the specified flag.
        /// </summary>
        /// <param name="value">The end-of-day session termination flag. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionTerminateEndOfDay(string value)
        {
            return CreateString(WisprAttributeType.SESSION_TERMINATE_END_OF_DAY, value);
        }

        /// <summary>
        /// Creates a WISPr-Billing-Class-Of-Service attribute (Type 11) with the specified class.
        /// </summary>
        /// <param name="value">The billing class of service identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BillingClassOfService(string value)
        {
            return CreateString(WisprAttributeType.BILLING_CLASS_OF_SERVICE, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(WisprAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(WisprAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}