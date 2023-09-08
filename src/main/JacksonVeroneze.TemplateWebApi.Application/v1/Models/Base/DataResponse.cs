namespace JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

public abstract record DataResponse<TType> : BaseResponse
{
    [JsonPropertyName("data")]
    public TType? Data { get; init; }
}