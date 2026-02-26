using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Aptilo Networks (IANA PEN 13209) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.aptilo</c>.
    /// </summary>
    public enum AptiloAttributeType : byte
    {
        /// <summary>Aptilo-Access-Profile (Type 1). String. Access profile name to apply.</summary>
        ACCESS_PROFILE = 1,

        /// <summary>Aptilo-Session-Timeout-Bi (Type 2). Integer. Bidirectional session timeout in seconds.</summary>
        SESSION_TIMEOUT_BI = 2,

        /// <summary>Aptilo-Downstream-Bandwidth (Type 3). Integer. Downstream bandwidth limit in bps.</summary>
        DOWNSTREAM_BANDWIDTH = 3,

        /// <summary>Aptilo-Upstream-Bandwidth (Type 4). Integer. Upstream bandwidth limit in bps.</summary>
        UPSTREAM_BANDWIDTH = 4,

        /// <summary>Aptilo-QoS-Class (Type 5). String. Quality of Service class name.</summary>
        QOS_CLASS = 5,

        /// <summary>Aptilo-Max-Input-Octets (Type 6). Integer. Maximum input octets allowed.</summary>
        MAX_INPUT_OCTETS = 6,

        /// <summary>Aptilo-Max-Output-Octets (Type 7). Integer. Maximum output octets allowed.</summary>
        MAX_OUTPUT_OCTETS = 7,

        /// <summary>Aptilo-Max-Total-Octets (Type 8). Integer. Maximum total octets allowed.</summary>
        MAX_TOTAL_OCTETS = 8,

        /// <summary>Aptilo-Realm (Type 9). String. Authentication realm name.</summary>
        REALM = 9,

        /// <summary>Aptilo-Redirect-URL (Type 10). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 10,

        /// <summary>Aptilo-Service-Name (Type 11). String. Service name.</summary>
        SERVICE_NAME = 11,

        /// <summary>Aptilo-Subscriber-Password (Type 12). String. Subscriber password.</summary>
        SUBSCRIBER_PASSWORD = 12,

        /// <summary>Aptilo-Subscriber-Name (Type 13). String. Subscriber display name.</summary>
        SUBSCRIBER_NAME = 13,

        /// <summary>Aptilo-Service-Type (Type 14). Integer. Service type identifier.</summary>
        SERVICE_TYPE = 14,

        /// <summary>Aptilo-Subscriber-Id (Type 15). String. Subscriber unique identifier.</summary>
        SUBSCRIBER_ID = 15,

        /// <summary>Aptilo-Zone-Name (Type 16). String. Zone name for location-based policy.</summary>
        ZONE_NAME = 16,

        /// <summary>Aptilo-Venue-Name (Type 17). String. Venue name for Hotspot 2.0.</summary>
        VENUE_NAME = 17,

        /// <summary>Aptilo-Max-Input-Packets (Type 18). Integer. Maximum input packets allowed.</summary>
        MAX_INPUT_PACKETS = 18,

        /// <summary>Aptilo-Max-Output-Packets (Type 19). Integer. Maximum output packets allowed.</summary>
        MAX_OUTPUT_PACKETS = 19,

        /// <summary>Aptilo-Walled-Garden (Type 20). String. Walled garden URL list.</summary>
        WALLED_GARDEN = 20,

        /// <summary>Aptilo-Accounting-Group (Type 21). String. Accounting group name.</summary>
        ACCOUNTING_GROUP = 21,

        /// <summary>Aptilo-Service-Category (Type 22). String. Service category name.</summary>
        SERVICE_CATEGORY = 22,

        /// <summary>Aptilo-VLAN-Name (Type 23). String. VLAN name assignment.</summary>
        VLAN_NAME = 23,

        /// <summary>Aptilo-MSISDN (Type 24). String. Mobile Station ISDN number.</summary>
        MSISDN = 24,

        /// <summary>Aptilo-IMSI (Type 25). String. International Mobile Subscriber Identity.</summary>
        IMSI = 25
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Aptilo Networks
    /// (IANA PEN 13209) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.aptilo</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Aptilo's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 13209</c>.
    /// </para>
    /// <para>
    /// These attributes are used by the Aptilo Service Management Platform (SMP)
    /// for carrier Wi-Fi subscriber management, QoS enforcement, bandwidth control,
    /// volume quotas, captive portal redirection, walled garden configuration,
    /// and Hotspot 2.0 venue identification.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AptiloAttributes.AccessProfile("premium"));
    /// packet.SetAttribute(AptiloAttributes.DownstreamBandwidth(50000000));
    /// packet.SetAttribute(AptiloAttributes.UpstreamBandwidth(10000000));
    /// packet.SetAttribute(AptiloAttributes.RedirectUrl("https://portal.example.com/login"));
    /// </code>
    /// </remarks>
    public static class AptiloAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Aptilo Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 13209;

        #region Integer Attributes

        /// <summary>
        /// Creates an Aptilo-Session-Timeout-Bi attribute (Type 2) with the specified timeout.
        /// </summary>
        /// <param name="value">The bidirectional session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeoutBi(int value)
        {
            return CreateInteger(AptiloAttributeType.SESSION_TIMEOUT_BI, value);
        }

        /// <summary>
        /// Creates an Aptilo-Downstream-Bandwidth attribute (Type 3) with the specified bandwidth.
        /// </summary>
        /// <param name="value">The downstream bandwidth limit in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DownstreamBandwidth(int value)
        {
            return CreateInteger(AptiloAttributeType.DOWNSTREAM_BANDWIDTH, value);
        }

        /// <summary>
        /// Creates an Aptilo-Upstream-Bandwidth attribute (Type 4) with the specified bandwidth.
        /// </summary>
        /// <param name="value">The upstream bandwidth limit in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UpstreamBandwidth(int value)
        {
            return CreateInteger(AptiloAttributeType.UPSTREAM_BANDWIDTH, value);
        }

        /// <summary>
        /// Creates an Aptilo-Max-Input-Octets attribute (Type 6) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum input octets allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxInputOctets(int value)
        {
            return CreateInteger(AptiloAttributeType.MAX_INPUT_OCTETS, value);
        }

        /// <summary>
        /// Creates an Aptilo-Max-Output-Octets attribute (Type 7) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum output octets allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxOutputOctets(int value)
        {
            return CreateInteger(AptiloAttributeType.MAX_OUTPUT_OCTETS, value);
        }

        /// <summary>
        /// Creates an Aptilo-Max-Total-Octets attribute (Type 8) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum total octets allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxTotalOctets(int value)
        {
            return CreateInteger(AptiloAttributeType.MAX_TOTAL_OCTETS, value);
        }

        /// <summary>
        /// Creates an Aptilo-Service-Type attribute (Type 14) with the specified service type.
        /// </summary>
        /// <param name="value">The service type identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServiceType(int value)
        {
            return CreateInteger(AptiloAttributeType.SERVICE_TYPE, value);
        }

        /// <summary>
        /// Creates an Aptilo-Max-Input-Packets attribute (Type 18) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum input packets allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxInputPackets(int value)
        {
            return CreateInteger(AptiloAttributeType.MAX_INPUT_PACKETS, value);
        }

        /// <summary>
        /// Creates an Aptilo-Max-Output-Packets attribute (Type 19) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum output packets allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxOutputPackets(int value)
        {
            return CreateInteger(AptiloAttributeType.MAX_OUTPUT_PACKETS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Aptilo-Access-Profile attribute (Type 1) with the specified profile name.
        /// </summary>
        /// <param name="value">The access profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessProfile(string value)
        {
            return CreateString(AptiloAttributeType.ACCESS_PROFILE, value);
        }

        /// <summary>
        /// Creates an Aptilo-QoS-Class attribute (Type 5) with the specified class name.
        /// </summary>
        /// <param name="value">The QoS class name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosClass(string value)
        {
            return CreateString(AptiloAttributeType.QOS_CLASS, value);
        }

        /// <summary>
        /// Creates an Aptilo-Realm attribute (Type 9) with the specified realm name.
        /// </summary>
        /// <param name="value">The authentication realm name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Realm(string value)
        {
            return CreateString(AptiloAttributeType.REALM, value);
        }

        /// <summary>
        /// Creates an Aptilo-Redirect-URL attribute (Type 10) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(AptiloAttributeType.REDIRECT_URL, value);
        }

        /// <summary>
        /// Creates an Aptilo-Service-Name attribute (Type 11) with the specified service name.
        /// </summary>
        /// <param name="value">The service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceName(string value)
        {
            return CreateString(AptiloAttributeType.SERVICE_NAME, value);
        }

        /// <summary>
        /// Creates an Aptilo-Subscriber-Password attribute (Type 12) with the specified password.
        /// </summary>
        /// <param name="value">The subscriber password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberPassword(string value)
        {
            return CreateString(AptiloAttributeType.SUBSCRIBER_PASSWORD, value);
        }

        /// <summary>
        /// Creates an Aptilo-Subscriber-Name attribute (Type 13) with the specified display name.
        /// </summary>
        /// <param name="value">The subscriber display name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberName(string value)
        {
            return CreateString(AptiloAttributeType.SUBSCRIBER_NAME, value);
        }

        /// <summary>
        /// Creates an Aptilo-Subscriber-Id attribute (Type 15) with the specified identifier.
        /// </summary>
        /// <param name="value">The subscriber unique identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberId(string value)
        {
            return CreateString(AptiloAttributeType.SUBSCRIBER_ID, value);
        }

        /// <summary>
        /// Creates an Aptilo-Zone-Name attribute (Type 16) with the specified zone name.
        /// </summary>
        /// <param name="value">The zone name for location-based policy. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ZoneName(string value)
        {
            return CreateString(AptiloAttributeType.ZONE_NAME, value);
        }

        /// <summary>
        /// Creates an Aptilo-Venue-Name attribute (Type 17) with the specified venue name.
        /// </summary>
        /// <param name="value">The venue name for Hotspot 2.0. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VenueName(string value)
        {
            return CreateString(AptiloAttributeType.VENUE_NAME, value);
        }

        /// <summary>
        /// Creates an Aptilo-Walled-Garden attribute (Type 20) with the specified URL list.
        /// </summary>
        /// <param name="value">The walled garden URL list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WalledGarden(string value)
        {
            return CreateString(AptiloAttributeType.WALLED_GARDEN, value);
        }

        /// <summary>
        /// Creates an Aptilo-Accounting-Group attribute (Type 21) with the specified group name.
        /// </summary>
        /// <param name="value">The accounting group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccountingGroup(string value)
        {
            return CreateString(AptiloAttributeType.ACCOUNTING_GROUP, value);
        }

        /// <summary>
        /// Creates an Aptilo-Service-Category attribute (Type 22) with the specified category name.
        /// </summary>
        /// <param name="value">The service category name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceCategory(string value)
        {
            return CreateString(AptiloAttributeType.SERVICE_CATEGORY, value);
        }

        /// <summary>
        /// Creates an Aptilo-VLAN-Name attribute (Type 23) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name assignment. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(AptiloAttributeType.VLAN_NAME, value);
        }

        /// <summary>
        /// Creates an Aptilo-MSISDN attribute (Type 24) with the specified MSISDN.
        /// </summary>
        /// <param name="value">The Mobile Station ISDN number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Msisdn(string value)
        {
            return CreateString(AptiloAttributeType.MSISDN, value);
        }

        /// <summary>
        /// Creates an Aptilo-IMSI attribute (Type 25) with the specified IMSI.
        /// </summary>
        /// <param name="value">The International Mobile Subscriber Identity. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Imsi(string value)
        {
            return CreateString(AptiloAttributeType.IMSI, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Aptilo attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AptiloAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Aptilo attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AptiloAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}