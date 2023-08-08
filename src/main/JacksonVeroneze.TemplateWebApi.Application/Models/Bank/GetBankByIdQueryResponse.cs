using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.Bank;

public record GetBankByIdQueryResponse : OkResponse
{
    [JsonPropertyName("data")]
    public BankResponse? Data { get; init; }
}
