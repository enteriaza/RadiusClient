using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Alcatel ESAM (IANA PEN 637) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.alcatel.esam</c>.
    /// </summary>
    public enum AlcatelEsamAttributeType : byte
    {
        /// <summary>Alcatel-ESAM-Auth-Type (Type 1). Integer. Authentication type.</summary>
        AUTH_TYPE = 1,

        /// <summary>Alcatel-ESAM-Traffic-Policy (Type 2). String. Traffic policy name.</summary>
        TRAFFIC_POLICY = 2,

        /// <summary>Alcatel-ESAM-Access-Control (Type 3). Integer. Access control level.</summary>
        ACCESS_CONTROL = 3,

        /// <summary>Alcatel-ESAM-Service-Name (Type 4). String. Service name.</summary>
        SERVICE_NAME = 4,

        /// <summary>Alcatel-ESAM-Bandwidth-Policy (Type 5). String. Bandwidth policy name.</summary>
        BANDWIDTH_POLICY = 5,

        /// <summary>Alcatel-ESAM-Subscriber-Name (Type 6). String. Subscriber name.</summary>
        SUBSCRIBER_NAME = 6,

        /// <summary>Alcatel-ESAM-Subscriber-Id (Type 7). String. Subscriber identifier.</summary>
        SUBSCRIBER_ID = 7,

        /// <summary>Alcatel-ESAM-VLAN-Id (Type 8). Integer. VLAN identifier.</summary>
        VLAN_ID = 8,

        /// <summary>Alcatel-ESAM-Service-Level (Type 9). Integer. Service level.</summary>
        SERVICE_LEVEL = 9,

        /// <summary>Alcatel-ESAM-QoS-Policy (Type 10). String. QoS policy name.</summary>
        QOS_POLICY = 10,

        /// <summary>Alcatel-ESAM-Redirect-URL (Type 11). String. Redirect URL for captive portal.</summary>
        REDIRECT_URL = 11,

        /// <summary>Alcatel-ESAM-Max-Upload-Rate (Type 12). Integer. Maximum upload rate in kbps.</summary>
        MAX_UPLOAD_RATE = 12,

        /// <summary>Alcatel-ESAM-Max-Download-Rate (Type 13). Integer. Maximum download rate in kbps.</summary>
        MAX_DOWNLOAD_RATE = 13,

        /// <summary>Alcatel-ESAM-Filter-Id (Type 14). String. Filter identifier.</summary>
        FILTER_ID = 14,

        /// <summary>Alcatel-ESAM-Acct-Policy (Type 15). String. Accounting policy name.</summary>
        ACCT_POLICY = 15,

        /// <summary>Alcatel-ESAM-Multicast-Policy (Type 16). String. Multicast policy name.</summary>
        MULTICAST_POLICY = 16,

        /// <summary>Alcatel-ESAM-Ingress-Policy (Type 17). String. Ingress policy name.</summary>
        INGRESS_POLICY = 17,

        /// <summary>Alcatel-ESAM-Egress-Policy (Type 18). String. Egress policy name.</summary>
        EGRESS_POLICY = 18
    }

    /// <summary>
    /// Alcatel-ESAM-Auth-Type attribute values (Type 1).
    /// </summary>
    public enum ALCATEL_ESAM_AUTH_TYPE
    {
        /// <summary>No authentication.</summary>
        NONE = 0,

        /// <summary>PAP authentication.</summary>
        PAP = 1,

        /// <summary>CHAP authentication.</summary>
        CHAP = 2,

        /// <summary>MS-CHAP authentication.</summary>
        MS_CHAP = 3,

        /// <summary>MS-CHAPv2 authentication.</summary>
        MS_CHAP_V2 = 4
    }

    /// <summary>
    /// Alcatel-ESAM-Access-Control attribute values (Type 3).
    /// </summary>
    public enum ALCATEL_ESAM_ACCESS_CONTROL
    {
        /// <summary>No access control applied.</summary>
        NONE = 0,

        /// <summary>Read-only access.</summary>
        READ_ONLY = 1,

        /// <summary>Read-write access.</summary>
        READ_WRITE = 2,

        /// <summary>Administrator access (full control).</summary>
        ADMIN = 3
    }

    /// <summary>
    /// Alcatel-ESAM-Service-Level attribute values (Type 9).
    /// </summary>
    public enum ALCATEL_ESAM_SERVICE_LEVEL
    {
        /// <summary>Basic service level.</summary>
        BASIC = 0,

        /// <summary>Premium service level.</summary>
        PREMIUM = 1,

        /// <summary>Gold service level.</summary>
        GOLD = 2,

        /// <summary>Platinum service level.</summary>
        PLATINUM = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Alcatel ESAM
    /// (IANA PEN 637) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.alcatel.esam</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Alcatel ESAM's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 637</c>.
    /// </para>
    /// <para>
    /// These attributes are used by the Alcatel Extended Subscriber Access Manager (E-SAM)
    /// platform for subscriber management, traffic and bandwidth policy assignment,
    /// QoS control, VLAN configuration, and captive portal redirection.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AlcatelEsamAttributes.ServiceName("Internet-Premium"));
    /// packet.SetAttribute(AlcatelEsamAttributes.ServiceLevel(ALCATEL_ESAM_SERVICE_LEVEL.GOLD));
    /// packet.SetAttribute(AlcatelEsamAttributes.MaxDownloadRate(50000));
    /// packet.SetAttribute(AlcatelEsamAttributes.VlanId(200));
    /// </code>
    /// </remarks>
    public static class AlcatelEsamAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Alcatel ESAM.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 637;

        #region Integer Attributes

        /// <summary>
        /// Creates an Alcatel-ESAM-Auth-Type attribute (Type 1) with the specified authentication type.
        /// </summary>
        /// <param name="value">The authentication type. See <see cref="ALCATEL_ESAM_AUTH_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthType(ALCATEL_ESAM_AUTH_TYPE value)
        {
            return CreateInteger(AlcatelEsamAttributeType.AUTH_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Access-Control attribute (Type 3) with the specified access control level.
        /// </summary>
        /// <param name="value">The access control level. See <see cref="ALCATEL_ESAM_ACCESS_CONTROL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessControl(ALCATEL_ESAM_ACCESS_CONTROL value)
        {
            return CreateInteger(AlcatelEsamAttributeType.ACCESS_CONTROL, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-VLAN-Id attribute (Type 8) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(AlcatelEsamAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Service-Level attribute (Type 9) with the specified service level.
        /// </summary>
        /// <param name="value">The service level. See <see cref="ALCATEL_ESAM_SERVICE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServiceLevel(ALCATEL_ESAM_SERVICE_LEVEL value)
        {
            return CreateInteger(AlcatelEsamAttributeType.SERVICE_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Max-Upload-Rate attribute (Type 12) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upload rate in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxUploadRate(int value)
        {
            return CreateInteger(AlcatelEsamAttributeType.MAX_UPLOAD_RATE, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Max-Download-Rate attribute (Type 13) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum download rate in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxDownloadRate(int value)
        {
            return CreateInteger(AlcatelEsamAttributeType.MAX_DOWNLOAD_RATE, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Alcatel-ESAM-Traffic-Policy attribute (Type 2) with the specified policy name.
        /// </summary>
        /// <param name="value">The traffic policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TrafficPolicy(string value)
        {
            return CreateString(AlcatelEsamAttributeType.TRAFFIC_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Service-Name attribute (Type 4) with the specified service name.
        /// </summary>
        /// <param name="value">The service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceName(string value)
        {
            return CreateString(AlcatelEsamAttributeType.SERVICE_NAME, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Bandwidth-Policy attribute (Type 5) with the specified policy name.
        /// </summary>
        /// <param name="value">The bandwidth policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BandwidthPolicy(string value)
        {
            return CreateString(AlcatelEsamAttributeType.BANDWIDTH_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Subscriber-Name attribute (Type 6) with the specified subscriber name.
        /// </summary>
        /// <param name="value">The subscriber name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberName(string value)
        {
            return CreateString(AlcatelEsamAttributeType.SUBSCRIBER_NAME, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Subscriber-Id attribute (Type 7) with the specified subscriber identifier.
        /// </summary>
        /// <param name="value">The subscriber identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberId(string value)
        {
            return CreateString(AlcatelEsamAttributeType.SUBSCRIBER_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-QoS-Policy attribute (Type 10) with the specified QoS policy name.
        /// </summary>
        /// <param name="value">The QoS policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosPolicy(string value)
        {
            return CreateString(AlcatelEsamAttributeType.QOS_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Redirect-URL attribute (Type 11) with the specified URL.
        /// </summary>
        /// <param name="value">The redirect URL for captive portal. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(AlcatelEsamAttributeType.REDIRECT_URL, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Filter-Id attribute (Type 14) with the specified filter identifier.
        /// </summary>
        /// <param name="value">The filter identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FilterId(string value)
        {
            return CreateString(AlcatelEsamAttributeType.FILTER_ID, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Acct-Policy attribute (Type 15) with the specified accounting policy name.
        /// </summary>
        /// <param name="value">The accounting policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctPolicy(string value)
        {
            return CreateString(AlcatelEsamAttributeType.ACCT_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Multicast-Policy attribute (Type 16) with the specified multicast policy name.
        /// </summary>
        /// <param name="value">The multicast policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MulticastPolicy(string value)
        {
            return CreateString(AlcatelEsamAttributeType.MULTICAST_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Ingress-Policy attribute (Type 17) with the specified ingress policy name.
        /// </summary>
        /// <param name="value">The ingress policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IngressPolicy(string value)
        {
            return CreateString(AlcatelEsamAttributeType.INGRESS_POLICY, value);
        }

        /// <summary>
        /// Creates an Alcatel-ESAM-Egress-Policy attribute (Type 18) with the specified egress policy name.
        /// </summary>
        /// <param name="value">The egress policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EgressPolicy(string value)
        {
            return CreateString(AlcatelEsamAttributeType.EGRESS_POLICY, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Alcatel ESAM attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AlcatelEsamAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Alcatel ESAM attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AlcatelEsamAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}