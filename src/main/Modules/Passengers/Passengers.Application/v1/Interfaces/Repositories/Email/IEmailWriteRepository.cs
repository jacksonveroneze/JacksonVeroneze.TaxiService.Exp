using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;

public interface IEmailWriteRepository
{
    public Task CreateAsync(
        EmailEntity entity,
        CancellationToken cancellationToken);

    public Task DeleteAsync(
        EmailEntity entity,
        CancellationToken cancellationToken);
}