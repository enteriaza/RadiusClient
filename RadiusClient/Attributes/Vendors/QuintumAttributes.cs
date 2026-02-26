using System.Buffers.Binary;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Quintum Technologies (IANA PEN 6618) vendor-specific RADIUS
    /// attribute type, as defined in the FreeRADIUS <c>dictionary.quintum</c>.
    /// </summary>
    /// <remarks>
    /// Quintum Technologies (acquired by NETGEAR in 2010) produced VoIP media
    /// gateways (Tenor series) for enterprise and service provider deployments,
    /// providing PSTN-to-IP trunking and SIP/H.323 interworking.
    /// </remarks>
    public enum QuintumAttributeType : byte
    {
        /// <summary>Quintum-AVPair (Type 1). String. Attribute-value pair string.</summary>
        AVPAIR = 1,

        /// <summary>Quintum-NAS-Port (Type 2). Integer. NAS port number.</summary>
        NAS_PORT = 2,

        /// <summary>Quintum-h323-remote-address (Type 23). String. H.323 remote IP address.</summary>
        H323_REMOTE_ADDRESS = 23,

        /// <summary>Quintum-h323-conf-id (Type 24). String. H.323 conference identifier.</summary>
        H323_CONF_ID = 24,

        /// <summary>Quintum-h323-setup-time (Type 25). String. H.323 call setup time.</summary>
        H323_SETUP_TIME = 25,

        /// <summary>Quintum-h323-call-origin (Type 26). String. H.323 call origin.</summary>
        H323_CALL_ORIGIN = 26,

        /// <summary>Quintum-h323-call-type (Type 27). String. H.323 call type.</summary>
        H323_CALL_TYPE = 27,

        /// <summary>Quintum-h323-connect-time (Type 28). String. H.323 call connect time.</summary>
        H323_CONNECT_TIME = 28,

        /// <summary>Quintum-h323-disconnect-time (Type 29). String. H.323 call disconnect time.</summary>
        H323_DISCONNECT_TIME = 29,

        /// <summary>Quintum-h323-disconnect-cause (Type 30). String. H.323 disconnect cause code.</summary>
        H323_DISCONNECT_CAUSE = 30,

        /// <summary>Quintum-h323-voice-quality (Type 31). String. H.323 voice quality metric.</summary>
        H323_VOICE_QUALITY = 31,

        /// <summary>Quintum-h323-gw-id (Type 33). String. H.323 gateway identifier.</summary>
        H323_GW_ID = 33,

        /// <summary>Quintum-h323-incoming-conf-id (Type 35). String. H.323 incoming conference identifier.</summary>
        H323_INCOMING_CONF_ID = 35,

        /// <summary>Quintum-h323-credit-amount (Type 101). String. Credit amount for the call.</summary>
        H323_CREDIT_AMOUNT = 101,

        /// <summary>Quintum-h323-credit-time (Type 102). String. Credit time for the call.</summary>
        H323_CREDIT_TIME = 102,

        /// <summary>Quintum-h323-return-code (Type 103). String. Return code.</summary>
        H323_RETURN_CODE = 103,

        /// <summary>Quintum-h323-prompt-id (Type 104). String. Prompt identifier.</summary>
        H323_PROMPT_ID = 104,

        /// <summary>Quintum-h323-time-and-day (Type 105). String. Time and day of the call.</summary>
        H323_TIME_AND_DAY = 105,

        /// <summary>Quintum-h323-redirect-number (Type 106). String. Redirect number.</summary>
        H323_REDIRECT_NUMBER = 106,

        /// <summary>Quintum-h323-preferred-lang (Type 107). String. Preferred language.</summary>
        H323_PREFERRED_LANG = 107,

        /// <summary>Quintum-h323-redirect-ip-address (Type 108). String. Redirect IP address.</summary>
        H323_REDIRECT_IP_ADDRESS = 108,

        /// <summary>Quintum-h323-billing-model (Type 109). String. Billing model.</summary>
        H323_BILLING_MODEL = 109,

        /// <summary>Quintum-h323-currency (Type 110). String. Currency code.</summary>
        H323_CURRENCY = 110,

        /// <summary>Quintum-Trunkid-In (Type 229). String. Incoming trunk identifier.</summary>
        TRUNKID_IN = 229,

        /// <summary>Quintum-Trunkid-Out (Type 230). String. Outgoing trunk identifier.</summary>
        TRUNKID_OUT = 230
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Quintum Technologies
    /// (IANA PEN 6618) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.quintum</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Quintum's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 6618</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Quintum Technologies (now NETGEAR) Tenor VoIP
    /// media gateways for RADIUS-based H.323/SIP call detail record (CDR) accounting
    /// including remote address, conference IDs, call setup/connect/disconnect
    /// timestamps, disconnect cause, call origin/type, voice quality metrics,
    /// gateway identification, credit amount/time, billing model, currency,
    /// redirect information, trunk identification (in/out), and general-purpose
    /// attribute-value pair configuration.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCOUNTING_REQUEST);
    /// packet.SetAttribute(QuintumAttributes.H323RemoteAddress("10.1.1.100"));
    /// packet.SetAttribute(QuintumAttributes.H323ConfId("AB12CD34-5678-EF90"));
    /// packet.SetAttribute(QuintumAttributes.H323CallOrigin("originate"));
    /// packet.SetAttribute(QuintumAttributes.H323DisconnectCause("10"));
    /// packet.SetAttribute(QuintumAttributes.TrunkidIn("PSTN-T1-01"));
    /// packet.SetAttribute(QuintumAttributes.TrunkidOut("SIP-TRUNK-01"));
    /// </code>
    /// </remarks>
    public static class QuintumAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Quintum Technologies.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 6618;

        #region Integer Attributes

        /// <summary>
        /// Creates a Quintum-NAS-Port attribute (Type 2) with the specified port number.
        /// </summary>
        /// <param name="value">The NAS port number.</param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        public static VendorSpecificAttributes NasPort(int value)
        {
            return CreateInteger(QuintumAttributeType.NAS_PORT, value);
        }

        #endregion

        #region String Attributes

        /// <summary>Creates a Quintum-AVPair attribute (Type 1).</summary>
        /// <param name="value">The attribute-value pair string. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes AvPair(string value) => CreateString(QuintumAttributeType.AVPAIR, value);

        /// <summary>Creates a Quintum-h323-remote-address attribute (Type 23).</summary>
        /// <param name="value">The H.323 remote IP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323RemoteAddress(string value) => CreateString(QuintumAttributeType.H323_REMOTE_ADDRESS, value);

        /// <summary>Creates a Quintum-h323-conf-id attribute (Type 24).</summary>
        /// <param name="value">The H.323 conference identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323ConfId(string value) => CreateString(QuintumAttributeType.H323_CONF_ID, value);

        /// <summary>Creates a Quintum-h323-setup-time attribute (Type 25).</summary>
        /// <param name="value">The H.323 call setup time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323SetupTime(string value) => CreateString(QuintumAttributeType.H323_SETUP_TIME, value);

        /// <summary>Creates a Quintum-h323-call-origin attribute (Type 26).</summary>
        /// <param name="value">The H.323 call origin (e.g. "originate", "answer"). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323CallOrigin(string value) => CreateString(QuintumAttributeType.H323_CALL_ORIGIN, value);

        /// <summary>Creates a Quintum-h323-call-type attribute (Type 27).</summary>
        /// <param name="value">The H.323 call type. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323CallType(string value) => CreateString(QuintumAttributeType.H323_CALL_TYPE, value);

        /// <summary>Creates a Quintum-h323-connect-time attribute (Type 28).</summary>
        /// <param name="value">The H.323 call connect time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323ConnectTime(string value) => CreateString(QuintumAttributeType.H323_CONNECT_TIME, value);

        /// <summary>Creates a Quintum-h323-disconnect-time attribute (Type 29).</summary>
        /// <param name="value">The H.323 call disconnect time. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323DisconnectTime(string value) => CreateString(QuintumAttributeType.H323_DISCONNECT_TIME, value);

        /// <summary>Creates a Quintum-h323-disconnect-cause attribute (Type 30).</summary>
        /// <param name="value">The H.323 disconnect cause code. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323DisconnectCause(string value) => CreateString(QuintumAttributeType.H323_DISCONNECT_CAUSE, value);

        /// <summary>Creates a Quintum-h323-voice-quality attribute (Type 31).</summary>
        /// <param name="value">The H.323 voice quality metric. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323VoiceQuality(string value) => CreateString(QuintumAttributeType.H323_VOICE_QUALITY, value);

        /// <summary>Creates a Quintum-h323-gw-id attribute (Type 33).</summary>
        /// <param name="value">The H.323 gateway identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323GwId(string value) => CreateString(QuintumAttributeType.H323_GW_ID, value);

        /// <summary>Creates a Quintum-h323-incoming-conf-id attribute (Type 35).</summary>
        /// <param name="value">The H.323 incoming conference identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323IncomingConfId(string value) => CreateString(QuintumAttributeType.H323_INCOMING_CONF_ID, value);

        /// <summary>Creates a Quintum-h323-credit-amount attribute (Type 101).</summary>
        /// <param name="value">The credit amount for the call. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323CreditAmount(string value) => CreateString(QuintumAttributeType.H323_CREDIT_AMOUNT, value);

        /// <summary>Creates a Quintum-h323-credit-time attribute (Type 102).</summary>
        /// <param name="value">The credit time for the call. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323CreditTime(string value) => CreateString(QuintumAttributeType.H323_CREDIT_TIME, value);

        /// <summary>Creates a Quintum-h323-return-code attribute (Type 103).</summary>
        /// <param name="value">The return code. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323ReturnCode(string value) => CreateString(QuintumAttributeType.H323_RETURN_CODE, value);

        /// <summary>Creates a Quintum-h323-prompt-id attribute (Type 104).</summary>
        /// <param name="value">The prompt identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323PromptId(string value) => CreateString(QuintumAttributeType.H323_PROMPT_ID, value);

        /// <summary>Creates a Quintum-h323-time-and-day attribute (Type 105).</summary>
        /// <param name="value">The time and day of the call. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323TimeAndDay(string value) => CreateString(QuintumAttributeType.H323_TIME_AND_DAY, value);

        /// <summary>Creates a Quintum-h323-redirect-number attribute (Type 106).</summary>
        /// <param name="value">The redirect number. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323RedirectNumber(string value) => CreateString(QuintumAttributeType.H323_REDIRECT_NUMBER, value);

        /// <summary>Creates a Quintum-h323-preferred-lang attribute (Type 107).</summary>
        /// <param name="value">The preferred language. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323PreferredLang(string value) => CreateString(QuintumAttributeType.H323_PREFERRED_LANG, value);

        /// <summary>Creates a Quintum-h323-redirect-ip-address attribute (Type 108).</summary>
        /// <param name="value">The redirect IP address. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323RedirectIpAddress(string value) => CreateString(QuintumAttributeType.H323_REDIRECT_IP_ADDRESS, value);

        /// <summary>Creates a Quintum-h323-billing-model attribute (Type 109).</summary>
        /// <param name="value">The billing model. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323BillingModel(string value) => CreateString(QuintumAttributeType.H323_BILLING_MODEL, value);

        /// <summary>Creates a Quintum-h323-currency attribute (Type 110).</summary>
        /// <param name="value">The currency code (e.g. "USD", "EUR"). Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes H323Currency(string value) => CreateString(QuintumAttributeType.H323_CURRENCY, value);

        /// <summary>Creates a Quintum-Trunkid-In attribute (Type 229).</summary>
        /// <param name="value">The incoming trunk identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TrunkidIn(string value) => CreateString(QuintumAttributeType.TRUNKID_IN, value);

        /// <summary>Creates a Quintum-Trunkid-Out attribute (Type 230).</summary>
        /// <param name="value">The outgoing trunk identifier. Must not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static VendorSpecificAttributes TrunkidOut(string value) => CreateString(QuintumAttributeType.TRUNKID_OUT, value);

        #endregion

        #region Private Helpers

        private static VendorSpecificAttributes CreateInteger(QuintumAttributeType type, int value)
        {
            byte[] data = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        private static VendorSpecificAttributes CreateString(QuintumAttributeType type, string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            byte[] data = Encoding.UTF8.GetBytes(value);
            return new VendorSpecificAttributes(VENDOR_ID, (byte)type, data);
        }

        #endregion
    }
}