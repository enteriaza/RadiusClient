using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Digium / Sangoma (IANA PEN 22736) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.digium</c>.
    /// </summary>
    public enum DigiumAttributeType : byte
    {
        /// <summary>Digium-Asterisk-Context (Type 1). String. Asterisk dialplan context.</summary>
        ASTERISK_CONTEXT = 1,

        /// <summary>Digium-Asterisk-Peer-Name (Type 2). String. Asterisk peer name.</summary>
        ASTERISK_PEER_NAME = 2,

        /// <summary>Digium-Asterisk-Call-Duration (Type 3). Integer. Call duration in seconds.</summary>
        ASTERISK_CALL_DURATION = 3,

        /// <summary>Digium-Asterisk-Call-Disposition (Type 4). Integer. Call disposition code.</summary>
        ASTERISK_CALL_DISPOSITION = 4,

        /// <summary>Digium-Asterisk-Exten (Type 5). String. Dialled extension.</summary>
        ASTERISK_EXTEN = 5,

        /// <summary>Digium-Asterisk-Source (Type 6). String. Call source identifier.</summary>
        ASTERISK_SOURCE = 6,

        /// <summary>Digium-Asterisk-Destination (Type 7). String. Call destination identifier.</summary>
        ASTERISK_DESTINATION = 7,

        /// <summary>Digium-Asterisk-Channel (Type 8). String. Asterisk channel name.</summary>
        ASTERISK_CHANNEL = 8,

        /// <summary>Digium-Asterisk-Dest-Channel (Type 9). String. Asterisk destination channel name.</summary>
        ASTERISK_DEST_CHANNEL = 9,

        /// <summary>Digium-Asterisk-Last-App (Type 10). String. Last Asterisk application executed.</summary>
        ASTERISK_LAST_APP = 10,

        /// <summary>Digium-Asterisk-Last-Data (Type 11). String. Last Asterisk application data.</summary>
        ASTERISK_LAST_DATA = 11,

        /// <summary>Digium-Asterisk-AMA-Flags (Type 12). Integer. AMA flags for billing.</summary>
        ASTERISK_AMA_FLAGS = 12,

        /// <summary>Digium-Asterisk-DNID (Type 13). String. Dialled Number Identification.</summary>
        ASTERISK_DNID = 13,

        /// <summary>Digium-Asterisk-Unique-Id (Type 14). String. Unique call identifier.</summary>
        ASTERISK_UNIQUE_ID = 14,

        /// <summary>Digium-Asterisk-Linked-Id (Type 15). String. Linked call identifier.</summary>
        ASTERISK_LINKED_ID = 15,

        /// <summary>Digium-Asterisk-User-Field (Type 16). String. User-defined field.</summary>
        ASTERISK_USER_FIELD = 16,

        /// <summary>Digium-Asterisk-Account-Code (Type 17). String. Account code for billing.</summary>
        ASTERISK_ACCOUNT_CODE = 17,

        /// <summary>Digium-Asterisk-Sequence (Type 18). String. CDR sequence number.</summary>
        ASTERISK_SEQUENCE = 18
    }

    /// <summary>
    /// Digium-Asterisk-Call-Disposition attribute values (Type 4).
    /// </summary>
    public enum DIGIUM_ASTERISK_CALL_DISPOSITION
    {
        /// <summary>No answer.</summary>
        NO_ANSWER = 0,

        /// <summary>Call answered.</summary>
        ANSWERED = 1,

        /// <summary>Call busy.</summary>
        BUSY = 2,

        /// <summary>Call failed.</summary>
        FAILED = 3
    }

    /// <summary>
    /// Digium-Asterisk-AMA-Flags attribute values (Type 12).
    /// </summary>
    public enum DIGIUM_ASTERISK_AMA_FLAGS
    {
        /// <summary>Default AMA flag.</summary>
        DEFAULT = 0,

        /// <summary>Omit from billing.</summary>
        OMIT = 1,

        /// <summary>Billing record.</summary>
        BILLING = 2,

        /// <summary>Documentation record.</summary>
        DOCUMENTATION = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Digium / Sangoma
    /// (IANA PEN 22736) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.digium</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Digium's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 22736</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Digium (now Sangoma) Asterisk PBX and Switchvox
    /// platforms for RADIUS-based call detail record (CDR) accounting, including
    /// dialplan context, peer identification, call duration and disposition,
    /// source/destination/channel information, last executed application,
    /// AMA billing flags, DNID, unique/linked call IDs, account codes,
    /// and user-defined fields.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(DigiumAttributes.AsteriskContext("from-internal"));
    /// packet.SetAttribute(DigiumAttributes.AsteriskExten("1001"));
    /// packet.SetAttribute(DigiumAttributes.AsteriskCallDuration(120));
    /// packet.SetAttribute(DigiumAttributes.AsteriskCallDisposition(DIGIUM_ASTERISK_CALL_DISPOSITION.ANSWERED));
    /// packet.SetAttribute(DigiumAttributes.AsteriskAccountCode("sales"));
    /// </code>
    /// </remarks>
    public static class DigiumAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Digium (Sangoma).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 22736;

        #region Integer Attributes

        /// <summary>
        /// Creates a Digium-Asterisk-Call-Duration attribute (Type 3) with the specified duration.
        /// </summary>
        /// <param name="value">The call duration in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AsteriskCallDuration(int value)
        {
            return CreateInteger(DigiumAttributeType.ASTERISK_CALL_DURATION, value);
        }

        /// <summary>
        /// Creates a Digium-Asterisk-Call-Disposition attribute (Type 4) with the specified disposition.
        /// </summary>
        /// <param name="value">The call disposition. See <see cref="DIGIUM_ASTERISK_CALL_DISPOSITION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AsteriskCallDisposition(DIGIUM_ASTERISK_CALL_DISPOSITION value)
        {
            return CreateInteger(DigiumAttributeType.ASTERISK_CALL_DISPOSITION, (int)value);
        }

        /// <summary>
        /// Creates a Digium-Asterisk-AMA-Flags attribute (Type 12) with the specified flags.
        /// </summary>
        /// <param name="value">The AMA flags for billing. See <see cref="DIGIUM_ASTERISK_AMA_FLAGS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AsteriskAmaFlags(DIGIUM_ASTERISK_AMA_FLAGS value)
        {
            return CreateInteger(DigiumAttributeType.ASTERISK_AMA_FLAGS, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>Creates a Digium-Asterisk-Context attribute (Type 1).</summary>
        /// <param name="value">The Asterisk dialplan context. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskContext(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_CONTEXT, value);
        }

        /// <summary>Creates a Digium-Asterisk-Peer-Name attribute (Type 2).</summary>
        /// <param name="value">The Asterisk peer name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskPeerName(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_PEER_NAME, value);
        }

        /// <summary>Creates a Digium-Asterisk-Exten attribute (Type 5).</summary>
        /// <param name="value">The dialled extension. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskExten(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_EXTEN, value);
        }

        /// <summary>Creates a Digium-Asterisk-Source attribute (Type 6).</summary>
        /// <param name="value">The call source identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskSource(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_SOURCE, value);
        }

        /// <summary>Creates a Digium-Asterisk-Destination attribute (Type 7).</summary>
        /// <param name="value">The call destination identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskDestination(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_DESTINATION, value);
        }

        /// <summary>Creates a Digium-Asterisk-Channel attribute (Type 8).</summary>
        /// <param name="value">The Asterisk channel name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskChannel(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_CHANNEL, value);
        }

        /// <summary>Creates a Digium-Asterisk-Dest-Channel attribute (Type 9).</summary>
        /// <param name="value">The Asterisk destination channel name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskDestChannel(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_DEST_CHANNEL, value);
        }

        /// <summary>Creates a Digium-Asterisk-Last-App attribute (Type 10).</summary>
        /// <param name="value">The last Asterisk application executed. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskLastApp(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_LAST_APP, value);
        }

        /// <summary>Creates a Digium-Asterisk-Last-Data attribute (Type 11).</summary>
        /// <param name="value">The last Asterisk application data. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskLastData(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_LAST_DATA, value);
        }

        /// <summary>Creates a Digium-Asterisk-DNID attribute (Type 13).</summary>
        /// <param name="value">The Dialled Number Identification. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskDnid(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_DNID, value);
        }

        /// <summary>Creates a Digium-Asterisk-Unique-Id attribute (Type 14).</summary>
        /// <param name="value">The unique call identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskUniqueId(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_UNIQUE_ID, value);
        }

        /// <summary>Creates a Digium-Asterisk-Linked-Id attribute (Type 15).</summary>
        /// <param name="value">The linked call identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskLinkedId(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_LINKED_ID, value);
        }

        /// <summary>Creates a Digium-Asterisk-User-Field attribute (Type 16).</summary>
        /// <param name="value">The user-defined field. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskUserField(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_USER_FIELD, value);
        }

        /// <summary>Creates a Digium-Asterisk-Account-Code attribute (Type 17).</summary>
        /// <param name="value">The account code for billing. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskAccountCode(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_ACCOUNT_CODE, value);
        }

        /// <summary>Creates a Digium-Asterisk-Sequence attribute (Type 18).</summary>
        /// <param name="value">The CDR sequence number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsteriskSequence(string value)
        {
            return CreateString(DigiumAttributeType.ASTERISK_SEQUENCE, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(DigiumAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(DigiumAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}