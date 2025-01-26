namespace TaxiService.Common.BaseApplication.Models.Base;

public abstract record DataResponse<TType> : BaseResponse
{
    public TType? Data { get; init; }
}