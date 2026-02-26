using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Dante / Audinate (IANA PEN 14765) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.dante</c>.
    /// </summary>
    public enum DanteAttributeType : byte
    {
        /// <summary>Dante-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Dante-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Dante / Audinate
    /// (IANA PEN 14765) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.dante</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Dante's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 14765</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Dante (Audinate) audio-over-IP networking
    /// platforms for RADIUS-based user group assignment and general-purpose
    /// attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(DanteAttributes.UserGroup("audio-engineers"));
    /// packet.SetAttribute(DanteAttributes.AvPair("role=operator"));
    /// </code>
    /// </remarks>
    public static class DanteAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Dante (Audinate).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 14765;

        #region String Attributes

        /// <summary>
        /// Creates a Dante-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(DanteAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Dante-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(DanteAttributeType.USER_GROUP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Dante attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(DanteAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}