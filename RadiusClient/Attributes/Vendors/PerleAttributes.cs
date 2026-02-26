using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Perle Systems (IANA PEN 1966) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.perle</c>.
    /// </summary>
    /// <remarks>
    /// Perle Systems produces serial-to-Ethernet console servers, device servers,
    /// terminal servers, media converters, and industrial IoT gateways (IOLAN,
    /// IRG, MCR series).
    /// </remarks>
    public enum PerleAttributeType : byte
    {
        /// <summary>Perle-User-Level (Type 1). Integer. User privilege level.</summary>
        USER_LEVEL = 1,

        /// <summary>Perle-Filter-Id (Type 2). String. Filter identifier.</summary>
        FILTER_ID = 2,

        /// <summary>Perle-Port-Access-List (Type 3). String. Port access list.</summary>
        PORT_ACCESS_LIST = 3,

        /// <summary>Perle-AVPair (Type 4). String. Attribute-value pair string.</summary>
        AVPAIR = 4,

        /// <summary>Perle-Cluster-Name (Type 5). String. Cluster name.</summary>
        CLUSTER_NAME = 5,

        /// <summary>Perle-Menu-Name (Type 6). String. Menu name.</summary>
        MENU_NAME = 6,

        /// <summary>Perle-Power-Group (Type 7). String. Power management group name.</summary>
        POWER_GROUP = 7
    }

    /// <summary>
    /// Perle-User-Level attribute values (Type 1).
    /// </summary>
    public enum PERLE_USER_LEVEL
    {
        /// <summary>Standard user access.</summary>
        USER = 0,

        /// <summary>Administrator access.</summary>
        ADMIN = 1,

        /// <summary>Super administrator (full) access.</summary>
        SUPER_ADMIN = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Perle Systems
    /// (IANA PEN 1966) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.perle</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Perle's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 1966</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Perle Systems console servers, device servers,
    /// and industrial IoT gateways (IOLAN, IRG series) for RADIUS-based user
    /// privilege level assignment, port access list configuration, filter
    /// identification, cluster and menu name selection, power management group
    /// assignment, and general-purpose attribute-value pair configuration during
    /// administrative authentication.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(PerleAttributes.UserLevel(PERLE_USER_LEVEL.ADMIN));
    /// packet.SetAttribute(PerleAttributes.PortAccessList("1-8,12"));
    /// packet.SetAttribute(PerleAttributes.ClusterName("datacenter-west"));
    /// packet.SetAttribute(PerleAttributes.PowerGroup("rack-a-power"));
    /// </code>
    /// </remarks>
    public static class PerleAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Perle Systems.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 1966;

        #region Integer Attributes

        /// <summary>
        /// Creates a Perle-User-Level attribute (Type 1) with the specified level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="PERLE_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserLevel(PERLE_USER_LEVEL value)
        {
            return CreateInteger(PerleAttributeType.USER_LEVEL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Perle-Filter-Id attribute (Type 2) with the specified filter.
        /// </summary>
        /// <param name="value">The filter identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FilterId(string value)
        {
            return CreateString(PerleAttributeType.FILTER_ID, value);
        }

        /// <summary>
        /// Creates a Perle-Port-Access-List attribute (Type 3) with the specified port list.
        /// </summary>
        /// <param name="value">The port access list (e.g. "1-8,12"). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PortAccessList(string value)
        {
            return CreateString(PerleAttributeType.PORT_ACCESS_LIST, value);
        }

        /// <summary>
        /// Creates a Perle-AVPair attribute (Type 4) with the specified attribute-value pair.
        /// </summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value)
        {
            return CreateString(PerleAttributeType.AVPAIR, value);
        }

        /// <summary>
        /// Creates a Perle-Cluster-Name attribute (Type 5) with the specified cluster name.
        /// </summary>
        /// <param name="value">The cluster name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ClusterName(string value)
        {
            return CreateString(PerleAttributeType.CLUSTER_NAME, value);
        }

        /// <summary>
        /// Creates a Perle-Menu-Name attribute (Type 6) with the specified menu name.
        /// </summary>
        /// <param name="value">The menu name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes MenuName(string value)
        {
            return CreateString(PerleAttributeType.MENU_NAME, value);
        }

        /// <summary>
        /// Creates a Perle-Power-Group attribute (Type 7) with the specified power group name.
        /// </summary>
        /// <param name="value">The power management group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PowerGroup(string value)
        {
            return CreateString(PerleAttributeType.POWER_GROUP, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Perle attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(PerleAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Perle attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(PerleAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}