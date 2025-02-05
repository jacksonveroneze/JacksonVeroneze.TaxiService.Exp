using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Cursor;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Filters;
using TaxiService.Modules.Drivers.Domain.Specifications.Driver;
using TaxiService.Modules.Drivers.Infrastructure.EfCore.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.EfCore.Repositories.User;

[ExcludeFromCodeCoverage]
public class DriverReadRepository(
    IEfCoreRepository<DriverEntity, DriversDbContext> service) : IDriverReadRepository
{
    public Task<DriverEntity?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        DriverByIdSpecification specByDriverId = new(id);

        return service.GetByIdAsync(specByDriverId.ToExpression(),
            cancellationToken);
    }
}