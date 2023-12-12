using System.Collections.Generic;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.Calendar
{
    /// <summary>
    /// Main file for this plug-in
    /// </summary>
    public class CalendarPlugin : BasePlugin, IWidgetPlugin
    {
        public bool HideInWidgetList => false;
        private readonly string WidgetViewComponentName = "Calendar";

        /// <summary>
        /// Get Widget View Component Name fro render plugin
        /// </summary>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return WidgetViewComponentName;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { PublicWidgetZones.HomepageBeforePoll };
        }
    }
}
