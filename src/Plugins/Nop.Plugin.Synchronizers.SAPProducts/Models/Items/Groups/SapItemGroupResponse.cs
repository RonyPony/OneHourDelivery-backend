using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPProducts.Models.Items.Groups
{
    /// <summary>
    /// Represents the response from SAP API for the item's groups request.
    /// </summary>
    public sealed class SapItemGroupResponse : SapBaseResponse
    {
        /// <summary>
        /// An instance of <see cref="List{T}"/> where T is an instance of <see cref="SapItemGroupModel"/>.
        /// </summary>
        public List<SapItemGroupModel> Extra { get; set; }
    }
}
