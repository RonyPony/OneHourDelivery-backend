using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Infrastructure.Mapper
{
    /// <summary>
    /// Represents an AutoMapper configuration for plugin models.
    /// </summary>
    public class MapperConfiguration : Profile, IOrderedMapperProfile
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="MapperConfiguration"/>.
        /// </summary>
        public MapperConfiguration()
        {
            CreateMap<PluginConfigurationSettings, PluginConfiguration>();
            CreateMap<PluginConfiguration, PluginConfigurationSettings>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Order of this mapper implementation.
        /// </summary>
        public int Order => int.MaxValue;

        #endregion
    }
}
