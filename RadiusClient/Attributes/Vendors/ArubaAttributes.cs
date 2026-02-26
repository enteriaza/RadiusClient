using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Aruba Networks / HPE Aruba (IANA PEN 14823) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.aruba</c>.
    /// </summary>
    public enum ArubaAttributeType : byte
    {
        /// <summary>Aruba-User-Role (Type 1). String. User role name to apply.</summary>
        USER_ROLE = 1,

        /// <summary>Aruba-User-Vlan (Type 2). Integer. VLAN identifier to assign.</summary>
        USER_VLAN = 2,

        /// <summary>Aruba-Priv-Admin-User (Type 3). Integer. Privileged admin user flag.</summary>
        PRIV_ADMIN_USER = 3,

        /// <summary>Aruba-Admin-Role (Type 4). String. Administrative role name.</summary>
        ADMIN_ROLE = 4,

        /// <summary>Aruba-Essid-Name (Type 5). String. ESSID (wireless network) name.</summary>
        ESSID_NAME = 5,

        /// <summary>Aruba-Location-Id (Type 6). String. Location identifier string.</summary>
        LOCATION_ID = 6,

        /// <summary>Aruba-Port-Id (Type 7). String. Port identifier string.</summary>
        PORT_ID = 7,

        /// <summary>Aruba-Template-User (Type 8). String. Template user name.</summary>
        TEMPLATE_USER = 8,

        /// <summary>Aruba-Named-User-Vlan (Type 9). String. Named VLAN assignment.</summary>
        NAMED_USER_VLAN = 9,

        /// <summary>Aruba-AP-Group (Type 10). String. Access point group name.</summary>
        AP_GROUP = 10,

        /// <summary>Aruba-Framed-IPv6-Address (Type 11). String. Framed IPv6 address.</summary>
        FRAMED_IPV6_ADDRESS = 11,

        /// <summary>Aruba-Device-Type (Type 12). String. Device type classification.</summary>
        DEVICE_TYPE = 12,

        /// <summary>Aruba-No-DHCP-Fingerprint (Type 14). Integer. Disable DHCP fingerprinting.</summary>
        NO_DHCP_FINGERPRINT = 14,

        /// <summary>Aruba-Mdps-Device-Udid (Type 15). String. MDPS device UDID.</summary>
        MDPS_DEVICE_UDID = 15,

        /// <summary>Aruba-Mdps-Device-Imei (Type 16). String. MDPS device IMEI.</summary>
        MDPS_DEVICE_IMEI = 16,

        /// <summary>Aruba-Mdps-Device-Iccid (Type 17). String. MDPS device ICCID.</summary>
        MDPS_DEVICE_ICCID = 17,

        /// <summary>Aruba-Mdps-Max-Devices (Type 18). Integer. MDPS maximum devices per user.</summary>
        MDPS_MAX_DEVICES = 18,

        /// <summary>Aruba-Mdps-Device-Name (Type 19). String. MDPS device name.</summary>
        MDPS_DEVICE_NAME = 19,

        /// <summary>Aruba-Mdps-Device-Product (Type 20). String. MDPS device product.</summary>
        MDPS_DEVICE_PRODUCT = 20,

        /// <summary>Aruba-Mdps-Device-Version (Type 21). String. MDPS device version.</summary>
        MDPS_DEVICE_VERSION = 21,

        /// <summary>Aruba-Mdps-Device-Serial (Type 22). String. MDPS device serial number.</summary>
        MDPS_DEVICE_SERIAL = 22,

        /// <summary>Aruba-CPPM-Role (Type 23). String. ClearPass Policy Manager role name.</summary>
        CPPM_ROLE = 23,

        /// <summary>Aruba-AirGroup-User-Name (Type 24). String. AirGroup user name.</summary>
        AIRGROUP_USER_NAME = 24,

        /// <summary>Aruba-AirGroup-Shared-User (Type 25). String. AirGroup shared user name.</summary>
        AIRGROUP_SHARED_USER = 25,

        /// <summary>Aruba-AirGroup-Shared-Role (Type 26). String. AirGroup shared role name.</summary>
        AIRGROUP_SHARED_ROLE = 26,

        /// <summary>Aruba-AirGroup-Device-Type (Type 27). Integer. AirGroup device type code.</summary>
        AIRGROUP_DEVICE_TYPE = 27,

        /// <summary>Aruba-Auth-Survivability (Type 28). String. Authentication survivability parameters.</summary>
        AUTH_SURVIVABILITY = 28,

        /// <summary>Aruba-AS-User-Name (Type 29). String. Authentication server user name.</summary>
        AS_USER_NAME = 29,

        /// <summary>Aruba-AS-Credential-Hash (Type 30). String. Authentication server credential hash.</summary>
        AS_CREDENTIAL_HASH = 30,

        /// <summary>Aruba-WorkSpace-App-Name (Type 31). String. Workspace application name.</summary>
        WORKSPACE_APP_NAME = 31,

        /// <summary>Aruba-Mdps-Provisioning-Settings (Type 32). String. MDPS provisioning settings.</summary>
        MDPS_PROVISIONING_SETTINGS = 32,

        /// <summary>Aruba-Mdps-Device-Profile (Type 33). String. MDPS device profile name.</summary>
        MDPS_DEVICE_PROFILE = 33,

        /// <summary>Aruba-AP-IP-Address (Type 34). IP address. Access point IP address.</summary>
        AP_IP_ADDRESS = 34,

        /// <summary>Aruba-AirGroup-Shared-Group (Type 35). String. AirGroup shared group name.</summary>
        AIRGROUP_SHARED_GROUP = 35,

        /// <summary>Aruba-User-Group (Type 36). String. User group name.</summary>
        USER_GROUP = 36,

        /// <summary>Aruba-Network-SSO-Token (Type 37). String. Network SSO token.</summary>
        NETWORK_SSO_TOKEN = 37,

        /// <summary>Aruba-AirGroup-Version (Type 38). Integer. AirGroup version.</summary>
        AIRGROUP_VERSION = 38,

        /// <summary>Aruba-Auth-SurvMethod (Type 39). Integer. Authentication survivability method.</summary>
        AUTH_SURVMETHOD = 39,

        /// <summary>Aruba-Port-Bounce-Host (Type 40). Integer. Port bounce host flag.</summary>
        PORT_BOUNCE_HOST = 40,

        /// <summary>Aruba-Calea-Server-Ip (Type 41). IP address. CALEA server IP address.</summary>
        CALEA_SERVER_IP = 41,

        /// <summary>Aruba-Admin-Path (Type 42). String. Administrative path string.</summary>
        ADMIN_PATH = 42,

        /// <summary>Aruba-Captive-Portal-URL (Type 43). String. Captive portal redirect URL.</summary>
        CAPTIVE_PORTAL_URL = 43,

        /// <summary>Aruba-MPSK-Passphrase (Type 44). String. Multi Pre-Shared Key passphrase.</summary>
        MPSK_PASSPHRASE = 44,

        /// <summary>Aruba-ACL-Server-Query-Policy (Type 45). String. ACL server query policy name.</summary>
        ACL_SERVER_QUERY_POLICY = 45,

        /// <summary>Aruba-SOS-ESSID (Type 46). String. SOS ESSID name.</summary>
        SOS_ESSID = 46,

        /// <summary>Aruba-DPP-Connector (Type 47). String. DPP connector string.</summary>
        DPP_CONNECTOR = 47,

        /// <summary>Aruba-DPP-Net-Access-Key (Type 48). String. DPP network access key.</summary>
        DPP_NET_ACCESS_KEY = 48,

        /// <summary>Aruba-DPP-Net-Access-Key-Expiry (Type 49). Integer. DPP network access key expiry in seconds.</summary>
        DPP_NET_ACCESS_KEY_EXPIRY = 49,

        /// <summary>Aruba-DPP-C-Sign-Key (Type 50). String. DPP configurator signing key.</summary>
        DPP_C_SIGN_KEY = 50
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Aruba Networks / HPE Aruba
    /// (IANA PEN 14823) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.aruba</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Aruba's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14823</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Aruba (HPE Aruba Networking) wireless controllers,
    /// switches, and ClearPass Policy Manager for user role assignment, VLAN mapping,
    /// captive portal redirection, AP/ESSID identification, MDPS device management,
    /// AirGroup sharing, DPP (Device Provisioning Protocol), MPSK, and CALEA
    /// lawful intercept configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ArubaAttributes.UserRole("employee"));
    /// packet.SetAttribute(ArubaAttributes.UserVlan(100));
    /// packet.SetAttribute(ArubaAttributes.CaptivePortalUrl("https://portal.example.com"));
    /// packet.SetAttribute(ArubaAttributes.ApIpAddress(IPAddress.Parse("10.0.1.1")));
    /// </code>
    /// </remarks>
    public static class ArubaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Aruba Networks (HPE Aruba).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14823;

        #region Integer Attributes

        /// <summary>
        /// Creates an Aruba-User-Vlan attribute (Type 2) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier to assign.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserVlan(int value)
        {
            return CreateInteger(ArubaAttributeType.USER_VLAN, value);
        }

        /// <summary>
        /// Creates an Aruba-Priv-Admin-User attribute (Type 3) with the specified flag.
        /// </summary>
        /// <param name="value">The privileged admin user flag (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivAdminUser(int value)
        {
            return CreateInteger(ArubaAttributeType.PRIV_ADMIN_USER, value);
        }

        /// <summary>
        /// Creates an Aruba-No-DHCP-Fingerprint attribute (Type 14) with the specified setting.
        /// </summary>
        /// <param name="value">Whether to disable DHCP fingerprinting (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NoDhcpFingerprint(int value)
        {
            return CreateInteger(ArubaAttributeType.NO_DHCP_FINGERPRINT, value);
        }

        /// <summary>
        /// Creates an Aruba-Mdps-Max-Devices attribute (Type 18) with the specified maximum.
        /// </summary>
        /// <param name="value">The MDPS maximum devices per user.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MdpsMaxDevices(int value)
        {
            return CreateInteger(ArubaAttributeType.MDPS_MAX_DEVICES, value);
        }

        /// <summary>
        /// Creates an Aruba-AirGroup-Device-Type attribute (Type 27) with the specified type code.
        /// </summary>
        /// <param name="value">The AirGroup device type code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AirGroupDeviceType(int value)
        {
            return CreateInteger(ArubaAttributeType.AIRGROUP_DEVICE_TYPE, value);
        }

        /// <summary>
        /// Creates an Aruba-AirGroup-Version attribute (Type 38) with the specified version.
        /// </summary>
        /// <param name="value">The AirGroup version.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AirGroupVersion(int value)
        {
            return CreateInteger(ArubaAttributeType.AIRGROUP_VERSION, value);
        }

        /// <summary>
        /// Creates an Aruba-Auth-SurvMethod attribute (Type 39) with the specified method.
        /// </summary>
        /// <param name="value">The authentication survivability method.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthSurvMethod(int value)
        {
            return CreateInteger(ArubaAttributeType.AUTH_SURVMETHOD, value);
        }

        /// <summary>
        /// Creates an Aruba-Port-Bounce-Host attribute (Type 40) with the specified flag.
        /// </summary>
        /// <param name="value">The port bounce host flag (0 = disabled, 1 = enabled).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PortBounceHost(int value)
        {
            return CreateInteger(ArubaAttributeType.PORT_BOUNCE_HOST, value);
        }

        /// <summary>
        /// Creates an Aruba-DPP-Net-Access-Key-Expiry attribute (Type 49) with the specified expiry.
        /// </summary>
        /// <param name="value">The DPP network access key expiry in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DppNetAccessKeyExpiry(int value)
        {
            return CreateInteger(ArubaAttributeType.DPP_NET_ACCESS_KEY_EXPIRY, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Aruba-User-Role attribute (Type 1) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(ArubaAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates an Aruba-Admin-Role attribute (Type 4) with the specified administrative role.
        /// </summary>
        /// <param name="value">The administrative role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminRole(string value)
        {
            return CreateString(ArubaAttributeType.ADMIN_ROLE, value);
        }

        /// <summary>
        /// Creates an Aruba-Essid-Name attribute (Type 5) with the specified ESSID name.
        /// </summary>
        /// <param name="value">The ESSID (wireless network) name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EssidName(string value)
        {
            return CreateString(ArubaAttributeType.ESSID_NAME, value);
        }

        /// <summary>
        /// Creates an Aruba-Location-Id attribute (Type 6) with the specified location identifier.
        /// </summary>
        /// <param name="value">The location identifier string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LocationId(string value)
        {
            return CreateString(ArubaAttributeType.LOCATION_ID, value);
        }

        /// <summary>
        /// Creates an Aruba-Port-Id attribute (Type 7) with the specified port identifier.
        /// </summary>
        /// <param name="value">The port identifier string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PortId(string value)
        {
            return CreateString(ArubaAttributeType.PORT_ID, value);
        }

        /// <summary>
        /// Creates an Aruba-Template-User attribute (Type 8) with the specified template user.
        /// </summary>
        /// <param name="value">The template user name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TemplateUser(string value)
        {
            return CreateString(ArubaAttributeType.TEMPLATE_USER, value);
        }

        /// <summary>
        /// Creates an Aruba-Named-User-Vlan attribute (Type 9) with the specified named VLAN.
        /// </summary>
        /// <param name="value">The named VLAN assignment. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NamedUserVlan(string value)
        {
            return CreateString(ArubaAttributeType.NAMED_USER_VLAN, value);
        }

        /// <summary>
        /// Creates an Aruba-AP-Group attribute (Type 10) with the specified AP group name.
        /// </summary>
        /// <param name="value">The access point group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApGroup(string value)
        {
            return CreateString(ArubaAttributeType.AP_GROUP, value);
        }

        /// <summary>
        /// Creates an Aruba-Framed-IPv6-Address attribute (Type 11) with the specified IPv6 address.
        /// </summary>
        /// <param name="value">The framed IPv6 address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedIpv6Address(string value)
        {
            return CreateString(ArubaAttributeType.FRAMED_IPV6_ADDRESS, value);
        }

        /// <summary>
        /// Creates an Aruba-Device-Type attribute (Type 12) with the specified device type.
        /// </summary>
        /// <param name="value">The device type classification. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DeviceType(string value)
        {
            return CreateString(ArubaAttributeType.DEVICE_TYPE, value);
        }

        /// <summary>
        /// Creates an Aruba-Mdps-Device-Udid attribute (Type 15) with the specified UDID.
        /// </summary>
        /// <param name="value">The MDPS device UDID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MdpsDeviceUdid(string value)
        {
            return CreateString(ArubaAttributeType.MDPS_DEVICE_UDID, value);
        }

        /// <summary>
        /// Creates an Aruba-Mdps-Device-Imei attribute (Type 16) with the specified IMEI.
        /// </summary>
        /// <param name="value">The MDPS device IMEI. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MdpsDeviceImei(string value)
        {
            return CreateString(ArubaAttributeType.MDPS_DEVICE_IMEI, value);
        }

        /// <summary>
        /// Creates an Aruba-Mdps-Device-Iccid attribute (Type 17) with the specified ICCID.
        /// </summary>
        /// <param name="value">The MDPS device ICCID. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MdpsDeviceIccid(string value)
        {
            return CreateString(ArubaAttributeType.MDPS_DEVICE_ICCID, value);
        }

        /// <summary>
        /// Creates an Aruba-Mdps-Device-Name attribute (Type 19) with the specified name.
        /// </summary>
        /// <param name="value">The MDPS device name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MdpsDeviceName(string value)
        {
            return CreateString(ArubaAttributeType.MDPS_DEVICE_NAME, value);
        }

        /// <summary>
        /// Creates an Aruba-Mdps-Device-Product attribute (Type 20) with the specified product.
        /// </summary>
        /// <param name="value">The MDPS device product. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MdpsDeviceProduct(string value)
        {
            return CreateString(ArubaAttributeType.MDPS_DEVICE_PRODUCT, value);
        }

        /// <summary>
        /// Creates an Aruba-Mdps-Device-Version attribute (Type 21) with the specified version.
        /// </summary>
        /// <param name="value">The MDPS device version. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MdpsDeviceVersion(string value)
        {
            return CreateString(ArubaAttributeType.MDPS_DEVICE_VERSION, value);
        }

        /// <summary>
        /// Creates an Aruba-Mdps-Device-Serial attribute (Type 22) with the specified serial number.
        /// </summary>
        /// <param name="value">The MDPS device serial number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MdpsDeviceSerial(string value)
        {
            return CreateString(ArubaAttributeType.MDPS_DEVICE_SERIAL, value);
        }

        /// <summary>
        /// Creates an Aruba-CPPM-Role attribute (Type 23) with the specified role name.
        /// </summary>
        /// <param name="value">The ClearPass Policy Manager role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CppmRole(string value)
        {
            return CreateString(ArubaAttributeType.CPPM_ROLE, value);
        }

        /// <summary>
        /// Creates an Aruba-AirGroup-User-Name attribute (Type 24) with the specified user name.
        /// </summary>
        /// <param name="value">The AirGroup user name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AirGroupUserName(string value)
        {
            return CreateString(ArubaAttributeType.AIRGROUP_USER_NAME, value);
        }

        /// <summary>
        /// Creates an Aruba-AirGroup-Shared-User attribute (Type 25) with the specified user name.
        /// </summary>
        /// <param name="value">The AirGroup shared user name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AirGroupSharedUser(string value)
        {
            return CreateString(ArubaAttributeType.AIRGROUP_SHARED_USER, value);
        }

        /// <summary>
        /// Creates an Aruba-AirGroup-Shared-Role attribute (Type 26) with the specified role name.
        /// </summary>
        /// <param name="value">The AirGroup shared role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AirGroupSharedRole(string value)
        {
            return CreateString(ArubaAttributeType.AIRGROUP_SHARED_ROLE, value);
        }

        /// <summary>
        /// Creates an Aruba-Auth-Survivability attribute (Type 28) with the specified parameters.
        /// </summary>
        /// <param name="value">The authentication survivability parameters. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthSurvivability(string value)
        {
            return CreateString(ArubaAttributeType.AUTH_SURVIVABILITY, value);
        }

        /// <summary>
        /// Creates an Aruba-AS-User-Name attribute (Type 29) with the specified user name.
        /// </summary>
        /// <param name="value">The authentication server user name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsUserName(string value)
        {
            return CreateString(ArubaAttributeType.AS_USER_NAME, value);
        }

        /// <summary>
        /// Creates an Aruba-AS-Credential-Hash attribute (Type 30) with the specified hash.
        /// </summary>
        /// <param name="value">The authentication server credential hash. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AsCredentialHash(string value)
        {
            return CreateString(ArubaAttributeType.AS_CREDENTIAL_HASH, value);
        }

        /// <summary>
        /// Creates an Aruba-WorkSpace-App-Name attribute (Type 31) with the specified application name.
        /// </summary>
        /// <param name="value">The Workspace application name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WorkSpaceAppName(string value)
        {
            return CreateString(ArubaAttributeType.WORKSPACE_APP_NAME, value);
        }

        /// <summary>
        /// Creates an Aruba-Mdps-Provisioning-Settings attribute (Type 32) with the specified settings.
        /// </summary>
        /// <param name="value">The MDPS provisioning settings. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MdpsProvisioningSettings(string value)
        {
            return CreateString(ArubaAttributeType.MDPS_PROVISIONING_SETTINGS, value);
        }

        /// <summary>
        /// Creates an Aruba-Mdps-Device-Profile attribute (Type 33) with the specified profile name.
        /// </summary>
        /// <param name="value">The MDPS device profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MdpsDeviceProfile(string value)
        {
            return CreateString(ArubaAttributeType.MDPS_DEVICE_PROFILE, value);
        }

        /// <summary>
        /// Creates an Aruba-AirGroup-Shared-Group attribute (Type 35) with the specified group name.
        /// </summary>
        /// <param name="value">The AirGroup shared group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AirGroupSharedGroup(string value)
        {
            return CreateString(ArubaAttributeType.AIRGROUP_SHARED_GROUP, value);
        }

        /// <summary>
        /// Creates an Aruba-User-Group attribute (Type 36) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(ArubaAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates an Aruba-Network-SSO-Token attribute (Type 37) with the specified token.
        /// </summary>
        /// <param name="value">The network SSO token. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NetworkSsoToken(string value)
        {
            return CreateString(ArubaAttributeType.NETWORK_SSO_TOKEN, value);
        }

        /// <summary>
        /// Creates an Aruba-Admin-Path attribute (Type 42) with the specified path.
        /// </summary>
        /// <param name="value">The administrative path string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminPath(string value)
        {
            return CreateString(ArubaAttributeType.ADMIN_PATH, value);
        }

        /// <summary>
        /// Creates an Aruba-Captive-Portal-URL attribute (Type 43) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CaptivePortalUrl(string value)
        {
            return CreateString(ArubaAttributeType.CAPTIVE_PORTAL_URL, value);
        }

        /// <summary>
        /// Creates an Aruba-MPSK-Passphrase attribute (Type 44) with the specified passphrase.
        /// </summary>
        /// <param name="value">The Multi Pre-Shared Key passphrase. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MpskPassphrase(string value)
        {
            return CreateString(ArubaAttributeType.MPSK_PASSPHRASE, value);
        }

        /// <summary>
        /// Creates an Aruba-ACL-Server-Query-Policy attribute (Type 45) with the specified policy name.
        /// </summary>
        /// <param name="value">The ACL server query policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclServerQueryPolicy(string value)
        {
            return CreateString(ArubaAttributeType.ACL_SERVER_QUERY_POLICY, value);
        }

        /// <summary>
        /// Creates an Aruba-SOS-ESSID attribute (Type 46) with the specified SOS ESSID name.
        /// </summary>
        /// <param name="value">The SOS ESSID name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SosEssid(string value)
        {
            return CreateString(ArubaAttributeType.SOS_ESSID, value);
        }

        /// <summary>
        /// Creates an Aruba-DPP-Connector attribute (Type 47) with the specified connector string.
        /// </summary>
        /// <param name="value">The DPP connector string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DppConnector(string value)
        {
            return CreateString(ArubaAttributeType.DPP_CONNECTOR, value);
        }

        /// <summary>
        /// Creates an Aruba-DPP-Net-Access-Key attribute (Type 48) with the specified key.
        /// </summary>
        /// <param name="value">The DPP network access key. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DppNetAccessKey(string value)
        {
            return CreateString(ArubaAttributeType.DPP_NET_ACCESS_KEY, value);
        }

        /// <summary>
        /// Creates an Aruba-DPP-C-Sign-Key attribute (Type 50) with the specified key.
        /// </summary>
        /// <param name="value">The DPP configurator signing key. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DppCSignKey(string value)
        {
            return CreateString(ArubaAttributeType.DPP_C_SIGN_KEY, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an Aruba-AP-IP-Address attribute (Type 34) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The access point IP address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ApIpAddress(IPAddress value)
        {
            return CreateIpv4(ArubaAttributeType.AP_IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates an Aruba-Calea-Server-Ip attribute (Type 41) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The CALEA server IP address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes CaleaServerIp(IPAddress value)
        {
            return CreateIpv4(ArubaAttributeType.CALEA_SERVER_IP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Aruba attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ArubaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Aruba attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ArubaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified Aruba attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(ArubaAttributeType type, IPAddress value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException(
                    $"Expected an IPv4 (InterNetwork) address, got '{value.AddressFamily}'.",
                    nameof(value));

            byte[] addrBytes = new byte[4];
            value.TryWriteBytes(addrBytes, out _);

            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, addrBytes);
        }

        #endregion
    }
}