using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Position;

public interface IPositionWriteRepository
{
    public Task CreateAsync(
        PositionEntity entity,
        CancellationToken cancellationToken = default);
}