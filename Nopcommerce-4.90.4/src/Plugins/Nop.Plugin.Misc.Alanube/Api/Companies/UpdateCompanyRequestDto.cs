namespace Nop.Plugin.Misc.Alanube.Api.Companies;

/// <summary>
/// Represents the documented request to update a Panama Company.
/// </summary>
public sealed class UpdateCompanyRequestDto
{
    public long? NextIdRequest { get; set; }

    public string Qr { get; set; }

    public string Logo { get; set; }

    public string TradeName { get; set; }

    public CompanyCertificatesRequestDto Certificates { get; set; }

    public object Webhooks { get; set; }

    public object Emails { get; set; }
}
