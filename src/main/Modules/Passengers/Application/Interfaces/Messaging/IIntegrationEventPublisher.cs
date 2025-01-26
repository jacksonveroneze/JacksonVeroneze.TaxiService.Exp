using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace TaxiService.Modules.Passengers.Application.Interfaces.Messaging;

public interface IIntegrationEventPublisher
{
    public Task PublishAsync(DomainEvent data,
        CancellationToken cancellationToken);
}