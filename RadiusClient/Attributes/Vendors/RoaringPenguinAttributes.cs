using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Roaring Penguin Software (IANA PEN 10055) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.roaringpenguin</c>.
    /// </summary>
    /// <remarks>
    /// Roaring Penguin Software produces PPPoE (Point-to-Point Protocol over
    /// Ethernet) server software (rp-pppoe) and CanIt anti-spam solutions,
    /// widely used by ISPs for broadband subscriber management.
    /// </remarks>
    public enum RoaringPenguinAttributeType : byte
    {
        /// <summary>RP-Upstream-Speed-Limit (Type 1). Integer. Upstream speed limit in Kbps.</summary>
        UPSTREAM_SPEED_LIMIT = 1,

        /// <summary>RP-Downstream-Speed-Limit (Type 2). Integer. Downstream speed limit in Kbps.</summary>
        DOWNSTREAM_SPEED_LIMIT = 2,

        /// <summary>RP-HURL (Type 3). String. HTTP URL redirect (captive portal).</summary>
        HURL = 3,

        /// <summary>RP-MOTM (Type 4). String. Message of the Moment.</summary>
        MOTM = 4,

        /// <summary>RP-Max-Sessions-Per-User (Type 5). Integer. Maximum simultaneous sessions per user.</summary>
        MAX_SESSIONS_PER_USER = 5
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Roaring Penguin Software
    /// (IANA PEN 10055) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.roaringpenguin</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Roaring Penguin's vendor-specific attributes follow the standard VSA layout
    /// defined in RFC 2865 §5.26. All attributes produced by this class are wrapped
    /// in a <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 10055</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Roaring Penguin rp-pppoe PPPoE servers and
    /// related ISP subscriber management systems for RADIUS-based upstream/downstream
    /// speed limiting, HTTP URL redirection (captive portal), message-of-the-moment
    /// display, and maximum simultaneous session enforcement per user.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(RoaringPenguinAttributes.UpstreamSpeedLimit(10000));
    /// packet.SetAttribute(RoaringPenguinAttributes.DownstreamSpeedLimit(50000));
    /// packet.SetAttribute(RoaringPenguinAttributes.MaxSessionsPerUser(1));
    /// packet.SetAttribute(RoaringPenguinAttributes.Hurl("https://portal.isp.com/welcome"));
    /// </code>
    /// </remarks>
    public static class RoaringPenguinAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Roaring Penguin Software.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 10055;

        #region Integer Attributes

        /// <summary>
        /// Creates an RP-Upstream-Speed-Limit attribute (Type 1) with the specified rate.
        /// </summary>
        /// <param name="value">The upstream speed limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes UpstreamSpeedLimit(int value)
        {
            return CreateInteger(RoaringPenguinAttributeType.UPSTREAM_SPEED_LIMIT, value);
        }

        /// <summary>
        /// Creates an RP-Downstream-Speed-Limit attribute (Type 2) with the specified rate.
        /// </summary>
        /// <param name="value">The downstream speed limit in Kbps.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes DownstreamSpeedLimit(int value)
        {
            return CreateInteger(RoaringPenguinAttributeType.DOWNSTREAM_SPEED_LIMIT, value);
        }

        /// <summary>
        /// Creates an RP-Max-Sessions-Per-User attribute (Type 5) with the specified limit.
        /// </summary>
        /// <param name="value">The maximum number of simultaneous sessions per user.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes MaxSessionsPerUser(int value)
        {
            return CreateInteger(RoaringPenguinAttributeType.MAX_SESSIONS_PER_USER, value);
        }

        #endregion

        #region String Attributes

        /// <summary>
        /// Creates an RP-HURL attribute (Type 3) with the specified URL.
        /// </summary>
        /// <param name="value">The HTTP URL redirect (captive portal). Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Hurl(string value)
        {
            return CreateString(RoaringPenguinAttributeType.HURL, value);
        }

        /// <summary>
        /// Creates an RP-MOTM attribute (Type 4) with the specified message.
        /// </summary>
        /// <param name="value">The Message of the Moment text. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Motm(string value)
        {
            return CreateString(RoaringPenguinAttributeType.MOTM, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified Roaring Penguin attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(RoaringPenguinAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified Roaring Penguin attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(RoaringPenguinAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}