using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Arbor Networks / NETSCOUT (IANA PEN 9694) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.arbor</c>.
    /// </summary>
    public enum ArborAttributeType : byte
    {
        /// <summary>Arbor-Privilege-Level (Type 1). String. User privilege level name.</summary>
        PRIVILEGE_LEVEL = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Arbor Networks / NETSCOUT
    /// (IANA PEN 9694) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.arbor</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Arbor's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 9694</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Arbor Networks (now NETSCOUT) Sightline / Peakflow
    /// and TMS platforms for RADIUS-based user privilege level assignment during
    /// administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ArborAttributes.PrivilegeLevel("admin"));
    /// </code>
    /// </remarks>
    public static class ArborAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Arbor Networks (NETSCOUT).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 9694;

        #region String Attributes

        /// <summary>
        /// Creates an Arbor-Privilege-Level attribute (Type 1) with the specified privilege level.
        /// </summary>
        /// <param name="value">The user privilege level name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrivilegeLevel(string value)
        {
            return CreateString(ArborAttributeType.PRIVILEGE_LEVEL, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Arbor attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ArborAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}