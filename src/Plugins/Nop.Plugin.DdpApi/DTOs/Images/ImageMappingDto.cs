using Newtonsoft.Json;
using Nop.Plugin.DdpApi.Attributes;

namespace Nop.Plugin.DdpApi.DTO.Images
{
    [ImageValidation]
    [JsonObject(Title = "image")]
    public class ImageMappingDto : ImageDto
    {
        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("picture_id")]
        public int PictureId { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }
    }
}