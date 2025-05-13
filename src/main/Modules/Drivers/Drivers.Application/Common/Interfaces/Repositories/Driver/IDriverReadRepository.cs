using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain.Filters;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;

public interface IDriverReadRepository
{
    public Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    public Task<DriverDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task<Page<DriverDto>> GetPagedAsync(
        DriverPagedFilter filter,
        CancellationToken cancellationToken);
}
