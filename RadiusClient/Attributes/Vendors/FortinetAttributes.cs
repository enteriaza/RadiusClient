using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Fortinet (IANA PEN 12356) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.fortinet</c>.
    /// </summary>
    public enum FortinetAttributeType : byte
    {
        /// <summary>Fortinet-Group-Name (Type 1). String. User group name.</summary>
        GROUP_NAME = 1,

        /// <summary>Fortinet-Client-IP-Address (Type 2). String. Client IP address.</summary>
        CLIENT_IP_ADDRESS = 2,

        /// <summary>Fortinet-VDOM-Name (Type 3). String. Virtual domain (VDOM) name.</summary>
        VDOM_NAME = 3,

        /// <summary>Fortinet-Client-IPv6-Address (Type 4). String. Client IPv6 address.</summary>
        CLIENT_IPV6_ADDRESS = 4,

        /// <summary>Fortinet-Interface-Name (Type 5). String. Interface name.</summary>
        INTERFACE_NAME = 5,

        /// <summary>Fortinet-Access-Profile (Type 6). String. Access profile name.</summary>
        ACCESS_PROFILE = 6,

        /// <summary>Fortinet-SSID (Type 7). String. Wireless SSID.</summary>
        SSID = 7,

        /// <summary>Fortinet-AP-Name (Type 8). String. Access point name.</summary>
        AP_NAME = 8,

        /// <summary>Fortinet-Policy-Name (Type 9). String. Firewall policy name.</summary>
        POLICY_NAME = 9,

        /// <summary>Fortinet-User-Group (Type 10). String. User group for authorization.</summary>
        USER_GROUP = 10,

        /// <summary>Fortinet-Host-Port-AVPair (Type 11). String. Host port attribute-value pair.</summary>
        HOST_PORT_AVPAIR = 11,

        /// <summary>Fortinet-Fct-Token-SN (Type 12). String. FortiToken serial number.</summary>
        FCT_TOKEN_SN = 12,

        /// <summary>Fortinet-Webfilter-Category-Allow (Type 16). String. Web filter allowed categories.</summary>
        WEBFILTER_CATEGORY_ALLOW = 16,

        /// <summary>Fortinet-Webfilter-Category-Block (Type 17). String. Web filter blocked categories.</summary>
        WEBFILTER_CATEGORY_BLOCK = 17,

        /// <summary>Fortinet-Webfilter-Category-Monitor (Type 18). String. Web filter monitored categories.</summary>
        WEBFILTER_CATEGORY_MONITOR = 18,

        /// <summary>Fortinet-AppCtrl-Category-Allow (Type 19). String. Application control allowed categories.</summary>
        APPCTRL_CATEGORY_ALLOW = 19,

        /// <summary>Fortinet-AppCtrl-Category-Block (Type 20). String. Application control blocked categories.</summary>
        APPCTRL_CATEGORY_BLOCK = 20,

        /// <summary>Fortinet-AppCtrl-Risk-Allow (Type 21). String. Application control allowed risk levels.</summary>
        APPCTRL_RISK_ALLOW = 21,

        /// <summary>Fortinet-AppCtrl-Risk-Block (Type 22). String. Application control blocked risk levels.</summary>
        APPCTRL_RISK_BLOCK = 22,

        /// <summary>Fortinet-WirelessController-WIDS-Profile (Type 23). String. Wireless IDS profile name.</summary>
        WIRELESSCONTROLLER_WIDS_PROFILE = 23,

        /// <summary>Fortinet-WirelessController-AP-Handoff (Type 24). Integer. Wireless AP handoff flag.</summary>
        WIRELESSCONTROLLER_AP_HANDOFF = 24,

        /// <summary>Fortinet-Admin-Access (Type 25). Integer. Administrative access level.</summary>
        ADMIN_ACCESS = 25,

        /// <summary>Fortinet-Admin-VDOM (Type 26). String. Administrative VDOM name.</summary>
        ADMIN_VDOM = 26,

        /// <summary>Fortinet-Admin-SPI (Type 27). String. Administrative SPI.</summary>
        ADMIN_SPI = 27
    }

    /// <summary>
    /// Fortinet-WirelessController-AP-Handoff attribute values (Type 24).
    /// </summary>
    public enum FORTINET_WIRELESSCONTROLLER_AP_HANDOFF
    {
        /// <summary>AP handoff disabled.</summary>
        DISABLED = 0,

        /// <summary>AP handoff enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Fortinet-Admin-Access attribute values (Type 25).
    /// </summary>
    public enum FORTINET_ADMIN_ACCESS
    {
        /// <summary>No access.</summary>
        NONE = 0,

        /// <summary>Read-only access.</summary>
        READ = 1,

        /// <summary>Read-write access.</summary>
        READ_WRITE = 2,

        /// <summary>API access only.</summary>
        API = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Fortinet
    /// (IANA PEN 12356) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.fortinet</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Fortinet's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 12356</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Fortinet FortiGate firewalls, FortiAP wireless
    /// access points, FortiAuthenticator, and other Fortinet Security Fabric
    /// products for RADIUS-based user group assignment, VDOM selection, access
    /// profile mapping, firewall policy enforcement, web filter and application
    /// control category overrides, wireless SSID/AP identification, FortiToken
    /// serial binding, wireless controller IDS profiles, AP handoff control,
    /// and administrative access provisioning.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(FortinetAttributes.GroupName("VPN-Users"));
    /// packet.SetAttribute(FortinetAttributes.VdomName("root"));
    /// packet.SetAttribute(FortinetAttributes.AccessProfile("prof_admin"));
    /// packet.SetAttribute(FortinetAttributes.AdminAccess(FORTINET_ADMIN_ACCESS.READ_WRITE));
    /// packet.SetAttribute(FortinetAttributes.PolicyName("vpn-policy"));
    /// </code>
    /// </remarks>
    public static class FortinetAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Fortinet.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 12356;

        #region Integer Attributes

        /// <summary>
        /// Creates a Fortinet-WirelessController-AP-Handoff attribute (Type 24) with the specified setting.
        /// </summary>
        /// <param name="value">The AP handoff setting. See <see cref="FORTINET_WIRELESSCONTROLLER_AP_HANDOFF"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes WirelessControllerApHandoff(FORTINET_WIRELESSCONTROLLER_AP_HANDOFF value)
        {
            return CreateInteger(FortinetAttributeType.WIRELESSCONTROLLER_AP_HANDOFF, (int)value);
        }

        /// <summary>
        /// Creates a Fortinet-Admin-Access attribute (Type 25) with the specified access level.
        /// </summary>
        /// <param name="value">The administrative access level. See <see cref="FORTINET_ADMIN_ACCESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminAccess(FORTINET_ADMIN_ACCESS value)
        {
            return CreateInteger(FortinetAttributeType.ADMIN_ACCESS, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>Creates a Fortinet-Group-Name attribute (Type 1).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GroupName(string value)
        {
            return CreateString(FortinetAttributeType.GROUP_NAME, value);
        }

        /// <summary>Creates a Fortinet-Client-IP-Address attribute (Type 2).</summary>
        /// <param name="value">The client IP address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientIpAddress(string value)
        {
            return CreateString(FortinetAttributeType.CLIENT_IP_ADDRESS, value);
        }

        /// <summary>Creates a Fortinet-VDOM-Name attribute (Type 3).</summary>
        /// <param name="value">The virtual domain (VDOM) name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VdomName(string value)
        {
            return CreateString(FortinetAttributeType.VDOM_NAME, value);
        }

        /// <summary>Creates a Fortinet-Client-IPv6-Address attribute (Type 4).</summary>
        /// <param name="value">The client IPv6 address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientIpv6Address(string value)
        {
            return CreateString(FortinetAttributeType.CLIENT_IPV6_ADDRESS, value);
        }

        /// <summary>Creates a Fortinet-Interface-Name attribute (Type 5).</summary>
        /// <param name="value">The interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes InterfaceName(string value)
        {
            return CreateString(FortinetAttributeType.INTERFACE_NAME, value);
        }

        /// <summary>Creates a Fortinet-Access-Profile attribute (Type 6).</summary>
        /// <param name="value">The access profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessProfile(string value)
        {
            return CreateString(FortinetAttributeType.ACCESS_PROFILE, value);
        }

        /// <summary>Creates a Fortinet-SSID attribute (Type 7).</summary>
        /// <param name="value">The wireless SSID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ssid(string value)
        {
            return CreateString(FortinetAttributeType.SSID, value);
        }

        /// <summary>Creates a Fortinet-AP-Name attribute (Type 8).</summary>
        /// <param name="value">The access point name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApName(string value)
        {
            return CreateString(FortinetAttributeType.AP_NAME, value);
        }

        /// <summary>Creates a Fortinet-Policy-Name attribute (Type 9).</summary>
        /// <param name="value">The firewall policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(FortinetAttributeType.POLICY_NAME, value);
        }

        /// <summary>Creates a Fortinet-User-Group attribute (Type 10).</summary>
        /// <param name="value">The user group for authorization. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(FortinetAttributeType.USER_GROUP, value);
        }

        /// <summary>Creates a Fortinet-Host-Port-AVPair attribute (Type 11).</summary>
        /// <param name="value">The host port attribute-value pair. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes HostPortAvPair(string value)
        {
            return CreateString(FortinetAttributeType.HOST_PORT_AVPAIR, value);
        }

        /// <summary>Creates a Fortinet-Fct-Token-SN attribute (Type 12).</summary>
        /// <param name="value">The FortiToken serial number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FctTokenSn(string value)
        {
            return CreateString(FortinetAttributeType.FCT_TOKEN_SN, value);
        }

        /// <summary>Creates a Fortinet-Webfilter-Category-Allow attribute (Type 16).</summary>
        /// <param name="value">The web filter allowed categories. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WebfilterCategoryAllow(string value)
        {
            return CreateString(FortinetAttributeType.WEBFILTER_CATEGORY_ALLOW, value);
        }

        /// <summary>Creates a Fortinet-Webfilter-Category-Block attribute (Type 17).</summary>
        /// <param name="value">The web filter blocked categories. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WebfilterCategoryBlock(string value)
        {
            return CreateString(FortinetAttributeType.WEBFILTER_CATEGORY_BLOCK, value);
        }

        /// <summary>Creates a Fortinet-Webfilter-Category-Monitor attribute (Type 18).</summary>
        /// <param name="value">The web filter monitored categories. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WebfilterCategoryMonitor(string value)
        {
            return CreateString(FortinetAttributeType.WEBFILTER_CATEGORY_MONITOR, value);
        }

        /// <summary>Creates a Fortinet-AppCtrl-Category-Allow attribute (Type 19).</summary>
        /// <param name="value">The application control allowed categories. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AppCtrlCategoryAllow(string value)
        {
            return CreateString(FortinetAttributeType.APPCTRL_CATEGORY_ALLOW, value);
        }

        /// <summary>Creates a Fortinet-AppCtrl-Category-Block attribute (Type 20).</summary>
        /// <param name="value">The application control blocked categories. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AppCtrlCategoryBlock(string value)
        {
            return CreateString(FortinetAttributeType.APPCTRL_CATEGORY_BLOCK, value);
        }

        /// <summary>Creates a Fortinet-AppCtrl-Risk-Allow attribute (Type 21).</summary>
        /// <param name="value">The application control allowed risk levels. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AppCtrlRiskAllow(string value)
        {
            return CreateString(FortinetAttributeType.APPCTRL_RISK_ALLOW, value);
        }

        /// <summary>Creates a Fortinet-AppCtrl-Risk-Block attribute (Type 22).</summary>
        /// <param name="value">The application control blocked risk levels. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AppCtrlRiskBlock(string value)
        {
            return CreateString(FortinetAttributeType.APPCTRL_RISK_BLOCK, value);
        }

        /// <summary>Creates a Fortinet-WirelessController-WIDS-Profile attribute (Type 23).</summary>
        /// <param name="value">The wireless IDS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WirelessControllerWidsProfile(string value)
        {
            return CreateString(FortinetAttributeType.WIRELESSCONTROLLER_WIDS_PROFILE, value);
        }

        /// <summary>Creates a Fortinet-Admin-VDOM attribute (Type 26).</summary>
        /// <param name="value">The administrative VDOM name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminVdom(string value)
        {
            return CreateString(FortinetAttributeType.ADMIN_VDOM, value);
        }

        /// <summary>Creates a Fortinet-Admin-SPI attribute (Type 27).</summary>
        /// <param name="value">The administrative SPI. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminSpi(string value)
        {
            return CreateString(FortinetAttributeType.ADMIN_SPI, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Fortinet attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(FortinetAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Fortinet attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(FortinetAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}