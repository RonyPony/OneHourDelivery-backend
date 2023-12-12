using Newtonsoft.Json;
using Nop.Plugin.DdpApi.DTO.Images;
using System;

namespace Nop.Plugin.DdpApi.DTO.ProductImages
{
    public class ProductPicturesRootObjectDto : ISerializableObject
    {
        public ProductPicturesRootObjectDto()
        {
            Image = new ImageMappingDto();
        }

        [JsonProperty("image")]
        public ImageMappingDto Image { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "image";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (ImageMappingDto);
        }
    }
}