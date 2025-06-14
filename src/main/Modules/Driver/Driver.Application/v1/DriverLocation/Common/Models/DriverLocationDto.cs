using TaxiService.Common.BaseApplication.Models.Models.Common;

namespace TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Models;

public sealed record DriverLocationDto
{
    public Guid Id { get; init; }
    
    public Guid DriverId { get; init; }
    
    public required CoordinateResponse Location { get; init; }
    
    public DateTime? DeletedAt { get; init; }
}
