using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a FreeSWITCH (IANA PEN 27880) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.freeswitch</c>.
    /// </summary>
    public enum FreeSwitchAttributeType : byte
    {
        /// <summary>Freeswitch-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Freeswitch-CLID (Type 2). String. Caller ID string.</summary>
        CLID = 2,

        /// <summary>Freeswitch-Dialplan (Type 3). String. Dialplan context name.</summary>
        DIALPLAN = 3,

        /// <summary>Freeswitch-Src (Type 4). String. Call source identifier.</summary>
        SRC = 4,

        /// <summary>Freeswitch-Dst (Type 5). String. Call destination identifier.</summary>
        DST = 5,

        /// <summary>Freeswitch-Src-Channel (Type 6). String. Source channel name.</summary>
        SRC_CHANNEL = 6,

        /// <summary>Freeswitch-Dst-Channel (Type 7). String. Destination channel name.</summary>
        DST_CHANNEL = 7,

        /// <summary>Freeswitch-Ani (Type 8). String. Automatic Number Identification.</summary>
        ANI = 8,

        /// <summary>Freeswitch-Aniii (Type 9). String. ANI II digits.</summary>
        ANIII = 9,

        /// <summary>Freeswitch-Lastapp (Type 10). String. Last application executed.</summary>
        LASTAPP = 10,

        /// <summary>Freeswitch-Lastdata (Type 11). String. Last application data.</summary>
        LASTDATA = 11,

        /// <summary>Freeswitch-Disposition (Type 12). String. Call disposition.</summary>
        DISPOSITION = 12,

        /// <summary>Freeswitch-Hangupcause (Type 13). Integer. Hangup cause code.</summary>
        HANGUPCAUSE = 13,

        /// <summary>Freeswitch-Billusec (Type 15). Integer. Billable duration in microseconds.</summary>
        BILLUSEC = 15,

        /// <summary>Freeswitch-AMAFlags (Type 16). Integer. AMA flags for billing.</summary>
        AMA_FLAGS = 16,

        /// <summary>Freeswitch-RDNIS (Type 17). String. Redirecting Number Information Service.</summary>
        RDNIS = 17,

        /// <summary>Freeswitch-Context (Type 18). String. FreeSWITCH context.</summary>
        CONTEXT = 18,

        /// <summary>Freeswitch-Source (Type 19). String. Source module name.</summary>
        SOURCE = 19,

        /// <summary>Freeswitch-Callstartdate (Type 20). String. Call start date/time.</summary>
        CALLSTARTDATE = 20,

        /// <summary>Freeswitch-Callanswerdate (Type 21). String. Call answer date/time.</summary>
        CALLANSWERDATE = 21,

        /// <summary>Freeswitch-Calltransferdate (Type 22). String. Call transfer date/time.</summary>
        CALLTRANSFERDATE = 22,

        /// <summary>Freeswitch-Callenddate (Type 23). String. Call end date/time.</summary>
        CALLENDDATE = 23,

        /// <summary>Freeswitch-Signalbond (Type 24). String. Signal bond UUID.</summary>
        SIGNALBOND = 24
    }

    /// <summary>
    /// Freeswitch-Hangupcause attribute values (Type 13).
    /// </summary>
    public enum FREESWITCH_HANGUPCAUSE
    {
        /// <summary>Unallocated number.</summary>
        UNALLOCATED_NUMBER = 1,

        /// <summary>No route to transit network.</summary>
        NO_ROUTE_TRANSIT_NET = 2,

        /// <summary>No route to destination.</summary>
        NO_ROUTE_DESTINATION = 3,

        /// <summary>Channel unacceptable.</summary>
        CHANNEL_UNACCEPTABLE = 6,

        /// <summary>Call awarded and being delivered.</summary>
        CALL_AWARDED_DELIVERED = 7,

        /// <summary>Normal clearing.</summary>
        NORMAL_CLEARING = 16,

        /// <summary>User busy.</summary>
        USER_BUSY = 17,

        /// <summary>No user response.</summary>
        NO_USER_RESPONSE = 18,

        /// <summary>No answer from user.</summary>
        NO_ANSWER = 19,

        /// <summary>Subscriber absent.</summary>
        SUBSCRIBER_ABSENT = 20,

        /// <summary>Call rejected.</summary>
        CALL_REJECTED = 21,

        /// <summary>Number changed.</summary>
        NUMBER_CHANGED = 22,

        /// <summary>Destination out of order.</summary>
        DESTINATION_OUT_OF_ORDER = 27,

        /// <summary>Invalid number format.</summary>
        INVALID_NUMBER_FORMAT = 28,

        /// <summary>Normal unspecified.</summary>
        NORMAL_UNSPECIFIED = 31,

        /// <summary>Normal circuit congestion.</summary>
        NORMAL_CIRCUIT_CONGESTION = 34,

        /// <summary>Network out of order.</summary>
        NETWORK_OUT_OF_ORDER = 38,

        /// <summary>Normal temporary failure.</summary>
        NORMAL_TEMPORARY_FAILURE = 41,

        /// <summary>Switch congestion.</summary>
        SWITCH_CONGESTION = 42,

        /// <summary>Requested channel not available.</summary>
        REQUESTED_CHAN_UNAVAIL = 44,

        /// <summary>Facility not subscribed.</summary>
        FACILITY_NOT_SUBSCRIBED = 50,

        /// <summary>Outgoing call barred.</summary>
        OUTGOING_CALL_BARRED = 52,

        /// <summary>Incoming call barred.</summary>
        INCOMING_CALL_BARRED = 54,

        /// <summary>Bearer capability not authorized.</summary>
        BEARERCAPABILITY_NOTAUTH = 57,

        /// <summary>Bearer capability not available.</summary>
        BEARERCAPABILITY_NOTAVAIL = 58,

        /// <summary>Service not available.</summary>
        SERVICE_UNAVAILABLE = 63,

        /// <summary>Bearer capability not implemented.</summary>
        BEARERCAPABILITY_NOTIMPL = 65,

        /// <summary>Facility not implemented.</summary>
        FACILITY_NOT_IMPLEMENTED = 69,

        /// <summary>Service not implemented.</summary>
        SERVICE_NOT_IMPLEMENTED = 79,

        /// <summary>Incompatible destination.</summary>
        INCOMPATIBLE_DESTINATION = 88,

        /// <summary>Recovery on timer expire.</summary>
        RECOVERY_ON_TIMER_EXPIRE = 102,

        /// <summary>Interworking.</summary>
        INTERWORKING = 127
    }

    /// <summary>
    /// Freeswitch-AMAFlags attribute values (Type 16).
    /// </summary>
    public enum FREESWITCH_AMA_FLAGS
    {
        /// <summary>Default AMA flag.</summary>
        DEFAULT = 0,

        /// <summary>Omit from billing.</summary>
        OMIT = 1,

        /// <summary>Billing record.</summary>
        BILLING = 2,

        /// <summary>Documentation record.</summary>
        DOCUMENTATION = 3
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing FreeSWITCH
    /// (IANA PEN 27880) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.freeswitch</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// FreeSWITCH's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 27880</c>.
    /// </para>
    /// <para>
    /// These attributes are used by the FreeSWITCH open-source telephony platform
    /// for RADIUS-based call detail record (CDR) accounting, including caller ID,
    /// dialplan context, source/destination channels, ANI, last executed application,
    /// call disposition and hangup cause, billable duration, AMA billing flags,
    /// RDNIS, call timestamps (start/answer/transfer/end), and signal bond tracking.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(FreeSwitchAttributes.Src("1001"));
    /// packet.SetAttribute(FreeSwitchAttributes.Dst("1002"));
    /// packet.SetAttribute(FreeSwitchAttributes.Hangupcause(FREESWITCH_HANGUPCAUSE.NORMAL_CLEARING));
    /// packet.SetAttribute(FreeSwitchAttributes.Billusec(120000000));
    /// packet.SetAttribute(FreeSwitchAttributes.Context("default"));
    /// </code>
    /// </remarks>
    public static class FreeSwitchAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for FreeSWITCH.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 27880;

        #region Integer Attributes

        /// <summary>
        /// Creates a Freeswitch-Hangupcause attribute (Type 13) with the specified cause code.
        /// </summary>
        /// <param name="value">The hangup cause code. See <see cref="FREESWITCH_HANGUPCAUSE"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Hangupcause(FREESWITCH_HANGUPCAUSE value)
        {
            return CreateInteger(FreeSwitchAttributeType.HANGUPCAUSE, (int)value);
        }

        /// <summary>
        /// Creates a Freeswitch-Billusec attribute (Type 15) with the specified duration.
        /// </summary>
        /// <param name="value">The billable duration in microseconds.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes Billusec(int value)
        {
            return CreateInteger(FreeSwitchAttributeType.BILLUSEC, value);
        }

        /// <summary>
        /// Creates a Freeswitch-AMAFlags attribute (Type 16) with the specified flags.
        /// </summary>
        /// <param name="value">The AMA flags for billing. See <see cref="FREESWITCH_AMA_FLAGS"/>.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes AmaFlags(FREESWITCH_AMA_FLAGS value)
        {
            return CreateInteger(FreeSwitchAttributeType.AMA_FLAGS, (int)value);
        }

        #endregion

        #region String Attributes

        /// <summary>Creates a Freeswitch-AVPair attribute (Type 1).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(FreeSwitchAttributeType.AVPAIR, value);

        /// <summary>Creates a Freeswitch-CLID attribute (Type 2).</summary>
        /// <param name="value">The caller ID string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Clid(string value) => CreateString(FreeSwitchAttributeType.CLID, value);

        /// <summary>Creates a Freeswitch-Dialplan attribute (Type 3).</summary>
        /// <param name="value">The dialplan context name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Dialplan(string value) => CreateString(FreeSwitchAttributeType.DIALPLAN, value);

        /// <summary>Creates a Freeswitch-Src attribute (Type 4).</summary>
        /// <param name="value">The call source identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Src(string value) => CreateString(FreeSwitchAttributeType.SRC, value);

        /// <summary>Creates a Freeswitch-Dst attribute (Type 5).</summary>
        /// <param name="value">The call destination identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Dst(string value) => CreateString(FreeSwitchAttributeType.DST, value);

        /// <summary>Creates a Freeswitch-Src-Channel attribute (Type 6).</summary>
        /// <param name="value">The source channel name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes SrcChannel(string value) => CreateString(FreeSwitchAttributeType.SRC_CHANNEL, value);

        /// <summary>Creates a Freeswitch-Dst-Channel attribute (Type 7).</summary>
        /// <param name="value">The destination channel name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes DstChannel(string value) => CreateString(FreeSwitchAttributeType.DST_CHANNEL, value);

        /// <summary>Creates a Freeswitch-Ani attribute (Type 8).</summary>
        /// <param name="value">The Automatic Number Identification. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Ani(string value) => CreateString(FreeSwitchAttributeType.ANI, value);

        /// <summary>Creates a Freeswitch-Aniii attribute (Type 9).</summary>
        /// <param name="value">The ANI II digits. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Aniii(string value) => CreateString(FreeSwitchAttributeType.ANIII, value);

        /// <summary>Creates a Freeswitch-Lastapp attribute (Type 10).</summary>
        /// <param name="value">The last application executed. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Lastapp(string value) => CreateString(FreeSwitchAttributeType.LASTAPP, value);

        /// <summary>Creates a Freeswitch-Lastdata attribute (Type 11).</summary>
        /// <param name="value">The last application data. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Lastdata(string value) => CreateString(FreeSwitchAttributeType.LASTDATA, value);

        /// <summary>Creates a Freeswitch-Disposition attribute (Type 12).</summary>
        /// <param name="value">The call disposition. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Disposition(string value) => CreateString(FreeSwitchAttributeType.DISPOSITION, value);

        /// <summary>Creates a Freeswitch-RDNIS attribute (Type 17).</summary>
        /// <param name="value">The Redirecting Number Information Service. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Rdnis(string value) => CreateString(FreeSwitchAttributeType.RDNIS, value);

        /// <summary>Creates a Freeswitch-Context attribute (Type 18).</summary>
        /// <param name="value">The FreeSWITCH context. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Context(string value) => CreateString(FreeSwitchAttributeType.CONTEXT, value);

        /// <summary>Creates a Freeswitch-Source attribute (Type 19).</summary>
        /// <param name="value">The source module name. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Source(string value) => CreateString(FreeSwitchAttributeType.SOURCE, value);

        /// <summary>Creates a Freeswitch-Callstartdate attribute (Type 20).</summary>
        /// <param name="value">The call start date/time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Callstartdate(string value) => CreateString(FreeSwitchAttributeType.CALLSTARTDATE, value);

        /// <summary>Creates a Freeswitch-Callanswerdate attribute (Type 21).</summary>
        /// <param name="value">The call answer date/time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Callanswerdate(string value) => CreateString(FreeSwitchAttributeType.CALLANSWERDATE, value);

        /// <summary>Creates a Freeswitch-Calltransferdate attribute (Type 22).</summary>
        /// <param name="value">The call transfer date/time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Calltransferdate(string value) => CreateString(FreeSwitchAttributeType.CALLTRANSFERDATE, value);

        /// <summary>Creates a Freeswitch-Callenddate attribute (Type 23).</summary>
        /// <param name="value">The call end date/time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Callenddate(string value) => CreateString(FreeSwitchAttributeType.CALLENDDATE, value);

        /// <summary>Creates a Freeswitch-Signalbond attribute (Type 24).</summary>
        /// <param name="value">The signal bond UUID. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes Signalbond(string value) => CreateString(FreeSwitchAttributeType.SIGNALBOND, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(FreeSwitchAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(FreeSwitchAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}