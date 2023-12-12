using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Infrastructure
{
    /// <summary>
    /// A view location expander for this plugin.
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
        ///<inheritdoc/>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        ///<inheritdoc/>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] { $"/{Defaults.OutputDir}/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/{Defaults.OutputDir}/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}
