using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Payments.BAC.Services;

namespace Nop.Plugin.Payments.BAC.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for register the different dependency need for the AZUL Payment Page plug-in.
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <inheritdoc />
        void IDependencyRegistrar.Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
            => builder.RegisterType<BacServiceManager>().AsSelf().InstancePerLifetimeScope();

        /// <summary>
        /// Represents the oder for this plugin.
        /// </summary>
        public int Order => 1;
    }
}
