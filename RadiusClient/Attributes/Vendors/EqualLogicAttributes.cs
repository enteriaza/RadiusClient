using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an EqualLogic / Dell EqualLogic (IANA PEN 12740) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.equallogic</c>.
    /// </summary>
    public enum EqualLogicAttributeType : byte
    {
        /// <summary>EqualLogic-Admin-Account-Type (Type 1). Integer. Administrative account type.</summary>
        ADMIN_ACCOUNT_TYPE = 1,

        /// <summary>EqualLogic-Admin-Full-Name (Type 2). String. Administrator full name.</summary>
        ADMIN_FULL_NAME = 2,

        /// <summary>EqualLogic-Admin-Email (Type 3). String. Administrator e-mail address.</summary>
        ADMIN_EMAIL = 3,

        /// <summary>EqualLogic-Admin-Phone (Type 4). String. Administrator phone number.</summary>
        ADMIN_PHONE = 4,

        /// <summary>EqualLogic-Admin-Mobile (Type 5). String. Administrator mobile number.</summary>
        ADMIN_MOBILE = 5,

        /// <summary>EqualLogic-Poll-Interval (Type 6). Integer. SNMP poll interval in seconds.</summary>
        POLL_INTERVAL = 6,

        /// <summary>EqualLogic-Admin-Account-Privilege (Type 7). String. Administrator account privilege.</summary>
        ADMIN_ACCOUNT_PRIVILEGE = 7
    }

    /// <summary>
    /// EqualLogic-Admin-Account-Type attribute values (Type 1).
    /// </summary>
    public enum EQUALLOGIC_ADMIN_ACCOUNT_TYPE
    {
        /// <summary>Read-only account.</summary>
        READ_ONLY = 0,

        /// <summary>Read-write (group administrator) account.</summary>
        GROUP_ADMIN = 1,

        /// <summary>Pool administrator account.</summary>
        POOL_ADMIN = 2,

        /// <summary>Volume administrator account.</summary>
        VOLUME_ADMIN = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing EqualLogic / Dell EqualLogic
    /// (IANA PEN 12740) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.equallogic</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// EqualLogic's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 12740</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Dell EqualLogic PS Series iSCSI SAN storage
    /// arrays for RADIUS-based administrative account type assignment, administrator
    /// contact information (name, e-mail, phone, mobile), SNMP poll intervals,
    /// and account privilege configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(EqualLogicAttributes.AdminAccountType(EQUALLOGIC_ADMIN_ACCOUNT_TYPE.GROUP_ADMIN));
    /// packet.SetAttribute(EqualLogicAttributes.AdminFullName("John Smith"));
    /// packet.SetAttribute(EqualLogicAttributes.AdminEmail("john.smith@example.com"));
    /// packet.SetAttribute(EqualLogicAttributes.AdminAccountPrivilege("group-admin"));
    /// </code>
    /// </remarks>
    public static class EqualLogicAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for EqualLogic (Dell EqualLogic).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 12740;

        #region Integer Attributes

        /// <summary>
        /// Creates an EqualLogic-Admin-Account-Type attribute (Type 1) with the specified type.
        /// </summary>
        /// <param name="value">The administrative account type. See <see cref="EQUALLOGIC_ADMIN_ACCOUNT_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminAccountType(EQUALLOGIC_ADMIN_ACCOUNT_TYPE value)
        {
            return CreateInteger(EqualLogicAttributeType.ADMIN_ACCOUNT_TYPE, (int)value);
        }

        /// <summary>
        /// Creates an EqualLogic-Poll-Interval attribute (Type 6) with the specified interval.
        /// </summary>
        /// <param name="value">The SNMP poll interval in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PollInterval(int value)
        {
            return CreateInteger(EqualLogicAttributeType.POLL_INTERVAL, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an EqualLogic-Admin-Full-Name attribute (Type 2) with the specified name.
        /// </summary>
        /// <param name="value">The administrator full name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminFullName(string value)
        {
            return CreateString(EqualLogicAttributeType.ADMIN_FULL_NAME, value);
        }

        /// <summary>
        /// Creates an EqualLogic-Admin-Email attribute (Type 3) with the specified e-mail.
        /// </summary>
        /// <param name="value">The administrator e-mail address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminEmail(string value)
        {
            return CreateString(EqualLogicAttributeType.ADMIN_EMAIL, value);
        }

        /// <summary>
        /// Creates an EqualLogic-Admin-Phone attribute (Type 4) with the specified phone number.
        /// </summary>
        /// <param name="value">The administrator phone number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminPhone(string value)
        {
            return CreateString(EqualLogicAttributeType.ADMIN_PHONE, value);
        }

        /// <summary>
        /// Creates an EqualLogic-Admin-Mobile attribute (Type 5) with the specified mobile number.
        /// </summary>
        /// <param name="value">The administrator mobile number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminMobile(string value)
        {
            return CreateString(EqualLogicAttributeType.ADMIN_MOBILE, value);
        }

        /// <summary>
        /// Creates an EqualLogic-Admin-Account-Privilege attribute (Type 7) with the specified privilege.
        /// </summary>
        /// <param name="value">The administrator account privilege. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdminAccountPrivilege(string value)
        {
            return CreateString(EqualLogicAttributeType.ADMIN_ACCOUNT_PRIVILEGE, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified EqualLogic attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(EqualLogicAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified EqualLogic attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(EqualLogicAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}