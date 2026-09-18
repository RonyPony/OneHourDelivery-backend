using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nop.Plugin.Misc.Alanube.Api.Webhooks;

public sealed class AlanubeDocumentWebhookDto
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("stampDate")]
    public DateTimeOffset? StampDate { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("legalStatus")]
    public string LegalStatus { get; set; }

    [JsonPropertyName("companyIdentification")]
    public string CompanyIdentification { get; set; }

    [JsonPropertyName("trackId")]
    public string TrackId { get; set; }

    [JsonPropertyName("documentNumber")]
    public string DocumentNumber { get; set; }

    [JsonPropertyName("sequenceConsumed")]
    public JsonElement? SequenceConsumed { get; set; }

    [JsonPropertyName("signatureDate")]
    public DateTimeOffset? SignatureDate { get; set; }

    [JsonPropertyName("securityCode")]
    public string SecurityCode { get; set; }

    [JsonPropertyName("documentStampUrl")]
    public string DocumentStampUrl { get; set; }

    [JsonPropertyName("xml")]
    public string Xml { get; set; }

    [JsonPropertyName("resumeXml")]
    public string ResumeXml { get; set; }

    [JsonPropertyName("pdf")]
    public string Pdf { get; set; }

    [JsonPropertyName("governmentResponse")]
    public JsonElement? GovernmentResponse { get; set; }

    [JsonPropertyName("error")]
    public JsonElement? Error { get; set; }
}
