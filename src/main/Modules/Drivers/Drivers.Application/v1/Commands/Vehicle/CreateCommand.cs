using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Enums;
using TaxiService.Modules.Drivers.Application.v1.Models.Vehicle;

namespace TaxiService.Modules.Drivers.Application.v1.Commands.Vehicle;

public sealed record CreateCommand
    : IRequest<Result<CreateCommandResponse>>
{
    public Guid DriverId { get; init; }

    public string? Model { get; init; }

    public string? Brand { get; init; }

    public string? Plate { get; init; }

    public string? Color { get; init; }

    public int? Year { get; init; }
}