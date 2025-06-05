using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Ride.Domain.Events;

[ExcludeFromCodeCoverage]
public class RideFinishedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, nameof(RideFinishedDomainEvent));
