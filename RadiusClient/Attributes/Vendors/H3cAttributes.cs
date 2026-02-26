using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an H3C / New H3C (IANA PEN 25506) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.h3c</c>.
    /// </summary>
    /// <remarks>
    /// H3C (Huawei-3Com) was originally a joint venture between Huawei and 3Com.
    /// Now known as New H3C Technologies, it produces enterprise networking equipment
    /// including switches, routers, and wireless controllers.
    /// </remarks>
    public enum H3cAttributeType : byte
    {
        /// <summary>H3C-Input-Peak-Rate (Type 1). Integer. Input peak rate in bps.</summary>
        INPUT_PEAK_RATE = 1,

        /// <summary>H3C-Input-Average-Rate (Type 2). Integer. Input average rate in bps.</summary>
        INPUT_AVERAGE_RATE = 2,

        /// <summary>H3C-Input-Basic-Rate (Type 3). Integer. Input basic rate in bps.</summary>
        INPUT_BASIC_RATE = 3,

        /// <summary>H3C-Output-Peak-Rate (Type 4). Integer. Output peak rate in bps.</summary>
        OUTPUT_PEAK_RATE = 4,

        /// <summary>H3C-Output-Average-Rate (Type 5). Integer. Output average rate in bps.</summary>
        OUTPUT_AVERAGE_RATE = 5,

        /// <summary>H3C-Output-Basic-Rate (Type 6). Integer. Output basic rate in bps.</summary>
        OUTPUT_BASIC_RATE = 6,

        /// <summary>H3C-Remanent-Volume (Type 15). Integer. Remanent (remaining) data volume in KB.</summary>
        REMANENT_VOLUME = 15,

        /// <summary>H3C-Command (Type 20). Integer. Command authorization level.</summary>
        COMMAND = 20,

        /// <summary>H3C-Control-Identifier (Type 24). Integer. Control identifier.</summary>
        CONTROL_IDENTIFIER = 24,

        /// <summary>H3C-Result-Code (Type 25). Integer. Result code.</summary>
        RESULT_CODE = 25,

        /// <summary>H3C-Connect-Id (Type 26). Integer. Connection identifier.</summary>
        CONNECT_ID = 26,

        /// <summary>H3C-Ftp-Directory (Type 28). String. FTP directory path.</summary>
        FTP_DIRECTORY = 28,

        /// <summary>H3C-Exec-Privilege (Type 29). Integer. CLI exec privilege level.</summary>
        EXEC_PRIVILEGE = 29,

        /// <summary>H3C-NAS-Startup-Timestamp (Type 59). Integer. NAS startup timestamp.</summary>
        NAS_STARTUP_TIMESTAMP = 59,

        /// <summary>H3C-Ip-Host-Addr (Type 60). String. IP host address string.</summary>
        IP_HOST_ADDR = 60,

        /// <summary>H3C-User-Notify (Type 61). String. User notification string.</summary>
        USER_NOTIFY = 61,

        /// <summary>H3C-User-HeartBeat (Type 62). String. User heartbeat string.</summary>
        USER_HEARTBEAT = 62,

        /// <summary>H3C-User-Group (Type 140). String. User group name.</summary>
        USER_GROUP = 140,

        /// <summary>H3C-Security-Level (Type 141). Integer. Security level.</summary>
        SECURITY_LEVEL = 141,

        /// <summary>H3C-Input-Interval-Octets (Type 201). Integer. Input interval octets.</summary>
        INPUT_INTERVAL_OCTETS = 201,

        /// <summary>H3C-Output-Interval-Octets (Type 202). Integer. Output interval octets.</summary>
        OUTPUT_INTERVAL_OCTETS = 202,

        /// <summary>H3C-Input-Interval-Packets (Type 203). Integer. Input interval packets.</summary>
        INPUT_INTERVAL_PACKETS = 203,

        /// <summary>H3C-Output-Interval-Packets (Type 204). Integer. Output interval packets.</summary>
        OUTPUT_INTERVAL_PACKETS = 204,

        /// <summary>H3C-Input-Interval-Gigawords (Type 205). Integer. Input interval gigawords.</summary>
        INPUT_INTERVAL_GIGAWORDS = 205,

        /// <summary>H3C-Output-Interval-Gigawords (Type 206). Integer. Output interval gigawords.</summary>
        OUTPUT_INTERVAL_GIGAWORDS = 206,

        /// <summary>H3C-Backup-NAS-IP (Type 207). IP address. Backup NAS IP address.</summary>
        BACKUP_NAS_IP = 207,

        /// <summary>H3C-Product-ID (Type 255). String. Product identifier string.</summary>
        PRODUCT_ID = 255
    }

    /// <summary>
    /// H3C-Exec-Privilege attribute values (Type 29).
    /// </summary>
    public enum H3C_EXEC_PRIVILEGE
    {
        /// <summary>Visit level (level 0).</summary>
        VISIT = 0,

        /// <summary>Monitor level (level 1).</summary>
        MONITOR = 1,

        /// <summary>System level (level 2).</summary>
        SYSTEM = 2,

        /// <summary>Management level (level 3).</summary>
        MANAGE = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing H3C / New H3C
    /// (IANA PEN 25506) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.h3c</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// H3C's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 25506</c>.
    /// </para>
    /// <para>
    /// These attributes are used by H3C (New H3C Technologies) switches, routers,
    /// and wireless controllers for RADIUS-based QoS rate limiting (input/output
    /// peak, average, and basic rates), CLI exec privilege assignment, command
    /// authorization, user group mapping, security level control, FTP directory
    /// assignment, interval-based accounting (octets, packets, gigawords),
    /// remanent volume tracking, NAS startup timestamps, backup NAS addressing,
    /// user notifications and heartbeat, and product identification.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(H3cAttributes.ExecPrivilege(H3C_EXEC_PRIVILEGE.MANAGE));
    /// packet.SetAttribute(H3cAttributes.UserGroup("network-admins"));
    /// packet.SetAttribute(H3cAttributes.InputAverageRate(100000000));
    /// packet.SetAttribute(H3cAttributes.OutputAverageRate(50000000));
    /// packet.SetAttribute(H3cAttributes.SecurityLevel(3));
    /// </code>
    /// </remarks>
    public static class H3cAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for H3C (New H3C Technologies).
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 25506;

        #region Integer Attributes

        /// <summary>Creates an H3C-Input-Peak-Rate attribute (Type 1).</summary>
        /// <param name="value">The input peak rate in bps.</param>
        public static VendorSpecificAttributes InputPeakRate(int value) => CreateInteger(H3cAttributeType.INPUT_PEAK_RATE, value);

        /// <summary>Creates an H3C-Input-Average-Rate attribute (Type 2).</summary>
        /// <param name="value">The input average rate in bps.</param>
        public static VendorSpecificAttributes InputAverageRate(int value) => CreateInteger(H3cAttributeType.INPUT_AVERAGE_RATE, value);

        /// <summary>Creates an H3C-Input-Basic-Rate attribute (Type 3).</summary>
        /// <param name="value">The input basic rate in bps.</param>
        public static VendorSpecificAttributes InputBasicRate(int value) => CreateInteger(H3cAttributeType.INPUT_BASIC_RATE, value);

        /// <summary>Creates an H3C-Output-Peak-Rate attribute (Type 4).</summary>
        /// <param name="value">The output peak rate in bps.</param>
        public static VendorSpecificAttributes OutputPeakRate(int value) => CreateInteger(H3cAttributeType.OUTPUT_PEAK_RATE, value);

        /// <summary>Creates an H3C-Output-Average-Rate attribute (Type 5).</summary>
        /// <param name="value">The output average rate in bps.</param>
        public static VendorSpecificAttributes OutputAverageRate(int value) => CreateInteger(H3cAttributeType.OUTPUT_AVERAGE_RATE, value);

        /// <summary>Creates an H3C-Output-Basic-Rate attribute (Type 6).</summary>
        /// <param name="value">The output basic rate in bps.</param>
        public static VendorSpecificAttributes OutputBasicRate(int value) => CreateInteger(H3cAttributeType.OUTPUT_BASIC_RATE, value);

        /// <summary>Creates an H3C-Remanent-Volume attribute (Type 15).</summary>
        /// <param name="value">The remanent (remaining) data volume in KB.</param>
        public static VendorSpecificAttributes RemanentVolume(int value) => CreateInteger(H3cAttributeType.REMANENT_VOLUME, value);

        /// <summary>Creates an H3C-Command attribute (Type 20).</summary>
        /// <param name="value">The command authorization level.</param>
        public static VendorSpecificAttributes Command(int value) => CreateInteger(H3cAttributeType.COMMAND, value);

        /// <summary>Creates an H3C-Control-Identifier attribute (Type 24).</summary>
        /// <param name="value">The control identifier.</param>
        public static VendorSpecificAttributes ControlIdentifier(int value) => CreateInteger(H3cAttributeType.CONTROL_IDENTIFIER, value);

        /// <summary>Creates an H3C-Result-Code attribute (Type 25).</summary>
        /// <param name="value">The result code.</param>
        public static VendorSpecificAttributes ResultCode(int value) => CreateInteger(H3cAttributeType.RESULT_CODE, value);

        /// <summary>Creates an H3C-Connect-Id attribute (Type 26).</summary>
        /// <param name="value">The connection identifier.</param>
        public static VendorSpecificAttributes ConnectId(int value) => CreateInteger(H3cAttributeType.CONNECT_ID, value);

        /// <summary>
        /// Creates an H3C-Exec-Privilege attribute (Type 29) with the specified level.
        /// </summary>
        /// <param name="value">The CLI exec privilege level. See <see cref="H3C_EXEC_PRIVILEGE"/>.</param>
        public static VendorSpecificAttributes ExecPrivilege(H3C_EXEC_PRIVILEGE value) => CreateInteger(H3cAttributeType.EXEC_PRIVILEGE, (int)value);

        /// <summary>Creates an H3C-NAS-Startup-Timestamp attribute (Type 59).</summary>
        /// <param name="value">The NAS startup timestamp.</param>
        public static VendorSpecificAttributes NasStartupTimestamp(int value) => CreateInteger(H3cAttributeType.NAS_STARTUP_TIMESTAMP, value);

        /// <summary>Creates an H3C-Security-Level attribute (Type 141).</summary>
        /// <param name="value">The security level.</param>
        public static VendorSpecificAttributes SecurityLevel(int value) => CreateInteger(H3cAttributeType.SECURITY_LEVEL, value);

        /// <summary>Creates an H3C-Input-Interval-Octets attribute (Type 201).</summary>
        /// <param name="value">The input interval octets.</param>
        public static VendorSpecificAttributes InputIntervalOctets(int value) => CreateInteger(H3cAttributeType.INPUT_INTERVAL_OCTETS, value);

        /// <summary>Creates an H3C-Output-Interval-Octets attribute (Type 202).</summary>
        /// <param name="value">The output interval octets.</param>
        public static VendorSpecificAttributes OutputIntervalOctets(int value) => CreateInteger(H3cAttributeType.OUTPUT_INTERVAL_OCTETS, value);

        /// <summary>Creates an H3C-Input-Interval-Packets attribute (Type 203).</summary>
        /// <param name="value">The input interval packets.</param>
        public static VendorSpecificAttributes InputIntervalPackets(int value) => CreateInteger(H3cAttributeType.INPUT_INTERVAL_PACKETS, value);

        /// <summary>Creates an H3C-Output-Interval-Packets attribute (Type 204).</summary>
        /// <param name="value">The output interval packets.</param>
        public static VendorSpecificAttributes OutputIntervalPackets(int value) => CreateInteger(H3cAttributeType.OUTPUT_INTERVAL_PACKETS, value);

        /// <summary>Creates an H3C-Input-Interval-Gigawords attribute (Type 205).</summary>
        /// <param name="value">The input interval gigawords.</param>
        public static VendorSpecificAttributes InputIntervalGigawords(int value) => CreateInteger(H3cAttributeType.INPUT_INTERVAL_GIGAWORDS, value);

        /// <summary>Creates an H3C-Output-Interval-Gigawords attribute (Type 206).</summary>
        /// <param name="value">The output interval gigawords.</param>
        public static VendorSpecificAttributes OutputIntervalGigawords(int value) => CreateInteger(H3cAttributeType.OUTPUT_INTERVAL_GIGAWORDS, value);

        #endregion

        #region String Attributes

        /// <summary>Creates an H3C-Ftp-Directory attribute (Type 28).</summary>
        /// <param name="value">The FTP directory path. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FtpDirectory(string value) => CreateString(H3cAttributeType.FTP_DIRECTORY, value);

        /// <summary>Creates an H3C-Ip-Host-Addr attribute (Type 60).</summary>
        /// <param name="value">The IP host address string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpHostAddr(string value) => CreateString(H3cAttributeType.IP_HOST_ADDR, value);

        /// <summary>Creates an H3C-User-Notify attribute (Type 61).</summary>
        /// <param name="value">The user notification string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserNotify(string value) => CreateString(H3cAttributeType.USER_NOTIFY, value);

        /// <summary>Creates an H3C-User-HeartBeat attribute (Type 62).</summary>
        /// <param name="value">The user heartbeat string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserHeartBeat(string value) => CreateString(H3cAttributeType.USER_HEARTBEAT, value);

        /// <summary>Creates an H3C-User-Group attribute (Type 140).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value) => CreateString(H3cAttributeType.USER_GROUP, value);

        /// <summary>Creates an H3C-Product-ID attribute (Type 255).</summary>
        /// <param name="value">The product identifier string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ProductId(string value) => CreateString(H3cAttributeType.PRODUCT_ID, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates an H3C-Backup-NAS-IP attribute (Type 207) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The backup NAS IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes BackupNasIp(IPAddress value) => CreateIpv4(H3cAttributeType.BACKUP_NAS_IP, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(H3cAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(H3cAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(H3cAttributeType type, IPAddress value)
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