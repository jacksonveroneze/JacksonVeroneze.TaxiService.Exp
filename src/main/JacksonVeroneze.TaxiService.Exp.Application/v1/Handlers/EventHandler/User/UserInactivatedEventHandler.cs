using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Messaging;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.User;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.EventHandler.User;

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
