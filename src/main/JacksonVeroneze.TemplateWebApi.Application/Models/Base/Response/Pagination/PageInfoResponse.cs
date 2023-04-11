namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

public class PageInfoResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_elements")]
    public int TotalElements { get; set; }
}