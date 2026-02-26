using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Palo Alto Networks (IANA PEN 25461) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.paloalto</c>.
    /// </summary>
    /// <remarks>
    /// Palo Alto Networks is a cybersecurity company producing next-generation
    /// firewalls (PA series), Panorama management, GlobalProtect VPN, and
    /// Prisma Access cloud-delivered security platforms.
    /// </remarks>
    public enum PaloAltoAttributeType : byte
    {
        /// <summary>PaloAlto-Admin-Role (Type 1). String. Administrative role name.</summary>
        ADMIN_ROLE = 1,

        /// <summary>PaloAlto-Admin-Access-Domain (Type 2). String. Administrative access domain.</summary>
        ADMIN_ACCESS_DOMAIN = 2,

        /// <summary>PaloAlto-Panorama-Admin-Role (Type 3). String. Panorama administrative role name.</summary>
        PANORAMA_ADMIN_ROLE = 3,

        /// <summary>PaloAlto-Panorama-Admin-Access-Domain (Type 4). String. Panorama administrative access domain.</summary>
        PANORAMA_ADMIN_ACCESS_DOMAIN = 4,

        /// <summary>PaloAlto-User-Group (Type 5). String. User group name.</summary>
        USER_GROUP = 5,

        /// <summary>PaloAlto-User-Domain (Type 6). String. User domain name.</summary>
        USER_DOMAIN = 6,

        /// <summary>PaloAlto-Client-Source-IP (Type 7). String. Client source IP address.</summary>
        CLIENT_SOURCE_IP = 7,

        /// <summary>PaloAlto-GlobalProtect-Gateway-Name (Type 8). String. GlobalProtect gateway name.</summary>
        GLOBALPROTECT_GATEWAY_NAME = 8,

        /// <summary>PaloAlto-GlobalProtect-Client-Type (Type 9). Integer. GlobalProtect client type.</summary>
        GLOBALPROTECT_CLIENT_TYPE = 9,

        /// <summary>PaloAlto-GlobalProtect-VPN-Type (Type 10). Integer. GlobalProtect VPN tunnel type.</summary>
        GLOBALPROTECT_VPN_TYPE = 10,

        /// <summary>PaloAlto-GlobalProtect-Client-IP (Type 11). String. GlobalProtect client IP address.</summary>
        GLOBALPROTECT_CLIENT_IP = 11,

        /// <summary>PaloAlto-GlobalProtect-Client-IPv6 (Type 12). String. GlobalProtect client IPv6 address.</summary>
        GLOBALPROTECT_CLIENT_IPV6 = 12,

        /// <summary>PaloAlto-GlobalProtect-HIP-Match (Type 13). String. GlobalProtect HIP match result.</summary>
        GLOBALPROTECT_HIP_MATCH = 13,

        /// <summary>PaloAlto-GlobalProtect-Device-Name (Type 14). String. GlobalProtect device name.</summary>
        GLOBALPROTECT_DEVICE_NAME = 14,

        /// <summary>PaloAlto-GlobalProtect-Client-OS (Type 15). String. GlobalProtect client operating system.</summary>
        GLOBALPROTECT_CLIENT_OS = 15,

        /// <summary>PaloAlto-GlobalProtect-Client-Hostname (Type 16). String. GlobalProtect client hostname.</summary>
        GLOBALPROTECT_CLIENT_HOSTNAME = 16,

        /// <summary>PaloAlto-GlobalProtect-Split-Tunneling (Type 17). Integer. GlobalProtect split tunneling policy.</summary>
        GLOBALPROTECT_SPLIT_TUNNELING = 17,

        /// <summary>PaloAlto-GlobalProtect-Timeout (Type 18). Integer. GlobalProtect session timeout in minutes.</summary>
        GLOBALPROTECT_TIMEOUT = 18,

        /// <summary>PaloAlto-GlobalProtect-Client-Config (Type 19). String. GlobalProtect client configuration name.</summary>
        GLOBALPROTECT_CLIENT_CONFIG = 19
    }

    /// <summary>
    /// PaloAlto-GlobalProtect-Client-Type attribute values (Type 9).
    /// </summary>
    public enum PALOALTO_GLOBALPROTECT_CLIENT_TYPE
    {
        /// <summary>GlobalProtect Agent (desktop client).</summary>
        AGENT = 0,

        /// <summary>GlobalProtect Clientless VPN (browser-based).</summary>
        CLIENTLESS_VPN = 1,

        /// <summary>GlobalProtect Satellite (site-to-site).</summary>
        SATELLITE = 2
    }

    /// <summary>
    /// PaloAlto-GlobalProtect-VPN-Type attribute values (Type 10).
    /// </summary>
    public enum PALOALTO_GLOBALPROTECT_VPN_TYPE
    {
        /// <summary>SSL VPN tunnel.</summary>
        SSL = 0,

        /// <summary>IPSec VPN tunnel.</summary>
        IPSEC = 1
    }

    /// <summary>
    /// PaloAlto-GlobalProtect-Split-Tunneling attribute values (Type 17).
    /// </summary>
    public enum PALOALTO_GLOBALPROTECT_SPLIT_TUNNELING
    {
        /// <summary>Split tunneling disabled (all traffic through tunnel).</summary>
        DISABLED = 0,

        /// <summary>Split tunneling enabled.</summary>
        ENABLED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Palo Alto Networks
    /// (IANA PEN 25461) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.paloalto</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Palo Alto Networks' vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped
    /// in a <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 25461</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Palo Alto Networks next-generation firewalls
    /// (PA series), Panorama management appliances, and GlobalProtect VPN gateways
    /// for RADIUS-based administrative role and access domain assignment (firewall
    /// and Panorama), user group and domain identification, GlobalProtect VPN
    /// client management (client type, VPN type, split tunneling, timeout, client
    /// IP/IPv6, HIP match, device name, client OS, hostname, client config, and
    /// gateway name), and client source IP tracking.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(PaloAltoAttributes.AdminRole("superadmin"));
    /// packet.SetAttribute(PaloAltoAttributes.AdminAccessDomain("all"));
    /// packet.SetAttribute(PaloAltoAttributes.UserGroup("vpn-users"));
    /// packet.SetAttribute(PaloAltoAttributes.GlobalProtectGatewayName("gw-us-east"));
    /// packet.SetAttribute(PaloAltoAttributes.GlobalProtectVpnType(PALOALTO_GLOBALPROTECT_VPN_TYPE.IPSEC));
    /// packet.SetAttribute(PaloAltoAttributes.GlobalProtectTimeout(480));
    /// </code>
    /// </remarks>
    public static class PaloAltoAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Palo Alto Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 25461;

        #region Integer Attributes

        /// <summary>Creates a PaloAlto-GlobalProtect-Client-Type attribute (Type 9).</summary>
        /// <param name="value">The GlobalProtect client type. See <see cref="PALOALTO_GLOBALPROTECT_CLIENT_TYPE"/>.</param>
        public static VendorSpecificAttributes GlobalProtectClientType(PALOALTO_GLOBALPROTECT_CLIENT_TYPE value)
        {
            return CreateInteger(PaloAltoAttributeType.GLOBALPROTECT_CLIENT_TYPE, (int)value);
        }

        /// <summary>Creates a PaloAlto-GlobalProtect-VPN-Type attribute (Type 10).</summary>
        /// <param name="value">The GlobalProtect VPN tunnel type. See <see cref="PALOALTO_GLOBALPROTECT_VPN_TYPE"/>.</param>
        public static VendorSpecificAttributes GlobalProtectVpnType(PALOALTO_GLOBALPROTECT_VPN_TYPE value)
        {
            return CreateInteger(PaloAltoAttributeType.GLOBALPROTECT_VPN_TYPE, (int)value);
        }

        /// <summary>Creates a PaloAlto-GlobalProtect-Split-Tunneling attribute (Type 17).</summary>
        /// <param name="value">The GlobalProtect split tunneling policy. See <see cref="PALOALTO_GLOBALPROTECT_SPLIT_TUNNELING"/>.</param>
        public static VendorSpecificAttributes GlobalProtectSplitTunneling(PALOALTO_GLOBALPROTECT_SPLIT_TUNNELING value)
        {
            return CreateInteger(PaloAltoAttributeType.GLOBALPROTECT_SPLIT_TUNNELING, (int)value);
        }

        /// <summary>Creates a PaloAlto-GlobalProtect-Timeout attribute (Type 18).</summary>
        /// <param name="value">The GlobalProtect session timeout in minutes.</param>
        public static VendorSpecificAttributes GlobalProtectTimeout(int value)
        {
            return CreateInteger(PaloAltoAttributeType.GLOBALPROTECT_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>Creates a PaloAlto-Admin-Role attribute (Type 1).</summary>
        /// <param name="value">The administrative role name (e.g. "superadmin", "deviceadmin"). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminRole(string value) => CreateString(PaloAltoAttributeType.ADMIN_ROLE, value);

        /// <summary>Creates a PaloAlto-Admin-Access-Domain attribute (Type 2).</summary>
        /// <param name="value">The administrative access domain. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminAccessDomain(string value) => CreateString(PaloAltoAttributeType.ADMIN_ACCESS_DOMAIN, value);

        /// <summary>Creates a PaloAlto-Panorama-Admin-Role attribute (Type 3).</summary>
        /// <param name="value">The Panorama administrative role name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PanoramaAdminRole(string value) => CreateString(PaloAltoAttributeType.PANORAMA_ADMIN_ROLE, value);

        /// <summary>Creates a PaloAlto-Panorama-Admin-Access-Domain attribute (Type 4).</summary>
        /// <param name="value">The Panorama administrative access domain. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PanoramaAdminAccessDomain(string value) => CreateString(PaloAltoAttributeType.PANORAMA_ADMIN_ACCESS_DOMAIN, value);

        /// <summary>Creates a PaloAlto-User-Group attribute (Type 5).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value) => CreateString(PaloAltoAttributeType.USER_GROUP, value);

        /// <summary>Creates a PaloAlto-User-Domain attribute (Type 6).</summary>
        /// <param name="value">The user domain name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserDomain(string value) => CreateString(PaloAltoAttributeType.USER_DOMAIN, value);

        /// <summary>Creates a PaloAlto-Client-Source-IP attribute (Type 7).</summary>
        /// <param name="value">The client source IP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientSourceIp(string value) => CreateString(PaloAltoAttributeType.CLIENT_SOURCE_IP, value);

        /// <summary>Creates a PaloAlto-GlobalProtect-Gateway-Name attribute (Type 8).</summary>
        /// <param name="value">The GlobalProtect gateway name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GlobalProtectGatewayName(string value) => CreateString(PaloAltoAttributeType.GLOBALPROTECT_GATEWAY_NAME, value);

        /// <summary>Creates a PaloAlto-GlobalProtect-Client-IP attribute (Type 11).</summary>
        /// <param name="value">The GlobalProtect client IP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GlobalProtectClientIp(string value) => CreateString(PaloAltoAttributeType.GLOBALPROTECT_CLIENT_IP, value);

        /// <summary>Creates a PaloAlto-GlobalProtect-Client-IPv6 attribute (Type 12).</summary>
        /// <param name="value">The GlobalProtect client IPv6 address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GlobalProtectClientIpv6(string value) => CreateString(PaloAltoAttributeType.GLOBALPROTECT_CLIENT_IPV6, value);

        /// <summary>Creates a PaloAlto-GlobalProtect-HIP-Match attribute (Type 13).</summary>
        /// <param name="value">The GlobalProtect HIP match result. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GlobalProtectHipMatch(string value) => CreateString(PaloAltoAttributeType.GLOBALPROTECT_HIP_MATCH, value);

        /// <summary>Creates a PaloAlto-GlobalProtect-Device-Name attribute (Type 14).</summary>
        /// <param name="value">The GlobalProtect device name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GlobalProtectDeviceName(string value) => CreateString(PaloAltoAttributeType.GLOBALPROTECT_DEVICE_NAME, value);

        /// <summary>Creates a PaloAlto-GlobalProtect-Client-OS attribute (Type 15).</summary>
        /// <param name="value">The GlobalProtect client operating system. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GlobalProtectClientOs(string value) => CreateString(PaloAltoAttributeType.GLOBALPROTECT_CLIENT_OS, value);

        /// <summary>Creates a PaloAlto-GlobalProtect-Client-Hostname attribute (Type 16).</summary>
        /// <param name="value">The GlobalProtect client hostname. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GlobalProtectClientHostname(string value) => CreateString(PaloAltoAttributeType.GLOBALPROTECT_CLIENT_HOSTNAME, value);

        /// <summary>Creates a PaloAlto-GlobalProtect-Client-Config attribute (Type 19).</summary>
        /// <param name="value">The GlobalProtect client configuration name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GlobalProtectClientConfig(string value) => CreateString(PaloAltoAttributeType.GLOBALPROTECT_CLIENT_CONFIG, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(PaloAltoAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(PaloAltoAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}