using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Compat / Compatible Systems (IANA PEN 255) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.compat</c>.
    /// </summary>
    /// <remarks>
    /// The FreeRADIUS <c>dictionary.compat</c> defines compatibility attributes that
    /// map non-standard vendor-specific encodings to standard RADIUS attribute formats.
    /// These are used to provide interoperability with legacy NAS devices that encode
    /// certain standard attributes in vendor-specific form.
    /// </remarks>
    public enum CompatAttributeType : byte
    {
        /// <summary>Compat-Area-Code (Type 1). String. Area code string.</summary>
        AREA_CODE = 1,

        /// <summary>Compat-Session-Id (Type 2). String. Session identifier string.</summary>
        SESSION_ID = 2,

        /// <summary>Compat-Disconnect-Reason (Type 3). Integer. Disconnect reason code.</summary>
        DISCONNECT_REASON = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Compat / Compatible Systems
    /// (IANA PEN 255) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.compat</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Compat's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 255</c>.
    /// </para>
    /// <para>
    /// These compatibility attributes are used to provide interoperability with legacy
    /// NAS devices that encode certain session-related information (area code,
    /// session identifier, disconnect reason) as vendor-specific attributes rather
    /// than using standard RADIUS attributes.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_REQUEST);
    /// packet.SetAttribute(CompatAttributes.AreaCode("212"));
    /// packet.SetAttribute(CompatAttributes.SessionId("session-001"));
    /// packet.SetAttribute(CompatAttributes.DisconnectReason(10));
    /// </code>
    /// </remarks>
    public static class CompatAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Compatible Systems.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 255;

        #region Integer Attributes

        /// <summary>
        /// Creates a Compat-Disconnect-Reason attribute (Type 3) with the specified reason code.
        /// </summary>
        /// <param name="value">The disconnect reason code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DisconnectReason(int value)
        {
            return CreateInteger(CompatAttributeType.DISCONNECT_REASON, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Compat-Area-Code attribute (Type 1) with the specified area code.
        /// </summary>
        /// <param name="value">The area code string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AreaCode(string value)
        {
            return CreateString(CompatAttributeType.AREA_CODE, value);
        }

        /// <summary>
        /// Creates a Compat-Session-Id attribute (Type 2) with the specified session identifier.
        /// </summary>
        /// <param name="value">The session identifier string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SessionId(string value)
        {
            return CreateString(CompatAttributeType.SESSION_ID, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Compat attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(CompatAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Compat attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(CompatAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}