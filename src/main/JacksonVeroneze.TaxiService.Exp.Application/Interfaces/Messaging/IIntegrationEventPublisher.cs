using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Messaging;

public interface IIntegrationEventPublisher
{
    public Task PublishAsync(DomainEvent data,
        CancellationToken cancellationToken);
}
