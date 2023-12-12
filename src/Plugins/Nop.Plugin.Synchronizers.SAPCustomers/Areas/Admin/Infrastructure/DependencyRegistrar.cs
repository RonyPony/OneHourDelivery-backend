using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Areas.Admin.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for registering the different dependencies of this plugin.
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        void IDependencyRegistrar.Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config) { }

        /// <summary>
        /// Represents the order.
        /// </summary>
        public int Order => 1;
    }
}
