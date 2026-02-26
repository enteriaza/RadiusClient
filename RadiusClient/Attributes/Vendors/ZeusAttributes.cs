using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Zeus Technology (IANA PEN 7054) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.zeus</c>.
    /// </summary>
    /// <remarks>
    /// Zeus Technology produced the Zeus Web Server (later Zeus Traffic Manager / ZXTM),
    /// a high-performance reverse proxy, load balancer, and application delivery
    /// controller. Zeus was acquired by Riverbed Technology in 2011, and the product
    /// became Stingray Traffic Manager (later Brocade vTM, now Pulse Secure vTM).
    /// These RADIUS attributes are used for administrative authentication to the
    /// Zeus/Stingray management interface.
    /// </remarks>
    public enum ZeusAttributeType : byte
    {
        /// <summary>Zeus-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Zeus-Admin-Privilege (Type 2). Integer. Administrative privilege level.</summary>
        ADMIN_PRIVILEGE = 2,

        /// <summary>Zeus-User-Group (Type 3). String. User group name.</summary>
        USER_GROUP = 3,

        /// <summary>Zeus-Access-List (Type 4). String. Access list name.</summary>
        ACCESS_LIST = 4
    }

    /// <summary>
    /// Zeus-Admin-Privilege attribute values (Type 2).
    /// </summary>
    public enum ZEUS_ADMIN_PRIVILEGE
    {
        /// <summary>No administrative access.</summary>
        NONE = 0,

        /// <summary>Read-only (monitoring) access.</summary>
        READ_ONLY = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Zeus Technology
    /// (IANA PEN 7054) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.zeus</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Zeus' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 7054</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Zeus Technology (Riverbed Stingray / Brocade vTM /
    /// Pulse Secure vTM) traffic manager platforms for RADIUS-based administrative
    /// privilege level assignment, user group mapping, access list configuration,
    /// and general-purpose attribute-value pair configuration during management
    /// interface authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ZeusAttributes.AdminPrivilege(ZEUS_ADMIN_PRIVILEGE.ADMIN));
    /// packet.SetAttribute(ZeusAttributes.UserGroup("traffic-managers"));
    /// packet.SetAttribute(ZeusAttributes.AccessList("management-acl"));
    /// </code>
    /// </remarks>
    public static class ZeusAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Zeus Technology.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 7054;

        #region Integer Attributes

        /// <summary>
        /// Creates a Zeus-Admin-Privilege attribute (Type 2) with the specified level.
        /// </summary>
        /// <param name="value">The administrative privilege level. See <see cref="ZEUS_ADMIN_PRIVILEGE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminPrivilege(ZEUS_ADMIN_PRIVILEGE value)
        {
            return CreateInteger(ZeusAttributeType.ADMIN_PRIVILEGE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Zeus-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(ZeusAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Zeus-User-Group attribute (Type 3) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(ZeusAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Zeus-Access-List attribute (Type 4) with the specified access list name.
        /// </summary>
        /// <param name="value">The access list name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccessList(string value)
        {
            return CreateString(ZeusAttributeType.ACCESS_LIST, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(ZeusAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(ZeusAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}