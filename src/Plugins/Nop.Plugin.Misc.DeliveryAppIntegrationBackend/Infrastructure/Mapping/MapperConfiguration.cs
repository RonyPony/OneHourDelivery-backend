using AutoMapper;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Discounts;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domain;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Infrastructure.Mapping
{
    /// <summary>
    /// Represents a configuration mapper
    /// </summary>
    public class MapperConfiguration : Profile, IOrderedMapperProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperConfiguration"/> class.
        /// </summary>
        public MapperConfiguration()
        {
            CreateMap<Category, CategoryData>();
            CreateMap<Address, AddressData>().ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.County));
            CreateMap<VendorWarehouseMappingModel, VendorWarehouseMapping>();
            CreateMap<VendorWarehouseMapping, VendorWarehouseMappingModel>();
            CreateMap<DeliveryAppBackendConfigurationModel, DeliveryAppBackendConfigurationSettings>();
            CreateMap<DeliveryAppBackendConfigurationSettings, DeliveryAppBackendConfigurationModel>();
            CreateMap<OrderDeliveryStatusMapping, OrderDeliveryInfoModel>();
            CreateMap<OrderDeliveryInfoModel, OrderDeliveryStatusMapping>();
            CreateMap<Address, DeliveryAppAddressModel>();
            CreateMap<DeliveryAppAddressModel, Address>();
            CreateMap<DeliveryAppDiscountModel, Discount>();
            CreateMap<Discount, DeliveryAppDiscountModel>();
            CreateMap<OrderPaymentCollectionStatus, OrderPaymentCollectionModel>();
            CreateMap<OrderPaymentCollectionModel, OrderPaymentCollectionStatus>();
        }

        /// <summary>
        /// Gets order of this configuration implementation
        /// </summary>
        public int Order => int.MaxValue;
    }
}
