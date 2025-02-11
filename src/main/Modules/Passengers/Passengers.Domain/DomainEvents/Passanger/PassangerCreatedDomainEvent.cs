using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.DomainEvents.Passanger;

[ExcludeFromCodeCoverage]
public class PassangerCreatedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "created");