using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;

public interface IVehicleService
{
    Task<Result<VehicleEntity>> GetEntityByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
    
    Task<Result<VehicleDto>> GetDtoByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}
