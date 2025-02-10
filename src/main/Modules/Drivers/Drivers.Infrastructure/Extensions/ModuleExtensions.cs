using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Drivers.Application;
using TaxiService.Modules.Drivers.Infrastructure.Configurations;

namespace TaxiService.Modules.Drivers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddDriverInfra(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        Assembly assembly = typeof(AssemblyReference).Assembly;

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
