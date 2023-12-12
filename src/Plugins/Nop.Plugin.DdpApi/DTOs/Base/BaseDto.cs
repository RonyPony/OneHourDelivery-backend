using Newtonsoft.Json;

namespace Nop.Plugin.DdpApi.DTO.Base
{
    public abstract class BaseDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}