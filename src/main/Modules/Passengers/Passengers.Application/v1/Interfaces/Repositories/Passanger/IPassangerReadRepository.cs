using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;

public interface IPassangerReadRepository
{
    public Task<PassangerEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task<Page<PassangerEntity>> GetPagedAsync(
        PassangerPagedFilter filter,
        CancellationToken cancellationToken);
}