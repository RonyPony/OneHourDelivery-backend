using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nop.Plugin.Misc.Alanube.Api.Invoices;

/// <summary>
/// Structural invoice request. The contract does not publish the members of the fiscal groups yet.
/// </summary>
public sealed class CreateInvoiceRequest
{
    [JsonPropertyName("information")]
    public JsonElement Information { get; set; }

    [JsonPropertyName("receiver")]
    public JsonElement Receiver { get; set; }

    [JsonPropertyName("items")]
    public JsonElement Items { get; set; }

    [JsonPropertyName("totals")]
    public JsonElement Totals { get; set; }

    [JsonPropertyName("authorizedGroup")]
    public JsonElement? AuthorizedGroup { get; set; }

    [JsonPropertyName("exportation")]
    public JsonElement? Exportation { get; set; }

    [JsonPropertyName("global")]
    public JsonElement? Global { get; set; }

    [JsonPropertyName("logistic")]
    public JsonElement? Logistic { get; set; }

    [JsonPropertyName("delivery")]
    public JsonElement? Delivery { get; set; }
}

/// <summary>
/// Invoice creation response. Alanube's response schema remains unresolved in the contract.
/// </summary>
public sealed class CreateInvoiceResponse
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> Data { get; set; } = new();
}

/// <summary>
/// Invoice query response. Alanube's response schema remains unresolved in the contract.
/// </summary>
public sealed class GetInvoiceResponse
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement> Data { get; set; } = new();
}
