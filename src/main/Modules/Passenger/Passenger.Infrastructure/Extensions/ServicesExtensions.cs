using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passenger.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passenger.Application.v1.Passenger.PublicApi;
using TaxiService.Modules.Passenger.Domain.Repositories;
using TaxiService.Modules.Passenger.Infrastructure.Repositories.Email;
using TaxiService.Modules.Passenger.Infrastructure.Repositories.Outbox;
using TaxiService.Modules.Passenger.Infrastructure.Repositories.Passenger;
using TaxiService.Modules.Passenger.PublicApi.v1.Interfaces;

namespace TaxiService.Modules.Passenger.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class ServicesExtensions
{
    internal static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        #region Passenger

        // Repositories
        services.AddScoped<IPassengerReadRepository, PassengerReadRepository>();
        services.AddScoped<IPassengerRepository, PassengerRepository>();

        // Services (Public API)
        services.AddTransient<IPassengerPublicApi, PassengerPublicApi>();

        #endregion

        #region Email

        // Repositories
        services.AddScoped<IEmailReadRepository, EmailReadRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();

        #endregion

        #region Outbox

        services.AddScoped<IOutboxWriteRepository, OutboxWriteWriteRepository>();

        #endregion

        return services;
    }
}
