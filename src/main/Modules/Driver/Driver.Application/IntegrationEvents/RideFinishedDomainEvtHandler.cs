using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Domain.Enums;
using TaxiService.Modules.Driver.Domain.Repositories;
using TaxiService.Modules.Driver.Domain.Services;
using TaxiService.Modules.Ride.Contracts.Events;

namespace TaxiService.Modules.Driver.Application.IntegrationEvents;

public class RideFinishedDomainEvtHandler(
    IDriverAvailabilityDomainService availabilityDomainService,
    IDriverAvailabilityHistoryRepository driverAvailabilityHistoryRepository,
    IDriverAvailabilityHistoryReadRepository driverAvailabilityHistoryReadRepository)
    : INotificationHandler<RideFinishedDomainEvent>
{
    public async Task Handle(
        RideFinishedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        DriverAvailabilityHistory? currentAvailability =
            await driverAvailabilityHistoryReadRepository
                .GetLastByDriverIdAsync(notification.DriverId, cancellationToken);

        Result<DriverAvailabilityHistory> result = availabilityDomainService
            .TryUpdateAvailabilityStatus(
                notification.DriverId,
                currentAvailability?.Status,
                DriverAvailabilityStatus.Online);

        await driverAvailabilityHistoryRepository.CreateAsync(
            result.Value!, cancellationToken);
    }
}
