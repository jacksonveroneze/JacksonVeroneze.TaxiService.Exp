using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Rides.Domain.Events;

[ExcludeFromCodeCoverage]
public class RideAcceptedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, nameof(RideAcceptedDomainEvent));
