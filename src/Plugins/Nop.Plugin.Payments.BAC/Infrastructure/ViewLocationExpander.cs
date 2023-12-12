using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Web.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Payments.BAC.Infrastructure
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName ==  AreaNames.Admin)
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Payments.BAC/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/Nop.Plugin.Payments.BAC/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}
