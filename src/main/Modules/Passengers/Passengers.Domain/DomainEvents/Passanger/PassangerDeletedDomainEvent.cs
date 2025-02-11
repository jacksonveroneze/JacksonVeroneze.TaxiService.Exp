using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.DomainEvents.Passanger;

[ExcludeFromCodeCoverage]
public class PassangerDeletedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "deleted");