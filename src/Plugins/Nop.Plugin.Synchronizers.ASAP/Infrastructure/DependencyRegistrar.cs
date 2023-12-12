using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Synchronizers.ASAP.Contracts;
using Nop.Plugin.Synchronizers.ASAP.Managers;
using Nop.Plugin.Synchronizers.ASAP.Models;
using Nop.Plugin.Synchronizers.ASAP.Services;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Common;
using Nop.Plugin.Synchronizers.ASAP.Controllers;

namespace Nop.Plugin.Synchronizers.ASAP.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        #region Methods

        /// <inheritdoc/>
        void IDependencyRegistrar.Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<AsapDeliveryService>().As<IAsapDeliveryService>().InstancePerLifetimeScope();
            builder.RegisterType<ShippingManager>().As<ShippingManager>().InstancePerLifetimeScope();
            builder.RegisterType<AsapShipmentTracker>().As<AsapShipmentTracker>().InstancePerLifetimeScope();
            builder.RegisterType<OrderManager>().As<OrderManager>().InstancePerLifetimeScope();
            builder.RegisterType<Shipment>().As<Shipment>().InstancePerLifetimeScope();
            builder.RegisterType<Address>().As<Address>().InstancePerLifetimeScope();
            builder.RegisterType<AsapDeliveryController>().As<AsapDeliveryController>().InstancePerLifetimeScope();
        }

        /// <inheritdoc/>
        int IDependencyRegistrar.Order => 1;
    }

    #endregion
}
