using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Airespace / Cisco WLC (IANA PEN 14179) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.airespace</c>.
    /// </summary>
    public enum AirespaceAttributeType : byte
    {
        /// <summary>Airespace-Wlan-Id (Type 1). Integer. WLAN identifier.</summary>
        WLAN_ID = 1,

        /// <summary>Airespace-QOS-Level (Type 2). Integer. Quality of service level.</summary>
        QOS_LEVEL = 2,

        /// <summary>Airespace-DSCP (Type 3). Integer. DiffServ Code Point value.</summary>
        DSCP = 3,

        /// <summary>Airespace-802.1p-Tag (Type 4). Integer. IEEE 802.1p priority tag.</summary>
        DOT1P_TAG = 4,

        /// <summary>Airespace-Interface-Name (Type 5). String. Dynamic interface name.</summary>
        INTERFACE_NAME = 5,

        /// <summary>Airespace-ACL-Name (Type 6). String. Access control list name.</summary>
        ACL_NAME = 6,

        /// <summary>Airespace-Data-Bandwidth-Average-Contract (Type 7). Integer. Average data bandwidth contract in kbps.</summary>
        DATA_BANDWIDTH_AVERAGE_CONTRACT = 7,

        /// <summary>Airespace-Real-Time-Bandwidth-Average-Contract (Type 8). Integer. Average real-time bandwidth contract in kbps.</summary>
        REAL_TIME_BANDWIDTH_AVERAGE_CONTRACT = 8,

        /// <summary>Airespace-Data-Bandwidth-Burst-Contract (Type 9). Integer. Burst data bandwidth contract in kbps.</summary>
        DATA_BANDWIDTH_BURST_CONTRACT = 9,

        /// <summary>Airespace-Real-Time-Bandwidth-Burst-Contract (Type 10). Integer. Burst real-time bandwidth contract in kbps.</summary>
        REAL_TIME_BANDWIDTH_BURST_CONTRACT = 10,

        /// <summary>Airespace-Guest-Role-Name (Type 11). String. Guest user role name.</summary>
        GUEST_ROLE_NAME = 11,

        /// <summary>Airespace-Data-Bandwidth-Average-Contract-Upstream (Type 13). Integer. Average upstream data bandwidth in kbps.</summary>
        DATA_BANDWIDTH_AVERAGE_CONTRACT_UPSTREAM = 13,

        /// <summary>Airespace-Real-Time-Bandwidth-Average-Contract-Upstream (Type 14). Integer. Average upstream real-time bandwidth in kbps.</summary>
        REAL_TIME_BANDWIDTH_AVERAGE_CONTRACT_UPSTREAM = 14,

        /// <summary>Airespace-Data-Bandwidth-Burst-Contract-Upstream (Type 15). Integer. Burst upstream data bandwidth in kbps.</summary>
        DATA_BANDWIDTH_BURST_CONTRACT_UPSTREAM = 15,

        /// <summary>Airespace-Real-Time-Bandwidth-Burst-Contract-Upstream (Type 16). Integer. Burst upstream real-time bandwidth in kbps.</summary>
        REAL_TIME_BANDWIDTH_BURST_CONTRACT_UPSTREAM = 16
    }

    /// <summary>
    /// Airespace-QOS-Level attribute values (Type 2).
    /// </summary>
    public enum AIRESPACE_QOS_LEVEL
    {
        /// <summary>Silver QoS level (best effort).</summary>
        SILVER = 0,

        /// <summary>Gold QoS level (video).</summary>
        GOLD = 1,

        /// <summary>Platinum QoS level (voice).</summary>
        PLATINUM = 2,

        /// <summary>Bronze QoS level (background).</summary>
        BRONZE = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Airespace / Cisco WLC
    /// (IANA PEN 14179) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.airespace</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Airespace's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14179</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Cisco Wireless LAN Controllers (WLCs), formerly
    /// Airespace, for WLAN selection, QoS policy enforcement, bandwidth contracts,
    /// ACL assignment, dynamic interface mapping, and guest role assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AirespaceAttributes.WlanId(5));
    /// packet.SetAttribute(AirespaceAttributes.QosLevel(AIRESPACE_QOS_LEVEL.PLATINUM));
    /// packet.SetAttribute(AirespaceAttributes.InterfaceName("vlan100"));
    /// packet.SetAttribute(AirespaceAttributes.AclName("GUEST-ACL"));
    /// </code>
    /// </remarks>
    public static class AirespaceAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Airespace (Cisco WLC).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14179;

        #region Integer Attributes

        /// <summary>
        /// Creates an Airespace-Wlan-Id attribute (Type 1) with the specified WLAN identifier.
        /// </summary>
        /// <param name="value">The WLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WlanId(int value)
        {
            return CreateInteger(AirespaceAttributeType.WLAN_ID, value);
        }

        /// <summary>
        /// Creates an Airespace-QOS-Level attribute (Type 2) with the specified QoS level.
        /// </summary>
        /// <param name="value">The quality of service level. See <see cref="AIRESPACE_QOS_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes QosLevel(AIRESPACE_QOS_LEVEL value)
        {
            return CreateInteger(AirespaceAttributeType.QOS_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates an Airespace-DSCP attribute (Type 3) with the specified DSCP value.
        /// </summary>
        /// <param name="value">The DiffServ Code Point value (0–63).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Dscp(int value)
        {
            return CreateInteger(AirespaceAttributeType.DSCP, value);
        }

        /// <summary>
        /// Creates an Airespace-802.1p-Tag attribute (Type 4) with the specified priority tag.
        /// </summary>
        /// <param name="value">The IEEE 802.1p priority tag value (0–7).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Dot1pTag(int value)
        {
            return CreateInteger(AirespaceAttributeType.DOT1P_TAG, value);
        }

        /// <summary>
        /// Creates an Airespace-Data-Bandwidth-Average-Contract attribute (Type 7)
        /// with the specified average bandwidth.
        /// </summary>
        /// <param name="value">The average data bandwidth contract in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DataBandwidthAverageContract(int value)
        {
            return CreateInteger(AirespaceAttributeType.DATA_BANDWIDTH_AVERAGE_CONTRACT, value);
        }

        /// <summary>
        /// Creates an Airespace-Real-Time-Bandwidth-Average-Contract attribute (Type 8)
        /// with the specified average bandwidth.
        /// </summary>
        /// <param name="value">The average real-time bandwidth contract in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RealTimeBandwidthAverageContract(int value)
        {
            return CreateInteger(AirespaceAttributeType.REAL_TIME_BANDWIDTH_AVERAGE_CONTRACT, value);
        }

        /// <summary>
        /// Creates an Airespace-Data-Bandwidth-Burst-Contract attribute (Type 9)
        /// with the specified burst bandwidth.
        /// </summary>
        /// <param name="value">The burst data bandwidth contract in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DataBandwidthBurstContract(int value)
        {
            return CreateInteger(AirespaceAttributeType.DATA_BANDWIDTH_BURST_CONTRACT, value);
        }

        /// <summary>
        /// Creates an Airespace-Real-Time-Bandwidth-Burst-Contract attribute (Type 10)
        /// with the specified burst bandwidth.
        /// </summary>
        /// <param name="value">The burst real-time bandwidth contract in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RealTimeBandwidthBurstContract(int value)
        {
            return CreateInteger(AirespaceAttributeType.REAL_TIME_BANDWIDTH_BURST_CONTRACT, value);
        }

        /// <summary>
        /// Creates an Airespace-Data-Bandwidth-Average-Contract-Upstream attribute (Type 13)
        /// with the specified average upstream bandwidth.
        /// </summary>
        /// <param name="value">The average upstream data bandwidth in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DataBandwidthAverageContractUpstream(int value)
        {
            return CreateInteger(AirespaceAttributeType.DATA_BANDWIDTH_AVERAGE_CONTRACT_UPSTREAM, value);
        }

        /// <summary>
        /// Creates an Airespace-Real-Time-Bandwidth-Average-Contract-Upstream attribute (Type 14)
        /// with the specified average upstream bandwidth.
        /// </summary>
        /// <param name="value">The average upstream real-time bandwidth in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RealTimeBandwidthAverageContractUpstream(int value)
        {
            return CreateInteger(AirespaceAttributeType.REAL_TIME_BANDWIDTH_AVERAGE_CONTRACT_UPSTREAM, value);
        }

        /// <summary>
        /// Creates an Airespace-Data-Bandwidth-Burst-Contract-Upstream attribute (Type 15)
        /// with the specified burst upstream bandwidth.
        /// </summary>
        /// <param name="value">The burst upstream data bandwidth in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DataBandwidthBurstContractUpstream(int value)
        {
            return CreateInteger(AirespaceAttributeType.DATA_BANDWIDTH_BURST_CONTRACT_UPSTREAM, value);
        }

        /// <summary>
        /// Creates an Airespace-Real-Time-Bandwidth-Burst-Contract-Upstream attribute (Type 16)
        /// with the specified burst upstream bandwidth.
        /// </summary>
        /// <param name="value">The burst upstream real-time bandwidth in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RealTimeBandwidthBurstContractUpstream(int value)
        {
            return CreateInteger(AirespaceAttributeType.REAL_TIME_BANDWIDTH_BURST_CONTRACT_UPSTREAM, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Airespace-Interface-Name attribute (Type 5) with the specified interface name.
        /// </summary>
        /// <param name="value">The dynamic interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes InterfaceName(string value)
        {
            return CreateString(AirespaceAttributeType.INTERFACE_NAME, value);
        }

        /// <summary>
        /// Creates an Airespace-ACL-Name attribute (Type 6) with the specified ACL name.
        /// </summary>
        /// <param name="value">The access control list name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value)
        {
            return CreateString(AirespaceAttributeType.ACL_NAME, value);
        }

        /// <summary>
        /// Creates an Airespace-Guest-Role-Name attribute (Type 11) with the specified role name.
        /// </summary>
        /// <param name="value">The guest user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GuestRoleName(string value)
        {
            return CreateString(AirespaceAttributeType.GUEST_ROLE_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Airespace attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AirespaceAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Airespace attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AirespaceAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}