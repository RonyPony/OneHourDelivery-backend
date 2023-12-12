namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Helpers
{
    /// <summary>
    /// Represents the default info to the RegionsOnRegisterPage plugin.
    /// </summary>
    public sealed class DefaultsInfo
    {
        /// <summary>
        /// Gets the plug-in system name
        /// </summary>
        public static string SystemName => "Widgets.RegionsOnRegisterPage";

        /// <summary>
        /// RegionsOnRegisterPagePlugin component name
        /// </summary>
        public const string RegionsOnRegisterPagePluginComponentName = "WidgetsRegionsOnRegisterPage";

        /// <summary>
        ///  Represents the widgets zone name.
        /// </summary>
        public static string RegionWidgetZoneName = "register_page_region_dropdown";
    }
}