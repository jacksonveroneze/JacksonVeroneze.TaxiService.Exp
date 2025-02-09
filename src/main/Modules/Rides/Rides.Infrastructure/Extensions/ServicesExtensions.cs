using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Position;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Services;
using TaxiService.Modules.Rides.Infrastructure.Repositories.Position;
using TaxiService.Modules.Rides.Infrastructure.Repositories.Ride;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        #region Ride

        // Repositories
        services.AddScoped<IRideReadRepository, RideReadRepository>();
        services.AddScoped<IRideWriteRepository, RideWriteRepository>();

        #endregion

        #region Position

        // Repositories
        services.AddScoped<IPositionReadRepository, PositionReadRepository>();
        services.AddScoped<IPositionWriteRepository, PositionWriteRepository>();

        #endregion

        services.AddScoped<IDistanceCalculatorService, DistanceCalculatorService>();
        
        return services;
    }
}