using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Localweb (IANA PEN 4775) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.localweb</c>.
    /// </summary>
    public enum LocalwebAttributeType : byte
    {
        /// <summary>Localweb-Acct-Resource-Info (Type 1). String. Accounting resource information.</summary>
        ACCT_RESOURCE_INFO = 1,

        /// <summary>Localweb-Acct-Balance-Info (Type 2). String. Accounting balance information.</summary>
        ACCT_BALANCE_INFO = 2,

        /// <summary>Localweb-Acct-Quota-Info (Type 3). String. Accounting quota information.</summary>
        ACCT_QUOTA_INFO = 3,

        /// <summary>Localweb-Acct-Resource-Type (Type 4). Integer. Accounting resource type.</summary>
        ACCT_RESOURCE_TYPE = 4,

        /// <summary>Localweb-Acct-User-Level (Type 5). Integer. Accounting user level.</summary>
        ACCT_USER_LEVEL = 5,

        /// <summary>Localweb-Acct-Time (Type 6). Integer. Accounting time value.</summary>
        ACCT_TIME = 6,

        /// <summary>Localweb-Acct-Duration (Type 7). Integer. Accounting duration in seconds.</summary>
        ACCT_DURATION = 7
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Localweb
    /// (IANA PEN 4775) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.localweb</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Localweb's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 4775</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Localweb platforms for RADIUS-based accounting
    /// including resource information and type, balance and quota tracking, user
    /// level assignment, time stamping, and session duration reporting.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(LocalwebAttributes.AcctResourceInfo("internet-plan-50m"));
    /// packet.SetAttribute(LocalwebAttributes.AcctUserLevel(3));
    /// packet.SetAttribute(LocalwebAttributes.AcctDuration(7200));
    /// packet.SetAttribute(LocalwebAttributes.AcctBalanceInfo("1024MB"));
    /// </code>
    /// </remarks>
    public static class LocalwebAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Localweb.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 4775;

        #region Integer Attributes

        /// <summary>
        /// Creates a Localweb-Acct-Resource-Type attribute (Type 4) with the specified type.
        /// </summary>
        /// <param name="value">The accounting resource type.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctResourceType(int value)
        {
            return CreateInteger(LocalwebAttributeType.ACCT_RESOURCE_TYPE, value);
        }

        /// <summary>
        /// Creates a Localweb-Acct-User-Level attribute (Type 5) with the specified level.
        /// </summary>
        /// <param name="value">The accounting user level.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctUserLevel(int value)
        {
            return CreateInteger(LocalwebAttributeType.ACCT_USER_LEVEL, value);
        }

        /// <summary>
        /// Creates a Localweb-Acct-Time attribute (Type 6) with the specified time value.
        /// </summary>
        /// <param name="value">The accounting time value.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctTime(int value)
        {
            return CreateInteger(LocalwebAttributeType.ACCT_TIME, value);
        }

        /// <summary>
        /// Creates a Localweb-Acct-Duration attribute (Type 7) with the specified duration.
        /// </summary>
        /// <param name="value">The accounting duration in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AcctDuration(int value)
        {
            return CreateInteger(LocalwebAttributeType.ACCT_DURATION, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Localweb-Acct-Resource-Info attribute (Type 1) with the specified information.
        /// </summary>
        /// <param name="value">The accounting resource information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctResourceInfo(string value)
        {
            return CreateString(LocalwebAttributeType.ACCT_RESOURCE_INFO, value);
        }

        /// <summary>
        /// Creates a Localweb-Acct-Balance-Info attribute (Type 2) with the specified balance.
        /// </summary>
        /// <param name="value">The accounting balance information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctBalanceInfo(string value)
        {
            return CreateString(LocalwebAttributeType.ACCT_BALANCE_INFO, value);
        }

        /// <summary>
        /// Creates a Localweb-Acct-Quota-Info attribute (Type 3) with the specified quota.
        /// </summary>
        /// <param name="value">The accounting quota information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctQuotaInfo(string value)
        {
            return CreateString(LocalwebAttributeType.ACCT_QUOTA_INFO, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Localweb attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(LocalwebAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Localweb attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(LocalwebAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}