using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Email;

public sealed record CreatePassengerCommand
    : IRequest<Result<CreateEmailCommandResponse>>
{
    public Guid PassengerId { get; init; }

    public CreateBody? Body { get; init; }
}

public class CreateBody
{
    public string? Email { get; init; }
}
