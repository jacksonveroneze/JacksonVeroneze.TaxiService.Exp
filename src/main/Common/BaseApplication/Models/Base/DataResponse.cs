namespace TaxiService.Common.BaseApplication.Models.Base;

public abstract record DataResponse<TType>
{
    public TType? Data { get; init; }
}
