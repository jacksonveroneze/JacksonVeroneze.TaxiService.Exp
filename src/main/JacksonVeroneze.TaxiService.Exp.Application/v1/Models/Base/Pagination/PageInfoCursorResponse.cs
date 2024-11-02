namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination;

public sealed class PageInfoCursorResponse
{
    [JsonPropertyName("has_more")]
    public bool? HasMore { get; set; }

    [JsonPropertyName("cursor")]
    public string? Cursor { get; set; }
}