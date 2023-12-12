using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widget.AutoAdvanceSearch.Infrastructure
{
    /// <summary>
    /// Represents an implementation of <see cref="IViewLocationExpander"/> for this plugin.
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
                viewLocations = new[] { $"/Plugins/Widget.AutoAdvanceSearch/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/Widget.AutoAdvanceSearch/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}
