using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an Aptis / Nortel CVX (IANA PEN 2637) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.aptis</c>.
    /// </summary>
    public enum AptisAttributeType : byte
    {
        /// <summary>CVX-Identification (Type 1). String. CVX system identification string.</summary>
        IDENTIFICATION = 1,

        /// <summary>CVX-VPOP-ID (Type 2). Integer. Virtual POP identifier.</summary>
        VPOP_ID = 2,

        /// <summary>CVX-SS7-Session-ID-Type (Type 3). Integer. SS7 session ID type.</summary>
        SS7_SESSION_ID_TYPE = 3,

        /// <summary>CVX-Radius-Redirect (Type 4). Integer. RADIUS redirect mode.</summary>
        RADIUS_REDIRECT = 4,

        /// <summary>CVX-Client-Assign-DNS (Type 5). Integer. Client-assigned DNS mode.</summary>
        CLIENT_ASSIGN_DNS = 5,

        /// <summary>CVX-Client-DNS-Pri (Type 6). IP address. Client primary DNS server address.</summary>
        CLIENT_DNS_PRI = 6,

        /// <summary>CVX-Client-DNS-Sec (Type 7). IP address. Client secondary DNS server address.</summary>
        CLIENT_DNS_SEC = 7,

        /// <summary>CVX-Multicast-Rate-Limit (Type 8). Integer. Multicast rate limit in bps.</summary>
        MULTICAST_RATE_LIMIT = 8,

        /// <summary>CVX-Multicast-Client (Type 9). Integer. Multicast client mode.</summary>
        MULTICAST_CLIENT = 9,

        /// <summary>CVX-Disconnect-Cause (Type 10). Integer. Session disconnect cause code.</summary>
        DISCONNECT_CAUSE = 10,

        /// <summary>CVX-Data-Rate (Type 11). Integer. Data rate in bps.</summary>
        DATA_RATE = 11,

        /// <summary>CVX-PreSession-Time (Type 12). Integer. Pre-session time in seconds.</summary>
        PRESESSION_TIME = 12,

        /// <summary>CVX-Assign-IP-Pool (Type 13). Integer. IP address pool assignment.</summary>
        ASSIGN_IP_POOL = 13,

        /// <summary>CVX-Maximum-Channels (Type 14). Integer. Maximum channels allowed.</summary>
        MAXIMUM_CHANNELS = 14,

        /// <summary>CVX-Data-Filter (Type 15). String. Data filter name.</summary>
        DATA_FILTER = 15,

        /// <summary>CVX-Idle-Timeout (Type 16). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 16,

        /// <summary>CVX-Called-Number-Filter (Type 17). String. Called number filter string.</summary>
        CALLED_NUMBER_FILTER = 17,

        /// <summary>CVX-Calling-Number-Filter (Type 18). String. Calling number filter string.</summary>
        CALLING_NUMBER_FILTER = 18,

        /// <summary>CVX-Modem-Type-Override (Type 19). Integer. Modem type override.</summary>
        MODEM_TYPE_OVERRIDE = 19,

        /// <summary>CVX-Link-Speed (Type 20). Integer. Link speed in bps.</summary>
        LINK_SPEED = 20,

        /// <summary>CVX-Required-Beginning-Of-Call (Type 21). Integer. Required at beginning of call.</summary>
        REQUIRED_BEGINNING_OF_CALL = 21,

        /// <summary>CVX-Login-Username (Type 22). String. Login username string.</summary>
        LOGIN_USERNAME = 22,

        /// <summary>CVX-Login-Password (Type 23). String. Login password string.</summary>
        LOGIN_PASSWORD = 23,

        /// <summary>CVX-Protocol-Log (Type 24). String. Protocol log string.</summary>
        PROTOCOL_LOG = 24,

        /// <summary>CVX-DNIS-Number (Type 25). String. Dialed Number Identification Service number.</summary>
        DNIS_NUMBER = 25,

        /// <summary>CVX-PPP-Outgoing-Address (Type 26). IP address. PPP outgoing peer address.</summary>
        PPP_OUTGOING_ADDRESS = 26,

        /// <summary>CVX-Primary-Home-Agent (Type 27). String. Primary Mobile IP home agent.</summary>
        PRIMARY_HOME_AGENT = 27,

        /// <summary>CVX-Secondary-Home-Agent (Type 28). String. Secondary Mobile IP home agent.</summary>
        SECONDARY_HOME_AGENT = 28,

        /// <summary>CVX-Multilink-Match-Info (Type 29). Integer. Multilink match information.</summary>
        MULTILINK_MATCH_INFO = 29,

        /// <summary>CVX-Multilink-Group-Number (Type 30). Integer. Multilink group number.</summary>
        MULTILINK_GROUP_NUMBER = 30,

        /// <summary>CVX-PPP-Log (Type 31). String. PPP session log string.</summary>
        PPP_LOG = 31,

        /// <summary>CVX-IPSEC-Log (Type 32). String. IPsec session log string.</summary>
        IPSEC_LOG = 32,

        /// <summary>CVX-Modem-Begin-Modulation (Type 33). String. Modem begin modulation parameters.</summary>
        MODEM_BEGIN_MODULATION = 33,

        /// <summary>CVX-Modem-End-Modulation (Type 34). String. Modem end modulation parameters.</summary>
        MODEM_END_MODULATION = 34,

        /// <summary>CVX-Modem-Error-Correction (Type 35). String. Modem error correction parameters.</summary>
        MODEM_ERROR_CORRECTION = 35,

        /// <summary>CVX-Modem-Data-Compression (Type 36). String. Modem data compression parameters.</summary>
        MODEM_DATA_COMPRESSION = 36,

        /// <summary>CVX-Modem-Tx-Packets (Type 37). Integer. Modem transmitted packets count.</summary>
        MODEM_TX_PACKETS = 37,

        /// <summary>CVX-Modem-Rx-Packets (Type 38). Integer. Modem received packets count.</summary>
        MODEM_RX_PACKETS = 38,

        /// <summary>CVX-Modem-SNR (Type 39). Integer. Modem signal-to-noise ratio.</summary>
        MODEM_SNR = 39,

        /// <summary>CVX-Modem-Local-Retrains (Type 40). Integer. Modem local retrain count.</summary>
        MODEM_LOCAL_RETRAINS = 40,

        /// <summary>CVX-Modem-Remote-Retrains (Type 41). Integer. Modem remote retrain count.</summary>
        MODEM_REMOTE_RETRAINS = 41,

        /// <summary>CVX-Modem-Local-Rate-Negs (Type 42). Integer. Modem local rate renegotiations.</summary>
        MODEM_LOCAL_RATE_NEGS = 42,

        /// <summary>CVX-Modem-Remote-Rate-Negs (Type 43). Integer. Modem remote rate renegotiations.</summary>
        MODEM_REMOTE_RATE_NEGS = 43,

        /// <summary>CVX-Modem-Begin-Recv-Line-Level (Type 44). Integer. Modem begin receive line level.</summary>
        MODEM_BEGIN_RECV_LINE_LEVEL = 44,

        /// <summary>CVX-Modem-End-Recv-Line-Level (Type 45). Integer. Modem end receive line level.</summary>
        MODEM_END_RECV_LINE_LEVEL = 45
    }

    /// <summary>
    /// CVX-Radius-Redirect attribute values (Type 4).
    /// </summary>
    public enum CVX_RADIUS_REDIRECT
    {
        /// <summary>No redirection.</summary>
        NO_REDIRECT = 0,

        /// <summary>Redirect RADIUS requests.</summary>
        REDIRECT = 1
    }

    /// <summary>
    /// CVX-Client-Assign-DNS attribute values (Type 5).
    /// </summary>
    public enum CVX_CLIENT_ASSIGN_DNS
    {
        /// <summary>Do not assign DNS to client.</summary>
        DISABLED = 0,

        /// <summary>Assign DNS to client.</summary>
        ENABLED = 1
    }

    /// <summary>
    /// CVX-Multicast-Client attribute values (Type 9).
    /// </summary>
    public enum CVX_MULTICAST_CLIENT
    {
        /// <summary>Multicast client disabled.</summary>
        DISABLED = 0,

        /// <summary>Multicast client enabled.</summary>
        ENABLED = 1
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Aptis / Nortel CVX
    /// (IANA PEN 2637) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.aptis</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Aptis's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2637</c>.
    /// </para>
    /// <para>
    /// These attributes are used by the Nortel CVX (formerly Aptis Communications)
    /// access concentrators for PPP session management, RADIUS proxying, DNS
    /// assignment, multicast control, modem diagnostics, channel configuration,
    /// and session disconnect cause reporting.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AptisAttributes.VpopId(100));
    /// packet.SetAttribute(AptisAttributes.ClientAssignDns(CVX_CLIENT_ASSIGN_DNS.ENABLED));
    /// packet.SetAttribute(AptisAttributes.ClientDnsPri(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(AptisAttributes.MaximumChannels(2));
    /// </code>
    /// </remarks>
    public static class AptisAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Aptis Communications (Nortel CVX).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2637;

        #region Integer Attributes

        /// <summary>
        /// Creates a CVX-VPOP-ID attribute (Type 2) with the specified identifier.
        /// </summary>
        /// <param name="value">The Virtual POP identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VpopId(int value)
        {
            return CreateInteger(AptisAttributeType.VPOP_ID, value);
        }

        /// <summary>
        /// Creates a CVX-SS7-Session-ID-Type attribute (Type 3) with the specified type.
        /// </summary>
        /// <param name="value">The SS7 session ID type.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Ss7SessionIdType(int value)
        {
            return CreateInteger(AptisAttributeType.SS7_SESSION_ID_TYPE, value);
        }

        /// <summary>
        /// Creates a CVX-Radius-Redirect attribute (Type 4) with the specified mode.
        /// </summary>
        /// <param name="value">The RADIUS redirect mode. See <see cref="CVX_RADIUS_REDIRECT"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RadiusRedirect(CVX_RADIUS_REDIRECT value)
        {
            return CreateInteger(AptisAttributeType.RADIUS_REDIRECT, (int)value);
        }

        /// <summary>
        /// Creates a CVX-Client-Assign-DNS attribute (Type 5) with the specified mode.
        /// </summary>
        /// <param name="value">The client-assigned DNS mode. See <see cref="CVX_CLIENT_ASSIGN_DNS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ClientAssignDns(CVX_CLIENT_ASSIGN_DNS value)
        {
            return CreateInteger(AptisAttributeType.CLIENT_ASSIGN_DNS, (int)value);
        }

        /// <summary>
        /// Creates a CVX-Multicast-Rate-Limit attribute (Type 8) with the specified rate.
        /// </summary>
        /// <param name="value">The multicast rate limit in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MulticastRateLimit(int value)
        {
            return CreateInteger(AptisAttributeType.MULTICAST_RATE_LIMIT, value);
        }

        /// <summary>
        /// Creates a CVX-Multicast-Client attribute (Type 9) with the specified mode.
        /// </summary>
        /// <param name="value">The multicast client mode. See <see cref="CVX_MULTICAST_CLIENT"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MulticastClient(CVX_MULTICAST_CLIENT value)
        {
            return CreateInteger(AptisAttributeType.MULTICAST_CLIENT, (int)value);
        }

        /// <summary>
        /// Creates a CVX-Disconnect-Cause attribute (Type 10) with the specified cause code.
        /// </summary>
        /// <param name="value">The session disconnect cause code.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DisconnectCause(int value)
        {
            return CreateInteger(AptisAttributeType.DISCONNECT_CAUSE, value);
        }

        /// <summary>
        /// Creates a CVX-Data-Rate attribute (Type 11) with the specified rate.
        /// </summary>
        /// <param name="value">The data rate in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DataRate(int value)
        {
            return CreateInteger(AptisAttributeType.DATA_RATE, value);
        }

        /// <summary>
        /// Creates a CVX-PreSession-Time attribute (Type 12) with the specified time.
        /// </summary>
        /// <param name="value">The pre-session time in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes PreSessionTime(int value)
        {
            return CreateInteger(AptisAttributeType.PRESESSION_TIME, value);
        }

        /// <summary>
        /// Creates a CVX-Assign-IP-Pool attribute (Type 13) with the specified pool.
        /// </summary>
        /// <param name="value">The IP address pool assignment.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AssignIpPool(int value)
        {
            return CreateInteger(AptisAttributeType.ASSIGN_IP_POOL, value);
        }

        /// <summary>
        /// Creates a CVX-Maximum-Channels attribute (Type 14) with the specified maximum.
        /// </summary>
        /// <param name="value">The maximum channels allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaximumChannels(int value)
        {
            return CreateInteger(AptisAttributeType.MAXIMUM_CHANNELS, value);
        }

        /// <summary>
        /// Creates a CVX-Idle-Timeout attribute (Type 16) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(AptisAttributeType.IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Type-Override attribute (Type 19) with the specified type.
        /// </summary>
        /// <param name="value">The modem type override.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ModemTypeOverride(int value)
        {
            return CreateInteger(AptisAttributeType.MODEM_TYPE_OVERRIDE, value);
        }

        /// <summary>
        /// Creates a CVX-Link-Speed attribute (Type 20) with the specified speed.
        /// </summary>
        /// <param name="value">The link speed in bps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes LinkSpeed(int value)
        {
            return CreateInteger(AptisAttributeType.LINK_SPEED, value);
        }

        /// <summary>
        /// Creates a CVX-Required-Beginning-Of-Call attribute (Type 21) with the specified value.
        /// </summary>
        /// <param name="value">The required beginning-of-call value.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes RequiredBeginningOfCall(int value)
        {
            return CreateInteger(AptisAttributeType.REQUIRED_BEGINNING_OF_CALL, value);
        }

        /// <summary>
        /// Creates a CVX-Multilink-Match-Info attribute (Type 29) with the specified value.
        /// </summary>
        /// <param name="value">The multilink match information.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MultilinkMatchInfo(int value)
        {
            return CreateInteger(AptisAttributeType.MULTILINK_MATCH_INFO, value);
        }

        /// <summary>
        /// Creates a CVX-Multilink-Group-Number attribute (Type 30) with the specified group number.
        /// </summary>
        /// <param name="value">The multilink group number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MultilinkGroupNumber(int value)
        {
            return CreateInteger(AptisAttributeType.MULTILINK_GROUP_NUMBER, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Tx-Packets attribute (Type 37) with the specified count.
        /// </summary>
        /// <param name="value">The modem transmitted packets count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ModemTxPackets(int value)
        {
            return CreateInteger(AptisAttributeType.MODEM_TX_PACKETS, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Rx-Packets attribute (Type 38) with the specified count.
        /// </summary>
        /// <param name="value">The modem received packets count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ModemRxPackets(int value)
        {
            return CreateInteger(AptisAttributeType.MODEM_RX_PACKETS, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-SNR attribute (Type 39) with the specified SNR.
        /// </summary>
        /// <param name="value">The modem signal-to-noise ratio.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ModemSnr(int value)
        {
            return CreateInteger(AptisAttributeType.MODEM_SNR, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Local-Retrains attribute (Type 40) with the specified count.
        /// </summary>
        /// <param name="value">The modem local retrain count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ModemLocalRetrains(int value)
        {
            return CreateInteger(AptisAttributeType.MODEM_LOCAL_RETRAINS, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Remote-Retrains attribute (Type 41) with the specified count.
        /// </summary>
        /// <param name="value">The modem remote retrain count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ModemRemoteRetrains(int value)
        {
            return CreateInteger(AptisAttributeType.MODEM_REMOTE_RETRAINS, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Local-Rate-Negs attribute (Type 42) with the specified count.
        /// </summary>
        /// <param name="value">The modem local rate renegotiations count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ModemLocalRateNegs(int value)
        {
            return CreateInteger(AptisAttributeType.MODEM_LOCAL_RATE_NEGS, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Remote-Rate-Negs attribute (Type 43) with the specified count.
        /// </summary>
        /// <param name="value">The modem remote rate renegotiations count.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ModemRemoteRateNegs(int value)
        {
            return CreateInteger(AptisAttributeType.MODEM_REMOTE_RATE_NEGS, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Begin-Recv-Line-Level attribute (Type 44) with the specified level.
        /// </summary>
        /// <param name="value">The modem begin receive line level.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ModemBeginRecvLineLevel(int value)
        {
            return CreateInteger(AptisAttributeType.MODEM_BEGIN_RECV_LINE_LEVEL, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-End-Recv-Line-Level attribute (Type 45) with the specified level.
        /// </summary>
        /// <param name="value">The modem end receive line level.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes ModemEndRecvLineLevel(int value)
        {
            return CreateInteger(AptisAttributeType.MODEM_END_RECV_LINE_LEVEL, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a CVX-Identification attribute (Type 1) with the specified identification string.
        /// </summary>
        /// <param name="value">The CVX system identification string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Identification(string value)
        {
            return CreateString(AptisAttributeType.IDENTIFICATION, value);
        }

        /// <summary>
        /// Creates a CVX-Data-Filter attribute (Type 15) with the specified filter name.
        /// </summary>
        /// <param name="value">The data filter name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DataFilter(string value)
        {
            return CreateString(AptisAttributeType.DATA_FILTER, value);
        }

        /// <summary>
        /// Creates a CVX-Called-Number-Filter attribute (Type 17) with the specified filter.
        /// </summary>
        /// <param name="value">The called number filter string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalledNumberFilter(string value)
        {
            return CreateString(AptisAttributeType.CALLED_NUMBER_FILTER, value);
        }

        /// <summary>
        /// Creates a CVX-Calling-Number-Filter attribute (Type 18) with the specified filter.
        /// </summary>
        /// <param name="value">The calling number filter string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallingNumberFilter(string value)
        {
            return CreateString(AptisAttributeType.CALLING_NUMBER_FILTER, value);
        }

        /// <summary>
        /// Creates a CVX-Login-Username attribute (Type 22) with the specified username.
        /// </summary>
        /// <param name="value">The login username. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LoginUsername(string value)
        {
            return CreateString(AptisAttributeType.LOGIN_USERNAME, value);
        }

        /// <summary>
        /// Creates a CVX-Login-Password attribute (Type 23) with the specified password.
        /// </summary>
        /// <param name="value">The login password. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes LoginPassword(string value)
        {
            return CreateString(AptisAttributeType.LOGIN_PASSWORD, value);
        }

        /// <summary>
        /// Creates a CVX-Protocol-Log attribute (Type 24) with the specified log string.
        /// </summary>
        /// <param name="value">The protocol log string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProtocolLog(string value)
        {
            return CreateString(AptisAttributeType.PROTOCOL_LOG, value);
        }

        /// <summary>
        /// Creates a CVX-DNIS-Number attribute (Type 25) with the specified DNIS.
        /// </summary>
        /// <param name="value">The Dialed Number Identification Service number. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DnisNumber(string value)
        {
            return CreateString(AptisAttributeType.DNIS_NUMBER, value);
        }

        /// <summary>
        /// Creates a CVX-Primary-Home-Agent attribute (Type 27) with the specified agent.
        /// </summary>
        /// <param name="value">The primary Mobile IP home agent. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrimaryHomeAgent(string value)
        {
            return CreateString(AptisAttributeType.PRIMARY_HOME_AGENT, value);
        }

        /// <summary>
        /// Creates a CVX-Secondary-Home-Agent attribute (Type 28) with the specified agent.
        /// </summary>
        /// <param name="value">The secondary Mobile IP home agent. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SecondaryHomeAgent(string value)
        {
            return CreateString(AptisAttributeType.SECONDARY_HOME_AGENT, value);
        }

        /// <summary>
        /// Creates a CVX-PPP-Log attribute (Type 31) with the specified log string.
        /// </summary>
        /// <param name="value">The PPP session log string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PppLog(string value)
        {
            return CreateString(AptisAttributeType.PPP_LOG, value);
        }

        /// <summary>
        /// Creates a CVX-IPSEC-Log attribute (Type 32) with the specified log string.
        /// </summary>
        /// <param name="value">The IPsec session log string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpsecLog(string value)
        {
            return CreateString(AptisAttributeType.IPSEC_LOG, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Begin-Modulation attribute (Type 33) with the specified parameters.
        /// </summary>
        /// <param name="value">The modem begin modulation parameters. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemBeginModulation(string value)
        {
            return CreateString(AptisAttributeType.MODEM_BEGIN_MODULATION, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-End-Modulation attribute (Type 34) with the specified parameters.
        /// </summary>
        /// <param name="value">The modem end modulation parameters. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemEndModulation(string value)
        {
            return CreateString(AptisAttributeType.MODEM_END_MODULATION, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Error-Correction attribute (Type 35) with the specified parameters.
        /// </summary>
        /// <param name="value">The modem error correction parameters. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemErrorCorrection(string value)
        {
            return CreateString(AptisAttributeType.MODEM_ERROR_CORRECTION, value);
        }

        /// <summary>
        /// Creates a CVX-Modem-Data-Compression attribute (Type 36) with the specified parameters.
        /// </summary>
        /// <param name="value">The modem data compression parameters. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ModemDataCompression(string value)
        {
            return CreateString(AptisAttributeType.MODEM_DATA_COMPRESSION, value);
        }

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates a CVX-Client-DNS-Pri attribute (Type 6) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The client primary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientDnsPri(IPAddress value)
        {
            return CreateIpv4(AptisAttributeType.CLIENT_DNS_PRI, value);
        }

        /// <summary>
        /// Creates a CVX-Client-DNS-Sec attribute (Type 7) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The client secondary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ClientDnsSec(IPAddress value)
        {
            return CreateIpv4(AptisAttributeType.CLIENT_DNS_SEC, value);
        }

        /// <summary>
        /// Creates a CVX-PPP-Outgoing-Address attribute (Type 26) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The PPP outgoing peer address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PppOutgoingAddress(IPAddress value)
        {
            return CreateIpv4(AptisAttributeType.PPP_OUTGOING_ADDRESS, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Aptis attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AptisAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Aptis attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AptisAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified Aptis attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(AptisAttributeType type, IPAddress value)
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