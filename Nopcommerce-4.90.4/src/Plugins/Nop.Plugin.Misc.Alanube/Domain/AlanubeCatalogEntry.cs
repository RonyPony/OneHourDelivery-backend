using Nop.Core;

namespace Nop.Plugin.Misc.Alanube.Domain;

public sealed class AlanubeCatalogEntry : BaseEntity
{
    public AlanubeCatalogType CatalogType { get; set; }
    public string ExternalCode { get; set; }
    public string Description { get; set; }
    public string ParentCode { get; set; }
    public bool Published { get; set; }
    public DateTime LastSyncedOnUtc { get; set; }
}
