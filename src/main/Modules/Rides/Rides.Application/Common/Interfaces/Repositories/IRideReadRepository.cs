using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Filters;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories;

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
