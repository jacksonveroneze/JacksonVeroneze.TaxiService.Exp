using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;

public interface IDriverReadRepository
{
    public Task<DriverEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}