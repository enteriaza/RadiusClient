using System.Net;

namespace Radius.Attributes
{
    /// <summary>
    /// Provides strongly typed, pre-built <see cref="RadiusAttributeDescriptor{T}"/>
    /// instances for all standard RADIUS attributes, enabling compile-time type safety
    /// when constructing attribute TLVs.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each field binds a <see cref="RadiusAttributeType"/> to its correct CLR value type
    /// and encoding strategy. Using these descriptors with
    /// <see cref="RadiusPacket.SetAttribute{T}"/> eliminates the possibility of encoding
    /// the wrong data type for an attribute — errors that would otherwise surface only
    /// at runtime as malformed RADIUS packets.
    /// </para>
    /// <para>
    /// <b>Example — compile-time safe attribute construction:</b>
    /// </para>
    /// <code>
    /// packet.SetAttribute(RadiusAttributeDescriptors.UserName, "alice@example.com");
    /// packet.SetAttribute(RadiusAttributeDescriptors.ServiceType, SERVICE_TYPE.FRAMED);
    /// packet.SetAttribute(RadiusAttributeDescriptors.NasIpAddress, IPAddress.Loopback);
    /// packet.SetAttribute(RadiusAttributeDescriptors.EventTimestamp, DateTime.UtcNow);
    /// </code>
    /// <para>
    /// Attributes not listed here (e.g. vendor-specific attributes, or attributes with
    /// complex tagged/fragmented wire formats like Tunnel-Type) should continue to use
    /// the existing <see cref="RadiusAttributes"/> constructors or specialised subclasses
    /// (<see cref="TunnelTypeAttributes"/>, <see cref="TunnelMediumTypeAttributes"/>,
    /// <see cref="VendorSpecificAttributes"/>) directly.
    /// </para>
    /// <para>
    /// <b>Naming convention:</b> Fields use PascalCase names that correspond to the
    /// SCREAMING_CASE <see cref="RadiusAttributeType"/> members, matching the canonical
    /// RFC attribute names (e.g. <c>UserName</c> for <see cref="RadiusAttributeType.USER_NAME"/>).
    /// </para>
    /// </remarks>
    public static class RadiusAttributeDescriptors
    {
        #region String Attributes (RFC 2865 §5 — "string" / "text" data type)

        /// <summary>User-Name (1). UTF-8 string identifying the user (RFC 2865 §5.1).</summary>
        public static readonly StringAttributeDescriptor UserName =
            new(RadiusAttributeType.USER_NAME);

        /// <summary>User-Password (2). PAP password obfuscated with MD5 (RFC 2865 §5.2). Encoded as raw UTF-8; caller is responsible for MD5 obfuscation.</summary>
        public static readonly StringAttributeDescriptor UserPassword =
            new(RadiusAttributeType.USER_PASSWORD);

        /// <summary>Filter-Id (11). Name of a filter list (RFC 2865 §5.11).</summary>
        public static readonly StringAttributeDescriptor FilterId =
            new(RadiusAttributeType.FILTER_ID);

        /// <summary>Reply-Message (18). Text displayed to the user (RFC 2865 §5.18).</summary>
        public static readonly StringAttributeDescriptor ReplyMessage =
            new(RadiusAttributeType.REPLY_MESSAGE);

        /// <summary>Callback-Number (19). Dialling string for callback (RFC 2865 §5.19).</summary>
        public static readonly StringAttributeDescriptor CallbackNumber =
            new(RadiusAttributeType.CALLBACK_NUMBER);

        /// <summary>Callback-Id (20). Name of a callback location (RFC 2865 §5.20).</summary>
        public static readonly StringAttributeDescriptor CallbackId =
            new(RadiusAttributeType.CALLBACK_ID);

        /// <summary>Framed-Route (22). Routing information for the session (RFC 2865 §5.22).</summary>
        public static readonly StringAttributeDescriptor FramedRoute =
            new(RadiusAttributeType.FRAMED_ROUTE);

        /// <summary>Called-Station-Id (30). Dialled phone number or MAC address (RFC 2865 §5.30).</summary>
        public static readonly StringAttributeDescriptor CalledStationId =
            new(RadiusAttributeType.CALLED_STATION_ID);

        /// <summary>Calling-Station-Id (31). Phone number or MAC address of the caller (RFC 2865 §5.31).</summary>
        public static readonly StringAttributeDescriptor CallingStationId =
            new(RadiusAttributeType.CALLING_STATION_ID);

