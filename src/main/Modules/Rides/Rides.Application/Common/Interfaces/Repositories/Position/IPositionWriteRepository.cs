using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Position;

public interface IPositionWriteRepository
{
    public Task CreateAsync(
        PositionEntity entity,
        CancellationToken cancellationToken);
}