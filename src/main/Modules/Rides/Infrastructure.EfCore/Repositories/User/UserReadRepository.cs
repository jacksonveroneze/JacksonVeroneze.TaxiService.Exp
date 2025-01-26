using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Pagination.Cursor.Extensions;
using LinqKit;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Filters;
using TaxiService.Modules.Rides.Domain.Util;
using TaxiService.Modules.Rides.Domain.Util.Operators;
using TaxiService.Modules.Rides.Infrastructure.EfCore.Repositories.Util;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Repositories.User;

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

        ExpressionStarter<UserEntity> predicate =
            FactoryFilterPage(query);

        (UserEntity[] Data, bool HasMore, UserEntity? Last) result =
            await service.GetPagedCursorAsync(
                query.Pagination!,
                predicate,
                order => order.Name.Value,
                cancellationToken);

        string? cursor = result.HasMore
            ? result.Last?.Id.ToString()
            : null;

        return result.Data.ToPage(result.HasMore, cursor);
    }

    private static ExpressionStarter<UserEntity> FactoryFilterPage(
        UserPagedQuery query)
    {
        ExpressionsBuilder<UserEntity> predicate = new();

        if (query.Pagination!.Cursor is not null)
        {
            Guid cursorId = Guid.Parse(query.Pagination!.Cursor!);

            predicate.AddFilter(u => u.Id,
                GuidFilter.Create(cursorId, FilterOperator.Gte));
        }

        UserPagedFilter filter = query.Filter!;

        predicate
            .AddFilter(u => u.Name.Value!, filter.NameFilter)
            .AddFilter(u => u.Status, filter.StatusFilter)
            .AddFilter(u => u.Birthday, filter.StartBirthdayFilter)
            .AddFilter(u => u.Birthday, filter.EndBirthdayFilter);

        return predicate.Build();
    }
}