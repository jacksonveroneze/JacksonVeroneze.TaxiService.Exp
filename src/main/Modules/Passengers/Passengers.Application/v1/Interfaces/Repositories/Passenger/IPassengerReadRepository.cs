using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;

public interface IPassengerReadRepository
{
    public Task<PassengerEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task<Page<PassengerEntity>> GetPagedAsync(
        PassengerPagedFilter filter,
        CancellationToken cancellationToken);
}
