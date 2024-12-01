using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
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

    public async Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Guid? id = !string.IsNullOrEmpty(filter.Pagination?.Cursor)
            ? Guid.Parse(filter.Pagination!.Cursor)
            : null;

        Expression<Func<UserEntity, bool>> spec =
            spec => !id.HasValue || spec.Id > id;

        int paginationLimit = filter.Pagination!.Limit!.Value;
        int databaselimit = paginationLimit + 1;

        ICollection<UserEntity> result = await service.GetAllAsync(
            spec,
            order => order.Name.Value,
            databaselimit,
            cancellationToken: cancellationToken);

        IList<UserEntity> data = result.Take(paginationLimit).ToList();
        bool hasMore = result.Count > paginationLimit;
        string cursor = result.LastOrDefault()?.Id.ToString() ?? string.Empty;

        PageInfo pageInfo = new(hasMore, cursor);

        return new Page<UserEntity>(data, pageInfo);
    }
}