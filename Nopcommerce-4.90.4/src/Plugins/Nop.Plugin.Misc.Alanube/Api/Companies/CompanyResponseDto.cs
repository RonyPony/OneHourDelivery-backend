namespace Nop.Plugin.Misc.Alanube.Api.Companies;

/// <summary>
/// Represents the documented company data returned by Alanube.
/// </summary>
public sealed class CompanyResponseDto
{
    /// <summary>
    /// Gets or sets the Alanube company identifier.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the taxpayer registration number.
    /// </summary>
    public string Ruc { get; set; }

    /// <summary>
    /// Gets or sets the taxpayer type: 1 natural or 2 legal.
    /// </summary>
    public int? TypeRuc { get; set; }

    /// <summary>
    /// Gets or sets the trade name.
    /// </summary>
    public string TradeName { get; set; }

    /// <summary>
    /// Gets or sets the DGI affiliation status.
    /// </summary>
    public string Affiliated { get; set; }

    /// <summary>
    /// Gets or sets the company type: main or associated.
    /// </summary>
    public AlanubeCompanyType? Type { get; set; }
}
