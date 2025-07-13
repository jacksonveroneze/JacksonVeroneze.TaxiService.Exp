namespace TaxiService.Common.BaseApplication.Models.Pagination.Cursor;

public record PagedResponse<TType>
{
    public ICollection<TType>? Data { get; init; }

    public PageInfoResponse? Pagination { get; init; }
}
