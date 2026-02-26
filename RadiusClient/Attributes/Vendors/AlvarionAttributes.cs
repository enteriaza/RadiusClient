using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Alvarion (IANA PEN 12394) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.alvarion</c>.
    /// </summary>
    public enum AlvarionAttributeType : byte
    {
        /// <summary>Alvarion-VSA-1 (Type 1). String. Default VLAN or general-purpose string.</summary>
        VSA_1 = 1,

        /// <summary>Alvarion-Default-VLAN (Type 2). Integer. Default VLAN identifier.</summary>
        DEFAULT_VLAN = 2,

        /// <summary>Alvarion-BRAS-URL (Type 3). String. BRAS redirect URL.</summary>
        BRAS_URL = 3,

        /// <summary>Alvarion-VSA-4 (Type 4). Integer. General-purpose integer attribute.</summary>
        VSA_4 = 4,

        /// <summary>Alvarion-Max-Downstream-Rate (Type 5). Integer. Maximum downstream rate in kbps.</summary>
        MAX_DOWNSTREAM_RATE = 5,

        /// <summary>Alvarion-Max-Upstream-Rate (Type 6). Integer. Maximum upstream rate in kbps.</summary>
        MAX_UPSTREAM_RATE = 6,

        /// <summary>Alvarion-QoS-Policy (Type 7). String. QoS policy name.</summary>
        QOS_POLICY = 7,

        /// <summary>Alvarion-VLAN-Tag (Type 8). Integer. VLAN tag to assign.</summary>
        VLAN_TAG = 8,

        /// <summary>Alvarion-Time-Of-Day (Type 9). String. Time-of-day access restriction.</summary>
        TIME_OF_DAY = 9,

        /// <summary>Alvarion-NAT-IP-Address (Type 10). String. NAT IP address string.</summary>
        NAT_IP_ADDRESS = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Alvarion
    /// (IANA PEN 12394) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.alvarion</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Alvarion's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 12394</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Alvarion (Wavion) wireless broadband equipment
    /// for VLAN assignment, rate limiting, QoS policy, BRAS redirection, NAT
    /// configuration, and time-of-day access control.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AlvarionAttributes.DefaultVlan(100));
    /// packet.SetAttribute(AlvarionAttributes.MaxDownstreamRate(10000));
    /// packet.SetAttribute(AlvarionAttributes.MaxUpstreamRate(5000));
    /// packet.SetAttribute(AlvarionAttributes.QosPolicy("gold"));
    /// </code>
    /// </remarks>
    public static class AlvarionAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Alvarion.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 12394;

        #region Integer Attributes

        /// <summary>
        /// Creates an Alvarion-Default-VLAN attribute (Type 2) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The default VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DefaultVlan(int value)
        {
            return CreateInteger(AlvarionAttributeType.DEFAULT_VLAN, value);
        }

        /// <summary>
        /// Creates an Alvarion-VSA-4 attribute (Type 4) with the specified integer value.
        /// </summary>
        /// <param name="value">The general-purpose integer value.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Vsa4(int value)
        {
            return CreateInteger(AlvarionAttributeType.VSA_4, value);
        }

        /// <summary>
        /// Creates an Alvarion-Max-Downstream-Rate attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream rate in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxDownstreamRate(int value)
        {
            return CreateInteger(AlvarionAttributeType.MAX_DOWNSTREAM_RATE, value);
        }

        /// <summary>
        /// Creates an Alvarion-Max-Upstream-Rate attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream rate in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxUpstreamRate(int value)
        {
            return CreateInteger(AlvarionAttributeType.MAX_UPSTREAM_RATE, value);
        }

        /// <summary>
        /// Creates an Alvarion-VLAN-Tag attribute (Type 8) with the specified VLAN tag.
        /// </summary>
        /// <param name="value">The VLAN tag to assign.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanTag(int value)
        {
            return CreateInteger(AlvarionAttributeType.VLAN_TAG, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Alvarion-VSA-1 attribute (Type 1) with the specified string value.
        /// </summary>
        /// <param name="value">The general-purpose string value. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Vsa1(string value)
        {
            return CreateString(AlvarionAttributeType.VSA_1, value);
        }

        /// <summary>
        /// Creates an Alvarion-BRAS-URL attribute (Type 3) with the specified redirect URL.
        /// </summary>
        /// <param name="value">The BRAS redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BrasUrl(string value)
        {
            return CreateString(AlvarionAttributeType.BRAS_URL, value);
        }

        /// <summary>
        /// Creates an Alvarion-QoS-Policy attribute (Type 7) with the specified policy name.
        /// </summary>
        /// <param name="value">The QoS policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosPolicy(string value)
        {
            return CreateString(AlvarionAttributeType.QOS_POLICY, value);
        }

        /// <summary>
        /// Creates an Alvarion-Time-Of-Day attribute (Type 9) with the specified restriction.
        /// </summary>
        /// <param name="value">The time-of-day access restriction. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TimeOfDay(string value)
        {
            return CreateString(AlvarionAttributeType.TIME_OF_DAY, value);
        }

        /// <summary>
        /// Creates an Alvarion-NAT-IP-Address attribute (Type 10) with the specified address.
        /// </summary>
        /// <param name="value">The NAT IP address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatIpAddress(string value)
        {
            return CreateString(AlvarionAttributeType.NAT_IP_ADDRESS, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Alvarion attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AlvarionAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Alvarion attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AlvarionAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}