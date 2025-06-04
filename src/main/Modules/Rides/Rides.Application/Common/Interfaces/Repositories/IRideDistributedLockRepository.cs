using RedLockNet;

namespace TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories;

public interface IRideDistributedLockRepository
{
    public Task<IRedLock?> AcquireLockAsync(
        Guid rideId,
        CancellationToken cancellationToken);
}
