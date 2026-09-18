namespace Nop.Plugin.Misc.Alanube.Api.Companies;

/// <summary>
/// Represents the documented DGI certificates required by the Panama Company API.
/// </summary>
public sealed class CompanyCertificatesRequestDto
{
    public CompanyCertificateRequestDto Signature { get; set; }

    public CompanyCertificateRequestDto Authentication { get; set; }
}

/// <summary>
/// Represents one certificate payload supplied as base64 content.
/// </summary>
public sealed class CompanyCertificateRequestDto
{
    public string Content { get; set; }
}
