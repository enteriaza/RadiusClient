using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a BroadSoft (IANA PEN 6431) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.broadsoft</c>.
    /// </summary>
    public enum BroadSoftAttributeType : byte
    {
        /// <summary>BroadSoft-Profile-Name (Type 1). String. Subscriber profile name.</summary>
        PROFILE_NAME = 1,

        /// <summary>BroadSoft-Num-In-Group (Type 2). Integer. Number of members in the group.</summary>
        NUM_IN_GROUP = 2,

        /// <summary>BroadSoft-Policy-Name (Type 3). String. Policy name to apply.</summary>
        POLICY_NAME = 3,

        /// <summary>BroadSoft-Calling-Line-Id (Type 4). String. Calling line identifier.</summary>
        CALLING_LINE_ID = 4,

        /// <summary>BroadSoft-Called-Line-Id (Type 5). String. Called line identifier.</summary>
        CALLED_LINE_ID = 5,

        /// <summary>BroadSoft-Service-Provider-Id (Type 6). String. Service provider identifier.</summary>
        SERVICE_PROVIDER_ID = 6,

        /// <summary>BroadSoft-Group-Id (Type 7). String. Group identifier.</summary>
        GROUP_ID = 7,

        /// <summary>BroadSoft-User-Id (Type 8). String. User identifier.</summary>
        USER_ID = 8,

        /// <summary>BroadSoft-Device-Name (Type 9). String. Device name.</summary>
        DEVICE_NAME = 9,

        /// <summary>BroadSoft-Authorization-Code (Type 10). String. Authorization code.</summary>
        AUTHORIZATION_CODE = 10,

        /// <summary>BroadSoft-Other-Party-Name (Type 11). String. Other party name.</summary>
        OTHER_PARTY_NAME = 11
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing BroadSoft
    /// (IANA PEN 6431) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.broadsoft</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// BroadSoft's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 6431</c>.
    /// </para>
    /// <para>
    /// These attributes are used by BroadSoft (now Cisco BroadWorks) VoIP and unified
    /// communications platforms for subscriber profile identification, calling/called
    /// line identity, service provider and group context, device naming, authorization
    /// codes, and call party information.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(BroadSoftAttributes.ProfileName("premium-voip"));
    /// packet.SetAttribute(BroadSoftAttributes.UserId("user@example.com"));
    /// packet.SetAttribute(BroadSoftAttributes.ServiceProviderId("SP-001"));
    /// packet.SetAttribute(BroadSoftAttributes.GroupId("GRP-100"));
    /// </code>
    /// </remarks>
    public static class BroadSoftAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for BroadSoft (Cisco BroadWorks).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 6431;

        #region Integer Attributes

        /// <summary>
        /// Creates a BroadSoft-Num-In-Group attribute (Type 2) with the specified count.
        /// </summary>
        /// <param name="value">The number of members in the group.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NumInGroup(int value)
        {
            return CreateInteger(BroadSoftAttributeType.NUM_IN_GROUP, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a BroadSoft-Profile-Name attribute (Type 1) with the specified profile name.
        /// </summary>
        /// <param name="value">The subscriber profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProfileName(string value)
        {
            return CreateString(BroadSoftAttributeType.PROFILE_NAME, value);
        }

        /// <summary>
        /// Creates a BroadSoft-Policy-Name attribute (Type 3) with the specified policy name.
        /// </summary>
        /// <param name="value">The policy name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(BroadSoftAttributeType.POLICY_NAME, value);
        }

        /// <summary>
        /// Creates a BroadSoft-Calling-Line-Id attribute (Type 4) with the specified calling line ID.
        /// </summary>
        /// <param name="value">The calling line identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingLineId(string value)
        {
            return CreateString(BroadSoftAttributeType.CALLING_LINE_ID, value);
        }

        /// <summary>
        /// Creates a BroadSoft-Called-Line-Id attribute (Type 5) with the specified called line ID.
        /// </summary>
        /// <param name="value">The called line identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledLineId(string value)
        {
            return CreateString(BroadSoftAttributeType.CALLED_LINE_ID, value);
        }

        /// <summary>
        /// Creates a BroadSoft-Service-Provider-Id attribute (Type 6) with the specified provider ID.
        /// </summary>
        /// <param name="value">The service provider identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProviderId(string value)
        {
            return CreateString(BroadSoftAttributeType.SERVICE_PROVIDER_ID, value);
        }

        /// <summary>
        /// Creates a BroadSoft-Group-Id attribute (Type 7) with the specified group ID.
        /// </summary>
        /// <param name="value">The group identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GroupId(string value)
        {
            return CreateString(BroadSoftAttributeType.GROUP_ID, value);
        }

        /// <summary>
        /// Creates a BroadSoft-User-Id attribute (Type 8) with the specified user ID.
        /// </summary>
        /// <param name="value">The user identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserId(string value)
        {
            return CreateString(BroadSoftAttributeType.USER_ID, value);
        }

        /// <summary>
        /// Creates a BroadSoft-Device-Name attribute (Type 9) with the specified device name.
        /// </summary>
        /// <param name="value">The device name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DeviceName(string value)
        {
            return CreateString(BroadSoftAttributeType.DEVICE_NAME, value);
        }

        /// <summary>
        /// Creates a BroadSoft-Authorization-Code attribute (Type 10) with the specified code.
        /// </summary>
        /// <param name="value">The authorization code. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthorizationCode(string value)
        {
            return CreateString(BroadSoftAttributeType.AUTHORIZATION_CODE, value);
        }

        /// <summary>
        /// Creates a BroadSoft-Other-Party-Name attribute (Type 11) with the specified name.
        /// </summary>
        /// <param name="value">The other party name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes OtherPartyName(string value)
        {
            return CreateString(BroadSoftAttributeType.OTHER_PARTY_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified BroadSoft attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(BroadSoftAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified BroadSoft attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(BroadSoftAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}