using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a KarlNet (IANA PEN 762) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.karlnet</c>.
    /// </summary>
    /// <remarks>
    /// KarlNet (later acquired by ZoomTel/Zyxel) produced wireless LAN
    /// infrastructure equipment including outdoor wireless bridges and
    /// access points.
    /// </remarks>
    public enum KarlnetAttributeType : byte
    {
        /// <summary>KarlNet-TurboCell-Name (Type 1). String. TurboCell network name.</summary>
        TURBOCELL_NAME = 1,

        /// <summary>KarlNet-TurboCell-TxRate (Type 2). Integer. TurboCell transmit rate.</summary>
        TURBOCELL_TXRATE = 2,

        /// <summary>KarlNet-TurboCell-OpState (Type 3). Integer. TurboCell operational state.</summary>
        TURBOCELL_OPSTATE = 3,

        /// <summary>KarlNet-TurboCell-OpMode (Type 4). Integer. TurboCell operational mode.</summary>
        TURBOCELL_OPMODE = 4
    }

    /// <summary>
    /// KarlNet-TurboCell-TxRate attribute values (Type 2).
    /// </summary>
    public enum KARLNET_TURBOCELL_TXRATE
    {
        /// <summary>Automatic rate selection.</summary>
        AUTO = 0,

        /// <summary>1 Mbps fixed rate.</summary>
        FIX_1 = 1,

        /// <summary>2 Mbps fixed rate.</summary>
        FIX_2 = 2,

        /// <summary>5.5 Mbps fixed rate.</summary>
        FIX_5_5 = 3,

        /// <summary>11 Mbps fixed rate.</summary>
        FIX_11 = 4
    }

    /// <summary>
    /// KarlNet-TurboCell-OpState attribute values (Type 3).
    /// </summary>
    public enum KARLNET_TURBOCELL_OPSTATE
    {
        /// <summary>TurboCell enabled.</summary>
        ENABLED = 0,

        /// <summary>TurboCell disabled.</summary>
        DISABLED = 1
    }

    /// <summary>
    /// KarlNet-TurboCell-OpMode attribute values (Type 4).
    /// </summary>
    public enum KARLNET_TURBOCELL_OPMODE
    {
        /// <summary>Base station mode.</summary>
        BASE = 0,

        /// <summary>Satellite (client) mode.</summary>
        SATELLITE = 1,

        /// <summary>Bridge mode.</summary>
        BRIDGE = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing KarlNet
    /// (IANA PEN 762) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.karlnet</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// KarlNet's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 762</c>.
    /// </para>
    /// <para>
    /// These attributes are used by KarlNet TurboCell wireless access points
    /// and bridges for RADIUS-based TurboCell network name assignment,
    /// transmit rate selection, operational state control (enable/disable),
    /// and operational mode configuration (base station, satellite, bridge).
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(KarlnetAttributes.TurboCellName("outdoor-link-1"));
    /// packet.SetAttribute(KarlnetAttributes.TurboCellTxRate(KARLNET_TURBOCELL_TXRATE.FIX_11));
    /// packet.SetAttribute(KarlnetAttributes.TurboCellOpState(KARLNET_TURBOCELL_OPSTATE.ENABLED));
    /// packet.SetAttribute(KarlnetAttributes.TurboCellOpMode(KARLNET_TURBOCELL_OPMODE.BASE));
    /// </code>
    /// </remarks>
    public static class KarlnetAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for KarlNet.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 762;

        #region Integer Attributes

        /// <summary>
        /// Creates a KarlNet-TurboCell-TxRate attribute (Type 2) with the specified rate.
        /// </summary>
        /// <param name="value">The TurboCell transmit rate. See <see cref="KARLNET_TURBOCELL_TXRATE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TurboCellTxRate(KARLNET_TURBOCELL_TXRATE value)
        {
            return CreateInteger(KarlnetAttributeType.TURBOCELL_TXRATE, (int)value);
        }

        /// <summary>
        /// Creates a KarlNet-TurboCell-OpState attribute (Type 3) with the specified state.
        /// </summary>
        /// <param name="value">The TurboCell operational state. See <see cref="KARLNET_TURBOCELL_OPSTATE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TurboCellOpState(KARLNET_TURBOCELL_OPSTATE value)
        {
            return CreateInteger(KarlnetAttributeType.TURBOCELL_OPSTATE, (int)value);
        }

        /// <summary>
        /// Creates a KarlNet-TurboCell-OpMode attribute (Type 4) with the specified mode.
        /// </summary>
        /// <param name="value">The TurboCell operational mode. See <see cref="KARLNET_TURBOCELL_OPMODE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TurboCellOpMode(KARLNET_TURBOCELL_OPMODE value)
        {
            return CreateInteger(KarlnetAttributeType.TURBOCELL_OPMODE, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a KarlNet-TurboCell-Name attribute (Type 1) with the specified network name.
        /// </summary>
        /// <param name="value">The TurboCell network name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TurboCellName(string value)
        {
            return CreateString(KarlnetAttributeType.TURBOCELL_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified KarlNet attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(KarlnetAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified KarlNet attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(KarlnetAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}