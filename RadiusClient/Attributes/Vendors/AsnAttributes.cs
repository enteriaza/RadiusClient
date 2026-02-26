using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an ASN / Zhone (IANA PEN 23782) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.asn</c>.
    /// </summary>
    public enum AsnAttributeType : byte
    {
        /// <summary>ASN-IP-Pool-Name (Type 1). String. IP address pool name.</summary>
        IP_POOL_NAME = 1,

        /// <summary>ASN-FR-Link-Status-DLCI (Type 2). Integer. Frame Relay link status DLCI.</summary>
        FR_LINK_STATUS_DLCI = 2,

        /// <summary>ASN-FR-Circuit-Name (Type 3). String. Frame Relay circuit name.</summary>
        FR_CIRCUIT_NAME = 3,

        /// <summary>ASN-FR-Link-Name (Type 4). String. Frame Relay link name.</summary>
        FR_LINK_NAME = 4,

        /// <summary>ASN-FR-DLCI (Type 5). Integer. Frame Relay DLCI number.</summary>
        FR_DLCI = 5,

        /// <summary>ASN-FR-Profile-Name (Type 6). String. Frame Relay profile name.</summary>
        FR_PROFILE_NAME = 6,

        /// <summary>ASN-FR-N391 (Type 7). Integer. Frame Relay N391 full status polling counter.</summary>
        FR_N391 = 7,

        /// <summary>ASN-FR-DCE-N392 (Type 8). Integer. Frame Relay DCE N392 error threshold.</summary>
        FR_DCE_N392 = 8,

        /// <summary>ASN-FR-DTE-N392 (Type 9). Integer. Frame Relay DTE N392 error threshold.</summary>
        FR_DTE_N392 = 9,

        /// <summary>ASN-FR-DCE-N393 (Type 10). Integer. Frame Relay DCE N393 monitored events.</summary>
        FR_DCE_N393 = 10,

        /// <summary>ASN-FR-DTE-N393 (Type 11). Integer. Frame Relay DTE N393 monitored events.</summary>
        FR_DTE_N393 = 11,

        /// <summary>ASN-FR-T391 (Type 12). Integer. Frame Relay T391 link integrity timer.</summary>
        FR_T391 = 12,

        /// <summary>ASN-FR-T392 (Type 13). Integer. Frame Relay T392 polling verification timer.</summary>
        FR_T392 = 13,

        /// <summary>ASN-Transceiver-Slot (Type 14). Integer. Transceiver slot number.</summary>
        TRANSCEIVER_SLOT = 14,

        /// <summary>ASN-Transceiver-Port (Type 15). Integer. Transceiver port number.</summary>
        TRANSCEIVER_PORT = 15,

        /// <summary>ASN-VPI (Type 16). Integer. ATM Virtual Path Identifier.</summary>
        VPI = 16,

        /// <summary>ASN-VCI (Type 17). Integer. ATM Virtual Channel Identifier.</summary>
        VCI = 17,

        /// <summary>ASN-ATM-Service-Type (Type 18). Integer. ATM service type.</summary>
        ATM_SERVICE_TYPE = 18,

        /// <summary>ASN-ATM-PCR (Type 19). Integer. ATM Peak Cell Rate in cells/s.</summary>
        ATM_PCR = 19,

        /// <summary>ASN-ATM-SCR (Type 20). Integer. ATM Sustained Cell Rate in cells/s.</summary>
        ATM_SCR = 20,

        /// <summary>ASN-ATM-MBS (Type 21). Integer. ATM Maximum Burst Size in cells.</summary>
        ATM_MBS = 21,

        /// <summary>ASN-CLI-Initial-Menu (Type 22). String. CLI initial menu name.</summary>
        CLI_INITIAL_MENU = 22,

        /// <summary>ASN-CLI-Allow-All-Menu-Items (Type 23). Integer. CLI allow all menu items flag.</summary>
        CLI_ALLOW_ALL_MENU_ITEMS = 23,

        /// <summary>ASN-User-Level (Type 24). Integer. User privilege level.</summary>
        USER_LEVEL = 24,

        /// <summary>ASN-Tunnel-Protocol (Type 25). Integer. Tunnel protocol type.</summary>
        TUNNEL_PROTOCOL = 25,

        /// <summary>ASN-Primary-DNS (Type 26). IP address. Primary DNS server address.</summary>
        PRIMARY_DNS = 26,

        /// <summary>ASN-Secondary-DNS (Type 27). IP address. Secondary DNS server address.</summary>
        SECONDARY_DNS = 27,

        /// <summary>ASN-Domain-Name (Type 28). String. Domain name for the subscriber.</summary>
        DOMAIN_NAME = 28,

        /// <summary>ASN-Subscriber-Location (Type 29). String. Subscriber location information.</summary>
        SUBSCRIBER_LOCATION = 29,

        /// <summary>ASN-PPPoE-URL (Type 30). String. PPPoE redirect URL.</summary>
        PPPOE_URL = 30,

        /// <summary>ASN-PPPoE-MOTM (Type 31). String. PPPoE Message of the Moment.</summary>
        PPPOE_MOTM = 31,

        /// <summary>ASN-Service (Type 32). String. Service name.</summary>
        SERVICE = 32,

        /// <summary>ASN-Primary-NBNS (Type 33). IP address. Primary NetBIOS Name Server address.</summary>
        PRIMARY_NBNS = 33,

        /// <summary>ASN-Secondary-NBNS (Type 34). IP address. Secondary NetBIOS Name Server address.</summary>
        SECONDARY_NBNS = 34
    }

    /// <summary>
    /// ASN-ATM-Service-Type attribute values (Type 18).
    /// </summary>
    public enum ASN_ATM_SERVICE_TYPE
    {
        /// <summary>Constant Bit Rate (CBR).</summary>
        CBR = 1,

        /// <summary>Variable Bit Rate — real-time (VBR-rt).</summary>
        VBR_RT = 2,

        /// <summary>Variable Bit Rate — non-real-time (VBR-nrt).</summary>
        VBR_NRT = 3,

        /// <summary>Unspecified Bit Rate (UBR).</summary>
        UBR = 4,

        /// <summary>Available Bit Rate (ABR).</summary>
        ABR = 5
    }

    /// <summary>
    /// ASN-Tunnel-Protocol attribute values (Type 25).
    /// </summary>
    public enum ASN_TUNNEL_PROTOCOL
    {
        /// <summary>No tunnelling.</summary>
        NONE = 0,

        /// <summary>PPPoA tunnelling.</summary>
        PPPOA = 1,

        /// <summary>PPPoE tunnelling.</summary>
        PPPOE = 2,

        /// <summary>L2TP tunnelling.</summary>
        L2TP = 3
    }

    /// <summary>
    /// ASN-User-Level attribute values (Type 24).
    /// </summary>
    public enum ASN_USER_LEVEL
    {
        /// <summary>No access.</summary>
        NONE = 0,

        /// <summary>Monitor (read-only) access.</summary>
        MONITOR = 1,

        /// <summary>Operator access.</summary>
        OPERATOR = 2,

        /// <summary>Administrator access.</summary>
        ADMIN = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing ASN / Zhone
    /// (IANA PEN 23782) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.asn</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ASN's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 23782</c>.
    /// </para>
    /// <para>
    /// These attributes are used by ASN (All Systems Networx / Zhone) DSL access
    /// concentrators for subscriber IP pool assignment, DNS/NBNS configuration,
    /// Frame Relay and ATM circuit parameters, PPPoE management, CLI access
    /// control, and subscriber location identification.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AsnAttributes.IpPoolName("subscriber-pool"));
    /// packet.SetAttribute(AsnAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(AsnAttributes.SecondaryDns(IPAddress.Parse("8.8.4.4")));
    /// packet.SetAttribute(AsnAttributes.UserLevel(ASN_USER_LEVEL.ADMIN));
    /// </code>
    /// </remarks>
    public static class AsnAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for ASN (All Systems Networx / Zhone).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 23782;

        #region Integer Attributes

        /// <summary>Creates an ASN-FR-Link-Status-DLCI attribute (Type 2).</summary>
        /// <param name="value">The Frame Relay link status DLCI.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FrLinkStatusDlci(int value)
        {
            return CreateInteger(AsnAttributeType.FR_LINK_STATUS_DLCI, value);
        }

        /// <summary>Creates an ASN-FR-DLCI attribute (Type 5).</summary>
        /// <param name="value">The Frame Relay DLCI number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes FrDlci(int value)
        {
            return CreateInteger(AsnAttributeType.FR_DLCI, value);
        }

        /// <summary>Creates an ASN-FR-N391 attribute (Type 7).</summary>
        public static VendorSpecificAttributes FrN391(int value) => CreateInteger(AsnAttributeType.FR_N391, value);

        /// <summary>Creates an ASN-FR-DCE-N392 attribute (Type 8).</summary>
        public static VendorSpecificAttributes FrDceN392(int value) => CreateInteger(AsnAttributeType.FR_DCE_N392, value);

        /// <summary>Creates an ASN-FR-DTE-N392 attribute (Type 9).</summary>
        public static VendorSpecificAttributes FrDteN392(int value) => CreateInteger(AsnAttributeType.FR_DTE_N392, value);

        /// <summary>Creates an ASN-FR-DCE-N393 attribute (Type 10).</summary>
        public static VendorSpecificAttributes FrDceN393(int value) => CreateInteger(AsnAttributeType.FR_DCE_N393, value);

        /// <summary>Creates an ASN-FR-DTE-N393 attribute (Type 11).</summary>
        public static VendorSpecificAttributes FrDteN393(int value) => CreateInteger(AsnAttributeType.FR_DTE_N393, value);

        /// <summary>Creates an ASN-FR-T391 attribute (Type 12).</summary>
        public static VendorSpecificAttributes FrT391(int value) => CreateInteger(AsnAttributeType.FR_T391, value);

        /// <summary>Creates an ASN-FR-T392 attribute (Type 13).</summary>
        public static VendorSpecificAttributes FrT392(int value) => CreateInteger(AsnAttributeType.FR_T392, value);

        /// <summary>Creates an ASN-Transceiver-Slot attribute (Type 14).</summary>
        /// <param name="value">The transceiver slot number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TransceiverSlot(int value)
        {
            return CreateInteger(AsnAttributeType.TRANSCEIVER_SLOT, value);
        }

        /// <summary>Creates an ASN-Transceiver-Port attribute (Type 15).</summary>
        /// <param name="value">The transceiver port number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TransceiverPort(int value)
        {
            return CreateInteger(AsnAttributeType.TRANSCEIVER_PORT, value);
        }

        /// <summary>Creates an ASN-VPI attribute (Type 16).</summary>
        /// <param name="value">The ATM Virtual Path Identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Vpi(int value)
        {
            return CreateInteger(AsnAttributeType.VPI, value);
        }

        /// <summary>Creates an ASN-VCI attribute (Type 17).</summary>
        /// <param name="value">The ATM Virtual Channel Identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Vci(int value)
        {
            return CreateInteger(AsnAttributeType.VCI, value);
        }

        /// <summary>
        /// Creates an ASN-ATM-Service-Type attribute (Type 18) with the specified service type.
        /// </summary>
        /// <param name="value">The ATM service type. See <see cref="ASN_ATM_SERVICE_TYPE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AtmServiceType(ASN_ATM_SERVICE_TYPE value)
        {
            return CreateInteger(AsnAttributeType.ATM_SERVICE_TYPE, (int)value);
        }

        /// <summary>Creates an ASN-ATM-PCR attribute (Type 19).</summary>
        /// <param name="value">The ATM Peak Cell Rate in cells/s.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AtmPcr(int value)
        {
            return CreateInteger(AsnAttributeType.ATM_PCR, value);
        }

        /// <summary>Creates an ASN-ATM-SCR attribute (Type 20).</summary>
        /// <param name="value">The ATM Sustained Cell Rate in cells/s.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AtmScr(int value)
        {
            return CreateInteger(AsnAttributeType.ATM_SCR, value);
        }

        /// <summary>Creates an ASN-ATM-MBS attribute (Type 21).</summary>
        /// <param name="value">The ATM Maximum Burst Size in cells.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AtmMbs(int value)
        {
            return CreateInteger(AsnAttributeType.ATM_MBS, value);
        }

        /// <summary>Creates an ASN-CLI-Allow-All-Menu-Items attribute (Type 23).</summary>
        /// <param name="value">Whether all CLI menu items are allowed (0 = no, 1 = yes).</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes CliAllowAllMenuItems(int value)
        {
            return CreateInteger(AsnAttributeType.CLI_ALLOW_ALL_MENU_ITEMS, value);
        }

        /// <summary>
        /// Creates an ASN-User-Level attribute (Type 24) with the specified privilege level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="ASN_USER_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserLevel(ASN_USER_LEVEL value)
        {
            return CreateInteger(AsnAttributeType.USER_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates an ASN-Tunnel-Protocol attribute (Type 25) with the specified protocol.
        /// </summary>
        /// <param name="value">The tunnel protocol type. See <see cref="ASN_TUNNEL_PROTOCOL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TunnelProtocol(ASN_TUNNEL_PROTOCOL value)
        {
            return CreateInteger(AsnAttributeType.TUNNEL_PROTOCOL, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an ASN-IP-Pool-Name attribute (Type 1) with the specified pool name.
        /// </summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPoolName(string value)
        {
            return CreateString(AsnAttributeType.IP_POOL_NAME, value);
        }

        /// <summary>Creates an ASN-FR-Circuit-Name attribute (Type 3).</summary>
        /// <param name="value">The Frame Relay circuit name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FrCircuitName(string value)
        {
            return CreateString(AsnAttributeType.FR_CIRCUIT_NAME, value);
        }

        /// <summary>Creates an ASN-FR-Link-Name attribute (Type 4).</summary>
        /// <param name="value">The Frame Relay link name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FrLinkName(string value)
        {
            return CreateString(AsnAttributeType.FR_LINK_NAME, value);
        }

        /// <summary>Creates an ASN-FR-Profile-Name attribute (Type 6).</summary>
        /// <param name="value">The Frame Relay profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FrProfileName(string value)
        {
            return CreateString(AsnAttributeType.FR_PROFILE_NAME, value);
        }

        /// <summary>Creates an ASN-CLI-Initial-Menu attribute (Type 22).</summary>
        /// <param name="value">The CLI initial menu name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CliInitialMenu(string value)
        {
            return CreateString(AsnAttributeType.CLI_INITIAL_MENU, value);
        }

        /// <summary>Creates an ASN-Domain-Name attribute (Type 28).</summary>
        /// <param name="value">The domain name for the subscriber. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DomainName(string value)
        {
            return CreateString(AsnAttributeType.DOMAIN_NAME, value);
        }

        /// <summary>Creates an ASN-Subscriber-Location attribute (Type 29).</summary>
        /// <param name="value">The subscriber location information. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberLocation(string value)
        {
            return CreateString(AsnAttributeType.SUBSCRIBER_LOCATION, value);
        }

        /// <summary>Creates an ASN-PPPoE-URL attribute (Type 30).</summary>
        /// <param name="value">The PPPoE redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PppoeUrl(string value)
        {
            return CreateString(AsnAttributeType.PPPOE_URL, value);
        }

        /// <summary>Creates an ASN-PPPoE-MOTM attribute (Type 31).</summary>
        /// <param name="value">The PPPoE Message of the Moment. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PppoeMotm(string value)
        {
            return CreateString(AsnAttributeType.PPPOE_MOTM, value);
        }

        /// <summary>Creates an ASN-Service attribute (Type 32).</summary>
        /// <param name="value">The service name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Service(string value)
        {
            return CreateString(AsnAttributeType.SERVICE, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an ASN-Primary-DNS attribute (Type 26) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The primary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value)
        {
            return CreateIpv4(AsnAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates an ASN-Secondary-DNS attribute (Type 27) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The secondary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value)
        {
            return CreateIpv4(AsnAttributeType.SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates an ASN-Primary-NBNS attribute (Type 33) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The primary NetBIOS Name Server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryNbns(IPAddress value)
        {
            return CreateIpv4(AsnAttributeType.PRIMARY_NBNS, value);
        }

        /// <summary>
        /// Creates an ASN-Secondary-NBNS attribute (Type 34) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The secondary NetBIOS Name Server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryNbns(IPAddress value)
        {
            return CreateIpv4(AsnAttributeType.SECONDARY_NBNS, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified ASN attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AsnAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified ASN attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AsnAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified ASN attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(AsnAttributeType type, IPAddress value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException(
                    $"Expected an IPv4 (InterNetwork) address, got '{value.AddressFamily}'.",
                    nameof(value));

            byte[] addrBytes = new byte[4];
            value.TryWriteBytes(addrBytes, out _);

            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, addrBytes);
        }

        #endregion
    }
}