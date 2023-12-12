using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Misc.ImportProduct.Services;

namespace Nop.Plugin.Misc.ImportProduct.Infrastructure
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
            builder.RegisterType<ExcelImportService>().AsSelf().InstancePerLifetimeScope();
        }
        /// <summary>
        /// Represents the display order.
        /// </summary>
        public int Order => 1;
    }
}
