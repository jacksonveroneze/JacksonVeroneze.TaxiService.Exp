using System.Diagnostics.CodeAnalysis;

using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.Events.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerCreatedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "created");
