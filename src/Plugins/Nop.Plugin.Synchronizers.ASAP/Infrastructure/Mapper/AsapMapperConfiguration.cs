using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Synchronizers.ASAP.Domains;
using Nop.Plugin.Synchronizers.ASAP.Models;

namespace Nop.Plugin.Synchronizers.ASAP.Infrastructure.Mapper
{
    /// <summary>
    /// Represents a mapper configuration implementationfor this plugin.
    /// </summary>
    public sealed class AsapMapperConfiguration : Profile, IOrderedMapperProfile
    {
        #region Fields

        /// <summary>
        /// Gets the order number for this mapper configuration.
        /// </summary>
        public int Order => int.MaxValue;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="AsapMapperConfiguration"/>.
        /// </summary>
        public AsapMapperConfiguration()
        {
            CreateMap<DeliveryAsapConfigurationModel, AsapDeliveryConfigurationSettings>();
            CreateMap<AsapDeliveryConfigurationSettings, DeliveryAsapConfigurationModel>()
                .ForMember(dest => dest.Warehouses, opt => opt.Ignore());
        }

        #endregion
    }
}
