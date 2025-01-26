using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;

public interface IUserReadRepository
{
    public Task<UserEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    public Task<Page<UserEntity>> GetPagedAsync(
        UserPagedQuery query,
        CancellationToken cancellationToken = default);
}