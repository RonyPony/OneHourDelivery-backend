using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.ProductAvailability.Infrastructure
{
    /// <summary>
    /// Plug-in view location expander
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
        /// <summary>
        /// Invoked by a <see cref="RazorViewEngine"/> to determine the values that would be consumed by this instance
        /// of <see cref="IViewLocationExpander"/>. The calculated values are used to determine if the view location
        /// has changed since the last time it was located.
        /// </summary>
        /// <param name="context">The <see cref="ViewLocationExpanderContext"/> for the current view location
        /// expansion operation.</param>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        /// <summary>
        /// Invoked by a <see cref="RazorViewEngine"/> to determine potential locations for a view.
        /// </summary>
        /// <param name="context">The <see cref="ViewLocationExpanderContext"/> for the current view location
        /// expansion operation.</param>
        /// <param name="viewLocations">The sequence of view locations to expand.</param>
        /// <returns>A list of expanded view locations.</returns>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] { $"/{ProductAvailabilityDefault.OutputDir}/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/{ProductAvailabilityDefault.OutputDir}/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}