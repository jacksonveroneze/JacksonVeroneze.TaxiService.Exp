namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base;

public abstract record DataResponse<TType> : BaseResponse
{
    [JsonPropertyName("data")]
    public TType? Data { get; init; }
}
