using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Passenger.Domain.Events;

[ExcludeFromCodeCoverage]
public class PassengerDeletedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, nameof(PassengerDeletedDomainEvent));
