namespace TaxiService.Common.BaseApplication.Models.Base;

public abstract record CollectionResponse<TType>
    : DataResponse<ICollection<TType>>;
