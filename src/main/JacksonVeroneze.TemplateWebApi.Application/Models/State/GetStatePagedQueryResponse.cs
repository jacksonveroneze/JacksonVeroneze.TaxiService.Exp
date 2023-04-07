using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.State;

public record GetStatePagedQueryResponse : OkResponse
{
    [JsonPropertyName("data")]
    public ICollection<StateResponse>? Data { get; set; }
}