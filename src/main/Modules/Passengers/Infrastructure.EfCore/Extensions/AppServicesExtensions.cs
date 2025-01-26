using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application.Interfaces.Repositories.Outbox;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Position;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Transaction;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Email;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Outbox;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Position;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Ride;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Transaction;
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

        #region Ride

        // Repositories
        services.AddScoped<IRideReadRepository, RideReadRepository>();
        services.AddScoped<IRideWriteRepository, RideWriteRepository>();

        #endregion

        #region Position

        // Repositories
        services.AddScoped<IPositionReadRepository, PositionReadRepository>();
        services.AddScoped<IPositionWriteRepository, PositionWriteRepository>();

        #endregion

        #region Transaction

        // Repositories
        services.AddScoped<ITransactionReadRepository, TransactionReadRepository>();
        services.AddScoped<ITransactionWriteRepository, TransactionWriteRepository>();

        #endregion

        #region Outbox

        // Repositories
        services.AddScoped<IOutboxWriteRepository, OutboxWriteWriteRepository>();

        #endregion

        return services;
    }
}