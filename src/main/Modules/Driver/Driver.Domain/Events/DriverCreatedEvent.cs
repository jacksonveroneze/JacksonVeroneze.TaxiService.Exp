using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Driver.Domain.Events;

[ExcludeFromCodeCoverage]
public class DriverCreatedEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, nameof(DriverCreatedEvent));
