using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.Alanube.Models;

public sealed record OfficeEditModel : BaseNopModel
{
    public string Id { get; set; }
    public string CompanyId { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Offices.Fields.Type")]
    [Required]
    public string Type { get; set; } = "associated";

    [NopResourceDisplayName("Plugins.Misc.Alanube.Offices.Fields.Email")]
    [StringLength(50)]
    public string Email { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Offices.Fields.Code")]
    [Required, StringLength(4, MinimumLength = 4)]
    public string Code { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Offices.Fields.Coordinates")]
    [Required, StringLength(22, MinimumLength = 1)]
    public string Coordinates { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Offices.Fields.Address")]
    [Required, StringLength(100, MinimumLength = 1)]
    public string Address { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Offices.Fields.Telephone")]
    [Required, StringLength(12, MinimumLength = 7)]
    public string Telephone { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Offices.Fields.Location")]
    public string Location { get; set; }
}
