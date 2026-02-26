using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Mimosa Networks (IANA PEN 43356) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.mimosa</c>.
    /// </summary>
    /// <remarks>
    /// Mimosa Networks (acquired by Airspan Networks) produces fixed wireless
    /// broadband and point-to-point/point-to-multipoint radio equipment,
    /// including the A5, B5, B11, and C5 series.
    /// </remarks>
    public enum MimosaAttributeType : byte
    {
        /// <summary>Mimosa-Device-Name (Type 1). String. Device name.</summary>
        DEVICE_NAME = 1,

        /// <summary>Mimosa-SSID (Type 2). String. Wireless SSID name.</summary>
        SSID = 2,

        /// <summary>Mimosa-User-Role (Type 3). String. User role name.</summary>
        USER_ROLE = 3,

        /// <summary>Mimosa-VLAN-Id (Type 4). Integer. VLAN identifier.</summary>
        VLAN_ID = 4,

        /// <summary>Mimosa-Bandwidth-Max-Up (Type 5). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 5,

        /// <summary>Mimosa-Bandwidth-Max-Down (Type 6). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 6
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Mimosa Networks
    /// (IANA PEN 43356) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.mimosa</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Mimosa's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 43356</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Mimosa Networks (now Airspan) fixed wireless
    /// broadband radios (A5, B5, B11, C5 series) for RADIUS-based device
    /// identification, wireless SSID identification, user role assignment,
    /// VLAN assignment, and upstream/downstream bandwidth provisioning.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(MimosaAttributes.UserRole("subscriber"));
    /// packet.SetAttribute(MimosaAttributes.VlanId(200));
    /// packet.SetAttribute(MimosaAttributes.BandwidthMaxUp(25000));
    /// packet.SetAttribute(MimosaAttributes.BandwidthMaxDown(100000));
    /// </code>
    /// </remarks>
    public static class MimosaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Mimosa Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 43356;

        #region Integer Attributes

        /// <summary>
        /// Creates a Mimosa-VLAN-Id attribute (Type 4) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(MimosaAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Mimosa-Bandwidth-Max-Up attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(MimosaAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Mimosa-Bandwidth-Max-Down attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(MimosaAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Mimosa-Device-Name attribute (Type 1) with the specified device name.
        /// </summary>
        /// <param name="value">The device name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DeviceName(string value)
        {
            return CreateString(MimosaAttributeType.DEVICE_NAME, value);
        }

        /// <summary>
        /// Creates a Mimosa-SSID attribute (Type 2) with the specified wireless SSID.
        /// </summary>
        /// <param name="value">The wireless SSID name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ssid(string value)
        {
            return CreateString(MimosaAttributeType.SSID, value);
        }

        /// <summary>
        /// Creates a Mimosa-User-Role attribute (Type 3) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(MimosaAttributeType.USER_ROLE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Mimosa attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(MimosaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Mimosa attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(MimosaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}