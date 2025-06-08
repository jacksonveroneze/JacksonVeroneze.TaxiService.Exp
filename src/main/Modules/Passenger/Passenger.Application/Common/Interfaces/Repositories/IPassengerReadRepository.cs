using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Filters;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Models;

namespace TaxiService.Modules.Passenger.Application.Common.Interfaces.Repositories;

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
