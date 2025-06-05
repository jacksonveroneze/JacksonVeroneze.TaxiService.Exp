namespace TaxiService.Modules.Driver.Domain.Repositories;

public interface IDriverRepository
{
    public Task<Aggregates.Driver?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        Aggregates.Driver entity,
        CancellationToken cancellationToken);

    public Task DeleteAsync(
        Aggregates.Driver entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        Aggregates.Driver entity,
        CancellationToken cancellationToken);
}
