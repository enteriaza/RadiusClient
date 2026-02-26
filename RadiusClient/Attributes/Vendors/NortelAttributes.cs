using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Nortel Networks (IANA PEN 562) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.nortel</c>.
    /// </summary>
    /// <remarks>
    /// Nortel Networks (formerly Northern Telecom, acquired and dissolved — assets
    /// purchased by Avaya, Ciena, Ericsson, and others) produced enterprise networking
    /// equipment including Ethernet Routing Switch (ERS/BayStack), Passport/VSP,
    /// Contivity/Nortel VPN, WLAN, and Meridian/CS1000 platforms.
    /// </remarks>
    public enum NortelAttributeType : byte
    {
        /// <summary>Nortel-User-Role (Type 1). String. User role name.</summary>
        USER_ROLE = 1,

        /// <summary>Nortel-Privilege-Level (Type 2). Integer. CLI privilege level.</summary>
        PRIVILEGE_LEVEL = 2,

        /// <summary>Nortel-AVPair (Type 3). String. Attribute-value pair string.</summary>
        AVPAIR = 3,

        /// <summary>Nortel-VLAN-Id (Type 4). Integer. VLAN identifier to assign.</summary>
        VLAN_ID = 4,

        /// <summary>Nortel-VLAN-Name (Type 5). String. VLAN name to assign.</summary>
        VLAN_NAME = 5,

        /// <summary>Nortel-Policy-Name (Type 6). String. Policy name to apply.</summary>
        POLICY_NAME = 6,

        /// <summary>Nortel-Port-Priority (Type 7). Integer. Port priority level.</summary>
        PORT_PRIORITY = 7,

        /// <summary>Nortel-CoS (Type 8). Integer. Class of Service value.</summary>
        COS = 8,

        /// <summary>Nortel-Bandwidth-Max-Ingress (Type 9). Integer. Maximum ingress bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_INGRESS = 9,

        /// <summary>Nortel-Bandwidth-Max-Egress (Type 10). Integer. Maximum egress bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_EGRESS = 10,

        /// <summary>Nortel-ACL-Id (Type 11). Integer. ACL identifier.</summary>
        ACL_ID = 11,

        /// <summary>Nortel-ACL-Name (Type 12). String. ACL name to apply.</summary>
        ACL_NAME = 12,

        /// <summary>Nortel-Access-Level (Type 13). Integer. Management access level.</summary>
        ACCESS_LEVEL = 13,

        /// <summary>Nortel-Session-Timeout (Type 14). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 14,

        /// <summary>Nortel-Idle-Timeout (Type 15). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 15,

        /// <summary>Nortel-Filter-Id (Type 16). String. Filter identifier.</summary>
        FILTER_ID = 16,

        /// <summary>Nortel-Redirect-URL (Type 17). String. Redirect URL.</summary>
        REDIRECT_URL = 17,

        /// <summary>Nortel-SSID (Type 18). String. Wireless SSID name.</summary>
        SSID = 18,

        /// <summary>Nortel-AP-Name (Type 19). String. Access point name.</summary>
        AP_NAME = 19,

        /// <summary>Nortel-AP-IP-Address (Type 20). IP address. Access point IP address.</summary>
        AP_IP_ADDRESS = 20
    }

    /// <summary>
    /// Nortel-Privilege-Level attribute values (Type 2).
    /// </summary>
    public enum NORTEL_PRIVILEGE_LEVEL
    {
        /// <summary>Read-only access.</summary>
        READ_ONLY = 0,

        /// <summary>Layer 1 read-write access.</summary>
        L1_READ_WRITE = 1,

        /// <summary>Layer 2 read-write access.</summary>
        L2_READ_WRITE = 2,

        /// <summary>Layer 3 read-write access.</summary>
        L3_READ_WRITE = 3,

        /// <summary>Full read-write access.</summary>
        READ_WRITE = 4,

        /// <summary>Read-write-all (super-user) access.</summary>
        READ_WRITE_ALL = 5
    }

    /// <summary>
    /// Nortel-Access-Level attribute values (Type 13).
    /// </summary>
    public enum NORTEL_ACCESS_LEVEL
    {
        /// <summary>No management access.</summary>
        NONE = 0,

        /// <summary>Read-only management access.</summary>
        READ_ONLY = 1,

        /// <summary>Read-write management access.</summary>
        READ_WRITE = 2,

        /// <summary>Full administrative management access.</summary>
        ADMIN = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Nortel Networks
    /// (IANA PEN 562) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.nortel</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Nortel's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 562</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Nortel Networks (now Avaya/Extreme) Ethernet
    /// Routing Switches (ERS/BayStack), Passport/VSP, and WLAN platforms for
    /// RADIUS-based user role assignment, CLI privilege level control, VLAN
    /// assignment (by ID and name), policy and ACL enforcement (by ID and name),
    /// port priority and CoS assignment, ingress/egress bandwidth provisioning,
    /// management access level control, session and idle timeout management,
    /// filter ID, URL redirection, wireless SSID and AP identification, and
    /// general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(NortelAttributes.UserRole("network-admin"));
    /// packet.SetAttribute(NortelAttributes.PrivilegeLevel(NORTEL_PRIVILEGE_LEVEL.READ_WRITE_ALL));
    /// packet.SetAttribute(NortelAttributes.VlanId(200));
    /// packet.SetAttribute(NortelAttributes.BandwidthMaxIngress(100000));
    /// packet.SetAttribute(NortelAttributes.BandwidthMaxEgress(50000));
    /// packet.SetAttribute(NortelAttributes.PolicyName("corporate-policy"));
    /// </code>
    /// </remarks>
    public static class NortelAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Nortel Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 562;

        #region Integer Attributes

        /// <summary>Creates a Nortel-Privilege-Level attribute (Type 2).</summary>
        /// <param name="value">The CLI privilege level. See <see cref="NORTEL_PRIVILEGE_LEVEL"/>.</param>
        public static VendorSpecificAttributes PrivilegeLevel(NORTEL_PRIVILEGE_LEVEL value) => CreateInteger(NortelAttributeType.PRIVILEGE_LEVEL, (int)value);

        /// <summary>Creates a Nortel-VLAN-Id attribute (Type 4).</summary>
        /// <param name="value">The VLAN identifier to assign.</param>
        public static VendorSpecificAttributes VlanId(int value) => CreateInteger(NortelAttributeType.VLAN_ID, value);

        /// <summary>Creates a Nortel-Port-Priority attribute (Type 7).</summary>
        /// <param name="value">The port priority level (0–7).</param>
        public static VendorSpecificAttributes PortPriority(int value) => CreateInteger(NortelAttributeType.PORT_PRIORITY, value);

        /// <summary>Creates a Nortel-CoS attribute (Type 8).</summary>
        /// <param name="value">The Class of Service value.</param>
        public static VendorSpecificAttributes Cos(int value) => CreateInteger(NortelAttributeType.COS, value);

        /// <summary>Creates a Nortel-Bandwidth-Max-Ingress attribute (Type 9).</summary>
        /// <param name="value">The maximum ingress bandwidth in Kbps.</param>
        public static VendorSpecificAttributes BandwidthMaxIngress(int value) => CreateInteger(NortelAttributeType.BANDWIDTH_MAX_INGRESS, value);

        /// <summary>Creates a Nortel-Bandwidth-Max-Egress attribute (Type 10).</summary>
        /// <param name="value">The maximum egress bandwidth in Kbps.</param>
        public static VendorSpecificAttributes BandwidthMaxEgress(int value) => CreateInteger(NortelAttributeType.BANDWIDTH_MAX_EGRESS, value);

        /// <summary>Creates a Nortel-ACL-Id attribute (Type 11).</summary>
        /// <param name="value">The ACL identifier.</param>
        public static VendorSpecificAttributes AclId(int value) => CreateInteger(NortelAttributeType.ACL_ID, value);

        /// <summary>Creates a Nortel-Access-Level attribute (Type 13).</summary>
        /// <param name="value">The management access level. See <see cref="NORTEL_ACCESS_LEVEL"/>.</param>
        public static VendorSpecificAttributes AccessLevel(NORTEL_ACCESS_LEVEL value) => CreateInteger(NortelAttributeType.ACCESS_LEVEL, (int)value);

        /// <summary>Creates a Nortel-Session-Timeout attribute (Type 14).</summary>
        /// <param name="value">The session timeout in seconds.</param>
        public static VendorSpecificAttributes SessionTimeout(int value) => CreateInteger(NortelAttributeType.SESSION_TIMEOUT, value);

        /// <summary>Creates a Nortel-Idle-Timeout attribute (Type 15).</summary>
        /// <param name="value">The idle timeout in seconds.</param>
        public static VendorSpecificAttributes IdleTimeout(int value) => CreateInteger(NortelAttributeType.IDLE_TIMEOUT, value);

        #endregion

        #region String Attributes

        /// <summary>Creates a Nortel-User-Role attribute (Type 1).</summary>
        /// <param name="value">The user role name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserRole(string value) => CreateString(NortelAttributeType.USER_ROLE, value);

        /// <summary>Creates a Nortel-AVPair attribute (Type 3).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(NortelAttributeType.AVPAIR, value);

        /// <summary>Creates a Nortel-VLAN-Name attribute (Type 5).</summary>
        /// <param name="value">The VLAN name to assign. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes VlanName(string value) => CreateString(NortelAttributeType.VLAN_NAME, value);

        /// <summary>Creates a Nortel-Policy-Name attribute (Type 6).</summary>
        /// <param name="value">The policy name to apply. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes PolicyName(string value) => CreateString(NortelAttributeType.POLICY_NAME, value);

        /// <summary>Creates a Nortel-ACL-Name attribute (Type 12).</summary>
        /// <param name="value">The ACL name to apply. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AclName(string value) => CreateString(NortelAttributeType.ACL_NAME, value);

        /// <summary>Creates a Nortel-Filter-Id attribute (Type 16).</summary>
        /// <param name="value">The filter identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes FilterId(string value) => CreateString(NortelAttributeType.FILTER_ID, value);

        /// <summary>Creates a Nortel-Redirect-URL attribute (Type 17).</summary>
        /// <param name="value">The redirect URL. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value) => CreateString(NortelAttributeType.REDIRECT_URL, value);

        /// <summary>Creates a Nortel-SSID attribute (Type 18).</summary>
        /// <param name="value">The wireless SSID name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ssid(string value) => CreateString(NortelAttributeType.SSID, value);

        /// <summary>Creates a Nortel-AP-Name attribute (Type 19).</summary>
        /// <param name="value">The access point name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ApName(string value) => CreateString(NortelAttributeType.AP_NAME, value);

        #endregion

        #region IP Address Attributes

        /// <summary>
        /// Creates a Nortel-AP-IP-Address attribute (Type 20) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">The access point IP address. Must be IPv4. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes ApIpAddress(IPAddress value)
        {
            return CreateIpv4(NortelAttributeType.AP_IP_ADDRESS, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(NortelAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(NortelAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateIpv4(NortelAttributeType type, IPAddress value)
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