namespace JacksonVeroneze.TaxiService.Exp.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    public Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}