using Nop.Plugin.Misc.Alanube.Api.Companies;
using Nop.Plugin.Misc.Alanube.Api.Offices;
using Nop.Web.Framework.Models;
namespace Nop.Plugin.Misc.Alanube.Models;
public sealed record OfficesModel : BaseNopModel
{
    public string CompanyId { get; set; }
    public IList<CompanyResponseDto> Companies { get; set; } = new List<CompanyResponseDto>();
    public IList<OfficeResponseDto> Offices { get; set; } = new List<OfficeResponseDto>();
    public string ErrorMessage { get; set; }
}
