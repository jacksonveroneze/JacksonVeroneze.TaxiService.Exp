using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.User;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.EventHandler.User;

[ExcludeFromCodeCoverage]
public class UserInactivatedEventHandler(
    IIntegrationEventPublisher publisher) :
    INotificationHandler<UserInactivatedDomainEvent>
{
    public Task Handle(
        UserInactivatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        return publisher.PublishAsync(notification,
            cancellationToken);
    }
}
