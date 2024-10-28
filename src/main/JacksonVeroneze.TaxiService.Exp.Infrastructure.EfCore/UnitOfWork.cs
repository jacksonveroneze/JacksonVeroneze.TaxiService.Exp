using JacksonVeroneze.TaxiService.Exp.Application.Interfaces;
using BaseUow = JacksonVeroneze.NET.EntityFramework.Interfaces;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore;

public class UnitOfWork(BaseUow.IUnitOfWork unitOfWork)
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