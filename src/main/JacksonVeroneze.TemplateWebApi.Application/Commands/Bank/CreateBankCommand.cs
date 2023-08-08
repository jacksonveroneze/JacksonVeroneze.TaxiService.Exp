using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

public record CreateBankCommand : IRequest<BaseResponse>
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
