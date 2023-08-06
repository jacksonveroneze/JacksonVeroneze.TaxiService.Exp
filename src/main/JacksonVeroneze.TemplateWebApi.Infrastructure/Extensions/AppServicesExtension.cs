using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Common;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Identity;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Mail;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Common;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Identity;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Mail;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtension
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services)
    {
        #region Common

        services.AddScoped<IDateTime, MachineDateTime>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IMailService, SmtpMailService>();
        services.AddScoped<IIntegrationEventPublisher, RabbitMqEventPublisher>();

        #endregion

        #region Bank

        services.AddScoped<IBankRepository, BankRepository>();

        #endregion

        #region Client

        services.AddScoped<IClientRepository, ClientRepository>();

        #endregion

        return services;
    }
}
