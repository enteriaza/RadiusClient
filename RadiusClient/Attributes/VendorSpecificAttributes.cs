using System.Buffers.Binary;

namespace Radius.Attributes
{
    /// <summary>
    /// Describes the sub-attribute encoding format used within a Vendor-Specific Attribute (VSA).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Most vendors use the standard format defined in RFC 2865 §5.26 (1-byte type, 1-byte length).
    /// Some vendors use non-standard formats with wider type fields, wider or omitted length
    /// fields, or a continuation byte for long attributes.
    /// </para>
    /// <para>
    /// The naming convention follows <c>Type{typeBytes}Len{lengthBytes}</c>, with an optional
    /// <c>Continuation</c> suffix when the format includes a continuation flag byte
    /// (RFC 6929 §2.4, used by WiMAX).
    /// </para>
    /// <para>
    /// See RFC 2865 §5.26, RFC 6929 §2.4, and the FreeRADIUS <c>dictionary.format</c>
    /// documentation for wire-format details of each variant.
    /// </para>
    /// </remarks>
    public enum VendorSpecificFormat
    {
        /// <summary>
        /// Standard VSA sub-attribute format (RFC 2865 §5.26): 1-byte Vendor-Type, 1-byte Vendor-Length.
        /// <para>On-wire layout: <c>[Vendor-Type(1)][Vendor-Length(1)][Vendor-Data(n)]</c></para>
        /// <para>FreeRADIUS dictionary notation: <c>format=1,1</c></para>
        /// </summary>
        Type1Len1 = 0,

        /// <summary>
        /// 1-byte Vendor-Type with no Vendor-Length field.
        /// <para>On-wire layout: <c>[Vendor-Type(1)][Vendor-Data(n)]</c></para>
        /// <para>FreeRADIUS dictionary notation: <c>format=1,0</c></para>
        /// </summary>
        Type1Len0 = 1,

        /// <summary>
        /// 2-byte Vendor-Type, 1-byte Vendor-Length.
        /// <para>On-wire layout: <c>[Vendor-Type(2)][Vendor-Length(1)][Vendor-Data(n)]</c></para>
        /// <para>FreeRADIUS dictionary notation: <c>format=2,1</c></para>
        /// </summary>
        Type2Len1 = 2,

        /// <summary>
        /// 2-byte Vendor-Type with no Vendor-Length field.
        /// <para>On-wire layout: <c>[Vendor-Type(2)][Vendor-Data(n)]</c></para>
        /// <para>FreeRADIUS dictionary notation: <c>format=2,0</c></para>
        /// </summary>
        Type2Len0 = 3,

        /// <summary>
        /// 2-byte Vendor-Type, 2-byte Vendor-Length.
        /// <para>On-wire layout: <c>[Vendor-Type(2)][Vendor-Length(2)][Vendor-Data(n)]</c></para>
        /// <para>FreeRADIUS dictionary notation: <c>format=2,2</c></para>
        /// </summary>
        Type2Len2 = 4,

        /// <summary>
        /// 4-byte Vendor-Type with no Vendor-Length field.
        /// <para>On-wire layout: <c>[Vendor-Type(4)][Vendor-Data(n)]</c></para>
        /// <para>FreeRADIUS dictionary notation: <c>format=4,0</c></para>
        /// <para>Used by US Robotics / 3Com (PEN 429).</para>
        /// </summary>
        Type4Len0 = 5,

        /// <summary>
        /// 4-byte Vendor-Type, 1-byte Vendor-Length.
        /// <para>On-wire layout: <c>[Vendor-Type(4)][Vendor-Length(1)][Vendor-Data(n)]</c></para>
        /// <para>FreeRADIUS dictionary notation: <c>format=4,1</c></para>
        /// </summary>
        Type4Len1 = 6,

        /// <summary>
        /// 4-byte Vendor-Type, 2-byte Vendor-Length.
        /// <para>On-wire layout: <c>[Vendor-Type(4)][Vendor-Length(2)][Vendor-Data(n)]</c></para>
        /// <para>FreeRADIUS dictionary notation: <c>format=4,2</c></para>
        /// </summary>
        Type4Len2 = 7,

