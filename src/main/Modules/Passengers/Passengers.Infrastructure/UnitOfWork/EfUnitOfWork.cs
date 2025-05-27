using TaxiService.Modules.Passengers.Application.v1.Interfaces;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.UnitOfWork;

public class EfUnitOfWork(
    PassengersDbContext context) : IUnitOfWork
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
