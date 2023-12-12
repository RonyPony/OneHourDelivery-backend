using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Widgets.Zoom.Domains;
using Nop.Plugin.Widgets.Zoom.Models;

namespace Nop.Plugin.Widgets.Zoom.Infrastructure.Mapper
{
    /// <summary>
    /// Represents Automapper profile configuration for this plugin.
    /// </summary>
    public sealed class MapperProfile : Profile, IOrderedMapperProfile
    {
        ///<inheritdoc/>
        public int Order => int.MaxValue;

        /// <summary>
        /// Initialize a new instance of <see cref="MapperProfile"/>.
        /// </summary>
        public MapperProfile()
        {
            CreateMap<ZoomPluginSettings, ZoomPluginConfigurationModel>();
            CreateMap<ZoomPluginConfigurationModel, ZoomPluginSettings>();
        }
    }
}