        /// <summary>NAS-Identifier (32). String identifying the NAS (RFC 2865 §5.32).</summary>
        public static readonly StringAttributeDescriptor NasIdentifier =
            new(RadiusAttributeType.NAS_IDENTIFIER);

        /// <summary>Login-LAT-Service (34). System to connect the user to via LAT (RFC 2865 §5.34).</summary>
        public static readonly StringAttributeDescriptor LoginLatService =
            new(RadiusAttributeType.LOGIN_LAT_SERVICE);

        /// <summary>Login-LAT-Node (35). Node to connect the user to via LAT (RFC 2865 §5.35).</summary>
        public static readonly StringAttributeDescriptor LoginLatNode =
            new(RadiusAttributeType.LOGIN_LAT_NODE);

        /// <summary>Acct-Session-Id (44). Unique identifier for the accounting session (RFC 2866 §5.5).</summary>
        public static readonly StringAttributeDescriptor AcctSessionId =
            new(RadiusAttributeType.ACCT_SESSION_ID);

        /// <summary>Acct-Multi-Session-Id (50). Identifier linking multiple related sessions (RFC 2866 §5.11).</summary>
        public static readonly StringAttributeDescriptor AcctMultiSessionId =
            new(RadiusAttributeType.ACCT_MULTI_SESSION_ID);

        /// <summary>NAS-Port-Id (87). String identifying the NAS port (RFC 2869 §5.17).</summary>
        public static readonly StringAttributeDescriptor NasPortId =
            new(RadiusAttributeType.NAS_PORT_ID);

        /// <summary>Connect-Info (77). Connection speed and modulation description (RFC 2869 §5.11).</summary>
        public static readonly StringAttributeDescriptor ConnectInfo =
            new(RadiusAttributeType.CONNECT_INFO);

        /// <summary>Framed-Pool (88). Name of an assigned address pool (RFC 2869 §5.18).</summary>
        public static readonly StringAttributeDescriptor FramedPool =
            new(RadiusAttributeType.FRAMED_POOL);

        /// <summary>Framed-IPv6-Route (99). IPv6 routing information for the session (RFC 3162 §2.5).</summary>
        public static readonly StringAttributeDescriptor FramedIpv6Route =
            new(RadiusAttributeType.FRAMED_IPV6_ROUTE);

        /// <summary>Framed-IPv6-Pool (100). Name of an assigned IPv6 address pool (RFC 3162 §2.6).</summary>
        public static readonly StringAttributeDescriptor FramedIpv6Pool =
            new(RadiusAttributeType.FRAMED_IPV6_POOL);

        /// <summary>Tunnel-Client-Endpoint (66). Address of the tunnel initiator (RFC 2868 §3.3).</summary>
        public static readonly StringAttributeDescriptor TunnelClientEndpoint =
            new(RadiusAttributeType.TUNNEL_CLIENT_ENDPOINT);

        /// <summary>Tunnel-Server-Endpoint (67). Address of the tunnel server (RFC 2868 §3.4).</summary>
        public static readonly StringAttributeDescriptor TunnelServerEndpoint =
            new(RadiusAttributeType.TUNNEL_SERVER_ENDPOINT);

        /// <summary>Tunnel-Private-Group-ID (81). Group ID for the tunnel (RFC 2868 §3.6).</summary>
        public static readonly StringAttributeDescriptor TunnelPrivateGroupId =
            new(RadiusAttributeType.TUNNEL_PRIVATE_GROUP_ID);

        /// <summary>Tunnel-Assignment-ID (82). Identifier assigned to the tunnel (RFC 2868 §3.7).</summary>
        public static readonly StringAttributeDescriptor TunnelAssignmentId =
            new(RadiusAttributeType.TUNNEL_ASSIGNMENT_ID);

        /// <summary>Tunnel-Client-Auth-ID (90). Tunnel client authentication name (RFC 2868 §3.9).</summary>
        public static readonly StringAttributeDescriptor TunnelClientAuthId =
            new(RadiusAttributeType.TUNNEL_CLIENT_AUTH_ID);

        /// <summary>Tunnel-Server-Auth-ID (91). Tunnel server authentication name (RFC 2868 §3.10).</summary>
        public static readonly StringAttributeDescriptor TunnelServerAuthId =
            new(RadiusAttributeType.TUNNEL_SERVER_AUTH_ID);

