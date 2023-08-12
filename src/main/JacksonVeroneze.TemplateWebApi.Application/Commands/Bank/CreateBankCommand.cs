using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

public class CreateBankCommand : IRequest<Result<BaseResponse>>
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