        /// <summary>
        /// 1-byte Vendor-Type, 1-byte Vendor-Length, plus a 1-byte continuation flag
        /// (RFC 6929 §2.4). The high bit of the continuation byte indicates whether
        /// additional fragments follow.
        /// <para>On-wire layout: <c>[Vendor-Type(1)][Vendor-Length(1)][Continuation(1)][Vendor-Data(n)]</c></para>
        /// <para>FreeRADIUS dictionary notation: <c>format=1,1,c</c></para>
        /// <para>Used by the WiMAX Forum (PEN 24757).</para>
        /// </summary>
        Type1Len1Continuation = 8
    }

    /// <summary>
    /// Represents a RADIUS Vendor-Specific Attribute (VSA), as defined in RFC 2865 §5.26.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A Vendor-Specific Attribute encapsulates vendor-defined sub-attributes within the
    /// standard RADIUS attribute TLV envelope (Type 26). The standard on-wire layout is:
    /// </para>
    /// <list type="table">
    ///   <listheader><term>Field</term><description>Size / Description</description></listheader>
    ///   <item><term>Type</term><description>1 byte — always <c>26</c> (<see cref="RadiusAttributeType.VENDOR_SPECIFIC"/>).</description></item>
    ///   <item><term>Length</term><description>1 byte — total length of the entire attribute including Type and Length fields.</description></item>
    ///   <item><term>Vendor-Id</term><description>4 bytes — IANA-assigned Private Enterprise Number (big-endian). See http://www.iana.org/assignments/enterprise-numbers.</description></item>
    ///   <item><term>Vendor sub-attribute</term><description>Variable — encoding depends on <see cref="Format"/>.</description></item>
    /// </list>
    /// <para>
    /// Supported sub-attribute encoding formats (see <see cref="VendorSpecificFormat"/>):
    /// </para>
    /// <list type="bullet">
    ///   <item><description><b>format=1,1</b> (Standard, RFC 2865 §5.26): 1-byte type, 1-byte length.</description></item>
    ///   <item><description><b>format=1,0</b>: 1-byte type, no length.</description></item>
    ///   <item><description><b>format=2,1</b>: 2-byte type, 1-byte length.</description></item>
    ///   <item><description><b>format=2,0</b>: 2-byte type, no length.</description></item>
    ///   <item><description><b>format=2,2</b>: 2-byte type, 2-byte length.</description></item>
    ///   <item><description><b>format=4,0</b>: 4-byte type, no length (e.g. US Robotics PEN 429).</description></item>
    ///   <item><description><b>format=4,1</b>: 4-byte type, 1-byte length.</description></item>
    ///   <item><description><b>format=4,2</b>: 4-byte type, 2-byte length.</description></item>
    ///   <item><description><b>format=1,1,c</b> (RFC 6929 §2.4): 1-byte type, 1-byte length, 1-byte continuation flag (WiMAX).</description></item>
    /// </list>
    /// </remarks>
    public sealed class VendorSpecificAttributes : RadiusAttributes
    {
        #region Constants

        /// <summary>Byte offset of the Vendor-Id field within the outer VSA TLV (after Type and Length).</summary>
        private const int VSA_ID_INDEX = 2;

        /// <summary>Byte offset of the vendor sub-attribute header within the outer VSA TLV.</summary>
        private const int VSA_SUBATTR_INDEX = 6;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the vendor-defined sub-attribute type byte, identifying the specific VSA
        /// within the vendor's namespace. For standard-format VSAs only.
        /// For extended-format VSAs, use <see cref="VendorSpecificTypeExtended"/>.
        /// </summary>
        public byte VendorSpecificType { get; }

