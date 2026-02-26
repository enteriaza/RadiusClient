using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Microsemi (IANA PEN 3563) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.microsemi</c>.
    /// </summary>
    /// <remarks>
    /// Microsemi Corporation (acquired by Microchip Technology in 2018) produced
    /// networking equipment including Ethernet switches, timing/synchronization
    /// solutions, and PoE products (formerly Vitesse Semiconductor).
    /// </remarks>
    public enum MicrosemiAttributeType : byte
    {
        /// <summary>Microsemi-Auth-Priv-Level (Type 1). Integer. Authentication privilege level.</summary>
        AUTH_PRIV_LEVEL = 1,

        /// <summary>Microsemi-Auth-Group-ID (Type 2). Integer. Authentication group identifier.</summary>
        AUTH_GROUP_ID = 2,

        /// <summary>Microsemi-Auth-Group-Name (Type 3). String. Authentication group name.</summary>
        AUTH_GROUP_NAME = 3,

        /// <summary>Microsemi-AVPair (Type 4). String. Attribute-value pair string.</summary>
        AVPAIR = 4
    }

    /// <summary>
    /// Microsemi-Auth-Priv-Level attribute values (Type 1).
    /// </summary>
    public enum MICROSEMI_AUTH_PRIV_LEVEL
    {
        /// <summary>No access.</summary>
        NO_ACCESS = 0,

        /// <summary>Read-only access (level 1).</summary>
        READ_ONLY = 1,

        /// <summary>Read-write access (level 5).</summary>
        READ_WRITE = 5,

        /// <summary>Administrator access (level 10).</summary>
        ADMIN = 10,

        /// <summary>Full privilege (level 15).</summary>
        FULL = 15
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Microsemi
    /// (IANA PEN 3563) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.microsemi</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Microsemi's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 3563</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Microsemi (now Microchip) managed Ethernet
    /// switches and networking equipment for RADIUS-based authentication privilege
    /// level assignment, group identification (by ID and name), and general-purpose
    /// attribute-value pair configuration during administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(MicrosemiAttributes.AuthPrivLevel(MICROSEMI_AUTH_PRIV_LEVEL.ADMIN));
    /// packet.SetAttribute(MicrosemiAttributes.AuthGroupName("network-admins"));
    /// packet.SetAttribute(MicrosemiAttributes.AuthGroupId(10));
    /// </code>
    /// </remarks>
    public static class MicrosemiAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Microsemi.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 3563;

        #region Integer Attributes

        /// <summary>
        /// Creates a Microsemi-Auth-Priv-Level attribute (Type 1) with the specified level.
        /// </summary>
        /// <param name="value">The authentication privilege level. See <see cref="MICROSEMI_AUTH_PRIV_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthPrivLevel(MICROSEMI_AUTH_PRIV_LEVEL value)
        {
            return CreateInteger(MicrosemiAttributeType.AUTH_PRIV_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates a Microsemi-Auth-Group-ID attribute (Type 2) with the specified group identifier.
        /// </summary>
        /// <param name="value">The authentication group identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthGroupId(int value)
        {
            return CreateInteger(MicrosemiAttributeType.AUTH_GROUP_ID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Microsemi-Auth-Group-Name attribute (Type 3) with the specified group name.
        /// </summary>
        /// <param name="value">The authentication group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthGroupName(string value)
        {
            return CreateString(MicrosemiAttributeType.AUTH_GROUP_NAME, value);
        }

        /// <summary>
        /// Creates a Microsemi-AVPair attribute (Type 4) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(MicrosemiAttributeType.AVPAIR, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Microsemi attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(MicrosemiAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Microsemi attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(MicrosemiAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}