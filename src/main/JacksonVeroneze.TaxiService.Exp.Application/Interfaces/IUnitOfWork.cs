namespace JacksonVeroneze.TaxiService.Exp.Application.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}