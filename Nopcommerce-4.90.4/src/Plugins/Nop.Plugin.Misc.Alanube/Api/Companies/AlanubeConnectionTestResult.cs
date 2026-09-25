namespace Nop.Plugin.Misc.Alanube.Api.Companies;

public sealed class AlanubeConnectionTestResult
{
    public bool Success { get; set; }

    public int? StatusCode { get; set; }

    public string ReasonPhrase { get; set; }

    public string Endpoint { get; set; }

    public string HttpMethod { get; set; }

    public string BaseUrl { get; set; }

    public string MaskedApiToken { get; set; }

    public string ResponseBody { get; set; }

    public IReadOnlyList<CompanyResponseDto> Companies { get; set; } = Array.Empty<CompanyResponseDto>();

    public string ErrorMessage { get; set; }

    public string CorrelationId { get; set; }

    public long? ElapsedMilliseconds { get; set; }
}
