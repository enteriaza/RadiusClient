using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Xedia (IANA PEN 838) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.xedia</c>.
    /// </summary>
    /// <remarks>
    /// Xedia Corporation produced WAN access switches and routers (Access Point
    /// series) for enterprise branch office and service provider edge deployments,
    /// providing IP routing, multiprotocol bridging, traffic shaping, and
    /// VPN functionality. Xedia was acquired by Lucent Technologies in 1999.
    /// </remarks>
    public enum XediaAttributeType : byte
    {
        /// <summary>Xedia-DNS-Server (Type 1). String. DNS server address.</summary>
        DNS_SERVER = 1,

        /// <summary>Xedia-NetBIOS-Server (Type 2). String. NetBIOS (WINS) server address.</summary>
        NETBIOS_SERVER = 2,

        /// <summary>Xedia-Address-Pool (Type 3). String. IP address pool name.</summary>
        ADDRESS_POOL = 3,

        /// <summary>Xedia-PPP-Echo-Interval (Type 4). Integer. PPP LCP echo interval in seconds.</summary>
        PPP_ECHO_INTERVAL = 4,

        /// <summary>Xedia-SSH-Privileges (Type 5). Integer. SSH privilege level.</summary>
        SSH_PRIVILEGES = 5,

        /// <summary>Xedia-Admin-Privileges (Type 6). Integer. Administrative privilege level.</summary>
        ADMIN_PRIVILEGES = 6,

        /// <summary>Xedia-Client-Access-Network (Type 7). String. Client access network name.</summary>
        CLIENT_ACCESS_NETWORK = 7
    }

    /// <summary>
    /// Xedia-SSH-Privileges attribute values (Type 5).
    /// </summary>
    public enum XEDIA_SSH_PRIVILEGES
    {
        /// <summary>No SSH access.</summary>
        NONE = 0,

        /// <summary>Read-only SSH access.</summary>
        READ_ONLY = 1,

        /// <summary>Read-write SSH access.</summary>
        READ_WRITE = 2,

        /// <summary>Full administrative SSH access.</summary>
        ADMIN = 3
    }

    /// <summary>
    /// Xedia-Admin-Privileges attribute values (Type 6).
    /// </summary>
    public enum XEDIA_ADMIN_PRIVILEGES
    {
        /// <summary>No administrative access.</summary>
        NONE = 0,

        /// <summary>Read-only (monitoring) access.</summary>
        READ_ONLY = 1,

        /// <summary>Read-write (operator) access.</summary>
        READ_WRITE = 2,

        /// <summary>Full administrative access.</summary>
        ADMIN = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Xedia
    /// (IANA PEN 838) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.xedia</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Xedia's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 838</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Xedia (Lucent) Access Point WAN switches and
    /// routers for RADIUS-based DNS and NetBIOS server provisioning, IP address pool
    /// assignment, PPP LCP echo interval configuration, SSH and administrative
    /// privilege level assignment, and client access network selection.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(XediaAttributes.AdminPrivileges(XEDIA_ADMIN_PRIVILEGES.ADMIN));
    /// packet.SetAttribute(XediaAttributes.SshPrivileges(XEDIA_SSH_PRIVILEGES.READ_WRITE));
    /// packet.SetAttribute(XediaAttributes.AddressPool("branch-pool"));
    /// packet.SetAttribute(XediaAttributes.DnsServer("10.0.0.1"));
    /// packet.SetAttribute(XediaAttributes.PppEchoInterval(30));
    /// </code>
    /// </remarks>
    public static class XediaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Xedia Corporation.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 838;

        #region Integer Attributes

        /// <summary>
        /// Creates a Xedia-PPP-Echo-Interval attribute (Type 4) with the specified interval.
        /// </summary>
        /// <param name="value">The PPP LCP echo interval in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PppEchoInterval(int value)
        {
            return CreateInteger(XediaAttributeType.PPP_ECHO_INTERVAL, value);
        }

        /// <summary>
        /// Creates a Xedia-SSH-Privileges attribute (Type 5) with the specified level.
        /// </summary>
        /// <param name="value">The SSH privilege level. See <see cref="XEDIA_SSH_PRIVILEGES"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SshPrivileges(XEDIA_SSH_PRIVILEGES value)
        {
            return CreateInteger(XediaAttributeType.SSH_PRIVILEGES, (int)value);
        }

        /// <summary>
        /// Creates a Xedia-Admin-Privileges attribute (Type 6) with the specified level.
        /// </summary>
        /// <param name="value">The administrative privilege level. See <see cref="XEDIA_ADMIN_PRIVILEGES"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminPrivileges(XEDIA_ADMIN_PRIVILEGES value)
        {
            return CreateInteger(XediaAttributeType.ADMIN_PRIVILEGES, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Xedia-DNS-Server attribute (Type 1) with the specified server address.
        /// </summary>
        /// <param name="value">The DNS server address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DnsServer(string value)
        {
            return CreateString(XediaAttributeType.DNS_SERVER, value);
        }

        /// <summary>
        /// Creates a Xedia-NetBIOS-Server attribute (Type 2) with the specified server address.
        /// </summary>
        /// <param name="value">The NetBIOS (WINS) server address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NetBiosServer(string value)
        {
            return CreateString(XediaAttributeType.NETBIOS_SERVER, value);
        }

        /// <summary>
        /// Creates a Xedia-Address-Pool attribute (Type 3) with the specified pool name.
        /// </summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AddressPool(string value)
        {
            return CreateString(XediaAttributeType.ADDRESS_POOL, value);
        }

        /// <summary>
        /// Creates a Xedia-Client-Access-Network attribute (Type 7) with the specified network name.
        /// </summary>
        /// <param name="value">The client access network name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientAccessNetwork(string value)
        {
            return CreateString(XediaAttributeType.CLIENT_ACCESS_NETWORK, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(XediaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(XediaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}