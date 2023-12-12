using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Domain.Orders;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Api.Factories;
using Nop.Plugin.Api.Helpers;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Services;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Factories;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Services;
using Nop.Services.Orders;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Infrastructure
{
    /// <summary>
    /// Plugin dependency registrar.
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// This will registry the dependency of our services.
        /// </summary>
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<CustomerAddressGeocodingServices>().As<ICustomerAddressGeocodingServices>().InstancePerLifetimeScope();
            builder.RegisterType<VendorReviewsService>().As<IVendorReviewsService>().InstancePerLifetimeScope();
            builder.RegisterType<StoreService>().As<IStoreService>().InstancePerLifetimeScope();           
            builder.RegisterType<DeliveryAppVendorService>().As<IVendorDeliveryAppService>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppProductService>().As<IDeliveryAppProductService>().InstancePerLifetimeScope();
            builder.RegisterType<VendorWarehouseMappingModelFactory>().As<IVendorWarehouseMappingModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppOrderService>().As<IDeliveryAppOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppAddressService>().As<IDeliveryAppAddressService>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppAccountService>().As<IDeliveryAppAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppShippingService>().As<IDeliveryAppShippingService>().InstancePerLifetimeScope();
            builder.RegisterInstance(new VendorAttributesHelper());
            builder.RegisterType<AddressGeoCoordinatesService>().As<IAddressGeoCoordinatesService>().InstancePerLifetimeScope();
            builder.RegisterType<Services.ProductAttributeConverter>().As<Services.IProductAttributeConverter>().InstancePerLifetimeScope();
            builder.RegisterType<JsonFieldsSerializer>().As<IJsonFieldsSerializer>().InstancePerLifetimeScope();
            builder.RegisterType<OrderFactory>().As<IFactory<Order>>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppDtoHelper>().As<IDTODeliveryAppHelper>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRatingService>().As<ICustomerRatingService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderPendingToClosePaymentModelFactory>().As<IOrderPendingToClosePaymentModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<OrderPendingToClosePaymentService>().As<IOrderPendingToClosePaymentService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderExportManager>().As<IOrderExportManager>().InstancePerLifetimeScope();
            builder.RegisterType<GoogleDirectionService>().As<IGoogleDirectionService>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationCenterService>().As<INotificationCenterService>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationRequestBuilder>().As<INotificationRequestBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppDriverService>().As<IDeliveryAppDriverService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderPendingToPayReportServices>().As<IOrderPendingToPayReportServices>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppBaseAdminModelFactory>().As<IDeliveryAppBaseAdminModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppDiscountModelFactory>().As<IDeliveryAppDiscountModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppDiscountService>().As<IDeliveryAppDiscountService>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppOrderReportService>().As<IDeliveryAppOrderReportService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerFavoriteMappingService>().As<ICustomerFavoriteMappingService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderPaymentCollectionService>().As<IOrderPaymentCollectionService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderPaymentMethodService>().As<IOrderPaymentMethodService>().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppDriverReviewValorationFactory>().As<IDeliveryAppDriverReviewValorationFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CustomOrderProcessingService>().As<IOrderProcessingService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomOrderService>().As<IOrderService>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Represents the display order.
        /// </summary>
        public int Order => 1;
    }
}
