using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.ImportProduct.Infrastructure
{
    /// <summary>
    /// Represents the view location expander for the plugin.
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
        /// <summary>
        /// Determines the values that would be consumed by this implementation of <see cref="IViewLocationExpander"/>.
        /// </summary>
        /// <param name="context">An implementation of <see cref="ViewLocationExpanderContext"/>.</param>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
        /// <summary>
        /// Determines potential locations for a view.
        /// </summary>
        /// /// <param name="context">An implementation of <see cref="ViewLocationExpanderContext"/>.</param>
        /// /// <param name="viewLocations">An implementation of <see cref="IEnumerable<string>"/>.</param>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Misc.ImportProduct/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Misc.ImportProduct/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}
