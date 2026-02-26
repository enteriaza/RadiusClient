using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Cisco Meraki (IANA PEN 29671) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.meraki</c>.
    /// </summary>
    /// <remarks>
    /// Cisco Meraki is a cloud-managed networking company (acquired by Cisco)
    /// producing wireless access points (MR), switches (MS), security appliances
    /// (MX), and cameras (MV), all centrally managed via the Meraki Dashboard.
    /// </remarks>
    public enum MerakiAttributeType : byte
    {
        /// <summary>Meraki-Device-Name (Type 1). String. Meraki device name.</summary>
        DEVICE_NAME = 1,

        /// <summary>Meraki-Network-Name (Type 2). String. Meraki network name.</summary>
        NETWORK_NAME = 2,

        /// <summary>Meraki-AP-Name (Type 3). String. Access point name.</summary>
        AP_NAME = 3,

        /// <summary>Meraki-AP-Tags (Type 4). String. Access point tags.</summary>
        AP_TAGS = 4,

        /// <summary>Meraki-VLAN-Tag (Type 5). Integer. VLAN tag to assign.</summary>
        VLAN_TAG = 5,

        /// <summary>Meraki-Group-Policy-Id (Type 6). Integer. Group policy identifier.</summary>
        GROUP_POLICY_ID = 6,

        /// <summary>Meraki-Device-Serial (Type 7). String. Meraki device serial number.</summary>
        DEVICE_SERIAL = 7,

        /// <summary>Meraki-SSID-Name (Type 8). String. Wireless SSID name.</summary>
        SSID_NAME = 8,

        /// <summary>Meraki-SSID-Number (Type 9). Integer. Wireless SSID number.</summary>
        SSID_NUMBER = 9,

        /// <summary>Meraki-Bandwidth-Up (Type 10). Integer. Upstream bandwidth limit in Kbps.</summary>
        BANDWIDTH_UP = 10,

        /// <summary>Meraki-Bandwidth-Down (Type 11). Integer. Downstream bandwidth limit in Kbps.</summary>
        BANDWIDTH_DOWN = 11,

        /// <summary>Meraki-Session-Duration (Type 12). Integer. Session duration limit in seconds.</summary>
        SESSION_DURATION = 12,

        /// <summary>Meraki-Reply-Message (Type 13). String. Reply message string.</summary>
        REPLY_MESSAGE = 13
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Cisco Meraki
    /// (IANA PEN 29671) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.meraki</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Meraki's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 29671</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Cisco Meraki cloud-managed wireless access
    /// points (MR series), switches (MS series), and security appliances (MX series)
    /// for RADIUS-based device and network identification, AP name and tag reporting,
    /// VLAN assignment, group policy enforcement, device serial identification,
    /// wireless SSID identification (by name and number), upstream/downstream
    /// bandwidth provisioning, session duration limits, and reply messaging.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(MerakiAttributes.VlanTag(100));
    /// packet.SetAttribute(MerakiAttributes.GroupPolicyId(200));
    /// packet.SetAttribute(MerakiAttributes.BandwidthUp(10000));
    /// packet.SetAttribute(MerakiAttributes.BandwidthDown(50000));
    /// packet.SetAttribute(MerakiAttributes.SessionDuration(3600));
    /// </code>
    /// </remarks>
    public static class MerakiAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Cisco Meraki.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 29671;

        #region Integer Attributes

        /// <summary>
        /// Creates a Meraki-VLAN-Tag attribute (Type 5) with the specified VLAN tag.
        /// </summary>
        /// <param name="value">The VLAN tag to assign.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanTag(int value)
        {
            return CreateInteger(MerakiAttributeType.VLAN_TAG, value);
        }

        /// <summary>
        /// Creates a Meraki-Group-Policy-Id attribute (Type 6) with the specified group policy.
        /// </summary>
        /// <param name="value">The group policy identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes GroupPolicyId(int value)
        {
            return CreateInteger(MerakiAttributeType.GROUP_POLICY_ID, value);
        }

        /// <summary>
        /// Creates a Meraki-SSID-Number attribute (Type 9) with the specified SSID number.
        /// </summary>
        /// <param name="value">The wireless SSID number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SsidNumber(int value)
        {
            return CreateInteger(MerakiAttributeType.SSID_NUMBER, value);
        }

        /// <summary>
        /// Creates a Meraki-Bandwidth-Up attribute (Type 10) with the specified rate.
        /// </summary>
        /// <param name="value">The upstream bandwidth limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthUp(int value)
        {
            return CreateInteger(MerakiAttributeType.BANDWIDTH_UP, value);
        }

        /// <summary>
        /// Creates a Meraki-Bandwidth-Down attribute (Type 11) with the specified rate.
        /// </summary>
        /// <param name="value">The downstream bandwidth limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthDown(int value)
        {
            return CreateInteger(MerakiAttributeType.BANDWIDTH_DOWN, value);
        }

        /// <summary>
        /// Creates a Meraki-Session-Duration attribute (Type 12) with the specified duration.
        /// </summary>
        /// <param name="value">The session duration limit in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionDuration(int value)
        {
            return CreateInteger(MerakiAttributeType.SESSION_DURATION, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Meraki-Device-Name attribute (Type 1) with the specified device name.
        /// </summary>
        /// <param name="value">The Meraki device name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DeviceName(string value)
        {
            return CreateString(MerakiAttributeType.DEVICE_NAME, value);
        }

        /// <summary>
        /// Creates a Meraki-Network-Name attribute (Type 2) with the specified network name.
        /// </summary>
        /// <param name="value">The Meraki network name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NetworkName(string value)
        {
            return CreateString(MerakiAttributeType.NETWORK_NAME, value);
        }

        /// <summary>
        /// Creates a Meraki-AP-Name attribute (Type 3) with the specified access point name.
        /// </summary>
        /// <param name="value">The access point name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApName(string value)
        {
            return CreateString(MerakiAttributeType.AP_NAME, value);
        }

        /// <summary>
        /// Creates a Meraki-AP-Tags attribute (Type 4) with the specified tags.
        /// </summary>
        /// <param name="value">The access point tags. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApTags(string value)
        {
            return CreateString(MerakiAttributeType.AP_TAGS, value);
        }

        /// <summary>
        /// Creates a Meraki-Device-Serial attribute (Type 7) with the specified serial number.
        /// </summary>
        /// <param name="value">The Meraki device serial number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DeviceSerial(string value)
        {
            return CreateString(MerakiAttributeType.DEVICE_SERIAL, value);
        }

        /// <summary>
        /// Creates a Meraki-SSID-Name attribute (Type 8) with the specified SSID name.
        /// </summary>
        /// <param name="value">The wireless SSID name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SsidName(string value)
        {
            return CreateString(MerakiAttributeType.SSID_NAME, value);
        }

        /// <summary>
        /// Creates a Meraki-Reply-Message attribute (Type 13) with the specified message.
        /// </summary>
        /// <param name="value">The reply message string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ReplyMessage(string value)
        {
            return CreateString(MerakiAttributeType.REPLY_MESSAGE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Meraki attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(MerakiAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Meraki attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(MerakiAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}