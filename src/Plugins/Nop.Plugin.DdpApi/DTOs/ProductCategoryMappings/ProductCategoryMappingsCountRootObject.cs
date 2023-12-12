using Newtonsoft.Json;

namespace Nop.Plugin.DdpApi.DTO.ProductCategoryMappings
{
    public class ProductCategoryMappingsCountRootObject
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}