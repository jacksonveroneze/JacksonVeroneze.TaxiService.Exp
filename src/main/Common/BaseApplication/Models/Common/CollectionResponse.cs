namespace TaxiService.Common.BaseApplication.Models.Common;

public abstract record CollectionResponse<TType>
    : DataResponse<ICollection<TType>>;
