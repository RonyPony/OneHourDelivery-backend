using Nop.Plugin.DdpApi.AutoMapper;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.DdpApi.DTO.ProductCategoryMappings;

namespace Nop.Plugin.DdpApi.MappingExtensions
{
    public static class ProductCategoryMappingDtoMappings
    {
        public static ProductCategoryMappingDto ToDto(this ProductCategory mapping)
        {
            return mapping.MapTo<ProductCategory, ProductCategoryMappingDto>();
        }
    }
}