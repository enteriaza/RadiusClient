using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an AudioCodes (IANA PEN 5003) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.audiocodes</c>.
    /// </summary>
    public enum AudioCodesAttributeType : byte
    {
        /// <summary>ACL-Auth-Level (Type 35). Integer. Authentication / privilege level.</summary>
        AUTH_LEVEL = 35,

        /// <summary>ACL-AVPair (Type 36). String. Attribute-value pair string.</summary>
        AVPAIR = 36
    }

    /// <summary>
    /// ACL-Auth-Level attribute values (Type 35).
    /// </summary>
    public enum ACL_AUTH_LEVEL
    {
        /// <summary>No access.</summary>
        NONE = 0,

        /// <summary>Monitor (read-only) access.</summary>
        MONITOR = 50,

        /// <summary>Administrator access (full management).</summary>
        ADMIN = 100,

        /// <summary>Security administrator access.</summary>
        SECURITY_ADMIN = 200
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing AudioCodes
    /// (IANA PEN 5003) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.audiocodes</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// AudioCodes' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 5003</c>.
    /// </para>
    /// <para>
    /// These attributes are used by AudioCodes Session Border Controllers (SBCs),
    /// Mediant media gateways, and management platforms for RADIUS-based
    /// administrative privilege level assignment and attribute-value pair
    /// configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AudioCodesAttributes.AuthLevel(ACL_AUTH_LEVEL.ADMIN));
    /// packet.SetAttribute(AudioCodesAttributes.AvPair("shell:priv-lvl=15"));
    /// </code>
    /// </remarks>
    public static class AudioCodesAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for AudioCodes.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 5003;

        #region Integer Attributes

        /// <summary>
        /// Creates an ACL-Auth-Level attribute (Type 35) with the specified privilege level.
        /// </summary>
        /// <param name="value">The authentication / privilege level. See <see cref="ACL_AUTH_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthLevel(ACL_AUTH_LEVEL value)
        {
            return CreateInteger(AudioCodesAttributeType.AUTH_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an ACL-AVPair attribute (Type 36) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(AudioCodesAttributeType.AVPAIR, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified AudioCodes attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AudioCodesAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified AudioCodes attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AudioCodesAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}