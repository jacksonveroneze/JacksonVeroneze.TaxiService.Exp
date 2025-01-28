using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseApplication.Behaviors;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtensions
{
    public static IServiceCollection AddBehavior(
        this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

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