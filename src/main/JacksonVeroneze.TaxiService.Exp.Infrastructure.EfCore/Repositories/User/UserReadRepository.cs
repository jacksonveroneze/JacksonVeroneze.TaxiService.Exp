using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Util;
using JacksonVeroneze.TaxiService.Exp.Domain.Util.Operators;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Util;
using LinqKit;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.User;

[ExcludeFromCodeCoverage]
public class UserReadRepository(
    IEfCoreRepository<UserEntity> service) : IUserReadRepository
{
    public Task<UserEntity?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        return service.GetByIdAsync(
            query => query.Id == id, cancellationToken);
    }

    public async Task<Page<UserEntity>> GetPagedAsync(
        UserPagedQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        Guid cursorId = Guid.Parse(query.Pagination!.Cursor!);

        ExpressionStarter<UserEntity> predicate =
            new ExpressionBuilder<UserEntity>()
                .AddFilter(u => u.Id, GuidFilter.Create(cursorId, FilterOperator.Gte))
                .AddFilter(u => u.Name.Value!, query.Filter!.NameFilter)
                .AddFilter(u => u.Status, query.Filter!.StatusFilter!)
                .AddFilter(u => u.Birthday, query.Filter!.StartBirthdayFilter!)
                .AddFilter(u => u.Birthday, query.Filter!.EndBirthdayFilter!)
                .Build();

        int paginationLimit = query.Pagination!.Limit!.Value;
        int databaselimit = paginationLimit + 1;

        ICollection<UserEntity> result = await service.GetAllAsync(
            predicate,
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