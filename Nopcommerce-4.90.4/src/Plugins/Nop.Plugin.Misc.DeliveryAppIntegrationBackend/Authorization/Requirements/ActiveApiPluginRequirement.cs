namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Authorization.Requirements
{
    using Microsoft.AspNetCore.Authorization;
    using Nop.Core.Infrastructure;
    using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domain;

    public class ActiveApiPluginRequirement : IAuthorizationRequirement
    {
        public bool IsActive()
        {
            var settings = EngineContext.Current.Resolve<DeliveryAppBackendConfigurationSettings>();

            if (settings.EnableApi)
            {
                return true;
            }

            return false;
        }
    }
}