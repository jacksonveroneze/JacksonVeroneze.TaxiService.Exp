using System.Linq.Expressions;

using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;
using TaxiService.Modules.Passengers.Domain.Specifications.Passenger;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.Repositories.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerReadRepository(
    IEfCoreRepository<PassengerEntity, PassengersDbContext> service) : IPassengerReadRepository
{
    public Task<PassengerEntity?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        PassengerByIdSpecification specByPassengerId = new(id);

        return service.GetByIdAsync(specByPassengerId.ToExpression(),
            cancellationToken);
    }

    public async Task<Page<PassengerEntity>> GetPagedAsync(
        PassengerPagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Guid? id = !string.IsNullOrEmpty(filter.Pagination?.Cursor)
            ? Guid.Parse(filter.Pagination!.Cursor)
            : null;

        Expression<Func<PassengerEntity, bool>> spec =
            spec => !id.HasValue || spec.Id > id;

        int paginationLimit = filter.Pagination!.Limit!.Value;
        int databaselimit = paginationLimit + 1;

        ICollection<PassengerEntity> result = await service.GetAllAsync(
            spec,
            order => order.Name.Value,
            databaselimit,
            cancellationToken: cancellationToken);

        IList<PassengerEntity> data = result.Take(paginationLimit).ToList();
        bool hasMore = result.Count > paginationLimit;
        string cursor = result.LastOrDefault()?.Id.ToString() ?? string.Empty;

        PageInfo pageInfo = new(hasMore, cursor, null);

        return new Page<PassengerEntity>(data, pageInfo);
    }
}
