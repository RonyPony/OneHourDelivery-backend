using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Helpers
{
    /// <summary>
    /// Represents the default values used by the WAPI Orders Synchronizer plugin.
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// Gets the system name of the plugin.
        /// </summary>
        public static string SystemName => "Synchronizers.WAPIOrders";

        /// <summary>
        /// Gets the plugin locale resources prefix.
        /// </summary>
        public static string LocaleResorcesPrefix => "Plugins.Synchronizers.WAPIOrders";

        /// <summary>
        /// Gets the plugin output directory.
        /// </summary>
        public static string PluginOutputDir => "Plugins/Synchronizers.WAPIOrders";

        /// <summary>
        /// Gets the plugin configuration route name.
        /// </summary>
        public static string ConfigurationRouteName => "Plugin.Synchronizers.WAPIOrders.Configure";

        /// <summary>
        /// Gets the authorization scheme value used for request to WAPI environment.
        /// </summary>
        public static string AuthorizationScheme => "key";

        /// <summary>
        /// Gets the pickup type of an order depending on whether is pickup in store or not.
        /// </summary>
        public static Dictionary<bool, string> PickupTypesDictionary => new Dictionary<bool, string>
        {
            [true] = "InStore",
            [false] = "Delivery"
        };

        /// <summary>
        /// Gets the delivery type of an order depending on whether is pickup in store or not.
        /// </summary>
        public static Dictionary<bool, string> DeliveryTypesDictionary => new Dictionary<bool, string>
        {
            [true] = "own",
            [false] = "thirdParty"
        };

        /// <summary>
        /// Gets the shipping method name for asap delivery.
        /// </summary>
        public static Dictionary<string, string> DeliveryItemCodeByShippingMethodName => new Dictionary<string, string>
        {
            ["ASAP"] = "S00704",
            ["Multientrega"] = "S00705"
        };
    }
}
