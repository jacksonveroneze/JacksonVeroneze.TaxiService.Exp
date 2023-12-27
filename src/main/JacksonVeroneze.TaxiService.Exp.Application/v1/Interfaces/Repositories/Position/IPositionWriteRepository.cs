using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;

public interface IPositionWriteRepository
{
    Task CreateAsync(
        PositionEntity entity,
        CancellationToken cancellationToken = default);
}