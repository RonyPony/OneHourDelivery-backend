using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.DdpApi.Areas.Admin.Models
{
    public class ConfigurationModel
    {
        [NopResourceDisplayName("Plugins.DdpApi.Admin.EnableApi")]
        public bool EnableApi { get; set; }
        public bool EnableApi_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.DdpApi.Admin.AllowRequestsFromSwagger")]
        public bool AllowRequestsFromSwagger { get; set; }
        public bool AllowRequestsFromSwagger_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.DdpApi.Admin.EnableLogging")]
        public bool EnableLogging { get; set; }
        public bool EnableLogging_OverrideForStore { get; set; }

        public int ActiveStoreScopeConfiguration { get; set; }
    }
}