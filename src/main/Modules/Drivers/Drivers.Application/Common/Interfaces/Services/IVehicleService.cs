using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;

public interface IVehicleService
{
    Task<Result<VehicleEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}
