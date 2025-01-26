namespace TaxiService.Modules.Rides.Application.Models.Base;

public abstract record CollectionResponse<TType>
    : DataResponse<ICollection<TType>>;