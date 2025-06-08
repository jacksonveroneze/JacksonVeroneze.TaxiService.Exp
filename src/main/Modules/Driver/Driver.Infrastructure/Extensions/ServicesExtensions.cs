using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Services;
using TaxiService.Modules.Driver.Application.Common.Services;
using TaxiService.Modules.Driver.Application.v1.Driver.PublicApi;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.PublicApi;
using TaxiService.Modules.Driver.Application.v1.Vehicle.PublicApi;
using TaxiService.Modules.Driver.Domain.Repositories;
using TaxiService.Modules.Driver.Infrastructure.Repositories.Driver;
using TaxiService.Modules.Driver.Infrastructure.Repositories.DriverAvailability;
using TaxiService.Modules.Driver.Infrastructure.Repositories.DriverLocation;
using TaxiService.Modules.Driver.Infrastructure.Repositories.Vehicle;
using TaxiService.Modules.Driver.PublicApi.v1.Interfaces;

namespace TaxiService.Modules.Driver.Infrastructure.Extensions;

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

        // Services (Public API)
        services.AddScoped<IVehiclePublicApi, VehiclePublicApi>();

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

        // Services (Public API)
        services.AddScoped<IDriverLocationPublicApi, DriverLocationPublicApi>();

        #endregion

        return services;
    }
}
