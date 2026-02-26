namespace Radius
{
    /// <summary>
    /// Identifies the RADIUS packet type as defined in RFC 2865 §3 (octet 1 of the packet header).
    /// </summary>
    /// <remarks>
    /// Core access codes are defined in RFC 2865; accounting codes in RFC 2866;
    /// CoA and Disconnect codes in RFC 3576/RFC 5176.
    /// Values are narrowed to <see cref="byte"/> via explicit cast when written to the wire.
    /// </remarks>
    public enum RadiusCode
    {
        /// <summary>Access-Request packet (RFC 2865 §4.1). Sent by NAS to RADIUS server.</summary>
        ACCESS_REQUEST = 1,

        /// <summary>Access-Accept packet (RFC 2865 §4.2). Sent by RADIUS server on authentication success.</summary>
        ACCESS_ACCEPT = 2,

        /// <summary>Access-Reject packet (RFC 2865 §4.3). Sent by RADIUS server on authentication failure.</summary>
        ACCESS_REJECT = 3,

        /// <summary>Accounting-Request packet (RFC 2866 §4.1). Sent by NAS to report accounting data.</summary>
        ACCOUNTING_REQUEST = 4,

        /// <summary>Accounting-Response packet (RFC 2866 §4.2). Sent by RADIUS server to acknowledge accounting.</summary>
        ACCOUNTING_RESPONSE = 5,

        /// <summary>Accounting-Status packet (Livingston extension, deprecated; see RFC 2866).</summary>
        ACCOUNTING_STATUS = 6,

        /// <summary>Password-Request packet (Livingston extension, deprecated).</summary>
        PASSWORD_REQUEST = 7,

        /// <summary>Password-Accept packet (Livingston extension, deprecated).</summary>
        PASSWORD_ACCEPT = 8,

        /// <summary>Password-Reject packet (Livingston extension, deprecated).</summary>
        PASSWORD_REJECT = 9,

        /// <summary>Accounting-Message packet (Livingston extension, deprecated).</summary>
        ACCOUNTING_MESSAGE = 10,

        /// <summary>Access-Challenge packet (RFC 2865 §4.4). Sent by RADIUS server to initiate a challenge-response exchange.</summary>
        ACCESS_CHALLENGE = 11,

        /// <summary>Status-Server packet (RFC 5997). Used for server liveness probing.</summary>
        SERVER_STATUS = 12,

        /// <summary>Status-Client packet (RFC 5997). Used for client liveness probing.</summary>
        CLIENT_STATUS = 13,

        /// <summary>Resource-Free-Request packet (Ascend extension).</summary>
        RESOURCE_FREE_REQUEST = 21,

        /// <summary>Resource-Free-Response packet (Ascend extension).</summary>
        RESOURCE_FREE_RESPONSE = 22,

        /// <summary>Resource-Query-Request packet (Ascend extension).</summary>
        RESOURCE_QUERY_REQUEST = 23,

        /// <summary>Resource-Query-Response packet (Ascend extension).</summary>
        RESOURCE_QUERY_RESPONSE = 24,

        /// <summary>Alternate-Resource-Reclaim-Request packet (Ascend extension).</summary>
        ALTERNATE_RESOURCE_RECLAIM_REQUEST = 25,

        /// <summary>NAS-Reboot-Request packet (Ascend extension).</summary>
        NAS_REBOOT_REQUEST = 26,

        /// <summary>NAS-Reboot-Response packet (Ascend extension).</summary>
        NAS_REBOOT_RESPONSE = 27,

        /// <summary>Next-Passcode packet (Livingston extension).</summary>
        NEXT_PASSCODE = 29,

        /// <summary>New-Pin packet (Livingston extension).</summary>
        NEW_PIN = 30,

        /// <summary>Terminate-Session packet (Livingston extension).</summary>
        TERMINATE_SESSION = 31,

        /// <summary>Password-Expired packet (Livingston extension).</summary>
        PASSWORD_EXPIRED = 32,

        /// <summary>Event-Request packet (Livingston extension).</summary>
        EVENT_REQUEST = 33,

        /// <summary>Event-Response packet (Livingston extension).</summary>
        EVENT_RESPONSE = 34,

        /// <summary>Disconnect-Request packet (RFC 5176 §3). Sent by RADIUS server to terminate a user session.</summary>
        DISCONNECT_REQUEST = 40,

        /// <summary>Disconnect-ACK packet (RFC 5176 §3). Sent by NAS to confirm session termination.</summary>
        DISCONNECT_ACK = 41,

        /// <summary>Disconnect-NACK packet (RFC 5176 §3). Sent by NAS when session termination fails.</summary>
        DISCONNECT_NACK = 42,

        /// <summary>CoA-Request packet (RFC 5176 §3). Sent by RADIUS server to change session authorisation.</summary>
        COA_REQUEST = 43,

        /// <summary>CoA-ACK packet (RFC 5176 §3). Sent by NAS to confirm a CoA-Request was applied.</summary>
        COA_ACK = 44,

        /// <summary>CoA-NACK packet (RFC 5176 §3). Sent by NAS when a CoA-Request could not be applied.</summary>
        COA_NACK = 45,

        /// <summary>IP-Address-Allocate packet (vendor extension).</summary>
        IP_ADDRESS_ALLOCATE = 50,

        /// <summary>IP-Address-Release packet (vendor extension).</summary>
        IP_ADDRESS_RELEASE = 51,

        /// <summary>Protocol-Error packet (RFC 7930). Indicates a protocol-level error.</summary>
        PROTOCOL_ERROR = 52
    }

    /// <summary>
    /// Identifies the RADIUS attribute type as defined in RFC 2865 §5 (octet 1 of each attribute TLV).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Standard attributes are defined in RFC 2865 and RFC 2866. Extended attributes follow
    /// RFC 6929. Attributes marked <c>// ENUM</c> have a corresponding enum type in this file.
    /// Values are narrowed to <see cref="byte"/> via explicit cast when written to the wire.
    /// </para>
    /// <para>
    /// The IANA RADIUS Types registry (https://www.iana.org/assignments/radius-types)
    /// is the authoritative source for attribute type assignments. This enum covers
    /// types 1–246, which are the standard and extended attribute spaces. Types 247–255
    /// are reserved per RFC 6929 §10.
    /// </para>
    /// </remarks>
    public enum RadiusAttributeType
    {
        /// <summary>User-Name attribute (RFC 2865 §5.1). UTF-8 string identifying the user.</summary>
        USER_NAME = 1,

        /// <summary>User-Password attribute (RFC 2865 §5.2). PAP password obfuscated with MD5.</summary>
        USER_PASSWORD = 2,

        /// <summary>CHAP-Password attribute (RFC 2865 §5.3). CHAP response value.</summary>
        CHAP_PASSWORD = 3,

        /// <summary>NAS-IP-Address attribute (RFC 2865 §5.4). IPv4 address of the NAS.</summary>
        NAS_IP_ADDRESS = 4,

        /// <summary>NAS-Port attribute (RFC 2865 §5.5). Physical port number on the NAS.</summary>
        NAS_PORT = 5,

        /// <summary>Service-Type attribute (RFC 2865 §5.6). Type of service requested or provided. See <see cref="SERVICE_TYPE"/>.</summary>
        SERVICE_TYPE = 6,                           // ENUM

        /// <summary>Framed-Protocol attribute (RFC 2865 §5.7). Framing protocol to use. See <see cref="FRAMED_PROTOCOL"/>.</summary>
        FRAMED_PROTOCOL = 7,                        // ENUM

        /// <summary>Framed-IP-Address attribute (RFC 2865 §5.8). IPv4 address to assign to the user.</summary>
        FRAMED_IP_ADDRESS = 8,

        /// <summary>Framed-IP-Netmask attribute (RFC 2865 §5.9). Subnet mask to assign to the user.</summary>
        FRAMED_IP_NETMASK = 9,

        /// <summary>Framed-Routing attribute (RFC 2865 §5.10). Routing method for the user. See <see cref="FRAMED_ROUTING"/>.</summary>
        FRAMED_ROUTING = 10,                        // ENUM

        /// <summary>Filter-Id attribute (RFC 2865 §5.11). Name of a filter list.</summary>
        FILTER_ID = 11,

        /// <summary>Framed-MTU attribute (RFC 2865 §5.12). Maximum Transmission Unit for the connection.</summary>
        FRAMED_MTU = 12,

        /// <summary>Framed-Compression attribute (RFC 2865 §5.13). Compression protocol to use. See <see cref="FRAMED_COMPRESSION"/>.</summary>
        FRAMED_COMPRESSION = 13,                    // ENUM

        /// <summary>Login-IP-Host attribute (RFC 2865 §5.14). Host to connect the user to.</summary>
        LOGIN_IP_HOST = 14,

        /// <summary>Login-Service attribute (RFC 2865 §5.15). Service to connect the user to. See <see cref="LOGIN_SERVICE"/>.</summary>
        LOGIN_SERVICE = 15,                         // ENUM

        /// <summary>Login-TCP-Port attribute (RFC 2865 §5.16). TCP port to connect the user to.</summary>
        LOGIN_TCP_PORT = 16,

        // Type 17 is unassigned per IANA RADIUS Types registry.

        /// <summary>Reply-Message attribute (RFC 2865 §5.18). Text that may be displayed to the user.</summary>
        REPLY_MESSAGE = 18,

        /// <summary>Callback-Number attribute (RFC 2865 §5.19). Dialling string for callback.</summary>
        CALLBACK_NUMBER = 19,

        /// <summary>Callback-Id attribute (RFC 2865 §5.20). Name of a callback location.</summary>
        CALLBACK_ID = 20,

        // Type 21 is unassigned per IANA RADIUS Types registry.

        /// <summary>Framed-Route attribute (RFC 2865 §5.22). Routing information for the user's session.</summary>
        FRAMED_ROUTE = 22,

        /// <summary>Framed-IPX-Network attribute (RFC 2865 §5.23). IPX network to route to the user.</summary>
        FRAMED_IPX_NETWORK = 23,

        /// <summary>State attribute (RFC 2865 §5.24). Arbitrary state used to link Access-Challenge and response packets.</summary>
        STATE = 24,

        /// <summary>Class attribute (RFC 2865 §5.25). Sent by RADIUS server; returned unchanged by NAS in accounting packets.</summary>
        CLASS = 25,

        /// <summary>Vendor-Specific attribute (RFC 2865 §5.26). Container for vendor-defined sub-attributes.</summary>
        VENDOR_SPECIFIC = 26,

        /// <summary>Session-Timeout attribute (RFC 2865 §5.27). Maximum number of seconds for the session.</summary>
        SESSION_TIMEOUT = 27,

        /// <summary>Idle-Timeout attribute (RFC 2865 §5.28). Maximum idle seconds before session termination.</summary>
        IDLE_TIMEOUT = 28,

        /// <summary>Termination-Action attribute (RFC 2865 §5.29). Action to take when the session ends. See <see cref="TERMINATION_ACTION"/>.</summary>
        TERMINATION_ACTION = 29,                    // ENUM

        /// <summary>Called-Station-Id attribute (RFC 2865 §5.30). Dialled phone number or MAC address.</summary>
        CALLED_STATION_ID = 30,

        /// <summary>Calling-Station-Id attribute (RFC 2865 §5.31). Phone number or MAC address of the caller.</summary>
        CALLING_STATION_ID = 31,

        /// <summary>NAS-Identifier attribute (RFC 2865 §5.32). String identifying the NAS.</summary>
        NAS_IDENTIFIER = 32,

        /// <summary>Proxy-State attribute (RFC 2865 §5.33). Used by proxy servers to maintain state.</summary>
        PROXY_STATE = 33,

        /// <summary>Login-LAT-Service attribute (RFC 2865 §5.34). System to connect the user to via LAT.</summary>
        LOGIN_LAT_SERVICE = 34,

        /// <summary>Login-LAT-Node attribute (RFC 2865 §5.35). Node to connect the user to via LAT.</summary>
        LOGIN_LAT_NODE = 35,

        /// <summary>Login-LAT-Group attribute (RFC 2865 §5.36). LAT group codes the user is authorised to connect to.</summary>
        LOGIN_LAT_GROUP = 36,

        /// <summary>Framed-AppleTalk-Link attribute (RFC 2865 §5.37). AppleTalk network link for the user.</summary>
        FRAMED_APPLETALK_LINK = 37,

        /// <summary>Framed-AppleTalk-Network attribute (RFC 2865 §5.38). AppleTalk network to route to the user.</summary>
        FRAMED_APPLETALK_NETWORK = 38,

        /// <summary>Framed-AppleTalk-Zone attribute (RFC 2865 §5.39). AppleTalk default zone for the user.</summary>
        FRAMED_APPLETALK_ZONE = 39,

        /// <summary>Acct-Status-Type attribute (RFC 2866 §5.1). Accounting record type. See <see cref="ACCT_STATUS_TYPE"/>.</summary>
        ACCT_STATUS_TYPE = 40,                      // ENUM

        /// <summary>Acct-Delay-Time attribute (RFC 2866 §5.2). Seconds the NAS has been sending the accounting packet.</summary>
        ACCT_DELAY_TIME = 41,

        /// <summary>Acct-Input-Octets attribute (RFC 2866 §5.3). Octets received from the port over the session.</summary>
        ACCT_INPUT_OCTETS = 42,

        /// <summary>Acct-Output-Octets attribute (RFC 2866 §5.4). Octets sent to the port over the session.</summary>
        ACCT_OUTPUT_OCTETS = 43,

        /// <summary>Acct-Session-Id attribute (RFC 2866 §5.5). Unique identifier for the accounting session.</summary>
        ACCT_SESSION_ID = 44,

        /// <summary>Acct-Authentic attribute (RFC 2866 §5.6). How the user was authenticated. See <see cref="ACCT_AUTHENTIC"/>.</summary>
        ACCT_AUTHENTIC = 45,                        // ENUM

        /// <summary>Acct-Session-Time attribute (RFC 2866 §5.7). Seconds the user has been receiving service.</summary>
        ACCT_SESSION_TIME = 46,

        /// <summary>Acct-Input-Packets attribute (RFC 2866 §5.8). Packets received from the port over the session.</summary>
        ACCT_INPUT_PACKETS = 47,

        /// <summary>Acct-Output-Packets attribute (RFC 2866 §5.9). Packets sent to the port over the session.</summary>
        ACCT_OUTPUT_PACKETS = 48,

        /// <summary>Acct-Terminate-Cause attribute (RFC 2866 §5.10). Reason the session was terminated. See <see cref="ACCT_TERMINATE_CAUSE"/>.</summary>
        ACCT_TERMINATE_CAUSE = 49,                  // ENUM

        /// <summary>Acct-Multi-Session-Id attribute (RFC 2866 §5.11). Identifier linking multiple related sessions.</summary>
        ACCT_MULTI_SESSION_ID = 50,

        /// <summary>Acct-Link-Count attribute (RFC 2866 §5.12). Number of links in a multi-link session.</summary>
        ACCT_LINK_COUNT = 51,

        /// <summary>Acct-Input-Gigawords attribute (RFC 2869 §5.1). How many times Acct-Input-Octets has wrapped around 2^32.</summary>
        ACCT_INPUT_GIGAWORDS = 52,

        /// <summary>Acct-Output-Gigawords attribute (RFC 2869 §5.2). How many times Acct-Output-Octets has wrapped around 2^32.</summary>
        ACCT_OUTPUT_GIGAWORDS = 53,

        // Type 54 is unassigned per IANA RADIUS Types registry.

        /// <summary>Event-Timestamp attribute (RFC 2869 §5.3). Unix timestamp of when the session event occurred.</summary>
        EVENT_TIMESTAMP = 55,

        /// <summary>Egress-VLANID attribute (RFC 4675 §2.1). VLAN identifier for egress traffic.</summary>
        EGRESS_VLANID = 56,

        /// <summary>Ingress-Filters attribute (RFC 4675 §2.2). Whether ingress filters are enabled on the port. See <see cref="INGRESS_FILTERS_VALUE"/>.</summary>
        INGRESS_FILTERS = 57,                       // ENUM

        /// <summary>Egress-VLAN-Name attribute (RFC 4675 §2.3). Name of the VLAN for egress traffic.</summary>
        EGRESS_VLAN_NAME = 58,

        /// <summary>User-Priority-Table attribute (RFC 4675 §2.4). 802.1D priority table for the port.</summary>
        USER_PRIORITY_TABLE = 59,

        /// <summary>CHAP-Challenge attribute (RFC 2865 §5.40). CHAP challenge value sent by the NAS.</summary>
        CHAP_CHALLENGE = 60,

        /// <summary>NAS-Port-Type attribute (RFC 2865 §5.41). Physical port type the user is connected to. See <see cref="NAS_PORT_TYPE"/>.</summary>
        NAS_PORT_TYPE = 61,                         // ENUM

        /// <summary>Port-Limit attribute (RFC 2865 §5.42). Maximum number of ports NAS should provide to the user.</summary>
        PORT_LIMIT = 62,

        /// <summary>Login-LAT-Port attribute (RFC 2865 §5.43). Port with which to connect the user via LAT.</summary>
        LOGIN_LAT_PORT = 63,

        /// <summary>Tunnel-Type attribute (RFC 2868 §3.1). Tunnelling protocol to use. See <see cref="TUNNEL_TYPE"/>.</summary>
        TUNNEL_TYPE = 64,                           // ENUM

        /// <summary>Tunnel-Medium-Type attribute (RFC 2868 §3.2). Transport medium to use when creating a tunnel. See <see cref="TUNNEL_MEDIUM_TYPE"/>.</summary>
        TUNNEL_MEDIUM_TYPE = 65,                    // ENUM

        /// <summary>Tunnel-Client-Endpoint attribute (RFC 2868 §3.3). Address of the initiator of the tunnel.</summary>
        TUNNEL_CLIENT_ENDPOINT = 66,

        /// <summary>Tunnel-Server-Endpoint attribute (RFC 2868 §3.4). Address of the server end of the tunnel.</summary>
        TUNNEL_SERVER_ENDPOINT = 67,

        /// <summary>Acct-Tunnel-Connection attribute (RFC 2867 §4.1). Identifier for the tunnel session.</summary>
        ACCT_TUNNEL_CONNECTION = 68,

        /// <summary>Tunnel-Password attribute (RFC 2868 §3.5). Password for authenticating to a tunnel server.</summary>
        TUNNEL_PASSWORD = 69,

        /// <summary>ARAP-Password attribute (RFC 2869 §5.4). ARAP authentication data.</summary>
        ARAP_PASSWORD = 70,

        /// <summary>ARAP-Features attribute (RFC 2869 §5.5). Information for ARAP authentication.</summary>
        ARAP_FEATURES = 71,

        /// <summary>ARAP-Zone-Access attribute (RFC 2869 §5.6). Zone access restrictions for ARAP. See <see cref="ARAP_ZONE_ACCESS"/>.</summary>
        ARAP_ZONE_ACCESS = 72,                      // ENUM

        /// <summary>ARAP-Security attribute (RFC 2869 §5.7). Security module to use for ARAP.</summary>
        ARAP_SECURITY = 73,

        /// <summary>ARAP-Security-Data attribute (RFC 2869 §5.8). Security module challenge or response.</summary>
        ARAP_SECURITY_DATA = 74,

        /// <summary>Password-Retry attribute (RFC 2869 §5.9). Number of password retry attempts permitted.</summary>
        PASSWORD_RETRY = 75,

        /// <summary>Prompt attribute (RFC 2869 §5.10). Whether the NAS should echo user input. See <see cref="PROMPT"/>.</summary>
        PROMPT = 76,                                // ENUM

        /// <summary>Connect-Info attribute (RFC 2869 §5.11). Description of the connection speed and modulation.</summary>
        CONNECT_INFO = 77,

        /// <summary>Configuration-Token attribute (RFC 2869 §5.12). Token for use by a proxy.</summary>
        CONFIGURATION_TOKEN = 78,

        /// <summary>EAP-Message attribute (RFC 3579 §3.1). EAP packet encapsulated within a RADIUS attribute.</summary>
        EAP_MESSAGE = 79,

        /// <summary>Message-Authenticator attribute (RFC 3579 §3.2). HMAC-MD5 signature over the entire RADIUS packet.</summary>
        MESSAGE_AUTHENTICATOR = 80,

        /// <summary>Tunnel-Private-Group-ID attribute (RFC 2868 §3.6). Group ID for the tunnel.</summary>
        TUNNEL_PRIVATE_GROUP_ID = 81,

        /// <summary>Tunnel-Assignment-ID attribute (RFC 2868 §3.7). Identifier assigned to the tunnel.</summary>
        TUNNEL_ASSIGNMENT_ID = 82,

        /// <summary>Tunnel-Preference attribute (RFC 2868 §3.8). Preference for a particular tunnel.</summary>
        TUNNEL_PREFERENCE = 83,

        /// <summary>ARAP-Challenge-Response attribute (RFC 2869 §5.13). Response to an ARAP challenge.</summary>
        ARAP_CHALLENGE_RESPONSE = 84,

        /// <summary>Acct-Interim-Interval attribute (RFC 2869 §5.16). Seconds between interim accounting packets.</summary>
        ACCT_INTERIM_INTERVAL = 85,

        /// <summary>Acct-Tunnel-Packets-Lost attribute (RFC 2867 §4.2). Number of packets lost on a tunnel.</summary>
        ACCT_TUNNEL_PACKETS_LOST = 86,

        /// <summary>NAS-Port-Id attribute (RFC 2869 §5.17). String identifying the NAS port.</summary>
        NAS_PORT_ID = 87,

        /// <summary>Framed-Pool attribute (RFC 2869 §5.18). Name of an assigned address pool.</summary>
        FRAMED_POOL = 88,

        /// <summary>CUI (Chargeable-User-Identity) attribute (RFC 4372 §2). Identifier for billing purposes.</summary>
        CUI = 89,

        /// <summary>Tunnel-Client-Auth-ID attribute (RFC 2868 §3.9). Name of the tunnel client during authentication.</summary>
        TUNNEL_CLIENT_AUTH_ID = 90,

        /// <summary>Tunnel-Server-Auth-ID attribute (RFC 2868 §3.10). Name of the tunnel server during authentication.</summary>
        TUNNEL_SERVER_AUTH_ID = 91,

        /// <summary>NAS-Filter-Rule attribute (RFC 4849 §2). Packet filter rule to install on the NAS.</summary>
        NAS_FILTER_RULE = 92,

        // Type 93 is unassigned per IANA RADIUS Types registry.

        /// <summary>Originating-Line-Info attribute (RFC 7155 §4.2.8, IANA). ANI/II digits information.</summary>
        ORIGINATING_LINE_INFO = 94,

        /// <summary>NAS-IPv6-Address attribute (RFC 3162 §2.1). IPv6 address of the NAS.</summary>
        NAS_IPV6_ADDRESS = 95,

        /// <summary>Framed-Interface-Id attribute (RFC 3162 §2.2). IPv6 interface identifier to assign to the user.</summary>
        FRAMED_INTERFACE_ID = 96,

        /// <summary>Framed-IPv6-Prefix attribute (RFC 3162 §2.3). IPv6 prefix to assign to the user.</summary>
        FRAMED_IPV6_PREFIX = 97,

        /// <summary>Login-IPv6-Host attribute (RFC 3162 §2.4). IPv6 host to connect the user to.</summary>
        LOGIN_IPV6_HOST = 98,

        /// <summary>Framed-IPv6-Route attribute (RFC 3162 §2.5). Routing information for the user's IPv6 session.</summary>
        FRAMED_IPV6_ROUTE = 99,

        /// <summary>Framed-IPv6-Pool attribute (RFC 3162 §2.6). Name of an assigned IPv6 address pool.</summary>
        FRAMED_IPV6_POOL = 100,

        /// <summary>Error-Cause attribute (RFC 5176 §3.6). Provides additional information about an error. See <see cref="ERROR_CAUSE"/>.</summary>
        ERROR_CAUSE = 101,                          // ENUM

        /// <summary>EAP-Key-Name attribute (RFC 4072 §2.1). EAP session key name.</summary>
        EAP_KEY_NAME = 102,

        /// <summary>Digest-Response attribute (RFC 5090 §3.1). HTTP Digest response value.</summary>
        DIGEST_RESPONSE = 103,

        /// <summary>Digest-Realm attribute (RFC 5090 §3.2). HTTP Digest realm.</summary>
        DIGEST_REALM = 104,

        /// <summary>Digest-Nonce attribute (RFC 5090 §3.3). HTTP Digest nonce value.</summary>
        DIGEST_NONCE = 105,

        /// <summary>Digest-Response-Auth attribute (RFC 5090 §3.4). HTTP Digest response-auth.</summary>
        DIGEST_RESPONSE_AUTH = 106,

        /// <summary>Digest-Nextnonce attribute (RFC 5090 §3.5). HTTP Digest nextnonce value.</summary>
        DIGEST_NEXTNONCE = 107,

        /// <summary>Digest-Method attribute (RFC 5090 §3.6). HTTP method used in the Digest exchange.</summary>
        DIGEST_METHOD = 108,

        /// <summary>Digest-URI attribute (RFC 5090 §3.7). HTTP Digest request URI.</summary>
        DIGEST_URI = 109,

        /// <summary>Digest-Qop attribute (RFC 5090 §3.8). HTTP Digest quality-of-protection value.</summary>
        DIGEST_QOP = 110,

        /// <summary>Digest-Algorithm attribute (RFC 5090 §3.9). Algorithm used in the HTTP Digest exchange.</summary>
        DIGEST_ALGORITHM = 111,

        /// <summary>Digest-Entity-Body-Hash attribute (RFC 5090 §3.10). Hash of the HTTP message body.</summary>
        DIGEST_ENTITY_BODY_HASH = 112,

        /// <summary>Digest-Cnonce attribute (RFC 5090 §3.11). HTTP Digest client nonce.</summary>
        DIGEST_CNONCE = 113,

        /// <summary>Digest-Nonce-Count attribute (RFC 5090 §3.12). HTTP Digest nonce count.</summary>
        DIGEST_NONCE_COUNT = 114,

        /// <summary>Digest-Username attribute (RFC 5090 §3.13). HTTP Digest username.</summary>
        DIGEST_USERNAME = 115,

        /// <summary>Digest-Opaque attribute (RFC 5090 §3.14). HTTP Digest opaque value.</summary>
        DIGEST_OPAQUE = 116,

        /// <summary>Digest-Auth-Param attribute (RFC 5090 §3.15). Additional HTTP Digest parameters.</summary>
        DIGEST_AUTH_PARAM = 117,

        /// <summary>Digest-AKA-Auts attribute (RFC 5090 §3.16). AKA synchronisation token.</summary>
        DIGEST_AKA_AUTS = 118,

        /// <summary>Digest-Domain attribute (RFC 5090 §3.17). HTTP Digest domain.</summary>
        DIGEST_DOMAIN = 119,

        /// <summary>Digest-Stale attribute (RFC 5090 §3.18). HTTP Digest stale flag.</summary>
        DIGEST_STALE = 120,

        /// <summary>Digest-HA1 attribute (RFC 5090 §3.19). Pre-computed HA1 hash for HTTP Digest.</summary>
        DIGEST_HA1 = 121,

        /// <summary>SIP-AOR attribute (RFC 5090 §3.20). SIP Address of Record.</summary>
        SIP_AOR = 122,

        /// <summary>Delegated-IPv6-Prefix attribute (RFC 4818 §3). IPv6 prefix delegated via DHCPv6-PD.</summary>
        DELEGATED_IPV6_PREFIX = 123,

        /// <summary>MIP6-Feature-Vector attribute (RFC 5447 §4.2.2). Mobile IPv6 feature flags.</summary>
        MIP6_FEATURE_VECTOR = 124,

        /// <summary>MIP6-Home-Link-Prefix attribute (RFC 5447 §4.2.7). Home link prefix for Mobile IPv6.</summary>
        MIP6_HOME_LINK_PREFIX = 125,

        /// <summary>Operator-Name attribute (RFC 5580 §4.1). Identifies the operator of the network access server.</summary>
        OPERATOR_NAME = 126,

        /// <summary>Location-Information attribute (RFC 5580 §4.2). Civic or geospatial location of the user.</summary>
        LOCATION_INFORMATION = 127,

        /// <summary>Location-Data attribute (RFC 5580 §4.3). Opaque location data blob.</summary>
        LOCATION_DATA = 128,

        /// <summary>Basic-Location-Policy-Rules attribute (RFC 5580 §4.4). Policy rules for location data handling.</summary>
        BASIC_LOCATION_POLICY_RULES = 129,

        /// <summary>Extended-Location-Policy-Rules attribute (RFC 5580 §4.5). Extended policy rules for location data.</summary>
        EXTENDED_LOCATION_POLICY_RULES = 130,

        /// <summary>Location-Capable attribute (RFC 5580 §4.6). Bitmask of location capabilities. See <see cref="LOCATION_CAPABLE"/>.</summary>
        LOCATION_CAPABLE = 131,                     // ENUM

        /// <summary>Requested-Location-Info attribute (RFC 5580 §4.7). Bitmask of requested location types. See <see cref="REQUESTED_LOCATION_INFO"/>.</summary>
        REQUESTED_LOCATION_INFO = 132,              // ENUM

        /// <summary>Framed-Management-Protocol attribute (RFC 5607 §4.1). Management protocol for the session. See <see cref="FRAMED_MANAGEMENT_PROTOCOL"/>.</summary>
        FRAMED_MANAGEMENT_PROTOCOL = 133,           // ENUM

        /// <summary>Management-Transport-Protection attribute (RFC 5607 §4.2). Transport security level. See <see cref="MANAGEMENT_TRANSPORT_PROTECTION"/>.</summary>
        MANAGEMENT_TRANSPORT_PROTECTION = 134,      // ENUM

        /// <summary>Management-Policy-Id attribute (RFC 5607 §4.3). Identifier of the management policy.</summary>
        MANAGEMENT_POLICY_ID = 135,

        /// <summary>Management-Privilege-Level attribute (RFC 5607 §4.4). Privilege level for management access.</summary>
        MANAGEMENT_PRIVILEGE_LEVEL = 136,

        /// <summary>PKM-SS-Cert attribute (RFC 5904 §3.1). PKM subscriber station certificate.</summary>
        PKM_SS_CERT = 137,

        /// <summary>PKM-CA-Cert attribute (RFC 5904 §3.2). PKM certificate authority certificate.</summary>
        PKM_CA_CERT = 138,

        /// <summary>PKM-Config-Settings attribute (RFC 5904 §3.3). PKM configuration settings.</summary>
        PKM_CONFIG_SETTINGS = 139,

        /// <summary>PKM-Cryptosuite-List attribute (RFC 5904 §3.4). List of supported PKM cryptographic suites.</summary>
        PKM_CRYPTOSUITE_LIST = 140,

        /// <summary>PKM-SAID attribute (RFC 5904 §3.5). PKM Security Association Identifier.</summary>
        PKM_SAID = 141,

        /// <summary>PKM-SA-Descriptor attribute (RFC 5904 §3.6). PKM Security Association descriptor.</summary>
        PKM_SA_DESCRIPTOR = 142,

        /// <summary>PKM-Auth-Key attribute (RFC 5904 §3.7). PKM authentication key.</summary>
        PKM_AUTH_KEY = 143,

        /// <summary>DS-Lite-Tunnel-Name attribute (RFC 6519 §4). Name of the DS-Lite tunnel.</summary>
        DS_LITE_TUNNEL_NAME = 144,

        /// <summary>Mobile-Node-Identifier attribute (RFC 6572 §6.1). Identifier of the mobile node.</summary>
        MOBILE_NODE_IDENTIFIER = 145,

        /// <summary>Service-Selection attribute (RFC 5765 §3.1, RFC 5779 §6.1). APN or service name for the session.</summary>
        SERVICE_SELECTION = 146,

        /// <summary>PMIP6-Home-LMA-IPv6-Address attribute (RFC 6572 §6.2). IPv6 address of the home LMA.</summary>
        PMIP6_HOME_LMA_IPV6_ADDRESS = 147,

        /// <summary>PMIP6-Visited-LMA-IPv6-Address attribute (RFC 6572 §6.3). IPv6 address of the visited LMA.</summary>
        PMIP6_VISITED_LMA_IPV6_ADDRESS = 148,

        /// <summary>PMIP6-Home-LMA-IPv4-Address attribute (RFC 6572 §6.4). IPv4 address of the home LMA.</summary>
        PMIP6_HOME_LMA_IPV4_ADDRESS = 149,

        /// <summary>PMIP6-Visited-LMA-IPv4-Address attribute (RFC 6572 §6.5). IPv4 address of the visited LMA.</summary>
        PMIP6_VISITED_LMA_IPV4_ADDRESS = 150,

        /// <summary>PMIP6-Home-HN-Prefix attribute (RFC 6572 §6.6). Home network prefix for the mobile node.</summary>
        PMIP6_HOME_HN_PREFIX = 151,

        /// <summary>PMIP6-Visited-HN-Prefix attribute (RFC 6572 §6.7). Visited network prefix for the mobile node.</summary>
        PMIP6_VISITED_HN_PREFIX = 152,

        /// <summary>PMIP6-Home-Interface-ID attribute (RFC 6572 §6.8). Interface identifier for the home network.</summary>
        PMIP6_HOME_INTERFACE_ID = 153,

        /// <summary>PMIP6-Visited-Interface-ID attribute (RFC 6572 §6.9). Interface identifier for the visited network.</summary>
        PMIP6_VISITED_INTERFACE_ID = 154,

        /// <summary>PMIP6-Home-IPv4-HoA attribute (RFC 6572 §6.10). Home IPv4 address of the mobile node.</summary>
        PMIP6_HOME_IPV4_HOA = 155,

        /// <summary>PMIP6-Visited-IPv4-HoA attribute (RFC 6572 §6.11). Visited IPv4 address of the mobile node.</summary>
        PMIP6_VISITED_IPV4_HOA = 156,

        /// <summary>PMIP6-Home-DHCP4-Server-Address attribute (RFC 6572 §6.12). IPv4 address of the home DHCPv4 server.</summary>
        PMIP6_HOME_DHCP4_SERVER_ADDRESS = 157,

        /// <summary>PMIP6-Visited-DHCP4-Server-Address attribute (RFC 6572 §6.13). IPv4 address of the visited DHCPv4 server.</summary>
        PMIP6_VISITED_DHCP4_SERVER_ADDRESS = 158,

        /// <summary>PMIP6-Home-DHCP6-Server-Address attribute (RFC 6572 §6.14). IPv6 address of the home DHCPv6 server.</summary>
        PMIP6_HOME_DHCP6_SERVER_ADDRESS = 159,

        /// <summary>PMIP6-Visited-DHCP6-Server-Address attribute (RFC 6572 §6.15). IPv6 address of the visited DHCPv6 server.</summary>
        PMIP6_VISITED_DHCP6_SERVER_ADDRESS = 160,

        /// <summary>PMIP6-Home-IPv4-Gateway attribute (RFC 6572 §6.16). IPv4 default gateway for the home network.</summary>
        PMIP6_HOME_IPV4_GATEWAY = 161,

        /// <summary>PMIP6-Visited-IPv4-Gateway attribute (RFC 6572 §6.17). IPv4 default gateway for the visited network.</summary>
        PMIP6_VISITED_IPV4_GATEWAY = 162,

        /// <summary>EAP-Lower-Layer attribute (RFC 7057 §4.1). Identifies the lower layer used with EAP. See <see cref="EAP_LOWER_LAYER"/>.</summary>
        EAP_LOWER_LAYER = 163,                      // ENUM

        /// <summary>GSS-Acceptor-Service-Name attribute (RFC 7055 §3.1). GSS-API service name.</summary>
        GSS_ACCEPTOR_SERVICE_NAME = 164,

        /// <summary>GSS-Acceptor-Host-Name attribute (RFC 7055 §3.2). GSS-API host name.</summary>
        GSS_ACCEPTOR_HOST_NAME = 165,

        /// <summary>GSS-Acceptor-Service-Specifics attribute (RFC 7055 §3.3). GSS-API service-specific data.</summary>
        GSS_ACCEPTOR_SERVICE_SPECIFICS = 166,

        /// <summary>GSS-Acceptor-Realm-Name attribute (RFC 7055 §3.4). GSS-API Kerberos realm name.</summary>
        GSS_ACCEPTOR_REALM_NAME = 167,

        /// <summary>Framed-IPv6-Address attribute (RFC 6911 §3.1). IPv6 address to assign to the user.</summary>
        FRAMED_IPV6_ADDRESS = 168,

        /// <summary>DNS-Server-IPv6-Address attribute (RFC 6911 §3.2). IPv6 address of the DNS server.</summary>
        DNS_SERVER_IPV6_ADDRESS = 169,

        /// <summary>Route-IPv6-Information attribute (RFC 6911 §3.3). IPv6 routing information for the user.</summary>
        ROUTE_IPV6_INFORMATION = 170,

        /// <summary>Delegated-IPv6-Prefix-Pool attribute (RFC 6911 §3.4). Name of a delegated IPv6 prefix pool.</summary>
        DELEGATED_IPV6_PREFIX_POOL = 171,

        /// <summary>Stateful-IPv6-Address-Pool attribute (RFC 6911 §3.5). Name of a stateful IPv6 address pool.</summary>
        STATEFUL_IPV6_ADDRESS_POOL = 172,

        /// <summary>IPv6-6rd-Configuration attribute (RFC 6930 §3). 6rd tunnel configuration parameters.</summary>
        IPV6_6RD_CONFIGURATION = 173,

        /// <summary>Allowed-Called-Station-Id attribute (RFC 7268 §3.1). Permitted called station identifiers.</summary>
        ALLOWED_CALLED_STATION_ID = 174,

        /// <summary>EAP-Peer-Id attribute (RFC 7268 §3.2). EAP peer identifier.</summary>
        EAP_PEER_ID = 175,

        /// <summary>EAP-Server-Id attribute (RFC 7268 §3.3). EAP server identifier.</summary>
        EAP_SERVER_ID = 176,

        /// <summary>Mobility-Domain-Id attribute (RFC 7268 §3.4). IEEE 802.11r mobility domain identifier.</summary>
        MOBILITY_DOMAIN_ID = 177,

        /// <summary>Preauth-Timeout attribute (RFC 7268 §3.5). Timeout for IEEE 802.11r pre-authentication.</summary>
        PREAUTH_TIMEOUT = 178,

        /// <summary>Network-Id-Name attribute (RFC 7268 §3.6). Network identifier name.</summary>
        NETWORK_ID_NAME = 179,

        /// <summary>EAPoL-Announcement attribute (RFC 7268 §3.7). EAPoL announcement data.</summary>
        EAPOL_ANNOUNCEMENT = 180,

        /// <summary>WLAN-HESSID attribute (RFC 7268 §3.8). Homogeneous ESS Identifier.</summary>
        WLAN_HESSID = 181,

        /// <summary>WLAN-Venue-Info attribute (RFC 7268 §3.9). Venue type and group information.</summary>
        WLAN_VENUE_INFO = 182,

        /// <summary>WLAN-Venue-Language attribute (RFC 7268 §3.10). Language code for venue name.</summary>
        WLAN_VENUE_LANGUAGE = 183,

        /// <summary>WLAN-Venue-Name attribute (RFC 7268 §3.11). Human-readable venue name.</summary>
        WLAN_VENUE_NAME = 184,

        /// <summary>WLAN-Reason-Code attribute (RFC 7268 §3.12). IEEE 802.11 reason code for deauthentication/disassociation.</summary>
        WLAN_REASON_CODE = 185,

        /// <summary>WLAN-Pairwise-Cipher attribute (RFC 7268 §3.13). Negotiated pairwise cipher suite.</summary>
        WLAN_PAIRWISE_CIPHER = 186,

        /// <summary>WLAN-Group-Cipher attribute (RFC 7268 §3.14). Negotiated group cipher suite.</summary>
        WLAN_GROUP_CIPHER = 187,

        /// <summary>WLAN-AKM-Suite attribute (RFC 7268 §3.15). Authentication and Key Management suite.</summary>
        WLAN_AKM_SUITE = 188,

        /// <summary>WLAN-Group-Mgmt-Cipher attribute (RFC 7268 §3.16). Group management frame cipher suite.</summary>
        WLAN_GROUP_MGMT_CIPHER = 189,

        /// <summary>WLAN-RF-Band attribute (RFC 7268 §3.17). RF band in use (e.g., 2.4 GHz, 5 GHz).</summary>
        WLAN_RF_BAND = 190,

        // Types 191–240 are unassigned per IANA RADIUS Types registry.

        /// <summary>
        /// Extended-Type-1 attribute (RFC 6929 §3). Container for extended attributes in the
        /// first extended type space. The first data byte is the Extended-Type code.
        /// </summary>
        EXTENDED_TYPE_1 = 241,

        /// <summary>
        /// Extended-Type-2 attribute (RFC 6929 §3). Container for extended attributes in the
        /// second extended type space. The first data byte is the Extended-Type code.
        /// </summary>
        EXTENDED_TYPE_2 = 242,

        /// <summary>
        /// Extended-Type-3 attribute (RFC 6929 §3). Container for extended attributes in the
        /// third extended type space. The first data byte is the Extended-Type code.
        /// </summary>
        EXTENDED_TYPE_3 = 243,

        /// <summary>
        /// Extended-Type-4 attribute (RFC 6929 §3). Container for extended attributes in the
        /// fourth extended type space. The first data byte is the Extended-Type code.
        /// </summary>
        EXTENDED_TYPE_4 = 244,

        /// <summary>
        /// Long-Extended-Type-1 attribute (RFC 6929 §4). Container for long extended attributes
        /// in the first long extended type space. Supports fragmentation via the More (M) bit.
        /// </summary>
        LONG_EXTENDED_TYPE_1 = 245,

        /// <summary>
        /// Long-Extended-Type-2 attribute (RFC 6929 §4). Container for long extended attributes
        /// in the second long extended type space. Supports fragmentation via the More (M) bit.
        /// </summary>
        LONG_EXTENDED_TYPE_2 = 246
    }

    /// <summary>
    /// Defines the type of service requested or provided, as used in the
    /// Service-Type attribute (RFC 2865 §5.6, <see cref="RadiusAttributeType.SERVICE_TYPE"/>).
    /// </summary>
    public enum SERVICE_TYPE
    {
        /// <summary>The NAS should connect the user to a host (Telnet, Rlogin, etc.).</summary>
        LOGIN = 1,

        /// <summary>The NAS should provide framed access (PPP, SLIP, etc.).</summary>
        FRAMED = 2,

        /// <summary>The NAS should connect the user to a host, then call back.</summary>
        CALLBACK_LOGIN = 3,

        /// <summary>The NAS should provide framed access, then call back.</summary>
        CALLBACK_FRAMED = 4,

        /// <summary>The NAS should provide outbound network access.</summary>
        OUTBOUND = 5,

        /// <summary>The NAS should provide administrative access (NAS prompt).</summary>
        ADMINISTRATIVE = 6,

        /// <summary>The NAS should present a NAS prompt to the user.</summary>
        NAS_PROMPT = 7,

        /// <summary>The NAS should only authenticate the user — no session is started.</summary>
        AUTHENTICATE_ONLY = 8,

        /// <summary>The NAS should call back and present a NAS prompt.</summary>
        CALLBACK_NAS_PROMPT = 9,

        /// <summary>The NAS should use call-check to validate a call before accepting it.</summary>
        CALL_CHECK = 10,

        /// <summary>The NAS should call back and provide administrative access.</summary>
        CALLBACK_ADMINISTRATIVE = 11,

        /// <summary>Voice service.</summary>
        VOICE = 12,

        /// <summary>Fax service.</summary>
        FAX = 13,

        /// <summary>Modem relay service.</summary>
        MODEM_RELAY = 14,

        /// <summary>IAPP register service (IEEE 802.11f).</summary>
        IAPP_REGISTER = 15,

        /// <summary>IAPP AP-Check service (IEEE 802.11f).</summary>
        IAPP_AP_CHECK = 16,

        /// <summary>The NAS should only authorise the user — no authentication is performed.</summary>
        AUTHORIZE_ONLY = 17,

        /// <summary>Framed management session (RFC 5607).</summary>
        FRAMED_MANAGEMENT = 18,

        /// <summary>Additional authorisation attributes are required (RFC 5580).</summary>
        ADDITIONAL_AUTHORIZATION = 19
    }

    /// <summary>
    /// Defines the framing protocol to use for the session, as used in the
    /// Framed-Protocol attribute (RFC 2865 §5.7, <see cref="RadiusAttributeType.FRAMED_PROTOCOL"/>).
    /// </summary>
    public enum FRAMED_PROTOCOL
    {
        /// <summary>Point-to-Point Protocol.</summary>
        PPP = 1,

        /// <summary>Serial Line Internet Protocol.</summary>
        SLIP = 2,

        /// <summary>AppleTalk Remote Access Protocol.</summary>
        ARAP = 3,

        /// <summary>Gandalf proprietary Single-Link/Multi-Link Protocol.</summary>
        GANDALF_SLML = 4,

        /// <summary>Xylogics proprietary IPX/SLIP.</summary>
        XYLOGICS_PROPRIETARY_IPX_SLIP = 5,

        /// <summary>X.75 synchronous protocol.</summary>
        X75_SYNCHRONOUS = 6,

        /// <summary>GPRS PDP Context (3GPP).</summary>
        GPRS_PDP_CONTEXT = 7
    }

    /// <summary>
    /// Defines the routing method to use for the framed user session, as used in the
    /// Framed-Routing attribute (RFC 2865 §5.10, <see cref="RadiusAttributeType.FRAMED_ROUTING"/>).
    /// </summary>
    public enum FRAMED_ROUTING
    {
        /// <summary>No routing is performed.</summary>
        NONE = 0,

        /// <summary>Send routing packets only.</summary>
        BROADCAST = 1,

        /// <summary>Listen for routing packets only.</summary>
        LISTEN = 2,

        /// <summary>Send and listen for routing packets.</summary>
        BROADCAST_LISTEN = 3
    }

    /// <summary>
    /// Defines the compression protocol to use for the session, as used in the
    /// Framed-Compression attribute (RFC 2865 §5.13, <see cref="RadiusAttributeType.FRAMED_COMPRESSION"/>).
    /// </summary>
    public enum FRAMED_COMPRESSION
    {
        /// <summary>No compression.</summary>
        NONE = 0,

        /// <summary>Van Jacobson TCP/IP header compression (RFC 1144).</summary>
        VJ_TCP_IP_HEADER_COMPRESSION = 1,

        /// <summary>IPX header compression.</summary>
        IPX_HEADER_COMPRESSION = 2,

        /// <summary>Stac-LZS compression.</summary>
        STAC_LZS_COMPRESSION = 3
    }

    /// <summary>
    /// Defines the login service to connect the user to, as used in the
    /// Login-Service attribute (RFC 2865 §5.15, <see cref="RadiusAttributeType.LOGIN_SERVICE"/>).
    /// </summary>
    public enum LOGIN_SERVICE
    {
        /// <summary>Telnet session.</summary>
        TELNET = 0,

        /// <summary>Rlogin session.</summary>
        RLOGIN = 1,

        /// <summary>TCP clear (no terminal emulation).</summary>
        TCP_CLEAR = 2,

        /// <summary>PortMaster proprietary session.</summary>
        PORTMASTER = 3,

        /// <summary>LAT (Local Area Transport) session.</summary>
        LAT = 4,

        /// <summary>X.25 PAD session.</summary>
        X25_PAD = 5,

        /// <summary>X.25 T3POS session.</summary>
        X25_T3POS = 6,

        /// <summary>TCP clear with no output echo.</summary>
        TCP_CLEAR_QUIET = 8
    }

    /// <summary>
    /// Defines the action the NAS should take when the session terminates, as used in the
    /// Termination-Action attribute (RFC 2865 §5.29, <see cref="RadiusAttributeType.TERMINATION_ACTION"/>).
    /// </summary>
    public enum TERMINATION_ACTION
    {
        /// <summary>Take the default action for the service.</summary>
        DEFAULT = 0,

        /// <summary>Send a new RADIUS Access-Request to re-authorise the session.</summary>
        RADIUS_REQUEST = 1
    }

    /// <summary>
    /// Defines the type of accounting record, as used in the
    /// Acct-Status-Type attribute (RFC 2866 §5.1, <see cref="RadiusAttributeType.ACCT_STATUS_TYPE"/>).
    /// </summary>
    public enum ACCT_STATUS_TYPE
    {
        /// <summary>Accounting Start — the session has begun.</summary>
        START = 1,

        /// <summary>Accounting Stop — the session has ended.</summary>
        STOP = 2,

        /// <summary>Interim Update — periodic accounting update during an active session.</summary>
        INTERIM_UPDATE = 3,

        /// <summary>Accounting On — the NAS is starting up and all sessions are being re-reported.</summary>
        ACCOUNTING_ON = 7,

        /// <summary>Accounting Off — the NAS is shutting down.</summary>
        ACCOUNTING_OFF = 8,

        /// <summary>Tunnel Start — a tunnel has been established (RFC 2867 §3.1).</summary>
        TUNNEL_START = 9,

        /// <summary>Tunnel Stop — a tunnel has been terminated (RFC 2867 §3.2).</summary>
        TUNNEL_STOP = 10,

        /// <summary>Tunnel Reject — a tunnel establishment request was rejected (RFC 2867 §3.3).</summary>
        TUNNEL_REJECT = 11,

        /// <summary>Tunnel Link Start — a new link within a tunnel has started (RFC 2867 §3.4).</summary>
        TUNNEL_LINK_START = 12,

        /// <summary>Tunnel Link Stop — a link within a tunnel has been terminated (RFC 2867 §3.5).</summary>
        TUNNEL_LINK_STOP = 13,

        /// <summary>Tunnel Link Reject — a new link within a tunnel was rejected (RFC 2867 §3.6).</summary>
        TUNNEL_LINK_REJECT = 14,

        /// <summary>Failed — the accounting start failed.</summary>
        FAILED = 15,

        /// <summary>Subsystem On — a RADIUS subsystem is starting up (vendor extension).</summary>
        SUBSYSTEM_ON = 18,

        /// <summary>Subsystem Off — a RADIUS subsystem is shutting down (vendor extension).</summary>
        SUBSYSTEM_OFF = 19
    }

    /// <summary>
    /// Defines how the user was authenticated, as used in the
    /// Acct-Authentic attribute (RFC 2866 §5.6, <see cref="RadiusAttributeType.ACCT_AUTHENTIC"/>).
    /// </summary>
    public enum ACCT_AUTHENTIC
    {
        /// <summary>Authenticated via RADIUS.</summary>
        RADIUS = 1,

        /// <summary>Authenticated locally on the NAS.</summary>
        LOCAL = 2,

        /// <summary>Authenticated via a remote method.</summary>
        REMOTE = 3,

        /// <summary>Authenticated via Diameter (RFC 4005).</summary>
        DIAMETER = 4
    }

    /// <summary>
    /// Defines the reason a session was terminated, as used in the
    /// Acct-Terminate-Cause attribute (RFC 2866 §5.10, <see cref="RadiusAttributeType.ACCT_TERMINATE_CAUSE"/>).
    /// </summary>
    public enum ACCT_TERMINATE_CAUSE
    {
        /// <summary>The user requested the session to be terminated.</summary>
        USER_REQUEST = 1,

        /// <summary>The carrier signal was lost.</summary>
        LOST_CARRIER = 2,

        /// <summary>The service was lost.</summary>
        LOST_SERVICE = 3,

        /// <summary>The session exceeded the idle timeout.</summary>
        IDLE_TIMEOUT = 4,

        /// <summary>The session exceeded the maximum session time.</summary>
        SESSION_TIMEOUT = 5,

        /// <summary>An administrator reset the session.</summary>
        ADMIN_RESET = 6,

        /// <summary>The NAS was rebooted by an administrator.</summary>
        ADMIN_REBOOT = 7,

        /// <summary>A port error caused the session to be terminated.</summary>
        PORT_ERROR = 8,

        /// <summary>The NAS experienced an internal error.</summary>
        NAS_ERROR = 9,

        /// <summary>The NAS terminated the session.</summary>
        NAS_REQUEST = 10,

        /// <summary>The NAS was rebooted.</summary>
        NAS_REBOOT = 11,

        /// <summary>The port is no longer needed.</summary>
        PORT_UNNEEDED = 12,

        /// <summary>The port was preempted by a higher-priority session.</summary>
        PORT_PREEMPTED = 13,

        /// <summary>The port was suspended.</summary>
        PORT_SUSPENDED = 14,

        /// <summary>The requested service is unavailable.</summary>
        SERVICE_UNAVAILABLE = 15,

        /// <summary>The session was terminated due to a callback.</summary>
        CALLBACK = 16,

        /// <summary>A user error occurred.</summary>
        USER_ERROR = 17,

        /// <summary>The remote host terminated the connection.</summary>
        HOST_REQUEST = 18,

        /// <summary>The supplicant was restarted (IEEE 802.1X).</summary>
        SUPPLICANT_RESTART = 19,

        /// <summary>Re-authentication failed (IEEE 802.1X).</summary>
        REAUTHENTICATION_FAILURE = 20,

        /// <summary>The port was re-initialised.</summary>
        PORT_REINITIALIZED = 21,

        /// <summary>The port was administratively disabled.</summary>
        PORT_ADMINISTRATIVELY_DISABLED = 22,

        /// <summary>Power was lost to the port.</summary>
        LOST_POWER = 23
    }

    /// <summary>
    /// Defines the physical port type the user is connected to, as used in the
    /// NAS-Port-Type attribute (RFC 2865 §5.41, <see cref="RadiusAttributeType.NAS_PORT_TYPE"/>).
    /// </summary>
    public enum NAS_PORT_TYPE
    {
        /// <summary>Asynchronous (modem) connection.</summary>
        ASYNC = 0,

        /// <summary>Synchronous connection.</summary>
        SYNC = 1,

        /// <summary>ISDN synchronous connection.</summary>
        ISDN_SYNC = 2,

        /// <summary>ISDN asynchronous V.120 connection.</summary>
        ISDN_ASYNC_V120 = 3,

        /// <summary>ISDN asynchronous V.110 connection.</summary>
        ISDN_ASYNC_V110 = 4,

        /// <summary>Virtual connection (e.g., VPN).</summary>
        VIRTUAL = 5,

        /// <summary>PIAFS (PHS Internet Access Forum Standard) wireless connection.</summary>
        PIAFS = 6,

        /// <summary>HDLC clear channel connection.</summary>
        HDLC_CLEAR_CHANNEL = 7,

        /// <summary>X.25 connection.</summary>
        X25 = 8,

        /// <summary>X.75 connection.</summary>
        X75 = 9,

        /// <summary>G.3 fax connection.</summary>
        G3_FAX = 10,

        /// <summary>Symmetric DSL connection.</summary>
        SDSL = 11,

        /// <summary>Asymmetric DSL (CAP) connection.</summary>
        ADSL_CAP = 12,

        /// <summary>Asymmetric DSL (DMT) connection.</summary>
        ADSL_DMT = 13,

        /// <summary>ISDN DSL connection.</summary>
        IDSL = 14,

        /// <summary>Ethernet connection.</summary>
        ETHERNET = 15,

        /// <summary>xDSL (generic DSL) connection.</summary>
        XDSL = 16,

        /// <summary>Cable modem connection.</summary>
        CABLE = 17,

        /// <summary>Wireless connection (other).</summary>
        WIRELESS_OTHER = 18,

        /// <summary>Wireless IEEE 802.11 connection.</summary>
        WIRELESS_IEEE_802_11 = 19,

        /// <summary>Token Ring connection.</summary>
        TOKEN_RING = 20,

        /// <summary>FDDI connection.</summary>
        FDDI = 21,

        /// <summary>Wireless CDMA2000 connection.</summary>
        WIRELESS_CDMA2000 = 22,

        /// <summary>Wireless UMTS connection.</summary>
        WIRELESS_UMTS = 23,

        /// <summary>Wireless 1xEV-DO connection.</summary>
        WIRELESS_1XEV = 24,

        /// <summary>IAPP (IEEE 802.11f) connection.</summary>
        IAPP = 25,

        /// <summary>Fibre To The Premises connection.</summary>
        FTTP = 26,

        /// <summary>Wireless IEEE 802.16 (WiMAX) connection.</summary>
        WIRELESS_IEEE80216 = 27,

        /// <summary>Wireless IEEE 802.20 connection.</summary>
        WIRELESS_IEEE80220 = 28,

        /// <summary>Wireless IEEE 802.22 connection.</summary>
        WIRELESS_IEEE80222 = 29,

        /// <summary>PPP over ATM connection.</summary>
        PPPOA = 30,

        /// <summary>PPPoE over ATM connection.</summary>
        PPPOEOA = 31,

        /// <summary>PPPoE over Ethernet connection.</summary>
        PPPOEOE = 32,

        /// <summary>PPPoE over VLAN connection.</summary>
        PPPOEOVLAN = 33,

        /// <summary>PPPoE over QinQ (double-tagged VLAN) connection.</summary>
        PPPOEOQINQ = 34,

        /// <summary>xPON (Passive Optical Network) connection.</summary>
        XPON = 35,

        /// <summary>Wireless XGP connection.</summary>
        WIRELESS_XGP = 36,

        /// <summary>WiMAX connection.</summary>
        WIMAX = 37,

        /// <summary>WiMAX/Wi-Fi interworking connection.</summary>
        WIMAX_WIFI_IWK = 38,

        /// <summary>WiMAX Simple IP / Full Function connection.</summary>
        WIMAX_SFF = 39,

        /// <summary>WiMAX HA/LMA connection.</summary>
        WIMAX_HA_LMA = 40,

        /// <summary>WiMAX DHCP connection.</summary>
        WIMAX_DHCP = 41,

        /// <summary>WiMAX LBS (Location-Based Services) connection.</summary>
        WIMAX_LBS = 42,

        /// <summary>WiMAX WVS connection.</summary>
        WIMAX_WVS = 43
    }

    /// <summary>
    /// Defines the tunnelling protocol to use, as used in the
    /// Tunnel-Type attribute (RFC 2868 §3.1, <see cref="RadiusAttributeType.TUNNEL_TYPE"/>).
    /// </summary>
    public enum TUNNEL_TYPE
    {
        /// <summary>No tunnel type specified.</summary>
        None = 0,

        /// <summary>Point-to-Point Tunnelling Protocol (RFC 2637).</summary>
        PPTP = 1,

        /// <summary>Layer 2 Forwarding (Cisco proprietary).</summary>
        L2F = 2,

        /// <summary>Layer 2 Tunnelling Protocol (RFC 2661).</summary>
        L2TP = 3,

        /// <summary>Ascend Tunnel Management Protocol.</summary>
        ATMP = 4,

        /// <summary>Virtual Tunnelling Protocol.</summary>
        VTP = 5,

        /// <summary>IP Authentication Header (RFC 2402).</summary>
        AH = 6,

        /// <summary>IP-in-IP encapsulation (RFC 2003).</summary>
        IP_IP = 7,

        /// <summary>Minimal IP-in-IP encapsulation (RFC 2004).</summary>
        MIN_IP_IP = 8,

        /// <summary>IP Encapsulating Security Payload (RFC 2406).</summary>
        ESP = 9,

        /// <summary>Generic Routing Encapsulation (RFC 2784).</summary>
        GRE = 10,

        /// <summary>Bay Dial Virtual Services (DVS).</summary>
        DVS = 11,

        /// <summary>IP-in-IP tunnelling.</summary>
        IP_IN_IP = 12,

        /// <summary>IEEE 802.1Q VLAN.</summary>
        VLAN = 13
    }

    /// <summary>
    /// Defines the transport medium for a tunnel, as used in the
    /// Tunnel-Medium-Type attribute (RFC 2868 §3.2, <see cref="RadiusAttributeType.TUNNEL_MEDIUM_TYPE"/>).
    /// </summary>
    public enum TUNNEL_MEDIUM_TYPE
    {
        /// <summary>IPv4 (IP version 4).</summary>
        IPv4 = 1,

        /// <summary>IPv6 (IP version 6).</summary>
        IPv6 = 2,

        /// <summary>NSAP (Network Service Access Point).</summary>
        NSAP = 3,

        /// <summary>HDLC (when no serial encapsulation).</summary>
        HDLC = 4,

        /// <summary>BBN 1822.</summary>
        BBN = 5,

        /// <summary>IEEE 802 (includes all 802 media plus Ethernet canonical format).</summary>
        IEEE802 = 6,

        /// <summary>E.163 (POTS).</summary>
        E163 = 7,

        /// <summary>E.164 (SMDS, Frame Relay, ATM).</summary>
        E164 = 8,

        /// <summary>F.69 (Telex).</summary>
        F69 = 9,

        /// <summary>X.121 (X.25, Frame Relay).</summary>
        X121 = 10,

        /// <summary>IPX.</summary>
        IPX = 11,

        /// <summary>AppleTalk.</summary>
        APPLETALK = 12,

        /// <summary>DECnet Phase IV.</summary>
        DECNET_IV = 13,

        /// <summary>Banyan Vines.</summary>
        BANYAN_VINES = 14,

        /// <summary>E.164 with NSAP-format subaddress.</summary>
        E164_NSAP = 15
    }

    /// <summary>
    /// Defines zone access behaviour for ARAP sessions, as used in the
    /// ARAP-Zone-Access attribute (RFC 2869 §5.6, <see cref="RadiusAttributeType.ARAP_ZONE_ACCESS"/>).
    /// </summary>
    public enum ARAP_ZONE_ACCESS
    {
        /// <summary>Only allow access to the default zone.</summary>
        ONLY_ALLOW_ACCESS_TO_DEFAULT_ZONE = 1,

        /// <summary>Use the zone filter inclusively (allow listed zones).</summary>
        USE_ZONE_FILTER_INCLUSIVELY = 2,

        /// <summary>Not used.</summary>
        NOT_USED = 3,

        /// <summary>Use the zone filter exclusively (deny listed zones).</summary>
        USE_ZONE_FILTER_EXCLUSIVELY = 4
    }

    /// <summary>
    /// Defines whether the NAS should echo user input, as used in the
    /// Prompt attribute (RFC 2869 §5.10, <see cref="RadiusAttributeType.PROMPT"/>).
    /// </summary>
    public enum PROMPT
    {
        /// <summary>Do not echo user input (e.g., for password entry).</summary>
        NO_ECHO = 0,

        /// <summary>Echo user input.</summary>
        ECHO = 1
    }

    /// <summary>
    /// Defines the ingress filter setting, as used in the
    /// Ingress-Filters attribute (RFC 4675 §2.2, <see cref="RadiusAttributeType.INGRESS_FILTERS"/>).
    /// </summary>
    public enum INGRESS_FILTERS_VALUE
    {
        /// <summary>Ingress filters are enabled on the port.</summary>
        ENABLED = 1,

        /// <summary>Ingress filters are disabled on the port.</summary>
        DISABLED = 2
    }

    /// <summary>
    /// Defines error codes for CoA and Disconnect responses, as used in the
    /// Error-Cause attribute (RFC 5176 §3.6, <see cref="RadiusAttributeType.ERROR_CAUSE"/>).
    /// </summary>
    public enum ERROR_CAUSE
    {
        /// <summary>Residual session context was removed.</summary>
        RESIDUAL_SESSION_CONTEXT_REMOVED = 201,

        /// <summary>An invalid EAP packet was received.</summary>
        INVALID_EAP_PACKET = 202,

        /// <summary>An unsupported attribute was included in the request.</summary>
        UNSUPPORTED_ATTRIBUTE = 401,

        /// <summary>A required attribute is missing from the request.</summary>
        MISSING_ATTRIBUTE = 402,

        /// <summary>The NAS identification does not match.</summary>
        NAS_IDENTIFICATION_MISMATCH = 403,

        /// <summary>The request is invalid.</summary>
        INVALID_REQUEST = 404,

        /// <summary>The requested service is not supported.</summary>
        UNSUPPORTED_SERVICE = 405,

        /// <summary>The requested extension is not supported.</summary>
        UNSUPPORTED_EXTENSION = 406,

        /// <summary>An attribute contains an invalid value.</summary>
        INVALID_ATTRIBUTE_VALUE = 407,

        /// <summary>The request was administratively prohibited.</summary>
        ADMINISTRATIVELY_PROHIBITED = 501,

        /// <summary>The request cannot be routed.</summary>
        REQUEST_NOT_ROUTABLE = 502,

        /// <summary>The session context was not found.</summary>
        SESSION_CONTEXT_NOT_FOUND = 503,

        /// <summary>The session context cannot be removed.</summary>
        SESSION_CONTEXT_NOT_REMOVABLE = 504,

        /// <summary>A proxy encountered an unspecified processing error.</summary>
        OTHER_PROXY_PROCESSING_ERROR = 505,

        /// <summary>Resources are unavailable to fulfil the request.</summary>
        RESOURCES_UNAVAILABLE = 506,

        /// <summary>The requested action has been initiated.</summary>
        REQUEST_INITIATED = 507,

        /// <summary>Multiple session selection is not supported.</summary>
        MULTIPLE_SESSION_SELECTION_UNSUPPORTED = 508,

        /// <summary>Location information is required to fulfil the request.</summary>
        LOCATION_INFO_REQUIRED = 509,

        /// <summary>The response packet exceeds the maximum allowed size.</summary>
        RESPONSE_TOO_BIG = 601
    }

    /// <summary>
    /// Defines namespace identifier values for the Operator-Name attribute
    /// (RFC 5580 §4.1, <see cref="RadiusAttributeType.OPERATOR_NAME"/>).
    /// </summary>
    public enum NAMESPACE_IDENTIFIER
    {
        /// <summary>TADIG (Transferred Account Data Interchange Group) operator identifier.</summary>
        TADIG = 0x30,

        /// <summary>Realm (FQDN) operator identifier.</summary>
        REALM = 0x31,

        /// <summary>E.212 (MCC+MNC) operator identifier.</summary>
        E212 = 0x32,

        /// <summary>ICC (International Code Designator) operator identifier.</summary>
        ICC = 0x33,

        /// <summary>WBA Identifier (Wi-Fi Alliance).</summary>
        WBAID = 0x34
    }

    /// <summary>
    /// Defines the location profile encoding format within the
    /// Location-Information attribute (RFC 5580 §4.2, <see cref="RadiusAttributeType.LOCATION_INFORMATION"/>).
    /// </summary>
    public enum LOCATION_PROFILES
    {
        /// <summary>Civic location profile (e.g., street address).</summary>
        CIVIC_LOCATION = 0,

        /// <summary>Geospatial location profile (e.g., latitude/longitude).</summary>
        GEOSPATIAL_LOCATION_PROFILE = 1
    }

    /// <summary>
    /// Defines the entity that generated the location information within the
    /// Location-Information attribute (RFC 5580 §4.2).
    /// </summary>
    /// <remarks>
    /// Note: This enum is distinct from <see cref="RadiusAttributeType.LOCATION_INFORMATION"/>
    /// (the attribute type identifier) and describes the location source field within that attribute's value.
    /// </remarks>
    public enum LOCATION_INFORMATION_SOURCE
    {
        /// <summary>Location was determined by the client device.</summary>
        CLIENT_DEVICE = 0,

        /// <summary>Location was determined by the RADIUS/NAS device.</summary>
        RADIUS_DEVICE = 1
    }

    /// <summary>
    /// Defines the retransmission policy flags for the
    /// Basic-Location-Policy-Rules attribute (RFC 5580 §4.4, <see cref="RadiusAttributeType.BASIC_LOCATION_POLICY_RULES"/>).
    /// </summary>
    public enum BASIC_LOCATION_POLICY_RULES
    {
        /// <summary>Retransmission of location data to third parties is allowed.</summary>
        RETRANSMISSION_ALLOWED = 0
    }

    /// <summary>
    /// Defines the location capabilities of the NAS, as used in the
    /// Location-Capable attribute (RFC 5580 §4.6, <see cref="RadiusAttributeType.LOCATION_CAPABLE"/>).
    /// </summary>
    /// <remarks>
    /// This is a bitmask enum — multiple values may be combined with bitwise OR.
    /// </remarks>
    [Flags]
    public enum LOCATION_CAPABLE
    {
        /// <summary>The NAS can provide civic location information.</summary>
        CIVIC_LOCATION = 1,

        /// <summary>The NAS can provide geospatial location information.</summary>
        GEO_LOCATION = 2,

        /// <summary>The NAS can provide user location information.</summary>
        USER_LOCATION = 4,

        /// <summary>The NAS can provide its own location information.</summary>
        NAS_LOCATION = 8
    }

    /// <summary>
    /// Defines the types of location information being requested, as used in the
    /// Requested-Location-Info attribute (RFC 5580 §4.7, <see cref="RadiusAttributeType.REQUESTED_LOCATION_INFO"/>).
    /// </summary>
    /// <remarks>
    /// This is a bitmask enum — multiple values may be combined with bitwise OR.
    /// </remarks>
    [Flags]
    public enum REQUESTED_LOCATION_INFO
    {
        /// <summary>Request civic location information.</summary>
        CIVIC_LOCATION = 1,

        /// <summary>Request geospatial location information.</summary>
        GEO_LOCATION = 2,

        /// <summary>Request the user's location.</summary>
        USERS_LOCATION = 4,

        /// <summary>Request the NAS's own location.</summary>
        NAS_LOCATION = 8,

        /// <summary>Request that future location updates also be sent.</summary>
        FUTURE_REQUESTS = 16,

        /// <summary>Request that no location information be sent.</summary>
        NONE = 32
    }

    /// <summary>
    /// Defines the management protocol to use for a framed management session, as used in the
    /// Framed-Management-Protocol attribute (RFC 5607 §4.1, <see cref="RadiusAttributeType.FRAMED_MANAGEMENT_PROTOCOL"/>).
    /// </summary>
    public enum FRAMED_MANAGEMENT_PROTOCOL
    {
        /// <summary>Simple Network Management Protocol.</summary>
        SNMP = 1,

        /// <summary>Web-based management interface (HTTP/HTTPS).</summary>
        WEB = 2,

        /// <summary>NETCONF (RFC 6241).</summary>
        NETCONF = 3,

        /// <summary>File Transfer Protocol.</summary>
        FTP = 4,

        /// <summary>Trivial File Transfer Protocol.</summary>
        TFTP = 5,

        /// <summary>SSH File Transfer Protocol.</summary>
        SFTP = 6,

        /// <summary>Remote Copy Protocol.</summary>
        RCP = 7,

        /// <summary>Secure Copy Protocol.</summary>
        SCP = 8
    }

    /// <summary>
    /// Defines the transport security level required for a management session, as used in the
    /// Management-Transport-Protection attribute (RFC 5607 §4.2, <see cref="RadiusAttributeType.MANAGEMENT_TRANSPORT_PROTECTION"/>).
    /// </summary>
    public enum MANAGEMENT_TRANSPORT_PROTECTION
    {
        /// <summary>No transport protection required.</summary>
        NO_PROTECTION = 1,

        /// <summary>Integrity protection required (e.g., SSH, SNMPv3 auth).</summary>
        INTEGRITY_PROTECTION = 2,

        /// <summary>Both integrity and confidentiality protection required (e.g., SSH with encryption, SNMPv3 auth+priv).</summary>
        INTEGRITY_CONFIDENTIALITY_PROTECTION = 3
    }

    /// <summary>
    /// Defines the EAP lower-layer type, as used in the
    /// EAP-Lower-Layer attribute (RFC 7057 §4.1, <see cref="RadiusAttributeType.EAP_LOWER_LAYER"/>).
    /// </summary>
    public enum EAP_LOWER_LAYER
    {
        /// <summary>IEEE 802.3 (Ethernet).</summary>
        IEEE_802_3 = 1,

        /// <summary>IEEE 802.11 (Wi-Fi).</summary>
        IEEE_802_11 = 2,

        /// <summary>IEEE 802.16 (WiMAX).</summary>
        IEEE_802_16 = 3,

        /// <summary>IEEE 802.1X.</summary>
        IEEE_802_1X = 4,

        /// <summary>PPP.</summary>
        PPP = 5,

        /// <summary>IKEv2 (RFC 5106).</summary>
        IKEv2 = 6,

        /// <summary>3GPP EPS (LTE).</summary>
        THIRDGPP_EPS = 7,

        /// <summary>CDMA2000.</summary>
        CDMA2000 = 8
    }

    /// <summary>
    /// Defines the fragmentation status for EAP fragmentation, as referenced in
    /// vendor-specific or extended RADIUS attribute implementations (RFC 7930).
    /// </summary>
    public enum FRAG_STATUS
    {
        /// <summary>Reserved value; not used.</summary>
        RESERVED = 0,

        /// <summary>Fragmentation is supported by this peer.</summary>
        FRAGMENTATION_SUPPORTED = 1,

        /// <summary>More data fragments are pending from the sender.</summary>
        MORE_DATA_PENDING = 2,

        /// <summary>The receiver is requesting more data fragments.</summary>
        MORE_DATA_REQUEST = 3
    }
}