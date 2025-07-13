using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Passenger.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class PassengerCreatedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, nameof(PassengerCreatedDomainEvent));