        /// <summary>NAS-Filter-Rule (92). Packet filter rule for the NAS (RFC 4849 §2).</summary>
        public static readonly StringAttributeDescriptor NasFilterRule =
            new(RadiusAttributeType.NAS_FILTER_RULE);

        #endregion

        #region Integer Attributes (RFC 2865 §5 — "integer" data type, 32-bit big-endian)

        /// <summary>NAS-Port (5). Physical port number on the NAS (RFC 2865 §5.5).</summary>
        public static readonly IntegerAttributeDescriptor NasPort =
            new(RadiusAttributeType.NAS_PORT);

        /// <summary>Framed-MTU (12). Maximum Transmission Unit (RFC 2865 §5.12).</summary>
        public static readonly IntegerAttributeDescriptor FramedMtu =
            new(RadiusAttributeType.FRAMED_MTU);

        /// <summary>Login-TCP-Port (16). TCP port to connect the user to (RFC 2865 §5.16).</summary>
        public static readonly IntegerAttributeDescriptor LoginTcpPort =
            new(RadiusAttributeType.LOGIN_TCP_PORT);

        /// <summary>Framed-IPX-Network (23). IPX network to route to the user (RFC 2865 §5.23).</summary>
        public static readonly IntegerAttributeDescriptor FramedIpxNetwork =
            new(RadiusAttributeType.FRAMED_IPX_NETWORK);

        /// <summary>Session-Timeout (27). Maximum seconds for the session (RFC 2865 §5.27).</summary>
        public static readonly IntegerAttributeDescriptor SessionTimeout =
            new(RadiusAttributeType.SESSION_TIMEOUT);

        /// <summary>Idle-Timeout (28). Maximum idle seconds before termination (RFC 2865 §5.28).</summary>
        public static readonly IntegerAttributeDescriptor IdleTimeout =
            new(RadiusAttributeType.IDLE_TIMEOUT);

        /// <summary>Port-Limit (62). Maximum ports NAS should provide (RFC 2865 §5.42).</summary>
        public static readonly IntegerAttributeDescriptor PortLimit =
            new(RadiusAttributeType.PORT_LIMIT);

        /// <summary>Framed-AppleTalk-Link (37). AppleTalk network link (RFC 2865 §5.37).</summary>
        public static readonly IntegerAttributeDescriptor FramedAppleTalkLink =
            new(RadiusAttributeType.FRAMED_APPLETALK_LINK);

        /// <summary>Framed-AppleTalk-Network (38). AppleTalk network to route (RFC 2865 §5.38).</summary>
        public static readonly IntegerAttributeDescriptor FramedAppleTalkNetwork =
            new(RadiusAttributeType.FRAMED_APPLETALK_NETWORK);

        /// <summary>Acct-Delay-Time (41). Seconds the NAS has been sending the accounting packet (RFC 2866 §5.2).</summary>
        public static readonly IntegerAttributeDescriptor AcctDelayTime =
            new(RadiusAttributeType.ACCT_DELAY_TIME);

        /// <summary>Acct-Input-Octets (42). Octets received from the port (RFC 2866 §5.3).</summary>
        public static readonly IntegerAttributeDescriptor AcctInputOctets =
            new(RadiusAttributeType.ACCT_INPUT_OCTETS);

        /// <summary>Acct-Output-Octets (43). Octets sent to the port (RFC 2866 §5.4).</summary>
        public static readonly IntegerAttributeDescriptor AcctOutputOctets =
            new(RadiusAttributeType.ACCT_OUTPUT_OCTETS);

        /// <summary>Acct-Session-Time (46). Seconds of service (RFC 2866 §5.7).</summary>
        public static readonly IntegerAttributeDescriptor AcctSessionTime =
            new(RadiusAttributeType.ACCT_SESSION_TIME);

        /// <summary>Acct-Input-Packets (47). Packets received from the port (RFC 2866 §5.8).</summary>
        public static readonly IntegerAttributeDescriptor AcctInputPackets =
            new(RadiusAttributeType.ACCT_INPUT_PACKETS);

        /// <summary>Acct-Output-Packets (48). Packets sent to the port (RFC 2866 §5.9).</summary>
        public static readonly IntegerAttributeDescriptor AcctOutputPackets =
            new(RadiusAttributeType.ACCT_OUTPUT_PACKETS);

