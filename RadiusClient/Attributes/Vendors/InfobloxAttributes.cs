using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Infoblox (IANA PEN 7779) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.infoblox</c>.
    /// </summary>
    /// <remarks>
    /// Infoblox is a provider of DDI (DNS, DHCP, IPAM) and network security
    /// solutions, including the NIOS operating system.
    /// </remarks>
    public enum InfobloxAttributeType : byte
    {
        /// <summary>Infoblox-Group-Info (Type 1). String. User group information.</summary>
        GROUP_INFO = 1,

        /// <summary>Infoblox-Role-Info (Type 2). String. User role information.</summary>
        ROLE_INFO = 2,

        /// <summary>Infoblox-Saml-Id (Type 3). String. SAML identity.</summary>
        SAML_ID = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Infoblox
    /// (IANA PEN 7779) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.infoblox</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Infoblox's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 7779</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Infoblox NIOS appliances (physical and virtual)
    /// for RADIUS-based user group assignment, role mapping, and SAML identity
    /// binding during administrative authentication to the DDI management platform.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(InfobloxAttributes.GroupInfo("dns-admins"));
    /// packet.SetAttribute(InfobloxAttributes.RoleInfo("superuser"));
    /// packet.SetAttribute(InfobloxAttributes.SamlId("user@example.com"));
    /// </code>
    /// </remarks>
    public static class InfobloxAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Infoblox.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 7779;

        #region String Attributes

        /// <summary>
        /// Creates an Infoblox-Group-Info attribute (Type 1) with the specified group information.
        /// </summary>
        /// <param name="value">The user group information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GroupInfo(string value)
        {
            return CreateString(InfobloxAttributeType.GROUP_INFO, value);
        }

        /// <summary>
        /// Creates an Infoblox-Role-Info attribute (Type 2) with the specified role information.
        /// </summary>
        /// <param name="value">The user role information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RoleInfo(string value)
        {
            return CreateString(InfobloxAttributeType.ROLE_INFO, value);
        }

        /// <summary>
        /// Creates an Infoblox-Saml-Id attribute (Type 3) with the specified SAML identity.
        /// </summary>
        /// <param name="value">The SAML identity. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SamlId(string value)
        {
            return CreateString(InfobloxAttributeType.SAML_ID, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Infoblox attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(InfobloxAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}