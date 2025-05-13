using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application.v1.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Validators;
using TaxiService.Modules.Passengers.Application.v1.Services;
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
        #region Passanger

        // Services
        services.AddScoped<IPassengerValidator, PassengerValidator>();
        
        // Repositories
        services.AddScoped<IPassangerReadRepository, PassangerReadRepository>();
        services.AddScoped<IPassangerWriteRepository, PassangerWriteRepository>();

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