using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace TaxiService.Common.BaseApplication.Interfaces.Messaging;

public interface IIntegrationEventPublisher
{
    public Task PublishAsync(DomainEvent data,
        CancellationToken cancellationToken);
}