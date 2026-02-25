namespace Radius.Attributes;

/// <summary>
/// Represents a RADIUS Tunnel-Type attribute (Type 64), as defined in RFC 2868 §3.1.
/// </summary>
/// <remarks>
/// <para>
/// The Tunnel-Type attribute identifies the tunnelling protocol to use for a session.
/// Its on-wire layout is:
/// </para>
/// <list type="table">
///   <listheader><term>Field</term><description>Size / Description</description></listheader>
///   <item><term>Type</term><description>1 byte — always <c>64</c> (<see cref="RadiusAttributeType.TUNNEL_TYPE"/>).</description></item>
///   <item><term>Length</term><description>1 byte — always <c>6</c>.</description></item>
///   <item><term>Tag</term><description>1 byte — groups related tunnel attributes (RFC 2868 §3.1). Use <c>0x00</c> when tagging is not required.</description></item>
///   <item><term>Value</term><description>3 bytes — big-endian tunnel type code from <see cref="TUNNEL_TYPE"/>.</description></item>
/// </list>
/// <para>
/// See also: <see href="https://tools.ietf.org/html/rfc2868">RFC 2868</see>.
/// </para>
/// </remarks>
public sealed class TunnelTypeAttributes : RadiusAttributes
{
    #region Constants

    /// <summary>Fixed wire length of a Tunnel-Type attribute in bytes (RFC 2868 §3.1).</summary>
    private const byte TUNNEL_TYPE_LENGTH = 6;

    /// <summary>Byte offset of the Tag field within <see cref="RadiusAttributes.RawData"/>.</summary>
    private const byte TUNNEL_TAG_INDEX = 2;

    /// <summary>Byte offset of the 3-byte Value field within <see cref="RadiusAttributes.RawData"/>.</summary>
    private const int TUNNEL_TYPE_VALUE_INDEX = 3;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the Tag byte that groups this attribute with other tunnel attributes
    /// belonging to the same tunnel (RFC 2868 §3.1).
    /// A value of <c>0x00</c> indicates that tagging is not used.
    /// </summary>
    public byte Tag { get; }

    /// <summary>
    /// Gets the tunnelling protocol identified by this attribute,
    /// as defined in RFC 2868 §3.1.
    /// </summary>
    public TUNNEL_TYPE TunnelType { get; }

    #endregion

    #region Constructor

    /// <summary>
    /// Initialises a new instance of <see cref="TunnelTypeAttributes"/> with the
    /// specified tag and tunnel type.
    /// </summary>
    /// <param name="tag">
    /// The Tag byte used to group related tunnel attributes (RFC 2868 §3.1).
    /// Use <c>0x00</c> when attribute tagging is not required.
    /// </param>
    /// <param name="tunnelType">
    /// The tunnelling protocol to encode. Must be a defined member of <see cref="TUNNEL_TYPE"/>.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="tunnelType"/> is not a defined member of <see cref="TUNNEL_TYPE"/>.
    /// </exception>
    public TunnelTypeAttributes(byte tag, TUNNEL_TYPE tunnelType)
        : base(RadiusAttributeType.TUNNEL_TYPE)
    {
        if (!Enum.IsDefined(tunnelType))
            throw new ArgumentOutOfRangeException(
                nameof(tunnelType),
                tunnelType,
                $"'{tunnelType}' is not a defined {nameof(TUNNEL_TYPE)} value.");

        Tag = tag;
        TunnelType = tunnelType;

        // Allocate the single fixed-size TLV buffer (6 bytes) once.
        // Layout: [Type(1)][Length(1)][Tag(1)][Value(3)] — RFC 2868 §3.1.
        // This replaces the previous pattern of calling RadiusUtils.IntTo3Byte(), which
        // heap-allocated a separate byte[3] for Data, followed by a second allocation for
        // RawData and a CopyTo. All fields are now written directly into one buffer.
        Length = TUNNEL_TYPE_LENGTH;
        RawData = new byte[TUNNEL_TYPE_LENGTH];

        // Byte 0: outer RADIUS attribute Type (RFC 2865 §5, octet 1).
        RawData[0] = (byte)Type;

        // Byte 1: outer RADIUS attribute Length = 6 (RFC 2868 §3.1).
        RawData[1] = TUNNEL_TYPE_LENGTH;

        // Byte 2: Tag — used to group related tunnel attributes (RFC 2868 §3.1).
        RawData[TUNNEL_TAG_INDEX] = tag;

        // Bytes 3–5: Tunnel-Type value as big-endian 24-bit integer (RFC 2868 §3.1).
        uint typeValue = (uint)tunnelType;
        RawData[TUNNEL_TYPE_VALUE_INDEX] = (byte)(typeValue >> 16);
        RawData[TUNNEL_TYPE_VALUE_INDEX + 1] = (byte)(typeValue >> 8);
        RawData[TUNNEL_TYPE_VALUE_INDEX + 2] = (byte)typeValue;

        // Data is a slice view of RawData's value region (bytes 3–5) — zero extra allocation.
        // This replaces the separate byte[3] that RadiusUtils.IntTo3Byte() previously returned.
        Data = RawData[TUNNEL_TYPE_VALUE_INDEX..];
    }

    #endregion
}