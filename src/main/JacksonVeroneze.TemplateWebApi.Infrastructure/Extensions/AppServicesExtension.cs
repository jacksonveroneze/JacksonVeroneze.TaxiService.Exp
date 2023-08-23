using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Common;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Identity;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Mail;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Common;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank.Dapper;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Client.Stub;
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

        services.AddScoped<IDateTime, SystemDateTime>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IEmailService, SmtpEmailService>();
        services.AddScoped<IIntegrationEventPublisher, RabbitMqEventPublisher>();

        #endregion

        #region Bank

        services.AddScoped<IBankReadRepository, BankReadRepository>();
        services.AddScoped<IBankWriteRepository, BankWriteRepository>();

        //services.AddScoped<IBankReadRepository, BankReadRepository>();
        //services.AddScoped<IBankWriteRepository, BankWriteRepository>();

        //services.AddSingleton<IBankReadRepository, BankReadStubRepository>();
        //services.AddSingleton<IBankWriteRepository, BankWriteStubRepository>();

        #endregion

        #region Client

        services.AddScoped<IClientReadRepository, ClientReadStubRepository>();
        services.AddScoped<IClientWriteRepository, ClientWriteStubRepository>();

        #endregion

        return services;
    }
}
