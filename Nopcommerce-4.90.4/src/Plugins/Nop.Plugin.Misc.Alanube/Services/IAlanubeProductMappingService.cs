using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public interface IAlanubeProductMappingService
{
    Task<AlanubeProductMapping> GetByProductIdAsync(int productId);
    Task SaveAsync(AlanubeProductMapping mapping);
}
