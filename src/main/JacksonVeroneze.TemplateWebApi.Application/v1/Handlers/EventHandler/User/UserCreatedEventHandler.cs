using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.User;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.EventHandler.User;

[ExcludeFromCodeCoverage]
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
