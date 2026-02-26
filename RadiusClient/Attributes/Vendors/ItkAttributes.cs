using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an ITK (IANA PEN 1195) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.itk</c>.
    /// </summary>
    /// <remarks>
    /// ITK (Institut für Telematik und Kommunikationstechnik) produced
    /// telecommunications and VoIP equipment.
    /// </remarks>
    public enum ItkAttributeType : byte
    {
        /// <summary>ITK-Auth-Serv-IP (Type 100). String. Authentication server IP address.</summary>
        AUTH_SERV_IP = 100,

        /// <summary>ITK-Auth-Serv-Prot (Type 101). Integer. Authentication server protocol.</summary>
        AUTH_SERV_PROT = 101,

        /// <summary>ITK-Provider-Id (Type 102). Integer. Provider identifier.</summary>
        PROVIDER_ID = 102,

        /// <summary>ITK-Usergroup (Type 103). String. User group name.</summary>
        USERGROUP = 103,

        /// <summary>ITK-Banner (Type 104). String. Login banner text.</summary>
        BANNER = 104,

        /// <summary>ITK-Username-Prompt (Type 105). String. Username prompt text.</summary>
        USERNAME_PROMPT = 105,

        /// <summary>ITK-Password-Prompt (Type 106). String. Password prompt text.</summary>
        PASSWORD_PROMPT = 106,

        /// <summary>ITK-Welcome-Message (Type 107). String. Welcome message text.</summary>
        WELCOME_MESSAGE = 107,

        /// <summary>ITK-Reject-Message (Type 108). String. Reject message text.</summary>
        REJECT_MESSAGE = 108,

        /// <summary>ITK-Channel-Binding (Type 135). String. Channel binding specification.</summary>
        CHANNEL_BINDING = 135,

        /// <summary>ITK-Start-Delay (Type 136). Integer. Start delay in seconds.</summary>
        START_DELAY = 136,

        /// <summary>ITK-NAS-Name (Type 137). String. NAS name.</summary>
        NAS_NAME = 137,

        /// <summary>ITK-Tunnel-Prot (Type 138). Integer. Tunnel protocol type.</summary>
        TUNNEL_PROT = 138,

        /// <summary>ITK-Acct-Serv-IP (Type 139). String. Accounting server IP address.</summary>
        ACCT_SERV_IP = 139,

        /// <summary>ITK-Acct-Serv-Prot (Type 140). Integer. Accounting server protocol.</summary>
        ACCT_SERV_PROT = 140,

        /// <summary>ITK-Filter-Rule (Type 141). String. Filter rule string.</summary>
        FILTER_RULE = 141,

        /// <summary>ITK-Channel-Group (Type 142). Integer. Channel group number.</summary>
        CHANNEL_GROUP = 142,

        /// <summary>ITK-Modem-Name (Type 143). String. Modem name.</summary>
        MODEM_NAME = 143,

        /// <summary>ITK-Modem-Init-String (Type 144). String. Modem initialization string.</summary>
        MODEM_INIT_STRING = 144,

        /// <summary>ITK-PPP-Client-Assign-IP (Type 145). Integer. PPP client IP assignment flag.</summary>
        PPP_CLIENT_ASSIGN_IP = 145,

        /// <summary>ITK-PPP-Client-Server-Mode (Type 146). Integer. PPP client-server mode.</summary>
        PPP_CLIENT_SERVER_MODE = 146,

        /// <summary>ITK-Backup-Number (Type 147). String. Backup phone number.</summary>
        BACKUP_NUMBER = 147,

        /// <summary>ITK-Dialout-Type (Type 148). Integer. Dialout type.</summary>
        DIALOUT_TYPE = 148,

        /// <summary>ITK-ISDN-Prot (Type 149). Integer. ISDN protocol.</summary>
        ISDN_PROT = 149,

        /// <summary>ITK-Modem-Prot (Type 150). Integer. Modem protocol.</summary>
        MODEM_PROT = 150,

        /// <summary>ITK-PPP-Compression-Prot (Type 151). Integer. PPP compression protocol.</summary>
        PPP_COMPRESSION_PROT = 151,

        /// <summary>ITK-Username (Type 152). String. Username string.</summary>
        USERNAME = 152,

        /// <summary>ITK-Password (Type 153). String. Password string.</summary>
        PASSWORD = 153
    }

    /// <summary>
    /// ITK-Auth-Serv-Prot attribute values (Type 101).
    /// </summary>
    public enum ITK_AUTH_SERV_PROT
    {
        /// <summary>RADIUS authentication protocol.</summary>
        RADIUS = 1,

        /// <summary>Local authentication.</summary>
        LOCAL = 2,

        /// <summary>TACACS+ authentication protocol.</summary>
        TACACS_PLUS = 3
    }

    /// <summary>
    /// ITK-Tunnel-Prot attribute values (Type 138).
    /// </summary>
    public enum ITK_TUNNEL_PROT
    {
        /// <summary>PPTP tunnel protocol.</summary>
        PPTP = 1,

        /// <summary>L2F tunnel protocol.</summary>
        L2F = 2,

        /// <summary>L2TP tunnel protocol.</summary>
        L2TP = 3
    }

    /// <summary>
    /// ITK-Acct-Serv-Prot attribute values (Type 140).
    /// </summary>
    public enum ITK_ACCT_SERV_PROT
    {
        /// <summary>RADIUS accounting protocol.</summary>
        RADIUS = 1,

        /// <summary>Local accounting.</summary>
        LOCAL = 2,

        /// <summary>TACACS+ accounting protocol.</summary>
        TACACS_PLUS = 3
    }

    /// <summary>
    /// ITK-Dialout-Type attribute values (Type 148).
    /// </summary>
    public enum ITK_DIALOUT_TYPE
    {
        /// <summary>Dialout disabled.</summary>
        NONE = 0,

        /// <summary>Async dialout.</summary>
        ASYNC = 1,

        /// <summary>ISDN dialout.</summary>
        ISDN = 2
    }

    /// <summary>
    /// ITK-ISDN-Prot attribute values (Type 149).
    /// </summary>
    public enum ITK_ISDN_PROT
    {
        /// <summary>HDLC protocol.</summary>
        HDLC = 1,

        /// <summary>PPP protocol.</summary>
        PPP = 2,

        /// <summary>X.75 protocol.</summary>
        X75 = 3,

        /// <summary>V.110 protocol.</summary>
        V110 = 4,

        /// <summary>V.120 protocol.</summary>
        V120 = 5
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing ITK
    /// (IANA PEN 1195) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.itk</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ITK's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 1195</c>.
    /// </para>
    /// <para>
    /// These attributes are used by ITK telecommunications and VoIP equipment
    /// for RADIUS-based authentication/accounting server configuration, user
    /// group assignment, login banner and prompt customization, welcome/reject
    /// messages, channel binding and grouping, tunnel protocol selection, filter
    /// rules, modem configuration (name, init string, protocol), PPP settings
    /// (client IP assignment, client-server mode, compression), ISDN protocol
    /// selection, dialout configuration, and backup number assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(ItkAttributes.Usergroup("dialup-users"));
    /// packet.SetAttribute(ItkAttributes.AuthServProt(ITK_AUTH_SERV_PROT.RADIUS));
    /// packet.SetAttribute(ItkAttributes.TunnelProt(ITK_TUNNEL_PROT.L2TP));
    /// packet.SetAttribute(ItkAttributes.Banner("Welcome to ITK Network"));
    /// </code>
    /// </remarks>
    public static class ItkAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for ITK.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 1195;

        #region Integer Attributes

        /// <summary>
        /// Creates an ITK-Auth-Serv-Prot attribute (Type 101) with the specified protocol.
        /// </summary>
        /// <param name="value">The authentication server protocol. See <see cref="ITK_AUTH_SERV_PROT"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthServProt(ITK_AUTH_SERV_PROT value) => CreateInteger(ItkAttributeType.AUTH_SERV_PROT, (int)value);

        /// <summary>Creates an ITK-Provider-Id attribute (Type 102).</summary>
        /// <param name="value">The provider identifier.</param>
        public static VendorSpecificAttributes ProviderId(int value) => CreateInteger(ItkAttributeType.PROVIDER_ID, value);

        /// <summary>Creates an ITK-Start-Delay attribute (Type 136).</summary>
        /// <param name="value">The start delay in seconds.</param>
        public static VendorSpecificAttributes StartDelay(int value) => CreateInteger(ItkAttributeType.START_DELAY, value);

        /// <summary>
        /// Creates an ITK-Tunnel-Prot attribute (Type 138) with the specified protocol.
        /// </summary>
        /// <param name="value">The tunnel protocol type. See <see cref="ITK_TUNNEL_PROT"/>.</param>
        public static VendorSpecificAttributes TunnelProt(ITK_TUNNEL_PROT value) => CreateInteger(ItkAttributeType.TUNNEL_PROT, (int)value);

        /// <summary>
        /// Creates an ITK-Acct-Serv-Prot attribute (Type 140) with the specified protocol.
        /// </summary>
        /// <param name="value">The accounting server protocol. See <see cref="ITK_ACCT_SERV_PROT"/>.</param>
        public static VendorSpecificAttributes AcctServProt(ITK_ACCT_SERV_PROT value) => CreateInteger(ItkAttributeType.ACCT_SERV_PROT, (int)value);

        /// <summary>Creates an ITK-Channel-Group attribute (Type 142).</summary>
        /// <param name="value">The channel group number.</param>
        public static VendorSpecificAttributes ChannelGroup(int value) => CreateInteger(ItkAttributeType.CHANNEL_GROUP, value);

        /// <summary>Creates an ITK-PPP-Client-Assign-IP attribute (Type 145).</summary>
        /// <param name="value">The PPP client IP assignment flag.</param>
        public static VendorSpecificAttributes PppClientAssignIp(int value) => CreateInteger(ItkAttributeType.PPP_CLIENT_ASSIGN_IP, value);

        /// <summary>Creates an ITK-PPP-Client-Server-Mode attribute (Type 146).</summary>
        /// <param name="value">The PPP client-server mode.</param>
        public static VendorSpecificAttributes PppClientServerMode(int value) => CreateInteger(ItkAttributeType.PPP_CLIENT_SERVER_MODE, value);

        /// <summary>
        /// Creates an ITK-Dialout-Type attribute (Type 148) with the specified type.
        /// </summary>
        /// <param name="value">The dialout type. See <see cref="ITK_DIALOUT_TYPE"/>.</param>
        public static VendorSpecificAttributes DialoutType(ITK_DIALOUT_TYPE value) => CreateInteger(ItkAttributeType.DIALOUT_TYPE, (int)value);

        /// <summary>
        /// Creates an ITK-ISDN-Prot attribute (Type 149) with the specified protocol.
        /// </summary>
        /// <param name="value">The ISDN protocol. See <see cref="ITK_ISDN_PROT"/>.</param>
        public static VendorSpecificAttributes IsdnProt(ITK_ISDN_PROT value) => CreateInteger(ItkAttributeType.ISDN_PROT, (int)value);

        /// <summary>Creates an ITK-Modem-Prot attribute (Type 150).</summary>
        /// <param name="value">The modem protocol.</param>
        public static VendorSpecificAttributes ModemProt(int value) => CreateInteger(ItkAttributeType.MODEM_PROT, value);

        /// <summary>Creates an ITK-PPP-Compression-Prot attribute (Type 151).</summary>
        /// <param name="value">The PPP compression protocol.</param>
        public static VendorSpecificAttributes PppCompressionProt(int value) => CreateInteger(ItkAttributeType.PPP_COMPRESSION_PROT, value);

        #endregion

        #region String Attributes

        /// <summary>Creates an ITK-Auth-Serv-IP attribute (Type 100).</summary>
        /// <param name="value">The authentication server IP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AuthServIp(string value) => CreateString(ItkAttributeType.AUTH_SERV_IP, value);

        /// <summary>Creates an ITK-Usergroup attribute (Type 103).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Usergroup(string value) => CreateString(ItkAttributeType.USERGROUP, value);

        /// <summary>Creates an ITK-Banner attribute (Type 104).</summary>
        /// <param name="value">The login banner text. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Banner(string value) => CreateString(ItkAttributeType.BANNER, value);

        /// <summary>Creates an ITK-Username-Prompt attribute (Type 105).</summary>
        /// <param name="value">The username prompt text. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UsernamePrompt(string value) => CreateString(ItkAttributeType.USERNAME_PROMPT, value);

        /// <summary>Creates an ITK-Password-Prompt attribute (Type 106).</summary>
        /// <param name="value">The password prompt text. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PasswordPrompt(string value) => CreateString(ItkAttributeType.PASSWORD_PROMPT, value);

        /// <summary>Creates an ITK-Welcome-Message attribute (Type 107).</summary>
        /// <param name="value">The welcome message text. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes WelcomeMessage(string value) => CreateString(ItkAttributeType.WELCOME_MESSAGE, value);

        /// <summary>Creates an ITK-Reject-Message attribute (Type 108).</summary>
        /// <param name="value">The reject message text. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RejectMessage(string value) => CreateString(ItkAttributeType.REJECT_MESSAGE, value);

        /// <summary>Creates an ITK-Channel-Binding attribute (Type 135).</summary>
        /// <param name="value">The channel binding specification. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ChannelBinding(string value) => CreateString(ItkAttributeType.CHANNEL_BINDING, value);

        /// <summary>Creates an ITK-NAS-Name attribute (Type 137).</summary>
        /// <param name="value">The NAS name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NasName(string value) => CreateString(ItkAttributeType.NAS_NAME, value);

        /// <summary>Creates an ITK-Acct-Serv-IP attribute (Type 139).</summary>
        /// <param name="value">The accounting server IP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AcctServIp(string value) => CreateString(ItkAttributeType.ACCT_SERV_IP, value);

        /// <summary>Creates an ITK-Filter-Rule attribute (Type 141).</summary>
        /// <param name="value">The filter rule string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FilterRule(string value) => CreateString(ItkAttributeType.FILTER_RULE, value);

        /// <summary>Creates an ITK-Modem-Name attribute (Type 143).</summary>
        /// <param name="value">The modem name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemName(string value) => CreateString(ItkAttributeType.MODEM_NAME, value);

        /// <summary>Creates an ITK-Modem-Init-String attribute (Type 144).</summary>
        /// <param name="value">The modem initialization string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemInitString(string value) => CreateString(ItkAttributeType.MODEM_INIT_STRING, value);

        /// <summary>Creates an ITK-Backup-Number attribute (Type 147).</summary>
        /// <param name="value">The backup phone number. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes BackupNumber(string value) => CreateString(ItkAttributeType.BACKUP_NUMBER, value);

        /// <summary>Creates an ITK-Username attribute (Type 152).</summary>
        /// <param name="value">The username string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Username(string value) => CreateString(ItkAttributeType.USERNAME, value);

        /// <summary>Creates an ITK-Password attribute (Type 153).</summary>
        /// <param name="value">The password string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Password(string value) => CreateString(ItkAttributeType.PASSWORD, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(ItkAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(ItkAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}