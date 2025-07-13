namespace TaxiService.Common.SharedKernel.Interfaces;

public interface IUnitOfWork
{
    public Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}
