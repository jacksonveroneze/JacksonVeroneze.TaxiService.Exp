namespace JacksonVeroneze.TaxiService.Exp.Application.Models.Base;

public abstract record CollectionResponse<TType>
    : DataResponse<ICollection<TType>>;