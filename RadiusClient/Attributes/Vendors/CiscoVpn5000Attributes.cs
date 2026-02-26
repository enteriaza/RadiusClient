using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Cisco VPN 5000 (IANA PEN 255) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.cisco.vpn5000</c>.
    /// </summary>
    /// <remarks>
    /// The Cisco VPN 5000 series concentrator uses vendor ID 255 for its RADIUS
    /// attributes, separate from the main Cisco vendor ID 9.
    /// </remarks>
    public enum CiscoVpn5000AttributeType : byte
    {
        /// <summary>Cisco-VPN5000-Tunnel-Server (Type 1). IP address. Tunnel server address.</summary>
        TUNNEL_SERVER = 1,

        /// <summary>Cisco-VPN5000-Client-Assigned-IP (Type 2). IP address. Client assigned IP address.</summary>
        CLIENT_ASSIGNED_IP = 2,

        /// <summary>Cisco-VPN5000-Client-Real-IP (Type 3). IP address. Client real (public) IP address.</summary>
        CLIENT_REAL_IP = 3,

        /// <summary>Cisco-VPN5000-VPN-GroupInfo (Type 4). String. VPN group information.</summary>
        VPN_GROUP_INFO = 4,

        /// <summary>Cisco-VPN5000-VPN-Password (Type 5). String. VPN group password.</summary>
        VPN_PASSWORD = 5,

        /// <summary>Cisco-VPN5000-Echo (Type 6). Integer. Echo interval in seconds.</summary>
        ECHO = 6,

        /// <summary>Cisco-VPN5000-Client-Assigned-IPX (Type 7). Integer. Client assigned IPX network.</summary>
        CLIENT_ASSIGNED_IPX = 7,

        /// <summary>Cisco-VPN5000-Client-Real-IPX (Type 8). Integer. Client real IPX network.</summary>
        CLIENT_REAL_IPX = 8,

        /// <summary>Cisco-VPN5000-VPN-Client-Type (Type 9). Integer. VPN client type code.</summary>
        VPN_CLIENT_TYPE = 9,

        /// <summary>Cisco-VPN5000-Connection-Type (Type 10). Integer. Connection type code.</summary>
        CONNECTION_TYPE = 10,

        /// <summary>Cisco-VPN5000-Disconnect-Cause (Type 11). Integer. Disconnect cause code.</summary>
        DISCONNECT_CAUSE = 11,

        /// <summary>Cisco-VPN5000-Client-Firmware-Version (Type 12). Integer. Client firmware version.</summary>
        CLIENT_FIRMWARE_VERSION = 12,

        /// <summary>Cisco-VPN5000-Client-Platform (Type 13). Integer. Client platform code.</summary>
        CLIENT_PLATFORM = 13
    }

    /// <summary>
    /// Cisco-VPN5000-VPN-Client-Type attribute values (Type 9).
    /// </summary>
    public enum CISCO_VPN5000_VPN_CLIENT_TYPE
    {
        /// <summary>Windows client.</summary>
        WINDOWS = 1,

        /// <summary>Macintosh client.</summary>
        MACINTOSH = 2,

        /// <summary>DOS client.</summary>
        DOS = 3
    }

    /// <summary>
    /// Cisco-VPN5000-Connection-Type attribute values (Type 10).
    /// </summary>
    public enum CISCO_VPN5000_CONNECTION_TYPE
    {
        /// <summary>PPTP connection.</summary>
        PPTP = 1,

        /// <summary>L2TP connection.</summary>
        L2TP = 2,

        /// <summary>L2F connection.</summary>
        L2F = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Cisco VPN 5000
    /// (IANA PEN 255) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.cisco.vpn5000</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Cisco VPN 5000's vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped
    /// in a <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 255</c>.
    /// </para>
    /// <para>
    /// These attributes are used by the legacy Cisco VPN 5000 series concentrators
    /// for RADIUS-based tunnel server assignment, client IP/IPX addressing,
    /// VPN group configuration, echo intervals, client type and platform
    /// identification, connection type reporting, and disconnect cause tracking.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CiscoVpn5000Attributes.TunnelServer(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(CiscoVpn5000Attributes.ClientAssignedIp(IPAddress.Parse("192.168.1.100")));
    /// packet.SetAttribute(CiscoVpn5000Attributes.VpnGroupInfo("engineering"));
    /// packet.SetAttribute(CiscoVpn5000Attributes.Echo(30));
    /// </code>
    /// </remarks>
    public static class CiscoVpn5000Attributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Cisco VPN 5000.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 255;

        #region Integer Attributes

        /// <summary>Creates a Cisco-VPN5000-Echo attribute (Type 6).</summary>
        /// <param name="value">The echo interval in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Echo(int value) => CreateInteger(CiscoVpn5000AttributeType.ECHO, value);

        /// <summary>Creates a Cisco-VPN5000-Client-Assigned-IPX attribute (Type 7).</summary>
        /// <param name="value">The client assigned IPX network.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ClientAssignedIpx(int value) => CreateInteger(CiscoVpn5000AttributeType.CLIENT_ASSIGNED_IPX, value);

        /// <summary>Creates a Cisco-VPN5000-Client-Real-IPX attribute (Type 8).</summary>
        /// <param name="value">The client real IPX network.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ClientRealIpx(int value) => CreateInteger(CiscoVpn5000AttributeType.CLIENT_REAL_IPX, value);

        /// <summary>
        /// Creates a Cisco-VPN5000-VPN-Client-Type attribute (Type 9).
        /// </summary>
        /// <param name="value">The VPN client type. See <see cref="CISCO_VPN5000_VPN_CLIENT_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VpnClientType(CISCO_VPN5000_VPN_CLIENT_TYPE value) => CreateInteger(CiscoVpn5000AttributeType.VPN_CLIENT_TYPE, (int)value);

        /// <summary>
        /// Creates a Cisco-VPN5000-Connection-Type attribute (Type 10).
        /// </summary>
        /// <param name="value">The connection type. See <see cref="CISCO_VPN5000_CONNECTION_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ConnectionType(CISCO_VPN5000_CONNECTION_TYPE value) => CreateInteger(CiscoVpn5000AttributeType.CONNECTION_TYPE, (int)value);

        /// <summary>Creates a Cisco-VPN5000-Disconnect-Cause attribute (Type 11).</summary>
        /// <param name="value">The disconnect cause code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DisconnectCause(int value) => CreateInteger(CiscoVpn5000AttributeType.DISCONNECT_CAUSE, value);

        /// <summary>Creates a Cisco-VPN5000-Client-Firmware-Version attribute (Type 12).</summary>
        /// <param name="value">The client firmware version.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ClientFirmwareVersion(int value) => CreateInteger(CiscoVpn5000AttributeType.CLIENT_FIRMWARE_VERSION, value);

        /// <summary>Creates a Cisco-VPN5000-Client-Platform attribute (Type 13).</summary>
        /// <param name="value">The client platform code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ClientPlatform(int value) => CreateInteger(CiscoVpn5000AttributeType.CLIENT_PLATFORM, value);

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Cisco-VPN5000-VPN-GroupInfo attribute (Type 4) with the specified group info.
        /// </summary>
        /// <param name="value">The VPN group information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnGroupInfo(string value) => CreateString(CiscoVpn5000AttributeType.VPN_GROUP_INFO, value);

        /// <summary>
        /// Creates a Cisco-VPN5000-VPN-Password attribute (Type 5) with the specified password.
        /// </summary>
        /// <param name="value">The VPN group password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VpnPassword(string value) => CreateString(CiscoVpn5000AttributeType.VPN_PASSWORD, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates a Cisco-VPN5000-Tunnel-Server attribute (Type 1) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The tunnel server address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes TunnelServer(IPAddress value) => CreateIpv4(CiscoVpn5000AttributeType.TUNNEL_SERVER, value);

        /// <summary>
        /// Creates a Cisco-VPN5000-Client-Assigned-IP attribute (Type 2) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The client assigned IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientAssignedIp(IPAddress value) => CreateIpv4(CiscoVpn5000AttributeType.CLIENT_ASSIGNED_IP, value);

        /// <summary>
        /// Creates a Cisco-VPN5000-Client-Real-IP attribute (Type 3) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The client real (public) IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientRealIp(IPAddress value) => CreateIpv4(CiscoVpn5000AttributeType.CLIENT_REAL_IP, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(CiscoVpn5000AttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(CiscoVpn5000AttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(CiscoVpn5000AttributeType type, IPAddress value)
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