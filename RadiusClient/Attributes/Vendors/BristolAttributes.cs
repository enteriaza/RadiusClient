using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Radius.Attributes.Vendors
{
    #region Enums

    /// <summary>
    /// Identifies a Bristol Networks (IANA PEN 4329) vendor-specific RADIUS attribute type,
    /// as defined in the FreeRADIUS <c>dictionary.bristol</c>.
    /// </summary>
    public enum BristolAttributeType : byte
    {
        /// <summary>Bristol-Primary-DNS (Type 1). IP address. Primary DNS server address.</summary>
        PRIMARY_DNS = 1,

        /// <summary>Bristol-Secondary-DNS (Type 2). IP address. Secondary DNS server address.</summary>
        SECONDARY_DNS = 2
    }

    #endregion

    /// <summary>
    /// Provides strongly-typed factory methods for constructing Bristol Networks
    /// (IANA PEN 4329) Vendor-Specific Attributes (VSAs), as defined in the FreeRADIUS
    /// <c>dictionary.bristol</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Bristol's vendor-specific attributes follow the standard VSA layout defined in
    /// RFC 2865 §5.26. All attributes produced by this class are wrapped in a
    /// <see cref="VendorSpecificAttributes"/> instance with <c>VendorId = 4329</c>.
    /// </para>
    /// <para>
    /// These attributes are used by Bristol Networks (now part of Brocade / Broadcom)
    /// VPN and IPsec gateways for RADIUS-based primary and secondary DNS server
    /// assignment to connecting clients.
    /// </para>
    /// <para>
    /// <b>Usage example:</b>
    /// </para>
    /// <code>
    /// var packet = new RadiusPacket(RadiusCode.ACCESS_ACCEPT);
    /// packet.SetAttribute(BristolAttributes.PrimaryDns(IPAddress.Parse("8.8.8.8")));
    /// packet.SetAttribute(BristolAttributes.SecondaryDns(IPAddress.Parse("8.8.4.4")));
    /// </code>
    /// </remarks>
    public static class BristolAttributes
    {
        /// <summary>
        /// The IANA Private Enterprise Number for Bristol Networks.
        /// See http://www.iana.org/assignments/enterprise-numbers.
        /// </summary>
        public const uint VENDOR_ID = 4329;

        #region IP Address Attributes

        /// <summary>
        /// Creates a Bristol-Primary-DNS attribute (Type 1) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The primary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes PrimaryDns(IPAddress value)
        {
            return CreateIpv4(BristolAttributeType.PRIMARY_DNS, value);
        }

        /// <summary>
        /// Creates a Bristol-Secondary-DNS attribute (Type 2) with the specified IPv4 address.
        /// </summary>
        /// <param name="value">
        /// The secondary DNS server address. Must be an IPv4 (<see cref="AddressFamily.InterNetwork"/>) address.
        /// Must not be <see langword="null"/>.
        /// </param>
        /// <returns>A <see cref="VendorSpecificAttributes"/> instance ready to add to a <see cref="RadiusPacket"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not an IPv4 address.</exception>
        public static VendorSpecificAttributes SecondaryDns(IPAddress value)
        {
            return CreateIpv4(BristolAttributeType.SECONDARY_DNS, value);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Creates a VSA with an IPv4 address value for the specified Bristol attribute type.
        /// </summary>
        private static VendorSpecificAttributes CreateIpv4(BristolAttributeType type, IPAddress value)
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