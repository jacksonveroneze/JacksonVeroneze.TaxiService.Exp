using System.Linq.Expressions;

using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;

using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain.Filters;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.Driver;

[ExcludeFromCodeCoverage]
public class DriverReadRepository(
    IEfCoreRepository<DriverDto, DriversReadDbContext> service)
    : IDriverReadRepository
{
    public Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Expression<Func<DriverDto, bool>> spec
            = entity => entity.Id == id;

        return service.AnyAsync(spec, cancellationToken);
    }

    public Task<bool> ExistsByEmailAndDocumentAsync(
        string email,
        string document,
        CancellationToken cancellationToken)
    {
        return service.AnyAsync(
            conf => conf.Email == email || conf.Document == document,
            cancellationToken);
    }

    public Task<DriverDto?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        Expression<Func<DriverDto, bool>> spec
            = entity => entity.Id == id;

        return service.GetByIdAsync(spec,
            cancellationToken);
    }

    public async Task<Page<DriverDto>> GetPagedAsync(
        DriverPagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        bool parseWithSuccess =
            Guid.TryParse(filter.Pagination!.Cursor, out Guid id);

        Expression<Func<DriverDto, bool>> spec =
            spec => !parseWithSuccess || spec.Id > id;

        int paginationLimit = filter.Pagination!.Limit!.Value;
        int databaselimit = paginationLimit + 1;

        ICollection<DriverDto> result = await service.GetAllAsync(
            spec,
            order => order.FullName,
            databaselimit,
            cancellationToken: cancellationToken);

        DriverDto[] data = result
            .Take(paginationLimit)
            .ToArray();

        bool hasMore = result.Count > paginationLimit;

        string? currentCursor = hasMore
            ? data.LastOrDefault()?.Id.ToString()
            : null;

        PageInfo pageInfo = new(hasMore, currentCursor);

        return new Page<DriverDto>(data, pageInfo);
    }
}
