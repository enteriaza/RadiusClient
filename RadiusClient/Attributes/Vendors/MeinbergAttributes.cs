using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Meinberg Funkuhren (IANA PEN 5765) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.meinberg</c>.
    /// </summary>
    /// <remarks>
    /// Meinberg Funkuhren is a German manufacturer of precision time and frequency
    /// synchronization solutions, including NTP time servers (LANTIME series),
    /// PTP grandmaster clocks, and GPS/GNSS receivers.
    /// </remarks>
    public enum MeinbergAttributeType : byte
    {
        /// <summary>Meinberg-User-Level (Type 1). Integer. User privilege level.</summary>
        USER_LEVEL = 1,

        /// <summary>Meinberg-User-Role (Type 2). String. User role name.</summary>
        USER_ROLE = 2,

        /// <summary>Meinberg-AVPair (Type 3). String. Attribute-value pair string.</summary>
        AVPAIR = 3
    }

    /// <summary>
    /// Meinberg-User-Level attribute values (Type 1).
    /// </summary>
    public enum MEINBERG_USER_LEVEL
    {
        /// <summary>Read-only user access (status/monitoring only).</summary>
        READ_ONLY = 0,

        /// <summary>Standard user access.</summary>
        USER = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2,

        /// <summary>Super administrator access.</summary>
        SUPER_ADMIN = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Meinberg Funkuhren
    /// (IANA PEN 5765) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.meinberg</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Meinberg's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 5765</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Meinberg LANTIME NTP time servers and related
    /// precision timing equipment for RADIUS-based user privilege level assignment,
    /// user role mapping, and general-purpose attribute-value pair configuration
    /// during administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(MeinbergAttributes.UserLevel(MEINBERG_USER_LEVEL.ADMIN));
    /// packet.SetAttribute(MeinbergAttributes.UserRole("ntp-admin"));
    /// </code>
    /// </remarks>
    public static class MeinbergAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Meinberg Funkuhren.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 5765;

        #region Integer Attributes

        /// <summary>
        /// Creates a Meinberg-User-Level attribute (Type 1) with the specified level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="MEINBERG_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserLevel(MEINBERG_USER_LEVEL value)
        {
            return CreateInteger(MeinbergAttributeType.USER_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Meinberg-User-Role attribute (Type 2) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(MeinbergAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates a Meinberg-AVPair attribute (Type 3) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(MeinbergAttributeType.AVPAIR, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Meinberg attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(MeinbergAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Meinberg attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(MeinbergAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}