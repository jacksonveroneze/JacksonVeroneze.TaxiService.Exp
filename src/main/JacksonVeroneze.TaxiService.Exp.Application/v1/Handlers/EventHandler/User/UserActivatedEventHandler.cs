using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Messaging;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.User;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.EventHandler.User;

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
