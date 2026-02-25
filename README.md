# RadiusClient

A high-performance, thread-safe .NET 8 client library for communicating with RADIUS servers, implementing RFC 2865 (Authentication) and RFC 2866 (Accounting).

## Features

- **Full RADIUS protocol support** — Access-Request, Access-Accept/Reject/Challenge, Accounting-Request/Response, CoA, Disconnect, and Status-Server (RFC 5997)
- **High throughput** — Maintains a single pre-connected UDP socket per instance, eliminating per-request socket creation overhead, ephemeral port exhaustion, and `TIME_WAIT` accumulation
- **Thread-safe** — All public members are safe for concurrent use; responses are correlated by RADIUS Identifier
- **IPv4 and IPv6** — Dual-stack support with automatic address family detection
- **Async/await** — Fully asynchronous send/receive with `CancellationToken` support
- **Configurable retries** — Per-request retry count and socket timeout
- **Comprehensive attribute support** — Standard attributes, Vendor-Specific Attributes (VSA), Tunnel attributes, and more
- **Authentication protocols** — PAP, CHAP, and EAP (Message-Authenticator) support
- **Security** — Sensitive key material is zeroed after use via `CryptographicOperations.ZeroMemory`; constant-time authenticator comparison prevents timing side-channel attacks
- **Trimming and AOT compatible** — Annotated for .NET trimming and Native AOT scenarios

## Installation

Clone the repository and add a project reference:
    
```bash
git clone https://github.com/enteriaza/RadiusClient.git
```

In your project file, add the following reference:

```xml
<ProjectReference Include="..\RadiusClient\RadiusClient.csproj" />
```

## Quick Start

### PAP Authentication

```csharp
using Radius;

using var client = new RadiusClient("radius.example.com", "MySharedSecret");

// Build the Access-Request packet
RadiusPacket request = client.CreatePacket(RadiusCode.ACCESS_REQUEST);
request.SetAttribute(new RadiusAttributes(RadiusAttributeType.USER_NAME, "alice"));

// Encode the password using PAP (RFC 2865 §5.2)
byte[] encodedPassword = RadiusUtils.EncodePapPassword(
    System.Text.Encoding.ASCII.GetBytes("password123"),
    request.Authenticator,
    System.Text.Encoding.ASCII.GetBytes("MySharedSecret"));
request.SetAttribute(new RadiusAttributes(RadiusAttributeType.USER_PASSWORD, encodedPassword));

// Sign and send
request.SetAuthenticator(System.Text.Encoding.ASCII.GetBytes("MySharedSecret"));

RadiusPacket? response = await client.SendAndReceivePacketAsync(request);

if (response is not null && client.VerifyAuthenticator(request, response))
{
    Console.WriteLine($"Response: {response.PacketType}");
}
```

### Accounting

```csharp
using Radius;

using var client = new RadiusClient("radius.example.com", "MySharedSecret");

RadiusPacket acctRequest = client.CreatePacket(RadiusCode.ACCOUNTING_REQUEST);
acctRequest.SetAttribute(new RadiusAttributes(RadiusAttributeType.USER_NAME, "alice"));
acctRequest.SetAttribute(new RadiusAttributes(RadiusAttributeType.ACCT_STATUS_TYPE, (int)ACCT_STATUS_TYPE.START));
acctRequest.SetAttribute(new RadiusAttributes(RadiusAttributeType.ACCT_SESSION_ID, "session-001"));

// Accounting packets use a zeroed-then-hashed authenticator
acctRequest.SetAuthenticator(System.Text.Encoding.ASCII.GetBytes("MySharedSecret"));

RadiusPacket? response = await client.SendAndReceivePacketAsync(acctRequest);
```

### Server Ping (Status-Server)

```csharp
using Radius;

using var client = new RadiusClient("radius.example.com", "MySharedSecret");
RadiusPacket? pong = await client.PingAsync();

Console.WriteLine(pong is not null ? "Server is reachable" : "Server did not respond");
```

### Vendor-Specific Attributes (VSA)

```csharp
// Append a vendor-specific attribute (e.g., Cisco AV-Pair)
var vsa = new VendorSpecificAttributes(
    vendorId: 9,           // Cisco
    vendorSpecificType: 1, // AV-Pair
    vendorSpecificData: System.Text.Encoding.UTF8.GetBytes("shell:priv-lvl=15"));

request.SetAttribute(vsa);
```

## API Overview

### Core Classes

| Class | Description |
|---|---|
| `RadiusClient` | High-throughput UDP client for sending/receiving RADIUS packets. Implements `IDisposable`. |
| `RadiusPacket` | Represents a RADIUS packet — construct outbound requests or parse inbound responses. |
| `RadiusAttributes` | Standard RADIUS attribute in TLV format (RFC 2865 §5). Supports `byte[]`, `string`, `int`, `long`, `DateTime`, and `IPAddress` values. |
| `VendorSpecificAttributes` | Vendor-Specific Attribute (Type 26, RFC 2865 §5.26). |
| `TunnelTypeAttributes` | Tunnel-Type attribute (Type 64, RFC 2868 §3.1). |
| `TunnelMediumTypeAttributes` | Tunnel-Medium-Type attribute (Type 65, RFC 2868 §3.2). |
| `RadiusUtils` | Static utilities for authenticator computation, PAP/CHAP encoding, Tunnel-Password obfuscation, and binary helpers. |

### RadiusClient Constructor

```csharp
public RadiusClient(
    string hostName,
    string sharedSecret,
    int sockTimeout = 3000,
    int authPort = 1812,
    int acctPort = 1813,
    IPEndPoint? localEndPoint = null)
```

### Key Methods

| Method | Description |
|---|---|
| `CreatePacket(RadiusCode)` | Creates a new outbound packet with a random Identifier. |
| `SendAndReceivePacketAsync(RadiusPacket, int, CancellationToken)` | Sends a packet and awaits a matching response, with configurable retries. |
| `PingAsync(CancellationToken)` | Sends a Status-Server probe (RFC 5997) — single attempt, no retries. |
| `VerifyAuthenticator(RadiusPacket, RadiusPacket)` | Verifies the Response Authenticator of a received reply (constant-time comparison). |

### Packet Construction Sequence

1. Create a packet via `CreatePacket()` or `new RadiusPacket(RadiusCode)`.
2. Add attributes via `SetAttribute()`.
3. *(Optional)* Call `SetMessageAuthenticator()` for EAP or enhanced security (RFC 3579 §3.2).
4. Call `SetAuthenticator()` to finalize the packet.
5. Send via `SendAndReceivePacketAsync()`.

## RFC Compliance

| RFC | Description | Support |
|---|---|---|
| [RFC 2865](https://tools.ietf.org/html/rfc2865) | RADIUS Authentication | ✅ Full |
| [RFC 2866](https://tools.ietf.org/html/rfc2866) | RADIUS Accounting | ✅ Full |
| [RFC 2868](https://tools.ietf.org/html/rfc2868) | RADIUS Tunnel Attributes | ✅ Full |
| [RFC 3579](https://tools.ietf.org/html/rfc3579) | RADIUS EAP Support (Message-Authenticator) | ✅ Full |
| [RFC 5176](https://tools.ietf.org/html/rfc5176) | Dynamic Authorization (CoA / Disconnect) | ✅ Full |
| [RFC 5997](https://tools.ietf.org/html/rfc5997) | Status-Server | ✅ Full |

## Requirements

- .NET 8.0 or later

## License

This project is licensed under the [MIT License](LICENSE).  