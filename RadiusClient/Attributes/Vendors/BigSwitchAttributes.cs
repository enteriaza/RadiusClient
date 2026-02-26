using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Big Switch Networks (IANA PEN 37538) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.bigswitch</c>.
    /// </summary>
    public enum BigSwitchAttributeType : byte
    {
        /// <summary>BigSwitch-User-Group (Type 1). String. User group name.</summary>
        USER_GROUP = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Big Switch Networks
    /// (IANA PEN 37538) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.bigswitch</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Big Switch's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 37538</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Big Switch Networks (now part of Arista)
    /// SDN controllers for RADIUS-based user group assignment during
    /// administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(BigSwitchAttributes.UserGroup("network-admin"));
    /// </code>
    /// </remarks>
    public static class BigSwitchAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Big Switch Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 37538;

        #region String Attributes

        /// <summary>
        /// Creates a BigSwitch-User-Group attribute (Type 1) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(BigSwitchAttributeType.USER_GROUP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Big Switch attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(BigSwitchAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}