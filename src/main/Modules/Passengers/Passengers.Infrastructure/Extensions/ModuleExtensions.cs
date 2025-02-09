using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passengers.Application;
using TaxiService.Modules.Passengers.Application.v1.Interfaces;

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
            .AddFluentValidation(assembly);
        
        services.AddServices()
            .AddDatabase(appConfiguration);

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        return services;
    }
}