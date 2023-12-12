namespace Nop.Plugin.Synchronizers.SAPOrders
{
    /// <summary>
    /// Contains default settings for this plug-in
    /// </summary>
    public static class SapOrdersSyncDefaults
    {
        /// <summary>
        /// Gets this plug-in's system name.
        /// </summary>
        public const string SystemName = "Synchronizers.SAPOrders";

        /// <summary>
        /// Gets the prefix of the locale resources for this plug-in.
        /// </summary>
        public const string LocaleResourcesPrefix = "Plugins.Synchronizers.SAPOrders";

        /// <summary>
        /// Gets the name of the Schedule task used to sync orders.
        /// </summary>
        public const string TaskName = "SAP Order Synchronizer";

        /// <summary>
        /// Gets the order-synching task's type.
        /// </summary>
        public const string TaskType = "Nop.Plugin.Synchronizers.SAPOrders.Tasks.SapOrdersSyncTask";

        /// <summary>
        /// Gets default task duration.
        /// </summary>
        public const int TaskDuration = 43000;

        /// <summary>
        /// Gets default Customer CardCode from SAP, used when a NopCommerce customer (not registered in SAP) makes an order.
        /// </summary>
        public const string NopCommerceCustomerCardCode = "CW000002";
    }
}
