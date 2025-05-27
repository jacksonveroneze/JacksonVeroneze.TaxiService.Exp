using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Email.Create;

public sealed record CreateEmailCommand
    : IRequest<Result<CreateEmailCommandResponse>>
{
    public Guid PassengerId { get; init; }

    public CreateBody? Body { get; init; }
}

public class CreateBody
{
    public string? Email { get; init; }
}
