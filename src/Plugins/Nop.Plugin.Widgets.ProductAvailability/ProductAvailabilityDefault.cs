namespace Nop.Plugin.Widgets.ProductAvailability
{
    /// <summary>
    /// Misc.ProductAvailability Constants
    /// </summary>
    public static class ProductAvailabilityDefault
    {
        /// <summary>
        /// ProductAvailability plug-in system name
        /// </summary>
        public static string SystemName => "Widgets.ProductAvailability";

        /// <summary>
        /// ProductAvailibitiy RouteName     
        /// </summary>
        public static string ConfigurationRouteName => "Plugin.Widgets.ProductAvailibitiy.Configure";

        /// <summary>
        /// ProductAvailability Product Template Name
        /// </summary>
        public static string ProductTemplateName => "Product Availability Template";

        /// <summary>
        /// ProductAvailability LocalResourcesPrefix       
        /// </summary>
        public static string LocaleResourcesPrefix => "Plugins.Widgets.ProductAvailability";

        /// <summary>
        /// Gets the inventory search topic page search engine name.
        /// </summary>
        public static string InventorySearchTopicPageSeName => "inventory-search";

        /// <summary>
        /// Gets the inventory search topic page system name.
        /// </summary>
        public static string InventorySearchTopicPageSystemName => "ProductAvailability.InventorySearch";

        /// <summary>
        /// Gets the out put directory for this plugin.
        /// </summary>
        public static string OutputDir => "Plugins/Widgets.ProductAvailability";

        /// <summary>
        /// Gets the name of the inventory sync schedule task.
        /// </summary>
        public static string ScheduleTaskName => "DDP Inventory Sync Task";

        /// <summary>
        /// Gets the type of the inventory sync schedule task.
        /// </summary>
        public static string ScheduleTaskType => "Nop.Plugin.Widgets.ProductAvailability.Tasks.DdpInventorySyncTask";

        /// <summary>
        /// Gets the seconds in a 12 hour period.
        /// </summary>
        public static int SecondsIn12Hours => 43200;

        /// <summary>
        /// Gets the XML format for inserting product attributes combinations.
        /// </summary>
        public static string DefaultProductAttributeXmlFormat => "<ProductAttribute ID=\"{0}\"><ProductAttributeValue><Value>{1}</Value></ProductAttributeValue></ProductAttribute>";

        /// <summary>
        /// Gets the store number of the default warehouse when isn't configure.
        /// </summary>
        public static string DefaultStoreNumberIfNoneConfigured => "33.0";

        /// <summary>
        /// Gets a template for the admin comment of the warehouses for warehouse sync. 
        /// </summary>
        public static string WarehouseAdminCommentTemplate => "No. bodega: {0}, No. Tienda: {1}.";

        /// <summary>
        /// Gets the default code that identifies the one sized products.
        /// </summary>
        public static string DefaultOneSizeProductsIdentifierCode => "ONE";
    }
}
