using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an ADTRAN (IANA PEN 664) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.adtran</c>.
    /// </summary>
    public enum AdtranAttributeType : byte
    {
        /// <summary>Adtran-Agent-Circuit-Id (Type 1). String. Agent Circuit ID (Option 82 sub-option 1).</summary>
        AGENT_CIRCUIT_ID = 1,

        /// <summary>Adtran-Agent-Remote-Id (Type 2). String. Agent Remote ID (Option 82 sub-option 2).</summary>
        AGENT_REMOTE_ID = 2,

        /// <summary>Adtran-Auth-Group (Type 3). Integer. Authorisation group for the user.</summary>
        AUTH_GROUP = 3,

        /// <summary>Adtran-Access-Level (Type 4). Integer. CLI access privilege level.</summary>
        ACCESS_LEVEL = 4,

        /// <summary>Adtran-Activate-Service (Type 5). String. Service to activate for the subscriber.</summary>
        ACTIVATE_SERVICE = 5,

        /// <summary>Adtran-Deactivate-Service (Type 6). String. Service to deactivate for the subscriber.</summary>
        DEACTIVATE_SERVICE = 6,

        /// <summary>Adtran-User-Privilege (Type 7). Integer. User privilege level.</summary>
        USER_PRIVILEGE = 7,

        /// <summary>Adtran-QOS-Downstream (Type 8). String. Downstream QoS policy name.</summary>
        QOS_DOWNSTREAM = 8,

        /// <summary>Adtran-QOS-Upstream (Type 9). String. Upstream QoS policy name.</summary>
        QOS_UPSTREAM = 9,

        /// <summary>Adtran-VPI (Type 10). Integer. ATM Virtual Path Identifier.</summary>
        VPI = 10,

        /// <summary>Adtran-VCI (Type 11). Integer. ATM Virtual Channel Identifier.</summary>
        VCI = 11,

        /// <summary>Adtran-Max-Sessions (Type 12). Integer. Maximum concurrent sessions allowed.</summary>
        MAX_SESSIONS = 12,

        /// <summary>Adtran-Primary-DNS (Type 13). String. Primary DNS server address.</summary>
        PRIMARY_DNS = 13,

        /// <summary>Adtran-Secondary-DNS (Type 14). String. Secondary DNS server address.</summary>
        SECONDARY_DNS = 14,

        /// <summary>Adtran-IP-Pool-Name (Type 15). String. IP address pool name.</summary>
        IP_POOL_NAME = 15,

        /// <summary>Adtran-Framed-Route (Type 16). String. Framed route entry.</summary>
        FRAMED_ROUTE = 16,

        /// <summary>Adtran-Subscriber-Id (Type 17). String. Subscriber identifier.</summary>
        SUBSCRIBER_ID = 17,

        /// <summary>Adtran-DHCP-Option (Type 18). String. DHCP option string.</summary>
        DHCP_OPTION = 18,

        /// <summary>Adtran-Domain-Name (Type 19). String. Domain name for the subscriber.</summary>
        DOMAIN_NAME = 19,

        /// <summary>Adtran-ANCP-String (Type 20). String. ANCP (Access Node Control Protocol) string.</summary>
        ANCP_STRING = 20,

        /// <summary>Adtran-Service (Type 21). String. Service type string.</summary>
        SERVICE = 21,

        /// <summary>Adtran-DHCP-Server (Type 22). String. DHCP server address.</summary>
        DHCP_SERVER = 22,

        /// <summary>Adtran-DHCP-Lease-Time (Type 23). Integer. DHCP lease time in seconds.</summary>
        DHCP_LEASE_TIME = 23,

        /// <summary>Adtran-Downstream-Rate (Type 24). Integer. Downstream data rate in kbps.</summary>
        DOWNSTREAM_RATE = 24,

        /// <summary>Adtran-Upstream-Rate (Type 25). Integer. Upstream data rate in kbps.</summary>
        UPSTREAM_RATE = 25,

        /// <summary>Adtran-VLAN-Id (Type 26). Integer. VLAN identifier to assign.</summary>
        VLAN_ID = 26,

        /// <summary>Adtran-Inner-VLAN-Id (Type 27). Integer. Inner (QinQ) VLAN identifier.</summary>
        INNER_VLAN_ID = 27,

        /// <summary>Adtran-Acl-Name (Type 28). String. Access control list name.</summary>
        ACL_NAME = 28,

        /// <summary>Adtran-Encapsulation (Type 29). String. Encapsulation type string.</summary>
        ENCAPSULATION = 29,

        /// <summary>Adtran-IF-Name (Type 30). String. Interface name.</summary>
        IF_NAME = 30
    }

    /// <summary>
    /// Adtran-Auth-Group attribute values (Type 3).
    /// </summary>
    public enum ADTRAN_AUTH_GROUP
    {
        /// <summary>No group assigned.</summary>
        NONE = 0,

        /// <summary>Monitor group (read-only).</summary>
        MONITOR = 1,

        /// <summary>Manager group (read-write).</summary>
        MANAGER = 2,

        /// <summary>Administrator group (full control).</summary>
        ADMIN = 3
    }

    /// <summary>
    /// Adtran-Access-Level attribute values (Type 4).
    /// </summary>
    public enum ADTRAN_ACCESS_LEVEL
    {
        /// <summary>Level 0 — minimal access.</summary>
        LEVEL_0 = 0,

        /// <summary>Level 1 — basic user access.</summary>
        LEVEL_1 = 1,

        /// <summary>Level 2 — operator access.</summary>
        LEVEL_2 = 2,

        /// <summary>Level 3 — manager access.</summary>
        LEVEL_3 = 3,

        /// <summary>Level 4 — administrator access.</summary>
        LEVEL_4 = 4,

        /// <summary>Level 5 — super-user access.</summary>
        LEVEL_5 = 5,

        /// <summary>Level 6 — extended privilege access.</summary>
        LEVEL_6 = 6,

        /// <summary>Level 7 — advanced privilege access.</summary>
        LEVEL_7 = 7,

        /// <summary>Level 8 — high privilege access.</summary>
        LEVEL_8 = 8,

        /// <summary>Level 9 — highest privilege access.</summary>
        LEVEL_9 = 9,

        /// <summary>Level 10 — unrestricted access.</summary>
        LEVEL_10 = 10,

        /// <summary>Level 11 — unrestricted access (extended).</summary>
        LEVEL_11 = 11,

        /// <summary>Level 12 — unrestricted access (extended).</summary>
        LEVEL_12 = 12,

        /// <summary>Level 13 — unrestricted access (extended).</summary>
        LEVEL_13 = 13,

        /// <summary>Level 14 — unrestricted access (extended).</summary>
        LEVEL_14 = 14,

        /// <summary>Level 15 — full unrestricted access (enable mode).</summary>
        LEVEL_15 = 15
    }

    /// <summary>
    /// Adtran-User-Privilege attribute values (Type 7).
    /// </summary>
    public enum ADTRAN_USER_PRIVILEGE
    {
        /// <summary>Basic user privilege.</summary>
        USER = 1,

        /// <summary>Operator privilege.</summary>
        OPERATOR = 5,

        /// <summary>Administrator privilege (enable mode).</summary>
        ADMIN = 15
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing ADTRAN (IANA PEN 664)
    /// Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.adtran</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ADTRAN's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 664</c>.
    /// </para>
    /// <para>
    /// These attributes are used by ADTRAN routers, switches, and access devices for
    /// subscriber management, QoS policy assignment, VLAN configuration, DHCP control,
    /// and CLI access level authorisation.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(AdtranAttributes.AccessLevel(ADTRAN_ACCESS_LEVEL.LEVEL_15));
    /// packet.SetAttribute(AdtranAttributes.QosDownstream("10Mbps-Profile"));
    /// packet.SetAttribute(AdtranAttributes.VlanId(100));
    /// packet.SetAttribute(AdtranAttributes.IpPoolName("subscriber-pool"));
    /// </code>
    /// </remarks>
    public static class AdtranAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for ADTRAN, Inc.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 664;

        #region Integer Attributes

        /// <summary>
        /// Creates an Adtran-Auth-Group attribute (Type 3) with the specified authorisation group.
        /// </summary>
        /// <param name="value">The authorisation group. See <see cref="ADTRAN_AUTH_GROUP"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AuthGroup(ADTRAN_AUTH_GROUP value)
        {
            return CreateInteger(AdtranAttributeType.AUTH_GROUP, (int)value);
        }

        /// <summary>
        /// Creates an Adtran-Access-Level attribute (Type 4) with the specified CLI access level.
        /// </summary>
        /// <param name="value">The CLI access privilege level. See <see cref="ADTRAN_ACCESS_LEVEL"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AccessLevel(ADTRAN_ACCESS_LEVEL value)
        {
            return CreateInteger(AdtranAttributeType.ACCESS_LEVEL, (int)value);
        }

        /// <summary>
        /// Creates an Adtran-User-Privilege attribute (Type 7) with the specified privilege level.
        /// </summary>
        /// <param name="value">The user privilege level. See <see cref="ADTRAN_USER_PRIVILEGE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UserPrivilege(ADTRAN_USER_PRIVILEGE value)
        {
            return CreateInteger(AdtranAttributeType.USER_PRIVILEGE, (int)value);
        }

        /// <summary>
        /// Creates an Adtran-VPI attribute (Type 10) with the specified Virtual Path Identifier.
        /// </summary>
        /// <param name="value">The ATM Virtual Path Identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Vpi(int value)
        {
            return CreateInteger(AdtranAttributeType.VPI, value);
        }

        /// <summary>
        /// Creates an Adtran-VCI attribute (Type 11) with the specified Virtual Channel Identifier.
        /// </summary>
        /// <param name="value">The ATM Virtual Channel Identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Vci(int value)
        {
            return CreateInteger(AdtranAttributeType.VCI, value);
        }

        /// <summary>
        /// Creates an Adtran-Max-Sessions attribute (Type 12) with the specified maximum session count.
        /// </summary>
        /// <param name="value">The maximum concurrent sessions allowed.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSessions(int value)
        {
            return CreateInteger(AdtranAttributeType.MAX_SESSIONS, value);
        }

        /// <summary>
        /// Creates an Adtran-DHCP-Lease-Time attribute (Type 23) with the specified lease time.
        /// </summary>
        /// <param name="value">The DHCP lease time in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DhcpLeaseTime(int value)
        {
            return CreateInteger(AdtranAttributeType.DHCP_LEASE_TIME, value);
        }

        /// <summary>
        /// Creates an Adtran-Downstream-Rate attribute (Type 24) with the specified rate.
        /// </summary>
        /// <param name="value">The downstream data rate in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DownstreamRate(int value)
        {
            return CreateInteger(AdtranAttributeType.DOWNSTREAM_RATE, value);
        }

        /// <summary>
        /// Creates an Adtran-Upstream-Rate attribute (Type 25) with the specified rate.
        /// </summary>
        /// <param name="value">The upstream data rate in kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UpstreamRate(int value)
        {
            return CreateInteger(AdtranAttributeType.UPSTREAM_RATE, value);
        }

        /// <summary>
        /// Creates an Adtran-VLAN-Id attribute (Type 26) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier to assign.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(AdtranAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates an Adtran-Inner-VLAN-Id attribute (Type 27) with the specified inner VLAN identifier.
        /// </summary>
        /// <param name="value">The inner (QinQ) VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes InnerVlanId(int value)
        {
            return CreateInteger(AdtranAttributeType.INNER_VLAN_ID, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an Adtran-Agent-Circuit-Id attribute (Type 1) with the specified circuit ID.
        /// </summary>
        /// <param name="value">The Agent Circuit ID (Option 82 sub-option 1). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AgentCircuitId(string value)
        {
            return CreateString(AdtranAttributeType.AGENT_CIRCUIT_ID, value);
        }

        /// <summary>
        /// Creates an Adtran-Agent-Remote-Id attribute (Type 2) with the specified remote ID.
        /// </summary>
        /// <param name="value">The Agent Remote ID (Option 82 sub-option 2). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AgentRemoteId(string value)
        {
            return CreateString(AdtranAttributeType.AGENT_REMOTE_ID, value);
        }

        /// <summary>
        /// Creates an Adtran-Activate-Service attribute (Type 5) with the specified service name.
        /// </summary>
        /// <param name="value">The service to activate for the subscriber. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ActivateService(string value)
        {
            return CreateString(AdtranAttributeType.ACTIVATE_SERVICE, value);
        }

        /// <summary>
        /// Creates an Adtran-Deactivate-Service attribute (Type 6) with the specified service name.
        /// </summary>
        /// <param name="value">The service to deactivate for the subscriber. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DeactivateService(string value)
        {
            return CreateString(AdtranAttributeType.DEACTIVATE_SERVICE, value);
        }

        /// <summary>
        /// Creates an Adtran-QOS-Downstream attribute (Type 8) with the specified QoS policy name.
        /// </summary>
        /// <param name="value">The downstream QoS policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosDownstream(string value)
        {
            return CreateString(AdtranAttributeType.QOS_DOWNSTREAM, value);
        }

        /// <summary>
        /// Creates an Adtran-QOS-Upstream attribute (Type 9) with the specified QoS policy name.
        /// </summary>
        /// <param name="value">The upstream QoS policy name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosUpstream(string value)
        {
            return CreateString(AdtranAttributeType.QOS_UPSTREAM, value);
        }

        /// <summary>
        /// Creates an Adtran-Primary-DNS attribute (Type 13) with the specified DNS server address.
        /// </summary>
        /// <param name="value">The primary DNS server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PrimaryDns(string value)
        {
            return CreateString(AdtranAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates an Adtran-Secondary-DNS attribute (Type 14) with the specified DNS server address.
        /// </summary>
        /// <param name="value">The secondary DNS server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SecondaryDns(string value)
        {
            return CreateString(AdtranAttributeType.SECONDARY_DNS, value);
        }

        /// <summary>
        /// Creates an Adtran-IP-Pool-Name attribute (Type 15) with the specified pool name.
        /// </summary>
        /// <param name="value">The IP address pool name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IpPoolName(string value)
        {
            return CreateString(AdtranAttributeType.IP_POOL_NAME, value);
        }

        /// <summary>
        /// Creates an Adtran-Framed-Route attribute (Type 16) with the specified route entry.
        /// </summary>
        /// <param name="value">The framed route entry string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FramedRoute(string value)
        {
            return CreateString(AdtranAttributeType.FRAMED_ROUTE, value);
        }

        /// <summary>
        /// Creates an Adtran-Subscriber-Id attribute (Type 17) with the specified subscriber identifier.
        /// </summary>
        /// <param name="value">The subscriber identifier. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberId(string value)
        {
            return CreateString(AdtranAttributeType.SUBSCRIBER_ID, value);
        }

        /// <summary>
        /// Creates an Adtran-DHCP-Option attribute (Type 18) with the specified option string.
        /// </summary>
        /// <param name="value">The DHCP option string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpOption(string value)
        {
            return CreateString(AdtranAttributeType.DHCP_OPTION, value);
        }

        /// <summary>
        /// Creates an Adtran-Domain-Name attribute (Type 19) with the specified domain name.
        /// </summary>
        /// <param name="value">The domain name for the subscriber. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DomainName(string value)
        {
            return CreateString(AdtranAttributeType.DOMAIN_NAME, value);
        }

        /// <summary>
        /// Creates an Adtran-ANCP-String attribute (Type 20) with the specified ANCP string.
        /// </summary>
        /// <param name="value">The ANCP (Access Node Control Protocol) string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AncpString(string value)
        {
            return CreateString(AdtranAttributeType.ANCP_STRING, value);
        }

        /// <summary>
        /// Creates an Adtran-Service attribute (Type 21) with the specified service type.
        /// </summary>
        /// <param name="value">The service type string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Service(string value)
        {
            return CreateString(AdtranAttributeType.SERVICE, value);
        }

        /// <summary>
        /// Creates an Adtran-DHCP-Server attribute (Type 22) with the specified DHCP server address.
        /// </summary>
        /// <param name="value">The DHCP server address string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DhcpServer(string value)
        {
            return CreateString(AdtranAttributeType.DHCP_SERVER, value);
        }

        /// <summary>
        /// Creates an Adtran-Acl-Name attribute (Type 28) with the specified ACL name.
        /// </summary>
        /// <param name="value">The access control list name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value)
        {
            return CreateString(AdtranAttributeType.ACL_NAME, value);
        }

        /// <summary>
        /// Creates an Adtran-Encapsulation attribute (Type 29) with the specified encapsulation type.
        /// </summary>
        /// <param name="value">The encapsulation type string. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Encapsulation(string value)
        {
            return CreateString(AdtranAttributeType.ENCAPSULATION, value);
        }

        /// <summary>
        /// Creates an Adtran-IF-Name attribute (Type 30) with the specified interface name.
        /// </summary>
        /// <param name="value">The interface name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes IfName(string value)
        {
            return CreateString(AdtranAttributeType.IF_NAME, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified ADTRAN attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(AdtranAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified ADTRAN attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(AdtranAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}