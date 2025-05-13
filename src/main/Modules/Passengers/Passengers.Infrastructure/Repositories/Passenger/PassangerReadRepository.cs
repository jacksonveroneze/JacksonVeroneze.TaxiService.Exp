using System.Linq.Expressions;

using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;
using TaxiService.Modules.Passengers.Domain.Specifications.Passanger;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.Repositories.Passenger;

[ExcludeFromCodeCoverage]
public class PassangerReadRepository(
    IEfCoreRepository<PassangerEntity, PassengersDbContext> service) : IPassangerReadRepository
{
    public Task<PassangerEntity?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        PassangerByIdSpecification specByPassangerId = new(id);

        return service.GetByIdAsync(specByPassangerId.ToExpression(),
            cancellationToken);
    }

    public async Task<Page<PassangerEntity>> GetPagedAsync(
        PassangerPagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Guid? id = !string.IsNullOrEmpty(filter.Pagination?.Cursor)
            ? Guid.Parse(filter.Pagination!.Cursor)
            : null;

        Expression<Func<PassangerEntity, bool>> spec =
            spec => !id.HasValue || spec.Id > id;

        int paginationLimit = filter.Pagination!.Limit!.Value;
        int databaselimit = paginationLimit + 1;

        ICollection<PassangerEntity> result = await service.GetAllAsync(
            spec,
            order => order.Name.Value,
            databaselimit,
            cancellationToken: cancellationToken);

        IList<PassangerEntity> data = result.Take(paginationLimit).ToList();
        bool hasMore = result.Count > paginationLimit;
        string cursor = result.LastOrDefault()?.Id.ToString() ?? string.Empty;

        PageInfo pageInfo = new(hasMore, cursor, null);

        return new Page<PassangerEntity>(data, pageInfo);
    }
}
