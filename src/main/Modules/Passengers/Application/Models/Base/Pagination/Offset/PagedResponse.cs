namespace TaxiService.Modules.Passengers.Application.Models.Base.Pagination.Offset;

public record PagedResponse<TType> : BaseResponse
{
    public ICollection<TType>? Data { get; init; }

    public PageInfoResponse? Pagination { get; init; }
}