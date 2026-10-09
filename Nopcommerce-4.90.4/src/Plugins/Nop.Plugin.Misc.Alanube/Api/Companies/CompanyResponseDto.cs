using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nop.Plugin.Misc.Alanube.Api.Companies;

/// <summary>
/// Represents the documented company data returned by Alanube.
/// </summary>
public sealed class CompanyResponseDto
{
    public string Id { get; set; }

    public int? NextIdRequest { get; set; }

    public int? NextIdBatchRequest { get; set; }

    public string Name { get; set; }

    public string TradeName { get; set; }

    public string Identification { get; set; }

    public string Ruc { get; set; }

    public CompanyTypeRucResponseDto TypeRuc { get; set; }

    public string CheckDigit { get; set; }

    public string Address { get; set; }

    public string Province { get; set; }

    public string Municipality { get; set; }

    public AlanubeCompanyType? Type { get; set; }

    public string Affiliated { get; set; }

    public string Qr { get; set; }

    public CompanyEmailsResponseDto Emails { get; set; }

    public JsonElement? Logo { get; set; }

    public CompanyCertificatesResponseDto Certificates { get; set; }

    public bool? IsActive { get; set; }

    public int? CertificationStep { get; set; }

    public Dictionary<string, JsonElement> Webhooks { get; set; }

    public CompanyUrlsResponseDto CompanyUrls { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; }

    [JsonIgnore]
    public string TaxIdentifier => !string.IsNullOrWhiteSpace(Ruc) ? Ruc : Identification;

    [JsonIgnore]
    public int? TypeRucCode => TypeRuc?.Code;
}

public sealed class CompanyUrlsResponseDto
{
    public string Reception { get; set; }

    public string Approval { get; set; }

    public string Authentication { get; set; }
}

[JsonConverter(typeof(CompanyTypeRucResponseDtoConverter))]
public sealed class CompanyTypeRucResponseDto
{
    public int? Code { get; set; }

    public string Value { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; }
}

public sealed class CompanyTypeRucResponseDtoConverter : JsonConverter<CompanyTypeRucResponseDto>
{
    public override CompanyTypeRucResponseDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType == JsonTokenType.Number)
        {
            return new CompanyTypeRucResponseDto
            {
                Code = reader.TryGetInt32(out var code) ? code : null
            };
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            return new CompanyTypeRucResponseDto
            {
                Code = int.TryParse(value, out var code) ? code : null,
                Value = value
            };
        }

        if (reader.TokenType == JsonTokenType.StartObject)
        {
            using var document = JsonDocument.ParseValue(ref reader);
            var result = new CompanyTypeRucResponseDto
            {
                AdditionalData = new Dictionary<string, JsonElement>()
            };

            foreach (var property in document.RootElement.EnumerateObject())
            {
                if (property.NameEquals("code"))
                {
                    result.Code = property.Value.ValueKind == JsonValueKind.Number && property.Value.TryGetInt32(out var code)
                        ? code
                        : int.TryParse(property.Value.ToString(), out var parsedCode) ? parsedCode : null;
                    continue;
                }

                if (property.NameEquals("value"))
                {
                    result.Value = property.Value.ValueKind == JsonValueKind.String ? property.Value.GetString() : property.Value.ToString();
                    continue;
                }

                result.AdditionalData[property.Name] = property.Value.Clone();
            }

            if (result.AdditionalData.Count == 0)
                result.AdditionalData = null;

            return result;
        }

        using var skipped = JsonDocument.ParseValue(ref reader);
        return new CompanyTypeRucResponseDto { Value = skipped.RootElement.ToString() };
    }

    public override void Write(Utf8JsonWriter writer, CompanyTypeRucResponseDto value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, new { value?.Code, value?.Value }, options);
    }
}

public sealed class CompanyEmailsResponseDto
{
    public bool? Active { get; set; }

    public string ReplyTo { get; set; }

    public string Message { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; }
}

public sealed class CompanyCertificatesResponseDto
{
    public CompanyCertificateResponseDto Authentication { get; set; }

    public CompanyCertificateResponseDto Signature { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; }
}

public sealed class CompanyCertificateResponseDto
{
    public string Extension { get; set; }

    [JsonIgnore]
    public string Password { get; set; }

    public string IssuerName { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }

    public string SerialNumber { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; }
}