        /// <summary>Acct-Link-Count (51). Number of links in a multi-link session (RFC 2866 §5.12).</summary>
        public static readonly IntegerAttributeDescriptor AcctLinkCount =
            new(RadiusAttributeType.ACCT_LINK_COUNT);

        /// <summary>Acct-Input-Gigawords (52). Gigaword wrap counter for input octets (RFC 2869 §5.1).</summary>
        public static readonly IntegerAttributeDescriptor AcctInputGigawords =
            new(RadiusAttributeType.ACCT_INPUT_GIGAWORDS);

        /// <summary>Acct-Output-Gigawords (53). Gigaword wrap counter for output octets (RFC 2869 §5.2).</summary>
        public static readonly IntegerAttributeDescriptor AcctOutputGigawords =
            new(RadiusAttributeType.ACCT_OUTPUT_GIGAWORDS);

        /// <summary>Acct-Interim-Interval (85). Seconds between interim accounting updates (RFC 2869 §5.16).</summary>
        public static readonly IntegerAttributeDescriptor AcctInterimInterval =
            new(RadiusAttributeType.ACCT_INTERIM_INTERVAL);

        /// <summary>Acct-Tunnel-Packets-Lost (86). Packets lost on a tunnel (RFC 2867 §4.2).</summary>
        public static readonly IntegerAttributeDescriptor AcctTunnelPacketsLost =
            new(RadiusAttributeType.ACCT_TUNNEL_PACKETS_LOST);

        /// <summary>Tunnel-Preference (83). Preference for a tunnel (RFC 2868 §3.8).</summary>
        public static readonly IntegerAttributeDescriptor TunnelPreference =
            new(RadiusAttributeType.TUNNEL_PREFERENCE);

        /// <summary>Management-Privilege-Level (136). Privilege level for management access (RFC 5607 §4.4).</summary>
        public static readonly IntegerAttributeDescriptor ManagementPrivilegeLevel =
            new(RadiusAttributeType.MANAGEMENT_PRIVILEGE_LEVEL);

        #endregion

        #region 64-bit Integer Attributes (RFC 8044 §3.3)

        /// <summary>MIP6-Feature-Vector (124). Mobile IPv6 feature flags (RFC 5447 §4.2.2).</summary>
        public static readonly Integer64AttributeDescriptor Mip6FeatureVector =
            new(RadiusAttributeType.MIP6_FEATURE_VECTOR);

        #endregion

        #region IP Address Attributes (RFC 2865 §5 — "address" data type)

        /// <summary>NAS-IP-Address (4). IPv4 address of the NAS (RFC 2865 §5.4).</summary>
        public static readonly IPAddressAttributeDescriptor NasIpAddress =
            new(RadiusAttributeType.NAS_IP_ADDRESS);

        /// <summary>Framed-IP-Address (8). IPv4 address to assign to the user (RFC 2865 §5.8).</summary>
        public static readonly IPAddressAttributeDescriptor FramedIpAddress =
            new(RadiusAttributeType.FRAMED_IP_ADDRESS);

        /// <summary>Framed-IP-Netmask (9). Subnet mask to assign to the user (RFC 2865 §5.9).</summary>
        public static readonly IPAddressAttributeDescriptor FramedIpNetmask =
            new(RadiusAttributeType.FRAMED_IP_NETMASK);

        /// <summary>Login-IP-Host (14). Host to connect the user to (RFC 2865 §5.14).</summary>
        public static readonly IPAddressAttributeDescriptor LoginIpHost =
            new(RadiusAttributeType.LOGIN_IP_HOST);

        /// <summary>NAS-IPv6-Address (95). IPv6 address of the NAS (RFC 3162 §2.1).</summary>
        public static readonly IPAddressAttributeDescriptor NasIpv6Address =
            new(RadiusAttributeType.NAS_IPV6_ADDRESS);

        /// <summary>Login-IPv6-Host (98). IPv6 host to connect the user to (RFC 3162 §2.4).</summary>
        public static readonly IPAddressAttributeDescriptor LoginIpv6Host =
            new(RadiusAttributeType.LOGIN_IPV6_HOST);

        /// <summary>Framed-IPv6-Address (168). IPv6 address to assign to the user (RFC 6911 §3.1).</summary>
        public static readonly IPAddressAttributeDescriptor FramedIpv6Address =
            new(RadiusAttributeType.FRAMED_IPV6_ADDRESS);

