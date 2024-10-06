using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Messaging;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.System;
using JacksonVeroneze.TaxiService.Exp.Domain.Services;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Messaging;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.System;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services)
    {
        #region Common

        services.AddScoped<IDateTime, SystemDateTime>();
        //services.AddScoped<IIdentityService, IdentityService>();
        //services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IIntegrationEventPublisher, RabbitMqEventPublisher>();

        #endregion

        services.AddScoped<IDistanceCalculatorService, DistanceCalculatorService>();

        return services;
    }
}