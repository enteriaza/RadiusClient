using System.Buffers.Binary;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes
{
    /// <summary>
    /// Represents a single RADIUS protocol attribute in Type-Length-Value (TLV) format,
    /// as defined in RFC 2865 §5.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each attribute occupies at least 2 bytes on the wire:
    /// </para>
    /// <list type="table">
    ///   <listheader><term>Field</term><description>Size / Description</description></listheader>
    ///   <item><term>Type</term><description>1 byte — attribute type code; see <see cref="RadiusAttributeType"/>.</description></item>
    ///   <item><term>Length</term><description>1 byte — total attribute length including Type and Length fields (minimum 2, maximum 255).</description></item>
    ///   <item><term>Value</term><description>0–253 bytes — attribute-specific payload stored in <see cref="Data"/>.</description></item>
    /// </list>
    /// <para>
    /// The complete serialised TLV is always available via <see cref="RawData"/>.
    /// The human-readable value is available via <see cref="Value"/>.
    /// </para>
    /// <para>
    /// Subclasses (e.g. <see cref="VendorSpecificAttributes"/>) may use a different internal
    /// layout and should set <see cref="Type"/>, <see cref="Length"/>, <see cref="RawData"/>,
    /// and <see cref="Data"/> directly via the protected base constructor.
    /// </para>
    /// </remarks>
    public class RadiusAttributes
    {
        #region Constants

        /// <summary>
        /// Size in bytes of the fixed RADIUS attribute header (Type + Length fields), as defined
        /// in RFC 2865 §5.
        /// </summary>
        protected const byte ATTRIBUTE_HEADER_SIZE = 2;

        /// <summary>Maximum permitted value payload length in bytes (255 − 2 header bytes).</summary>
        private const int MAX_VALUE_LENGTH = 255 - ATTRIBUTE_HEADER_SIZE;

        /// <summary>
        /// Byte offset of the Tag field within the value region of tagged tunnel attributes
        /// (RFC 2868 §3.1). The Tag occupies the first byte of the attribute value, followed
        /// by 3 bytes of the tunnel type/medium code.
        /// </summary>
        private const int TUNNEL_TAG_SIZE = 1;

        #endregion

        #region Fields

        /// <summary>
        /// The complete serialised TLV (Type-Length-Value) byte array for this attribute,
        /// including the 2-byte Type and Length header fields (RFC 2865 §5).
        /// Subclasses may read and write this field directly.
        /// </summary>
        protected byte[] RawData { get; set; }

        /// <summary>
        /// The raw value bytes of this attribute, excluding the 2-byte Type and Length
        /// header fields. This is a slice view into <see cref="RawData"/> starting at
        /// offset <see cref="ATTRIBUTE_HEADER_SIZE"/>.
        /// Subclasses may read and write this field directly.
        /// </summary>
        protected byte[] Data { get; set; }

        /// <summary>
        /// Returns a copy of the complete serialised TLV (Type-Length-Value) byte array
        /// for this attribute, including the 2-byte header (RFC 2865 §5).
        /// </summary>
        /// <returns>
        /// A new byte array containing the full wire representation of this attribute.
        /// </returns>
        public byte[] GetWireBytes()
        {
            return (byte[])RawData.Clone();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the RADIUS attribute type code (RFC 2865 §5, octet 1).
        /// </summary>
        public RadiusAttributeType Type { get; protected set; }

        /// <summary>
        /// Gets the total wire length of this attribute in bytes, including the
        /// 2-byte Type and Length header fields (RFC 2865 §5, octet 2).
        /// </summary>
        public byte Length { get; protected set; }

        /// <summary>
        /// Gets a human-readable string representation of this attribute's value,
        /// decoded according to the data type associated with <see cref="Type"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Decoding rules by attribute type (per the relevant RFC wire format):
        /// </para>
        /// <list type="bullet">
        ///   <item><description><b>IP address types</b> — decoded via <see cref="IPAddress"/>
        ///     (4 bytes for IPv4, 16 bytes for IPv6).</description></item>
        ///   <item><description><b>Enumerated integer types</b> — read as big-endian 32-bit
        ///     integer and cast to the relevant enum (RFC 2865 §5).</description></item>
        ///   <item><description><b>String types</b> — decoded as UTF-8 text
        ///     (RFC 2865 §5).</description></item>
        ///   <item><description><b>Plain integer types</b> — read as big-endian 32-bit integer
        ///     and formatted as decimal (RFC 2865 §5).</description></item>
        ///   <item><description><b>Tagged tunnel types</b> — the 1-byte Tag is skipped and the
        ///     remaining 3 bytes are decoded as a 24-bit big-endian tunnel code
        ///     (RFC 2868 §3.1, §3.2).</description></item>
        ///   <item><description><b>All other types</b> — fall back to a hex dump via
        ///     <see cref="BitConverter.ToString(byte[])"/>.</description></item>
        /// </list>
        /// <para>
        /// This property never modifies <see cref="Data"/>. Big-endian integers are read
        /// non-destructively using <see cref="BinaryPrimitives"/>.
        /// </para>
        /// </remarks>
        public string Value
        {
            get
            {
                if (Data is null)
                    return string.Empty;

                // Use a ReadOnlySpan<byte> view to avoid re-boxing Data in the switch arms
                // that call BinaryPrimitives — no additional allocation is made here.
                ReadOnlySpan<byte> dataSpan = Data;

                switch (Type)
                {
                    // ── IP address attributes (4 bytes IPv4, 16 bytes IPv6) ──

                    case RadiusAttributeType.NAS_IP_ADDRESS:         // RFC 2865 §5.4
                    case RadiusAttributeType.FRAMED_IP_ADDRESS:      // RFC 2865 §5.8
                    case RadiusAttributeType.FRAMED_IP_NETMASK:      // RFC 2865 §5.9
                    case RadiusAttributeType.LOGIN_IP_HOST:          // RFC 2865 §5.14
                    case RadiusAttributeType.NAS_IPV6_ADDRESS:       // RFC 3162 §2.1
                    case RadiusAttributeType.LOGIN_IPV6_HOST:        // RFC 3162 §2.4
                    case RadiusAttributeType.FRAMED_IPV6_ADDRESS:    // RFC 6911 §2.1
                    case RadiusAttributeType.DNS_SERVER_IPV6_ADDRESS: // RFC 6911 §2.2
                        // IPAddress(ReadOnlySpan<byte>) avoids the array copy that
                        // IPAddress(byte[]) performs internally on .NET 8.
                        return new IPAddress(dataSpan).ToString();

                    // ── Enumerated 32-bit integer attributes (RFC 2865 §5, RFC 2866 §5) ──

                    case RadiusAttributeType.SERVICE_TYPE:            // RFC 2865 §5.6
                        return ((SERVICE_TYPE)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.FRAMED_PROTOCOL:        // RFC 2865 §5.7
                        return ((FRAMED_PROTOCOL)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.FRAMED_ROUTING:         // RFC 2865 §5.10
                        return ((FRAMED_ROUTING)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.FRAMED_COMPRESSION:     // RFC 2865 §5.13
                        return ((FRAMED_COMPRESSION)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.LOGIN_SERVICE:          // RFC 2865 §5.15
                        return ((LOGIN_SERVICE)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.TERMINATION_ACTION:     // RFC 2865 §5.29
                        return ((TERMINATION_ACTION)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.ACCT_STATUS_TYPE:       // RFC 2866 §5.1
                        return ((ACCT_STATUS_TYPE)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.ACCT_AUTHENTIC:         // RFC 2866 §5.6
                        return ((ACCT_AUTHENTIC)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.ACCT_TERMINATE_CAUSE:   // RFC 2866 §5.10
                        return ((ACCT_TERMINATE_CAUSE)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.NAS_PORT_TYPE:          // RFC 2865 §5.41
                        return ((NAS_PORT_TYPE)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.ARAP_ZONE_ACCESS:       // RFC 2869 §5.6
                        return ((ARAP_ZONE_ACCESS)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.PROMPT:                 // RFC 2869 §5.10
                        return ((PROMPT)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.ERROR_CAUSE:            // RFC 5176 §3.6
                        return ((ERROR_CAUSE)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.FRAMED_MANAGEMENT_PROTOCOL:       // RFC 5607 §4.1
                        return ((FRAMED_MANAGEMENT_PROTOCOL)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.MANAGEMENT_TRANSPORT_PROTECTION:  // RFC 5607 §4.2
                        return ((MANAGEMENT_TRANSPORT_PROTECTION)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    case RadiusAttributeType.EAP_LOWER_LAYER:        // RFC 7057 §4.1
                        return ((EAP_LOWER_LAYER)BinaryPrimitives.ReadInt32BigEndian(dataSpan)).ToString();

                    // ── Tagged tunnel attributes (1-byte Tag + 3-byte big-endian value, RFC 2868 §3) ──
                    // When parsed from the wire, Data = [Tag(1), Value(3)] — 4 bytes total.
                    // When constructed via TunnelTypeAttributes/TunnelMediumTypeAttributes,
                    // Data = [Value(3)] — 3 bytes (Tag is only in the RawData header region).
                    // We handle both cases: if Data is 4 bytes, skip the Tag; if 3, read directly.

                    case RadiusAttributeType.TUNNEL_TYPE:             // RFC 2868 §3.1
                        return dataSpan.Length >= TUNNEL_TAG_SIZE + 3
                            ? ((TUNNEL_TYPE)RadiusUtils.ThreeBytesToUInt(dataSpan, TUNNEL_TAG_SIZE)).ToString()
                            : ((TUNNEL_TYPE)RadiusUtils.ThreeBytesToUInt(dataSpan, 0)).ToString();

                    case RadiusAttributeType.TUNNEL_MEDIUM_TYPE:     // RFC 2868 §3.2
                        return dataSpan.Length >= TUNNEL_TAG_SIZE + 3
                            ? ((TUNNEL_MEDIUM_TYPE)RadiusUtils.ThreeBytesToUInt(dataSpan, TUNNEL_TAG_SIZE)).ToString()
                            : ((TUNNEL_MEDIUM_TYPE)RadiusUtils.ThreeBytesToUInt(dataSpan, 0)).ToString();

                    // ── UTF-8 string attributes (RFC 2865 §5) ──

                    case RadiusAttributeType.USER_NAME:              // RFC 2865 §5.1
                    case RadiusAttributeType.FILTER_ID:              // RFC 2865 §5.11
                    case RadiusAttributeType.REPLY_MESSAGE:          // RFC 2865 §5.18
                    case RadiusAttributeType.CALLBACK_NUMBER:        // RFC 2865 §5.19
                    case RadiusAttributeType.CALLBACK_ID:            // RFC 2865 §5.20
                    case RadiusAttributeType.FRAMED_ROUTE:           // RFC 2865 §5.22
                    case RadiusAttributeType.CALLED_STATION_ID:      // RFC 2865 §5.30
                    case RadiusAttributeType.CALLING_STATION_ID:     // RFC 2865 §5.31
                    case RadiusAttributeType.NAS_IDENTIFIER:         // RFC 2865 §5.32
                    case RadiusAttributeType.LOGIN_LAT_SERVICE:      // RFC 2865 §5.34
                    case RadiusAttributeType.LOGIN_LAT_NODE:         // RFC 2865 §5.35
                    case RadiusAttributeType.ACCT_SESSION_ID:        // RFC 2866 §5.5
                    case RadiusAttributeType.ACCT_MULTI_SESSION_ID:  // RFC 2866 §5.11
                    case RadiusAttributeType.NAS_PORT_ID:            // RFC 2869 §5.17
                    case RadiusAttributeType.CONNECT_INFO:           // RFC 2869 §5.11
                    case RadiusAttributeType.FRAMED_IPV6_ROUTE:      // RFC 3162 §2.5
                    case RadiusAttributeType.FRAMED_POOL:            // RFC 2869 §5.18
                    case RadiusAttributeType.FRAMED_IPV6_POOL:       // RFC 3162 §2.6
                    case RadiusAttributeType.TUNNEL_CLIENT_ENDPOINT: // RFC 2868 §3.3 (tag + string, but tag is in first byte)
                    case RadiusAttributeType.TUNNEL_SERVER_ENDPOINT: // RFC 2868 §3.4
                    case RadiusAttributeType.TUNNEL_PRIVATE_GROUP_ID: // RFC 2868 §3.6
                    case RadiusAttributeType.TUNNEL_ASSIGNMENT_ID:   // RFC 2868 §3.7
                    case RadiusAttributeType.TUNNEL_CLIENT_AUTH_ID:  // RFC 2868 §3.9
                    case RadiusAttributeType.TUNNEL_SERVER_AUTH_ID:  // RFC 2868 §3.10
                        // Encoding.UTF8.GetString(ReadOnlySpan<byte>) avoids an array copy.
                        return Encoding.UTF8.GetString(dataSpan);

                    // ── Plain 32-bit integer attributes (RFC 2865 §5, RFC 2866 §5) ──

                    case RadiusAttributeType.NAS_PORT:               // RFC 2865 §5.5
                    case RadiusAttributeType.FRAMED_MTU:             // RFC 2865 §5.12
                    case RadiusAttributeType.LOGIN_TCP_PORT:         // RFC 2865 §5.16
                    case RadiusAttributeType.SESSION_TIMEOUT:        // RFC 2865 §5.27
                    case RadiusAttributeType.IDLE_TIMEOUT:           // RFC 2865 §5.28
                    case RadiusAttributeType.PORT_LIMIT:             // RFC 2865 §5.42
                    case RadiusAttributeType.FRAMED_IPX_NETWORK:     // RFC 2865 §5.23
                    case RadiusAttributeType.FRAMED_APPLETALK_LINK:  // RFC 2865 §5.37
                    case RadiusAttributeType.FRAMED_APPLETALK_NETWORK: // RFC 2865 §5.38
                    case RadiusAttributeType.ACCT_DELAY_TIME:        // RFC 2866 §5.2
                    case RadiusAttributeType.ACCT_INPUT_OCTETS:      // RFC 2866 §5.3
                    case RadiusAttributeType.ACCT_OUTPUT_OCTETS:     // RFC 2866 §5.4
                    case RadiusAttributeType.ACCT_SESSION_TIME:      // RFC 2866 §5.7
                    case RadiusAttributeType.ACCT_INPUT_PACKETS:     // RFC 2866 §5.8
                    case RadiusAttributeType.ACCT_OUTPUT_PACKETS:    // RFC 2866 §5.9
                    case RadiusAttributeType.ACCT_LINK_COUNT:        // RFC 2866 §5.12
                    case RadiusAttributeType.ACCT_INPUT_GIGAWORDS:   // RFC 2869 §5.1
                    case RadiusAttributeType.ACCT_OUTPUT_GIGAWORDS:  // RFC 2869 §5.2
                    case RadiusAttributeType.ACCT_INTERIM_INTERVAL:  // RFC 2869 §5.16
                    case RadiusAttributeType.ACCT_TUNNEL_PACKETS_LOST: // RFC 2867 §4.2
                    case RadiusAttributeType.TUNNEL_PREFERENCE:      // RFC 2868 §3.8
                    case RadiusAttributeType.MANAGEMENT_PRIVILEGE_LEVEL: // RFC 5607 §4.4
                        return BinaryPrimitives.ReadUInt32BigEndian(dataSpan).ToString(CultureInfo.InvariantCulture);

                    // ── Date/time attribute (32-bit Unix timestamp, RFC 2869 §5.3) ──

                    case RadiusAttributeType.EVENT_TIMESTAMP:        // RFC 2869 §5.3
                        uint unixTime = BinaryPrimitives.ReadUInt32BigEndian(dataSpan);
                        return DateTimeOffset.FromUnixTimeSeconds(unixTime)
                            .UtcDateTime.ToString("o", CultureInfo.InvariantCulture);

                    default:
                        // Convert.ToHexString is faster than BitConverter.ToString and
                        // produces uppercase hex without the '-' separators. Use
                        // BitConverter.ToString only if the dash-delimited legacy format
                        // must be preserved for callers that depend on it.
                        return BitConverter.ToString(Data);
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of <see cref="RadiusAttributes"/> with only the
        /// attribute type set. Intended for use by subclasses that populate all fields directly.
        /// </summary>
        /// <param name="type">
        /// The RADIUS attribute type code (RFC 2865 §5, octet 1).
        /// </param>
        protected RadiusAttributes(RadiusAttributeType type)
        {
            Type = type;
            RawData = [];
            Data = [];
        }

        /// <summary>
        /// Creates a RADIUS attribute with a raw byte array value.
        /// </summary>
        /// <remarks>
        /// The <paramref name="attributeValue"/> bytes are copied into an internally-owned
        /// <see cref="RawData"/> buffer. The caller's array is not retained, so subsequent
        /// mutations to the passed array will not affect this attribute instance.
        /// </remarks>
        /// <param name="attributeName">
        /// The RADIUS attribute type code identifying this attribute (RFC 2865 §5, octet 1).
        /// </param>
        /// <param name="attributeValue">
        /// The raw value bytes to encode. Must not be <see langword="null"/>. Maximum length
        /// is <c>253</c> bytes (the 255-byte attribute maximum minus the 2-byte header).
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="attributeValue"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="attributeValue"/> exceeds <c>253</c> bytes.
        /// </exception>
        public RadiusAttributes(RadiusAttributeType attributeName, byte[] attributeValue)
        {
            ArgumentNullException.ThrowIfNull(attributeValue);
            if (attributeValue.Length > MAX_VALUE_LENGTH)
                throw new ArgumentOutOfRangeException(
                    nameof(attributeValue), attributeValue.Length,
                    $"Attribute value must not exceed {MAX_VALUE_LENGTH} bytes.");

            Type = attributeName;

            // Allocate the TLV buffer once and copy the value bytes directly into it.
            Length = (byte)(attributeValue.Length + ATTRIBUTE_HEADER_SIZE);
            RawData = new byte[Length];
            RawData[0] = (byte)Type;
            RawData[1] = Length;
            attributeValue.AsSpan().CopyTo(RawData.AsSpan(ATTRIBUTE_HEADER_SIZE));

            // Data is a slice view into RawData's value region — zero extra allocation.
            // This ensures Data and RawData always share the same backing buffer,
            // and the caller's original array is not retained.
            Data = RawData[ATTRIBUTE_HEADER_SIZE..];
        }

        /// <summary>
        /// Creates a RADIUS attribute with a 32-bit signed integer value, encoded as a
        /// big-endian (network byte order) 4-byte sequence per RFC 2865 §5.
        /// </summary>
        /// <param name="attributeName">
        /// The RADIUS attribute type code identifying this attribute (RFC 2865 §5, octet 1).
        /// </param>
        /// <param name="attributeValue">
        /// The integer value to encode in big-endian byte order.
        /// </param>
        public RadiusAttributes(RadiusAttributeType attributeName, int attributeValue)
        {
            Type = attributeName;

            // Allocate exactly the final TLV size — header (2) + int payload (4) = 6 bytes.
            // Writing directly into RawData avoids the intermediate valueBytes array.
            Length = (byte)(sizeof(int) + ATTRIBUTE_HEADER_SIZE);
            RawData = new byte[Length];
            RawData[0] = (byte)Type;
            RawData[1] = Length;
            BinaryPrimitives.WriteInt32BigEndian(RawData.AsSpan(ATTRIBUTE_HEADER_SIZE), attributeValue);

            // Data is a slice view into RawData's value region — zero extra allocation.
            Data = RawData[ATTRIBUTE_HEADER_SIZE..];
        }

        /// <summary>
        /// Creates a RADIUS attribute carrying a <see cref="DateTime"/> value encoded as a
        /// 32-bit big-endian Unix timestamp (seconds since 1970-01-01 00:00:00 UTC),
        /// as used by the Event-Timestamp attribute (RFC 2869 §5.3).
        /// </summary>
        /// <param name="attributeName">
        /// The RADIUS attribute type code identifying this attribute (RFC 2865 §5, octet 1).
        /// </param>
        /// <param name="attributeValue">
        /// The date and time to encode. Values before the Unix epoch or after
        /// 2106-02-07 06:28:15 UTC will overflow the 32-bit unsigned timestamp field.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="attributeValue"/> produces a Unix timestamp that
        /// does not fit in a 32-bit unsigned integer (i.e. is before the Unix epoch or
        /// after 2106-02-07 06:28:15 UTC).
        /// </exception>
        public RadiusAttributes(RadiusAttributeType attributeName, DateTime attributeValue)
        {
            long unixSeconds = ((DateTimeOffset)attributeValue).ToUnixTimeSeconds();
            if (unixSeconds < 0 || unixSeconds > uint.MaxValue)
                throw new ArgumentOutOfRangeException(
                    nameof(attributeValue), attributeValue,
                    "DateTime value must produce a Unix timestamp in the range [0, 4294967295] " +
                    "(between 1970-01-01 and 2106-02-07 06:28:15 UTC).");

            Type = attributeName;

            // Allocate exactly the final TLV size — header (2) + uint payload (4) = 6 bytes.
            // Writing directly into RawData avoids the intermediate valueBytes array.
            Length = (byte)(sizeof(uint) + ATTRIBUTE_HEADER_SIZE);
            RawData = new byte[Length];
            RawData[0] = (byte)Type;
            RawData[1] = Length;
            BinaryPrimitives.WriteUInt32BigEndian(RawData.AsSpan(ATTRIBUTE_HEADER_SIZE), (uint)unixSeconds);

            // Data is a slice view into RawData's value region — zero extra allocation.
            Data = RawData[ATTRIBUTE_HEADER_SIZE..];
        }

        /// <summary>
        /// Creates a RADIUS attribute with a UTF-8 encoded string value.
        /// </summary>
        /// <param name="attributeName">
        /// The RADIUS attribute type code identifying this attribute (RFC 2865 §5, octet 1).
        /// </param>
        /// <param name="attributeValue">
        /// The string value to encode as UTF-8. Must not be <see langword="null"/>. The
        /// UTF-8 encoded byte length must not exceed <c>253</c> bytes.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="attributeValue"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the UTF-8 encoded form of <paramref name="attributeValue"/> exceeds
        /// <c>253</c> bytes.
        /// </exception>
        public RadiusAttributes(RadiusAttributeType attributeName, string attributeValue)
        {
            ArgumentNullException.ThrowIfNull(attributeValue);

            // Measure the UTF-8 byte count without allocating to perform the bounds check first.
            int byteCount = Encoding.UTF8.GetByteCount(attributeValue);
            if (byteCount > MAX_VALUE_LENGTH)
                throw new ArgumentOutOfRangeException(
                    nameof(attributeValue), byteCount,
                    $"UTF-8 encoded attribute value must not exceed {MAX_VALUE_LENGTH} bytes.");

            Type = attributeName;

            // Allocate the final TLV buffer once and encode the string directly into it,
            // eliminating the intermediate valueBytes array entirely.
            Length = (byte)(byteCount + ATTRIBUTE_HEADER_SIZE);
            RawData = new byte[Length];
            RawData[0] = (byte)Type;
            RawData[1] = Length;
            Encoding.UTF8.GetBytes(attributeValue, RawData.AsSpan(ATTRIBUTE_HEADER_SIZE));

            // Data is a slice view into RawData's value region — zero extra allocation.
            Data = RawData[ATTRIBUTE_HEADER_SIZE..];
        }

        /// <summary>
        /// Creates a RADIUS attribute with an <see cref="IPAddress"/> value, encoded as the
        /// address's raw byte representation (4 bytes for IPv4, 16 bytes for IPv6).
        /// </summary>
        /// <param name="attributeName">
        /// The RADIUS attribute type code identifying this attribute (RFC 2865 §5, octet 1).
        /// </param>
        /// <param name="attributeValue">
        /// The IP address to encode. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>)
        /// or IPv6 (<see cref="AddressFamily.InterNetworkV6"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="attributeValue"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown when <paramref name="attributeValue"/> is not an IPv4 or IPv6 address.
        /// </exception>
        public RadiusAttributes(RadiusAttributeType attributeName, IPAddress attributeValue)
        {
            ArgumentNullException.ThrowIfNull(attributeValue);

            if (attributeValue.AddressFamily != AddressFamily.InterNetwork &&
                attributeValue.AddressFamily != AddressFamily.InterNetworkV6)
            {
                throw new NotSupportedException(
                    $"Unsupported address family '{attributeValue.AddressFamily}'. " +
                    "Only IPv4 (InterNetwork) and IPv6 (InterNetworkV6) are supported.");
            }

            // IPv4 = 4 bytes, IPv6 = 16 bytes. Known at this point from AddressFamily.
            int addrByteCount = attributeValue.AddressFamily == AddressFamily.InterNetwork ? 4 : 16;

            Type = attributeName;

            // Allocate the final TLV buffer once and write the address bytes directly,
            // eliminating the intermediate GetAddressBytes() array allocation.
            Length = (byte)(addrByteCount + ATTRIBUTE_HEADER_SIZE);
            RawData = new byte[Length];
            RawData[0] = (byte)Type;
            RawData[1] = Length;

            // TryWriteBytes writes directly into the span — no intermediate byte[] allocation.
            attributeValue.TryWriteBytes(RawData.AsSpan(ATTRIBUTE_HEADER_SIZE), out _);

            // Data is a slice view into RawData's value region — zero extra allocation.
            Data = RawData[ATTRIBUTE_HEADER_SIZE..];
        }

        /// <summary>
        /// Creates a RADIUS attribute carrying a 64-bit signed integer value, encoded as a
        /// big-endian (network byte order) 8-byte sequence per RFC 8044 §3.3 (integer64).
        /// </summary>
        /// <param name="attributeName">
        /// The RADIUS attribute type code identifying this attribute (RFC 2865 §5, octet 1).
        /// </param>
        /// <param name="attributeValue">
        /// The 64-bit signed integer value to encode in big-endian byte order.
        /// </param>
        public RadiusAttributes(RadiusAttributeType attributeName, long attributeValue)
        {
            Type = attributeName;

            // Allocate exactly the final TLV size — header (2) + long payload (8) = 10 bytes.
            // Writing directly into RawData avoids the intermediate valueBytes array.
            Length = (byte)(sizeof(long) + ATTRIBUTE_HEADER_SIZE);
            RawData = new byte[Length];
            RawData[0] = (byte)Type;
            RawData[1] = Length;
            BinaryPrimitives.WriteInt64BigEndian(RawData.AsSpan(ATTRIBUTE_HEADER_SIZE), attributeValue);

            // Data is a slice view into RawData's value region — zero extra allocation.
            Data = RawData[ATTRIBUTE_HEADER_SIZE..];
        }

        #endregion

        #region Static Factory Methods

        /// <summary>
        /// Creates a RADIUS attribute carrying an IPv4 prefix value, encoded as a 6-byte
        /// sequence per RFC 8044 §3.9: 1 reserved byte, 1 prefix-length byte (0–32),
        /// and 4 bytes of the masked IPv4 network address in network byte order.
        /// </summary>
        /// <param name="type">
        /// The RADIUS attribute type code identifying this attribute (RFC 2865 §5, octet 1).
        /// </param>
        /// <param name="address">
        /// The IPv4 network address. Must be an <see cref="AddressFamily.InterNetwork"/> address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <param name="prefixLength">
        /// The prefix length in bits. Must be in the range [0, 32].
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="address"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="address"/> is not an IPv4 address.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="prefixLength"/> is not in the range [0, 32].
        /// </exception>
        public static RadiusAttributes CreateIpv4Prefix(RadiusAttributeType type, IPAddress address, byte prefixLength)
        {
            ArgumentNullException.ThrowIfNull(address);

            if (address.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException(
                    $"Expected an IPv4 (InterNetwork) address, got '{address.AddressFamily}'.",
                    nameof(address));

            if (prefixLength > 32)
                throw new ArgumentOutOfRangeException(
                    nameof(prefixLength), prefixLength,
                    "IPv4 prefix length must be in the range [0, 32].");

            // Use a stackalloc span to read and mask the IPv4 address without any heap allocation.
            // TryWriteBytes writes 4 bytes for IPv4 directly into the stack buffer.
            Span<byte> addrBytes = stackalloc byte[4];
            address.TryWriteBytes(addrBytes, out _);

            // Mask host bits so only the network portion is encoded (RFC 8044 §3.9).
            if (prefixLength < 32)
            {
                uint mask = prefixLength == 0 ? 0u : ~((1u << (32 - prefixLength)) - 1u);
                uint masked = BinaryPrimitives.ReadUInt32BigEndian(addrBytes) & mask;
                BinaryPrimitives.WriteUInt32BigEndian(addrBytes, masked);
            }

            // Wire format: 1 reserved byte + 1 prefix-length byte + 4 address bytes = 6 bytes.
            // Build in a stack buffer and hand off to the byte[] constructor — one heap allocation.
            Span<byte> valueBytes = stackalloc byte[6];
            valueBytes[0] = 0; // reserved
            valueBytes[1] = prefixLength;
            addrBytes.CopyTo(valueBytes[2..]);

            return new RadiusAttributes(type, valueBytes.ToArray());
        }

        /// <summary>
        /// Creates a RADIUS attribute carrying an IPv6 prefix value, encoded per RFC 8044 §3.8
        /// and RFC 3162 §2.3: 1 reserved byte, 1 prefix-length byte (0–128), followed by
        /// the minimal number of bytes required to represent the masked prefix (ceiling of
        /// prefix-length / 8), with trailing host bits zeroed.
        /// </summary>
        /// <param name="type">
        /// The RADIUS attribute type code identifying this attribute (RFC 2865 §5, octet 1).
        /// </param>
        /// <param name="address">
        /// The IPv6 network address. Must be an <see cref="AddressFamily.InterNetworkV6"/> address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <param name="prefixLength">
        /// The prefix length in bits. Must be in the range [0, 128].
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="address"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="address"/> is not an IPv6 address.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="prefixLength"/> is not in the range [0, 128].
        /// </exception>
        public static RadiusAttributes CreateIpv6Prefix(RadiusAttributeType type, IPAddress address, byte prefixLength)
        {
            ArgumentNullException.ThrowIfNull(address);

            if (address.AddressFamily != AddressFamily.InterNetworkV6)
                throw new ArgumentException(
                    $"Expected an IPv6 (InterNetworkV6) address, got '{address.AddressFamily}'.",
                    nameof(address));

            if (prefixLength > 128)
                throw new ArgumentOutOfRangeException(
                    nameof(prefixLength), prefixLength,
                    "IPv6 prefix length must be in the range [0, 128].");

            // Use a stackalloc span — IPv6 is always 16 bytes, well within stack safety limits.
            Span<byte> addrBytes = stackalloc byte[16];
            address.TryWriteBytes(addrBytes, out _);

            // Zero host bits beyond the prefix length.
            for (int i = prefixLength; i < 128; i++)
            {
                addrBytes[i / 8] &= (byte)~(1 << (7 - i % 8));
            }

            // Only encode the bytes needed to carry the prefix (RFC 3162 §2.3).
            int prefixBytes = (prefixLength + 7) / 8;

            // Wire format: 1 reserved byte + 1 prefix-length byte + prefixBytes of address.
            // Build in a stack buffer (max 2 + 16 = 18 bytes) — one final heap allocation.
            Span<byte> valueBytes = stackalloc byte[2 + prefixBytes];
            valueBytes[0] = 0; // reserved
            valueBytes[1] = prefixLength;
            addrBytes[..prefixBytes].CopyTo(valueBytes[2..]);

            return new RadiusAttributes(type, valueBytes.ToArray());
        }

        #endregion
    }
}