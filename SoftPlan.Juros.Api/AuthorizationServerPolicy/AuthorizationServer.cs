using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Softplan.Api.AuthorizationServerPolicy
{

    public class AuthorizationServer : AuthorizationHandler<AuthorizationServer>, IAuthorizationRequirement
    {
        private readonly string Scope = "scope.juros";
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationServer requirement)
        {
            
            if (!context.User.HasClaim("scope", Scope))
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
