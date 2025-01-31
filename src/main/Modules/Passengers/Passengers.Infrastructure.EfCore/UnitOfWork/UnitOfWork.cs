using TaxiService.Modules.Passengers.Application.v1.Interfaces;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.UnitOfWork;

public class UnitOfWork(
    PassengersDbContext context) : IUnitOfWork
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