namespace TaxiService.Modules.Passengers.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    public Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}