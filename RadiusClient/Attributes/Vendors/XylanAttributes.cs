using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Xylan (IANA PEN 800) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.xylan</c>.
    /// </summary>
    /// <remarks>
    /// Xylan Corporation produced high-performance LAN switches (OmniSwitch series)
    /// and multi-layer switching platforms for enterprise campus and data center
    /// networks. Xylan was acquired by Alcatel (now Nokia) in 1999, and the
    /// OmniSwitch line continues under the Alcatel-Lucent Enterprise (ALE) brand.
    /// </remarks>
    public enum XylanAttributeType : byte
    {
        /// <summary>Xylan-Auth-Group (Type 1). Integer. Authorization group number.</summary>
        AUTH_GROUP = 1,

        /// <summary>Xylan-Slot-Port (Type 2). String. Slot/port identifier.</summary>
        SLOT_PORT = 2,

        /// <summary>Xylan-Time-Of-Day (Type 3). String. Time-of-day access restriction.</summary>
        TIME_OF_DAY = 3,

        /// <summary>Xylan-Client-IP-Addr (Type 4). IP address. Client IP address.</summary>
        CLIENT_IP_ADDR = 4,

        /// <summary>Xylan-Group-Desc (Type 5). String. Group description.</summary>
        GROUP_DESC = 5,

        /// <summary>Xylan-Port-Desc (Type 6). String. Port description.</summary>
        PORT_DESC = 6,

        /// <summary>Xylan-Profiled-VLANs (Type 7). String. Profiled VLAN list.</summary>
        PROFILED_VLANS = 7,

        /// <summary>Xylan-Access-Priv (Type 8). Integer. Access privilege level.</summary>
        ACCESS_PRIV = 8,

        /// <summary>Xylan-VLAN-Tag (Type 9). Integer. VLAN tag identifier.</summary>
        VLAN_TAG = 9,

        /// <summary>Xylan-Acce-Priv-R1 (Type 10). Integer. Access privilege for routing domain 1.</summary>
        ACCE_PRIV_R1 = 10,

        /// <summary>Xylan-Acce-Priv-R2 (Type 11). Integer. Access privilege for routing domain 2.</summary>
        ACCE_PRIV_R2 = 11,

        /// <summary>Xylan-Acce-Priv-W1 (Type 12). Integer. Write privilege for routing domain 1.</summary>
        ACCE_PRIV_W1 = 12,

        /// <summary>Xylan-Acce-Priv-W2 (Type 13). Integer. Write privilege for routing domain 2.</summary>
        ACCE_PRIV_W2 = 13,

        /// <summary>Xylan-Acce-Priv-G1 (Type 14). Integer. Global privilege 1.</summary>
        ACCE_PRIV_G1 = 14,

        /// <summary>Xylan-Acce-Priv-G2 (Type 15). Integer. Global privilege 2.</summary>
        ACCE_PRIV_G2 = 15,

        /// <summary>Xylan-Acce-Priv-F-R1 (Type 16). Integer. Functional read privilege 1.</summary>
        ACCE_PRIV_F_R1 = 16,

        /// <summary>Xylan-Acce-Priv-F-R2 (Type 17). Integer. Functional read privilege 2.</summary>
        ACCE_PRIV_F_R2 = 17,

        /// <summary>Xylan-Acce-Priv-F-W1 (Type 18). Integer. Functional write privilege 1.</summary>
        ACCE_PRIV_F_W1 = 18,

        /// <summary>Xylan-Acce-Priv-F-W2 (Type 19). Integer. Functional write privilege 2.</summary>
        ACCE_PRIV_F_W2 = 19
    }

    /// <summary>
    /// Xylan-Access-Priv attribute values (Type 8).
    /// </summary>
    public enum XYLAN_ACCESS_PRIV
    {
        /// <summary>Read-only access.</summary>
        READ_ONLY = 0,

        /// <summary>Layer 2 read-write access.</summary>
        L2_READ_WRITE = 1,

        /// <summary>Layer 3 read-write access.</summary>
        L3_READ_WRITE = 2,

        /// <summary>Full read-write access.</summary>
        READ_WRITE_ALL = 3,

        /// <summary>Administrative access.</summary>
        ADMIN = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Xylan
    /// (IANA PEN 800) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.xylan</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Xylan's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 800</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Xylan (Alcatel-Lucent Enterprise / Nokia)
    /// OmniSwitch LAN switches for RADIUS-based authorization group assignment,
    /// slot/port identification, time-of-day access restrictions, client IP
    /// address provisioning, group and port descriptions, profiled VLAN lists,
    /// access privilege level assignment, VLAN tag configuration, and granular
    /// per-domain read/write/global/functional privilege bitmask settings for
    /// multi-layer switching administration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(XylanAttributes.AccessPriv(XYLAN_ACCESS_PRIV.ADMIN));
    /// packet.SetAttribute(XylanAttributes.AuthGroup(10));
    /// packet.SetAttribute(XylanAttributes.VlanTag(200));
    /// packet.SetAttribute(XylanAttributes.ProfiledVlans("100,200,300"));
    /// packet.SetAttribute(XylanAttributes.SlotPort("1/24"));
    /// </code>
    /// </remarks>
    public static class XylanAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Xylan Corporation.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 800;

        #region Integer Attributes

        /// <summary>Creates a Xylan-Auth-Group attribute (Type 1).</summary>
        /// <param name="value">The authorization group number.</param>
        public static VendorSpecificAttributes AuthGroup(int value) => CreateInteger(XylanAttributeType.AUTH_GROUP, value);

        /// <summary>Creates a Xylan-Access-Priv attribute (Type 8).</summary>
        /// <param name="value">The access privilege level. See <see cref="XYLAN_ACCESS_PRIV"/>.</param>
        public static VendorSpecificAttributes AccessPriv(XYLAN_ACCESS_PRIV value) => CreateInteger(XylanAttributeType.ACCESS_PRIV, (int)value);

        /// <summary>Creates a Xylan-VLAN-Tag attribute (Type 9).</summary>
        /// <param name="value">The VLAN tag identifier.</param>
        public static VendorSpecificAttributes VlanTag(int value) => CreateInteger(XylanAttributeType.VLAN_TAG, value);

        /// <summary>Creates a Xylan-Acce-Priv-R1 attribute (Type 10).</summary>
        /// <param name="value">The access privilege for routing domain 1.</param>
        public static VendorSpecificAttributes AccePrivR1(int value) => CreateInteger(XylanAttributeType.ACCE_PRIV_R1, value);

        /// <summary>Creates a Xylan-Acce-Priv-R2 attribute (Type 11).</summary>
        /// <param name="value">The access privilege for routing domain 2.</param>
        public static VendorSpecificAttributes AccePrivR2(int value) => CreateInteger(XylanAttributeType.ACCE_PRIV_R2, value);

        /// <summary>Creates a Xylan-Acce-Priv-W1 attribute (Type 12).</summary>
        /// <param name="value">The write privilege for routing domain 1.</param>
        public static VendorSpecificAttributes AccePrivW1(int value) => CreateInteger(XylanAttributeType.ACCE_PRIV_W1, value);

        /// <summary>Creates a Xylan-Acce-Priv-W2 attribute (Type 13).</summary>
        /// <param name="value">The write privilege for routing domain 2.</param>
        public static VendorSpecificAttributes AccePrivW2(int value) => CreateInteger(XylanAttributeType.ACCE_PRIV_W2, value);

        /// <summary>Creates a Xylan-Acce-Priv-G1 attribute (Type 14).</summary>
        /// <param name="value">The global privilege 1.</param>
        public static VendorSpecificAttributes AccePrivG1(int value) => CreateInteger(XylanAttributeType.ACCE_PRIV_G1, value);

        /// <summary>Creates a Xylan-Acce-Priv-G2 attribute (Type 15).</summary>
        /// <param name="value">The global privilege 2.</param>
        public static VendorSpecificAttributes AccePrivG2(int value) => CreateInteger(XylanAttributeType.ACCE_PRIV_G2, value);

        /// <summary>Creates a Xylan-Acce-Priv-F-R1 attribute (Type 16).</summary>
        /// <param name="value">The functional read privilege 1.</param>
        public static VendorSpecificAttributes AccePrivFR1(int value) => CreateInteger(XylanAttributeType.ACCE_PRIV_F_R1, value);

        /// <summary>Creates a Xylan-Acce-Priv-F-R2 attribute (Type 17).</summary>
        /// <param name="value">The functional read privilege 2.</param>
        public static VendorSpecificAttributes AccePrivFR2(int value) => CreateInteger(XylanAttributeType.ACCE_PRIV_F_R2, value);

        /// <summary>Creates a Xylan-Acce-Priv-F-W1 attribute (Type 18).</summary>
        /// <param name="value">The functional write privilege 1.</param>
        public static VendorSpecificAttributes AccePrivFW1(int value) => CreateInteger(XylanAttributeType.ACCE_PRIV_F_W1, value);

        /// <summary>Creates a Xylan-Acce-Priv-F-W2 attribute (Type 19).</summary>
        /// <param name="value">The functional write privilege 2.</param>
        public static VendorSpecificAttributes AccePrivFW2(int value) => CreateInteger(XylanAttributeType.ACCE_PRIV_F_W2, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Xylan-Slot-Port attribute (Type 2).</summary>
        /// <param name="value">The slot/port identifier (e.g. "1/24"). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SlotPort(string value) => CreateString(XylanAttributeType.SLOT_PORT, value);

        /// <summary>Creates a Xylan-Time-Of-Day attribute (Type 3).</summary>
        /// <param name="value">The time-of-day access restriction. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TimeOfDay(string value) => CreateString(XylanAttributeType.TIME_OF_DAY, value);

        /// <summary>Creates a Xylan-Group-Desc attribute (Type 5).</summary>
        /// <param name="value">The group description. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GroupDesc(string value) => CreateString(XylanAttributeType.GROUP_DESC, value);

        /// <summary>Creates a Xylan-Port-Desc attribute (Type 6).</summary>
        /// <param name="value">The port description. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PortDesc(string value) => CreateString(XylanAttributeType.PORT_DESC, value);

        /// <summary>Creates a Xylan-Profiled-VLANs attribute (Type 7).</summary>
        /// <param name="value">The profiled VLAN list (e.g. "100,200,300"). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProfiledVlans(string value) => CreateString(XylanAttributeType.PROFILED_VLANS, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a Xylan-Client-IP-Addr attribute (Type 4).</summary>
        /// <param name="value">The client IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientIpAddr(IPAddress value)
        {
            return CreateIpv4(XylanAttributeType.CLIENT_IP_ADDR, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(XylanAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(XylanAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(XylanAttributeType type, IPAddress value)
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