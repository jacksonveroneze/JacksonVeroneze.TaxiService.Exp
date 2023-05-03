namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

public record PagedResponse<TType> : OkResponse
{
    [JsonPropertyName("data")]
    public ICollection<TType>? Data { get; init; }

    [JsonPropertyName("pagination")]
    public PageInfoResponse? Pagination { get; init; }
}