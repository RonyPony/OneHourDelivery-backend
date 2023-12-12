using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Nop.Plugin.DdpApi.Authorization.Requirements;
using System.Threading.Tasks;

namespace Nop.Plugin.DdpApi.Authorization.Policies
{
    public class ValidSchemeAuthorizationPolicy : AuthorizationHandler<AuthorizationSchemeRequirement>
    {
        IHttpContextAccessor _httpContextAccessor = null;
        public ValidSchemeAuthorizationPolicy(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationSchemeRequirement requirement)
        {
            if (requirement.IsValid(_httpContextAccessor?.HttpContext.Request.Headers))
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
