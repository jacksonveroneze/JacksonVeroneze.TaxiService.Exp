using TaxiService.Modules.Drivers.Application.Common.Interfaces;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.UnitOfWork;

public class EfUnitOfWork(
    DriversDbContext context) : IUnitOfWork
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
