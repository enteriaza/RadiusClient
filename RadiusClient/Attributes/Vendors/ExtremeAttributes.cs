using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Extreme Networks (IANA PEN 1916) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.extreme</c>.
    /// </summary>
    public enum ExtremeAttributeType : byte
    {
        /// <summary>Extreme-CLI-Authorization (Type 201). Integer. CLI authorization level.</summary>
        CLI_AUTHORIZATION = 201,

        /// <summary>Extreme-Shell-Command (Type 202). String. Shell command string.</summary>
        SHELL_COMMAND = 202,

        /// <summary>Extreme-Netlogin-Vlan (Type 203). String. Netlogin VLAN name.</summary>
        NETLOGIN_VLAN = 203,

        /// <summary>Extreme-Netlogin-Url (Type 204). String. Netlogin redirect URL.</summary>
        NETLOGIN_URL = 204,

        /// <summary>Extreme-Netlogin-Url-Desc (Type 205). String. Netlogin URL description.</summary>
        NETLOGIN_URL_DESC = 205,

        /// <summary>Extreme-Netlogin-Only (Type 206). Integer. Netlogin only flag.</summary>
        NETLOGIN_ONLY = 206,

        /// <summary>Extreme-User-Location (Type 208). String. User location string.</summary>
        USER_LOCATION = 208,

        /// <summary>Extreme-Netlogin-Vlan-Tag (Type 209). Integer. Netlogin VLAN tag (802.1Q).</summary>
        NETLOGIN_VLAN_TAG = 209,

        /// <summary>Extreme-Netlogin-Extended-Vlan (Type 211). String. Netlogin extended VLAN configuration.</summary>
        NETLOGIN_EXTENDED_VLAN = 211,

        /// <summary>Extreme-Security-Profile (Type 212). String. Security profile name.</summary>
        SECURITY_PROFILE = 212,

        /// <summary>Extreme-VM-Name (Type 213). String. Virtual machine name.</summary>
        VM_NAME = 213,

        /// <summary>Extreme-VM-VPP-Name (Type 214). String. Virtual machine VPP name.</summary>
        VM_VPP_NAME = 214,

        /// <summary>Extreme-VM-IP-Addr (Type 215). String. Virtual machine IP address.</summary>
        VM_IP_ADDR = 215,

        /// <summary>Extreme-VM-VLAN-ID (Type 216). Integer. Virtual machine VLAN identifier.</summary>
        VM_VLAN_ID = 216,

        /// <summary>Extreme-VM-VR-Name (Type 217). String. Virtual machine virtual router name.</summary>
        VM_VR_NAME = 217,

        /// <summary>Extreme-Policy-Name (Type 218). String. Policy name to apply.</summary>
        POLICY_NAME = 218
    }

    /// <summary>
    /// Extreme-CLI-Authorization attribute values (Type 201).
    /// </summary>
    public enum EXTREME_CLI_AUTHORIZATION
    {
        /// <summary>CLI authorization disabled.</summary>
        DISABLED = 0,

        /// <summary>CLI authorization enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// Extreme-Netlogin-Only attribute values (Type 206).
    /// </summary>
    public enum EXTREME_NETLOGIN_ONLY
    {
        /// <summary>Netlogin only disabled.</summary>
        DISABLED = 0,

        /// <summary>Netlogin only enabled.</summary>
        ENABLED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Extreme Networks
    /// (IANA PEN 1916) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.extreme</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Extreme Networks' vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped
    /// in a <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 1916</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Extreme Networks (formerly Enterasys / Cabletron)
    /// switches and wireless controllers, including ExtremeXOS and EXOS platforms,
    /// for RADIUS-based CLI authorization, netlogin VLAN assignment (by name, tag,
    /// and extended configuration), captive portal URL redirection, security profile
    /// selection, policy assignment, user location tracking, shell command
    /// authorization, and virtual machine (VM) network configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ExtremeAttributes.CliAuthorization(EXTREME_CLI_AUTHORIZATION.ENABLED));
    /// packet.SetAttribute(ExtremeAttributes.NetloginVlan("corp-vlan"));
    /// packet.SetAttribute(ExtremeAttributes.NetloginVlanTag(100));
    /// packet.SetAttribute(ExtremeAttributes.PolicyName("employee-policy"));
    /// packet.SetAttribute(ExtremeAttributes.SecurityProfile("wpa2-enterprise"));
    /// </code>
    /// </remarks>
    public static class ExtremeAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Extreme Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 1916;

        #region Integer Attributes

        /// <summary>
        /// Creates an Extreme-CLI-Authorization attribute (Type 201) with the specified setting.
        /// </summary>
        /// <param name="value">The CLI authorization setting. See <see cref="EXTREME_CLI_AUTHORIZATION"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CliAuthorization(EXTREME_CLI_AUTHORIZATION value)
        {
            return CreateInteger(ExtremeAttributeType.CLI_AUTHORIZATION, (int)value);
        }

        /// <summary>
        /// Creates an Extreme-Netlogin-Only attribute (Type 206) with the specified setting.
        /// </summary>
        /// <param name="value">The netlogin only setting. See <see cref="EXTREME_NETLOGIN_ONLY"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NetloginOnly(EXTREME_NETLOGIN_ONLY value)
        {
            return CreateInteger(ExtremeAttributeType.NETLOGIN_ONLY, (int)value);
        }

        /// <summary>
        /// Creates an Extreme-Netlogin-Vlan-Tag attribute (Type 209) with the specified VLAN tag.
        /// </summary>
        /// <param name="value">The netlogin VLAN tag (802.1Q).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NetloginVlanTag(int value)
        {
            return CreateInteger(ExtremeAttributeType.NETLOGIN_VLAN_TAG, value);
        }

        /// <summary>
        /// Creates an Extreme-VM-VLAN-ID attribute (Type 216) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The virtual machine VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VmVlanId(int value)
        {
            return CreateInteger(ExtremeAttributeType.VM_VLAN_ID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Extreme-Shell-Command attribute (Type 202) with the specified command.
        /// </summary>
        /// <param name="value">The shell command string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ShellCommand(string value)
        {
            return CreateString(ExtremeAttributeType.SHELL_COMMAND, value);
        }

        /// <summary>
        /// Creates an Extreme-Netlogin-Vlan attribute (Type 203) with the specified VLAN name.
        /// </summary>
        /// <param name="value">The netlogin VLAN name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NetloginVlan(string value)
        {
            return CreateString(ExtremeAttributeType.NETLOGIN_VLAN, value);
        }

        /// <summary>
        /// Creates an Extreme-Netlogin-Url attribute (Type 204) with the specified URL.
        /// </summary>
        /// <param name="value">The netlogin redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NetloginUrl(string value)
        {
            return CreateString(ExtremeAttributeType.NETLOGIN_URL, value);
        }

        /// <summary>
        /// Creates an Extreme-Netlogin-Url-Desc attribute (Type 205) with the specified description.
        /// </summary>
        /// <param name="value">The netlogin URL description. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NetloginUrlDesc(string value)
        {
            return CreateString(ExtremeAttributeType.NETLOGIN_URL_DESC, value);
        }

        /// <summary>
        /// Creates an Extreme-User-Location attribute (Type 208) with the specified location.
        /// </summary>
        /// <param name="value">The user location string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserLocation(string value)
        {
            return CreateString(ExtremeAttributeType.USER_LOCATION, value);
        }

        /// <summary>
        /// Creates an Extreme-Netlogin-Extended-Vlan attribute (Type 211) with the specified configuration.
        /// </summary>
        /// <param name="value">The netlogin extended VLAN configuration. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NetloginExtendedVlan(string value)
        {
            return CreateString(ExtremeAttributeType.NETLOGIN_EXTENDED_VLAN, value);
        }

        /// <summary>
        /// Creates an Extreme-Security-Profile attribute (Type 212) with the specified profile name.
        /// </summary>
        /// <param name="value">The security profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SecurityProfile(string value)
        {
            return CreateString(ExtremeAttributeType.SECURITY_PROFILE, value);
        }

        /// <summary>
        /// Creates an Extreme-VM-Name attribute (Type 213) with the specified VM name.
        /// </summary>
        /// <param name="value">The virtual machine name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VmName(string value)
        {
            return CreateString(ExtremeAttributeType.VM_NAME, value);
        }

        /// <summary>
        /// Creates an Extreme-VM-VPP-Name attribute (Type 214) with the specified VPP name.
        /// </summary>
        /// <param name="value">The virtual machine VPP name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VmVppName(string value)
        {
            return CreateString(ExtremeAttributeType.VM_VPP_NAME, value);
        }

        /// <summary>
        /// Creates an Extreme-VM-IP-Addr attribute (Type 215) with the specified IP address.
        /// </summary>
        /// <param name="value">The virtual machine IP address. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VmIpAddr(string value)
        {
            return CreateString(ExtremeAttributeType.VM_IP_ADDR, value);
        }

        /// <summary>
        /// Creates an Extreme-VM-VR-Name attribute (Type 217) with the specified virtual router name.
        /// </summary>
        /// <param name="value">The virtual machine virtual router name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VmVrName(string value)
        {
            return CreateString(ExtremeAttributeType.VM_VR_NAME, value);
        }

        /// <summary>
        /// Creates an Extreme-Policy-Name attribute (Type 218) with the specified policy name.
        /// </summary>
        /// <param name="value">The policy name to apply. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value)
        {
            return CreateString(ExtremeAttributeType.POLICY_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Extreme attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(ExtremeAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Extreme attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(ExtremeAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}