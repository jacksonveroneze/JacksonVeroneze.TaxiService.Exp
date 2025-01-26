namespace TaxiService.Modules.Passengers.Application.Models.Base;

public abstract record CollectionResponse<TType>
    : DataResponse<ICollection<TType>>;