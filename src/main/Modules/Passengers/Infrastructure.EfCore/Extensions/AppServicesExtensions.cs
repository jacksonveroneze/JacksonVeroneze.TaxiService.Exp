using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Email;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.User;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddEfCoreServices(
        this IServiceCollection services)
    {
        #region User

        // Repositories
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        #endregion

        #region Email

        // Repositories
        services.AddScoped<IEmailReadRepository, EmailReadRepository>();
        services.AddScoped<IEmailWriteRepository, EmailWriteRepository>();

        #endregion

        return services;
    }
}