using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Domain.Repositories;

public interface IPositionRepository
{
    public Task CreateAsync(
        Position entity,
        CancellationToken cancellationToken);
}
