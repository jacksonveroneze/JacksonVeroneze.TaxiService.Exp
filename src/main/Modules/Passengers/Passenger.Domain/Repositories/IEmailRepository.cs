using TaxiService.Modules.Passenger.Domain.Entities;

namespace TaxiService.Modules.Passenger.Domain.Repositories;

public interface IEmailRepository
{
    public Task CreateAsync(
        Email entity,
        CancellationToken cancellationToken);

    public Task DeleteAsync(
        Email entity,
        CancellationToken cancellationToken);
}
