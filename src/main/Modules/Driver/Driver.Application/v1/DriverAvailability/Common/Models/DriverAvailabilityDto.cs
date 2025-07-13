using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Application.v1.DriverAvailability.Common.Models;

public sealed record DriverAvailabilityDto
{
    public Guid Id { get; init; }

    public Guid DriverId { get; init; }

    public DriverAvailabilityStatus Status { get; init; }
    
    public DateTime? CreatedAt { get; init; }
    public DateTime? DeletedAt { get; init; }
}
