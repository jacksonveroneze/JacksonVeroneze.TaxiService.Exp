namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

public class PageInfoResponse
{
    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; init; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; init; }

    [JsonPropertyName("total_elements")]
    public int TotalElements { get; init; }
}