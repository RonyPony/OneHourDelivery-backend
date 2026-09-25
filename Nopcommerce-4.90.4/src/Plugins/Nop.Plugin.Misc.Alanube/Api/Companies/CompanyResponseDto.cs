using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nop.Plugin.Misc.Alanube.Api.Companies;

/// <summary>
/// Represents the documented company data returned by Alanube.
/// </summary>
public sealed class CompanyResponseDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string TradeName { get; set; }

    public string Identification { get; set; }

    public string Address { get; set; }

    public string Province { get; set; }

    public string Municipality { get; set; }

    public AlanubeCompanyType? Type { get; set; }

    public int CertificationStep { get; set; }

    public Dictionary<string, JsonElement> Webhooks { get; set; }

    public CompanyUrlsResponseDto CompanyUrls { get; set; }

    [JsonIgnore]
    public string Ruc => Identification;

    [JsonIgnore]
    public int? TypeRuc => null;
}

public sealed class CompanyUrlsResponseDto
{
    public string Reception { get; set; }

    public string Approval { get; set; }

    public string Authentication { get; set; }
}
