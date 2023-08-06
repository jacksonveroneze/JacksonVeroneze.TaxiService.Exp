using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.Old.State;

public record GetStateByIdQueryQueryResponse : OkResponse
{
    [JsonPropertyName("data")]
    public StateResponse? Data { get; init; }
}
