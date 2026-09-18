using Nop.Data;
using Nop.Plugin.Misc.Alanube.Api;
using Nop.Plugin.Misc.Alanube.Api.Catalogs;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public sealed class AlanubeCatalogService : IAlanubeCatalogService
{
    private readonly IRepository<AlanubeCatalogEntry> _repository;
    private readonly IAlanubeCatalogClient _catalogClient;

    public AlanubeCatalogService(IRepository<AlanubeCatalogEntry> repository, IAlanubeCatalogClient catalogClient)
    {
        _repository = repository;
        _catalogClient = catalogClient;
    }

    public async Task<AlanubeCatalogSyncResult> SyncCatalogsAsync(CancellationToken cancellationToken = default)
    {
        var result = new AlanubeCatalogSyncResult();
        await SyncAsync(AlanubeCatalogType.Country, () => _catalogClient.GetCountriesAsync(cancellationToken), x => x.Code, x => x.Value, result, cancellationToken);
        await SyncAsync(AlanubeCatalogType.Province, () => _catalogClient.GetProvincesAsync(cancellationToken), x => x.Code, x => x.Value, result, cancellationToken);
        await SyncAsync(AlanubeCatalogType.District, () => _catalogClient.GetDistrictsAsync(cancellationToken), x => x.Code, x => x.Value, result, cancellationToken);
        await SyncAsync(AlanubeCatalogType.Corregimiento, () => _catalogClient.GetCorregimientosAsync(cancellationToken), x => x.Code, x => x.Value, result, cancellationToken);
        await SyncAsync(AlanubeCatalogType.Location, () => _catalogClient.GetLocationsAsync(cancellationToken), x => x.Code, x => x.Value, result, cancellationToken);
        await SyncAsync(AlanubeCatalogType.MeasurementUnit, () => _catalogClient.GetMeasurementUnitsAsync(cancellationToken), x => x.Code, x => x.Value, result, cancellationToken);
        await SyncAsync(AlanubeCatalogType.GoodsAndServices, () => _catalogClient.GetGoodsAndServicesAsync(cancellationToken), x => x.Code, x => x.Value, result, cancellationToken);
        result.CompletedOnUtc = DateTime.UtcNow;
        return result;
    }

    private async Task SyncAsync<T>(AlanubeCatalogType type, Func<Task<AlanubeApiResponse<IReadOnlyList<T>>>> load,
        Func<T, string> code, Func<T, string> description, AlanubeCatalogSyncResult result, CancellationToken cancellationToken)
    {
        var summary = new AlanubeCatalogSyncItem { CatalogType = type };
        result.Items.Add(summary);
        try
        {
            var response = await load();
            if (!response.IsSuccessStatusCode || response.Body == null)
            {
                summary.ErrorMessage = response.Error?.Message ?? $"Alanube returned HTTP status {response.StatusCode}.";
                return;
            }
            foreach (var item in response.Body)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (string.IsNullOrWhiteSpace(code(item)))
                    continue;
                await UpsertAsync(new AlanubeCatalogEntry
                {
                    CatalogType = type, ExternalCode = code(item), Description = description(item) ?? string.Empty, Published = true
                });
                summary.RecordsProcessed++;
            }
            summary.Succeeded = true;
        }
        catch (AlanubeApiException exception)
        {
            summary.ErrorMessage = exception.Error?.Message ?? exception.Message;
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            summary.ErrorMessage = exception.Message;
        }
    }

    public async Task UpsertAsync(AlanubeCatalogEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);
        if (string.IsNullOrWhiteSpace(entry.ExternalCode))
            throw new ArgumentException("An external catalog code is required.", nameof(entry));

        var existing = await GetByCodeAsync(entry.CatalogType, entry.ExternalCode);
        if (existing == null)
        {
            if (entry.LastSyncedOnUtc == default)
                entry.LastSyncedOnUtc = DateTime.UtcNow;
            await _repository.InsertAsync(entry);
            return;
        }

        existing.Description = entry.Description;
        existing.ParentCode = entry.ParentCode;
        existing.Published = entry.Published;
        existing.LastSyncedOnUtc = entry.LastSyncedOnUtc == default ? DateTime.UtcNow : entry.LastSyncedOnUtc;
        await _repository.UpdateAsync(existing);
    }

    public async Task<AlanubeCatalogEntry> GetByCodeAsync(AlanubeCatalogType catalogType, string externalCode) =>
        string.IsNullOrWhiteSpace(externalCode) ? null : await _repository.Table.FirstOrDefaultAsync(entry =>
            entry.CatalogType == catalogType && entry.ExternalCode == externalCode);

    public async Task<IList<AlanubeCatalogEntry>> SearchAsync(AlanubeCatalogType catalogType, string searchText = null)
    {
        var query = _repository.Table.Where(entry => entry.CatalogType == catalogType);
        if (!string.IsNullOrWhiteSpace(searchText))
            query = query.Where(entry => entry.ExternalCode.Contains(searchText) || entry.Description.Contains(searchText));
        return await query.OrderBy(entry => entry.Description).ToListAsync();
    }

    public async Task<IList<AlanubeCatalogEntry>> GetStaleAsync(DateTime olderThanUtc, int maxCount = 100) =>
        maxCount <= 0 ? new List<AlanubeCatalogEntry>() : await _repository.Table
            .Where(entry => entry.LastSyncedOnUtc < olderThanUtc)
            .OrderBy(entry => entry.LastSyncedOnUtc)
            .Take(maxCount)
            .ToListAsync();
}
