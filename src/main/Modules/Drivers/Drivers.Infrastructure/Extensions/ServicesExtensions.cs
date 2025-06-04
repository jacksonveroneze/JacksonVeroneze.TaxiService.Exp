using Microsoft.Extensions.DependencyInjection;

using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Application.Common.Services;
using TaxiService.Modules.Drivers.Application.v1.Driver.PublicApi;
using TaxiService.Modules.Drivers.Domain.Repositories;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.Driver;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverLocation;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.Vehicle;
using TaxiService.Modules.Drivers.PublicApi.v1.Interfaces;

namespace TaxiService.Modules.Drivers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class ServicesExtensions
{
    internal static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        #region Driver

        // Services
        services.AddScoped<IDriverService, DriverService>();

        // Repositories
        services.AddScoped<IDriverReadRepository, DriverReadRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        
        // Services (Public API)
        services.AddTransient<IDriverPublicApi, DriverPublicApi>();

        #endregion

        #region Vehicle

        // Repositories
        services.AddScoped<IVehicleReadRepository, VehicleReadRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();

        #endregion

        #region DriverAvailability

        // Repositories
        services.AddScoped<IDriverAvailabilityHistoryReadRepository, DriverAvailabilityHistoryReadRepository>();
        services.AddScoped<IDriverAvailabilityHistoryRepository, DriverAvailabilityHistoryRepository>();

        #endregion

        #region DriverLocation

        // Repositories
        services.AddScoped<IDriverLocationHistoryReadRepository, DriverLocationHistoryReadRepository>();
        services.AddScoped<IDriverLocationHistoryRepository, DriverLocationHistoryRepository>();

        #endregion

        return services;
    }
}
