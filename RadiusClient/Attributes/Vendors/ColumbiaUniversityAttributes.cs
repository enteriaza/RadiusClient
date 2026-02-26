using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Columbia University (IANA PEN 11862) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.columbia_university</c>.
    /// </summary>
    public enum ColumbiaUniversityAttributeType : byte
    {
        /// <summary>Columbia-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Columbia-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Columbia University
    /// (IANA PEN 11862) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.columbia_university</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Columbia University's vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 11862</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Columbia University's network infrastructure
    /// for RADIUS-based user group assignment and general-purpose attribute-value
    /// pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ColumbiaUniversityAttributes.UserGroup("faculty"));
    /// packet.SetAttribute(ColumbiaUniversityAttributes.AvPair("role=admin"));
    /// </code>
    /// </remarks>
    public static class ColumbiaUniversityAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Columbia University.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 11862;

        #region String Attributes

        /// <summary>
        /// Creates a Columbia-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(ColumbiaUniversityAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Columbia-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(ColumbiaUniversityAttributeType.USER_GROUP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Columbia University attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ColumbiaUniversityAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}