using JacksonVeroneze.NET.DomainObjects.Messaging;
using TaxiService.Modules.Rides.Application.Interfaces.Messaging;

namespace TaxiService.Modules.Rides.Infrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class RabbitMqEventPublisher : IIntegrationEventPublisher
{
    public Task PublishAsync(DomainEvent data,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}