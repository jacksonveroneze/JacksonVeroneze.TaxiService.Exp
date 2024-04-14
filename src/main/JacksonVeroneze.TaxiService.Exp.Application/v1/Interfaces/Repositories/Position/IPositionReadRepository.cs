using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;

public interface IPositionReadRepository
{
    Task<ICollection<PositionEntity>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken = default);
}