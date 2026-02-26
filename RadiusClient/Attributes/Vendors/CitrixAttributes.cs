using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Citrix Systems (IANA PEN 3845) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.citrix</c>.
    /// </summary>
    public enum CitrixAttributeType : byte
    {
        /// <summary>Citrix-Context (Type 1). String. Session context string.</summary>
        CONTEXT = 1,

        /// <summary>Citrix-Group-Names (Type 2). String. Group names for authorization.</summary>
        GROUP_NAMES = 2,

        /// <summary>Citrix-User-Groups (Type 3). String. User group membership list.</summary>
        USER_GROUPS = 3,

        /// <summary>Citrix-User-Name2 (Type 4). String. Secondary user name.</summary>
        USER_NAME2 = 4,

        /// <summary>Citrix-User-Domain (Type 5). String. User domain name.</summary>
        USER_DOMAIN = 5,

        /// <summary>Citrix-Multi-Session (Type 6). Integer. Multi-session allowed flag.</summary>
        MULTI_SESSION = 6,

        /// <summary>Citrix-Client-IP-Address (Type 7). String. Client IP address string.</summary>
        CLIENT_IP_ADDRESS = 7,

        /// <summary>Citrix-Client-Type (Type 8). Integer. Client type code.</summary>
        CLIENT_TYPE = 8,

        /// <summary>Citrix-Proxy-Level (Type 9). Integer. Proxy level.</summary>
        PROXY_LEVEL = 9,

        /// <summary>Citrix-Application-Name (Type 10). String. Application name.</summary>
        APPLICATION_NAME = 10,

        /// <summary>Citrix-Published-Name (Type 11). String. Published application name.</summary>
        PUBLISHED_NAME = 11,

        /// <summary>Citrix-Server-Name (Type 12). String. Server name.</summary>
        SERVER_NAME = 12,

        /// <summary>Citrix-Client-Name (Type 13). String. Client device name.</summary>
        CLIENT_NAME = 13,

        /// <summary>Citrix-Connection-Id (Type 14). Integer. Connection identifier.</summary>
        CONNECTION_ID = 14,

        /// <summary>Citrix-Session-Id (Type 15). Integer. Session identifier.</summary>
        SESSION_ID = 15,

        /// <summary>Citrix-Policy-Name (Type 16). String. Policy name to apply.</summary>
        POLICY_NAME = 16,

        /// <summary>Citrix-Max-Connections (Type 17). Integer. Maximum connections allowed.</summary>
        MAX_CONNECTIONS = 17,

        /// <summary>Citrix-Max-Users (Type 18). Integer. Maximum users allowed.</summary>
        MAX_USERS = 18
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Citrix Systems
    /// (IANA PEN 3845) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.citrix</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Citrix's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3845</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Citrix NetScaler (ADC), XenApp, XenDesktop,
    /// and Gateway platforms for RADIUS-based session management, group-based
    /// authorization, published application access, policy enforcement, client
    /// identification, connection/session tracking, and multi-session control.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CitrixAttributes.GroupNames("VPN-Users;Admins"));
    /// packet.SetAttribute(CitrixAttributes.PolicyName("default-policy"));
    /// packet.SetAttribute(CitrixAttributes.MaxConnections(5));
    /// packet.SetAttribute(CitrixAttributes.ApplicationName("Notepad"));
    /// </code>
    /// </remarks>
    public static class CitrixAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Citrix Systems.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3845;

        #region Integer Attributes

        /// <summary>
        /// Creates a Citrix-Multi-Session attribute (Type 6) with the specified flag.
        /// </summary>
        /// <param name="value">Whether multi-session is allowed (0 = no, 1 = yes).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MultiSession(int value)
        {
            return CreateInteger(CitrixAttributeType.MULTI_SESSION, value);
        }

        /// <summary>
        /// Creates a Citrix-Client-Type attribute (Type 8) with the specified type code.
        /// </summary>
        /// <param name="value">The client type code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ClientType(int value)
        {
            return CreateInteger(CitrixAttributeType.CLIENT_TYPE, value);
        }

        /// <summary>
        /// Creates a Citrix-Proxy-Level attribute (Type 9) with the specified level.
        /// </summary>
        /// <param name="value">The proxy level.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ProxyLevel(int value)
        {
            return CreateInteger(CitrixAttributeType.PROXY_LEVEL, value);
        }

        /// <summary>
        /// Creates a Citrix-Connection-Id attribute (Type 14) with the specified identifier.
        /// </summary>
        /// <param name="value">The connection identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ConnectionId(int value)
        {
            return CreateInteger(CitrixAttributeType.CONNECTION_ID, value);
        }

        /// <summary>
        /// Creates a Citrix-Session-Id attribute (Type 15) with the specified identifier.
        /// </summary>
        /// <param name="value">The session identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionId(int value)
        {
            return CreateInteger(CitrixAttributeType.SESSION_ID, value);
        }

        /// <summary>
        /// Creates a Citrix-Max-Connections attribute (Type 17) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum connections allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxConnections(int value)
        {
            return CreateInteger(CitrixAttributeType.MAX_CONNECTIONS, value);
        }

        /// <summary>
        /// Creates a Citrix-Max-Users attribute (Type 18) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum users allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxUsers(int value)
        {
            return CreateInteger(CitrixAttributeType.MAX_USERS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Citrix-Context attribute (Type 1) with the specified session context.
        /// </summary>
        /// <param name="value">The session context string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Context(string value)
        {
            return CreateString(CitrixAttributeType.CONTEXT, value);
        }

        /// <summary>
        /// Creates a Citrix-Group-Names attribute (Type 2) with the specified group names.
        /// </summary>
        /// <param name="value">The group names for authorization. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes GroupNames(string value)
        {
            return CreateString(CitrixAttributeType.GROUP_NAMES, value);
        }

        /// <summary>
        /// Creates a Citrix-User-Groups attribute (Type 3) with the specified membership list.
        /// </summary>
        /// <param name="value">The user group membership list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroups(string value)
        {
            return CreateString(CitrixAttributeType.USER_GROUPS, value);
        }

        /// <summary>
        /// Creates a Citrix-User-Name2 attribute (Type 4) with the specified secondary user name.
        /// </summary>
        /// <param name="value">The secondary user name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserName2(string value)
        {
            return CreateString(CitrixAttributeType.USER_NAME2, value);
        }

        /// <summary>
        /// Creates a Citrix-User-Domain attribute (Type 5) with the specified domain.
        /// </summary>
        /// <param name="value">The user domain name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserDomain(string value)
        {
            return CreateString(CitrixAttributeType.USER_DOMAIN, value);
        }

        /// <summary>
        /// Creates a Citrix-Client-IP-Address attribute (Type 7) with the specified address.
        /// </summary>
        /// <param name="value">The client IP address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientIpAddress(string value)
        {
            return CreateString(CitrixAttributeType.CLIENT_IP_ADDRESS, value);
        }

        /// <summary>
        /// Creates a Citrix-Application-Name attribute (Type 10) with the specified name.
        /// </summary>
        /// <param name="value">The application name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApplicationName(string value)
        {
            return CreateString(CitrixAttributeType.APPLICATION_NAME, value);
        }

        /// <summary>
        /// Creates a Citrix-Published-Name attribute (Type 11) with the specified name.
        /// </summary>
        /// <param name="value">The published application name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PublishedName(string value)
        {
            return CreateString(CitrixAttributeType.PUBLISHED_NAME, value);
        }

        /// <summary>
        /// Creates a Citrix-Server-Name attribute (Type 12) with the specified name.
        /// </summary>
        /// <param name="value">The server name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServerName(string value)
        {
            return CreateString(CitrixAttributeType.SERVER_NAME, value);
        }

        /// <summary>
        /// Creates a Citrix-Client-Name attribute (Type 13) with the specified device name.
        /// </summary>
        /// <param name="value">The client device name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClientName(string value)
        {
            return CreateString(CitrixAttributeType.CLIENT_NAME, value);
        }

        /// <summary>
        /// Creates a Citrix-Policy-Name attribute (Type 16) with the specified policy name.
        /// </summary>
        /// <param name="value">The policy name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(CitrixAttributeType.POLICY_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Citrix attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(CitrixAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Citrix attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(CitrixAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}