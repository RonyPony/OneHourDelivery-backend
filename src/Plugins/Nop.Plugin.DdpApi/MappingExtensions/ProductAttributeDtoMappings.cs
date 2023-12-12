using Nop.Core.Domain.Catalog;
using Nop.Plugin.DdpApi.AutoMapper;
using Nop.Plugin.DdpApi.DTO.ProductAttributes;

namespace Nop.Plugin.DdpApi.MappingExtensions
{
    public static class ProductAttributeDtoMappings
    {
        public static ProductAttributeDto ToDto(this ProductAttribute productAttribute)
        {
            return productAttribute.MapTo<ProductAttribute, ProductAttributeDto>();
        }
    }
}
