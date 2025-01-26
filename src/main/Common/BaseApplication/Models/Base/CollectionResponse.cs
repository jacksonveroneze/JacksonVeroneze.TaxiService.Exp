namespace TaxiService.Common.Application.Models.Base;

public abstract record CollectionResponse<TType>
    : DataResponse<ICollection<TType>>;