using System.Linq.Expressions;

using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Pagination.Cursor.Extensions;

using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Passenger;
using TaxiService.Modules.Passengers.Domain.Filters;
using TaxiService.Modules.Passengers.Infrastructure.Builders.Filters;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.Repositories.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerReadRepository(
    IEfCoreRepository<PassengerDto, PassengersReadDbContext> service) 
    : IPassengerReadRepository
{
    public async Task<bool> ExistsByIdAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        Expression<Func<PassengerDto, bool>> spec
            = entity => entity.Id == id;

        bool exists = await service
            .AnyAsync(spec, cancellationToken);

        return exists;
    }

    public Task<PassengerDto?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        Expression<Func<PassengerDto, bool>> spec
            = entity => entity.Id == id;

        return service.GetByIdAsync(spec,
            cancellationToken);
    }

    public async Task<ICollection<PassengerDto>> GetAsync(
        PassengerFilter filter, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Expression<Func<PassengerDto, bool>> expression =
            PassengerFilterBuilder.Create(filter).Build();

        ICollection<PassengerDto> result =
            await service.GetAllAsync(
                expression,
                cancellationToken: cancellationToken);

        return result;
    }

    public async Task<Page<PassengerDto>> GetPagedAsync(
        PassengerPagedFilter filter, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Expression<Func<PassengerDto, bool>> expression =
            PassengerPagedFilterBuilder.Create(filter).Build();

        int pageLimit = filter.Pagination!.Limit!.Value;
        int databaselimit = pageLimit + 1;

        ICollection<PassengerDto> result =
            await service.GetAllAsync(
                expression,
                order => order.Id,
                databaselimit,
                cancellationToken: cancellationToken);

        return result.ToPage(
            filter.Pagination, d => d.Id.ToString());
    }
}
