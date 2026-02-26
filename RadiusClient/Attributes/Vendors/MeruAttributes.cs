using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Meru Networks (IANA PEN 15983) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.meru</c>.
    /// </summary>
    /// <remarks>
    /// Meru Networks (acquired by Fortinet in 2015) produced enterprise wireless
    /// LAN controllers and access points. These attributes are used by Meru
    /// wireless controllers for RADIUS-based policy enforcement.
    /// </remarks>
    public enum MeruAttributeType : byte
    {
        /// <summary>Meru-User-Group (Type 1). String. User group name.</summary>
        USER_GROUP = 1,

        /// <summary>Meru-SSID (Type 2). String. Wireless SSID name.</summary>
        SSID = 2,

        /// <summary>Meru-AP-Name (Type 3). String. Access point name.</summary>
        AP_NAME = 3,

        /// <summary>Meru-VLAN-Name (Type 4). String. VLAN name to assign.</summary>
        VLAN_NAME = 4,

        /// <summary>Meru-Bandwidth-Max-Up (Type 5). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 5,

        /// <summary>Meru-Bandwidth-Max-Down (Type 6). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 6,

        /// <summary>Meru-URL-Redirect (Type 7). String. Captive portal redirect URL.</summary>
        URL_REDIRECT = 7,

        /// <summary>Meru-ACL-Name (Type 8). String. ACL name to apply.</summary>
        ACL_NAME = 8,

        /// <summary>Meru-Session-Timeout (Type 9). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 9,

        /// <summary>Meru-QoS-Profile (Type 10). String. QoS profile name.</summary>
        QOS_PROFILE = 10,

        /// <summary>Meru-Captive-Portal (Type 11). String. Captive portal profile name.</summary>
        CAPTIVE_PORTAL = 11,

        /// <summary>Meru-Location (Type 12). String. Location identifier.</summary>
        LOCATION = 12,

        /// <summary>Meru-Data-Rate (Type 13). Integer. Data rate in Kbps.</summary>
        DATA_RATE = 13,

        /// <summary>Meru-VLAN-Id (Type 14). Integer. VLAN identifier to assign.</summary>
        VLAN_ID = 14
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Meru Networks
    /// (IANA PEN 15983) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.meru</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Meru's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 15983</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Meru Networks (now Fortinet) enterprise
    /// wireless LAN controllers and access points for RADIUS-based user group
    /// assignment, wireless SSID and AP identification, VLAN assignment (by name
    /// and ID), upstream/downstream bandwidth provisioning, captive portal URL
    /// redirection and profile assignment, ACL enforcement, QoS profile selection,
    /// session timeout configuration, location identification, and data rate
    /// control.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(MeruAttributes.UserGroup("employees"));
    /// packet.SetAttribute(MeruAttributes.VlanId(200));
    /// packet.SetAttribute(MeruAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(MeruAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(MeruAttributes.QosProfile("voice-priority"));
    /// packet.SetAttribute(MeruAttributes.SessionTimeout(7200));
    /// </code>
    /// </remarks>
    public static class MeruAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Meru Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 15983;

        #region Integer Attributes

        /// <summary>
        /// Creates a Meru-Bandwidth-Max-Up attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(MeruAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Meru-Bandwidth-Max-Down attribute (Type 6) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(MeruAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a Meru-Session-Timeout attribute (Type 9) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(MeruAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Meru-Data-Rate attribute (Type 13) with the specified rate.
        /// </summary>
        /// <param name="value">The data rate in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DataRate(int value)
        {
            return CreateInteger(MeruAttributeType.DATA_RATE, value);
        }

        /// <summary>
        /// Creates a Meru-VLAN-Id attribute (Type 14) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier to assign.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(MeruAttributeType.VLAN_ID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Meru-User-Group attribute (Type 1) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(MeruAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Meru-SSID attribute (Type 2) with the specified wireless SSID.
        /// </summary>
        /// <param name="value">The wireless SSID name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ssid(string value)
        {
            return CreateString(MeruAttributeType.SSID, value);
        }

        /// <summary>
        /// Creates a Meru-AP-Name attribute (Type 3) with the specified access point name.
        /// </summary>
        /// <param name="value">The access point name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApName(string value)
        {
            return CreateString(MeruAttributeType.AP_NAME, value);
        }

        /// <summary>
        /// Creates a Meru-VLAN-Name attribute (Type 4) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(MeruAttributeType.VLAN_NAME, value);
        }

        /// <summary>
        /// Creates a Meru-URL-Redirect attribute (Type 7) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UrlRedirect(string value)
        {
            return CreateString(MeruAttributeType.URL_REDIRECT, value);
        }

        /// <summary>
        /// Creates a Meru-ACL-Name attribute (Type 8) with the specified ACL name.
        /// </summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value)
        {
            return CreateString(MeruAttributeType.ACL_NAME, value);
        }

        /// <summary>
        /// Creates a Meru-QoS-Profile attribute (Type 10) with the specified profile name.
        /// </summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value)
        {
            return CreateString(MeruAttributeType.QOS_PROFILE, value);
        }

        /// <summary>
        /// Creates a Meru-Captive-Portal attribute (Type 11) with the specified portal profile.
        /// </summary>
        /// <param name="value">The captive portal profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CaptivePortal(string value)
        {
            return CreateString(MeruAttributeType.CAPTIVE_PORTAL, value);
        }

        /// <summary>
        /// Creates a Meru-Location attribute (Type 12) with the specified location.
        /// </summary>
        /// <param name="value">The location identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Location(string value)
        {
            return CreateString(MeruAttributeType.LOCATION, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Meru attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(MeruAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Meru attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(MeruAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}