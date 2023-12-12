using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Web.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for informing the location of the different views of this plugin.
    /// </summary>
    public sealed class ViewLocationExpander : IViewLocationExpander
    {
        void IViewLocationExpander.PopulateValues(ViewLocationExpanderContext context)
        {
        }

        IEnumerable<string> IViewLocationExpander.ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == AreaNames.Admin)
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Synchronizers.SAPCustomers/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Synchronizers.SAPCustomers/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}
