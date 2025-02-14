using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.Driver;

namespace TaxiService.Modules.Drivers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class ServicesExtensions
{
    internal static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        #region Driver

        // Repositories
        services.AddScoped<IDriverReadRepository, DriverReadRepository>();
        services.AddScoped<IDriverWriteRepository, DriverWriteRepository>();

        #endregion

        return services;
    }
}