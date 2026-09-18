using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public sealed class AlanubeCatalogSyncResult
{
    public IList<AlanubeCatalogSyncItem> Items { get; } = new List<AlanubeCatalogSyncItem>();
    public DateTime CompletedOnUtc { get; set; }
}

public sealed class AlanubeCatalogSyncItem
{
    public AlanubeCatalogType CatalogType { get; set; }
    public int RecordsProcessed { get; set; }
    public bool Succeeded { get; set; }
    public string ErrorMessage { get; set; }
}
