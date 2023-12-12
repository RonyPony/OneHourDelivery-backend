namespace Nop.Plugin.Synchronizers.SAPProducts.Models.Items.Groups
{
    /// <summary>
    /// Represents the item's groups included in the response from the SAP API.
    /// </summary>
    public sealed class SapItemGroupModel
    {
        /// <summary>
        /// Indicates the id number of the item group.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Indicates the name of the item group.
        /// </summary>
        public string GroupName { get; set; }
    }
}
