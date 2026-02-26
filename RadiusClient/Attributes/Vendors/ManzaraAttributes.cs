using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Manzara (IANA PEN 16394) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.manzara</c>.
    /// </summary>
    public enum ManzaraAttributeType : byte
    {
        /// <summary>Manzara-Service-Class (Type 1). String. Service class name.</summary>
        SERVICE_CLASS = 1,

        /// <summary>Manzara-Calling-Station-Id (Type 2). String. Calling station identifier.</summary>
        CALLING_STATION_ID = 2,

        /// <summary>Manzara-Called-Station-Id (Type 3). String. Called station identifier.</summary>
        CALLED_STATION_ID = 3,

        /// <summary>Manzara-NAS-Id (Type 4). String. NAS identifier.</summary>
        NAS_ID = 4,

        /// <summary>Manzara-Billing-Id (Type 5). String. Billing identifier.</summary>
        BILLING_ID = 5,

        /// <summary>Manzara-Balance (Type 6). String. Account balance.</summary>
        BALANCE = 6,

        /// <summary>Manzara-Account-Code (Type 7). String. Account code.</summary>
        ACCOUNT_CODE = 7,

        /// <summary>Manzara-Rate-Plan (Type 8). String. Rate plan name.</summary>
        RATE_PLAN = 8,

        /// <summary>Manzara-Currency (Type 9). String. Currency code.</summary>
        CURRENCY = 9,

        /// <summary>Manzara-ANI (Type 10). String. Automatic Number Identification.</summary>
        ANI = 10,

        /// <summary>Manzara-DNIS (Type 11). String. Dialed Number Identification Service.</summary>
        DNIS = 11
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Manzara
    /// (IANA PEN 16394) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.manzara</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Manzara's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 16394</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Manzara platforms for RADIUS-based VoIP and
    /// telecommunications billing, including service class assignment,
    /// calling/called station identification, NAS identification, billing ID
    /// and account code assignment, account balance reporting, rate plan
    /// selection, currency specification, and ANI/DNIS tracking.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(ManzaraAttributes.ServiceClass("premium"));
    /// packet.SetAttribute(ManzaraAttributes.AccountCode("ACCT-5678"));
    /// packet.SetAttribute(ManzaraAttributes.RatePlan("international"));
    /// packet.SetAttribute(ManzaraAttributes.Currency("USD"));
    /// packet.SetAttribute(ManzaraAttributes.Ani("+15551234567"));
    /// packet.SetAttribute(ManzaraAttributes.Dnis("+442071234567"));
    /// </code>
    /// </remarks>
    public static class ManzaraAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Manzara.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 16394;

        #region String Attributes

        /// <summary>
        /// Creates a Manzara-Service-Class attribute (Type 1) with the specified service class.
        /// </summary>
        /// <param name="value">The service class name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceClass(string value)
        {
            return CreateString(ManzaraAttributeType.SERVICE_CLASS, value);
        }

        /// <summary>
        /// Creates a Manzara-Calling-Station-Id attribute (Type 2) with the specified identifier.
        /// </summary>
        /// <param name="value">The calling station identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingStationId(string value)
        {
            return CreateString(ManzaraAttributeType.CALLING_STATION_ID, value);
        }

        /// <summary>
        /// Creates a Manzara-Called-Station-Id attribute (Type 3) with the specified identifier.
        /// </summary>
        /// <param name="value">The called station identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledStationId(string value)
        {
            return CreateString(ManzaraAttributeType.CALLED_STATION_ID, value);
        }

        /// <summary>
        /// Creates a Manzara-NAS-Id attribute (Type 4) with the specified NAS identifier.
        /// </summary>
        /// <param name="value">The NAS identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NasId(string value)
        {
            return CreateString(ManzaraAttributeType.NAS_ID, value);
        }

        /// <summary>
        /// Creates a Manzara-Billing-Id attribute (Type 5) with the specified billing identifier.
        /// </summary>
        /// <param name="value">The billing identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BillingId(string value)
        {
            return CreateString(ManzaraAttributeType.BILLING_ID, value);
        }

        /// <summary>
        /// Creates a Manzara-Balance attribute (Type 6) with the specified balance.
        /// </summary>
        /// <param name="value">The account balance. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Balance(string value)
        {
            return CreateString(ManzaraAttributeType.BALANCE, value);
        }

        /// <summary>
        /// Creates a Manzara-Account-Code attribute (Type 7) with the specified account code.
        /// </summary>
        /// <param name="value">The account code. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AccountCode(string value)
        {
            return CreateString(ManzaraAttributeType.ACCOUNT_CODE, value);
        }

        /// <summary>
        /// Creates a Manzara-Rate-Plan attribute (Type 8) with the specified rate plan.
        /// </summary>
        /// <param name="value">The rate plan name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RatePlan(string value)
        {
            return CreateString(ManzaraAttributeType.RATE_PLAN, value);
        }

        /// <summary>
        /// Creates a Manzara-Currency attribute (Type 9) with the specified currency code.
        /// </summary>
        /// <param name="value">The currency code (e.g. "USD", "EUR"). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Currency(string value)
        {
            return CreateString(ManzaraAttributeType.CURRENCY, value);
        }

        /// <summary>
        /// Creates a Manzara-ANI attribute (Type 10) with the specified ANI.
        /// </summary>
        /// <param name="value">The Automatic Number Identification. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ani(string value)
        {
            return CreateString(ManzaraAttributeType.ANI, value);
        }

        /// <summary>
        /// Creates a Manzara-DNIS attribute (Type 11) with the specified DNIS.
        /// </summary>
        /// <param name="value">The Dialed Number Identification Service. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Dnis(string value)
        {
            return CreateString(ManzaraAttributeType.DNIS, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Manzara attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ManzaraAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}