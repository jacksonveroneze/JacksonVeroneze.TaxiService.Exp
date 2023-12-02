using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User.Email;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;

public sealed record CreateEmailCommand :
    IRequest<Result<CreateEmailCommandResponse>>
{
    [FromRoute(Name = "userId")]
    public Guid Id { get; init; }

    [FromBody]
    public CreateEmailBodyCommand? Body { get; init; }
}

public class CreateEmailBodyCommand
{
    [JsonPropertyName("email")]
    public string? Email { get; init; }
}
