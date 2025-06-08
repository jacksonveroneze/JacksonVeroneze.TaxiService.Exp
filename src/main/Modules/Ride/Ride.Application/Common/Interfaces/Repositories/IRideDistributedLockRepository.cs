using RedLockNet;

namespace TaxiService.Modules.Ride.Application.Common.Interfaces.Repositories;

public interface IRideDistributedLockRepository
{
    public Task<IRedLock?> AcquireLockAsync(
        Guid rideId,
        CancellationToken cancellationToken);
}
