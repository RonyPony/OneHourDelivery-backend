using Newtonsoft.Json;

namespace Nop.Plugin.DdpApi.DTO.ProductAttributes
{
    public class ProductAttributesCountRootObject
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}