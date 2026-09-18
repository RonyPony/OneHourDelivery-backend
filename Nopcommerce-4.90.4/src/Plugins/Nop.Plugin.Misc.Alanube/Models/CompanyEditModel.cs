using System.ComponentModel.DataAnnotations;
using Nop.Plugin.Misc.Alanube.Api.Companies;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.Alanube.Models;

public sealed record CompanyEditModel : BaseNopModel
{
    public string Id { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Companies.Fields.Ruc")]
    [Required]
    public string Ruc { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Companies.Fields.TypeRuc")]
    [Required]
    public int TypeRuc { get; set; } = 2;

    [NopResourceDisplayName("Plugins.Misc.Alanube.Companies.Fields.Type")]
    [Required]
    public AlanubeCompanyType Type { get; set; } = AlanubeCompanyType.Associated;

    [NopResourceDisplayName("Plugins.Misc.Alanube.Companies.Fields.TradeName")]
    [StringLength(200, MinimumLength = 2)]
    public string TradeName { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Companies.Fields.Qr")]
    public string Qr { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Companies.Fields.SignatureCertificate")]
    [DataType(DataType.Password)]
    [NoTrim]
    public string SignatureCertificate { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Companies.Fields.AuthenticationCertificate")]
    [DataType(DataType.Password)]
    [NoTrim]
    public string AuthenticationCertificate { get; set; }
}
