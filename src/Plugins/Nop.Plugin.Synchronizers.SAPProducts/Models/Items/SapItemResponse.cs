using Nop.Plugin.Synchronizers.SAPProducts.Models.Items;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPProducts.Models
{
    /// <summary>
    /// Represents the response from SAP API for the item's request.
    /// </summary>
    public sealed class SapItemResponse : SapBaseResponse
    {
        /// <summary>
        /// An instance of <see cref="List{T}"/> where T is an instance of <see cref="SapItemModel"/>.
        /// </summary>
        public List<SapItemModel> Extra { get; set; }
    }
}
