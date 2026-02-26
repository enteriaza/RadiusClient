using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Waverider Communications (IANA PEN 2979) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.waverider</c>.
    /// </summary>
    /// <remarks>
    /// Waverider Communications (later acquired by Solectek Corporation) produced
    /// fixed wireless broadband equipment including point-to-point and
    /// point-to-multipoint radio systems for last-mile broadband access in
    /// rural, enterprise, and WISP deployments.
    /// </remarks>
    public enum WaveriderAttributeType : byte
    {
        /// <summary>Waverider-Grade-Of-Service (Type 1). Integer. Grade of service level.</summary>
        GRADE_OF_SERVICE = 1,

        /// <summary>Waverider-Priority-Enabled (Type 2). Integer. Priority enabled flag.</summary>
        PRIORITY_ENABLED = 2,

        /// <summary>Waverider-Authentication-Key (Type 3). String. Authentication key.</summary>
        AUTHENTICATION_KEY = 3,

        /// <summary>Waverider-Current-Password (Type 4). String. Current password.</summary>
        CURRENT_PASSWORD = 4,

        /// <summary>Waverider-New-Password (Type 5). String. New password.</summary>
        NEW_PASSWORD = 5,

        /// <summary>Waverider-Radio-Frequency (Type 6). Integer. Radio frequency.</summary>
        RADIO_FREQUENCY = 6,

        /// <summary>Waverider-SNMP-Read-Community (Type 7). String. SNMP read community string.</summary>
        SNMP_READ_COMMUNITY = 7,

        /// <summary>Waverider-SNMP-Write-Community (Type 8). String. SNMP write community string.</summary>
        SNMP_WRITE_COMMUNITY = 8,

        /// <summary>Waverider-SNMP-Trap-Server (Type 9). String. SNMP trap server address.</summary>
        SNMP_TRAP_SERVER = 9,

        /// <summary>Waverider-SNMP-Contact (Type 10). String. SNMP contact information.</summary>
        SNMP_CONTACT = 10,

        /// <summary>Waverider-SNMP-Location (Type 11). String. SNMP location information.</summary>
        SNMP_LOCATION = 11,

        /// <summary>Waverider-SNMP-Name (Type 12). String. SNMP system name.</summary>
        SNMP_NAME = 12,

        /// <summary>Waverider-Max-Customers (Type 13). Integer. Maximum customer connections.</summary>
        MAX_CUSTOMERS = 13,

        /// <summary>Waverider-Rf-Power (Type 14). Integer. RF transmit power level.</summary>
        RF_POWER = 14
    }

    /// <summary>
    /// Waverider-Grade-Of-Service attribute values (Type 1).
    /// </summary>
    public enum WAVERIDER_GRADE_OF_SERVICE
    {
        /// <summary>Best effort service.</summary>
        BEST_EFFORT = 0,

        /// <summary>Premium service.</summary>
        PREMIUM = 1
    }

    /// <summary>
    /// Waverider-Priority-Enabled attribute values (Type 2).
    /// </summary>
    public enum WAVERIDER_PRIORITY_ENABLED
    {
        /// <summary>Priority disabled.</summary>
        DISABLED = 0,

        /// <summary>Priority enabled.</summary>
        ENABLED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Waverider Communications
    /// (IANA PEN 2979) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.waverider</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Waverider's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2979</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Waverider Communications (Solectek) fixed wireless
    /// broadband equipment for RADIUS-based grade of service assignment, traffic
    /// priority enablement, authentication key and password management, radio
    /// frequency configuration, SNMP community string and trap server provisioning,
    /// SNMP contact/location/name metadata, maximum customer connection limits,
    /// and RF transmit power level configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(WaveriderAttributes.GradeOfService(WAVERIDER_GRADE_OF_SERVICE.PREMIUM));
    /// packet.SetAttribute(WaveriderAttributes.PriorityEnabled(WAVERIDER_PRIORITY_ENABLED.ENABLED));
    /// packet.SetAttribute(WaveriderAttributes.MaxCustomers(64));
    /// packet.SetAttribute(WaveriderAttributes.SnmpReadCommunity("public"));
    /// packet.SetAttribute(WaveriderAttributes.RfPower(20));
    /// </code>
    /// </remarks>
    public static class WaveriderAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Waverider Communications.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2979;

        #region Integer Attributes

        /// <summary>
        /// Creates a Waverider-Grade-Of-Service attribute (Type 1) with the specified level.
        /// </summary>
        /// <param name="value">The grade of service level. See <see cref="WAVERIDER_GRADE_OF_SERVICE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes GradeOfService(WAVERIDER_GRADE_OF_SERVICE value)
        {
            return CreateInteger(WaveriderAttributeType.GRADE_OF_SERVICE, (int)value);
        }

        /// <summary>
        /// Creates a Waverider-Priority-Enabled attribute (Type 2) with the specified flag.
        /// </summary>
        /// <param name="value">The priority enabled flag. See <see cref="WAVERIDER_PRIORITY_ENABLED"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PriorityEnabled(WAVERIDER_PRIORITY_ENABLED value)
        {
            return CreateInteger(WaveriderAttributeType.PRIORITY_ENABLED, (int)value);
        }

        /// <summary>
        /// Creates a Waverider-Radio-Frequency attribute (Type 6) with the specified frequency.
        /// </summary>
        /// <param name="value">The radio frequency.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RadioFrequency(int value)
        {
            return CreateInteger(WaveriderAttributeType.RADIO_FREQUENCY, value);
        }

        /// <summary>
        /// Creates a Waverider-Max-Customers attribute (Type 13) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum customer connections.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxCustomers(int value)
        {
            return CreateInteger(WaveriderAttributeType.MAX_CUSTOMERS, value);
        }

        /// <summary>
        /// Creates a Waverider-Rf-Power attribute (Type 14) with the specified power level.
        /// </summary>
        /// <param name="value">The RF transmit power level.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RfPower(int value)
        {
            return CreateInteger(WaveriderAttributeType.RF_POWER, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Waverider-Authentication-Key attribute (Type 3) with the specified key.
        /// </summary>
        /// <param name="value">The authentication key. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthenticationKey(string value)
        {
            return CreateString(WaveriderAttributeType.AUTHENTICATION_KEY, value);
        }

        /// <summary>
        /// Creates a Waverider-Current-Password attribute (Type 4) with the specified password.
        /// </summary>
        /// <param name="value">The current password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CurrentPassword(string value)
        {
            return CreateString(WaveriderAttributeType.CURRENT_PASSWORD, value);
        }

        /// <summary>
        /// Creates a Waverider-New-Password attribute (Type 5) with the specified password.
        /// </summary>
        /// <param name="value">The new password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NewPassword(string value)
        {
            return CreateString(WaveriderAttributeType.NEW_PASSWORD, value);
        }

        /// <summary>
        /// Creates a Waverider-SNMP-Read-Community attribute (Type 7) with the specified community string.
        /// </summary>
        /// <param name="value">The SNMP read community string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SnmpReadCommunity(string value)
        {
            return CreateString(WaveriderAttributeType.SNMP_READ_COMMUNITY, value);
        }

        /// <summary>
        /// Creates a Waverider-SNMP-Write-Community attribute (Type 8) with the specified community string.
        /// </summary>
        /// <param name="value">The SNMP write community string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SnmpWriteCommunity(string value)
        {
            return CreateString(WaveriderAttributeType.SNMP_WRITE_COMMUNITY, value);
        }

        /// <summary>
        /// Creates a Waverider-SNMP-Trap-Server attribute (Type 9) with the specified server address.
        /// </summary>
        /// <param name="value">The SNMP trap server address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SnmpTrapServer(string value)
        {
            return CreateString(WaveriderAttributeType.SNMP_TRAP_SERVER, value);
        }

        /// <summary>
        /// Creates a Waverider-SNMP-Contact attribute (Type 10) with the specified contact information.
        /// </summary>
        /// <param name="value">The SNMP contact information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SnmpContact(string value)
        {
            return CreateString(WaveriderAttributeType.SNMP_CONTACT, value);
        }

        /// <summary>
        /// Creates a Waverider-SNMP-Location attribute (Type 11) with the specified location.
        /// </summary>
        /// <param name="value">The SNMP location information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SnmpLocation(string value)
        {
            return CreateString(WaveriderAttributeType.SNMP_LOCATION, value);
        }

        /// <summary>
        /// Creates a Waverider-SNMP-Name attribute (Type 12) with the specified system name.
        /// </summary>
        /// <param name="value">The SNMP system name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SnmpName(string value)
        {
            return CreateString(WaveriderAttributeType.SNMP_NAME, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(WaveriderAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(WaveriderAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}