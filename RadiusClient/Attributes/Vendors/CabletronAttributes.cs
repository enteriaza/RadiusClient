using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Cabletron / Enterasys (IANA PEN 52) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.cabletron</c>.
    /// </summary>
    public enum CabletronAttributeType : byte
    {
        /// <summary>Cabletron-Protocol-Enable (Type 201). Integer. Protocol enable flags.</summary>
        PROTOCOL_ENABLE = 201,

        /// <summary>Cabletron-Protocol-Callable (Type 202). Integer. Protocol callable flags.</summary>
        PROTOCOL_CALLABLE = 202,

        /// <summary>Cabletron-RADIUS-Port-Type (Type 203). Integer. RADIUS port type.</summary>
        RADIUS_PORT_TYPE = 203,

        /// <summary>Cabletron-Admin-Level (Type 204). Integer. Administrative privilege level.</summary>
        ADMIN_LEVEL = 204
    }

    /// <summary>
    /// Cabletron-Protocol-Enable attribute values (Type 201).
    /// Bit flags for enabled protocols.
    /// </summary>
    public enum CABLETRON_PROTOCOL_ENABLE
    {
        /// <summary>IP protocol enabled.</summary>
        IP = 1,

        /// <summary>IPX protocol enabled.</summary>
        IPX = 2,

        /// <summary>Both IP and IPX protocols enabled.</summary>
        IP_IPX = 3
    }

    /// <summary>
    /// Cabletron-Protocol-Callable attribute values (Type 202).
    /// Bit flags for callable protocols.
    /// </summary>
    public enum CABLETRON_PROTOCOL_CALLABLE
    {
        /// <summary>IP protocol callable.</summary>
        IP = 1,

        /// <summary>IPX protocol callable.</summary>
        IPX = 2,

        /// <summary>Both IP and IPX protocols callable.</summary>
        IP_IPX = 3
    }

    /// <summary>
    /// Cabletron-Admin-Level attribute values (Type 204).
    /// </summary>
    public enum CABLETRON_ADMIN_LEVEL
    {
        /// <summary>Read-only access.</summary>
        READ_ONLY = 0,

        /// <summary>Read-write access (non-privileged).</summary>
        READ_WRITE = 1,

        /// <summary>Read-write-all access (privileged).</summary>
        READ_WRITE_ALL = 2,

        /// <summary>Super-user access.</summary>
        SUPER_USER = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Cabletron / Enterasys
    /// (IANA PEN 52) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.cabletron</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Cabletron's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 52</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Cabletron (later Enterasys, now Extreme Networks)
    /// switches and routers for RADIUS-based protocol enable/callable flags,
    /// port type identification, and administrative privilege level assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(CabletronAttributes.AdminLevel(CABLETRON_ADMIN_LEVEL.READ_WRITE_ALL));
    /// packet.SetAttribute(CabletronAttributes.ProtocolEnable(CABLETRON_PROTOCOL_ENABLE.IP));
    /// </code>
    /// </remarks>
    public static class CabletronAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Cabletron (Enterasys / Extreme Networks).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 52;

        #region Integer Attributes

        /// <summary>
        /// Creates a Cabletron-Protocol-Enable attribute (Type 201) with the specified flags.
        /// </summary>
        /// <param name="value">The protocol enable flags. See <see cref="CABLETRON_PROTOCOL_ENABLE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ProtocolEnable(CABLETRON_PROTOCOL_ENABLE value)
        {
            return CreateInteger(CabletronAttributeType.PROTOCOL_ENABLE, (int)value);
        }

        /// <summary>
        /// Creates a Cabletron-Protocol-Callable attribute (Type 202) with the specified flags.
        /// </summary>
        /// <param name="value">The protocol callable flags. See <see cref="CABLETRON_PROTOCOL_CALLABLE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ProtocolCallable(CABLETRON_PROTOCOL_CALLABLE value)
        {
            return CreateInteger(CabletronAttributeType.PROTOCOL_CALLABLE, (int)value);
        }

        /// <summary>
        /// Creates a Cabletron-RADIUS-Port-Type attribute (Type 203) with the specified port type.
        /// </summary>
        /// <param name="value">The RADIUS port type.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RadiusPortType(int value)
        {
            return CreateInteger(CabletronAttributeType.RADIUS_PORT_TYPE, value);
        }

        /// <summary>
        /// Creates a Cabletron-Admin-Level attribute (Type 204) with the specified privilege level.
        /// </summary>
        /// <param name="value">The administrative privilege level. See <see cref="CABLETRON_ADMIN_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AdminLevel(CABLETRON_ADMIN_LEVEL value)
        {
            return CreateInteger(CabletronAttributeType.ADMIN_LEVEL, (int)value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Cabletron attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(CabletronAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}