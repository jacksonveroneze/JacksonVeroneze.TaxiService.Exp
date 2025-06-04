using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passengers.Application.v1.Passenger.PublicApi;
using TaxiService.Modules.Passengers.Domain.Repositories;
using TaxiService.Modules.Passengers.Infrastructure.Repositories.Email;
using TaxiService.Modules.Passengers.Infrastructure.Repositories.Outbox;
using TaxiService.Modules.Passengers.Infrastructure.Repositories.Passenger;
using TaxiService.Modules.Passengers.PublicApi.v1.Interfaces;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

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
