using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Shasta Networks (IANA PEN 3199) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.shasta</c>.
    /// </summary>
    /// <remarks>
    /// Shasta Networks (acquired by Nortel Networks in 2000) produced broadband
    /// service creation platforms and service node appliances (BSN 5000) for ISPs
    /// and service providers, providing per-subscriber policy enforcement,
    /// bandwidth management, and service selection.
    /// </remarks>
    public enum ShastaAttributeType : byte
    {
        /// <summary>Shasta-User-Privilege (Type 1). Integer. User privilege level.</summary>
        USER_PRIVILEGE = 1,

        /// <summary>Shasta-Service-Profile (Type 2). String. Service profile name.</summary>
        SERVICE_PROFILE = 2,

        /// <summary>Shasta-VPN-Name (Type 3). String. VPN name.</summary>
        VPN_NAME = 3,

        /// <summary>Shasta-Address-Pool (Type 4). String. IP address pool name.</summary>
        ADDRESS_POOL = 4,

        /// <summary>Shasta-Bandwidth-Up (Type 5). Integer. Upstream bandwidth in Kbps.</summary>
        BANDWIDTH_UP = 5,

        /// <summary>Shasta-Bandwidth-Down (Type 6). Integer. Downstream bandwidth in Kbps.</summary>
        BANDWIDTH_DOWN = 6,

        /// <summary>Shasta-Service-Name (Type 7). String. Service name.</summary>
        SERVICE_NAME = 7,

        /// <summary>Shasta-Service-Type (Type 8). Integer. Service type.</summary>
        SERVICE_TYPE = 8,

        /// <summary>Shasta-Primary-DNS (Type 9). IP address. Primary DNS server.</summary>
        PRIMARY_DNS = 9,

        /// <summary>Shasta-Secondary-DNS (Type 10). IP address. Secondary DNS server.</summary>
        SECONDARY_DNS = 10,

        /// <summary>Shasta-Session-Timeout (Type 11). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 11,

        /// <summary>Shasta-Idle-Timeout (Type 12). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 12,

        /// <summary>Shasta-User-Group (Type 13). String. User group name.</summary>
        USER_GROUP = 13,

        /// <summary>Shasta-AVPair (Type 14). String. Attribute-value pair string.</summary>
        AVPAIR = 14
    }

    /// <summary>
    /// Shasta-User-Privilege attribute values (Type 1).
    /// </summary>
    public enum SHASTA_USER_PRIVILEGE
    {
        /// <summary>Standard user access.</summary>
        USER = 0,

        /// <summary>Super-user (administrative) access.</summary>
        SUPER_USER = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Shasta Networks
    /// (IANA PEN 3199) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.shasta</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Shasta's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3199</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Shasta Networks (Nortel) BSN broadband service
    /// creation platforms for RADIUS-based user privilege level assignment, service
    /// profile and service name selection, VPN naming, IP address pool assignment,
    /// upstream/downstream bandwidth provisioning, DNS server provisioning,
    /// session and idle timeout management, user group mapping, service type
    /// identification, and general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ShastaAttributes.ServiceProfile("residential-50m"));
    /// packet.SetAttribute(ShastaAttributes.BandwidthUp(10000));
    /// packet.SetAttribute(ShastaAttributes.BandwidthDown(50000));
    /// packet.SetAttribute(ShastaAttributes.AddressPool("subscriber-pool-1"));
    /// packet.SetAttribute(ShastaAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(ShastaAttributes.SessionTimeout(86400));
    /// </code>
    /// </remarks>
    public static class ShastaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Shasta Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3199;

        #region Integer Attributes

        /// <summary>
        /// Creates a Shasta-User-Privilege attribute (Type 1) with the specified level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="SHASTA_USER_PRIVILEGE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserPrivilege(SHASTA_USER_PRIVILEGE value)
        {
            return CreateInteger(ShastaAttributeType.USER_PRIVILEGE, (int)value);
        }

        /// <summary>
        /// Creates a Shasta-Bandwidth-Up attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthUp(int value)
        {
            return CreateInteger(ShastaAttributeType.BANDWIDTH_UP, value);
        }

        /// <summary>
        /// Creates a Shasta-Bandwidth-Down attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthDown(int value)
        {
            return CreateInteger(ShastaAttributeType.BANDWIDTH_DOWN, value);
        }

        /// <summary>
        /// Creates a Shasta-Service-Type attribute (Type 8) with the specified type.
        /// </summary>
        /// <param name="value">The service type.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServiceType(int value)
        {
            return CreateInteger(ShastaAttributeType.SERVICE_TYPE, value);
        }

        /// <summary>
        /// Creates a Shasta-Session-Timeout attribute (Type 11) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(ShastaAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Shasta-Idle-Timeout attribute (Type 12) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(ShastaAttributeType.IDLE_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Shasta-Service-Profile attribute (Type 2) with the specified profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(ShastaAttributeType.SERVICE_PROFILE, value);
        }

        /// <summary>
        /// Creates a Shasta-VPN-Name attribute (Type 3) with the specified VPN name.
        /// </summary>
        /// <param name="value">The VPN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnName(string value)
        {
            return CreateString(ShastaAttributeType.VPN_NAME, value);
        }

        /// <summary>
        /// Creates a Shasta-Address-Pool attribute (Type 4) with the specified pool name.
        /// </summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AddressPool(string value)
        {
            return CreateString(ShastaAttributeType.ADDRESS_POOL, value);
        }

        /// <summary>
        /// Creates a Shasta-Service-Name attribute (Type 7) with the specified service name.
        /// </summary>
        /// <param name="value">The service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceName(string value)
        {
            return CreateString(ShastaAttributeType.SERVICE_NAME, value);
        }

        /// <summary>
        /// Creates a Shasta-User-Group attribute (Type 13) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(ShastaAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Shasta-AVPair attribute (Type 14) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(ShastaAttributeType.AVPAIR, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates a Shasta-Primary-DNS attribute (Type 9) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The primary DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value)
        {
            return CreateIpv4(ShastaAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates a Shasta-Secondary-DNS attribute (Type 10) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The secondary DNS server. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value)
        {
            return CreateIpv4(ShastaAttributeType.SECONDARY_DNS, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(ShastaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(ShastaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(ShastaAttributeType type, IPAddress value)
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