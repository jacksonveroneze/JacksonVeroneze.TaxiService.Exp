using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

public class CreateBankCommand : IRequest<IResult<CreateBankCommandResponse>>
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
