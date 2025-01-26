using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Email;

public sealed record CreateEmailCommand
    : IRequest<Result<CreateEmailCommandResponse>>
{
    public Guid UserId { get; init; }

    public CreateEmailBodyCommand? Body { get; init; }
}

public class CreateEmailBodyCommand
{
    public string? Email { get; init; }
}