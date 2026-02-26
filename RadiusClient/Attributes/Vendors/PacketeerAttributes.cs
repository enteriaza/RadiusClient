using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Packeteer (IANA PEN 2334) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.packeteer</c>.
    /// </summary>
    /// <remarks>
    /// Packeteer (acquired by Blue Coat Systems, then Symantec, now Broadcom)
    /// produced WAN optimization and application traffic management appliances
    /// (PacketShaper, IntelligenceCenter) for bandwidth management, QoS, and
    /// application performance monitoring.
    /// </remarks>
    public enum PacketeerAttributeType : byte
    {
        /// <summary>Packeteer-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Packeteer-Access-Level (Type 2). Integer. Administrative access level.</summary>
        ACCESS_LEVEL = 2,

        /// <summary>Packeteer-Partition-Id (Type 3). String. Traffic partition identifier.</summary>
        PARTITION_ID = 3,

        /// <summary>Packeteer-Policy-Name (Type 4). String. Traffic policy name.</summary>
        POLICY_NAME = 4,

        /// <summary>Packeteer-Class-Name (Type 5). String. Traffic class name.</summary>
        CLASS_NAME = 5
    }

    /// <summary>
    /// Packeteer-Access-Level attribute values (Type 2).
    /// </summary>
    public enum PACKETEER_ACCESS_LEVEL
    {
        /// <summary>No access.</summary>
        NONE = 0,

        /// <summary>Read-only (monitor) access.</summary>
        MONITOR = 1,

        /// <summary>Read-write (operator) access.</summary>
        OPERATOR = 2,

        /// <summary>Full administrative access.</summary>
        ADMIN = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Packeteer
    /// (IANA PEN 2334) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.packeteer</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Packeteer's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2334</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Packeteer (now Broadcom) PacketShaper WAN
    /// optimization and application traffic management appliances for RADIUS-based
    /// administrative access level assignment, traffic partition identification,
    /// traffic policy and class name selection, and general-purpose attribute-value
    /// pair configuration during administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(PacketeerAttributes.AccessLevel(PACKETEER_ACCESS_LEVEL.ADMIN));
    /// packet.SetAttribute(PacketeerAttributes.PartitionId("wan-partition-1"));
    /// packet.SetAttribute(PacketeerAttributes.PolicyName("business-critical"));
    /// packet.SetAttribute(PacketeerAttributes.ClassName("voip-traffic"));
    /// </code>
    /// </remarks>
    public static class PacketeerAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Packeteer.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2334;

        #region Integer Attributes

        /// <summary>
        /// Creates a Packeteer-Access-Level attribute (Type 2) with the specified level.
        /// </summary>
        /// <param name="value">The administrative access level. See <see cref="PACKETEER_ACCESS_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLevel(PACKETEER_ACCESS_LEVEL value)
        {
            return CreateInteger(PacketeerAttributeType.ACCESS_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Packeteer-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(PacketeerAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Packeteer-Partition-Id attribute (Type 3) with the specified partition.
        /// </summary>
        /// <param name="value">The traffic partition identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PartitionId(string value)
        {
            return CreateString(PacketeerAttributeType.PARTITION_ID, value);
        }

        /// <summary>
        /// Creates a Packeteer-Policy-Name attribute (Type 4) with the specified policy name.
        /// </summary>
        /// <param name="value">The traffic policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(PacketeerAttributeType.POLICY_NAME, value);
        }

        /// <summary>
        /// Creates a Packeteer-Class-Name attribute (Type 5) with the specified class name.
        /// </summary>
        /// <param name="value">The traffic class name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClassName(string value)
        {
            return CreateString(PacketeerAttributeType.CLASS_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Packeteer attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(PacketeerAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Packeteer attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(PacketeerAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}