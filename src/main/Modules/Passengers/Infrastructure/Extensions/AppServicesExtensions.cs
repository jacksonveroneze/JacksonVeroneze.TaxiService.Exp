using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application.Interfaces.Messaging;
using TaxiService.Modules.Passengers.Application.Interfaces.System;
using TaxiService.Modules.Passengers.Domain.Services;
using TaxiService.Modules.Passengers.Infrastructure.Messaging;
using TaxiService.Modules.Passengers.Infrastructure.System;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services)
    {
        #region Common

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IIntegrationEventPublisher, RabbitMqEventPublisher>();

        #endregion

        services.AddScoped<IDistanceCalculatorService, DistanceCalculatorService>();

        return services;
    }
}