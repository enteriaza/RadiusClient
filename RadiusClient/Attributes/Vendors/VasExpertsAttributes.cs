using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a VAS Experts (IANA PEN 43823) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.vasexperts</c>.
    /// </summary>
    /// <remarks>
    /// VAS Experts produces deep packet inspection (DPI), traffic management, and
    /// subscriber analytics platforms (СКАТ/SKAT DPI) for ISPs and
    /// telecommunications operators, providing policy enforcement, traffic
    /// classification, lawful intercept, and quality of experience management.
    /// </remarks>
    public enum VasExpertsAttributeType : byte
    {
        /// <summary>VasExperts-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>VasExperts-Service-Profile (Type 2). String. DPI service profile name.</summary>
        SERVICE_PROFILE = 2,

        /// <summary>VasExperts-Policing-Profile (Type 3). String. Policing profile name.</summary>
        POLICING_PROFILE = 3,

        /// <summary>VasExperts-Shaping-Profile (Type 4). String. Traffic shaping profile name.</summary>
        SHAPING_PROFILE = 4,

        /// <summary>VasExperts-Bandwidth-Max-Up (Type 5). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 5,

        /// <summary>VasExperts-Bandwidth-Max-Down (Type 6). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 6,

        /// <summary>VasExperts-VLAN-Id (Type 7). Integer. VLAN identifier.</summary>
        VLAN_ID = 7,

        /// <summary>VasExperts-User-Group (Type 8). String. User group name.</summary>
        USER_GROUP = 8,

        /// <summary>VasExperts-Redirect-URL (Type 9). String. Redirect URL.</summary>
        REDIRECT_URL = 9,

        /// <summary>VasExperts-Subscriber-Id (Type 10). String. Subscriber identifier.</summary>
        SUBSCRIBER_ID = 10,

        /// <summary>VasExperts-Subscriber-IP (Type 11). IP address. Subscriber IP address.</summary>
        SUBSCRIBER_IP = 11,

        /// <summary>VasExperts-NAT-Pool (Type 12). String. NAT pool name.</summary>
        NAT_POOL = 12,

        /// <summary>VasExperts-NAT-IP (Type 13). IP address. NAT translated IP address.</summary>
        NAT_IP = 13,

        /// <summary>VasExperts-Session-Timeout (Type 14). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 14,

        /// <summary>VasExperts-Idle-Timeout (Type 15). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 15,

        /// <summary>VasExperts-Max-Sessions (Type 16). Integer. Maximum simultaneous sessions.</summary>
        MAX_SESSIONS = 16,

        /// <summary>VasExperts-Filter-Id (Type 17). String. Filter identifier.</summary>
        FILTER_ID = 17,

        /// <summary>VasExperts-ACL-Name (Type 18). String. ACL name to apply.</summary>
        ACL_NAME = 18,

        /// <summary>VasExperts-QoS-Profile (Type 19). String. QoS profile name.</summary>
        QOS_PROFILE = 19,

        /// <summary>VasExperts-CoA-Profile (Type 20). String. Change of Authorization profile name.</summary>
        COA_PROFILE = 20
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing VAS Experts
    /// (IANA PEN 43823) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.vasexperts</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// VAS Experts' vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 43823</c>.
    /// </para>
    /// <para>
    /// These attributes are used by VAS Experts СКАТ/SKAT DPI platforms for
    /// RADIUS-based DPI service profile selection, policing and shaping profile
    /// assignment, upstream/downstream bandwidth provisioning, VLAN assignment,
    /// user group mapping, URL redirection, subscriber identification and IP
    /// assignment, NAT pool and IP provisioning, session and idle timeout
    /// management, maximum simultaneous session enforcement, filter and ACL
    /// assignment, QoS profile selection, CoA profile configuration, and
    /// general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(VasExpertsAttributes.ServiceProfile("residential-100m"));
    /// packet.SetAttribute(VasExpertsAttributes.ShapingProfile("shaper-100m"));
    /// packet.SetAttribute(VasExpertsAttributes.BandwidthMaxUp(50000));
    /// packet.SetAttribute(VasExpertsAttributes.BandwidthMaxDown(100000));
    /// packet.SetAttribute(VasExpertsAttributes.SubscriberIp(IPAddress.Parse("10.0.1.100")));
    /// packet.SetAttribute(VasExpertsAttributes.NatPool("public-pool-1"));
    /// </code>
    /// </remarks>
    public static class VasExpertsAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for VAS Experts.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 43823;

        #region Integer Attributes

        /// <summary>Creates a VasExperts-Bandwidth-Max-Up attribute (Type 5).</summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        public static VendorSpecificAttributes BandwidthMaxUp(int value) => CreateInteger(VasExpertsAttributeType.BANDWIDTH_MAX_UP, value);

        /// <summary>Creates a VasExperts-Bandwidth-Max-Down attribute (Type 6).</summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        public static VendorSpecificAttributes BandwidthMaxDown(int value) => CreateInteger(VasExpertsAttributeType.BANDWIDTH_MAX_DOWN, value);

        /// <summary>Creates a VasExperts-VLAN-Id attribute (Type 7).</summary>
        /// <param name="value">The VLAN identifier.</param>
        public static VendorSpecificAttributes VlanId(int value) => CreateInteger(VasExpertsAttributeType.VLAN_ID, value);

        /// <summary>Creates a VasExperts-Session-Timeout attribute (Type 14).</summary>
        /// <param name="value">The session timeout in seconds.</param>
        public static VendorSpecificAttributes SessionTimeout(int value) => CreateInteger(VasExpertsAttributeType.SESSION_TIMEOUT, value);

        /// <summary>Creates a VasExperts-Idle-Timeout attribute (Type 15).</summary>
        /// <param name="value">The idle timeout in seconds.</param>
        public static VendorSpecificAttributes IdleTimeout(int value) => CreateInteger(VasExpertsAttributeType.IDLE_TIMEOUT, value);

        /// <summary>Creates a VasExperts-Max-Sessions attribute (Type 16).</summary>
        /// <param name="value">The maximum number of simultaneous sessions.</param>
        public static VendorSpecificAttributes MaxSessions(int value) => CreateInteger(VasExpertsAttributeType.MAX_SESSIONS, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a VasExperts-AVPair attribute (Type 1).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(VasExpertsAttributeType.AVPAIR, value);

        /// <summary>Creates a VasExperts-Service-Profile attribute (Type 2).</summary>
        /// <param name="value">The DPI service profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value) => CreateString(VasExpertsAttributeType.SERVICE_PROFILE, value);

        /// <summary>Creates a VasExperts-Policing-Profile attribute (Type 3).</summary>
        /// <param name="value">The policing profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicingProfile(string value) => CreateString(VasExpertsAttributeType.POLICING_PROFILE, value);

        /// <summary>Creates a VasExperts-Shaping-Profile attribute (Type 4).</summary>
        /// <param name="value">The traffic shaping profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ShapingProfile(string value) => CreateString(VasExpertsAttributeType.SHAPING_PROFILE, value);

        /// <summary>Creates a VasExperts-User-Group attribute (Type 8).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value) => CreateString(VasExpertsAttributeType.USER_GROUP, value);

        /// <summary>Creates a VasExperts-Redirect-URL attribute (Type 9).</summary>
        /// <param name="value">The redirect URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value) => CreateString(VasExpertsAttributeType.REDIRECT_URL, value);

        /// <summary>Creates a VasExperts-Subscriber-Id attribute (Type 10).</summary>
        /// <param name="value">The subscriber identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SubscriberId(string value) => CreateString(VasExpertsAttributeType.SUBSCRIBER_ID, value);

        /// <summary>Creates a VasExperts-NAT-Pool attribute (Type 12).</summary>
        /// <param name="value">The NAT pool name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes NatPool(string value) => CreateString(VasExpertsAttributeType.NAT_POOL, value);

        /// <summary>Creates a VasExperts-Filter-Id attribute (Type 17).</summary>
        /// <param name="value">The filter identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FilterId(string value) => CreateString(VasExpertsAttributeType.FILTER_ID, value);

        /// <summary>Creates a VasExperts-ACL-Name attribute (Type 18).</summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value) => CreateString(VasExpertsAttributeType.ACL_NAME, value);

        /// <summary>Creates a VasExperts-QoS-Profile attribute (Type 19).</summary>
        /// <param name="value">The QoS profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes QosProfile(string value) => CreateString(VasExpertsAttributeType.QOS_PROFILE, value);

        /// <summary>Creates a VasExperts-CoA-Profile attribute (Type 20).</summary>
        /// <param name="value">The Change of Authorization profile name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CoaProfile(string value) => CreateString(VasExpertsAttributeType.COA_PROFILE, value);

        #endregion

        #region IP Address Attributes

        /// <summary>Creates a VasExperts-Subscriber-IP attribute (Type 11).</summary>
        /// <param name="value">The subscriber IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SubscriberIp(IPAddress value) => CreateIpv4(VasExpertsAttributeType.SUBSCRIBER_IP, value);

        /// <summary>Creates a VasExperts-NAT-IP attribute (Type 13).</summary>
        /// <param name="value">The NAT translated IP address. Must be IPv4.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes NatIp(IPAddress value) => CreateIpv4(VasExpertsAttributeType.NAT_IP, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(VasExpertsAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(VasExpertsAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(VasExpertsAttributeType type, IPAddress value)
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