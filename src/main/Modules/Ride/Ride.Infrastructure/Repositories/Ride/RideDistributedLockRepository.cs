using RedLockNet;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Repositories;

namespace TaxiService.Modules.Ride.Infrastructure.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideDistributedLockRepository(
    IDistributedLockFactory distributedLockFactory) : IRideDistributedLockRepository
{
    private const string PrefixLockKey = "_lock_ride_action_accept_";

    // Lock expira após 5 segundos
    private readonly TimeSpan _expiryTime = TimeSpan.FromSeconds(10);

    // Tempo máximo de espera para adquirir o lock
    private readonly TimeSpan _waitTime = TimeSpan.FromSeconds(5);

    // Intervalo de 2 segundos entre tentativas
    private readonly TimeSpan _retryDelay = TimeSpan.FromSeconds(1);

    public async Task<IRedLock?> AcquireLockAsync(
        Guid rideId,
        CancellationToken cancellationToken)
    {
        string key = $"{PrefixLockKey}{rideId}";

        IRedLock? distributedLock = await distributedLockFactory
            .CreateLockAsync(key, _expiryTime,
                _waitTime, _retryDelay, cancellationToken);

        return distributedLock.IsAcquired ? distributedLock : null;
    }
}
