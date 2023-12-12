using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Synchronizers.CommonERPTables.Services;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for register the different dependency need for the CommonERPTables plug-in.
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <inheritdoc />
        void IDependencyRegistrar.Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<ErpCustomerServiceManager>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ErpOrderServiceManager>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ErpProductServiceManager>().AsSelf().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Represents the oder for this plugin.
        /// </summary>
        public int Order => 1;
    }
}
