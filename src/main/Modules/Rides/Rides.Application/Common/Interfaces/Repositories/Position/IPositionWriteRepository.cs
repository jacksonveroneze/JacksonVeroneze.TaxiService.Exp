using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Position;

public interface IPositionWriteRepository
{
    public Task CreateAsync(
        PositionEntity entity,
        CancellationToken cancellationToken);
}