namespace Nop.Plugin.Synchronizers.CommonERPTables.Helpers
{
    /// <summary>
    /// Represents a helper class with some default information that the plugin used across the application.
    /// </summary>
    public sealed class DefaultsInfo
    {
        /// <summary>
        /// Gets the plugin system name.
        /// </summary>
        public static string SystemName => "Synchronizers.CommonERPTables";

        /// <summary>
        /// Gets the Customers mapping route name
        /// </summary>
        public static string CustomersMappingRouteName => "Synchronizers.CommonERPTables.Customers";

        /// <summary>
        /// Gets the Orders mapping route name
        /// </summary>
        public static string OrdersMappingRouteName => "Synchronizers.CommonERPTables.Orders";

        /// <summary>
        /// Gets the Orders mapping route name
        /// </summary>
        public static string ProductsMappingRouteName => "Synchronizers.CommonERPTables.Products";
    }
}
