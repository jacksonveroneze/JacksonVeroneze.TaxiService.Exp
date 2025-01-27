using Microsoft.Extensions.DependencyInjection;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddRideInfra(
        this IServiceCollection services)
    {
        services.AddAppServices()
            .AddFluentValidation();

        return services;
    }
}