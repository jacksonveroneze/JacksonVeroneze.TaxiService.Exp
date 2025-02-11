using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Filters;

namespace TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;

public interface IRideReadRepository
{
    public Task<bool> ExistsActiveByPassangerIdAsync(
        Guid passangerId,
        CancellationToken cancellationToken);

    public Task<RideEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task<Page<RideEntity>> GetPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken);
}