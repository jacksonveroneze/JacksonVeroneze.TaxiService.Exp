namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

public sealed class PageInfoResponse
{
    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; init; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; init; }

    [JsonPropertyName("total_elements")]
    public int TotalElements { get; init; }

    [JsonPropertyName("is_first_page")]
    public bool? IsFirstPage { get; init; }

    [JsonPropertyName("is_last_page")]
    public bool? IsLastPage { get; init; }

    [JsonPropertyName("has_next_page")]
    public bool? HasNextPage { get; init; }

    [JsonPropertyName("has_back_page")]
    public bool? HasBackPage { get; init; }

    [JsonPropertyName("next_page")]
    public int? NextPage { get; init; }

    [JsonPropertyName("back_page")]
    public int? BackPage { get; init; }
}
