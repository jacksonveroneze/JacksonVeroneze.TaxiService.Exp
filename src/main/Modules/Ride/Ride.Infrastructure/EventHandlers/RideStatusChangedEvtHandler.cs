using MediatR;
using TaxiService.Common.Messaging.Abstractions;
using TaxiService.Modules.Ride.Contracts.Events;
using TaxiService.Modules.Ride.Contracts.Events.DomainEvents;
using TaxiService.Modules.Ride.Contracts.Events.IntegrationEvents;
using TaxiService.Modules.Ride.Domain.Enums;

namespace TaxiService.Modules.Ride.Infrastructure.EventHandlers;

public class RideStatusChangedEvtHandler(IMessageBusPublisher busPublisher)
    : INotificationHandler<RideCanceledDomainEvent>,
      INotificationHandler<RideRequestedDomainEvent>,
      INotificationHandler<RideAcceptedDomainEvent>,
      INotificationHandler<RideStartedDomainEvent>,
      INotificationHandler<RideFinishedDomainEvent>
{
    public async Task Handle(
        RideCanceledDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);

        RideStatusChangedIntegrationEvent evt = new(
            notification.AggregateId,
            notification.PassengerId!.Value,
            nameof(RideStatus.Canceled),
            notification.DriverId
        );

        await busPublisher.PublishAsync(evt, cancellationToken);
    }

    public async Task Handle(
        RideRequestedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);

        RideStatusChangedIntegrationEvent evt = new(
            notification.AggregateId,
            notification.PassengerId,
            nameof(RideStatus.Requested)
        );

        await busPublisher.PublishAsync(evt, cancellationToken);
    }

    public async Task Handle(
        RideAcceptedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);

        RideStatusChangedIntegrationEvent evt = new(
            notification.AggregateId,
            notification.PassengerId,
            nameof(RideStatus.Accepted),
            notification.DriverId
        );

        await busPublisher.PublishAsync(evt, cancellationToken);
    }

    public async Task Handle(
        RideStartedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);

        RideStatusChangedIntegrationEvent evt = new(
            notification.AggregateId,
            notification.PassengerId,
            nameof(RideStatus.InProgress),
            notification.DriverId
        );

        await busPublisher.PublishAsync(evt, cancellationToken);
    }

    public async Task Handle(
        RideFinishedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);

        RideStatusChangedIntegrationEvent evt = new(
            notification.AggregateId,
            notification.PassengerId,
            nameof(RideStatus.Completed),
            notification.DriverId
        );

        await busPublisher.PublishAsync(evt, cancellationToken);
    }
}
