using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;

public interface IRideWriteRepository
{
    public Task CreateAsync(
        RideEntity entity,
        CancellationToken cancellationToken = default);

    public Task UpdateAsync(
        RideEntity entity,
        CancellationToken cancellationToken = default);
}