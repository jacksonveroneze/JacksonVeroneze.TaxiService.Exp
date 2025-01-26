using System.Diagnostics.CodeAnalysis;
using TaxiService.Modules.Passengers.Domain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserDeletedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "deleted");