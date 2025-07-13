using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Mapper;
using TaxiService.Common.BaseInfrastructure.Messaging;
using TaxiService.Common.BaseInfrastructure.System;
using TaxiService.Common.SharedKernel.Interfaces;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddCommonInfraAppServices(
        this IServiceCollection services)
    {
        services.AddScoped<IDataMapper, AutoMapperAdapter>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();

        return services;
    }
}
