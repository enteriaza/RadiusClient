using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a pfSense (IANA PEN 52015) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.pfsense</c>.
    /// </summary>
    /// <remarks>
    /// pfSense is an open-source firewall and router platform based on FreeBSD,
    /// widely used for network security, VPN, captive portal, and traffic shaping
    /// in enterprise and SOHO environments.
    /// </remarks>
    public enum PfsenseAttributeType : byte
    {
        /// <summary>pfSense-Bandwidth-Max-Up (Type 1). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 1,

        /// <summary>pfSense-Bandwidth-Max-Down (Type 2). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 2,

        /// <summary>pfSense-Max-Total-Octets (Type 3). Integer. Maximum total octets (data cap).</summary>
        MAX_TOTAL_OCTETS = 3,

        /// <summary>pfSense-Max-Total-Octets-Direction (Type 4). Integer. Data cap direction.</summary>
        MAX_TOTAL_OCTETS_DIRECTION = 4,

        /// <summary>pfSense-Shell-Group (Type 5). String. Shell access group name.</summary>
        SHELL_GROUP = 5,

        /// <summary>pfSense-Admin-Access (Type 6). Integer. Administrative access flag.</summary>
        ADMIN_ACCESS = 6,

        /// <summary>pfSense-User-Group (Type 7). String. User group name.</summary>
        USER_GROUP = 7,

        /// <summary>pfSense-Simultaneous-Use (Type 8). Integer. Maximum simultaneous sessions.</summary>
        SIMULTANEOUS_USE = 8,

        /// <summary>pfSense-Auth-Type (Type 9). Integer. Authentication type.</summary>
        AUTH_TYPE = 9
    }

    /// <summary>
    /// pfSense-Max-Total-Octets-Direction attribute values (Type 4).
    /// </summary>
    public enum PFSENSE_MAX_TOTAL_OCTETS_DIRECTION
    {
        /// <summary>Apply data cap to both upload and download combined.</summary>
        TOTAL = 0,

        /// <summary>Apply data cap to upload only.</summary>
        UP = 1,

        /// <summary>Apply data cap to download only.</summary>
        DOWN = 2
    }

    /// <summary>
    /// pfSense-Admin-Access attribute values (Type 6).
    /// </summary>
    public enum PFSENSE_ADMIN_ACCESS
    {
        /// <summary>No administrative access.</summary>
        DENIED = 0,

        /// <summary>Administrative access granted.</summary>
        GRANTED = 1
    }

    /// <summary>
    /// pfSense-Auth-Type attribute values (Type 9).
    /// </summary>
    public enum PFSENSE_AUTH_TYPE
    {
        /// <summary>Local authentication.</summary>
        LOCAL = 0,

        /// <summary>RADIUS authentication.</summary>
        RADIUS = 1,

        /// <summary>LDAP authentication.</summary>
        LDAP = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing pfSense
    /// (IANA PEN 52015) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.pfsense</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// pfSense's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 52015</c>.
    /// </para>
    /// <para>
    /// These attributes are used by pfSense firewall/router platforms for
    /// RADIUS-based upstream/downstream bandwidth provisioning, data cap
    /// enforcement (total octets with directional control), shell and user group
    /// assignment, administrative access control, simultaneous session limiting,
    /// and authentication type identification — primarily used with the pfSense
    /// captive portal and user management features.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(PfsenseAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(PfsenseAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(PfsenseAttributes.MaxTotalOctets(1073741824));
    /// packet.SetAttribute(PfsenseAttributes.MaxTotalOctetsDirection(PFSENSE_MAX_TOTAL_OCTETS_DIRECTION.TOTAL));
    /// packet.SetAttribute(PfsenseAttributes.UserGroup("captive-portal-users"));
    /// packet.SetAttribute(PfsenseAttributes.SimultaneousUse(1));
    /// </code>
    /// </remarks>
    public static class PfsenseAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for pfSense.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 52015;

        #region Integer Attributes

        /// <summary>
        /// Creates a pfSense-Bandwidth-Max-Up attribute (Type 1) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(PfsenseAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a pfSense-Bandwidth-Max-Down attribute (Type 2) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(PfsenseAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a pfSense-Max-Total-Octets attribute (Type 3) with the specified data cap.
        /// </summary>
        /// <param name="value">The maximum total octets (data cap).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxTotalOctets(int value)
        {
            return CreateInteger(PfsenseAttributeType.MAX_TOTAL_OCTETS, value);
        }

        /// <summary>
        /// Creates a pfSense-Max-Total-Octets-Direction attribute (Type 4) with the specified direction.
        /// </summary>
        /// <param name="value">The data cap direction. See <see cref="PFSENSE_MAX_TOTAL_OCTETS_DIRECTION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxTotalOctetsDirection(PFSENSE_MAX_TOTAL_OCTETS_DIRECTION value)
        {
            return CreateInteger(PfsenseAttributeType.MAX_TOTAL_OCTETS_DIRECTION, (int)value);
        }

        /// <summary>
        /// Creates a pfSense-Admin-Access attribute (Type 6) with the specified access flag.
        /// </summary>
        /// <param name="value">The administrative access flag. See <see cref="PFSENSE_ADMIN_ACCESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminAccess(PFSENSE_ADMIN_ACCESS value)
        {
            return CreateInteger(PfsenseAttributeType.ADMIN_ACCESS, (int)value);
        }

        /// <summary>
        /// Creates a pfSense-Simultaneous-Use attribute (Type 8) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum number of simultaneous sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SimultaneousUse(int value)
        {
            return CreateInteger(PfsenseAttributeType.SIMULTANEOUS_USE, value);
        }

        /// <summary>
        /// Creates a pfSense-Auth-Type attribute (Type 9) with the specified type.
        /// </summary>
        /// <param name="value">The authentication type. See <see cref="PFSENSE_AUTH_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthType(PFSENSE_AUTH_TYPE value)
        {
            return CreateInteger(PfsenseAttributeType.AUTH_TYPE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a pfSense-Shell-Group attribute (Type 5) with the specified group name.
        /// </summary>
        /// <param name="value">The shell access group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ShellGroup(string value)
        {
            return CreateString(PfsenseAttributeType.SHELL_GROUP, value);
        }

        /// <summary>
        /// Creates a pfSense-User-Group attribute (Type 7) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(PfsenseAttributeType.USER_GROUP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified pfSense attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(PfsenseAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified pfSense attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(PfsenseAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}