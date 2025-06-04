using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Services;
using TaxiService.Modules.Rides.Application.Common.Services;
using TaxiService.Modules.Rides.Domain.Repositories;
using TaxiService.Modules.Rides.Domain.Services;
using TaxiService.Modules.Rides.Infrastructure.Repositories.Position;
using TaxiService.Modules.Rides.Infrastructure.Repositories.Ride;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

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

        #endregion

        #region Position

        // Repositories
        services.AddScoped<IPositionReadRepository, PositionReadRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();

        #endregion

        #region Services

        services.AddScoped<IDistanceCalculatorService, DistanceCalculatorService>();

        #endregion

        return services;
    }
}
