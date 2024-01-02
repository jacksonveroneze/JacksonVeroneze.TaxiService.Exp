namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.UnitOfWork;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}