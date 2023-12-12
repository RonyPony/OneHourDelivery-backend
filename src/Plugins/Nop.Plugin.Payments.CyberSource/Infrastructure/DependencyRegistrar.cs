using Autofac;
using Microsoft.AspNetCore.Http;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Payments.CyberSource.Services;

namespace Nop.Plugin.Payments.CyberSource.Infrastructure
{
    /// <summary>
    /// Plug-in dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <inheritdoc />
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<CyberSourceService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DeliveryAppPaymentService>().As<IDeliveryAppPaymentService>().InstancePerLifetimeScope();
            builder.RegisterType<CyberSourceService>().As<ICyberSourceService>().InstancePerLifetimeScope();
        }

        /// <inheritdoc />
        public int Order => 1;
    }
}
