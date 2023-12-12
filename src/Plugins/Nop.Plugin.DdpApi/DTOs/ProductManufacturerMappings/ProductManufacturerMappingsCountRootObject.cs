using Newtonsoft.Json;

namespace Nop.Plugin.DdpApi.DTO.ProductManufacturerMappings
{
    public class ProductManufacturerMappingsCountRootObject
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}