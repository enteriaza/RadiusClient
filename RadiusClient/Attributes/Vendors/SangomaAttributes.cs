using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Sangoma Technologies (IANA PEN 35164) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.sangoma</c>.
    /// </summary>
    /// <remarks>
    /// Sangoma Technologies (acquired Digium/Asterisk in 2018) produces VoIP
    /// gateways, media transcoding appliances, session border controllers (SBCs),
    /// PBX systems (FreePBX/PBXact), and telephony interface cards for enterprise
    /// and service provider unified communications deployments.
    /// </remarks>
    public enum SangomaAttributeType : byte
    {
        /// <summary>Sangoma-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Sangoma-User-Level (Type 2). Integer. User privilege level.</summary>
        USER_LEVEL = 2,

        /// <summary>Sangoma-User-Group (Type 3). String. User group name.</summary>
        USER_GROUP = 3,

        /// <summary>Sangoma-Call-Duration (Type 4). Integer. Call duration in seconds.</summary>
        CALL_DURATION = 4,

        /// <summary>Sangoma-Called-Station-Id (Type 5). String. Called station identifier.</summary>
        CALLED_STATION_ID = 5,

        /// <summary>Sangoma-Calling-Station-Id (Type 6). String. Calling station identifier.</summary>
        CALLING_STATION_ID = 6,

        /// <summary>Sangoma-Call-Direction (Type 7). Integer. Call direction.</summary>
        CALL_DIRECTION = 7,

        /// <summary>Sangoma-Disconnect-Cause (Type 8). Integer. Call disconnect cause code.</summary>
        DISCONNECT_CAUSE = 8,

        /// <summary>Sangoma-Codec (Type 9). String. Audio codec used for the call.</summary>
        CODEC = 9,

        /// <summary>Sangoma-Trunk-Name (Type 10). String. Trunk name.</summary>
        TRUNK_NAME = 10
    }

    /// <summary>
    /// Sangoma-User-Level attribute values (Type 2).
    /// </summary>
    public enum SANGOMA_USER_LEVEL
    {
        /// <summary>Read-only access.</summary>
        READ_ONLY = 0,

        /// <summary>Standard user access.</summary>
        USER = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    /// <summary>
    /// Sangoma-Call-Direction attribute values (Type 7).
    /// </summary>
    public enum SANGOMA_CALL_DIRECTION
    {
        /// <summary>Inbound call.</summary>
        INBOUND = 1,

        /// <summary>Outbound call.</summary>
        OUTBOUND = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Sangoma Technologies
    /// (IANA PEN 35164) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.sangoma</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Sangoma's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 35164</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Sangoma Technologies VoIP gateways, session
    /// border controllers, and PBX systems (FreePBX/PBXact) for RADIUS-based
    /// user privilege level assignment, user group mapping, VoIP call detail
    /// record (CDR) accounting including call duration, called/calling station
    /// identification, call direction, disconnect cause codes, codec
    /// identification, trunk naming, and general-purpose attribute-value pair
    /// configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(SangomaAttributes.UserLevel(SANGOMA_USER_LEVEL.ADMIN));
    /// packet.SetAttribute(SangomaAttributes.UserGroup("pbx-admins"));
    /// packet.SetAttribute(SangomaAttributes.TrunkName("SIP-TRUNK-01"));
    /// </code>
    /// </remarks>
    public static class SangomaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Sangoma Technologies.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 35164;

        #region Integer Attributes

        /// <summary>
        /// Creates a Sangoma-User-Level attribute (Type 2) with the specified level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="SANGOMA_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserLevel(SANGOMA_USER_LEVEL value)
        {
            return CreateInteger(SangomaAttributeType.USER_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Sangoma-Call-Duration attribute (Type 4) with the specified duration.
        /// </summary>
        /// <param name="value">The call duration in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallDuration(int value)
        {
            return CreateInteger(SangomaAttributeType.CALL_DURATION, value);
        }

        /// <summary>
        /// Creates a Sangoma-Call-Direction attribute (Type 7) with the specified direction.
        /// </summary>
        /// <param name="value">The call direction. See <see cref="SANGOMA_CALL_DIRECTION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CallDirection(SANGOMA_CALL_DIRECTION value)
        {
            return CreateInteger(SangomaAttributeType.CALL_DIRECTION, (int)value);
        }

        /// <summary>
        /// Creates a Sangoma-Disconnect-Cause attribute (Type 8) with the specified cause code.
        /// </summary>
        /// <param name="value">The call disconnect cause code (Q.931/SIP).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DisconnectCause(int value)
        {
            return CreateInteger(SangomaAttributeType.DISCONNECT_CAUSE, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Sangoma-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(SangomaAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Sangoma-User-Group attribute (Type 3) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(SangomaAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Sangoma-Called-Station-Id attribute (Type 5) with the specified identifier.
        /// </summary>
        /// <param name="value">The called station identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledStationId(string value)
        {
            return CreateString(SangomaAttributeType.CALLED_STATION_ID, value);
        }

        /// <summary>
        /// Creates a Sangoma-Calling-Station-Id attribute (Type 6) with the specified identifier.
        /// </summary>
        /// <param name="value">The calling station identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingStationId(string value)
        {
            return CreateString(SangomaAttributeType.CALLING_STATION_ID, value);
        }

        /// <summary>
        /// Creates a Sangoma-Codec attribute (Type 9) with the specified codec name.
        /// </summary>
        /// <param name="value">The audio codec (e.g. "G.711u", "G.729", "opus"). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Codec(string value)
        {
            return CreateString(SangomaAttributeType.CODEC, value);
        }

        /// <summary>
        /// Creates a Sangoma-Trunk-Name attribute (Type 10) with the specified trunk name.
        /// </summary>
        /// <param name="value">The trunk name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TrunkName(string value)
        {
            return CreateString(SangomaAttributeType.TRUNK_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Sangoma attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(SangomaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Sangoma attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(SangomaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}