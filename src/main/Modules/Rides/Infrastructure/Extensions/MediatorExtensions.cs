using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Rides.Application.Behaviors;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtensions
{
    public static IServiceCollection AddMediatr(
        this IServiceCollection services)
    {
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssemblyContaining<global::TaxiService.Modules.Rides.Application.AssemblyReference>());

        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}