using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Synchronizers.ASAP.Infrastructure
{
    /// <summary>
    /// Represents the view location expander for the plugin.
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
        #region Methods

        /// <summary>
        /// Determines the values that would be consumed by this implementation of <see cref="IViewLocationExpander"/>.
        /// </summary>
        /// <param name="context">The <see cref="ViewLocationExpanderContext"/> for the current view location expansion operation.</param>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        /// <summary>
        /// Determines potential locations for a view.
        /// </summary>
        /// <param name="context">The <see cref="ViewLocationExpanderContext"/> for the current view location expansion operation.</param>
        /// <param name="viewLocations">The sequence of view locations to expand.</param>
        /// <returns>A list of expanded view locations.</returns>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Synchronizers.ASAP/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Synchronizers.ASAP/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }

        #endregion
    }
}
