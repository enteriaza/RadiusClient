using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Patton Electronics (IANA PEN 1768) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.patton</c>.
    /// </summary>
    /// <remarks>
    /// Patton Electronics produces VoIP gateways, IADs (Integrated Access Devices),
    /// routers, and Ethernet extenders (SmartNode series, OnSite series) for
    /// enterprise and service provider deployments.
    /// </remarks>
    public enum PattonAttributeType : byte
    {
        /// <summary>Patton-Protocol (Type 1). Integer. Protocol type.</summary>
        PROTOCOL = 1,

        /// <summary>Patton-Setup-Time (Type 2). String. Call setup time.</summary>
        SETUP_TIME = 2,

        /// <summary>Patton-Connect-Time (Type 3). String. Call connect time.</summary>
        CONNECT_TIME = 3,

        /// <summary>Patton-Disconnect-Time (Type 4). String. Call disconnect time.</summary>
        DISCONNECT_TIME = 4,

        /// <summary>Patton-Disconnect-Cause (Type 5). Integer. Call disconnect cause code.</summary>
        DISCONNECT_CAUSE = 5,

        /// <summary>Patton-Disconnect-Source (Type 6). Integer. Call disconnect source.</summary>
        DISCONNECT_SOURCE = 6,

        /// <summary>Patton-Disconnect-Reason (Type 7). String. Call disconnect reason text.</summary>
        DISCONNECT_REASON = 7,

        /// <summary>Patton-Called-Unique-Id (Type 8). String. Called party unique identifier.</summary>
        CALLED_UNIQUE_ID = 8,

        /// <summary>Patton-Called-IP-Address (Type 9). String. Called party IP address.</summary>
        CALLED_IP_ADDRESS = 9,

        /// <summary>Patton-Called-Numbering-Plan (Type 10). Integer. Called party numbering plan.</summary>
        CALLED_NUMBERING_PLAN = 10,

        /// <summary>Patton-Called-Type-Of-Number (Type 11). Integer. Called party type of number.</summary>
        CALLED_TYPE_OF_NUMBER = 11,

        /// <summary>Patton-Called-Name (Type 12). String. Called party name.</summary>
        CALLED_NAME = 12,

        /// <summary>Patton-Called-Station-Id (Type 13). String. Called station identifier.</summary>
        CALLED_STATION_ID = 13,

        /// <summary>Patton-Calling-Unique-Id (Type 14). String. Calling party unique identifier.</summary>
        CALLING_UNIQUE_ID = 14,

        /// <summary>Patton-Calling-IP-Address (Type 15). String. Calling party IP address.</summary>
        CALLING_IP_ADDRESS = 15,

        /// <summary>Patton-Calling-Numbering-Plan (Type 16). Integer. Calling party numbering plan.</summary>
        CALLING_NUMBERING_PLAN = 16,

        /// <summary>Patton-Calling-Type-Of-Number (Type 17). Integer. Calling party type of number.</summary>
        CALLING_TYPE_OF_NUMBER = 17,

        /// <summary>Patton-Calling-Name (Type 18). String. Calling party name.</summary>
        CALLING_NAME = 18,

        /// <summary>Patton-Calling-Station-Id (Type 19). String. Calling station identifier.</summary>
        CALLING_STATION_ID = 19,

        /// <summary>Patton-Call-Duration (Type 20). Integer. Call duration in seconds.</summary>
        CALL_DURATION = 20,

        /// <summary>Patton-Call-Direction (Type 21). Integer. Call direction.</summary>
        CALL_DIRECTION = 21,

        /// <summary>Patton-Call-Origin (Type 22). Integer. Call origin type.</summary>
        CALL_ORIGIN = 22,

        /// <summary>Patton-Codec (Type 23). String. Codec used for the call.</summary>
        CODEC = 23,

        /// <summary>Patton-Packets-Sent (Type 24). Integer. RTP packets sent.</summary>
        PACKETS_SENT = 24,

        /// <summary>Patton-Packets-Received (Type 25). Integer. RTP packets received.</summary>
        PACKETS_RECEIVED = 25,

        /// <summary>Patton-Packets-Lost (Type 26). Integer. RTP packets lost.</summary>
        PACKETS_LOST = 26,

        /// <summary>Patton-Jitter (Type 27). Integer. RTP jitter in milliseconds.</summary>
        JITTER = 27,

        /// <summary>Patton-Latency (Type 28). Integer. RTP latency in milliseconds.</summary>
        LATENCY = 28
    }

    /// <summary>
    /// Patton-Protocol attribute values (Type 1).
    /// </summary>
    public enum PATTON_PROTOCOL
    {
        /// <summary>SIP protocol.</summary>
        SIP = 1,

        /// <summary>H.323 protocol.</summary>
        H323 = 2,

        /// <summary>ISDN protocol.</summary>
        ISDN = 3,

        /// <summary>FXS/FXO (analog) protocol.</summary>
        FXS_FXO = 4
    }

    /// <summary>
    /// Patton-Disconnect-Source attribute values (Type 6).
    /// </summary>
    public enum PATTON_DISCONNECT_SOURCE
    {
        /// <summary>Disconnect initiated by calling party.</summary>
        CALLING_PARTY = 1,

        /// <summary>Disconnect initiated by called party.</summary>
        CALLED_PARTY = 2,

        /// <summary>Disconnect initiated by the gateway (internal).</summary>
        INTERNAL = 3
    }

    /// <summary>
    /// Patton-Call-Direction attribute values (Type 21).
    /// </summary>
    public enum PATTON_CALL_DIRECTION
    {
        /// <summary>Inbound call.</summary>
        INBOUND = 1,

        /// <summary>Outbound call.</summary>
        OUTBOUND = 2
    }

    /// <summary>
    /// Patton-Call-Origin attribute values (Type 22).
    /// </summary>
    public enum PATTON_CALL_ORIGIN
    {
        /// <summary>Call originated from PSTN.</summary>
        PSTN = 1,

        /// <summary>Call originated from IP/VoIP.</summary>
        VOIP = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Patton Electronics
    /// (IANA PEN 1768) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.patton</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Patton's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 1768</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Patton Electronics SmartNode VoIP gateways and
    /// IADs for RADIUS-based VoIP call detail record (CDR) accounting including
    /// protocol type, call setup/connect/disconnect timestamps, disconnect cause
    /// code/source/reason, called and calling party identification (unique ID, IP
    /// address, numbering plan, type of number, name, station ID), call duration,
    /// direction, and origin, codec identification, and RTP quality metrics
    /// (packets sent/received/lost, jitter, latency).
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(PattonAttributes.Protocol(PATTON_PROTOCOL.SIP));
    /// packet.SetAttribute(PattonAttributes.CallingStationId("+15551234567"));
    /// packet.SetAttribute(PattonAttributes.CalledStationId("+442071234567"));
    /// packet.SetAttribute(PattonAttributes.CallDuration(360));
    /// packet.SetAttribute(PattonAttributes.CallDirection(PATTON_CALL_DIRECTION.OUTBOUND));
    /// packet.SetAttribute(PattonAttributes.Codec("G.711u"));
    /// packet.SetAttribute(PattonAttributes.Jitter(15));
    /// </code>
    /// </remarks>
    public static class PattonAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Patton Electronics.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 1768;

        #region Integer Attributes

        /// <summary>Creates a Patton-Protocol attribute (Type 1).</summary>
        /// <param name="value">The protocol type. See <see cref="PATTON_PROTOCOL"/>.</param>
        public static VendorSpecificAttributes Protocol(PATTON_PROTOCOL value) => CreateInteger(PattonAttributeType.PROTOCOL, (int)value);

        /// <summary>Creates a Patton-Disconnect-Cause attribute (Type 5).</summary>
        /// <param name="value">The call disconnect cause code (Q.931/SIP).</param>
        public static VendorSpecificAttributes DisconnectCause(int value) => CreateInteger(PattonAttributeType.DISCONNECT_CAUSE, value);

        /// <summary>Creates a Patton-Disconnect-Source attribute (Type 6).</summary>
        /// <param name="value">The call disconnect source. See <see cref="PATTON_DISCONNECT_SOURCE"/>.</param>
        public static VendorSpecificAttributes DisconnectSource(PATTON_DISCONNECT_SOURCE value) => CreateInteger(PattonAttributeType.DISCONNECT_SOURCE, (int)value);

        /// <summary>Creates a Patton-Called-Numbering-Plan attribute (Type 10).</summary>
        /// <param name="value">The called party numbering plan.</param>
        public static VendorSpecificAttributes CalledNumberingPlan(int value) => CreateInteger(PattonAttributeType.CALLED_NUMBERING_PLAN, value);

        /// <summary>Creates a Patton-Called-Type-Of-Number attribute (Type 11).</summary>
        /// <param name="value">The called party type of number.</param>
        public static VendorSpecificAttributes CalledTypeOfNumber(int value) => CreateInteger(PattonAttributeType.CALLED_TYPE_OF_NUMBER, value);

        /// <summary>Creates a Patton-Calling-Numbering-Plan attribute (Type 16).</summary>
        /// <param name="value">The calling party numbering plan.</param>
        public static VendorSpecificAttributes CallingNumberingPlan(int value) => CreateInteger(PattonAttributeType.CALLING_NUMBERING_PLAN, value);

        /// <summary>Creates a Patton-Calling-Type-Of-Number attribute (Type 17).</summary>
        /// <param name="value">The calling party type of number.</param>
        public static VendorSpecificAttributes CallingTypeOfNumber(int value) => CreateInteger(PattonAttributeType.CALLING_TYPE_OF_NUMBER, value);

        /// <summary>Creates a Patton-Call-Duration attribute (Type 20).</summary>
        /// <param name="value">The call duration in seconds.</param>
        public static VendorSpecificAttributes CallDuration(int value) => CreateInteger(PattonAttributeType.CALL_DURATION, value);

        /// <summary>Creates a Patton-Call-Direction attribute (Type 21).</summary>
        /// <param name="value">The call direction. See <see cref="PATTON_CALL_DIRECTION"/>.</param>
        public static VendorSpecificAttributes CallDirection(PATTON_CALL_DIRECTION value) => CreateInteger(PattonAttributeType.CALL_DIRECTION, (int)value);

        /// <summary>Creates a Patton-Call-Origin attribute (Type 22).</summary>
        /// <param name="value">The call origin type. See <see cref="PATTON_CALL_ORIGIN"/>.</param>
        public static VendorSpecificAttributes CallOrigin(PATTON_CALL_ORIGIN value) => CreateInteger(PattonAttributeType.CALL_ORIGIN, (int)value);

        /// <summary>Creates a Patton-Packets-Sent attribute (Type 24).</summary>
        /// <param name="value">The number of RTP packets sent.</param>
        public static VendorSpecificAttributes PacketsSent(int value) => CreateInteger(PattonAttributeType.PACKETS_SENT, value);

        /// <summary>Creates a Patton-Packets-Received attribute (Type 25).</summary>
        /// <param name="value">The number of RTP packets received.</param>
        public static VendorSpecificAttributes PacketsReceived(int value) => CreateInteger(PattonAttributeType.PACKETS_RECEIVED, value);

        /// <summary>Creates a Patton-Packets-Lost attribute (Type 26).</summary>
        /// <param name="value">The number of RTP packets lost.</param>
        public static VendorSpecificAttributes PacketsLost(int value) => CreateInteger(PattonAttributeType.PACKETS_LOST, value);

        /// <summary>Creates a Patton-Jitter attribute (Type 27).</summary>
        /// <param name="value">The RTP jitter in milliseconds.</param>
        public static VendorSpecificAttributes Jitter(int value) => CreateInteger(PattonAttributeType.JITTER, value);

        /// <summary>Creates a Patton-Latency attribute (Type 28).</summary>
        /// <param name="value">The RTP latency in milliseconds.</param>
        public static VendorSpecificAttributes Latency(int value) => CreateInteger(PattonAttributeType.LATENCY, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Patton-Setup-Time attribute (Type 2).</summary>
        /// <param name="value">The call setup time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SetupTime(string value) => CreateString(PattonAttributeType.SETUP_TIME, value);

        /// <summary>Creates a Patton-Connect-Time attribute (Type 3).</summary>
        /// <param name="value">The call connect time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ConnectTime(string value) => CreateString(PattonAttributeType.CONNECT_TIME, value);

        /// <summary>Creates a Patton-Disconnect-Time attribute (Type 4).</summary>
        /// <param name="value">The call disconnect time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DisconnectTime(string value) => CreateString(PattonAttributeType.DISCONNECT_TIME, value);

        /// <summary>Creates a Patton-Disconnect-Reason attribute (Type 7).</summary>
        /// <param name="value">The call disconnect reason text. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DisconnectReason(string value) => CreateString(PattonAttributeType.DISCONNECT_REASON, value);

        /// <summary>Creates a Patton-Called-Unique-Id attribute (Type 8).</summary>
        /// <param name="value">The called party unique identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledUniqueId(string value) => CreateString(PattonAttributeType.CALLED_UNIQUE_ID, value);

        /// <summary>Creates a Patton-Called-IP-Address attribute (Type 9).</summary>
        /// <param name="value">The called party IP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledIpAddress(string value) => CreateString(PattonAttributeType.CALLED_IP_ADDRESS, value);

        /// <summary>Creates a Patton-Called-Name attribute (Type 12).</summary>
        /// <param name="value">The called party name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledName(string value) => CreateString(PattonAttributeType.CALLED_NAME, value);

        /// <summary>Creates a Patton-Called-Station-Id attribute (Type 13).</summary>
        /// <param name="value">The called station identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledStationId(string value) => CreateString(PattonAttributeType.CALLED_STATION_ID, value);

        /// <summary>Creates a Patton-Calling-Unique-Id attribute (Type 14).</summary>
        /// <param name="value">The calling party unique identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingUniqueId(string value) => CreateString(PattonAttributeType.CALLING_UNIQUE_ID, value);

        /// <summary>Creates a Patton-Calling-IP-Address attribute (Type 15).</summary>
        /// <param name="value">The calling party IP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingIpAddress(string value) => CreateString(PattonAttributeType.CALLING_IP_ADDRESS, value);

        /// <summary>Creates a Patton-Calling-Name attribute (Type 18).</summary>
        /// <param name="value">The calling party name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingName(string value) => CreateString(PattonAttributeType.CALLING_NAME, value);

        /// <summary>Creates a Patton-Calling-Station-Id attribute (Type 19).</summary>
        /// <param name="value">The calling station identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingStationId(string value) => CreateString(PattonAttributeType.CALLING_STATION_ID, value);

        /// <summary>Creates a Patton-Codec attribute (Type 23).</summary>
        /// <param name="value">The codec used for the call (e.g. "G.711u", "G.729"). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Codec(string value) => CreateString(PattonAttributeType.CODEC, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(PattonAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(PattonAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}