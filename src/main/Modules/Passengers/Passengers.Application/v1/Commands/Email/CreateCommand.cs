using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Email;

public sealed record CreateCommand
    : IRequest<Result<CreateCommandResponse>>
{
    public Guid UserId { get; init; }

    public CreateBody? Body { get; init; }
}

public class CreateBody
{
    public string? Email { get; init; }
}