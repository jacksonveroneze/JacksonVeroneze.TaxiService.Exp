using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Identity;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Messaging;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.System;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Tenant;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Services.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Services.User;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Ride;
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

        // Services
        services.AddScoped<IActivateUserService, ActivateUserService>();
        services.AddScoped<IInactivateUserService, InactivateUserService>();
        services.AddScoped<IDeleteUserService, DeleteUserService>();
        services.AddScoped<IGetUserService, GetUserService>();

        // Repositories
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        // services.AddSingleton<IUserReadRepository, UserReadStubRepository>();
        // services.AddSingleton<IUserWriteRepository, UserWriteStubRepository>();

        #endregion

        #region Ride

        // Services
        services.AddScoped<IGetRideService, GetRideService>();
        services.AddScoped<IStatusRideService, StatusRideService>();

        // Repositories
        services.AddScoped<IRideReadRepository, RideReadRepository>();
        services.AddScoped<IRideWriteRepository, RideWriteRepository>();

        #endregion

        return services;
    }
}
