using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.CustomHomePage.Components
{
    /// <summary> 
    ///  Widgets CustomHomePage View Component
    /// </summary>
    public class WidgetsCustomHomePageViewComponent : NopViewComponent
    {
        /// <summary>
        /// Invoke method of plugin
        /// </summary>
        /// <param name="widgetZone"></param>
        /// <param name="additionalData"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Widgets.CustomHomePage/Themes/DefaultClean/Views/MundoTresM/Index.cshtml");
        }
    }
}
