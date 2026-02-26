using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Microsoft (IANA PEN 311) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.microsoft</c>.
    /// </summary>
    /// <remarks>
    /// Microsoft uses vendor ID 311 for attributes related to Windows NPS (Network
    /// Policy Server, formerly IAS), RRAS (Routing and Remote Access Service),
    /// MS-CHAP/MS-CHAPv2 authentication, PEAP, SSTP VPN, and other Microsoft
    /// networking and remote access technologies.
    /// </remarks>
    public enum MicrosoftAttributeType : byte
    {
        /// <summary>MS-CHAP-Response (Type 1). Octets. MS-CHAP response.</summary>
        CHAP_RESPONSE = 1,

        /// <summary>MS-CHAP-Error (Type 2). String. MS-CHAP error message.</summary>
        CHAP_ERROR = 2,

        /// <summary>MS-CHAP-CPW-1 (Type 3). Octets. MS-CHAP change password v1.</summary>
        CHAP_CPW_1 = 3,

        /// <summary>MS-CHAP-CPW-2 (Type 4). Octets. MS-CHAP change password v2.</summary>
        CHAP_CPW_2 = 4,

        /// <summary>MS-CHAP-LM-Enc-PW (Type 5). Octets. MS-CHAP LM encrypted password.</summary>
        CHAP_LM_ENC_PW = 5,

        /// <summary>MS-CHAP-NT-Enc-PW (Type 6). Octets. MS-CHAP NT encrypted password.</summary>
        CHAP_NT_ENC_PW = 6,

        /// <summary>MS-MPPE-Encryption-Policy (Type 7). Integer. MPPE encryption policy.</summary>
        MPPE_ENCRYPTION_POLICY = 7,

        /// <summary>MS-MPPE-Encryption-Type (Type 8). Integer. MPPE encryption types allowed.</summary>
        MPPE_ENCRYPTION_TYPE = 8,

        /// <summary>MS-RAS-Vendor (Type 9). Integer. RAS vendor identifier.</summary>
        RAS_VENDOR = 9,

        /// <summary>MS-CHAP-Domain (Type 10). String. MS-CHAP authentication domain.</summary>
        CHAP_DOMAIN = 10,

        /// <summary>MS-CHAP-Challenge (Type 11). Octets. MS-CHAP challenge.</summary>
        CHAP_CHALLENGE = 11,

        /// <summary>MS-CHAP-MPPE-Keys (Type 12). Octets. MS-CHAP MPPE keys.</summary>
        CHAP_MPPE_KEYS = 12,

        /// <summary>MS-BAP-Usage (Type 13). Integer. BAP usage policy.</summary>
        BAP_USAGE = 13,

        /// <summary>MS-Link-Utilization-Threshold (Type 14). Integer. BAP link utilization threshold percent.</summary>
        LINK_UTILIZATION_THRESHOLD = 14,

        /// <summary>MS-Link-Drop-Time-Limit (Type 15). Integer. BAP link drop time limit in seconds.</summary>
        LINK_DROP_TIME_LIMIT = 15,

        /// <summary>MS-MPPE-Send-Key (Type 16). Octets. MPPE send key.</summary>
        MPPE_SEND_KEY = 16,

        /// <summary>MS-MPPE-Recv-Key (Type 17). Octets. MPPE receive key.</summary>
        MPPE_RECV_KEY = 17,

        /// <summary>MS-RAS-Version (Type 18). String. RAS version string.</summary>
        RAS_VERSION = 18,

        /// <summary>MS-Old-ARAP-Password (Type 19). Octets. Old ARAP password.</summary>
        OLD_ARAP_PASSWORD = 19,

        /// <summary>MS-New-ARAP-Password (Type 20). Octets. New ARAP password.</summary>
        NEW_ARAP_PASSWORD = 20,

        /// <summary>MS-ARAP-PW-Change-Reason (Type 21). Integer. ARAP password change reason.</summary>
        ARAP_PW_CHANGE_REASON = 21,

        /// <summary>MS-Filter (Type 22). Octets. MS NPS filter rule.</summary>
        FILTER = 22,

        /// <summary>MS-Acct-Auth-Type (Type 23). Integer. Accounting authentication type.</summary>
        ACCT_AUTH_TYPE = 23,

        /// <summary>MS-Acct-EAP-Type (Type 24). Integer. Accounting EAP type.</summary>
        ACCT_EAP_TYPE = 24,

        /// <summary>MS-CHAP2-Response (Type 25). Octets. MS-CHAPv2 response.</summary>
        CHAP2_RESPONSE = 25,

        /// <summary>MS-CHAP2-Success (Type 26). Octets. MS-CHAPv2 success message.</summary>
        CHAP2_SUCCESS = 26,

        /// <summary>MS-CHAP2-CPW (Type 27). Octets. MS-CHAPv2 change password.</summary>
        CHAP2_CPW = 27,

        /// <summary>MS-Primary-DNS-Server (Type 28). IP address. Primary DNS server.</summary>
        PRIMARY_DNS_SERVER = 28,

        /// <summary>MS-Secondary-DNS-Server (Type 29). IP address. Secondary DNS server.</summary>
        SECONDARY_DNS_SERVER = 29,

        /// <summary>MS-Primary-NBNS-Server (Type 30). IP address. Primary NBNS/WINS server.</summary>
        PRIMARY_NBNS_SERVER = 30,

        /// <summary>MS-Secondary-NBNS-Server (Type 31). IP address. Secondary NBNS/WINS server.</summary>
        SECONDARY_NBNS_SERVER = 31,

        /// <summary>MS-ARAP-Challenge (Type 33). Octets. ARAP challenge.</summary>
        ARAP_CHALLENGE = 33,

        /// <summary>MS-RAS-Client-Name (Type 34). String. RAS client name.</summary>
        RAS_CLIENT_NAME = 34,

        /// <summary>MS-RAS-Client-Version (Type 35). String. RAS client version.</summary>
        RAS_CLIENT_VERSION = 35,

        /// <summary>MS-Quarantine-IPFilter (Type 36). Octets. Quarantine IP filter.</summary>
        QUARANTINE_IPFILTER = 36,

        /// <summary>MS-Quarantine-Session-Timeout (Type 37). Integer. Quarantine session timeout in seconds.</summary>
        QUARANTINE_SESSION_TIMEOUT = 37,

        /// <summary>MS-User-Security-Identity (Type 38). String. User security identity (SID).</summary>
        USER_SECURITY_IDENTITY = 38,

        /// <summary>MS-Identity-Type (Type 39). Integer. Identity type.</summary>
        IDENTITY_TYPE = 39,

        /// <summary>MS-Service-Class (Type 40). String. Service class name.</summary>
        SERVICE_CLASS = 40,

        /// <summary>MS-Quarantine-User-Class (Type 41). String. Quarantine user class.</summary>
        QUARANTINE_USER_CLASS = 41,

        /// <summary>MS-Quarantine-State (Type 42). Integer. Quarantine state.</summary>
        QUARANTINE_STATE = 42,

        /// <summary>MS-Quarantine-Grace-Time (Type 43). Integer. Quarantine grace time in seconds.</summary>
        QUARANTINE_GRACE_TIME = 43,

        /// <summary>MS-Network-Access-Server-Type (Type 44). Integer. NAS type.</summary>
        NETWORK_ACCESS_SERVER_TYPE = 44,

        /// <summary>MS-AFW-Zone (Type 45). Integer. Advanced Firewall zone.</summary>
        AFW_ZONE = 45,

        /// <summary>MS-AFW-Protection-Level (Type 46). Integer. Advanced Firewall protection level.</summary>
        AFW_PROTECTION_LEVEL = 46,

        /// <summary>MS-Machine-Name (Type 47). String. Machine name (computer name).</summary>
        MACHINE_NAME = 47,

        /// <summary>MS-IPv6-Filter (Type 48). Octets. IPv6 filter rule.</summary>
        IPV6_FILTER = 48,

        /// <summary>MS-IPv4-Remediation-Servers (Type 49). Octets. IPv4 remediation servers.</summary>
        IPV4_REMEDIATION_SERVERS = 49,

        /// <summary>MS-IPv6-Remediation-Servers (Type 50). Octets. IPv6 remediation servers.</summary>
        IPV6_REMEDIATION_SERVERS = 50,

        /// <summary>MS-RNAP-Not-Quarantine-Capable (Type 51). Integer. RNAP not quarantine capable flag.</summary>
        RNAP_NOT_QUARANTINE_CAPABLE = 51,

        /// <summary>MS-Quarantine-SOH (Type 55). Octets. Quarantine Statement of Health.</summary>
        QUARANTINE_SOH = 55,

        /// <summary>MS-RAS-Correlation (Type 56). Octets. RAS correlation ID.</summary>
        RAS_CORRELATION = 56,

        /// <summary>MS-Extended-Quarantine-State (Type 57). Integer. Extended quarantine state.</summary>
        EXTENDED_QUARANTINE_STATE = 57,

        /// <summary>MS-HCAP-User-Groups (Type 58). String. HCAP user groups.</summary>
        HCAP_USER_GROUPS = 58,

        /// <summary>MS-HCAP-Location-Group-Name (Type 59). String. HCAP location group name.</summary>
        HCAP_LOCATION_GROUP_NAME = 59,

        /// <summary>MS-HCAP-User-Name (Type 60). String. HCAP user name.</summary>
        HCAP_USER_NAME = 60,

        /// <summary>MS-User-IPv4-Address (Type 61). IP address. User IPv4 address.</summary>
        USER_IPV4_ADDRESS = 61,

        /// <summary>MS-User-IPv6-Address (Type 62). String. User IPv6 address.</summary>
        USER_IPV6_ADDRESS = 62,

        /// <summary>MS-TSGS-Device-Id (Type 63). String. TS Gateway device identifier.</summary>
        TSGS_DEVICE_ID = 63,

        /// <summary>MS-Quarantine-Update-Non-Compliant (Type 64). Integer. Quarantine update non-compliant flag.</summary>
        QUARANTINE_UPDATE_NON_COMPLIANT = 64
    }

    /// <summary>
    /// MS-MPPE-Encryption-Policy attribute values (Type 7).
    /// </summary>
    public enum MS_MPPE_ENCRYPTION_POLICY
    {
        /// <summary>Encryption allowed but not required.</summary>
        ALLOWED = 1,

        /// <summary>Encryption required.</summary>
        REQUIRED = 2
    }

    /// <summary>
    /// MS-MPPE-Encryption-Type attribute values (Type 8). Bitmask.
    /// </summary>
    [Flags]
    public enum MS_MPPE_ENCRYPTION_TYPE
    {
        /// <summary>RC4 40-bit encryption.</summary>
        RC4_40BIT = 0x02,

        /// <summary>RC4 128-bit encryption.</summary>
        RC4_128BIT = 0x04,

        /// <summary>RC4 56-bit encryption.</summary>
        RC4_56BIT = 0x08
    }

    /// <summary>
    /// MS-BAP-Usage attribute values (Type 13).
    /// </summary>
    public enum MS_BAP_USAGE
    {
        /// <summary>BAP not allowed.</summary>
        NOT_ALLOWED = 0,

        /// <summary>BAP allowed.</summary>
        ALLOWED = 1,

        /// <summary>BAP required.</summary>
        REQUIRED = 2
    }

    /// <summary>
    /// MS-ARAP-PW-Change-Reason attribute values (Type 21).
    /// </summary>
    public enum MS_ARAP_PW_CHANGE_REASON
    {
        /// <summary>Just change password.</summary>
        JUST_CHANGE = 1,

        /// <summary>Password expired.</summary>
        EXPIRED = 2,

        /// <summary>Admin requires password change.</summary>
        ADMIN_REQUIRES = 3,

        /// <summary>Password too short.</summary>
        TOO_SHORT = 4
    }

    /// <summary>
    /// MS-Acct-Auth-Type attribute values (Type 23).
    /// </summary>
    public enum MS_ACCT_AUTH_TYPE
    {
        /// <summary>PAP authentication.</summary>
        PAP = 1,

        /// <summary>CHAP authentication.</summary>
        CHAP = 2,

        /// <summary>MS-CHAPv1 authentication.</summary>
        MS_CHAP_1 = 3,

        /// <summary>MS-CHAPv2 authentication.</summary>
        MS_CHAP_2 = 4,

        /// <summary>EAP authentication.</summary>
        EAP = 5
    }

    /// <summary>
    /// MS-Acct-EAP-Type attribute values (Type 24).
    /// </summary>
    public enum MS_ACCT_EAP_TYPE
    {
        /// <summary>EAP-MD5.</summary>
        MD5 = 4,

        /// <summary>EAP-OTP.</summary>
        OTP = 5,

        /// <summary>EAP-GTC.</summary>
        GENERIC_TOKEN_CARD = 6,

        /// <summary>EAP-TLS.</summary>
        TLS = 13,

        /// <summary>PEAP.</summary>
        PEAP = 25,

        /// <summary>EAP-MSCHAPv2.</summary>
        MS_CHAP_V2 = 26
    }

    /// <summary>
    /// MS-Network-Access-Server-Type attribute values (Type 44).
    /// </summary>
    public enum MS_NETWORK_ACCESS_SERVER_TYPE
    {
        /// <summary>Unspecified NAS type.</summary>
        UNSPECIFIED = 0,

        /// <summary>Terminal Server Gateway (TSGS).</summary>
        TERMINAL_SERVER_GATEWAY = 1,

        /// <summary>Remote Access Server (RAS/VPN).</summary>
        REMOTE_ACCESS_SERVER = 2,

        /// <summary>DHCP Server.</summary>
        DHCP_SERVER = 3,

        /// <summary>Wireless Access Point (802.1X).</summary>
        WIRELESS_ACCESS_POINT = 4,

        /// <summary>Health Registration Authority (HRA).</summary>
        HRA = 5,

        /// <summary>HCAP Server.</summary>
        HCAP_SERVER = 6
    }

    /// <summary>
    /// MS-AFW-Protection-Level attribute values (Type 46).
    /// </summary>
    public enum MS_AFW_PROTECTION_LEVEL
    {
        /// <summary>Low protection.</summary>
        LOW = 1,

        /// <summary>Medium protection.</summary>
        MEDIUM = 2,

        /// <summary>High protection.</summary>
        HIGH = 3
    }

    /// <summary>
    /// MS-Quarantine-State attribute values (Type 42).
    /// </summary>
    public enum MS_QUARANTINE_STATE
    {
        /// <summary>Full access (not quarantined).</summary>
        FULL_ACCESS = 0,

        /// <summary>Quarantined.</summary>
        QUARANTINE = 1,

        /// <summary>Probation.</summary>
        PROBATION = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Microsoft
    /// (IANA PEN 311) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.microsoft</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Microsoft's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 311</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Microsoft Windows NPS (Network Policy Server),
    /// RRAS (Routing and Remote Access Service), and related technologies for
    /// RADIUS-based MS-CHAP/MS-CHAPv2 authentication, MPPE encryption key and
    /// policy negotiation, DNS/NBNS provisioning, BAP (Bandwidth Allocation
    /// Protocol) policy, Network Access Protection (NAP) quarantine management
    /// (state, SOH, remediation servers, grace time), RAS client/version
    /// identification, HCAP user groups and location groups, service class
    /// assignment, firewall zone/protection level, user security identity (SID),
    /// machine name, TS Gateway device identification, and IPv4/IPv6 filter rules.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(MicrosoftAttributes.MppeEncryptionPolicy(MS_MPPE_ENCRYPTION_POLICY.REQUIRED));
    /// packet.SetAttribute(MicrosoftAttributes.MppeEncryptionType(MS_MPPE_ENCRYPTION_TYPE.RC4_128BIT));
    /// packet.SetAttribute(MicrosoftAttributes.PrimaryDnsServer(IPAddress.Parse("10.0.0.1")));
    /// packet.SetAttribute(MicrosoftAttributes.SecondaryDnsServer(IPAddress.Parse("10.0.0.2")));
    /// packet.SetAttribute(MicrosoftAttributes.ServiceClass("premium-vpn"));
    /// </code>
    /// </remarks>
    public static class MicrosoftAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Microsoft.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 311;

        #region Integer Attributes

        /// <summary>Creates an MS-MPPE-Encryption-Policy attribute (Type 7).</summary>
        /// <param name="value">The MPPE encryption policy. See <see cref="MS_MPPE_ENCRYPTION_POLICY"/>.</param>
        public static VendorSpecificAttributes MppeEncryptionPolicy(MS_MPPE_ENCRYPTION_POLICY value) => CreateInteger(MicrosoftAttributeType.MPPE_ENCRYPTION_POLICY, (int)value);

        /// <summary>Creates an MS-MPPE-Encryption-Type attribute (Type 8).</summary>
        /// <param name="value">The MPPE encryption types bitmask. See <see cref="MS_MPPE_ENCRYPTION_TYPE"/>.</param>
        public static VendorSpecificAttributes MppeEncryptionType(MS_MPPE_ENCRYPTION_TYPE value) => CreateInteger(MicrosoftAttributeType.MPPE_ENCRYPTION_TYPE, (int)value);

        /// <summary>Creates an MS-RAS-Vendor attribute (Type 9).</summary>
        /// <param name="value">The RAS vendor identifier (IANA PEN).</param>
        public static VendorSpecificAttributes RasVendor(int value) => CreateInteger(MicrosoftAttributeType.RAS_VENDOR, value);

        /// <summary>Creates an MS-BAP-Usage attribute (Type 13).</summary>
        /// <param name="value">The BAP usage policy. See <see cref="MS_BAP_USAGE"/>.</param>
        public static VendorSpecificAttributes BapUsage(MS_BAP_USAGE value) => CreateInteger(MicrosoftAttributeType.BAP_USAGE, (int)value);

        /// <summary>Creates an MS-Link-Utilization-Threshold attribute (Type 14).</summary>
        /// <param name="value">The BAP link utilization threshold percent (1–100).</param>
        public static VendorSpecificAttributes LinkUtilizationThreshold(int value) => CreateInteger(MicrosoftAttributeType.LINK_UTILIZATION_THRESHOLD, value);

        /// <summary>Creates an MS-Link-Drop-Time-Limit attribute (Type 15).</summary>
        /// <param name="value">The BAP link drop time limit in seconds.</param>
        public static VendorSpecificAttributes LinkDropTimeLimit(int value) => CreateInteger(MicrosoftAttributeType.LINK_DROP_TIME_LIMIT, value);

        /// <summary>Creates an MS-ARAP-PW-Change-Reason attribute (Type 21).</summary>
        /// <param name="value">The ARAP password change reason. See <see cref="MS_ARAP_PW_CHANGE_REASON"/>.</param>
        public static VendorSpecificAttributes ArapPwChangeReason(MS_ARAP_PW_CHANGE_REASON value) => CreateInteger(MicrosoftAttributeType.ARAP_PW_CHANGE_REASON, (int)value);

        /// <summary>Creates an MS-Acct-Auth-Type attribute (Type 23).</summary>
        /// <param name="value">The accounting authentication type. See <see cref="MS_ACCT_AUTH_TYPE"/>.</param>
        public static VendorSpecificAttributes AcctAuthType(MS_ACCT_AUTH_TYPE value) => CreateInteger(MicrosoftAttributeType.ACCT_AUTH_TYPE, (int)value);

        /// <summary>Creates an MS-Acct-EAP-Type attribute (Type 24).</summary>
        /// <param name="value">The accounting EAP type. See <see cref="MS_ACCT_EAP_TYPE"/>.</param>
        public static VendorSpecificAttributes AcctEapType(MS_ACCT_EAP_TYPE value) => CreateInteger(MicrosoftAttributeType.ACCT_EAP_TYPE, (int)value);

        /// <summary>Creates an MS-Quarantine-Session-Timeout attribute (Type 37).</summary>
        /// <param name="value">The quarantine session timeout in seconds.</param>
        public static VendorSpecificAttributes QuarantineSessionTimeout(int value) => CreateInteger(MicrosoftAttributeType.QUARANTINE_SESSION_TIMEOUT, value);

        /// <summary>Creates an MS-Identity-Type attribute (Type 39).</summary>
        /// <param name="value">The identity type.</param>
        public static VendorSpecificAttributes IdentityType(int value) => CreateInteger(MicrosoftAttributeType.IDENTITY_TYPE, value);

        /// <summary>Creates an MS-Quarantine-State attribute (Type 42).</summary>
        /// <param name="value">The quarantine state. See <see cref="MS_QUARANTINE_STATE"/>.</param>
        public static VendorSpecificAttributes QuarantineState(MS_QUARANTINE_STATE value) => CreateInteger(MicrosoftAttributeType.QUARANTINE_STATE, (int)value);

        /// <summary>Creates an MS-Quarantine-Grace-Time attribute (Type 43).</summary>
        /// <param name="value">The quarantine grace time in seconds.</param>
        public static VendorSpecificAttributes QuarantineGraceTime(int value) => CreateInteger(MicrosoftAttributeType.QUARANTINE_GRACE_TIME, value);

        /// <summary>Creates an MS-Network-Access-Server-Type attribute (Type 44).</summary>
        /// <param name="value">The NAS type. See <see cref="MS_NETWORK_ACCESS_SERVER_TYPE"/>.</param>
        public static VendorSpecificAttributes NetworkAccessServerType(MS_NETWORK_ACCESS_SERVER_TYPE value) => CreateInteger(MicrosoftAttributeType.NETWORK_ACCESS_SERVER_TYPE, (int)value);

        /// <summary>Creates an MS-AFW-Zone attribute (Type 45).</summary>
        /// <param name="value">The Advanced Firewall zone.</param>
        public static VendorSpecificAttributes AfwZone(int value) => CreateInteger(MicrosoftAttributeType.AFW_ZONE, value);

        /// <summary>Creates an MS-AFW-Protection-Level attribute (Type 46).</summary>
        /// <param name="value">The Advanced Firewall protection level. See <see cref="MS_AFW_PROTECTION_LEVEL"/>.</param>
        public static VendorSpecificAttributes AfwProtectionLevel(MS_AFW_PROTECTION_LEVEL value) => CreateInteger(MicrosoftAttributeType.AFW_PROTECTION_LEVEL, (int)value);

        /// <summary>Creates an MS-RNAP-Not-Quarantine-Capable attribute (Type 51).</summary>
        /// <param name="value">The RNAP not quarantine capable flag.</param>
        public static VendorSpecificAttributes RnapNotQuarantineCapable(int value) => CreateInteger(MicrosoftAttributeType.RNAP_NOT_QUARANTINE_CAPABLE, value);

        /// <summary>Creates an MS-Extended-Quarantine-State attribute (Type 57).</summary>
        /// <param name="value">The extended quarantine state.</param>
        public static VendorSpecificAttributes ExtendedQuarantineState(int value) => CreateInteger(MicrosoftAttributeType.EXTENDED_QUARANTINE_STATE, value);

        /// <summary>Creates an MS-Quarantine-Update-Non-Compliant attribute (Type 64).</summary>
        /// <param name="value">The quarantine update non-compliant flag.</param>
        public static VendorSpecificAttributes QuarantineUpdateNonCompliant(int value) => CreateInteger(MicrosoftAttributeType.QUARANTINE_UPDATE_NON_COMPLIANT, value);

        #endregion

        #region String Attributes

        /// <summary>Creates an MS-CHAP-Error attribute (Type 2).</summary>
        public static VendorSpecificAttributes ChapError(string value) => CreateString(MicrosoftAttributeType.CHAP_ERROR, value);

        /// <summary>Creates an MS-CHAP-Domain attribute (Type 10).</summary>
        public static VendorSpecificAttributes ChapDomain(string value) => CreateString(MicrosoftAttributeType.CHAP_DOMAIN, value);

        /// <summary>Creates an MS-RAS-Version attribute (Type 18).</summary>
        public static VendorSpecificAttributes RasVersion(string value) => CreateString(MicrosoftAttributeType.RAS_VERSION, value);

        /// <summary>Creates an MS-RAS-Client-Name attribute (Type 34).</summary>
        public static VendorSpecificAttributes RasClientName(string value) => CreateString(MicrosoftAttributeType.RAS_CLIENT_NAME, value);

        /// <summary>Creates an MS-RAS-Client-Version attribute (Type 35).</summary>
        public static VendorSpecificAttributes RasClientVersion(string value) => CreateString(MicrosoftAttributeType.RAS_CLIENT_VERSION, value);

        /// <summary>Creates an MS-User-Security-Identity attribute (Type 38).</summary>
        public static VendorSpecificAttributes UserSecurityIdentity(string value) => CreateString(MicrosoftAttributeType.USER_SECURITY_IDENTITY, value);

        /// <summary>Creates an MS-Service-Class attribute (Type 40).</summary>
        public static VendorSpecificAttributes ServiceClass(string value) => CreateString(MicrosoftAttributeType.SERVICE_CLASS, value);

        /// <summary>Creates an MS-Quarantine-User-Class attribute (Type 41).</summary>
        public static VendorSpecificAttributes QuarantineUserClass(string value) => CreateString(MicrosoftAttributeType.QUARANTINE_USER_CLASS, value);

        /// <summary>Creates an MS-Machine-Name attribute (Type 47).</summary>
        public static VendorSpecificAttributes MachineName(string value) => CreateString(MicrosoftAttributeType.MACHINE_NAME, value);

        /// <summary>Creates an MS-HCAP-User-Groups attribute (Type 58).</summary>
        public static VendorSpecificAttributes HcapUserGroups(string value) => CreateString(MicrosoftAttributeType.HCAP_USER_GROUPS, value);

        /// <summary>Creates an MS-HCAP-Location-Group-Name attribute (Type 59).</summary>
        public static VendorSpecificAttributes HcapLocationGroupName(string value) => CreateString(MicrosoftAttributeType.HCAP_LOCATION_GROUP_NAME, value);

        /// <summary>Creates an MS-HCAP-User-Name attribute (Type 60).</summary>
        public static VendorSpecificAttributes HcapUserName(string value) => CreateString(MicrosoftAttributeType.HCAP_USER_NAME, value);

        /// <summary>Creates an MS-User-IPv6-Address attribute (Type 62).</summary>
        public static VendorSpecificAttributes UserIpv6Address(string value) => CreateString(MicrosoftAttributeType.USER_IPV6_ADDRESS, value);

        /// <summary>Creates an MS-TSGS-Device-Id attribute (Type 63).</summary>
        public static VendorSpecificAttributes TsgsDeviceId(string value) => CreateString(MicrosoftAttributeType.TSGS_DEVICE_ID, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates an MS-Primary-DNS-Server attribute (Type 28).</summary>
        public static VendorSpecificAttributes PrimaryDnsServer(IPAddress value) => CreateIpv4(MicrosoftAttributeType.PRIMARY_DNS_SERVER, value);

        /// <summary>Creates an MS-Secondary-DNS-Server attribute (Type 29).</summary>
        public static VendorSpecificAttributes SecondaryDnsServer(IPAddress value) => CreateIpv4(MicrosoftAttributeType.SECONDARY_DNS_SERVER, value);

        /// <summary>Creates an MS-Primary-NBNS-Server attribute (Type 30).</summary>
        public static VendorSpecificAttributes PrimaryNbnsServer(IPAddress value) => CreateIpv4(MicrosoftAttributeType.PRIMARY_NBNS_SERVER, value);

        /// <summary>Creates an MS-Secondary-NBNS-Server attribute (Type 31).</summary>
        public static VendorSpecificAttributes SecondaryNbnsServer(IPAddress value) => CreateIpv4(MicrosoftAttributeType.SECONDARY_NBNS_SERVER, value);

        /// <summary>Creates an MS-User-IPv4-Address attribute (Type 61).</summary>
        public static VendorSpecificAttributes UserIpv4Address(IPAddress value) => CreateIpv4(MicrosoftAttributeType.USER_IPV4_ADDRESS, value);

        #endregion

        #region Octets Attributes

        /// <summary>Creates an MS-CHAP-Response attribute (Type 1).</summary>
        public static VendorSpecificAttributes ChapResponse(byte[] value) => CreateOctets(MicrosoftAttributeType.CHAP_RESPONSE, value);

        /// <summary>Creates an MS-CHAP-CPW-1 attribute (Type 3).</summary>
        public static VendorSpecificAttributes ChapCpw1(byte[] value) => CreateOctets(MicrosoftAttributeType.CHAP_CPW_1, value);

        /// <summary>Creates an MS-CHAP-CPW-2 attribute (Type 4).</summary>
        public static VendorSpecificAttributes ChapCpw2(byte[] value) => CreateOctets(MicrosoftAttributeType.CHAP_CPW_2, value);

        /// <summary>Creates an MS-CHAP-LM-Enc-PW attribute (Type 5).</summary>
        public static VendorSpecificAttributes ChapLmEncPw(byte[] value) => CreateOctets(MicrosoftAttributeType.CHAP_LM_ENC_PW, value);

        /// <summary>Creates an MS-CHAP-NT-Enc-PW attribute (Type 6).</summary>
        public static VendorSpecificAttributes ChapNtEncPw(byte[] value) => CreateOctets(MicrosoftAttributeType.CHAP_NT_ENC_PW, value);

        /// <summary>Creates an MS-CHAP-Challenge attribute (Type 11).</summary>
        public static VendorSpecificAttributes ChapChallenge(byte[] value) => CreateOctets(MicrosoftAttributeType.CHAP_CHALLENGE, value);

        /// <summary>Creates an MS-CHAP-MPPE-Keys attribute (Type 12).</summary>
        public static VendorSpecificAttributes ChapMppeKeys(byte[] value) => CreateOctets(MicrosoftAttributeType.CHAP_MPPE_KEYS, value);

        /// <summary>Creates an MS-MPPE-Send-Key attribute (Type 16).</summary>
        public static VendorSpecificAttributes MppeSendKey(byte[] value) => CreateOctets(MicrosoftAttributeType.MPPE_SEND_KEY, value);

        /// <summary>Creates an MS-MPPE-Recv-Key attribute (Type 17).</summary>
        public static VendorSpecificAttributes MppeRecvKey(byte[] value) => CreateOctets(MicrosoftAttributeType.MPPE_RECV_KEY, value);

        /// <summary>Creates an MS-Old-ARAP-Password attribute (Type 19).</summary>
        public static VendorSpecificAttributes OldArapPassword(byte[] value) => CreateOctets(MicrosoftAttributeType.OLD_ARAP_PASSWORD, value);

        /// <summary>Creates an MS-New-ARAP-Password attribute (Type 20).</summary>
        public static VendorSpecificAttributes NewArapPassword(byte[] value) => CreateOctets(MicrosoftAttributeType.NEW_ARAP_PASSWORD, value);

        /// <summary>Creates an MS-Filter attribute (Type 22).</summary>
        public static VendorSpecificAttributes Filter(byte[] value) => CreateOctets(MicrosoftAttributeType.FILTER, value);

        /// <summary>Creates an MS-CHAP2-Response attribute (Type 25).</summary>
        public static VendorSpecificAttributes Chap2Response(byte[] value) => CreateOctets(MicrosoftAttributeType.CHAP2_RESPONSE, value);

        /// <summary>Creates an MS-CHAP2-Success attribute (Type 26).</summary>
        public static VendorSpecificAttributes Chap2Success(byte[] value) => CreateOctets(MicrosoftAttributeType.CHAP2_SUCCESS, value);

        /// <summary>Creates an MS-CHAP2-CPW attribute (Type 27).</summary>
        public static VendorSpecificAttributes Chap2Cpw(byte[] value) => CreateOctets(MicrosoftAttributeType.CHAP2_CPW, value);

        /// <summary>Creates an MS-ARAP-Challenge attribute (Type 33).</summary>
        public static VendorSpecificAttributes ArapChallenge(byte[] value) => CreateOctets(MicrosoftAttributeType.ARAP_CHALLENGE, value);

        /// <summary>Creates an MS-Quarantine-IPFilter attribute (Type 36).</summary>
        public static VendorSpecificAttributes QuarantineIpFilter(byte[] value) => CreateOctets(MicrosoftAttributeType.QUARANTINE_IPFILTER, value);

        /// <summary>Creates an MS-IPv6-Filter attribute (Type 48).</summary>
        public static VendorSpecificAttributes Ipv6Filter(byte[] value) => CreateOctets(MicrosoftAttributeType.IPV6_FILTER, value);

        /// <summary>Creates an MS-IPv4-Remediation-Servers attribute (Type 49).</summary>
        public static VendorSpecificAttributes Ipv4RemediationServers(byte[] value) => CreateOctets(MicrosoftAttributeType.IPV4_REMEDIATION_SERVERS, value);

        /// <summary>Creates an MS-IPv6-Remediation-Servers attribute (Type 50).</summary>
        public static VendorSpecificAttributes Ipv6RemediationServers(byte[] value) => CreateOctets(MicrosoftAttributeType.IPV6_REMEDIATION_SERVERS, value);

        /// <summary>Creates an MS-Quarantine-SOH attribute (Type 55).</summary>
        public static VendorSpecificAttributes QuarantineSoh(byte[] value) => CreateOctets(MicrosoftAttributeType.QUARANTINE_SOH, value);

        /// <summary>Creates an MS-RAS-Correlation attribute (Type 56).</summary>
        public static VendorSpecificAttributes RasCorrelation(byte[] value) => CreateOctets(MicrosoftAttributeType.RAS_CORRELATION, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(MicrosoftAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(MicrosoftAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateOctets(MicrosoftAttributeType type, byte[] value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, value);
        }

        private static VendorSpecificAttributes CreateIpv4(MicrosoftAttributeType type, IPAddress value)
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