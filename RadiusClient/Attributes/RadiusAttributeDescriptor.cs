using System.Net;

namespace Radius.Attributes
{
    /// <summary>
    /// Abstract base for strongly typed RADIUS attribute descriptors that enforce
    /// compile-time type safety when constructing attribute TLVs.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each descriptor binds a <see cref="RadiusAttributeType"/> to a specific CLR
    /// value type <typeparamref name="T"/> and provides the encoding logic to produce
    /// a <see cref="RadiusAttributes"/> instance suitable for appending to a
    /// <see cref="RadiusPacket"/> via <see cref="RadiusPacket.SetAttribute{T}"/>.
    /// </para>
    /// <para>
    /// This eliminates the class of bugs where the wrong value type is passed to an
    /// attribute constructor at runtime. For example, passing a <see cref="string"/>
    /// to <see cref="RadiusAttributeType.SERVICE_TYPE"/> (which requires a 32-bit
    /// enumerated integer) is a compile-time error when using descriptors:
    /// </para>
    /// <code>
    /// // Compile-time error: cannot convert 'string' to 'SERVICE_TYPE'
    /// packet.SetAttribute(RadiusAttributeDescriptors.ServiceType, "hello");
    ///
    /// // Correct — compiler enforces the enum type:
    /// packet.SetAttribute(RadiusAttributeDescriptors.ServiceType, SERVICE_TYPE.AUTHENTICATE_ONLY);
    /// </code>
    /// <para>
    /// Concrete implementations are provided for each RADIUS data type: strings,
    /// 32-bit integers, IP addresses, enumerations, raw byte arrays, timestamps,
    /// and 64-bit integers.
    /// </para>
    /// </remarks>
    /// <typeparam name="T">
    /// The CLR type that callers must supply when setting this attribute.
    /// </typeparam>
    public abstract class RadiusAttributeDescriptor<T>
    {
        /// <summary>
        /// Gets the RADIUS attribute type code (RFC 2865 §5, octet 1) that this
        /// descriptor represents.
        /// </summary>
        public RadiusAttributeType Type { get; }

        /// <summary>
        /// Initialises a new descriptor for the specified attribute type.
        /// </summary>
        /// <param name="type">
        /// The RADIUS attribute type code this descriptor represents.
        /// </param>
        protected RadiusAttributeDescriptor(RadiusAttributeType type)
        {
            Type = type;
        }

        /// <summary>
        /// Creates a fully encoded <see cref="RadiusAttributes"/> TLV instance from the
        /// supplied value.
        /// </summary>
        /// <param name="value">
        /// The strongly typed value to encode into the attribute's wire format.
        /// </param>
        /// <returns>
        /// A <see cref="RadiusAttributes"/> instance ready to be appended to a
        /// <see cref="RadiusPacket"/> via <see cref="RadiusPacket.SetAttribute"/>.
        /// </returns>
        public abstract RadiusAttributes Encode(T value);
    }

    /// <summary>
    /// Descriptor for attributes whose value is a UTF-8 encoded string (RFC 2865 §5).
    /// </summary>
    /// <remarks>
    /// Used for attributes such as User-Name (1), Filter-Id (11), Reply-Message (18),
    /// Called-Station-Id (30), Calling-Station-Id (31), NAS-Identifier (32), etc.
    /// </remarks>
    public sealed class StringAttributeDescriptor : RadiusAttributeDescriptor<string>
    {
        /// <summary>
        /// Initialises a new string attribute descriptor for the specified type.
        /// </summary>
        /// <param name="type">The RADIUS attribute type code.</param>
        public StringAttributeDescriptor(RadiusAttributeType type) : base(type) { }

        /// <inheritdoc/>
        public override RadiusAttributes Encode(string value) => new(Type, value);
    }

    /// <summary>
    /// Descriptor for attributes whose value is a 32-bit unsigned integer encoded in
    /// big-endian (network byte order) per RFC 2865 §5.
    /// </summary>
    /// <remarks>
    /// Used for attributes such as NAS-Port (5), Framed-MTU (12), Session-Timeout (27),
    /// Idle-Timeout (28), Acct-Session-Time (46), etc.
    /// </remarks>
    public sealed class IntegerAttributeDescriptor : RadiusAttributeDescriptor<int>
    {
        /// <summary>
        /// Initialises a new integer attribute descriptor for the specified type.
        /// </summary>
        /// <param name="type">The RADIUS attribute type code.</param>
        public IntegerAttributeDescriptor(RadiusAttributeType type) : base(type) { }

        /// <inheritdoc/>
        public override RadiusAttributes Encode(int value) => new(Type, value);
    }

    /// <summary>
    /// Descriptor for attributes whose value is a 64-bit signed integer encoded in
    /// big-endian (network byte order) per RFC 8044 §3.3.
    /// </summary>
    /// <remarks>
    /// Used for attributes such as MIP6-Feature-Vector (124).
    /// </remarks>
    public sealed class Integer64AttributeDescriptor : RadiusAttributeDescriptor<long>
    {
        /// <summary>
        /// Initialises a new 64-bit integer attribute descriptor for the specified type.
        /// </summary>
        /// <param name="type">The RADIUS attribute type code.</param>
        public Integer64AttributeDescriptor(RadiusAttributeType type) : base(type) { }

