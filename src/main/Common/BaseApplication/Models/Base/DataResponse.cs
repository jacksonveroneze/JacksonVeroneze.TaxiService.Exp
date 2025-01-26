namespace TaxiService.Common.Application.Models.Base;

public abstract record DataResponse<TType> : BaseResponse
{
    public TType? Data { get; init; }
}