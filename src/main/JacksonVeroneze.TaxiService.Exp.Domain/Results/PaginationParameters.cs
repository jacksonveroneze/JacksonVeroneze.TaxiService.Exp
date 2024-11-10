namespace JacksonVeroneze.TaxiService.Exp.Domain.Results;

public record CursorPaginationParameters
{
    public string? Cursor { get; init; }

    public int? Limit { get; init; }
}