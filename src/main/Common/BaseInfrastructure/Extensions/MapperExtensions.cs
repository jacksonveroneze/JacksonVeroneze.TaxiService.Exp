using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class PassengersMapperExtensions
{
    public static IServiceCollection AddCommonMapper(
        this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BaseApplication.AssemblyReference));

        return services;
    }

    public static IServiceCollection AddMapper(
        this IServiceCollection services, Assembly assembly)
    {
        services.AddAutoMapper(assembly);

        return services;
    }
}