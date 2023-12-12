using Nop.Plugin.DdpApi.AutoMapper;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.DdpApi.DTO.Products;

namespace Nop.Plugin.DdpApi.MappingExtensions
{
    public static class ProductDtoMappings
    {
        public static ProductDto ToDto(this Product product)
        {
            return product.MapTo<Product, ProductDto>();
        }

        public static ProductAttributeValueDto ToDto(this ProductAttributeValue productAttributeValue)
        {
            return productAttributeValue.MapTo<ProductAttributeValue, ProductAttributeValueDto>();
        }
    }
}