using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.Email;
using TaxiService.Modules.Drivers.Infrastructure.Repositories.User;

namespace TaxiService.Modules.Drivers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        #region User

        // Repositories
        services.AddScoped<IDriverReadRepository, DriverReadRepository>();
        services.AddScoped<IDriverWriteRepository, DriverWriteRepository>();

        #endregion

        #region Email

        // Repositories
        services.AddScoped<IEmailReadRepository, EmailReadRepository>();
        services.AddScoped<IEmailWriteRepository, EmailWriteRepository>();

        #endregion

        return services;
    }
}