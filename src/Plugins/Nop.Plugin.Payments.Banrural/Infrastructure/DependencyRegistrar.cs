using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Payments.Banrural.Services;

namespace Nop.Plugin.Payments.Banrural.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for register the different dependency need for the Banrural plug-in.
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
          => builder.RegisterType<BanruralServiceManager>().AsSelf().InstancePerLifetimeScope();

        public int Order => 1;
    }
}