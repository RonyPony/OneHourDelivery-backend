using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public interface IAlanubeCatalogService
{
    Task<AlanubeCatalogSyncResult> SyncCatalogsAsync(CancellationToken cancellationToken = default);
    Task UpsertAsync(AlanubeCatalogEntry entry);
    Task<AlanubeCatalogEntry> GetByCodeAsync(AlanubeCatalogType catalogType, string externalCode);
    Task<IList<AlanubeCatalogEntry>> SearchAsync(AlanubeCatalogType catalogType, string searchText = null);
    Task<IList<AlanubeCatalogEntry>> GetStaleAsync(DateTime olderThanUtc, int maxCount = 100);
}
