using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.Domain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserInactivatedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "inactivated");