using AutoMapper;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Localization;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.DdpApi.Areas.Admin.Models;
using Nop.Plugin.DdpApi.Domain;
using Nop.Plugin.DdpApi.DTO.Languages;
using Nop.Plugin.DdpApi.DTO.ProductAttributes;
using Nop.Plugin.DdpApi.DTO.ProductCategoryMappings;
using Nop.Plugin.DdpApi.DTO.ProductManufacturerMappings;
using Nop.Plugin.DdpApi.DTO.Products;
using Nop.Plugin.DdpApi.DTO.SpecificationAttributes;
using Nop.Plugin.DdpApi.MappingExtensions;
using System.Net;

namespace Nop.Plugin.DdpApi.AutoMapper
{
    public class ApiMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public ApiMapperConfiguration()
        {
            CreateMap<ApiSettings, ConfigurationModel>();
            CreateMap<ConfigurationModel, ApiSettings>();

            CreateMap<ProductCategory, ProductCategoryMappingDto>();

            CreateMap<ProductManufacturer, ProductManufacturerMappingsDto>();

            CreateMap<Language, LanguageDto>();

            CreateProductMap();

            CreateMap<ProductAttributeValue, ProductAttributeValueDto>();

            CreateMap<ProductAttribute, ProductAttributeDto>();

            CreateMap<ProductSpecificationAttribute, ProductSpecificationAttributeDto>();

            CreateMap<SpecificationAttribute, SpecificationAttributeDto>();
            CreateMap<SpecificationAttributeOption, SpecificationAttributeOptionDto>();
        }

        public int Order => 0;

        private new static void CreateMap<TSource, TDestination>()
        {
            AutoMapperApiConfiguration.MapperConfigurationExpression.CreateMap<TSource, TDestination>()
                                      .IgnoreAllNonExisting();
        }

        private void CreateProductMap()
        {
            AutoMapperApiConfiguration.MapperConfigurationExpression.CreateMap<Product, ProductDto>()
            .ForMember(x => x.FullDescription, y => y.MapFrom(src => WebUtility.HtmlEncode(src.FullDescription)))
            .IgnoreAllNonExisting();
        }
    }
}
