using System.Diagnostics.CodeAnalysis;
using Ride.Common;
using TaxiService.Common.Messaging.Abstractions;

namespace TaxiService.Modules.Ride.Contracts.Events.IntegrationEvents;

[ExcludeFromCodeCoverage]
public record RideStatusChangedIntegrationEvent(
    Guid RideId,
    Guid PassengerId,
    string Status,
    Guid? DriverId = null)
    : BaseIntegrationEvent(
        ModuleConstants.ContextName,
        nameof(RideStatusChangedIntegrationEvent),
        Guid.NewGuid().ToString());
