using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Factories;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Services;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Infrastructure
{
    /// <summary>
    /// Plug-in dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// This will registry the dependency of our services
        /// </summary>
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<AddressGeoCoordinatesService>().As<IAddressGeoCoordinatesService>().InstancePerLifetimeScope();
            builder.RegisterType<CheckoutModelCustomFactory>().As<ICheckoutModelCustomFactory>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Represents the display order.
        /// </summary>
        public int Order => 1;
    }
}
