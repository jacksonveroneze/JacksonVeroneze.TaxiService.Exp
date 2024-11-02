namespace JacksonVeroneze.TaxiService.Exp.Domain.Results;

public class CursorPaginationParameters
{
    public string? Cursor { get; set; }

    public int? Limit { get; set; }
}