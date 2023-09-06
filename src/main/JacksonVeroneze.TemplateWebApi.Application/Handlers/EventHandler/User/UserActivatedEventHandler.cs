using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.EventHandler.User;

[ExcludeFromCodeCoverage]
public class UserActivatedEventHandler :
    INotificationHandler<UserActivatedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public UserActivatedEventHandler(
        IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Handle(
        UserActivatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        return _publisher.PublishAsync(notification,
            cancellationToken);
    }
}
