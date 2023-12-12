namespace Nop.Plugin.Synchronizers.SAPProducts.Helpers
{
    /// <summary>
    /// Represents the default values for the SAP products synchronizer.
    /// </summary>
    public sealed class ProductSyncDefaults
    {
        /// <summary>
        /// Gets the type of the synchronization schedule task.
        /// </summary>
        public static string SynchronizationTask => "Nop.Plugin.Synchronizers.SAPProducts.Services.ProductSyncTask";

        /// <summary>
        /// Gets a default synchronization period (in hours).
        /// </summary>
        public static int DefaultSynchronizationPeriod => 12;

        /// <summary>
        /// Gets a name of the synchronization schedule task.
        /// </summary>
        public static string SynchronizationTaskName => "SAP Products Synchronizer";

        /// <summary>
        /// Gets the configuration route name.
        /// </summary>
        public static string ConfigurationRouteName => "Plugin.Synchronizers.SAPProducts.Configure";
    }
}
