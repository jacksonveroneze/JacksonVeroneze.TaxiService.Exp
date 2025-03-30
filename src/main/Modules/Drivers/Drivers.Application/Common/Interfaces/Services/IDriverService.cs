using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;

public interface IDriverService
{
    Task<Result<bool>> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken);
    
    Task<Result<bool>> ExistsAndIsActiveAsync(
        Guid id,
        CancellationToken cancellationToken);
    
    Task<Result<DriverEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}