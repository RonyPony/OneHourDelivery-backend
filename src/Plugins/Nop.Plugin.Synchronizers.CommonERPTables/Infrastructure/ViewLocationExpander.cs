using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Web.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for informing the location of the different views of this plugin.
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
        /// <inheritdoc />
        void IViewLocationExpander.PopulateValues(ViewLocationExpanderContext context) { }

        /// <inheritdoc />
        IEnumerable<string> IViewLocationExpander.ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            viewLocations = context.AreaName == AreaNames.Admin ?
                new[] { $"/Plugins/Nop.Plugin.Synchronizers.CommonERPTables/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations)
                : new[] { $"/Plugins/Nop.Plugin.Synchronizers.CommonERPTables/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);

            return viewLocations;
        }
    }
}
