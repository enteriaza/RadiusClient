using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Gandalf (IANA PEN 64) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.gandalf</c>.
    /// </summary>
    /// <remarks>
    /// Gandalf Technologies was a Canadian manufacturer of data communications
    /// equipment including ISDN terminal adapters, modems, and remote access
    /// concentrators.
    /// </remarks>
    public enum GandalfAttributeType : byte
    {
        /// <summary>Gandalf-Remote-LAN-Name (Type 1). String. Remote LAN name.</summary>
        REMOTE_LAN_NAME = 1,

        /// <summary>Gandalf-Operational-Modes (Type 2). Integer. Operational modes bitmask.</summary>
        OPERATIONAL_MODES = 2,

        /// <summary>Gandalf-Compression-Status (Type 3). Integer. Compression status.</summary>
        COMPRESSION_STATUS = 3,

        /// <summary>Gandalf-Min-Outgoing-Bearer (Type 4). Integer. Minimum outgoing bearer channel.</summary>
        MIN_OUTGOING_BEARER = 4,

        /// <summary>Gandalf-Authentication-String (Type 5). String. Authentication string.</summary>
        AUTHENTICATION_STRING = 5,

        /// <summary>Gandalf-PPP-Authentication (Type 6). Integer. PPP authentication protocol.</summary>
        PPP_AUTHENTICATION = 6,

        /// <summary>Gandalf-PPP-NCP-Type (Type 7). Integer. PPP NCP type.</summary>
        PPP_NCP_TYPE = 7,

        /// <summary>Gandalf-Fwd-Multicast-In (Type 8). Integer. Forward multicast inbound flag.</summary>
        FWD_MULTICAST_IN = 8,

        /// <summary>Gandalf-Fwd-Broadcast-In (Type 9). Integer. Forward broadcast inbound flag.</summary>
        FWD_BROADCAST_IN = 9,

        /// <summary>Gandalf-Fwd-Unicast-In (Type 10). Integer. Forward unicast inbound flag.</summary>
        FWD_UNICAST_IN = 10,

        /// <summary>Gandalf-Fwd-Multicast-Out (Type 11). Integer. Forward multicast outbound flag.</summary>
        FWD_MULTICAST_OUT = 11,

        /// <summary>Gandalf-Fwd-Broadcast-Out (Type 12). Integer. Forward broadcast outbound flag.</summary>
        FWD_BROADCAST_OUT = 12,

        /// <summary>Gandalf-Fwd-Unicast-Out (Type 13). Integer. Forward unicast outbound flag.</summary>
        FWD_UNICAST_OUT = 13,

        /// <summary>Gandalf-Around-The-Corner (Type 14). Integer. Around-the-corner flag.</summary>
        AROUND_THE_CORNER = 14,

        /// <summary>Gandalf-Channel-Group-Name-1 (Type 15). String. Channel group name 1.</summary>
        CHANNEL_GROUP_NAME_1 = 15,

        /// <summary>Gandalf-Dial-Prefix-Name-1 (Type 16). String. Dial prefix name 1.</summary>
        DIAL_PREFIX_NAME_1 = 16,

        /// <summary>Gandalf-Phone-Number-1 (Type 17). String. Phone number 1.</summary>
        PHONE_NUMBER_1 = 17,

        /// <summary>Gandalf-Calling-Line-ID-1 (Type 18). String. Calling line ID 1.</summary>
        CALLING_LINE_ID_1 = 18,

        /// <summary>Gandalf-Channel-Group-Name-2 (Type 19). String. Channel group name 2.</summary>
        CHANNEL_GROUP_NAME_2 = 19,

        /// <summary>Gandalf-Dial-Prefix-Name-2 (Type 20). String. Dial prefix name 2.</summary>
        DIAL_PREFIX_NAME_2 = 20,

        /// <summary>Gandalf-Phone-Number-2 (Type 21). String. Phone number 2.</summary>
        PHONE_NUMBER_2 = 21,

        /// <summary>Gandalf-Calling-Line-ID-2 (Type 22). String. Calling line ID 2.</summary>
        CALLING_LINE_ID_2 = 22
    }

    /// <summary>
    /// Gandalf-Compression-Status attribute values (Type 3).
    /// </summary>
    public enum GANDALF_COMPRESSION_STATUS
    {
        /// <summary>Compression disabled.</summary>
        DISABLED = 0,

        /// <summary>Compression enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Gandalf-PPP-Authentication attribute values (Type 6).
    /// </summary>
    public enum GANDALF_PPP_AUTHENTICATION
    {
        /// <summary>No PPP authentication.</summary>
        NONE = 0,

        /// <summary>PAP authentication.</summary>
        PAP = 1,

        /// <summary>CHAP authentication.</summary>
        CHAP = 2,

        /// <summary>PAP then CHAP.</summary>
        PAP_CHAP = 3,

        /// <summary>CHAP then PAP.</summary>
        CHAP_PAP = 4
    }

    /// <summary>
    /// Gandalf-PPP-NCP-Type attribute values (Type 7).
    /// </summary>
    public enum GANDALF_PPP_NCP_TYPE
    {
        /// <summary>BCP (Bridging Control Protocol).</summary>
        BCP = 0,

        /// <summary>IPCP (IP Control Protocol).</summary>
        IPCP = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Gandalf
    /// (IANA PEN 64) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.gandalf</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Gandalf's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 64</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Gandalf Technologies ISDN terminal adapters
    /// and remote access concentrators for RADIUS-based remote LAN configuration,
    /// operational mode selection, compression control, PPP authentication and
    /// NCP type, multicast/broadcast/unicast forwarding (inbound/outbound),
    /// ISDN channel group and dial prefix configuration, phone number assignment,
    /// and calling line identification.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(GandalfAttributes.RemoteLanName("branch-office"));
    /// packet.SetAttribute(GandalfAttributes.PppAuthentication(GANDALF_PPP_AUTHENTICATION.CHAP));
    /// packet.SetAttribute(GandalfAttributes.CompressionStatus(GANDALF_COMPRESSION_STATUS.ENABLED));
    /// packet.SetAttribute(GandalfAttributes.PhoneNumber1("+15551234567"));
    /// </code>
    /// </remarks>
    public static class GandalfAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Gandalf Technologies.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 64;

        #region Integer Attributes

        /// <summary>Creates a Gandalf-Operational-Modes attribute (Type 2).</summary>
        /// <param name="value">The operational modes bitmask.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes OperationalModes(int value)
        {
            return CreateInteger(GandalfAttributeType.OPERATIONAL_MODES, value);
        }

        /// <summary>
        /// Creates a Gandalf-Compression-Status attribute (Type 3).
        /// </summary>
        /// <param name="value">The compression status. See <see cref="GANDALF_COMPRESSION_STATUS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CompressionStatus(GANDALF_COMPRESSION_STATUS value)
        {
            return CreateInteger(GandalfAttributeType.COMPRESSION_STATUS, (int)value);
        }

        /// <summary>Creates a Gandalf-Min-Outgoing-Bearer attribute (Type 4).</summary>
        /// <param name="value">The minimum outgoing bearer channel.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MinOutgoingBearer(int value)
        {
            return CreateInteger(GandalfAttributeType.MIN_OUTGOING_BEARER, value);
        }

        /// <summary>
        /// Creates a Gandalf-PPP-Authentication attribute (Type 6).
        /// </summary>
        /// <param name="value">The PPP authentication protocol. See <see cref="GANDALF_PPP_AUTHENTICATION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PppAuthentication(GANDALF_PPP_AUTHENTICATION value)
        {
            return CreateInteger(GandalfAttributeType.PPP_AUTHENTICATION, (int)value);
        }

        /// <summary>
        /// Creates a Gandalf-PPP-NCP-Type attribute (Type 7).
        /// </summary>
        /// <param name="value">The PPP NCP type. See <see cref="GANDALF_PPP_NCP_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PppNcpType(GANDALF_PPP_NCP_TYPE value)
        {
            return CreateInteger(GandalfAttributeType.PPP_NCP_TYPE, (int)value);
        }

        /// <summary>Creates a Gandalf-Fwd-Multicast-In attribute (Type 8).</summary>
        /// <param name="value">The forward multicast inbound flag.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FwdMulticastIn(int value)
        {
            return CreateInteger(GandalfAttributeType.FWD_MULTICAST_IN, value);
        }

        /// <summary>Creates a Gandalf-Fwd-Broadcast-In attribute (Type 9).</summary>
        /// <param name="value">The forward broadcast inbound flag.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FwdBroadcastIn(int value)
        {
            return CreateInteger(GandalfAttributeType.FWD_BROADCAST_IN, value);
        }

        /// <summary>Creates a Gandalf-Fwd-Unicast-In attribute (Type 10).</summary>
        /// <param name="value">The forward unicast inbound flag.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FwdUnicastIn(int value)
        {
            return CreateInteger(GandalfAttributeType.FWD_UNICAST_IN, value);
        }

        /// <summary>Creates a Gandalf-Fwd-Multicast-Out attribute (Type 11).</summary>
        /// <param name="value">The forward multicast outbound flag.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FwdMulticastOut(int value)
        {
            return CreateInteger(GandalfAttributeType.FWD_MULTICAST_OUT, value);
        }

        /// <summary>Creates a Gandalf-Fwd-Broadcast-Out attribute (Type 12).</summary>
        /// <param name="value">The forward broadcast outbound flag.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FwdBroadcastOut(int value)
        {
            return CreateInteger(GandalfAttributeType.FWD_BROADCAST_OUT, value);
        }

        /// <summary>Creates a Gandalf-Fwd-Unicast-Out attribute (Type 13).</summary>
        /// <param name="value">The forward unicast outbound flag.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FwdUnicastOut(int value)
        {
            return CreateInteger(GandalfAttributeType.FWD_UNICAST_OUT, value);
        }

        /// <summary>Creates a Gandalf-Around-The-Corner attribute (Type 14).</summary>
        /// <param name="value">The around-the-corner flag.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AroundTheCorner(int value)
        {
            return CreateInteger(GandalfAttributeType.AROUND_THE_CORNER, value);
        }

        #endregion

        #region String Attributes

        /// <summary>Creates a Gandalf-Remote-LAN-Name attribute (Type 1).</summary>
        /// <param name="value">The remote LAN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RemoteLanName(string value) => CreateString(GandalfAttributeType.REMOTE_LAN_NAME, value);

        /// <summary>Creates a Gandalf-Authentication-String attribute (Type 5).</summary>
        /// <param name="value">The authentication string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthenticationString(string value) => CreateString(GandalfAttributeType.AUTHENTICATION_STRING, value);

        /// <summary>Creates a Gandalf-Channel-Group-Name-1 attribute (Type 15).</summary>
        /// <param name="value">The channel group name 1. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChannelGroupName1(string value) => CreateString(GandalfAttributeType.CHANNEL_GROUP_NAME_1, value);

        /// <summary>Creates a Gandalf-Dial-Prefix-Name-1 attribute (Type 16).</summary>
        /// <param name="value">The dial prefix name 1. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DialPrefixName1(string value) => CreateString(GandalfAttributeType.DIAL_PREFIX_NAME_1, value);

        /// <summary>Creates a Gandalf-Phone-Number-1 attribute (Type 17).</summary>
        /// <param name="value">The phone number 1. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PhoneNumber1(string value) => CreateString(GandalfAttributeType.PHONE_NUMBER_1, value);

        /// <summary>Creates a Gandalf-Calling-Line-ID-1 attribute (Type 18).</summary>
        /// <param name="value">The calling line ID 1. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingLineId1(string value) => CreateString(GandalfAttributeType.CALLING_LINE_ID_1, value);

        /// <summary>Creates a Gandalf-Channel-Group-Name-2 attribute (Type 19).</summary>
        /// <param name="value">The channel group name 2. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChannelGroupName2(string value) => CreateString(GandalfAttributeType.CHANNEL_GROUP_NAME_2, value);

        /// <summary>Creates a Gandalf-Dial-Prefix-Name-2 attribute (Type 20).</summary>
        /// <param name="value">The dial prefix name 2. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DialPrefixName2(string value) => CreateString(GandalfAttributeType.DIAL_PREFIX_NAME_2, value);

        /// <summary>Creates a Gandalf-Phone-Number-2 attribute (Type 21).</summary>
        /// <param name="value">The phone number 2. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PhoneNumber2(string value) => CreateString(GandalfAttributeType.PHONE_NUMBER_2, value);

        /// <summary>Creates a Gandalf-Calling-Line-ID-2 attribute (Type 22).</summary>
        /// <param name="value">The calling line ID 2. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingLineId2(string value) => CreateString(GandalfAttributeType.CALLING_LINE_ID_2, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(GandalfAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(GandalfAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}