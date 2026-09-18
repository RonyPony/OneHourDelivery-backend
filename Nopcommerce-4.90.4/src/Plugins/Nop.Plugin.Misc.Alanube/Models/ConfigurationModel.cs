using System.ComponentModel.DataAnnotations;
using Nop.Plugin.Misc.Alanube.Configuration;
using Nop.Plugin.Misc.Alanube.Api.Companies;
using Nop.Plugin.Misc.Alanube.Api.Offices;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.Alanube.Models;

/// <summary>
/// Represents the Alanube configuration model.
/// </summary>
public record ConfigurationModel : BaseNopModel
{
    public string CompanyId { get; set; }
    public string OfficeId { get; set; }
    public IList<CompanyResponseDto> Companies { get; set; } = new List<CompanyResponseDto>();
    public IList<OfficeResponseDto> Offices { get; set; } = new List<OfficeResponseDto>();
    public bool? TestConnectionSucceeded { get; set; }

    public int? TestConnectionStatusCode { get; set; }

    public string TestConnectionErrorCode { get; set; }

    public string TestConnectionMessage { get; set; }

    public string TestConnectionEnvironment { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Fields.Enabled")]
    public bool Enabled { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Fields.Environment")]
    public AlanubeEnvironment Environment { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Fields.SandboxApiToken")]
    [NoTrim]
    [DataType(DataType.Password)]
    public string SandboxApiToken { get; set; }

    [NopResourceDisplayName("Plugins.Misc.Alanube.Fields.ProductionApiToken")]
    [NoTrim]
    [DataType(DataType.Password)]
    public string ProductionApiToken { get; set; }
}
