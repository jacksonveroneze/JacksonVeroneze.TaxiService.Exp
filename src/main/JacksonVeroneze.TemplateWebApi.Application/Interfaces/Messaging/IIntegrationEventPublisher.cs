using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;

public interface IIntegrationEventPublisher
{
    public Task PublishAsync(BaseDomainEvent data,
        CancellationToken cancellationToken);
}
