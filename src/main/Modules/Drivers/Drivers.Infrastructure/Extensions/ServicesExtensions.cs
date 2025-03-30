using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverLocation;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Vehicle;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Application.Common.Services;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.Driver;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverLocation;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.Vehicle;

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
        services.AddScoped<IDriverReadCacheRepository, DriverReadCacheRepository>();

        services.AddScoped<IDriverRepository, DriverRepository>();

        #endregion

        #region Vehicle

        // Repositories
        services.AddScoped<IVehicleReadRepository, VehicleReadRepository>();
        services.AddScoped<IVehicleWriteRepository, VehicleWriteRepository>();

        #endregion

        #region DriverAvailability

        // Repositories
        services.AddScoped<IDriverAvailabilityReadRepository, DriverAvailabilityReadRepository>();
        services.AddScoped<IDriverAvailabilityWriteRepository, DriverAvailabilityWriteRepository>();

        #endregion

        #region DriverLocation

        // Repositories
        services.AddScoped<IDriverLocationReadRepository, DriverLocationReadRepository>();
        services.AddScoped<IDriverLocationWriteRepository, DriverLocationWriteRepository>();

        #endregion

        return services;
    }
}