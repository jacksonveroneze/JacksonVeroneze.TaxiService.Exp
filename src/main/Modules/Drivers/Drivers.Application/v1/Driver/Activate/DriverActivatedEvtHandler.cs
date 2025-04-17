using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Events.Driver;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Activate;

public class DriverActivatedEvtHandler(
    IDriverReadCacheRepository cacheRepository)
    : INotificationHandler<DriverActivatedEvent>
{
    public Task Handle(
        DriverActivatedEvent notification,
        CancellationToken cancellationToken)
    {
        return cacheRepository.RemoveAsync(
            notification.AggregateId, cancellationToken);
    }
}