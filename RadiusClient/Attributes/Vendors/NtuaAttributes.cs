using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an NTUA (National Technical University of Athens) (IANA PEN 969)
    /// vendor-specific RADIUS attribute type, as defined in the FreeRADIUS
    /// <c>dictionary.ntua</c>.
    /// </summary>
    /// <remarks>
    /// The National Technical University of Athens (NTUA / Εθνικό Μετσόβιο
    /// Πολυτεχνείο) is a Greek research university that defined these RADIUS
    /// attributes for managing network access on its campus infrastructure.
    /// </remarks>
    public enum NtuaAttributeType : byte
    {
        /// <summary>NTUA-User-Group (Type 1). String. User group name.</summary>
        USER_GROUP = 1,

        /// <summary>NTUA-AVPair (Type 2). String. Attribute-value pair string.</summary>
        AVPAIR = 2,

        /// <summary>NTUA-VLAN-Id (Type 3). Integer. VLAN identifier.</summary>
        VLAN_ID = 3,

        /// <summary>NTUA-Bandwidth-Max-Up (Type 4). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 4,

        /// <summary>NTUA-Bandwidth-Max-Down (Type 5). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 5,

        /// <summary>NTUA-Session-Timeout (Type 6). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 6,

        /// <summary>NTUA-Idle-Timeout (Type 7). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 7,

        /// <summary>NTUA-Redirect-URL (Type 8). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 8
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing NTUA (National
    /// Technical University of Athens) (IANA PEN 969) Vendor-Specific Attributes
    /// (VSAs), as defined in the FreeRADIUS <c>dictionary.ntua</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// NTUA's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 969</c>.
    /// </para>
    /// <para>
    /// These attributes are used by NTUA campus network infrastructure for
    /// RADIUS-based user group assignment, VLAN assignment, upstream/downstream
    /// bandwidth provisioning, session and idle timeout management, captive
    /// portal URL redirection, and general-purpose attribute-value pair
    /// configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(NtuaAttributes.UserGroup("students"));
    /// packet.SetAttribute(NtuaAttributes.VlanId(200));
    /// packet.SetAttribute(NtuaAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(NtuaAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(NtuaAttributes.SessionTimeout(86400));
    /// </code>
    /// </remarks>
    public static class NtuaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for NTUA.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 969;

        #region Integer Attributes

        /// <summary>
        /// Creates an NTUA-VLAN-Id attribute (Type 3) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(NtuaAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates an NTUA-Bandwidth-Max-Up attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(NtuaAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates an NTUA-Bandwidth-Max-Down attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(NtuaAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates an NTUA-Session-Timeout attribute (Type 6) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(NtuaAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates an NTUA-Idle-Timeout attribute (Type 7) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(NtuaAttributeType.IDLE_TIMEOUT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an NTUA-User-Group attribute (Type 1) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(NtuaAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates an NTUA-AVPair attribute (Type 2) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(NtuaAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates an NTUA-Redirect-URL attribute (Type 8) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(NtuaAttributeType.REDIRECT_URL, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified NTUA attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(NtuaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified NTUA attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(NtuaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}