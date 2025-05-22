using System.Linq.Expressions;

using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Pagination.Cursor.Extensions;

using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain.Filters;
using TaxiService.Modules.Drivers.Infrastructure.Builders.Filters;
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

    [SuppressMessage("Globalization", "CA1309:Use ordinal string comparison")]
    public Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return service.AnyAsync(
            conf => conf.Email!.Equals(email.ToUpperInvariant()),
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

        Expression<Func<DriverDto, bool>> expression =
            DriverFilterBuilder.Create(filter).Build();

        int pageLimit = filter.Pagination!.Limit!.Value;
        int databaselimit = pageLimit + 1;

        ICollection<DriverDto> result =
            await service.GetAllAsync(
                expression,
                order => order.Id,
                databaselimit,
                cancellationToken: cancellationToken);

        return result.ToPage(
            filter.Pagination, d => d.Id.ToString());
    }
}
