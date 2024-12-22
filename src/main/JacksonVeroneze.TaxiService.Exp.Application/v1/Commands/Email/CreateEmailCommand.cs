using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Email;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Email;

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