namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination.OffsetBased;

public record PagedResponse<TType> : BaseResponse
{
    public ICollection<TType>? Data { get; init; }

    public PageInfoResponse? Pagination { get; init; }
}