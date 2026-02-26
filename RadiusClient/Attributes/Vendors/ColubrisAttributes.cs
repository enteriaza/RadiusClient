using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Colubris Networks / HP MSM (IANA PEN 8744) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.colubris</c>.
    /// </summary>
    public enum ColubrisAttributeType : byte
    {
        /// <summary>Colubris-AVPair (Type 0). String. Attribute-value pair string.</summary>
        AVPAIR = 0,

        /// <summary>Colubris-Intercept (Type 1). Integer. Intercept (lawful intercept) flag.</summary>
        INTERCEPT = 1,

        /// <summary>Colubris-Policy-Name (Type 2). String. Policy name to apply.</summary>
        POLICY_NAME = 2,

        /// <summary>Colubris-Forwarding-Profile (Type 3). String. Forwarding profile name.</summary>
        FORWARDING_PROFILE = 3,

        /// <summary>Colubris-Role-Name (Type 4). String. Role name to assign.</summary>
        ROLE_NAME = 4,

        /// <summary>Colubris-VLAN-Name (Type 5). String. VLAN name to assign.</summary>
        VLAN_NAME = 5,

        /// <summary>Colubris-URL-Redirect (Type 6). String. URL redirect destination.</summary>
        URL_REDIRECT = 6,

        /// <summary>Colubris-Bandwidth-Max-Up (Type 7). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 7,

        /// <summary>Colubris-Bandwidth-Max-Down (Type 8). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 8,

        /// <summary>Colubris-Access-Profile (Type 9). String. Access profile name.</summary>
        ACCESS_PROFILE = 9,

        /// <summary>Colubris-Acct-Interim-Interval (Type 10). Integer. Accounting interim interval in seconds.</summary>
        ACCT_INTERIM_INTERVAL = 10,

        /// <summary>Colubris-Security-Profile (Type 11). String. Security profile name.</summary>
        SECURITY_PROFILE = 11,

        /// <summary>Colubris-Ingress-VLAN-ID (Type 12). Integer. Ingress VLAN identifier.</summary>
        INGRESS_VLAN_ID = 12,

        /// <summary>Colubris-Egress-VLAN-ID (Type 13). Integer. Egress VLAN identifier.</summary>
        EGRESS_VLAN_ID = 13
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Colubris Networks / HP MSM
    /// (IANA PEN 8744) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.colubris</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Colubris' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 8744</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Colubris Networks (now HP MSM / Aruba Instant)
    /// wireless controllers and access points for RADIUS-based policy and role
    /// assignment, VLAN mapping (by name and ID), URL redirection, bandwidth
    /// provisioning, access and security profile selection, forwarding profile
    /// configuration, accounting interim intervals, and lawful intercept control.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ColubrisAttributes.RoleName("employee"));
    /// packet.SetAttribute(ColubrisAttributes.VlanName("corp-vlan"));
    /// packet.SetAttribute(ColubrisAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(ColubrisAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(ColubrisAttributes.PolicyName("standard-policy"));
    /// </code>
    /// </remarks>
    public static class ColubrisAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Colubris Networks (HP MSM).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 8744;

        #region Integer Attributes

        /// <summary>
        /// Creates a Colubris-Intercept attribute (Type 1) with the specified flag.
        /// </summary>
        /// <param name="value">The intercept (lawful intercept) flag.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Intercept(int value)
        {
            return CreateInteger(ColubrisAttributeType.INTERCEPT, value);
        }

        /// <summary>
        /// Creates a Colubris-Bandwidth-Max-Up attribute (Type 7) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(ColubrisAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Colubris-Bandwidth-Max-Down attribute (Type 8) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(ColubrisAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a Colubris-Acct-Interim-Interval attribute (Type 10) with the specified interval.
        /// </summary>
        /// <param name="value">The accounting interim interval in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctInterimInterval(int value)
        {
            return CreateInteger(ColubrisAttributeType.ACCT_INTERIM_INTERVAL, value);
        }

        /// <summary>
        /// Creates a Colubris-Ingress-VLAN-ID attribute (Type 12) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The ingress VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IngressVlanId(int value)
        {
            return CreateInteger(ColubrisAttributeType.INGRESS_VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Colubris-Egress-VLAN-ID attribute (Type 13) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The egress VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes EgressVlanId(int value)
        {
            return CreateInteger(ColubrisAttributeType.EGRESS_VLAN_ID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Colubris-AVPair attribute (Type 0) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(ColubrisAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Colubris-Policy-Name attribute (Type 2) with the specified policy name.
        /// </summary>
        /// <param name="value">The policy name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(ColubrisAttributeType.POLICY_NAME, value);
        }

        /// <summary>
        /// Creates a Colubris-Forwarding-Profile attribute (Type 3) with the specified profile name.
        /// </summary>
        /// <param name="value">The forwarding profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ForwardingProfile(string value)
        {
            return CreateString(ColubrisAttributeType.FORWARDING_PROFILE, value);
        }

        /// <summary>
        /// Creates a Colubris-Role-Name attribute (Type 4) with the specified role name.
        /// </summary>
        /// <param name="value">The role name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RoleName(string value)
        {
            return CreateString(ColubrisAttributeType.ROLE_NAME, value);
        }

        /// <summary>
        /// Creates a Colubris-VLAN-Name attribute (Type 5) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(ColubrisAttributeType.VLAN_NAME, value);
        }

        /// <summary>
        /// Creates a Colubris-URL-Redirect attribute (Type 6) with the specified URL.
        /// </summary>
        /// <param name="value">The URL redirect destination. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UrlRedirect(string value)
        {
            return CreateString(ColubrisAttributeType.URL_REDIRECT, value);
        }

        /// <summary>
        /// Creates a Colubris-Access-Profile attribute (Type 9) with the specified profile name.
        /// </summary>
        /// <param name="value">The access profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessProfile(string value)
        {
            return CreateString(ColubrisAttributeType.ACCESS_PROFILE, value);
        }

        /// <summary>
        /// Creates a Colubris-Security-Profile attribute (Type 11) with the specified profile name.
        /// </summary>
        /// <param name="value">The security profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SecurityProfile(string value)
        {
            return CreateString(ColubrisAttributeType.SECURITY_PROFILE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Colubris attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ColubrisAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Colubris attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ColubrisAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}