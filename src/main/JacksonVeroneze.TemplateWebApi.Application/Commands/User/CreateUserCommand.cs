using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.User;

public sealed record CreateUserCommand :
    IRequest<IResult<CreateUserCommandResponse>>
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("birthday")]
    public DateTime? Birthday { get; init; }

    [JsonPropertyName("gender")]
    public Gender? Gender { get; init; }
}
