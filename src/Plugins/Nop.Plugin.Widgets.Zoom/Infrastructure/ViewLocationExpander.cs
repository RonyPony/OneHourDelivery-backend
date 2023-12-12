using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Plugin.Widgets.Zoom.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.Zoom.Infrastructure
{
    /// <summary>
    /// Represents an implementation of <see cref="IViewLocationExpander"/> for Widget.Zoom plugin.
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
                viewLocations = new[] { $"/{ZoomDefaults.PluginOutputDir}/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/{ZoomDefaults.PluginOutputDir}/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}
