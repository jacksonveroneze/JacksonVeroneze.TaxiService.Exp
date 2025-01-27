using TaxiService.Common.BaseApplication.Interfaces.UnitOfWork;
using EfCoreUnitOfWork = JacksonVeroneze.NET.EntityFramework.Interfaces.IUnitOfWork;

namespace TaxiService.Common.BaseInfrastructure;

public class UnitOfWork(EfCoreUnitOfWork unitOfWork)
    : IUnitOfWork
{
    public async Task<bool> CommitAsync(
        CancellationToken cancellationToken)
    {
        bool success = await unitOfWork
            .CommitAsync(cancellationToken);

        return success;
    }
}