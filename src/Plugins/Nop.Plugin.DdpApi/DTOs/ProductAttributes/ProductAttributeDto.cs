using FluentValidation.Attributes;
using Newtonsoft.Json;
using Nop.Plugin.DdpApi.DTO.Base;
using Nop.Plugin.DdpApi.Validators;

namespace Nop.Plugin.DdpApi.DTO.ProductAttributes
{
    [JsonObject(Title = "product_attribute")]
    [Validator(typeof(ProductAttributeDtoValidator))]
    public class ProductAttributeDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}