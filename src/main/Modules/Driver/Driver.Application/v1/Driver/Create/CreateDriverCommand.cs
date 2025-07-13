using JacksonVeroneze.NET.Result;
using TaxiService.Common.SharedKernel.Enums;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Create;

public sealed record CreateDriverCommand
    : IRequest<Result<CreateDriverCommandResponse>>
{
    public string? FullName { get; init; }

    public DateOnly? Birthday { get; init; }

    public Gender? Gender { get; init; }

    public string? Phone { get; init; }

    public string? Document { get; init; }

    public string? Email { get; init; }
}
