using Newtonsoft.Json;

namespace Nop.Plugin.DdpApi.DTO.Products
{
    public class ProductsCountRootObject
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}