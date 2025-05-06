using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
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
}