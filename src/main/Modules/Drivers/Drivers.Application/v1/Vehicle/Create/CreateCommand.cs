using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Vehicle.Create;

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