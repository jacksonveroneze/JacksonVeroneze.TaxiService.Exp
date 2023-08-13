namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public abstract record DataResponse<TType> : BaseResponse
{
    [JsonPropertyName("data")]
    public TType? Data { get; init; }
}
