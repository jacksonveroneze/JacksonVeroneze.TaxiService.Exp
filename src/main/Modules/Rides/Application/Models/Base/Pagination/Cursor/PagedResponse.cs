namespace TaxiService.Modules.Rides.Application.Models.Base.Pagination.Cursor;

public record PagedResponse<TType> : BaseResponse
{
    public ICollection<TType>? Data { get; init; }

    public PageInfoResponse? Pagination { get; init; }
}