using TaxiService.Modules.Drivers.Application.v1.Interfaces;
using TaxiService.Modules.Drivers.Infrastructure.EfCore.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.EfCore.UnitOfWork;

public class UnitOfWork(
    DriversDbContext context) : IUnitOfWork
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