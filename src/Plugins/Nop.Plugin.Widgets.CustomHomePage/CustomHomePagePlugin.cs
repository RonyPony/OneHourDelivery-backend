using Nop.Core;
using Nop.Services.Plugins;


namespace Nop.Plugin.Widgets.CustomHomePage
{
    /// <summary>
    /// CustomHomePage Plugin
    /// </summary>
    public class CustomHomePagePlugin : BasePlugin
    {
        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
    }
}