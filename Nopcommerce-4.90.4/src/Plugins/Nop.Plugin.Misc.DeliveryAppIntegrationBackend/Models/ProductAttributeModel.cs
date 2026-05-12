using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents product attribute for model the data.
    /// </summary>
    public sealed class ProductAttributeModel
    {
        /// <summary>
        /// Get or set product attribute id.
        /// </summary>
        [JsonProperty("productAttributeId")]
        public int ProductAttributeId { get; set; }

        /// <summary>
        /// Get or set product attribute name.
        /// </summary>
        [JsonProperty("productAttributeName")]
        public string ProductAttributeName { get; set; }

        /// <summary>
        /// Get or set control type id.
        /// </summary>
        [JsonProperty("controlTypeId")]
        public int ControlTypeId { get; set; }

        /// <summary>
        /// Get or set a list of product attribute value model.
        /// </summary>
        [JsonProperty("productAttributeValueModels")]
        public IList<ProductAttributeValueModel> ProductAttributeValueModels { get; set; }
    }
}
