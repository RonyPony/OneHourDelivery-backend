using Nop.Core.Domain.Catalog;
using Nop.Plugin.DdpApi.AutoMapper;
using Nop.Plugin.DdpApi.DTO.Products;

namespace Nop.Plugin.DdpApi.MappingExtensions
{
    public static class ProductAttributeCombinationDtoMappings
    {
        public static ProductAttributeCombinationDto ToDto(this ProductAttributeCombination productAttributeCombination)
        {
            return productAttributeCombination.MapTo<ProductAttributeCombination, ProductAttributeCombinationDto>();
        }
    }
}