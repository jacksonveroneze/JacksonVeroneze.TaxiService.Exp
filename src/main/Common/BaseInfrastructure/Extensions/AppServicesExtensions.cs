using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Common.BaseInfrastructure.Messaging;
using TaxiService.Common.BaseInfrastructure.System;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddCommonInfraAppServices(
        this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IIntegrationEventPublisher, RabbitMqEventPublisher>();

        return services;
    }
}