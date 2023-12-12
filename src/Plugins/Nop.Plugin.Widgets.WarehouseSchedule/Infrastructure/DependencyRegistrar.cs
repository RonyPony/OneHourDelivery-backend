using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Widgets.WarehouseSchedule.Factories;
using Nop.Plugin.Widgets.WarehouseSchedule.Services;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Infrastructure
{
    /// <summary>
    /// Plugin dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register the plugin dependencies.
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<WarehouseScheduleService>().As<IWarehouseScheduleService>().InstancePerLifetimeScope();
            builder.RegisterType<WarehouseScheduleMappingFactory>().As<IWarehouseScheduleMappingFactory>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 1;
    }
}
