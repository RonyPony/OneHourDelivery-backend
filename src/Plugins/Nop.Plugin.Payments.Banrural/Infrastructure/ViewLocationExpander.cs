using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Web.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Payments.Banrural.Infrastructure
{
    /// <summary>
    /// Plug-in view location expander
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
        ///<inheritdoc/>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        /// <summary>
        /// Gets the name of a specific view location.
        /// </summary>
        /// <param name="context">A context for containing information for IViewLocationExpander</param>
        /// <param name="viewLocations">A strings' enumerable that is used to concat all the view locations of specifics views.</param>
        /// <returns>A sting that represents a ViewLocation.</returns>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == AreaNames.Admin)
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Payments.Banrural/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Payments.Banrural/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            return viewLocations;
        }

    }
}