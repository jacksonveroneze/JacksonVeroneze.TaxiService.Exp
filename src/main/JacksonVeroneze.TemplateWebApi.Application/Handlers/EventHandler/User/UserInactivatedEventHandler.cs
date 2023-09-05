using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.EventHandler.User;

public class UserInactivatedEventHandler :
    INotificationHandler<UserInactivatedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public UserInactivatedEventHandler(
        IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Handle(
        UserInactivatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        return _publisher.PublishAsync(notification,
            cancellationToken);
    }
}
