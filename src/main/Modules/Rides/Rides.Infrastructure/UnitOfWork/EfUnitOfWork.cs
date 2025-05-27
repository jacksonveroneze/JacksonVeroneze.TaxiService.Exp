using TaxiService.Modules.Rides.Application.v1;
using TaxiService.Modules.Rides.Infrastructure.Contexts;

namespace TaxiService.Modules.Rides.Infrastructure.UnitOfWork;

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
