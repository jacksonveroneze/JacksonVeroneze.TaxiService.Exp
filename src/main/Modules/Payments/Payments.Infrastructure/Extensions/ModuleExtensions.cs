using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Rides.Application;

namespace TaxiService.Modules.Payments.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPaymentInfra(
        this IServiceCollection services)
    {
        Assembly assembly = typeof(AssemblyReference).Assembly;

        services.AddServices()
            .AddHandlers(assembly)
            .AddMapper(assembly)
            .AddFluentValidation(assembly);

        return services;
    }
}