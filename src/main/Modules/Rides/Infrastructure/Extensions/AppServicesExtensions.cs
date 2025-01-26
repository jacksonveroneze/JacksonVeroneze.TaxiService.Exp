using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Rides.Application.Interfaces.Messaging;
using TaxiService.Modules.Rides.Application.Interfaces.System;
using TaxiService.Modules.Rides.Domain.Services;
using TaxiService.Modules.Rides.Infrastructure.Messaging;
using TaxiService.Modules.Rides.Infrastructure.System;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

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