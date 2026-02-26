using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Clavister (IANA PEN 5089) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.clavister</c>.
    /// </summary>
    public enum ClavisterAttributeType : byte
    {
        /// <summary>Clavister-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Clavister-Admin-Access (Type 2). Integer. Administrative access level.</summary>
        ADMIN_ACCESS = 2,

        /// <summary>Clavister-Admin-Role (Type 3). String. Administrative role name.</summary>
        ADMIN_ROLE = 3,

        /// <summary>Clavister-VPN-Group (Type 4). String. VPN group name.</summary>
        VPN_GROUP = 4,

        /// <summary>Clavister-Address-Pool (Type 5). String. Address pool name.</summary>
        ADDRESS_POOL = 5,

        /// <summary>Clavister-Bandwidth-Up (Type 6). Integer. Upstream bandwidth in Kbps.</summary>
        BANDWIDTH_UP = 6,

        /// <summary>Clavister-Bandwidth-Down (Type 7). Integer. Downstream bandwidth in Kbps.</summary>
        BANDWIDTH_DOWN = 7
    }

    /// <summary>
    /// Clavister-Admin-Access attribute values (Type 2).
    /// </summary>
    public enum CLAVISTER_ADMIN_ACCESS
    {
        /// <summary>No administrative access.</summary>
        NONE = 0,

        /// <summary>Read-only administrative access.</summary>
        READ_ONLY = 1,

        /// <summary>Read-write administrative access.</summary>
        READ_WRITE = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Clavister
    /// (IANA PEN 5089) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.clavister</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Clavister's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 5089</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Clavister NetShield firewalls and VPN gateways
    /// for RADIUS-based administrative access control, VPN group assignment,
    /// address pool selection, bandwidth provisioning, and general attribute-value
    /// pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ClavisterAttributes.AdminAccess(CLAVISTER_ADMIN_ACCESS.READ_WRITE));
    /// packet.SetAttribute(ClavisterAttributes.AdminRole("firewall-admin"));
    /// packet.SetAttribute(ClavisterAttributes.VpnGroup("remote-users"));
    /// packet.SetAttribute(ClavisterAttributes.BandwidthDown(50000));
    /// </code>
    /// </remarks>
    public static class ClavisterAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Clavister.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 5089;

        #region Integer Attributes

        /// <summary>
        /// Creates a Clavister-Admin-Access attribute (Type 2) with the specified access level.
        /// </summary>
        /// <param name="value">The administrative access level. See <see cref="CLAVISTER_ADMIN_ACCESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminAccess(CLAVISTER_ADMIN_ACCESS value)
        {
            return CreateInteger(ClavisterAttributeType.ADMIN_ACCESS, (int)value);
        }

        /// <summary>
        /// Creates a Clavister-Bandwidth-Up attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthUp(int value)
        {
            return CreateInteger(ClavisterAttributeType.BANDWIDTH_UP, value);
        }

        /// <summary>
        /// Creates a Clavister-Bandwidth-Down attribute (Type 7) with the specified rate.
        /// </summary>
        /// <param name="value">The downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthDown(int value)
        {
            return CreateInteger(ClavisterAttributeType.BANDWIDTH_DOWN, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Clavister-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(ClavisterAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Clavister-Admin-Role attribute (Type 3) with the specified role name.
        /// </summary>
        /// <param name="value">The administrative role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminRole(string value)
        {
            return CreateString(ClavisterAttributeType.ADMIN_ROLE, value);
        }

        /// <summary>
        /// Creates a Clavister-VPN-Group attribute (Type 4) with the specified group name.
        /// </summary>
        /// <param name="value">The VPN group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnGroup(string value)
        {
            return CreateString(ClavisterAttributeType.VPN_GROUP, value);
        }

        /// <summary>
        /// Creates a Clavister-Address-Pool attribute (Type 5) with the specified pool name.
        /// </summary>
        /// <param name="value">The address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AddressPool(string value)
        {
            return CreateString(ClavisterAttributeType.ADDRESS_POOL, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Clavister attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ClavisterAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Clavister attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ClavisterAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}