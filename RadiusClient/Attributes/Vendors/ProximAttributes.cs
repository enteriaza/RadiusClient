using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Proxim Wireless (IANA PEN 841) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.proxim</c>.
    /// </summary>
    /// <remarks>
    /// Proxim Wireless (formerly Western Multiplex, later acquired by GTEK/Extreme
    /// Networks) produced enterprise and carrier-class wireless networking equipment
    /// including the ORiNOCO, Tsunami, and AP-4000 series access points, wireless
    /// bridges, and controllers.
    /// </remarks>
    public enum ProximAttributeType : byte
    {
        /// <summary>Proxim-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Proxim-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>Proxim-Privilege-Level (Type 3). Integer. Administrative privilege level.</summary>
        PRIVILEGE_LEVEL = 3,

        /// <summary>Proxim-VLAN-Name (Type 4). String. VLAN name to assign.</summary>
        VLAN_NAME = 4,

        /// <summary>Proxim-VLAN-Id (Type 5). Integer. VLAN identifier to assign.</summary>
        VLAN_ID = 5,

        /// <summary>Proxim-QoS-Profile (Type 6). String. QoS profile name.</summary>
        QOS_PROFILE = 6,

        /// <summary>Proxim-Bandwidth-Max-Up (Type 7). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 7,

        /// <summary>Proxim-Bandwidth-Max-Down (Type 8). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 8,

        /// <summary>Proxim-ACL-Name (Type 9). String. ACL name to apply.</summary>
        ACL_NAME = 9,

        /// <summary>Proxim-Session-Timeout (Type 10). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 10,

        /// <summary>Proxim-Idle-Timeout (Type 11). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 11,

        /// <summary>Proxim-URL-Redirect (Type 12). String. Captive portal redirect URL.</summary>
        URL_REDIRECT = 12,

        /// <summary>Proxim-SSID (Type 13). String. Wireless SSID name.</summary>
        SSID = 13,

        /// <summary>Proxim-AP-Name (Type 14). String. Access point name.</summary>
        AP_NAME = 14
    }

    /// <summary>
    /// Proxim-Privilege-Level attribute values (Type 3).
    /// </summary>
    public enum PROXIM_PRIVILEGE_LEVEL
    {
        /// <summary>Read-only (monitoring) access.</summary>
        READ_ONLY = 0,

        /// <summary>Standard operator access.</summary>
        OPERATOR = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2,

        /// <summary>Super administrator access.</summary>
        SUPER_ADMIN = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Proxim Wireless
    /// (IANA PEN 841) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.proxim</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Proxim's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 841</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Proxim Wireless (ORiNOCO, Tsunami, AP-4000
    /// series) access points, wireless bridges, and controllers for RADIUS-based
    /// user group assignment, administrative privilege level control, VLAN
    /// assignment (by name and ID), QoS profile selection, upstream/downstream
    /// bandwidth provisioning, ACL enforcement, session and idle timeout
    /// management, captive portal URL redirection, wireless SSID identification,
    /// AP naming, and general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ProximAttributes.PrivilegeLevel(PROXIM_PRIVILEGE_LEVEL.ADMIN));
    /// packet.SetAttribute(ProximAttributes.VlanId(100));
    /// packet.SetAttribute(ProximAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(ProximAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(ProximAttributes.QosProfile("voice-priority"));
    /// </code>
    /// </remarks>
    public static class ProximAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Proxim Wireless.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 841;

        #region Integer Attributes

        /// <summary>
        /// Creates a Proxim-Privilege-Level attribute (Type 3) with the specified level.
        /// </summary>
        /// <param name="value">The administrative privilege level. See <see cref="PROXIM_PRIVILEGE_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PrivilegeLevel(PROXIM_PRIVILEGE_LEVEL value)
        {
            return CreateInteger(ProximAttributeType.PRIVILEGE_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Proxim-VLAN-Id attribute (Type 5) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier to assign.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(ProximAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Proxim-Bandwidth-Max-Up attribute (Type 7) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(ProximAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Proxim-Bandwidth-Max-Down attribute (Type 8) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(ProximAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a Proxim-Session-Timeout attribute (Type 10) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(ProximAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Proxim-Idle-Timeout attribute (Type 11) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(ProximAttributeType.IDLE_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Proxim-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(ProximAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Proxim-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(ProximAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Proxim-VLAN-Name attribute (Type 4) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(ProximAttributeType.VLAN_NAME, value);
        }

        /// <summary>
        /// Creates a Proxim-QoS-Profile attribute (Type 6) with the specified profile name.
        /// </summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value)
        {
            return CreateString(ProximAttributeType.QOS_PROFILE, value);
        }

        /// <summary>
        /// Creates a Proxim-ACL-Name attribute (Type 9) with the specified ACL name.
        /// </summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value)
        {
            return CreateString(ProximAttributeType.ACL_NAME, value);
        }

        /// <summary>
        /// Creates a Proxim-URL-Redirect attribute (Type 12) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UrlRedirect(string value)
        {
            return CreateString(ProximAttributeType.URL_REDIRECT, value);
        }

        /// <summary>
        /// Creates a Proxim-SSID attribute (Type 13) with the specified wireless SSID.
        /// </summary>
        /// <param name="value">The wireless SSID name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ssid(string value)
        {
            return CreateString(ProximAttributeType.SSID, value);
        }

        /// <summary>
        /// Creates a Proxim-AP-Name attribute (Type 14) with the specified access point name.
        /// </summary>
        /// <param name="value">The access point name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApName(string value)
        {
            return CreateString(ProximAttributeType.AP_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Proxim attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ProximAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Proxim attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ProximAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}