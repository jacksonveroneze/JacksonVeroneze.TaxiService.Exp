namespace TaxiService.Common.Application.Models.Base.Pagination.Cursor;

public sealed record PageInfoResponse
{
    public bool? HasMore { get; init; }

    public string? Cursor { get; init; }
}