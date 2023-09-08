using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Common;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Identity;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Mail;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Common;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Identity;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Mail;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;
using DapperType = JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.Dapper;
using EntityFrameworkType =
    JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.EntityFramework;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtension
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        #region Common

        services.AddScoped<IDateTime, SystemDateTime>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IEmailService, SmtpEmailService>();
        services.AddScoped<IIntegrationEventPublisher, RabbitMqEventPublisher>();

        #endregion

        #region User

        if (appConfiguration.Database!.Type == DatabaseType.Dapper)
        {
            services.AddScoped<IUserReadRepository, DapperType.UserReadRepository>();
            services.AddScoped<IUserWriteRepository, DapperType.UserWriteRepository>();
        }
        else
        {
            services.AddScoped<IUserReadRepository, EntityFrameworkType.UserReadRepository>();
            services.AddScoped<IUserWriteRepository, EntityFrameworkType.UserWriteRepository>();
        }

        // services.AddSingleton<IUserReadRepository, UserReadStubRepository>();
        // services.AddSingleton<IUserWriteRepository, UserWriteStubRepository>();

        #endregion

        return services;
    }
}
