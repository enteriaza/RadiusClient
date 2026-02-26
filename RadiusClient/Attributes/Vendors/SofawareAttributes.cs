using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a SofaWare Technologies (IANA PEN 6983) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.sofaware</c>.
    /// </summary>
    /// <remarks>
    /// SofaWare Technologies (acquired by Check Point Software Technologies in 2007)
    /// produced embedded security appliances and firmware for small-to-medium
    /// business UTM gateways (Safe@Office, Safe@Home), providing firewall, VPN,
    /// and intrusion prevention capabilities.
    /// </remarks>
    public enum SofawareAttributeType : byte
    {
        /// <summary>SofaWare-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>SofaWare-Firmware-Version (Type 2). String. Firmware version string.</summary>
        FIRMWARE_VERSION = 2,

        /// <summary>SofaWare-Service-Type (Type 3). Integer. SofaWare service type.</summary>
        SERVICE_TYPE = 3,

        /// <summary>SofaWare-Admin-Level (Type 4). Integer. Administrative access level.</summary>
        ADMIN_LEVEL = 4,

        /// <summary>SofaWare-Expiration (Type 5). String. Service expiration date.</summary>
        EXPIRATION = 5,

        /// <summary>SofaWare-User-Group (Type 6). String. User group name.</summary>
        USER_GROUP = 6
    }

    /// <summary>
    /// SofaWare-Service-Type attribute values (Type 3).
    /// </summary>
    public enum SOFAWARE_SERVICE_TYPE
    {
        /// <summary>Standard internet access.</summary>
        INTERNET = 0,

        /// <summary>VPN remote access.</summary>
        VPN = 1,

        /// <summary>Management access.</summary>
        MANAGEMENT = 2
    }

    /// <summary>
    /// SofaWare-Admin-Level attribute values (Type 4).
    /// </summary>
    public enum SOFAWARE_ADMIN_LEVEL
    {
        /// <summary>No administrative access.</summary>
        NONE = 0,

        /// <summary>Read-only (monitoring) access.</summary>
        READ_ONLY = 1,

        /// <summary>Full administrative access.</summary>
        ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing SofaWare Technologies
    /// (IANA PEN 6983) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.sofaware</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// SofaWare's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 6983</c>.
    /// </para>
    /// <para>
    /// These attributes are used by SofaWare Technologies (Check Point) embedded
    /// security appliances (Safe@Office, Safe@Home) for RADIUS-based service type
    /// identification, administrative access level assignment, firmware version
    /// reporting, service expiration configuration, user group mapping, and
    /// general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(SofawareAttributes.AdminLevel(SOFAWARE_ADMIN_LEVEL.ADMIN));
    /// packet.SetAttribute(SofawareAttributes.ServiceType(SOFAWARE_SERVICE_TYPE.VPN));
    /// packet.SetAttribute(SofawareAttributes.UserGroup("vpn-users"));
    /// packet.SetAttribute(SofawareAttributes.Expiration("2026-12-31"));
    /// </code>
    /// </remarks>
    public static class SofawareAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for SofaWare Technologies.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 6983;

        #region Integer Attributes

        /// <summary>
        /// Creates a SofaWare-Service-Type attribute (Type 3) with the specified type.
        /// </summary>
        /// <param name="value">The SofaWare service type. See <see cref="SOFAWARE_SERVICE_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ServiceType(SOFAWARE_SERVICE_TYPE value)
        {
            return CreateInteger(SofawareAttributeType.SERVICE_TYPE, (int)value);
        }

        /// <summary>
        /// Creates a SofaWare-Admin-Level attribute (Type 4) with the specified level.
        /// </summary>
        /// <param name="value">The administrative access level. See <see cref="SOFAWARE_ADMIN_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminLevel(SOFAWARE_ADMIN_LEVEL value)
        {
            return CreateInteger(SofawareAttributeType.ADMIN_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a SofaWare-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(SofawareAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a SofaWare-Firmware-Version attribute (Type 2) with the specified version.
        /// </summary>
        /// <param name="value">The firmware version string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FirmwareVersion(string value)
        {
            return CreateString(SofawareAttributeType.FIRMWARE_VERSION, value);
        }

        /// <summary>
        /// Creates a SofaWare-Expiration attribute (Type 5) with the specified expiration date.
        /// </summary>
        /// <param name="value">The service expiration date. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Expiration(string value)
        {
            return CreateString(SofawareAttributeType.EXPIRATION, value);
        }

        /// <summary>
        /// Creates a SofaWare-User-Group attribute (Type 6) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(SofawareAttributeType.USER_GROUP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified SofaWare attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(SofawareAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified SofaWare attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(SofawareAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}