using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Results;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;

public interface IUserReadRepository
{
    public Task<UserEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    public Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default);

    public Task<PaginatedResult<UserEntity>> GetCursorPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default);
}