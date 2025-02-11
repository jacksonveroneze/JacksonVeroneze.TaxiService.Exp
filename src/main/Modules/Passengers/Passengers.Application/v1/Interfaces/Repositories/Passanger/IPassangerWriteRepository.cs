using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;

public interface IPassangerWriteRepository
{
    public Task CreateAsync(
        PassangerEntity entity,
        CancellationToken cancellationToken);

    public Task DeleteAsync(
        PassangerEntity entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        PassangerEntity entity,
        CancellationToken cancellationToken);
}