using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Plugin.Synchronizers.WAPIOrders.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Infrastructure
{
    /// <summary>
    /// Represents a view location expander for WAPI Orders Synchronizer.
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
        /// <summary>
        /// Determines the values that would be consumed by this implementation of <see cref="IViewLocationExpander"/>.
        /// </summary>
        /// <param name="context">An instance of <see cref="ViewLocationExpanderContext"/>.</param>
        public void PopulateValues(ViewLocationExpanderContext context) { }

        /// <summary>
        /// Determines potential locations for a view.
        /// </summary>
        /// <param name="context">An instance of <see cref="ViewLocationExpanderContext"/>.</param>
        /// <param name="viewLocations">An implementation of <see cref="IEnumerable{T}"/> where T is <see cref="typeof(string)"/>.</param>
        /// <returns>An implementation of <see cref="IEnumerable{T}"/> where T is <see cref="typeof(string)"/>.</returns>
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
