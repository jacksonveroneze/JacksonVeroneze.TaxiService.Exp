namespace JacksonVeroneze.TaxiService.Exp.Application.Interfaces;

public interface IUnitOfWork
{
    public Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}