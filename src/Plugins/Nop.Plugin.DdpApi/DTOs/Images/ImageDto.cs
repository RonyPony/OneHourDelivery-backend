using Newtonsoft.Json;
using Nop.Plugin.DdpApi.Attributes;
using Nop.Plugin.DdpApi.DTO.Base;

namespace Nop.Plugin.DdpApi.DTO.Images
{
    [ImageValidation]
    public class ImageDto : BaseDto
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("attachment")]
        public string Attachment { get; set; }

        [JsonIgnore]
        public byte[] Binary { get; set; }

        [JsonIgnore]
        public string MimeType { get; set; }

        [JsonProperty("seoFilename")]
        public string SeoFilename { get; set; }
    }
}