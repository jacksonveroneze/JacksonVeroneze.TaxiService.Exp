using JacksonVeroneze.NET.DomainObjects.Messaging;
using TaxiService.Modules.Passengers.Application.Interfaces.Messaging;

namespace TaxiService.Modules.Passengers.Infrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class RabbitMqEventPublisher : IIntegrationEventPublisher
{
    public Task PublishAsync(DomainEvent data,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}