        /// <summary>
        /// Gets the vendor-defined sub-attribute type as a 32-bit value.
        /// For standard-format VSAs this equals <see cref="VendorSpecificType"/>.
        /// For extended-format VSAs (e.g. USR <c>format=4,0</c>) this holds the full type code.
        /// </summary>
        public uint VendorSpecificTypeExtended { get; }

        /// <summary>
        /// Gets the Vendor-Length field value from the sub-attribute header.
        /// For formats with a 1-byte length, this is the raw byte value.
        /// For formats with a 2-byte length, use <see cref="VendorSpecificLengthExtended"/>.
        /// For formats without a length field, this value is <c>0</c>.
        /// </summary>
        public byte VendorSpecificLength { get; }

        /// <summary>
        /// Gets the Vendor-Length field value as a 16-bit value.
        /// For formats with a 2-byte length field (<c>format=2,2</c> or <c>format=4,2</c>),
        /// this holds the full 16-bit length. For all other formats this equals
        /// <see cref="VendorSpecificLength"/>.
        /// </summary>
        public ushort VendorSpecificLengthExtended { get; }

        /// <summary>
        /// Gets the IANA-assigned Private Enterprise Number (Vendor-Id) identifying the vendor,
        /// as defined in RFC 2865 §5.26.
        /// </summary>
        public uint VendorId { get; }

        /// <summary>
        /// Gets the sub-attribute encoding format used by this VSA instance.
        /// </summary>
        public VendorSpecificFormat Format { get; }

