namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public record DataResponse<TType> : OkResponse
{
    [JsonPropertyName("data")]
    public TType? Data { get; init; }
}
