using Nop.Plugin.Api.DTO.Products;

namespace Nop.Service.AppIPOSSync.Models
{
    /// <summary>
    /// Class used to make requests to Nop.Api product controller
    /// </summary>
    public class ProductDelta
    {
        /// <summary>
        /// Product "Delta" object
        /// </summary>
        public ProductDto product { get; set; }
    }
}
