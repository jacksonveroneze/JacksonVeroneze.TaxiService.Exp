namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination.CursorBased;

public sealed record PageInfoCursorResponse
{
    public bool? HasMore { get; set; }

    public string? Next { get; set; }

    public string? Back { get; set; }
}