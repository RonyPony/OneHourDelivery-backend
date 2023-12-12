using Nop.Plugin.Api.DTO.Categories;

namespace Nop.Service.AppIPOSSync.Models
{
    /// <summary>
    /// Class used to make requests to Nop.Api category controller
    /// </summary>
    public sealed class CategoryDelta
    {
        /// <summary>
        /// Category "Delta" object
        /// </summary>
        public CategoryDto category { get; set; }
    }
}
