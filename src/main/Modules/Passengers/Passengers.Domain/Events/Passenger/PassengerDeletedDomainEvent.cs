using System.Diagnostics.CodeAnalysis;

using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.Events.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerDeletedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "deleted");
