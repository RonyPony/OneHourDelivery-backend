using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Nop.Plugin.DdpApi.Authorization.Requirements;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.DdpApi.Authorization.Policies
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
                var message = Encoding.UTF8.GetBytes("User not authenticated or does not have required roles.");
                this._httpContextAccessor.HttpContext.Response.Body.WriteAsync(message);
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
