using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseApplication;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MapperExtensions
{
    public static IServiceCollection AddCommonMapper(
        this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AssemblyReference));

        return services;
    }

    public static IServiceCollection AddMapper(
        this IServiceCollection services, Assembly assembly)
    {
        services.AddAutoMapper(assembly);

        return services;
    }
}
