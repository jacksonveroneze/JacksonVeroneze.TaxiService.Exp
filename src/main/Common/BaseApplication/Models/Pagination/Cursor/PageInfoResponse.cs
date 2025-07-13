namespace TaxiService.Common.BaseApplication.Models.Pagination.Cursor;

public sealed record PageInfoResponse
{
    public bool? HasMore { get; init; }

    public string? NextCursor { get; init; }

    public string? PreviousCursor { get; init; }
}
