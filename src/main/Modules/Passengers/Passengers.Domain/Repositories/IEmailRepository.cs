using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Domain.Repositories;

public interface IEmailRepository
{
    public Task CreateAsync(
        Email entity,
        CancellationToken cancellationToken);

    public Task DeleteAsync(
        Email entity,
        CancellationToken cancellationToken);
}
