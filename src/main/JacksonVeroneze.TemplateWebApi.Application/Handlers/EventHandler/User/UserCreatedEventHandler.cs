using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.EventHandler.User;

public class UserCreatedEventHandler :
    INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public UserCreatedEventHandler(
        IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Handle(
        UserCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        return _publisher.PublishAsync(notification,
            cancellationToken);
    }
}
