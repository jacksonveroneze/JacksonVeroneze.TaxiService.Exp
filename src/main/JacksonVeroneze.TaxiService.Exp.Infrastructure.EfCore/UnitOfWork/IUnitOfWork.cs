namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.UnitOfWork;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}