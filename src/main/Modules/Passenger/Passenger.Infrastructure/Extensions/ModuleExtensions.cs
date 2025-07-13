using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Common.SharedKernel.Extensions;
using TaxiService.Modules.Passenger.Application;
using TaxiService.Modules.Passenger.Infrastructure.Configurations;

namespace TaxiService.Modules.Passenger.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPassengerInfra(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        Assembly assembly = AssemblyReference.Assembly;

        services.AddConfiguration<ModuleConfiguration>(
            configuration, ModuleConfiguration.Name);

        ModuleConfiguration moduleConfiguration = services
            .BuildServiceProvider()
            .GetRequiredService<ModuleConfiguration>();

        services.AddServices()
            .AddHandlers(assembly)
            .AddMapper(assembly)
            .AddFluentValidation(assembly)
            .AddDatabase(moduleConfiguration);

        return services;
    }
}
