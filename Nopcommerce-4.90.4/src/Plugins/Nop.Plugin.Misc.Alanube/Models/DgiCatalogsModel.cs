using Nop.Plugin.Misc.Alanube.Domain;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.Alanube.Models;

public sealed record DgiCatalogsModel : BaseNopModel
{
    public DateTime? LastSynchronizationUtc { get; set; }
    public IList<DgiCatalogSummaryModel> Catalogs { get; set; } = new List<DgiCatalogSummaryModel>();
}

public sealed class DgiCatalogSummaryModel
{
    public AlanubeCatalogType CatalogType { get; set; }
    public int Count { get; set; }
    public DateTime? LastSynchronizationUtc { get; set; }
}