        /// <summary>
        /// Gets the continuation flag byte for <see cref="VendorSpecificFormat.Type1Len1Continuation"/>
        /// format VSAs (RFC 6929 §2.4). The high bit (<c>0x80</c>) indicates whether additional
        /// fragments follow. For all other formats this value is <c>0</c>.
        /// </summary>
        public byte ContinuationFlag { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an outbound Vendor-Specific Attribute with the specified Vendor-Id,
        /// sub-attribute type (1-byte), and sub-attribute value, using the standard
        /// RFC 2865 §5.26 encoding (1-byte type, 1-byte length).
        /// </summary>
        /// <param name="vendorId">
        /// The IANA-assigned Private Enterprise Number identifying the vendor.
        /// </param>
        /// <param name="vendorSpecificType">
        /// The vendor-defined sub-attribute type byte.
        /// </param>
        /// <param name="vendorSpecificData">
        /// The raw value bytes of the vendor sub-attribute. Must not be <see langword="null"/>.
        /// The maximum length is <c>247</c> bytes (255 − 8 bytes of VSA header overhead).
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="vendorSpecificData"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="vendorSpecificData"/> exceeds the maximum allowed length.
        /// </exception>
        public VendorSpecificAttributes(uint vendorId, byte vendorSpecificType, byte[] vendorSpecificData)
            : this(vendorId, vendorSpecificType, vendorSpecificData, VendorSpecificFormat.Type1Len1)
        {
        }

        /// <summary>
        /// Constructs an outbound Vendor-Specific Attribute with the specified Vendor-Id,
        /// sub-attribute type (up to 32-bit), sub-attribute value, and encoding format.
        /// </summary>
        /// <param name="vendorId">
        /// The IANA-assigned Private Enterprise Number identifying the vendor.
        /// </param>
        /// <param name="vendorSpecificType">
        /// The vendor-defined sub-attribute type as a 32-bit value.
        /// </param>
        /// <param name="vendorSpecificData">
        /// The raw value bytes of the vendor sub-attribute. Must not be <see langword="null"/>.
        /// </param>
        /// <param name="format">
        /// The sub-attribute encoding format to use on the wire.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="vendorSpecificData"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="vendorSpecificData"/> exceeds the maximum allowed length
        /// for the specified <paramref name="format"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="format"/> is not a recognised <see cref="VendorSpecificFormat"/> value.
        /// </exception>
        public VendorSpecificAttributes(uint vendorId, uint vendorSpecificType, byte[] vendorSpecificData, VendorSpecificFormat format)
            : this(vendorId, vendorSpecificType, vendorSpecificData, format, continuationFlag: 0x00)
        {
        }

        /// <summary>
        /// Constructs an outbound Vendor-Specific Attribute with the specified Vendor-Id,
        /// sub-attribute type, sub-attribute value, encoding format, and continuation flag.
        /// </summary>
        /// <remarks>
        /// The <paramref name="continuationFlag"/> parameter is only meaningful for
        /// <see cref="VendorSpecificFormat.Type1Len1Continuation"/>. For all other formats
        /// it is ignored and stored as <c>0</c>.
        /// </remarks>
        /// <param name="vendorId">
        /// The IANA-assigned Private Enterprise Number identifying the vendor.
        /// </param>
        /// <param name="vendorSpecificType">
        /// The vendor-defined sub-attribute type as a 32-bit value.
        /// </param>
        /// <param name="vendorSpecificData">
        /// The raw value bytes of the vendor sub-attribute. Must not be <see langword="null"/>.
        /// </param>
        /// <param name="format">
        /// The sub-attribute encoding format to use on the wire.
        /// </param>
        /// <param name="continuationFlag">
        /// The continuation flag byte for <see cref="VendorSpecificFormat.Type1Len1Continuation"/>
        /// format. The high bit (<c>0x80</c>) indicates more fragments follow. Ignored for
        /// other formats.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="vendorSpecificData"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="vendorSpecificData"/> exceeds the maximum allowed length
        /// for the specified <paramref name="format"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="format"/> is not a recognised <see cref="VendorSpecificFormat"/> value.
        /// </exception>
        public VendorSpecificAttributes(uint vendorId, uint vendorSpecificType, byte[] vendorSpecificData, VendorSpecificFormat format, byte continuationFlag)
            : base(RadiusAttributeType.VENDOR_SPECIFIC)
        {
            ArgumentNullException.ThrowIfNull(vendorSpecificData);

            Format = format;
            VendorId = vendorId;
            VendorSpecificTypeExtended = vendorSpecificType;

            // Compute per-format field sizes.
            int typeFieldSize = GetTypeFieldSize(format);
            int lengthFieldSize = GetLengthFieldSize(format);
            int continuationFieldSize = format == VendorSpecificFormat.Type1Len1Continuation ? 1 : 0;
            int subHeaderSize = typeFieldSize + lengthFieldSize + continuationFieldSize;
            int dataIndex = VSA_SUBATTR_INDEX + subHeaderSize;
            int maxDataLength = 255 - dataIndex;

            if (vendorSpecificData.Length > maxDataLength)
                throw new ArgumentOutOfRangeException(
                    nameof(vendorSpecificData),
                    vendorSpecificData.Length,
                    $"Vendor-specific data must not exceed {maxDataLength} bytes for {format}.");

            // Populate type properties.
            VendorSpecificType = (byte)(vendorSpecificType & 0xFF);

            // Populate length properties.
            if (lengthFieldSize > 0)
            {
                int vendorLength = lengthFieldSize + typeFieldSize + continuationFieldSize + vendorSpecificData.Length;
                VendorSpecificLength = (byte)(vendorLength & 0xFF);
                VendorSpecificLengthExtended = (ushort)vendorLength;
            }

            // Populate continuation flag.
            ContinuationFlag = format == VendorSpecificFormat.Type1Len1Continuation ? continuationFlag : (byte)0;

            // Allocate the single TLV buffer once.
            Length = (byte)(dataIndex + vendorSpecificData.Length);
            RawData = new byte[Length];

            // Outer RADIUS attribute header.
            RawData[0] = (byte)Type;
            RawData[1] = Length;

            // Vendor-Id: 4-byte big-endian.
            BinaryPrimitives.WriteUInt32BigEndian(RawData.AsSpan(VSA_ID_INDEX), vendorId);

            // Vendor sub-attribute header: type field.
            int pos = VSA_SUBATTR_INDEX;
            switch (typeFieldSize)
            {
                case 1:
                    RawData[pos] = VendorSpecificType;
                    break;
                case 2:
                    BinaryPrimitives.WriteUInt16BigEndian(RawData.AsSpan(pos), (ushort)vendorSpecificType);
                    break;
                case 4:
                    BinaryPrimitives.WriteUInt32BigEndian(RawData.AsSpan(pos), vendorSpecificType);
                    break;
            }
            pos += typeFieldSize;

            // Vendor sub-attribute header: length field.
            switch (lengthFieldSize)
            {
                case 1:
                    RawData[pos] = VendorSpecificLength;
                    break;
                case 2:
                    BinaryPrimitives.WriteUInt16BigEndian(RawData.AsSpan(pos), VendorSpecificLengthExtended);
                    break;
            }
            pos += lengthFieldSize;

            // Continuation flag (only for Type1Len1Continuation).
            if (continuationFieldSize > 0)
            {
                RawData[pos] = ContinuationFlag;
                pos += continuationFieldSize;
            }

            // Vendor-Data.
            vendorSpecificData.AsSpan().CopyTo(RawData.AsSpan(pos));

            // Data is a slice view into RawData's vendor-data region.
            Data = RawData[dataIndex..];
        }

        /// <summary>
        /// Parses a Vendor-Specific Attribute from a raw packet buffer at the specified offset,
        /// using the standard RFC 2865 §5.26 encoding (1-byte type, 1-byte length).
        /// </summary>
        /// <param name="rawData">
        /// The raw byte buffer containing the VSA TLV. Must not be <see langword="null"/>.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset within <paramref name="rawData"/> at which the outer
        /// VSA TLV begins (the Type byte). Must be non-negative.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="rawData"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="offset"/> is negative, when the buffer is too short,
        /// or when the Vendor-Length field is inconsistent with the available data.
        /// </exception>
        public VendorSpecificAttributes(byte[] rawData, int offset)
            : this(rawData, offset, VendorSpecificFormat.Type1Len1)
        {
        }

        /// <summary>
        /// Parses a Vendor-Specific Attribute from a raw packet buffer at the specified offset,
        /// using the specified sub-attribute encoding format.
        /// </summary>
        /// <param name="rawData">
        /// The raw byte buffer containing the VSA TLV. Must not be <see langword="null"/>.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset within <paramref name="rawData"/> at which the outer
        /// VSA TLV begins (the Type byte). Must be non-negative.
        /// </param>
        /// <param name="format">
        /// The sub-attribute encoding format used by the vendor.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="rawData"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="offset"/> is negative, when the buffer is too short,
        /// or when the length field is inconsistent with the available data.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="format"/> is not a recognised <see cref="VendorSpecificFormat"/> value.
        /// </exception>
        public VendorSpecificAttributes(byte[] rawData, int offset, VendorSpecificFormat format)
            : base(RadiusAttributeType.VENDOR_SPECIFIC)
        {
            ArgumentNullException.ThrowIfNull(rawData);

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), offset,
                    "Offset must be non-negative.");

