using TaxiService.Modules.Rides.Application.v1;
using TaxiService.Modules.Rides.Infrastructure.EfCore.Contexts;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.UnitOfWork;

public class UnitOfWork(
    RidesDbContext context) : IUnitOfWork
{
    public async Task<bool> CommitAsync(
        CancellationToken cancellationToken)
    {
        int total = await context
            .SaveChangesAsync(cancellationToken);

        bool success = total > 0;

        return success;
    }
}