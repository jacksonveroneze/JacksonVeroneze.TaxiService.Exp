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
        ExpressionStarter<UserEntity> predicate =
            new ExpressionBuilder<UserEntity>()
                .AddFilter(u => u.Id, GuidFilter.Create(id))
                .Build();

        return service.GetByIdAsync(predicate, cancellationToken);
    }

    public async Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Guid cursorId = Guid.Parse(filter!.Pagination!.Cursor!);

        ExpressionStarter<UserEntity> predicate =
            new ExpressionBuilder<UserEntity>()
                .AddFilter(u => u.Id, GuidFilter.Create(cursorId, GuidOperator.Gte))
                .AddFilter(u => u.Name.Value!, filter.NameFilter)
                .AddFilter(u => u.Status, filter.StatusFilter)
                .AddFilter(u => u.Birthday, filter.StartBirthdayFilter)
                .AddFilter(u => u.Birthday, filter.EndBirthdayFilter)
                .Build();

        int paginationLimit = filter.Pagination!.Limit!.Value;
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