using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.User;

public class CreateUserCommand : IRequest<IResult<CreateUserCommandResponse>>
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("birthday")]
    public DateTime? Birthday { get; init; }
}