        /// <inheritdoc/>
        public override RadiusAttributes Encode(long value) => new(Type, value);
    }

    /// <summary>
    /// Descriptor for attributes whose value is an IPv4 or IPv6 address (RFC 2865 §5.4,
    /// RFC 3162 §2.1).
    /// </summary>
    /// <remarks>
    /// Used for attributes such as NAS-IP-Address (4), Framed-IP-Address (8),
    /// NAS-IPv6-Address (95), Framed-IPv6-Address (168), etc.
    /// </remarks>
    public sealed class IPAddressAttributeDescriptor : RadiusAttributeDescriptor<IPAddress>
    {
        /// <summary>
        /// Initialises a new IP address attribute descriptor for the specified type.
        /// </summary>
        /// <param name="type">The RADIUS attribute type code.</param>
        public IPAddressAttributeDescriptor(RadiusAttributeType type) : base(type) { }

        /// <inheritdoc/>
        public override RadiusAttributes Encode(IPAddress value) => new(Type, value);
    }

    /// <summary>
    /// Descriptor for attributes whose value is a <see cref="DateTime"/> encoded as a
    /// 32-bit big-endian Unix timestamp (RFC 2869 §5.3).
    /// </summary>
    /// <remarks>
    /// Used for the Event-Timestamp (55) attribute.
    /// </remarks>
    public sealed class DateTimeAttributeDescriptor : RadiusAttributeDescriptor<DateTime>
    {
        /// <summary>
        /// Initialises a new date/time attribute descriptor for the specified type.
        /// </summary>
        /// <param name="type">The RADIUS attribute type code.</param>
        public DateTimeAttributeDescriptor(RadiusAttributeType type) : base(type) { }

        /// <inheritdoc/>
        public override RadiusAttributes Encode(DateTime value) => new(Type, value);
    }

    /// <summary>
    /// Descriptor for attributes whose value is a raw byte array (RFC 2865 §5).
    /// </summary>
    /// <remarks>
    /// Used for opaque or binary-valued attributes such as State (24), Class (25),
    /// CHAP-Password (3), CHAP-Challenge (60), EAP-Message (79), Proxy-State (33), etc.
    /// </remarks>
    public sealed class OctetStringAttributeDescriptor : RadiusAttributeDescriptor<byte[]>
    {
        /// <summary>
        /// Initialises a new raw byte array attribute descriptor for the specified type.
        /// </summary>
        /// <param name="type">The RADIUS attribute type code.</param>
        public OctetStringAttributeDescriptor(RadiusAttributeType type) : base(type) { }

        /// <inheritdoc/>
        public override RadiusAttributes Encode(byte[] value) => new(Type, value);
    }

    /// <summary>
    /// Descriptor for attributes whose value is a RADIUS enumerated integer
    /// (RFC 2865 §5), providing full compile-time type safety by binding the
    /// attribute to its specific <typeparamref name="TEnum"/> type.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is the most powerful descriptor — it makes it impossible to pass
    /// the wrong enum type for an enumerated attribute at compile time:
    /// </para>
    /// <code>
    /// // Compile-time error: cannot convert 'FRAMED_PROTOCOL' to 'SERVICE_TYPE'
    /// packet.SetAttribute(RadiusAttributeDescriptors.ServiceType, FRAMED_PROTOCOL.PPP);
    ///
    /// // Correct:
    /// packet.SetAttribute(RadiusAttributeDescriptors.ServiceType, SERVICE_TYPE.FRAMED);
    /// </code>
    /// <para>
    /// The enum value is encoded as a 32-bit big-endian unsigned integer on the wire,
    /// matching the RADIUS "Enumerated" data type (RFC 2865 §5).
    /// </para>
    /// </remarks>
    /// <typeparam name="TEnum">
    /// The specific RADIUS enum type (e.g. <see cref="SERVICE_TYPE"/>,
    /// <see cref="FRAMED_PROTOCOL"/>, <see cref="ACCT_STATUS_TYPE"/>).
    /// </typeparam>
    public sealed class EnumAttributeDescriptor<TEnum> : RadiusAttributeDescriptor<TEnum>
        where TEnum : struct, Enum
    {
        /// <summary>
        /// Initialises a new enumerated attribute descriptor for the specified type.
        /// </summary>
        /// <param name="type">The RADIUS attribute type code.</param>
        public EnumAttributeDescriptor(RadiusAttributeType type) : base(type) { }

        /// <inheritdoc/>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is not a defined member of
        /// <typeparamref name="TEnum"/>.
        /// </exception>
        public override RadiusAttributes Encode(TEnum value)
        {
            if (!Enum.IsDefined(value))
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    value,
                    $"'{value}' is not a defined {typeof(TEnum).Name} value.");

            return new RadiusAttributes(Type, Convert.ToInt32(value, System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}