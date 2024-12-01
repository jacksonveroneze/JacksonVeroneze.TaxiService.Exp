using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Results;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;

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

    public Task<PaginatedResult<RideEntity>> GetCursorPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken = default);
}