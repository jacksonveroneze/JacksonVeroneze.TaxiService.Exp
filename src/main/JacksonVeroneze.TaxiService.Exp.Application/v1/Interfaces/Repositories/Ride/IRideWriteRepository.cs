using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;

public interface IRideWriteRepository
{
    Task CreateAsync(
        RideEntity entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        RideEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        RideEntity entity,
        CancellationToken cancellationToken = default);
}
