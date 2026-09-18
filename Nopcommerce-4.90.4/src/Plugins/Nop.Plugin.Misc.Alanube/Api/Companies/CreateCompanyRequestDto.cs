namespace Nop.Plugin.Misc.Alanube.Api.Companies;

/// <summary>
/// Represents the documented request to create a Panama Company.
/// </summary>
public sealed class CreateCompanyRequestDto
{
    public long? NextIdRequest { get; set; }

    public string Qr { get; set; }

    public string Logo { get; set; }

    public string TradeName { get; set; }

    public string Ruc { get; set; }

    public int TypeRuc { get; set; }

    public string Affiliated { get; set; }

    public AlanubeCompanyType? Type { get; set; }

    public CompanyCertificatesRequestDto Certificates { get; set; }
}
