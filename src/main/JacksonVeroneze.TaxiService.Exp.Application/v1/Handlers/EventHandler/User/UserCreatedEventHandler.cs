using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Messaging;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.User;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.EventHandler.User;

[ExcludeFromCodeCoverage]
public class UserCreatedEventHandler(
    IIntegrationEventPublisher publisher) :
    INotificationHandler<UserCreatedDomainEvent>
{
    public Task Handle(
        UserCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        return publisher.PublishAsync(notification,
            cancellationToken);
    }
}