using System.Buffers.Binary;

namespace Radius.Attributes
{
    /// <summary>
    /// Represents a RADIUS Vendor-Specific Attribute (VSA), as defined in RFC 2865 §5.26.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A Vendor-Specific Attribute encapsulates vendor-defined sub-attributes within the
    /// standard RADIUS attribute TLV envelope (Type 26). The on-wire layout is:
    /// </para>
    /// <list type="table">
    ///   <listheader><term>Field</term><description>Size / Description</description></listheader>
    ///   <item><term>Type</term><description>1 byte — always <c>26</c> (<see cref="RadiusAttributeType.VENDOR_SPECIFIC"/>).</description></item>
    ///   <item><term>Length</term><description>1 byte — total length of the entire attribute including Type and Length fields.</description></item>
    ///   <item><term>Vendor-Id</term><description>4 bytes — IANA-assigned Private Enterprise Number (big-endian). See http://www.iana.org/assignments/enterprise-numbers.</description></item>
    ///   <item><term>Vendor-Type</term><description>1 byte — vendor-defined sub-attribute type.</description></item>
    ///   <item><term>Vendor-Length</term><description>1 byte — length of the sub-attribute including Vendor-Type and Vendor-Length fields.</description></item>
    ///   <item><term>Vendor-Data</term><description>Variable — the vendor-defined sub-attribute value.</description></item>
    /// </list>
    /// <para>
    /// Use <see cref="VendorSpecificAttributes(uint, byte, byte[])"/> to construct an outbound VSA.
    /// Use <see cref="VendorSpecificAttributes(byte[], int)"/> to parse a VSA from a received packet buffer.
    /// </para>
    /// </remarks>
    public sealed class VendorSpecificAttributes : RadiusAttributes
    {
        #region Constants

        /// <summary>Byte offset of the Vendor-Id field within the outer VSA TLV (after Type and Length).</summary>
        private const int VSA_ID_INDEX = 2;

        /// <summary>Byte offset of the Vendor-Type field within the outer VSA TLV.</summary>
        private const int VSA_TYPE_INDEX = 6;

        /// <summary>Byte offset of the Vendor-Length field within the outer VSA TLV.</summary>
        private const int VSA_LENGTH_INDEX = 7;

        /// <summary>Byte offset of the Vendor-Data field within the outer VSA TLV.</summary>
        private const int VSA_DATA_INDEX = 8;

        /// <summary>
        /// Maximum permitted length of Vendor-Data in bytes (255 − 8 bytes of VSA header overhead).
        /// </summary>
        private const int MAX_VENDOR_DATA_LENGTH = 255 - VSA_DATA_INDEX;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the vendor-defined sub-attribute type byte, identifying the specific VSA
        /// within the vendor's namespace.
        /// </summary>
        public byte VendorSpecificType { get; }

        /// <summary>
        /// Gets the Vendor-Length field value from the sub-attribute header.
        /// This is the length of the sub-attribute including the Vendor-Type and Vendor-Length
        /// bytes (i.e. <c>Data.Length + 2</c>).
        /// </summary>
        public byte VendorSpecificLength { get; }

