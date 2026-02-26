using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Trapeze Networks (IANA PEN 14525) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.trapeze</c>.
    /// </summary>
    /// <remarks>
    /// Trapeze Networks (acquired by Juniper Networks in 2010, later Extreme
    /// Networks) produced enterprise WLAN controllers and managed access points
    /// (MX/MXE series) for high-density enterprise wireless deployments.
    /// </remarks>
    public enum TrapezeAttributeType : byte
    {
        /// <summary>Trapeze-VLAN-Name (Type 1). String. VLAN name to assign.</summary>
        VLAN_NAME = 1,

        /// <summary>Trapeze-Mobility-Profile (Type 2). String. Mobility profile name.</summary>
        MOBILITY_PROFILE = 2,

        /// <summary>Trapeze-Encryption-Type (Type 3). String. Encryption type.</summary>
        ENCRYPTION_TYPE = 3,

        /// <summary>Trapeze-Time-Of-Day (Type 4). String. Time-of-day access policy.</summary>
        TIME_OF_DAY = 4,

        /// <summary>Trapeze-SSID (Type 5). String. Wireless SSID name.</summary>
        SSID = 5,

        /// <summary>Trapeze-End-Date (Type 6). String. Account end date.</summary>
        END_DATE = 6,

        /// <summary>Trapeze-Start-Date (Type 7). String. Account start date.</summary>
        START_DATE = 7,

        /// <summary>Trapeze-URL (Type 8). String. Captive portal URL.</summary>
        URL = 8,

        /// <summary>Trapeze-User-Group-Name (Type 9). String. User group name.</summary>
        USER_GROUP_NAME = 9,

        /// <summary>Trapeze-QoS-Profile (Type 10). String. QoS profile name.</summary>
        QOS_PROFILE = 10,

        /// <summary>Trapeze-Simultaneous-Logins (Type 11). String. Maximum simultaneous logins.</summary>
        SIMULTANEOUS_LOGINS = 11,

        /// <summary>Trapeze-CoA-Username (Type 12). String. Change of Authorization username.</summary>
        COA_USERNAME = 12,

        /// <summary>Trapeze-Audit (Type 13). String. Audit flag.</summary>
        AUDIT = 13
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Trapeze Networks
    /// (IANA PEN 14525) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.trapeze</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Trapeze's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14525</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Trapeze Networks (Juniper / Extreme Networks)
    /// MX/MXE WLAN controllers and access points for RADIUS-based VLAN assignment
    /// (by name), mobility profile selection, encryption type specification,
    /// time-of-day access policies, SSID identification, account start/end dates,
    /// captive portal URL redirection, user group assignment, QoS profile
    /// selection, simultaneous login limits, CoA username tracking, and audit
    /// flag configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(TrapezeAttributes.VlanName("employees"));
    /// packet.SetAttribute(TrapezeAttributes.MobilityProfile("corporate"));
    /// packet.SetAttribute(TrapezeAttributes.Ssid("Corp-WiFi"));
    /// packet.SetAttribute(TrapezeAttributes.QosProfile("voice-priority"));
    /// packet.SetAttribute(TrapezeAttributes.UserGroupName("staff"));
    /// packet.SetAttribute(TrapezeAttributes.SimultaneousLogins("3"));
    /// </code>
    /// </remarks>
    public static class TrapezeAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Trapeze Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14525;

        #region String Attributes

        /// <summary>
        /// Creates a Trapeze-VLAN-Name attribute (Type 1) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value)
        {
            return CreateString(TrapezeAttributeType.VLAN_NAME, value);
        }

        /// <summary>
        /// Creates a Trapeze-Mobility-Profile attribute (Type 2) with the specified profile name.
        /// </summary>
        /// <param name="value">The mobility profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MobilityProfile(string value)
        {
            return CreateString(TrapezeAttributeType.MOBILITY_PROFILE, value);
        }

        /// <summary>
        /// Creates a Trapeze-Encryption-Type attribute (Type 3) with the specified type.
        /// </summary>
        /// <param name="value">The encryption type. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EncryptionType(string value)
        {
            return CreateString(TrapezeAttributeType.ENCRYPTION_TYPE, value);
        }

        /// <summary>
        /// Creates a Trapeze-Time-Of-Day attribute (Type 4) with the specified policy.
        /// </summary>
        /// <param name="value">The time-of-day access policy. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TimeOfDay(string value)
        {
            return CreateString(TrapezeAttributeType.TIME_OF_DAY, value);
        }

        /// <summary>
        /// Creates a Trapeze-SSID attribute (Type 5) with the specified SSID.
        /// </summary>
        /// <param name="value">The wireless SSID name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ssid(string value)
        {
            return CreateString(TrapezeAttributeType.SSID, value);
        }

        /// <summary>
        /// Creates a Trapeze-End-Date attribute (Type 6) with the specified end date.
        /// </summary>
        /// <param name="value">The account end date. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes EndDate(string value)
        {
            return CreateString(TrapezeAttributeType.END_DATE, value);
        }

        /// <summary>
        /// Creates a Trapeze-Start-Date attribute (Type 7) with the specified start date.
        /// </summary>
        /// <param name="value">The account start date. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes StartDate(string value)
        {
            return CreateString(TrapezeAttributeType.START_DATE, value);
        }

        /// <summary>
        /// Creates a Trapeze-URL attribute (Type 8) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Url(string value)
        {
            return CreateString(TrapezeAttributeType.URL, value);
        }

        /// <summary>
        /// Creates a Trapeze-User-Group-Name attribute (Type 9) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroupName(string value)
        {
            return CreateString(TrapezeAttributeType.USER_GROUP_NAME, value);
        }

        /// <summary>
        /// Creates a Trapeze-QoS-Profile attribute (Type 10) with the specified profile name.
        /// </summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value)
        {
            return CreateString(TrapezeAttributeType.QOS_PROFILE, value);
        }

        /// <summary>
        /// Creates a Trapeze-Simultaneous-Logins attribute (Type 11) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum simultaneous logins. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SimultaneousLogins(string value)
        {
            return CreateString(TrapezeAttributeType.SIMULTANEOUS_LOGINS, value);
        }

        /// <summary>
        /// Creates a Trapeze-CoA-Username attribute (Type 12) with the specified username.
        /// </summary>
        /// <param name="value">The Change of Authorization username. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CoaUsername(string value)
        {
            return CreateString(TrapezeAttributeType.COA_USERNAME, value);
        }

        /// <summary>
        /// Creates a Trapeze-Audit attribute (Type 13) with the specified flag.
        /// </summary>
        /// <param name="value">The audit flag. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Audit(string value)
        {
            return CreateString(TrapezeAttributeType.AUDIT, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Trapeze attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(TrapezeAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}