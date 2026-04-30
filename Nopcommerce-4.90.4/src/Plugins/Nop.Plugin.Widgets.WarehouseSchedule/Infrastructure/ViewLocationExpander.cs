using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Infrastructure
{
    /// <summary>
    /// A view location expander for the plugin.
    /// </summary>
    public class ViewLocationExpander : IViewLocationExpander
    {
    
        /// <summary>
        ///<inheritdoc/>
        /// </summary>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        /// <summary>
        ///<inheritdoc/>
        /// </summary>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] { $"/{WarehouseScheduleDefaults.OutputDir}/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/{WarehouseScheduleDefaults.OutputDir}/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}