            Format = format;

            int typeFieldSize = GetTypeFieldSize(format);
            int lengthFieldSize = GetLengthFieldSize(format);
            int continuationFieldSize = format == VendorSpecificFormat.Type1Len1Continuation ? 1 : 0;
            int subHeaderSize = typeFieldSize + lengthFieldSize + continuationFieldSize;
            int dataIndex = VSA_SUBATTR_INDEX + subHeaderSize;

            if (rawData.Length < offset + dataIndex)
                throw new ArgumentOutOfRangeException(nameof(rawData),
                    $"Buffer is too short to contain a {format} VSA header at offset {offset}. " +
                    $"Required: {offset + dataIndex} bytes, available: {rawData.Length}.");

            // Read Vendor-Id.
            VendorId = BinaryPrimitives.ReadUInt32BigEndian(rawData.AsSpan(offset + VSA_ID_INDEX));

            // Read Vendor-Type.
            int pos = offset + VSA_SUBATTR_INDEX;
            VendorSpecificTypeExtended = typeFieldSize switch
            {
                1 => rawData[pos],
                2 => BinaryPrimitives.ReadUInt16BigEndian(rawData.AsSpan(pos)),
                4 => BinaryPrimitives.ReadUInt32BigEndian(rawData.AsSpan(pos)),
                _ => rawData[pos]
            };
            VendorSpecificType = (byte)(VendorSpecificTypeExtended & 0xFF);
            pos += typeFieldSize;

