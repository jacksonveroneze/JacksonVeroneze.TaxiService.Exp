namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination.CursorBased;

public sealed record PageInfoCursorResponse
{
    public bool? HasMore { get; init; }

    public string? Cursor { get; init; }
}