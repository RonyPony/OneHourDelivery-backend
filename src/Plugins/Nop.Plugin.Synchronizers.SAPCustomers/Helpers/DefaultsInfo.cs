namespace Nop.Plugin.Synchronizers.SAPCustomers.Helpers
{
    /// <summary>
    /// Represents a helper class with some default information that the plugin used across the application.
    /// </summary>
    public sealed class DefaultsInfo
    {
        /// <summary>
        /// Gets the configuration route name
        /// </summary>
        public static string ConfigurationRouteName => "Plugin.Synchronizers.SAPCustomers.Configure";

        /// <summary>
        /// Gets the plugin system name.
        /// </summary>
        public static string SystemName => "Synchronizers.SAPCustomersSynchronizer";

        /// <summary>
        /// Gets the plugin task name.
        /// </summary>
        public static string TaskName => "SAP Customer Monitor";

        /// <summary>
        /// Gets the plugin task duration in seconds.
        /// </summary>
        public static int TaskDuration => 43200;

        /// <summary>
        /// Gets the plugin task type.
        /// </summary>
        public static string TaskType => "Nop.Plugin.Synchronizers.SAPCustomers.Services.CustomerSyncTask";

        /// <summary>
        /// Gets the default password for the new users from the SAP environment.
        /// </summary>
        public static string DefaultUserPassword => "123456";

        /// <summary>
        /// Gets the prefix used for knowing when a data is coming from SAP environment.
        /// </summary>
        public static string SapPrefix => "_SAP";

        /// <summary>
        /// Gets the id for the Registered role.
        /// </summary>
        public static int RegisteredRoleId => 3;
    }
}
