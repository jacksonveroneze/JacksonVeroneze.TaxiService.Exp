using System.Diagnostics.CodeAnalysis;
using TaxiService.Modules.Passengers.Domain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideCanceledDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "canceled");