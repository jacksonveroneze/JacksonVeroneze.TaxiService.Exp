namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination.CursorBased;

public record PagedCursorResponse<TType> : BaseResponse
{
    public ICollection<TType>? Data { get; init; }

    public PageInfoCursorResponse? Pagination { get; init; }
}