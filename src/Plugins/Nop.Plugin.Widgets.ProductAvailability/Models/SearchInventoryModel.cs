using Nop.Plugin.Widgets.ProductAvailability.Models.Inventory;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.ProductAvailability.Models
{
    /// <summary>
    /// Represents a model for inventory search request.
    /// </summary>
    public sealed class SearchInventoryModel : BaseNopModel
    {
        /// <summary>
        /// Indicates the sku of the product to search.
        /// </summary>
        public string ProductSku { get; set; }

        /// <summary>
        /// Indicates the response of the request.
        /// </summary>
        public InventoryRequestResponseModel RequestResponseModel { get; set; }
    }
}
