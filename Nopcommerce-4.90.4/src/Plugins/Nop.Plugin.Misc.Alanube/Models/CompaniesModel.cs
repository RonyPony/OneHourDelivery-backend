using Nop.Plugin.Misc.Alanube.Api.Companies;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.Alanube.Models;

/// <summary>
/// Represents the read-only companies page model.
/// </summary>
public sealed record CompaniesModel : BaseNopModel
{
    public IList<CompanyResponseDto> Companies { get; set; } = new List<CompanyResponseDto>();

    public string ErrorMessage { get; set; }
}
