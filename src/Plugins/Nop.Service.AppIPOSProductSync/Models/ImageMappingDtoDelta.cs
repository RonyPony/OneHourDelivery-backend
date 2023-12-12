using Nop.Plugin.Api.DTO.Images;

namespace Nop.Service.AppIPOSSync.Models
{
    /// <summary>
    /// Class used to make requests to Nop.Api image mapping controller
    /// </summary>
    public class ImageMappingDtoDelta
    {
        /// <summary>
        /// Image "Delta" object
        /// </summary>
        public ImageMappingDto image { get; set; }
    }
}