        /// <summary>
        /// Gets the IANA-assigned Private Enterprise Number (Vendor-Id) identifying the vendor,
        /// as defined in RFC 2865 §5.26.
        /// </summary>
        public uint VendorId { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an outbound Vendor-Specific Attribute with the specified Vendor-Id,
        /// sub-attribute type, and sub-attribute value, as defined in RFC 2865 §5.26.
        /// </summary>
        /// <param name="vendorId">
        /// The IANA-assigned Private Enterprise Number identifying the vendor.
        /// See http://www.iana.org/assignments/enterprise-numbers.
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
            : base(RadiusAttributeType.VENDOR_SPECIFIC)
        {
            ArgumentNullException.ThrowIfNull(vendorSpecificData);

            if (vendorSpecificData.Length > MAX_VENDOR_DATA_LENGTH)
                throw new ArgumentOutOfRangeException(
                    nameof(vendorSpecificData),
                    vendorSpecificData.Length,
                    $"Vendor-specific data must not exceed {MAX_VENDOR_DATA_LENGTH} bytes.");

            VendorId = vendorId;
            VendorSpecificType = vendorSpecificType;

            // Compute the Vendor-Length field: Vendor-Type(1) + Vendor-Length(1) + Data(n).
            VendorSpecificLength = (byte)(ATTRIBUTE_HEADER_SIZE + vendorSpecificData.Length);

            // Allocate the single TLV buffer once — all fields are written directly into it.
            // Layout: [Type(1)][Length(1)][Vendor-Id(4)][Vendor-Type(1)][Vendor-Length(1)][Vendor-Data(n)]
            Length = (byte)(VSA_DATA_INDEX + vendorSpecificData.Length);
            RawData = new byte[Length];

            // Outer RADIUS attribute header: Type (1 byte) + Length (1 byte).
            RawData[0] = (byte)Type;
            RawData[1] = Length;

            // Vendor-Id: 4-byte big-endian IANA Private Enterprise Number (RFC 2865 §5.26).
            BinaryPrimitives.WriteUInt32BigEndian(RawData.AsSpan(VSA_ID_INDEX), vendorId);

            // Vendor sub-attribute header: Vendor-Type + Vendor-Length.
            RawData[VSA_TYPE_INDEX] = vendorSpecificType;
            RawData[VSA_LENGTH_INDEX] = VendorSpecificLength;

            // Vendor-Data: copy the sub-attribute value bytes into the final buffer.
            vendorSpecificData.AsSpan().CopyTo(RawData.AsSpan(VSA_DATA_INDEX));

            // Data is a slice view into RawData's vendor-data region — zero extra allocation.
            // This ensures Data cannot diverge from RawData and matches the pattern used by
            // RadiusAttributes, TunnelMediumTypeAttributes, and TunnelTypeAttributes.
            Data = RawData[VSA_DATA_INDEX..];
        }

        /// <summary>
        /// Parses a Vendor-Specific Attribute from a raw packet buffer at the specified offset,
        /// as defined in RFC 2865 §5.26.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <paramref name="offset"/> must point to the first byte of the outer VSA TLV
        /// (i.e. the Type byte). The buffer must contain at least <c>offset + 8</c>
        /// bytes to accommodate the full VSA header.
        /// </para>
        /// <para>
        /// The Vendor-Length field embedded in the sub-attribute header is validated against
        /// the available buffer data. If the value is inconsistent, an
        /// <see cref="ArgumentOutOfRangeException"/> is thrown.
        /// </para>
        /// </remarks>
        /// <param name="rawData">
        /// The raw byte buffer containing the VSA TLV. Must not be <see langword="null"/>
        /// and must be large enough to contain the complete attribute at <paramref name="offset"/>.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset within <paramref name="rawData"/> at which the outer
        /// VSA TLV begins (the Type byte). Must be non-negative.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="rawData"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="offset"/> is negative, when the buffer does not
        /// contain enough bytes to read the complete VSA header, or when the Vendor-Length
        /// field is inconsistent with the available buffer data.
        /// </exception>
        public VendorSpecificAttributes(byte[] rawData, int offset)
            : base(RadiusAttributeType.VENDOR_SPECIFIC)
        {
            ArgumentNullException.ThrowIfNull(rawData);

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), offset,
                    "Offset must be non-negative.");

            if (rawData.Length < offset + VSA_DATA_INDEX)
                throw new ArgumentOutOfRangeException(nameof(rawData),
                    $"Buffer is too short to contain a VSA header at offset {offset}. " +
                    $"Required: {offset + VSA_DATA_INDEX} bytes, available: {rawData.Length}.");

            // Read Vendor-Id: 4-byte big-endian value at offset + VSA_ID_INDEX.
            VendorId = BinaryPrimitives.ReadUInt32BigEndian(rawData.AsSpan(offset + VSA_ID_INDEX));

            VendorSpecificType = rawData[offset + VSA_TYPE_INDEX];
            VendorSpecificLength = rawData[offset + VSA_LENGTH_INDEX];

            // Vendor-Data length is Vendor-Length minus the 2-byte sub-attribute header.
            int dataLength = VendorSpecificLength - ATTRIBUTE_HEADER_SIZE;
            if (dataLength < 0 || rawData.Length < offset + VSA_DATA_INDEX + dataLength)
                throw new ArgumentOutOfRangeException(nameof(rawData),
                    $"Vendor-Length field value {VendorSpecificLength} is inconsistent " +
                    $"with the available buffer data at offset {offset}.");

            // Outer attribute length = all VSA header fields + vendor data.
            Length = (byte)(VSA_DATA_INDEX + dataLength);

            // RawData holds the full outer VSA TLV — one allocation.
            RawData = rawData.AsSpan(offset, Length).ToArray();

            // Data is a slice view into RawData's vendor-data region — zero extra allocation.
            // This replaces the previous separate .ToArray() call that produced a second
            // heap allocation containing a copy of the same bytes.
            Data = RawData[VSA_DATA_INDEX..];
        }

        #endregion
    }
}