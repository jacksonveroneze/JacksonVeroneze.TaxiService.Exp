using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Common.SharedKernel.Extensions;
using TaxiService.Modules.Ride.Application;
using TaxiService.Modules.Ride.Infrastructure.Configurations;

namespace TaxiService.Modules.Ride.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddRideInfra(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        Assembly applicationAssembly = AssemblyReference.Assembly;
        Assembly infraAssembly = typeof(ModuleExtensions).Assembly;

        services.AddConfiguration<ModuleConfiguration>(
            configuration, ModuleConfiguration.Name);

        ModuleConfiguration moduleConfiguration = services
            .BuildServiceProvider()
            .GetRequiredService<ModuleConfiguration>();

        services.AddServices()
            .AddHandlers(applicationAssembly)
            .AddHandlers(infraAssembly)
            .AddMapper(applicationAssembly)
            .AddFluentValidation(applicationAssembly)
            .AddDatabase(moduleConfiguration);

        return services;
    }
}
