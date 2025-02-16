using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.Events.Passanger;

[ExcludeFromCodeCoverage]
public class PassangerDeletedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "deleted");