using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.UnitOfWork;
using EfCoreUnitOfWork = JacksonVeroneze.NET.EntityFramework.Interfaces.IUnitOfWork;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore;

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