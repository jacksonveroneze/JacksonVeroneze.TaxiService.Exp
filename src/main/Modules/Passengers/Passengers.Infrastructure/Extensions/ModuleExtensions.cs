using Microsoft.Extensions.DependencyInjection;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPassangerInfra(
        this IServiceCollection services)
    {
        services.AddAppServices()
            .AddFluentValidation();

        return services;
    }
}