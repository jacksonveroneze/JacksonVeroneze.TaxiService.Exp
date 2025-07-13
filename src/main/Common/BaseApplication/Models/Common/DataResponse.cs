namespace TaxiService.Common.BaseApplication.Models.Common;

public abstract record DataResponse<TType>
{
    public TType? Data { get; init; }
}
