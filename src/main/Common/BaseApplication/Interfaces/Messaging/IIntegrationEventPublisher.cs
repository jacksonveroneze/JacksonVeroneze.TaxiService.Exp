using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace TaxiService.Common.Application.Interfaces.Messaging;

public interface IIntegrationEventPublisher
{
    public Task PublishAsync(DomainEvent data,
        CancellationToken cancellationToken);
}