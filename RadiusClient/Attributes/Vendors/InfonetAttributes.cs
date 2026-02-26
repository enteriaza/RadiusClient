using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Infonet Services Corporation (IANA PEN 4453) vendor-specific
    /// RADIUS attribute type, as defined in the FreeRADIUS <c>dictionary.infonet</c>.
    /// </summary>
    public enum InfonetAttributeType : byte
    {
        /// <summary>Infonet-Proxy (Type 1). String. Proxy server address.</summary>
        PROXY = 1,

        /// <summary>Infonet-Config (Type 2). String. Configuration string.</summary>
        CONFIG = 2,

        /// <summary>Infonet-MCS-Country (Type 3). String. MCS country code.</summary>
        MCS_COUNTRY = 3,

        /// <summary>Infonet-MCS-Region (Type 4). String. MCS region identifier.</summary>
        MCS_REGION = 4,

        /// <summary>Infonet-MCS-Off-Peak (Type 5). String. MCS off-peak specification.</summary>
        MCS_OFF_PEAK = 5,

        /// <summary>Infonet-MCS-Overflow (Type 6). String. MCS overflow specification.</summary>
        MCS_OVERFLOW = 6,

        /// <summary>Infonet-MCS-Port (Type 7). String. MCS port specification.</summary>
        MCS_PORT = 7,

        /// <summary>Infonet-MCS-Port-Count (Type 8). String. MCS port count.</summary>
        MCS_PORT_COUNT = 8,

        /// <summary>Infonet-Account-Number (Type 9). String. Account number.</summary>
        ACCOUNT_NUMBER = 9,

        /// <summary>Infonet-Type (Type 10). String. Service type identifier.</summary>
        TYPE = 10
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Infonet Services
    /// Corporation (IANA PEN 4453) Vendor-Specific Attributes (VSAs), as defined in
    /// the FreeRADIUS <c>dictionary.infonet</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Infonet's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 4453</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Infonet Services Corporation (now part of
    /// BT Global Services) network access platforms for RADIUS-based proxy
    /// configuration, device configuration, MCS (Managed Communications Service)
    /// parameters including country, region, off-peak, overflow, and port
    /// specifications, account number assignment, and service type identification.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(InfonetAttributes.AccountNumber("ACCT-12345"));
    /// packet.SetAttribute(InfonetAttributes.McsCountry("US"));
    /// packet.SetAttribute(InfonetAttributes.McsRegion("EAST"));
    /// packet.SetAttribute(InfonetAttributes.Type("premium"));
    /// </code>
    /// </remarks>
    public static class InfonetAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Infonet Services Corporation.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 4453;

        #region String Attributes

        /// <summary>
        /// Creates an Infonet-Proxy attribute (Type 1) with the specified proxy address.
        /// </summary>
        /// <param name="value">The proxy server address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Proxy(string value)
        {
            return CreateString(InfonetAttributeType.PROXY, value);
        }

        /// <summary>
        /// Creates an Infonet-Config attribute (Type 2) with the specified configuration.
        /// </summary>
        /// <param name="value">The configuration string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Config(string value)
        {
            return CreateString(InfonetAttributeType.CONFIG, value);
        }

        /// <summary>
        /// Creates an Infonet-MCS-Country attribute (Type 3) with the specified country code.
        /// </summary>
        /// <param name="value">The MCS country code. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes McsCountry(string value)
        {
            return CreateString(InfonetAttributeType.MCS_COUNTRY, value);
        }

        /// <summary>
        /// Creates an Infonet-MCS-Region attribute (Type 4) with the specified region.
        /// </summary>
        /// <param name="value">The MCS region identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes McsRegion(string value)
        {
            return CreateString(InfonetAttributeType.MCS_REGION, value);
        }

        /// <summary>
        /// Creates an Infonet-MCS-Off-Peak attribute (Type 5) with the specified specification.
        /// </summary>
        /// <param name="value">The MCS off-peak specification. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes McsOffPeak(string value)
        {
            return CreateString(InfonetAttributeType.MCS_OFF_PEAK, value);
        }

        /// <summary>
        /// Creates an Infonet-MCS-Overflow attribute (Type 6) with the specified specification.
        /// </summary>
        /// <param name="value">The MCS overflow specification. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes McsOverflow(string value)
        {
            return CreateString(InfonetAttributeType.MCS_OVERFLOW, value);
        }

        /// <summary>
        /// Creates an Infonet-MCS-Port attribute (Type 7) with the specified port specification.
        /// </summary>
        /// <param name="value">The MCS port specification. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes McsPort(string value)
        {
            return CreateString(InfonetAttributeType.MCS_PORT, value);
        }

        /// <summary>
        /// Creates an Infonet-MCS-Port-Count attribute (Type 8) with the specified count.
        /// </summary>
        /// <param name="value">The MCS port count. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes McsPortCount(string value)
        {
            return CreateString(InfonetAttributeType.MCS_PORT_COUNT, value);
        }

        /// <summary>
        /// Creates an Infonet-Account-Number attribute (Type 9) with the specified account number.
        /// </summary>
        /// <param name="value">The account number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccountNumber(string value)
        {
            return CreateString(InfonetAttributeType.ACCOUNT_NUMBER, value);
        }

        /// <summary>
        /// Creates an Infonet-Type attribute (Type 10) with the specified service type.
        /// </summary>
        /// <param name="value">The service type identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Type(string value)
        {
            return CreateString(InfonetAttributeType.TYPE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Infonet attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(InfonetAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}