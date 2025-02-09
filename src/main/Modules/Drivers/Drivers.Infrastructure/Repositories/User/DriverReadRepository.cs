using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Specifications.Driver;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.User;

[ExcludeFromCodeCoverage]
public class DriverReadRepository(
    IEfCoreRepository<DriverEntity, DriversDbContext> service) : IDriverReadRepository
{
    public Task<DriverEntity?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        DriverByIdSpecification specByDriverId = new(id);

        return service.GetByIdAsync(specByDriverId.ToExpression(),
            cancellationToken);
    }
}