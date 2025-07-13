using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Ride.Application.Common.Interfaces;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Services;
using TaxiService.Modules.Ride.Application.Common.Services;
using TaxiService.Modules.Ride.Domain.Repositories;
using TaxiService.Modules.Ride.Domain.Services;
using TaxiService.Modules.Ride.Infrastructure.Repositories.Position;
using TaxiService.Modules.Ride.Infrastructure.Repositories.Ride;

namespace TaxiService.Modules.Ride.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class ServicesExtensions
{
    internal static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        #region Ride

        // Repositories
        services.AddScoped<IRideReadRepository, RideReadRepository>();
        services.AddScoped<IRideRepository, RideRepository>();
        services.AddScoped<IRideDistributedLockRepository, RideDistributedLockRepository>();

        services.AddScoped<IRideExpandsService, RideExpandsService>();
        services.AddScoped<IRideExpandsStrategy, RideExpandsDriverDriver>();
        services.AddScoped<IRideExpandsStrategy, RideExpandsPassengerStrategy>();

        services.AddScoped<IRideMatchDriversService, RideMatchDriversService>();
        
        services.AddScoped<IMetricsPublisher, PrometheusMetricsPublisher>();

        #endregion

        #region Position

        // Repositories
        services.AddScoped<IPositionReadRepository, PositionReadRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();

        #endregion

        #region Services

        services.AddScoped<IDistanceCalculatorService, DistanceCalculatorService>();
        services.AddScoped<IRideStatusTransitionValidator, RideStatusTransitionValidator>();

        #endregion

        return services;
    }
}
