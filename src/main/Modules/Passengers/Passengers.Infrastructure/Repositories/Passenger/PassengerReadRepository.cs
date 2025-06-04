using System.Linq.Expressions;

using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Pagination.Cursor.Extensions;

using Microsoft.Extensions.Logging;

using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passengers.Application.Common;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passengers.Application.v1.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Filters;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Models;
using TaxiService.Modules.Passengers.Infrastructure.Builders.Filters;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.Repositories.Passenger;

[ExcludeFromCodeCoverage]
public class PassengerReadRepository(
    ILogger<PassengerReadRepository> logger,
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

        logger.LogExistsByValue(ModuleContext.ContextName,
            nameof(PassengerReadRepository),
            nameof(ExistsByIdAsync),
            id.ToString(), exists);
        
        return exists;
    }

    public async Task<PassengerDto?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        Expression<Func<PassengerDto, bool>> spec
            = entity => entity.Id == id;

        PassengerDto? result = await service
            .GetByIdAsync(spec, cancellationToken);

        logger.LogGetById(ModuleContext.ContextName,
            nameof(PassengerReadRepository),
            nameof(GetByIdAsync),
            id, result != null);

        return result;
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
