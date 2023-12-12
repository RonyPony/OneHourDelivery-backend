using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Infrastructure
{
    /// <summary>
    /// Plug-in view location expander
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
        /// <inheritdoc />
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            viewLocations = new[] { $"/Plugins/Nop.Plugin.Widgets.RegionsOnRegisterPage/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);

            return viewLocations;
        }

        /// <inheritdoc />
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}