using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a ProSoft Technology (IANA PEN 4735) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.prosoft</c>.
    /// </summary>
    /// <remarks>
    /// ProSoft Technology produces industrial wireless and networking solutions
    /// including industrial radio/wireless gateways, protocol converters, and
    /// remote I/O modules for SCADA, process control, and industrial automation
    /// environments.
    /// </remarks>
    public enum ProsoftAttributeType : byte
    {
        /// <summary>Prosoft-User-Level (Type 1). Integer. User privilege level.</summary>
        USER_LEVEL = 1,

        /// <summary>Prosoft-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>Prosoft-AVPair (Type 3). String. Attribute-value pair string.</summary>
        AVPAIR = 3
    }

    /// <summary>
    /// Prosoft-User-Level attribute values (Type 1).
    /// </summary>
    public enum PROSOFT_USER_LEVEL
    {
        /// <summary>Read-only (monitoring) access.</summary>
        READ_ONLY = 0,

        /// <summary>Standard user access.</summary>
        USER = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing ProSoft Technology
    /// (IANA PEN 4735) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.prosoft</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ProSoft's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 4735</c>.
    /// </para>
    /// <para>
    /// These attributes are used by ProSoft Technology industrial wireless gateways
    /// and networking equipment for RADIUS-based user privilege level assignment,
    /// user group mapping, and general-purpose attribute-value pair configuration
    /// during administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ProsoftAttributes.UserLevel(PROSOFT_USER_LEVEL.ADMIN));
    /// packet.SetAttribute(ProsoftAttributes.UserGroup("scada-operators"));
    /// </code>
    /// </remarks>
    public static class ProsoftAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for ProSoft Technology.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 4735;

        #region Integer Attributes

        /// <summary>
        /// Creates a Prosoft-User-Level attribute (Type 1) with the specified level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="PROSOFT_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserLevel(PROSOFT_USER_LEVEL value)
        {
            return CreateInteger(ProsoftAttributeType.USER_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Prosoft-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(ProsoftAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Prosoft-AVPair attribute (Type 3) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(ProsoftAttributeType.AVPAIR, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified ProSoft attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ProsoftAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified ProSoft attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ProsoftAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}