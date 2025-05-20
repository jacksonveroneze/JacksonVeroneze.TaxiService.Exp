using System.Diagnostics.CodeAnalysis;

using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.Events.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerInactivatedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "inactivated");
