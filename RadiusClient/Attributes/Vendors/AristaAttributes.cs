using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Arista Networks (IANA PEN 30065) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.arista</c>.
    /// </summary>
    public enum AristaAttributeType : byte
    {
        /// <summary>Arista-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Arista-User-Priv (Type 2). Integer. User privilege level (0–15).</summary>
        USER_PRIV = 2,

        /// <summary>Arista-User-Role (Type 3). String. User role name.</summary>
        USER_ROLE = 3,

        /// <summary>Arista-CVP-Role (Type 4). String. CloudVision Portal role name.</summary>
        CVP_ROLE = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Arista Networks
    /// (IANA PEN 30065) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.arista</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Arista's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 30065</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Arista Networks EOS switches and CloudVision
    /// Portal (CVP) for RADIUS-based CLI privilege level assignment, user role
    /// mapping, and general attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AristaAttributes.UserPriv(15));
    /// packet.SetAttribute(AristaAttributes.UserRole("network-admin"));
    /// packet.SetAttribute(AristaAttributes.AvPair("shell:priv-lvl=15"));
    /// </code>
    /// </remarks>
    public static class AristaAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Arista Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 30065;

        #region Integer Attributes

        /// <summary>
        /// Creates an Arista-User-Priv attribute (Type 2) with the specified privilege level.
        /// </summary>
        /// <param name="value">The user privilege level (0–15).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserPriv(int value)
        {
            return CreateInteger(AristaAttributeType.USER_PRIV, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Arista-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(AristaAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates an Arista-User-Role attribute (Type 3) with the specified role name.
        /// </summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value)
        {
            return CreateString(AristaAttributeType.USER_ROLE, value);
        }

        /// <summary>
        /// Creates an Arista-CVP-Role attribute (Type 4) with the specified CloudVision Portal role name.
        /// </summary>
        /// <param name="value">The CloudVision Portal role name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CvpRole(string value)
        {
            return CreateString(AristaAttributeType.CVP_ROLE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Arista attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AristaAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Arista attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AristaAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}