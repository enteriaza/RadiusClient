using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Livingston Enterprises (IANA PEN 307) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.livingston</c>.
    /// </summary>
    /// <remarks>
    /// Livingston Enterprises was a pioneer in remote access equipment and one of
    /// the original developers of the RADIUS protocol (RFC 2058/2138/2865).
    /// Livingston was acquired by Lucent Technologies. These attributes are used
    /// by Livingston PortMaster and related NAS equipment.
    /// </remarks>
    public enum LivingstonAttributeType : byte
    {
        /// <summary>LE-Terminate-Detail (Type 2). String. Session termination detail.</summary>
        TERMINATE_DETAIL = 2,

        /// <summary>LE-Advice-of-Charge (Type 3). String. Advice of charge information.</summary>
        ADVICE_OF_CHARGE = 3,

        /// <summary>LE-Connect-Detail (Type 4). String. Connection detail information.</summary>
        CONNECT_DETAIL = 4,

        /// <summary>LE-IP-Pool (Type 6). Integer. IP address pool number.</summary>
        IP_POOL = 6,

        /// <summary>LE-IP-Gateway (Type 7). IP address. IP gateway address.</summary>
        IP_GATEWAY = 7,

        /// <summary>LE-Modem-Begin-Modulation (Type 8). String. Modem begin modulation.</summary>
        MODEM_BEGIN_MODULATION = 8,

        /// <summary>LE-Modem-Error-Correction (Type 9). String. Modem error correction type.</summary>
        MODEM_ERROR_CORRECTION = 9,

        /// <summary>LE-Modem-Data-Compression (Type 10). String. Modem data compression type.</summary>
        MODEM_DATA_COMPRESSION = 10,

        /// <summary>LE-Modem-Transmit-Speed (Type 11). Integer. Modem transmit speed in bps.</summary>
        MODEM_TRANSMIT_SPEED = 11,

        /// <summary>LE-Modem-Receive-Speed (Type 12). Integer. Modem receive speed in bps.</summary>
        MODEM_RECEIVE_SPEED = 12,

        /// <summary>LE-Admin-Access (Type 14). Integer. Administrative access level.</summary>
        ADMIN_ACCESS = 14,

        /// <summary>LE-Modem-End-Modulation (Type 15). String. Modem end modulation.</summary>
        MODEM_END_MODULATION = 15,

        /// <summary>LE-Modem-Quality (Type 16). String. Modem quality information.</summary>
        MODEM_QUALITY = 16,

        /// <summary>LE-Modem-Retrain-Req-Sent (Type 17). Integer. Modem retrain requests sent.</summary>
        MODEM_RETRAIN_REQ_SENT = 17,

        /// <summary>LE-Modem-Retrain-Req-Rcvd (Type 18). Integer. Modem retrain requests received.</summary>
        MODEM_RETRAIN_REQ_RCVD = 18,

        /// <summary>LE-Modem-Retrain-Succeeded (Type 19). Integer. Modem retrain succeeded count.</summary>
        MODEM_RETRAIN_SUCCEEDED = 19,

        /// <summary>LE-Modem-Speed-Shifts (Type 20). Integer. Modem speed shift count.</summary>
        MODEM_SPEED_SHIFTS = 20,

        /// <summary>LE-Modem-Local-Retrains (Type 21). Integer. Modem local retrain count.</summary>
        MODEM_LOCAL_RETRAINS = 21,

        /// <summary>LE-Modem-Remote-Retrains (Type 22). Integer. Modem remote retrain count.</summary>
        MODEM_REMOTE_RETRAINS = 22,

        /// <summary>LE-Modem-Local-Rate-Negs (Type 23). Integer. Modem local rate negotiation count.</summary>
        MODEM_LOCAL_RATE_NEGS = 23,

        /// <summary>LE-Modem-Remote-Rate-Negs (Type 24). Integer. Modem remote rate negotiation count.</summary>
        MODEM_REMOTE_RATE_NEGS = 24,

        /// <summary>LE-Modem-Begin-Recv-Line-Lvl (Type 25). Integer. Modem begin receive line level.</summary>
        MODEM_BEGIN_RECV_LINE_LVL = 25,

        /// <summary>LE-Modem-End-Recv-Line-Lvl (Type 26). Integer. Modem end receive line level.</summary>
        MODEM_END_RECV_LINE_LVL = 26,

        /// <summary>LE-NAT-TCP-Session-Timeout (Type 27). Integer. NAT TCP session timeout in seconds.</summary>
        NAT_TCP_SESSION_TIMEOUT = 27,

        /// <summary>LE-NAT-Other-Session-Timeout (Type 28). Integer. NAT other session timeout in seconds.</summary>
        NAT_OTHER_SESSION_TIMEOUT = 28,

        /// <summary>LE-NAT-Log-Pref (Type 29). Integer. NAT logging preference.</summary>
        NAT_LOG_PREF = 29,

        /// <summary>LE-NAT-Sess-Dir-Fail-Action (Type 30). Integer. NAT session direct fail action.</summary>
        NAT_SESS_DIR_FAIL_ACTION = 30,

        /// <summary>LE-NAT-Inmap (Type 31). String. NAT inbound map.</summary>
        NAT_INMAP = 31,

        /// <summary>LE-NAT-Outmap (Type 32). String. NAT outbound map.</summary>
        NAT_OUTMAP = 32,

        /// <summary>LE-NAT-Default-Not-Logged (Type 33). Integer. NAT default not logged flag.</summary>
        NAT_DEFAULT_NOT_LOGGED = 33,

        /// <summary>LE-Multicast-Client (Type 34). Integer. Multicast client flag.</summary>
        MULTICAST_CLIENT = 34
    }

    /// <summary>
    /// LE-Admin-Access attribute values (Type 14).
    /// </summary>
    public enum LE_ADMIN_ACCESS
    {
        /// <summary>No administrative access.</summary>
        NONE = 0,

        /// <summary>Read-only access.</summary>
        READ_ONLY = 1,

        /// <summary>Read-write access.</summary>
        READ_WRITE = 2,

        /// <summary>Disabled access.</summary>
        DISABLED = 3
    }

    /// <summary>
    /// LE-NAT-Log-Pref attribute values (Type 29).
    /// </summary>
    public enum LE_NAT_LOG_PREF
    {
        /// <summary>NAT logging disabled.</summary>
        DISABLED = 0,

        /// <summary>NAT logging enabled.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// LE-NAT-Sess-Dir-Fail-Action attribute values (Type 30).
    /// </summary>
    public enum LE_NAT_SESS_DIR_FAIL_ACTION
    {
        /// <summary>Drop packet on failure.</summary>
        DROP = 0,

        /// <summary>Forward packet as-is on failure.</summary>
        FORWARD = 1
    }

    /// <summary>
    /// LE-Multicast-Client attribute values (Type 34).
    /// </summary>
    public enum LE_MULTICAST_CLIENT
    {
        /// <summary>Multicast client disabled.</summary>
        DISABLED = 0,

        /// <summary>Multicast client enabled.</summary>
        ENABLED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Livingston Enterprises
    /// (IANA PEN 307) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.livingston</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Livingston's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 307</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Livingston Enterprises (Lucent) PortMaster
    /// NAS equipment for RADIUS-based session termination and connection detail
    /// reporting, advice of charge, IP address pool and gateway assignment,
    /// modem diagnostics (modulation, error correction, compression, speeds,
    /// retrains, rate negotiations, line levels), administrative access control,
    /// NAT configuration (TCP/other session timeouts, logging, inbound/outbound
    /// maps, fail actions), and multicast client control.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(LivingstonAttributes.AdminAccess(LE_ADMIN_ACCESS.READ_WRITE));
    /// packet.SetAttribute(LivingstonAttributes.IpPool(2));
    /// packet.SetAttribute(LivingstonAttributes.IpGateway(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(LivingstonAttributes.NatTcpSessionTimeout(3600));
    /// packet.SetAttribute(LivingstonAttributes.MulticastClient(LE_MULTICAST_CLIENT.ENABLED));
    /// </code>
    /// </remarks>
    public static class LivingstonAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Livingston Enterprises.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 307;

        #region Integer Attributes

        /// <summary>Creates an LE-IP-Pool attribute (Type 6).</summary>
        /// <param name="value">The IP address pool number.</param>
        public static VendorSpecificAttributes IpPool(int value) => CreateInteger(LivingstonAttributeType.IP_POOL, value);

        /// <summary>Creates an LE-Modem-Transmit-Speed attribute (Type 11).</summary>
        /// <param name="value">The modem transmit speed in bps.</param>
        public static VendorSpecificAttributes ModemTransmitSpeed(int value) => CreateInteger(LivingstonAttributeType.MODEM_TRANSMIT_SPEED, value);

        /// <summary>Creates an LE-Modem-Receive-Speed attribute (Type 12).</summary>
        /// <param name="value">The modem receive speed in bps.</param>
        public static VendorSpecificAttributes ModemReceiveSpeed(int value) => CreateInteger(LivingstonAttributeType.MODEM_RECEIVE_SPEED, value);

        /// <summary>Creates an LE-Admin-Access attribute (Type 14).</summary>
        /// <param name="value">The administrative access level. See <see cref="LE_ADMIN_ACCESS"/>.</param>
        public static VendorSpecificAttributes AdminAccess(LE_ADMIN_ACCESS value) => CreateInteger(LivingstonAttributeType.ADMIN_ACCESS, (int)value);

        /// <summary>Creates an LE-Modem-Retrain-Req-Sent attribute (Type 17).</summary>
        public static VendorSpecificAttributes ModemRetrainReqSent(int value) => CreateInteger(LivingstonAttributeType.MODEM_RETRAIN_REQ_SENT, value);

        /// <summary>Creates an LE-Modem-Retrain-Req-Rcvd attribute (Type 18).</summary>
        public static VendorSpecificAttributes ModemRetrainReqRcvd(int value) => CreateInteger(LivingstonAttributeType.MODEM_RETRAIN_REQ_RCVD, value);

        /// <summary>Creates an LE-Modem-Retrain-Succeeded attribute (Type 19).</summary>
        public static VendorSpecificAttributes ModemRetrainSucceeded(int value) => CreateInteger(LivingstonAttributeType.MODEM_RETRAIN_SUCCEEDED, value);

        /// <summary>Creates an LE-Modem-Speed-Shifts attribute (Type 20).</summary>
        public static VendorSpecificAttributes ModemSpeedShifts(int value) => CreateInteger(LivingstonAttributeType.MODEM_SPEED_SHIFTS, value);

        /// <summary>Creates an LE-Modem-Local-Retrains attribute (Type 21).</summary>
        public static VendorSpecificAttributes ModemLocalRetrains(int value) => CreateInteger(LivingstonAttributeType.MODEM_LOCAL_RETRAINS, value);

        /// <summary>Creates an LE-Modem-Remote-Retrains attribute (Type 22).</summary>
        public static VendorSpecificAttributes ModemRemoteRetrains(int value) => CreateInteger(LivingstonAttributeType.MODEM_REMOTE_RETRAINS, value);

        /// <summary>Creates an LE-Modem-Local-Rate-Negs attribute (Type 23).</summary>
        public static VendorSpecificAttributes ModemLocalRateNegs(int value) => CreateInteger(LivingstonAttributeType.MODEM_LOCAL_RATE_NEGS, value);

        /// <summary>Creates an LE-Modem-Remote-Rate-Negs attribute (Type 24).</summary>
        public static VendorSpecificAttributes ModemRemoteRateNegs(int value) => CreateInteger(LivingstonAttributeType.MODEM_REMOTE_RATE_NEGS, value);

        /// <summary>Creates an LE-Modem-Begin-Recv-Line-Lvl attribute (Type 25).</summary>
        public static VendorSpecificAttributes ModemBeginRecvLineLvl(int value) => CreateInteger(LivingstonAttributeType.MODEM_BEGIN_RECV_LINE_LVL, value);

        /// <summary>Creates an LE-Modem-End-Recv-Line-Lvl attribute (Type 26).</summary>
        public static VendorSpecificAttributes ModemEndRecvLineLvl(int value) => CreateInteger(LivingstonAttributeType.MODEM_END_RECV_LINE_LVL, value);

        /// <summary>Creates an LE-NAT-TCP-Session-Timeout attribute (Type 27).</summary>
        /// <param name="value">The NAT TCP session timeout in seconds.</param>
        public static VendorSpecificAttributes NatTcpSessionTimeout(int value) => CreateInteger(LivingstonAttributeType.NAT_TCP_SESSION_TIMEOUT, value);

        /// <summary>Creates an LE-NAT-Other-Session-Timeout attribute (Type 28).</summary>
        /// <param name="value">The NAT other session timeout in seconds.</param>
        public static VendorSpecificAttributes NatOtherSessionTimeout(int value) => CreateInteger(LivingstonAttributeType.NAT_OTHER_SESSION_TIMEOUT, value);

        /// <summary>Creates an LE-NAT-Log-Pref attribute (Type 29).</summary>
        /// <param name="value">The NAT logging preference. See <see cref="LE_NAT_LOG_PREF"/>.</param>
        public static VendorSpecificAttributes NatLogPref(LE_NAT_LOG_PREF value) => CreateInteger(LivingstonAttributeType.NAT_LOG_PREF, (int)value);

        /// <summary>Creates an LE-NAT-Sess-Dir-Fail-Action attribute (Type 30).</summary>
        /// <param name="value">The NAT session direct fail action. See <see cref="LE_NAT_SESS_DIR_FAIL_ACTION"/>.</param>
        public static VendorSpecificAttributes NatSessDirFailAction(LE_NAT_SESS_DIR_FAIL_ACTION value) => CreateInteger(LivingstonAttributeType.NAT_SESS_DIR_FAIL_ACTION, (int)value);

        /// <summary>Creates an LE-NAT-Default-Not-Logged attribute (Type 33).</summary>
        public static VendorSpecificAttributes NatDefaultNotLogged(int value) => CreateInteger(LivingstonAttributeType.NAT_DEFAULT_NOT_LOGGED, value);

        /// <summary>Creates an LE-Multicast-Client attribute (Type 34).</summary>
        /// <param name="value">The multicast client flag. See <see cref="LE_MULTICAST_CLIENT"/>.</param>
        public static VendorSpecificAttributes MulticastClient(LE_MULTICAST_CLIENT value) => CreateInteger(LivingstonAttributeType.MULTICAST_CLIENT, (int)value);

        #endregion

        #region String Attributes

        /// <summary>Creates an LE-Terminate-Detail attribute (Type 2).</summary>
        /// <param name="value">The session termination detail. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TerminateDetail(string value) => CreateString(LivingstonAttributeType.TERMINATE_DETAIL, value);

        /// <summary>Creates an LE-Advice-of-Charge attribute (Type 3).</summary>
        /// <param name="value">The advice of charge information. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AdviceOfCharge(string value) => CreateString(LivingstonAttributeType.ADVICE_OF_CHARGE, value);

        /// <summary>Creates an LE-Connect-Detail attribute (Type 4).</summary>
        /// <param name="value">The connection detail information. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ConnectDetail(string value) => CreateString(LivingstonAttributeType.CONNECT_DETAIL, value);

        /// <summary>Creates an LE-Modem-Begin-Modulation attribute (Type 8).</summary>
        /// <param name="value">The modem begin modulation. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemBeginModulation(string value) => CreateString(LivingstonAttributeType.MODEM_BEGIN_MODULATION, value);

        /// <summary>Creates an LE-Modem-Error-Correction attribute (Type 9).</summary>
        /// <param name="value">The modem error correction type. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemErrorCorrection(string value) => CreateString(LivingstonAttributeType.MODEM_ERROR_CORRECTION, value);

        /// <summary>Creates an LE-Modem-Data-Compression attribute (Type 10).</summary>
        /// <param name="value">The modem data compression type. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemDataCompression(string value) => CreateString(LivingstonAttributeType.MODEM_DATA_COMPRESSION, value);

        /// <summary>Creates an LE-Modem-End-Modulation attribute (Type 15).</summary>
        /// <param name="value">The modem end modulation. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemEndModulation(string value) => CreateString(LivingstonAttributeType.MODEM_END_MODULATION, value);

        /// <summary>Creates an LE-Modem-Quality attribute (Type 16).</summary>
        /// <param name="value">The modem quality information. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemQuality(string value) => CreateString(LivingstonAttributeType.MODEM_QUALITY, value);

        /// <summary>Creates an LE-NAT-Inmap attribute (Type 31).</summary>
        /// <param name="value">The NAT inbound map. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatInmap(string value) => CreateString(LivingstonAttributeType.NAT_INMAP, value);

        /// <summary>Creates an LE-NAT-Outmap attribute (Type 32).</summary>
        /// <param name="value">The NAT outbound map. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatOutmap(string value) => CreateString(LivingstonAttributeType.NAT_OUTMAP, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an LE-IP-Gateway attribute (Type 7) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The IP gateway address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes IpGateway(IPAddress value)
        {
            return CreateIpv4(LivingstonAttributeType.IP_GATEWAY, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(LivingstonAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(LivingstonAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(LivingstonAttributeType type, IPAddress value)
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