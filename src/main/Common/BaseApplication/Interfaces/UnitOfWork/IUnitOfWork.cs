namespace TaxiService.Common.BaseApplication.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    public Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}
