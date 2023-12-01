using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.User;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.EventHandler.User;

[ExcludeFromCodeCoverage]
public class UserActivatedEventHandler(
    IIntegrationEventPublisher publisher) :
    INotificationHandler<UserActivatedDomainEvent>
{
    public Task Handle(
        UserActivatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        return publisher.PublishAsync(notification,
            cancellationToken);
    }
}
