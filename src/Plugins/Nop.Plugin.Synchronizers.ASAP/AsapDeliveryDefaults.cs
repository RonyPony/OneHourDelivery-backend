namespace Nop.Plugin.Synchronizers.ASAP
{
    /// <summary>
    /// Represents a ASAP delivery plug-in information.
    /// </summary>
    public static class AsapDeliveryDefaults
    {
        /// <summary>
        /// Represent the system name.
        /// </summary>
        public const string SystemName = "Synchronizers.ASAP";

        /// <summary>
        /// Represent the configuration route.
        /// </summary>
        public const string AsapConfigurationRoute = "Plugin.Synchronizers.ASAP.Configure";

        /// <summary>
        /// Represent the configuration resource.
        /// </summary>
        public const string AsapConfigurationResources = "Plugins.Synchronizers.ASAP";

        /// <summary>
        /// Gets the plugin task name.
        /// </summary>
        public static string TaskName => "ASAP Delivery Status Monitor";

        /// <summary>
        /// Gets the plugin task type.
        /// </summary>
        public static string TaskType => "Nop.Plugin.Synchronizers.ASAP.Services.DeliveryStatusSyncTask";

        /// <summary>
        /// Gets the plugin task duration in seconds.
        /// </summary>
        public static int TaskDuration => 3600;

        /// <summary>
        /// Gets the shipping method name.
        /// </summary>
        public static string ShippingMethodName => "ASAP";
    }
}
