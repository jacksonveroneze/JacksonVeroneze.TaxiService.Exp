using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Pagination.Cursor.Extensions;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Filters;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Driver.Domain;
using TaxiService.Modules.Driver.Infrastructure.Builders.Filters;
using TaxiService.Modules.Driver.Infrastructure.Contexts;

namespace TaxiService.Modules.Driver.Infrastructure.Repositories.Driver;

[ExcludeFromCodeCoverage]
public class DriverReadRepository(
    ILogger<DriverReadRepository> logger,
    IEfCoreRepository<DriverDto, DriversReadDbContext> service)
    : IDriverReadRepository
{
    public async Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Expression<Func<DriverDto, bool>> spec
            = entity => entity.Id == id;

        bool exists = await service
            .AnyAsync(spec, cancellationToken);

        logger.LogExistsByValue(
            ModuleConstants.ContextName,
            nameof(DriverReadRepository),
            nameof(ExistsByIdAsync),
            id.ToString(), exists);

        return exists;
    }

    [SuppressMessage("Globalization", "CA1309:Use ordinal string comparison")]
    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        bool exists = await service.AnyAsync(
            conf => conf.Email!.Equals(email.ToUpperInvariant()),
            cancellationToken);

        logger.LogExistsByValue(
            ModuleConstants.ContextName,
            nameof(DriverReadRepository),
            nameof(ExistsByIdAsync),
            email, exists);

        return exists;
    }

    public async Task<DriverDto?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        Expression<Func<DriverDto, bool>> spec
            = entity => entity.Id == id;

        DriverDto? result = await service
            .GetByIdAsync(spec, cancellationToken);

        logger.LogGetById(
            ModuleConstants.ContextName,
            nameof(DriverReadRepository),
            nameof(GetByIdAsync),
            id, result != null);

        return result;
    }

    public async Task<ICollection<DriverDto>> GetAsync(
        DriverFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Expression<Func<DriverDto, bool>> expression =
            DriverFilterBuilder.Create(filter).Build();

        ICollection<DriverDto> result =
            await service.GetAllAsync(
                expression, cancellationToken);

        return result;
    }

    public async Task<Page<DriverDto>> GetPagedAsync(
        DriverPagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Expression<Func<DriverDto, bool>> expression =
            DriverPagedFilterBuilder.Create(filter).Build();

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
