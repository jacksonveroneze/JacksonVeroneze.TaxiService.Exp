using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Transaction;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Transaction;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddEfCoreServices(
        this IServiceCollection services)
    {
        #region User

        // Repositories
        //services.AddScoped<IUserReadRepository, UserReadRepository>();
        //services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        //services.AddScoped<IUserReadDistribCachedRepository, UserReadDistribCachedRepository>();

        #endregion

        #region Email

        // Repositories
        // services.AddScoped<IEmailReadRepository, EmailReadRepository>();
        // services.AddScoped<IEmailWriteRepository, EmailWriteRepository>();

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

        return services;
    }
}