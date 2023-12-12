using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Nop.Plugin.Widgets.CustomFieldsValidator.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for informing the location of the different views of this plugin.
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
        /// <inheritdoc/>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Widgets.CustomFieldValidator/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Widgets.CustomFieldValidator/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            viewLocations = new[] { $"/Plugins/Nop.Plugin.Widgets.CustomFieldValidator/Views/Customer/Register.cshtml" }.Concat(viewLocations);
            viewLocations = new[] { $"/Plugins/Nop.Plugin.Widgets.CustomFieldValidator/Views/Customer/Info.cshtml" }.Concat(viewLocations);

            return viewLocations;
        }
    }
}
