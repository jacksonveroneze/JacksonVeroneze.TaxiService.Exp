using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Create;

public sealed record CreateCommand
    : IRequest<Result<CreateCommandResponse>>
{
    public string? FullName { get; init; }

    public DateOnly? Birthday { get; init; }

    public Gender? Gender { get; init; }

    public string? Phone { get; init; }

    public string? Document { get; init; }

    public string? Email { get; init; }
}