using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Caching;
using Nop.Core.Domain.Orders;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Services;
using Nop.Plugin.Api.Factories;
using Nop.Plugin.Api.Helpers;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Factories;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Orders;
using Nop.Services.Vendors;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Infrastructure
{
    /// <summary>
    /// Plugin dependency registrar.
    /// </summary>
    public class DependencyRegistrar : INopStartup
    {
        /// <summary>
        /// Register plugin services.
        /// </summary>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerAddressGeocodingServices, CustomerAddressGeocodingServices>();
            services.AddScoped<IVendorReviewsService, VendorReviewsService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IVendorDeliveryAppService, DeliveryAppVendorService>();
            services.AddScoped<IDeliveryAppProductService, DeliveryAppProductService>();
            services.AddScoped<IVendorWarehouseMappingModelFactory, VendorWarehouseMappingModelFactory>();
            services.AddScoped<IDeliveryAppOrderService, DeliveryAppOrderService>();
            services.AddScoped<IDeliveryAppAddressService, DeliveryAppAddressService>();
            services.AddScoped<IDeliveryAppAccountService, DeliveryAppAccountService>();
            services.AddScoped<IDeliveryAppShippingService, DeliveryAppShippingService>();
            services.AddSingleton<VendorAttributesHelper>();
            services.AddSingleton<CachingSettings>();
            services.AddScoped<IAddressAttributeParser, AddressAttributeParserAdapter>();
            services.AddScoped<IAddressAttributeService, AddressAttributeServiceAdapter>();
            services.AddScoped<IAddressAttributeFormatter, AddressAttributeFormatterAdapter>();
            services.AddScoped<ICustomerAttributeParser, CustomerAttributeParserAdapter>();
            services.AddScoped<ICustomerAttributeService, CustomerAttributeServiceAdapter>();
            services.AddScoped<ICheckoutAttributeParser, CheckoutAttributeParserAdapter>();
            services.AddScoped<ICheckoutAttributeService, CheckoutAttributeServiceAdapter>();
            services.AddScoped<IVendorAttributeService, VendorAttributeServiceAdapter>();
            services.AddScoped<IAddressGeoCoordinatesService, AddressGeoCoordinatesService>();
            services.AddScoped<PluginConfigurationSettings>();
            services.AddScoped<Services.IProductAttributeConverter, Services.ProductAttributeConverter>();
            services.AddScoped<IJsonFieldsSerializer, JsonFieldsSerializer>();
            services.AddScoped<IFactory<Order>, OrderFactory>();
            services.AddScoped<IDTODeliveryAppHelper, DeliveryAppDtoHelper>();
            services.AddScoped<ICustomerRatingService, CustomerRatingService>();
            services.AddScoped<IOrderPendingToClosePaymentModelFactory, OrderPendingToClosePaymentModelFactory>();
            services.AddScoped<IOrderPendingToClosePaymentService, OrderPendingToClosePaymentService>();
            services.AddScoped<IOrderExportManager, OrderExportManager>();
            services.AddScoped<IGoogleDirectionService, GoogleDirectionService>();
            services.AddScoped<INotificationCenterService, NotificationCenterService>();
            services.AddScoped<INotificationRequestBuilder, NotificationRequestBuilder>();
            services.AddScoped<IDeliveryAppDriverService, DeliveryAppDriverService>();
            services.AddScoped<IOrderPendingToPayReportServices, OrderPendingToPayReportServices>();
            services.AddScoped<IDeliveryAppBaseAdminModelFactory, DeliveryAppBaseAdminModelFactory>();
            services.AddScoped<IDeliveryAppDiscountModelFactory, DeliveryAppDiscountModelFactory>();
            services.AddScoped<IDeliveryAppDiscountService, DeliveryAppDiscountService>();
            services.AddScoped<IDeliveryAppOrderReportService, DeliveryAppOrderReportService>();
            services.AddScoped<ICustomerFavoriteMappingService, CustomerFavoriteMappingService>();
            services.AddScoped<IOrderPaymentCollectionService, OrderPaymentCollectionService>();
            services.AddScoped<IOrderPaymentMethodService, OrderPaymentMethodService>();
            services.AddScoped<IDeliveryAppDriverReviewValorationFactory, DeliveryAppDriverReviewValorationFactory>();
        }

        /// <summary>
        /// Configure the using of added middleware.
        /// </summary>
        public void Configure(IApplicationBuilder application)
        {
        }

        /// <summary>
        /// Represents the display order.
        /// </summary>
        public int Order => 1;
    }
}
