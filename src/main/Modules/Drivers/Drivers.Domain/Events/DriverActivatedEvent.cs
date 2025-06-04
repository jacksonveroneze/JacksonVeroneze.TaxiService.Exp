using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Drivers.Domain.Events;

[ExcludeFromCodeCoverage]
public class DriverActivatedEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, nameof(DriverActivatedEvent));
