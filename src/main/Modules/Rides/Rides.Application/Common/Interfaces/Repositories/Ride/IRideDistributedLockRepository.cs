using RedLockNet;

namespace TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;

public interface IRideDistributedLockRepository
{
    public Task<IRedLock?> AcquireLockAsync(
        Guid rideId,
        CancellationToken cancellationToken);

}