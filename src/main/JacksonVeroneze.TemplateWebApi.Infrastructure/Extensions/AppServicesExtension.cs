using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Identity;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.System;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Tenant;
using JacksonVeroneze.TemplateWebApi.Application.v1.Services;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.EntityFramework;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Identity;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Messaging;
using JacksonVeroneze.TemplateWebApi.Infrastructure.System;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Tenant;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

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

        // Repositories
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        // services.AddSingleton<IUserReadRepository, UserReadStubRepository>();
        // services.AddSingleton<IUserWriteRepository, UserWriteStubRepository>();

        #endregion

        return services;
    }
}
