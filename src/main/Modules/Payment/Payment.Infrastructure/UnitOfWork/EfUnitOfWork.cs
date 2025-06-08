using TaxiService.Modules.Payment.Application.Common.Interfaces;
using TaxiService.Modules.Payment.Infrastructure.Contexts;

namespace TaxiService.Modules.Payment.Infrastructure.UnitOfWork;

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
