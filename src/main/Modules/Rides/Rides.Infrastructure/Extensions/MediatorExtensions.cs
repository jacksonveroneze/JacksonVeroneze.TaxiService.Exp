using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtensions
{
    public static IServiceCollection AddMediatr(
        this IServiceCollection services)
    {
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssemblyContaining<AssemblyReference>());

        return services;
    }
}