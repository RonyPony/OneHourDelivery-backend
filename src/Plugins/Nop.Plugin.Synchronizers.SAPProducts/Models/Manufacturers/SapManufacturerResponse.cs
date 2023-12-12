using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPProducts.Models.Manufacturers
{
    /// <summary>
    /// Represents the response from SAP API for the manufacturers request.
    /// </summary>
    public sealed class SapManufacturerResponse : SapBaseResponse
    {
        /// <summary>
        /// An instance of <see cref="List{T}"/> where T is an instance of <see cref="SapManufacturerModel"/>.
        /// </summary>
        public List<SapManufacturerModel> Extra { get; set; }
    }
}
