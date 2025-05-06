using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Payments.Application;
using TaxiService.Modules.Payments.Infrastructure.Configurations;

namespace TaxiService.Modules.Payments.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPaymentInfra(
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
            .AddHttpClients(moduleConfiguration)
            .AddDatabase(moduleConfiguration);

        return services;
    }
}
