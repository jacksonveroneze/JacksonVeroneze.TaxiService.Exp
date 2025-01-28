using Microsoft.EntityFrameworkCore;
using TaxiService.Common.BaseApplication.Interfaces.UnitOfWork;

namespace TaxiService.Common.BaseInfrastructure;

public class UnitOfWork<TDbContext>(
    JacksonVeroneze.NET.EntityFramework.Interfaces.IUnitOfWork<TDbContext> unitOfWork)
    : IUnitOfWork
    where TDbContext : DbContext
{
    public async Task<bool> CommitAsync(
        CancellationToken cancellationToken)
    {
        bool success = await unitOfWork
            .CommitAsync(cancellationToken);

        return success;
    }
}