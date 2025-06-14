using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.v1.DriverAvailability.Models;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Domain.Enums;
using TaxiService.Modules.Driver.Domain.Repositories;
using TaxiService.Modules.Driver.Domain.Services;
using TaxiService.Modules.Ride.Contracts.Events;

namespace TaxiService.Modules.Driver.Application.IntegrationEvents;

public class RideCanceledDomainEvtHandler(
    IDriverAvailabilityDomainService availabilityDomainService,
    IDriverAvailabilityHistoryRepository driverAvailabilityHistoryRepository,
    IDriverAvailabilityHistoryReadRepository driverAvailabilityHistoryReadRepository)
    : INotificationHandler<RideCanceledDomainEvent>
{
    public async Task Handle(
        RideCanceledDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.DriverId is null)
        {
            return;
        }
        
        DriverAvailabilityDto? currentAvailability =
            await driverAvailabilityHistoryReadRepository
                .GetLastByDriverIdAsync(notification.DriverId!.Value, cancellationToken);

        Result<DriverAvailabilityHistory> result = availabilityDomainService
            .TryUpdateAvailabilityStatus(
                notification.DriverId!.Value,
                currentAvailability?.Status,
                DriverAvailabilityStatus.Online);

        await driverAvailabilityHistoryRepository.CreateAsync(
            result.Value!, cancellationToken);
    }
}
