using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents product attribute value to model data.
    /// </summary>
    public sealed class ProductAttributeValueModel
    {
        /// <summary>
        /// Get or set product attribute id.
        /// </summary>
        [JsonProperty("productAttributeValueId")]
        public int ProductAttributeValueId { get; set; }

        /// <summary>
        /// Get or set product attribute name.
        /// </summary>
        [JsonProperty("productAttributeName")]
        public string ProductAttributeName { get; set; }

        /// <summary>
        /// Get or set product attribute price adjustment
        /// </summary>
        [JsonProperty("priceAdjustment")]
        public decimal PriceAdjustment { get; set; }

        /// <summary>
        /// Indicates if the PriceAdjustment is percentage based. 
        /// </summary>
        [JsonProperty("priceAdjustmentUsePercentage")]
        public bool PriceAdjustmentUsePercentage { get; set; }

        /// <summary>
        /// Get or set product attribute additional cost.
        /// </summary>
        [JsonProperty("additionalCost")]
        public decimal AdditionalCost { get; set; }

        /// <summary>
        /// Get or ser product attribute preselected state.
        /// </summary>
        [JsonProperty("isPreselected")]
        public bool IsPreselected { get; set; }
    }
}
