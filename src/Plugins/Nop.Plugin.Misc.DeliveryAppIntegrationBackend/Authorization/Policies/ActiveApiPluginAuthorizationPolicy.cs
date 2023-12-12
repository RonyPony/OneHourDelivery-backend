namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Policies
{
    using Microsoft.AspNetCore.Authorization;
    using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Authorization.Requirements;
    using System.Threading.Tasks;

    public class ActiveApiPluginAuthorizationPolicy : AuthorizationHandler<ActiveApiPluginRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveApiPluginRequirement requirement)
        {
            if (requirement.IsActive())
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}