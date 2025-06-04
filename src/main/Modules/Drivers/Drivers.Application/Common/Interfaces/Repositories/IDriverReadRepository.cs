using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Filters;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories;

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

    public Task<ICollection<DriverDto>> GetAsync(
        DriverFilter filter,
        CancellationToken cancellationToken);

    public Task<Page<DriverDto>> GetPagedAsync(
        DriverPagedFilter filter,
        CancellationToken cancellationToken);
}
