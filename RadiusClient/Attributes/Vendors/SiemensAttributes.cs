using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Siemens (IANA PEN 4329) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.siemens</c>.
    /// </summary>
    /// <remarks>
    /// Siemens AG produces a broad range of industrial networking equipment,
    /// enterprise communications platforms (HiPath, OpenScape), WLAN controllers,
    /// SCALANCE industrial switches/routers, and process automation systems.
    /// </remarks>
    public enum SiemensAttributeType : byte
    {
        /// <summary>Siemens-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Siemens-Admin-Access (Type 2). Integer. Administrative access level.</summary>
        ADMIN_ACCESS = 2,

        /// <summary>Siemens-User-Group (Type 3). String. User group name.</summary>
        USER_GROUP = 3,

        /// <summary>Siemens-User-Role (Type 4). String. User role name.</summary>
        USER_ROLE = 4,

        /// <summary>Siemens-VLAN-Id (Type 5). Integer. VLAN identifier.</summary>
        VLAN_ID = 5,

        /// <summary>Siemens-VLAN-Name (Type 6). String. VLAN name.</summary>
        VLAN_NAME = 6,

        /// <summary>Siemens-Bandwidth-Max-Up (Type 7). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 7,

        /// <summary>Siemens-Bandwidth-Max-Down (Type 8). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 8,

        /// <summary>Siemens-Session-Timeout (Type 9). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 9,

        /// <summary>Siemens-Idle-Timeout (Type 10). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 10,

        /// <summary>Siemens-ACL-Name (Type 11). String. ACL name to apply.</summary>
        ACL_NAME = 11,

        /// <summary>Siemens-Policy-Name (Type 12). String. Policy name to apply.</summary>
        POLICY_NAME = 12,

        /// <summary>Siemens-CoS (Type 13). Integer. Class of Service value.</summary>
        COS = 13,

        /// <summary>Siemens-QoS-Profile (Type 14). String. QoS profile name.</summary>
        QOS_PROFILE = 14
    }

    /// <summary>
    /// Siemens-Admin-Access attribute values (Type 2).
    /// </summary>
    public enum SIEMENS_ADMIN_ACCESS
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
    /// Provides strongly-typed factory methods for constructing Siemens
    /// (IANA PEN 4329) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.siemens</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Siemens' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 4329</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Siemens enterprise communications platforms
    /// (HiPath, OpenScape), WLAN controllers, and SCALANCE industrial networking
    /// equipment for RADIUS-based administrative access level assignment, user
    /// group and role mapping, VLAN assignment (by ID and name), upstream/downstream
    /// bandwidth provisioning, session and idle timeout management, ACL and policy
    /// enforcement, Class of Service assignment, QoS profile selection, and
    /// general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(SiemensAttributes.AdminAccess(SIEMENS_ADMIN_ACCESS.ADMIN));
    /// packet.SetAttribute(SiemensAttributes.UserRole("network-admin"));
    /// packet.SetAttribute(SiemensAttributes.VlanId(200));
    /// packet.SetAttribute(SiemensAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(SiemensAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(SiemensAttributes.PolicyName("corporate-policy"));
    /// </code>
    /// </remarks>
    public static class SiemensAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Siemens.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 4329;

        #region Integer Attributes

        /// <summary>
        /// Creates a Siemens-Admin-Access attribute (Type 2) with the specified level.
        /// </summary>
        /// <param name="value">The administrative access level. See <see cref="SIEMENS_ADMIN_ACCESS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminAccess(SIEMENS_ADMIN_ACCESS value)
        {
            return CreateInteger(SiemensAttributeType.ADMIN_ACCESS, (int)value);
        }

        /// <summary>
        /// Creates a Siemens-VLAN-Id attribute (Type 5) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(SiemensAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Siemens-Bandwidth-Max-Up attribute (Type 7) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(SiemensAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Siemens-Bandwidth-Max-Down attribute (Type 8) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(SiemensAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a Siemens-Session-Timeout attribute (Type 9) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(SiemensAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Siemens-Idle-Timeout attribute (Type 10) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(SiemensAttributeType.IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Siemens-CoS attribute (Type 13) with the specified value.
        /// </summary>
        /// <param name="value">The Class of Service value.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Cos(int value)
        {
            return CreateInteger(SiemensAttributeType.COS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Siemens-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(SiemensAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Siemens-User-Group attribute (Type 3) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(SiemensAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Siemens-User-Role attribute (Type 4) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(SiemensAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates a Siemens-VLAN-Name attribute (Type 6) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(SiemensAttributeType.VLAN_NAME, value);
        }

        /// <summary>
        /// Creates a Siemens-ACL-Name attribute (Type 11) with the specified ACL name.
        /// </summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value)
        {
            return CreateString(SiemensAttributeType.ACL_NAME, value);
        }

        /// <summary>
        /// Creates a Siemens-Policy-Name attribute (Type 12) with the specified policy name.
        /// </summary>
        /// <param name="value">The policy name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(SiemensAttributeType.POLICY_NAME, value);
        }

        /// <summary>
        /// Creates a Siemens-QoS-Profile attribute (Type 14) with the specified profile name.
        /// </summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value)
        {
            return CreateString(SiemensAttributeType.QOS_PROFILE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Siemens attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(SiemensAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Siemens attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(SiemensAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}