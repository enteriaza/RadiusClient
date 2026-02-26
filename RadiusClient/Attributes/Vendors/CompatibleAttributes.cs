using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Compatible Systems (IANA PEN 255) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.compatible</c>.
    /// </summary>
    public enum CompatibleAttributeType : byte
    {
        /// <summary>Compatible-Tunnel-Server-Endpoint (Type 1). String. Tunnel server endpoint address.</summary>
        TUNNEL_SERVER_ENDPOINT = 1,

        /// <summary>Compatible-Tunnel-Type (Type 2). Integer. Tunnel type code.</summary>
        TUNNEL_TYPE = 2,

        /// <summary>Compatible-Tunnel-Medium-Type (Type 3). Integer. Tunnel medium type code.</summary>
        TUNNEL_MEDIUM_TYPE = 3,

        /// <summary>Compatible-Tunnel-Client-Endpoint (Type 4). String. Tunnel client endpoint address.</summary>
        TUNNEL_CLIENT_ENDPOINT = 4,

        /// <summary>Compatible-Tunnel-Password (Type 5). String. Tunnel password.</summary>
        TUNNEL_PASSWORD = 5,

        /// <summary>Compatible-Tunnel-Private-Group-Id (Type 6). String. Tunnel private group identifier.</summary>
        TUNNEL_PRIVATE_GROUP_ID = 6,

        /// <summary>Compatible-Tunnel-Assignment-Id (Type 7). String. Tunnel assignment identifier.</summary>
        TUNNEL_ASSIGNMENT_ID = 7
    }

    /// <summary>
    /// Compatible-Tunnel-Type attribute values (Type 2).
    /// </summary>
    public enum COMPATIBLE_TUNNEL_TYPE
    {
        /// <summary>PPTP tunnel.</summary>
        PPTP = 1,

        /// <summary>L2F tunnel.</summary>
        L2F = 2,

        /// <summary>L2TP tunnel.</summary>
        L2TP = 3,

        /// <summary>ATMP tunnel.</summary>
        ATMP = 4,

        /// <summary>VTP tunnel.</summary>
        VTP = 5,

        /// <summary>AH tunnel.</summary>
        AH = 6,

        /// <summary>IP-IP-Encap tunnel.</summary>
        IP_IP_ENCAP = 7,

        /// <summary>MIN-IP-IP tunnel.</summary>
        MIN_IP_IP = 8,

        /// <summary>ESP tunnel.</summary>
        ESP = 9,

        /// <summary>GRE tunnel.</summary>
        GRE = 10,

        /// <summary>DVS tunnel.</summary>
        DVS = 11,

        /// <summary>IP-in-IP tunnelling.</summary>
        IP_IN_IP = 12
    }

    /// <summary>
    /// Compatible-Tunnel-Medium-Type attribute values (Type 3).
    /// </summary>
    public enum COMPATIBLE_TUNNEL_MEDIUM_TYPE
    {
        /// <summary>IPv4 medium.</summary>
        IPV4 = 1,

        /// <summary>IPv6 medium.</summary>
        IPV6 = 2,

        /// <summary>NSAP medium.</summary>
        NSAP = 3,

        /// <summary>HDLC medium.</summary>
        HDLC = 4,

        /// <summary>BBN 1822 medium.</summary>
        BBN_1822 = 5,

        /// <summary>IEEE 802 medium.</summary>
        IEEE_802 = 6,

        /// <summary>E.163 medium.</summary>
        E_163 = 7,

        /// <summary>E.164 medium.</summary>
        E_164 = 8,

        /// <summary>F.69 medium.</summary>
        F_69 = 9,

        /// <summary>X.121 medium.</summary>
        X_121 = 10,

        /// <summary>IPX medium.</summary>
        IPX = 11,

        /// <summary>Appletalk medium.</summary>
        APPLETALK = 12,

        /// <summary>DECnet IV medium.</summary>
        DECNET_IV = 13,

        /// <summary>Banyan Vines medium.</summary>
        BANYAN_VINES = 14,

        /// <summary>E.164 with NSAP sub-address medium.</summary>
        E_164_NSAP = 15
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Compatible Systems
    /// (IANA PEN 255) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.compatible</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Compatible Systems' vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 255</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Compatible Systems (now part of Cisco) RISC routers
    /// and VPN gateways to encode tunnel-related attributes in vendor-specific form.
    /// They mirror the standard RFC 2868 tunnel attributes but are carried as VSAs
    /// for compatibility with older NAS devices that predate the tunnel attribute
    /// standards.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CompatibleAttributes.TunnelType(COMPATIBLE_TUNNEL_TYPE.L2TP));
    /// packet.SetAttribute(CompatibleAttributes.TunnelMediumType(COMPATIBLE_TUNNEL_MEDIUM_TYPE.IPV4));
    /// packet.SetAttribute(CompatibleAttributes.TunnelServerEndpoint("10.0.0.1"));
    /// packet.SetAttribute(CompatibleAttributes.TunnelPrivateGroupId("vpn-group-1"));
    /// </code>
    /// </remarks>
    public static class CompatibleAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Compatible Systems.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 255;

        #region Integer Attributes

        /// <summary>
        /// Creates a Compatible-Tunnel-Type attribute (Type 2) with the specified tunnel type.
        /// </summary>
        /// <param name="value">The tunnel type. See <see cref="COMPATIBLE_TUNNEL_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelType(COMPATIBLE_TUNNEL_TYPE value)
        {
            return CreateInteger(CompatibleAttributeType.TUNNEL_TYPE, (int)value);
        }

        /// <summary>
        /// Creates a Compatible-Tunnel-Medium-Type attribute (Type 3) with the specified medium.
        /// </summary>
        /// <param name="value">The tunnel medium type. See <see cref="COMPATIBLE_TUNNEL_MEDIUM_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelMediumType(COMPATIBLE_TUNNEL_MEDIUM_TYPE value)
        {
            return CreateInteger(CompatibleAttributeType.TUNNEL_MEDIUM_TYPE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Compatible-Tunnel-Server-Endpoint attribute (Type 1) with the specified endpoint.
        /// </summary>
        /// <param name="value">The tunnel server endpoint address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelServerEndpoint(string value)
        {
            return CreateString(CompatibleAttributeType.TUNNEL_SERVER_ENDPOINT, value);
        }

        /// <summary>
        /// Creates a Compatible-Tunnel-Client-Endpoint attribute (Type 4) with the specified endpoint.
        /// </summary>
        /// <param name="value">The tunnel client endpoint address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelClientEndpoint(string value)
        {
            return CreateString(CompatibleAttributeType.TUNNEL_CLIENT_ENDPOINT, value);
        }

        /// <summary>
        /// Creates a Compatible-Tunnel-Password attribute (Type 5) with the specified password.
        /// </summary>
        /// <param name="value">The tunnel password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelPassword(string value)
        {
            return CreateString(CompatibleAttributeType.TUNNEL_PASSWORD, value);
        }

        /// <summary>
        /// Creates a Compatible-Tunnel-Private-Group-Id attribute (Type 6) with the specified group ID.
        /// </summary>
        /// <param name="value">The tunnel private group identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelPrivateGroupId(string value)
        {
            return CreateString(CompatibleAttributeType.TUNNEL_PRIVATE_GROUP_ID, value);
        }

        /// <summary>
        /// Creates a Compatible-Tunnel-Assignment-Id attribute (Type 7) with the specified assignment ID.
        /// </summary>
        /// <param name="value">The tunnel assignment identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TunnelAssignmentId(string value)
        {
            return CreateString(CompatibleAttributeType.TUNNEL_ASSIGNMENT_ID, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Compatible attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(CompatibleAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Compatible attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(CompatibleAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}