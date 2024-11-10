using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Results;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.User;

[ExcludeFromCodeCoverage]
public class UserReadRepository(
    IEfCoreRepository<UserEntity> service) : IUserReadRepository
{
    public Task<UserEntity?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        UserByIdSpecification specByUserId = new(id);

        return service.GetByIdAsync(specByUserId.ToExpression(),
            cancellationToken);
    }

    public Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        UserByNameSpecification specByName = new(filter.Name);
        UserByStatusSpecification specByStatus = new(filter.Status);

        Expression<Func<UserEntity, bool>> spec =
            specByName.ToExpression().And(specByStatus);

        return service.GetPagedAsync(
            filter.Pagination!,
            spec,
            order => order.Name.Value,
            cancellationToken: cancellationToken);
    }

    public async Task<PaginatedResult<UserEntity>> GetCursorPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Guid? id = !string.IsNullOrEmpty(filter.CursorPagination?.Cursor)
            ? Guid.Parse(filter.CursorPagination!.Cursor)
            : null;

        Expression<Func<UserEntity, bool>> spec =
            spec => !id.HasValue || spec.Id > id;

        int paginationLimit = filter.CursorPagination!.Limit!.Value;
        int databaselimit = paginationLimit + 1;

        ICollection<UserEntity> result = await service.GetAllAsync(
            spec,
            order => order.Name.Value,
            databaselimit,
            cancellationToken: cancellationToken);

        ICollection<UserEntity> data = result.Take(paginationLimit).ToArray();
        bool hasMore = result.Count > paginationLimit;
        string cursor = result.LastOrDefault()?.Id.ToString() ?? string.Empty;

        return new PaginatedResult<UserEntity>(data, hasMore, cursor);
    }
}