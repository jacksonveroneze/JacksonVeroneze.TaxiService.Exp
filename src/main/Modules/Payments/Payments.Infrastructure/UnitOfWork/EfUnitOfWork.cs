using TaxiService.Modules.Payments.Application.v1;
using TaxiService.Modules.Payments.Infrastructure.Contexts;

namespace TaxiService.Modules.Payments.Infrastructure.UnitOfWork;

public class EfUnitOfWork(
    PaymentsDbContext context) : IUnitOfWork
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