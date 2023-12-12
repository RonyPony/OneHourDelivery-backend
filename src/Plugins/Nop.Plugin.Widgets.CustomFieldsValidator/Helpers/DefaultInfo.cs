namespace Nop.Plugin.Widgets.CustomFieldsValidator.Helpers
{
    /// <summary>
    /// Represents the class used for setting the default information used by this plugin
    /// </summary>
    public sealed class DefaultInfo
    {
        /// <summary>
        /// Gets a name of the view component to display payment info in public store
        /// </summary>
        public const string CustomFieldsValidatorViewComponentName = "CustomFieldsValidatorViewComponent";

        /// <summary>
        /// Gets the plugin system name
        /// </summary>
        public static string SystemName => "Widgets.CustomFieldsValidator";

        /// <summary>
        /// Gets the field name used for the NIT
        /// </summary>
        public static string NitFieldName  => "NIT";

        /// <summary>
        /// Gets the field name used for the NCR
        /// </summary>
        public static string NcrFieldName => "NCR";
    }
}
