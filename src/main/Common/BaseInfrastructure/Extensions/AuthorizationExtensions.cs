using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Authentication;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AuthorizationExtensions
{
    private const string PassangerRead = "passanger:read";
    private const string PassangerWrite = "passanger:write";

    public static IServiceCollection AddAuthorization(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy(PassangerRead,
                policy => policy.Requirements.Add(
                    new HasScopeRequirement(PassangerRead, appConfiguration.Auth!.Issuer!)));

            options.AddPolicy(PassangerWrite,
                policy => policy.Requirements.Add(
                    new HasScopeRequirement(PassangerWrite, appConfiguration.Auth!.Issuer!)));
        });

        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        return services;
    }
}