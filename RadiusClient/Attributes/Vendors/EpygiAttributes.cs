using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Epygi Technologies (IANA PEN 16459) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.epygi</c>.
    /// </summary>
    public enum EpygiAttributeType : byte
    {
        /// <summary>Epygi-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Epygi-NAS-Port (Type 2). String. NAS port information string.</summary>
        NAS_PORT = 2,

        /// <summary>Epygi-Call-Info (Type 3). String. Call information string.</summary>
        CALL_INFO = 3,

        /// <summary>Epygi-Called-Number (Type 4). String. Called party number.</summary>
        CALLED_NUMBER = 4,

        /// <summary>Epygi-Calling-Number (Type 5). String. Calling party number.</summary>
        CALLING_NUMBER = 5,

        /// <summary>Epygi-Call-Type (Type 6). Integer. Call type code.</summary>
        CALL_TYPE = 6,

        /// <summary>Epygi-Call-Duration (Type 7). Integer. Call duration in seconds.</summary>
        CALL_DURATION = 7,

        /// <summary>Epygi-Disconnect-Cause (Type 8). Integer. Disconnect cause code.</summary>
        DISCONNECT_CAUSE = 8,

        /// <summary>Epygi-Bytes-In (Type 9). Integer. Ingress bytes count.</summary>
        BYTES_IN = 9,

        /// <summary>Epygi-Bytes-Out (Type 10). Integer. Egress bytes count.</summary>
        BYTES_OUT = 10,

        /// <summary>Epygi-Packets-In (Type 11). Integer. Ingress packets count.</summary>
        PACKETS_IN = 11,

        /// <summary>Epygi-Packets-Out (Type 12). Integer. Egress packets count.</summary>
        PACKETS_OUT = 12,

        /// <summary>Epygi-Call-Id (Type 13). String. SIP Call-ID.</summary>
        CALL_ID = 13,

        /// <summary>Epygi-Codec (Type 14). String. Audio codec used.</summary>
        CODEC = 14,

        /// <summary>Epygi-Jitter (Type 15). Integer. Jitter in milliseconds.</summary>
        JITTER = 15,

        /// <summary>Epygi-Packet-Loss (Type 16). Integer. Packet loss percentage.</summary>
        PACKET_LOSS = 16
    }

    /// <summary>
    /// Epygi-Call-Type attribute values (Type 6).
    /// </summary>
    public enum EPYGI_CALL_TYPE
    {
        /// <summary>Incoming call.</summary>
        INCOMING = 0,

        /// <summary>Outgoing call.</summary>
        OUTGOING = 1,

        /// <summary>Internal call.</summary>
        INTERNAL = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Epygi Technologies
    /// (IANA PEN 16459) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.epygi</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Epygi's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 16459</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Epygi Technologies IP PBX and VoIP gateway
    /// platforms for RADIUS-based call detail record (CDR) accounting, including
    /// called/calling party numbers, call type, call duration, disconnect cause,
    /// byte/packet counters, SIP Call-ID, codec identification, and QoS metrics
    /// (jitter, packet loss).
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(EpygiAttributes.CallingNumber("+15551234567"));
    /// packet.SetAttribute(EpygiAttributes.CalledNumber("+15559876543"));
    /// packet.SetAttribute(EpygiAttributes.CallType(EPYGI_CALL_TYPE.OUTGOING));
    /// packet.SetAttribute(EpygiAttributes.CallDuration(180));
    /// packet.SetAttribute(EpygiAttributes.Codec("G.711"));
    /// </code>
    /// </remarks>
    public static class EpygiAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Epygi Technologies.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 16459;

        #region Integer Attributes

        /// <summary>
        /// Creates an Epygi-Call-Type attribute (Type 6) with the specified call type.
        /// </summary>
        /// <param name="value">The call type. See <see cref="EPYGI_CALL_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallType(EPYGI_CALL_TYPE value)
        {
            return CreateInteger(EpygiAttributeType.CALL_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an Epygi-Call-Duration attribute (Type 7) with the specified duration.
        /// </summary>
        /// <param name="value">The call duration in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallDuration(int value)
        {
            return CreateInteger(EpygiAttributeType.CALL_DURATION, value);
        }

        /// <summary>
        /// Creates an Epygi-Disconnect-Cause attribute (Type 8) with the specified cause code.
        /// </summary>
        /// <param name="value">The disconnect cause code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DisconnectCause(int value)
        {
            return CreateInteger(EpygiAttributeType.DISCONNECT_CAUSE, value);
        }

        /// <summary>
        /// Creates an Epygi-Bytes-In attribute (Type 9) with the specified count.
        /// </summary>
        /// <param name="value">The ingress bytes count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BytesIn(int value)
        {
            return CreateInteger(EpygiAttributeType.BYTES_IN, value);
        }

        /// <summary>
        /// Creates an Epygi-Bytes-Out attribute (Type 10) with the specified count.
        /// </summary>
        /// <param name="value">The egress bytes count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BytesOut(int value)
        {
            return CreateInteger(EpygiAttributeType.BYTES_OUT, value);
        }

        /// <summary>
        /// Creates an Epygi-Packets-In attribute (Type 11) with the specified count.
        /// </summary>
        /// <param name="value">The ingress packets count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PacketsIn(int value)
        {
            return CreateInteger(EpygiAttributeType.PACKETS_IN, value);
        }

        /// <summary>
        /// Creates an Epygi-Packets-Out attribute (Type 12) with the specified count.
        /// </summary>
        /// <param name="value">The egress packets count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PacketsOut(int value)
        {
            return CreateInteger(EpygiAttributeType.PACKETS_OUT, value);
        }

        /// <summary>
        /// Creates an Epygi-Jitter attribute (Type 15) with the specified jitter.
        /// </summary>
        /// <param name="value">The jitter in milliseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Jitter(int value)
        {
            return CreateInteger(EpygiAttributeType.JITTER, value);
        }

        /// <summary>
        /// Creates an Epygi-Packet-Loss attribute (Type 16) with the specified percentage.
        /// </summary>
        /// <param name="value">The packet loss percentage.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PacketLoss(int value)
        {
            return CreateInteger(EpygiAttributeType.PACKET_LOSS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Epygi-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(EpygiAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates an Epygi-NAS-Port attribute (Type 2) with the specified port information.
        /// </summary>
        /// <param name="value">The NAS port information string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NasPort(string value)
        {
            return CreateString(EpygiAttributeType.NAS_PORT, value);
        }

        /// <summary>
        /// Creates an Epygi-Call-Info attribute (Type 3) with the specified call information.
        /// </summary>
        /// <param name="value">The call information string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallInfo(string value)
        {
            return CreateString(EpygiAttributeType.CALL_INFO, value);
        }

        /// <summary>
        /// Creates an Epygi-Called-Number attribute (Type 4) with the specified number.
        /// </summary>
        /// <param name="value">The called party number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledNumber(string value)
        {
            return CreateString(EpygiAttributeType.CALLED_NUMBER, value);
        }

        /// <summary>
        /// Creates an Epygi-Calling-Number attribute (Type 5) with the specified number.
        /// </summary>
        /// <param name="value">The calling party number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingNumber(string value)
        {
            return CreateString(EpygiAttributeType.CALLING_NUMBER, value);
        }

        /// <summary>
        /// Creates an Epygi-Call-Id attribute (Type 13) with the specified SIP Call-ID.
        /// </summary>
        /// <param name="value">The SIP Call-ID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallId(string value)
        {
            return CreateString(EpygiAttributeType.CALL_ID, value);
        }

        /// <summary>
        /// Creates an Epygi-Codec attribute (Type 14) with the specified codec name.
        /// </summary>
        /// <param name="value">The audio codec used. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Codec(string value)
        {
            return CreateString(EpygiAttributeType.CODEC, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Epygi attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(EpygiAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Epygi attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(EpygiAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}