using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Blue Coat Systems / Symantec (IANA PEN 14501) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.bluecoat</c>.
    /// </summary>
    public enum BlueCoatAttributeType : byte
    {
        /// <summary>Blue-Coat-Group (Type 1). String. Administrative group name.</summary>
        GROUP = 1,

        /// <summary>Blue-Coat-Authorization (Type 2). Integer. Authorization level.</summary>
        AUTHORIZATION = 2
    }

    /// <summary>
    /// Blue-Coat-Authorization attribute values (Type 2).
    /// </summary>
    public enum BLUECOAT_AUTHORIZATION
    {
        /// <summary>No authorization — access denied.</summary>
        NONE = 0,

        /// <summary>Read-only authorization.</summary>
        READ_ONLY = 1,

        /// <summary>Read-write authorization.</summary>
        READ_WRITE = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Blue Coat Systems / Symantec
    /// (IANA PEN 14501) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.bluecoat</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Blue Coat's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14501</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Blue Coat (now Symantec / Broadcom) ProxySG,
    /// PacketShaper, and related appliances for RADIUS-based administrative group
    /// assignment and authorization level control.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(BlueCoatAttributes.Group("admin-group"));
    /// packet.SetAttribute(BlueCoatAttributes.Authorization(BLUECOAT_AUTHORIZATION.READ_WRITE));
    /// </code>
    /// </remarks>
    public static class BlueCoatAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Blue Coat Systems (Symantec / Broadcom).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14501;

        #region Integer Attributes

        /// <summary>
        /// Creates a Blue-Coat-Authorization attribute (Type 2) with the specified authorization level.
        /// </summary>
        /// <param name="value">The authorization level. See <see cref="BLUECOAT_AUTHORIZATION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Authorization(BLUECOAT_AUTHORIZATION value)
        {
            return CreateInteger(BlueCoatAttributeType.AUTHORIZATION, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Blue-Coat-Group attribute (Type 1) with the specified group name.
        /// </summary>
        /// <param name="value">The administrative group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Group(string value)
        {
            return CreateString(BlueCoatAttributeType.GROUP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Blue Coat attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(BlueCoatAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Blue Coat attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(BlueCoatAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}