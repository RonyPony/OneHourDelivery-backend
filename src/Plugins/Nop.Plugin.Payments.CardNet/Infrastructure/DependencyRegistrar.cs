using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Payments.CardNet.Services;

namespace Nop.Plugin.Payments.CardNet.Infrastructure
{
    /// <summary>
    /// Plug-in dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<CardNetService>().AsSelf().InstancePerLifetimeScope();
        }

        public int Order => 1;
    }
}
