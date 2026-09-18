using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.Alanube.Models;

public sealed record ProductMappingModel : BaseNopModel
{
    public int ProductId { get; set; }
    public string GoodsServiceCode { get; set; }
    public string MeasurementUnitCode { get; set; }
    public string TaxCode { get; set; }
    public string DescriptionOverride { get; set; }
    public IList<CatalogOptionModel> GoodsServices { get; set; } = new List<CatalogOptionModel>();
    public IList<CatalogOptionModel> MeasurementUnits { get; set; } = new List<CatalogOptionModel>();
}

public sealed class CatalogOptionModel { public string Code { get; set; } public string Description { get; set; } }
