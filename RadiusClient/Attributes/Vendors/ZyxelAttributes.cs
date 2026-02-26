using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Zyxel Communications (IANA PEN 890) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.zyxel</c>.
    /// </summary>
    /// <remarks>
    /// Zyxel Communications Corp. produces networking equipment including DSL/fiber
    /// CPE (VMG/AMG/PMG series), managed switches, security appliances (USG/ZyWALL/ATP),
    /// wireless access points (NWA/WAX/WAC), DSLAM/MSAN (IES/VES), and cloud-managed
    /// platforms (Nebula) for SMB, enterprise, and service provider deployments.
    /// </remarks>
    public enum ZyxelAttributeType : byte
    {
        /// <summary>Zyxel-Callback-Phone-Source (Type 1). Integer. Callback phone source.</summary>
        CALLBACK_PHONE_SOURCE = 1,

        /// <summary>Zyxel-Callback-Phone-Num (Type 2). String. Callback phone number.</summary>
        CALLBACK_PHONE_NUM = 2,

        /// <summary>Zyxel-Privilege-AVPair (Type 3). String. Privilege attribute-value pair.</summary>
        PRIVILEGE_AVPAIR = 3,

        /// <summary>Zyxel-Policy-Name (Type 4). String. Policy/firewall rule name.</summary>
        POLICY_NAME = 4,

        /// <summary>Zyxel-Auth-Role (Type 5). String. Authentication role name.</summary>
        AUTH_ROLE = 5,

        /// <summary>Zyxel-Access-VLAN (Type 6). Integer. Access VLAN identifier.</summary>
        ACCESS_VLAN = 6,

        /// <summary>Zyxel-Bandwidth-Max-Up (Type 7). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 7,

        /// <summary>Zyxel-Bandwidth-Max-Down (Type 8). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 8,

        /// <summary>Zyxel-Ingress-VLAN-Map (Type 9). String. Ingress VLAN map/translation.</summary>
        INGRESS_VLAN_MAP = 9,

        /// <summary>Zyxel-Egress-VLAN-Map (Type 10). String. Egress VLAN map/translation.</summary>
        EGRESS_VLAN_MAP = 10,

        /// <summary>Zyxel-User-VLAN-Tag (Type 11). Integer. User VLAN tag.</summary>
        USER_VLAN_TAG = 11,

        /// <summary>Zyxel-Sec-Profile (Type 12). String. Security profile name.</summary>
        SEC_PROFILE = 12,

        /// <summary>Zyxel-Tunnel-Profile (Type 13). String. VPN/tunnel profile name.</summary>
        TUNNEL_PROFILE = 13
    }

    /// <summary>
    /// Zyxel-Callback-Phone-Source attribute values (Type 1).
    /// </summary>
    public enum ZYXEL_CALLBACK_PHONE_SOURCE
    {
        /// <summary>Callback using user-specified phone number.</summary>
        USER_SPECIFIED = 0,

        /// <summary>Callback using pre-configured (server) phone number.</summary>
        PRE_CONFIGURED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Zyxel Communications
    /// (IANA PEN 890) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.zyxel</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Zyxel's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 890</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Zyxel security appliances (USG/ZyWALL/ATP),
    /// managed switches, wireless access points, DSL CPE, and DSLAM/MSAN platforms
    /// for RADIUS-based callback phone source and number configuration, privilege
    /// attribute-value pair assignment, policy/firewall rule naming, authentication
    /// role assignment, access VLAN provisioning, upstream/downstream bandwidth
    /// control, ingress/egress VLAN map translation, user VLAN tagging, security
    /// profile selection, and VPN/tunnel profile assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ZyxelAttributes.AuthRole("admin"));
    /// packet.SetAttribute(ZyxelAttributes.AccessVlan(100));
    /// packet.SetAttribute(ZyxelAttributes.BandwidthMaxUp(50000));
    /// packet.SetAttribute(ZyxelAttributes.BandwidthMaxDown(100000));
    /// packet.SetAttribute(ZyxelAttributes.PolicyName("corporate-policy"));
    /// packet.SetAttribute(ZyxelAttributes.SecProfile("high-security"));
    /// </code>
    /// </remarks>
    public static class ZyxelAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Zyxel Communications.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 890;

        #region Integer Attributes

        /// <summary>
        /// Creates a Zyxel-Callback-Phone-Source attribute (Type 1) with the specified source.
        /// </summary>
        /// <param name="value">The callback phone source. See <see cref="ZYXEL_CALLBACK_PHONE_SOURCE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallbackPhoneSource(ZYXEL_CALLBACK_PHONE_SOURCE value)
        {
            return CreateInteger(ZyxelAttributeType.CALLBACK_PHONE_SOURCE, (int)value);
        }

        /// <summary>
        /// Creates a Zyxel-Access-VLAN attribute (Type 6) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The access VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessVlan(int value)
        {
            return CreateInteger(ZyxelAttributeType.ACCESS_VLAN, value);
        }

        /// <summary>
        /// Creates a Zyxel-Bandwidth-Max-Up attribute (Type 7) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(ZyxelAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Zyxel-Bandwidth-Max-Down attribute (Type 8) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(ZyxelAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a Zyxel-User-VLAN-Tag attribute (Type 11) with the specified VLAN tag.
        /// </summary>
        /// <param name="value">The user VLAN tag.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserVlanTag(int value)
        {
            return CreateInteger(ZyxelAttributeType.USER_VLAN_TAG, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Zyxel-Callback-Phone-Num attribute (Type 2) with the specified phone number.
        /// </summary>
        /// <param name="value">The callback phone number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallbackPhoneNum(string value)
        {
            return CreateString(ZyxelAttributeType.CALLBACK_PHONE_NUM, value);
        }

        /// <summary>
        /// Creates a Zyxel-Privilege-AVPair attribute (Type 3) with the specified pair.
        /// </summary>
        /// <param name="value">The privilege attribute-value pair. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrivilegeAvPair(string value)
        {
            return CreateString(ZyxelAttributeType.PRIVILEGE_AVPAIR, value);
        }

        /// <summary>
        /// Creates a Zyxel-Policy-Name attribute (Type 4) with the specified policy name.
        /// </summary>
        /// <param name="value">The policy/firewall rule name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(ZyxelAttributeType.POLICY_NAME, value);
        }

        /// <summary>
        /// Creates a Zyxel-Auth-Role attribute (Type 5) with the specified role name.
        /// </summary>
        /// <param name="value">The authentication role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthRole(string value)
        {
            return CreateString(ZyxelAttributeType.AUTH_ROLE, value);
        }

        /// <summary>
        /// Creates a Zyxel-Ingress-VLAN-Map attribute (Type 9) with the specified VLAN map.
        /// </summary>
        /// <param name="value">The ingress VLAN map/translation. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IngressVlanMap(string value)
        {
            return CreateString(ZyxelAttributeType.INGRESS_VLAN_MAP, value);
        }

        /// <summary>
        /// Creates a Zyxel-Egress-VLAN-Map attribute (Type 10) with the specified VLAN map.
        /// </summary>
        /// <param name="value">The egress VLAN map/translation. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EgressVlanMap(string value)
        {
            return CreateString(ZyxelAttributeType.EGRESS_VLAN_MAP, value);
        }

        /// <summary>
        /// Creates a Zyxel-Sec-Profile attribute (Type 12) with the specified security profile.
        /// </summary>
        /// <param name="value">The security profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SecProfile(string value)
        {
            return CreateString(ZyxelAttributeType.SEC_PROFILE, value);
        }

        /// <summary>
        /// Creates a Zyxel-Tunnel-Profile attribute (Type 13) with the specified tunnel profile.
        /// </summary>
        /// <param name="value">The VPN/tunnel profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelProfile(string value)
        {
            return CreateString(ZyxelAttributeType.TUNNEL_PROFILE, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(ZyxelAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(ZyxelAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}