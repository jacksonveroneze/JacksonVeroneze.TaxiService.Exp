using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.v1.DriverAvailability.Models;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Domain.Enums;
using TaxiService.Modules.Driver.Domain.Repositories;
using TaxiService.Modules.Driver.Domain.Services;
using TaxiService.Modules.Ride.Contracts.Events;

namespace TaxiService.Modules.Driver.Application.IntegrationEvents;

public class RideAcceptedDomainEvtHandler(
    IDriverAvailabilityDomainService availabilityDomainService,
    IDriverAvailabilityHistoryRepository driverAvailabilityRepository,
    IDriverAvailabilityHistoryReadRepository driverAvailabilityReadRepository)
    : INotificationHandler<RideAcceptedDomainEvent>
{
    public async Task Handle(
        RideAcceptedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        DriverAvailabilityDto? currentAvailability =
            await driverAvailabilityReadRepository
                .GetLastByDriverIdAsync(notification.DriverId, cancellationToken);

        Result<DriverAvailabilityHistory> result = availabilityDomainService
            .TryUpdateAvailabilityStatus(
                notification.DriverId,
                currentAvailability?.Status,
                DriverAvailabilityStatus.InRide);

        await driverAvailabilityRepository.CreateAsync(
            result.Value!, cancellationToken);
    }
}
