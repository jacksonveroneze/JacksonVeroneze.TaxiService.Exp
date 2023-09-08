using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User.Email;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;

public sealed record CreateEmailCommand :
    IRequest<IResult<CreateEmailCommandResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }
}
