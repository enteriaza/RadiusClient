using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Netscreen / Juniper Networks (IANA PEN 3224) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.netscreen</c>.
    /// </summary>
    /// <remarks>
    /// NetScreen Technologies (acquired by Juniper Networks in 2004) produced firewall
    /// and VPN security appliances. These attributes are used by Juniper ScreenOS-based
    /// platforms (SSG, ISG, NS series) and are separate from the Junos-based Juniper
    /// attributes (PEN 2636).
    /// </remarks>
    public enum NetscreenAttributeType : byte
    {
        /// <summary>NS-Admin-Privilege (Type 1). Integer. Administrative privilege level.</summary>
        ADMIN_PRIVILEGE = 1,

        /// <summary>NS-VSYS-Name (Type 2). String. Virtual system name.</summary>
        VSYS_NAME = 2,

        /// <summary>NS-User-Group (Type 3). String. User group name.</summary>
        USER_GROUP = 3,

        /// <summary>NS-Primary-DNS (Type 4). IP address. Primary DNS server.</summary>
        PRIMARY_DNS = 4,

        /// <summary>NS-Secondary-DNS (Type 5). IP address. Secondary DNS server.</summary>
        SECONDARY_DNS = 5,

        /// <summary>NS-Primary-WINS (Type 6). IP address. Primary WINS server.</summary>
        PRIMARY_WINS = 6,

        /// <summary>NS-Secondary-WINS (Type 7). IP address. Secondary WINS server.</summary>
        SECONDARY_WINS = 7,

        /// <summary>NS-NSM-User-Domain-Name (Type 8). String. NSM user domain name.</summary>
        NSM_USER_DOMAIN_NAME = 8,

        /// <summary>NS-NSM-User-Role-Mapping (Type 9). String. NSM user role mapping.</summary>
        NSM_USER_ROLE_MAPPING = 9,

        /// <summary>NS-NSM-User-Profile (Type 220). String. NSM user profile name.</summary>
        NSM_USER_PROFILE = 220
    }

    /// <summary>
    /// NS-Admin-Privilege attribute values (Type 1).
    /// </summary>
    public enum NS_ADMIN_PRIVILEGE
    {
        /// <summary>Root administrator (full access).</summary>
        ROOT_ADMIN = 1,

        /// <summary>All VSYS root administrator.</summary>
        ALL_VSYS_ROOT = 2,

        /// <summary>VSYS administrator.</summary>
        VSYS_ADMIN = 3,

        /// <summary>Read-only administrator.</summary>
        READ_ONLY_ADMIN = 4,

        /// <summary>Read-only VSYS administrator.</summary>
        READ_ONLY_VSYS = 5
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing NetScreen / Juniper
    /// Networks (IANA PEN 3224) Vendor-Specific Attributes (VSAs), as defined in the
    /// FreeRADIUS <c>dictionary.netscreen</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// NetScreen's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3224</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Juniper Networks ScreenOS-based security appliances
    /// (SSG, ISG, NS series) for RADIUS-based administrative privilege level assignment,
    /// virtual system (VSYS) selection, user group mapping, DNS and WINS server
    /// provisioning, and NSM (NetScreen Security Manager) user domain, role mapping,
    /// and profile configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(NetscreenAttributes.AdminPrivilege(NS_ADMIN_PRIVILEGE.ROOT_ADMIN));
    /// packet.SetAttribute(NetscreenAttributes.VsysName("corporate-vsys"));
    /// packet.SetAttribute(NetscreenAttributes.UserGroup("vpn-users"));
    /// packet.SetAttribute(NetscreenAttributes.PrimaryDns(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(NetscreenAttributes.SecondaryDns(IPAddress.Parse("10.0.0.2")));
    /// </code>
    /// </remarks>
    public static class NetscreenAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for NetScreen Technologies / Juniper Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3224;

        #region Integer Attributes

        /// <summary>
        /// Creates an NS-Admin-Privilege attribute (Type 1) with the specified privilege level.
        /// </summary>
        /// <param name="value">The administrative privilege level. See <see cref="NS_ADMIN_PRIVILEGE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminPrivilege(NS_ADMIN_PRIVILEGE value)
        {
            return CreateInteger(NetscreenAttributeType.ADMIN_PRIVILEGE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an NS-VSYS-Name attribute (Type 2) with the specified virtual system name.
        /// </summary>
        /// <param name="value">The virtual system name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VsysName(string value)
        {
            return CreateString(NetscreenAttributeType.VSYS_NAME, value);
        }

        /// <summary>
        /// Creates an NS-User-Group attribute (Type 3) with the specified user group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(NetscreenAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates an NS-NSM-User-Domain-Name attribute (Type 8) with the specified domain name.
        /// </summary>
        /// <param name="value">The NSM user domain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NsmUserDomainName(string value)
        {
            return CreateString(NetscreenAttributeType.NSM_USER_DOMAIN_NAME, value);
        }

        /// <summary>
        /// Creates an NS-NSM-User-Role-Mapping attribute (Type 9) with the specified role mapping.
        /// </summary>
        /// <param name="value">The NSM user role mapping. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NsmUserRoleMapping(string value)
        {
            return CreateString(NetscreenAttributeType.NSM_USER_ROLE_MAPPING, value);
        }

        /// <summary>
        /// Creates an NS-NSM-User-Profile attribute (Type 220) with the specified profile name.
        /// </summary>
        /// <param name="value">The NSM user profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NsmUserProfile(string value)
        {
            return CreateString(NetscreenAttributeType.NSM_USER_PROFILE, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an NS-Primary-DNS attribute (Type 4) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value)
        {
            return CreateIpv4(NetscreenAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates an NS-Secondary-DNS attribute (Type 5) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value)
        {
            return CreateIpv4(NetscreenAttributeType.SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates an NS-Primary-WINS attribute (Type 6) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary WINS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryWins(IPAddress value)
        {
            return CreateIpv4(NetscreenAttributeType.PRIMARY_WINS, value);
        }

        /// <summary>
        /// Creates an NS-Secondary-WINS attribute (Type 7) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary WINS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryWins(IPAddress value)
        {
            return CreateIpv4(NetscreenAttributeType.SECONDARY_WINS, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(NetscreenAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(NetscreenAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(NetscreenAttributeType type, IPAddress value)
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