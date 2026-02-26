using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Tropos Networks (IANA PEN 14529) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.tropos</c>.
    /// </summary>
    /// <remarks>
    /// Tropos Networks (acquired by ABB in 2012) produced outdoor mesh wireless
    /// networking equipment for utility, municipal, public safety, and
    /// transportation deployments, including mesh routers, gateways, and
    /// network management platforms for smart grid and industrial IoT
    /// applications.
    /// </remarks>
    public enum TroposAttributeType : byte
    {
        /// <summary>Tropos-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Tropos-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>Tropos-Admin-Level (Type 3). Integer. Administrative access level.</summary>
        ADMIN_LEVEL = 3,

        /// <summary>Tropos-VLAN-Id (Type 4). Integer. VLAN identifier.</summary>
        VLAN_ID = 4,

        /// <summary>Tropos-Bandwidth-Max-Up (Type 5). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 5,

        /// <summary>Tropos-Bandwidth-Max-Down (Type 6). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 6,

        /// <summary>Tropos-Session-Timeout (Type 7). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 7,

        /// <summary>Tropos-Idle-Timeout (Type 8). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 8,

        /// <summary>Tropos-QoS-Profile (Type 9). String. QoS profile name.</summary>
        QOS_PROFILE = 9,

        /// <summary>Tropos-ACL-Name (Type 10). String. ACL name to apply.</summary>
        ACL_NAME = 10,

        /// <summary>Tropos-Redirect-URL (Type 11). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 11,

        /// <summary>Tropos-Mesh-Node-Name (Type 12). String. Mesh node name.</summary>
        MESH_NODE_NAME = 12
    }

    /// <summary>
    /// Tropos-Admin-Level attribute values (Type 3).
    /// </summary>
    public enum TROPOS_ADMIN_LEVEL
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
    /// Provides strongly-typed factory methods for constructing Tropos Networks
    /// (IANA PEN 14529) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.tropos</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Tropos' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14529</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Tropos Networks (ABB) outdoor mesh wireless
    /// routers and gateways for RADIUS-based user group assignment, administrative
    /// access level control, VLAN assignment, upstream/downstream bandwidth
    /// provisioning, session and idle timeout management, QoS profile and ACL
    /// enforcement, captive portal URL redirection, mesh node identification,
    /// and general-purpose attribute-value pair configuration for utility,
    /// municipal, and smart grid wireless mesh deployments.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(TroposAttributes.AdminLevel(TROPOS_ADMIN_LEVEL.ADMIN));
    /// packet.SetAttribute(TroposAttributes.UserGroup("grid-operators"));
    /// packet.SetAttribute(TroposAttributes.VlanId(100));
    /// packet.SetAttribute(TroposAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(TroposAttributes.QosProfile("scada-priority"));
    /// </code>
    /// </remarks>
    public static class TroposAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Tropos Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14529;

        #region Integer Attributes

        /// <summary>
        /// Creates a Tropos-Admin-Level attribute (Type 3) with the specified level.
        /// </summary>
        /// <param name="value">The administrative access level. See <see cref="TROPOS_ADMIN_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminLevel(TROPOS_ADMIN_LEVEL value)
        {
            return CreateInteger(TroposAttributeType.ADMIN_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Tropos-VLAN-Id attribute (Type 4) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(TroposAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Tropos-Bandwidth-Max-Up attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(TroposAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Tropos-Bandwidth-Max-Down attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(TroposAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a Tropos-Session-Timeout attribute (Type 7) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(TroposAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Tropos-Idle-Timeout attribute (Type 8) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(TroposAttributeType.IDLE_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Tropos-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(TroposAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Tropos-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(TroposAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Tropos-QoS-Profile attribute (Type 9) with the specified profile name.
        /// </summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value)
        {
            return CreateString(TroposAttributeType.QOS_PROFILE, value);
        }

        /// <summary>
        /// Creates a Tropos-ACL-Name attribute (Type 10) with the specified ACL name.
        /// </summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value)
        {
            return CreateString(TroposAttributeType.ACL_NAME, value);
        }

        /// <summary>
        /// Creates a Tropos-Redirect-URL attribute (Type 11) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(TroposAttributeType.REDIRECT_URL, value);
        }

        /// <summary>
        /// Creates a Tropos-Mesh-Node-Name attribute (Type 12) with the specified node name.
        /// </summary>
        /// <param name="value">The mesh node name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MeshNodeName(string value)
        {
            return CreateString(TroposAttributeType.MESH_NODE_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Tropos attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(TroposAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Tropos attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(TroposAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}