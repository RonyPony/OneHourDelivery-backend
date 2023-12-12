using Nop.Plugin.Api.DTO.Manufacturers;

namespace Nop.Service.AppIPOSSync.Models
{
    /// <summary>
    /// Class used to make requests to Nop.Api manufacturer controller
    /// </summary>
    public class ManufacturerDelta
    {
        /// <summary>
        /// Manufacturer "Delta" object
        /// </summary>
        public ManufacturerDto manufacturer { get; set; }
    }
}
