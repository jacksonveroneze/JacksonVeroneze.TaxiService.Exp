using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Email;

public interface IEmailWriteRepository
{
    public Task CreateAsync(
        EmailEntity entity,
        CancellationToken cancellationToken);
}