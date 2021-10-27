using Fitweb.Application.Constants;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fitweb.API.Authorization
{
    public class AthleteOrAdministratorRequirement : IAuthorizationRequirement
    {
    }

    public class AthleteOrAdministratorRequirementHandler : AuthorizationHandler<AthleteOrAdministratorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AthleteOrAdministratorRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type is ClaimTypes.Role && (c.Value is Roles.Administrator || c.Value is Roles.Athlete)))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }

}
