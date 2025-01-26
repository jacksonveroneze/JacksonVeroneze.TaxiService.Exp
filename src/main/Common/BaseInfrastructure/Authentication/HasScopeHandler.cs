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

        if (!context.User.HasClaim(
                claim => claim.Type == "scope"
                         && claim.Issuer == requirement.Issuer))
        {
            return Task.CompletedTask;
        }

        string[] scopes = context.User.FindFirst(
                c => c.Type == "scope" && c.Issuer == requirement.Issuer)
            ?.Value.Split(' ')!;

        if (scopes.Any(s => s.Equals(requirement.Scope,
                StringComparison.OrdinalIgnoreCase)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}