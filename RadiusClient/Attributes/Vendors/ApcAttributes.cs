using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an APC / Schneider Electric (IANA PEN 318) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.apc</c>.
    /// </summary>
    public enum ApcAttributeType : byte
    {
        /// <summary>APC-Service-Type (Type 1). Integer. Service type / privilege level.</summary>
        SERVICE_TYPE = 1,

        /// <summary>APC-Outlets (Type 2). String. Outlet group access list.</summary>
        OUTLETS = 2
    }

    /// <summary>
    /// APC-Service-Type attribute values (Type 1).
    /// </summary>
    public enum APC_SERVICE_TYPE
    {
        /// <summary>Administrator — full device access.</summary>
        ADMIN = 1,

        /// <summary>Device user — limited management access.</summary>
        DEVICE = 2,

        /// <summary>Read-only user — monitoring only.</summary>
        READ_ONLY = 3,

        /// <summary>Outlet user — outlet-level control only.</summary>
        OUTLET = 4,

        /// <summary>Network-only user — network configuration access.</summary>
        NETWORK_ONLY = 5
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing APC / Schneider Electric
    /// (IANA PEN 318) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.apc</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// APC's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 318</c>.
    /// </para>
    /// <para>
    /// These attributes are used by APC (Schneider Electric) network management
    /// cards in UPS, PDU, and environmental monitoring devices for user privilege
    /// level assignment and outlet group access control.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ApcAttributes.ServiceType(APC_SERVICE_TYPE.ADMIN));
    /// packet.SetAttribute(ApcAttributes.Outlets("1,2,3"));
    /// </code>
    /// </remarks>
    public static class ApcAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for APC (Schneider Electric).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 318;

        #region Integer Attributes

        /// <summary>
        /// Creates an APC-Service-Type attribute (Type 1) with the specified service type.
        /// </summary>
        /// <param name="value">The service type / privilege level. See <see cref="APC_SERVICE_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServiceType(APC_SERVICE_TYPE value)
        {
            return CreateInteger(ApcAttributeType.SERVICE_TYPE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an APC-Outlets attribute (Type 2) with the specified outlet group access list.
        /// </summary>
        /// <param name="value">The outlet group access list. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Outlets(string value)
        {
            return CreateString(ApcAttributeType.OUTLETS, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified APC attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ApcAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified APC attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ApcAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}