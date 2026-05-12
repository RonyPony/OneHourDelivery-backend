using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Requirements;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Authorization.Policies
{
    public class CustomerRoleAuthorizationPolicy : AuthorizationHandler<CustomerRoleRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerRoleAuthorizationPolicy(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomerRoleRequirement requirement)
        {
            if (requirement.IsCustomerInRole())
            {
                context.Succeed(requirement);
            }
            else
            {
                var message = Encoding.UTF8.GetBytes("User authenticated but not in Api Role.");
                this._httpContextAccessor.HttpContext.Response.Body.WriteAsync(message);
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