            // Read Vendor-Length (if present) and compute data length.
            int dataLength;

            if (lengthFieldSize > 0)
            {
                VendorSpecificLengthExtended = lengthFieldSize switch
                {
                    1 => rawData[pos],
                    2 => BinaryPrimitives.ReadUInt16BigEndian(rawData.AsSpan(pos)),
                    _ => rawData[pos]
                };
                VendorSpecificLength = (byte)(VendorSpecificLengthExtended & 0xFF);
                pos += lengthFieldSize;

                // Data length = vendor-length minus the sub-attribute header fields that
                // are counted within the vendor-length.
                dataLength = VendorSpecificLengthExtended - subHeaderSize;
                if (dataLength < 0 || rawData.Length < offset + dataIndex + dataLength)
                    throw new ArgumentOutOfRangeException(nameof(rawData),
                        $"Vendor-Length field value {VendorSpecificLengthExtended} is inconsistent " +
                        $"with the available buffer data at offset {offset}.");
            }
            else
            {
                // No length field — infer data length from the outer RADIUS Length field.
                byte outerLength = rawData[offset + 1];
                dataLength = outerLength - dataIndex;
                if (dataLength < 0 || rawData.Length < offset + outerLength)
                    throw new ArgumentOutOfRangeException(nameof(rawData),
                        $"Outer Length field value {outerLength} is inconsistent " +
                        $"with the available buffer data at offset {offset}.");
            }

            // Read continuation flag (if present).
            if (continuationFieldSize > 0)
            {
                ContinuationFlag = rawData[pos];
                pos += continuationFieldSize;
            }

            // Set outer attribute length and copy RawData.
            Length = (byte)(dataIndex + dataLength);
            RawData = rawData.AsSpan(offset, Length).ToArray();

            // Data is a slice view into RawData's vendor-data region.
            Data = RawData[dataIndex..];
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Returns the Vendor-Type field size in bytes for the given format.
        /// </summary>
        private static int GetTypeFieldSize(VendorSpecificFormat format) => format switch
        {
            VendorSpecificFormat.Type1Len1 => 1,
            VendorSpecificFormat.Type1Len0 => 1,
            VendorSpecificFormat.Type1Len1Continuation => 1,
            VendorSpecificFormat.Type2Len1 => 2,
            VendorSpecificFormat.Type2Len0 => 2,
            VendorSpecificFormat.Type2Len2 => 2,
            VendorSpecificFormat.Type4Len0 => 4,
            VendorSpecificFormat.Type4Len1 => 4,
            VendorSpecificFormat.Type4Len2 => 4,
            _ => throw new ArgumentException($"Unrecognised vendor-specific format '{format}'.", nameof(format))
        };

        /// <summary>
        /// Returns the Vendor-Length field size in bytes for the given format.
        /// </summary>
        private static int GetLengthFieldSize(VendorSpecificFormat format) => format switch
        {
            VendorSpecificFormat.Type1Len1 => 1,
            VendorSpecificFormat.Type1Len0 => 0,
            VendorSpecificFormat.Type1Len1Continuation => 1,
            VendorSpecificFormat.Type2Len1 => 1,
            VendorSpecificFormat.Type2Len0 => 0,
            VendorSpecificFormat.Type2Len2 => 2,
            VendorSpecificFormat.Type4Len0 => 0,
            VendorSpecificFormat.Type4Len1 => 1,
            VendorSpecificFormat.Type4Len2 => 2,
            _ => throw new ArgumentException($"Unrecognised vendor-specific format '{format}'.", nameof(format))
        };

        #endregion
    }
}