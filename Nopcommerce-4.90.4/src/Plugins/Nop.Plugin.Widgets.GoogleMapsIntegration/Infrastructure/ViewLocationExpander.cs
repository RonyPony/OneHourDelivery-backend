using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Infrastructure
{
    /// <summary>
    /// A view location expander for the plugin.
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
                viewLocations = new[] { $"/{Defaults.PluginOutputDir}/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/{Defaults.PluginOutputDir}/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}
