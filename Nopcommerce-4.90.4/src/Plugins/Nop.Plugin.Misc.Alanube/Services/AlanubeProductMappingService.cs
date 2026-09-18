using Nop.Data;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public sealed class AlanubeProductMappingService : IAlanubeProductMappingService
{
    private readonly IRepository<AlanubeProductMapping> _mappingRepository;
    private readonly IAlanubeCatalogService _catalogService;

    public AlanubeProductMappingService(IRepository<AlanubeProductMapping> mappingRepository, IAlanubeCatalogService catalogService)
    { _mappingRepository = mappingRepository; _catalogService = catalogService; }

    public async Task<AlanubeProductMapping> GetByProductIdAsync(int productId) => productId <= 0 ? null : await _mappingRepository.Table.FirstOrDefaultAsync(x => x.ProductId == productId);

    public async Task SaveAsync(AlanubeProductMapping mapping)
    {
        ArgumentNullException.ThrowIfNull(mapping);
        if (mapping.ProductId <= 0) throw new ArgumentException("A product is required.", nameof(mapping));
        if (await _catalogService.GetByCodeAsync(AlanubeCatalogType.GoodsAndServices, mapping.GoodsServiceCode) == null)
            throw new ArgumentException("The goods and services code does not exist in the local catalog.", nameof(mapping));
        if (await _catalogService.GetByCodeAsync(AlanubeCatalogType.MeasurementUnit, mapping.MeasurementUnitCode) == null)
            throw new ArgumentException("The measurement unit code does not exist in the local catalog.", nameof(mapping));
        var existing = await GetByProductIdAsync(mapping.ProductId);
        var now = DateTime.UtcNow;
        if (existing == null) { mapping.CreatedOnUtc = now; mapping.UpdatedOnUtc = now; await _mappingRepository.InsertAsync(mapping); return; }
        existing.GoodsServiceCode = mapping.GoodsServiceCode; existing.MeasurementUnitCode = mapping.MeasurementUnitCode;
        existing.TaxCode = mapping.TaxCode; existing.DescriptionOverride = mapping.DescriptionOverride; existing.UpdatedOnUtc = now;
        await _mappingRepository.UpdateAsync(existing);
    }
}
