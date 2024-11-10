namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

public record CollectionResponse<TType>
    : DataResponse<ICollection<TType>>;