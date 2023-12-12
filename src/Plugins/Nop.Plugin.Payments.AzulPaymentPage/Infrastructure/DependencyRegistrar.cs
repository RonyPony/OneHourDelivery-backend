using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Payments.AzulPaymentPage.Services;

namespace Nop.Plugin.Payments.AzulPaymentPage.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for register the different dependency need for the AZUL Payment Page plug-in.
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">An instance of <see cref="ContainerBuilder"/></param>
        /// <param name="typeFinder">An implementation of <see cref="ITypeFinder"/></param>
        /// <param name="config">An instance of <see cref="NopConfig"/></param>
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config) 
            => builder.RegisterType<AzulServiceManager>().AsSelf().InstancePerLifetimeScope();

        public int Order => 1;
    }
}
