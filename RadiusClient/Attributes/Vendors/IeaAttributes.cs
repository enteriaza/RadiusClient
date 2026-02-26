using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an IEA Software (IANA PEN 24023) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.iea</c>.
    /// </summary>
    public enum IeaAttributeType : byte
    {
        /// <summary>IEA-Radius-Token (Type 1). String. Authentication token.</summary>
        RADIUS_TOKEN = 1,

        /// <summary>IEA-Radius-Command (Type 2). String. Command string.</summary>
        RADIUS_COMMAND = 2,

        /// <summary>IEA-Radius-Response (Type 3). String. Response string.</summary>
        RADIUS_RESPONSE = 3,

        /// <summary>IEA-Radius-Session (Type 4). String. Session identifier.</summary>
        RADIUS_SESSION = 4,

        /// <summary>IEA-Radius-Action (Type 5). Integer. Action code.</summary>
        RADIUS_ACTION = 5,

        /// <summary>IEA-Radius-User-Level (Type 6). Integer. User privilege level.</summary>
        RADIUS_USER_LEVEL = 6,

        /// <summary>IEA-Radius-User-Group (Type 7). String. User group name.</summary>
        RADIUS_USER_GROUP = 7
    }

    /// <summary>
    /// IEA-Radius-Action attribute values (Type 5).
    /// </summary>
    public enum IEA_RADIUS_ACTION
    {
        /// <summary>No action.</summary>
        NONE = 0,

        /// <summary>Login action.</summary>
        LOGIN = 1,

        /// <summary>Logout action.</summary>
        LOGOUT = 2,

        /// <summary>Accounting action.</summary>
        ACCOUNTING = 3
    }

    /// <summary>
    /// IEA-Radius-User-Level attribute values (Type 6).
    /// </summary>
    public enum IEA_RADIUS_USER_LEVEL
    {
        /// <summary>Normal user level.</summary>
        USER = 0,

        /// <summary>Operator level.</summary>
        OPERATOR = 1,

        /// <summary>Administrator level.</summary>
        ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing IEA Software
    /// (IANA PEN 24023) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.iea</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// IEA Software's vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped
    /// in a <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 24023</c>.
    /// </para>
    /// <para>
    /// These attributes are used by IEA Software platforms for RADIUS-based
    /// authentication token management, command/response exchange, session
    /// identification, action signalling (login/logout/accounting), user
    /// privilege level assignment, and user group mapping.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(IeaAttributes.RadiusUserLevel(IEA_RADIUS_USER_LEVEL.ADMIN));
    /// packet.SetAttribute(IeaAttributes.RadiusUserGroup("administrators"));
    /// packet.SetAttribute(IeaAttributes.RadiusAction(IEA_RADIUS_ACTION.LOGIN));
    /// packet.SetAttribute(IeaAttributes.RadiusToken("abc123token"));
    /// </code>
    /// </remarks>
    public static class IeaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for IEA Software.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 24023;

        #region Integer Attributes

        /// <summary>
        /// Creates an IEA-Radius-Action attribute (Type 5) with the specified action.
        /// </summary>
        /// <param name="value">The action code. See <see cref="IEA_RADIUS_ACTION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RadiusAction(IEA_RADIUS_ACTION value)
        {
            return CreateInteger(IeaAttributeType.RADIUS_ACTION, (int)value);
        }

        /// <summary>
        /// Creates an IEA-Radius-User-Level attribute (Type 6) with the specified level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="IEA_RADIUS_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RadiusUserLevel(IEA_RADIUS_USER_LEVEL value)
        {
            return CreateInteger(IeaAttributeType.RADIUS_USER_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an IEA-Radius-Token attribute (Type 1) with the specified token.
        /// </summary>
        /// <param name="value">The authentication token. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RadiusToken(string value)
        {
            return CreateString(IeaAttributeType.RADIUS_TOKEN, value);
        }

        /// <summary>
        /// Creates an IEA-Radius-Command attribute (Type 2) with the specified command.
        /// </summary>
        /// <param name="value">The command string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RadiusCommand(string value)
        {
            return CreateString(IeaAttributeType.RADIUS_COMMAND, value);
        }

        /// <summary>
        /// Creates an IEA-Radius-Response attribute (Type 3) with the specified response.
        /// </summary>
        /// <param name="value">The response string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RadiusResponse(string value)
        {
            return CreateString(IeaAttributeType.RADIUS_RESPONSE, value);
        }

        /// <summary>
        /// Creates an IEA-Radius-Session attribute (Type 4) with the specified session identifier.
        /// </summary>
        /// <param name="value">The session identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RadiusSession(string value)
        {
            return CreateString(IeaAttributeType.RADIUS_SESSION, value);
        }

        /// <summary>
        /// Creates an IEA-Radius-User-Group attribute (Type 7) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RadiusUserGroup(string value)
        {
            return CreateString(IeaAttributeType.RADIUS_USER_GROUP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified IEA attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(IeaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified IEA attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(IeaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}