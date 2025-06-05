using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;

public sealed record DriverDto
{
    public Guid? Id { get; init; }

    public string? FullName { get; init; }

    public string? Document { get; init; }

    public string? Email { get; init; }

    public DateOnly? Birthday { get; init; }

    public Gender? Gender { get; init; }

    public DriverStatus? Status { get; init; }

    public DateTime? DeletedAt { get; init; }
}
