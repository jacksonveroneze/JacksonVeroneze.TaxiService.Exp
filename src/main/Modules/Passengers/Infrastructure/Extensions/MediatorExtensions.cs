using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application.Behaviors;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtensions
{
    public static IServiceCollection AddMediatr(
        this IServiceCollection services)
    {
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssemblyContaining<global::TaxiService.Modules.Passengers.Application.AssemblyReference>());

        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}