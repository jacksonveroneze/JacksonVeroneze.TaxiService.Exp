using JacksonVeroneze.TemplateWebApi.Application.Models.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Client;

public class CreateClientCommand : IRequest<IResult<CreateClientCommandResponse>>
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }
}
