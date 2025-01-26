using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Filters;

namespace TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;

public interface IRideReadRepository
{
    public Task<bool> ExistsActiveByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    public Task<RideEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    public Task<Page<RideEntity>> GetPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken = default);
}