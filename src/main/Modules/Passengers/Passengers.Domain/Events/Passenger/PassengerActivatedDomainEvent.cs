using System.Diagnostics.CodeAnalysis;

using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.Events.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerActivatedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "activated");
