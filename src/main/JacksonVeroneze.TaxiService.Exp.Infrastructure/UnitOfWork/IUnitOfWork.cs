namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}
