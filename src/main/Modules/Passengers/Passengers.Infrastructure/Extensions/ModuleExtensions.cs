using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passengers.Application;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPassengerInfra(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        Assembly assembly = typeof(AssemblyReference).Assembly;

        services.AddServices()
            .AddHandlers(assembly)
            .AddMapper(assembly)
            .AddFluentValidation(assembly)
            .AddDatabase(appConfiguration);

        return services;
    }
}