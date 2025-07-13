using TaxiService.Common.SharedKernel.Enums;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.PublicApi.v1.Models;

public sealed record DriverPublicApiDto
{
    public Guid Id { get; init; }

    public required string FullName { get; init; }

    public required string Document { get; init; }

    public required string Email { get; init; }

    public DateOnly Birthday { get; init; }

    public Gender Gender { get; init; }

    public DriverStatus Status { get; init; }
}