        /// <summary>DNS-Server-IPv6-Address (169). IPv6 address of the DNS server (RFC 6911 §3.2).</summary>
        public static readonly IPAddressAttributeDescriptor DnsServerIpv6Address =
            new(RadiusAttributeType.DNS_SERVER_IPV6_ADDRESS);

        #endregion

        #region Date/Time Attributes (RFC 2869 §5.3 — 32-bit Unix timestamp)

        /// <summary>Event-Timestamp (55). Unix timestamp of the session event (RFC 2869 §5.3).</summary>
        public static readonly DateTimeAttributeDescriptor EventTimestamp =
            new(RadiusAttributeType.EVENT_TIMESTAMP);

        #endregion

        #region Octet String (Raw Byte) Attributes

        /// <summary>CHAP-Password (3). CHAP response value (RFC 2865 §5.3).</summary>
        public static readonly OctetStringAttributeDescriptor ChapPassword =
            new(RadiusAttributeType.CHAP_PASSWORD);

        /// <summary>State (24). Arbitrary state for Access-Challenge linkage (RFC 2865 §5.24).</summary>
        public static readonly OctetStringAttributeDescriptor State =
            new(RadiusAttributeType.STATE);

        /// <summary>Class (25). Opaque data echoed back in accounting (RFC 2865 §5.25).</summary>
        public static readonly OctetStringAttributeDescriptor Class =
            new(RadiusAttributeType.CLASS);

        /// <summary>Proxy-State (33). Opaque data maintained by proxy servers (RFC 2865 §5.33).</summary>
        public static readonly OctetStringAttributeDescriptor ProxyState =
            new(RadiusAttributeType.PROXY_STATE);

        /// <summary>Login-LAT-Group (36). LAT group codes (RFC 2865 §5.36).</summary>
        public static readonly OctetStringAttributeDescriptor LoginLatGroup =
            new(RadiusAttributeType.LOGIN_LAT_GROUP);

        /// <summary>CHAP-Challenge (60). CHAP challenge value (RFC 2865 §5.40).</summary>
        public static readonly OctetStringAttributeDescriptor ChapChallenge =
            new(RadiusAttributeType.CHAP_CHALLENGE);

        /// <summary>EAP-Message (79). Encapsulated EAP packet (RFC 3579 §3.1).</summary>
        public static readonly OctetStringAttributeDescriptor EapMessage =
            new(RadiusAttributeType.EAP_MESSAGE);

        /// <summary>Framed-AppleTalk-Zone (39). AppleTalk default zone (RFC 2865 §5.39).</summary>
        public static readonly OctetStringAttributeDescriptor FramedAppleTalkZone =
            new(RadiusAttributeType.FRAMED_APPLETALK_ZONE);

        /// <summary>CUI (89). Chargeable-User-Identity (RFC 4372 §2).</summary>
        public static readonly OctetStringAttributeDescriptor Cui =
            new(RadiusAttributeType.CUI);

        /// <summary>Framed-Interface-Id (96). IPv6 interface identifier (RFC 3162 §2.2).</summary>
        public static readonly OctetStringAttributeDescriptor FramedInterfaceId =
            new(RadiusAttributeType.FRAMED_INTERFACE_ID);

        /// <summary>Framed-IPv6-Prefix (97). IPv6 prefix to assign (RFC 3162 §2.3).</summary>
        public static readonly OctetStringAttributeDescriptor FramedIpv6Prefix =
            new(RadiusAttributeType.FRAMED_IPV6_PREFIX);

        #endregion

        #region Enumerated Attributes (RFC 2865 §5 — "integer" data type with defined value set)

        /// <summary>Service-Type (6). Type of service requested or provided (RFC 2865 §5.6).</summary>
        public static readonly EnumAttributeDescriptor<SERVICE_TYPE> ServiceType =
            new(RadiusAttributeType.SERVICE_TYPE);

        /// <summary>Framed-Protocol (7). Framing protocol to use (RFC 2865 §5.7).</summary>
        public static readonly EnumAttributeDescriptor<FRAMED_PROTOCOL> FramedProtocol =
            new(RadiusAttributeType.FRAMED_PROTOCOL);

