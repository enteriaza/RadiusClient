using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a CNergee (IANA PEN 27262) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.cnergee</c>.
    /// </summary>
    public enum CnergeeAttributeType : byte
    {
        /// <summary>CNergee-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>CNergee-Proxy-Auth-Group (Type 2). String. Proxy authentication group name.</summary>
        PROXY_AUTH_GROUP = 2,

        /// <summary>CNergee-Admin-Role (Type 3). String. Administrative role name.</summary>
        ADMIN_ROLE = 3,

        /// <summary>CNergee-User-Profile (Type 4). String. User profile name.</summary>
        USER_PROFILE = 4,

        /// <summary>CNergee-Splash-Page (Type 5). String. Captive portal splash page URL.</summary>
        SPLASH_PAGE = 5,

        /// <summary>CNergee-Bandwidth-Max-Up (Type 6). Integer. Maximum upstream bandwidth in bps.</summary>
        BANDWIDTH_MAX_UP = 6,

        /// <summary>CNergee-Bandwidth-Max-Down (Type 7). Integer. Maximum downstream bandwidth in bps.</summary>
        BANDWIDTH_MAX_DOWN = 7,

        /// <summary>CNergee-Quota-Total (Type 8). Integer. Total data quota in bytes.</summary>
        QUOTA_TOTAL = 8,

        /// <summary>CNergee-VLAN-Id (Type 9). Integer. VLAN identifier.</summary>
        VLAN_ID = 9
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing CNergee
    /// (IANA PEN 27262) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.cnergee</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// CNergee's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 27262</c>.
    /// </para>
    /// <para>
    /// These attributes are used by CNergee hotspot and network access control
    /// platforms for RADIUS-based proxy authentication groups, admin role
    /// assignment, user profile selection, captive portal splash page URLs,
    /// bandwidth provisioning, data quota enforcement, and VLAN mapping.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CnergeeAttributes.UserProfile("premium"));
    /// packet.SetAttribute(CnergeeAttributes.BandwidthMaxUp(5000000));
    /// packet.SetAttribute(CnergeeAttributes.BandwidthMaxDown(20000000));
    /// packet.SetAttribute(CnergeeAttributes.VlanId(100));
    /// </code>
    /// </remarks>
    public static class CnergeeAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for CNergee.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 27262;

        #region Integer Attributes

        /// <summary>
        /// Creates a CNergee-Bandwidth-Max-Up attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(CnergeeAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a CNergee-Bandwidth-Max-Down attribute (Type 7) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(CnergeeAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a CNergee-Quota-Total attribute (Type 8) with the specified quota.
        /// </summary>
        /// <param name="value">The total data quota in bytes.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes QuotaTotal(int value)
        {
            return CreateInteger(CnergeeAttributeType.QUOTA_TOTAL, value);
        }

        /// <summary>
        /// Creates a CNergee-VLAN-Id attribute (Type 9) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(CnergeeAttributeType.VLAN_ID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a CNergee-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(CnergeeAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a CNergee-Proxy-Auth-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The proxy authentication group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProxyAuthGroup(string value)
        {
            return CreateString(CnergeeAttributeType.PROXY_AUTH_GROUP, value);
        }

        /// <summary>
        /// Creates a CNergee-Admin-Role attribute (Type 3) with the specified role name.
        /// </summary>
        /// <param name="value">The administrative role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminRole(string value)
        {
            return CreateString(CnergeeAttributeType.ADMIN_ROLE, value);
        }

        /// <summary>
        /// Creates a CNergee-User-Profile attribute (Type 4) with the specified profile name.
        /// </summary>
        /// <param name="value">The user profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserProfile(string value)
        {
            return CreateString(CnergeeAttributeType.USER_PROFILE, value);
        }

        /// <summary>
        /// Creates a CNergee-Splash-Page attribute (Type 5) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal splash page URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SplashPage(string value)
        {
            return CreateString(CnergeeAttributeType.SPLASH_PAGE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified CNergee attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(CnergeeAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified CNergee attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(CnergeeAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}