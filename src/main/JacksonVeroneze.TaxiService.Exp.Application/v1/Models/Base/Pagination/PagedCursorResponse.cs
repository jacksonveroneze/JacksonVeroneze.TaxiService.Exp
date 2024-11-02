namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination;

public record PagedCursorResponse<TType> : BaseResponse
{
    [JsonPropertyName("data")]
    public ICollection<TType>? Data { get; init; }

    [JsonPropertyName("pagination")]
    public PageInfoCursorResponse? Pagination { get; set; }
}