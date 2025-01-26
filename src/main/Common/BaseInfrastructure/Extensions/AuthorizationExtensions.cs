using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Authentication;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

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