using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passengers.Application;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPassangerInfra(
        this IServiceCollection services)
    {
        Assembly assembly = typeof(AssemblyReference).Assembly;

        services.AddAppServices()
            .AddHandlers(assembly)
            .AddCommonMapper(assembly)
            .AddFluentValidation(assembly);

        return services;
    }
}