using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;

public interface IPositionWriteRepository
{
    public Task CreateAsync(
        PositionEntity entity,
        CancellationToken cancellationToken = default);
}