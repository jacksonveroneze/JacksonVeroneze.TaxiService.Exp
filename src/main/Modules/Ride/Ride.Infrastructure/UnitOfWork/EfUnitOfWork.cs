using TaxiService.Modules.Ride.Application.Common.Interfaces;
using TaxiService.Modules.Ride.Infrastructure.Contexts;

namespace TaxiService.Modules.Ride.Infrastructure.UnitOfWork;

public class EfUnitOfWork(
    RidesDbContext context) : IUnitOfWork
{
    public async Task<bool> CommitAsync(
        CancellationToken cancellationToken)
    {
        int totalChanges = await context
            .SaveChangesAsync(cancellationToken);

        bool success = totalChanges > 0;

        return success;
    }
}
