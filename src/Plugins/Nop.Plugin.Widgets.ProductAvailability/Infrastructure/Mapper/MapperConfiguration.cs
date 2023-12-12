using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Widgets.ProductAvailability.Models;

namespace Nop.Plugin.Widgets.ProductAvailability.Infrastructure.Mapper
{
    /// <summary>
    /// Represents an AutoMapper configuration for plugin models.
    /// </summary>
    public sealed class MapperConfiguration : Profile, IOrderedMapperProfile
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MapperConfiguration"/>.
        /// </summary>
        public MapperConfiguration()
        {
            CreateMap<ConfigurationModel, ProductAvailabilitySettings>();
            CreateMap<ProductAvailabilitySettings, ConfigurationModel>()
                .ForMember(model => model.Warehouses, opt => opt.Ignore())
                .ForMember(model => model.ProductAttributes, opt => opt.Ignore());
        }

        /// <summary>
        /// Retrieves the order of this mapper implementation.
        /// </summary>
        public int Order => int.MaxValue;
    }
}
