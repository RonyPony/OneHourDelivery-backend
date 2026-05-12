using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a vendor product.
    /// </summary>
    public sealed class DeliveryAppProduct
    {
        /// <summary>
        /// Indicates the product Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Indicates the product Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Indicates the Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Indicates the Price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Indicates the ImageUrl
        /// </summary>
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Indicates the related Products
        /// </summary>
        [JsonProperty("relatedProducts")]
        public IEnumerable<DeliveryAppRelatedProduct> RelatedProducts { get; set; }

        /// <summary>
        /// Indicates when the product has discounts
        /// </summary>
        [JsonProperty("hasPromotion")]
        public bool HasPromotion { get; set; }

        /// <summary>
        /// Indicates the product discounts specification
        /// </summary>
        [JsonProperty("discountSpecification")]
        public IEnumerable<string> DiscountSpecification { get; set; }

        /// <summary>
        /// Indicates if the product has availability
        /// </summary>
        [JsonProperty("isAvailable")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Indicate the attributes of product
        /// </summary>
        [JsonProperty("productAttributes")]
        public IList<ProductAttributeModel> ProductAttributes { get; set; }

    }
}
