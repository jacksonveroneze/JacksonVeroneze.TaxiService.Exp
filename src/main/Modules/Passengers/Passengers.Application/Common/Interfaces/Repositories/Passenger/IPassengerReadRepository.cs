using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Passengers.Application.v1.Passenger;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;

public interface IPassengerReadRepository
{
    public Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
    
    public Task<PassengerDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task<ICollection<PassengerDto>> GetAsync(
        PassengerFilter filter,
        CancellationToken cancellationToken);

    public Task<Page<PassengerDto>> GetPagedAsync(
        PassengerPagedFilter filter,
        CancellationToken cancellationToken);
}
