using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Helpers
{
    /// <summary>
    /// Represents plugin constants.
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// Gets the plugin system name.
        /// </summary>
        public static string SystemName => "Widgets.GoogleMapsIntegrationMultientrega";

        /// <summary>
        /// Gets the locale resources name prefix.
        /// </summary>
        public static string ResourcesNamePrefix => "Plugin.Widgets.GoogleMapsIntegration";

        /// <summary>
        /// Gets the default Google Maps Integration plugin output directory.
        /// </summary>
        public static string PluginOutputDir => "Plugins/Widgets.GoogleMapsIntegrationMultientrega";

        /// <summary>
        /// Gets the configuration route name for Google Maps Integration plugin.
        /// </summary>
        public static string ConfigurationRouteName => "Plugin.Widgets.GoogleMapsIntegrationMultientrega.Configure";

        /// <summary>
        /// Gets the default directory for checkout views.
        /// </summary>
        public static string CheckoutViewsDir => "Views/Checkout";

        /// <summary>
        /// Gets the name of the Multientrega's structure sync schedule task.
        /// </summary>
        public static string ScheduleTaskName => "Multientrega's Structure Sync Task";

        /// <summary>
        /// Gets the type of the inventory sync schedule task.
        /// </summary>
        public static string ScheduleTaskType => "Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Tasks.SyncMultientregaStructureTask";

        /// <summary>
        /// Gets the seconds in a week period.
        /// </summary>
        public static int SecondsInAWeek => 604800;

        /// <summary>
        /// Indicates the shipping method name for this plugin.
        /// </summary>
        public static string ShippingMethodName => "Multientrega";

        public static Dictionary<string, string> WidgetZonesViewComponentNamesDictionary => new Dictionary<string, string>
        {
            [AdminWidgetZones.OrderAddressDetailsBottom] = "UpdateOrderAddressLatLng",
            [PublicWidgetZones.CheckoutBillingAddressMiddle] = "CheckoutAddNewAddress",
            [PublicWidgetZones.CheckoutShippingAddressMiddle] = "CheckoutAddNewAddress"
        };
    }
}
