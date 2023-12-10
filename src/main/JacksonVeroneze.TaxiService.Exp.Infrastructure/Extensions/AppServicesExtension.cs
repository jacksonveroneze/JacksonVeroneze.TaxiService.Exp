using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Identity;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Messaging;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.System;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Tenant;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Transaction;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Services;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Transaction;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Identity;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Messaging;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.System;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Tenant;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtension
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services)
    {
        #region Common

        services.AddScoped<IDateTime, SystemDateTime>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITenantService, TenantService>();

        services.AddScoped<IIntegrationEventPublisher, RabbitMqEventPublisher>();

        #endregion

        #region User

        // Repositories
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        services.AddScoped<IUserReadDistribCachedRepository, UserReadDistribCachedRepository>();

        // services.AddSingleton<IUserReadRepository, UserReadStubRepository>();
        // services.AddSingleton<IUserWriteRepository, UserWriteStubRepository>();

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

        services.AddScoped<IDistanceCalculatorService, DistanceCalculatorService>();

        return services;
    }
}
