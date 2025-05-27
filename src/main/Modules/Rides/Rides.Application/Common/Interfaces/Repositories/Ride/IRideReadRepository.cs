using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Rides.Application.v1.Ride;
using TaxiService.Modules.Rides.Domain.Filters;

namespace TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Ride;

public interface IRideReadRepository
{
    public Task<bool> ExistsActiveByPassengerIdAsync(
        Guid passengerId,
        CancellationToken cancellationToken);

    public Task<RideDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task<Page<RideDto>> GetPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken);
}
