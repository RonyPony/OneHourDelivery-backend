
using Nop.Web.Framework.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Change for Represents the information neeeded to get the list of products by vendor.
    /// </summary>
    public class ProductModel : BaseNopEntityModel
    {

        /// <summary>
        /// Product Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product Category Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Product Category Name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Product Picture Url.
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        public decimal Price { get; set; }
    }
}