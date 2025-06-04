using Microsoft.AspNetCore.Authorization;

namespace TaxiService.Common.BaseInfrastructure.Authentication;

public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        HasScopeRequirement requirement)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(requirement);

        if (!context.User.HasClaim(claim => string.Equals(claim.Type, "scope"
                , StringComparison.OrdinalIgnoreCase) && string.Equals(claim.Issuer,
                requirement.Issuer, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.CompletedTask;
        }

        string[] scopes = context.User.FindFirst(c =>
                string.Equals(c.Type, "scope", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(c.Issuer, requirement.Issuer,
                    StringComparison.OrdinalIgnoreCase))
            ?.Value.Split(' ')!;

        if (scopes.Any(s => s.Equals(requirement.Scope,
                StringComparison.OrdinalIgnoreCase)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
