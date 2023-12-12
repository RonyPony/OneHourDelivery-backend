using Nop.Plugin.DdpApi.Areas.Admin.Models;
using Nop.Plugin.DdpApi.AutoMapper;
using Nop.Plugin.DdpApi.Domain;
using Nop.Plugin.DdpApi.Models;

namespace Nop.Plugin.DdpApi.MappingExtensions
{
    public static class ConfigurationMappings
    {
        public static ConfigurationModel ToModel(this ApiSettings apiSettings)
        {
            return apiSettings.MapTo<ApiSettings, ConfigurationModel>();
        }

        public static ApiSettings ToEntity(this ConfigurationModel apiSettingsModel)
        {
            return apiSettingsModel.MapTo<ConfigurationModel, ApiSettings>();
        }
    }
}