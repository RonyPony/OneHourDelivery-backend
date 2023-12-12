using Newtonsoft.Json;
using Nop.Plugin.DdpApi.DTO.Base;

namespace Nop.Plugin.DdpApi.DTO
{
    [JsonObject(Title = "attribute")]
    public class ProductItemAttributeDto : BaseDto
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
