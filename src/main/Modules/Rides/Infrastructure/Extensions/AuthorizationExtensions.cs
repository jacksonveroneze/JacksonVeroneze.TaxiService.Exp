using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Rides.Infrastructure.Authentication;
using TaxiService.Modules.Rides.Infrastructure.Configurations;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AuthorizationExtensions
{
    public const string UserRead = "user:read";
    public const string UserWrite = "user:write";

    public static IServiceCollection AddAuthorization(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy(UserRead,
                policy => policy.Requirements.Add(
                    new HasScopeRequirement(UserRead, appConfiguration.Auth!.Issuer!)));

            options.AddPolicy(UserWrite,
                policy => policy.Requirements.Add(
                    new HasScopeRequirement(UserWrite, appConfiguration.Auth!.Issuer!)));
        });

        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        return services;
    }
}