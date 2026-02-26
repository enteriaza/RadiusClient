using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Versanet (IANA PEN 2180) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.versanet</c>.
    /// </summary>
    /// <remarks>
    /// Versanet Communications produced broadband access and DSL equipment for
    /// service providers and ISPs, providing subscriber authentication, session
    /// management, and bandwidth control capabilities.
    /// </remarks>
    public enum VersanetAttributeType : byte
    {
        /// <summary>Versanet-Termination-Cause (Type 1). Integer. Session termination cause.</summary>
        TERMINATION_CAUSE = 1,

        /// <summary>Versanet-User-Group (Type 2). String. User group name.</summary>
        USER_GROUP = 2,

        /// <summary>Versanet-Service-Profile (Type 3). String. Service profile name.</summary>
        SERVICE_PROFILE = 3,

        /// <summary>Versanet-Bandwidth-Max-Up (Type 4). Integer. Maximum upstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_UP = 4,

        /// <summary>Versanet-Bandwidth-Max-Down (Type 5). Integer. Maximum downstream bandwidth in Kbps.</summary>
        BANDWIDTH_MAX_DOWN = 5,

        /// <summary>Versanet-Session-Timeout (Type 6). Integer. Session timeout in seconds.</summary>
        SESSION_TIMEOUT = 6,

        /// <summary>Versanet-Idle-Timeout (Type 7). Integer. Idle timeout in seconds.</summary>
        IDLE_TIMEOUT = 7,

        /// <summary>Versanet-VLAN-Id (Type 8). Integer. VLAN identifier.</summary>
        VLAN_ID = 8,

        /// <summary>Versanet-Redirect-URL (Type 9). String. Captive portal redirect URL.</summary>
        REDIRECT_URL = 9,

        /// <summary>Versanet-Max-Sessions (Type 10). Integer. Maximum simultaneous sessions.</summary>
        MAX_SESSIONS = 10
    }

    /// <summary>
    /// Versanet-Termination-Cause attribute values (Type 1).
    /// </summary>
    public enum VERSANET_TERMINATION_CAUSE
    {
        /// <summary>Normal session termination.</summary>
        NORMAL = 0,

        /// <summary>Session terminated due to error.</summary>
        ERROR = 1,

        /// <summary>Session terminated by account expiration.</summary>
        ACCOUNT_EXPIRED = 2,

        /// <summary>Session terminated by session limit.</summary>
        SESSION_LIMIT = 3,

        /// <summary>Session terminated by administrative action.</summary>
        ADMIN_RESET = 4
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Versanet
    /// (IANA PEN 2180) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.versanet</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Versanet's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 2180</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Versanet Communications broadband access and DSL
    /// equipment for RADIUS-based session termination cause reporting, user group
    /// assignment, service profile selection, upstream/downstream bandwidth
    /// provisioning, session and idle timeout management, VLAN assignment,
    /// captive portal URL redirection, maximum simultaneous session enforcement,
    /// and general-purpose attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(VersanetAttributes.ServiceProfile("dsl-premium"));
    /// packet.SetAttribute(VersanetAttributes.BandwidthMaxUp(10000));
    /// packet.SetAttribute(VersanetAttributes.BandwidthMaxDown(50000));
    /// packet.SetAttribute(VersanetAttributes.VlanId(100));
    /// packet.SetAttribute(VersanetAttributes.SessionTimeout(86400));
    /// </code>
    /// </remarks>
    public static class VersanetAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Versanet Communications.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 2180;

        #region Integer Attributes

        /// <summary>
        /// Creates a Versanet-Termination-Cause attribute (Type 1) with the specified cause.
        /// </summary>
        /// <param name="value">The session termination cause. See <see cref="VERSANET_TERMINATION_CAUSE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes TerminationCause(VERSANET_TERMINATION_CAUSE value)
        {
            return CreateInteger(VersanetAttributeType.TERMINATION_CAUSE, (int)value);
        }

        /// <summary>
        /// Creates a Versanet-Bandwidth-Max-Up attribute (Type 4) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum upstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxUp(int value)
        {
            return CreateInteger(VersanetAttributeType.BANDWIDTH_MAX_UP, value);
        }

        /// <summary>
        /// Creates a Versanet-Bandwidth-Max-Down attribute (Type 5) with the specified rate.
        /// </summary>
        /// <param name="value">The maximum downstream bandwidth in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes BandwidthMaxDown(int value)
        {
            return CreateInteger(VersanetAttributeType.BANDWIDTH_MAX_DOWN, value);
        }

        /// <summary>
        /// Creates a Versanet-Session-Timeout attribute (Type 6) with the specified timeout.
        /// </summary>
        /// <param name="value">The session timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes SessionTimeout(int value)
        {
            return CreateInteger(VersanetAttributeType.SESSION_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Versanet-Idle-Timeout attribute (Type 7) with the specified timeout.
        /// </summary>
        /// <param name="value">The idle timeout in seconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes IdleTimeout(int value)
        {
            return CreateInteger(VersanetAttributeType.IDLE_TIMEOUT, value);
        }

        /// <summary>
        /// Creates a Versanet-VLAN-Id attribute (Type 8) with the specified VLAN identifier.
        /// </summary>
        /// <param name="value">The VLAN identifier.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes VlanId(int value)
        {
            return CreateInteger(VersanetAttributeType.VLAN_ID, value);
        }

        /// <summary>
        /// Creates a Versanet-Max-Sessions attribute (Type 10) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum number of simultaneous sessions.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSessions(int value)
        {
            return CreateInteger(VersanetAttributeType.MAX_SESSIONS, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates a Versanet-User-Group attribute (Type 2) with the specified group name.
        /// </summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes UserGroup(string value)
        {
            return CreateString(VersanetAttributeType.USER_GROUP, value);
        }

        /// <summary>
        /// Creates a Versanet-Service-Profile attribute (Type 3) with the specified profile name.
        /// </summary>
        /// <param name="value">The service profile name. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes ServiceProfile(string value)
        {
            return CreateString(VersanetAttributeType.SERVICE_PROFILE, value);
        }

        /// <summary>
        /// Creates a Versanet-Redirect-URL attribute (Type 9) with the specified URL.
        /// </summary>
        /// <param name="value">The captive portal redirect URL. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes RedirectUrl(string value)
        {
            return CreateString(VersanetAttributeType.REDIRECT_URL, value);
        }

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(VersanetAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(VersanetAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}