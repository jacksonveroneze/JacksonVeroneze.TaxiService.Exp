
using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Drivers.Domain.Events.Driver;

[ExcludeFromCodeCoverage]
public class DriverBlockedTemporarilyEvent(Guid driverId)
    : BaseDomainEvent(driverId, "blockedTemporarily");