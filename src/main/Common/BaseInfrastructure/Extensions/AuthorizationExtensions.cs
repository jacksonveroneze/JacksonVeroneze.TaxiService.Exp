using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Authentication;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AuthorizationExtensions
{
    private const string PassengerRead = "passenger:read";
    private const string PassengerWrite = "passenger:write";

    public static IServiceCollection AddAuthorization(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy(PassengerRead,
                policy => policy.Requirements.Add(
                    new HasScopeRequirement(PassengerRead, appConfiguration.Auth!.Issuer!)));

            options.AddPolicy(PassengerWrite,
                policy => policy.Requirements.Add(
                    new HasScopeRequirement(PassengerWrite, appConfiguration.Auth!.Issuer!)));
        });

        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        return services;
    }
}
