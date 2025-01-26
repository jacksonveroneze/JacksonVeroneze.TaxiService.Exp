namespace TaxiService.Common.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    public Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}