using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.User;

public interface IUserReadRepository
{
    Task<bool> ExistsAsync(
        string document,
        CancellationToken cancellationToken = default);

    Task<UserEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default);
}
