using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Camiant / Tekelec (IANA PEN 21274) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.camiant</c>.
    /// </summary>
    public enum CamiantAttributeType : byte
    {
        /// <summary>Camiant-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Camiant / Tekelec
    /// (IANA PEN 21274) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.camiant</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Camiant's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 21274</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Camiant (now part of Tekelec / Oracle
    /// Communications) Session Resource Controller (SRC) and Session Border
    /// Controller platforms for RADIUS-based attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CamiantAttributes.AvPair("policy=premium"));
    /// </code>
    /// </remarks>
    public static class CamiantAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Camiant (Tekelec / Oracle Communications).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 21274;

        #region String Attributes

        /// <summary>
        /// Creates a Camiant-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(CamiantAttributeType.AVPAIR, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Camiant attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(CamiantAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}