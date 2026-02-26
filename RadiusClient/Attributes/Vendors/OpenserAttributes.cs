using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies an OpenSER / Kamailio (IANA PEN 24483) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.openser</c>.
    /// </summary>
    /// <remarks>
    /// OpenSER (now Kamailio) is an open-source SIP server used as a SIP proxy,
    /// registrar, location server, and application server. These RADIUS attributes
    /// are used by the OpenSER/Kamailio <c>acc</c> (accounting) and <c>auth_radius</c>
    /// modules for SIP call accounting and authentication.
    /// </remarks>
    public enum OpenserAttributeType : byte
    {
        /// <summary>OpenSER-Src-Leg (Type 1). String. SIP source (caller) leg information.</summary>
        SRC_LEG = 1,

        /// <summary>OpenSER-Dst-Leg (Type 2). String. SIP destination (callee) leg information.</summary>
        DST_LEG = 2,

        /// <summary>OpenSER-Group (Type 3). String. User group name.</summary>
        GROUP = 3,

        /// <summary>OpenSER-Caller-ID (Type 4). String. SIP caller identifier (From header).</summary>
        CALLER_ID = 4,

        /// <summary>OpenSER-Callee-ID (Type 5). String. SIP callee identifier (To header).</summary>
        CALLEE_ID = 5,

        /// <summary>OpenSER-Trans-Id (Type 6). String. SIP transaction identifier.</summary>
        TRANS_ID = 6,

        /// <summary>OpenSER-Trans-RID (Type 7). String. SIP transaction reply identifier.</summary>
        TRANS_RID = 7,

        /// <summary>OpenSER-SIP-Method (Type 8). String. SIP method (INVITE, BYE, etc.).</summary>
        SIP_METHOD = 8,

        /// <summary>OpenSER-SIP-Response-Code (Type 9). Integer. SIP response code.</summary>
        SIP_RESPONSE_CODE = 9,

        /// <summary>OpenSER-SIP-Reason-Phrase (Type 10). String. SIP reason phrase.</summary>
        SIP_REASON_PHRASE = 10,

        /// <summary>OpenSER-SIP-Request-Timestamp (Type 11). Integer. SIP request UNIX timestamp.</summary>
        SIP_REQUEST_TIMESTAMP = 11,

        /// <summary>OpenSER-SIP-Response-Timestamp (Type 12). Integer. SIP response UNIX timestamp.</summary>
        SIP_RESPONSE_TIMESTAMP = 12,

        /// <summary>OpenSER-SIP-URI-User (Type 13). String. SIP URI user part.</summary>
        SIP_URI_USER = 13,

        /// <summary>OpenSER-SIP-URI-Host (Type 14). String. SIP URI host part.</summary>
        SIP_URI_HOST = 14,

        /// <summary>OpenSER-SIP-From-Tag (Type 15). String. SIP From header tag parameter.</summary>
        SIP_FROM_TAG = 15,

        /// <summary>OpenSER-SIP-To-Tag (Type 16). String. SIP To header tag parameter.</summary>
        SIP_TO_TAG = 16,

        /// <summary>OpenSER-SIP-CSeq (Type 17). String. SIP CSeq header value.</summary>
        SIP_CSEQ = 17,

        /// <summary>OpenSER-SIP-Call-ID (Type 18). String. SIP Call-ID header value.</summary>
        SIP_CALL_ID = 18,

        /// <summary>OpenSER-SIP-Call-Duration (Type 19). Integer. SIP call duration in seconds.</summary>
        SIP_CALL_DURATION = 19,

        /// <summary>OpenSER-SIP-RPId (Type 20). String. SIP Remote-Party-ID header value.</summary>
        SIP_RPID = 20
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing OpenSER / Kamailio
    /// (IANA PEN 24483) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.openser</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// OpenSER's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 24483</c>.
    /// </para>
    /// <para>
    /// These attributes are used by the OpenSER/Kamailio SIP server's <c>acc</c>
    /// (accounting) and <c>auth_radius</c> modules for RADIUS-based SIP call
    /// accounting and authentication, including source/destination leg information,
    /// caller/callee identification, SIP method and response code/reason tracking,
    /// request/response timestamps, URI user and host, From/To tags, CSeq, Call-ID,
    /// call duration, Remote-Party-ID, transaction identifiers, and user group
    /// assignment.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(OpenserAttributes.SipMethod("INVITE"));
    /// packet.SetAttribute(OpenserAttributes.SipCallId("abc123@sip.example.com"));
    /// packet.SetAttribute(OpenserAttributes.CallerId("sip:alice@example.com"));
    /// packet.SetAttribute(OpenserAttributes.CalleeId("sip:bob@example.com"));
    /// packet.SetAttribute(OpenserAttributes.SipResponseCode(200));
    /// packet.SetAttribute(OpenserAttributes.SipCallDuration(360));
    /// </code>
    /// </remarks>
    public static class OpenserAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for OpenSER / Kamailio.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 24483;

        #region Integer Attributes

        /// <summary>Creates an OpenSER-SIP-Response-Code attribute (Type 9).</summary>
        /// <param name="value">The SIP response code (e.g. 200, 404, 503).</param>
        public static VendorSpecificAttributes SipResponseCode(int value)
        {
            return CreateInteger(OpenserAttributeType.SIP_RESPONSE_CODE, value);
        }

        /// <summary>Creates an OpenSER-SIP-Request-Timestamp attribute (Type 11).</summary>
        /// <param name="value">The SIP request UNIX timestamp.</param>
        public static VendorSpecificAttributes SipRequestTimestamp(int value)
        {
            return CreateInteger(OpenserAttributeType.SIP_REQUEST_TIMESTAMP, value);
        }

        /// <summary>Creates an OpenSER-SIP-Response-Timestamp attribute (Type 12).</summary>
        /// <param name="value">The SIP response UNIX timestamp.</param>
        public static VendorSpecificAttributes SipResponseTimestamp(int value)
        {
            return CreateInteger(OpenserAttributeType.SIP_RESPONSE_TIMESTAMP, value);
        }

        /// <summary>Creates an OpenSER-SIP-Call-Duration attribute (Type 19).</summary>
        /// <param name="value">The SIP call duration in seconds.</param>
        public static VendorSpecificAttributes SipCallDuration(int value)
        {
            return CreateInteger(OpenserAttributeType.SIP_CALL_DURATION, value);
        }

        #endregion

        #region String Attributes

        /// <summary>Creates an OpenSER-Src-Leg attribute (Type 1).</summary>
        /// <param name="value">The SIP source leg information. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SrcLeg(string value)
        {
            return CreateString(OpenserAttributeType.SRC_LEG, value);
        }

        /// <summary>Creates an OpenSER-Dst-Leg attribute (Type 2).</summary>
        /// <param name="value">The SIP destination leg information. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DstLeg(string value)
        {
            return CreateString(OpenserAttributeType.DST_LEG, value);
        }

        /// <summary>Creates an OpenSER-Group attribute (Type 3).</summary>
        /// <param name="value">The user group name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Group(string value)
        {
            return CreateString(OpenserAttributeType.GROUP, value);
        }

        /// <summary>Creates an OpenSER-Caller-ID attribute (Type 4).</summary>
        /// <param name="value">The SIP caller identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CallerId(string value)
        {
            return CreateString(OpenserAttributeType.CALLER_ID, value);
        }

        /// <summary>Creates an OpenSER-Callee-ID attribute (Type 5).</summary>
        /// <param name="value">The SIP callee identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes CalleeId(string value)
        {
            return CreateString(OpenserAttributeType.CALLEE_ID, value);
        }

        /// <summary>Creates an OpenSER-Trans-Id attribute (Type 6).</summary>
        /// <param name="value">The SIP transaction identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TransId(string value)
        {
            return CreateString(OpenserAttributeType.TRANS_ID, value);
        }

        /// <summary>Creates an OpenSER-Trans-RID attribute (Type 7).</summary>
        /// <param name="value">The SIP transaction reply identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TransRid(string value)
        {
            return CreateString(OpenserAttributeType.TRANS_RID, value);
        }

        /// <summary>Creates an OpenSER-SIP-Method attribute (Type 8).</summary>
        /// <param name="value">The SIP method (e.g. "INVITE", "BYE"). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipMethod(string value)
        {
            return CreateString(OpenserAttributeType.SIP_METHOD, value);
        }

        /// <summary>Creates an OpenSER-SIP-Reason-Phrase attribute (Type 10).</summary>
        /// <param name="value">The SIP reason phrase. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipReasonPhrase(string value)
        {
            return CreateString(OpenserAttributeType.SIP_REASON_PHRASE, value);
        }

        /// <summary>Creates an OpenSER-SIP-URI-User attribute (Type 13).</summary>
        /// <param name="value">The SIP URI user part. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipUriUser(string value)
        {
            return CreateString(OpenserAttributeType.SIP_URI_USER, value);
        }

        /// <summary>Creates an OpenSER-SIP-URI-Host attribute (Type 14).</summary>
        /// <param name="value">The SIP URI host part. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipUriHost(string value)
        {
            return CreateString(OpenserAttributeType.SIP_URI_HOST, value);
        }

        /// <summary>Creates an OpenSER-SIP-From-Tag attribute (Type 15).</summary>
        /// <param name="value">The SIP From header tag parameter. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipFromTag(string value)
        {
            return CreateString(OpenserAttributeType.SIP_FROM_TAG, value);
        }

        /// <summary>Creates an OpenSER-SIP-To-Tag attribute (Type 16).</summary>
        /// <param name="value">The SIP To header tag parameter. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipToTag(string value)
        {
            return CreateString(OpenserAttributeType.SIP_TO_TAG, value);
        }

        /// <summary>Creates an OpenSER-SIP-CSeq attribute (Type 17).</summary>
        /// <param name="value">The SIP CSeq header value. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipCSeq(string value)
        {
            return CreateString(OpenserAttributeType.SIP_CSEQ, value);
        }

        /// <summary>Creates an OpenSER-SIP-Call-ID attribute (Type 18).</summary>
        /// <param name="value">The SIP Call-ID header value. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipCallId(string value)
        {
            return CreateString(OpenserAttributeType.SIP_CALL_ID, value);
        }

        /// <summary>Creates an OpenSER-SIP-RPId attribute (Type 20).</summary>
        /// <param name="value">The SIP Remote-Party-ID header value. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SipRpId(string value)
        {
            return CreateString(OpenserAttributeType.SIP_RPID, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with a 32-bit big-endian integer value for the specified OpenSER attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateInteger(OpenserAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        /// <summary>
        /// Creates a VSA with a UTF-8 encoded string value for the specified OpenSER attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateString(OpenserAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}