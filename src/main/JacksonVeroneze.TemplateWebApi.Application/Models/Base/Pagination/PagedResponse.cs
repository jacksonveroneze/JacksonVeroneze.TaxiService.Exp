namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Pagination;

public record PagedResponse<TType> : BaseResponse
{
    [JsonPropertyName("data")]
    public ICollection<TType>? Data { get; init; }

    [JsonPropertyName("pagination")]
    public PageInfoResponse? Pagination { get; init; }
}
