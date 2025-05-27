using Microsoft.Extensions.DependencyInjection;

using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Outbox;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Infrastructure.Repositories.Email;
using TaxiService.Modules.Passengers.Infrastructure.Repositories.Outbox;
using TaxiService.Modules.Passengers.Infrastructure.Repositories.Passenger;

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

        #endregion

        #region Email

        // Repositories
        services.AddScoped<IEmailReadRepository, EmailReadRepository>();
        services.AddScoped<IEmailWriteRepository, EmailWriteRepository>();

        #endregion

        #region Outbox

        services.AddScoped<IOutboxWriteRepository, OutboxWriteWriteRepository>();

        #endregion

        return services;
    }
}
