using Nop.Core.Domain.Catalog;
using Nop.Plugin.DdpApi.AutoMapper;
using Nop.Plugin.DdpApi.DTO.ProductManufacturerMappings;

namespace Nop.Plugin.DdpApi.MappingExtensions
{
    public static class ProductManufacturerMappingDtoMappings
    {
        public static ProductManufacturerMappingsDto ToDto(this ProductManufacturer mapping)
        {
            return mapping.MapTo<ProductManufacturer, ProductManufacturerMappingsDto>();
        }
    }
}