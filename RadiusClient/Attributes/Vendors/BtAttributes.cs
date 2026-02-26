using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a British Telecommunications / BT (IANA PEN 2592) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.bt</c>.
    /// </summary>
    public enum BtAttributeType : byte
    {
        /// <summary>BT-UPC (Type 1). String. User Profile Class.</summary>
        UPC = 1,

        /// <summary>BT-ISP-ID (Type 2). String. ISP identifier.</summary>
        ISP_ID = 2,

        /// <summary>BT-Agent-Circuit-Id (Type 3). String. Agent circuit identifier.</summary>
        AGENT_CIRCUIT_ID = 3,

        /// <summary>BT-Agent-Remote-Id (Type 4). String. Agent remote identifier.</summary>
        AGENT_REMOTE_ID = 4,

        /// <summary>BT-Bearer-Type (Type 5). Integer. Bearer type code.</summary>
        BEARER_TYPE = 5,

        /// <summary>BT-Upstream-Speed-Kbps (Type 6). Integer. Upstream speed in Kbps.</summary>
        UPSTREAM_SPEED_KBPS = 6,

        /// <summary>BT-Downstream-Speed-Kbps (Type 7). Integer. Downstream speed in Kbps.</summary>
        DOWNSTREAM_SPEED_KBPS = 7,

        /// <summary>BT-LNS-Container (Type 8). Octets. LNS container data.</summary>
        LNS_CONTAINER = 8,

        /// <summary>BT-Agent-Circuit-Id-Tag (Type 9). String. Agent circuit ID tag.</summary>
        AGENT_CIRCUIT_ID_TAG = 9,

        /// <summary>BT-Agent-Remote-Id-Tag (Type 10). String. Agent remote ID tag.</summary>
        AGENT_REMOTE_ID_TAG = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing British Telecommunications / BT
    /// (IANA PEN 2592) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.bt</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// BT's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2592</c>.
    /// </para>
    /// <para>
    /// These attributes are used by British Telecommunications (BT) wholesale
    /// broadband platforms for subscriber profile classification, ISP identification,
    /// agent circuit/remote ID relay, bearer type and speed provisioning, and
    /// L2TP LNS container transport.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(BtAttributes.Upc("premium"));
    /// packet.SetAttribute(BtAttributes.IspId("ISP-001"));
    /// packet.SetAttribute(BtAttributes.DownstreamSpeedKbps(80000));
    /// packet.SetAttribute(BtAttributes.UpstreamSpeedKbps(20000));
    /// </code>
    /// </remarks>
    public static class BtAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for British Telecommunications (BT).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2592;

        #region Integer Attributes

        /// <summary>
        /// Creates a BT-Bearer-Type attribute (Type 5) with the specified bearer type.
        /// </summary>
        /// <param name="value">The bearer type code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BearerType(int value)
        {
            return CreateInteger(BtAttributeType.BEARER_TYPE, value);
        }

        /// <summary>
        /// Creates a BT-Upstream-Speed-Kbps attribute (Type 6) with the specified speed.
        /// </summary>
        /// <param name="value">The upstream speed in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UpstreamSpeedKbps(int value)
        {
            return CreateInteger(BtAttributeType.UPSTREAM_SPEED_KBPS, value);
        }

        /// <summary>
        /// Creates a BT-Downstream-Speed-Kbps attribute (Type 7) with the specified speed.
        /// </summary>
        /// <param name="value">The downstream speed in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DownstreamSpeedKbps(int value)
        {
            return CreateInteger(BtAttributeType.DOWNSTREAM_SPEED_KBPS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a BT-UPC attribute (Type 1) with the specified User Profile Class.
        /// </summary>
        /// <param name="value">The User Profile Class. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Upc(string value)
        {
            return CreateString(BtAttributeType.UPC, value);
        }

        /// <summary>
        /// Creates a BT-ISP-ID attribute (Type 2) with the specified ISP identifier.
        /// </summary>
        /// <param name="value">The ISP identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IspId(string value)
        {
            return CreateString(BtAttributeType.ISP_ID, value);
        }

        /// <summary>
        /// Creates a BT-Agent-Circuit-Id attribute (Type 3) with the specified circuit ID.
        /// </summary>
        /// <param name="value">The agent circuit identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AgentCircuitId(string value)
        {
            return CreateString(BtAttributeType.AGENT_CIRCUIT_ID, value);
        }

        /// <summary>
        /// Creates a BT-Agent-Remote-Id attribute (Type 4) with the specified remote ID.
        /// </summary>
        /// <param name="value">The agent remote identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AgentRemoteId(string value)
        {
            return CreateString(BtAttributeType.AGENT_REMOTE_ID, value);
        }

        /// <summary>
        /// Creates a BT-Agent-Circuit-Id-Tag attribute (Type 9) with the specified tag.
        /// </summary>
        /// <param name="value">The agent circuit ID tag. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AgentCircuitIdTag(string value)
        {
            return CreateString(BtAttributeType.AGENT_CIRCUIT_ID_TAG, value);
        }

        /// <summary>
        /// Creates a BT-Agent-Remote-Id-Tag attribute (Type 10) with the specified tag.
        /// </summary>
        /// <param name="value">The agent remote ID tag. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AgentRemoteIdTag(string value)
        {
            return CreateString(BtAttributeType.AGENT_REMOTE_ID_TAG, value);
        }

        #endregion

        #region Octets Attributes

        /// <summary>
        /// Creates a BT-LNS-Container attribute (Type 8) with the specified raw data.
        /// </summary>
        /// <param name="value">The LNS container data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LnsContainer(byte[] value)
        {
            return CreateOctets(BtAttributeType.LNS_CONTAINER, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified BT attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(BtAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified BT attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(BtAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a raw byte array value for the specified BT attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateOctets(BtAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        #endregion
    }
}