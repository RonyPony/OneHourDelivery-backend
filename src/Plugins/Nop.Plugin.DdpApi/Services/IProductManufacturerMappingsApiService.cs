using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using static Nop.Plugin.DdpApi.Infrastructure.Constants;

namespace Nop.Plugin.DdpApi.Services
{
    public interface IProductManufacturerMappingsApiService
    {
        IList<ProductManufacturer> GetMappings(int? productId = null, int? manufacturerId = null, int limit = Configurations.DefaultLimit, 
            int page = Configurations.DefaultPageValue, int sinceId = Configurations.DefaultSinceId);

        int GetMappingsCount(int? productId = null, int? manufacturerId = null);

        ProductManufacturer GetById(int id);
    }
}