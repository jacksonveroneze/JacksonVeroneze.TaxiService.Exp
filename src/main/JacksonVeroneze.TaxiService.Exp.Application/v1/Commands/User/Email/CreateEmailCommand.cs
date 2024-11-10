using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User.Email;

public sealed record CreateEmailCommand :
    IRequest<Result<CreateEmailCommandResponse>>
{
    public Guid Id { get; init; }

    public CreateEmailBodyCommand? Body { get; init; }
}

public class CreateEmailBodyCommand
{
    public string? Email { get; init; }
}