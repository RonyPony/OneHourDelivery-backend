using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Widgets.RegionsOnRegisterPage.Services;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Infrastructure
{
    /// <summary>
    /// Plug-in dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <inheritdoc />
        public int Order => 1;

        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">An instance of <see cref="ContainerBuilder"/></param>
        /// <param name="typeFinder">An implementation of <see cref="ITypeFinder"/></param>
        /// <param name="config">An instance of <see cref="NopConfig"/></param>
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<RegionsOnRegisterPageService>().AsSelf().InstancePerLifetimeScope();
        }
    }
}