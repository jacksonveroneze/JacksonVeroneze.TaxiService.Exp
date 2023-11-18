using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.User;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.EventHandler.User;

[ExcludeFromCodeCoverage]
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
