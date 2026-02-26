using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a RuggedCom (IANA PEN 15004) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.ruggedcom</c>.
    /// </summary>
    /// <remarks>
    /// RuggedCom (acquired by Siemens in 2012) produces ruggedized industrial
    /// networking equipment including managed Ethernet switches, routers, and
    /// serial device servers (ROS, ROX, RSG, RS series) for utility, transportation,
    /// oil and gas, and military/defense environments.
    /// </remarks>
    public enum RuggedcomAttributeType : byte
    {
        /// <summary>Ruggedcom-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Ruggedcom-User-Level (Type 2). Integer. User privilege level.</summary>
        USER_LEVEL = 2,

        /// <summary>Ruggedcom-User-Group (Type 3). String. User group name.</summary>
        USER_GROUP = 3
    }

    /// <summary>
    /// Ruggedcom-User-Level attribute values (Type 2).
    /// </summary>
    public enum RUGGEDCOM_USER_LEVEL
    {
        /// <summary>Guest (read-only monitoring) access.</summary>
        GUEST = 1,

        /// <summary>Operator access.</summary>
        OPERATOR = 2,

        /// <summary>Administrator access.</summary>
        ADMIN = 3,

        /// <summary>Factory/super-user access.</summary>
        FACTORY = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing RuggedCom
    /// (IANA PEN 15004) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.ruggedcom</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RuggedCom's vendor-specific attributes follow the standard VSA layout defined
    /// in RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 15004</c>.
    /// </para>
    /// <para>
    /// These attributes are used by RuggedCom (Siemens) ruggedized industrial
    /// managed Ethernet switches and routers (ROS, ROX, RSG, RS series) for
    /// RADIUS-based user privilege level assignment, user group mapping, and
    /// general-purpose attribute-value pair configuration during administrative
    /// authentication in utility, transportation, oil and gas, and military/defense
    /// environments.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(RuggedcomAttributes.UserLevel(RUGGEDCOM_USER_LEVEL.ADMIN));
    /// packet.SetAttribute(RuggedcomAttributes.UserGroup("scada-admins"));
    /// </code>
    /// </remarks>
    public static class RuggedcomAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for RuggedCom.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 15004;

        #region Integer Attributes

        /// <summary>
        /// Creates a Ruggedcom-User-Level attribute (Type 2) with the specified level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="RUGGEDCOM_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserLevel(RUGGEDCOM_USER_LEVEL value)
        {
            return CreateInteger(RuggedcomAttributeType.USER_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Ruggedcom-AVPair attribute (Type 1) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(RuggedcomAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Ruggedcom-User-Group attribute (Type 3) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(RuggedcomAttributeType.USER_GROUP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified RuggedCom attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(RuggedcomAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified RuggedCom attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(RuggedcomAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}