        /// <summary>Framed-Routing (10). Routing method for the user (RFC 2865 §5.10).</summary>
        public static readonly EnumAttributeDescriptor<FRAMED_ROUTING> FramedRouting =
            new(RadiusAttributeType.FRAMED_ROUTING);

        /// <summary>Framed-Compression (13). Compression protocol to use (RFC 2865 §5.13).</summary>
        public static readonly EnumAttributeDescriptor<FRAMED_COMPRESSION> FramedCompression =
            new(RadiusAttributeType.FRAMED_COMPRESSION);

        /// <summary>Login-Service (15). Service to connect the user to (RFC 2865 §5.15).</summary>
        public static readonly EnumAttributeDescriptor<LOGIN_SERVICE> LoginService =
            new(RadiusAttributeType.LOGIN_SERVICE);

        /// <summary>Termination-Action (29). Action on session termination (RFC 2865 §5.29).</summary>
        public static readonly EnumAttributeDescriptor<TERMINATION_ACTION> TerminationAction =
            new(RadiusAttributeType.TERMINATION_ACTION);

        /// <summary>Acct-Status-Type (40). Accounting record type (RFC 2866 §5.1).</summary>
        public static readonly EnumAttributeDescriptor<ACCT_STATUS_TYPE> AcctStatusType =
            new(RadiusAttributeType.ACCT_STATUS_TYPE);

        /// <summary>Acct-Authentic (45). How the user was authenticated (RFC 2866 §5.6).</summary>
        public static readonly EnumAttributeDescriptor<ACCT_AUTHENTIC> AcctAuthentic =
            new(RadiusAttributeType.ACCT_AUTHENTIC);

        /// <summary>Acct-Terminate-Cause (49). Reason the session was terminated (RFC 2866 §5.10).</summary>
        public static readonly EnumAttributeDescriptor<ACCT_TERMINATE_CAUSE> AcctTerminateCause =
            new(RadiusAttributeType.ACCT_TERMINATE_CAUSE);

        /// <summary>NAS-Port-Type (61). Physical port type (RFC 2865 §5.41).</summary>
        public static readonly EnumAttributeDescriptor<NAS_PORT_TYPE> NasPortType =
            new(RadiusAttributeType.NAS_PORT_TYPE);

        /// <summary>ARAP-Zone-Access (72). Zone access restrictions for ARAP (RFC 2869 §5.6).</summary>
        public static readonly EnumAttributeDescriptor<ARAP_ZONE_ACCESS> ArapZoneAccess =
            new(RadiusAttributeType.ARAP_ZONE_ACCESS);

        /// <summary>Prompt (76). Whether the NAS should echo user input (RFC 2869 §5.10).</summary>
        public static readonly EnumAttributeDescriptor<PROMPT> Prompt =
            new(RadiusAttributeType.PROMPT);

        /// <summary>Ingress-Filters (57). Ingress filter setting (RFC 4675 §2.2).</summary>
        public static readonly EnumAttributeDescriptor<INGRESS_FILTERS_VALUE> IngressFilters =
            new(RadiusAttributeType.INGRESS_FILTERS);

        /// <summary>Error-Cause (101). Error code for CoA/Disconnect responses (RFC 5176 §3.6).</summary>
        public static readonly EnumAttributeDescriptor<ERROR_CAUSE> ErrorCause =
            new(RadiusAttributeType.ERROR_CAUSE);

        /// <summary>Framed-Management-Protocol (133). Management protocol (RFC 5607 §4.1).</summary>
        public static readonly EnumAttributeDescriptor<FRAMED_MANAGEMENT_PROTOCOL> FramedManagementProtocol =
            new(RadiusAttributeType.FRAMED_MANAGEMENT_PROTOCOL);

        /// <summary>Management-Transport-Protection (134). Transport security level (RFC 5607 §4.2).</summary>
        public static readonly EnumAttributeDescriptor<MANAGEMENT_TRANSPORT_PROTECTION> ManagementTransportProtection =
            new(RadiusAttributeType.MANAGEMENT_TRANSPORT_PROTECTION);

        /// <summary>EAP-Lower-Layer (163). EAP lower-layer type (RFC 7057 §4.1).</summary>
        public static readonly EnumAttributeDescriptor<EAP_LOWER_LAYER> EapLowerLayer =
            new(RadiusAttributeType.EAP_LOWER_LAYER);

        #endregion
    }
}