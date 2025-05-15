using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtensions
{
    public static IServiceCollection AddBehavior(
        this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddHandlers(
        this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
