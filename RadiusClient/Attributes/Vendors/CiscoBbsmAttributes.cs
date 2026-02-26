using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Cisco BBSM (IANA PEN 5263) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.cisco.bbsm</c>.
    /// </summary>
    /// <remarks>
    /// Cisco BBSM (Building Broadband Service Manager) uses vendor ID 5263 for its
    /// RADIUS attributes, separate from the main Cisco vendor ID 9.
    /// </remarks>
    public enum CiscoBbsmAttributeType : byte
    {
        /// <summary>BBSM-Bandwidth (Type 1). Integer. Bandwidth in Kbps.</summary>
        BANDWIDTH = 1,

        /// <summary>BBSM-Service (Type 2). String. Service name.</summary>
        SERVICE = 2,

        /// <summary>BBSM-VLAN-ID (Type 3). Integer. VLAN identifier.</summary>
        VLAN_ID = 3,

        /// <summary>BBSM-Policy (Type 4). String. Policy name.</summary>
        POLICY = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Cisco BBSM
    /// (IANA PEN 5263) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.cisco.bbsm</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Cisco BBSM's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 5263</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Cisco Building Broadband Service Manager (BBSM)
    /// platforms for RADIUS-based bandwidth provisioning, service assignment,
    /// VLAN mapping, and policy enforcement for broadband subscribers.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CiscoBbsmAttributes.Bandwidth(10000));
    /// packet.SetAttribute(CiscoBbsmAttributes.Service("premium-internet"));
    /// packet.SetAttribute(CiscoBbsmAttributes.VlanId(100));
    /// packet.SetAttribute(CiscoBbsmAttributes.Policy("default-policy"));
    /// </code>
    /// </remarks>
    public static class CiscoBbsmAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Cisco BBSM.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 5263;

        #region Integer Attributes

        /// <summary>
        /// Creates a BBSM-Bandwidth attribute (Type 1) with the specified bandwidth.
        /// </summary>
        /// <param name="value">The bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Bandwidth(int value)
        {
            return CreateInteger(CiscoBbsmAttributeType.BANDWIDTH, value);
        }

        /// <summary>
        /// Creates a BBSM-VLAN-ID attribute (Type 3) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(CiscoBbsmAttributeType.VLAN_ID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a BBSM-Service attribute (Type 2) with the specified service name.
        /// </summary>
        /// <param name="value">The service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Service(string value)
        {
            return CreateString(CiscoBbsmAttributeType.SERVICE, value);
        }

        /// <summary>
        /// Creates a BBSM-Policy attribute (Type 4) with the specified policy name.
        /// </summary>
        /// <param name="value">The policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Policy(string value)
        {
            return CreateString(CiscoBbsmAttributeType.POLICY, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Cisco BBSM attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(CiscoBbsmAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Cisco BBSM attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(CiscoBbsmAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}